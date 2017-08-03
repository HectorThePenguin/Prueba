using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for ReimplanteGanado.xaml
    /// </summary>
    public partial class ReimplanteGanado
    {
        #region Atributos
        private ReimplanteInfo reimplanteInfoGen;
        private CorralInfo corralInfoGen;
        private LoteInfo loteNuevoInfoGen;
        private CorralInfo corralDestinoInfoGen;
        private SerialPortManager spManagerBascula;
        private SerialPortManager spManagerRFID;
        private BasculaCorteSection configBasculaReimplante;
        private BasculaCorteSection configRFID;

        private bool cambiarArete = false;
        private bool cambiarTipoGanado = false;
        private bool esAreteNuevo = false;
        
        private int organizacionID;
        private int folioProgReimplante;
        IList<TratamientoInfo> _listaTratamientos; //Lista de tratamientos
        TrampaInfo trampaInfo = null;
        private bool bandFoco;
        private bool bandBack;
        LoteReimplanteInfo loteReimplante = new LoteReimplanteInfo();
        private bool CtrlPegar;
        private string TextoAnterior;
        AlmacenInfo almacenInfo;

        private bool basculaConectada = false;
        private bool pesoTomado;
        private string _pesoParcial = string.Empty;
        private string _pesoGlobal = string.Empty;
        private DispatcherTimer _timerListeningPort;
        private int vecesPesoCotejado;
        private bool capturaInmediata;

        private bool rfidConectado = false;
        private bool CapturaObligatoriaRFID = false;
        private int codigoAreteColorReimplante = 0;
        private string rfidCapturadoGlobal = string.Empty;
        private string areteIndividualAnterior = string.Empty;
        private string areteMetalicoAnterior = string.Empty;
        private bool parametroLecturaDobleArete = false;
        private uint start = 0;
        private uint stop = 0;
        private bool aplicaScanner;
        private bool bloquearRFID;
        #endregion

        #region Constructor


        public ReimplanteGanado()
        {
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            bandBack = false;
            bandFoco = false;
            CtrlPegar = false;
            TextoAnterior = string.Empty;
            InitializeComponent();

            LlenarComboImplantador();
            dpFechaReimplante.SelectedDate = DateTime.Now;
            dtpFechaRecepcion.SelectedDate = DateTime.Now;
            folioProgReimplante = 0;
            // noLoteReimplante = 0;

            if (!ExisteProgramacionReimplante())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ReimplanteGanado_NoExisteProgramacionReimplante,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                HabilitarControlesGeneral(false);
            }

            if (!ExisteTrampa())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_NoExistenTrampas,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                HabilitarControlesGeneral(false);
            }

            UserInitialization();
            ProbarBascula();
            ProbarLectorRFID();
            //se valida que existan un almacen para la trampa configurada
            if (!ExistenAlmacenParaTrampa())
            {
                HabilitarControlesGeneral(false);
            }

            //Leer configuracion de arete color
            LeerConfiguracionAreteColor();

            ConfiguracionParametrosPL parametrosPL = new ConfiguracionParametrosPL();
            /* Obtener Configuracion de Arete RFID obligatorio*/
            var parametroSolicitado = new ConfiguracionParametrosInfo
            {
                Clave = ParametrosEnum.CapturaObligatoriaRFID.ToString(),
                TipoParametro = (int)TiposParametrosEnum.ObligatorioAreteRFID,
                OrganizacionID = organizacionID
            };

            var parametro = parametrosPL.ObtenerPorOrganizacionTipoParametroClave(parametroSolicitado);
            if (parametro != null)
            {
                CapturaObligatoriaRFID = bool.Parse(parametro.Valor);
            }
            try
            {
                ValidarParametroLecturaDobleArete();
            }
            catch (Exception ex) 
            {
                Logger.Error(ex);
                SkMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Eventos

        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();

        /// <summary>
        /// Evento closing de Corte de ganado 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            if (basculaConectada)
                spManagerBascula.Dispose();

            if (rfidConectado)
                spManagerRFID.Dispose();

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
                string strEnd = spManagerRFID.ObtenerLeturaRFID(e.Data);
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


        /// <summary>
        /// Evento cargar pantalla y llena los combos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bloquearRFID = false;
            txtNoIndividual.Focus();
            HabilitarBotones(false);
            
            if (basculaConectada)
            {
                InicializarBascula(); 
            }
            else 
            {
                ProbarBascula();
                InicializarBascula();
            }
            if (rfidConectado)
                InicializarLectorRFID();
        }

        private void TxtNoIndividual_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void TxtPesoReimplante_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void TxtCorralDestino_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuiones(e.Text);
        }

        private void TxtNoIndividual_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                bandBack = false;
                if (txtAreteTestigo.Text.Trim().Length == 0)
                    Limpiar();
            }
            if (aplicaScanner)
            {
                stop = GetTickCount();
                uint elapsed = (stop - start);
                if (elapsed > 30)
                {
                    txtNoIndividual.Text = string.Empty;
                }
            }
        }

        private void txtPesoReimplante_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bandFoco == false)
                {
                    if (txtPesoReimplante.Text.Trim().Length > 0)
                    {
                        if (reimplanteInfoGen != null)
                        {
                            ObtenerIndicadoresProductividad(reimplanteInfoGen);
                            Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                        }
                        txtCorralDestino.Focus();
                        bandFoco = false;
                    }
                    else
                    {
                        LimpiarTratamiento(false);
                    }
                }
                else
                {
                    bandFoco = false;
                    if (txtPesoReimplante.Text.Trim().Length == 0)
                    {
                        LimpiarTratamiento(false);
                    }
                    else
                    {
                        int resultado;
                        if (!int.TryParse(txtPesoReimplante.Text.Trim(), out resultado))
                        {
                            LimpiarTratamiento(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarTratamiento(bool origenEnter)
        {
            dgTratamientos.ItemsSource = null;
            HabilitarBotones(false);
            bandFoco = origenEnter;
        }

        private void txtNoIndividual_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtNoIndividual.Text.Trim().Length > 0)
            {
                try
                {
                    if (txtAreteTestigo.Text.Trim().Length == 0 && bandFoco == false)
                        Dispatcher.BeginInvoke(new Action(ObtenerInformacionNoIndividual), DispatcherPriority.Background, null);
                    else if (esAreteNuevo || txtNoIndividual.Text != areteIndividualAnterior && txtAreteTestigo.Text.Trim().Length != 0)
                        ValidarAreteIndividualRegistrado();
                    bandFoco = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            else
            {
                bandFoco = true;
            }
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
                txtNoIndividual.Text = txtNoIndividual.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtNoIndividual.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }
        }

        private void txtCorralDestino_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtCorralDestino.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtCorralDestino.Text;
            }
        }

        private void txtCorralDestino_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtCorralDestino.Text))
            {
                CtrlPegar = false;
                txtCorralDestino.Text = txtCorralDestino.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtCorralDestino.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }
            bandFoco = false;
        }

        private void txtCorralDestino_LostFocus(object sender, RoutedEventArgs e)
        {
            if (bandFoco == false)
            {
                if (txtCorralDestino.Text.Trim().Length > 0)
                {

                    if (ValidarCorralDestino())
                    {
                        cboImplantador.Focus();
                    }
                    else
                    {
                        txtCorralDestino.Focus();

                    }
                    bandFoco = true;
                }
                else
                {

                    bandFoco = true;
                }
            }
            else
            {
                bandFoco = false;
            }
        }

        private void txtPesoReimplante_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtPesoReimplante.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtPesoReimplante.Text;
            }
        }

        private void txtPesoReimplante_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtPesoReimplante.Text))
            {
                CtrlPegar = false;
                txtPesoReimplante.Text = txtPesoReimplante.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtPesoReimplante.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }
            bandFoco = false;
        }

        private void TxtAreteTestigo_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                start = GetTickCount();
                if (e.Key != Key.Enter && e.Key != Key.Tab)
                {
                    bandFoco = false;
                    return;
                }
                if (e.Key != Key.Tab)
                {
                    bandFoco = true;
                }
                if (txtAreteTestigo.Text.Trim().Length > 0)
                {
                    if (txtNoIndividual.Text.Trim().Length == 0)
                        Dispatcher.BeginInvoke(new Action(ObtenerInformacionAreteMetalico), DispatcherPriority.Background, null);
                    if (e.Key == Key.Tab)
                    {
                        bandFoco = true;
                    }
                    if (e.Key == Key.Enter)
                    {
                        txtPesoReimplante.Focus();
                        bandFoco = true;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CapturaAreteMetalico_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (e.Key != Key.Tab)
                    {
                        bandFoco = false;
                    }

                    if (txtPesoReimplante.IsEnabled)
                    {
                        txtPesoReimplante.Focus();
                    }
                    else
                    {
                        txtCorralDestino.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void txtCorralDestino_onKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                ValidarSiEsTransferenciaAsignarAreteColor(false);
            }
        }

        private void txtCorralOrigen_keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (String.IsNullOrWhiteSpace(txtCorralOrigen.Text))
                {
                    txtCorralOrigen.Focus();
                    e.Handled = true;
                }
                else
                {
                    try
                    {
                        //Validar si el corral destino es de tipo Venta
                        var corralPl = new CorralPL();
                        var corralInfo = new CorralInfo
                        {
                            Codigo = txtCorralOrigen.Text,
                            TipoCorral = new TipoCorralInfo { GrupoCorral = new GrupoCorralInfo { GrupoCorralID = GrupoCorralEnum.Produccion.GetHashCode() } },
                            Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID }
                        };

                        corralInfo = corralPl.ObtenerPorGrupoCorral(corralInfo);
                        if (corralInfo == null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CorteGanado_CorralNoEsTipoProduccion,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                        }
                        else
                        {
                            txtCorralOrigen.Text = corralInfo.Codigo;
                            if (esAreteNuevo)
                            {
                                reimplanteInfoGen = new ReimplanteInfo();
                            }
                            reimplanteInfoGen.Corral = corralInfo;
                            corralInfoGen = corralInfo;

                            //Se busca el corral para ver si tiene Lote de lo contraria se le creara uno
                            var lotePl = new LotePL();
                            // si no tiene lote validar si el corral ya tiene lote
                            var loteInfo = new LoteInfo
                            {
                                OrganizacionID = organizacionID,
                                CorralID = corralInfo.CorralID
                            };
                            reimplanteInfoGen.Lote = lotePl.ObtenerPorCorralID(loteInfo);

                            //reimplanteInfoGen.Lote = lotePl.ObtenerPorCorral(organizacionID, corralInfo.CorralID);

                            if (reimplanteInfoGen.Lote != null && reimplanteInfoGen.Lote.Cabezas > 0)
                            {
                                var programacionReimplantePL = new ProgramacionReimplantePL();
                                List<ProgramacionReinplanteInfo> listaProgramacion =
                                    programacionReimplantePL.ObtenerProgramacionReimplantePorLoteID(
                                        reimplanteInfoGen.Lote);
                                if (listaProgramacion != null &&
                                    listaProgramacion.Count > 0)
                                {
                                    var programacionReinplanteInfo = listaProgramacion.FirstOrDefault();
                                    if (programacionReinplanteInfo != null)
                                    {
                                        loteReimplante.NumCabezas = reimplanteInfoGen.Lote.Cabezas;
                                        txtCorralOrigen.IsEnabled = false;
                                        txtCorralDestino.Focus();
                                        if (esAreteNuevo)
                                        {
                                            //obtener arete a remplazar
                                            var reimplantePL = new ReimplantePL();
                                            var reimplanteInfo =
                                                reimplantePL.ObtenerAreteIndividualReimplantar(reimplanteInfoGen.Lote);
                                            if (reimplanteInfo != null)
                                            {
                                                reimplanteInfoGen = reimplanteInfo;

                                                ObtenerDatosCompra(reimplanteInfo);
                                                ObtenerIndicadoresProductividad(reimplanteInfo);

                                                txtCorralOrigen.Text = reimplanteInfoGen.Corral.Codigo;
                                                corralInfoGen = reimplanteInfo.Corral;
                                                folioProgReimplante = reimplanteInfo.FolioProgramacionReimplanteID;
                                                ObtenerTotales(reimplanteInfo);
                                                loteReimplante.NumCabezas = reimplanteInfo.Lote.Cabezas;

                                                loteReimplante.LoteReimplanteID =
                                                reimplanteInfo.FolioProgramacionReimplanteID;
                                                loteReimplante.TipoMovimientoID = (int)TipoMovimiento.Reimplante;
                                                loteReimplante.FolioEntrada = reimplanteInfo.Animal.FolioEntrada;
                                                Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                                                cambiarArete = false;
                                            }
                                            else
                                            {
                                                // no hay animales activos en el corral seleccionado
                                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.ReimplanteGanado_CorralNoTieneGanado,
                                                MessageBoxButton.OK,
                                                MessageImage.Warning);
                                                //Limpiar(true);
                                                txtCorralOrigen.Text = string.Empty;
                                                txtCorralOrigen.Focus();
                                            }
                                        }
                                        else
                                        {
                                            folioProgReimplante = programacionReinplanteInfo.FolioProgramacionID;
                                            ObtenerTotales(reimplanteInfoGen);
                                            cambiarArete = true;
                                        }
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.Reimplante_CorralSinReimplante,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        //Limpiar(true);
                                        txtCorralOrigen.Text = string.Empty;
                                        txtCorralOrigen.Focus();
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.Reimplante_CorralSinReimplante,
                                        MessageBoxButton.OK, MessageImage.Warning);
                                    //Limpiar(true);
                                    txtCorralOrigen.Text = string.Empty;
                                    txtCorralOrigen.Focus();
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ReimplanteGanado_CorralNoTieneGanado,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                                //Limpiar(true);
                                txtCorralOrigen.Text = string.Empty;
                                txtCorralOrigen.Focus();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        txtCorralOrigen.Text = string.Empty;
                        txtCorralOrigen.Focus();
                        Logger.Error(ex);
                    }
                }
            }
        }

        private void TxtAreteTestigo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtAreteTestigo.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtAreteTestigo.Text;
            }

        }

        private void TxtAreteTestigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtAreteTestigo.Text))
            {
                CtrlPegar = false;
                txtAreteTestigo.Text = txtAreteTestigo.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtAreteTestigo.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }
        }

        private void TxtAreteTestigo_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                bandBack = false;
                if (txtNoIndividual.Text.Trim().Length == 0)
                    Limpiar();
            }
        }

        private void TxtAreteTestigo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtAreteTestigo.Text.Trim().Length > 0)
            {
                try
                {
                    if (txtNoIndividual.Text.Trim().Length == 0 && bandFoco == false)
                        Dispatcher.BeginInvoke(new Action(ObtenerInformacionAreteMetalico), DispatcherPriority.Background, null);
                    else if (esAreteNuevo || txtAreteTestigo.Text != areteMetalicoAnterior && txtNoIndividual.Text.Trim().Length != 0)
                        ValidarAreteTestigoRegistrado();
                    bandFoco = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            else
            {
                bandFoco = true;
            }
        }

        /// <summary>
        /// Evento keyDown para validar el enter y el tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoReimplante_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key != Key.Enter && e.Key != Key.Tab)
                {
                    bandFoco = false;
                    return;
                }

                if (txtPesoReimplante.Text.Trim().Length > 0)
                {
                    if (reimplanteInfoGen != null)
                    {
                        ObtenerIndicadoresProductividad(reimplanteInfoGen);
                        Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                    }
                    if (txtCorralOrigen.IsEnabled)
                    {
                        txtCorralOrigen.Focus();
                    }
                    else
                    {
                        txtCorralDestino.Focus();
                    }
                }
                else
                {
                    LimpiarTratamiento(true);
                }
                bandFoco = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se selecciona "Leer peso"
        /// </summary>
        /// <param name="sender">Boton que invoca el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void BtnLeerPeso_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info();
                if (basculaConectada)
                {
                        capturaInmediata = true;
                        this.spManagerBascula.StopListening();
                        this.BtnLeerPeso.IsEnabled = false;

                        spManagerBascula.StartListening(this.configBasculaReimplante.Puerto,
                                this.configBasculaReimplante.Baudrate,
                                this.configBasculaReimplante.Paridad,
                                this.configBasculaReimplante.Databits,
                                this.configBasculaReimplante.BitStop);
                }
            }
            catch (Exception error)
            {
                Logger.Error(error);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], error.Message, MessageBoxButton.OK, MessageImage.Error);
            }

        }


        /// <summary>
        /// Evento keyDown para validar el enter y el tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtNoIndividual_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                start = GetTickCount();
                if (e.Key != Key.Enter && e.Key != Key.Tab)
                {
                    bandFoco = false;
                    return;
                }
                if (e.Key != Key.Tab)
                {
                    bandFoco = true;
                }

                if (txtNoIndividual.Text.Trim().Length > 0)
                {
                    if (txtAreteTestigo.Text.Trim().Length == 0)
                        Dispatcher.BeginInvoke(new Action(ObtenerInformacionNoIndividual), DispatcherPriority.Background, null);
                    if (e.Key == Key.Tab)
                    {
                        bandFoco = true;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CapturaArete_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (e.Key != Key.Tab)
                    {
                        bandFoco = false;
                    }
                }
                if (e.Key == Key.Enter)
                {
                    if (txtAreteTestigo.IsEnabled)
                        txtAreteTestigo.Focus();
                    else
                        txtPesoReimplante.Focus();
                    bandFoco = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManagerBascula_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            string strEnd = "";
            double val;
            try
            {
                strEnd = spManagerBascula.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    this._pesoParcial = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aqui es para que se este reflejando la bascula en el display
                    //Dispatcher.BeginInvoke(new Action(() =>
                    //{
                    //    txtDisplayPeso.Text = peso;
                    //}), null);

                    Dispatcher.BeginInvoke(new Action(CapturaPesoEnDisplay), DispatcherPriority.Normal);


                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento keyDown para validar el enter y el tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter && e.Key != Key.Tab) return;

                if (txtCorralDestino.Text.Trim().Length > 0)
                {
                    if (txtCorralDestino.Text.Trim() != txtCorralOrigen.Text.Trim())
                    {
                        if (ValidarCorralDestino())
                        {
                            cboImplantador.Focus();
                        }
                        else
                        {
                            txtCorralDestino.Focus();
                        }
                    }
                    else
                    {
                        loteNuevoInfoGen = reimplanteInfoGen.Lote;
                        cboImplantador.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReimplanteGanado_CapturaDestino,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento keyDown para validar el enter y el tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboImplantador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !String.IsNullOrEmpty(cboImplantador.Text))
            {
                txtObservaciones.Focus();
            }
        }

        /// <summary>
        /// Evento keyDown para validar el enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGuardar.Focus();
            }
        }

        /// <summary>
        /// Evento del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Cancelarcaptura_CorteGanado,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                bandBack = true;
                cboImplantador.SelectedValue = 0;
                Limpiar();
                txtNoIndividual.Focus();
            }
        }

        /// <summary>
        /// 
        /// Evento del boton Guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidarCamposBlancos())
                {
                    if (parametroLecturaDobleArete)
                    {
                        if (txtNoIndividual.Text.Trim() == txtAreteTestigo.Text.Trim())
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ReimplanteGanado_MsgAretesIguales,
                                MessageBoxButton.OK, MessageImage.Warning);
                            return;
                        }
                    }

                    ResultadoValidacion resultadoValidacion = null;
                    if ((txtNoIndividual.IsEnabled && txtNoIndividual.Text != areteIndividualAnterior) || esAreteNuevo)
                    {
                        resultadoValidacion = ComprobarAreteRegistrado(txtNoIndividual.Text, "");
                        if (resultadoValidacion.Resultado)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CorteGanado_AreteRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                            btnGuardar.IsEnabled = true;
                            txtNoIndividual.Text = "";
                            txtNoIndividual.Focus();
                            return;
                        }
                    }

                    if ((txtAreteTestigo.IsEnabled && txtAreteTestigo.Text != areteMetalicoAnterior) || esAreteNuevo)
                    {
                        resultadoValidacion = ComprobarAreteRegistrado("", txtAreteTestigo.Text);
                        if (resultadoValidacion.Resultado)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CorteGanado_AreteRFIDRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                            btnGuardar.IsEnabled = true;
                            txtAreteTestigo.Text = "";
                            txtAreteTestigo.Focus();
                            return;
                        }
                    }

                    resultadoValidacion = ComprobarExistenciaTratamientos();
                    if (resultadoValidacion.Resultado)
                    {
                        /* ObtenerInformacionNoIndividual();  */
                        if (corralDestinoInfoGen == null)
                        {
                            if (!ValidarCorralDestino())
                            {
                                txtCorralDestino.Focus();
                                return;
                            }
                        }
                        var usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();
                        using (var scope = new System.Transactions.TransactionScope())
                        {
                            var animalPL = new AnimalPL();
                            var tipoGanadoInfo = new TipoGanadoInfo();
                            tipoGanadoInfo = reimplanteInfoGen.Animal.TipoGanado;
                            if (esAreteNuevo || txtNoIndividual.Text != areteIndividualAnterior || txtAreteTestigo.Text != areteMetalicoAnterior)
                            {
                                reimplanteInfoGen.Animal.UsuarioModificacionID = usuarioID;
                                reimplanteInfoGen.Animal.Arete = txtNoIndividual.Text;
                                reimplanteInfoGen.Animal.AreteMetalico = txtAreteTestigo.Text;
                                animalPL.ActializaAretesEnAnimal(reimplanteInfoGen.Animal);
                                esAreteNuevo = false;

                            }
                            else if (cambiarArete)
                            {
                                //Cambiar de corral el arete
                                var deteccionGrabar = new DeteccionInfo
                                {
                                    CorralID = corralInfoGen.CorralID,
                                    LoteID = reimplanteInfoGen.Lote.LoteID,
                                    UsuarioCreacionID = usuarioID
                                };
                                // Se intercambian aretes por encontrarse el animal en un corral distinto y ser carga inicial
                                animalPL.ReemplazarAretes(reimplanteInfoGen.Animal, deteccionGrabar);

                                reimplanteInfoGen.Animal =
                                    animalPL.ObtenerAnimalPorArete(reimplanteInfoGen.Animal.Arete, reimplanteInfoGen.Animal.OrganizacionIDEntrada);
                                cambiarArete = false;
                            }
                            if (cambiarTipoGanado)
                            {
                                reimplanteInfoGen.Animal.TipoGanado = tipoGanadoInfo;
                                reimplanteInfoGen.Animal.UsuarioModificacionID = usuarioID;
                                animalPL.ActializaTipoGanado(reimplanteInfoGen.Animal);
                                cambiarTipoGanado = false;
                            }

                            //Guardar AnimalMovimiento
                            reimplanteInfoGen.AnimalMovimiento = GuardarAnimalMovimiento(reimplanteInfoGen, usuarioID);
                            if (reimplanteInfoGen.AnimalMovimiento != null)
                            {
                                //Se almacenan los tratamientos(Almacena, Descuenta, AnimalCosto)
                                GuardarSalidaPorConsumo(reimplanteInfoGen.AnimalMovimiento);

                                if (String.Compare(txtCorralOrigen.Text.Trim(), txtCorralDestino.Text.Trim(),
                                        StringComparison.Ordinal) != 0)
                                {
                                    var lotePl = new LotePL();
                                    //Una vez insertado el lote y el animal se incrementan las cabezas de lote
                                    loteNuevoInfoGen.CabezasInicio = loteNuevoInfoGen.CabezasInicio + 1;
                                    loteNuevoInfoGen.Cabezas = loteNuevoInfoGen.Cabezas + 1;
                                    //Decrementamos cabezas en Lote origen
                                    reimplanteInfoGen.Lote.Cabezas = reimplanteInfoGen.Lote.Cabezas - 1;
                                    //Se actualizan las cabezas que tiene el lote
                                    var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                                    {
                                        LoteIDOrigen = reimplanteInfoGen.Lote.LoteID,
                                        LoteIDDestino = loteNuevoInfoGen.LoteID,
                                        CabezasProcesadas = 1,
                                        UsuarioModificacionID = usuarioID
                                    };
                                    lotePl.ActualizarCabezasProcesadas(filtroActualizaCabezas);
                                    //lotePl.ActualizaCabezasEnLoteProductivo(loteNuevoInfoGen, reimplanteInfoGen.Lote);
                                }

                                //Limpiar y decrementos de totales
                                var total = (int)lblToltalReimplantarResultado.Content;
                                lblToltalReimplantarResultado.Content = total - 1;

                                var totalReimplantadas = (int)lblReimplantadasResultado.Content;
                                lblReimplantadasResultado.Content = totalReimplantadas + 1;
                                bandBack = true;
							
                                int totalReimplantar = 0;
                                totalReimplantar = (int)lblToltalReimplantarResultado.Content;

                                if ((int)lblToltalReimplantarResultado.Content <= 0)
                                {
                                    var programacionCortePl = new ProgramacionReimplantePL();
                                    programacionCortePl.EliminarProgramacionReimplante(folioProgReimplante);
                                }

                                var loteReimplantePL = new LoteReimplantePL();
                                loteReimplante = loteReimplantePL.ObtenerPorID(reimplanteInfoGen.LoteReimplanteID);
                                DateTime? fechaReal = loteReimplante.FechaReal;
                                if (fechaReal == null || !fechaReal.HasValue || fechaReal.Value == default(DateTime))
                                {
                                    GuardarFechaReal();
                                }
                                scope.Complete();
								
                                Limpiar();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.Reimplante_DatosGuardadosConExito,
                                    MessageBoxButton.OK, MessageImage.Correct);

                                if (totalReimplantar == 15)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.Reimplante_ValidarCabezasFisicasContraPendientes,
                                        MessageBoxButton.OK, MessageImage.Warning);
                                }

                                txtNoIndividual.Focus();
                            }
                        }
                    }
                    else
                    {
                        MostrarMensajesComprobarExistenciaTratamientos(resultadoValidacion);
                    }
                }
                else
                {
                    if (parametroLecturaDobleArete) 
                    {
                        if (String.IsNullOrEmpty(txtAreteTestigo.Text))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ReimplanteGanado_MsgParametroDobleAreteRFID,
                            MessageBoxButton.OK, MessageImage.Warning);
                            return;
                        }
                    }
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.Reimplante_DatosEnBlanco,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_ErrorInesperado,
                    MessageBoxButton.OK, MessageImage.Error);
                Logger.Error(ex);
            }

        }

        private void btnReasignar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reasignarArete = new ReasignarArete();

                if (rfidConectado)
                    spManagerRFID.Dispose();

                reasignarArete.TrampaID = trampaInfo.TrampaID;
                reasignarArete.Left = (ActualWidth - reasignarArete.Width) / 2;
                reasignarArete.Top = ((ActualHeight - reasignarArete.Height) / 2) + 132;
                reasignarArete.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                reasignarArete.ShowDialog();
                if (rfidConectado)
                    if (reasignarArete.RfidNuevo.Trim() != string.Empty)
                    {
                        txtAreteTestigo.Text = reasignarArete.RfidNuevo;
                    }
                if (rfidConectado)
                    InicializarLectorRFID();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Grupo_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void checkTratamiento_checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var dato = (TratamientoInfo)dgTratamientos.SelectedItem;
                if (dato != null)
                {
                    var resultadoValidacion = ExistenProductosEnTratamientosSeleccionados(dato);
                    if (resultadoValidacion.Resultado)
                    {
                        var cb = (CheckBox)sender;
                        if (cb != null)
                        {
                            cb.IsChecked = false;
                        }
                        _listaTratamientos[_listaTratamientos.IndexOf(dato)].Seleccionado = false;
                        dato.Seleccionado = false;

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           string.Format("{0}{1}{2}{3}{4}",
                               Properties.Resources.CorteGanado_MedicamentosContenidosEnOtroTratamiento,
                               dato.Descripcion.Trim(),
                               Properties.Resources.CorteGanado_MedicamentosContenidosEnOtroTratamiento2,
                               resultadoValidacion.Mensaje.Trim(),
                               "."),
                           MessageBoxButton.OK, MessageImage.Warning);
                    }
                    else
                    {
                        _listaTratamientos[_listaTratamientos.IndexOf(dato)].Seleccionado = true;
                        dato.Seleccionado = true;
                    }
                }
                dgTratamientos.SelectedItem = dato;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
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
            }
            return resultadoValidacion;
        }

        private void checkTratamiento_unchecked(object sender, RoutedEventArgs e)
        {
            var dato = (TratamientoInfo)dgTratamientos.SelectedItem;
            if (dato != null)
                dato.Seleccionado = false;
        }

        private void BtnMedicamentos_OnClick(object sender, RoutedEventArgs e)
        {
            if (reimplanteInfoGen != null)
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

        /// <summary>
        /// Evento para el boton cambiar de sexo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgCambiarSexo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (reimplanteInfoGen != null && reimplanteInfoGen.Animal != null &&
                reimplanteInfoGen.Animal.PesoCompra > 0)
            {
                if (reimplanteInfoGen != null && reimplanteInfoGen.Animal != null)
                {
                    var tipoGanadoPl = new TipoGanadoPL();
                    var tipoGanadoInfo = new TipoGanadoInfo();

                    tipoGanadoInfo = tipoGanadoPl.ObtenerPorID(reimplanteInfoGen.Animal.TipoGanadoID);
                    if (tipoGanadoInfo != null)
                    {
                        //Se cambia el sexo y se calcula el nuevo tipo de ganado
                        if (tipoGanadoInfo.Sexo == Sexo.Hembra)
                        {
                            tipoGanadoInfo = tipoGanadoPl.ObtenerTipoGanadoSexoPeso(Sexo.Macho.ToString(),
                                                                                    reimplanteInfoGen.Animal.PesoCompra);
                            //int.Parse(txtPesoReimplante.Text));
                            txtTipoAnimal.Text = tipoGanadoInfo.Descripcion;
                            reimplanteInfoGen.Animal.TipoGanado = tipoGanadoInfo;
                            reimplanteInfoGen.Animal.TipoGanadoID = tipoGanadoInfo.TipoGanadoID;
                        }
                        else
                        {
                            tipoGanadoInfo = tipoGanadoPl.ObtenerTipoGanadoSexoPeso(Sexo.Hembra.ToString(),
                                                                                    reimplanteInfoGen.Animal.PesoCompra);
                            //int.Parse(txtPesoReimplante.Text));

                            txtTipoAnimal.Text = tipoGanadoInfo.Descripcion;
                            reimplanteInfoGen.Animal.TipoGanado = tipoGanadoInfo;
                            reimplanteInfoGen.Animal.TipoGanadoID = tipoGanadoInfo.TipoGanadoID;
                        }

                        //Obtener tratamientos para el nuevo sexo
                        Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                        cambiarTipoGanado = true;
                    }
                }
            }
        }


        private void TimerLisnteingPort_Tick(object sender, EventArgs e)
        {
            try
            {
                var timer = (DispatcherTimer)sender;
                this.spManagerBascula.StartListening(configBasculaReimplante.Puerto,
                                configBasculaReimplante.Baudrate,
                                configBasculaReimplante.Paridad,
                                configBasculaReimplante.Databits,
                                configBasculaReimplante.BitStop);
            }
            catch
            {
            }
        }

        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para almacenar los Tratamientos y descontar del almacen
        /// </summary>
        private void GuardarSalidaPorConsumo(AnimalMovimientoInfo animalMovimientoInfo)
        {
            try
            {
                var almacenpl = new AlmacenPL();
                var usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                var almacenMovimientoInfo = new AlmacenMovimientoInfo
                {
                    AlmacenID = almacenInfo.AlmacenID,
                    AnimalMovimientoID = animalMovimientoInfo.AnimalMovimientoID,
                    TipoMovimientoID = (int)TipoMovimiento.SalidaPorConsumo,
                    Status = (int)EstatusInventario.Aplicado,
                    Observaciones = "",
                    UsuarioCreacionID = usuarioID,
                    AnimalID = animalMovimientoInfo.AnimalID,
                    CostoID = (int)Costo.MedicamentoDeReimplante,
                };

                almacenpl.GuardarDescontarTratamientos(_listaTratamientos.Where(item => item.Seleccionado
                                                        && item.Habilitado).ToList(),
                                                        almacenMovimientoInfo
                                                        );
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
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
            }
            return resultado;
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

        /// <summary>
        /// 
        /// </summary>
        private void ObtenerInformacionNoIndividual()
        {
            try
            {
                if (txtNoIndividual.Text.Trim().Length > 0)
                {
                    var tipoAreteIndividual = 1;
                    if (ObtenerInfoAnimal(txtNoIndividual.Text, tipoAreteIndividual))
                    {
                        txtNoIndividual.IsEnabled = false;
                        if (!pesoTomado)
                        {
                            //Si el peso no ha sido tomado
                            InicializarBascula();
                           //CapturarPeso();
                        }
                        if (txtAreteTestigo.IsEnabled)
                        {
                            txtAreteTestigo.Focus();
                        }
                        else if (txtPesoReimplante.IsEnabled)
                        {
                            txtPesoReimplante.Focus();
                        }
                        else if (txtCorralOrigen.IsEnabled)
                        {
                            txtCorralOrigen.Focus();
                        }
                        else
                        {
                            txtCorralDestino.Focus();
                        }
                    }
                    else
                    {
                        txtNoIndividual.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CapturaArete_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para tomar el pedo del display
        /// </summary>
        private void CapturarPeso()
        {
            try
            {
                if (!String.IsNullOrEmpty(txtDisplayPeso.Text))
                {
                    txtPesoReimplante.Text = txtDisplayPeso.Text.Replace(".00", "").Replace(",00", "");
                    if (reimplanteInfoGen != null)
                    {
                        ObtenerIndicadoresProductividad(reimplanteInfoGen);
                        pesoTomado = true;
                        ObtenerTratamiento();
                    }
                    BtnLeerPeso.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// Leer la configuracion de la pantalla
        /// </summary>
        private void LeerConfiguracionAreteColor()
        {
            try
            {
                var parametrosPl = new ConfiguracionParametrosPL();
                /* Obtener Configuracion de Arete Color Reimplante*/
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.CodigoAreteColorReimplante,
                    OrganizacionID = organizacionID
                };
                var parametro = parametrosPl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado).FirstOrDefault();
                if (parametro != null)
                    codigoAreteColorReimplante = int.Parse(parametro.Valor, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException ex)
            {
                Logger.Error(ex);

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
                            TipoMovimientoID = (int)TipoMovimiento.DiferenciasDeInventario,
                            Status = (int)EstatusInventario.Pendiente
                        };
                        //Validar que no queden ajustes pendientes por aplicar para el almacen
                        var existeAjustesPendientes = almacenPL.ExistenAjustesPendientesParaAlmacen(
                                                        almacenMovimientoInfo);
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
            }
            return existe;
        }

        private void HabilitarBotones(bool estado)
        {
            btnMedicamentos.IsEnabled = estado;
        }
        /// <summary>
        /// Obtiene la lista de tratamientos para remplante
        /// </summary>
        private void ObtenerTratamiento()
        {
            try
            {
                HabilitarBotones(true);
                if (txtPesoReimplante.Text.Trim().Length > 0 && reimplanteInfoGen != null &&
                    reimplanteInfoGen.Animal != null)
                {
                    var tratamientoPl = new TratamientoPL();
                    var tratamientoInfo = new TratamientoInfo();
                    var tipoGanadoPl = new TipoGanadoPL();
                    TipoGanadoInfo tipo = tipoGanadoPl.ObtenerPorID(reimplanteInfoGen.Animal.TipoGanadoID);

                    if (tipo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ReimplanteGanado_FalloAlObtenerTipoGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                        btnMedicamentos.IsEnabled = false;
                        btnGuardar.IsEnabled = false;
                        HabilitarBotones(false);
                        return;
                    }

                    tratamientoInfo.OrganizacionId = organizacionID;
                    tratamientoInfo.Sexo = tipo.Sexo;
                    tratamientoInfo.Peso = int.Parse(txtPesoReimplante.Text);

                    //Obtenemos la lista de tratamientos
                    _listaTratamientos = tratamientoPl.ObtenerTratamientosPorTipoReimplante(tratamientoInfo);

                    if (_listaTratamientos == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ExisteTratamiento_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                        LimpiarTratamiento(true);
                    }
                    else
                    {
                        dgTratamientos.ItemsSource = _listaTratamientos;
                        ValidarSiEsTransferenciaAsignarAreteColor(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                     Properties.Resources.ReimplanteGanado_FalloAlObtenerTipoGanado,
                     MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Funcion para inicilizar bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                this.pesoTomado = false;
                this._pesoGlobal = string.Empty;
                this._pesoParcial = string.Empty;
                BtnLeerPeso.IsEnabled = true;
                _timerListeningPort = new DispatcherTimer();
                _timerListeningPort.Interval = new TimeSpan(0, 0, 1);
                _timerListeningPort.Tick += (TimerLisnteingPort_Tick);
                _timerListeningPort.Start();
                    BtnLeerPeso.IsEnabled = true;
                   


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// Prueba si el lector RFID se encuentra conectado al equipo
        /// </summary>
        private void ProbarLectorRFID()
        {
            //Se prueba el funcionamiento de los display del RFID
            try
            {
                if (spManagerRFID != null)
                {
                    spManagerRFID.StartListening(configRFID.Puerto,
                        configRFID.Baudrate,
                        configRFID.Paridad,
                        configRFID.Databits,
                        configRFID.BitStop);
                    spManagerRFID.StopListening();

                    rfidConectado = true;
                    txtAreteTestigo.IsEnabled = false;
                }
            }
            catch (UnauthorizedAccessException /*ex*/)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    //ex.Message,
                                  Properties.Resources.CorteGanado_ErrorInicializarLectorRFID,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
            catch (Exception /*ex*/)
            {
                //Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarLectorRFID,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
                //Si el Lector RFID estan desconectados se verifica si se tiene permiso para capturar manualmente
                VerificarPermisosCapturaManual();
            }
        }

        /// <summary>
        /// Inicializa el lector RFID cuando el formulario esta listo para captura
        /// </summary>
        private void InicializarLectorRFID()
        {
            try
            {
                if (spManagerRFID != null)
                    spManagerRFID.StartListening(configRFID.Puerto,
                        configRFID.Baudrate,
                        configRFID.Paridad,
                        configRFID.Databits,
                        configRFID.BitStop);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            VerificarPermisosCapturaManual();
        }

        /// <summary>
        /// Verifica si el equipo no tiene lector conectado, si es asi y tiene permisos de captura manual activa el txtAreteMetalico
        /// </summary>
        private void VerificarPermisosCapturaManual()
        {
            //Si el Lector RFID estan desconectados se verifica si se tiene permiso para capturar manualmente
            if (rfidConectado == false)
            {
                if (configRFID == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                }
                else
                {

                    txtAreteTestigo.IsEnabled = true;
                    if (!configRFID.CapturaManual)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Metodo que se ejecuta cuando el lector RFID obtiene una lectura de arete
        /// </summary>
        private void CapturarAreteRFID()
        {
            if (Application.Current.Windows[ConstantesVista.WindowPrincipal].IsActive)
            {
                if (!bloquearRFID)
                {
                    if (txtNoIndividual.Text == "" && rfidCapturadoGlobal != "")
                    {
                        var tipoAreteMetalico = 2;
                        bloquearRFID = ObtenerInfoAnimal(rfidCapturadoGlobal, tipoAreteMetalico);

                        if (bloquearRFID)
                            this.InicializarBascula();

                        txtNoIndividual.Focus();
                    }
                    else if ((reimplanteInfoGen != null || esAreteNuevo) && rfidCapturadoGlobal != txtAreteTestigo.Text)
                    {

                        txtAreteTestigo.Text = rfidCapturadoGlobal;
                        ValidarAreteTestigoRegistrado();
                    }
                }
                //else if (!bloquearRFID && (reimplanteInfoGen != null || esAreteNuevo) && rfidCapturadoGlobal != txtAreteTestigo.Text)

            }
        }

        /// <summary>
        /// Metodo para obtener la infomracion de un Arete Metalico
        /// </summary>
        private void ObtenerInformacionAreteMetalico()
        {
            try
            {
                if (txtAreteTestigo.Text.Trim().Length > 0)
                {
                    var tipoAreteMetalico = 2;
                    if (ObtenerInfoAnimal(txtAreteTestigo.Text, tipoAreteMetalico))
                    {
                        txtAreteTestigo.IsEnabled = false;
                        if (!pesoTomado)
                        {
                            //Si el peso no ha sido tomado
                            CapturarPeso();
                        }
                        txtNoIndividual.Focus();
                    }
                    else
                    {
                        txtAreteTestigo.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CapturaAreteMetalico_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para limpiar la informacion cuando no se encuentra el animal.
        /// </summary>
        /// <param name="tipoArete"></param>
        private void LimpiarObtenerAnimalInfo(int tipoArete)
        {
            Limpiar();
            txtNoIndividual.Text = string.Empty;
            txtAreteTestigo.Text = string.Empty;
            if (tipoArete == 1)
                txtNoIndividual.Focus();
            else
                txtAreteTestigo.Focus();
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

        /// <summary>
        /// Metodo para validar si un Arete Metalico ya se encuentra registrado en la organizacion
        /// </summary>
        /// <returns></returns>
        private bool ValidarAreteTestigoRegistrado()
        {
            var resultadoValidacion = ComprobarAreteRegistrado("", txtAreteTestigo.Text);
            if (resultadoValidacion.Resultado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_AreteRFIDRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                txtAreteTestigo.Text = "";
                bloquearRFID = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo para validar si un Arete Individual ya se encuentra registrado en la organizacion
        /// </summary>
        /// <returns></returns>
        private bool ValidarAreteIndividualRegistrado()
        {
            var resultadoValidacion = ComprobarAreteRegistrado(txtNoIndividual.Text, "");
            if (resultadoValidacion.Resultado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_AreteRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                txtNoIndividual.Text = "";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Valida si el parametro lectura doble arete esta registrado
        /// </summary>
        private void ValidarParametroLecturaDobleArete()
        {
            ParametroPL parametroPL = new ParametroPL();
            ParametroInfo parametroInfo = new ParametroInfo();
            ParametroOrganizacionPL parametroOrganizacionPL = new ParametroOrganizacionPL();
            ParametroOrganizacionInfo parametroOrganizacionInfo = new ParametroOrganizacionInfo();
            List<ParametroInfo> listaParametro = parametroPL.ObtenerTodos(EstatusEnum.Activo).Where(param => param.Clave == ParametrosEnum.LECTURADOBLEARETE.ToString()).ToList();
            if (listaParametro != null)
            {
                listaParametro = listaParametro.Where(param => param.Clave == ParametrosEnum.LECTURADOBLEARETE.ToString()).ToList();
            }

            if (listaParametro.Count == 1)
            {
                parametroOrganizacionInfo = parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionID, ParametrosEnum.LECTURADOBLEARETE.ToString());
                if (parametroOrganizacionInfo != null)
                {
                    parametroLecturaDobleArete = (parametroOrganizacionInfo.Valor.ToUpper() == "TRUE");
                }
            }
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
                configBasculaReimplante = ObtenerParametroDispositivo(parametros);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoRFID,
                    OrganizacionID = organizacionID
                };
                parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaInfo.TrampaID);
                configRFID = ObtenerParametroDispositivo(parametros);

                spManagerBascula = new SerialPortManager();
                spManagerRFID = new SerialPortManager();

                spManagerBascula.NewSerialDataRecieved += (spManagerBascula_NewSerialDataRecieved);
                spManagerRFID.NewSerialDataRecieved += (spManager_NewSerialDataRecievedRFID);

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



        /// <summary>
        /// Funcion para verificar que la bascula este enfuncionamient
        /// </summary>
        private void ProbarBascula()
        {
            //Se prueba el funcionamiento de los display de la bascula
            try
            {
                if (spManagerBascula == null) 
                    return;
                try
                {
                    spManagerBascula.Dispose();
                }
                catch (Exception) { }
                spManagerBascula.StopListening();

                spManagerBascula.StartListening(configBasculaReimplante.Puerto,
                    configBasculaReimplante.Baudrate,
                    configBasculaReimplante.Paridad,
                    configBasculaReimplante.Databits,
                    configBasculaReimplante.BitStop);
                spManagerBascula.StopListening();
                basculaConectada = true;
                txtPesoReimplante.IsEnabled = false;

            }
            catch (Exception)
            {
                Logger.Info();
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarBascula,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                basculaConectada = false;
            }
        }

        /// <summary>
        /// Metodo para llenar el combo de implantador
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
                    var oficio = new OperadorInfo { Nombre = "Seleccione", OperadorID = 0 };
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

            }
        }

        /// <summary>
        /// Metodo para habilitar o deshabilitar los groupBox del formulario
        /// </summary>
        private void HabilitarControlesGeneral(bool habilitar)
        {
            gpbDatosGenerales.IsEnabled = false;
            gpbIndicadoresProductividad.IsEnabled = false;
            gpbDatosGanado.IsEnabled = habilitar;
            gpbDatosImplantador.IsEnabled = habilitar;
            gpbObservaciones.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
            btnReasignar.IsEnabled = habilitar;
        }

        private void Limpiar()
        {
            if (bandBack)
            {
                txtNoIndividual.Text = String.Empty;
                txtAreteTestigo.Text = String.Empty;
            }
            else
            {
                if (txtNoIndividual.IsFocused)
                    txtAreteTestigo.Text = String.Empty;
                else
                    txtNoIndividual.Text = String.Empty;
            }

            txtPesoReimplante.Text = String.Empty;
            dpFechaReimplante.SelectedDate = DateTime.Now;

            txtCorralDestino.Text = String.Empty;
            txtGananciaDiaria.Text = String.Empty;
            txtPeriodoDias.Text = String.Empty;
            txtPesoOrigen.Text = String.Empty;
            txtPesoCorte.Text = String.Empty;
            txtNoReimplante.Text = String.Empty;

            txtObservaciones.Text = String.Empty;
            txtDisplayPeso.Text = String.Empty;

            dgTratamientos.ItemsSource = null;

            pesoTomado = false;
            this._pesoParcial = string.Empty;
            this._pesoGlobal = string.Empty;
            this.vecesPesoCotejado = 0;

            if(this._timerListeningPort != null)
             this._timerListeningPort.Stop();

            if (basculaConectada)
            {
                this.spManagerBascula.StopListening();
            }

            corralInfoGen = null;
            reimplanteInfoGen = null;

            txtCorralOrigen.Text = String.Empty;

            lblTotalResultado.Content = 0;
            lblEnfermeriaResultado.Content = 0;
            lblToltalReimplantarResultado.Content = 0;
            lblMuertasResultado.Content = 0;
            lblReimplantadasResultado.Content = 0;

            cambiarArete = false;
            cambiarTipoGanado = false;
            esAreteNuevo = false;
            bloquearRFID = false;
            dtpFechaRecepcion.SelectedDate = DateTime.Now;
            txtOrigen.Text = String.Empty;
            txtProveedor.Text = String.Empty;
            txtTipoAnimal.Text = String.Empty;
            bandBack = true;
            txtNoIndividual.IsEnabled = true;
            txtAreteTestigo.IsEnabled = false;

            areteIndividualAnterior = string.Empty;
            areteMetalicoAnterior = string.Empty;

            VerificarPermisosCapturaManual();
            HabilitarBotones(false);
        }


        /// <summary>
        /// Metodo para obtener No Individual
        /// </summary>
        private bool ObtenerInfoAnimal(string arete, int tipoArete)
        {
            var resp = false;
            try
            {
                var reimplante = new ReimplantePL();
                var animalPl = new AnimalPL();
                var ordenSacrificioPL = new OrdenSacrificioPL();
                var esReimplanteValido = false;
                
                var animal = new AnimalInfo
                {
                    OrganizacionIDEntrada = organizacionID,
                };
          
                //Se inicializan la busqueda de corral destino
                txtCorralOrigen.IsEnabled = false;
                txtCorralDestino.Text = "";
                corralInfoGen = null;
                esAreteNuevo = false;

                if (tipoArete == 1)
                {
                    animal.Arete = arete;
                    esReimplanteValido = reimplante.ValidarReimplate(animal);
                }
                else
                {
                    animal.AreteMetalico = arete;
                    esReimplanteValido = reimplante.ValidarReimplatePorAreteMetalico(animal);
                }

                if (esReimplanteValido)
                {
                    ReimplanteInfo reimplanteInfo = null;
                    if (tipoArete == 1)
                    {
                        reimplanteInfo = reimplante.ObtenerAreteIndividual(animal, TipoMovimiento.Corte);
                    }
                    else
                    {
                        reimplanteInfo = reimplante.ObtenerAreteMetalico(animal, TipoMovimiento.Corte);
                    }

                    if (reimplanteInfo != null)
                    {
                        if (reimplanteInfo.Animal.Venta)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.Reimplante_RechazadoEnCorte,
                                MessageBoxButton.OK, MessageImage.Warning);
                            LimpiarObtenerAnimalInfo(tipoArete);
                        }
                        else
                        {
                            AnimalInfo animalInventario = null;
                            if (reimplanteInfo.FolioProgramacionReimplanteID == -1)
                            {
                                /* Validar Si el arete existe en el inventario */
                                if (tipoArete == 1)
                                {
                                    animalInventario = animalPl.ObtenerAnimalPorArete(animal.Arete, organizacionID);
                                }
                                else
                                {
                                    animalInventario = animalPl.ObtenerAnimalPorAreteTestigo(animal.AreteMetalico, organizacionID);
                                }
                                if (!(animalInventario != null && animalInventario.CargaInicial))
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.Reimplante_CorralSinReimplante,
                                     MessageBoxButton.OK, MessageImage.Warning);
                                    LimpiarObtenerAnimalInfo(tipoArete);
                                    return false;
                                }
                                corralInfoGen = null;
                                txtCorralOrigen.Text = String.Empty;
                            }

                            if (corralInfoGen == null)
                            {
                                reimplanteInfoGen = reimplanteInfo;

                                ObtenerDatosCompra(reimplanteInfo);
                                ObtenerIndicadoresProductividad(reimplanteInfo);
                                loteReimplante = loteReimplante ?? new LoteReimplanteInfo();
                                /* Si el animal Es carga inicial se habilita la captura de corral origen  */
                                if (reimplanteInfo.FolioProgramacionReimplanteID == -1 &&
                                    animalInventario != null && animalInventario.CargaInicial)
                                {
                                    txtCorralOrigen.IsEnabled = true;
                                }
                                else
                                {
                                    txtCorralOrigen.Text = reimplanteInfoGen.Corral.Codigo;
                                    corralInfoGen = reimplanteInfo.Corral;
                                    folioProgReimplante = reimplanteInfo.FolioProgramacionReimplanteID;
                                    ObtenerTotales(reimplanteInfo);
                                    loteReimplante.NumCabezas = reimplanteInfo.Lote.Cabezas;

                                    loteReimplante.LoteReimplanteID =
                                    reimplanteInfo.FolioProgramacionReimplanteID;

                                    bool existeSacrificio =
                                   ordenSacrificioPL.ValidarLoteOrdenSacrificio(reimplanteInfo.Lote.LoteID);

                                    if (existeSacrificio)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.ReimplanteGanado_CorralProgramadoSacrificio,
                                        MessageBoxButton.OK, MessageImage.Warning);
                                        LimpiarObtenerAnimalInfo(tipoArete);
                                        return false;
                                    }

                                }

                                loteReimplante.TipoMovimientoID = (int)TipoMovimiento.Reimplante;
                                loteReimplante.FolioEntrada = reimplanteInfo.Animal.FolioEntrada;

                                resp = true;
                            }
                            else
                            {
                                if (reimplanteInfo.Corral.CorralID == corralInfoGen.CorralID)
                                {
                                    reimplanteInfoGen = reimplanteInfo;
                                    txtCorralOrigen.Text = reimplanteInfoGen.Corral.Codigo;
                                    corralInfoGen = reimplanteInfo.Corral;
                                    ObtenerDatosCompra(reimplanteInfo);
                                    ObtenerIndicadoresProductividad(reimplanteInfo);
                                    //ObtenerTotales(reimplanteInfo);
                                    resp = true;
                                    folioProgReimplante = reimplanteInfo.FolioProgramacionReimplanteID;
                                    // noLoteReimplante = reimplanteInfo.LoteReimplanteID;
                                    loteReimplante.LoteReimplanteID =
                                        reimplanteInfo.FolioProgramacionReimplanteID;

                                    loteReimplante.TipoMovimientoID = (int)TipoMovimiento.Reimplante;
                                    loteReimplante.FolioEntrada = reimplanteInfo.Animal.FolioEntrada;

                                    loteReimplante.NumCabezas = reimplanteInfo.Lote.Cabezas;

                                    bool existeSacrificio = ordenSacrificioPL.ValidarLoteOrdenSacrificio(reimplanteInfo.Lote.LoteID);

                                    if (existeSacrificio)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.ReimplanteGanado_CorralProgramadoSacrificio,
                                        MessageBoxButton.OK, MessageImage.Warning);
                                        LimpiarObtenerAnimalInfo(tipoArete);
                                        return false;
                                    }

                                }
                                else
                                {
                                    var mensaje = String.Format("{0} {1}{2}",
                                        Properties.Resources.Reimplante_AreteNoPerteneceACorral1,
                                        reimplanteInfo.Corral.Codigo.Trim(),
                                        Properties.Resources.Reimplante_AreteNoPerteneceACorral2);
                                    SkMessageBox.Show(
                                        Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        mensaje, MessageBoxButton.OK, MessageImage.Warning);
                                    bandFoco = true;
                                    LimpiarObtenerAnimalInfo(tipoArete);
                                }

                            }
                            txtNoIndividual.Text = reimplanteInfo.Animal.Arete;
                            areteIndividualAnterior = reimplanteInfo.Animal.Arete;
                            txtAreteTestigo.Text = reimplanteInfo.Animal.AreteMetalico;
                            areteMetalicoAnterior = reimplanteInfo.Animal.AreteMetalico;
                        }

                    }
                    else
                    {
                        /* Validar Si el arete existe en el inventario */
                        AnimalInfo animalInventario = null;
                        if (tipoArete == 1)
                        {
                            animalInventario = animalPl.ObtenerAnimalPorArete(animal.Arete, organizacionID);
                        }
                        else
                        {
                            txtAreteTestigo.Text = animal.AreteMetalico;
                            animalInventario = animalPl.ObtenerAnimalPorAreteTestigo(animal.AreteMetalico, organizacionID);
                        }

                        if (animalInventario != null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.Reimplante_CorralSinReimplante,
                             MessageBoxButton.OK, MessageImage.Warning);
                            LimpiarObtenerAnimalInfo(tipoArete);
                            return false;
                        }

                        if (tipoArete == 1 && ValidarAreteIndividualRegistrado())
                        {
                            LimpiarObtenerAnimalInfo(tipoArete);
                            return false;
                        }
                        if (tipoArete == 2 && ValidarAreteTestigoRegistrado())
                        {
                            LimpiarObtenerAnimalInfo(tipoArete);
                            return false;
                        }

                        txtCorralOrigen.IsEnabled = true;
                        corralInfoGen = null;
                        txtCorralOrigen.Text = String.Empty;
                        esAreteNuevo = true;
                        resp = true;
                        bandFoco = true;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.Reimplante_Reimplantada,
                            MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarObtenerAnimalInfo(tipoArete);
                    bandFoco = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_ErrorConsultaNoIndividual,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            return resp;
        }

        /// <summary>
        /// Metodo para verificar el corral destino
        /// </summary>
        private bool ValidarCorralDestino()
        {
            var resp = false;
            var corralDestino = txtCorralDestino.Text;
            var reimplante = new ReimplantePL();
            try
            {
                /*Se valida el corral destino */
                int idCorral = reimplante.ValidarCorralDestinio(txtCorralOrigen.Text.Trim(), corralDestino, organizacionID);

                if (idCorral == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.Reimplante_CorralInvalido,
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtCorralDestino.Text = "";
                }
                else
                {
                    //Valida punta chica
                    var puntaChica = reimplante.ValidarCorralDestinoPuntaChica(txtCorralOrigen.Text.Trim(), corralDestino, organizacionID);
                    corralDestinoInfoGen = new CorralInfo
                    {
                        CorralID = idCorral,
                        PuntaChica = puntaChica != null && puntaChica.PuntaChica
                    };
                    resp = true;
                    var lotepl = new LotePL();
                    var corralPL = new CorralPL();

                    var loteFiltro = new LoteInfo
                                              {
                                                  CorralID = idCorral,
                                                  OrganizacionID = organizacionID
                                              };

                    CorralInfo corralInfo = corralPL.ObtenerPorId(idCorral);
                    loteNuevoInfoGen = lotepl.ObtenerPorCorralID(loteFiltro);

                    if (loteNuevoInfoGen != null && loteNuevoInfoGen.LoteID != 0)
                    {
                        if (loteNuevoInfoGen.Cabezas > corralInfo.Capacidad)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                string.Format(Properties.Resources.Reimplante_CorralCapacidad, loteNuevoInfoGen.Cabezas, corralInfo.Capacidad),
                                                MessageBoxButton.OK, MessageImage.Warning);
                            txtCorralDestino.Text = "";
                            resp = false;

                        }
                    }

                    //loteNuevoInfoGen = lotepl.ObtenerPorCorralCerrado(organizacionID, idCorral);
                    ValidarSiEsTransferenciaAsignarAreteColor(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_CorralInvalido,
                    MessageBoxButton.OK, MessageImage.Error);

            }
            return resp;
        }

        /// <summary>
        /// Validar si es transferencia para asignar un corral de color en los tratamientos
        /// </summary>
        private void ValidarSiEsTransferenciaAsignarAreteColor(bool activar)
        {
            if (!String.IsNullOrEmpty(txtCorralDestino.Text.Trim()) &&
                txtCorralDestino.Text.Trim() != txtCorralOrigen.Text.Trim() &&
                codigoAreteColorReimplante > 0)
            {
                //Checar el codigo de tratamiento
                if (_listaTratamientos != null)
                {
                    foreach (var listaTratamiento in
                                from listaTratamiento in _listaTratamientos
                                where listaTratamiento.Productos != null
                                from producto in listaTratamiento.Productos.Where(
                                     producto => producto.ProductoId == codigoAreteColorReimplante)
                                select listaTratamiento)
                    {
                        listaTratamiento.Seleccionado = activar;
                    }

                    dgTratamientos.ItemsSource = null;
                    dgTratamientos.ItemsSource = _listaTratamientos;
                }
            }
        }

        /// <summary>
        /// Metodo para obtener los datos Totales
        /// </summary>
        private void ObtenerIndicadoresProductividad(ReimplanteInfo infoReimplante)
        {
            var ganancia = "";
            try
            {
                txtPesoOrigen.Text = infoReimplante.Animal.PesoCompra.ToString(CultureInfo.InvariantCulture);
                txtPesoCorte.Text = infoReimplante.PesoCorte.ToString(CultureInfo.InvariantCulture);
                txtNoReimplante.Text = infoReimplante.NumeroReimplante.ToString(CultureInfo.InvariantCulture);
                //Ganancia Diaria = (Peso Reimplante – Peso Origen)/ Días de engorda.
                var diasEngorda = DateTime.Now - DateTime.Parse(dtpFechaRecepcion.Text);
                //var diasEngorda = DateTime.Now - infoReimplante.Animal.FechaCompra;
                txtPeriodoDias.Text = diasEngorda.Days.ToString(CultureInfo.InvariantCulture);
                try
                {
                    if (diasEngorda.Days > 0 && !String.IsNullOrEmpty(txtPesoReimplante.Text))
                    {
                        var peso = int.Parse(txtPesoReimplante.Text) - infoReimplante.Animal.PesoCompra;
                        ganancia = Math.Round(((decimal)peso / diasEngorda.Days), 3).ToString(CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {

                    txtPesoReimplante.Text = string.Empty;
                    LimpiarTratamiento(true);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            txtGananciaDiaria.Text = ganancia.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Metodo para obtener los datos Totales
        /// </summary>
        private void ObtenerTotales(ReimplanteInfo infoReimplante)
        {
            try
            {
                lblTotalResultado.Content = infoReimplante.Lote.Cabezas;
                var pl = new ReimplantePL();
                var ganadoEnfermeria = new EntradaGanadoInfo
                {
                    OrganizacionID = organizacionID,
                    FolioEntrada = infoReimplante.Animal.FolioEntrada,
                    FolioEntradaAgrupado = infoReimplante.Animal.FolioEntrada.ToString(CultureInfo.InvariantCulture),
                    LoteID = infoReimplante.Lote.LoteID
                };

                lblEnfermeriaResultado.Content =
                pl.ObtenerCabezasEnEnfermeria(ganadoEnfermeria,
                                                        (int)TipoMovimiento.EntradaEnfermeria);
                var cabezas = new CabezasCortadas
                {
                    TipoMovimiento = (int)TipoMovimiento.Reimplante,
                    OrganizacionID = organizacionID,
                    NoPartida = infoReimplante.Lote.LoteID.ToString(CultureInfo.InvariantCulture)

                };

                List<CabezasCortadas> cabezasReimplantadas = pl.ObtenerCabezasReimplantadas(cabezas);

                if (cabezasReimplantadas == null)
                {
                    cabezasReimplantadas = new List<CabezasCortadas>();
                }

                CabezasCortadas cabezasLote =
                    cabezasReimplantadas.FirstOrDefault(cab => cab.LoteID == infoReimplante.Lote.LoteID);

                var cabezaMuertas = new CabezasCortadas
                {
                    TipoMovimiento = (int)TipoMovimiento.Muerte,
                    OrganizacionID = organizacionID,
                    NoPartida = infoReimplante.Lote.LoteID.ToString(CultureInfo.InvariantCulture)
                };

                var cabezarMuertas = pl.ObtenerCabezasMuertas(cabezaMuertas);

                cabezarMuertas = cabezarMuertas < 0 ? 0 : cabezarMuertas;
                int reimplantadas = cabezasLote == null ? 0 : cabezasLote.Cabezas;

                lblMuertasResultado.Content = cabezarMuertas;

                lblReimplantadasResultado.Content = cabezasReimplantadas.Sum(cab => cab.Cabezas);

                var animalPL = new AnimalPL();
                infoReimplante.Lote.OrganizacionID = organizacionID;
                var animalesLote = animalPL.ObtenerAnimalesPorLoteID(infoReimplante.Lote);

                int animalesLoteTotal = 0;

                if (animalesLote != null && animalesLote.Any())
                {
                    animalesLoteTotal = animalesLote.Count;
                }
                var totalCabezas = (animalesLoteTotal -
                                    (int)lblEnfermeriaResultado.Content)
                                    - reimplantadas
                                    - cabezarMuertas;

                lblToltalReimplantarResultado.Content = totalCabezas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_ErrorCalculoTotales,
                    MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Metodo para obtener los datos de la compra
        /// </summary>
        private void ObtenerDatosCompra(ReimplanteInfo infoReimplante)
        {
            try
            {
                var reimplante = new ReimplantePL();
                var animal = infoReimplante.Animal;
                animal.OrganizacionIDEntrada = organizacionID;

                var infoDatosCopmra = reimplante.ObtenerDatosCompra(animal);

                if (infoDatosCopmra != null)
                {
                    txtOrigen.Text = infoDatosCopmra.Origen;
                    txtProveedor.Text = infoDatosCopmra.Proveedor;
                    txtTipoAnimal.Text = infoDatosCopmra.TipoAnimal;
                    dtpFechaRecepcion.SelectedDate = infoDatosCopmra.FechaInicio;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_ErrorConsultaNoIndividual,
                    MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Metodo Verificar si existen Partidas programadas
        /// </summary>
        private bool ExisteProgramacionReimplante()
        {
            var bExiste = false;
            try
            {
                var reimplantePL = new ReimplantePL();
                var resultadoBusqueda = reimplantePL.ExisteProgramacionReimplate(organizacionID);
                if (resultadoBusqueda)
                {
                    bExiste = true;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_ExisteProgramacionReimplante,
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Reimplante_ExisteProgramacionReimplante,
                    MessageBoxButton.OK,
                    MessageImage.Error);
            }
            return bExiste;
        }

        /// <summary>
        /// Metodo Verificar si existen Trampas configuradas
        /// </summary>
        private bool ExisteTrampa()
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
            }
            return bExiste;
        }

        private bool ValidarCamposBlancos()
        {
            if (String.IsNullOrEmpty(txtNoIndividual.Text))
            {
                txtNoIndividual.Focus();
                return true;
            }
            if (String.IsNullOrEmpty(txtAreteTestigo.Text))
            {
                if (parametroLecturaDobleArete)
                {
                    txtAreteTestigo.Focus();
                    return true;
                }
                else
                    txtAreteTestigo.Text = string.Empty;
            }
            if (String.IsNullOrEmpty(txtPesoReimplante.Text))
            {
                txtPesoReimplante.Focus();
                return true;
            }
            if (String.IsNullOrEmpty(txtCorralDestino.Text))
            {
                txtCorralDestino.Focus();
                return true;
            }
            if (String.IsNullOrEmpty(cboImplantador.Text) || cboImplantador.Text.Trim() == "Seleccione")
            {
                cboImplantador.Focus();
                return true;
            }
            if (txtCorralOrigen.IsEnabled)
            {
                if (String.IsNullOrEmpty(txtCorralOrigen.Text))
                {
                    txtCorralOrigen.Focus();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Almacena el registro de animal movimiento
        /// </summary>
        /// <param name="reimplanteInfo">Informacion del Animal</param>
        /// <param name="usuarioId">Id del Usuario</param>
        /// <returns></returns>
        private AnimalMovimientoInfo GuardarAnimalMovimiento(ReimplanteInfo reimplanteInfo, int usuarioId)
        {
            var animalMovimientoPL = new AnimalMovimientoPL();
            var lotePl = new LotePL();
            var animalMovimientoInfo = new AnimalMovimientoInfo
                                           {
                                               AnimalID = reimplanteInfo.Animal.AnimalID,
                                               OrganizacionID = organizacionID,
                                               CorralID = reimplanteInfo.Corral.CorralID,
                                               LoteID = reimplanteInfo.Lote.LoteID
                                           };
            if (String.Compare(txtCorralOrigen.Text.Trim(), txtCorralDestino.Text.Trim(), StringComparison.Ordinal) == 0)
            {
                animalMovimientoInfo.CorralID = reimplanteInfo.Corral.CorralID;
                animalMovimientoInfo.LoteID = reimplanteInfo.Lote.LoteID;
            }
            else
            {
                //Si son diferentes obtener el corral origen del corralDestino
                animalMovimientoInfo.CorralID = corralDestinoInfoGen.CorralID;
                if (loteNuevoInfoGen != null && loteNuevoInfoGen.LoteID > 0)//Si ya tiene un lote asignado
                {
                    animalMovimientoInfo.LoteID = loteNuevoInfoGen.LoteID;
                }
                else
                {
                    //Sino tiene asignado un Lote Se crea uno
                    GeneraLote();
                    if (loteNuevoInfoGen != null)
                    {
                        loteNuevoInfoGen.LoteID = lotePl.GuardaLote(loteNuevoInfoGen);
                        loteNuevoInfoGen = lotePl.ObtenerPorId(loteNuevoInfoGen.LoteID);

                        //Se crea un LoteID nuevo y se conserva el campo Lote
                        loteNuevoInfoGen.Lote = reimplanteInfo.Lote.Lote;
                        loteNuevoInfoGen.FechaCierre = reimplanteInfo.Lote.FechaCierre;
                        loteNuevoInfoGen.FechaDisponibilidad = reimplanteInfo.Lote.FechaDisponibilidad;

                        //Se actualiza el campo Lote de la tabla Lote para conservar el proceso de engorda
                        // Se va actualizar el campo Lote cuando el reimplante sea de 1 corral origen a 1 corral destino
                        var programacionCortePl = new ProgramacionReimplantePL();
                        if (programacionCortePl.ValidarReimplanteCorralACorral(
                                        reimplanteInfo.Lote.LoteID,
                                        loteNuevoInfoGen.CorralID))
                        {
                            lotePl.ActualizarLoteALote(loteNuevoInfoGen);
                        }

                        animalMovimientoInfo.LoteID = loteNuevoInfoGen.LoteID;
                    }
                }
            }
            animalMovimientoInfo.FechaMovimiento = DateTime.Parse(dpFechaReimplante.Text);
            //Modificado por: Andres Vejar. Se coloca el peso de reimplante no peso corte
            animalMovimientoInfo.Peso = int.Parse(txtPesoReimplante.Text);
            animalMovimientoInfo.Temperatura = 0;
            animalMovimientoInfo.TipoMovimientoID = (int)TipoMovimiento.Reimplante;

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
            animalMovimientoInfo = animalMovimientoPL.GuardarAnimalMovimiento(animalMovimientoInfo);
            //Se valida si el corral destino tiene puncha chica
            if (corralDestinoInfoGen.PuntaChica)
            {
                var animalPl = new AnimalPL();
                var animalInfo = new AnimalInfo
                {
                    AnimalID = animalMovimientoInfo.AnimalID,
                    ClasificacionGanadoID = ClasificacionGanado.PuntaChica.GetHashCode(),
                    UsuarioModificacionID = usuarioId,
                };
                animalPl.ActualizaClasificacionGanado(animalInfo);
            }

            return animalMovimientoInfo;
        }

        /// <summary>
        /// Guarda la fecha real en la tabla de lote reimplante
        /// </summary>
        private void GuardarFechaReal()
        {
            try
            {
                //loteReimplante.FolioEntrada = folioProgReimplante;
                loteReimplante.FolioEntrada = reimplanteInfoGen.Animal.FolioEntrada;
                loteReimplante.TipoMovimientoID = (int)TipoMovimiento.Reimplante;
                loteReimplante.LoteReimplanteID = reimplanteInfoGen.LoteReimplanteID;
                var programacionCortePl = new ProgramacionReimplantePL();
                //Se actualiza la fehca real en lote reimplante
                programacionCortePl.GuardarFechaReal(dpFechaReimplante.Text, loteReimplante);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Genera un Objeto de Tipo Lote
        /// </summary>
        private void GeneraLote()
        {
            var lotePl = new LotePL();

            if (loteNuevoInfoGen != null)
            {
                loteNuevoInfoGen = lotePl.ObtenerPorOrganizacionIdLote(organizacionID, loteNuevoInfoGen.Lote);
            }
            if (loteNuevoInfoGen == null)
            {
                loteNuevoInfoGen = (loteNuevoInfoGen ?? new LoteInfo());
                var tipoOrganizacion = ObtenerTiposOrigen(reimplanteInfoGen.TipoOrigen);
                loteNuevoInfoGen.TipoProcesoID = tipoOrganizacion.TipoProceso.TipoProcesoID;
                loteNuevoInfoGen.OrganizacionID = organizacionID;
                loteNuevoInfoGen.UsuarioCreacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                if (corralDestinoInfoGen.CorralID > 0)
                {
                    var corral = ObtenerCorral(corralDestinoInfoGen.CorralID);
                    corralDestinoInfoGen = corral;
                    if (corral != null)
                    {
                        loteNuevoInfoGen.CorralID = corralDestinoInfoGen.CorralID;
                        loteNuevoInfoGen.TipoCorralID = corral.TipoCorral.TipoCorralID;
                    }
                }
                loteNuevoInfoGen.Activo = EstatusEnum.Activo;
                loteNuevoInfoGen.DisponibilidadManual = false;
                loteNuevoInfoGen.Cabezas = Convert.ToInt32(0);
                loteNuevoInfoGen.CabezasInicio = Convert.ToInt32(0);
            }
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
            }
            return resp;
        }



        /// <summary>
        /// Metodo para capturar el peso en el display
        /// </summary>
        private void CapturaPesoEnDisplay()
        {
            txtDisplayPeso.Text = this._pesoParcial;
            if (!capturaInmediata)
            {
                int iPesoParcial = 0;
                int iPesoGlobla = 0;
                int.TryParse(this._pesoParcial, out iPesoParcial);
                int.TryParse(this._pesoGlobal, out iPesoGlobla);

                if (iPesoParcial > 0 && iPesoGlobla > 0)
                {
                    if (iPesoParcial == iPesoGlobla)
                    {
                        this.vecesPesoCotejado++;
                        if (this.vecesPesoCotejado == this.configBasculaReimplante.Espera)
                        {
                            this.txtPesoReimplante.Text = _pesoGlobal = _pesoParcial;
                            this._timerListeningPort.Stop();
                            this.spManagerBascula.StopListening();
                            if (reimplanteInfoGen != null)
                            {
                                ObtenerIndicadoresProductividad(reimplanteInfoGen);
                                this.pesoTomado = true;
                                ObtenerTratamiento();
                            }
                        }else
                            this.spManagerBascula.StopListening();
                    }
                    else
                    {
                        this.vecesPesoCotejado = 0;
                        this._pesoGlobal = this._pesoParcial;
                        this.spManagerBascula.StopListening();

                    }
                }
                else
                {
                    this.vecesPesoCotejado = 0;
                    this._pesoGlobal = this._pesoParcial;
                    this.spManagerBascula.StopListening();
                }


            }
            else
            {
                this.txtPesoReimplante.Text = this._pesoGlobal = this._pesoParcial;
                capturaInmediata = false;
                this.spManagerBascula.StopListening();
                if (reimplanteInfoGen != null)
                {
                    ObtenerIndicadoresProductividad(reimplanteInfoGen);
                    this.pesoTomado = true;
                    ObtenerTratamiento();
                }

            }
        }
        #endregion


      
    }
}
