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
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Manejo;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para EntradaGanadoEnfermeria.xaml
    /// </summary>
    public partial class EntradaGanadoEnfermeria
    {
        #region Atributos

        InterfaceSalidaAnimalInfo interfaceSalidoAnimalInfo;
        private int organizacionId;
        private int usuario;
        private SerialPortManager spManager;
        private SerialPortManager spManagerTermo;
        private BasculaCorteSection configBascula;
        private BasculaCorteSection configTermometro;
        private SerialPortManager _spManagerRFID;
        private BasculaCorteSection _configRFIDCorte;
        private bool termometroConectado;
        private bool basculaConectada;
        private bool rfidConectado;

        private string pesoParcial;
        private int timerTickCount;
        private bool pesoTomado;
        private bool capturaInmediata = false;
        private string pesoCapturadoGlobal = "";

        private bool tempTomada;
        private DispatcherTimer timer;
        int countTemp;
        TrampaInfo trampaID;
     
        double lastTemp, maxTemp = 0.0d;
        private double maxTemperaturaAnimal;
        IList<TratamientoInfo> listaTratamientos;
        private int codigoTratamientoTemperatura;
        private bool bandBack;
        private bool ctrlPegar;
        private String TextoAnterior;
        private bool partidaCapturada;
        private AnimalDeteccionInfo AnimalDetectado;
        private AnimalDeteccionInfo _animalDetectado;
        private EnfermeriaInfo enfermeriaSeleccionada;
        private AnimalInfo animalActual;
        private InterfaceSalidaAnimalInfo animalInterface;
        private LoteInfo loteDestino;
        private LoteInfo loteOrigen;
        private DatosCompra infoDatosCopmra;
        private CorralInfo corralDestino;
        private CorralInfo corralOrigen;
        private List<ProblemaInfo> listaProblemas;
        DiagnosticoAnalista busquedaPartidas;
        private IList<HistorialClinicoInfo> historialClinico;
        AlmacenInfo almacenInfo;
        private bool esNuevo;
        private bool bandFoco;
        private bool CtrlPegar;
        private int idProblemaCrb;
        private string temperaturaCapturadaGlobal = "";
        
        private string rfidCapturadoGlobal = "";
        private int codigoAreteBlanco = 0;
        private bool cambiarTipoGanado = false;
        private bool animalRecaido = false;
        

        private string justificacion = string.Empty;

        private uint start = 0;
        private uint stop = 0;
        private bool aplicaScanner;

        private int tipoCapturaArete = 3;
        private string _pesoGanado;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EntradaGanadoEnfermeria()
        {

            InitializeComponent();
            _pesoGanado = string.Empty;
            ctrlPegar = false;
            bandFoco = false;
            bandBack = true;
            CtrlPegar = false;
            TextoAnterior = string.Empty;
            loteDestino = null;
            infoDatosCopmra = null;
            corralDestino = null;
            corralOrigen = null;
            historialClinico = null;
            almacenInfo = null;
            esNuevo = false;
            loteOrigen = null;
            idProblemaCrb = -1;
            listaProblemas = new List<ProblemaInfo>();
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            partidaCapturada = false;
            DesactivarControlesFijos();
            
            //
            ConfiguracionParametrosPL parametrosPL = new ConfiguracionParametrosPL();
            if (!ExistenTrampas())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_ValidacionTrampa,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                DeshabilitarControles(false);
                return;
            }
            //se valida que existan un almacen para la trampa configurada
            if (!ExistenAlmacenParaTrampa())
            {
                DeshabilitarControles(false);
                return;
            }
            UserInitialization();

            //Probar el Funcionamiento de los dispositivos
            //ProbarFuncionamientoDispositivos();

            Inicializarcontroles(false);

            //Leer configuracion de arete blanco
            LeerConfiguracionAreteBlanco();

            ColocarFocoPrimerControl();
        }

        /// <summary>
        /// Inicializa el lector RFID cuando el formulario esta listo para captura
        /// </summary>
        private void InicializarLectorRFID()
        {
            try
            {
                if (_spManagerRFID != null)
                    _spManagerRFID.StartListening(_configRFIDCorte.Puerto,
                        _configRFIDCorte.Baudrate,
                        _configRFIDCorte.Paridad,
                        _configRFIDCorte.Databits,
                        _configRFIDCorte.BitStop);

                rfidConectado = true;
                txtAreteTestigo.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarLectorRFID,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
        }

        /// <summary>
        /// Inicializa la lectura de la bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                if (this.basculaConectada)
                {
                    this.btnLeerPeso.IsEnabled = true;
                    if (!this.pesoTomado)
                    {
                        this.pesoCapturadoGlobal = string.Empty;
                        this.pesoParcial = string.Empty;
                        this.timerTickCount = 0;
                        if (this.timer == null)
                        {
                            this.timer = new DispatcherTimer();
                            timer.Interval = new TimeSpan(0, 0, 1);
                            timer.Tick += (Timer_Tick);
                        }
                        timer.Start();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void IniciarTermometro()
        {
            //Se prueba el funcionamiento de los display del termometro
            tempTomada = false;
            maxTemp = 0.0d;
            try
            {
                if (spManagerTermo != null)
                {
                    if (!termometroConectado)
                    {
                        spManagerTermo.StartListening(configTermometro.Puerto,
                            configTermometro.Baudrate,
                            configTermometro.Paridad,
                            configTermometro.Databits,
                            configTermometro.BitStop);

                        termometroConectado = true;
                        txtTemperatura.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                termometroConectado = false;
                txtTemperatura.IsEnabled = true;
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
                if (spManager != null)
                {
                    spManager.StartListening(configBascula.Puerto,
                        configBascula.Baudrate,
                        configBascula.Paridad,
                        configBascula.Databits,
                        configBascula.BitStop);
                    basculaConectada = true;
                    txtPeso.IsEnabled = false;
                    btnLeerPeso.IsEnabled = true;
                    spManager.StopListening();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarBascula,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
            }

            try
            {
                if (spManagerTermo != null)
                {
                    if (termometroConectado)
                    {
                        spManagerTermo.StopListening();
                        termometroConectado = false;
                    }
                    else
                    {
                        spManagerTermo.StartListening(configTermometro.Puerto,
                            configTermometro.Baudrate,
                            configTermometro.Paridad,
                            configTermometro.Databits,
                            configTermometro.BitStop);

                        spManagerTermo.StopListening();
                        termometroConectado = false;
                        txtTemperatura.IsEnabled = false;
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

            IniciarTermometro();

            AnimalDetectado = null;
            enfermeriaSeleccionada = null;
            animalActual = null;
            animalInterface = null;
            busquedaPartidas = new DiagnosticoAnalista();

            //Si la bascula o el termometro estan desconectados se verifica si se tiene permiso para capturar manualmente
            if (termometroConectado == false)
            {
                if (configTermometro == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManual,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                    DeshabilitarControles(false);
                }
                else
                {
                    if (!configTermometro.CapturaManual)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManual,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                        DeshabilitarControles(false);
                    }
                }
            }

            if (basculaConectada == false)
            {
                basculaConectada = true;
                if (configBascula == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManual,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                    DeshabilitarControles(false);
                }
                else
                {

                    if (!configBascula.CapturaManual)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManual,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                        DeshabilitarControles(false);
                    }
                }
            }
            //Se prueba el funcionamiento de los display del RFID
            try
            {
                if (_spManagerRFID != null)
                {
                    InicializarLectorRFID();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    ex.Message,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorInicializarLectorRFID,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
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
                        txtAreteTestigo.IsEnabled = true;
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
        }

        /// <summary>
        /// Evento clising de 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
                DisposeDispositivosConectados();

            if (termometroConectado)
            {
                if(spManagerTermo != null)
                spManagerTermo.Dispose();
            }
        }

        /// <summary>
        /// Metodo para aplicar un Dispose a los dispositivos conectados en caso de cerrar la ventana
        /// </summary>
        private void DisposeDispositivosConectados()
        {
            if (basculaConectada)
            {
                if(spManager != null)
                    spManager.Dispose();
                else
                    basculaConectada = false;
                basculaConectada = false;
            }

            if (rfidConectado)
            {
                if (_spManagerRFID != null)
                    _spManagerRFID.Dispose();
            }
        }

        /// <summary>
        /// Desactiva todos los controles que no se necesitan
        /// </summary>
        private void DesactivarControlesFijos()
        {
            txtCorralOrigen.IsEnabled = false;
            txtDiasAlta.IsEnabled = false;
            txtProblemaDetectado.IsEnabled = false;
            cboGradoEnfermedad.IsEnabled = false;
            txtGananciaDiaria.IsEnabled = false;
            cboDetector.IsEnabled = false;
        }

        /// <summary>
        /// Validar si es transferencia para asignar un corral de color en los tratamientos
        /// </summary>
        private void ValidarSiEsTransferenciaAsignarAreteBlanco(bool activar)
        {
            if (codigoAreteBlanco > 0 && listaTratamientos != null)
            {
                foreach (var listaTratamiento in
                    from listaTratamiento in listaTratamientos
                    where listaTratamiento.Productos != null
                    from producto in listaTratamiento.Productos.Where(
                        producto => producto.ProductoId == codigoAreteBlanco)
                    select listaTratamiento)
                {
                    listaTratamiento.Seleccionado = activar;
                }
            }
        }


        private void ValidarTratamientos()
        {
            List<int> productos = ObtenerProductosTratamientos();
            listaTratamientos.ToList().ForEach(pds => pds.Productos.ToList().ForEach(d =>
                                                                                         {
                                                                                             bool existe =
                                                                                                 productos.
                                                                                                     Any(
                                                                                                         id =>
                                                                                                         id ==
                                                                                                         d.
                                                                                                             ProductoId);
                                                                                             if (!existe)
                                                                                             {
                                                                                                 pds.Habilitado =
                                                                                                     true;
                                                                                                 pds.Seleccionado =
                                                                                                     true;
                                                                                             }
                                                                                         }));
        }

        private List<int> ObtenerProductosTratamientos()
        {
            var parametroGeneralPL = new ParametroGeneralPL();
            ParametroGeneralInfo parametroGeneral =
                parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.ProductoDiasTratamiento.ToString());

            List<int> productoTratamientos = null;
            if (parametroGeneral != null)
            {
                List<string> productos = parametroGeneral.Valor.Split('|').ToList();
                if (productos != null && productos.Any())
                {
                    productoTratamientos = productos.Select(p => Convert.ToInt32(p)).ToList();
                }
            }
            return productoTratamientos;
        }


        /// <summary>
        /// Leer la configuracion de la pantalla
        /// </summary>
        private void LeerConfiguracionAreteBlanco()
        {
            try
            {
                var parametrosPl = new ConfiguracionParametrosPL();
                /* Obtener Configuracion de Arete Color Reimplante*/
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.CodigoAreteBlancoEntradaEnfermeria,
                    OrganizacionID = organizacionId
                };
                var parametro = parametrosPl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado).FirstOrDefault();
                if (parametro != null)
                    codigoAreteBlanco = int.Parse(parametro.Valor, CultureInfo.InvariantCulture);
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
        /// Leer la configuracion de la pantalla
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
                                codigoTratamientoTemperatura = int.Parse(parametro.Valor, CultureInfo.InvariantCulture);
                                break;
                            case ParametrosTratamientoTemperatura.temperaturaAnimal:
                                maxTemperaturaAnimal = double.Parse(parametro.Valor, CultureInfo.InvariantCulture);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ConvertirConfiguracion, MessageBoxButton.OK, MessageImage.Error);
                DeshabilitarControles(false);
            }
        }
        /// <summary>
        /// Evento cargar pantalla y llena los combos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (this.txtNoIndividual.Text != string.Empty) {
                this.TextoAnterior = this.txtNoIndividual.Text;
            }

            if (cboSexo.SelectedIndex <= 0)
            {
                CargarCboSexo();
            }

            if (cboClasificacion.SelectedIndex <= 0)
            {
                LlenarComboClasificacion();
            }

            if (cboCorralDestino.SelectedIndex <= 0)
            {
                LlenarComboCorralDestino();
            }
           

          
            imgBuscar.Focus();
            txtProveedor.IsEnabled = false;
            txtOrigen.IsEnabled = false;
            LeerConfiguracion();

            if (this.TextoAnterior != string.Empty && !partidaCapturada)
            {
                this.txtNoIndividual.Text = this.TextoAnterior;
                esNuevo = false;
                if (ValidarAreteCapturado())
                {
                    txtNoIndividual.IsEnabled = String.IsNullOrEmpty(txtNoIndividual.Text);
                }
            }
            else if (partidaCapturada) {
                LimpiarCaptura(true);
                imgBuscar.IsEnabled = true;
                txtTemperatura.Clear();
                pesoTomado = false;
                tempTomada = false;
               
            }

        

        }
        /// <summary>
        /// Inicializacion de los valores de usuario, lectura de configuracion y Handler de eventos
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
                var parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaID.TrampaID);
                configBascula = ObtenerParametroDispositivo(parametros);
                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoTermometro,
                    OrganizacionID = organizacionId
                };
                parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaID.TrampaID);
                configTermometro = ObtenerParametroDispositivo(parametros);

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.DispositivoRFID,
                    OrganizacionID = organizacionId
                };
                parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaID.TrampaID);
                _configRFIDCorte = ObtenerParametroDispositivo(parametros);

                if (configBascula != null && configTermometro != null && _configRFIDCorte != null)
                {
                    spManager = new SerialPortManager();
                    spManagerTermo = new SerialPortManager();
                    _spManagerRFID = new SerialPortManager();

                    spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);
                    spManagerTermo.NewSerialDataRecieved += (spManager_NewSerialDataRecievedTermo);
                    _spManagerRFID.NewSerialDataRecieved += (spManager_NewSerialDataRecievedRFID);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_ValidacionParametros,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    DeshabilitarControles(false);
                }

                parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.LectorCodigoBarra,
                    OrganizacionID = organizacionId
                };
                IList<ConfiguracionParametrosInfo> parametrosScanner = parametrosPl.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, trampaID.TrampaID);

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
        /// Obtiene los parametros de los dispositivos
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

        #endregion

        #region Eventos

        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();

        /// <summary>
        /// Evento clic de la imagen de lupa de la busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgBuscar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var busquedaPartidas = new EntradaGanadoEnfermeriaBusqueda();
                busquedaPartidas.InicializaPaginador();

                busquedaPartidas.Left = (ActualWidth - busquedaPartidas.Width) / 2;
                busquedaPartidas.Top = ((ActualHeight - busquedaPartidas.Height) / 2) + 132;
                busquedaPartidas.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                busquedaPartidas.ShowDialog();

                if (busquedaPartidas.Seleccionado)
                {
                    if (busquedaPartidas.AnimalEnfermeria != null)
                    {
                        e.Handled = true;
                        LimpiarCaptura(true);
                        AnimalDetectado = busquedaPartidas.AnimalEnfermeria;
                        this._animalDetectado = busquedaPartidas.AnimalEnfermeria;
                        enfermeriaSeleccionada = busquedaPartidas.Corral;
                        ObtenerDatosDePartidaSeleccionada(enfermeriaSeleccionada);
                        txtNoIndividual.IsEnabled = true;
                        ColocarFocoPrimerControl();
                        partidaCapturada = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_ErrorAlBuscarPartidas, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento de cambio de seleccion del combo del sexo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSexo.SelectedItem == null || cboSexo.SelectedIndex == 0) return;
            var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);
            CargarComboCalidad(sexoGanado);
            ObtenerTipoGanado();
            Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
        }
        /// <summary>
        /// Evento tick para la captura del peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
           try
            {
                try
                {
                    var timer = (DispatcherTimer)sender;
                    this.spManager.StartListening(this.configBascula.Puerto,
                                    this.configBascula.Baudrate,
                                    this.configBascula.Paridad,
                                    this.configBascula.Databits,
                                    this.configBascula.BitStop);
                }
                catch
                {
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
        void spManager_NewSerialDataRecievedRFID(object sender, SerialDataEventArgs e)
        {
            try
            {
                string strEnd = _spManagerRFID.ObtenerLeturaRFID(e.Data);

                if (strEnd.Trim() == "") return;

                rfidCapturadoGlobal = strEnd;
                //Aquie es para que se este reflejando la lectura en el display
                Dispatcher.BeginInvoke(new Action(CapturarAreteRFID),
                    DispatcherPriority.Background);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void CapturarAreteRFID()
        {
            if (cboSexo.SelectedIndex == 0)
            {
                if (Application.Current.Windows[ConstantesVista.WindowPrincipal].IsActive)
                {
                    txtAreteTestigo.Text = rfidCapturadoGlobal;
                    if (ValidarAreteCapturado())
                    {
                        txtAreteTestigo.IsEnabled = String.IsNullOrEmpty(txtAreteTestigo.Text);
                    }
                }
            }
            else
            {
                if (Application.Current.Windows[ConstantesVista.WindowPrincipal].IsActive)
                {
                    txtAreteTestigo.Text = rfidCapturadoGlobal;
                    if (VerificarExisteciaArete(true,rfidCapturadoGlobal,true,organizacionId))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.EntradaGanadoEnfermeria_MsgNuevoRFIDYaRegistrado,
                                            MessageBoxButton.OK, MessageImage.Error);
                        txtAreteTestigo.Clear();
                    }
                }
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
                string strEnd = spManagerTermo.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    if (val < 10.0) return;

                    if (lastTemp == val)
                    {
                        countTemp++;
                    }
                    else
                    {
                        countTemp = 0; //si no son iguales establecemos el contador en 0 para iniciar con el conteo de stop
                    }

                    lastTemp = val;
                    double dif = maxTemp - lastTemp;
                    //asignamos la temperatura maxima
                    if (lastTemp > maxTemp)
                    {
                        maxTemp = lastTemp;
                        // damos formato al valor peso para presentarlo
                        temperaturaCapturadaGlobal = String.Format(CultureInfo.CurrentCulture, "{0:0.0}", maxTemp).Replace(",", ".");
                    }

                    //Modificado por: Andres Vejar: Se modifica para que obtenga la temperatura mas alta leida desde el termometro, cuando inicia a descender libera la lectura
                    //Solicitado por: Rosario RolastTempmero, Isabel Ramirez
                    //if (countTemp > configTermometro.Espera && lastTemp > 0.0d)
                    //TODO: REvisar configuracion de temperatura base para termometro: Jesus Garcia
                    if (dif > 2.0d && tempTomada == false)
                    {
                        countTemp = 0;
                        if (maxTemp > 36)
                        {
                            Dispatcher.BeginInvoke(new Action(CapturarTemperaturaDeDisplay),
                                                DispatcherPriority.Background);

                            // spManagerTermo.StopListening();
                            tempTomada = true;
                            
                        }
                        
                    }
                    temperaturaCapturadaGlobal = String.Format(CultureInfo.CurrentCulture, "{0:0.0}", val).Replace(",", ".");
                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(CapturarTemperaturaEnDisplay),
                                           DispatcherPriority.Background);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para capturar temperatura y validarla
        /// </summary>
        private void CapturarTemperaturaDeDisplay()
        {
            txtTemperatura.Text = maxTemp.ToString();
            temperaturaCapturadaGlobal = string.Empty;
            txtDisplayTemperatura.Text = string.Empty;
            ValidarTemperatura(maxTemp);
            //AsignarDatosGridTratamientos();
        }

        /// <summary>
        /// Metodo para capturar temperatura en display
        /// </summary>
        private void CapturarTemperaturaEnDisplay()
        {
            txtDisplayTemperatura.Text = temperaturaCapturadaGlobal;
        }

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
                string strEnd = spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    this.pesoParcial = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

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
            txtDisplayPeso.Text = this.pesoParcial;
            if (capturaInmediata)
            {
                this.pesoCapturadoGlobal = this.pesoParcial;
                this.pesoTomado = true;
                this.txtPeso.IsEnabled = false;
                txtPeso.Text = txtDisplayPeso.Text = this.pesoParcial;
                Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                Dispatcher.BeginInvoke(new Action(ObtenerTipoGanado), DispatcherPriority.Background, null);
                Dispatcher.BeginInvoke(new Action(CalcularGanancia), DispatcherPriority.Background, null);

                if (!String.IsNullOrEmpty(txtTemperatura.Text))
                {
                    var valor = double.Parse(txtTemperatura.Text);
                    ValidarTemperatura(valor);
                    AsignarDatosGridTratamientos();
                }

                capturaInmediata = false;
                this.spManager.StopListening();
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
                        this.timerTickCount++;
                        if (this.timerTickCount == this.configBascula.Espera){
                            this.txtPeso.IsEnabled = false;
                            this.txtPeso.Text = pesoCapturadoGlobal = this.pesoParcial;
                            this.timer.Stop();
                            this.spManager.StopListening();

                            this.pesoTomado = true;

                            if (cboSexo.SelectedItem == null)
                                return;

                            if (cboSexo.SelectedItem.ToString() == "Seleccione" || cboSexo.SelectedItem.ToString() == string.Empty)
                                return;

                            this.pesoTomado = true;

                            Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                            Dispatcher.BeginInvoke(new Action(ObtenerTipoGanado), DispatcherPriority.Background, null);
                            Dispatcher.BeginInvoke(new Action(CalcularGanancia), DispatcherPriority.Background, null);

                            if (!String.IsNullOrEmpty(txtTemperatura.Text))
                            {
                                var valor = double.Parse(txtTemperatura.Text);
                                ValidarTemperatura(valor);
                                AsignarDatosGridTratamientos();
                            }
                        }
                        else
                            this.spManager.StopListening();
                    }
                    else
                    {
                        this.timerTickCount = 0;
                        this.pesoCapturadoGlobal = this.pesoParcial;
                        this.spManager.StopListening();

                    }
                }
                else
                {
                    this.timerTickCount = 0;
                    this.pesoCapturadoGlobal = pesoParcial;
                    this.spManager.StopListening();
                }
            }
            
            if (capturaInmediata)
            {
                txtPeso.Text = txtDisplayPeso.Text;
                Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                Dispatcher.BeginInvoke(new Action(ObtenerTipoGanado), DispatcherPriority.Background, null);
                capturaInmediata = false;
            }
            
        }

        /// <summary>
        /// Evento previewTextInput del campo corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCorralDestino_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        /// <summary>
        /// Evento keyDown del campo numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    e.Handled = true;
                    if (cboSexo.SelectedIndex == 0)
                    {
                        if (ValidarAreteCapturado())
                        {
                            txtNoIndividual.IsEnabled = String.IsNullOrEmpty(txtNoIndividual.Text);
                           
                        }
                    }
                    else
                    {
                        if (VerificarExisteciaArete(true, txtNoIndividual.Text, false, organizacionId))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.EntradaGanadoEnfermeria_MsgNuevoAreteYaRegistrado,
                                                MessageBoxButton.OK, MessageImage.Error);
                            txtNoIndividual.Clear();
                            txtNoIndividual.Focus();
                        }
                        else
                        {
                            ColocarFocoPrimerControl();
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(txtNoIndividual.Text))
                    {
                        txtNoIndividual.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloAlIngresarNumeroIndividual, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Metodo para validar el arete capturado
        /// </summary>
        /// <param name="e"></param>
        private bool ValidarAreteCapturado()
        {
            bool res=false;
            if (txtNoIndividual.Text.Trim().Length > 0 || txtAreteTestigo.Text.Trim().Length > 0 )
            {
                if (esNuevo)
                {
                    var animalInfo = new AnimalInfo
                    {
                        Arete =
                            txtNoIndividual.Text.
                            Trim(),
                            AreteMetalico = txtAreteTestigo.Text,
                        OrganizacionIDEntrada = organizacionId
                    };
                    
                    if (corralOrigen != null &&
                        corralOrigen.TipoCorral != null)
                    {
                        if (corralOrigen.TipoCorral.GrupoCorralID ==
                            (int)GrupoCorralEnum.Recepcion)
                        {
                            if (
                                !ValidarAreteEnCorralDeRecepcion(
                                    animalInfo))
                            {
                                res = true;
                            }
                        }
                        else if (
                            corralOrigen.TipoCorral.GrupoCorralID ==
                            (int)GrupoCorralEnum.Produccion ||
                            corralOrigen.TipoCorral.GrupoCorralID ==
                            (int)GrupoCorralEnum.Corraleta ||
                            (corralOrigen.TipoCorral.GrupoCorralID ==
                                (int)GrupoCorralEnum.Enfermeria &&
                                corralOrigen.TipoCorral.TipoCorralID !=
                                (int)TipoCorral.CronicoVentaMuerte))
                        {
                            if (
                                !ValidarAreteEnCorralDeProduccion(
                                    animalInfo))
                            {
                                res = true;
                            }
                        }
                        else
                        {
                            // El número individual no se encuentra en detección o enfermería. Favor de verificar.
                            SkMessageBox.Show(
                                Application.Current.Windows[
                                    ConstantesVista.
                                        WindowPrincipal],
                                Properties.Resources.
                                    EntradaGanadoEnfermeria_AreteNoSeEncuentraDeteccionEnfermeria,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                            txtNoIndividual.Clear();
                            txtNoIndividual.Focus();
                            res = true;
                        }
                    }
                }
                else
                {
                    AnimalDetectado = null;
                    if (BuscarAnimal(false))
                    {
                        res = true;
                        if (txtNoIndividual.Text.Trim().Length > 0 && txtAreteTestigo.Text.Trim().Length > 0)
                        {
                            tipoCapturaArete = 2;
                        }
                        else if (txtAreteTestigo.Text.Trim().Length > 0)
                        {
                            tipoCapturaArete = 1;
                        }
                        else
                        {
                            tipoCapturaArete = 0;
                        }
                        ColocarFocoPrimerControl();
                    }
                }
            }
            else
            {
                SkMessageBox.Show(
                    Application.Current.Windows[
                        ConstantesVista.WindowPrincipal],
                    Properties.Resources.
                        EntradaGanadoEnfermeria_NumeroIndividual,
                    MessageBoxButton.OK, MessageImage.Warning);
                txtNoIndividual.Clear();
                res = true;
                txtNoIndividual.Focusable = true;
                txtNoIndividual.Focus();
            }

            return res;
        }

        /// <summary>
        /// Metodo para validar si el aerte se encuentra en un corral de produccion
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        private bool ValidarAreteEnCorralDeProduccion(AnimalInfo animalInfo)
        {
            var resp = false;
            /* Si el corral de la deteccion es  de Produccion-Corraleta */
            if (ExisteAreteEnInventario(animalInfo))
            {
                if (AnimalDetectado.Animal != null &&
                    AnimalDetectado.EnfermeriaCorral != null &&
                    AnimalDetectado.EnfermeriaCorral.Corral != null &&
                    !string.IsNullOrEmpty(AnimalDetectado.RutaFotoDeteccion))
                {
                    if (AnimalDetectado.EnfermeriaCorral.Corral.CorralID == animalActual.CorralID)
                    {
                        if (!string.IsNullOrEmpty(AnimalDetectado.RutaFotoDeteccion))
                        {
                            AnimalDetectado.ActualizarAreteDeteccion = true;
                            AnimalDetectado.Animal.Arete = txtNoIndividual.Text.Trim();
                        }
                        BuscarAnimal(true);
                        resp = true;
                    }
                    else
                    {
                        // Si esta detectado con foto y no pertenece a la partida muestra el mensaje
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaGanadoEnfermeria_AreteNoPertenceACorral,
                            MessageBoxButton.OK, MessageImage.Warning);
                        txtNoIndividual.Clear();
                        txtNoIndividual.Focus();
                        resp = false;
                    }
                }
                else
                {
                    // Si no esta detectado con foto muestra el mensaje de que el animal ya esta en el inventario
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.EntradaGanadoEnfermeria_NumeroExiste,
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtNoIndividual.Clear();
                    txtNoIndividual.Focus();
                    resp = false;
                }
            }
            else
            {
                // El arete no existe en el inventario
                MessageBoxResult respuesta = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_NumeroNoExisteEnElInventarioNuevo,
                    MessageBoxButton.OKCancel, MessageImage.Warning);

                if (respuesta == MessageBoxResult.OK)
                {
                    if (!ValidarAreteCorrecto())
                    {
                        txtNoIndividual.Clear();
                        txtNoIndividual.Focus();
                        return false;
                    }
                    else {
                        this.InicializarBascula();
                    }
                }
                else
                {
                    txtNoIndividual.Clear();
                    txtNoIndividual.Focus();
                    resp = false;
                }
            }
            return resp;
        }

        private bool ValidarAreteCorrecto()
        {
            var parametroOrganizacionPL = new ParametroOrganizacionPL();

            ParametroOrganizacionInfo parametroOrganizacion =
                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionId,
                    ParametrosEnum.CORRALSOBRANTE.ToString());

            if (parametroOrganizacion == null || string.IsNullOrWhiteSpace(parametroOrganizacion.Valor))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_ParametroCorralSobrante,
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            var corralPL = new CorralPL();

            var corralSobrante = new CorralInfo
                                        {
                                            Codigo = parametroOrganizacion.Valor,
                                            Organizacion = new OrganizacionInfo
                                                           {
                                                               OrganizacionID = organizacionId
                                                           }
                                        };

            corralSobrante = corralPL.ObtenerPorCodicoOrganizacionCorral(corralSobrante);

            if (corralSobrante == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.EntradaGanadoEnfermeria_CorralSobranteInvalido,parametroOrganizacion.Valor),
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            //Validamos q no pertenezca a otra partida en la informacion de centros
            var interfaceSalidaAnimalPL = new InterfaceSalidaAnimalPL();
            if (!string.IsNullOrEmpty(txtNoIndividual.Text))
            {
                animalInterface = interfaceSalidaAnimalPL.ObtenerNumeroAreteIndividualPartidaActiva(txtNoIndividual.Text, organizacionId);
            }

            if (animalInterface != null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.EntradaGanadoEnfermeria_AreteNoPerteneceAPartidaPerteneceA, animalInterface.Partida),
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            var animalPL = new AnimalPL();
            AnimalInfo animalSobrante = animalPL.ObtenerAnimalAntiguoCorral(corralSobrante.CorralID);

            if (animalSobrante == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.EntradaGanadoEnfermeria_AnimalCorralSobrante, corralSobrante.Codigo),
                    MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            var lotePL = new LotePL();
            LoteInfo loteOrigenSobrante = lotePL.ObtenerPorCorral(organizacionId, corralSobrante.CorralID);
            if (corralDestino != null)
            {
                var loteDestinoSobrante = new LoteInfo
                                          {
                                              CorralID = corralDestino.CorralID,
                                              OrganizacionID = organizacionId
                                          };
                loteDestinoSobrante = lotePL.ObtenerPorCorralID(loteDestinoSobrante);
                loteDestino = loteDestinoSobrante;
            }
            animalSobrante.AplicaBitacora = true;
            animalActual = animalSobrante;
            corralOrigen = corralSobrante;
            loteOrigen = loteOrigenSobrante;
            AnimalDetectado.Animal = animalSobrante;
            txtCorralOrigen.Text = corralOrigen.Codigo;
            AnimalDetectado.ActualizarAreteDeteccion = true;
            return true;
        }

        /// <summary>
        /// Metodo para realizar las validaciones si el arete pertence aun corral de recepcion
        /// </summary>
        private bool ValidarAreteEnCorralDeRecepcion(AnimalInfo animalInfo)
        {
            /* Si el corral de la deteccion es  de recepcion */
            var entradaGanadoCalidadPL = new EntradaGanadoPL();

            var entradaGanadoInfo =
                entradaGanadoCalidadPL.ObtenerEntradaPorLote(loteOrigen);
            if (entradaGanadoInfo != null &&
                entradaGanadoInfo.TipoOrganizacionOrigenId == (int)TipoOrganizacion.CompraDirecta)
            {
                //si es una compra directa
                if (ExisteAreteEnInventario(animalInfo))
                {
                    // Si no esta detectado con foto muestra el mensaje de que el animal ya esta en el inventario
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.EntradaGanadoEnfermeria_NumeroExiste,
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtNoIndividual.Clear();
                    txtNoIndividual.Focus();
                    return false;
                }

                //Validamos q no pertenezca a otra partida en la informacion de centros
                var iPl = new InterfaceSalidaAnimalPL();
                if (!string.IsNullOrEmpty(animalInfo.Arete))
                {
                    animalInterface = iPl.ObtenerNumeroAreteIndividual(animalInfo.Arete,
                        animalInfo.OrganizacionIDEntrada);
                }
                else
                {
                    animalInterface = iPl.ObtenerNumeroAreteMetalico(animalInfo.AreteMetalico,
                        animalInfo.OrganizacionIDEntrada);
                }
                if (animalInterface != null)
                {
                    // Validar que el arete pertenezca a la partida de corral detectado
                    if (animalInterface.Partida != enfermeriaSeleccionada.FolioEntrada)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            string.Format("{0} {1}",
                                Properties.Resources.CorteGanado_ErrorAreteNoPerteneceAPartidaPerteneceA,
                                animalInterface.Partida),
                            MessageBoxButton.OK, MessageImage.Warning);

                        txtNoIndividual.Text = String.Empty;
                        txtNoIndividual.Focus();
                        return false;
                    }
                }


                InicializarDispositivos();
                if (!string.IsNullOrEmpty(AnimalDetectado.RutaFotoDeteccion))
                {
                    AnimalDetectado.ActualizarAreteDeteccion = true;
                    AnimalDetectado.Animal.Arete = txtNoIndividual.Text.Trim();
                }

                ColocarFocoPrimerControl();

                //Si el animal no se encuentra en el inventario se da de alta
                if (!rfidConectado)
                {
                    txtAreteTestigo.IsEnabled = String.IsNullOrEmpty(txtAreteTestigo.Text);
                    txtAreteTestigo.Focus();
                    return true;
                }
            }
            else
            {
                //Si es de centros Buscamos la informacion del animal en InterfaceSalidaAnimal
                var iPl = new InterfaceSalidaAnimalPL();
                animalInterface = iPl.ObtenerNumeroAreteIndividual(animalInfo.Arete,
                    animalInfo.OrganizacionIDEntrada);
                if (!string.IsNullOrEmpty(animalInfo.Arete))
                {
                    animalInterface = iPl.ObtenerNumeroAreteIndividual(animalInfo.Arete,
                        animalInfo.OrganizacionIDEntrada);
                }
                else
                {
                    animalInterface = iPl.ObtenerNumeroAreteMetalico(animalInfo.AreteMetalico,
                        animalInfo.OrganizacionIDEntrada);
                }
                if (animalInterface != null)
                {
                    // Validar que el arete pertenezca a la partida de corral detectado
                    if (animalInterface.Partida != enfermeriaSeleccionada.FolioEntrada)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            string.Format("{0} {1}",
                                Properties.Resources.CorteGanado_ErrorAreteNoPerteneceAPartidaPerteneceA,
                                animalInterface.Partida),
                            MessageBoxButton.OK, MessageImage.Warning);

                        txtNoIndividual.Text = String.Empty;
                        txtNoIndividual.Focus();
                        return false;
                    }

                    cboSexo.SelectedItem = animalInterface.TipoGanado.Sexo.ToString();
                    cboSexo.IsEnabled = false;

                    animalActual = new AnimalInfo
                    {
                        Arete = animalInterface.Arete,
                        FechaCompra = animalInterface.FechaCompra,
                        TipoGanado = animalInterface.TipoGanado,
                        PesoCompra = decimal.ToInt32(animalInterface.PesoCompra),
                        PesoAlCorte = decimal.ToInt32(animalInterface.PesoOrigen)
                    };

                    AsignarTipoGanado(animalActual.TipoGanado);
                    ObtenerDiasAlta();
                    if (!string.IsNullOrEmpty(AnimalDetectado.RutaFotoDeteccion))
                    {
                        AnimalDetectado.ActualizarAreteDeteccion = true;
                        AnimalDetectado.Animal.Arete = txtNoIndividual.Text.Trim();
                    }
                    cboTipoGanado.IsEnabled = false;
                    InicializarDispositivos();
                    if (!rfidConectado)
                    {
                        txtAreteTestigo.IsEnabled = String.IsNullOrEmpty(txtAreteTestigo.Text);
                        txtAreteTestigo.Focus();
                        return true;
                    }
                    ColocarFocoPrimerControl();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_ErrorAreteNoPerteneceCorral,
                    MessageBoxButton.OK, MessageImage.Warning);

                    txtNoIndividual.Text = String.Empty;
                    txtNoIndividual.Focus();
                    return false;
                }
            }
            return false;
        }

        private void ColocarFocoPrimerControl()
        {
            if (txtNoIndividual.IsEnabled && txtNoIndividual.Text.Trim().Length == 0)
            {
                txtNoIndividual.Focus();
                return;
            }
            if (txtAreteTestigo.IsEnabled && txtAreteTestigo.Text.Trim().Length == 0)
            {
                txtAreteTestigo.Focus();
                return;
            }

            if (cboSexo.IsEnabled)
            {
                cboSexo.Focus();
                return;
            }

            if (cboCalidad.IsEnabled)
            {
                cboCalidad.Focus();
                return;
            }
            if (txtPeso.IsEnabled)
            {
                txtPeso.Focus();
                return;
            }
            if (txtTemperatura.IsEnabled)
            {
                txtTemperatura.Focus();
                return;
            }
            if (txtCorralDestino.IsEnabled)
            {
                txtCorralDestino.Focus();
            }
        }
        /// <summary>
        /// Evento key up del campo numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                if (!esNuevo)
                {
                    if (txtAreteTestigo.Text.Trim().Length == 0)
                    {
                        bandBack = false;
                        LimpiarCaptura(false);
                        bandBack = true;
                        pesoTomado = false;
                        Inicializarcontroles(false);
                        imgBuscar.IsEnabled = true;
                    }
                }
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
        /// <summary>
        /// Evento OnpreviewTextInput del campo numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtNoIndividual_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Evento Cambio de texto en el campo numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
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
                        txtNoIndividual.Text = TextoAnterior;
                        ctrlPegar = false;
                        TextoAnterior = String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloAlIngresarNumeroIndividual, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento PreviewKeyDown del campo numero individual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoIndividual_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Back || e.Key == Key.Delete)
                {
                    if (!esNuevo)
                    {
                        if (txtAreteTestigo.Text.Trim().Length == 0)
                        {
                            bandBack = false;

                            LimpiarCaptura(false);
                            bandBack = false;
                        }
                    }
                }
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
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloAlIngresarNumeroIndividual, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento click del boton cancelar
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
                pesoTomado = false;
                tempTomada = false;
                this._animalDetectado = null;
                IniciarTermometro();

                Inicializarcontroles(false);
                imgBuscar.IsEnabled = true;
                txtTemperatura.Clear();

            }
        }
        /// <summary>
        /// Evento KeyDown del corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (txtCorralDestino.Text.Trim().Length > 0)
                {
                    ConsultarCorralDestino();
                    CalcularGanancia();
                }
                else
                {
                    txtCorralDestino.Text = string.Empty;
                }
            }
        }
        /// <summary>
        /// Evento checked de del checkbox de cronico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbCronico_Checked(object sender, RoutedEventArgs e)
        {
            cboCorralDestino.Visibility = Visibility.Visible;
            txtCorralDestino.Visibility = Visibility.Hidden;
            cboCorralDestino.IsEnabled = true;
        }
        /// <summary>
        /// Evento Uncheked del checkbox de cronico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbCronico_Unchecked(object sender, RoutedEventArgs e)
        {
            cboCorralDestino.Visibility = Visibility.Hidden;
            txtCorralDestino.Visibility = Visibility.Visible;
            cboCorralDestino.SelectedIndex = 0;

            // se inicializa el corral destino
            txtCorralDestino.Text = string.Empty;
            corralDestino = null;
            loteDestino = null;

            var gradoSeleccionado = (GradoInfo)cboGradoEnfermedad.SelectedItem;
            if (gradoSeleccionado != null &&
                gradoSeleccionado.GradoID == (int)GradoEnfermedadEnum.Level2)
            {
                txtCorralDestino.Text = txtCorralOrigen.Text;
                corralDestino = enfermeriaSeleccionada.Corral;
                loteDestino = ConsultarLote(corralDestino.CorralID);
                txtCorralDestino.IsEnabled = false;
            }
            else
            {
                txtCorralDestino.IsEnabled = true;
                txtCorralDestino.Focus();
            }
        }
        /// <summary>
        /// Evento checked del diagnostico analista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbDiagnosticoAnalista_Checked(object sender, RoutedEventArgs e)
        {
            btnDiagnostico.IsEnabled = true;
        }
        /// <summary>
        /// Evento unchecked del diagnostico analista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbDiagnosticoAnalista_Unchecked(object sender, RoutedEventArgs e)
        {
            btnDiagnostico.IsEnabled = false;
        }
        /// <summary>
        /// evento clic del boton diagnostico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiagnostico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (busquedaPartidas.Seleccionado)
                {
                    busquedaPartidas = new DiagnosticoAnalista
                    {
                        ListaGrados = busquedaPartidas.ListaGrados,
                        GradoSeleccionado = busquedaPartidas.GradoSeleccionado,
                        Justificacion = busquedaPartidas.Justificacion,
                        ListaProblemas = busquedaPartidas.ListaProblemas,
                        Seleccionado = busquedaPartidas.Seleccionado
                    };
                }
                else
                {
                    busquedaPartidas = new DiagnosticoAnalista();
                }

                busquedaPartidas.Left = (ActualWidth - busquedaPartidas.Width) / 2;
                busquedaPartidas.Top = ((ActualHeight - busquedaPartidas.Height) / 2) + 132;
                busquedaPartidas.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                busquedaPartidas.ProblemasObtenidos = listaProblemas;
                if (cboGradoEnfermedad.SelectedIndex >= 0)
                {
                    var gradoActiual = (GradoInfo)cboGradoEnfermedad.SelectedItem;
                    gradoActiual.isChecked = true;
                    busquedaPartidas.GradoSeleccionado = gradoActiual;
                }
                busquedaPartidas.ShowDialog();

                if (busquedaPartidas.Seleccionado)
                {
                    listaProblemas = busquedaPartidas.ListaProblemas;
                    cboGradoEnfermedad.Items.Clear();
                    busquedaPartidas.GradoSeleccionado.isChecked = true;
                    cboGradoEnfermedad.Items.Add(busquedaPartidas.GradoSeleccionado);
                    cboGradoEnfermedad.SelectedIndex = 0;
                    Dispatcher.BeginInvoke(new Action(MostrarProblemas), DispatcherPriority.Background, null);
                    if (AnimalDetectado != null)
                    {
                        justificacion = busquedaPartidas.Justificacion;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloMostrarDiagnostico, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento click del boton de tratamientos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTratamientos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var medicamientoDialog = new Medicamentos(listaTratamientos,
                    txtCorralDestino.Text,
                    txtNoIndividual.Text)
                {
                    Left = (ActualWidth - Width) / 2,
                    Top = ((ActualHeight - Height) / 2) + 132,
                    Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal]
                };
                medicamientoDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloMostrarTratamientos, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento PreviewTextInput del campo peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPeso_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Evento PreviewKeyDown del campo peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPeso_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (this.txtPeso.Text != this._pesoGanado)
                {
                    if (esNuevo)
                    {
                        Dispatcher.BeginInvoke(new Action(CalcularGanancia), DispatcherPriority.Background, null);
                        ObtenerTipoGanado();
                    }
                    Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                    Dispatcher.BeginInvoke(new Action(CalcularGanancia), DispatcherPriority.Background, null);
                    bandFoco = true;
                    this._pesoGanado = this.txtPeso.Text;
                }
            }
        }
        /// <summary>
        /// evento checked del check de tratamientos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloSeleccionarTratamiento, MessageBoxButton.OK,
                    MessageImage.Error);
            }

        }
        /// <summary>
        /// Evento unchecked del check de tratamientos
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
        /// Evento click del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParametroPL parametroPL = new ParametroPL();
                IList<ParametroInfo> parametros = parametroPL.ObtenerTodos(EstatusEnum.Activo);
                ParametroInfo parametro;
                if(parametros != null)
                {
                     parametro = parametros.Where(parametroEncontrado => parametroEncontrado.Clave==ParametrosEnum.LECTURADOBLEARETE.ToString()).FirstOrDefault();
                  if(parametro != null)
                  {
                    ParametroOrganizacionPL parametroOrganizacionPL = new ParametroOrganizacionPL();
                    ParametroOrganizacionInfo parametroOrganizacion = parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(int.Parse(Application.Current.Properties["OrganizacionID"].ToString()), ParametrosEnum.LECTURADOBLEARETE.ToString());
                    if (parametroOrganizacion != null && parametroOrganizacion.Activo==EstatusEnum.Activo && parametroOrganizacion.Valor.ToUpper() == "TRUE")
                    {
                        if (txtNoIndividual.Text.Trim() == String.Empty)
                        {
                            txtNoIndividual.Focus();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                         Properties.Resources.EntradaGanadoEnfermeria_MsgAreteNoIndividualNoProporcionado,
                                         MessageBoxButton.OK, MessageImage.Stop);
                            return;
                        }
                        if (txtAreteTestigo.Text.Trim() == String.Empty)
                        {
                            txtAreteTestigo.Focus();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                         Properties.Resources.EntradaGanadoEnfermeria_MsgAreteTestigoNoProporcionado,
                                         MessageBoxButton.OK, MessageImage.Stop);
                            return;
                        }
                        if(txtAreteTestigo.Text.Trim() == txtNoIndividual.Text.Trim())
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                         Properties.Resources.EntradaGanadoEnfermeria_MsgAreteTestigoIgualAreteNoIndividual,
                                         MessageBoxButton.OK, MessageImage.Stop);
                            return;
                        }
                    }
                 }
               }
               
                var enfermeriaPl = new EnfermeriaPL();
                var entradaGanadoEnfermeria = new EntradaGanadoEnfermeriaInfo();
                var usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                var animalInfo = new AnimalInfo();
                var resultadoValidacion = CompruebaCamposEnBlanco();
                string Arete = string.Empty;
                bool EsRFID = false;
                bool validarExistenciaArete = true;

                switch (tipoCapturaArete)
                {
                    case 0:
                        Arete = txtAreteTestigo.Text.Trim();
                        EsRFID = true;
                        break;
                    case 1:
                        Arete = txtNoIndividual.Text.Trim();
                        break;
                    case 2:
                        if(txtNoIndividual.Text.Trim() !=  animalActual.Arete)
                        {
                            Arete = txtNoIndividual.Text.Trim();
                        }
                        else if (txtAreteTestigo.Text.Trim() != animalActual.AreteMetalico)
                        {
                            Arete = txtAreteTestigo.Text.Trim();
                        }
                        else
                        {
                            validarExistenciaArete = false;
                        }
                        break;
                }

                if (resultadoValidacion.Resultado)
                {
                    if (!VerificarExisteciaArete(validarExistenciaArete, Arete, EsRFID, organizacionId))
                    {
                        if (listaTratamientos != null && listaTratamientos.Any(registro => registro.Seleccionado))
                            resultadoValidacion = ComprobarExistenciaTratamientos();

                        AnimalDetectado.Animal.Arete = txtNoIndividual.Text.Trim();
                        if (resultadoValidacion.Resultado)
                        {
                            if (esNuevo)
                            {
                                animalInfo.Arete = txtNoIndividual.Text.Trim();
                                animalInfo.AreteMetalico = txtAreteTestigo.Text.Trim();
                                if (ExisteAreteEnInventario(animalInfo))
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.EntradaGanadoEnfermeria_NumeroExiste,
                                        MessageBoxButton.OK, MessageImage.Correct);
                                    return;
                                }
                            }

                            // Se almacena el animal
                            entradaGanadoEnfermeria.Animal = ObtenerAnimal(usuarioId);

                            //Se almacena el movimiento
                            entradaGanadoEnfermeria.Movimiento = ObtenerAnimalMovimiento(animalInfo, usuarioId);

                            entradaGanadoEnfermeria.LoteDestino = loteDestino;
                            entradaGanadoEnfermeria.LoteOrigen = loteOrigen;
                            entradaGanadoEnfermeria.LoteOrigen.Corral = corralOrigen;

                            AnimalDetectado.UsuarioID = usuarioId;
                            AnimalDetectado.Justificacion = txtObservacion.Text;
                            if (ckbDiagnosticoAnalista.IsChecked != null && (bool)ckbDiagnosticoAnalista.IsChecked)
                            {
                                AnimalDetectado.Diagnostico = 1;
                            }
                            else
                            {
                                AnimalDetectado.Diagnostico = 0;
                            }

                            var almacenMovimientoInfo = new AlmacenMovimientoInfo
                            {
                                AlmacenID = almacenInfo.AlmacenID,
                                TipoMovimientoID = (int)TipoMovimiento.SalidaPorConsumo,
                                Status = (int)EstatusInventario.Aplicado,
                                Observaciones = "",
                                UsuarioCreacionID = usuario,
                                AnimalID = AnimalDetectado.Animal.AnimalID,
                                CostoID = (int)Costo.MedicamentoEnfermeria,
                            };
                            entradaGanadoEnfermeria.AlmacenMovimiento = almacenMovimientoInfo;
                            entradaGanadoEnfermeria.Tratamientos = listaTratamientos.Where(item => item.Seleccionado).ToList();
                            entradaGanadoEnfermeria.AlmacenMovimiento = almacenMovimientoInfo;
                            AnimalDetectado.Justificacion = justificacion;
                            entradaGanadoEnfermeria.Deteccion = AnimalDetectado;
                            entradaGanadoEnfermeria.Deteccion.GradoEnfermedad = (GradoInfo)cboGradoEnfermedad.SelectedItem;
                            entradaGanadoEnfermeria.ListaProblemas = listaProblemas;
                            entradaGanadoEnfermeria.UsuarioId = usuarioId;
                            entradaGanadoEnfermeria.CambiarTipoGanado = cambiarTipoGanado;
                            entradaGanadoEnfermeria.AnimalRecaido = animalRecaido;

                            var entradaGanadoPL = new EntradaGanadoPL();
                            var entradaGanadoInfo = entradaGanadoPL.ObtenerEntradaPorLote(loteOrigen);
                            if (entradaGanadoInfo != null && entradaGanadoInfo.TipoOrganizacionOrigenId != (int)TipoOrganizacion.CompraDirecta)
                            {
                                if (entradaGanadoEnfermeria.Animal.PesoCompra == 0)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.EntradaGanadoEnfermeria_GuardadoIncorrecto, MessageBoxButton.OK, MessageImage.Error);
                                    return;
                                }
                            }

                            var resultadoGuardado = enfermeriaPl.GurdarEntradaEnfermeria(entradaGanadoEnfermeria);

                            if (resultadoGuardado.Resultado)
                            {
                                if (esNuevo && animalInterface != null)
                                {
                                    var interfacePl = new InterfaceSalidaAnimalPL();
                                    long AnimalID = resultadoGuardado.Animal.AnimalID;
                                    interfacePl.GuardarAnimalID(animalInterface, AnimalID);

                                }
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.EntradaGanadoEnfermeria_GuardadoCorrecto,
                                    MessageBoxButton.OK, MessageImage.Correct);
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.EntradaGanadoEnfermeria_GuardadoIncorrecto, MessageBoxButton.OK, MessageImage.Error);
                            }

                            txtNoIndividual.Focus();

                            pesoTomado = false;
                            tempTomada = false;
                            cambiarTipoGanado = false;
                            animalRecaido = false;
                            Inicializarcontroles(false);
                            bandBack = true;
                            LimpiarCaptura(true);
                            this._animalDetectado = null;
                            txtTemperatura.Clear();
                            bandBack = false;

                            IniciarTermometro();
                        }
                        else
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
                    }
                    else
                    {
                        if (EsRFID || esNuevo)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.EntradaGanadoEnfermeria_MsgNuevoRFIDYaRegistrado,
                                            MessageBoxButton.OK, MessageImage.Error);
                            txtAreteTestigo.Clear();
                            txtAreteTestigo.Focus();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.EntradaGanadoEnfermeria_MsgNuevoAreteYaRegistrado,
                                            MessageBoxButton.OK, MessageImage.Error);
                            txtNoIndividual.Clear();
                            txtNoIndividual.Focus();
                        }
                    }
                }
                else
                {
                    var mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.DatosBlancos_CorteGanado : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
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
        /// Evento clic del boton rasignar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReasignar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reasignarArete = new ReasignarArete();

                if(rfidConectado)
                    _spManagerRFID.Dispose();
                reasignarArete.TrampaID = trampaID.TrampaID;
                reasignarArete.Left = (ActualWidth - reasignarArete.Width) / 2;
                reasignarArete.Top = ((ActualHeight - reasignarArete.Height) / 2) + 132;
                reasignarArete.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                reasignarArete.ShowDialog();
                if(rfidConectado)
                    InicializarLectorRFID();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Grupo_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento Cambio de seleccion del combo corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCorralDestino_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ckbCronico.IsChecked == true)
                {
                    if (cboCorralDestino.SelectedIndex > 0)
                    {
                        corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;
                        if (corralDestino.TipoCorral.TipoCorralID == TipoCorral.CronicoVentaMuerte.GetHashCode())
                        {
                            if (SkMessageBox.Show(
                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.EntradaGanadoEnfermeria_PreguntaCronico,
                                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                            {
                                corralDestino = (CorralInfo)cboCorralDestino.SelectedItem;
                                loteDestino = ConsultarLote(corralDestino.CorralID);
                                cboCorralDestino.IsEnabled = false;
                                txtCorralDestino.Text = corralDestino.Codigo;
                            }
                            else
                            {
                                cboCorralDestino.SelectedIndex = 0;
                                cboCorralDestino.IsEnabled = true;
                            }
                        }
                        else
                        {
                            loteDestino = ConsultarLote(corralDestino.CorralID);
                            cboCorralDestino.IsEnabled = false;
                            txtCorralDestino.Text = corralDestino.Codigo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloSeleccionarCorralDestino, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento Cambio de seleccion del combo de grado de enfermedad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGrado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboGradoEnfermedad.SelectedItem != null)
                {
                    var gradoSeleccionado = (GradoInfo)cboGradoEnfermedad.SelectedItem;
                    if (ckbCronico.IsChecked == false)
                    {
                        if (gradoSeleccionado != null &&
                            gradoSeleccionado.GradoID == (int)GradoEnfermedadEnum.Level2)
                        {
                            txtCorralDestino.Text = txtCorralOrigen.Text;
                            corralDestino = enfermeriaSeleccionada.Corral;
                            txtCorralDestino.IsEnabled = false;
                        }
                        else
                        {
                            txtCorralDestino.IsEnabled = true;
                            ColocarFocoPrimerControl();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloSeleccionaGrado, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento GotFocus del campo arete testigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAreteTestigo_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNoIndividual.Text))
            {
                //txtNoIndividual.Focus();
            }
        }
        /// <summary>
        /// Evento FocusableChanged del campo Arete testigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAreteTestigo_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNoIndividual.Text))
            {
                txtNoIndividual.Focus();
            }
        }
        /// <summary>
        /// Evento lost focus del campo peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPeso_LostFocus(object sender, RoutedEventArgs e)
        {
            if (bandFoco == false)
            {
                if (txtPeso.Text.Trim().Length > 0)
                {
                    if (this.txtPeso.Text != this._pesoGanado)
                    {
                        if (esNuevo)
                        {
                            Dispatcher.BeginInvoke(new Action(ObtenerTipoGanado), DispatcherPriority.Background, null);
                        }
                        Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                        Dispatcher.BeginInvoke(new Action(CalcularGanancia), DispatcherPriority.Background, null);
                        bandFoco = false;
                        this._pesoGanado = this.txtPeso.Text;
                    }

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
        /// <summary>
        /// Evento cambio de texto del campo peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPeso_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtPeso.Text))
            {
                CtrlPegar = false;
                txtPeso.Text = txtPeso.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtPeso.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }
        }
        /// <summary>
        /// Evento keyUp del campo peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPeso_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                bandBack = false;
            }
        }
        /// <summary>
        /// Evento KeyDown del campo observaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtObservaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                btnGuardar.Focus();
            }
        }
        /// <summary>
        /// Evento OnPreviewTextInput Del campo temperatura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTemperatura_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, txtTemperatura.Text);
        }
        /// <summary>
        /// Evento OnPreviewKeyDown del campo temperatura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTemperatura_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (txtPeso.Text.Trim().Length > 0)
                {
                    try
                    {
                        var valor = double.Parse(txtTemperatura.Text);
                        ValidarTemperatura(valor);
                        AsignarDatosGridTratamientos();
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
        /// Evento cambio de texto del cmpo corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtCorralDestino.Text))
            {
                ctrlPegar = false;
                txtCorralDestino.Text = txtCorralDestino.Text.Replace(" ", "");
            }
            else
            {
                if (ctrlPegar)
                {
                    txtCorralDestino.Text = TextoAnterior;
                    ctrlPegar = false;
                    TextoAnterior = String.Empty;
                }
            }
        }
        /// <summary>
        /// Evento PreviewKeyDown del campo corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Evento lostfocus del campo corral destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCorralDestino_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCorralDestino.Text.Trim().Length > 0)
            {
                ConsultarCorralDestino();
                CalcularGanancia();
            }
        }
        /// <summary>
        /// Evento PreviewTextInput del campo arete testigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAreteTestigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            MostrarDetalleHistorial();
        }

        /// <summary>
        /// Evento para el boton cambiar de sexo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgCambiarSexo_MouseDown(object sender, MouseButtonEventArgs e)
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
                cboCalidad.IsEnabled = true;
                Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
                cambiarTipoGanado = true;
            }
        }
        /// <summary>
        /// Evento KeyDown para campo arete RFID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAreteTestigo_KeyDown(object sender, KeyEventArgs e)
        {
            start = GetTickCount();
            string Arete = string.Empty;

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                e.Handled = true;
                if (cboSexo.SelectedIndex == 0)
                {
                        //Busca tabla Animal
                        var animalPL = new AnimalPL();
                        var animalArete = animalPL.ObtenerAnimalPorAreteTestigo(txtAreteTestigo.Text.Trim(), organizacionId);
                        if (animalArete != null)
                        {
                            Arete = animalArete.Arete;
                        }
                        else
                        {
                            var interfacePL = new InterfaceSalidaAnimalPL();
                            var animalAreteInterface = interfacePL.ObtenerNumeroAreteMetalico(txtAreteTestigo.Text.Trim(), organizacionId);
                            if (animalAreteInterface != null)
                            {
                                Arete = animalAreteInterface.Arete;
                            }
                        }

                    if (Arete.Trim() != string.Empty)
                    {
                        txtNoIndividual.Text = Arete;
                        if (ValidarAreteCapturado())
                        {
                            txtAreteTestigo.IsEnabled = String.IsNullOrEmpty(txtAreteTestigo.Text);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaGanadoEnfermeria_NumeroNoExisteEnElInventario,
                            MessageBoxButton.OK, MessageImage.Warning);
                        txtNoIndividual.Focusable = true;
                        txtNoIndividual.Focus();
                        LimpiarCaptura(true);
                        txtTemperatura.Clear();
                        pesoTomado = false;
                        Inicializarcontroles(false);
                    }
                }
                else
                {
                    if (VerificarExisteciaArete(true, txtAreteTestigo.Text, true, organizacionId))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.EntradaGanadoEnfermeria_MsgNuevoRFIDYaRegistrado,
                                        MessageBoxButton.OK, MessageImage.Error);
                        txtAreteTestigo.Clear();
                        txtAreteTestigo.Focus();
                    }
                    else
                    {
                        ColocarFocoPrimerControl();
                    }
                }
            }
        }

        /// <summary>
        /// evento para leer el peso de la bascula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeerPeso_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info();
                if (basculaConectada)
                {
                    capturaInmediata = true;
                    spManager.StopListening();
                    spManager.StartListening(configBascula.Puerto,
                        configBascula.Baudrate,
                        configBascula.Paridad,
                        configBascula.Databits,
                        configBascula.BitStop);
                }
                else
                {
                    if (spManager != null)
                    {
                        spManager.Dispose();
                        spManager.StartListening(configBascula.Puerto,
                                    configBascula.Baudrate,
                                    configBascula.Paridad,
                                    configBascula.Databits,
                                    configBascula.BitStop);
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
                btnLeerPeso.IsEnabled = true;
            }
        }
        /// <summary>
        /// evento para leer la temperatura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnLeerTemp_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info();
                if (termometroConectado)
                {
                    btnLeerTemp.IsEnabled = false;
                    maxTemp = double.Parse(txtDisplayTemperatura.Text);
                    Dispatcher.BeginInvoke(new Action(CapturarTemperaturaDeDisplay),
                                           DispatcherPriority.Background);
                    AsignarDatosGridTratamientos();
                    tempTomada = true;
                    btnLeerTemp.IsEnabled = true;
                }
                else
                {
                    if (spManagerTermo != null)
                    {
                        btnLeerTemp.IsEnabled = false;
                        spManagerTermo.Dispose();
                        spManagerTermo.StartListening(configTermometro.Puerto,
                                    configTermometro.Baudrate,
                                    configTermometro.Paridad,
                                    configTermometro.Databits,
                                    configTermometro.BitStop);
                        
                        btnLeerTemp.IsEnabled = true;
                        termometroConectado = true;
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Error(error);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                  Properties.Resources.CorteGanado_ErrorCapturaTermometro,
                    MessageBoxButton.OK, MessageImage.Stop);
                btnLeerTemp.IsEnabled = true;
                termometroConectado = false;
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// desabilita los controles
        /// </summary>
        /// <param name="habilitar"></param>
        private void DeshabilitarControles(bool habilitar)
        {
            gpbDatosGenerales.IsEnabled = habilitar;
            gpbGenerales.IsEnabled = habilitar;
            gpbDatosClinicos.IsEnabled = habilitar;
            gpbDatosCorte.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
        }
        /// <summary>
        /// Inicializa los dispositivos
        /// </summary>
        private void InicializarDispositivos()
        {
            try
            {
                if (!pesoTomado)
                {
                    if (timer == null)
                    {
                        timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                        timer.Tick += (Timer_Tick);
                        timerTickCount = 0;
                    }
                    timerTickCount = 0;
                    //Una vez creadas las instancias de el SerialPorts se ejecuta el timer
                    timer.Start();
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
                    countTemp = 0;
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
                    InicializarLectorRFID();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo Verificar si existen Trampas configuradas
        /// </summary>
        private bool ExistenTrampas()
        {
            var bExiste = false;
            var trampaPl = new TrampaPL();

            try
            {
                var trampaInfo = new TrampaInfo
                {
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    TipoTrampa = (char)TipoTrampa.Enfermeria,
                    HostName = Environment.MachineName
                };

                var trampaInfoResp = trampaPl.ObtenerTrampa(trampaInfo);
                if (trampaInfoResp != null)
                {
                    trampaID = trampaInfoResp;
                    bExiste = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloValidarTrampa, MessageBoxButton.OK, MessageImage.Error);
            }
            return bExiste;
        }

        /// <summary>
        /// Se muestran los datos de la partida seleccionada
        /// </summary>
        /// <param name="enfermeriaInfo"></param>
        private void ObtenerDatosDePartidaSeleccionada(EnfermeriaInfo enfermeriaInfo)
        {
            LlenarCorral(enfermeriaInfo);
            Dispatcher.BeginInvoke(new Action(LlenarAnimal), DispatcherPriority.Background, null);
        }
        /// <summary>
        /// Llena los datos del corral
        /// </summary>
        /// <param name="enfermeriaInfo"></param>
        private void LlenarCorral(EnfermeriaInfo enfermeriaInfo)
        {
            if (enfermeriaInfo == null) return;
            txtCorralOrigen.Text = enfermeriaInfo.Corral.Codigo;
            loteOrigen = ConsultarLote(enfermeriaInfo.Corral.CorralID);
            txtNoPartida.Text = enfermeriaInfo.FolioEntrada.ToString(CultureInfo.InvariantCulture);
            corralOrigen = enfermeriaInfo.Corral;
        }
        /// <summary>
        /// Llena los datos del animal
        /// </summary>
        private void LlenarAnimal()
        {
            if (AnimalDetectado != null)
            {
                if (AnimalDetectado.Animal != null)
                {
                    if (!string.IsNullOrEmpty(AnimalDetectado.Animal.Arete))
                    {
                        txtNoIndividual.Text = AnimalDetectado.Animal.Arete;
                        BuscarAnimal(false);
                    }
                    else
                    {
                        esNuevo = true;
                        Inicializarcontroles(true);
                        Dispatcher.BeginInvoke(new Action(MostrarDatosDeteccion), DispatcherPriority.Background, null);
                        ObtenerDiasAlta();
                    }
                }
            }
        }

        /// <summary>
        /// Calcula de la ganancia 
        /// </summary>
        private void CalcularGanancia()
        {
            try
            {
                txtGananciaDiaria.Text = "0";
                if (txtPeso.Text.Length > 0)
                {
                    if (AnimalDetectado.EnfermeriaCorral.Corral.TipoCorral.TipoCorralID == (int)TipoCorral.Recepcion)
                    {
                        txtGananciaDiaria.Text = "0";
                    }
                    else
                    {
                        var newDate = DateTime.Now;
                        if (infoDatosCopmra != null && animalActual != null)
                        {
                            var ts = newDate - infoDatosCopmra.FechaInicio;
                            if (ts.Days > 0)
                            {
                                txtGananciaDiaria.Text =
                                   Convert.ToDecimal(((decimal.Parse(txtPeso.Text) - animalActual.PesoCompra) / ts.Days)).ToString("N3");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.EntradaGanadoEnfermeria_FalloCalcularGanancia,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para obtener los datos de la compra
        /// </summary>
        private void ObtenerDatosCompra()
        {
            try
            {
                var enfermeria = new EnfermeriaPL();
                var animal = animalActual;
                animal.OrganizacionIDEntrada = organizacionId;
                int noPartida;
                if (!int.TryParse(txtNoPartida.Text, out noPartida))
                {
                    noPartida = 0;
                }

                infoDatosCopmra = enfermeria.ObtenerDatosCompra(noPartida, organizacionId);
                if (infoDatosCopmra != null)
                {
                    txtOrigen.Text = infoDatosCopmra.Origen;
                    txtProveedor.Text = infoDatosCopmra.Proveedor;
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
        /// Llena el combo de sexo de ganado, obtiene el sexo del ganado de un Enum dentro de la aplicacion
        /// </summary>
        private void CargarCboSexo()
        {
            try
            {
                IList<Sexo> sexoEnums = Enum.GetValues(typeof(Sexo)).Cast<Sexo>().ToList();
                var listaSexo = new Dictionary<char, string>();

                var i = 0;
                listaSexo.Add('S', "Seleccione");
                foreach (var varSexo in sexoEnums)
                {
                    if (i == 0)
                    {
                        listaSexo.Add('H', varSexo.ToString());
                    }
                    if (i == 1)
                    {
                        listaSexo.Add('M', varSexo.ToString());
                    }

                    i++;
                }
                cboSexo.ItemsSource = listaSexo.Values;
                cboSexo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.EntradaGanadoEnfermeria_FalloCargarSexo,
                    MessageBoxButton.OK, MessageImage.Error);
            }
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
                    var seleccione = new CalidadGanadoInfo { Descripcion = "Seleccione", CalidadGanadoID = 0 };
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
            }
        }

        /// <summary>
        /// Metodo Para obtener el tipo de ganado
        /// </summary>
        private void ObtenerTipoGanado()
        {
            try
            {
                var tipoGanadoPl = new TipoGanadoPL();
                if (cboSexo.SelectedItem == null) return;
                var sexoGanado = ObtieneSexoGanado(cboSexo.SelectedItem, true);

                // Si traemos peso compra calculamos el tipo de ganado con el peso compra de lo contrario con el peso actual del animal
                var pesoCompra = 0;
                if (animalActual != null && animalActual.PesoCompra > 0)
                {
                    pesoCompra = animalActual.PesoCompra;
                }
                else if (!String.IsNullOrWhiteSpace(txtPeso.Text.Trim()))
                {
                    int resultado;
                    if (int.TryParse(txtPeso.Text, out resultado))
                    {
                        pesoCompra = int.Parse(txtPeso.Text);
                    }
                }
                //se determian el tipo de ganado en base al sexo y peso
                if (!String.IsNullOrWhiteSpace(sexoGanado) && pesoCompra > 0)
                {
                    TipoGanadoInfo tipoGanadoInfo = tipoGanadoPl.ObtenerTipoGanadoSexoPeso(sexoGanado,
                        pesoCompra);
                    if (tipoGanadoInfo != null)
                    {
                        AsignarTipoGanado(tipoGanadoInfo);
                        if (animalActual != null)
                        {
                            animalActual.TipoGanado = tipoGanadoInfo;
                            if(animalActual.CalidadGanadoID > 0)
                                cboCalidad.SelectedValue = animalActual.CalidadGanadoID;
                        }
                    }
                    else
                    {
                        cboTipoGanado.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloCargarTipoGanado,
                       MessageBoxButton.OK, MessageImage.Error);
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
        /// Metodo Para asignar el tipo de ganadoal combo de tipo de ganado
        /// </summary>
        private void AsignarTipoGanado(TipoGanadoInfo tipoGanadoInfo)
        {
            if (tipoGanadoInfo == null) return;
            cboTipoGanado.Items.Clear();
            cboTipoGanado.Items.Add(tipoGanadoInfo);
            cboTipoGanado.SelectedIndex = 0;
        }

        /// <summary>
        /// Metodo que valida la temperatura del animal y marcar el tratamiento 26 de acuerdo a la regla de negocio
        /// </summary>
        /// <param name="temperatura"></param>
        private void ValidarTemperatura(double temperatura)
        {
            try
            {
                if (temperatura >= maxTemperaturaAnimal && listaTratamientos != null)
                {
                    foreach (var item in listaTratamientos)
                    {
                        if (item.CodigoTratamiento == codigoTratamientoTemperatura)
                        {
                            item.Seleccionado = true;
                            dgTratamientos.ItemsSource = null;
                            listaTratamientos.ToList().ForEach(sel => sel.Seleccionado = false);
                            dgTratamientos.ItemsSource = listaTratamientos;
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
                            if (item.CodigoTratamiento == codigoTratamientoTemperatura)
                            {
                                item.Seleccionado = false;
                                dgTratamientos.ItemsSource = null;
                                listaTratamientos.ToList().ForEach(sel => sel.Seleccionado = false);
                                dgTratamientos.ItemsSource = listaTratamientos;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloValidarTemperatura,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo Verificar si existen Partidas programadas
        /// </summary>
        private bool ExisteAreteEnInventario(AnimalInfo animalInfo)
        {
            var existe = false;
            try
            {
                var corteGanadoPl = new CorteGanadoPL();

                AnimalInfo resultadoBusqueda = null;
                if (animalInfo.Arete != string.Empty)
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
                        txtNoIndividual.Focus();
                        existe = false;
                    }
                    animalActual = resultadoBusqueda;
                    AnimalDetectado.Animal = animalActual;
                    existe = true;
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
        /// Obtiene los dias de alta
        /// </summary>
        public void ObtenerDiasAlta()
        {
            try
            {
                var enfermeriaPl = new EnfermeriaPL();
                if (animalActual == null)
                {
                    txtDiasAlta.Text = "0";
                    return;
                }
                var animalMovimiento = enfermeriaPl.ObtenerUltimoMovimientoEnfermeria(animalActual);

                if (animalMovimiento == null)
                {
                    txtDiasAlta.Text = "0";
                }
                else
                {
                    if (animalMovimiento.Activo == EstatusEnum.Inactivo)
                    {
                        var newDate = DateTime.Now;
                        var ts = newDate - animalMovimiento.FechaMovimiento;
                        txtDiasAlta.Text = ts.Days.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        txtDiasAlta.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloObtenerDiasAlta,
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
                    var clasificacion = new ClasificacionGanadoInfo
                    {
                        Descripcion = "Seleccione",
                        ClasificacionGanadoID = 0
                    };
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
        /// Metodo para llenar el combo de la corrales destinos.
        /// </summary>
        private void LlenarComboCorralDestino()
        {
            try
            {
                var corralPl = new CorralPL();
                var corralFiltro = new CorralInfo
                {
                    GrupoCorral = (int)GrupoCorralEnum.Enfermeria,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId }
                };
                var resultado = corralPl.ObtenerCorralesPorTipoEnfermeria(corralFiltro);
                if (resultado == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_SinCorral,
                       MessageBoxButton.OK, MessageImage.Warning);
                }
                else
                {
                    var corralInicio = new CorralInfo { Codigo = "Seleccione", CorralID = 0 };
                    resultado.Lista.Insert(0, corralInicio);
                    cboCorralDestino.ItemsSource = resultado.Lista;
                    cboCorralDestino.SelectedValue = 0;
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_SinCorral,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Busqueda del animal
        /// </summary>
        /// <returns></returns>
        private bool BuscarAnimal(bool existeAnimalInventario)
        {
            bool consultarDeteccion = true;

            try
            {
                //Se valida que pertenezca a la misma partida
                var animalInfo = new AnimalInfo
                {
                    Arete = txtNoIndividual.Text,
                    AreteMetalico = txtAreteTestigo.Text,
                    OrganizacionIDEntrada = organizacionId
                };
                var recaido = 0;
                var enfermeriaPl = new EnfermeriaPL();
                var animalPl = new AnimalPL();
                var corralPL = new CorralPL();
                Inicializarcontroles(true);

                if (AnimalDetectado == null)
                {
                    AnimalDetectado = enfermeriaPl.ObtenerAnimalDetectadoPorArete(animalInfo);
                }

                if (AnimalDetectado == null)
                {
                    AnimalInfo animalEnfermo = null;

                    if (!string.IsNullOrEmpty(txtNoIndividual.Text.Trim()))
                        animalEnfermo = animalPl.ObtenerAnimalPorArete(txtNoIndividual.Text, organizacionId);
                    else
                    {
                        animalEnfermo = animalPl.ObtenerAnimalPorAreteTestigo(txtAreteTestigo.Text, organizacionId);
                        if (animalEnfermo != null)
                        {
                            animalInfo.Arete = animalEnfermo.Arete;
                        }
                    }

                    if (animalEnfermo != null)
                    {
                        AnimalDetectado = new AnimalDeteccionInfo { Animal = animalEnfermo };
                        var animalMovimiento = animalPl.ObtenerUltimoMovimientoAnimal(animalEnfermo);
                        if (animalMovimiento != null)
                        {
                            var corral = corralPL.ObtenerPorId(animalMovimiento.CorralID);
                            if (corral.GrupoCorral == (int)GrupoCorralEnum.Enfermeria)
                            {
                                if (corral.TipoCorral.TipoCorralID != (int)TipoCorral.CronicoVentaMuerte)
                                {
                                    //Obtener ultima deteccion
                                    AnimalDetectado =
                                        enfermeriaPl.ObtenerAnimalDetectadoPorAreteUltimaDeteccion(animalInfo);
                                    AnimalDetectado.Animal = animalEnfermo;
                                    animalActual = animalEnfermo;
                                    if (AnimalDetectado != null)
                                    {
                                        enfermeriaSeleccionada = AnimalDetectado.EnfermeriaCorral;
                                        enfermeriaSeleccionada.Corral = corral;
                                        recaido = 1;
                                        existeAnimalInventario = true;
                                        animalRecaido = true;
                                    }
                                }
                                else
                                {
                                    // El número individual no se encuentra en detección o enfermería. Favor de verificar.
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                      Properties.Resources.
                                                          EntradaGanadoEnfermeria_AreteNoSeEncuentraDeteccionEnfermeria,
                                                      MessageBoxButton.OK, MessageImage.Warning);
                                    txtNoIndividual.Focusable = true;
                                    txtNoIndividual.Focus();
                                    LimpiarCaptura(true);
                                    txtTemperatura.Clear();
                                    pesoTomado = false;
                                    Inicializarcontroles(false);
                                    return false;
                                }
                            }
                            else
                            {
                                AnimalDetectado = null;
                            }
                        }
                        else
                        {
                            AnimalDetectado = null;
                        }
                    }
                }
                if (AnimalDetectado == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.EntradaGanadoEnfermeria_AreteNoSeEncuentraDeteccionEnfermeria,
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtNoIndividual.Focusable = true;
                    txtNoIndividual.Focus();
                    LimpiarCaptura(true);
                    txtTemperatura.Clear();
                    pesoTomado = false;
                    Inicializarcontroles(false);
                    return false;
                }

                animalActual = animalActual ?? AnimalDetectado.Animal;
                enfermeriaSeleccionada = AnimalDetectado.EnfermeriaCorral;
                LlenarCorral(enfermeriaSeleccionada);
                ObtenerDatosCompra();
                InicializarDispositivos();

                // Se valida el grupo del corral
                corralOrigen = enfermeriaSeleccionada.Corral ?? corralPL.ObtenerPorId(corralOrigen.CorralID);
                if (corralOrigen.GrupoCorral == (int)GrupoCorralEnum.Recepcion && recaido == 0)
                {
                    consultarDeteccion = ValidarAreteEnCorralDeRecepcion(animalInfo);
                }
                else if ((corralOrigen.GrupoCorral == (int)GrupoCorralEnum.Produccion ||
                          corralOrigen.GrupoCorral == (int)GrupoCorralEnum.Corraleta) ||
                         (corralOrigen.GrupoCorral == (int)GrupoCorralEnum.Enfermeria &&
                          corralOrigen.TipoCorral.TipoCorralID != (int)TipoCorral.CronicoVentaMuerte))
                {
                    /* Se valida que exista el arete en Animal */
                    if (existeAnimalInventario || ExisteAreteEnInventario(animalInfo))
                    {
                        var tipoGanadoPl = new TipoGanadoPL();
                        var tipoGanado = tipoGanadoPl.ObtenerPorID(animalActual.TipoGanadoID);
                        cboSexo.SelectedItem = tipoGanado.Sexo.ToString();
                        AsignarTipoGanado(tipoGanado);
                        ObtenerDiasAlta();
                        cboCalidad.SelectedValue = animalActual.CalidadGanadoID;
                        cboClasificacion.SelectedValue = animalActual.ClasificacionGanadoID;
                        txtNoIndividual.Text = animalActual.Arete;
                        txtAreteTestigo.Text = animalActual.AreteMetalico;
                        txtNoPartida.Text = animalActual.FolioEntrada.ToString(CultureInfo.InvariantCulture);
                        cboCalidad.IsEnabled = false;
                        cboClasificacion.IsEnabled = false;
                        cboTipoGanado.IsEnabled = false;
                        cboSexo.IsEnabled = false;
                        if (!rfidConectado)
                        {
                            txtAreteTestigo.Focus();
                        }
                    }
                    else
                    {
                        consultarDeteccion = ConsultarAreteAnimal(txtNoIndividual.Text);
                    }
                }

                if (consultarDeteccion)
                {
                    if (animalActual != null && animalActual.AnimalID == 0)
                        esNuevo = true;
                    else if(animalActual != null)
                        esNuevo = false;


                    if (AnimalDetectado != null)
                    {
                        if (AnimalDetectado.Animal.AreteMetalico != string.Empty)
                        {
                            txtAreteTestigo.Text = AnimalDetectado.Animal.AreteMetalico;
                        }
                        MostrarDatosDeteccion();
                    }
                }
            }
            catch (Exception ex)
            {
                consultarDeteccion = false;
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloBuscarAnimal,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            return consultarDeteccion;
        }
        /// <summary>
        /// Mustra los datos de la deteccion
        /// </summary>
        private void MostrarDatosDeteccion()
        {
            try
            {
                dtpFechaDeteccion.SelectedDate = AnimalDetectado.FechaDeteccion;
                dtuHorasDeteccion.Value = AnimalDetectado.FechaDeteccion;
                listaProblemas = AnimalDetectado.Problemas;
                MostrarProblemas();

                cboDetector.Items.Clear();
                if (AnimalDetectado.Detector != null)
                {
                    cboDetector.Items.Add(AnimalDetectado.Detector);
                    cboDetector.SelectedIndex = 0;
                }

                cboGradoEnfermedad.Items.Clear();
                cboGradoEnfermedad.Items.Add(AnimalDetectado.GradoEnfermedad);
                cboGradoEnfermedad.SelectedIndex = 0;
                txtComentario.Text = AnimalDetectado.DescripcionGanado.Descripcion;

                CalcularGanancia();
                ObtenerHistorialClinico();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloMostrarDatosDeteccion,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Inicializa los controles
        /// </summary>
        /// <param name="habilitar"></param>
        private void Inicializarcontroles(bool habilitar)
        {
            cboSexo.IsEnabled = habilitar;
            cboCalidad.IsEnabled = habilitar;
            cboClasificacion.IsEnabled = habilitar;

            cboTipoGanado.IsEnabled = habilitar;
            if (!basculaConectada && this.configBascula.CapturaManual)
            {
                txtPeso.IsEnabled = habilitar;
            }
            if (!termometroConectado && this.configTermometro.CapturaManual)
            {
                txtTemperatura.IsEnabled = habilitar;
            }
            if (!rfidConectado)
            {
                txtAreteTestigo.IsEnabled = _configRFIDCorte.CapturaManual;
            }

            gpbGenerales.IsEnabled = habilitar;
            gpbDatosClinicos.IsEnabled = habilitar;
            gpbDatosCorte.IsEnabled = habilitar;
            gpbDisplayBascula.IsEnabled = habilitar;
            btnDiagnostico.IsEnabled = false;
            btnGuardar.IsEnabled = habilitar;
        }
        /// <summary>
        /// Obtiene los tratamientos
        /// </summary>
        private void ObtenerTratamiento()
        {
            if (txtPeso.Text.Trim().Length > 0 && cboSexo.SelectedItem != null && cboSexo.SelectedItem.ToString() != "Seleccione")
            {
                try
                {
                    var tratamientoPl = new TratamientoPL();
                    var tratamientoInfo = new TratamientoInfo
                    {
                        OrganizacionId = organizacionId,

                        Sexo = (Sexo)Enum.Parse(typeof(Sexo), cboSexo.SelectedItem.ToString()),
                        Peso = int.Parse(txtPeso.Text)
                    };
                    if (listaProblemas == null)
                    {
                        listaProblemas = new List<ProblemaInfo>();
                    }
                    if (listaProblemas.Count >= 0)
                    {
                        var listProblems = new List<int>();
                        foreach (var problema in listaProblemas)
                        {
                            if (problema.Descripcion == ProblemasEnum.CRB.ToString())
                            {
                                idProblemaCrb = problema.ProblemaID;
                            }
                            if (problema.isCheked)
                            {
                                listProblems.Add(problema.ProblemaID);
                            }
                        }
                        listaTratamientos = tratamientoPl.ObtenerTratamientosPorProblemas(tratamientoInfo,
                            listProblems);
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
                            ValidarSiEsTransferenciaAsignarAreteBlanco(true);
                            if (!string.IsNullOrEmpty(txtTemperatura.Text))
                            {
                                var valor = double.Parse(txtTemperatura.Text);
                                ValidarTemperatura(valor);
                            }
                            ValidarTratamientoInicial();
                            AsignarDatosGridTratamientos();
                        }
                    }
                }
                catch (FormatException fex)
                {
                    Logger.Error(fex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteGanado_PesoCorteInvalido,
                        MessageBoxButton.OK, MessageImage.Warning);
                    txtPeso.Clear();
                    cboTipoGanado.SelectedIndex = -1;
                    txtCorralDestino.Text = "";
                    dgTratamientos.ItemsSource = null;
                    listaTratamientos = null;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.EntradaGanadoEnfermeria_FalloObtenerTratamientos,
                           MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        private void AsignarDatosGridTratamientos()
        {
            if (listaTratamientos == null) 
            {
                return;
            }
            ValidarTratamientos();
            listaTratamientos.ToList().ForEach(sel => sel.Seleccionado = false);
            dgTratamientos.ItemsSource = listaTratamientos;
        }

        /// <summary>
        /// Metodo para validar si ya se le aplico tratamiento inicial al animal.
        /// </summary>
        private void ValidarTratamientoInicial()
        {
            try
            {
                var animalInfo = new AnimalInfo
                {
                    Arete = txtNoIndividual.Text,
                    OrganizacionIDEntrada = organizacionId
                };
                var corteTransferenciaGanadoPl = new CorteTransferenciaGanadoPL();
                var tratamientosAplicados = corteTransferenciaGanadoPl.ObtenerTratamientosAplicados(animalInfo, -1);
                if (listaTratamientos != null)
                {
                    if (tratamientosAplicados != null)
                    {
                        foreach (var tratamientosApli in tratamientosAplicados)
                        {
                            foreach (var tratamientos in listaTratamientos)
                            {
                                if (tratamientos.TratamientoID == tratamientosApli.TratamientoID)
                                {
                                    var newDate = DateTime.Now;

                                    var ts = newDate - tratamientosApli.FechaAplicacion;
                                    var dias = ts.Days;
                                    tratamientos.Habilitado = (dias > tratamientos.Dias);

                                    tratamientos.Seleccionado = true;
                                }
                                else
                                {
                                    tratamientos.Seleccionado = false;
                                    tratamientos.Habilitado = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var tratamientos in listaTratamientos)
                        {
                            if (tratamientos.TipoTratamiento == (int)TipoTratamiento.Corte)
                                tratamientos.Seleccionado = true;
                            tratamientos.Habilitado = true;

                            if (tratamientos.ProblemaID == idProblemaCrb)
                            {
                                tratamientos.Seleccionado = true;
                            }
                        }
                    }

                    foreach (var tratamientos in listaTratamientos)
                    {
                        if (tratamientos.ProblemaID == idProblemaCrb)
                        {
                            tratamientos.Seleccionado = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloValidarTratamientoInicial,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Consulta el arete en la base de dato tabla InterfaceAnimal.
        /// </summary>
        /// <param name="arete"></param>
        private bool ConsultarAreteAnimal(string arete)
        {
            try
            {
                cboSexo.IsEnabled = true;

                //Se valida que pertenezca a la misma partida
                var animalPl = new InterfaceSalidaAnimalPL();
                interfaceSalidoAnimalInfo = animalPl.ObtenerNumeroAreteIndividual(arete, organizacionId);
                if (interfaceSalidoAnimalInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        "El No Individual no corresponde a la partida. Favor de verificar.",
                        MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCaptura(true);
                    txtTemperatura.Clear();
                    Inicializarcontroles(false);
                    txtNoIndividual.Clear();
                    txtNoIndividual.Focus();
                    return false;
                }
                InicializarDispositivos();

                cboSexo.SelectedItem = interfaceSalidoAnimalInfo.TipoGanado.Sexo.ToString();
                animalActual.FechaCompra = interfaceSalidoAnimalInfo.FechaCompra;
                animalActual.PesoCompra = (int)interfaceSalidoAnimalInfo.PesoCompra;
                AsignarTipoGanado(interfaceSalidoAnimalInfo.TipoGanado);
                cboSexo.IsEnabled = false;
                cboTipoGanado.IsEnabled = false;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ErrorConsultaArete_CorteGanado,
                        MessageBoxButton.OK, MessageImage.Warning);
                return false;
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
            cboCalidad.SelectedIndex = 0;
            cboTipoGanado.SelectedIndex = -1;
            cboClasificacion.SelectedIndex = 0;
            cboGradoEnfermedad.SelectedIndex = -1;
            cboDetector.SelectedIndex = -1;
            tipoCapturaArete = 3;

            justificacion = string.Empty;

            // Limpia TextBox
            if (bCancelar)
            {
                txtCorralOrigen.Clear();
                txtNoPartida.Clear();

                txtProveedor.Clear();
                txtOrigen.Clear();
                dtpFechaRecepcion.SelectedDate = null;
                
                txtNoIndividual.IsEnabled = true;
                txtNoIndividual.Focus();
                
            }
            else
            {
                txtNoIndividual.Focus();
            }

            //Resetea los dispositivos, valida si estan conectados
            DisposeDispositivosConectados();
            if(rfidConectado)
                InicializarLectorRFID();

            cboCalidad.ItemsSource = null;
            if (bandBack)
            {
                txtNoIndividual.Clear();
            }
            txtAreteTestigo.Clear();
            txtPeso.Clear();
            pesoCapturadoGlobal = "";
            txtCorralDestino.Clear();

            txtProblemaDetectado.Clear();
            dtuHorasDeteccion.Value = null;
            txtDisplayPeso.Text = null;
            txtDiasAlta.Clear();
            txtCorralOrigen.Clear();
            txtObservacion.Clear();
            txtComentario.Clear();
            txtOrigen.Clear();
            txtProveedor.Clear();
            txtNoPartida.Clear();
            txtGananciaDiaria.Clear();
            // Limpia Grid Medicamentos
            dgTratamientos.ItemsSource = null;
            listaTratamientos = null;
            interfaceSalidoAnimalInfo = null;

            animalActual = null;
            animalInterface = null;
            historialClinico = null;
            dgHistorialClinico.ItemsSource = null;
            ckbDiagnosticoAnalista.IsChecked = false;
            ckbCronico.IsChecked = false;
            cambiarTipoGanado = false;
            animalRecaido = false;
            esNuevo = false;
            AnimalDetectado = null;

            busquedaPartidas = new DiagnosticoAnalista();
            lblCostoTotal.Content = string.Empty;
        }

        /// <summary>
        /// Consulta el corral destino
        /// </summary>
        private void ConsultarCorralDestino()
        {
            try
            {
                if (txtCorralDestino.Text.Length > 0)
                {
                    var corralPl = new CorralPL();
                    corralDestino =
                        corralPl.ObtenerPorCodigoGrupo(new CorralInfo
                        {
                            Codigo = txtCorralDestino.Text,
                            Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                            GrupoCorral = (int)GrupoCorralEnum.Enfermeria
                        });

                    if (corralDestino == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaGanadoEnfermeria_CorralDestinoNoExiste,
                            MessageBoxButton.OK, MessageImage.Warning);
                        txtCorralDestino.Text = string.Empty;
                    }
                    else
                    {
                        if (corralDestino.GrupoCorral != (int)GrupoCorralEnum.Enfermeria)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.EntradaGanadoEnfermeria_CorralDestinoNoEnfermeria,
                                MessageBoxButton.OK, MessageImage.Warning);
                            txtCorralDestino.Text = string.Empty;
                        }
                        else
                        {
                            if (corralDestino.TipoCorral != null &&
                                corralDestino.TipoCorral.TipoCorralID != (int)TipoCorral.Enfermeria &&
                                ckbCronico.IsChecked == false)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.EntradaGanadoEnfermeria_CorralDestinoCronico,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                txtCorralDestino.Text = string.Empty;
                            }
                            else
                            {
                                loteDestino = ConsultarLote(corralDestino.CorralID);

                                if (loteDestino != null)
                                {
                                    var fechaDefault = new DateTime(1900, 1, 1);
                                    if (loteDestino.FechaCierre != fechaDefault)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.EntradaGanadoEnfermeria_SeleccionarLoteCerrado,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        txtCorralDestino.Text = string.Empty;
                                        loteDestino = null;
                                        return;
                                    }

                                    if (loteDestino.Cabezas >= corralDestino.Capacidad)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.EntradaGanadoEnfermeria_CorralDestinoSinCapacidad,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        txtCorralDestino.Text = string.Empty;
                                        loteDestino = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloConsultarCorralDestino,
                       MessageBoxButton.OK, MessageImage.Error);
            }

        }
        /// <summary>
        /// Consulta el lote de un corral
        /// </summary>
        /// <param name="corralId"></param>
        /// <returns></returns>
        private LoteInfo ConsultarLote(int corralId)
        {
            LoteInfo loteResultado = null;
            try
            {
                var lotePl = new LotePL();
                var lote = new LoteInfo
                {
                    CorralID = corralId,
                    OrganizacionID = organizacionId
                };
                loteResultado = lotePl.ObtenerPorCorralID(lote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloConsultarLote,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            return loteResultado;
        }

        /// <summary>
        /// Muestra los problemas detectados
        /// </summary>
        private void MostrarProblemas()
        {
            try
            {
                txtProblemaDetectado.Text = string.Empty;
                if (listaProblemas != null)
                {
                    string problemas = string.Join(",",
                                                   listaProblemas.Where(sel => sel.isCheked).Select(
                                                       des => des.Descripcion));
                    if (!string.IsNullOrWhiteSpace(problemas))
                    {
                        txtProblemaDetectado.Text = problemas.TrimEnd(',');
                    }
                }
                Dispatcher.BeginInvoke(new Action(ObtenerTratamiento), DispatcherPriority.Background, null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloMostrarProblema,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Obtiene el historial clinico
        /// </summary>
        private void ObtenerHistorialClinico()
        {
            try
            {
                var enfermeriPl = new EnfermeriaPL();
                if (animalActual != null)
                {
                    if (animalActual.TipoGanado == null)
                    {
                        var sexo = cboSexo.SelectedItem;
                        animalActual.TipoGanado = new TipoGanadoInfo
                        {
                            Sexo = (string)sexo == Sexo.Macho.ToString() ? Sexo.Macho : Sexo.Hembra
                        };
                    }
                    historialClinico = enfermeriPl.ObtenerHistorialClinico(animalActual);
                }
                dgHistorialClinico.ItemsSource = null;
                dgHistorialClinico.ItemsSource = historialClinico;
                lblCostoTotal.Content = enfermeriPl.ObtenerCostoTotal(historialClinico);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloObtenerHistorialClinico,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida si existen productos en tratamientos
        /// </summary>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        private ResultadoValidacion ExistenProductosEnTratamientosSeleccionados(TratamientoInfo tratamiento)
        {
            var resultadoValidacion = new ResultadoValidacion();
            try
            {
                //Se obtiene la lista de productos de los tratamientos seleccionados
                IList<TratamientoInfo> listaTratamientosChecado = listaTratamientos.Where(
                    item => item.Seleccionado && !item.Habilitado
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloExistenProductosEnTratamientosSeleccionados,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            return resultadoValidacion;
        }

        /// <summary>
        /// Selecciona un historial
        /// </summary>
        /// <returns></returns>
        private HistorialClinicoInfo SeleccionarHistorial()
        {
            HistorialClinicoInfo historial = null;
            try
            {
                if (dgHistorialClinico.SelectedIndex == -1)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.EntradaGanadoEnfermeria_SeleccionarHistorial,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                }
                else
                {
                    var row =
                        (DataGridRow)
                        dgHistorialClinico.ItemContainerGenerator.ContainerFromIndex(dgHistorialClinico.SelectedIndex);
                    if (row.IsSelected)
                    {
                        historial = ((HistorialClinicoInfo)(row.Item));
                    }
                }
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloSeleccionarHistorial,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloSeleccionarHistorial,
                       MessageBoxButton.OK, MessageImage.Error);
            }

            return historial;
        }

        /// <summary>
        /// Valida si existen datos en blanco
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion CompruebaCamposEnBlanco()
        {
            var resultado = new ResultadoValidacion();
            try
            {
                if (String.IsNullOrEmpty(txtNoPartida.Text))
                {
                    imgBuscar.Focus();
                    resultado.Resultado = false;
                    return resultado;
                }
                if (String.IsNullOrEmpty(txtNoIndividual.Text))
                {
                    txtNoIndividual.Focus();

                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionNoIndividual;
                    return resultado;
                }
               
                if (String.IsNullOrEmpty(cboSexo.Text) || cboSexo.Text == "Seleccione")
                {
                    cboSexo.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionSexo;
                    return resultado;
                }

                if ((String.IsNullOrEmpty(cboCalidad.Text) || cboCalidad.Text == "Seleccione"))
                {
                    cboCalidad.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionCalidad;
                    return resultado;
                }

                if (String.IsNullOrEmpty(txtPeso.Text))
                {
                    txtPeso.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionPeso;
                    return resultado;
                }
                if (String.IsNullOrEmpty(txtTemperatura.Text))
                {
                    txtTemperatura.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionTemperatura;
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
                    resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionClasificacion;
                    return resultado;
                }

                if ((String.IsNullOrEmpty(cboDetector.Text) || cboDetector.Text.Trim() == "Seleccione"))
                {
                    cboDetector.Focus();
                    resultado.Resultado = false;
                    resultado.Mensaje = Properties.Resources.CorteGanado_RequiereImplentador;
                    return resultado;
                }

                if (ckbCronico.IsChecked == true)
                {
                    if ((String.IsNullOrEmpty(cboCorralDestino.Text) || cboCorralDestino.Text.Trim() == "Seleccione"))
                    {
                        cboCorralDestino.Focus();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionCorral;
                        return resultado;
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(txtCorralDestino.Text))
                    {
                        txtCorralDestino.Focus();
                        resultado.Resultado = false;
                        resultado.Mensaje = Properties.Resources.EntradaGanadoEnfermeria_ValidacionCorral;
                        return resultado;
                    }
                }
                resultado.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloComprobarDatosBlanco,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            return resultado;
        }
        /// <summary>
        /// Almacena el registro de animal movimiento
        /// </summary>
        /// <param name="animalInfo">Informacion del Animal</param>
        /// <param name="usuarioId">Id del Usuario</param>
        /// <returns></returns>
        private AnimalMovimientoInfo ObtenerAnimalMovimiento(AnimalInfo animalInfo, int usuarioId)
        {
            AnimalMovimientoInfo animalMovimientoInfo = null;
            try
            {
                animalMovimientoInfo = new AnimalMovimientoInfo
                {
                    AnimalID = animalInfo.AnimalID,
                    OrganizacionID = organizacionId
                };
                //Se cargan los datos para el Movimiento
                if (corralDestino != null)
                {
                    animalMovimientoInfo.CorralID = corralDestino.CorralID;
                }

                if (txtCorralDestino.Text.Trim() == txtCorralOrigen.Text)
                {
                    animalMovimientoInfo.LoteID = loteOrigen.LoteID;
                    loteDestino = loteOrigen;
                }
                else
                {
                    if (loteDestino != null)
                    {
                        animalMovimientoInfo.LoteID = loteDestino.LoteID;
                    }
                    else
                    {
                        //Sino tiene asignado un Lote Se crea uno
                        GeneraLote();
                    }
                }
                animalMovimientoInfo.FechaMovimiento = DateTime.Now;
                animalMovimientoInfo.Peso = int.Parse(txtPeso.Text);
                if (txtTemperatura.Text != "")
                {
                    animalMovimientoInfo.Temperatura = double.Parse(txtTemperatura.Text.Replace(',', '.'),
                        CultureInfo.InvariantCulture);
                }

                var grado = (GradoInfo)cboGradoEnfermedad.SelectedItem;
                if (grado != null)
                {
                    if (grado.GradoID == (int)GradoEnfermedadEnum.Level2)
                    {
                        animalMovimientoInfo.TipoMovimientoID = (int)TipoMovimiento.EntradaSalidaEnfermeria;
                    }
                    else
                    {
                        animalMovimientoInfo.TipoMovimientoID = (int)TipoMovimiento.EntradaEnfermeria;
                    }
                }

                animalMovimientoInfo.TrampaID = trampaID.TrampaID;
                var detector = (OperadorInfo)cboDetector.SelectedItem;

                if (detector != null)
                {
                    animalMovimientoInfo.OperadorID = detector.OperadorID;
                }

                animalMovimientoInfo.Observaciones = txtObservacion.Text;
                animalMovimientoInfo.Activo = EstatusEnum.Activo;
                animalMovimientoInfo.UsuarioCreacionID = usuarioId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloObtenerAnimalMovimiento,
                       MessageBoxButton.OK, MessageImage.Error);
            }

            return animalMovimientoInfo;
        }
        /// <summary>
        /// Guarda o actualiza los datos del animal
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        private AnimalInfo ObtenerAnimal(int usuarioId)
        {
            AnimalInfo animalInfo = null;
            try
            {
                animalInfo = new AnimalInfo
                {
                    Arete = txtNoIndividual.Text,
                    AreteMetalico = txtAreteTestigo.Text,
                    FechaCompra = dtpFechaRecepcion.DisplayDate.Date
                };

                //Se obtienen los valores para almacenar en Animal
                var tipoGanado = (TipoGanadoInfo)cboTipoGanado.SelectedItem;
                if (tipoGanado != null)
                {
                    animalInfo.TipoGanadoID = tipoGanado.TipoGanadoID;
                    animalInfo.TipoGanado = tipoGanado;
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

                animalInfo.OrganizacionIDEntrada = organizacionId;
                animalInfo.FolioEntrada = int.Parse(txtNoPartida.Text);

                if (ckbCronico.IsChecked != null)
                {
                    animalInfo.Cronico = (bool)ckbCronico.IsChecked;
                }
                else
                {
                    animalInfo.Cronico = false;
                }
                animalInfo.Activo = true;
                animalInfo.UsuarioCreacionID = usuarioId;

                if (animalActual != null)
                {
                    var fechaInicio = new DateTime(1900, 1, 1);
                    if (animalActual.FechaCompra < fechaInicio)
                    {
                        animalInfo.FechaCompra = animalInfo.FechaCompra;
                    }
                    else
                    {
                        animalInfo.FechaCompra = animalActual.FechaCompra;
                    }

                    animalInfo.PesoCompra = animalActual.PesoCompra;
                    animalInfo.PesoLlegada = animalActual.PesoLlegada;
                }
                else
                {
                    animalInfo.PesoCompra = 0;
                    animalInfo.PesoLlegada = 0;

                    //TODO: Validacion por si en algun momento se quiere guardar en 0 el Peso de compra cuando no es de compra directa
                    var entradaGanadoPL = new EntradaGanadoPL();
                    var entradaGanadoInfo = entradaGanadoPL.ObtenerEntradaPorLote(loteOrigen);
                    if (entradaGanadoInfo != null && entradaGanadoInfo.TipoOrganizacionOrigenId != (int)TipoOrganizacion.CompraDirecta)
                    {
                        //if (interfaceSalidoAnimalInfo != null)
                        //{
                        animalInfo.FechaCompra = interfaceSalidoAnimalInfo.FechaCompra;
                        animalInfo.PesoCompra = (int)interfaceSalidoAnimalInfo.PesoCompra;
                        //}
                    }

                    if (interfaceSalidoAnimalInfo != null)
                    {
                        animalInfo.FechaCompra = interfaceSalidoAnimalInfo.FechaCompra;
                        animalInfo.PesoCompra = (int)interfaceSalidoAnimalInfo.PesoCompra;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloObtenerAnimal,
                       MessageBoxButton.OK, MessageImage.Error);
            }

            return animalInfo;
        }
        /// <summary>
        /// Genera un Objeto de Tipo Lote
        /// </summary>
        private void GeneraLote()
        {
            try
            {
                var lotePl = new LotePL();
                LoteInfo loteInfo = null;
                if (loteDestino != null)
                {
                    loteInfo = lotePl.ObtenerPorOrganizacionIdLote(organizacionId, loteDestino.Lote);
                }

                if (loteInfo == null)
                {
                    loteInfo = (loteDestino ?? new LoteInfo());

                    var tipoOrganizacion = ObtenerTiposOrigen(enfermeriaSeleccionada.TipoOrigen);

                    if (tipoOrganizacion == null)
                    {
                        tipoOrganizacion = new TipoOrganizacionInfo
                                           {
                                               TipoProceso = new TipoProcesoInfo()
                                                             {
                                                                 TipoProcesoID = TipoProceso.EngordaPropio.GetHashCode()
                                                             }
                                           };
                    }

                    loteInfo.TipoProcesoID = tipoOrganizacion.TipoProceso.TipoProcesoID;
                    loteInfo.OrganizacionID = organizacionId;
                    loteInfo.UsuarioCreacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);

                    if (corralDestino.CorralID > 0)
                    {
                        if (corralDestino != null)
                        {
                            loteInfo.CorralID = corralDestino.CorralID;
                            loteInfo.TipoCorralID = corralDestino.TipoCorral.TipoCorralID;
                        }
                    }
                    loteInfo.Activo = EstatusEnum.Activo;
                    loteInfo.DisponibilidadManual = false;
                    loteInfo.Cabezas = Convert.ToInt32(0);
                    loteInfo.CabezasInicio = Convert.ToInt32(0);
                }

                loteDestino = loteInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloGenerarLote,
                       MessageBoxButton.OK, MessageImage.Error);
            }
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
        /// Valida si existe un almacen configurado para la trampa
        /// </summary>
        /// <returns></returns>
        private bool ExistenAlmacenParaTrampa()
        {
            var existe = false;
            try
            {
                var parametrosPl = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.AlmacenIDTrampa,
                    Clave = TiposParametrosEnum.AlmacenIDTrampa.ToString(),
                    OrganizacionID = organizacionId
                };
                var parametros = parametrosPl.ParametroObtenerPorTrampaTipoParametroClave(
                                parametroSolicitado,
                                trampaID.TrampaID
                            );

                if (parametros != null && parametros.Count > 0)
                {
                    var almacenPl = new AlmacenPL();
                    almacenInfo = almacenPl.ObtenerPorID(int.Parse(parametros[0].Valor));
                    if (almacenInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format("{0} {1} {2}",
                               Properties.Resources.CorteGanado_NoValidoAlmacenIDTrampas1,
                               trampaID.Descripcion,
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
                    string.Format("{0} {1} {2}",
                                Properties.Resources.CorteGanado_NoExisteAlmacenIDTrampas1,
                                trampaID.Descripcion,
                                Properties.Resources.CorteGanado_NoExisteAlmacenIDTrampas2),
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloExisteAlmacenTrampa,
                       MessageBoxButton.OK, MessageImage.Error);
            }
            return existe;
        }
        /// <summary>
        /// Se valida la existencia de tratmientos
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ComprobarExistenciaTratamientos()
        {
            var resultado = new ResultadoValidacion();
            try
            {
                var tratamientosPl = new TratamientoPL();

                if (listaTratamientos != null)
                {
                    resultado = tratamientosPl.ComprobarExistenciaTratamientos(listaTratamientos,
                        almacenInfo.AlmacenID);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloComprobarExistenciaTratamientos,
                       MessageBoxButton.OK, MessageImage.Error);
            }

            return resultado;
        }

        /// <summary>
        /// Muestra el detalle del historial
        /// </summary>
        private void MostrarDetalleHistorial()
        {
            try
            {
                var historialSeleccionado = SeleccionarHistorial();
                if (historialSeleccionado != null)
                {
                    try
                    {
                        var detalleHistorial = new DetalleHistorialClinico();
                        historialSeleccionado.OrganizacionID = organizacionId;
                        detalleHistorial.Left = (ActualWidth - busquedaPartidas.Width) / 2;
                        detalleHistorial.Top = ((ActualHeight - busquedaPartidas.Height) / 2) + 132;
                        detalleHistorial.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                        detalleHistorial.CargarHistorial(historialSeleccionado);
                        detalleHistorial.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaGanadoEnfermeria_ErrorConsultarDetalle, MessageBoxButton.OK,
                            MessageImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.EntradaGanadoEnfermeria_FalloMostrarDetalleHistorial,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Valida si el nuevo arete esta registrado para otro animal
        /// </summary>
        /// <param name="validarExistenciaArete">verdadero si es necesario realizar la validacion del arete</param>
        /// <param name="Arete">Arete a comprobar existencia</param>
        /// <param name="EsRFID">Indica si se trata de un arete RFID</param>
        /// <param name="Organizacion">Organizacion entrada</param>
        /// <returns></returns>
        private Boolean VerificarExisteciaArete(bool validarExistenciaArete, string Arete, bool EsRFID, int Organizacion)
        {
            Boolean Encontrado = false;
            AnimalPL animalPL = new AnimalPL();

            if (validarExistenciaArete)
            {
                try
                {
                    if (esNuevo)
                    {
                        if (animalPL.VerificarExistenciaArete(string.Empty, txtAreteTestigo.Text.Trim(), Organizacion))   //Se valida existencia de arete metalico
                        {
                            Encontrado = true;
                        }
                    }
                    else
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
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    Encontrado = false;
                }
            }

            return Encontrado;
        }
        #endregion
    }
}