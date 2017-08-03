using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using System.Globalization;
using System.Windows.Threading;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.WinForm.Auxiliar;

using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para SalidaIndividualDeGanado.xaml
    /// </summary>
    public partial class SalidaIndividualDeGanado
    {
        private SerialPortManager _spManager;
        private BasculaCorteSection _configBasculaCorte;
        private int organizacionID;
        private int usuario;
        TrampaInfo trampaInfo = null;
        private string _nombreImpresora;
        private int numeroTicket;
        private string nombreUsuarioSubioGanado;
        private bool basculaConectada;
        private bool _pesoTomado;

        private int _timerTickCount;
        private DispatcherTimer _timer;
        private SKAyuda<ClienteInfo> skAyudaCliente;

        public SalidaIndividualDeGanado()
        {
            InitializeComponent();
            numeroTicket = 0;
            basculaConectada = false;
            _pesoTomado = false;
            
        }

        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarAyudaClientes();
                CargarComboTipoVenta();
                organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
                //_nombreImpresora = ConfigurationManager.AppSettings["nombreImpresora"].Trim();
                _nombreImpresora = AuxConfiguracion.ObtenerConfiguracion().ImpresoraRecepcionGanado;

                txtFecha.Text = DateTime.Now.ToShortDateString();

                var tipoMovimientoPl = new TipoMovimientoPL();
                var tipoMovimientoInfo = tipoMovimientoPl.ObtenerSoloTipoMovimiento((int)TipoMovimiento.SalidaPorVenta);

                if (tipoMovimientoInfo != null)
                {
                    txtSalida.Text = tipoMovimientoInfo.Descripcion;

                    var causa = new CausaSalidaPL();
                    var causaInfo = causa.ObtenerPorID((int)CausaSalidaEnum.VentaEnPie);

                    if (causaInfo != null)
                    {
                        txtCausa.Text = causaInfo.Descripcion;

                        //se valida que existan trampas configuradas
                        if (!ExistenTrampas())
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SalidaIndividualGanado_MensajeNoExistenTrampas,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            DeshabilitarControles(false);
                            return;
                        }

                        InicializarBascula();

                        Limpiar();

                        ObtenerPesoBascula();

                        if (!basculaConectada)
                        {
                            if (_configBasculaCorte != null && !_configBasculaCorte.CapturaManual)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManual,
                                    MessageBoxButton.OK,
                                    MessageImage.Warning);
                                DeshabilitarControles(false);
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SalidaIndividualGanado_MensajeErrorConsultarCausaSalida,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                        DeshabilitarControles(false);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SalidaIndividualGanado_MensajeErrorConsultarTipoMovimiento,
                                MessageBoxButton.OK,
                                MessageImage.Warning);
                    DeshabilitarControles(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaIndividualGanado_MensajeErrorInesperado,
                                  MessageBoxButton.OK,
                                  MessageImage.Warning);
                DeshabilitarControles(false);
            }
        }

        #region Métodos

        private void CargarAyudaClientes()
        {
            skAyudaCliente = new SKAyuda<ClienteInfo>(150,
                false,
                new ClienteInfo
                {

                    ClienteID = 0
                },
                "PropiedadCodigoCliente",
                "PropiedadDescripcionCliente",
                "", false, 50, 9, true)
            {
                AyudaPL = new ClientePL(),
                MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaClienteInvalido,
                MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_AyudaClienteSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaClienteBusqueda,
                MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaClienteSeleccionar,
                TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_AyudaClienteLeyendaBusqueda,
                TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaClienteBusqueda_Titulo,

            };
            StpAyudaCliente.Children.Clear();
            StpAyudaCliente.Children.Add(skAyudaCliente);
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
        /// Metrodo Verificar si existen Trampas configuradas
        /// </summary>
        private bool ExistenTrampas()
        {
            var existeTrampa = false;
            var trampaPl = new TrampaPL();

            try
            {
                var trampaInfo = new TrampaInfo
                {
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                    TipoTrampa = (char)TipoTrampa.Enfermeria,
                    HostName = Environment.MachineName
                };

                var trampaInfoResp = trampaPl.ObtenerTrampa(trampaInfo);

                if (trampaInfoResp != null)
                {
                    this.trampaInfo = trampaInfoResp;
                    existeTrampa = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return existeTrampa;
        }
        /// <summary>
        /// Método que dehabilita o habilita controles.
        /// </summary>
        /// <param name="activado"></param>
        void DeshabilitarControles(bool activado)
        {
            txtFolioTicket.IsEnabled = activado;
            txtPesoTara.IsEnabled = activado;
            txtPesoBruto.IsEnabled = activado;
            btnGuardar.IsEnabled = activado;
            btnImprimir.IsEnabled = activado;
            skAyudaCliente.IsEnabled = activado;
            btnCancelar.IsEnabled = activado;
        }

        /// <summary>
        /// Método con el cual se obtiene el nombre de la organización
        /// </summary>
        /// <returns></returns>
        private string ObtenerNombreOrganizacion()
        {
            var organizacion = new OrganizacionPL();
            OrganizacionInfo organizacioninfo = organizacion.ObtenerPorID(organizacionID);
            return organizacioninfo.Descripcion;
        }

        /// <summary>
        /// Método con el cual se obtiene el Ticket Generado
        /// </summary>
        /// <returns></returns>
        private bool ObtenerFolioTicket()
        {
            decimal pesoTara;
            TipoVentaEnum tipoVenta;
            string idCliente = skAyudaCliente.Clave;
            bool retorno = false;
            var ticket = new SalidaIndividualPL();
            try
            {
                pesoTara = Convert.ToDecimal(txtPesoTara.Value);

                if ((string)cmbTipo.SelectedItem == Properties.Resources.SalidaIndividualDeGanado_cmbTipoExterno)
                {
                    tipoVenta = TipoVentaEnum.Externo;
                }
                else
                {
                    tipoVenta = TipoVentaEnum.Propio;
                }

                var generaticket = new TicketInfo
                                       {
                                           Cliente = idCliente,
                                           Organizacion = organizacionID,
                                           PesoTara = pesoTara,
                                           TipoFolio = tipoVenta == TipoVentaEnum.Propio ? (int) TipoFolio.SalidaIndividualVenta : (int) TipoFolio.VentaGanadoIntensivo,
                                           Usuario = usuario,
                                           TipoVenta = tipoVenta
                                       };

                numeroTicket = ticket.ObtenerTicket(generaticket);
                if (numeroTicket != 0)
                {
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }

        /// <summary>
        /// Método con el cual se valida el folio del ticket ingresado
        /// </summary>
        private bool ValidarFolioTicket()
        {
            if (txtFolioTicket.Text.Trim() != "")
            {
                var venta = new VentaGanadoPL();
                var ventadetalle = new VentaGanadoDetallePL();
                var usuarioPl = new UsuarioPL();
                TicketInfo Ticket = new TicketInfo();

                Ticket.FolioTicket = int.Parse(txtFolioTicket.Text.Trim());
                if ((string)cmbTipo.SelectedItem == Properties.Resources.SalidaIndividualDeGanado_cmbTipoExterno)
                {
                    Ticket.TipoVenta = TipoVentaEnum.Externo;
                }
                else
                {
                    Ticket.TipoVenta = TipoVentaEnum.Propio;
                }
                
                listaAretes.Items.Clear();
                skAyudaCliente.LimpiarCampos();

                Ticket.Organizacion = AuxConfiguracion.ObtenerOrganizacionUsuario();
                VentaGanadoInfo ganado = venta.ObtenerVentaGanadoPorTicket(Ticket);
                if (ganado != null)
                {
                    UsuarioInfo usuarioSubioGanado = usuarioPl.ObtenerPorID(ganado.UsuarioModificacionID);
                    var clientePl = new ClientePL();
                    var cliente = new ClienteInfo { ClienteID = ganado.ClienteID, CodigoSAP = ganado.CodigoSAP };
                    cliente = clientePl.ObtenerClientePorCliente(cliente);
                    skAyudaCliente.Clave = cliente.CodigoSAP;
                    skAyudaCliente.Descripcion = cliente.Descripcion;
                    skAyudaCliente.Info = cliente;
                    txtCorral.Text = ganado.CodigoCorral;

                    var pesoTara = decimal.Parse(ganado.PesoTara.ToString(CultureInfo.InvariantCulture).Replace(".00", "").Replace(",00", ""));
                    txtPesoTara.Text = pesoTara.ToString(CultureInfo.CurrentCulture);

                    txtPesoTara.IsEnabled = false;
                    txtFolioTicket.IsEnabled = false;
                    if (!basculaConectada)
                    {
                        txtPesoBruto.IsEnabled = true;
                    }
                    skAyudaCliente.IsEnabled = false;
                    _pesoTomado = false;
                    ObtenerPesoBascula();
                   
                    List<VentaGanadoDetalleInfo> detalle = ventadetalle.ObtenerVentaGanadoPorTicket(ganado.VentaGanadoID);
                    if (Ticket.TipoVenta == TipoVentaEnum.Propio)
                    {
                        if (detalle != null)
                        {
                       
                                if (detalle.Count > 0)
                                {
                                    lblTotalCabezas.IsEnabled = true;
                                    lblNumeroTotalCabezas.IsEnabled = true;
                                    lblNumeroTotalCabezas.Content = detalle.Count.ToString();
                                    lblNumeroTotalCabezas.Visibility = Visibility.Visible;
                                    lblTotalCabezas.Visibility = Visibility.Visible;
                                }

                                listaAretes.Visibility = Visibility.Visible;
                                cmbTipo.IsEnabled = false;
                                cmbTipo.SelectedIndex = 0;

                                foreach (VentaGanadoDetalleInfo ventaGanadoDetalleInfo in detalle)
                                {
                                    listaAretes.Items.Add(ventaGanadoDetalleInfo);
                                }                            
                                nombreUsuarioSubioGanado = usuarioSubioGanado.Nombre;
                        
                            
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SalidaIndividualGanado_MensajeTicketNoTieneAretes,
                                                MessageBoxButton.OK, MessageImage.Warning);
                            Limpiar();

                            return false;
                        }
                    }
                    else
                    {
                        if (ganado.TotalCabezas > 0)
                        {
                            cmbTipo.SelectedIndex = 1;
                            cmbTipo.IsEnabled = false;
                            listaAretes.Visibility = Visibility.Hidden;

                            lblTotalCabezas.IsEnabled = true;
                            lblNumeroTotalCabezas.IsEnabled = true;
                            lblNumeroTotalCabezas.Content = ganado.TotalCabezas.ToString();
                            lblNumeroTotalCabezas.Visibility = Visibility.Visible;
                            lblTotalCabezas.Visibility = Visibility.Visible;

                            nombreUsuarioSubioGanado = usuarioSubioGanado.Nombre;
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SalidaIndividualGanado_MensajeTicketNoTieneAretes,
                                                MessageBoxButton.OK, MessageImage.Warning);
                            Limpiar();

                            return false;
                        }
                    }
                   
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaIndividualGanado_MensajeFolioTicketInvalido,
                                            MessageBoxButton.OK, MessageImage.Warning);
                    Limpiar();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Método que manda imprimir los ticket
        /// </summary>
        private void Imprimir()
        {
            var print = new PrintDocument();
            print.PrintPage += print_Page;
            print.PrinterSettings.PrinterName = _nombreImpresora;
            print.PrintController = new StandardPrintController();
            print.Print();
        }

        /// <summary>
        /// Método que limpia los controles.
        /// </summary>
        private void Limpiar()
        {
            txtFolioTicket.Text = "";
            txtFolioTicket.IsEnabled = true;
            txtCorral.Text = "";
            txtPesoBruto.Text = "";
            txtPesoNeto.Text = "";
            cmbTipo.IsEnabled = true;
            cmbTipo.SelectedIndex = 0;
            if (!_pesoTomado)
            {
                txtPesoTara.Text = "";
            }
            skAyudaCliente.LimpiarCampos();
            nombreUsuarioSubioGanado = "";
            lblNumeroTotalCabezas.Content = "";
            lblNumeroTotalCabezas.Visibility = Visibility.Hidden;
            lblTotalCabezas.Visibility = Visibility.Hidden;
            listaAretes.Items.Clear();
            btnGuardar.IsEnabled = false;

            if (!basculaConectada)
            {
                txtPesoTara.IsEnabled = true;
                txtPesoBruto.IsEnabled = false;
            }
            else
            {
                txtPesoTara.IsEnabled = false;
                txtPesoBruto.IsEnabled = false;
            }

            skAyudaCliente.IsEnabled = true;
            txtFolioTicket.Focus();

            ObtenerPesoBascula();
        }

        private bool ValidarCliente()
        {
            var retorno = false;

            if (skAyudaCliente.Clave.Trim() != "")
            {
                var clientePL = new ClientePL();
                var clienteInfo = new ClienteInfo();
                clienteInfo.CodigoSAP = skAyudaCliente.Clave;
                clienteInfo = clientePL.ObtenerClientePorCliente(clienteInfo);
                retorno = true;
                if (clienteInfo == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaIndividualGanado_MensajeClienteInvalido,
                        MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaCliente.AsignarFoco();
                    retorno = false;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SalidaIndividualGanado_MensajeValidacionCliente,
                           MessageBoxButton.OK, MessageImage.Warning);
                skAyudaCliente.AsignarFoco();
            }

            return retorno;
        }

        private bool ValidarPesoTara()
        {
            var retorno = false;
            if (txtPesoTara.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtPesoTara.Value) > 0)
                {
                    retorno = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaIndividualGanado_MensajePesoMayorACero,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (!basculaConectada)
                    {
                        txtPesoTara.Focus();
                    }
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaIndividualGanado_MensajePesoMayorACero,
                    MessageBoxButton.OK, MessageImage.Warning);
                if (!basculaConectada)
                {
                    txtPesoTara.Focus();
                }
            }

            return retorno;
        }

        private bool ValidarPesoBruto()
        {
            decimal pesoNeto = 0, pesoBruto = 0, pesoTara = 0;
            var retorno = false;

            
            if (txtPesoBruto.Text.Trim() != "")
            {
                pesoBruto = Convert.ToDecimal(txtPesoBruto.Value); //decimal.Parse(txtPesoBruto.Text.Replace(".00", "").Replace(",00", ""), CultureInfo.InvariantCulture);
                pesoTara = Convert.ToDecimal(txtPesoTara.Value); //decimal.Parse(txtPesoTara.Text.Replace(".00", "").Replace(",00", ""), CultureInfo.InvariantCulture);
                if (pesoBruto > 0)
                {
                    if (pesoBruto > pesoTara)
                    {
                        pesoNeto = pesoBruto - pesoTara;
                        txtPesoNeto.Text = pesoNeto.ToString(CultureInfo.CurrentCulture); 

                        retorno = true;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaIndividualGanado_MensajePesoBrutoMayorAPesoTara,
                            MessageBoxButton.OK, MessageImage.Warning);
                        if (!basculaConectada)
                        {
                            txtPesoBruto.Focus();
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaIndividualGanado_MensajePesoBrutoMayorACero,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (!basculaConectada)
                    {
                        txtPesoBruto.Focus();
                    }
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaIndividualGanado_MensajePesoBrutoMayorACero,
                    MessageBoxButton.OK, MessageImage.Warning);
                if (!basculaConectada)
                {
                    txtPesoBruto.Focus();
                }
            }

            return retorno;
        }

        /// <summary>
        /// evento closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            try
            {
            
                if (basculaConectada)
                {
                    _spManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

		}

        /// <summary>
        /// Carga los elementos de combobox
        /// </summary>
        public void CargarComboTipoVenta()
        {
            try
            {
                IList<TipoVentaEnum> tipoVentaEnums = Enum.GetValues(typeof(TipoVentaEnum)).Cast<TipoVentaEnum>().ToList();
                var lsTipoVenta = new Dictionary<int, string>();

                lsTipoVenta.Add(1, Properties.Resources.SalidaIndividualDeGanado_cmbTipoPropio.ToString());
                lsTipoVenta.Add(2, Properties.Resources.SalidaIndividualDeGanado_cmbTipoExterno.ToString());
                
                cmbTipo.ItemsSource = lsTipoVenta.Values;
                cmbTipo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion

        #region Bascula

        

        //Obtener Peso De La Bascula.
        private void ObtenerPesoBascula()
        {
            try
            {
                if (!_pesoTomado)
                {
                    if (_configBasculaCorte != null)
                    {
                        if (_spManager != null)
                            _spManager.StartListening(_configBasculaCorte.Puerto,
                                _configBasculaCorte.Baudrate,
                                _configBasculaCorte.Paridad,
                                _configBasculaCorte.Databits,
                                _configBasculaCorte.BitStop);

                        _timer = new DispatcherTimer();
                        _timer.Interval = new TimeSpan(0, 0, 1);
                        _timer.Tick += (Timer_Tick);
                        //Una vez creadas las instancias de el SerialPorts se ejecuta el timer
                        _timer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Método que inicializa los datos de la bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                var parametrosPL = new ConfiguracionParametrosPL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int) TiposParametrosEnum.DispositivoBascula,
                    OrganizacionID = organizacionID
                };
                var parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado,
                    trampaInfo.TrampaID);
                _configBasculaCorte = ObtenerParametroDispositivo(parametros);

                _spManager = new SerialPortManager();
                _spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);

                if (_spManager != null)
                    _spManager.StartListening(_configBasculaCorte.Puerto,
                        _configBasculaCorte.Baudrate,
                        _configBasculaCorte.Paridad,
                        _configBasculaCorte.Databits,
                        _configBasculaCorte.BitStop);
                _spManager.StopListening();
                txtPesoTara.IsEnabled = false;
                txtPesoBruto.IsEnabled = false;
                basculaConectada = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Error(ex);
                /*_spManager.Dispose();
                if (_spManager != null)
                    _spManager.StartListening(_configBasculaCorte.Puerto,
                        _configBasculaCorte.Baudrate,
                        _configBasculaCorte.Paridad,
                        _configBasculaCorte.Databits,
                        _configBasculaCorte.BitStop);
                _spManager.StopListening();
                txtPesoTara.IsEnabled = false;
                txtPesoBruto.IsEnabled = false;
                basculaConectada = true;*/
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaIndividualGanado_MensajeErrorBascula,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                var timer = (DispatcherTimer) sender;
                if (_timerTickCount++ != _configBasculaCorte.Espera) return;
                timer.Stop();
                Dispatcher.Invoke(new Action(() =>
                {
                    if (!String.IsNullOrEmpty(txtDisplayPeso.Text))
                    {
                        if (txtFolioTicket.Text.Trim() == "" || txtPesoTara.Text.Trim() == "")
                        {
                            txtPesoTara.Text = txtDisplayPeso.Text.Replace(".00", "").Replace(",00", "");
                        }
                        else
                        {
                            decimal pesoNeto = 0, pesoBruto = 0, pesoTara = 0;
                            pesoBruto = decimal.Parse(txtDisplayPeso.Text.Replace(".00", "").Replace(",00", ""));
                            txtPesoBruto.Text = pesoBruto.ToString(CultureInfo.CurrentCulture);
                            // .ToString("N", CultureInfo.CurrentCulture);

                            /*pesoBruto = decimal.Parse(txtPesoBruto.Text.Replace(".00", "").Replace(",00", ""),
                                CultureInfo.InvariantCulture);*/

                            pesoTara = decimal.Parse(txtPesoTara.Text.Replace(".00", "").Replace(",00", ""),
                                CultureInfo.InvariantCulture);

                            pesoNeto = pesoBruto - pesoTara;
                            txtPesoNeto.Text = pesoNeto.ToString(CultureInfo.CurrentCulture);
                        }
                    }
                    if (_spManager != null)
                    {
                        _spManager.StopListening();
                    }
                }), null);
                _pesoTomado = true;
                _timerTickCount = 0;
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
        void spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            string strEnd = "", peso = "";
            double val;
            try
            {
                strEnd = _spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    peso = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txtDisplayPeso.Text = peso;
                    }), null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        #region Botones

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFolioTicket.Text.Trim() == "" && txtPesoBruto.Text.Trim() == "")
                {
                    if (ValidarCliente())
                    {
                        if (ValidarPesoTara())
                        {
                            if (ObtenerFolioTicket())
                            {
                                Imprimir();
                                _pesoTomado = false;
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    String.Format(Properties.Resources.SalidaIndividualGanado_MensajeImpresionTicket, numeroTicket.ToString(CultureInfo.InvariantCulture)),
                                    MessageBoxButton.OK, MessageImage.Correct);
                                Limpiar();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.SalidaIndividualGanado_MensajeErrorAlImprimir,
                                    MessageBoxButton.OK, MessageImage.Warning);
                                if (!basculaConectada)
                                {
                                    txtPesoTara.Focus();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ValidarPesoTara())
                    {
                        if (ValidarPesoBruto())
                        {
                            Imprimir();
                            btnGuardar.IsEnabled = true;
                            btnGuardar.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            int folioTicket, numeroDeCabezas;
            decimal pesoNeto, peso, pesoBruto;
            string corral= string.Empty;
            TipoVentaEnum tipoVenta;

            if (txtFolioTicket.Text.Trim() != string.Empty)
            {
                var salida = new SalidaIndividualPL();
                folioTicket = int.Parse(txtFolioTicket.Text.Trim());
                pesoNeto = Convert.ToDecimal(txtPesoNeto.Value);
                pesoBruto = Convert.ToDecimal(txtPesoBruto.Value);
                numeroDeCabezas = int.Parse(lblNumeroTotalCabezas.Content.ToString().Trim());
                peso = pesoNeto / numeroDeCabezas;
                corral = txtCorral.Text;

                if ((string)cmbTipo.SelectedItem == Properties.Resources.SalidaIndividualDeGanado_cmbTipoExterno)
                {
                    tipoVenta = TipoVentaEnum.Externo;
                }
                else
                {
                    tipoVenta = TipoVentaEnum.Propio;
                }
                
                try
                {
                    var salidaIndividual = new SalidaIndividualInfo
                                               {
                                                   FolioTicket = folioTicket,
                                                   PesoBruto = pesoBruto,
                                                   Peso = peso,
                                                   NumeroDeCabezas = numeroDeCabezas,
                                                   Organizacion = organizacionID,
                                                   Usuario = usuario,
                                                   TipoVenta = tipoVenta,
                                                   Corral = corral
                                               };

                    
                    MemoryStream stream = salida.GuardarSalidaIndividualGanadoVenta(salidaIndividual);
                  
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SalidaIndividualGanado_MensajeGuardadoOK,
                                           MessageBoxButton.OK, MessageImage.Correct);

                    try
                    {
                        if (stream != null)
                        {
                            var exportarPoliza = new ExportarPoliza();
                            exportarPoliza.ImprimirPoliza(stream,
                                                          string.Format("{0} {1}", "Poliza Salida Folio No.",
                                                                        folioTicket));
                        }
                    }
                    catch
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SalidaIndividualGanado_MensajeErrorAlImprimirPoliza,
                                           MessageBoxButton.OK, MessageImage.Warning);
                    }

                    _pesoTomado = false;
                    Limpiar();
                }
                catch (ExcepcionServicio ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      ex.Message, MessageBoxButton.OK, MessageImage.Stop);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SalidaIndividualGanado_MensajeErrorAlGuardar,
                                           MessageBoxButton.OK, MessageImage.Warning);
                    _pesoTomado = false;
                    Limpiar();
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Cancelarcaptura_CorteGanado,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                _pesoTomado = false;
                Limpiar();
            }
        }

        #endregion

        #region Impresion

        private void print_Page(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblHora, new Font("lucida console", 8), Brushes.Black, 10, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortTimeString(), new Font("lucida console", 8), Brushes.Black, 50, 10);
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblFecha, new Font("lucida console", 8), Brushes.Black, 140, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), new Font("lucida console", 8), Brushes.Black, 190, 10);

            // Organizacion
            e.Graphics.DrawString(ObtenerNombreOrganizacion(), new Font("lucida console", 10), Brushes.Black, 10, 40);

            //Ticket
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblTicket, new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 10, 65);
            if (txtFolioTicket.Text.Trim() == "")
            {
                e.Graphics.DrawString(numeroTicket.ToString(), new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 65);
            }
            else
            {
                e.Graphics.DrawString(txtFolioTicket.Text.Trim(), new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 65);
            }

            //Concepto
            string concepto = (string)cmbTipo.SelectedItem == Properties.Resources.SalidaIndividualDeGanado_cmbTipoPropio ?
                                Properties.Resources.SalidaIndividualGanado_lblConcepto : Properties.Resources.SalidaIndividualGanado_lblConceptoGanadoIntensivo;
            e.Graphics.DrawString(concepto, new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 10, 100);

            //Cliente
            string cliente = String.Format("{0} {1}", 
                Properties.Resources.SalidaIndividualGanado_lblClienteImpresion, 
                skAyudaCliente.Descripcion);
            e.Graphics.DrawString(cliente, new Font("lucida console", 10), Brushes.Black, 10, 120);

            //Peso Tara
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblPTara, new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 10, 150);
            e.Graphics.DrawString(txtPesoTara.Text.Trim().PadLeft(15), new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 110, 150);

            if (txtFolioTicket.Text.Trim() != "")
            {
                //Peso Bruto
                e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblPBruto,
                    new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 10, 170);
                e.Graphics.DrawString(txtPesoBruto.Text.Trim().PadLeft(15),
                    new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 110, 170);
                //Peso Neto
                e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblPNeto,
                    new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 10, 190);
                e.Graphics.DrawString(txtPesoNeto.Text.Trim().PadLeft(15),
                    new Font("lucida console", 12, System.Drawing.FontStyle.Bold), Brushes.Black, 110, 190);
            }
            //Registro
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblRegistro, new Font("lucida console", 9), Brushes.Black, 10, 230);
            e.Graphics.DrawString(Application.Current.Properties["Nombre"].ToString(), new Font("lucida console", 9), Brushes.Black, 90, 230);
            e.Graphics.DrawString(nombreUsuarioSubioGanado, new Font("lucida console", 9), Brushes.Black, 90, 250);
        }

        #endregion

        #region Eventos

        private void txtFolioTicket_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void txtPesoTara_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void txtPesoBruto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        
        private void txtFolioTicket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarFolioTicket())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPesoTara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarPesoTara())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPesoBruto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarPesoBruto())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPesoBruto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            txtPesoNeto.Text = "";
        }

        private void txtNumeros_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        
        private void cmbTipo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((string)cmbTipo.SelectedItem == Properties.Resources.SalidaIndividualDeGanado_cmbTipoExterno)
            {
                listaAretes.Visibility = Visibility.Hidden;
            }
            else
            {
                if (listaAretes != null)
                {
                    listaAretes.Visibility = Visibility.Visible;
                }
            }
        }

        

        #endregion
    }
}
