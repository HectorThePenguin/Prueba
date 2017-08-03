using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SIE.Base.Exepciones;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TipoCorral = SIE.Services.Info.Enums.TipoCorral;
using TipoOrganizacion = SIE.Services.Info.Enums.TipoOrganizacion;
using System.Transactions;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para CorteGanado.xaml
    /// </summary>
    public partial class CorteGanado
    {

        #region Atributos
        private EntradaGanadoInfo BusquedaPartidas { get; set; }
        private EntradaGanadoInfo EntradaSeleccionada { get; set; }
        InterfaceSalidaAnimalInfo _interfaceSalidoAnimalInfo;
        private SerialPortManager _spManager;
        private SerialPortManager _spManagerTermo;
        private SerialPortManager _spManagerRFID;
        private BasculaCorteSection _configRFIDCorte;
        private BasculaCorteSection _configBasculaCorte;
        private BasculaCorteSection _configTermometroCorte;
        private CorralInfo _corralInfoGen;
        private LoteInfo loteOrigen;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private int organizacionID;
        private int usuario;

        private bool _pesoTomado;
        private bool _tempTomada;
        private DispatcherTimer _timerListeningPort;
        private int _codigoTratamientoTemperatura;
        private double _maxTemperaturaAnimal;
        private AnimalInfo animalActual;
        double _lastTemp, maxTemp;
        int _countTemp;
        TrampaInfo trampaInfo = null;
        private bool banderaFoco = false;
        IList<TratamientoInfo> _listaTratamientos;
        private bool termometroConectado;
        private bool rfidConectado;
        private bool basculaConectada;
        private bool BanderaNoIndividual;
        private bool CtrlPegar;
        private String TextoAnterior;
        private bool FocoPeso;
        AlmacenInfo almacenInfo;
        private string temperaturaCapturadaGlobal = string.Empty;
        private string pesoCapturadoGlobal = string.Empty;

        private string pesoPercial = string.Empty;
        private int vecesPesoCotejado = 0;

        private string rfidCapturadoGlobal = string.Empty;
        private IList<CabezasSobrantesPorEntradaInfo> entradasCabezasSobrantes = null;
        private bool esCabezasSobrante = false;
    

        private uint start = 0;
        private uint stop = 0;
        private bool aplicaScanner;
        private bool bandBack = true;
        private bool capturaInmediata = false;
        private string _pesoCorte;
        #endregion

        #region Constructor
        public CorteGanado()
        {
            try
            {


                FocoPeso = false;
                CtrlPegar = false;
                TextoAnterior = string.Empty;
                BanderaNoIndividual = false;
                InitializeComponent();
                organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                dtpFechaCorte.SelectedDate = DateTime.Now;
                termometroConectado = false;
                basculaConectada = false;
                rfidConectado = false;
                this._pesoCorte = string.Empty;
                //Se valida que existan Programacion corte de ganado
                if (!ExisteProgramacionCorteGanado())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_NoExisteProgramacionCorteGanado,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    DeshabilitarControles(false);
                    return;
                }

                //Se valida que existan Corrales Disponibles
                if (!ExisteCorralesDisponibles())
                {
                    DeshabilitarControles(false);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_NoExisteCorralesDisponibles,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    return;
                }

                //se valida que existan trampas configuradas
                if (!ExistenTrampas())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_NoExistenTrampas,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    DeshabilitarControles(false);
                    return;
                }
                UserInitialization();
                //se valida que existan un almacen para la trampa configurada
                Inicializarcontroles(false);
                if (!ExistenAlmacenParaTrampa())
                {
                    DeshabilitarControles(false);
                    return;
                }
                
                ConfiguracionParametrosPL parametrosPL = new ConfiguracionParametrosPL();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCargaInicial,
                                      MessageBoxButton.OK, MessageImage.Error);
            }

        }
        #endregion

        #region Eventos

        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();

        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            double val;
            try
            {
                string strEnd = _spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != string.Empty)
                {
                    //this._spManager.StopListening();
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    this.pesoPercial = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");
                    //Aquie es para que se este reflejando la bascula en el display
                    if (!this._pesoTomado)
                    {
                        Dispatcher.BeginInvoke(new Action(CapturaPesoEnDisplay),
                                           DispatcherPriority.Normal);
                    }
                    else
                    {
                        this._timerListeningPort.Stop();
                    }
                }
            }
            catch (FormatException ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.CorteGanado_ErrorCapturaBascula,
                                     MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaBascula,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManager_NewSerialDataRecievedTermo(object sender, SerialDataEventArgs e)
        {
            double val;
            try
            {
                string strEnd = _spManagerTermo.ObtenerLetura(e.Data);
                if (strEnd.Trim() != string.Empty)
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);
                    if (val < 10.0) return;
                    if (_lastTemp == val)
                    {
                        _countTemp++;
                    }
                    else
                    {
                        _countTemp = 0;
                    }
                    _lastTemp = val;
                    double dif = maxTemp - _lastTemp;
                    //asignamos la temperatura maxima
                    if (_lastTemp > maxTemp)
                    {
                        maxTemp = _lastTemp;
                        // damos formato al valor peso para presentarlo
                        temperaturaCapturadaGlobal = String.Format(CultureInfo.CurrentCulture, "{0:0.0}", maxTemp).Replace(",", ".");
                    }
                    //Modificado por: Andres Vejar: Se modifica para que obtenga la temperatura mas alta leida desde el termometro, cuando inicia a descender libera la lectura
                    //Solicitado por: Rosario RolastTempmero, Isabel Ramirez
                    //TODO: REvisar configuracion de temperatura base para termometro: Jesus Garcia
                    if (dif > 2.0d && _tempTomada == false)
                    {
                        if (maxTemp > 36)
                        {
                             _countTemp = 0;
                        Dispatcher.BeginInvoke(new Action(CapturarTemperaturaDeDisplay),
                                               DispatcherPriority.Background);
                        _tempTomada = true;
                        }
                    }
                    //probando cambio de metodo para que tome temperatura constante no solo la goblal
                    temperaturaCapturadaGlobal = String.Format(CultureInfo.CurrentCulture, "{0:0.0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(CapturarTemperaturaEnDisplay),
                                           DispatcherPriority.Background);
                }
            }
            catch (FormatException ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaTermometro,
                                      MessageBoxButton.OK, MessageImage.Error);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaTermometro,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManager_NewSerialDataRecievedRFID(object sender, SerialDataEventArgs e)
        {
            try
            {
                string strEnd = _spManagerRFID.ObtenerLeturaRFID(e.Data);
                if (strEnd.Trim() == string.Empty) return;

                rfidCapturadoGlobal = strEnd;
                //Aquie es para que se este reflejando la lectura en el display
                Dispatcher.BeginInvoke(new Action(CapturarAreteRFID),
                    DispatcherPriority.Background);
            }
            catch (FormatException ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaRFID,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaRFID,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtNoIndividual_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back || e.Key == Key.Delete)
                {
                    try
                    {
                        bandBack = false;
                        if (txtAreteMetalico.Text.Trim().Length == 0)
                        {
                            LimpiarCaptura(false);
                            LimpiaParcial();
                        }
                        bandBack = true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
                if (aplicaScanner)
                {
                    stop = GetTickCount();
                    uint elapsed = (stop - start);
                    if (elapsed > 30 && txtNoIndividual.Text.Length < 5)
                    {
                        txtNoIndividual.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaArete,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void OnLostFocusHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                if (banderaFoco == false)
                {
                    if (this.txtPesoCorte.Text != this._pesoCorte)
                    {
                        this._pesoCorte = this.txtPesoCorte.Text;
                        Dispatcher.BeginInvoke(new Action(ConsultarPeso), DispatcherPriority.Background, null);
                    }
                }
                else
                {
                    banderaFoco = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCalculoPeso,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtNoIndividual_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void TxtAreteMetalico_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        private void btnCancelar_LostFocus(object sender, RoutedEventArgs e)
        {
            txtNoIndividual.Focus();
        }

        private void txtNoIndividual_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtNoIndividual.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtNoIndividual.Text;
            }
        }

        private void txtNoIndividual_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtNoIndividual.Text))
            {
                CtrlPegar = false;
                txtNoIndividual.Text = txtNoIndividual.Text.Replace(" ", string.Empty);
            }
            else
            {
                if (CtrlPegar)
                {
                    txtNoIndividual.Text = TextoAnterior;
                    CtrlPegar = false;
                    TextoAnterior = String.Empty;
                }
            }
        }
        /// <summary>
        /// Evento para el boton cambiar de sexo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgCambiarSexo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!cboSexo.IsEnabled)
                {
                    if (cboSexo.SelectedItem == null || cboSexo.SelectedIndex == 0) return;

                    var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                    if (sexoGanado == Sexo.Hembra.ToString())
                    {
                        cboSexo.SelectedItem = Sexo.Macho.ToString();
                    }
                    else
                    {
                        cboSexo.SelectedItem = Sexo.Hembra.ToString();
                    }
                    ObtenerTipoGanado();
                    Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCambioSexo,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento closing de Corte de ganado 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            DisposeDispositivosConectados();

            if (termometroConectado)
            {
                _spManagerTermo.Dispose();
            }
        }

        /// <summary>
        /// Evento cargar pantalla y llena los combos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                if (cboSexo.SelectedIndex <= 0)
                {
                    CargarCboSexo();
                }
                if (cboCausaRechazo.SelectedIndex <= 0)
                {
                    CargarComboCausaRechazo();
                }
                if (cboImplantador.SelectedIndex <= 0)
                {
                    LlenarComboImplantador();
                }
                if (cboClasificacion.SelectedIndex <= 0)
                {
                    LlenarComboClasificacion();
                }
                if (cboPaletas.SelectedIndex <= 0)
                {
                    CargarCboPaletas();
                }

                UserInitialization();
                ProbarFuncionamientoDispositivos();

                txtCorralDestino.IsEnabled = false;
                imgBuscar.Focus();
                txtProveedor.IsEnabled = false;
                txtOrigen.IsEnabled = false;
                LeerConfiguracion();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCargaInicial,
                                      MessageBoxButton.OK, MessageImage.Error);
            }

        }
        /// <summary>
        /// Evento click en la lupa para consultar la ayuda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgBuscar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BusquedaPartidas = null;
                var busquedaPartidas = new CorteGanadoBusqueda();
                busquedaPartidas.InicializaPaginador();

                busquedaPartidas.Left = (ActualWidth - busquedaPartidas.Width) / 2;
                busquedaPartidas.Top = ((ActualHeight - busquedaPartidas.Height) / 2) + 132;
                busquedaPartidas.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                busquedaPartidas.ShowDialog();

                if (busquedaPartidas.ListaEntradaGanado != null && busquedaPartidas.ListaEntradaGanado.Count > 0)
                {
                    ObtenerDatosDePartidaSeleccionada(busquedaPartidas.ListaEntradaGanado[0]);
                    txtNoIndividual.IsEnabled = true;
                    txtNoIndividual.Focus();
                    txtCorralDestino.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorAlBuscarPartidas, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TimerLisnteingPort_Tick(object sender, EventArgs e)
        {
            try
            {
                var timer = (DispatcherTimer)sender;
                if (!this._pesoTomado)
                    this._spManager.StartListening(_configBasculaCorte.Puerto,
                                    _configBasculaCorte.Baudrate,
                                    _configBasculaCorte.Paridad,
                                    _configBasculaCorte.Databits,
                                    _configBasculaCorte.BitStop);
                else
                    timer.Stop();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Evento keyDown para validar el enter y el tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_KeyDown(object sender, KeyEventArgs e)
        {

            start = GetTickCount();


            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (_interfaceSalidoAnimalInfo != null)
                {
                    e.Handled = false;
                    ValidarFocusDespuesAreteMetalico();
                    return;
                }

                if (txtNoIndividual.Text.Trim().Length > 0)
                {
                    try
                    {
                        esCabezasSobrante = false;
                        var animalInfo = new AnimalInfo
                        {
                            Arete = txtNoIndividual.Text,
                            OrganizacionIDEntrada = organizacionID
                        };
                        Inicializarcontroles(true);
                        /* Se valida que exista el arete en Animal */
                        if (!ExisteAreteEnPartida(animalInfo))
                        {
                            //Validar si en las partidas se encuentra algun centro
                            if (ExiteCentroEnOrigen())
                            {
                                // Si existe una partida q no sea compra directa
                                var tipoAreteIndividual = 1;
                                ConsultarAreteAnimal(txtNoIndividual.Text, tipoAreteIndividual);
                            }
                            else
                            {
                                //Se da tratamiento a compras directas -- Se valida que el arete no pertenezca a un centro
                                var animalPl = new InterfaceSalidaAnimalPL();
                                _interfaceSalidoAnimalInfo = animalPl.ObtenerNumeroAreteIndividual(animalInfo.Arete,
                                                                                                   organizacionID);
                                if (_interfaceSalidoAnimalInfo == null)
                                {
                                    //Validar si se cuenta con los datos generales
                                    if (String.IsNullOrEmpty(txtProveedor.Text) &&
                                        String.IsNullOrEmpty(txtOrigen.Text))
                                    {
                                        ObtenerProveedor();
                                        dtpFechaRecepcion.SelectedDate = EntradaSeleccionada.FechaEntrada.Date;
                                        txtOrigen.Text = EntradaSeleccionada.OrganizacionOrigen.Trim();
                                        txtNoPartida.Text = txtNoPartidaGrupo.Text;
                                    }

                                    // Validar si se corta como cabeza sobrante
                                    if (entradasCabezasSobrantes != null && entradasCabezasSobrantes.Any())
                                    {
                                        foreach (var cabezasSobrantesPorEntradaInfo in entradasCabezasSobrantes)
                                        {
                                            //Si alguna partida aun tienen cabezas sobrantes sin cortar
                                            // Si tiene Sobrantes, si aun no se cortan todas las sobrantes,
                                            if (cabezasSobrantesPorEntradaInfo.CabezasSobrantes > 0 &&
                                                cabezasSobrantesPorEntradaInfo.CabezasSobrantesCortadas <
                                                cabezasSobrantesPorEntradaInfo.CabezasSobrantes &&
                                                cabezasSobrantesPorEntradaInfo.CabezasCortadas >=
                                                cabezasSobrantesPorEntradaInfo.EntradaGanado.CabezasOrigen)
                                            {
                                                // Se va a cortar una cabeza sobrante
                                                esCabezasSobrante = true;
                                                txtNoPartida.Text =
                                                    cabezasSobrantesPorEntradaInfo.FolioEntrada.ToString(
                                                        CultureInfo.InvariantCulture);
                                                break;
                                            }
                                        }
                                    }

                                    txtCorralDestino.IsEnabled = false;
                                    txtNoIndividual.IsEnabled = false;
                                    this.InicializarDispositivos();
                                    ValdiarFocusAreteMetalico();
                                }
                                else
                                {
                                    // Si se esta cortando una compra directa y se teclea un arete que pertenece a un centro
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                      String.Format("{0}{1} {2}",
                                                                    Properties.Resources.CorteGanado_ElAretePerteneceA,
                                        //"El arete ingresado pertenece a la partida:",
                                                                    _interfaceSalidoAnimalInfo.Partida,
                                                                    Properties.Resources.
                                                                        CorteGanado_ElAretePerteneceA_FavoDerValidar
                                        // ". Favor de validar."
                                                          ),
                                                      MessageBoxButton.OK, MessageImage.Warning);
                                    LimpiaParcial();
                                    txtNoIndividual.Text = String.Empty;
                                    txtNoIndividual.Focus();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }

                if (e.Key == Key.Enter)
                {
                    e.Handled = false;
                    txtAreteMetalico.Focus();
                }
            }
        }

        private void txtAreteMetalico_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    if (_interfaceSalidoAnimalInfo != null || txtAreteMetalico.Text.Trim().Length == 0)
                    {
                        e.Handled = false;
                        ValidarFocusDespuesAreteMetalico();
                        return;
                    }

                    if (txtAreteMetalico.Text.Trim().Length >= 0)
                    {
                        e.Handled = true;
                        BuscarAreteMetalico(txtAreteMetalico.Text);
                        if(txtAreteMetalico.Text.Trim().Length == 0)
                            ValidarFocusDespuesAreteMetalico();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CapturaAreteMetalico_CorteGanado,
                            MessageBoxButton.OK, MessageImage.Warning);

                        e.Handled = true;
                        ValdiarFocusAreteMetalico();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorAreteMetalico, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento change del combosexo para obtener el valor del combo y llamar cargar combo de calidad de ganado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboSexo.SelectedItem == null || cboSexo.SelectedIndex == 0) return;
                var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                CargarComboCalidad(sexoGanado);
                ObtenerTipoGanado();
                Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorCambioSexo, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// RN: Evento checked para cargar el tipo de ganado de acuerdo al sexo y peso.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbVenta_Checked(object sender, RoutedEventArgs e)
        {
            txtCorralDestino.IsEnabled = true;
            txtCorralDestino.Text = string.Empty;
            lblRestantes.Content = string.Empty;
            lblRestantesNumero.Content = string.Empty;
            DeshabilitarControles(false);
            gpbDatosCorralGanado.IsEnabled = true;
            gpbDisplayBascula.IsEnabled = true;
            dgTratamientos.IsEnabled = false;

            dgTratamientos.ItemsSource = null;
            _listaTratamientos = null; //se limpia lista de tratamientos

            btnMedicamentoEnfermeria.IsEnabled = false;
            cboPaletas.SelectedIndex = 0;

            btnGuardar.IsEnabled = true;
        }
        /// <summary>
        /// Evento para validar solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPesoCorte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        private void txtNoPartida_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void txtTemperatura_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, txtTemperatura.Text);
        }

        private void txtNoPartida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (txtNoPartida.Text.Trim().Length > 0)
                {
                    try
                    {
                        var entradaPl = new EntradaGanadoPL();
                        var resultadoBusqueda =
                            entradaPl.ObtenerPartidasProgramadas(int.Parse(txtNoPartida.Text),
                                                                 organizacionID);
                        if (resultadoBusqueda != null)
                        {
                            txtNoIndividual.Focus();
                            ObtenerDatosDePartidaSeleccionada(resultadoBusqueda.Lista[0]);
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.FolioPartida_CorteGanado,
                            MessageBoxButton.OK, MessageImage.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.CorteGanado_ErrorBuscarPartida, MessageBoxButton.OK, MessageImage.Error);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_capturarnumeropartida,
                        MessageBoxButton.OK, MessageImage.Warning);

                    txtNoPartidaGrupo.Focus();
                }
            }

        }

        /// <summary>
        /// Para habilitar los campos q fueron desabilitados al hacer el checked y obtener el corrarl destino de nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbVenta_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtCorralDestino.IsEnabled = false;
                btnMedicamentoEnfermeria.IsEnabled = true;
                txtCorralDestino.Text = string.Empty;
                lblRestantes.Content = string.Empty;
                lblRestantesNumero.Content = string.Empty;
                if (cboSexo.SelectedItem != null)
                {
                    var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                    var tipoGanado = (TipoGanadoInfo)cboTipoGanado.SelectedItem;
                    if (tipoGanado != null)
                    {
                        ObtenerCorralDestino(tipoGanado, sexoGanado);
                        Dispatcher.BeginInvoke(new Action(ConsultarPeso), DispatcherPriority.Background, null);
                    }

                    if (!termometroConectado)
                    {
                        txtTemperatura.IsEnabled = true;
                    }
                    else
                    {
                        _tempTomada = false;
                        InicializarDispositivos();
                    }

                }
                DeshabilitarControles(true);
                dgTratamientos.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CorteGanado_ErrorVenta, MessageBoxButton.OK, MessageImage.Error);
            }

        }
        private void cboSexo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab) &&
                (!String.IsNullOrEmpty(cboSexo.Text) && cboSexo.Text != "Seleccione"))
            {
                cboCalidad.Focus();
            }
            else
            {
                cboSexo.Focus();
                cboSexo.SelectedIndex = 0;
                e.Handled = true;
            }
        }
        private void cboCalidad_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab) &&
                (!String.IsNullOrEmpty(cboCalidad.Text) && cboCalidad.Text != "Seleccione"))
            {
                cboCausaRechazo.Focus();

            }
            else
            {
                cboCalidad.Focus();
                e.Handled = true;
            }
        }
        private void cboCausaRechazo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab))
            {
                if (txtPesoCorte.IsEnabled)
                {
                    txtPesoCorte.Focus();
                }
                else
                {
                    cboClasificacion.Focus();
                }
            }
            else
            {
                cboCausaRechazo.Focus();
                e.Handled = true;
            }
        }
        private void cboTipoGanado_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab) &&
                (!String.IsNullOrEmpty(cboTipoGanado.Text) && cboTipoGanado.Text != "Seleccione"))
            {
                cboClasificacion.Focus();
            }
            else
            {
                cboTipoGanado.Focus();
                e.Handled = true;
            }
        }
        private void cboClasificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab) &&
                (!String.IsNullOrEmpty(cboClasificacion.Text) && cboClasificacion.Text != "Seleccione"))
            {
                if (ckbVenta.IsChecked != null && ckbVenta.IsChecked.Value)
                {
                    txtCorralDestino.Focus();
                }
                else
                {
                    if (txtCorralDestino.IsEnabled)
                    {
                        txtCorralDestino.Focus();
                    }
                    else if (txtTemperatura.IsEnabled)
                    {
                        txtTemperatura.Focus();
                    }
                    else
                    {
                        cboPaletas.Focus();
                    }
                }
            }
            else
                cboClasificacion.Focus();
        }

        private void txtObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                btnGuardar.Focus();
            }
        }
        private void cboImplantador_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab) &&
                (!String.IsNullOrEmpty(cboImplantador.Text) && cboImplantador.Text != "Selecccione"))
            {
                txtObservaciones.Focus();

            }
        }
        private void cboPaletas_keyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Tab) &&
                (!String.IsNullOrEmpty(cboPaletas.Text) && cboPaletas.Text != "Seleccione"))
            {

                cboImplantador.Focus();

            }
            else
            {

                cboPaletas.Focus();
                e.Handled = true;
            }
        }
        private void txtCorralDestino_keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (String.IsNullOrWhiteSpace(txtCorralDestino.Text))
                {
                    txtCorralDestino.Focus();
                    e.Handled = true;
                }
                else
                {
                    try
                    {
                        if (ckbVenta.IsChecked == true)
                        {
                            //Validar si el corral destino es de tipo Venta
                            var corralPl = new CorralPL();
                            var corralInfo = new CorralInfo
                            {
                                Codigo = txtCorralDestino.Text,
                                TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.CronicoVentaMuerte },
                                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                            };

                            corralInfo = corralPl.ObtenerPorCodigoCorral(corralInfo);
                            _corralInfoGen = corralInfo;
                            if (corralInfo == null)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CorteGanado_CorralNoEsTipoVenta,
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                                return;
                            }
                            ObtenerLoteParaCorralDestino();
                        }
                        else
                        {
                            //Validar si el corral destino es de tipo Venta
                            var corralPl = new CorralPL();
                            var corralInfo = new CorralInfo
                            {
                                Codigo = txtCorralDestino.Text,
                                TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.Produccion },
                                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                            };
                            corralInfo = corralPl.ObtenerPorCodigoCorral(corralInfo);
                            _corralInfoGen = corralInfo;
                            if (corralInfo == null)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CorteGanado_CorralNoEsTipoProduccion,
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                                txtCorralDestino.Text = string.Empty;
                                txtCorralDestino.Focus();
                                corralInfo = null;
                                return;
                            }
                            ObtenerLoteParaCorralDestino();
                            if (EntradaSeleccionada.Lote != null)
                            {
                                if (EntradaSeleccionada.Lote.Cabezas >= corralInfo.Capacidad)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    "El corral destino no tiene capacidad. Favor de validar.",
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                                    txtCorralDestino.Text = string.Empty;
                                    txtCorralDestino.Focus();
                                    corralInfo = null;
                                    return;
                                }
                            }
                            //Enviar el focus
                            if (txtTemperatura.IsEnabled)
                            {
                                txtTemperatura.Focus();
                            }
                            else
                            {
                                cboPaletas.Focus();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }
        }

        private void btnMedicamentoEnfermeria_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (EntradaSeleccionada == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.CorteGanado_SelecionarPartida,
                           MessageBoxButton.OK, MessageImage.Warning);
                    txtNoPartidaGrupo.Focus();
                }
                else
                {
                    var medicamientoDialog = new Medicamentos(_listaTratamientos,
                                                                txtCorralDestino.Text,
                                                                txtNoIndividual.Text)
                    {
                        Left = (ActualWidth - Width) / 2,
                        Top = ((ActualHeight - Height) / 2) + 132,
                        Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                    };
                    medicamientoDialog.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CorteGanado_ErrorMedicamentos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtPesoCorte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    if (this._pesoCorte != this.txtPesoCorte.Text)
                    {

                        banderaFoco = true;
                        Dispatcher.BeginInvoke(new Action(ConsultarPeso), DispatcherPriority.Background, null);
                    } else {
                        banderaFoco = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CorteGanado_ErrorCalculoPeso, MessageBoxButton.OK, MessageImage.Error);
            }

        }
        /// <summary>
        /// Agrega un corral al Datagrid y a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGuardar.IsEnabled = false;
                imgloading.Visibility = Visibility.Visible;
                Dispatcher.BeginInvoke((Action)(Guardar), DispatcherPriority.Background);
            }
            catch (InvalidPortNameException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ErrorGuardar_CorteGanado + ex.Message,
                        MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Error);
                if (String.IsNullOrEmpty(txtNoIndividual.Text))
                    txtNoIndividual.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
                if (String.IsNullOrEmpty(txtNoIndividual.Text))
                    txtNoIndividual.IsEnabled = false;
            }

        }

        /// Evento del boton Actualizar Bascula
        private void btnActualizarBascula_Click(object sender, RoutedEventArgs e) {
            try
            {

                if (basculaConectada)
                {
                    this._pesoTomado = false;
                    InicializarDispositivos();
                    Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                }
                else {
                    if (_spManager != null)
                    {
                        btnActualizarBascula.IsEnabled = false;
                        _spManager.Dispose();
                        _spManager.StartListening(_configBasculaCorte.Puerto,
                                    _configBasculaCorte.Baudrate,
                                    _configBasculaCorte.Paridad,
                                    _configBasculaCorte.Databits,
                                    _configBasculaCorte.BitStop);
                        _spManager.StopListening();
                        Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                    
                        _spManager.StartListening(_configBasculaCorte.Puerto,
                                _configBasculaCorte.Baudrate,
                                _configBasculaCorte.Paridad,
                                _configBasculaCorte.Databits,
                                _configBasculaCorte.BitStop);
                        btnActualizarBascula.IsEnabled = true;
                        basculaConectada = true;
                    } 
                }
            }
            catch 
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.CorteGanado_ErrorCapturaBascula,
                                     MessageBoxButton.OK, MessageImage.Error);
                basculaConectada = false;
                btnActualizarBascula.IsEnabled = true;
            }
        }

        /// Evento del boton Actualizar Temperatura
        private void btnActualizarTemperatura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (termometroConectado)
                {
                    _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                                   _configTermometroCorte.Baudrate,
                                   _configTermometroCorte.Paridad,
                                   _configTermometroCorte.Databits,
                                   _configTermometroCorte.BitStop);
                    btnActualizarTemperatura.IsEnabled = true;
                    termometroConectado = true;

                    if (!String.IsNullOrEmpty(txtDisplayTemperatura.Text))
                    {
                        maxTemp = double.Parse(txtDisplayTemperatura.Text);
                        if (maxTemp > 36)
                        {
                            Dispatcher.BeginInvoke(new Action(CapturarTemperaturaDeDisplay),
                                               DispatcherPriority.Background);
                        _tempTomada = true;
                        }
                        
                    }
                }
                else
                {
                    if (_spManagerTermo != null)
                    {
                        btnActualizarTemperatura.IsEnabled = false;
                        if (!termometroConectado)
                        {
                            _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                                    _configTermometroCorte.Baudrate,
                                    _configTermometroCorte.Paridad,
                                    _configTermometroCorte.Databits,
                                    _configTermometroCorte.BitStop);
                            btnActualizarTemperatura.IsEnabled = true;
                            termometroConectado = true;
                        }
                    }
                }
            }
            catch 
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.CorteGanado_ErrorCapturaTermometro,
                                     MessageBoxButton.OK, MessageImage.Error);

                termometroConectado = false;
                btnActualizarTemperatura.IsEnabled = true;
            }
        }

        /// <summary>
        /// Evento del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Cancelarcaptura_CorteGanado,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarCaptura(true);
                // Una vez guardado el corte se habilita para tomar elpeso de nuevo
                _pesoTomado = false;
                _tempTomada = false;
                this.vecesPesoCotejado = 0;
                this._spManager.StopListening();
                Inicializarcontroles(false);
                imgBuscar.IsEnabled = true;
                IniciarTermometro(false);
                VerificarPermisosCapturaManual();
            }

        }

        private void ValidarCheck(object sender, RoutedEventArgs e)
        {
            var dato = (TratamientoInfo)dgTratamientos.SelectedItem;
            var seleccionar = false;
            TratamientoInfo tratamientoSeleccionado;
            TratamientoInfo tratamiento = null;

            var chk = sender as CheckBox;
            if (chk.IsChecked.Value)
            {
                if (dato != null)
                {
                    try
                    {
                        tratamientoSeleccionado = Extensor.ClonarInfo(dato) as TratamientoInfo;

                        tratamiento =
                            _listaTratamientos.FirstOrDefault(
                                id => id.TratamientoID == tratamientoSeleccionado.TratamientoID);
                        var resultadoValidacion = ExistenProductosEnTratamientosSeleccionados(dato);
                        if (resultadoValidacion.Resultado)
                        {
                            dato.Seleccionado = false;
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              string.Format("{0}{1}{2}{3}{4}",
                                                            Properties.Resources.
                                                                CorteGanado_MedicamentosContenidosEnOtroTratamiento,
                                                            dato.Descripcion.Trim(),
                                                            Properties.Resources.
                                                                CorteGanado_MedicamentosContenidosEnOtroTratamiento2,
                                                            resultadoValidacion.Mensaje.Trim(),
                                                            "."),
                                              MessageBoxButton.OK, MessageImage.Warning);
                        }
                        else
                        {
                            seleccionar = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    finally
                    {
                        if (tratamiento != null)
                        {
                            tratamiento.Seleccionado = seleccionar;
                        }
                    }
                }
            }
            else
            {
                dato.Seleccionado = false;
            }
        }
        /// <summary>
        /// Handler del evento Keyup del texto temperatura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemperatura_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (txtPesoCorte.Text.Trim().Length > 0)
                {
                    try
                    {
                        var valor = double.Parse(txtTemperatura.Text);
                        ValidarTemperatura(valor);
                        cboPaletas.Focus();
                    }
                    catch (FormatException ex)
                    {
                        Logger.Error(ex);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CorteGanado_TemperaturaInvalida,
                       MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_IngresarTemperatura,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Inicializador del termometro
        /// </summary>
        private void IniciarTermometro(bool mensaje)
        {
            //Se prueba el funcionamiento de los display del termometro
            _tempTomada = false;
            maxTemp = 0.0d;
            temperaturaCapturadaGlobal = string.Empty;
            //Se prueba el funcionamiento de los display del termometro
            try
            {
                if (_spManagerTermo != null)
                {
                    if (!termometroConectado)
                    {
                        _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                            _configTermometroCorte.Baudrate,
                            _configTermometroCorte.Paridad,
                            _configTermometroCorte.Databits,
                            _configTermometroCorte.BitStop);

                        termometroConectado = true;
                        btnActualizarTemperatura.IsEnabled = true;
                        txtTemperatura.IsEnabled = false;
                        txtDisplayTemperatura.IsEnabled = true;
                    }
                }

                if (mensaje)
                {
                    _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                    _configTermometroCorte.Baudrate,
                    _configTermometroCorte.Paridad,
                    _configTermometroCorte.Databits,
                    _configTermometroCorte.BitStop);
                }
                
            }
            catch (UnauthorizedAccessException)
            {
                if (mensaje)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarTermometro,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (mensaje)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_ErrorInicializarTermometro,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                }
                //Si la bascula o el termometro estan desconectados se verifica si se tiene permiso para capturar manualmente
                if (termometroConectado == false)
                {
                    if (_configTermometroCorte == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualTermometro,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                        DeshabilitarControles(false);
                    }
                    else
                    {
                        txtTemperatura.IsEnabled = true;
                        if (!_configTermometroCorte.CapturaManual)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualTermometro,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                            DeshabilitarControles(false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para probar los dispositivos
        /// </summary>
        private void ProbarFuncionamientoDispositivos()
        {
            //Se prueba el funcionamiento de los display de la bascula
            try
            {
                if (_spManager != null)
                {
                    try {
                        _spManager.Dispose();
                    }
                    catch (Exception) { }
                    _spManager.StartListening(_configBasculaCorte.Puerto,
                        _configBasculaCorte.Baudrate,
                        _configBasculaCorte.Paridad,
                        _configBasculaCorte.Databits,
                        _configBasculaCorte.BitStop);
                    basculaConectada = true;
                    txtPesoCorte.IsEnabled = false;
                    this._spManager.StopListening();
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarBascula,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);

                if (basculaConectada == false)
                {
                    if (_configBasculaCorte == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualBascula,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                        DeshabilitarControles(false);
                    }
                    else
                    {
                        txtPesoCorte.IsEnabled = true;
                        if (!_configBasculaCorte.CapturaManual)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualBascula,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                            DeshabilitarControles(false);
                        }
                    }

                }
            }

            IniciarTermometro(true);

            //Se prueba el funcionamiento de los display del RFID
            try
            {
                if (_spManagerRFID != null)
                {
                    _spManagerRFID.StartListening(_configRFIDCorte.Puerto,
                        _configRFIDCorte.Baudrate,
                        _configRFIDCorte.Paridad,
                        _configRFIDCorte.Databits,
                        _configRFIDCorte.BitStop);
 
                    rfidConectado = true;
                    txtAreteMetalico.IsEnabled = false;
                }
            }
            catch (UnauthorizedAccessException )
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CorteGanado_ErrorInicializarLectorRFID,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarLectorRFID,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
                //Si el Lector RFID estan desconectados se verifica si se tiene permiso para capturar manualmente
                VerificarPermisosCapturaManual();
            }
        }

        private void ObtenerDatosDePartidaSeleccionada(EntradaGanadoInfo entradaGanado)
        {
            txtNoPartidaGrupo.Text = entradaGanado.FolioEntradaAgrupado.ToString(CultureInfo.InvariantCulture);
            txtNoPartida.IsEnabled = false;
            txtCorralOrigen.Text = entradaGanado.CodigoCorral;
            txtCorralOrigen.IsEnabled = false;
            imgBuscar.IsEnabled = false;
            lblTotalGenerado.Content = entradaGanado.CabezasRecibidasAgrupadas;
            loteOrigen = ObtenerLoteParaCorralOrigen(entradaGanado.CorralID);
            if (loteOrigen != null)
            {
                // este campo EsMetaFilaxia es un bit si es 1 es metafilaxia
                lblPartidasMetafilaxia.Visibility = Visibility.Visible;
                lblPartidasMetafilaxia.Content = (entradaGanado.EsMetaFilaxia ?
                                                  Properties.Resources.CorteGanado_lblPartidasMetafilaxia :
                                                  Properties.Resources.CorteGanado_lblPartidasMetafilaxiaNormal);

                if (entradaGanado.NivelGarrapata != NivelGarrapata.Ninguno)
                {
                    var conGarrapata = String.Format("{0} - {1}", lblPartidasMetafilaxia.Content,
                                                                  Properties.Resources.CorteGanado_lblGarrapata);
                    lblPartidasMetafilaxia.Content = conGarrapata;
                }

                lblEnEnfermeriaGenerado.Content = 0;
                lblMuertasGenerado.Content = 0;
                lblTotalCortadasGenerado.Content = 0;


                var ganadoEnfermeria = new EntradaGanadoInfo();
                var corteGanadoPl = new CorteGanadoPL();

                ganadoEnfermeria.OrganizacionID = organizacionID;
                ganadoEnfermeria.FolioEntrada = entradaGanado.FolioEntrada;
                ganadoEnfermeria.FolioEntradaAgrupado = txtNoPartidaGrupo.Text;

                lblEnEnfermeriaGenerado.Content =
                    corteGanadoPl.ObtenerCabezasEnEnfermeria(ganadoEnfermeria,
                                                            (int)GrupoCorralEnum.Enfermeria);
                var cabezas = new CabezasCortadas
                {
                    TipoMovimiento = (int)TipoMovimiento.Corte,
                    TipoCorral = (int)TipoCorral.Recepcion,
                    OrganizacionID = organizacionID,
                    NoPartida = entradaGanado.FolioEntradaAgrupado
                };

                var cabezarCortadas = corteGanadoPl.ObtenerCabezasCortadas(cabezas);

                var cabezaMuertas = new CabezasCortadas
                {
                    TipoMovimiento = (int)TipoMovimiento.Muerte,
                    TipoCorral = (int)TipoCorral.Recepcion,
                    OrganizacionID = organizacionID,
                    NoPartida = entradaGanado.FolioEntradaAgrupado
                };

                var cabezarMuertas = corteGanadoPl.ObtenerCabezasMuertas(cabezaMuertas);

                cabezarMuertas = cabezarMuertas < 0 ? 0 : cabezarMuertas;
                cabezarCortadas = cabezarCortadas < 0 ? 0 : cabezarCortadas;

                lblMuertasGenerado.Content = cabezarMuertas;
                lblTotalCortadasGenerado.Content = cabezarCortadas;

                var totalCabezas = (entradaGanado.CabezasRecibidasAgrupadas -
                                    (int)lblEnEnfermeriaGenerado.Content)
                                    - cabezarCortadas
                                    - cabezarMuertas;

                lblProgramadasCorteGenerado.Content = totalCabezas;

                //Obtener Cabezas tanto las sobrantes, cortadas y sobrantes cortadas por folio entrada
                var paramCabezaSobrantesCortadas = new CabezasCortadas
                {
                    OrganizacionID = organizacionID,
                    NoPartida = entradaGanado.FolioEntradaAgrupado
                };

                entradasCabezasSobrantes =
                    corteGanadoPl.ObtenerCabezasSobrantes(paramCabezaSobrantesCortadas);

                EntradaSeleccionada = entradaGanado;
                
                if (!entradaGanado.EsAgrupado)
                {
                    ObtenerProveedor();
                    dtpFechaRecepcion.SelectedDate = entradaGanado.FechaEntrada.Date;
                    txtOrigen.Text = entradaGanado.OrganizacionOrigen.Trim();
                    txtNoPartida.Text = txtNoPartidaGrupo.Text;
                }
                else
                {
                    txtNoPartida.Text = entradaGanado.FolioEntrada.ToString(CultureInfo.InvariantCulture);
                }

                txtOrigen.IsEnabled = false;
                txtNoIndividual.Focus();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.CorteGanado_LoteNoDisponible,
                      MessageBoxButton.OK,
                      MessageImage.Stop);
                txtNoPartida.Text = string.Empty;
                DeshabilitarControles(false);
            }

        }

        /// <summary>
        /// Metodo para aplicar un Dispose a los dispositivos conectados en caso de cerrar la ventana
        /// </summary>
        private void DisposeDispositivosConectados()
        {
            try
            {
                if (basculaConectada)
                {
                    _spManager.Dispose();
                }

                if (termometroConectado) {
                    _spManagerTermo.Dispose();
                }

                if (rfidConectado)
                {
                    _spManagerRFID.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metrodo Verificar si existen almacen para las Trampas configuradas
        /// </summary>
        private bool ExistenAlmacenParaTrampa()
        {
            var existe = false;
            try
            {
                var parametrosPL = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.AlmacenIDTrampa,
                    Clave = TiposParametrosEnum.AlmacenIDTrampa.ToString(),
                    OrganizacionID = organizacionID
                };
                var parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametroClave(
                                parametroSolicitado,
                                trampaInfo.TrampaID
                            );

                if (parametros != null && parametros.Count > 0)
                {
                    var almacenPL = new AlmacenPL();
                    almacenInfo = almacenPL.ObtenerPorID(int.Parse(parametros[0].Valor));
                    if (almacenInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format("{0} {1}{2}",
                               Properties.Resources.CorteGanado_NoValidoAlmacenIDTrampas1,
                               trampaInfo.Descripcion,
                               Properties.Resources.CorteGanado_NoValidoAlmacenIDTrampas2),
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    }
                    else
                    {
                        var almacenMovimientoInfo = new AlmacenMovimientoInfo
                        {
                            AlmacenID = almacenInfo.AlmacenID,
                            TipoMovimientoID =
                                (int)TipoMovimiento.DiferenciasDeInventario,
                            Status = (int)EstatusInventario.Pendiente
                        };

                        //Validar que no queden ajustes pendientes por aplicar para el almacen
                        var existeAjustesPendientes = almacenPL.ExistenAjustesPendientesParaAlmacen(
                            almacenMovimientoInfo
                            );
                        if (existeAjustesPendientes)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.CorteGanado_ExistenAjustesEnInventario,
                                              MessageBoxButton.OK,
                                              MessageImage.Stop);
                        }
                        else
                        {
                            existe = true;
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format("{0} {1}{2}",
                                Properties.Resources.CorteGanado_NoExisteAlmacenIDTrampas1,
                                trampaInfo.Descripcion,
                                Properties.Resources.CorteGanado_NoExisteAlmacenIDTrampas2),
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return existe;
        }

        private void ConsultarPeso()
        {
            try
            {
                if (txtPesoCorte.Text != string.Empty)
                {
                    if (cboSexo.SelectedItem != null && cboSexo.SelectedItem.ToString() == string.Empty)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CapturaSexo_CorteGanado,
                                          MessageBoxButton.OK, MessageImage.Warning);
                        cboSexo.Focus();
                        return;
                    }
                    if (ckbVenta.IsChecked == false)
                    {
                        Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                        if (!basculaConectada)
                        {
                            txtPesoCorte.IsEnabled = true;
                        }
                    }
                    if (FocoPeso)
                    {
                        txtPesoCorte.Focus();
                        FocoPeso = false;
                    }
                    else
                    {
                        ObtenerTipoGanado();
                        cboClasificacion.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.pesoVacio,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    cboTipoGanado.SelectedIndex = -1;
                    banderaFoco = true;
                    txtCorralDestino.Text = string.Empty;
                    dgTratamientos.ItemsSource = null;
                    _listaTratamientos = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        private void LimpiaParcial()
        {
            cboSexo.SelectedItem = 0;
            cboTipoGanado.SelectedIndex = -1;
            cboCalidad.SelectedIndex = 0;
            cboClasificacion.SelectedIndex = 0;
            txtAreteMetalico.Text = string.Empty;
            if (bandBack)
            {
                txtNoIndividual.Text = string.Empty;
            }
            btnGuardar.IsEnabled = false;

            txtNoIndividual.IsEnabled = true;
            txtAreteMetalico.IsEnabled = false;
            cboSexo.IsEnabled = false;
            cboCalidad.IsEnabled = false;
            cboCausaRechazo.IsEnabled = false;
            cboClasificacion.IsEnabled = false;

            txtPesoCorte.IsEnabled = false;
            txtTemperatura.IsEnabled = false;

            txtCorralDestino.IsEnabled = false;
            ckbVenta.IsEnabled = false;

            gpbImplementacion.IsEnabled = false;
            gpbGenerales.IsEnabled = false;
            gpbOtrosDatosGanado.IsEnabled = false;
            gpbDatosCorte.IsEnabled = false;
            DisplayTemperatura.IsEnabled = false;
            gpbDisplayBascula.IsEnabled = false;

            btnGuardar.IsEnabled = false;
            _pesoTomado = false;
            _tempTomada = false;

            VerificarPermisosCapturaManual();
        }

        /// <summary>
        /// Llena el combo de sexo de ganado, obtiene el sexo del ganado de un Enum dentro de la aplicacion
        /// </summary>
        private void CargarCboSexo()
        {
            IList<Sexo> sexoEnums = Enum.GetValues(typeof(Sexo)).Cast<Sexo>().ToList();
            var listaSexo = new Dictionary<char, string>();

            var i = 0;
            listaSexo.Add('S', "Seleccione");
            foreach (var sexo in sexoEnums)
            {
                if (i == 0)
                {
                    listaSexo.Add('H', sexo.ToString());
                }
                if (i == 1)
                {
                    listaSexo.Add('M', sexo.ToString());
                }

                i++;
            }
            cboSexo.ItemsSource = listaSexo.Values;
            cboSexo.SelectedIndex = 0;

        }
        /// <summary>
        /// Carga el combo paletas con un enumerador.
        /// </summary>
        private void CargarCboPaletas()
        {
            IList<Paletas> paletasEnums = Enum.GetValues(typeof(Paletas)).Cast<Paletas>().ToList();
            var listaPalete = new Dictionary<int, string> { { -1, "Seleccione" } };

            var i = 0;
            foreach (var palete in paletasEnums)
            {
                listaPalete.Add(i, palete.GetHashCode().ToString());
                i++;
            }
            cboPaletas.ItemsSource = listaPalete;
            cboPaletas.SelectedIndex = 0;

        }

        /// <summary>
        /// Consulta el arete en la base de dato tabla InterfaceAnimal.
        /// Si tipoArete es 1 buscara por arete individaual, si es 2 por arete metalico.
        /// </summary>
        /// <param name="arete"></param>
        private void ConsultarAreteAnimal(string arete, int tipoArete)
        {
            try
            {
                cboSexo.IsEnabled = true;

                //Se valida que pertenezca a la misma partida
                var animalPl = new InterfaceSalidaAnimalPL();
                if(tipoArete == 1)
                    _interfaceSalidoAnimalInfo = animalPl.ObtenerNumeroAreteIndividual(arete, organizacionID);
                else
                    _interfaceSalidoAnimalInfo = animalPl.ObtenerNumeroAreteMetalico(arete, organizacionID);

                //Validar si en las partidas se encuentra una compra directa
                bool existeCompraDirecta = ExiteCompraDirecta();

                //Si son partidas de centro y no se encuentra el arete en la interfaz
                if (!existeCompraDirecta && _interfaceSalidoAnimalInfo == null)
                {
                    if (!ExistenSobrantesSinCortar())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.NoAreteMarcado_CorteGanado,
                            MessageBoxButton.OK, MessageImage.Warning);
                        LimpiaParcial();
                        txtNoIndividual.Text = String.Empty;
                        txtNoIndividual.Focus();
                    }
                }
                else
                {
                    if (_interfaceSalidoAnimalInfo != null)
                    {
                        if (_interfaceSalidoAnimalInfo.CorralID != EntradaSeleccionada.CorralID)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format("{0} {1}",
                                    Properties.Resources.CorteGanado_ErrorAreteNoPerteneceAPartidaPerteneceA,
                                    _interfaceSalidoAnimalInfo.Partida),
                                MessageBoxButton.OK, MessageImage.Warning);

                            LimpiaParcial();
                            txtNoIndividual.Text = String.Empty;
                            txtNoIndividual.Focus();
                            return;
                        }

                        cboSexo.SelectedItem = _interfaceSalidoAnimalInfo.TipoGanado.Sexo.ToString();
                        cboSexo.IsEnabled = false;
                        txtNoIndividual.Text = _interfaceSalidoAnimalInfo.Arete;
                        txtAreteMetalico.Text = _interfaceSalidoAnimalInfo.AreteMetalico;
                        if (tipoArete == 1)
                            txtNoIndividual.IsEnabled = false;
                        else
                            txtAreteMetalico.IsEnabled = false;
                        this.InicializarDispositivos();
                    }

                    
                    var entradaPl = new EntradaGanadoPL();
                    if (_interfaceSalidoAnimalInfo != null && EntradaSeleccionada.EsAgrupado)
                    {

                        var tmplistaEntrada = entradaPl.ObtenerPartidasProgramadas(_interfaceSalidoAnimalInfo.Partida, organizacionID);

                        if (tmplistaEntrada != null && tmplistaEntrada.Lista.Count > 0)
                        {
                            var tmpEntrada = tmplistaEntrada.Lista[0];
                            ObtenerProveedor();
                            dtpFechaRecepcion.SelectedDate = tmpEntrada.FechaEntrada.Date;
                            txtNoPartida.Text = tmpEntrada.FolioEntrada.ToString();
                            txtOrigen.Text = tmpEntrada.OrganizacionOrigen.Trim();
                            EntradaSeleccionada.TipoOrigen = (int)TipoOrganizacion.Centro;
                            ValdiarFocusAreteMetalico();
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtProveedor.Text) && String.IsNullOrEmpty(txtOrigen.Text))
                        {
                            ObtenerProveedor();
                            dtpFechaRecepcion.SelectedDate = EntradaSeleccionada.FechaEntrada.Date;
                            txtOrigen.Text = EntradaSeleccionada.OrganizacionOrigen.Trim();
                        }

                        // Si en las partidas a cortar existe una compra directa se mete el arete en la que aún no se ha llenado.
                        if (EntradaSeleccionada.EsAgrupado && existeCompraDirecta)
                        {
                            //TODO: aki puede que se ballan algunos aretes de la compra directa 
                            //Obtener Una partida de compra directa
                            int folioEntrada = entradaPl.ObtenerPartidaCompraDirecta(EntradaSeleccionada);
                            if (folioEntrada > 0)
                            {
                                txtNoPartida.Text = folioEntrada.ToString();
                                EntradaSeleccionada.FolioEntrada = folioEntrada;
                                EntradaSeleccionada.TipoOrigen = (int)TipoOrganizacion.CompraDirecta;
                            }
                            else
                            {
                                //// TODO: Validar bien ---> Validar si tiene Sobrantes :(
                                if (!ExistenSobrantesSinCortar())
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                      Properties.Resources.NoAreteMarcado_CorteGanado,
                                                      MessageBoxButton.OK, MessageImage.Warning);
                                    LimpiaParcial();
                                    txtNoIndividual.Text = String.Empty;
                                    txtNoIndividual.Focus();
                                }
                            }
                        }
                        else
                        {
                            txtNoPartida.Text = txtNoPartidaGrupo.Text;
                        }
                        this.InicializarDispositivos();
                        ValdiarFocusAreteMetalico();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ErrorConsultaArete_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                throw;
            }
        }

        private bool ExiteCompraDirecta()
        {
            var resp = false;
            try
            {
                string[] tipoOrigen = EntradaSeleccionada.TipoOrigenAgrupado.Split('|');
                foreach (var tipo in tipoOrigen)
                {
                    int tip = int.Parse(tipo);
                    if (tip == (int)TipoOrganizacion.CompraDirecta)
                    {
                        resp = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resp;

        }

        /// <summary>
        /// Metodo para obtener si la aprtida tiene cabezas sobrantes
        /// </summary>
        /// <returns></returns>
        private bool ExistenSobrantesSinCortar()
        {
            //Si no existen cabezas sobrantes por cortar se regresa false
            bool resp = false;
            if (entradasCabezasSobrantes != null && entradasCabezasSobrantes.Any())
            {
                foreach (var cabezasSobrantesPorEntradaInfo in entradasCabezasSobrantes)
                {
                    //Si alguna partida aun tienen cabezas sobrantes sin cortar
                    if (cabezasSobrantesPorEntradaInfo.CabezasSobrantes > 0 &&
                        cabezasSobrantesPorEntradaInfo.CabezasSobrantesCortadas <
                        cabezasSobrantesPorEntradaInfo.CabezasSobrantes)
                    {
                        ObtenerProveedor();
                        dtpFechaRecepcion.SelectedDate = EntradaSeleccionada.FechaEntrada.Date;
                        txtOrigen.Text = EntradaSeleccionada.OrganizacionOrigen.Trim();
                        esCabezasSobrante = true;
                        txtNoPartida.Text = cabezasSobrantesPorEntradaInfo.FolioEntrada.ToString();
                        resp = true;
                        break;
                    }
                }

                if (resp)
                {
                    this.InicializarDispositivos();
                }
            }
            return resp;
        }

        /// <summary>
        /// Carga el combo calidad de ganado de acuerdo al sexo.
        /// </summary>
        /// <param name="sexo"></param>
        private void CargarComboCalidad(string sexo)
        {
            try
            {
                var entradaPl = new EntradaGanadoPL();
                var calidad = entradaPl.ObtenerCalidadPorSexo(sexo);
                if (calidad == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.NoRegistroCalidad_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    var seleccione = new CalidadGanadoInfo();
                    seleccione.Descripcion = "Seleccione";
                    seleccione.CalidadGanadoID = 0;
                    calidad.Insert(0, seleccione);
                    cboCalidad.ItemsSource = calidad;
                    cboCalidad.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorConsultaCalidad_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
        }
        /// <summary>
        /// Carga el combo de causas de rechazo.
        /// </summary>
        private void CargarComboCausaRechazo()
        {
            try
            {
                var entradaPl = new EntradaGanadoPL();
                var causas = entradaPl.ObtenerCalidadPorCausaRechazo();
                if (causas == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.NoExisteCausaRechazo_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Warning);

                }
                else
                {
                    var causarechazo = new CausaRechazoInfo();
                    causarechazo.CausaRechazoID = 0;
                    causarechazo.Descripcion = "Seleccione";
                    causas.Insert(0, causarechazo);
                    cboCausaRechazo.ItemsSource = causas;
                    cboCausaRechazo.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorCausaRechazo_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
        }
        /// <summary>
        /// Metrodo para llenar el combo de implantador
        /// </summary>
        private void LlenarComboImplantador()
        {
            try
            {
                var implantador = new OperadorPL();
                var implantadores = implantador.ObtenerPorIDRol(organizacionID, OperadorEmum.Implantador.GetHashCode());
                if (implantadores == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.NoImplantadores_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    var oficio = new OperadorInfo();
                    oficio.Nombre = "Seleccione";
                    oficio.OperadorID = 0;
                    implantadores.Insert(0, oficio);
                    cboImplantador.ItemsSource = implantadores;
                    cboImplantador.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorImplantadores_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
                throw;

            }
        }
        /// <summary>
        /// Metrodo para llenar el combo de la clasificación.
        /// </summary>
        private void LlenarComboClasificacion()
        {
            try
            {
                var entradaPl = new EntradaGanadoPL();
                var clasificaciones = entradaPl.ObtenerCatClasificacion();
                if (clasificaciones == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.NoClasificaciones_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    var clasificacion = new ClasificacionGanadoInfo();
                    clasificacion.Descripcion = "Seleccione";
                    clasificacion.ClasificacionGanadoID = 0;
                    clasificaciones.Insert(0, clasificacion);
                    cboClasificacion.ItemsSource = clasificaciones;
                    cboClasificacion.SelectedValue = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorConsultaClasificaciones_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
        }
        /// <summary>
        /// Obtiene el valor o el key del combo de sexo en base a la variable isKey
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="esClave"></param>
        /// <returns></returns>
        public String ObtieneSexoGanado(Object obj, Boolean esClave)
        {
            var sexoEnum = obj;
            if (esClave)
            {
                var sexoGanadoChar = sexoEnum;
                return sexoGanadoChar.ToString();
            }
            return sexoEnum.ToString();
        }

        /// <summary>
        /// Inicializacion de los valores de usuario, lectura de configuracion y Handler de eventos
        /// </summary>
        private void UserInitialization()
        {

            try
            {
                var parametrosPL = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoBascula,
                    OrganizacionID = organizacionID
                };
                var parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaInfo.TrampaID);
                _configBasculaCorte = ObtenerParametroDispositivo(parametros);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoTermometro,
                    OrganizacionID = organizacionID
                };
                parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaInfo.TrampaID);
                _configTermometroCorte = ObtenerParametroDispositivo(parametros);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoRFID,
                    OrganizacionID = organizacionID
                };
                parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaInfo.TrampaID);
                _configRFIDCorte = ObtenerParametroDispositivo(parametros);

                _spManager = new SerialPortManager();
                _spManagerTermo = new SerialPortManager();
                _spManagerRFID = new SerialPortManager();

                _spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);
                _spManagerTermo.NewSerialDataRecieved += (spManager_NewSerialDataRecievedTermo);
                _spManagerRFID.NewSerialDataRecieved += (spManager_NewSerialDataRecievedRFID);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.LectorCodigoBarra,
                    OrganizacionID = organizacionID
                };
                IList<ConfiguracionParametrosInfo> parametrosScanner = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaInfo.TrampaID);

                if (parametrosScanner != null)
                {
                    ConfiguracionParametrosInfo parametroScanner =
                        parametrosScanner.FirstOrDefault(
                            param =>
                            param.Clave.ToUpper().Trim().Equals(ParametrosEnum.ScannerArete.ToString().ToUpper().Trim()));

                    if (parametroScanner == null)
                    {
                        aplicaScanner = false;
                        return;
                    }
                    aplicaScanner = bool.Parse(parametroScanner.Valor);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Funciona para mapear la seccion de parametros de configuracion del dispositivo
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private BasculaCorteSection ObtenerParametroDispositivo(IList<ConfiguracionParametrosInfo> parametros)
        {

            try
            {
                var parametroResultado = new BasculaCorteSection();
                foreach (var parametro in parametros)
                {
                    ParametrosDispositivosEnum enumTemporal;
                    var res = ParametrosDispositivosEnum.TryParse(parametro.Clave, out enumTemporal);
                    if (res)
                    {
                        switch (enumTemporal)
                        {
                            case ParametrosDispositivosEnum.puerto:
                                parametroResultado.Puerto = parametro.Valor;
                                break;
                            case ParametrosDispositivosEnum.baudrate:
                                parametroResultado.Baudrate = int.Parse(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.databits:
                                parametroResultado.Databits = int.Parse(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.paridad:
                                parametroResultado.cambiarParidad(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.bitstop:
                                parametroResultado.cambiarBitStop(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.espera:
                                parametroResultado.Espera = int.Parse(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.CapturaManual:
                                parametroResultado.CapturaManual = bool.Parse(parametro.Valor);
                                break;
                        }
                    }
                }
                return parametroResultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }



        private void CapturarAreteRFID()
        {
            if (Application.Current.Windows[ConstantesVista.WindowPrincipal].IsActive)
            {
                if (_interfaceSalidoAnimalInfo != null)
                {
                    txtAreteMetalico.Text = rfidCapturadoGlobal;
                    ValidarAreteMetalicoRegistrado();
                    ValidarFocusDespuesAreteMetalico();
                }

                if (txtNoIndividual.Text == "" && rfidCapturadoGlobal != "")
                {
                    BuscarAreteMetalico(rfidCapturadoGlobal);
                    txtNoIndividual.Focus();
                }
                else if (_interfaceSalidoAnimalInfo == null)
                {
                    txtAreteMetalico.Text = rfidCapturadoGlobal;
                    ValidarAreteMetalicoRegistrado();
                    ValidarFocusDespuesAreteMetalico();
                }
            }
        }



        /// <summary>
        /// Metodo para capturar temperatura en display
        /// </summary>
        private void CapturarTemperaturaEnDisplay()
        {
            txtDisplayTemperatura.Text = temperaturaCapturadaGlobal;
        }

        /// <summary>
        /// Metodo para capturar temperatura y validarla
        /// </summary>
        private void CapturarTemperaturaDeDisplay()
        {
            try
            {
                txtTemperatura.Text = maxTemp.ToString();
                temperaturaCapturadaGlobal = string.Empty;
                txtDisplayTemperatura.Text = string.Empty;
                ValidarTemperatura(maxTemp);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }



        /// <summary>
        /// Metodo para capturar el peso en el display
        /// </summary>
        private void CapturaPesoEnDisplay()
        {
            txtDisplayPeso.Text = this.pesoPercial;
            if (!capturaInmediata)
            {
                int iPesoParcial = 0;
                int iPesoGlobla = 0;
                int.TryParse(this.pesoPercial, out iPesoParcial);
                int.TryParse(this.pesoCapturadoGlobal, out iPesoGlobla);

                if(iPesoParcial > 0 && iPesoGlobla > 0){
                    if (iPesoParcial == iPesoGlobla)
                    {
                        this.vecesPesoCotejado++;
                        if (this.vecesPesoCotejado == this._configBasculaCorte.Espera)
                        {
                            txtPesoCorte.Text = pesoCapturadoGlobal = pesoPercial;
                            this._timerListeningPort = new DispatcherTimer();
                            this._spManager.StopListening();

                            this._pesoTomado = true;

                            if (cboSexo.SelectedItem == null)
                                return;

                            if (cboSexo.SelectedItem.ToString() == "Seleccione" || cboSexo.SelectedItem.ToString() == string.Empty)
                                return;

                            ObtenerTipoGanado();
                            ObtenerTratamiento();
                        }
                    }
                    else
                    {
                        this.vecesPesoCotejado = 0;
                        this.pesoCapturadoGlobal = this.pesoPercial;

                    }
                }
                else
                {
                    this.vecesPesoCotejado = 0;
                    this.pesoCapturadoGlobal = pesoPercial;
                }
            }
            else {
                txtPesoCorte.Text = pesoCapturadoGlobal = pesoPercial;
                capturaInmediata = false;
                this._timerListeningPort = new DispatcherTimer();
                this._spManager.StopListening();
                ObtenerTipoGanado();
                ObtenerTratamiento();
                this._pesoTomado = true;
            }
            if (capturaInmediata) 
            {
                txtPesoCorte.Text = pesoCapturadoGlobal;
                capturaInmediata = false;
                ObtenerTipoGanado();
                ObtenerTratamiento();

            }
        }
        /// <summary>
        /// Limpia los campos capturados bCancelar = true limpia por completo el Formulario
        /// </summary>
        /// <param name="bCancelar"></param>
        private void LimpiarCaptura(bool bCancelar)
        {
            imgBuscar.IsEnabled = bCancelar;
            cboSexo.SelectedIndex = 0;
            cboSexo.IsEnabled = true;
            cboCalidad.IsEnabled = true;
            cboCalidad.SelectedIndex = 0;
            cboCausaRechazo.SelectedIndex = 0;
            cboTipoGanado.SelectedIndex = -1;
            cboClasificacion.SelectedIndex = 0;
            cboPaletas.SelectedIndex = 0;
            // Limpia TextBox
            if (bCancelar)
            {
                txtCorralOrigen.Clear();
                txtNoPartidaGrupo.Clear();
                txtNoPartida.Clear();
                txtNoPartidaGrupo.IsEnabled = true;
                txtTemperatura.Clear();
                txtProveedor.Clear();
                txtOrigen.Clear();

                dtpFechaRecepcion.SelectedDate = null;
                
                lblTotalGenerado.Content = 0;
                lblEnEnfermeriaGenerado.Content = 0;
                lblMuertasGenerado.Content = 0;
                lblPartidasMetafilaxia.Content = string.Empty;
                lblTotalCortadasGenerado.Content = 0;
                lblProgramadasCorteGenerado.Content = 0;
                cboImplantador.SelectedValue = 0;
                imgBuscar.Focus();
            }
            else
            {
                txtNoIndividual.Focus();
            }

            ckbVenta.IsChecked = false;
            cboCalidad.ItemsSource = null;
            if (bandBack)
            {
                txtNoIndividual.Clear();
            }
            lblRestantes.Content = string.Empty;
            lblRestantesNumero.Content = string.Empty;
            txtAreteMetalico.Clear();
            txtPesoCorte.Clear();
            pesoCapturadoGlobal = string.Empty;
            txtCorralDestino.Clear();
            temperaturaCapturadaGlobal = string.Empty;
            txtObservaciones.Clear();
            txtDisplayPeso.Text = null;
            txtDisplayTemperatura.Text = null;

            // Limpia Grid Medicamentos
            dgTratamientos.ItemsSource = null;
            _listaTratamientos = null; //se limpia lista de tratamientos
            _interfaceSalidoAnimalInfo = null;

            // Limpia  Fechas
            dtpFechaCorte.SelectedDate = DateTime.Now;

            // Limpiar flag para cabeza sobrante
            esCabezasSobrante = false;

            //Limpiar bsacula
            this._pesoCorte = string.Empty;
            this._pesoTomado = false;
            this.pesoPercial = string.Empty;
            this.pesoCapturadoGlobal = string.Empty;
            this.vecesPesoCotejado = 0;
            this.txtDisplayPeso.Text = string.Empty;
            try
            {
                if (this._spManager != null && this.basculaConectada)
                    this._spManager.StopListening();

                if (this._timerListeningPort != null)
                    this._timerListeningPort.Stop();
            }
            catch { }
        }

        /// <summary>
        /// Metrodo Verificar si existen Corrales Configurados disponibles
        /// </summary>
        private bool ExisteCorralesDisponibles()
        {
            var existe = false;
            try
            {
                var corralRangoPl = new CorralRangoPL();
                var corralesConfigurados = corralRangoPl.ObtenerCorralesConfiguradosPorOrganizacionID(organizacionID);

                if (corralesConfigurados != null && corralesConfigurados.Count > 0)
                {
                    existe = true;
                }

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ConfigurarCorralesDisponibles_ErrorCorralesDisponibles, MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
            return existe;
        }

        /// <summary>
        /// Metrodo Verificar si existen Partidas programadas
        /// </summary>
        private bool ExisteProgramacionCorteGanado()
        {
            var existe = false;
            try
            {
                var corteGanadoPl = new CorteGanadoPL();
                var resultadoBusqueda = corteGanadoPl.ExisteProgramacionCorteGanado(organizacionID);
                if (resultadoBusqueda) existe = true;

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorProgramacionCorteGanado,
                                MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorProgramacionCorteGanado,
                                MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
            return existe;
        }

        /// <summary>
        /// Metrodo Verificar si existen Trampas configuradas
        /// </summary>
        private bool ExistenTrampas()
        {
            var bExiste = false;
            var trampaPl = new TrampaPL();
            try
            {
                var trampaInfo = new TrampaInfo
                {
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                    TipoTrampa = (char)TipoTrampa.Manejo,
                    HostName = Environment.MachineName
                };

                var trampaInfoResp = trampaPl.ObtenerTrampa(trampaInfo);

                if (trampaInfoResp != null)
                {
                    this.trampaInfo = trampaInfoResp;
                    bExiste = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return bExiste;
        }


        /// <summary>
        /// Metrodo para inhabilitar los controles en caso de no tener condiguraciones requeridas
        /// </summary>
        private void DeshabilitarControles(bool habilitar)
        {
            gpbDatosCorralGanado.IsEnabled = habilitar;
            gpbGenerales.IsEnabled = habilitar;
            gpbOtrosDatosGanado.IsEnabled = habilitar;
            gpbDatosCorte.IsEnabled = habilitar;
            DisplayTemperatura.IsEnabled = habilitar;
            gpbDisplayBascula.IsEnabled = habilitar;

            btnGuardar.IsEnabled = habilitar;
            btnCancelar.IsEnabled = habilitar;
        }

        private void Inicializarcontroles(bool habilitar)
        {
            if (BanderaNoIndividual)
            {
                txtNoIndividual.IsEnabled = true;
            }
            else
            {
                txtNoIndividual.IsEnabled = habilitar;
            }

            cboSexo.IsEnabled = habilitar;
            cboCalidad.IsEnabled = habilitar;
            cboCausaRechazo.IsEnabled = habilitar;
            cboClasificacion.IsEnabled = habilitar;
           
            if (!basculaConectada && this._configBasculaCorte.CapturaManual)
            {
                txtPesoCorte.IsEnabled = habilitar;
            }
            if (!termometroConectado && this._configTermometroCorte.CapturaManual)
            {
                txtTemperatura.IsEnabled = habilitar;
            }
            if (!rfidConectado)
            {
                txtAreteMetalico.IsEnabled = habilitar;
            }

            ckbVenta.IsEnabled = habilitar;

            gpbImplementacion.IsEnabled = habilitar;
            gpbGenerales.IsEnabled = habilitar;
            gpbOtrosDatosGanado.IsEnabled = habilitar;
            gpbDatosCorte.IsEnabled = habilitar;
            DisplayTemperatura.IsEnabled = habilitar;
            gpbDisplayBascula.IsEnabled = habilitar;

            btnGuardar.IsEnabled = habilitar;
        }


        /// <summary>
        /// Metrodo Verificar si existen Partidas programadas
        /// </summary>
        private bool ExisteAreteEnPartida(AnimalInfo animalInfo)
        {
            var existe = false;
            try
            {
                var corteGanadoPl = new CorteGanadoPL();
                AnimalInfo resultadoBusqueda = null;
                if(animalInfo.Arete != "")
                    resultadoBusqueda = corteGanadoPl.ExisteAreteEnPartida(animalInfo);
                else
                    resultadoBusqueda = corteGanadoPl.ExisteAreteMetalicoEnPartida(animalInfo);
                if (resultadoBusqueda != null)
                {
                    if (!resultadoBusqueda.Activo)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_AnimalMuerto,
                        MessageBoxButton.OK, MessageImage.Warning);
                        LimpiaParcial();
                        txtNoIndividual.Focus();
                        return true;
                    }

                    var corralPl = new CorralPL();
                    var corralArete = corralPl.ObtenerPorId(resultadoBusqueda.CorralID);

                    animalActual = resultadoBusqueda;

                    if (corralArete.TipoCorral.TipoCorralID == (int)TipoCorral.Produccion)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteGanado_AreteCorralProduccion,
                            MessageBoxButton.OK, MessageImage.Warning);
                        LimpiaParcial();
                        LimpiarCaptura(false);
                        cboSexo.IsEnabled = false;
                        cboCalidad.IsEnabled = false;
                        txtNoIndividual.Focus();
                        return true;
                    }

                    foreach (var animalmovimiento in resultadoBusqueda.ListaAnimalesMovimiento)
                    {
                        if (animalmovimiento.TipoMovimientoID == (int)TipoMovimiento.EntradaEnfermeria)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteGanado_AreteEnEnfermeria,
                            MessageBoxButton.OK, MessageImage.Warning);
                            LimpiaParcial();
                            txtNoIndividual.Focus();
                            return true;
                        }
                    }


                    if (resultadoBusqueda.CorralID != EntradaSeleccionada.CorralID)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format("{0} {1}",
                            Properties.Resources.CorteGanado_ErrorAreteNoPerteneceAPartidaPerteneceA,
                            resultadoBusqueda.FolioEntrada),
                            MessageBoxButton.OK, MessageImage.Warning);
                        LimpiaParcial();
                        txtNoIndividual.Focus();
                        return true;

                    }
                    else
                    {
                        // Calidad, Clasificación, Tipo de Ganado

                        var tipoGanadoPl = new TipoGanadoPL();
                        var tipoGanado = tipoGanadoPl.ObtenerPorID(resultadoBusqueda.TipoGanadoID);
                        var tipoGanadoInfo = new TipoGanadoInfo();

                        cboSexo.SelectedItem = Sexo.Macho.ToString()[0].ToString() == (tipoGanado.Sexo.ToString()[0].ToString()) ? Sexo.Macho : Sexo.Hembra;

                        AsignarTipoGanado(tipoGanadoInfo);

                        cboCalidad.SelectedValue = resultadoBusqueda.CalidadGanadoID;
                        cboClasificacion.SelectedValue = resultadoBusqueda.ClasificacionGanadoID;
                        txtAreteMetalico.Text = resultadoBusqueda.AreteMetalico;
                        txtNoPartida.Text = resultadoBusqueda.FolioEntrada.ToString();
                        cboCalidad.IsEnabled = false;
                        cboClasificacion.IsEnabled = false;
                        cboTipoGanado.IsEnabled = false;
                        cboSexo.IsEnabled = false;
                        txtAreteMetalico.Focus();
                    }
                    existe = true;
                }

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorExisteNoIndividual,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorExisteNoIndividual,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
                throw;
            }
            return existe;
        }

        /// <summary>
        /// Metodo Para obtener el tipo de ganado
        /// </summary>
        private void ObtenerTipoGanado()
        {
            try
            {
                if (cboSexo.SelectedItem == null) return;

                var tipoGanadoPl = new TipoGanadoPL();
                string sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                TipoGanadoInfo tipoGanadoInfo = null;
                if (!String.IsNullOrWhiteSpace(sexoGanado) && !String.IsNullOrWhiteSpace(txtPesoCorte.Text.Trim()))
                {
                    tipoGanadoInfo = tipoGanadoPl.ObtenerTipoGanadoSexoPeso(sexoGanado, int.Parse(txtPesoCorte.Text));
                    if (tipoGanadoInfo != null)
                    {
                        AsignarTipoGanado(tipoGanadoInfo);
                    }
                    else
                    {
                        cboTipoGanado.SelectedIndex = -1;
                    }
                }

                tipoGanadoInfo = new TipoGanadoInfo();
                
                if (_interfaceSalidoAnimalInfo == null)
                {
                    if (EntradaSeleccionada.TipoOrigen == (int)TipoOrganizacion.CompraDirecta)
                    {
                        ObtenerCorralDestinoSinTipoGanado(sexoGanado);
                    }
                }


               


                sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                //Se obtiene el tipo de ganado en base al TipoGanadoID de InterfazSalidaAnimal
                if (_interfaceSalidoAnimalInfo != null && _interfaceSalidoAnimalInfo.TipoGanado != null &&
                    sexoGanado == _interfaceSalidoAnimalInfo.TipoGanado.Sexo.ToString())
                {
                    tipoGanadoInfo = tipoGanadoPl.ObtenerPorID(_interfaceSalidoAnimalInfo.TipoGanado.TipoGanadoID);
                    AsignarTipoGanado(tipoGanadoInfo);
                }
                else
                {
                    //se determian el tipo de ganado en base al sexo y peso
                    if (!String.IsNullOrWhiteSpace(sexoGanado) && !String.IsNullOrWhiteSpace(txtPesoCorte.Text.Trim()))
                    {
                        tipoGanadoInfo = tipoGanadoPl.ObtenerTipoGanadoSexoPeso(sexoGanado,
                            int.Parse(txtPesoCorte.Text));
                        if (tipoGanadoInfo != null)
                        {
                            AsignarTipoGanado(tipoGanadoInfo);
                        }
                        else
                        {
                            cboTipoGanado.SelectedIndex = -1;
                        }
                    }
                }
                if (ckbVenta.IsChecked == false)
                {
                    if (EntradaSeleccionada.TipoOrigen == (int)TipoOrganizacion.CompraDirecta)
                    {
                        ObtenerCorralDestinoSinTipoGanado(sexoGanado);
                    }
                    else
                    {
                        ObtenerCorralDestino(tipoGanadoInfo, sexoGanado);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Metrodo Para asignar el tipo de ganadoal combo de tipo de ganado
        /// </summary>
        private void AsignarTipoGanado(TipoGanadoInfo tipoGanadoInfo)
        {
            if (tipoGanadoInfo == null) return;
            cboTipoGanado.Items.Clear();
            cboTipoGanado.Items.Add(tipoGanadoInfo);
            cboTipoGanado.SelectedIndex = 0;
        }

        //
        /// <summary>
        /// Metrodo Para obtener Corral Destino
        /// Al tener el sexo, peso corte y tipo de ganado, el sistema deberá determinar el corral destino en base a la configuración de corrales.
        /// </summary>
        private void ObtenerCorralDestino(TipoGanadoInfo tipoGanadoInfo, string sexoGanado)
        {
            try
            {
                var corralRangoPl = new CorralRangoPL();

                var corrarRangoInfoParam = new CorralRangoInfo();
                var diasHabilidades = int.Parse(ConfigurationManager.AppSettings["diasHabilCorral"]);
                corrarRangoInfoParam.Sexo = sexoGanado;
                corrarRangoInfoParam.TipoGanadoID = tipoGanadoInfo.TipoGanadoID;
                if (String.IsNullOrWhiteSpace(txtPesoCorte.Text)) return;

                corrarRangoInfoParam.RangoInicial = int.Parse(txtPesoCorte.Text);
                corrarRangoInfoParam.OrganizacionID = organizacionID;

                var corralRangoInfo = corralRangoPl.ObtenerCorralDestino(corrarRangoInfoParam, diasHabilidades);
                if (corralRangoInfo != null && corralRangoInfo.Any())
                {
                    txtCorralDestino.Text = corralRangoInfo[0].Codigo.ToString(CultureInfo.InvariantCulture);
                    _corralInfoGen = new CorralInfo
                    {
                        CorralID = corralRangoInfo[0].CorralID,
                        Codigo = corralRangoInfo[0].Codigo.ToString(CultureInfo.InvariantCulture)
                    };
                    _corralInfoGen = ObtenerCorral(_corralInfoGen.CorralID);
                    ObtenerLoteParaCorralDestino();
                }
                else
                {
                    txtCorralDestino.Text = string.Empty;
                    lblRestantes.Content = string.Empty;
                    lblRestantesNumero.Content = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Metrodo Para obtener Corral Destino
        /// Al tener el sexo, peso corte y tipo de ganado, el sistema deberá determinar el corral destino en base a la configuración de corrales.
        /// </summary>
        private void ObtenerCorralDestinoSinTipoGanado(string sexoGanado)
        {
            try
            {

                var corralRangoPl = new CorralRangoPL();
                var diasBloqueo = int.Parse(ConfigurationManager.AppSettings["diasHabilCorral"]);
                var corrarRangoInfoParam = new CorralRangoInfo { Sexo = sexoGanado };
                if (String.IsNullOrWhiteSpace(txtPesoCorte.Text)) return;

                corrarRangoInfoParam.RangoInicial = int.Parse(txtPesoCorte.Text);
                corrarRangoInfoParam.OrganizacionID = organizacionID;

                var corralRangoInfo = corralRangoPl.ObtenerCorralDestinoSinTipoGanado(corrarRangoInfoParam, diasBloqueo);
                if (corralRangoInfo != null && corralRangoInfo.Any())
                {
                    txtCorralDestino.Text = corralRangoInfo[0].Codigo.ToString(CultureInfo.InvariantCulture);
                    _corralInfoGen = new CorralInfo
                    {
                        CorralID = corralRangoInfo[0].CorralID,
                        Codigo = corralRangoInfo[0].Codigo.ToString(CultureInfo.InvariantCulture)
                    };
                    _corralInfoGen = ObtenerCorral(_corralInfoGen.CorralID);
                    ObtenerLoteParaCorralDestino();
                }
                else
                {
                    txtCorralDestino.Clear();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo que valida la temperatura del animal y marcar el tratamiento 26 de acuerdo a la regla de negocio
        /// </summary>
        /// <param name="temperatura"></param>
        private void ValidarTemperatura(double temperatura)
        {
            try
            {
                if (temperatura >= _maxTemperaturaAnimal && _listaTratamientos != null)
                {
                    foreach (var item in _listaTratamientos)
                    {
                        if (item.CodigoTratamiento == _codigoTratamientoTemperatura)
                        {
                            item.Seleccionado = true;
                            dgTratamientos.ItemsSource = null;

                            dgTratamientos.ItemsSource = _listaTratamientos;
                            break;
                        }
                    }
                }
                else
                {
                    if (_listaTratamientos != null)
                    {
                        foreach (var item in _listaTratamientos)
                        {
                            if (item.CodigoTratamiento == _codigoTratamientoTemperatura)
                            {
                                item.Seleccionado = false;
                                dgTratamientos.ItemsSource = null;
                                dgTratamientos.ItemsSource = _listaTratamientos;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Leer la configuracion de la pantalla
        /// </summary>
        private void LeerConfiguracion()
        {
            try
            {
                var parametrosPl = new ConfiguracionParametrosPL();

                /* Obtener Configuracion de tratmiento temperatura*/
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.TratamientoTemperatura,
                    OrganizacionID = organizacionID
                };
                var parametros = parametrosPl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado);
                foreach (var parametro in parametros)
                {
                    ParametrosTratamientoTemperatura enumTemporal;
                    var res = ParametrosTratamientoTemperatura.TryParse(parametro.Clave, out enumTemporal);

                    if (res)
                    {
                        switch (enumTemporal)
                        {
                            case ParametrosTratamientoTemperatura.codigoTratamientoTemperatura:
                                _codigoTratamientoTemperatura = int.Parse(parametro.Valor, CultureInfo.InvariantCulture);
                                break;
                            case ParametrosTratamientoTemperatura.temperaturaAnimal:
                                _maxTemperaturaAnimal = double.Parse(parametro.Valor, CultureInfo.InvariantCulture);
                                break;
                        }
                    }
                }

            }
            catch (InvalidCastException ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ConvertirConfiguracion, MessageBoxButton.OK, MessageImage.Error);
                DeshabilitarControles(false);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ConvertirConfiguracion, MessageBoxButton.OK, MessageImage.Error);
                DeshabilitarControles(false);
                throw;
            }
        }

        /// <summary>
        /// Metodo de validacion de capturas en blanco
        /// </summary>
        private ResultadoValidacion CompruebaCamposEnBlanco()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(txtNoPartida.Text) && !txtNoPartidaGrupo.Text.Contains("|"))
            {
                imgBuscar.Focus();
                resultado.Resultado = false;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtNoIndividual.Text))
            {
                txtNoIndividual.Focus();

                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereNoIndividual;
                return resultado;
            }

            if (String.IsNullOrEmpty(cboSexo.Text))
            {
                cboSexo.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereSexo;
                return resultado;
            }

            if ((String.IsNullOrEmpty(cboCalidad.Text) || cboCalidad.Text == "Seleccione"))
            {
                cboCalidad.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereCalidad;
                return resultado;
            }

            if (String.IsNullOrEmpty(txtPesoCorte.Text))
            {
                txtPesoCorte.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequierePesoCorte;
                return resultado;
            }
            if ((String.IsNullOrEmpty(cboTipoGanado.Text) || cboTipoGanado.Text == "Seleccione"))
            {
                cboTipoGanado.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereTipoGanado;
                return resultado;
            }

            if ((String.IsNullOrEmpty(cboClasificacion.Text) || cboClasificacion.Text == "Seleccione"))
            {
                cboClasificacion.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereClasificacion;
                return resultado;
            }

            if (ckbVenta.IsChecked != true &&
                (ckbVenta.IsChecked.Value && String.IsNullOrEmpty(txtCorralDestino.Text)))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CorteGanado_ValidaCorrarDestino,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                txtCorralDestino.Focus();
                if (!basculaConectada)
                {
                    txtPesoCorte.IsEnabled = true;
                }
                else
                {
                    _tempTomada = false;
                    InicializarDispositivos();
                }

                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereCorralDestino;
                return resultado;
            }
            if (ckbVenta.IsChecked == false &&
                (!ckbVenta.IsChecked.Value && String.IsNullOrEmpty(txtCorralDestino.Text)))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ValidaCorrarDestino, MessageBoxButton.OK, MessageImage.Warning);
                txtCorralDestino.Focus();
                if (!basculaConectada)
                {
                    txtPesoCorte.IsEnabled = true;
                }
                else
                {
                    _pesoTomado = false;
                    InicializarDispositivos();
                }
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereCorralDestino;
                return resultado;
            }

            if (ckbVenta.IsChecked == false)
            {
                if (String.IsNullOrEmpty(txtTemperatura.Text))
                {
                    txtTemperatura.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CorteGanado_RequiereTemperatura;
                    return resultado;
                }
                if (String.IsNullOrEmpty(cboPaletas.Text) || cboPaletas.Text.Trim() == "Seleccione")
                {
                    cboPaletas.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CorteGanado_RequierePaletas;
                    return resultado;
                }
            }
            if ((String.IsNullOrEmpty(cboImplantador.Text) || cboImplantador.Text.Trim() == "Seleccione"))
            {
                cboImplantador.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereImplentador;
                return resultado;
            }
            if (String.IsNullOrEmpty(dtpFechaCorte.Text))
            {
                dtpFechaCorte.Focus();
                resultado.Resultado = false;
                return resultado;
            }

            if (String.IsNullOrEmpty(dtpFechaRecepcion.Text))
            {
                dtpFechaRecepcion.Focus();
                resultado.Resultado = false;
                return resultado;
            }
            if (String.IsNullOrEmpty(txtOrigen.Text))
            {
                txtOrigen.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereOrigen;
                return resultado;
            }

            if (String.IsNullOrEmpty(txtProveedor.Text))
            {
                txtProveedor.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereProveedor;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Metodo que obtiene el lote al corral destino
        /// </summary>
        private void ObtenerLoteParaCorralDestino()
        {
            try
            {
                //Se busca el corral para ver si tiene Lote de lo contraria se le creara uno
                var lotePl = new LotePL();
                // si no tiene lote validar si el corral ya tiene lote
                var loteInfo = new LoteInfo
                {
                    OrganizacionID = organizacionID,
                    CorralID = _corralInfoGen.CorralID
                };
                EntradaSeleccionada.Lote = lotePl.ObtenerPorCorralID(loteInfo);
                if (EntradaSeleccionada.Lote != null)
                {
                    int calculo = EntradaSeleccionada.Lote.Cabezas;
                    lblRestantes.Content = String.Format("{0}",
                        Properties.Resources.CorteGanado_Restantes);
                    lblRestantesNumero.Content = _corralInfoGen.Capacidad - calculo;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo que obtiene el lote al corral origen
        /// </summary>
        private LoteInfo ObtenerLoteParaCorralOrigen(int idCorral)
        {
            LoteInfo loteInfo = null;
            try
            {
                //Se busca el corral para ver si tiene Lote de lo contraria se le creara uno
                var lotePl = new LotePL();
                loteInfo = lotePl.ObtenerPorCorralCerrado(organizacionID, idCorral);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return loteInfo;
        }

        private void InicializarDispositivos()
        {
            try
            {
                if (this.basculaConectada)
                {
                    btnActualizarBascula.IsEnabled = true;
                    if (!_pesoTomado)
                    {
                        this.pesoCapturadoGlobal = string.Empty;
                        this.pesoPercial = string.Empty;
                        this.vecesPesoCotejado = 0;
                        _timerListeningPort = new DispatcherTimer();
                        _timerListeningPort.Interval = new TimeSpan(0, 0, 1);
                        _timerListeningPort.Tick += (TimerLisnteingPort_Tick);
                        _timerListeningPort.Start();
                    }
                }
                    btnActualizarBascula.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            try
            {
                if (!_tempTomada)
                {
                    _countTemp = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            try
            {
                if (rfidConectado)
                {
                    if (_spManagerRFID != null)
                        _spManagerRFID.StartListening(_configRFIDCorte.Puerto,
                            _configRFIDCorte.Baudrate,
                            _configRFIDCorte.Paridad,
                            _configRFIDCorte.Databits,
                            _configRFIDCorte.BitStop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            VerificarPermisosCapturaManual();
        }

        /// <summary>
        /// Obtiene el proveedor del ganado
        /// </summary>
        /// 
        private void ObtenerProveedor()
        {
            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            EntradaGanadoCosteoInfo entradaGanadoCosteo =
                entradaGanadoCosteoPL.ObtenerPorEntradaID(EntradaSeleccionada.EntradaGanadoID);
            if (entradaGanadoCosteo != null)
            {
                if (entradaGanadoCosteo.ListaCostoEntrada != null && entradaGanadoCosteo.ListaCostoEntrada.Any())
                {
                    ProveedorInfo proveedor =
                        entradaGanadoCosteo.ListaCostoEntrada.Where(costo => costo.Costo.CostoID == 1).Select(
                            prov => prov.Proveedor).FirstOrDefault();
                    if (proveedor != null)
                    {
                        txtProveedor.Text = proveedor.Descripcion;
                        if (string.IsNullOrWhiteSpace(proveedor.Descripcion))
                        {
                            txtProveedor.Text =
                                entradaGanadoCosteo.ListaCostoEntrada.Select(cuenta => cuenta.DescripcionCuenta).
                                    FirstOrDefault();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para validar si en las partidas se encuentra un centro
        /// </summary>
        /// <returns></returns>
        private bool ExiteCentroEnOrigen()
        {
            var resp = false;
            try
            {
                string[] tipoOrigen = EntradaSeleccionada.TipoOrigenAgrupado.Split('|');
                foreach (var tipo in tipoOrigen)
                {
                    int tip = int.Parse(tipo);
                    if (tip == (int)TipoOrganizacion.Centro)
                    {
                        resp = true;
                    }
                    if (tip == (int)TipoOrganizacion.Cadis)
                    {
                        resp = true;
                    }
                    if (tip == (int)TipoOrganizacion.Praderas)
                    {
                        resp = true;
                    }
                    if (tip == (int)TipoOrganizacion.Descanso)
                    {
                        resp = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return resp;
        }

        /// <summary>
        /// Metodo para validar si el focus se envia al arete metalico, esto si no esta conectado
        /// el lector de RFID
        /// </summary>
        private void ValdiarFocusAreteMetalico()
        {
            if (rfidConectado || txtAreteMetalico.Text.Trim().Length > 0)
            {
                ValidarFocusDespuesAreteMetalico();
                return;
            }
            else
            {
                txtAreteMetalico.Focus();
            }
            if (txtNoIndividual.IsEnabled)
            {
                txtNoIndividual.Focus();
            }
        }

        /// <summary>
        /// Valdiar el siguiente focus de arete metalico
        /// </summary>
        private void ValidarFocusDespuesAreteMetalico()
        {
            if (cboSexo.IsEnabled)
            {
                cboSexo.Focus();
            }
            else
            {
                if (cboCalidad.IsEnabled)
                {
                    cboCalidad.Focus();
                }
                else
                {
                    cboCausaRechazo.Focus();
                }

            }
        }

        /// <summary>
        /// Validar si los medicamentos del tratamiento seleccionado no existes ya en los 
        /// tratamientos seleccionados anteriormente
        /// </summary>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        private ResultadoValidacion ExistenProductosEnTratamientosSeleccionados(TratamientoInfo tratamiento)
        {
            var resultadoValidacion = new ResultadoValidacion();
            try
            {

                //Se obtiene la lista de productos de los tratamientos seleccionados
                IList<TratamientoInfo> listaTratamientosChecado = _listaTratamientos.Where(
                    item => item.Seleccionado && item.Habilitado
                            && item.TipoTratamiento != 3).ToList();

                if (tratamiento.TipoTratamiento != 3)
                {
                    // este ciclo es para recorrer los productos del tratamiento que se acaba de seleccionar
                    // con el fin de validar q no se repita el producto
                    foreach (var productoSeleccionado in tratamiento.Productos)
                    {
                        //Este ciclo recorre los tratamientos de Chekeados del grid
                        foreach (var tratamientosChecado in listaTratamientosChecado)
                        {

                            //Este ciclo recorre los productos de los tratamientos chekeados
                            foreach (var productoChecado in tratamientosChecado.Productos)
                            {
                                if (productoSeleccionado.ProductoId != productoChecado.ProductoId) continue;
                                //si el producto nuevo ya existe se regresa el medicamento al que pertenece
                                resultadoValidacion.Resultado = true;
                                resultadoValidacion.Mensaje = tratamientosChecado.Descripcion;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return resultadoValidacion;
        }

        /// <summary>
        /// Funcion para mostrar mensajes en base al resultado obtenido de la validacion
        /// </summary>
        /// <param name="resultadoValidacion"></param>
        private void MostrarMensajesComprobarExistenciaTratamientos(ResultadoValidacion resultadoValidacion)
        {
            switch (resultadoValidacion.TipoResultadoValidacion)
            {
                case TipoResultadoValidacion.InventarioInsuficiente:
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format("{0}{1}{2}",
                            Properties.Resources.CorteGanado_InventarioInsuficienteEnAlmacen,
                            resultadoValidacion.Mensaje,
                            Properties.Resources.CorteGanado_ProductoInexistenteEnAlmacen2),
                        MessageBoxButton.OK, MessageImage.Stop);
                    break;
                case TipoResultadoValidacion.ProductoInexistente:
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format("{0}{1}{2}",
                            Properties.Resources.CorteGanado_ProductoInexistenteEnAlmacen1,
                            resultadoValidacion.Mensaje,
                            Properties.Resources.CorteGanado_ProductoInexistenteEnAlmacen2),
                        MessageBoxButton.OK, MessageImage.Stop);
                    break;
                case TipoResultadoValidacion.TratamientosNoSeleccionados:
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteGanado_SeleccionarTratamientos,
                        MessageBoxButton.OK, MessageImage.Stop);
                    break;
            }
        }

        //Comprueba si el Nº induvidual o el arete metalico ya estan registrados

        /// <summary>
        /// Metodo para almacenar los Tratamientos y descontar del almacen
        /// </summary>
        private void GuardarSalidaPorConsumo(AnimalMovimientoInfo animalMovimientoInfo)
        {
            try
            {
                var almacenpl = new AlmacenPL();

                var almacenMovimientoInfo = new AlmacenMovimientoInfo
                {
                    AlmacenID = almacenInfo.AlmacenID,
                    AnimalMovimientoID = animalMovimientoInfo.AnimalMovimientoID,
                    TipoMovimientoID = (int)TipoMovimiento.SalidaPorConsumo,
                    Status = (int)EstatusInventario.Aplicado,
                    Observaciones = string.Empty,
                    UsuarioCreacionID = usuario,
                    AnimalID = animalMovimientoInfo.AnimalID,
                    CostoID = (int)Costo.MedicamentoDeImplante,
                };
                List<TratamientoInfo> tratamientos = _listaTratamientos.Where(item => item.Seleccionado && item.Habilitado).ToList();
                almacenpl.GuardarDescontarTratamientos(tratamientos, almacenMovimientoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        private void Guardar()
        {
            try
            {
                
                ParametroPL parametroPl = new ParametroPL();
                ParametroOrganizacionPL organizacion = new ParametroOrganizacionPL();
                ParametroOrganizacionInfo organizacionInfo = new ParametroOrganizacionInfo();
                List<ParametroInfo> listaParametro = parametroPl.ObtenerTodos(EstatusEnum.Activo).ToList();
                ParametroInfo parametroOrganizacion = null;
                if (listaParametro != null)
                {
                    
                    parametroOrganizacion = listaParametro.Where(parametro => parametro.Clave == ParametrosEnum.LECTURADOBLEARETE.ToString()).FirstOrDefault();
                    if (parametroOrganizacion != null)
                    {
                        
                        organizacionInfo = organizacion.ObtenerPorOrganizacionIDClaveParametro(int.Parse(Application.Current.Properties["OrganizacionID"].ToString()), ParametrosEnum.LECTURADOBLEARETE.ToString());
                        if (organizacionInfo != null)
                        {
                            if (organizacionInfo.Activo == EstatusEnum.Activo)
                            {
                                if (organizacionInfo.Valor.ToUpper() == "TRUE")
                                {
                                    if (txtNoIndividual.Text.Trim() == "")
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.CorteGanado_FaltaArete,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                        imgloading.Visibility = Visibility.Hidden;
                                        btnGuardar.IsEnabled = true;
                                        return;
                                    }

                                    if (txtAreteMetalico.Text.Trim() == "")
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.CorteGanado_FaltaAreteRFID,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                        imgloading.Visibility = Visibility.Hidden;
                                        btnGuardar.IsEnabled = true;
                                        return;
                                    }

                                    if (txtAreteMetalico.Text == txtNoIndividual.Text)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.CorteGanado_Igual_Individual_RFID,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                        imgloading.Visibility = Visibility.Hidden;
                                        btnGuardar.IsEnabled = true;
                                        return;
                                    }
                                }
                            }
                            
                        }

                    }
                
                }

                var usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                var entradaGanadoSobrantePL = new EntradaGanadoSobrantePL();
                var interfaceSalidaAnimalPL = new InterfaceSalidaAnimalPL();
                var animalInfo = new AnimalInfo();
                var animalMovimientoInfo = new AnimalMovimientoInfo();

                var resultadoValidacion = CompruebaCamposEnBlanco();
                if (resultadoValidacion.Resultado)
                {

                            if (txtNoIndividual.IsEnabled)
                            {
                                resultadoValidacion = ComprobarAreteRegistrado(txtNoIndividual.Text, "");
                                if (resultadoValidacion.Resultado)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.CorteGanado_AreteRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                                    imgloading.Visibility = Visibility.Hidden;
                                    btnGuardar.IsEnabled = true;
                                    txtNoIndividual.Text = "";
                                    txtNoIndividual.Focus();
                                    return;
                                }
                            }
                            else
                            {
                                resultadoValidacion = ComprobarAreteRegistrado("", txtAreteMetalico.Text);
                                if (resultadoValidacion.Resultado)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.CorteGanado_AreteRFIDRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                                    imgloading.Visibility = Visibility.Hidden;
                                    btnGuardar.IsEnabled = true;
                                    txtAreteMetalico.Text = "";
                                    txtAreteMetalico.Focus();
                                    return;
                                }
                            }
                    
                    resultadoValidacion = ComprobarExistenciaTratamientos();
                    if (resultadoValidacion.Resultado)
                    {
                        var transactionOption = new TransactionOptions();
                        transactionOption.IsolationLevel = IsolationLevel.ReadUncommitted;
                        using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOption))
                        {
                            // Se almacena el animal
                            animalInfo = GuardarAnimal(usuarioId);
                            if (animalInfo == null || animalInfo.AnimalID <= 0) return;

                            //Se relaciona con interface salida animal
                            if (_interfaceSalidoAnimalInfo != null)
                            {
                                _interfaceSalidoAnimalInfo.Arete = animalInfo.Arete;
                                _interfaceSalidoAnimalInfo.AreteMetalico = animalInfo.AreteMetalico;
                                interfaceSalidaAnimalPL.GuardarAnimalID(_interfaceSalidoAnimalInfo, animalInfo.AnimalID);
                            }

                            //Se almacena el movimiento
                            animalMovimientoInfo = GuardarAnimalMovimiento(animalInfo, usuarioId);
                            if (animalMovimientoInfo == null || animalMovimientoInfo.AnimalMovimientoID <= 0) return;

                            //Se almacenan los tratamientos(Almacena, Descuenta, AnimalCosto)
                            GuardarSalidaPorConsumo(animalMovimientoInfo);

                            //Si es cabeza sobrante de alguna partida
                            //CabezasSobrantesPorEntradaInfo cabezasSobrantesEntrada = null;
                            CabezasSobrantesPorEntradaInfo cabezasSobrantesEntrada = entradasCabezasSobrantes.FirstOrDefault(c => c.FolioEntrada == EntradaSeleccionada.FolioEntrada);
                            if (cabezasSobrantesEntrada != null)
                            {
                                // Si se detecto q se esta cortando una cabeza sobrante
                                if (esCabezasSobrante)
                                {
                                    var entradaGanadoSobranteInfo = new EntradaGanadoSobranteInfo
                                    {
                                        EntradaGanado = cabezasSobrantesEntrada.EntradaGanado,
                                        Animal = animalInfo,
                                        Importe = 0,
                                        Costeado = false,
                                        UsuarioCreacionID = usuarioId
                                    };
                                    entradaGanadoSobranteInfo.EntradaGanado.TipoOrganizacionOrigenId =
                                        cabezasSobrantesEntrada.OrganizacionOrigen.TipoOrganizacion.TipoOrganizacionID;
                                    entradaGanadoSobranteInfo.EntradaGanado.OrganizacionID =
                                        organizacionID;

                                    // Se guarda la Entrada Ganado Sobrante
                                    entradaGanadoSobrantePL.GuardarEntradaGanadoSobrante(entradaGanadoSobranteInfo);
                                    cabezasSobrantesEntrada.CabezasSobrantesCortadas++;
                                }
                                cabezasSobrantesEntrada.CabezasCortadas++;
                            }

                            var iTot = int.Parse(lblProgramadasCorteGenerado.Content.ToString());
                            lblProgramadasCorteGenerado.Content = iTot - 1;

                            var iTotCortadas = int.Parse(lblTotalCortadasGenerado.Content.ToString());
                            lblTotalCortadasGenerado.Content = iTotCortadas + 1;
                            ValidarCierreCorralPartida();

                            scope.Complete();
                        }

                        LimpiarCaptura(false);
                        _pesoTomado = false;
                        _tempTomada = false;
                       

                        txtTemperatura.Text = string.Empty;
                        BanderaNoIndividual = true;
                        Inicializarcontroles(false);
                        BanderaNoIndividual = false;
                        IniciarTermometro(false);
                        VerificarPermisosCapturaManual();
                        

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CorteGanado_SeguardoCorrectamenteElCorte,
                                MessageBoxButton.OK, MessageImage.Correct);
                        txtNoIndividual.Focus();
                        imgloading.Visibility = Visibility.Hidden;
                        btnGuardar.IsEnabled = true;
                    }
                    else
                    {
                        MostrarMensajesComprobarExistenciaTratamientos(resultadoValidacion);
                        imgloading.Visibility = Visibility.Hidden;
                        btnGuardar.IsEnabled = true;
                    }
                }
                else
                {
                    var mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                                      ? Properties.Resources.DatosBlancos_CorteGanado
                                      : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      mensaje, MessageBoxButton.OK, MessageImage.Stop);
                    imgloading.Visibility = Visibility.Hidden;
                    btnGuardar.IsEnabled = true;
                }
            }
            catch (InvalidPortNameException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ErrorGuardar_CorteGanado + ex.Message,
                        MessageBoxButton.OK, MessageImage.Error);
                imgloading.Visibility = Visibility.Hidden;
                btnGuardar.IsEnabled = true;
                throw;
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Error);
                if (String.IsNullOrEmpty(txtNoIndividual.Text))
                    txtNoIndividual.IsEnabled = false;
                imgloading.Visibility = Visibility.Hidden;
                btnGuardar.IsEnabled = true;
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
                if (String.IsNullOrEmpty(txtNoIndividual.Text))
                    txtNoIndividual.IsEnabled = false;
                imgloading.Visibility = Visibility.Hidden;
                btnGuardar.IsEnabled = true;
                throw;
            }
        }

        private void ObtenerTratamiento()
        {
            if (txtPesoCorte.Text.Trim().Length > 0 && cboSexo.SelectedItem != null &&
                cboSexo.SelectedItem.ToString() != "Seleccione")
            {
                try
                {
                    var tratamientoPl = new TratamientoPL();
                    var tratamientoInfo = new TratamientoInfo
                    {
                        OrganizacionId = organizacionID,
                        Sexo =
                            (Sexo)Enum.Parse(typeof(Sexo), cboSexo.SelectedItem.ToString()),
                        Peso = int.Parse(txtPesoCorte.Text)
                    };
                    // este es un bit si es 1 es metafilaxia
                    var metafilaxia = (EntradaSeleccionada.EsMetaFilaxia
                                           ? Metafilaxia.EsMetafilaxia
                                           : Metafilaxia.NoEsMetafilaxia);

                    _listaTratamientos = tratamientoPl.ObtenerTipoTratamientosCorte(tratamientoInfo,
                                                                                    metafilaxia);
                    if (_listaTratamientos == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.ExisteTratamiento_CorteGanado,
                                          MessageBoxButton.OK, MessageImage.Warning);
                        dgTratamientos.ItemsSource = null;
                        _listaTratamientos = null;

                    }
                    else
                    {
                        if (metafilaxia == Metafilaxia.EsMetafilaxia)
                        {
                            foreach (var tratamiento in _listaTratamientos)
                            {
                                if (tratamiento.CodigoTratamiento >= 200 && tratamiento.CodigoTratamiento <= 232)
                                {
                                    tratamiento.Seleccionado = true;
                                    break;
                                }
                            }
                        }

                        dgTratamientos.ItemsSource = _listaTratamientos;
                        if (!string.IsNullOrEmpty(txtTemperatura.Text))
                        {
                            var valor = double.Parse(txtTemperatura.Text);
                            ValidarTemperatura(valor);
                        }

                    }
                }
                catch (FormatException ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.CorteGanado_PesoCorteInvalido,
                   MessageBoxButton.OK, MessageImage.Warning);
                    FocoPeso = true;
                    txtPesoCorte.Clear();
                    cboTipoGanado.SelectedIndex = -1;
                    txtCorralDestino.Text = string.Empty;
                    lblRestantes.Content = string.Empty;
                    lblRestantesNumero.Content = string.Empty;
                    dgTratamientos.ItemsSource = null;
                    _listaTratamientos = null;
                    throw;

                }
            }

        }

        /// <summary>
        /// Genera un Objeto de Tipo Lote
        /// </summary>
        private void GeneraLote()
        {
            var lotePl = new LotePL();
            LoteInfo loteInfo = null;

            if (EntradaSeleccionada != null && EntradaSeleccionada.Lote != null)
            {
                loteInfo = lotePl.ObtenerPorOrganizacionIdLote(organizacionID, EntradaSeleccionada.Lote.Lote);
            }

            if (loteInfo == null)
            {
                loteInfo = (EntradaSeleccionada.Lote != null ? EntradaSeleccionada.Lote : new LoteInfo());
                var tipoOrganizacion = ObtenerTiposOrigen(EntradaSeleccionada.TipoOrigen);

                loteInfo.TipoProcesoID = tipoOrganizacion.TipoProceso.TipoProcesoID;
                loteInfo.OrganizacionID = organizacionID;
                loteInfo.UsuarioCreacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);

                if (_corralInfoGen.CorralID > 0)
                {
                    var corral = ObtenerCorral(_corralInfoGen.CorralID);
                    _corralInfoGen = corral;
                    if (corral != null)
                    {
                        loteInfo.CorralID = _corralInfoGen.CorralID;
                        loteInfo.TipoCorralID = corral.TipoCorral.TipoCorralID;
                    }
                }
                loteInfo.Activo = EstatusEnum.Activo;
                loteInfo.DisponibilidadManual = false;
                loteInfo.Cabezas = Convert.ToInt32(0);
                loteInfo.CabezasInicio = Convert.ToInt32(0);
            }

            EntradaSeleccionada.Lote = loteInfo;
        }

        /// <summary>
        /// Obtiene los Datos de Corral
        /// </summary>
        private CorralInfo ObtenerCorral(int corralId)
        {
            CorralInfo corral = null;
            try
            {
                var corralPl = new CorralPL();
                corral = corralPl.ObtenerPorId(corralId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return corral;
        }

        /// <summary>
        /// Obtiene los Tipos de Origen
        /// </summary>
        /// <param name="tipoOrganizacion">Tipo de Organizacion</param>
        private TipoOrganizacionInfo ObtenerTiposOrigen(int tipoOrganizacion)
        {
            TipoOrganizacionInfo resp = null;
            try
            {
                var tipoOrganizacionPl = new TipoOrganizacionPL();
                var tiposOrganizacion = tipoOrganizacionPl.ObtenerTodos();
                if (tiposOrganizacion != null)
                {
                    foreach (var tipoOrganizacionInfo in tiposOrganizacion)
                    {
                        if (tipoOrganizacionInfo.TipoOrganizacionID == tipoOrganizacion)
                        {
                            resp = tipoOrganizacionInfo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
                throw;
            }
            return resp;
        }

        /// <summary>
        /// Guarda la informacion del animal
        /// </summary>
        /// <param name="usuarioId">Usuario</param>
        /// <returns>AnimalInfo</returns>
        private AnimalInfo GuardarAnimal(int usuarioId)
        {
            var animalPl = new AnimalPL();
            var animalInfo = new AnimalInfo
            {
                Arete = txtNoIndividual.Text,
                AreteMetalico = txtAreteMetalico.Text,
                FechaCompra = dtpFechaRecepcion.DisplayDate.Date
            };

            //Se obtienen los valores para almacenar en Animal
            var tipoGanado = (TipoGanadoInfo)cboTipoGanado.SelectedItem;
            if (tipoGanado != null)
            {
                animalInfo.TipoGanadoID = tipoGanado.TipoGanadoID;
            }
            var calidadId = (CalidadGanadoInfo)cboCalidad.SelectedItem;
            if (calidadId.CalidadGanadoID != null)
            {
                animalInfo.CalidadGanadoID = (int)calidadId.CalidadGanadoID;
            }
            var clasificacionId = (ClasificacionGanadoInfo)cboClasificacion.SelectedItem;
            if (calidadId.CalidadGanadoID != null)
            {
                animalInfo.ClasificacionGanadoID = clasificacionId.ClasificacionGanadoID;
            }

            /* Se se encuentra regitradoen interfaz se toman los pesos de ahi */
            animalInfo.PesoCompra = 0;
            animalInfo.PesoLlegada = 0;
            if (EntradaSeleccionada.TipoOrigen != (int)TipoOrganizacion.CompraDirecta)
            {
                //Quiere decir que es nuestra organizacion
                if (_interfaceSalidoAnimalInfo != null)
                {
                    animalInfo.PesoCompra = (int)_interfaceSalidoAnimalInfo.PesoCompra;
                    if (EntradaSeleccionada.PesoLlegada != 0)
                    {
                        animalInfo.PesoLlegada = ((EntradaSeleccionada.PesoOrigen / EntradaSeleccionada.PesoLlegada) *
                                                   (int)_interfaceSalidoAnimalInfo.PesoOrigen);
                    }
                    string sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
                    if (_interfaceSalidoAnimalInfo.TipoGanado != null &&
                        sexoGanado != _interfaceSalidoAnimalInfo.TipoGanado.Sexo.ToString())
                    {
                        animalInfo.CambioSexo = true;
                    }
                }
                else
                {
                    if (!esCabezasSobrante)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          "Error al consultar los datos de Origen.", MessageBoxButton.OK,
                                          MessageImage.Error);
                        return null;
                    }
                }

            }

            animalInfo.OrganizacionIDEntrada = organizacionID;
            if (EntradaSeleccionada.EntradaGanadoCodigoAgrupados.Count > 1)
            {
                if (_interfaceSalidoAnimalInfo != null)
                {
                    EntradaGanadoCodigoAgrupado entradaGanadoCodigoAgrupado = EntradaSeleccionada.
                        EntradaGanadoCodigoAgrupados.FirstOrDefault(
                            id => id.FolioOrigen == _interfaceSalidoAnimalInfo.SalidaID);
                    if (entradaGanadoCodigoAgrupado != null)
                    {
                        EntradaSeleccionada.FolioEntrada = Convert.ToInt32(entradaGanadoCodigoAgrupado.FolioEntrada);
                    }
                }
            }
            animalInfo.FolioEntrada = EntradaSeleccionada.FolioEntrada;
            var paletas = cboPaletas.SelectedValue;
            if (paletas != null)
            {
                animalInfo.Paletas = (int)paletas;
            }

            var causeRechaso = (CausaRechazoInfo)cboCausaRechazo.SelectedItem;
            animalInfo.CausaRechadoID = causeRechaso.CausaRechazoID;
            animalInfo.Venta = ckbVenta.IsChecked.Value;

            animalInfo.Cronico = false;
            animalInfo.Activo = true;
            animalInfo.UsuarioCreacionID = usuarioId;

            if (animalActual != null)
            {
                animalInfo.FechaCompra = animalActual.FechaCompra;
            }
            else
            {
                if (_interfaceSalidoAnimalInfo != null)
                {
                    animalInfo.FechaCompra = _interfaceSalidoAnimalInfo.FechaCompra;
                }
            }

            //Se manda a guardar el registro en base de dato
            animalInfo = animalPl.GuardarAnimal(animalInfo);

            return animalInfo;
        }
        /// <summary>
        /// Almacena el registro de animal movimiento
        /// </summary>
        /// <param name="animalInfo">Informacion del Animal</param>
        /// <param name="usuarioId">Id del Usuario</param>
        /// <returns></returns>
        private AnimalMovimientoInfo GuardarAnimalMovimiento(AnimalInfo animalInfo, int usuarioId)
        {
            var animalMovimientoPl = new AnimalMovimientoPL();
            var lotePl = new LotePL();
            var animalMovimientoInfo = new AnimalMovimientoInfo();
            //Se cargan los datos para el Movimiento
            animalMovimientoInfo.AnimalID = animalInfo.AnimalID;
            animalMovimientoInfo.OrganizacionID = organizacionID;
            if (_corralInfoGen != null)
            {
                animalMovimientoInfo.CorralID = _corralInfoGen.CorralID;
            }

            if (EntradaSeleccionada != null && EntradaSeleccionada.Lote != null)
            {
                animalMovimientoInfo.LoteID = EntradaSeleccionada.Lote.LoteID;
            }
            else
            {
                //Sino tiene asignado un Lote Se crea uno
                GeneraLote();
                if (EntradaSeleccionada != null && EntradaSeleccionada.Lote != null)
                {
                    EntradaSeleccionada.Lote.LoteID = lotePl.GuardaLote(EntradaSeleccionada.Lote);
                    EntradaSeleccionada.Lote = lotePl.ObtenerPorId(EntradaSeleccionada.Lote.LoteID);
                    animalMovimientoInfo.LoteID = EntradaSeleccionada.Lote.LoteID;
                }
            }

            animalMovimientoInfo.FechaMovimiento = DateTime.Parse(dtpFechaCorte.Text);
            animalMovimientoInfo.Peso = int.Parse(txtPesoCorte.Text);

            if (txtTemperatura.Text != string.Empty)
            {
                animalMovimientoInfo.Temperatura = double.Parse(txtTemperatura.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            }
            animalMovimientoInfo.TipoMovimientoID = (int)TipoMovimiento.Corte;

            animalMovimientoInfo.TrampaID = trampaInfo.TrampaID;

            var operador = (OperadorInfo)cboImplantador.SelectedItem;

            if (operador != null)
            {
                animalMovimientoInfo.OperadorID = operador.OperadorID;
            }

            animalMovimientoInfo.Observaciones = txtObservaciones.Text;
            animalMovimientoInfo.Activo = EstatusEnum.Activo;
            animalMovimientoInfo.UsuarioCreacionID = usuarioId;

            //Se manda a guardar el registro en base de datos
            animalMovimientoInfo = animalMovimientoPl.GuardarAnimalMovimiento(animalMovimientoInfo);

            return animalMovimientoInfo;
        }

        private void ValidarCierreCorralPartida()
        {
            var lotePl = new LotePL();
            //var enfermeria = int.Parse(lblEnEnfermeriaGenerado.Content.ToString());

            //Validaciones sobre los corrales
            if (EntradaSeleccionada == null || EntradaSeleccionada.Lote == null) return;

            LoteInfo lote = lotePl.ObtenerPorId(loteOrigen.LoteID);
            if (lote != null)
            {
                loteOrigen = lote;
            }
            //Una vez insertado el lote y el animal se incrementan las cabezas de lote
            EntradaSeleccionada.Lote.CabezasInicio = EntradaSeleccionada.Lote.CabezasInicio + 1;
            EntradaSeleccionada.Lote.Cabezas = EntradaSeleccionada.Lote.Cabezas + 1;
            EntradaSeleccionada.Lote.UsuarioModificacionID = usuario;
            loteOrigen.Cabezas = loteOrigen.Cabezas - 1;

            //Se actualizan las cabezas que tiene el lote
            var infoCabezas = new FiltroActualizarCabezasLote
            {
                CabezasProcesadas = 1,
                UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                LoteIDDestino = EntradaSeleccionada.Lote.LoteID,
                LoteIDOrigen = loteOrigen.LoteID
            };
            var cabezasActualizadas = lotePl.ActualizarCabezasProcesadas(infoCabezas);

            var difCabezas = _corralInfoGen.Capacidad - EntradaSeleccionada.Lote.Cabezas;
            var diferenciaCapacidadCorralConf = int.Parse(ConfigurationManager.AppSettings["diferenciaCapacidadCorral"]);

            if (difCabezas <= 0)
            {
                //Si el corral esta llegando a su maxima capacidad alertar
                SkMessageBox.Show(
                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_CorralCapacidadAlLimite,
                    MessageBoxButton.OK, MessageImage.Warning);

                //Eliminar de configuracion de corrales disponibles

                var corralRango = new CorralRangoPL();
                var configuracionCorralEliminar = new CorralRangoInfo
                {
                    OrganizacionID = organizacionID,
                    CorralID = _corralInfoGen.CorralID,
                    UsuarioModificacionId = Convert.ToInt32(Application.Current.Properties["UsuarioID"])
                };

                corralRango.Eliminar(configuracionCorralEliminar);

            }
            else if (difCabezas <= diferenciaCapacidadCorralConf)
            {
                //Si el corral esta llegando a su maxima capacidad alertar
                SkMessageBox.Show(
                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_CorralCapacidadCasiAlLimite,
                    MessageBoxButton.OK, MessageImage.Warning);
            }

            var programacionCortePl = new ProgramacionCortePL();

            var programacionCorte = new ProgramacionCorte
            {
                NoPartida = EntradaSeleccionada.FolioEntradaAgrupado,
                OrganizacionID = organizacionID,
                UsuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"])
            };

            //Si es la primer cabeza se plancha la
            if ((int)lblTotalCortadasGenerado.Content == 1)
            {
                programacionCortePl.ActualizarFechaInicioProgramacionCorte(programacionCorte);
            }

            if (cabezasActualizadas.CabezasOrigen <= 0)
            {
                programacionCortePl.EliminarProgramacionCorte(programacionCorte, organizacionID);

                //Si se cerro el corral se manda actualizar los pesos de los animales
                var corteGanadoPl = new CorteGanadoPL();
                corteGanadoPl.ObtenerPesosOrigenLlegada(organizacionID, EntradaSeleccionada.CorralID, loteOrigen.LoteID);

                // Limpiar pantalla
                BanderaNoIndividual = false;
                LimpiarCaptura(true);
                _pesoTomado = false;
                _tempTomada = false;

            }
        }

        /// <summary>
        /// Se valida la existencia de tratmientos
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ComprobarExistenciaTratamientos()
        {
            var tratamientosPl = new TratamientoPL();
            var resultado = new ResultadoValidacion();
            try
            {
                if (_listaTratamientos != null)
                {
                    resultado = tratamientosPl.ComprobarExistenciaTratamientos(_listaTratamientos,
                        almacenInfo.AlmacenID);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo que busca por arete metalico.
        /// </summary>
        /// <param name="arete"></param>
        private void BuscarAreteMetalico(string arete)
        {
            try
            {
                esCabezasSobrante = false;
                var animalInfo = new AnimalInfo
                {
                    AreteMetalico = arete,
                    OrganizacionIDEntrada = organizacionID
                };
                Inicializarcontroles(true);
                /* Se valida que exista el arete en Animal */
                if (!ExisteAreteEnPartida(animalInfo))
                {
                    //Validar si en las partidas se encuentra algun centro
                    if (ExiteCentroEnOrigen())
                    {
                        // Si existe una partida q no sea compra directa
                        var tipoAreteMetalico = 2;
                        ConsultarAreteAnimal(animalInfo.AreteMetalico, tipoAreteMetalico);
                    }
                    else
                    {
                        //Se da tratamiento a compras directas -- Se valida que el arete no pertenezca a un centro
                        var animalPl = new InterfaceSalidaAnimalPL();
                        _interfaceSalidoAnimalInfo = animalPl.ObtenerNumeroAreteMetalico(animalInfo.AreteMetalico,
                                                                                           organizacionID);
                        if (_interfaceSalidoAnimalInfo == null)
                        {
                            //Validar si se cuenta con los datos generales
                            if (String.IsNullOrEmpty(txtProveedor.Text) &&
                                String.IsNullOrEmpty(txtOrigen.Text))
                            {
                                ObtenerProveedor();
                                dtpFechaRecepcion.SelectedDate = EntradaSeleccionada.FechaEntrada.Date;
                                txtOrigen.Text = EntradaSeleccionada.OrganizacionOrigen.Trim();
                                txtNoPartida.Text = txtNoPartidaGrupo.Text;
                            }

                            // Validar si se corta como cabeza sobrante
                            if (entradasCabezasSobrantes != null && entradasCabezasSobrantes.Any())
                            {
                                foreach (var cabezasSobrantesPorEntradaInfo in entradasCabezasSobrantes)
                                {
                                    //Si alguna partida aun tienen cabezas sobrantes sin cortar
                                    // Si tiene Sobrantes, si aun no se cortan todas las sobrantes,
                                    if (cabezasSobrantesPorEntradaInfo.CabezasSobrantes > 0 &&
                                        cabezasSobrantesPorEntradaInfo.CabezasSobrantesCortadas <
                                        cabezasSobrantesPorEntradaInfo.CabezasSobrantes &&
                                        cabezasSobrantesPorEntradaInfo.CabezasCortadas >=
                                        cabezasSobrantesPorEntradaInfo.EntradaGanado.CabezasOrigen)
                                    {
                                        // Se va a cortar una cabeza sobrante
                                        esCabezasSobrante = true;
                                        txtNoPartida.Text =
                                            cabezasSobrantesPorEntradaInfo.FolioEntrada.ToString(
                                                CultureInfo.InvariantCulture);
                                        break;
                                    }
                                }
                            }
                            if (rfidConectado)
                            {
                                txtAreteMetalico.Text = rfidCapturadoGlobal;
                            }
                           

                            txtCorralDestino.IsEnabled = false;
                            txtAreteMetalico.IsEnabled = false;

                            this.InicializarDispositivos();

                            ValdiarFocusAreteMetalico();
                        }
                        else
                        {
                            // Si se esta cortando una compra directa y se teclea un arete que pertenece a un centro
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              String.Format("{0}{1} {2}",
                                                            Properties.Resources.CorteGanado_ElAretePerteneceA,
                                //"El arete ingresado pertenece a la partida:",
                                                            _interfaceSalidoAnimalInfo.Partida,
                                                            Properties.Resources.
                                                                CorteGanado_ElAretePerteneceA_FavoDerValidar
                                // ". Favor de validar."
                                                  ),
                                              MessageBoxButton.OK, MessageImage.Warning);
                            _interfaceSalidoAnimalInfo = null;
                            LimpiaParcial();
                            txtNoIndividual.Text = String.Empty;
                            txtNoIndividual.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Verifica si el equipo no tiene lector conectado, si es asi y tiene permisos de captura manual activa el txtAreteMetalico
        /// </summary>
        private void VerificarPermisosCapturaManual()
        {
            //Si el Lector RFID estan desconectados se verifica si se tiene permiso para capturar manualmente
            if (rfidConectado == false)
            {
                if (_configRFIDCorte == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                    DeshabilitarControles(false);
                }
                else
                {
                    if (txtNoPartida.Text != "")
                        txtAreteMetalico.IsEnabled = true;
                    if (!_configRFIDCorte.CapturaManual)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                        DeshabilitarControles(false);
                    }
                }
            }
        }

        /// <summary>
        /// Comprueba si el arete o arete metalico no se encuentra registrado.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="areteMetalico"></param>
        /// <returns></returns>
        private ResultadoValidacion ComprobarAreteRegistrado(string arete, string areteMetalico)
        {
            var animalPl = new AnimalPL();
            var resultado = new ResultadoValidacion();
            try
            {
                resultado.Resultado = animalPl.VerificarExistenciaArete(arete, areteMetalico, organizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return resultado;
        }


        private void TxtAreteMetalico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back || e.Key == Key.Delete)
                {
                    try
                    {
                        bandBack = false;
                        if (txtNoIndividual.Text.Trim().Length == 0)
                        {
                            LimpiarCaptura(false);
                            LimpiaParcial();
                        }
                        bandBack = true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaArete,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void TxtAreteMetalico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtAreteMetalico.Text))
            {
                CtrlPegar = false;
                txtAreteMetalico.Text = txtAreteMetalico.Text.Replace(" ", string.Empty);
            }
            else
            {
                if (CtrlPegar)
                {
                    txtAreteMetalico.Text = TextoAnterior;
                    CtrlPegar = false;
                    TextoAnterior = String.Empty;
                }
            }
        }

        private void TxtAreteMetalico_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtAreteMetalico.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtAreteMetalico.Text;
            }
        }

        private void TxtAreteTestigo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAreteMetalico.Text.Trim().Length > 0)
            {
                ValidarAreteMetalicoRegistrado();
            }
        }

        private void TxtNoIndividual_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNoIndividual.Text.Trim().Length > 0)
            {
                var resultadoValidacion = ComprobarAreteRegistrado(txtNoIndividual.Text, "");
                if (resultadoValidacion.Resultado)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CorteGanado_AreteRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                    imgloading.Visibility = Visibility.Hidden;
                    txtNoIndividual.Text = "";
                }
            }
        }

        private void ValidarAreteMetalicoRegistrado()
        {
            var resultadoValidacion = ComprobarAreteRegistrado("", txtAreteMetalico.Text);
            if (resultadoValidacion.Resultado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.CorteGanado_AreteRFIDRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                imgloading.Visibility = Visibility.Hidden;
                txtAreteMetalico.Text = "";
            }
        }

        #endregion

        private void txtPesoCorte_GotFocus(object sender, RoutedEventArgs e)
        {
            _pesoCorte = this.txtPesoCorte.Text;
        }
    }
}
