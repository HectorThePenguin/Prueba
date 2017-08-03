using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.PL;
using Costo = SIE.Services.Info.Enums.Costo;
using TipoCorral = SIE.Services.Info.Enums.TipoCorral;
using System.Runtime.InteropServices;
using SIE.Services.Info.Filtros;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para CorteTransferenciaGanado.xaml
    /// </summary>
    public partial class CorteTransferenciaGanado
    {
        #region Atributos 
        private int trampaID;
        private int organizacionId;
        private SerialPortManager _spManager;
        private SerialPortManager _spManagerTermo;
        private SerialPortManager _spManagerRFID;
        private BasculaCorteSection _configBasculaCorte;
        private BasculaCorteSection _configTermometroCorte;
        private BasculaCorteSection _configRFIDCorte;

        private bool termometroConectado;
        private bool basculaConectada;
        private bool rfidConectado;
        private int _timerTickCount;
        private bool pesoTomado;
        private int usuario;
        private bool tempTomada;
        private DispatcherTimer _timer;
        int countTemp;
        private AnimalMovimientoInfo loteCorralOrigen;
        private TrampaInfo trampaIDInfo;

        double lastTemp, maxTemp;
        private double maxTemperaturaAnimal;
        IList<TratamientoInfo> listaTratamientos;
        private int _codigoTratamientoTemperatura;
        private int _tipoGanado;
        private bool bandBack = true;
        private bool ctrlPegar;
        private String textoAnterior;
        AlmacenInfo almacenInfo;
        TrampaInfo trampaInfo = null;
        
        private AnimalInfo animalActual;
        private bool banderaDeleteAnimalSalida;
        private CorralInfo corralInfoGen;
        private AnimalInfo animalInfoGlobal;
        private int corralIDGlobal;
        private CorralInfo corralGlobal;
        private LoteInfo loteCorralDestino;
        private bool banderaPermisoTranpa;
        private AnimalSalidaInfo animalSalidaInfo;
        private string temperaturaCapturadaGlobal = string.Empty;
        private string pesoCapturadoGlobal = string.Empty;
        private string pesoParcial = string.Empty;
        private string rfidCapturadoGlobal = string.Empty;
        private bool capturaInmediata = false;
        private bool capturaInmediataTermo = false;

        private List<int> listaTratamientosProduccion = new List<int> { 25, 27, 28 };

        private List<int> listaTratamientosArete = new List<int> { 25, 26, 27, 28 };

        private List<CorralInfo> corralesImproductivos = new List<CorralInfo>();

        private uint start = 0;
        private bool aplicaScanner;
        private int tipoCapturaArete = 3;
        #endregion

        #region Constructor
        public CorteTransferenciaGanado()
        {

            animalSalidaInfo = new AnimalSalidaInfo();
            banderaPermisoTranpa = false;
            loteCorralDestino = new LoteInfo();
            loteCorralOrigen = new AnimalMovimientoInfo();
            corralIDGlobal = 0;
            trampaID = 0;
            rfidConectado = false;
            banderaDeleteAnimalSalida = false;
            InitializeComponent();
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            DeshabilitarControles(false, false);

            ConfiguracionParametrosPL parametrosPL = new ConfiguracionParametrosPL();
            
            //Se valida que existan trampas configuradas
            if (!ExistenTrampas())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_NoExistenTrampas,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                banderaPermisoTranpa = true;
                DeshabilitarControles(false, false);
                return;
            }
            UserInitialization();

            //Se valida que la trampa tenga relacionado un almacen
            if (!ExistenAlmacenParaTrampa())
            {
                banderaPermisoTranpa = true;
                DeshabilitarControles(false, false);
                return;
            }

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
                    _spManager.StopListening();
                    basculaConectada = true;
                    txtPesoCorte.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
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
                        banderaPermisoTranpa = false;
                        DeshabilitarControles(false, false);
                    }
                    else
                    {
                        if (!_configBasculaCorte.CapturaManual)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualBascula,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                            banderaPermisoTranpa = false;
                            DeshabilitarControles(false, false);
                        }
                    }

                }
            }

            //Se prueba el funcionamiento de los display del termometro
            try
            {
                if (termometroConectado == false)
                {
                    if (_configTermometroCorte == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualTermometro,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                        banderaPermisoTranpa = false;
                        DeshabilitarControles(false, false);
                    }
                    else
                    {
                        if (!_configTermometroCorte.CapturaManual)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualTermometro,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                            banderaPermisoTranpa = false;
                            DeshabilitarControles(false, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarTermometro,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
               
            }

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
                }
                else
                {
                    txtAreteMetalico.IsEnabled = true;
                    if (!_configRFIDCorte.CapturaManual)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                            MessageBoxButton.OK,
                            MessageImage.Warning);
                        banderaPermisoTranpa = false;
                        DeshabilitarControles(false, false);
                    }
                }
                
            }
            catch (Exception ex)
            {
                txtAreteMetalico.IsEnabled = true;
                if (!_configRFIDCorte.CapturaManual)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                    banderaPermisoTranpa = false;
                    DeshabilitarControles(false, false);
                }
                Logger.Error(ex);
            }
        }
        #endregion

        #region Eventos
        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();
        /// <summary>
        /// Evento cargar pantalla y llena los combos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucTitulo_Loaded(object sender, RoutedEventArgs e)
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
            if (cboPaletas.SelectedIndex <= 0)
            {
                CargarComboTipoGanado();
                cboTipoGanado.SelectedValue = _tipoGanado;
            }
            
            try
            {
                if (_spManagerTermo != null)
                {
                    try { 
                        _spManagerTermo.Dispose();
                    }
                    catch (Exception) 
                    {
                    }

                    _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                        _configTermometroCorte.Baudrate,
                        _configTermometroCorte.Paridad,
                        _configTermometroCorte.Databits,
                        _configTermometroCorte.BitStop);
                    termometroConectado = true;
                    txtTemperatura.IsEnabled = false;
                }
                if (_spManager != null)
                {
                    try
                    {
                        _spManager.Dispose();
                    }
                    catch (Exception)
                    {
                    }

                    //_spManager.StartListening(_configBasculaCorte.Puerto,
                    //    _configBasculaCorte.Baudrate,
                    //    _configBasculaCorte.Paridad,
                    //    _configBasculaCorte.Databits,
                    //    _configBasculaCorte.BitStop);
                    basculaConectada = true;
                    this.InicializarDispositivos();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            txtNoIndividual.Focus();
            LeerConfiguracion();
        }

        /// <summary>
        /// Key up de retroseso y suprimir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (cboSexo.SelectedIndex == 0)
                {
                    bandBack = false;
                    LimpiarCaptura(false);
                    DeshabilitarControles(false, false);
                    pesoTomado = false;
                    tempTomada = false;
                    bandBack = true;
                }
            }
        }


        private bool ConsultarDatosPorArete()
        {
            bool handled = false;
            if (txtNoIndividual.Text.Trim().Length > 0 || txtAreteMetalico.Text.Length > 0)
            {
                var animalPL = new AnimalPL();
                //Se valida que pertenezca a la misma partida
                var animalInfo = new AnimalInfo
                {
                    Arete = txtNoIndividual.Text,
                    OrganizacionIDEntrada = organizacionId
                };
                animalInfoGlobal = null;
                if (string.IsNullOrWhiteSpace(animalInfo.Arete))
                {
                    if (txtAreteMetalico.Text.Trim() != string.Empty)
                    {
                        animalInfo = animalPL.ObtenerAnimalPorAreteTestigo(txtAreteMetalico.Text,
                                                                           organizacionId);
                        if (animalInfo == null)
                        {
                            animalInfo = new AnimalInfo
                            {
                                Arete = string.Empty,
                                OrganizacionIDEntrada = organizacionId
                            };
                        }
                    }
                }

                var corteGanadoPl = new CorteGanadoPL();
                var enfermeriaPL = new EnfermeriaPL();
                var ordenSacrificioPL = new OrdenSacrificioPL();

                if (animalInfo.Arete != string.Empty)
                {
                    animalInfoGlobal = corteGanadoPl.ExisteAreteEnPartida(animalInfo);
                }
                else if (animalInfo.AreteMetalico != string.Empty)
                {
                    animalInfoGlobal = corteGanadoPl.ExisteAreteMetalicoEnPartida(animalInfo);
                }
                if (animalInfoGlobal != null)
                {

                    if (ValidarAnimalCorraletaRecuperacion(animalInfoGlobal))
                    {
                        if (!ExisteAreteEnInventario(animalInfoGlobal)) return handled;
                        loteCorralOrigen = enfermeriaPL.ObtenerUltimoMovimientoRecuperacion(animalInfoGlobal);



                        if (loteCorralOrigen != null)
                        {
                            bool existeSacrificio =
                                ordenSacrificioPL.ValidarLoteOrdenSacrificio(loteCorralOrigen.LoteID);

                            if (existeSacrificio)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.CorteTransferenciaGanado_CorralProgramadoSacrificio,
                           MessageBoxButton.OK, MessageImage.Warning);
                                LimpiarCaptura(false);
                                pesoTomado = false;
                                tempTomada = false;
                                DeshabilitarControles(false, true);
                                handled = true;
                                return handled;
                            }

                            ObtenerTotalCabezas(loteCorralOrigen);
                            InicializarDispositivos();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_ConsultarNoIndividualError,
                            MessageBoxButton.OK, MessageImage.Warning);
                            LimpiarCaptura(false);
                            pesoTomado = false;
                            tempTomada = false;
                            DeshabilitarControles(false, true);
                            handled = true;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_NoIndividualNoCorralRecuperado,
                            MessageBoxButton.OK, MessageImage.Warning);
                        LimpiarCaptura(false);
                        pesoTomado = false;
                        tempTomada = false;
                        handled = true;
                    }
                }
                else
                {
                    if (txtNoIndividual.Text.Trim() != string.Empty)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CorteTransferenciaGanado_NoIndividualNoExiste,
                                MessageBoxButton.OK, MessageImage.Warning);
                    }
                    else if (txtAreteMetalico.Text.Trim() != string.Empty)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CorteTransferenciaGanado_MsgAreteMetalicoNoExiste,
                                MessageBoxButton.OK, MessageImage.Warning);
                    }
                    LimpiarCaptura(false);
                    pesoTomado = false;
                    tempTomada = false;
                    handled = true;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CapturaArete_CorteGanado,
                    MessageBoxButton.OK, MessageImage.Warning);

                txtNoIndividual.Focusable = true;
                txtNoIndividual.Focus();
                handled = true;
            }

            if (cboSexo.SelectedIndex > 0)
            {
                if (!basculaConectada)
                {
                    txtPesoCorte.IsEnabled = true;
                }

                if (!termometroConectado)
                {
                    txtTemperatura.IsEnabled = true;
                }
            }
            else
            {
                txtPesoCorte.IsEnabled = false;
                txtTemperatura.IsEnabled = false;
            }
            return handled;
        }

        /// <summary>
        /// Consultar el numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_KeyDown(object sender, KeyEventArgs e)
        {
            start = GetTickCount();
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (cboSexo.SelectedIndex == 0 && txtNoIndividual.Text.Trim() != string.Empty)
                {
                    e.Handled = ConsultarDatosPorArete();
                    ObtenerTipoCaptura(false);
                }
                else
                {
                    e.Handled = validarNuevoArete(txtNoIndividual.Text, false);
                }
            }
        }

        /// <summary>
        /// Consultar el RFID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAreteMetalico_KeyDown(object sender, KeyEventArgs e)
        {
            start = GetTickCount();
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (cboSexo.SelectedIndex == 0)
                {
                    e.Handled = ConsultarDatosPorArete();
                    ObtenerTipoCaptura(true);
                }
                else
                {
                    e.Handled = validarNuevoArete(txtAreteMetalico.Text, true);
                }
            }
        }

        private bool validarNuevoArete(string arete, bool esRFID)
        {
            bool resultado = false;
            if (arete.Trim() != string.Empty)
            {
                if (VerificarExisteciaArete(true, arete, esRFID, organizacionId))
                {
                    if (esRFID)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_MsgNuevoRFIDYaRegistrado,
                            MessageBoxButton.OK, MessageImage.Warning);
                        txtAreteMetalico.Text = "";
                        if (txtAreteMetalico.IsEnabled)
                        {
                            txtAreteMetalico.Focusable = true;
                            txtAreteMetalico.Focus();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteTransferenciaGanado_MsgNuevoAreteYaRegistrado,
                            MessageBoxButton.OK, MessageImage.Warning);
                        txtNoIndividual.Text = "";
                        txtNoIndividual.Focusable = true;
                        txtNoIndividual.Focus();
                    }
                }
            }
            else
            {
                resultado = true;
            }
            return !resultado;
        }

            
        /// <summary>
        /// Unchecked de tratamientos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTratamiento_unchecked(object sender, RoutedEventArgs e)
        {
            var dato = (TratamientoInfo)dgTratamientos.SelectedItem;
            if (dato != null)
                dato.Seleccionado = false;
        }


        /// <summary>
        /// textchanged de numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtNoIndividual.Text))
            {
                ctrlPegar = false;
                txtNoIndividual.Text = txtNoIndividual.Text.Replace(" ", "");
            }
            else
            {
                if (ctrlPegar)
                {
                    txtNoIndividual.Text = textoAnterior;
                    ctrlPegar = false;
                    textoAnterior = String.Empty;
                }
            }
        }

        /// <summary>
        /// Check para seleccionar tratamiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTratamiento_checked(object sender, RoutedEventArgs e)
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
                    listaTratamientos[listaTratamientos.IndexOf(dato)].Seleccionado = false;
                    dato.Seleccionado = false;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format("{0} {1} {2} {3} {4}",
                            Properties.Resources.CorteGanado_MedicamentosContenidosEnOtroTratamiento,
                            dato.Descripcion.Trim(),
                            Properties.Resources.CorteGanado_MedicamentosContenidosEnOtroTratamiento2,
                            resultadoValidacion.Mensaje.Trim(),
                            "."),
                        MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    listaTratamientos[listaTratamientos.IndexOf(dato)].Seleccionado = true;
                    dato.Seleccionado = true;
                }
            }
        }

        /// <summary>
        /// Validar solo letras y numero de numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        /// <summary>
        /// Validar solo numero en peso corte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPesoCorte_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Validar solo decimales en temperatura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemperatura_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, txtTemperatura.Text);
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
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void CapturarAreteRFID()
        {
            if(Application.Current.Windows[ConstantesVista.WindowPrincipal].IsActive)
            {
                txtAreteMetalico.Text = rfidCapturadoGlobal;
                if (cboSexo.SelectedIndex == 0 || tipoCapturaArete == 2 || tipoCapturaArete == 1)
                {
                    ConsultarDatosPorArete();
                    ObtenerTipoCaptura(true);
                }
                else
                {
                    
                    validarNuevoArete(txtAreteMetalico.Text, true);
                    
                }
            }
                
        }

        

        private void ObtenerTipoCaptura(bool EsRFID)
        {
            bool SeEncontraronDatos = false;

            if (cboSexo.SelectedIndex > 0)
            {
                SeEncontraronDatos = true;
            }

            if (txtNoIndividual.Text.Trim().Length > 0 && txtAreteMetalico.Text.Trim().Length > 0)
            {
                tipoCapturaArete = 2;
                if (SeEncontraronDatos)
                {
                    txtNoIndividual.IsEnabled = false;
                    txtAreteMetalico.IsEnabled = false;
                }
            }
            else if (EsRFID)
            {
                tipoCapturaArete = 1;
                if (SeEncontraronDatos)
                {
                    if (txtNoIndividual.Text.Trim() != string.Empty)
                    {
                        txtNoIndividual.IsEnabled = false;
                    }
                    else
                    {
                        txtNoIndividual.Focusable = true;
                        txtNoIndividual.Focus();
                    }
                }
            }
            else
            {
                tipoCapturaArete = 0;
                if (SeEncontraronDatos)
                {
                    txtNoIndividual.IsEnabled = false;
                }
            }
        }
        //Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        void spManager_NewSerialDataRecievedTermo(object sender, SerialDataEventArgs e)
        {
            string strEnd = "";
            double val;
            try
            {
                strEnd = _spManagerTermo.ObtenerLetura(e.Data);
                if (strEnd.Trim() != string.Empty)
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);
                    
                    if (val < 10.0) return;

                    if (lastTemp == val)
                    {
                        countTemp++;
                    }
                    else
                    {
                        countTemp = 0;
                    }

                    lastTemp = val;
                    double dif = maxTemp - lastTemp;

                     if (lastTemp > maxTemp)
                    {
                        maxTemp = lastTemp;
                        // damos formato al valor peso para presentarlo
                        temperaturaCapturadaGlobal = String.Format(CultureInfo.CurrentCulture, "{0:0.0}", maxTemp).Replace(",", ".");
                    }

                    if (dif > 2.0d && tempTomada == false)
                    {
                        if (maxTemp > 36)
                        {
                            countTemp = 0;
                            Dispatcher.BeginInvoke(new Action(CapturarTemperaturaDeDisplay),
                                           DispatcherPriority.Background);
                            
                        }
                    }

                    temperaturaCapturadaGlobal = String.Format(CultureInfo.CurrentCulture, "{0:0.0}", val).Replace(",", ".");

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
        /// Metodo para capturar temperatura en display
        /// </summary>
        private void CapturarTemperaturaEnDisplay()
        {
            txtDisplayTemperatura.Text = temperaturaCapturadaGlobal;
            if (capturaInmediataTermo)
            {
                lastTemp = double.Parse(temperaturaCapturadaGlobal);
                if (corralGlobal != null && corralGlobal.TipoCorral!=null)
                {
                    CapturarTemperaturaDeDisplay();
                }
                capturaInmediataTermo = false;
            }
        }

        /// <summary>
        /// Metodo para capturar temperatura y validarla
        /// </summary>
        private void CapturarTemperaturaDeDisplay()
        {
            if (!(String.IsNullOrEmpty(txtAreteMetalico.Text) && String.IsNullOrEmpty(txtNoIndividual.Text)))
            {
                
                    
                if (capturaInmediataTermo)
                {
                    txtTemperatura.Text = txtDisplayTemperatura.Text;
                }
                else
                {
                    txtTemperatura.Text = Convert.ToString(maxTemp);
                }

                
                tempTomada = true;
                    ValidarTemperatura(maxTemp);
                    BtnLeerTemperatura.IsEnabled = true;
                
                
            }
        }

        //Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        void spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            string strEnd = "";
            double val;
            try
            {
                strEnd = _spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    pesoParcial = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(CapturaPesoEnDisplay),
                                           DispatcherPriority.Normal);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para capturar el peso en el display
        /// </summary>
        private void CapturaPesoEnDisplay()
        {
            txtDisplayPeso.Text = pesoParcial;
            if (capturaInmediata)
            {
                txtPesoCorte.Text = pesoCapturadoGlobal = pesoParcial;
                this._spManager.StopListening();
                ObtenerTratamiento();
                capturaInmediata = false;
                this.pesoTomado = true;
            }
            else
            {
                int iPesoParcial = 0;
                int iPesoGlobla = 0;
                int.TryParse(this.pesoParcial, out iPesoParcial);
                int.TryParse(this.pesoCapturadoGlobal, out iPesoGlobla);

                if (iPesoParcial > 0 && iPesoGlobla > 0)
                {
                    if (iPesoParcial == iPesoGlobla)
                    {
                        this._timerTickCount++;
                        if (this._timerTickCount == this._configBasculaCorte.Espera)
                        {
                            txtPesoCorte.Text = pesoCapturadoGlobal = pesoParcial;
                            this._timer.Stop();
                            this._spManager.StopListening();

                            this.pesoTomado = true;

                            if (cboSexo.SelectedItem == null)
                                return;

                            if (cboSexo.SelectedItem.ToString() == "Seleccione" || cboSexo.SelectedItem.ToString() == string.Empty)
                                return;

                            ObtenerTratamiento();
                        }
                        else
                        {
                            this._spManager.StopListening();
                        }
                    }
                    else
                    {
                        this._timerTickCount = 0;
                        this.pesoCapturadoGlobal = this.pesoParcial;
                        this._spManager.StopListening();
                    }
                }
                else
                {
                    this._timerTickCount = 0;
                    this.pesoCapturadoGlobal = pesoParcial;
                    this._spManager.StopListening();
                }
            }
            if (capturaInmediata) 
            {
                txtPesoCorte.Text = txtDisplayPeso.Text;
                ObtenerTratamiento();
                capturaInmediata = false;
            }
        }

        //Evento change del combosexo para obtener el valor del combo y llamar cargar combo de calidad de ganado.
        private void cboSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSexo.SelectedItem == null || cboSexo.SelectedIndex == 0) return;
            var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
            CargarComboCalidad(sexoGanado);
            Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
        }
        /// <summary>
        /// validar corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            if (txtCorralDestino.Text.Trim().Length > 0)
            {
                if (!ValidarCorralDestino())
                {
                    e.Handled = true;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_CorralDestinoNoIngresado,
                    MessageBoxButton.OK, MessageImage.Warning);
                e.Handled = true;
            }
        }

        public bool ValidarCorralDestino()
        {
            ////VALIDAR Existencia
            
            var corralPL = new CorralPL();
            
            var corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;
            if (corralDestino == null || corralDestino.CorralID == 0)
            {
                if (txtCorralDestino.Visibility == Visibility.Visible)
                {
                    corralDestino = new CorralInfo
                        {
                            Codigo = txtCorralDestino.Text
                        };
                }
                else
                {
                    return false;
                }

            }
            var resultadoCorral = corralPL.ObtenerExistenciaCorral(organizacionId, corralDestino.Codigo);
            if (resultadoCorral != null)
            {
                if (!ValidarCorralDestinoCapacidad())
                {
                    return false;
                }
                var corralPl = new CorralPL();
                var corralInfo = corralPl.ObtenerPorId(resultadoCorral.CorralID);
                if (corralInfo == null)
                {
                    return false;
                }

                corralInfoGen = corralInfo;

                if (corralGlobal.TipoCorral.TipoCorralID == TipoCorral.CorraletaRecuperado.GetHashCode())
                {
                    var corralImproductivo =
                        corralesImproductivos.FirstOrDefault(cor => cor.CorralID == corralInfo.CorralID);

                    if (corralImproductivo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.CorteTransferenciaGanado_CorralDestinoImproductivo,
                                        MessageBoxButton.OK, MessageImage.Warning);
                        txtCorralDestino.Clear();
                        return false;
                    }
                }
                else if (corralGlobal.TipoCorral.TipoCorralID == TipoCorral.Produccion.GetHashCode())
                {
                    if (corralInfo.TipoCorral.TipoCorralID == TipoCorral.Produccion.GetHashCode())
                    {
                        if (corralGlobal.Codigo == corralInfo.Codigo)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.CorteTransferenciaGanado_CorralDestinoOrigenIguales,
                                        MessageBoxButton.OK, MessageImage.Warning);
                        }
                        else
                        {
                            cboClasificacion.SelectedValue = animalActual.ClasificacionGanadoID;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.CorteTransferenciaGanado_CorralDestinoNoEsProductivo,
                                        MessageBoxButton.OK, MessageImage.Warning);
                    }
                }


            }
            else
            {
                //No existe el corral
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CosteoGanado_CorralNoExiste,
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Boton para dialogo de medicamento enfermeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedicamentoEnfermeria_Click(object sender, RoutedEventArgs e)
        {
            var corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;

            if (corralDestino == null || corralDestino.CorralID == 0)
            {
                if (txtCorralDestino.Visibility == Visibility.Visible)
                {
                    corralDestino = new CorralInfo
                    {
                        Codigo = txtCorralDestino.Text
                    };
                }
                else
                {
                    return;
                }

            }

            var medicamientoDialog = new Medicamentos(listaTratamientos,
                                                        corralDestino.Codigo,
                                                        txtNoIndividual.Text)
            {
                Left = (ActualWidth - Width) / 2,
                Top = ((ActualHeight - Height) / 2) + 132,
                Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
            };
            medicamientoDialog.ShowDialog();
        }

        /// <summary>
        /// Validar solo letras y numeros en corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        /// <summary>
        /// Obtener tratamiento con el peso corte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPesoCorte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (_pesoCorte != this.txtPesoCorte.Text)
                {


                    Dispatcher.BeginInvoke(new Action(() =>
                                                          {
                                                              if (txtPesoCorte.Text != string.Empty)
                                                              {
                                                                  Dispatcher.BeginInvoke(new Action(ObtenerTratamiento),
                                                                                         DispatcherPriority.Background, null);
                                                              }
                                                              else
                                                              {
                                                                  SkMessageBox.Show(
                                                                      Application.Current.Windows[
                                                                          ConstantesVista.WindowPrincipal],
                                                                      Properties.Resources.pesoVacio,
                                                                      MessageBoxButton.OK, MessageImage.Warning);
                                                                  dgTratamientos.ItemsSource = null;
                                                                  listaTratamientos = null;
                                                                  e.Handled = true;
                                                              }
                                                          }));
                }
            }
        }

        /// <summary>
        /// Evento Timer_Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
               try
                {
                    var timer = (DispatcherTimer)sender;
                    this._spManager.StartListening(_configBasculaCorte.Puerto,
                                    _configBasculaCorte.Baudrate,
                                    _configBasculaCorte.Paridad,
                                    _configBasculaCorte.Databits,
                                    _configBasculaCorte.BitStop);
                    capturaInmediata = false;
                }
                catch
                {
                }


            //Dispatcher.Invoke(new Action(() =>
            //{
            //    if (!String.IsNullOrEmpty(txtDisplayPeso.Text))
            //    {
            //        txtPesoCorte.Text = txtDisplayPeso.Text.Replace(".00", "").Replace(",00", "");
            //        capturaInmediata = false;
            //    }
            //    Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);

            //    if (!String.IsNullOrEmpty(txtTemperatura.Text))
            //    {
            //        var valor = double.Parse(txtTemperatura.Text);
            //        ValidarTemperatura(valor);
            //    }
            //}), null);
            //pesoTomado = true;// pasando los tres segundos el peso se marcara como tomado
            //_timerTickCount = 0;
            //BtnLeerPeso.IsEnabled = true;
            BtnLeerPeso.IsEnabled = true;
        }

        /// <summary>
        /// Comportamiento de boton cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            maxTemp = 0;
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CorteTransferenciaGanado_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                // Una vez guardado el corte se habilita para tomar elpeso de nuevo
                LimpiarCaptura(true);
                pesoTomado = false;
                tempTomada = false;
                DeshabilitarControles(false, false);
                txtNoIndividual.Focus();
            }
        }

        /// <summary>
        /// Handler del evento Keyup del texto temperatura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemperatura_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (txtTemperatura.Text.Trim().Length > 0)
                {
                    try
                    {
                        var valor = double.Parse(txtTemperatura.Text);
                        ValidarTemperatura(valor);
                    }
                    catch (FormatException)
                    {
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

        /// <summary>
        /// Evento closing de Corte de ganado 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            _tipoGanado = Convert.ToInt32(this.cboTipoGanado.SelectedValue);
            DisposeDispositivosConectados();
        }

        /// <summary>
        /// Desconecta todo disponitivo conectado para liberar los puertos
        /// </summary>
        private void DisposeDispositivosConectados(){
            try
            {
                if (basculaConectada)
                {
                    _spManager.Dispose();
                }

                if (rfidConectado)
                {
                    _spManagerRFID.Dispose();
                }

                if (termometroConectado)
                {
                    _spManagerTermo.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Guardar el corte por transferencia (click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParametroPL parametroPl = new ParametroPL();
                List<ParametroInfo> parametros = parametroPl.ObtenerTodos(EstatusEnum.Activo).ToList();
                ParametroInfo parametroDobleArete = null;
                
                if (parametros != null)
                {
                    parametroDobleArete = parametros.Where(parametro => parametro.Clave == ParametrosEnum.LECTURADOBLEARETE.ToString()).FirstOrDefault();
                }

                if (parametroDobleArete != null )
                {
                    ParametroOrganizacionPL parametroOrganizacionPL = new ParametroOrganizacionPL();
                    ParametroOrganizacionInfo parametroOrganizacion = parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(int.Parse(Application.Current.Properties["OrganizacionID"].ToString()), ParametrosEnum.LECTURADOBLEARETE.ToString());
                    if (parametroOrganizacion != null && parametroOrganizacion.Activo == EstatusEnum.Activo && parametroOrganizacion.Valor.ToUpper() == "TRUE")
                    {
                        if (txtAreteMetalico.Text.Trim() == String.Empty)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CorteTransferenciaGanado_MsgCampoRFIDVacio, MessageBoxButton.OK, MessageImage.Stop);
                            txtAreteMetalico.Focus();
                            return;
                        }

                        if (txtNoIndividual.Text.Trim() == String.Empty)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CorteTransferenciaGanado_MsgCampoNoIndividualVacio, MessageBoxButton.OK, MessageImage.Stop);
                            txtNoIndividual.Focus();
                            return;
                        }

                        if (txtAreteMetalico.Text.Trim() == txtNoIndividual.Text.Trim())
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CorteTransferenciaGanado_MsgCampoNoIndividualCampoRFIDIguales, MessageBoxButton.OK, MessageImage.Stop);
                            return;
                        }

                    }

                }

                var usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                var animalInfo = new AnimalInfo();
                var animalMovimientoInfo = new AnimalMovimientoInfo();
                var resultadoValidacion = CompruebaCamposEnBlanco();
                var corteTransferenciaPl = new CorteTransferenciaGanadoPL();
                var guardarAnimalCorte = new CorteTransferenciaGanadoGuardarInfo();
                string Arete = string.Empty;
                bool EsRFID = false;
                bool validarExistenciaArete = true;

                switch(tipoCapturaArete)
                {
                    case 0:
                        Arete = txtAreteMetalico.Text.Trim();
                        EsRFID = true;
                        break;
                    case 1:
                        Arete = txtNoIndividual.Text.Trim();
                        break;
                    case 2: validarExistenciaArete = false;
                        break;
                }

                if (resultadoValidacion.Resultado)
                {
                    if (ValidarCorralDestino())
                    {
                        if (!VerificarExisteciaArete(validarExistenciaArete, Arete, EsRFID, organizacionId))
                        {
                            resultadoValidacion = ComprobarExistenciaTratamientos();
                            if (resultadoValidacion.Resultado)
                            {
                                // Se almacena el animal
                                animalInfo = CargarDatosGuardarAnimal(usuarioId);

                                guardarAnimalCorte.AnimalCorteTransferenciaInfo = animalInfo;

                                //Se manda a guardar el registro en base de dato

                                if (animalInfo != null)
                                {
                                    var corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;
                                    if (corralDestino == null || corralDestino.CorralID == 0)
                                    {
                                        if (txtCorralDestino.Visibility == Visibility.Visible)
                                        {
                                            var corralPL = new CorralPL();
                                            corralDestino = new CorralInfo
                                            {
                                                Codigo = txtCorralDestino.Text,
                                                Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = organizacionId
                                                    }
                                            };
                                            corralDestino = corralPL.ObtenerPorCodicoOrganizacionCorral(corralDestino);
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    //Se almacena el movimiento
                                    animalMovimientoInfo = CargarDatosGuardarAnimalMovimiento(animalInfo, usuarioId);
                                    guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo = animalMovimientoInfo;

                                    guardarAnimalCorte.CorralInfoGen = corralInfoGen;

                                    guardarAnimalCorte.AnimalMovimientoOrigenInfo = loteCorralOrigen;

                                    guardarAnimalCorte.BanderaEliminarAnimalSalida = banderaDeleteAnimalSalida;

                                    guardarAnimalCorte.LoteDestinoInfo = loteCorralDestino;

                                    guardarAnimalCorte.AnimalSalidaGuardarInfo = animalSalidaInfo;

                                    guardarAnimalCorte.TxtCorralDestino = corralDestino.Codigo;
                                    guardarAnimalCorte.CorralDestinoID = corralDestino.CorralID;

                                    guardarAnimalCorte.AnimalActualInfo = animalActual;

                                    guardarAnimalCorte.OrganizacionId = organizacionId;

                                    guardarAnimalCorte.AlmacenID = almacenInfo.AlmacenID;

                                    guardarAnimalCorte.UsuarioCreacionID = usuario;

                                    var resultadoGuardarCorte =
                                        corteTransferenciaPl.GuardarCorteTransferencia(guardarAnimalCorte, listaTratamientos);


                                    if (animalMovimientoInfo != null &&
                                        guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.AnimalMovimientoID > 0 &&
                                        resultadoGuardarCorte != null)
                                    {
                                        ObtenerTotalCabezas(loteCorralOrigen);
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.CorteGanado_SeguardoCorrectamenteElCorte,
                                            MessageBoxButton.OK, MessageImage.Correct);
                                        txtNoIndividual.Focus();

                                        DeshabilitarControles(false, false);
                                        LimpiarCaptura(false);
                                        pesoTomado = false;
                                        tempTomada = false;
                                        banderaDeleteAnimalSalida = false;
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.CorteTransferenciaGanado_GuardarError,
                                            MessageBoxButton.OK, MessageImage.Error);
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
                            if (EsRFID)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.CorteTransferenciaGanado_MsgNuevoRFIDYaRegistrado,
                                                MessageBoxButton.OK, MessageImage.Error);
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.CorteTransferenciaGanado_MsgNuevoAreteYaRegistrado,
                                                MessageBoxButton.OK, MessageImage.Error);
                            }
                        }
                    }
                    else
                    {
                        cboCorralDestino.Focus();
                    }
                }
                else
                {
                    var mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje)
                                      ? Properties.Resources.DatosBlancos_CorteGanado
                                      : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
                maxTemp = 0;
            }
            catch (InvalidPortNameException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ErrorGuardar_CorteGanado + ex.Message,
                        MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.ErrorGuardar_CorteGanado, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Boton Leer peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLeerPeso_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info();
                if (basculaConectada)
                {
                    capturaInmediata = true;
                    _spManager.StopListening();
                    _spManager.StartListening(_configBasculaCorte.Puerto,
                            _configBasculaCorte.Baudrate,
                            _configBasculaCorte.Paridad,
                            _configBasculaCorte.Databits,
                            _configBasculaCorte.BitStop);
                }
                else
                {
                    if (_spManager != null)
                    {
                        BtnLeerPeso.IsEnabled = false;
                        _spManager.Dispose();
                        _spManager.StartListening(_configBasculaCorte.Puerto,
                                    _configBasculaCorte.Baudrate,
                                    _configBasculaCorte.Paridad,
                                    _configBasculaCorte.Databits,
                                    _configBasculaCorte.BitStop);
                        _spManager.StopListening();
                        _spManager.StartListening(_configBasculaCorte.Puerto,
                                _configBasculaCorte.Baudrate,
                                _configBasculaCorte.Paridad,
                                _configBasculaCorte.Databits,
                                _configBasculaCorte.BitStop);
                        BtnLeerPeso.IsEnabled = true;
                        basculaConectada = true;
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Error(error);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                   Properties.Resources.CorteTransferenciaGanado_ErrorCapturaBascula,
                                   MessageBoxButton.OK, MessageImage.Error);
                BtnLeerPeso.IsEnabled = true;
            }

        }

        private void BtnLeerTempera_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info();
                if (termometroConectado)
                {

                    Dispatcher.BeginInvoke(new Action(CapturarTemperaturaEnDisplay),
                                       DispatcherPriority.Background);

                    capturaInmediataTermo = true;
                    //_spManagerTermo.Dispose();
                    //CapturarTemperaturaDeDisplay();
                    //_spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                    //            _configTermometroCorte.Baudrate,
                    //            _configTermometroCorte.Paridad,
                    //            _configTermometroCorte.Databits,
                    //            _configTermometroCorte.BitStop);
                    //termometroConectado = true;

                }
                else {
                    if (_spManagerTermo != null)
                    {
                        BtnLeerTemperatura.IsEnabled = false;
                        _spManagerTermo.Dispose();
                        _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                                    _configTermometroCorte.Baudrate,
                                    _configTermometroCorte.Paridad,
                                    _configTermometroCorte.Databits,
                                    _configTermometroCorte.BitStop);
                        BtnLeerTemperatura.IsEnabled = true;
                        termometroConectado = true;

                    }
                }
            }
            catch (Exception error)
            {
                Logger.Error(error);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.CorteTransferenciaGanado_ErrorCapturaTermometro,
                              MessageBoxButton.OK, MessageImage.Error);
                BtnLeerTemperatura.IsEnabled = true;
            }

        }

        #endregion

        #region Metodos
        /// <summary>
        /// Leer la configuracion de la pantalla.
        /// </summary>
        private void LeerConfiguracion()
        {
            try
            {
                var parametrosPl = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.TratamientoTemperatura,
                    OrganizacionID = organizacionId
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
                                maxTemperaturaAnimal = double.Parse(parametro.Valor, CultureInfo.InvariantCulture);
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
            if (listaTratamientos != null)
            {
                resultado = tratamientosPl.ComprobarExistenciaTratamientos(listaTratamientos,
                                                                           almacenInfo.AlmacenID);
            }

            return resultado;
        }

        /// <summary>
        /// Metodo que verifica que existan Trampas configuradas.
        /// </summary>
        /// <returns></returns>
        private bool ExistenTrampas()
        {
            var bExiste = false;
            var trampaPl = new TrampaPL();
            try
            {
                trampaInfo = new TrampaInfo
                {
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    TipoTrampa = (char)TipoTrampa.Manejo,
                    HostName = Environment.MachineName
                };
                var trampaInfoResp = trampaPl.ObtenerTrampa(trampaInfo);
                if (trampaInfoResp != null)
                {
                    trampaIDInfo = trampaInfoResp;
                    trampaID = trampaInfoResp.TrampaID;
                    bExiste = true;
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }

            return bExiste;
        }

        /// <summary>
        /// Llena el combo de sexo de ganado, obtiene el sexo del ganado de un Enum dentro de la aplicacion.
        /// </summary>
        private void CargarCboSexo()
        {
            IList<Sexo> sexoEnums = Enum.GetValues(typeof(Sexo)).Cast<Sexo>().ToList();
            var listaSexo = new Dictionary<char, string>();

            var i = 0;
            listaSexo.Add('S', "Seleccione");
            foreach (var VarSexo in sexoEnums)
            {
                if (i == 0)
                {
                    listaSexo.Add('H', VarSexo.ToString());
                }
                if (i == 1)
                {
                    listaSexo.Add('M', VarSexo.ToString());
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
            var listaPalete = new Dictionary<int, string>();

            listaPalete.Add(-1, "Seleccione");
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
        /// //Carga el combo calidad de ganado de acuerdo al sexo.
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
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorConsultaCalidad_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga el combo tipo ganado.
        /// </summary>
        private void CargarComboTipoGanado()
        {
            try
            {
                var tipoGanadoPL = new TipoGanadoPL();
                var tipoGanado = tipoGanadoPL.ObtenerTodos();
                if (tipoGanado == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.NoRegistroCalidad_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    var seleccione = new TipoGanadoInfo();
                    seleccione.Descripcion = "Seleccione";
                    seleccione.TipoGanadoID = 0;
                    tipoGanado.Insert(0, seleccione);
                    cboTipoGanado.ItemsSource = tipoGanado;
                    cboTipoGanado.SelectedValue = 0;
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorConsultaCalidad_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
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
                    var causarechazo = new CausaRechazoInfo {CausaRechazoID = 0, Descripcion = "Seleccione"};
                    causas.Insert(0, causarechazo);
                    cboCausaRechazo.ItemsSource = causas;
                    cboCausaRechazo.SelectedValue = 0;
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorCausaRechazo_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para llenar el combo de implantador.
        /// </summary>
        private void LlenarComboImplantador()
        {
            try
            {
                var implantador = new OperadorPL();
                var implantadores = implantador.ObtenerPorIDRol(organizacionId, OperadorEmum.Implantador.GetHashCode());
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
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorImplantadores_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Metodo para llenar el combo de la clasificación.
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
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.ErrorConsultaClasificaciones_CorteGanado,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que verifica si existe el arete en inventario y llena los campos de consulta.
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        private bool ExisteAreteEnInventario(AnimalInfo animalInfo)
        {
            var existe = false;
            var paletas = false;
            AnimalInfo resultadoBusqueda = new AnimalInfo();
            try
            {
                var corteGanadoPl = new CorteGanadoPL();
                if(animalInfo.Arete.Trim() != string.Empty)
                {
                    resultadoBusqueda = corteGanadoPl.ExisteAreteEnPartida(animalInfo);
                }
                else
                {
                    resultadoBusqueda = corteGanadoPl.ExisteAreteMetalicoEnPartida(animalInfo);
                }

                if (resultadoBusqueda != null)
                {
                    corralGlobal = null;
                    corralIDGlobal = resultadoBusqueda.CorralID;
                    var corralPL = new CorralPL();
                    corralGlobal = corralPL.ObtenerPorId(resultadoBusqueda.CorralID);

                    if (!resultadoBusqueda.Activo)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CorteGanado_AnimalMuerto,
                            MessageBoxButton.OK, MessageImage.Warning);
                        return false;
                    }
                    animalActual = resultadoBusqueda;
                    //Datos corral ganado
                    var tipoGanadoPl = new TipoGanadoPL();
                    var tipoGanado = tipoGanadoPl.ObtenerPorID(resultadoBusqueda.TipoGanadoID);
                    cboSexo.SelectedItem = tipoGanado.Sexo.ToString();
                    cboCalidad.SelectedValue = resultadoBusqueda.CalidadGanadoID;
                    cboTipoGanado.SelectedValue = tipoGanado.TipoGanadoID.ToString();
                    if (corralGlobal == null || corralGlobal.TipoCorral.TipoCorralID == TipoCorral.CorraletaRecuperado.GetHashCode())
                    {
                        cboClasificacion.SelectedValue = (animalInfo.ClasificacionGanadoID == (int)ClasificacionGanado.Normal) ? (int)ClasificacionGanado.Improductivo : cboClasificacion.SelectedValue = animalInfo.ClasificacionGanadoID;
                    }
                    else if (corralGlobal.TipoCorral.TipoCorralID == TipoCorral.CorraletaRecuperadosPartida.GetHashCode())
                    {
                        cboClasificacion.SelectedValue = (animalInfo.ClasificacionGanadoID == (int)ClasificacionGanado.Normal) ? (int)ClasificacionGanado.RecuperadoPartida : animalInfo.ClasificacionGanadoID;
                    }
                    else
                    {
                        cboClasificacion.SelectedValue = animalInfo.ClasificacionGanadoID;
                    }

                    txtCorralOrigen.Text = resultadoBusqueda.CodigoCorral;
                    txtAreteMetalico.Text = resultadoBusqueda.AreteMetalico;
                    txtNoIndividual.Text = resultadoBusqueda.Arete;

                    //Otros datos
                    //Validar paletas
                    if (resultadoBusqueda.Paletas > 0)
                    {
                        cboPaletas.SelectedValue = resultadoBusqueda.Paletas.ToString();
                    }
                    else
                    {
                        paletas = true;
                    }

                    //Datos Corte
                    dtpFechaCorte.Text = DateTime.Now.ToShortDateString();

                    //Datos Generales
                    LlenarDatosGenerales(resultadoBusqueda);

                    //ObtenerCabezas
                    HabilitarControles(true, paletas);
                    existe = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_NoIndividualNoExiste,
                    MessageBoxButton.OK, MessageImage.Warning);
                    txtNoIndividual.Text = "";
                    txtNoIndividual.Focus();
                    LimpiarCaptura(false);
                    return false;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorExisteNoIndividual,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorExisteNoIndividual,
                                  MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            return existe;
        }

        /// <summary>
        /// Obtiene el valor o el key del combo de sexo en base a la variable isKey.
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
        /// Deshabilita los controles.
        /// </summary>
        /// <param name="habilitar"></param>
        /// <param name="capturaManualDeshabilitada"></param>
        private void DeshabilitarControles(bool habilitar, bool capturaManualDeshabilitada)
        {
            if (capturaManualDeshabilitada)
            {
                txtNoIndividual.IsEnabled = true;
                txtAreteMetalico.IsEnabled = true;
            }
            if (banderaPermisoTranpa)
            {
                txtNoIndividual.IsEnabled = false;
                txtAreteMetalico.IsEnabled = false;
            }
            cboSexo.IsEnabled = habilitar;
            cboCalidad.IsEnabled = habilitar;
            cboCausaRechazo.IsEnabled = habilitar;
            txtPesoCorte.IsEnabled = habilitar;
            cboTipoGanado.IsEnabled = habilitar;
            cboClasificacion.IsEnabled = habilitar;
            txtCorralOrigen.IsEnabled = habilitar;
            cboCorralDestino.IsEnabled = habilitar;
            txtTemperatura.IsEnabled = habilitar;
            cboPaletas.IsEnabled = habilitar;
            cboImplantador.IsEnabled = habilitar;
            btnMedicamentoEnfermeria.IsEnabled = habilitar;
            txtObservaciones.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
            dtpFechaCorte.IsEnabled = habilitar;
            dtpFechaRecepcion.IsEnabled = habilitar;
            txtOrigen.IsEnabled = habilitar;
            txtProveedor.IsEnabled = habilitar;
            txtNoPartida.IsEnabled = habilitar;
            txtObservaciones.IsEnabled = habilitar;
            imgTratamientoInicial.Visibility = Visibility.Hidden;
            lblTratamientoInicial.Visibility = Visibility.Hidden;
            txtNoIndividual.Focus();
        }

        /// <summary>
        /// Habilita los controles.
        /// </summary>
        /// <param name="habilitar"></param>
        /// <param name="paletas"></param>
        private void HabilitarControles(bool habilitar, bool paletas)
        {
            cboCausaRechazo.IsEnabled = habilitar;
            cboCorralDestino.IsEnabled = habilitar;
            if (!basculaConectada)
            {
                txtPesoCorte.IsEnabled = habilitar;
            }
            if (!termometroConectado)
            {
                txtTemperatura.IsEnabled = habilitar;
            }
            cboPaletas.IsEnabled = paletas;
            cboImplantador.IsEnabled = habilitar;
            btnMedicamentoEnfermeria.IsEnabled = habilitar;
            txtObservaciones.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
        }

        /// <summary>
        /// Metodo que valida que corral destino sea improductivo, tenga capacidad y no este cerrado.
        /// </summary>
        /// <returns></returns>
        private bool ValidarCorralDestinoCapacidad()
        {
            //TODO revisar este codigo
            bool corralValido = true;
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;
            if (corralDestino == null || corralDestino.CorralID == 0)
            {
                if (txtCorralDestino.Visibility == Visibility.Visible)
                {
                    corralDestino = new CorralInfo
                        {
                            Organizacion = new OrganizacionInfo
                                {
                                    OrganizacionID = organizacionID
                                },
                            Codigo = txtCorralDestino.Text
                        };
                }
                else
                {
                    return false;
                }
            }

            var corralPL = new CorralPL();
            var resultadoCorral = corralPL.ObtenerPorCodicoOrganizacionCorral(corralDestino);
            if (resultadoCorral != null)
            {
                var lotePL = new LotePL();
                if(loteCorralDestino == null)
                {
                    loteCorralDestino = new LoteInfo();
                }
                loteCorralDestino.OrganizacionID = organizacionId;
                loteCorralDestino.CorralID = resultadoCorral.CorralID;
                loteCorralDestino = lotePL.ObtenerPorCorralID(loteCorralDestino);
                if (loteCorralDestino != null)
                {
                    if (!(loteCorralDestino.Cabezas < resultadoCorral.Capacidad))
                    {
                        //Corral no tiene capacidad
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_NoCapacidadCorral,
                        MessageBoxButton.OK, MessageImage.Warning);
                        corralValido = false;
                    }
                    if (loteCorralDestino.FechaCierre != DateTime.MinValue && loteCorralDestino.FechaCierre != new DateTime(1900,1,1))
                    {
                        //Corral destino cerrado
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_CorralDestinoCerrado,
                        MessageBoxButton.OK, MessageImage.Warning);
                        corralValido = false;
                    }
                }
               
            }
            else
            {
                //Corral no es improductivo
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CorteTransferenciaGanado_CorralNoEsImproductivo,
                MessageBoxButton.OK, MessageImage.Warning);
                corralValido = false;
            }
            return corralValido;
        }

        /// <summary>
        /// Metodo para obtener el tratamiento de acuerdo al sexo y peso.
        /// </summary>
        private void ObtenerTratamiento()
        {
            if (txtPesoCorte.Text.Trim().Length > 0 && cboSexo.SelectedItem != null && cboSexo.SelectedIndex != 0)
            {
                try
                {
                    var tratamientoPl = new TratamientoPL();
                    var tratamientoInfo = new TratamientoInfo
                        {
                            OrganizacionId = organizacionId,
                            Sexo = (Sexo)Enum.Parse(typeof(Sexo),
                                                     cboSexo.SelectedItem.ToString()),
                            Peso = int.Parse(txtPesoCorte.Text)
                        };

                    // este es un bit si es 1 es metafilaxia

                    var metafilaxia = Metafilaxia.EsMetafilaxia;

                    listaTratamientos = tratamientoPl.ObtenerTipoTratamientosCorte(tratamientoInfo, metafilaxia);

                    if (listaTratamientos != null && listaTratamientos.Any())
                    {
                        listaTratamientos.ToList().ForEach(tra =>
                                                           {
                                                               if (tra.TipoTratamiento !=
                                                                   TipoTratamiento.Arete.GetHashCode())
                                                               {
                                                                   tra.Habilitado = false;
                                                                   tra.Seleccionado = false;
                                                               }
                                                           });
                    }


                    if (listaTratamientos == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.ExisteTratamiento_CorteGanado,
                                          MessageBoxButton.OK, MessageImage.Warning);
                        dgTratamientos.ItemsSource = null;
                        listaTratamientos = null;
                    }
                    else
                    {
                        ValidarTratamientoInicial(listaTratamientos);
                        if (txtCorralDestino.Visibility == Visibility.Hidden)
                        {
                            CargarCorralDestino();
                        }
                        //Verificar
                        if (string.IsNullOrEmpty(txtTemperatura.Text)) return;
                        var valor = double.Parse(txtTemperatura.Text);
                        ValidarTemperatura(valor);

                        //
                    }
                }
                catch (FormatException)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_PesoCorteInvalido,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    txtPesoCorte.Clear();
                    dgTratamientos.ItemsSource = null;
                    listaTratamientos = null;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                     Properties.Resources.CorteGanado_ErrorEnPeso,
                                     MessageBoxButton.OK, MessageImage.Warning);
                }
            }
        }

        /// <summary>
        /// Metodo para determinar el Corral destino del Animal
        /// </summary>
        private void CargarCorralDestino()
        {
            var corralesDestino = new List<CorralInfo>();
            var corralRangoPL = new CorralRangoPL();
            var corralPL = new CorralPL();
            var lotePL = new LotePL();
            if (corralGlobal.TipoCorral.TipoCorralID == TipoCorral.CorraletaRecuperadosPartida.GetHashCode())
            {

                int pesoAnimal = Convert.ToInt32(txtPesoCorte.Text);


                IList<CorralRangoInfo> corralesConfigurados =
                    corralRangoPL.ObtenerCorralesConfiguradosPorOrganizacionID(organizacionId);

                if (corralesConfigurados == null || !corralesConfigurados.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CorteTransferenciaGanado_NoCorralesDestino,
                  MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                string sexoSeleccionado = cboSexo.SelectedItem.ToString();

                List<CorralRangoInfo> corralesEnRango =
                    corralesConfigurados.Where(cor => pesoAnimal >= cor.RangoInicial && pesoAnimal <= cor.RangoFinal
                        && cor.SexoDescripcion.Trim().ToUpper().Equals(sexoSeleccionado.Trim().ToUpper(), StringComparison.InvariantCultureIgnoreCase)).ToList();

                if (!corralesEnRango.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CorteTransferenciaGanado_NoCorralesDestino,
                MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                corralesEnRango = corralesEnRango.OrderBy(cor => cor.CorralID).ToList();
                for (int i = 0; i < corralesEnRango.Count; i++)
                {
                    CorralInfo corral = corralPL.ObtenerPorId(corralesEnRango[i].CorralID);
                    if (corral == null)
                    {
                        continue;
                    }
                    var lote = new LoteInfo
                    {
                        CorralID = corral.CorralID,
                        OrganizacionID = organizacionId
                    };
                    lote = lotePL.ObtenerPorCorralID(lote);
                    if (lote == null)
                    {
                        if (i == corralesEnRango.Count - 1)
                        {
                            corralesDestino.Add(corral);
                            break;
                        }
                    }
                    else
                    {
                        if (lote.Cabezas + 1 <= corral.Capacidad && lote.FechaCierre == new DateTime(1900, 1, 1))
                        {
                            corralesDestino.Add(corral);
                            break;
                        }
                    }
                    if (!corralesDestino.Any())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_NoCorralesDestino,
                    MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                }

            }
            else
            {
                corralesImproductivos =
                    corralPL.ObtenerCorralesImproductivos(TipoCorral.Improductivos.GetHashCode());

                if (corralesImproductivos == null || !corralesImproductivos.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CorteTransferenciaGanado_NoCorralesImproductivos,
                  MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                cboCorralDestino.Visibility = Visibility.Hidden;
                txtCorralDestino.Visibility = Visibility.Visible;
            }
            if (corralesDestino.Any())
            {
                cboCorralDestino.ItemsSource = corralesDestino;
                cboCorralDestino.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Metodo para validar si ya se le aplico tratamiento inicial al animal.
        /// </summary>
        /// <param name="_listaTratamientos"></param>
        private void ValidarTratamientoInicial(IList<TratamientoInfo> _listaTratamientos)
        {
            var animalInfo = new AnimalInfo
            {
                Arete = txtNoIndividual.Text,
                OrganizacionIDEntrada = organizacionId
            };

            var corteTransferenciaGanadoPl = new CorteTransferenciaGanadoPL();
            var tratamientosAplicados = corteTransferenciaGanadoPl.ObtenerTratamientosAplicados(animalInfo, (int)TipoMovimiento.Corte);
            if (_listaTratamientos != null)
            {
                if (tratamientosAplicados != null)
                {
                    imgTratamientoInicial.Visibility = Visibility.Visible;
                    lblTratamientoInicial.Visibility = Visibility.Visible;
                    //foreach (var tratamientosApli in tratamientosAplicados)
                    //{
                    //    foreach (var tratamientos in _listaTratamientos)
                    //    {
                    //        if (tratamientos.CodigoTratamiento == tratamientosApli.CodigoTratamiento)
                    //        {
                    //            tratamientos.Seleccionado = true;

                    //            var tratamientoArete =
                    //                listaTratamientosArete.FirstOrDefault(tra => tra == tratamientos.CodigoTratamiento);
                    //            if (tratamientoArete != 0)
                    //            {
                    //                tratamientos.Habilitado = true;
                    //            }
                    //            else
                    //            {
                    //                tratamientos.Habilitado = false;
                    //            }
                    //        }
                    //    }
                    //}
                }
                else
                {
                    foreach (var tratamientos in _listaTratamientos)
                    {
                        if (tratamientos.TipoTratamiento == (int) TipoTratamiento.Corte &&
                            corralGlobal.GrupoCorral != GrupoCorralEnum.Produccion.GetHashCode())
                        {
                            if (tratamientos.TipoTratamiento == (int) TipoTratamiento.Corte &&
                                tratamientos.Productos.Count <= 1)
                            {
                                continue;
                            }
                            tratamientos.Seleccionado = true;
                            tratamientos.Habilitado = true;
                        }
                    }
                }
                if (txtCorralDestino.Visibility == Visibility.Visible && corralGlobal.TipoCorral.TipoCorralID == TipoCorral.Produccion.GetHashCode())
                {
                    if (listaTratamientosProduccion != null)
                    {
                        _listaTratamientos =
                            _listaTratamientos.Where(tra => listaTratamientosProduccion.Contains(tra.CodigoTratamiento))
                                .ToList();
                    }
                }
                _listaTratamientos =
                            _listaTratamientos.Where(tra => tra.Habilitado || tra.Seleccionado)
                                .ToList();

                dgTratamientos.ItemsSource = _listaTratamientos;
                if (cboImplantador.Items.Count > 1)
                {
                    cboImplantador.SelectedIndex = 1;
                }
                else
                {
                    cboImplantador.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Inicializar termometro y bascula.
        /// </summary>
        private void InicializarDispositivos()
        {
            try
            {
                if (this.basculaConectada)
                {
                    this.BtnLeerPeso.IsEnabled = true;
                    if (!this.pesoTomado)
                    {
                        this.pesoCapturadoGlobal = string.Empty;
                        this.pesoParcial = string.Empty;
                        this._timerTickCount = 0;
                        _timer = new DispatcherTimer();
                        _timer.Interval = new TimeSpan(0, 0, 1);
                        _timer.Tick += (Timer_Tick);
                        _timer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            try
            {
                if (!tempTomada)
                {
                    if (_spManagerTermo != null)
                        _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                            _configTermometroCorte.Baudrate,
                            _configTermometroCorte.Paridad,
                            _configTermometroCorte.Databits,
                            _configTermometroCorte.BitStop);
                    countTemp = 0;
                }
            }
            catch(Exception ex)
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
        }

        /// <summary>
        /// Inicializacion de los valores de usuario, lectura de configuracion y Handler de eventos.
        /// </summary>
        private void UserInitialization()
        {
            try
            {
                var parametrosPl = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoBascula,
                    OrganizacionID = organizacionId
                };
                var parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaIDInfo.TrampaID);
                _configBasculaCorte = obtenerParametroDispositivo(parametros);
                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoTermometro,
                    OrganizacionID = organizacionId
                };
                parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaIDInfo.TrampaID);
                _configTermometroCorte = obtenerParametroDispositivo(parametros);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoRFID,
                    OrganizacionID = organizacionId
                };
                parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaIDInfo.TrampaID);
                _configRFIDCorte = obtenerParametroDispositivo(parametros);

                _spManager = new SerialPortManager();
                _spManagerTermo = new SerialPortManager();
                _spManagerRFID = new SerialPortManager();

                _spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);
                _spManagerTermo.NewSerialDataRecieved += (spManager_NewSerialDataRecievedTermo);
                _spManagerRFID.NewSerialDataRecieved += (spManager_NewSerialDataRecievedRFID);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.LectorCodigoBarra,
                    OrganizacionID = organizacionId
                };
                IList<ConfiguracionParametrosInfo> parametrosScanner = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaIDInfo.TrampaID);

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
        /// Configuracion de parametros de dispositivos.
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private BasculaCorteSection obtenerParametroDispositivo(IList<ConfiguracionParametrosInfo> parametros)
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
        /// Valida si el nuevo arete esta registrado para otro animal
        /// </summary>
        /// <param name="validarExistenciaArete">verdadero si es necesario realizar la validacion del arete</param>
        /// <param name="Arete">Arete a comprobar existencia</param>
        /// <param name="EsRFID">Indica si se trata de un arete RFID</param>
        /// <param name="Organizacion">Oranizacion entrada</param>
        /// <returns></returns>
        private Boolean VerificarExisteciaArete(bool validarExistenciaArete, string Arete, bool EsRFID, int Organizacion)
        {
            Boolean Encontrado = false;
            AnimalPL animalPL = new AnimalPL();

            if (validarExistenciaArete)
            {
                try
                {
                    if (EsRFID)
                    {
                        Encontrado = animalPL.VerificarExistenciaArete(string.Empty, Arete, Organizacion);
                    }
                    else
                    {
                        Encontrado = animalPL.VerificarExistenciaArete(Arete, string.Empty, Organizacion);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    Encontrado = false;
                }
            }

            return Encontrado;
        }
        /// <summary>
        /// Metodo que valida la temperatura del animal y marcar el tratamiento 26 de acuerdo a la regla de negocio.
        /// </summary>
        /// <param name="temperatura"></param>
        private void ValidarTemperatura(double temperatura)
        {
            if (corralGlobal == null) 
            {
                return;
            }
            if(corralGlobal.TipoCorral==null)
            {
                return;
            }
            if (corralGlobal.TipoCorral.TipoCorralID != TipoCorral.Produccion.GetHashCode())
            {
                if (temperatura >= maxTemperaturaAnimal && listaTratamientos != null)
                {
                    foreach (var item in listaTratamientos)
                    {
                        if (item.CodigoTratamiento == _codigoTratamientoTemperatura)
                        {
                            item.Seleccionado = true;
                            dgTratamientos.ItemsSource = null;

                            dgTratamientos.ItemsSource = listaTratamientos.Where(tra=> tra.Habilitado || tra.Seleccionado);
                            break;
                        }
                    }
                }
                else
                {
                    if (listaTratamientos != null)
                    {
                        foreach (var item in listaTratamientos)
                        {
                            if (item.CodigoTratamiento == _codigoTratamientoTemperatura)
                            {
                                item.Seleccionado = false;
                                dgTratamientos.ItemsSource = null;
                                dgTratamientos.ItemsSource = listaTratamientos.Where(tra => tra.Habilitado || tra.Seleccionado);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Obtener el numero de cabezas de corral de recuperacion de enfermeria
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        private void ObtenerTotalCabezas(AnimalMovimientoInfo animalInfo)
        {
            var objObtenerCabezas = new CorteTransferenciaGanadoPL();
            var resultado = objObtenerCabezas.ObtenerTotales(animalInfo);
            if (resultado == null) return;
            lblTotalCabezas.Content = resultado.Total;
            lblTotalCortados.Content = resultado.TotalCortadas;
            lblTotalPorCortar.Content = resultado.TotalPorCortar;
            if (resultado.TotalCortadas != resultado.Total) return;
            banderaDeleteAnimalSalida = true;
            corralIDGlobal = 0;
        }

        /// <summary>
        /// Limpiar campos  de captura al cancelar.
        /// </summary>
        /// <param name="bCancelar"></param>
        private void LimpiarCaptura(bool bCancelar)
        {

            txtAreteMetalico.Clear();
            txtPesoCorte.Clear();
            txtCorralDestino.Text = "";
            txtCorralOrigen.Text = "";
            if (rfidConectado)
            {
                txtAreteMetalico.IsEnabled = false;
            }
            else
            {
                txtAreteMetalico.IsEnabled = _configRFIDCorte.CapturaManual;
            }
            txtNoIndividual.IsEnabled = true;

            if (bCancelar)
            {
                txtCorralOrigen.Clear();
                lblTotalCabezas.Content = 0;
                lblTotalCortados.Content = 0;
                lblTotalPorCortar.Content = 0;
                if (_timer != null) 
                {
                    _timer.Stop(); 
                }
                BtnLeerPeso.IsEnabled = false;

            }
            else
            {
                txtNoIndividual.Focus();
            }
            this.pesoTomado = false;
            cboImplantador.SelectedValue = 0;
            cboCorralDestino.ItemsSource = null;
            txtTemperatura.Clear();
            imgTratamientoInicial.Visibility = Visibility.Hidden;
            lblTratamientoInicial.Visibility = Visibility.Hidden;
            txtCorralDestino.Visibility = Visibility.Hidden;
            cboCorralDestino.Visibility = Visibility.Visible;
            dgTratamientos.ItemsSource = null;
            listaTratamientos = null; //se limpia lista de tratamientos

            loteCorralDestino = new LoteInfo();
            loteCorralOrigen = new AnimalMovimientoInfo();
            animalSalidaInfo = new AnimalSalidaInfo();
            txtOrigen.Clear();
            txtProveedor.Clear();
            txtNoPartida.Clear();
            txtObservaciones.Clear();
            dtpFechaCorte.Text = string.Empty;
            dtpFechaRecepcion.Text = string.Empty;
            lblTemperaturaDisplay.Content = 0;
            pesoCapturadoGlobal = string.Empty;
            temperaturaCapturadaGlobal = string.Empty;
            txtDisplayPeso.Text = string.Empty;
            txtDisplayTemperatura.Text = string.Empty;

            //Limpiar combos
            cboSexo.SelectedIndex = 0;
            cboCalidad.SelectedIndex = 0;
            cboCausaRechazo.SelectedIndex = 0;
            cboTipoGanado.SelectedIndex = 0;
            cboClasificacion.SelectedIndex = 0;
            cboPaletas.SelectedIndex = 0;
            cboCorralDestino.SelectedIndex = 0;
            txtCorralDestino.Clear();

            btnGuardar.IsEnabled = false;

            if (termometroConectado)
            {
                _spManagerTermo.Dispose();
                _spManagerTermo.StartListening(_configTermometroCorte.Puerto,
                                            _configTermometroCorte.Baudrate,
                                            _configTermometroCorte.Paridad,
                                            _configTermometroCorte.Databits,
                                            _configTermometroCorte.BitStop);
            }
            if (rfidConectado)
            {
                _spManagerRFID.Dispose();
                _spManagerRFID.StartListening(_configRFIDCorte.Puerto,
                                            _configRFIDCorte.Baudrate,
                                            _configRFIDCorte.Paridad,
                                            _configRFIDCorte.Databits,
                                            _configRFIDCorte.BitStop);
            }

            if (bandBack)
            {
                txtNoIndividual.Clear();
            }
        }

        /// <summary>
        ///  Metodo para verificar que exista almacen para las Trampas configuradas.
        /// </summary>
        /// <returns></returns>
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
                    OrganizacionID = organizacionId
                };
                var parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametroClave(
                                parametroSolicitado,
                                trampaIDInfo.TrampaID
                            );

                if (parametros != null && parametros.Count > 0)
                {
                    var almacenPl = new AlmacenPL();
                    almacenInfo = almacenPl.ObtenerPorID(int.Parse(parametros[0].Valor));
                    if (almacenInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format("{0} {1}{2}",
                               Properties.Resources.CorteGanado_NoValidoAlmacenIDTrampas1,
                               trampaInfo.HostName,
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
                        var existeAjustesPendientes = almacenPl.ExistenAjustesPendientesParaAlmacen(
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
                            existe = true;
                    }

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format("{0} {1}{2}",
                                Properties.Resources.CorteGanado_NoExisteAlmacenIDTrampas1,
                                trampaInfo.HostName,
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
        ///  Se guardan los movimientos del animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        private AnimalMovimientoInfo CargarDatosGuardarAnimalMovimiento(AnimalInfo animalInfo, int usuarioId)
        {

            var animalMovimientoInfo = new AnimalMovimientoInfo();


            //Se cargan los datos para el Movimiento
            animalMovimientoInfo.AnimalID = animalInfo.AnimalID;
            animalMovimientoInfo.OrganizacionID = organizacionId;

            animalMovimientoInfo.FechaMovimiento = DateTime.Parse(dtpFechaCorte.Text);
            if (txtPesoCorte.Text != null)
            {
                animalMovimientoInfo.Peso = int.Parse(txtPesoCorte.Text);
            }
            if (txtTemperatura.Text != "")
            {
                animalMovimientoInfo.Temperatura = double.Parse(txtTemperatura.Text.Replace(',', '.'),
                    CultureInfo.InvariantCulture);
            }
            animalMovimientoInfo.TipoMovimientoID = (int)TipoMovimiento.CortePorTransferencia;
            animalMovimientoInfo.TrampaID = trampaID;
            var operador = (OperadorInfo)cboImplantador.SelectedItem;

            if (operador != null)
            {
                animalMovimientoInfo.OperadorID = operador.OperadorID;
            }
            animalMovimientoInfo.Observaciones = txtObservaciones.Text;
            animalMovimientoInfo.Activo = EstatusEnum.Activo;
            animalMovimientoInfo.UsuarioCreacionID = usuarioId;


            return animalMovimientoInfo;
        }


        /// <summary>
        ///  Obtiene los Datos de Corral
        /// </summary>
        /// <param name="corralId"></param>
        /// <returns></returns>
        private CorralInfo ObtenerCorral(int corralId)
        {
            CorralInfo corral;
            try
            {
                var corralPl = new CorralPL();
                corral = corralPl.ObtenerPorId(corralId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return corral;
        }

        /// <summary>
        ///  Guarda la informacion del animal
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        private AnimalInfo CargarDatosGuardarAnimal(int usuarioId)
        {

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

            animalInfo.OrganizacionIDEntrada = organizacionId;
            animalInfo.FolioEntrada = int.Parse(txtNoPartida.Text);
            var paletas = cboPaletas.SelectedIndex;

            if (paletas > 0)
            {
                animalInfo.Paletas = paletas - 1;
            }

            var causeRechaso = (CausaRechazoInfo)cboCausaRechazo.SelectedItem;
            animalInfo.CausaRechadoID = causeRechaso.CausaRechazoID;


            animalInfo.Cronico = false;
            animalInfo.Activo = true;
            animalInfo.UsuarioCreacionID = usuarioId;

            return animalInfo;
        }

        /// <summary>
        /// Metodo de validacion de capturas en blanco
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion CompruebaCamposEnBlanco()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(txtNoIndividual.Text))
            {
                txtNoIndividual.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereNoIndividual;
                return resultado;
            }
            
            if (String.IsNullOrEmpty(txtAreteMetalico.Text))
            {
                txtAreteMetalico.Text = string.Empty;
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
            var corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;
            if (txtCorralDestino.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(txtCorralDestino.Text))
                {
                    txtCorralDestino.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CorteGanado_RequiereCorralDestino;
                    return resultado;
                }
                corralDestino = new CorralInfo
                    {
                        Codigo = txtCorralDestino.Text
                    };
            }
            if (corralDestino == null || string.IsNullOrWhiteSpace(corralDestino.Codigo))
            {
                cboCorralDestino.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteGanado_RequiereCorralDestino;
                return resultado;
            }
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

            if (listaTratamientos == null || listaTratamientos.Count == 0)
            {
                txtPesoCorte.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CorteTransferenciaGanado_MsgFaltanTratamientos;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }


        /// <summary>
        /// Metodo para validar si el arete tiene salida por recuperacion
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        private bool ValidarAnimalCorraletaRecuperacion(AnimalInfo animalInfo)
        {
            var animalPL = new AnimalPL();
            var corralPL = new CorralPL();
            AnimalMovimientoInfo ultimoMovimientoAnimal = new AnimalMovimientoInfo();

            if (string.IsNullOrWhiteSpace(animalInfo.Arete))
            {
                animalInfo = animalPL.ObtenerAnimalPorAreteTestigo(animalInfo.AreteMetalico,
                                                                   animalInfo.OrganizacionIDEntrada);
            }
            else
            {
                animalInfo = animalPL.ObtenerAnimalPorArete(animalInfo.Arete, animalInfo.OrganizacionIDEntrada);
            }

            if (animalInfo != null)
            {
                ultimoMovimientoAnimal = animalPL.ObtenerUltimoMovimientoAnimal(animalInfo);
            }

            if (ultimoMovimientoAnimal == null)
            {
                return false;
            }
                        CorralInfo corral = corralPL.ObtenerPorId(ultimoMovimientoAnimal.CorralID);

            if (corral == null)
            {
                return false;
            }

            if (corral.GrupoCorral == GrupoCorralEnum.Produccion.GetHashCode())
            {
                cboCorralDestino.Visibility = Visibility.Hidden;
                txtCorralDestino.Visibility = Visibility.Visible;
                return true;
            }

            if (corral.GrupoCorral != GrupoCorralEnum.Corraleta.GetHashCode())
            {
                return false;
            }
            //Validar que el Lote en el que se encuentra el animal este activo
            var lotePL = new LotePL();
            LoteInfo loteAnimal = lotePL.ObtenerPorId(ultimoMovimientoAnimal.LoteID);
            if (loteAnimal == null || loteAnimal.Activo == EstatusEnum.Inactivo)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metodo llenar datos generales
        /// </summary>
        /// <param name="animalInfo"></param>
        private void LlenarDatosGenerales(AnimalInfo animalInfo)
        {
            var enfermeriaPL = new EnfermeriaPL();
            var datosCompra = enfermeriaPL.ObtenerDatosCompra(animalInfo.FolioEntrada, organizacionId);
            if (datosCompra != null)
            {
                txtProveedor.Text = datosCompra.Proveedor;
                dtpFechaRecepcion.SelectedDate = datosCompra.FechaInicio;
                txtOrigen.Text = datosCompra.Origen;
            }
            txtNoPartida.Text = animalInfo.FolioEntrada.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Resultado de la validacion de tratamientos
        /// </summary>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        private ResultadoValidacion ExistenProductosEnTratamientosSeleccionados(TratamientoInfo tratamiento)
        {
            var resultadoValidacion = new ResultadoValidacion();
            //Se obtiene la lista de productos de los tratamientos seleccionados
            IList<TratamientoInfo> listaTratamientosChecado = listaTratamientos.Where(
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
            return resultadoValidacion;
        }

        /// <summary>
        /// Metodo para almacenar los Tratamientos y descontar del almacen
        /// </summary>
        private void GuardarConsumoDeTratamientos(AnimalMovimientoInfo animalMovimientoInfo)
        {
            var almacenpl = new AlmacenPL();

            var almacenMovimientoInfo = new AlmacenMovimientoInfo
            {
                AlmacenID = almacenInfo.AlmacenID,
                AnimalMovimientoID = animalMovimientoInfo.AnimalMovimientoID,
                TipoMovimientoID = (int)TipoMovimiento.SalidaPorConsumo,
                Status = (int)EstatusInventario.Aplicado,
                Observaciones = "",
                UsuarioCreacionID = usuario,
                AnimalID = animalMovimientoInfo.AnimalID,
                //Verificar
                CostoID = (int)Costo.MedicamentoDeImplante,
            };
            almacenpl.GuardarDescontarTratamientos(
                listaTratamientos.Where(item => item.Seleccionado && item.Habilitado).ToList(),
                almacenMovimientoInfo
                );
        }

        #endregion
        private string _pesoCorte;
        private void txtPesoCorte_GotFocus(object sender, RoutedEventArgs e)
        {
            _pesoCorte = this.txtPesoCorte.Text;
        }
    }
}