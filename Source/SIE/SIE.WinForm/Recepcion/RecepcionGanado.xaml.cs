using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.Impresora;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for RecepcionGanado.xaml
    /// </summary>
    public partial class RecepcionGanado
    {
        #region CONSTRUCTORES

        public RecepcionGanado()
        {
            InitializeComponent();
            InicializaContexto();
            AgregarControles();
            DeshabilitarControles();
            ObtenerTiposOrigen();
        }

        #endregion CONSTRUCTORES

        #region PROPIEDADES

        private EntradaEmbarqueInfo EntradaEmbarque { get; set; }

        private EntradaGanadoInfo EntradaGanado
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (EntradaGanadoInfo) DataContext;
            }
            set { DataContext = value; }
        }

        private List<EmbarqueDetalleInfo> lsEscalas { get; set; }

        #endregion PROPIEDADES

        #region VARIABLES

        const int CAPTURAR_PESAJES_UNIFICADOS = -3;
        const int FALTAN_PESOS_BRUTOS = -1;
        const int NO_EXISTE_DETALLE_PESO_TARA = -2;
        const int PESOS_VALIDOS_RUTEO = 1;

        private SerialPortManager spManager;

        private int organizacionID;
        private bool validarRuteo;

        private SKAyuda<ChoferInfo> skAyudaChofer;
        private SKAyuda<CorralInfo> skAyudaCorral;
        private SKAyuda<EntradaGanadoInfo> skAyudaFolio;
        private SKAyuda<OrganizacionInfo> skAyudaOrigen;
        private SKAyuda<OperadorInfo> skAyudaQuienRecibe;
        private SKAyuda<CamionInfo> skAyudaCamion;
        private SKAyuda<JaulaInfo> skAyudaJaula;

        private IList<ChoferInfo> choferes;
        private IList<CamionInfo> camiones;
        private IList<OrganizacionInfo> organizaciones;
        private IList<OperadorInfo> operadores;

        #endregion VARIABLES

        #region METODOS

        /// <summary>
        /// Deshabilita Ciertos Controles
        /// al Inicio
        /// </summary>
        private void DeshabilitarControles()
        {
            txtCabezasOrigen.IsReadOnly = true;
            txtPesoBruto.IsReadOnly = true;
            txtPesoTara.IsReadOnly = true;
            txtPesoNeto.IsReadOnly = true;

            btnImpresionTicket.IsEnabled = false;
            btnCondicionesGanado.IsEnabled = false;
        }

        private void DeshabilitarEdicion(bool habilitar)
        {
            skAyudaChofer.IsEnabled = habilitar;
            skAyudaFolio.IsEnabled = habilitar;
            skAyudaOrigen.IsEnabled = habilitar;
            skAyudaQuienRecibe.IsEnabled = habilitar;
            txtFolioEmbarque.IsEnabled = habilitar;
            btnBuscarFolioEmbarque.IsEnabled = habilitar;
            txtSalida.IsEnabled = habilitar;
            cmbFechaSalida.IsEnabled = habilitar;
            dtuHoraSalida.IsEnabled = habilitar;
            skAyudaCamion.IsEnabled = habilitar;
            skAyudaJaula.IsEnabled = habilitar;
            btnCapturarPesoBruto.IsEnabled = habilitar;
        }

        /// <summary>
        /// Inicializa el Contexto de la Pantalla
        /// </summary>
        private void InicializaContexto()
        {
            EntradaGanado =
                new EntradaGanadoInfo
                    {
                        FechaEntrada = DateTime.Now,
                        FechaSalida = DateTime.Now,
                        Lote = new LoteInfo(),
                    };

            dtuHoraSalida.Text = DateTime.Now.ToShortTimeString();
            dtuHoraRecepcion.Text = DateTime.Now.ToShortTimeString();

            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
        }

        /// <summary>
        /// Agrega Controles de Ayuda
        /// </summary>
        private void AgregarControles()
        {
            AgregarAyudaOrigen();
            AgregarAyudaChofer();
            AgregarAyudaQuienRecibe();
            AgregarAyudaCorral();
            AgregarAyudaFolio();
            AgregarAyudaCamion();
            AgregarAyudaJaula();
        }

        /// <summary>
        /// Agrega control de Ayuda para Jaula
        /// </summary>
        private void AgregarAyudaJaula()
        {
            skAyudaJaula = new SKAyuda<JaulaInfo>(221, true,
                                                  new JaulaInfo { Proveedor = new ProveedorInfo { ProveedorID = 0 } },
                                                  "PropiedadClaveEntradaGanado"
                                                  , "PropiedadDescripcionEntradaGanado", false, 0, false)
            {
                AyudaPL = new JaulaPL(),
                MensajeClaveInexistente = Properties.Resources.Jaula_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Jaula_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Jaula_Busqueda,
                MensajeAgregar = Properties.Resources.Jaula_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaJaula,
                TituloPantalla = Properties.Resources.BusquedaJaula_Titulo,
            };
            skAyudaJaula.AsignaTabIndex(8);
            skAyudaJaula.LlamadaMetodos += IniciarProveedorAyudaJaula;
            stpJaula.Children.Add(skAyudaJaula);
        }

        /// <summary>
        /// Agrega control de Ayuda para Jaula
        /// </summary>
        private void AgregarAyudaCamion()
        {
            skAyudaCamion = new SKAyuda<CamionInfo>(221, true,
                                                  new CamionInfo {Proveedor = new ProveedorInfo {ProveedorID = 0}},
                                                  "PropiedadClaveEntradaGanado"
                                                  , "PropiedadDescripcionEntradaGanado", false, 0, false)
                               {
                                   AyudaPL = new CamionPL(),
                                   MensajeClaveInexistente = Properties.Resources.Camion_CodigoInvalido,
                                   MensajeBusquedaCerrar = Properties.Resources.Camion_SalirSinSeleccionar,
                                   MensajeBusqueda = Properties.Resources.Camion_Busqueda,
                                   MensajeAgregar = Properties.Resources.Camion_Seleccionar,
                                   TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCamion,
                                   TituloPantalla = Properties.Resources.BusquedaCamion_Titulo,
                               };
            skAyudaCamion.AsignaTabIndex(7);
            skAyudaCamion.LlamadaMetodos += IniciarProveedorAyudaJaula;
            stpTracto.Children.Add(skAyudaCamion);
        }

        /// <summary>
        /// Agrega control de Ayuda para Folio
        /// </summary>
        private void AgregarAyudaFolio()
        {
            skAyudaFolio = new SKAyuda<EntradaGanadoInfo>(100, true, new EntradaGanadoInfo(),
                                                          "PropiedadClaveEntradaGanado"
                                                          , "PropiedadDescripcionEntradaGanado"
                                                          , "PropiedadOcultaEntradaGanado", false, true)
                {
                    AyudaPL = new EntradaGanadoPL(),
                    MensajeClaveInexistente = Properties.Resources.Folio_Inexistente,
                    MensajeBusquedaCerrar = Properties.Resources.Folio_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Folio_Busqueda,
                    MensajeAgregar = Properties.Resources.Folio_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaFolio,
                    TituloPantalla = Properties.Resources.BusquedaEntradaGanado_Titulo,
                };
            IList<IDictionary<IList<string>, object>> dependencias = ObtenerDependenciasOrganizacion();
            skAyudaFolio.Dependencias = null;
            skAyudaFolio.Dependencias = dependencias;

            skAyudaFolio.MensajeDependencias = null;
            IDictionary<string, string> mensajeDependencias = new Dictionary<string, string>();
            mensajeDependencias.Add("OrganizacionID", Properties.Resources.RecepcionGanado_SeleccionarTipoOrganizacion);
            skAyudaFolio.MensajeDependencias = mensajeDependencias;

            skAyudaFolio.LlamadaMetodos += BuscarFolioEntrada;
            skAyudaFolio.AsignaTabIndex(0);

            stpFolio.Children.Add(skAyudaFolio);
        }

        /// <summary>
        /// Agrega control de Ayuda para Origen
        /// </summary>
        private void AgregarAyudaOrigen()
        {
            skAyudaOrigen = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo {OrganizacionID = 0}
                                                          , "PropiedadClaveEntradaGanado"
                                                          , "PropiedadDescripcionEntradaGanado", true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.Origen_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.Origen_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Origen_Busqueda,
                    MensajeAgregar = Properties.Resources.Origen_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrigen,
                    TituloPantalla = Properties.Resources.BusquedaOrigen_Titulo,
                    TipoCampoCodigo = TipoCampo.LetraNumeroPuntoComaGuion,
                };
            AsignaDependenciasAyudaOrigen();
            skAyudaOrigen.LlamadaMetodos += ObtenerEntradaGanadoPorProgramacionEmbarqueOrganizacionOrigen;
            skAyudaOrigen.AsignaTabIndex(2);

            stpOrigen.Children.Clear();
            stpOrigen.Children.Add(skAyudaOrigen);
        }

        /// <summary>
        /// Agrega control de Ayuda para Chofer
        /// </summary>
        private void AgregarAyudaChofer()
        {
            skAyudaChofer = new SKAyuda<ChoferInfo>(160, false, new ChoferInfo(), "PropiedadClaveEntradaGanado"
                                                    , "PropiedadDescripcionEntradaGanado", true)
                {
                    AyudaPL = new ChoferPL(),
                    MensajeClaveInexistente = Properties.Resources.Chofer_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.Chofer_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Chofer_Busqueda,
                    MensajeAgregar = Properties.Resources.Chofer_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                    TituloPantalla = Properties.Resources.BusquedaChofer_Titulo,
                };
            skAyudaChofer.AsignaTabIndex(6);
            stpChofer.Children.Add(skAyudaChofer);
        }

        /// <summary>
        /// Agrega control de Ayuda para Quien Recibe
        /// </summary>
        private void AgregarAyudaQuienRecibe()
        {            
            skAyudaQuienRecibe = new SKAyuda<OperadorInfo>(160, false,
                                                           new OperadorInfo
                                                               {
                                                                   Organizacion =
                                                                       new OrganizacionInfo
                                                                           {OrganizacionID = organizacionID},
                                                                   Rol = new RolInfo { RolID = Roles.Basculista.GetHashCode() }
                                                               }
                                                           , "PropiedadClaveEntradaGanado",
                                                           "PropiedadDescripcionEntradaGanado"
                                                           , true)
                                     {
                                         AyudaPL = new OperadorPL(),
                                         MensajeClaveInexistente = Properties.Resources.QuienRecibe_CodigoInvalido,
                                         MensajeBusquedaCerrar = Properties.Resources.QuienRecibe_SalirSinSeleccionar,
                                         MensajeBusqueda = Properties.Resources.QuienRecibe_Busqueda,
                                         MensajeAgregar = Properties.Resources.QuienRecibe_Seleccionar,
                                         TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                                         TituloPantalla = Properties.Resources.BusquedaOperador_Titulo,
                                     };
            skAyudaQuienRecibe.AsignaTabIndex(9);

            stpQuienRecibe.Children.Add(skAyudaQuienRecibe);
        }

        /// <summary>
        /// Agrega control de Ayuda para Corral
        /// </summary>
        private void AgregarAyudaCorral()
        {
            var corral =
                new CorralInfo
                    {
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionID },
                        TipoCorral = new TipoCorralInfo
                                         {
                                             TipoCorralID = TipoCorral.Intensivo.GetHashCode()
                                         }
                    };
            bool mostrarLupa = EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode();
            skAyudaCorral =
                new SKAyuda<CorralInfo>(60, true, mostrarLupa, corral,
                                        "PropiedadClaveEntradaGanado", 
                                        "PropiedadDescripcionEntradaGanado",
                                        false)
                    {
                        AyudaPL = new CorralPL(),
                        MensajeClaveInexistente = Properties.Resources.Corral_Inexistente,
                        MensajeBusquedaCerrar = Properties.Resources.Corral_SalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.Corral_Busqueda,
                        MensajeAgregar = Properties.Resources.Corral_Seleccionar,
                        TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCorral,
                        TituloPantalla = Properties.Resources.BusquedaCorral_Titulo,
                    };
            skAyudaCorral.IsEnabled = mostrarLupa;
            skAyudaCorral.LlamadaMetodos += VerificaCorralRuteo;
            skAyudaCorral.AsignaTabIndex(16);
            stpCorral.Children.Clear();
            stpCorral.Children.Add(skAyudaCorral);
            lblCorralRequerida.Visibility = mostrarLupa ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Inicializa el proveedor en Jaula,
        /// para obtener todas las Jaulas en
        /// consulta de ayuda
        /// </summary>
        private void IniciarProveedorAyudaJaula()
        {
            var proveedor = new ProveedorInfo {ProveedorID = 0};
            skAyudaJaula.Info.Proveedor = proveedor;
            skAyudaCamion.Info.Proveedor = proveedor;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado en Caso de que ya se
        /// capturara con anterioridad el Folio de Entrada y Organizacion
        /// </summary>
        private void ObtenerEntradaGanadoPorProgramacionEmbarqueOrganizacionOrigen()
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                int organizacionOrigenID;
                int.TryParse(skAyudaOrigen.Clave, out organizacionOrigenID);

                EntradaGanadoInfo entradaGanado;
                ValidarConfiguracionProrrateoRuteo();                
                decimal pesoTara = 0;
                List<EntradaGanadoInfo> entradasGanado = null;
                if (chkRuteo.IsChecked.Value && validarRuteo)
                {
                    decimal pesoBruto = 0;
                    entradasGanado =
                        entradaGanadoPL.ObtenerEntradasPorEmbarqueID(EntradaGanado.EmbarqueID);
                    if (entradasGanado != null)
                    {
                        entradaGanado = entradasGanado.OrderBy(fecha => fecha.FechaEntrada).FirstOrDefault();
                        pesoBruto = entradaGanado.PesoBruto;
                        if (entradasGanado.Count(tara => tara.PesoTara > 0) == 1)
                        {
                            pesoTara =
                                entradasGanado.Where(tara => tara.PesoTara > 0).Select(tara => tara.PesoTara).
                                    FirstOrDefault();
                        }
                        else
                        {
                            pesoTara =
                                entradasGanado.Where(
                                    tara => tara.PesoTara > 0 && tara.EntradaGanadoID == EntradaGanado.EntradaGanadoID).
                                    Select(tara => tara.PesoTara).
                                    FirstOrDefault();
                        }
                        if (pesoBruto > 0)
                        {
                            btnCapturarPesoBruto.IsEnabled = false;
                            btnImpresionTicket.IsEnabled = true;
                        }
                    }
                    EntradaGanado.PesoBruto = pesoBruto;
                }
                entradaGanado = entradaGanadoPL.ObtenerPorEmbarqueID(EntradaEmbarque.EmbarqueID,
                                                                     organizacionOrigenID);
                txtSalida.Text = "0";
                if (entradaGanado != null)
                {
                    if (entradaGanado.EntradaGanadoID > 0)
                    {
                        EntradaGanado = entradaGanado;
                        ProgramacionEntradaGanadoEmbarque(entradaGanado);
                    }
                }
                if (EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
                {
                    if (EntradaGanado.CorralID == 0)
                    {
                        AgregarAyudaCorral();
                    }
                }
                if (pesoTara > 0)
                {
                    btnCapturarPesoTara.IsEnabled = false;
                    EntradaGanado.PesoTara = pesoTara;
                }
                if (entradasGanado != null && entradasGanado.Any(imp => imp.ImpresionTicket))
                {
                    btnGuardar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarEntradaGanado, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ProgramacionEntradaGanadoEmbarque(EntradaGanadoInfo entradaGanado)
        {
            skAyudaFolio.Descripcion =
                EntradaGanado.FolioEntrada.ToString(CultureInfo.InvariantCulture);
            EmbarqueInfo programacionEmbarque = ObtenerProgramacionEmbarque();
            txtCheckList.Text = entradaGanado.CheckList.Trim();
            if (programacionEmbarque != null)
            {
                ValidaProgramacionEmbarque(programacionEmbarque);
            }
            bool tieneCondiciones = EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode() ||
                                    VerificarCondiciones();
            if (tieneCondiciones)
            {
                VerificaFolioCompleto();
                ObtenerLoteCorralRuteo();
                AsignaCamposRequeridos(true);
            }
            else
            {
                txtFolioEmbarque.Focus();
            }
        }

        /// <summary>
        /// Valida que se hayan capturado condiciones de ganado
        /// </summary>
        private bool VerificarCondiciones()
        {
            var resultado = true;
            if (EntradaGanado.EntradaGanadoID > 0)
            {
                if (EntradaGanado.ListaCondicionGanado == null || !EntradaGanado.ListaCondicionGanado.Any())
                {
                    resultado = false;
                    LimpiarPantalla(false);
                    InicializaContexto();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_CapturarCondiciones, MessageBoxButton.OK,
                                      MessageImage.Stop);
                }
            }            
            return resultado;
        }

        /// <summary>
        /// Valida si el Corral Seleccionado
        /// ya se ha usado para Ruteo, en caso de ser
        /// asi se utilizara el mismo lote
        /// </summary>
        private void VerificaCorralRuteo()
        {
            if (Convert.ToBoolean(chkRuteo.IsChecked))
            {
                try
                {
                    var corralPL = new CorralPL();
                    int corralID;

                    int.TryParse(skAyudaCorral.Clave, out corralID);

                    bool esUsandoEnRuteo = corralPL.CorralSeleccionadoParaRuteo(EntradaGanado.EmbarqueID, corralID);
                    if (esUsandoEnRuteo)
                    {
                        ObtenerLoteCorralRuteo();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_ErrorCorralRuteo, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Obtiene el Lote que se uso para el corral
        /// cuando la Entrada de Ganado es por Ruteo
        /// </summary>
        private void ObtenerLoteCorralRuteo()
        {
            try
            {
                int corralID;
                int.TryParse(skAyudaCorral.Clave, out corralID);

                if (corralID > 0)
                {
                    var lotePL = new LotePL();
                    LoteInfo lote = lotePL.ObtenerPorIdOrganizacionId(organizacionID, corralID,
                                                                      EntradaEmbarque.EmbarqueID);
                    if (lote != null)
                    {
                        EntradaGanado.Lote = lote;
                        txtLote.Text = Convert.ToString(lote.Lote);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Deshabilita controles
        /// </summary>
        private void DeshabilitaControlesGuardar()
        {
            if (EntradaGanado != null
                && EntradaGanado.EntradaGanadoID > 0
                && EntradaGanado.ImpresionTicket
                && EntradaGanado.PesoTara > 0
                && EntradaGanado.PesoBruto > 0
                && btnGuardar.IsEnabled)
            {
                btnCapturarPesoTara.IsEnabled = false;
                btnCapturarPesoBruto.IsEnabled = false;
                btnCondicionesGanado.IsEnabled = false;
                btnImpresionTicket.IsEnabled = false;
            }
        }

        /// <summary>
        /// Valida si el Peso es Valido
        /// para ser Capturado
        /// </summary>
        /// <param name="textPeso"></param>
        private bool CapturaPeso(TextBox textPeso)
        {
            bool resultado = true;

            decimal peso;
            decimal.TryParse(txtDisplay.Text, out peso);

            if (peso > 0)
            {
                textPeso.Text = peso.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                resultado = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_PesoValido, MessageBoxButton.OK,
                                  MessageImage.Error);
            }

            return resultado;
        }

        /// <summary>
        /// Valida Campos para Impresion de
        /// Ticket con Peso Bruto
        /// </summary>
        /// <returns></returns>
        private bool ValidaImpresionPesoBruto()
        {
            bool resultado = true;

            bool esCompraDirecta = EsCompraDirecta();

            bool salidaRequerida = EsSalidaRequerida();

            if (salidaRequerida)
            {
                if (txtSalida.Text == string.Empty || Convert.ToInt32(txtSalida.Text) <= 0)
                {
                    resultado = false;
                }
            }

            if (cmbTipoOrigen.SelectedIndex < 0)
            {
                resultado = false;
            }

            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Descripcion))
            {
                resultado = false;
            }

            if (!esCompraDirecta)
            {
                if (txtSalida.Text.Trim().Length == 0)
                {
                    resultado = false;
                }
                if (cmbFechaSalida.SelectedDate == null)
                {
                    resultado = false;
                }
            }

            if (string.IsNullOrWhiteSpace(skAyudaChofer.Descripcion))
            {
                resultado = false;
            }

            if (string.IsNullOrWhiteSpace(skAyudaCamion.Descripcion))
            {
                resultado = false;
            }

            if (string.IsNullOrWhiteSpace(skAyudaJaula.Descripcion))
            {
                resultado = false;
            }

            if (string.IsNullOrWhiteSpace(skAyudaQuienRecibe.Descripcion))
            {
                resultado = false;
            }

            decimal pesoBruto;
            if (!decimal.TryParse(txtPesoBruto.Text, out pesoBruto))
            {
                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Valida los campos para Impresion de Ticket
        /// con Peso Tara
        /// </summary>
        /// <returns></returns>
        private bool ValidaImpresionPesoTara()
        {
            bool resultado = ValidaImpresionPesoBruto();

            decimal pesoTara;
            if (!decimal.TryParse(txtPesoTara.Text, out pesoTara))
            {
                resultado = false;
            }
            if (pesoTara <= 0)
            {
                resultado = false;
            }

            int cabezas;
            if (!int.TryParse(txtCabezasRecibidas.Text, out cabezas))
            {
                resultado = false;
            }

            if (string.IsNullOrWhiteSpace(skAyudaCorral.Descripcion))
            {
                resultado = false;
            }

            if (txtCheckList.Text.Trim().Length == 0)
            {
                resultado = false;
            }

            return resultado;
        }

        /// <summary>
        /// Asigna Dependencias para Ayuda Origen
        /// </summary>
        private void AsignaDependenciasAyudaOrigen()
        {
            skAyudaOrigen.Dependencias = null;
            skAyudaOrigen.MensajeDependencias = null;

            IList<IDictionary<IList<string>, Object>> dependencias = new List<IDictionary<IList<string>, Object>>();
            IDictionary<IList<string>, Object> dependecia = new Dictionary<IList<string>, Object>();

            IList<string> camposDependientes = new List<string>();
            camposDependientes.Add("EmbarqueID");
            dependecia.Add(camposDependientes, EntradaGanado);
            dependencias.Add(dependecia);

            dependecia = new Dictionary<IList<string>, Object>();
            camposDependientes = new List<string>();
            camposDependientes.Add("TipoOrganizacionID");
            dependecia.Add(camposDependientes, cmbTipoOrigen.SelectedValue);
            dependencias.Add(dependecia);

            skAyudaOrigen.Dependencias = dependencias;

            skAyudaOrigen.MensajeDependencias = null;
            IDictionary<string, string> mensajeDependencias = new Dictionary<string, string>();
            mensajeDependencias.Add("EmbarqueID", Properties.Resources.RecepcionGanado_SeleccionarEmbarque);
            mensajeDependencias.Add("TipoOrganizacionID",
                                    Properties.Resources.RecepcionGanado_SeleccionarTipoOrganizacion);
            skAyudaOrigen.MensajeDependencias = mensajeDependencias;
        }

        /// <summary>
        /// Asigna Dependencias para Ayuda de Corral
        /// </summary>
        private void AsignaDependenciasAyudaCorral()
        {
            skAyudaCorral.Dependencias = null;
            skAyudaCorral.MensajeDependencias = null;

            ObtenerDependenciasCorral();
        }

        /// <summary>
        /// Obtiene la Depedencia de Organizacion
        /// </summary>
        /// <returns></returns>
        private IList<IDictionary<IList<string>, Object>> ObtenerDependenciasOrganizacion()
        {
            IList<IDictionary<IList<string>, Object>> dependencias = new List<IDictionary<IList<string>, Object>>();
            IDictionary<IList<string>, Object> dependecia = new Dictionary<IList<string>, Object>();
            IList<string> camposDependientes = new List<string>();

            camposDependientes.Add("OrganizacionID");

            var organizacionInfo = new OrganizacionInfo {OrganizacionID = organizacionID};
            dependecia.Add(camposDependientes, organizacionInfo);
            dependencias.Add(dependecia);

            return dependencias;
        }

        /// <summary>
        /// Genera las Dependencias para Corral
        /// </summary>
        /// <returns></returns>
        private void ObtenerDependenciasCorral()
        {
            var tipoOrganizacion = (TipoOrganizacion) EntradaGanado.TipoOrigen;
            TipoCorral tipoCorral;
            int tipoCorralId;
            switch (tipoOrganizacion)
            {
                case TipoOrganizacion.Ganadera:
                case TipoOrganizacion.Intensivo:
                case TipoOrganizacion.Maquila:
                    tipoCorral = TipoCorral.Intensivo;
                    tipoCorralId = TipoCorral.Intensivo.GetHashCode();
                    break;
                default:
                    tipoCorralId = TipoCorral.Recepcion.GetHashCode();
                    tipoCorral = TipoCorral.Recepcion;
                    break;
            }
            skAyudaCorral.Info.TipoCorral = new TipoCorralInfo {TipoCorralID = tipoCorralId};

            skAyudaCorral.MensajeClaveInexistente =
                String.Format(Properties.Resources.Corral_InexistenteEnOrganizacionTipoCorral
                              , tipoCorral.ToString().ToLower());

            skAyudaCorral.MensajeDependencias = null;
            IDictionary<string, string> mensajeDependencias = new Dictionary<string, string>();
            mensajeDependencias.Add("OrganizacionID",
                                    Properties.Resources.RecepcionGanado_SeleccionarTipoOrganizacion);
            skAyudaCorral.MensajeDependencias = mensajeDependencias;
        }

        /// <summary>
        /// Obtiene los Tipos de Origen
        /// </summary>
        private void ObtenerTiposOrigen()
        {
            try
            {
                var tipoOrganizacionPL = new TipoOrganizacionPL();
                IList<TipoOrganizacionInfo> tiposOrganizacion = tipoOrganizacionPL.ObtenerTodos();
                if (tiposOrganizacion != null)
                {
                    cmbTipoOrigen.ItemsSource = tiposOrganizacion;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida si los campos se Inicializaran
        /// </summary>
        private void LimpiarPantalla(bool preguntar)
        {
            var result = MessageBoxResult.Yes;
            if (preguntar)
            {
                result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                           Properties.Resources.RecepcionGanado_Cancelacion,
                                           MessageBoxButton.YesNo,
                                           MessageImage.Question);
            }
            if (result == MessageBoxResult.Yes)
            {
                InicializaPantalla();
            }
        }

        /// <summary>
        /// Reestablece los controles a sus valores
        /// originales
        /// </summary>
        private  void InicializaPantalla()
        {
            HabilitaDeshabilitaPantalla(true);
            DeshabilitarControles();
            InicializaContexto();
            ObtenerTiposOrigen();

            skAyudaChofer.LimpiarCampos();
            skAyudaCorral.LimpiarCampos();

            skAyudaOrigen = null;

            skAyudaQuienRecibe.LimpiarCampos();

            skAyudaFolio.LimpiarCampos();
            skAyudaFolio.IsEnabled = true;

            skAyudaCamion.LimpiarCampos();
            skAyudaJaula.LimpiarCampos();

            txtFolioEmbarque.Text = string.Empty;
            EntradaEmbarque = null;

            AgregarAyudaOrigen();
            HabilitaControlesImpresionTicketFinal();
            btnGuardar.IsEnabled = false;

            txtFolioEmbarque.LostFocus += BuscarDatosFolioEmbarque_LostFocus;
            skAyudaFolio.AsignarFoco();
            validarRuteo = false;
        }

        /// <summary>
        /// Valida El Numero de Cabezas de Ganado
        /// </summary>
        /// <returns></returns>
        private bool ValidaCabezasGanado()
        {
            bool cabezasValidas = false;
            if (EntradaGanado.ListaCondicionGanado != null && EntradaGanado.ListaCondicionGanado.Count > 0)
            {
                int cabezasCondicion = EntradaGanado.ListaCondicionGanado.Sum(cab => cab.Cabezas);
                if (cabezasCondicion == EntradaGanado.CabezasRecibidas)
                {
                    cabezasValidas = true;
                }
            }

            return cabezasValidas;
        }

        /// <summary>
        /// Obtiene la Programacion del Embarque
        /// </summary>
        /// <returns></returns>
        private EmbarqueInfo ObtenerProgramacionEmbarque()
        {
            EmbarqueInfo programacionEmbarque;

            var programacionEmbarquePL = new EmbarquePL();
            int folioEmbarque;
            int.TryParse(txtFolioEmbarque.Text, out folioEmbarque);

            if (EntradaEmbarque == null)
            {
                programacionEmbarque = programacionEmbarquePL.ObtenerPorFolioEmbarqueOrganizacion(folioEmbarque,
                                                                                                  organizacionID);
            }
            else
            {
                programacionEmbarque = programacionEmbarquePL.ObtenerPorID(EntradaEmbarque.EmbarqueID);
            }
            return programacionEmbarque;
        }

        /// <summary>
        /// Metodo para Buscar Datos Auxiliares
        /// del Embarque
        /// </summary>
        private void BuscarFolioEmbarque()
        {
            try
            {
                int folioEmbarque;
                int.TryParse(txtFolioEmbarque.Text, out folioEmbarque);
                if (folioEmbarque > 0)
                {
                    EmbarqueInfo programacionEmbarque = ObtenerProgramacionEmbarque();
                    if (programacionEmbarque != null)
                    {
                        ValidaProgramacionEmbarque(programacionEmbarque);
                    }
                    else
                    {
                        txtFolioEmbarque.Text = string.Empty;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.EntradaGanado_ErrorFolioEmbarque, MessageBoxButton.OK,
                                          MessageImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Valida que la Programacion de Embarque sea
        /// correcta
        /// </summary>
        /// <param name="programacionEmbarque"></param>
        private void ValidaProgramacionEmbarque(EmbarqueInfo programacionEmbarque)
        {
            if ((Estatus) programacionEmbarque.Estatus == Estatus.Recibido)
            {
                if (EntradaGanado != null && !EntradaGanado.ImpresionTicket)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.RecepcionGanado_FolioEmbarqueRecibido,
                                                    programacionEmbarque.FolioEmbarque), MessageBoxButton.OK,
                                      MessageImage.Warning);

                    txtFolioEmbarque.Text = string.Empty;
                    txtFolioEmbarque.Focus();

                    EntradaEmbarque = null;
                }
            }
            else
            {
                if ((Estatus) programacionEmbarque.Estatus == Estatus.Cancelado)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      string.Format(Properties.Resources.RecepcionGanado_FolioEmbarqueCancelado,
                                                    programacionEmbarque.FolioEmbarque), MessageBoxButton.OK,
                                      MessageImage.Warning);

                    txtFolioEmbarque.Text = string.Empty;
                    txtFolioEmbarque.Focus();
                    EntradaEmbarque = null;
                }
                else
                {
                    if (EntradaEmbarque == null)
                    {
                        EntradaEmbarque = new EntradaEmbarqueInfo {EmbarqueID = programacionEmbarque.EmbarqueID};
                    }
                    EmbarqueDetalleInfo programacionEmbarqueDetalle =
                        ObtenerDetalleProgramacionEmbarque(programacionEmbarque);
                    if (programacionEmbarqueDetalle == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.EntradaGanado_ProgramacionSinDetalle, MessageBoxButton.OK,
                                          MessageImage.Stop);

                        InicializaContexto();
                        txtFolioEmbarque.Focus();
                    }
                    else
                    {
                        lsEscalas = new List<EmbarqueDetalleInfo>();
                        if (programacionEmbarque.TipoEmbarque.TipoEmbarqueID.ToString().Trim() == TipoEmbarque.Descanso.GetHashCode().ToString().Trim())
                        {
                            if (programacionEmbarque.ListaEscala.Count > 0 && programacionEmbarque.Organizacion.OrganizacionID == organizacionID)
                            {
                                lsEscalas.AddRange(programacionEmbarque.ListaEscala);
                            }
                        }
                        else
                        {
                            if (programacionEmbarque.ListaEscala.Count > 0 && programacionEmbarque.Organizacion.OrganizacionID == organizacionID)
                            {
                                EmbarqueDetalleInfo embInfo = new EmbarqueDetalleInfo();
                                EmbarqueDetalleInfo embAux = new EmbarqueDetalleInfo();
                                embAux = ( from valor in programacionEmbarque.ListaEscala 
                                           where valor.Orden == 
                                           (
                                                from escalaprogramada in programacionEmbarque.ListaEscala select escalaprogramada.Orden
                                           ).Max<int>()
                                           select valor
                                    ).FirstOrDefault<EmbarqueDetalleInfo>();

                                embInfo.OrganizacionDestino = embAux.OrganizacionDestino;

                                embAux = (from valor in programacionEmbarque.ListaEscala
                                          where valor.Orden ==
                                              (
                                              from escalaprogramada in programacionEmbarque.ListaEscala select escalaprogramada.Orden
                                              ).Min<int>()
                                          select valor
                                    ).FirstOrDefault<EmbarqueDetalleInfo>();
                                embInfo.OrganizacionOrigen = embAux.OrganizacionOrigen;
                                embInfo.ListaCostoEmbarqueDetalle = embAux.ListaCostoEmbarqueDetalle;

                                embInfo.Proveedor = embAux.Proveedor;
                                embInfo.Chofer = embAux.Chofer;
                                lsEscalas.Add(embInfo);
                            }
                        }
                        
                        if (EntradaGanado == null || EntradaGanado.EntradaGanadoID == 0)
                        {
                            InicializaContexto();
                            AsignarValoresFolioEmbarque(programacionEmbarque);
                        }
                        EntradaGanado.EmbarqueID = programacionEmbarqueDetalle.EmbarqueID;
                        if (programacionEmbarqueDetalle.OrganizacionOrigen != null &&
                            programacionEmbarqueDetalle.OrganizacionOrigen.TipoOrganizacion != null)
                        {
                            EntradaGanado.TipoOrigen =
                                programacionEmbarqueDetalle.OrganizacionOrigen.TipoOrganizacion.TipoOrganizacionID;
                        }
                        AsignarValoresControlesContexto();
                        AsignaDependenciasAyudaOrigen();

                        if (EntradaGanado.PesoBruto > 0)
                        {
                            DesHabilitaControlesImpresionTicketFinal();
                            btnImpresionTicket.IsEnabled = true;
                            btnCondicionesGanado.IsEnabled = true;
                            if (EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
                            {
                                btnImpresionTicket.IsEnabled = false;
                                btnCapturarPesoTara.IsEnabled = false;
                                btnGuardar.IsEnabled = true;
                            }
                        }
                        if (programacionEmbarque.TipoEmbarque.TipoEmbarqueID == TipoEmbarque.Ruteo.GetHashCode())
                        {
                            var corralPL = new CorralPL();
                            CorralInfo corralInfo = corralPL.ObtenerPorEmbarqueRuteo(
                                programacionEmbarque.FolioEmbarque, programacionEmbarque.Organizacion.OrganizacionID);
                            chkRuteo.IsChecked = true;
                            if (corralInfo != null)
                            {
                                skAyudaCorral.Descripcion = corralInfo.Codigo;
                                skAyudaCorral.Clave = corralInfo.CorralID.ToString(CultureInfo.InvariantCulture);
                                VerificaCorralRuteo();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Asigna los valores a los controles
        /// no ligados al contexto
        /// </summary>
        private void AsignarValoresControlesContexto()
        {
            AsignarOrigen(EntradaGanado.TipoOrigen);
            ObtenerChoferPorID();
            ObtenerQuienRecibe();
            ObtenerOrganizacion();
            AsignarPlacaJaula(EntradaGanado.JaulaID);
            AsignarPlacaCamion(EntradaGanado.CamionID);
            if (EntradaGanado.CorralID > 0)
            {
                if (string.IsNullOrWhiteSpace(skAyudaCorral.Clave) ||
                    string.IsNullOrWhiteSpace(skAyudaCorral.Descripcion))
                {
                    CorralInfo corral = ObtenerCorral(EntradaGanado.CorralID);
                    if (corral != null)
                    {
                        skAyudaCorral.Descripcion = corral.Codigo;
                        skAyudaCorral.Clave = Convert.ToString(corral.CorralID);
                        skAyudaCorral.Info = corral;
                    }
                }
            }
            txtPesoNeto.Text = string.Format("{0:#,#}", EntradaGanado.PesoBruto - EntradaGanado.PesoTara); 
            dtuHoraRecepcion.Text = EntradaGanado.FechaEntrada.ToShortTimeString();
            dtuHoraSalida.Text = EntradaGanado.FechaSalida.ToShortTimeString();

            skAyudaFolio.IsEnabled = false;
        }

        /// <summary>
        /// Deshabilita los controles de la pantalla
        /// ya que se ha impreso el Ticket Peso Tara
        /// </summary>
        private void HabilitaDeshabilitaPantalla(bool habilitar)
        {
            skAyudaChofer.IsEnabled = habilitar;
            skAyudaFolio.IsEnabled = habilitar;

            skAyudaOrigen.IsEnabled = habilitar;
            skAyudaQuienRecibe.IsEnabled = habilitar;
            txtFolioEmbarque.IsEnabled = habilitar;
            btnBuscarFolioEmbarque.IsEnabled = habilitar;

            txtSalida.IsEnabled = habilitar;
            cmbFechaSalida.IsEnabled = habilitar;
            dtuHoraSalida.IsEnabled = habilitar;
            skAyudaCamion.IsEnabled = habilitar;
            skAyudaJaula.IsEnabled = habilitar;
            txtObservacion.IsEnabled = habilitar;
            btnCapturarPesoBruto.IsEnabled = habilitar;
            btnCapturarPesoTara.IsEnabled = habilitar;
            chkFleje.IsEnabled = habilitar;
            chkRuteo.IsEnabled = habilitar;
            btnCondicionesGanado.IsEnabled = habilitar;
            btnImpresionTicket.IsEnabled = habilitar;
        }

        /// <summary>
        /// DesHabilita controles sin
        /// impresion de ticket final
        /// </summary>
        private void DesHabilitaControlesImpresionTicketFinal()
        {
            btnCapturarPesoBruto.IsEnabled = false;
            txtFolioEmbarque.IsEnabled = false;
            btnBuscarFolioEmbarque.IsEnabled = false;
            btnCapturarPesoTara.IsEnabled = true;
        }

        /// <summary>
        /// Habilita controles
        /// </summary>
        private void HabilitaControlesImpresionTicketFinal()
        {
            btnCapturarPesoBruto.IsEnabled = true;
            skAyudaOrigen.IsEnabled = true;
            txtFolioEmbarque.IsEnabled = true;
            btnBuscarFolioEmbarque.IsEnabled = true;
            btnCapturarPesoTara.IsEnabled = false;
        }

        /// <summary>
        /// Asigna los Valores del Detalle del Embarque
        /// </summary>
        /// <param name="programacionEmbarque"></param>
        private void AsignarValoresFolioEmbarque(EmbarqueInfo programacionEmbarque)
        {
            if (programacionEmbarque.Organizacion != null)
            {
                if (programacionEmbarque.ListaEscala != null && programacionEmbarque.ListaEscala.Count > 0)
                {
                    DateTime fechaSalida = programacionEmbarque.ListaEscala.Where(id =>
                                                                                  id.EmbarqueID ==
                                                                                  programacionEmbarque.EmbarqueID)
                        .Select(fecha => fecha.FechaSalida).Min();

                    dtuHoraSalida.Text = fechaSalida.ToShortTimeString();
                    cmbFechaSalida.SelectedDate = fechaSalida;
                }
            }
        }

        /// <summary>
        /// Obtiene los Detalles de Programacion de Embarque
        /// Que Estan Pendientes Por Recibir
        /// </summary>
        /// <param name="programacionEmbarque"></param>
        /// <returns></returns>
        private EmbarqueDetalleInfo ObtenerDetalleProgramacionEmbarque(EmbarqueInfo programacionEmbarque)
        {
            EmbarqueDetalleInfo programacionEmbarqueDetalle = null;

            if (programacionEmbarque.ListaEscala != null && programacionEmbarque.ListaEscala.Count > 0)
            {
                programacionEmbarqueDetalle = programacionEmbarque.ListaEscala.First(proEmb => proEmb.EmbarqueID ==
                                                                                               programacionEmbarque.
                                                                                                   EmbarqueID
                                                                                               && !proEmb.Recibido);
            }

            return programacionEmbarqueDetalle;
        }

        /// <summary>
        /// Obtiene la Persona que Recibio la Entrada
        /// </summary>
        private void ObtenerQuienRecibe()
        {
            if (EntradaGanado != null && EntradaGanado.OperadorID > 0)
            {
                if (string.IsNullOrWhiteSpace(skAyudaQuienRecibe.Clave) ||
                    string.IsNullOrWhiteSpace(skAyudaQuienRecibe.Descripcion))
                {
                    OperadorInfo operador = operadores.FirstOrDefault(id => id.OperadorID == EntradaGanado.OperadorID);
                    if (operador != null)
                    {
                        skAyudaQuienRecibe.Clave = Convert.ToString(operador.OperadorID);
                        skAyudaQuienRecibe.Descripcion = operador.NombreCompleto;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el Chofer Por Su Clave
        /// </summary>
        private void ObtenerChoferPorID()
        {
            if (EntradaGanado != null && EntradaGanado.ChoferID > 0)
            {
                if (string.IsNullOrWhiteSpace(skAyudaChofer.Clave) ||
                    string.IsNullOrWhiteSpace(skAyudaChofer.Descripcion))
                {
                    ChoferInfo chofer = choferes.FirstOrDefault(id => id.ChoferID == EntradaGanado.ChoferID);
                    if (chofer != null)
                    {
                        skAyudaChofer.Clave = Convert.ToString(chofer.ChoferID);
                        skAyudaChofer.Descripcion = chofer.NombreCompleto;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la Organizacion
        /// </summary>
        private void ObtenerOrganizacion()
        {
            if (EntradaGanado != null && EntradaGanado.OrganizacionID > 0)
            {
                if (string.IsNullOrWhiteSpace(skAyudaOrigen.Clave) ||
                    string.IsNullOrWhiteSpace(skAyudaOrigen.Descripcion))
                {
                    OrganizacionInfo organizacion =
                        organizaciones.FirstOrDefault(id => id.OrganizacionID == EntradaGanado.OrganizacionOrigenID);
                    if (organizacion != null)
                    {
                        skAyudaOrigen.Clave = Convert.ToString(organizacion.OrganizacionID);
                        skAyudaOrigen.Descripcion = organizacion.Descripcion;
                    }
                }
            }
        }

        private OrganizacionInfo ObtenerOrganizacion(int idOriganizacion)
        {
            return (from OrganizacionInfo org in organizaciones where org.OrganizacionID == idOriganizacion select org).FirstOrDefault<OrganizacionInfo>();
        }

        /// <summary>
        /// Obtiene los Datos de Corral
        /// </summary>
        private CorralInfo ObtenerCorral(int corralID)
        {
            CorralInfo corral;
            try
            {
                var corralPL = new CorralPL();
                corral = corralPL.ObtenerPorId(corralID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return corral;
        }

        /// <summary>
        /// Busca Datos Complementarios
        /// del Folio de Entrada
        /// </summary>
        private void BuscarFolioEntrada()
        {
            try
            {
                skAyudaFolio.IsEnabled = false;
                int folio;
                int.TryParse(skAyudaFolio.Descripcion, out folio);
                if (folio == 0)
                {
                    skAyudaFolio.Descripcion = skAyudaFolio.Clave;
                    int.TryParse(skAyudaFolio.Descripcion, out folio);
                }
                if (folio > 0)
                {
                    var entradaGanadoPL = new EntradaGanadoPL();
                    EntradaGanado = entradaGanadoPL.ObtenerPorFolioEntrada(folio, organizacionID);
                    if (EntradaGanado == null)
                    {
                        InicializaContexto();
                    }
                    else
                    {
                        bool tieneCondiciones = EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode() ||
                                                VerificarCondiciones();
                        if (tieneCondiciones)
                        {
                            if (String.IsNullOrWhiteSpace(skAyudaFolio.Descripcion))
                            {
                                skAyudaFolio.Descripcion = Convert.ToString(folio);
                            }
                            txtCheckList.Text = EntradaGanado.CheckList.Trim();
                            var programacionEmbarquePL = new EmbarquePL();
                            EmbarqueInfo embarque = programacionEmbarquePL.ObtenerPorID(EntradaGanado.EmbarqueID);
                            if (embarque != null)
                            {
                                EntradaEmbarque = new EntradaEmbarqueInfo {EmbarqueID = embarque.EmbarqueID};
                                //ProveedorEmbarque = new Proveedor
                                //proveedor = new ProveedorInfo();
                                //proveedor = embarque.ListaEscala[0].Proveedor;
                                txtFolioEmbarque.Text = Convert.ToString(embarque.FolioEmbarque);
                                BuscarFolioEmbarque();
                            }
                            ObtenerLoteCorralRuteo();
                            VerificaFolioCompleto();
                            AsignaCamposRequeridos(true);

                            ValidarConfiguracionProrrateoRuteo();
                            if (chkRuteo.IsChecked.Value && validarRuteo)
                            {
                                List<EntradaGanadoInfo> entradasGanado =
                                    entradaGanadoPL.ObtenerEntradasPorEmbarqueID(EntradaGanado.EmbarqueID);
                                decimal pesoTara;
                                if (entradasGanado.Count(tara => tara.PesoTara > 0) == 1)
                                {
                                    pesoTara =
                                        entradasGanado.Where(tara => tara.PesoTara > 0).Select(tara => tara.PesoTara).
                                            FirstOrDefault();
                                }
                                else
                                {
                                    pesoTara =
                                        entradasGanado.Where(
                                            tara =>
                                            tara.PesoTara > 0 && tara.EntradaGanadoID == EntradaGanado.EntradaGanadoID).
                                            Select(tara => tara.PesoTara).
                                            FirstOrDefault();
                                }
                                if (pesoTara > 0)
                                {
                                    EntradaGanado.PesoTara = pesoTara;
                                    btnCapturarPesoTara.IsEnabled = false;
                                    btnImpresionTicket.IsEnabled = false;
                                }
                                if (entradasGanado.Any(imp => imp.ImpresionTicket))
                                {
                                    btnGuardar.IsEnabled = true;
                                }
                            }
                        }
                        else
                        {
                            skAyudaFolio.AsignarFoco();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarFolioEntrada, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Verifica si el Folio esta Completo
        /// </summary>
        private void VerificaFolioCompleto()
        {
            if (EntradaGanado.ImpresionTicket)
            {
                AsignarValoresControlesContexto();
                skAyudaFolio.IsEnabled = true;
                HabilitaDeshabilitaPantalla(false);
                btnGuardar.IsEnabled = true;
                btnImpresionTicket.IsEnabled = true;
                txtFolioEmbarque.LostFocus -= BuscarDatosFolioEmbarque_LostFocus;
            }
            else
            {
                DeshabilitarEdicion(false);
            }
        }

        /// <summary>
        /// Asigna el Elemento Almacenado en
        /// Base de Datos al Combo de Origenes
        /// </summary>
        /// <param name="tipoOrganizacionID"></param>
        private void AsignarOrigen(int tipoOrganizacionID)
        {
            TipoOrganizacionInfo itemTipoOrigen = ObtenerTipoOrganizacion(tipoOrganizacionID);
            if (itemTipoOrigen == null)
            {
                cmbTipoOrigen.SelectedIndex = 0;
            }
            else
            {
                cmbTipoOrigen.SelectedValue = itemTipoOrigen;
            }
        }

        /// <summary>
        /// Obtiene el Tipo de Organizacion
        /// </summary>
        /// <param name="tipoOrganizacionID"></param>
        /// <returns></returns>
        private TipoOrganizacionInfo ObtenerTipoOrganizacion(int tipoOrganizacionID)
        {
            TipoOrganizacionInfo itemTipoOrigen = cmbTipoOrigen.Items.Cast<TipoOrganizacionInfo>().FirstOrDefault(
                tipoID => tipoID.TipoOrganizacionID == tipoOrganizacionID);

            return itemTipoOrigen;
        }

        /// <summary>
        /// Asigna el Elemento Almancenado en Base
        /// de Datos al Combo de Jaulas
        /// </summary>
        /// <param name="placaCamionID"></param>
        private void AsignarPlacaCamion(int placaCamionID)
        {
            if (placaCamionID > 0)
            {
                var camionPL = new CamionPL();
                var camion = camionPL.ObtenerPorID(placaCamionID);
                if (camion != null)
                {
                    skAyudaCamion.Descripcion = camion.PlacaCamion;
                    skAyudaCamion.Clave = Convert.ToString(camion.CamionID);                    
                }
            }
        }

        /// <summary>
        /// Asigna el Elemento Almancenado en Base
        /// de Datos al Combo de Jaulas
        /// </summary>
        /// <param name="jaulaID"></param>
        private void AsignarPlacaJaula(int jaulaID)
        {
            if (jaulaID > 0)
            {
                var jaulaPL = new JaulaPL();
                var jaula = jaulaPL.ObtenerPorID(jaulaID);
                if (jaula != null)
                {
                    skAyudaJaula.Descripcion = jaula.PlacaJaula;
                    skAyudaJaula.Clave = Convert.ToString(jaula.JaulaID);
                }
            }
        }

        /// <summary>
        /// Valida que las Fechas sean Validas
        /// </summary>
        private bool ValidaFecha()
        {
            bool resultado = true;

            var fechaEntrada = new DateTime(EntradaGanado.FechaEntrada.Year, EntradaGanado.FechaEntrada.Month,
                                            EntradaGanado.FechaEntrada.Day, dtuHoraRecepcion.Value.Value.Hour,
                                            dtuHoraRecepcion.Value.Value.Minute, 0);
            var fechaSalida = new DateTime(EntradaGanado.FechaSalida.Year, EntradaGanado.FechaSalida.Month,
                                           EntradaGanado.FechaSalida.Day, dtuHoraSalida.Value.Value.Hour,
                                           dtuHoraSalida.Value.Value.Minute, 0);

            if (fechaSalida > fechaEntrada)
            {
                resultado = false;
            }
            else
            {
                EntradaGanado.FechaEntrada = fechaEntrada;
                EntradaGanado.FechaSalida = fechaSalida;
            }

            return resultado;
        }

        /// <summary>
        /// Valida que el corral no tenga un lote activo 
        /// </summary>
        /// <returns></returns>
        private bool ValidaCorralLote(bool verificarLote)
        {
            bool esValido = true;
            try
            {
                if (txtPesoTara.Text != string.Empty && txtPesoTara.Text != "0" && verificarLote &&
                    txtLote.Text == string.Empty)
                {
                    var lotePL = new LotePL();

                    int corralID = Convert.ToInt32(skAyudaCorral.Clave);
                    int totalActivos = lotePL.ObtenerActivosPorCorral(organizacionID, corralID);
                    if (totalActivos > 0)
                    {
                        esValido = false;
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return esValido;
        }

        private int MostrarPantallaPesosUnificados()
        {
            int resultado = 1;
            var embarquePL = new EmbarquePL();
            IList<EmbarqueDetalleInfo> detallesEmbarque =
                embarquePL.ObtenerEscalasPorEmbarqueID(EntradaGanado.EmbarqueID);
            if (detallesEmbarque != null && detallesEmbarque.Count > 1)
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                List<EntradaGanadoInfo> entradasGanado =
                    entradaGanadoPL.ObtenerEntradasPorEmbarqueID(EntradaGanado.EmbarqueID);

                if (entradasGanado == null)
                {
                    resultado = NO_EXISTE_DETALLE_PESO_TARA;
                }
                if (resultado > 0)
                {
                    if ((entradasGanado.Any(bruto => bruto.PesoBruto == 0) 
                            || detallesEmbarque.Count != entradasGanado.Count)
                         && EntradaGanado.EntradaGanadoID > 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.RecepcionGanado_PesosBrutosFaltantes,
                                          MessageBoxButton.OK,
                                          MessageImage.Stop);
                        resultado = FALTAN_PESOS_BRUTOS;
                        return resultado;
                    }
                    EntradaGanadoInfo entradaTara = entradasGanado.FirstOrDefault(imp => imp.ImpresionTicket);
                    if (entradaTara != null && entradaTara.EntradaGanadoID != EntradaGanado.EntradaGanadoID)
                    {
                        return PESOS_VALIDOS_RUTEO;
                    }
                    if (entradasGanado.Any(tara => tara.PesoTara == 0) && EntradaGanado.EntradaGanadoID == 0)
                    {
                        resultado = PESOS_VALIDOS_RUTEO;
                        return resultado;
                    }
                    if (!entradasGanado.Any(tara => tara.PesoTara > 0))
                    {
                        resultado = NO_EXISTE_DETALLE_PESO_TARA;
                        return resultado;
                    }
                    if (entradasGanado.Count == entradasGanado.Count(imp => imp.ImpresionTicket))
                    {
                        return PESOS_VALIDOS_RUTEO;
                    }
                    var pesosUnificados = new ObservableCollection<PesoUnificadoInfo>();

                    var interfaceSalidaAnimalPL = new InterfaceSalidaAnimalPL();
                    List<InterfaceSalidaAnimalInfo> pesosInterface =
                        interfaceSalidaAnimalPL.ObtenerPesosGanado(entradasGanado);
                    if (pesosInterface == null)
                    {
                        pesosInterface = new List<InterfaceSalidaAnimalInfo>();
                    }
                    OrganizacionInfo organizacion;
                    bool habilitar;
                    decimal peso;
                    int usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();

                    decimal pesoBruto = entradasGanado.Select(bruto => bruto.PesoBruto).FirstOrDefault();
                    decimal pesoTaraTotal = entradasGanado.Sum(tara => tara.PesoTara);
                    decimal pesoLlegadaTotal = pesoBruto - pesoTaraTotal;
                    decimal pesoOrigenTotal = pesosInterface.Sum(origen => origen.PesoOrigen);
                    decimal pesoLlegada = 0;
                    if (pesoOrigenTotal > 0)
                    {
                        pesoLlegada = (pesoLlegadaTotal/pesoOrigenTotal);
                    }                     
                    for (var indexDetalle = 0; indexDetalle < entradasGanado.Count; indexDetalle++)
                    {
                        organizacion =
                            organizaciones.FirstOrDefault(
                                id => id.OrganizacionID == entradasGanado[indexDetalle].OrganizacionOrigenID);
                        peso = 0;
                        switch ((TipoOrganizacion) organizacion.TipoOrganizacion.TipoOrganizacionID)
                        {
                            case TipoOrganizacion.Centro:
                            case TipoOrganizacion.Praderas:
                                habilitar = false;
                                peso =
                                    pesosInterface.Where(
                                        salida => salida.SalidaID == entradasGanado[indexDetalle].FolioOrigen).Sum(
                                            pesoOrigen => pesoOrigen.PesoOrigen);
                                break;
                            default:
                                habilitar = true;
                                break;
                        }
                        if (entradasGanado.Count(imp => imp.ImpresionTicket) !=
                            entradasGanado.Count(id => id.EntradaGanadoID > 0))
                        {
                            entradasGanado[indexDetalle].TipoOrganizacionOrigenId =
                                organizacion.TipoOrganizacion.TipoOrganizacionID;
                            entradasGanado[indexDetalle].OrganizacionOrigenID = organizacion.OrganizacionID;
                            entradasGanado[indexDetalle].OrganizacionOrigen = organizacion.Descripcion;
                            entradasGanado[indexDetalle].PesoTara = pesoBruto -
                                                                    Convert.ToInt32(Math.Ceiling(pesoLlegada*peso));
                            entradasGanado[indexDetalle].Lote.Cabezas = entradasGanado[indexDetalle].CabezasRecibidas;
                            entradasGanado[indexDetalle].Lote.UsuarioModificacionID = usuarioID;
                            entradasGanado[indexDetalle].PesoLlegada = Convert.ToInt32(Math.Ceiling(pesoLlegada*peso));
                        }
                        else
                        {
                            entradasGanado[indexDetalle].PesoLlegada =
                                Convert.ToInt32(entradasGanado[indexDetalle].PesoBruto -
                                                entradasGanado[indexDetalle].PesoTara);
                            pesoLlegadaTotal += entradasGanado[indexDetalle].PesoLlegada;
                        }
                        //entradasGanado[indexDetalle].ImpresionTicket = true;
                        entradasGanado[indexDetalle].HabilitarOrigen = habilitar;
                        pesosUnificados.Add(new PesoUnificadoInfo
                                                {
                                                    EntradaGanado = entradasGanado[indexDetalle],
                                                    PesoOrigen = peso
                                                });
                    }
                    pesosUnificados.Add(new PesoUnificadoInfo
                                            {
                                                EntradaGanado = new EntradaGanadoInfo
                                                                    {
                                                                        OrganizacionOrigen = "TOTAL",
                                                                        PesoBruto = pesoBruto,
                                                                        PesoTara = pesoTaraTotal,
                                                                        PesoLlegada =
                                                                            Convert.ToInt32(
                                                                                Math.Ceiling(pesoLlegadaTotal))
                                                                    },
                                                PesoOrigen = pesoOrigenTotal
                                            });
                    var recepcionGanadoPesosUnificados = new RecepcionGanadoPesosUnificados(pesosUnificados);
                    MostrarCentrado(recepcionGanadoPesosUnificados);
                    EntradaGanado.PesosUnificados = recepcionGanadoPesosUnificados.PesosUnificados;
                    if (EntradaGanado.PesosUnificados == null)
                    {
                        resultado = CAPTURAR_PESAJES_UNIFICADOS;
                    }
                    else
                    {
                        resultado = PESOS_VALIDOS_RUTEO;
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// Guarda los datos Capturados en Pantalla
        /// </summary>
        private int Guardar(bool actualizarRecibido)
        {
            if (actualizarRecibido)
            {
                bool camposRequeridos = ValidaImpresionPesoTara();
                if (!camposRequeridos && EntradaGanado.EntradaGanadoID > 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_CamposObligatorios, MessageBoxButton.OK,
                                      MessageImage.Stop);
                    DeshabilitaControlesGuardar();
                    return 0;
                }
                if (chkRuteo.IsChecked.Value && validarRuteo)
                {
                    int validarPesos = MostrarPantallaPesosUnificados();
                    if (validarPesos < 0)
                    {
                        switch (validarPesos)
                        {
                            case CAPTURAR_PESAJES_UNIFICADOS:
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                  Properties.Resources.RecepcionGanado_ProporcionarPesosRuteo,
                                                  MessageBoxButton.OK,
                                                  MessageImage.Stop);
                                return 0;
                            case FALTAN_PESOS_BRUTOS:
                                return 0;
                        }
                    }
                }
            }
            int folioEntrada = 0;
            bool fechasValidas = ValidaFecha();
            if (fechasValidas)
            {
                bool validaGuardar = ValidaImpresionPesoBruto();
                if (validaGuardar)
                {
                    if (ValidaCorralLote(!actualizarRecibido))
                    {
                        if (txtPesoTara.Text != string.Empty && txtPesoTara.Text != "0")
                        {
                            GeneraLote();
                        }
                        EntradaGanado.EmbarqueID = EntradaEmbarque.EmbarqueID;
                        EntradaGanado.TipoOrigen =
                            ((TipoOrganizacionInfo) cmbTipoOrigen.SelectedValue).TipoOrganizacionID;
                        EntradaGanado.OrganizacionID = organizacionID;
                        EntradaGanado.Activo = EstatusEnum.Activo;

                        int choferID;
                        int corralID;
                        int operadorID;
                        int organizacionOrigenID;
                        int camionID;
                        int jaulaID;

                        int.TryParse(skAyudaChofer.Clave, out choferID);
                        int.TryParse(skAyudaCorral.Clave, out corralID);
                        int.TryParse(skAyudaQuienRecibe.Clave, out operadorID);
                        int.TryParse(skAyudaOrigen.Clave, out organizacionOrigenID);
                        int.TryParse(skAyudaCamion.Clave, out camionID);
                        int.TryParse(skAyudaJaula.Clave, out jaulaID);

                        EntradaGanado.ChoferID = choferID;
                        EntradaGanado.CorralID = corralID;
                        EntradaGanado.OperadorID = operadorID;
                        EntradaGanado.JaulaID = jaulaID;
                        EntradaGanado.CamionID = camionID;
                        EntradaGanado.OrganizacionOrigenID = organizacionOrigenID;

                        if (EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
                        {
                            EntradaGanado.CabezasRecibidas = EntradaGanado.CabezasOrigen;
                            if (EntradaGanado.ListaCondicionGanado == null)
                            {
                                EntradaGanado.ListaCondicionGanado = new List<EntradaCondicionInfo>();
                            }
                        }

                        int usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();
                        if (EntradaGanado.EntradaGanadoID == 0)
                        {
                            EntradaGanado.UsuarioCreacionID = usuarioID;
                        }
                        else
                        {
                            EntradaGanado.UsuarioModificacionID = usuarioID;
                        }

                        if (EntradaGanado.ListaCondicionGanado != null && EntradaGanado.ListaCondicionGanado.Any())
                        {
                            EntradaGanado.ListaCondicionGanado.ToList().ForEach(usuario => usuario.UsuarioID = usuarioID);
                        }
                        var entradaGanadoPL = new EntradaGanadoPL();
                        try
                        {
                            int entradaGanadoID = entradaGanadoPL.GuardarEntradaGanado(EntradaGanado, actualizarRecibido);
                            if (EntradaGanado.FolioEntrada == 0)
                            {
                                EntradaGanadoInfo entradaGanado = entradaGanadoPL.ObtenerPorID(entradaGanadoID);
                                if (entradaGanado != null)
                                {
                                    folioEntrada = entradaGanado.FolioEntrada;
                                }
                            }
                            else
                            {
                                folioEntrada = EntradaGanado.FolioEntrada;
                            }
                            if (EntradaGanado.PesosUnificados != null)
                            {
                                List<EntradaGanadoInfo> entradas =
                                    EntradaGanado.PesosUnificados.Where(
                                        id =>
                                        id.EntradaGanado.EntradaGanadoID > 0).Select(ent => ent.EntradaGanado).ToList();
                                for (var indexPesos = 0; indexPesos < entradas.Count; indexPesos++)
                                {
                                    ImprimirTicket(entradas[indexPesos], true);
                                }
                            }
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              string.Format(Properties.Resources.RecepcionGanado_DatosGuardados,
                                                            folioEntrada),
                                              MessageBoxButton.OK, MessageImage.Correct);
                        }
                        catch (Exception ex)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message,
                                              MessageBoxButton.OK, MessageImage.Error);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.EntradaGanado_ErrorLoteActivo,
                                          MessageBoxButton.OK, MessageImage.Error);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_CamposObligatorios, MessageBoxButton.OK,
                                      MessageImage.Warning);
                    DeshabilitaControlesGuardar();
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ValidaFechas, MessageBoxButton.OK,
                                  MessageImage.Warning);
                DeshabilitaControlesGuardar();
            }
            return folioEntrada;
        }

        /// <summary>
        /// Genera un Objeto de Tipo Lote
        /// </summary>
        private void GeneraLote()
        {
            var lotePL = new LotePL();
            LoteInfo loteInfo = lotePL.ObtenerPorOrganizacionIdLote(organizacionID, EntradaGanado.Lote.Lote);

            if (loteInfo == null)
            {
                loteInfo = EntradaGanado.Lote;
                TipoOrganizacionInfo tipoOrganizacion = ObtenerTipoOrganizacion(EntradaGanado.TipoOrigen);

                loteInfo.TipoProcesoID = tipoOrganizacion.TipoProceso.TipoProcesoID;
                loteInfo.OrganizacionID = organizacionID;
                loteInfo.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                int corralID;
                int.TryParse(skAyudaCorral.Clave, out corralID);
                if (corralID > 0)
                {
                    CorralInfo corral = ObtenerCorral(corralID);
                    if (corral != null)
                    {
                        loteInfo.CorralID = corralID;
                        loteInfo.TipoCorralID = corral.TipoCorral.TipoCorralID;
                    }
                }
                loteInfo.Activo = EstatusEnum.Activo;
                loteInfo.DisponibilidadManual = false;
                if (EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
                {
                    loteInfo.Cabezas = Convert.ToInt32(txtCabezasOrigen.Text);
                    loteInfo.CabezasInicio = Convert.ToInt32(txtCabezasOrigen.Text);
                }
                else
                {
                    loteInfo.Cabezas = Convert.ToInt32(txtCabezasRecibidas.Text);
                    loteInfo.CabezasInicio = Convert.ToInt32(txtCabezasRecibidas.Text);
                }
            }
            else
            {
                loteInfo.Cabezas = Convert.ToInt32(txtCabezasRecibidas.Text);
                loteInfo.CabezasInicio = Convert.ToInt32(txtCabezasRecibidas.Text);
                loteInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();

                EntradaGanado.Lote = loteInfo;
            }
        }

        /// <summary>
        /// Metodo que imprime un ticket con los datos de la entrada
        /// </summary>
        private void ImprimirTicket(int folioEntrada, Boolean ticketTara)
        {
            try
            {
                ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
                string nombreImpresora = configuracion.ImpresoraRecepcionGanado;
                int maxCaracteresLinea = configuracion.MaxCaracteresLinea;
                string nombreFuente = ConfigurationManager.AppSettings["NombreFuente"];

                var opcionesLinea = new OpcionesLinea
                    {
                        Fuente = new Font(nombreFuente, 10),
                        MargenIzquierdo = 10
                    };
                var ticket = new Ticket
                    {
                        OpcionesImpresora = new OpcionesImpresora
                            {
                                Impresora = nombreImpresora,
                                MaximoLinea = maxCaracteresLinea
                            }
                    };
                IList<LineaImpresionInfo> lineas = new List<LineaImpresionInfo>();
                var lineaVacia = new LineaImpresionInfo {Texto = string.Empty, Opciones = opcionesLinea};
                var linea = new LineaImpresionInfo
                    {
                        Texto =
                            string.Format("{0} {1}    {2}{3}", Properties.Resources.ImpresionTicket_Hora,
                                          DateTime.Now.ToShortTimeString(), Properties.Resources.ImpresionTicket_Fecha,
                                          DateTime.Now.ToShortDateString()),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);

                OrganizacionInfo organizacion = organizaciones.FirstOrDefault(id => id.OrganizacionID == organizacionID);
                linea = new LineaImpresionInfo { Texto = organizacion.Descripcion, Opciones = opcionesLinea };
                lineas.Add(linea);
                lineas.Add(lineaVacia);                
                linea = new LineaImpresionInfo
                    {Texto = organizacion.Direccion, Opciones = opcionesLinea};
                lineas.Add(linea);

                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                    {
                        Texto = string.Format("{1} {0}", folioEntrada, Properties.Resources.ImpresionTicket_Ticket),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                    {
                        Texto = string.Format("{1} {0}", skAyudaCamion.Descripcion , Properties.Resources.ImpresionTicket_Placas),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                    {
                        Texto =
                            string.Format("{1} {0}", skAyudaChofer.Descripcion,
                                          Properties.Resources.ImpresionTicket_Chofer),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                    {
                        Texto =
                            string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Concepto,
                                          Properties.Resources.ImpresionTicket_ConceptoDescripcion),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                    {
                        Texto =
                            string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Origen,
                                          skAyudaOrigen.Descripcion),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                            {
                                Texto =
                                    string.Format("{0} {1, 10}", Properties.Resources.ImpresionTicket_PesoBruto,
                                                  Convert.ToDouble(txtPesoBruto.Text).ToString("N",
                                                                                               CultureInfo.
                                                                                                   CurrentCulture)),
                                Opciones = opcionesLinea
                            };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                if (ticketTara)
                {
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0}  {1, 10}", Properties.Resources.ImpresionTicket_PesoTara,
                                                      Convert.ToDouble(txtPesoTara.Text).ToString("N",
                                                                                                  CultureInfo.
                                                                                                      CurrentCulture)),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0}  {1, 10}", Properties.Resources.ImpresionTicket_PesoNeto,
                                                      Convert.ToDouble(txtPesoNeto.Text).ToString("N",
                                                                                                  CultureInfo.
                                                                                                      CurrentCulture)),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                }
                linea = new LineaImpresionInfo
                    {
                        Texto =
                            string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Observaciones,
                                          txtObservacion.Text),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                if (ticketTara)
                {
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0} {1}",
                                                      Convert.ToDouble(txtCabezasRecibidas.Text).ToString("N",
                                                                                                          CultureInfo.
                                                                                                              CurrentCulture)
                                                          .Replace(".00", string.Empty),
                                                      Properties.Resources.ImpresionTicket_CabezasGanado),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                }

                linea = new LineaImpresionInfo
                    {
                        Texto =
                            string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Registro,
                                          skAyudaQuienRecibe.Descripcion),
                        Opciones = opcionesLinea
                    };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                if (ticketTara)
                {
                    linea = new LineaImpresionInfo
                        {
                            Texto =
                                string.Format("{0,-30}{1}", Properties.Resources.ImpresionTicket_Condicion,
                                              Properties.Resources.ImpresionTicket_Cabezas),
                            Opciones = opcionesLinea
                        };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                    foreach (EntradaCondicionInfo condicion in EntradaGanado.ListaCondicionGanado)
                    {
                        linea = new LineaImpresionInfo
                                    {
                                        Texto =
                                            string.Format("{0,-27} {1,5}", condicion.CondicionDescripcion,
                                                          Convert.ToDouble(condicion.Cabezas).ToString("N",
                                                                                                       CultureInfo.
                                                                                                           CurrentCulture)
                                                              .Replace(".00", string.Empty)),
                                        Opciones = opcionesLinea
                                    };
                        lineas.Add(linea);
                    }
                }
                ticket.Imrpimir(lineas);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ImpresoraConectada, MessageBoxButton.OK,
                                  MessageImage.Stop);
            }
        }

        /// <summary>
        /// Metodo que imprime un ticket con los datos de la entrada
        /// </summary>
        private void ImprimirTicket(EntradaGanadoInfo entradaGanado, Boolean ticketTara)
        {
            try
            {
                ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
                string nombreImpresora = configuracion.ImpresoraRecepcionGanado;
                int maxCaracteresLinea = configuracion.MaxCaracteresLinea;
                string nombreFuente = ConfigurationManager.AppSettings["NombreFuente"];

                var opcionesLinea = new OpcionesLinea
                {
                    Fuente = new Font(nombreFuente, 10),
                    MargenIzquierdo = 10
                };
                var ticket = new Ticket
                {
                    OpcionesImpresora = new OpcionesImpresora
                    {
                        Impresora = nombreImpresora,
                        MaximoLinea = maxCaracteresLinea
                    }
                };
                IList<LineaImpresionInfo> lineas = new List<LineaImpresionInfo>();
                var lineaVacia = new LineaImpresionInfo { Texto = string.Empty, Opciones = opcionesLinea };
                var linea = new LineaImpresionInfo
                {
                    Texto =
                        string.Format("{0} {1}    {2}{3}", Properties.Resources.ImpresionTicket_Hora,
                                      DateTime.Now.ToShortTimeString(), Properties.Resources.ImpresionTicket_Fecha,
                                      DateTime.Now.ToShortDateString()),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);

                OrganizacionInfo organizacion = organizaciones.FirstOrDefault(id => id.OrganizacionID == organizacionID);
                linea = new LineaImpresionInfo { Texto = organizacion.Descripcion, Opciones = opcionesLinea };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo { Texto = organizacion.Direccion, Opciones = opcionesLinea };
                lineas.Add(linea);

                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                {
                    Texto = string.Format("{1} {0}", entradaGanado.FolioEntrada, Properties.Resources.ImpresionTicket_Ticket),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);

                CamionInfo camion = camiones.FirstOrDefault(id => id.CamionID == entradaGanado.CamionID);
                linea = new LineaImpresionInfo
                {
                    Texto = string.Format("{1} {0}", camion.PlacaCamion, Properties.Resources.ImpresionTicket_Placas),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);

                ChoferInfo chofer = choferes.FirstOrDefault(id => id.ChoferID == entradaGanado.ChoferID);
                linea = new LineaImpresionInfo
                {
                    Texto =
                        string.Format("{1} {0}", chofer.NombreCompleto,
                                      Properties.Resources.ImpresionTicket_Chofer),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                {
                    Texto =
                        string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Concepto,
                                      Properties.Resources.ImpresionTicket_ConceptoDescripcion),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);

                organizacion =
                    organizaciones.FirstOrDefault(id => id.OrganizacionID == entradaGanado.OrganizacionOrigenID);
                linea = new LineaImpresionInfo
                {
                    Texto =
                        string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Origen,
                                      organizacion.Descripcion),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                linea = new LineaImpresionInfo
                            {
                                Texto =
                                    string.Format("{0} {1, 10}", Properties.Resources.ImpresionTicket_PesoBruto,
                                                  entradaGanado.PesoBruto.ToString("N", CultureInfo.CurrentCulture)),
                                Opciones = opcionesLinea
                            };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                if (ticketTara)
                {
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0}  {1, 10}", Properties.Resources.ImpresionTicket_PesoTara,
                                                      entradaGanado.PesoTara.ToString("N", CultureInfo.CurrentCulture)),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0}  {1, 10}", Properties.Resources.ImpresionTicket_PesoNeto,
                                                      (entradaGanado.PesoBruto - entradaGanado.PesoTara).ToString("N",
                                                                                                                  CultureInfo
                                                                                                                      .
                                                                                                                      CurrentCulture)),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                }
                linea = new LineaImpresionInfo
                            {
                                Texto =
                                    string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Observaciones,
                                                  entradaGanado.Observacion),
                                Opciones = opcionesLinea
                            };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                if (ticketTara)
                {
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0} {1}",
                                                      entradaGanado.CabezasRecibidas.ToString("F0",
                                                                                              CultureInfo.CurrentCulture),
                                                      Properties.Resources.ImpresionTicket_CabezasGanado),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                }

                OperadorInfo operador = operadores.FirstOrDefault(id => id.OperadorID == entradaGanado.OperadorID);
                linea = new LineaImpresionInfo
                {
                    Texto =
                        string.Format("{0} {1}", Properties.Resources.ImpresionTicket_Registro,
                                      operador.NombreCompleto),
                    Opciones = opcionesLinea
                };
                lineas.Add(linea);
                lineas.Add(lineaVacia);
                if (ticketTara)
                {
                    linea = new LineaImpresionInfo
                                {
                                    Texto =
                                        string.Format("{0,-30}{1}", Properties.Resources.ImpresionTicket_Condicion,
                                                      Properties.Resources.ImpresionTicket_Cabezas),
                                    Opciones = opcionesLinea
                                };
                    lineas.Add(linea);
                    lineas.Add(lineaVacia);
                    foreach (EntradaCondicionInfo condicion in entradaGanado.ListaCondicionGanado)
                    {
                        linea = new LineaImpresionInfo
                                    {
                                        Texto =
                                            string.Format("{0,-27} {1,5}", condicion.CondicionDescripcion,
                                                          Convert.ToDouble(condicion.Cabezas).ToString("F0",
                                                                                                       CultureInfo.
                                                                                                           CurrentCulture)),
                                        Opciones = opcionesLinea
                                    };
                        lineas.Add(linea);
                    }
                }
                ticket.Imrpimir(lineas);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ImpresoraConectada, MessageBoxButton.OK,
                                  MessageImage.Stop);
            }
        }

        /// <summary>
        /// Obtiene la Cantidad de Cabezas
        /// Correspondiente a la Salida
        /// </summary>
        private void BuscarCabezasSalida()
        {
            try
            {
                int salidaID;
                int organizacionId;

                int.TryParse(txtSalida.Text, out salidaID);
                int.TryParse(skAyudaOrigen.Clave, out organizacionId);

                int cabezas = 0;
                var tipoOrigen = (TipoOrganizacion) EntradaGanado.TipoOrigen;
                switch (tipoOrigen)
                {
                    case TipoOrganizacion.Ganadera:
                        var interfaceSalidaTraspasoPL = new InterfaceSalidaTraspasoPL();
                        var interfaceSalidaTraspaso = new InterfaceSalidaTraspasoInfo
                                                          {
                                                              OrganizacionId = organizacionId,
                                                              FolioTraspaso = salidaID
                                                          };
                        interfaceSalidaTraspaso = interfaceSalidaTraspasoPL.
                            ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(
                                interfaceSalidaTraspaso);
                        if (interfaceSalidaTraspaso != null)
                        {
                            EntradaGanado.InterfaceSalidaTraspaso = interfaceSalidaTraspaso;

                            cabezas = interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Sum(cab => cab.Cabezas);
                            EntradaGanado.PesoBruto = interfaceSalidaTraspaso.PesoBruto;
                            EntradaGanado.PesoTara = interfaceSalidaTraspaso.PesoTara;
                            txtPesoNeto.Text = string.Format("{0:F2}", EntradaGanado.PesoBruto - EntradaGanado.PesoTara);
                            btnCapturarPesoBruto.IsEnabled = false;
                            btnCapturarPesoTara.IsEnabled = false;
                            btnImpresionTicket.IsEnabled = true;
                        }
                        break;
                    default:
                        var interfaceSalidaPL = new InterfaceSalidaPL();
                        InterfaceSalidaInfo interfaceSalida = interfaceSalidaPL.ObtenerPorID(salidaID, organizacionId);
                        if (interfaceSalida != null)
                        {
                            cabezas = interfaceSalida.Cabezas;
                        }
                        break;
                }
                txtCabezasOrigen.Text = cabezas.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarCabezasSalida, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }
        private bool ValidaSalidaInterface()
        {
            bool valido = true;
            try
            {
                int salidaID;                
                int organizacionOrigenId;
                int.TryParse(txtSalida.Text, out salidaID);
                int.TryParse(skAyudaOrigen.Clave, out organizacionOrigenId);
                
                var interfaceSalidaPL = new InterfaceSalidaPL();
                int totalEmbarques = interfaceSalidaPL.ObtenerPorEmbarque(salidaID, organizacionID, organizacionOrigenId);
                if (totalEmbarques > 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.RecepcionGanado_ErrorSalidaAsignadaEmbarque, MessageBoxButton.OK,
                               MessageImage.Error);                    
                    valido = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarCabezasSalida, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            return valido;
        }

        /// <summary>
        /// Metodo que obtiene el peso de una linea leida del puerto de la bascula
        /// </summary>
        /// <param name="peso"></param>
        /// <returns></returns>
        private decimal ObtenerPeso(string peso)
        {
            string tmpPeso = string.Empty;
            decimal pesoFinal = 0;
            tmpPeso = peso.Where(c => char.IsDigit(c) || c == '.').Aggregate(tmpPeso, (current, c) => current + c);
            if (tmpPeso != string.Empty)
            {
                decimal.TryParse(tmpPeso, out pesoFinal);
            }
            return pesoFinal;
        }

        /// <summary>
        /// Llama ventana para ayuda de folio de embarque
        /// </summary>
        private void AyudaFolioEmbarque()
        {
            EntradaEmbarque = null;
            var recepcionGanadoBusqueda = new RecepcionGanadoBusqueda();
            recepcionGanadoBusqueda.InicializaPaginador();
            recepcionGanadoBusqueda.LlenaCombos();

            recepcionGanadoBusqueda.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];

            recepcionGanadoBusqueda.ShowDialog();

            if (recepcionGanadoBusqueda.EntradaEmbarque != null)
            {
                EntradaEmbarque = recepcionGanadoBusqueda.EntradaEmbarque;
                txtFolioEmbarque.Text = Convert.ToString(EntradaEmbarque.FolioEmbarque);
                skAyudaOrigen.AsignarFoco();
                BuscarFolioEmbarque();
            }
        }

        /// <summary>
        /// Obtiene las condiciones de ganado
        /// </summary>
        private void ObtenerCondiciones()
        {
            try
            {
                var entradaGanadoPL = new EntradaGanadoPL();

                EntradaGanadoInfo entradaGanado = entradaGanadoPL.ObtenerPorID(EntradaGanado.EntradaGanadoID);
                if (entradaGanado != null)
                {
                    if (entradaGanado.ListaCondicionGanado != null)
                    {
                        EntradaGanado.ListaCondicionGanado = entradaGanado.ListaCondicionGanado;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarEntradaGanado, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene el recepcionista en caso de que el
        /// usuario en turno este configurado como uno
        /// </summary>
        private void CargarRecepcionista()
        {
            try
            {
                var operadorPL = new OperadorPL();
                int usuarioId = AuxConfiguracion.ObtenerUsuarioLogueado();
                OperadorInfo operador = operadorPL.ObtenerPorUsuarioIdRol(usuarioId, organizacionID, Roles.Basculista);
                if (operador != null)
                {
                    skAyudaQuienRecibe.Info = operador;                    
                    skAyudaQuienRecibe.Clave = Convert.ToString(operador.OperadorID);
                    skAyudaQuienRecibe.Descripcion = operador.NombreCompleto;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionGanado_ErrorConsultarUsuarioOperador, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Asignar valores a colecciones
        /// </summary>
        private void CargarColeccionesPesajeTara()
        {
            var organizacionPL = new OrganizacionPL();
            organizaciones = organizacionPL.ObtenerTodos(EstatusEnum.Activo);

            var camionPL = new CamionPL();
            camiones = camionPL.ObtenerTodos();

            var choferPL = new ChoferPL();
            choferes = choferPL.ObtenerTodos();

            var operadorPL = new OperadorPL();
            ResultadoInfo<OperadorInfo> operadoresResult = operadorPL.ObtenerPorPagina(new PaginacionInfo{ Inicio = 1, Limite = 99999},
                                                                                       new OperadorInfo
                                                                                           {
                                                                                               Organizacion =
                                                                                                   new OrganizacionInfo
                                                                                                       {
                                                                                                           OrganizacionID
                                                                                                               =
                                                                                                               organizacionID
                                                                                                       },
                                                                                                Rol = new RolInfo()
                                                                                           });
            if (operadoresResult != null)
            {
                operadores = operadoresResult.Lista;
            }
        }

        /// <summary>
        /// Valida la configuracion para el prorrateo
        /// de pesajes en caso de ser embarque de ruteo
        /// </summary>
        private void ValidarConfiguracionProrrateoRuteo()
        {
            var parametroOrganizacionPL = new ParametroOrganizacionPL();
            ParametroOrganizacionInfo parametroOrganizacionProrrateoRuteo =
                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionID,
                                                                               ParametrosEnum.PROPESLLEGADA.ToString());
            if (string.Compare(parametroOrganizacionProrrateoRuteo.Valor, "1", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                validarRuteo = true;
            }
        }

        #endregion METODOS

        #region EVENTOS

        /// <summary>
        /// Metodo para Abrir la Captura
        /// de Condiciones de Ganado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCondicionesGanado_Click(object sender, RoutedEventArgs e)
        {
            if (EntradaGanado.ListaCondicionGanado == null)
            {
                EntradaGanado.ListaCondicionGanado = new List<EntradaCondicionInfo>();
            }
            if (!EntradaGanado.ListaCondicionGanado.Any())
            {
                ObtenerCondiciones();
            }
            var recepcionGanadoCondiciones = new RecepcionGanadoCondiciones(EntradaGanado.ListaCondicionGanado,
                                                                            EntradaGanado.CabezasRecibidas);
            recepcionGanadoCondiciones.Owner = Application.Current.Windows[1];

            recepcionGanadoCondiciones.Left = (ActualWidth - recepcionGanadoCondiciones.Width)/2;
            recepcionGanadoCondiciones.Top = ((ActualHeight - recepcionGanadoCondiciones.Height)/2) + 132;
            recepcionGanadoCondiciones.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];

            recepcionGanadoCondiciones.ShowDialog();
        }

        /// <summary>
        /// Metodo para Guardar los Datos
        /// Contenidos en la Forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            int guardado = Guardar(true);
            if (guardado > 0)
            {
                enviarCorreo();
                LimpiarPantalla(false);
                AsignaCamposRequeridos(false);
            }
            
        }

        /// <summary>
        /// Metodo para Buscar Folio de Embarque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscarFolioEmbarque_Click(object sender, RoutedEventArgs e)
        {
            AyudaFolioEmbarque();
        }

        /// <summary>
        /// Metodo para Capturar Peso Bruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapturarPesoBruto_Click(object sender, RoutedEventArgs e)
        {
            bool pesoValido = CapturaPeso(txtPesoBruto);
            if (pesoValido)
            {
                btnImpresionTicket.IsEnabled = true;
            }
        }

        /// <summary>
        /// Metodo para Buscar el Folio de Embarque
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuscarDatosFolioEmbarque_LostFocus(object sender, RoutedEventArgs e)
        {
            EntradaEmbarque = null;
            BuscarFolioEmbarque();
            DeshabilitaControlesGuardar();
        }

        /// <summary>
        /// Envia notificacion al proveedor sobre la recepcion de ganado
        /// </summary>
        /// <returns></returns>
        private Boolean enviarCorreo()
        {
            //String sNombreProveedor = String.Empty;
            String sCorreo = String.Empty;
            String sMensaje = String.Empty;
            OrganizacionInfo organizacion = new OrganizacionInfo();
            //OrganizacionInfo organizacionOrigen = new OrganizacionInfo();
            //OrganizacionInfo organizacionDestino = new OrganizacionInfo();
            Decimal importeFlete = 0;
            Decimal importeIndirectos = 0;
            Decimal importeDemoras = 0;
            ProveedorPL proveedorPl = new ProveedorPL();
            CorreoInfo correoenviar = new CorreoInfo();
            CorreoPL correo = new CorreoPL();
            ProveedorInfo proveedor = new ProveedorInfo();

            if (lsEscalas != null && lsEscalas.Count > 0)
            {
                organizacion.OrganizacionID = organizacionID;
                organizacion.Descripcion = ObtenerOrganizacion(organizacionID).Descripcion;

                


                foreach (var escala in lsEscalas)
                {
                    //organizacionOrigen = escala.OrganizacionOrigen;
                    //organizacionDestino = escala.OrganizacionDestino;

                    importeFlete = 0;
                    importeIndirectos = 0;
                    importeDemoras = 0;


                    if (escala.ListaCostoEmbarqueDetalle != null && escala.ListaCostoEmbarqueDetalle.Count > 0)
                    {

                        importeFlete = Math.Round(Convert.ToDecimal((from entrada in escala.ListaCostoEmbarqueDetalle
                                                                     where (Costo)entrada.Costo.CostoID == Costo.Fletes
                                                                     select entrada.Costo.ImporteCosto).Sum(), System.Globalization.CultureInfo.InvariantCulture), 2);
                        importeIndirectos = Math.Round(Convert.ToDecimal((from entrada in escala.ListaCostoEmbarqueDetalle
                                                                          where (Costo)entrada.Costo.CostoID == Costo.GastosIndirectos
                                                                          select entrada.Costo.ImporteCosto).Sum(), System.Globalization.CultureInfo.InvariantCulture), 2);
                        importeDemoras = Math.Round(Convert.ToDecimal((from entrada in escala.ListaCostoEmbarqueDetalle
                                                                       where (Costo)entrada.Costo.CostoID == Costo.Demoras
                                                                       select entrada.Costo.ImporteCosto).Sum(), System.Globalization.CultureInfo.InvariantCulture), 2);
                    }
                    sMensaje = String.Format(
                        "FOLIO ENTRADA: {0}<BR><BR>TRANSPORTISTA: {1}<BR><BR>CHOFER: {2}<BR><BR>ORIGEN: {3}<BR><BR>DESTINO: {4}<BR><BR>IMPORTE FLETE: {5}<BR><BR>IMPORTE INDIRECTOS: {6}<BR><BR>IMPORTE DEMORAS: {7}",
                        skAyudaFolio.Descripcion.Trim(),
                        escala.Proveedor.Descripcion,
                        escala.Chofer.NombreCompleto,
                        escala.OrganizacionOrigen.Descripcion.Trim(),
                        escala.OrganizacionDestino.Descripcion.Trim(),
                        importeFlete,
                        importeIndirectos,
                        importeDemoras
                        );

                    if (EntradaGanado.ListaCondicionGanado != null && EntradaGanado.ListaCondicionGanado.Any())
                    {
                        var condiciones = new StringBuilder();
                        foreach (EntradaCondicionInfo condicion in EntradaGanado.ListaCondicionGanado)
                        {
                            condiciones.Append(string.Format("<BR><BR>{0}: {1}", condicion.CondicionDescripcion,
                                condicion.Cabezas));
                        }
                        sMensaje = string.Format("{0}{1}", sMensaje, condiciones);
                    }

                    proveedor = proveedorPl.ObtenerPorIDConCorreo(escala.Proveedor.ProveedorID);
                    sCorreo = proveedor.Correo == null ? "" : proveedor.Correo;

                    if (sCorreo.Trim().Length > 0)
                    {
                        correoenviar = new CorreoInfo();

                        correoenviar.Asunto = "Recepción de Ganado";
                        correoenviar.Correos = new List<string>();
                        correoenviar.Mensaje = sMensaje;
                        correoenviar.Correos.Add(sCorreo);

                        correo.EnviarCorreo(organizacion, correoenviar);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Metodo Para Capturar Peso Tara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapturarPesoTara_Click(object sender, RoutedEventArgs e)
        {
            if (chkRuteo.IsChecked.Value && validarRuteo)
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                List<EntradaGanadoInfo> entradasGanado =
                    entradaGanadoPL.ObtenerEntradasPorEmbarqueID(EntradaGanado.EmbarqueID);
                if (entradasGanado.Any(pesoBruto => pesoBruto.PesoBruto == 0))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_PesosBrutosFaltantes,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    return;
                }
            }
            CapturaPeso(txtPesoTara);
        }

        /// <summary>
        /// Metodo que se Ejecuta Cuando Se Captura Algun Peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CambioPeso(object sender, TextChangedEventArgs e)
        {
            decimal pesoBruto;
            decimal pesoTara;

            decimal.TryParse(txtPesoBruto.Text, out pesoBruto);
            decimal.TryParse(txtPesoTara.Text, out pesoTara);
            
            txtPesoNeto.Text = string.Format("{0:#,#}", pesoBruto - pesoTara);
        }

        /// <summary>
        /// Indica si el Origen es
        /// Compra Directa
        /// </summary>
        /// <returns></returns>
        private bool EsCompraDirecta()
        {
            bool resultado = false;

            if (cmbTipoOrigen.SelectedValue != null)
            {
                if (((TipoOrganizacionInfo) cmbTipoOrigen.SelectedValue).TipoOrganizacionID ==
                    TipoOrganizacion.CompraDirecta.GetHashCode())
                {
                    resultado = true;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Indica si el Origen es
        /// Compra Directa, Intensivo o Maquila
        /// </summary>
        /// <returns></returns>
        private bool EsSalidaRequerida()
        {
            var requerido = true;

            var tipoOrganizacion = (TipoOrganizacion)EntradaGanado.TipoOrigen;
            switch (tipoOrganizacion)
            {
                case TipoOrganizacion.CompraDirecta:
                case TipoOrganizacion.Intensivo:
                case TipoOrganizacion.Maquila:
                    requerido = false;
                    break;
            }
            return requerido;
        }

        /// <summary>
        /// Metodo que se Ejecuta al Cambiar un Tipo de Origen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTipoOrigen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            skAyudaOrigen.LimpiarCampos();
            AsignaDependenciasAyudaCorral();
            skAyudaCorral.LimpiarCampos();
            if (EsSalidaRequerida())
            {
                lblSalidaRequerida.Visibility = Visibility.Visible;
            }
            else
            {
                lblSalidaRequerida.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Regresa la Pantalla a su Estado Inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimpiarPantalla(object sender, RoutedEventArgs e)
        {
            LimpiarPantalla(true);
            AsignaCamposRequeridos(false);
        }

        /// <summary>
        /// Manda la Impresion de Ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImpresionTicket_Click(object sender, RoutedEventArgs e)
        {
            bool esPesoTara = EntradaGanado.EntradaGanadoID > 0;
            int folioEntrada;
            if (esPesoTara)
            {
                bool camposRequeridos = ValidaImpresionPesoTara();
                if (camposRequeridos)
                {
                    decimal pesoNeto = Convert.ToDecimal(txtPesoNeto.Text);
                    if (pesoNeto > 0)
                    {
                        bool cabezasValidas = EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode() ||
                                              ValidaCabezasGanado();
                        if (cabezasValidas)
                        {
                            EntradaGanado.ImpresionTicket = true;
                            folioEntrada = Guardar(false);
                            if (folioEntrada > 0)
                            {
                                ImprimirTicket(folioEntrada, true);
                                LimpiarPantalla(false);
                                AsignaCamposRequeridos(false);
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.RecepcionGanado_CantidadCabezas, MessageBoxButton.OK,
                                              MessageImage.Stop);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.RecepcionGanado_ErrorPesoBruto, MessageBoxButton.OK,
                                          MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_CamposObligatorios, MessageBoxButton.OK,
                                      MessageImage.Stop);
                }
            }
            else
            {
                bool camposRequeridosPesoBruto = ValidaImpresionPesoBruto();
                if (camposRequeridosPesoBruto)
                {
                    folioEntrada = Guardar(false);
                    if (folioEntrada > 0)
                    {
                        ImprimirTicket(folioEntrada, false);
                        LimpiarPantalla(false);
                        AsignaCamposRequeridos(false);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionGanado_CamposObligatorios, MessageBoxButton.OK,
                                      MessageImage.Stop);
                }
            }
        }


        private void chkRuteo_CambiaCheck(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(chkRuteo.IsChecked))
            {
                ObtenerLoteCorralRuteo();
            }
        }

        /// <summary>
        /// Evento de carga del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InicializarBascula();
            skAyudaFolio.AsignarFoco();
            AsignaCamposRequeridos(false);
            CargarRecepcionista();
            CargarColeccionesPesajeTara();
        }

        private void AsignaCamposRequeridos(bool mostrar)
        {
            lblCabezasRequerido.Visibility = mostrar ? Visibility.Visible : Visibility.Hidden;
            lblCheckListRequerido.Visibility = mostrar ? Visibility.Visible : Visibility.Hidden;
            lblPesoTaraRequerido.Visibility = mostrar ? Visibility.Visible : Visibility.Hidden;
            lblCorralRequerido.Visibility = mostrar ? Visibility.Visible : Visibility.Hidden;            
        }

        /// <summary>
        /// evento cuando se preciona cualquier tecla en el formulario 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        private void cmbFechaSalida_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (spManager != null)
                {
                    spManager.StopListening();
                    spManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void txtSalida_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSalida.Text.Trim() != string.Empty && txtSalida.Text != "0")
            {
                if (txtSalida.IsEnabled)
                {
                    if (EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode()
                        || ValidaSalidaInterface())
                    {
                        BuscarCabezasSalida();
                    }
                    else
                    {
                        txtSalida.Text = "0";
                    }
                }
            }
            else
            {
                txtCabezasOrigen.Text = string.Empty;
            }
        }

        private void txtFolioEmbarque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                EntradaEmbarque = null;
            }
            if (e.Key == Key.F1)
            {
                AyudaFolioEmbarque();   
            }
        }

        private void txtFolioEmbarque_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidarSoloNumeros(e.Text);
        }

        private bool ValidarSoloNumeros(string valor)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(valor));

            return !string.IsNullOrWhiteSpace(valor) && (ascci < 48 || ascci > 57);
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (btnCapturarPesoBruto.IsEnabled)
                {
                    btnCapturarPesoBruto.Focus();
                }
                else
                {
                    if (btnCapturarPesoTara.IsEnabled)
                    {
                        btnCapturarPesoTara.Focus();
                    }
                }
                e.Handled = true;
            }
        }

        private void txtCabezasRecibidas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = ValidarSoloNumeros(e.Text);
        }

        private void txtSalida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                if (txtSalida.Text.Trim() != string.Empty && txtSalida.Text != "0")
                {
                    if (txtSalida.IsEnabled)
                    {
                        if (EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode()
                            || ValidaSalidaInterface())
                        {
                            BuscarCabezasSalida();
                        }
                        else
                        {
                            e.Handled = true;
                            txtSalida.Text = "0";
                        }
                    }
                }
                else
                {
                    txtCabezasOrigen.Text = string.Empty;
                }
            }
        }

        private void PermitirAlfanumerico(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        #endregion EVENTOS

        #region Bascula
        /// <summary>
        /// Método que inicializa los datos de la bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                spManager = new SerialPortManager();
                spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);

                if (spManager != null)
                {

                    spManager.StartListening(AuxConfiguracion.ObtenerConfiguracion().PuertoBascula,
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaBaudrate),
                        ObtenerParidad(AuxConfiguracion.ObtenerConfiguracion().BasculaParidad),
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaDataBits),
                        ObtenerStopBits(AuxConfiguracion.ObtenerConfiguracion().BasculaBitStop));

                    //basculaConectada = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaMateriaPrima_MsgErrorBascula,
                    MessageBoxButton.OK, MessageImage.Warning);

            }
        }

        /// <summary>
        /// Cambia la variable string en una entidad Parity
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private Parity ObtenerParidad(string parametro)
        {
            Parity paridad;

            switch (parametro)
            {
                case "Even":
                    paridad = Parity.Even;
                    break;
                case "Mark":
                    paridad = Parity.Mark;
                    break;
                case "None":
                    paridad = Parity.None;
                    break;
                case "Odd":
                    paridad = Parity.Odd;
                    break;
                case "Space":
                    paridad = Parity.Space;
                    break;
                default:
                    paridad = Parity.None;
                    break;
            }
            return paridad;
        }

        /// <summary>
        /// Cambia la variable string en una entidad StopBit
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private StopBits ObtenerStopBits(string parametro)
        {
            StopBits stopBit;

            switch (parametro)
            {
                case "None":
                    stopBit = StopBits.None;
                    break;
                case "One":
                    stopBit = StopBits.One;
                    break;
                case "OnePointFive":
                    stopBit = StopBits.OnePointFive;
                    break;
                case "Two":
                    stopBit = StopBits.Two;
                    break;
                default:
                    stopBit = StopBits.One;
                    break;
            }
            return stopBit;
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
                strEnd = spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    peso = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txtDisplay.Text = peso.ToString(CultureInfo.InvariantCulture);
                    }), null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion
    }
} 