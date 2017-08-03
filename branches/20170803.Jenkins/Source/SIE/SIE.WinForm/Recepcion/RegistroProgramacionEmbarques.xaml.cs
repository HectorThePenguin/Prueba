using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Log;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for RegistroProgramacionEmbarques.xaml
    /// </summary>
    public partial class RegistroProgramacionEmbarques
    {
        #region CONSTRUCTORES

        /// <summary>
        /// Constructor parametrizado, que recibe el Folio de Embarque y la organizacion Id
        /// </summary>
        public RegistroProgramacionEmbarques(int folioEmbarque, int organizacionId)
        {
            InitializeComponent();
            InicializaContexto();
            CargarComboEstatusEmbarque();
            CargarAyudas();
            if (folioEmbarque > 0)
            {
                ConsultarEmbarque(folioEmbarque, organizacionId);
            }
            usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
            skAyudaOrganizacion.Clave = ContenedorEmbarque.Embarque.Organizacion.OrganizacionID.ToString(CultureInfo.InvariantCulture);
            skAyudaOrganizacion.Descripcion =
              ContenedorEmbarque.Embarque.Organizacion.Descripcion;
            BtnCancelarEmbarque.IsEnabled = true;
            dudKms.IsEnabled = false;
        }

        /// <summary>
        /// Constructor po Default
        /// </summary>
        public RegistroProgramacionEmbarques()
        {
            InitializeComponent();
            InicializaContexto();
            CargarComboEstatusEmbarque();
            ContenedorEmbarque.Embarque.Estatus = Estatus.Pendiente.GetHashCode();
            CargarAyudas();
            TxtFolioEmbarque.IsEnabled = false;
            CmbEstatus.IsEnabled = false;
            usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
            dudKms.IsEnabled = false;
        }

        #endregion CONSTRUCTORES

        #region PROPIEDADES

        /// <summary>
        /// Propiedad donde se almacena 
        /// </summary>
        private ContenedorEmbarqueInfo ContenedorEmbarque
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContenedorEmbarqueInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Propiedad donde se almacena la configuracion de Trayectos de los Embarques
        /// </summary>
        private ConfiguracionEmbarqueInfo ConfiguracionEmbarque { get; set; }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion PROPIEDADES

        #region VARIABLES

        /// <summary>
        /// Propiedad donde se revisan los Tipos de Embarque a los que se validarán la Fechas de Llegada de las Escalas anteriores
        /// </summary>
        private readonly int[] tiposEmbarqueValidaFechaSalida = new[] { TipoEmbarque.Ruteo.GetHashCode() };

        /// <summary>
        /// Propiedad donde se revisan los Tipos de Embarque a los que se validarán la Fechas de Llegada de las Escalas anteriores
        /// </summary>
        private readonly int[] tiposEmbarqueValidaMayorFechaSalida = new[] { TipoEmbarque.Descanso.GetHashCode() };

        /// <summary>
        /// Propiedad donde se revisan los Tipos de Embarque que no llevan mas de una Escala
        /// </summary>
        private readonly int[] tiposEmbarqueValidaUnicoDetalle = new[] { TipoEmbarque.Directo.GetHashCode() };

        /// <summary>
        /// Propiedad donde se revisan los Tipos de Embarque a los que nomas se les puede capturar una sola vez los Costos
        /// </summary>
        private readonly int[] tiposEmbarquesUnicoCosto = new[] { TipoEmbarque.Ruteo.GetHashCode(), TipoEmbarque.Directo.GetHashCode() };

        /// <summary>
        /// Propiedad donde se revisan los Tipos de Embarque a los que nomas se les puede capturar una sola vez los Costos
        /// </summary>
        private readonly int[] tiposEmbarquesMismosDatos = new[] { TipoEmbarque.Ruteo.GetHashCode() };

        /// <summary>
        /// Propiedad que indica si el registro es una modificación, para no agregarlo de nuevo a la lista de Escalas
        /// </summary>
        private bool esModificacion;

        /// <summary>
        /// Control para la ayuda de Chofer
        /// </summary>
        private SKAyuda<ChoferInfo> skAyudaChofer;

        /// <summary>
        /// Control para la ayuda de Organización de Destino
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaDestino;

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Control para la ayuda de Organización de Origen
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrigen;

        /// <summary>
        /// Control para la ayuda de Transportista
        /// </summary>
        private SKAyuda<ProveedorInfo> skAyudaTransportista;

        /// <summary>
        /// Control para la ayuda de Jaula
        /// </summary>
        private SKAyuda<JaulaInfo> skAyudaJaula;

        /// <summary>
        /// Control para la ayuda de Camion
        /// </summary>
        private SKAyuda<CamionInfo> skAyudaCamion;

        /// <summary>
        /// Vairiable para manejar el Usuario Logueado
        /// </summary>
        private readonly int usuarioLogueadoID;

        /// <summary>
        /// Vairiable para manejar cuando los eventos son disparados por CodeBehind
        /// </summary>
        private bool modificacionPorCodigo;

        /// <summary>
        /// Tipo de Organizacion seleccionada
        /// </summary>
        private TipoOrganizacion tipoOrganizacion;

        #endregion VARIABLES

        #region METODOS

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            ContenedorEmbarque = new ContenedorEmbarqueInfo();
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Embarque
        /// </summary>
        private void CargarComboTiposEmbarque()
        {
            var tipoEmbarquePL = new TipoEmbarquePL();
            CmbTipoEmbarque.ItemsSource = tipoEmbarquePL.ObtenerTodos();
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboTipoOrganizacion()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();
            IEnumerable<TipoOrganizacionInfo> tiposOrganizacion = tipoOrganizacionPL.ObtenerTodos(EstatusEnum.Activo);
            CmbTipoOrigen.ItemsSource = tiposOrganizacion.OrderBy(tipo => tipo.Descripcion);
        }

        /// <summary>
        /// Metodo que manda llamar las metodos para cargar los Combos de Camiones y Jaulas
        /// </summary>
        private void CargarJaulasyCamionesProveedor(string proveedor)
        {
            try
            {
                int proveedorId = Extensor.ValorEntero(proveedor);

                var jaulaPL = new JaulaPL();
                var jaulas = jaulaPL.ObtenerPorProveedorID(proveedorId);
                bool tieneJaulas = jaulas != null && jaulas.Any();

                var camionPL = new CamionPL();
                var camiones = camionPL.ObtenerPorProveedorID(proveedorId);
                bool tieneCamiones = camiones != null && camiones.Any();

                if (!tieneJaulas || !tieneCamiones)
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_TransportistaCamionJaula,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaTransportista.LimpiarCampos();
                    skAyudaTransportista.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Genera dependencias para ayuda por proveedor
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        private IList<IDictionary<IList<string>, Object>> GeneraDependenciaProveedor(int proveedorId)
        {
            IList<IDictionary<IList<string>, Object>> dependencias = new List<IDictionary<IList<string>, Object>>();
            IDictionary<IList<string>, Object> dependecia = new Dictionary<IList<string>, Object>();
            IList<string> camposDependientes = new List<string>();

            camposDependientes.Add("ProveedorID");

            ProveedorInfo proveedorInfo = null;
            if (proveedorId > 0)
            {
                proveedorInfo = new ProveedorInfo { ProveedorID = proveedorId };
            }
            dependecia.Add(camposDependientes, proveedorInfo);
            dependencias.Add(dependecia);

            return dependencias;
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Embarque
        /// </summary>
        private void CargarComboEstatusEmbarque()
        {
            var listaEstatus = EstatusInfo.ListFrom<Estatus>()
                .Select(x =>
                        new
                            {
                                Text = x.Descripcion,
                                Value = x.EstatusId
                            });

            CmbEstatus.ItemsSource = listaEstatus;
        }

        /// <summary>
        /// Metodo que carga las Horas por Default 08:00
        /// </summary>
        private void CargarHorasDefault()
        {
            DtuHoraSalida.Text = "8:00";
            DtuHoraLlegada.Text = "8:00";
        }

        /// <summary>
        /// Metodo que carga el objeto Organización de la ayuda a la Clase  Embarque
        /// </summary>
        private void CargarEntidadAyudaOrganizacion()
        {
            var organizacionInfo = new OrganizacionInfo
                {
                    OrganizacionID = Convert.ToInt32(skAyudaOrganizacion.Clave),
                    Descripcion = skAyudaOrganizacion.Descripcion
                };

            ContenedorEmbarque.Embarque.Organizacion = organizacionInfo;
        }

        /// <summary>
        /// Metodo que carga los objetos de las ayudas a sus correspondientes propiedades
        /// </summary>
        private void CargarEntidadesdeAyudas()
        {
            var choferInfo = new ChoferInfo
                {
                    ChoferID = Convert.ToInt32(skAyudaChofer.Clave),
                    Nombre = skAyudaChofer.Descripcion
                };
            var organizacionOrigen = new OrganizacionInfo
                {
                    OrganizacionID = Convert.ToInt32(skAyudaOrigen.Clave),
                    Descripcion = skAyudaOrigen.Descripcion,
                    TipoOrganizacion = (TipoOrganizacionInfo)CmbTipoOrigen.SelectedItem
                };
            var organizacionDestino = new OrganizacionInfo
                {
                    OrganizacionID = Convert.ToInt32(skAyudaDestino.Clave),
                    Descripcion = skAyudaDestino.Descripcion,
                };
            var transportista = new ProveedorInfo
                {
                    ProveedorID = Convert.ToInt32(skAyudaTransportista.Id),
                    CodigoSAP = skAyudaTransportista.Clave,
                    Descripcion = skAyudaTransportista.Descripcion,
                };
            ContenedorEmbarque.EmbarqueDetalle.Chofer = choferInfo;
            ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen = organizacionOrigen;
            ContenedorEmbarque.EmbarqueDetalle.OrganizacionDestino = organizacionDestino;
            ContenedorEmbarque.EmbarqueDetalle.Proveedor = transportista;
            ContenedorEmbarque.EmbarqueDetalle.Jaula = skAyudaJaula.Info;
            ContenedorEmbarque.EmbarqueDetalle.Camion = skAyudaCamion.Info;
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaChofer();
            AgregarAyudaOrganizacion();
            AgregarAyudaTransportista();
            AgregarAyudaOrigen();
            AgregarAyudaDestino();
            AgregarAyudaJaula();
            AgregarAyudaCamion();

            skAyudaTransportista.ObtenerDatos += CargarJaulasyCamionesProveedor;
            skAyudaDestino.LlamadaMetodos += ValidaDestino;
            skAyudaOrigen.ObtenerDatos += ValidarOrigen;
            skAyudaTransportista.LlamadaMetodos += AsignaDependenciaProveedores;
        }

        /// <summary>
        /// Metodo que asigna el Focus al siguiente control despues de utilizar la ayuda
        /// </summary>
        private void AsignaDependenciaProveedores()
        {
            if (ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID == TipoEmbarque.Ruteo.GetHashCode()
                    && !string.IsNullOrWhiteSpace(skAyudaCamion.Descripcion))
            {
                return;
            }
            skAyudaJaula.Dependencias = GeneraDependenciaProveedor(skAyudaTransportista.Info.ProveedorID);
            skAyudaCamion.Dependencias = GeneraDependenciaProveedor(skAyudaTransportista.Info.ProveedorID);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Camion
        /// </summary>
        private void AgregarAyudaCamion()
        {
            skAyudaCamion =
                new SKAyuda<CamionInfo>(160, true, new CamionInfo(), "PropiedadClaveRegistroProgramacionEmbarque"
                                        , "PropiedadDescripcionRegistroProgramacionEmbarque", false, 0, false)
                    {
                        AyudaPL = new CamionPL(),
                        MensajeClaveInexistente = Properties.Resources.Camion_CodigoInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.Camion_SalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.Camion_Busqueda,
                        MensajeAgregar = Properties.Resources.Camion_Seleccionar,
                        TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCamion,
                        TituloPantalla = Properties.Resources.BusquedaCamion_Titulo,
                        Dependencias = GeneraDependenciaProveedor(0),
                    };

            IDictionary<string, string> mensajeDependencias = new Dictionary<string, string>();
            mensajeDependencias.Add("ProveedorID", Properties.Resources.RegistroProgramacionEmbarque_Proveedor);
            skAyudaCamion.MensajeDependencias = mensajeDependencias;

            stpCamion.Children.Add(skAyudaCamion);
            skAyudaCamion.AsignaTabIndex(7);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Jaula
        /// </summary>
        private void AgregarAyudaJaula()
        {
            skAyudaJaula =
                new SKAyuda<JaulaInfo>(160, true, new JaulaInfo(), "PropiedadClaveRegistroProgramacionEmbarque"
                                       , "PropiedadDescripcionRegistroProgramacionEmbarque", false, 0, false)
                    {
                        AyudaPL = new JaulaPL(),
                        MensajeClaveInexistente =
                            Properties.Resources.Jaula_RegistroProgramacionEmbarque_CodigoInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.Jaula_SalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.Jaula_Busqueda,
                        MensajeAgregar = Properties.Resources.Jaula_Seleccionar,
                        TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaJaula,
                        TituloPantalla = Properties.Resources.BusquedaJaula_Titulo,
                        Dependencias = GeneraDependenciaProveedor(0),
                    };

            IDictionary<string, string> mensajeDependencias = new Dictionary<string, string>();
            mensajeDependencias.Add("ProveedorID", Properties.Resources.RegistroProgramacionEmbarque_Proveedor);
            skAyudaJaula.MensajeDependencias = mensajeDependencias;

            stpJaula.Children.Add(skAyudaJaula);
            skAyudaJaula.AsignaTabIndex(6);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Chofer
        /// </summary>
        private void AgregarAyudaChofer()
        {
            skAyudaChofer = new SKAyuda<ChoferInfo>(160, false, new ChoferInfo(), "PropiedadClaveRegistroProgramacionEmbarque"
                                                  , "PropiedadDescripcionRegistroProgramacionEmbarque", false, 80, true)
            {
                AyudaPL = new ChoferPL(),
                MensajeClaveInexistente = Properties.Resources.Chofer_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Chofer_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Chofer_Busqueda,
                MensajeAgregar = Properties.Resources.Chofer_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaChofer,
                TituloPantalla = Properties.Resources.BusquedaChofer_Titulo,
            };
            SplAyudaChofer.Children.Add(skAyudaChofer);
            skAyudaChofer.AsignaTabIndex(5);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                        , "PropiedadClaveRegistroProgramacionEmbarque"
                                                        , "PropiedadDescripcionRegistroProgramacionEmbarque", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Organizacion_Busqueda,
                MensajeAgregar = Properties.Resources.Organizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion,
                TituloPantalla = Properties.Resources.BusquedaOrganizacionFinal_Titulo,
            };
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
            skAyudaOrganizacion.AsignaTabIndex(0);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Transportista
        /// </summary>
        private void AgregarAyudaTransportista()
        {
            skAyudaTransportista = new SKAyuda<ProveedorInfo>(160, false, new ProveedorInfo()
                                                        , "PropiedadClaveRegistroProgramacionEmbarque"
                                                        , "PropiedadDescripcionRegistroProgramacionEmbarque"
                                                        , "PropiedadOcultaRegistroProgramacionEmbarque"
                                                        , false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.Transportista_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Transportista_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Transportista_Busqueda,
                MensajeAgregar = Properties.Resources.Transportista_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaTransportista,
                TituloPantalla = Properties.Resources.BusquedaTransportista_Titulo,
            };
            SplAyudaTransportista.Children.Add(skAyudaTransportista);
            skAyudaTransportista.AsignaTabIndex(4);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Origen
        /// </summary>
        private void AgregarAyudaOrigen()
        {
            skAyudaOrigen = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                        , "PropiedadClaveRegistroProgramacionEmbarqueAyudaOrigen"
                                                        , "PropiedadDescripcionRegistroProgramacionEmbarqueAyudaOrigen", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Origen_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Origen_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Origen_Busqueda,
                MensajeAgregar = Properties.Resources.Origen_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrigen,
                TituloPantalla = Properties.Resources.BusquedaOrigen_Titulo,
            };
            SplAyudaOrigen.Children.Add(skAyudaOrigen);

            skAyudaOrigen.MensajeDependencias = null;
            IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
            mensajeDependencias.Add("TipoOrganizacionID",
                                    Properties.Resources.RegistroProgramacionEmbarques_SeleccionarTipoOrganizacion);
            skAyudaOrigen.MensajeDependencias = mensajeDependencias;
            skAyudaOrigen.AsignaTabIndex(9);

            AsignaDependenciasAyudaOrganizacion(skAyudaOrigen, CmbTipoOrigen);
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Destino
        /// </summary>
        private void AgregarAyudaDestino()
        {
            skAyudaDestino = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                        , "PropiedadClaveRegistroProgramacionEmbarqueAyudaDestino"
                                                        , "PropiedadDescripcionRegistroProgramacionEmbarqueAyudaDestino", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Destino_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Destino_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Destino_Busqueda,
                MensajeAgregar = Properties.Resources.Destino_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaDestino,
                TituloPantalla = Properties.Resources.BusquedaDestino_Titulo,
            };
            SplAyudaDestino.Children.Add(skAyudaDestino);

            skAyudaDestino.MensajeDependencias = null;
            IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
            mensajeDependencias.Add("OrganizacionID",
                                    Properties.Resources.RegistroProgramacionEmbarque_OrganizacionOrigenID);
            skAyudaDestino.MensajeDependencias = mensajeDependencias;
            skAyudaDestino.AsignaTabIndex(10);

            AsignaDependenciasAyudaOrganizacionDestino(skAyudaDestino);
        }

        /// <summary>
        /// Metodo para agregar las dependencias a las ayudas de Organización Origen y Destino
        /// </summary>
        private void AsignaDependenciasAyudaOrganizacion(SKAyuda<OrganizacionInfo> controlAyuda, ComboBox combo)
        {
            controlAyuda.Dependencias = null;

            IList<IDictionary<IList<String>, Object>> dependencias = new List<IDictionary<IList<String>, Object>>();
            IDictionary<IList<String>, Object> dependecia = new Dictionary<IList<String>, Object>();

            var dependenciasGanado = new EntradaGanadoInfo();
            IList<String> camposDependientes = new List<String>();
            camposDependientes.Add("EmbarqueID");
            dependecia.Add(camposDependientes, dependenciasGanado);
            dependencias.Add(dependecia);

            dependecia = new Dictionary<IList<String>, Object>();
            camposDependientes = new List<String> { "TipoOrganizacionID" };
            dependecia.Add(camposDependientes, combo.SelectedItem);
            dependencias.Add(dependecia);

            controlAyuda.Dependencias = dependencias;
        }

        /// <summary>
        /// Metodo para agregar las dependencias a las ayudas de Organización Origen y Destino
        /// </summary>
        private void AsignaDependenciasAyudaOrganizacionDestino(SKAyuda<OrganizacionInfo> controlAyuda)
        {
            controlAyuda.Dependencias = null;

            IList<IDictionary<IList<String>, Object>> dependencias = new List<IDictionary<IList<String>, Object>>();
            var organizacionOrigen = new FiltroOrganizacionOrigenInfo
                {
                    OrganizacionOrigenID = Extensor.ValorEntero(skAyudaOrigen.Clave)
                };

            IDictionary<IList<string>, object> dependecia = new Dictionary<IList<String>, Object>();
            IList<string> camposDependientes = new List<String> { "OrganizacionOrigenID" };
            dependecia.Add(camposDependientes, organizacionOrigen);
            dependencias.Add(dependecia);

            controlAyuda.Dependencias = dependencias;
        }

        /// <summary>
        /// Metodo para cargar los Registros en el Grid
        /// </summary>
        private void CargarGridEmbarques()
        {
            var embarques = (from detalles in ContenedorEmbarque.Embarque.ListaEscala
                             where detalles.Activo == EstatusEnum.Activo
                             orderby detalles.Orden
                             select new
                                 {
                                     detalles.EmbarqueDetalleID,
                                     ContenedorEmbarque.Embarque.FolioEmbarque,
                                     detalles.Jaula.PlacaJaula,
                                     detalles.Camion.PlacaCamion,
                                     detalles.Chofer.NombreCompleto,
                                     OrganizacionOrigen = detalles.OrganizacionOrigen.Descripcion,
                                     OrganizacionDestino = detalles.OrganizacionDestino.Descripcion,
                                     detalles.FechaSalida,
                                     detalles.FechaLlegada,
                                     Estatus = ((Estatus)ContenedorEmbarque.Embarque.Estatus).ToString(),
                                     TipoEmbarque = ContenedorEmbarque.Embarque.TipoEmbarque.Descripcion,
                                     TipoOrigen = detalles.OrganizacionOrigen.TipoOrganizacion.Descripcion,
                                     detalles.Orden,
                                     detalles.Kilometros,
                                     detalles.Horas
                                 }
                            );
            DgEmbarques.ItemsSource = null;
            DgEmbarques.ItemsSource = embarques;
        }

        /// <summary>
        /// Metodo Limpiar los Controles
        /// </summary>
        private void LimpiarControles()
        {
            skAyudaTransportista.LimpiarCampos();
            skAyudaChofer.LimpiarCampos();
            skAyudaDestino.LimpiarCampos();
            skAyudaOrigen.LimpiarCampos();
            ConfiguracionEmbarque = new ConfiguracionEmbarqueInfo();
            dudKms.Value = null;
            dudHoras.Value = null;
            TxtComentarios.Text = string.Empty;
            CmbTipoOrigen.SelectedItem = null;
            BtnCostos.IsEnabled = false;
            skAyudaJaula.LimpiarCampos();
            skAyudaCamion.LimpiarCampos();
            DtpFechaSalida.SelectedDate = DateTime.Now;
            DtpFechaLlegada.SelectedDate = DateTime.Now;
            CargarHorasDefault();
            ContenedorEmbarque.EmbarqueDetalle.Orden = 0;
            ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID = 0;
            ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle = new List<CostoEmbarqueDetalleInfo>();
            esModificacion = false;
        }

        /// <summary>
        /// Metodo Limpiar los Controles
        /// </summary>
        private void Nuevo()
        {
            skAyudaOrganizacion.LimpiarCampos();
            skAyudaOrganizacion.IsEnabled = true;
            skAyudaTransportista.IsEnabled = true;
            skAyudaChofer.IsEnabled = true;
            skAyudaCamion.IsEnabled = true;
            skAyudaJaula.IsEnabled = true;
            skAyudaOrigen.IsEnabled = true;
            skAyudaDestino.IsEnabled = true;
            skAyudaOrganizacion.IsEnabled = true;
            CmbTipoOrigen.IsEnabled = true;
            CmbTipoEmbarque.IsEnabled = true;
            LimpiarControles();
            DgEmbarques.ItemsSource = null;
            InicializaContexto();
            CmbEstatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Metodo que valida las reglas de negocio de las organizaciones de Origen
        /// </summary>
        private void ValidarOrigen(string organizacionOrigenId)
        {
            if (skAyudaOrigen.Clave.Equals(skAyudaOrganizacion.Clave))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrganizacionDuplicada,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrigen.LimpiarCampos();
                return;
            }
            if (skAyudaOrigen.Clave.Equals(skAyudaDestino.Clave))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_DestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrigen.LimpiarCampos();
                return;
            }
            if ((ContenedorEmbarque.Embarque.ListaEscala.Any() && (ContenedorEmbarque.Embarque.ListaEscala.Count == 1 && !esModificacion)) ||
                    ContenedorEmbarque.Embarque.ListaEscala.Count(escl => escl.Activo == EstatusEnum.Activo) > 1)
            {
                var listaOrdenada = ContenedorEmbarque.Embarque.ListaEscala.OrderBy(esc => esc.Orden);
                EmbarqueDetalleInfo detalleAnterior =
                    listaOrdenada.LastOrDefault(esc => esc.Activo == EstatusEnum.Activo);
                if (detalleAnterior == null)
                {
                    return;
                }
                if (detalleAnterior.OrganizacionDestino.OrganizacionID != Extensor.ValorEntero(organizacionOrigenId))
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrigenDistinto,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaOrigen.LimpiarCampos();
                    skAyudaOrigen.AsignarFoco();
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(skAyudaDestino.Descripcion))
            {
                if (!CargarConfiguraciondeRuta())
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_SinConfiguracion,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaOrigen.LimpiarCampos();
                    return;
                }
            }
            AsignaDependenciasAyudaOrganizacionDestino(skAyudaDestino);
        }

        /// <summary>
        /// Metodo que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private void ValidaDestino()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrigenPrimero,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaDestino.LimpiarCampos();
                return;
            }
            if (skAyudaOrigen.Clave.Equals(skAyudaDestino.Clave))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_DestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaDestino.LimpiarCampos();
                return;
            }
            if (string.IsNullOrWhiteSpace(skAyudaDestino.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrganizacionDestino,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return;
            }
            if (!CargarConfiguraciondeRuta())
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_SinConfiguracion,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaDestino.LimpiarCampos();
            }
            BtnCostos.IsEnabled = true;
        }

        /// <summary>
        /// Metodo que Consulta un Embarque
        /// </summary>
        private void ConsultarEmbarque(int folioEmbarque, int organizacionId)
        {
            var embarquePL = new EmbarquePL();
            EmbarqueInfo programacionEmbarqueConsulta = embarquePL.ObtenerPorFolioEmbarqueOrganizacion(folioEmbarque,
                                                                                                       organizacionId);
            ContenedorEmbarque.Embarque = programacionEmbarqueConsulta;
            InicializarListaCosto();
            CargarGridEmbarques();
            BloquearControles();
            object dataContext = DataContext;
            DataContext = null;
            DataContext = dataContext;
        }

        /// <summary>
        /// Metodo para inicializar la Lista de Costos de un Embarque Detalle
        /// </summary>
        public void InicializarListaCosto()
        {
            ContenedorEmbarque.Embarque.ListaEscala.ToList().ForEach(
                det => det.ListaCostoEmbarqueDetalle = new List<CostoEmbarqueDetalleInfo>());
        }

        /// <summary>
        /// Metodo que Bloquea los controles despues de agregar una Escala
        /// </summary>
        private void BloquearControles()
        {
            TxtFolioEmbarque.IsEnabled = false;
            skAyudaOrganizacion.IsEnabled = false;
            CmbTipoEmbarque.IsEnabled = false;
            CmbEstatus.IsEnabled = false;
        }

        /// <summary>
        /// Metodo que Bloquea los controles despues de agregar una Escala
        /// </summary>
        private void BloquearControlesRuteo(bool enabled)
        {
            skAyudaTransportista.IsEnabled = enabled;
            skAyudaChofer.IsEnabled = enabled;
            skAyudaOrigen.IsEnabled = enabled;
            skAyudaCamion.IsEnabled = enabled;
            skAyudaJaula.IsEnabled = enabled;
            CmbTipoOrigen.IsEnabled = enabled;
        }

        /// <summary>
        /// Metodo que agrega a la Lista de Escalas un elemento Modificado
        /// </summary>
        private bool AgregarEmbarqueDetalleModificacion()
        {
            if (ContenedorEmbarque.Embarque.ListaEscala.Count(esc => esc.Activo == EstatusEnum.Activo) > 1)
            {
                if (!ValidarEscalaExistenteOrigen())
                {
                    return false;
                }
            }
            if (ValidarEscalaExistente() || ValidarEscalaExistenteDestinoDuplicado()) // || ValidarEscalaExistenteOrigenEnDestino())
            {
                return false;
            }
            if (!ValidarCamposObligatorios())
            {
                return false;
            }
            CargarEntidadesdeAyudas();
            EmbarqueDetalleInfo detalleModificar =
                ContenedorEmbarque.Embarque.ListaEscala.FirstOrDefault(
                    n =>
                    n.EmbarqueDetalleID ==
                    ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID &&
                    n.Orden == ContenedorEmbarque.EmbarqueDetalle.Orden);

            if (detalleModificar == null)
            {
                return false;
            }
            EmbarqueDetalleInfo detalleSinReferencia = ContenedorEmbarque.EmbarqueDetalle.Clone();
            detalleModificar = detalleSinReferencia;
            if (DtuHoraSalida.Value != null)
            {
                detalleModificar.FechaSalida =
                    detalleModificar.FechaSalida.Date.AddHours(DtuHoraSalida.Value.Value.Hour).AddMinutes(
                        DtuHoraSalida.Value.Value.Minute);
            }
            if (DtuHoraLlegada.Value != null)
            {
                detalleModificar.FechaLlegada =
                    detalleModificar.FechaLlegada.Date.AddHours(DtuHoraLlegada.Value.Value.Hour).AddMinutes(
                        DtuHoraLlegada.Value.Value.Minute);
            }
            detalleModificar.Kilometros = 0;
            detalleModificar.Horas = 0;
            //if (tipoOrganizacion == TipoOrganizacion.CompraDirecta)
            //{
            detalleModificar.Kilometros = dudKms.Value;
            detalleModificar.Horas = dudHoras.Value ?? 0;
            //}
            detalleModificar.ListaCostoEmbarqueDetalle.ForEach(de => de.Orden = detalleModificar.Orden);
            detalleModificar.Horas = dudHoras.Value.HasValue ? dudHoras.Value.Value : 0;
            EmbarqueDetalleInfo detalleBorrar = ContenedorEmbarque.Embarque.ListaEscala.FirstOrDefault(
                n =>
                n.EmbarqueDetalleID ==
                detalleModificar.EmbarqueDetalleID &&
                n.Orden == detalleModificar.Orden);

            ContenedorEmbarque.Embarque.ListaEscala.Remove(detalleBorrar);
            ContenedorEmbarque.Embarque.ListaEscala.Add(detalleModificar);
            CargarGridEmbarques();
            return true;
        }

        /// <summary>
        /// Metodo que agrega a la Lista de Escalas un elemento Nuevo
        /// </summary>
        private bool AgregarEmbarqueDetalle()
        {
            if (ContenedorEmbarque.Embarque.ListaEscala.Count(esc => esc.Activo == EstatusEnum.Activo) > 0)
            {
                if (!ValidarEscalaExistenteOrigen())
                {
                    return false;
                }
            }
            if (ValidarEscalaExistente() || ValidarEscalaExistenteDestinoDuplicado()) // || ValidarEscalaExistenteOrigenEnDestino())
            {
                return false;
            }

            if (!ValidarCamposObligatorios())
            {
                return false;
            }
            int orden = ContenedorEmbarque.Embarque.ListaEscala.Count;
            CargarEntidadesdeAyudas();
            ContenedorEmbarque.EmbarqueDetalle.Orden = orden + 1;
            ContenedorEmbarque.EmbarqueDetalle.Activo = EstatusEnum.Activo;
            EmbarqueDetalleInfo detalle = ContenedorEmbarque.EmbarqueDetalle.Clone();
            if (DtuHoraSalida.Value != null)
            {
                detalle.FechaSalida =
                    detalle.FechaSalida.Date.AddHours(DtuHoraSalida.Value.Value.Hour).AddMinutes(
                        DtuHoraSalida.Value.Value.Minute);
            }
            if (DtuHoraLlegada.Value != null)
            {
                detalle.FechaLlegada =
                    detalle.FechaLlegada.Date.AddHours(DtuHoraLlegada.Value.Value.Hour).AddMinutes(
                        DtuHoraLlegada.Value.Value.Minute);
            }
            detalle.ListaCostoEmbarqueDetalle.ForEach(de => de.Orden = detalle.Orden);
            detalle.Horas = dudHoras.Value.HasValue ? dudHoras.Value.Value : 0;
            detalle.Kilometros = 0;
            detalle.Horas = 0;
            //if (tipoOrganizacion == TipoOrganizacion.CompraDirecta)
            //{
            detalle.Kilometros = Convert.ToDecimal(dudKms.Value);
            detalle.Horas = dudHoras.Value ?? 0;
            //}
            if (tiposEmbarqueValidaFechaSalida.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID))
            {
                if (orden > 0 && !ValidarFechaAnterior(detalle.FechaSalida))
                {
                    return false;
                }
            }
            if (tiposEmbarqueValidaMayorFechaSalida.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID))
            {
                if (orden > 0 && !ValidarFechaMayorAnterior(detalle.FechaSalida))
                {
                    return false;
                }
            }
            ContenedorEmbarque.Embarque.ListaEscala.Add(detalle);
            return true;
        }

        /// <summary>
        /// Metodo que deshabilita los controles para la siguiente escala de Ruteo
        /// </summary>
        private void EscalaSiguienteRuteo()
        {
            BloquearControlesRuteo(false);

            dudKms.Value = null;
            dudHoras.Value = null;
            TxtComentarios.Text = string.Empty;
            CargarHorasDefault();
            DtpFechaSalida.SelectedDate = DateTime.Now;
            DtpFechaLlegada.SelectedDate = DateTime.Now;
            skAyudaOrigen.Clave = skAyudaDestino.Clave;
            skAyudaOrigen.Descripcion = skAyudaDestino.Descripcion;
            AsignaDependenciasAyudaOrganizacionDestino(skAyudaDestino);

            skAyudaDestino.LimpiarCampos();
            ContenedorEmbarque.EmbarqueDetalle.Orden = 0;
            ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID = 0;
            ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle = new List<CostoEmbarqueDetalleInfo>();
            esModificacion = false;
            BtnCostos.IsEnabled = false;
        }

        /// <summary>
        /// Metodo que valida que no haya una Escala con el mismo Origen y Destino
        /// </summary>
        private bool ValidarEscalaExistente()
        {
            int organizacionOrigen = Extensor.ValorEntero(skAyudaOrigen.Clave);
            int organizacionDestino = Extensor.ValorEntero(skAyudaDestino.Clave);

            if (
                ContenedorEmbarque.Embarque.ListaEscala.Any(
                    det =>
                    det.OrganizacionOrigen.OrganizacionID == organizacionOrigen &&
                    det.OrganizacionDestino.OrganizacionID == organizacionDestino &&
                    det.Activo == EstatusEnum.Activo &&
                    det.Orden != ContenedorEmbarque.EmbarqueDetalle.Orden))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_TrayectoriaRepetida,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo que valida que no haya una Escala con el mismo Destino
        /// </summary>
        private bool ValidarEscalaExistenteDestinoDuplicado()
        {
            int organizacionDestino = Extensor.ValorEntero(skAyudaDestino.Clave);

            if (
                ContenedorEmbarque.Embarque.ListaEscala.Any(
                    det =>
                    det.OrganizacionDestino.OrganizacionID == organizacionDestino &&
                    det.Activo == EstatusEnum.Activo &&
                    det.Orden != ContenedorEmbarque.EmbarqueDetalle.Orden))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_TrayectoriaDestino,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo que valida que no haya una Escala con el mismo Destino
        /// </summary>
        private bool ValidarEscalaExistenteOrigen()
        {
            int organizacionDestino = Extensor.ValorEntero(skAyudaDestino.Clave);

            var listaOrdenada = ContenedorEmbarque.Embarque.ListaEscala.OrderBy(esc => esc.Orden);
            var primerEscala = listaOrdenada.FirstOrDefault(esc => esc.Activo == EstatusEnum.Activo);

            if (primerEscala == null)
            {
                return false;
            }
            if (primerEscala.OrganizacionOrigen.OrganizacionID.Equals(organizacionDestino) && primerEscala.Orden != ContenedorEmbarque.EmbarqueDetalle.Orden)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarques_DetinoPrimerOrigen,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metodo que carga en memoria la Configuración de un Trayecto entre un Origen y un Destino
        /// </summary>
        private bool CargarConfiguraciondeRuta()
        {
            int organizacionOrigenId = Extensor.ValorEntero(skAyudaOrigen.Clave);
            int organizacionDestinoId = Extensor.ValorEntero(skAyudaDestino.Clave);
            var configuraconEmbarquePL = new ConfiguracionEmbarquePL();

            ConfiguracionEmbarque = configuraconEmbarquePL.ObtenerPorOrganizacion(organizacionOrigenId,
                                                                                  organizacionDestinoId);

            if (ConfiguracionEmbarque == null)
            {
                return false;
            }
            CalcularDistanciasEntreOrganizaciones();
            return true;
        }

        /// <summary>
        /// Metodo que calcula la fecha de Llegada basandose en la Configuración de Trayecto
        /// </summary>
        private void CalcularDistanciasEntreOrganizaciones()
        {
            var fechaSalida = new DateTime();
            if (DtuHoraSalida.Value != null)
            {
                fechaSalida = ContenedorEmbarque.EmbarqueDetalle.FechaSalida.Date.AddHours(
                    DtuHoraSalida.Value.Value.Hour)
                    .AddMinutes(DtuHoraSalida.Value.Value.Minute);
            }
            DateTime fechaLlegadaCalculada = fechaSalida.AddHours(Convert.ToDouble(ConfiguracionEmbarque.Horas));

            DtpFechaLlegada.SelectedDate = fechaLlegadaCalculada.Date;
            DtuHoraLlegada.Text = fechaLlegadaCalculada.ToString("HH:mm");

            decimal? kilometros = ContenedorEmbarque.EmbarqueDetalle.Kilometros;
            decimal horas = ContenedorEmbarque.EmbarqueDetalle.Horas;

            dudKms.Value = ConfiguracionEmbarque.Kilometros;
            dudHoras.Value = ConfiguracionEmbarque.Horas;
            if (kilometros > 0 && tipoOrganizacion == TipoOrganizacion.CompraDirecta)
            {
                dudKms.Value = kilometros;
                dudKms.IsEnabled = true;

                if (horas > 0)
                {
                    dudHoras.Value = horas;
                }
                dudHoras.IsEnabled = true;
            }
        }

        /// <summary>
        /// Metodo que Guarda un Embarque
        /// </summary>
        private void Guardar()
        {
            CargarEntidadAyudaOrganizacion();
            if (!ValidarAntesGuardar())
            {
                return;
            }
            var embarquePL = new EmbarquePL();
            if (ContenedorEmbarque.Embarque.EmbarqueID == 0)
            {
                ContenedorEmbarque.Embarque.UsuarioCreacionID = usuarioLogueadoID;
            }
            else
            {
                ContenedorEmbarque.Embarque.UsuarioModificacionID = usuarioLogueadoID;
            }

            ContenedorEmbarque.Embarque.ListaEscala.ToList().ForEach(n =>
                {
                    if (n.EmbarqueDetalleID == 0)
                    {
                        n.UsuarioCreacionID = usuarioLogueadoID;
                    }
                    else
                    {
                        n.UsuarioModificacionID = usuarioLogueadoID;
                    }

                    n.ListaCostoEmbarqueDetalle.ForEach(cos =>
                        {
                            if (cos.CostoEmbarqueDetalleID == 0)
                            {
                                cos.UsuarioCreacionID = usuarioLogueadoID;
                            }
                            else
                            {
                                cos.UsuarioModificacionID = usuarioLogueadoID;
                            }
                        });
                });
            var foliEmbarque = embarquePL.GuardarEmbarque(ContenedorEmbarque.Embarque);

            SkMessageBox.Show(this, string.Format(Properties.Resources.RegistroProgramacionEmbarque_GuardadoExito,
                                            ConstantesVista.SaltoLinea, foliEmbarque), MessageBoxButton.OK,
                              MessageImage.Correct);
            Nuevo();
        }

        /// <summary>
        /// Metodo que valida que el Destino de la última escala sea igual al de la Organización Final
        /// </summary>
        private bool ValidarAntesGuardar()
        {
            EmbarqueDetalleInfo ultimoDestino =
                ContenedorEmbarque.Embarque.ListaEscala.OrderBy(de => de.Orden).LastOrDefault(
                    det => det.Activo == EstatusEnum.Activo);
            if (ultimoDestino == null)
            {
                return false;
            }
            if (ultimoDestino.OrganizacionDestino.OrganizacionID !=
                ContenedorEmbarque.Embarque.Organizacion.OrganizacionID)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrganizacionFinalInvalida,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }

            var escalasOrdenadas = ContenedorEmbarque.Embarque.ListaEscala
                .Where(esc => esc.Activo == EstatusEnum.Activo)
                .OrderBy(esc => esc.Orden).ToList();

            for (int escala = 0; escala < escalasOrdenadas.Count(); escala++)
            {
                var escalaActual = escalasOrdenadas[escala];
                if (escalasOrdenadas.Count() - 1 == escala)
                {
                    continue;
                }

                var escalaSiguiente = escalasOrdenadas[escala + 1];

                if (escalaSiguiente.OrganizacionOrigen.OrganizacionID != escalaActual.OrganizacionDestino.OrganizacionID)
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_ErrorEscalas,
                                 MessageBoxButton.OK, MessageImage.Stop);
                    return false;
                }

                if (escalaSiguiente.FechaSalida < escalaActual.FechaLlegada)
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarques_ErrorFechas,
                                 MessageBoxButton.OK, MessageImage.Stop);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Metodo que valida que la Fecha de Salida de la Escala, sea mayor a la Fecha de Llegada de la Escala Anterior
        /// </summary>
        private bool ValidarFechaAnterior(DateTime fechaAgregar)
        {
            EmbarqueDetalleInfo ultimoDestino = ContenedorEmbarque.Embarque.ListaEscala.LastOrDefault();
            if (ultimoDestino == null)
            {
                return false;
            }
            if (ultimoDestino.FechaLlegada > fechaAgregar)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_FechaInvalida,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metodo que valida que la Fecha de Salida de la Escala, sea menor a la Fecha de Llegada de la Escala Anterior
        /// </summary>
        private bool ValidarFechaMayorAnterior(DateTime fechaAgregar)
        {
            EmbarqueDetalleInfo ultimoDestino = ContenedorEmbarque.Embarque.ListaEscala.LastOrDefault();
            if (ultimoDestino == null)
            {
                return false;
            }
            if (ultimoDestino.FechaLlegada >= fechaAgregar)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_FechaInvalidaMayor,
                                  MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Metodo que valida todos los campos requeridos, antes de Guardar o Agregar una Escala
        /// </summary>
        private bool ValidarCamposObligatorios()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrganizacion.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_Organizacion,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrganizacion.AsignarFoco();
                return false;
            }
            if (CmbTipoEmbarque.SelectedItem == null)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_TipoEmbarque,
                                  MessageBoxButton.OK, MessageImage.Stop);
                CmbTipoEmbarque.Focus();
                return false;
            }
            if (CmbEstatus.SelectedItem == null)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_Estatus, MessageBoxButton.OK,
                                  MessageImage.Stop);
                CmbEstatus.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(skAyudaTransportista.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_Transportista,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaTransportista.AsignarFoco();
                return false;
            }
            if (string.IsNullOrWhiteSpace(skAyudaChofer.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_Chofer, MessageBoxButton.OK,
                                  MessageImage.Stop);
                skAyudaChofer.AsignarFoco();
                return false;
            }
            if (String.IsNullOrWhiteSpace(skAyudaJaula.Clave))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_PlacaJaula,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaJaula.AsignarFoco();
                return false;
            }
            if (string.IsNullOrWhiteSpace(skAyudaCamion.Clave))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_PlacaCamion,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaCamion.AsignarFoco();
                return false;
            }
            if (CmbTipoOrigen.SelectedItem == null)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_TipoOrigen,
                                  MessageBoxButton.OK, MessageImage.Stop);
                CmbTipoOrigen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrganizacionOrigen,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrigen.AsignarFoco();
                return false;
            }
            if (string.IsNullOrWhiteSpace(skAyudaDestino.Descripcion))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrganizacionDestino,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaDestino.AsignarFoco();
                return false;
            }

            decimal kilometros;
            decimal.TryParse(Convert.ToString(dudKms.Value), out kilometros);
            if (kilometros <= 0)
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_KilometrosInvalidos,
                                  MessageBoxButton.OK, MessageImage.Stop);
                dudKms.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Metodo que carga los valores de una Escala a modificar
        /// </summary>
        private void CargarDatosModificacion()
        {
            skAyudaTransportista.Clave =
                ContenedorEmbarque.EmbarqueDetalle.Proveedor.CodigoSAP;
            skAyudaTransportista.Descripcion = ContenedorEmbarque.EmbarqueDetalle.Proveedor.Descripcion;
            skAyudaTransportista.Id = ContenedorEmbarque.EmbarqueDetalle.Proveedor.ProveedorID.ToString(CultureInfo.InvariantCulture);
            skAyudaTransportista.Info = ContenedorEmbarque.EmbarqueDetalle.Proveedor;

            skAyudaChofer.Clave =
                ContenedorEmbarque.EmbarqueDetalle.Chofer.ChoferID.ToString(CultureInfo.InvariantCulture);
            skAyudaChofer.Descripcion = ContenedorEmbarque.EmbarqueDetalle.Chofer.NombreCompleto;
            skAyudaChofer.Info = ContenedorEmbarque.EmbarqueDetalle.Chofer;

            skAyudaOrigen.Clave = ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen.OrganizacionID.ToString(CultureInfo.InvariantCulture);
            skAyudaOrigen.Descripcion = ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen.Descripcion;
            skAyudaOrigen.Info = ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen;

            skAyudaDestino.Clave = ContenedorEmbarque.EmbarqueDetalle.OrganizacionDestino.OrganizacionID.ToString(CultureInfo.InvariantCulture);
            skAyudaDestino.Descripcion = ContenedorEmbarque.EmbarqueDetalle.OrganizacionDestino.Descripcion;
            skAyudaDestino.Info = ContenedorEmbarque.EmbarqueDetalle.OrganizacionDestino;

            if (!ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID.Equals(TipoEmbarque.Ruteo.GetHashCode())
                || ContenedorEmbarque.Embarque.EmbarqueID != 0)
            {
                CargarJaulasyCamionesProveedor(skAyudaTransportista.Info.ProveedorID.ToString(CultureInfo.InvariantCulture));
            }

            modificacionPorCodigo = true;
            CmbTipoOrigen.SelectedValue =
                ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen.TipoOrganizacion.TipoOrganizacionID;

            tipoOrganizacion = (TipoOrganizacion)CmbTipoOrigen.SelectedValue;

            skAyudaCamion.Descripcion = ContenedorEmbarque.EmbarqueDetalle.Camion.PlacaCamion;
            skAyudaCamion.Clave =
                ContenedorEmbarque.EmbarqueDetalle.Camion.CamionID.ToString(CultureInfo.InvariantCulture);
            skAyudaCamion.Info = ContenedorEmbarque.EmbarqueDetalle.Camion;

            skAyudaJaula.Descripcion = ContenedorEmbarque.EmbarqueDetalle.Jaula.PlacaJaula;
            skAyudaJaula.Clave = ContenedorEmbarque.EmbarqueDetalle.Jaula.JaulaID.ToString(CultureInfo.InvariantCulture);
            skAyudaJaula.Info = ContenedorEmbarque.EmbarqueDetalle.Jaula;

            DtuHoraSalida.Text = ContenedorEmbarque.EmbarqueDetalle.FechaSalida.ToString("HH:mm");
            DtpFechaSalida.SelectedDate = ContenedorEmbarque.EmbarqueDetalle.FechaSalida.Date;

            skAyudaOrganizacion.Clave = ContenedorEmbarque.Embarque.Organizacion.OrganizacionID.ToString(CultureInfo.InvariantCulture);
            skAyudaOrganizacion.Descripcion = ContenedorEmbarque.Embarque.Organizacion.Descripcion;
            skAyudaOrganizacion.Info = ContenedorEmbarque.Embarque.Organizacion;

            CargarConfiguraciondeRuta();
            AsignaDependenciasAyudaOrganizacionDestino(skAyudaDestino);
            skAyudaJaula.Dependencias = GeneraDependenciaProveedor(skAyudaTransportista.Info.ProveedorID);
            skAyudaCamion.Dependencias = GeneraDependenciaProveedor(skAyudaTransportista.Info.ProveedorID);
        }

        /// <summary>
        /// Metodo que Elimina y/o desactiva las Escalas siguientes
        /// </summary>
        private void BorrarDetallesSiguientes(int orden)
        {
            List<EmbarqueDetalleInfo> listaDetalles = ContenedorEmbarque.Embarque.ListaEscala.ToList();
            listaDetalles.RemoveAll(n => n.Orden > orden && n.EmbarqueDetalleID == 0);
            listaDetalles.Where(n => n.Orden > orden).ToList().ForEach(det => det.Activo = EstatusEnum.Inactivo);
            ContenedorEmbarque.Embarque.ListaEscala = listaDetalles;
        }

        /// <summary>
        /// Metodo que Elimina y/o desactiva el Embarque con sus respectivas escalas y costos
        /// </summary>
        private void DesactivarRegistros()
        {
            ContenedorEmbarque.Embarque.Estatus = Estatus.Cancelado.GetHashCode();
            ContenedorEmbarque.Embarque.UsuarioModificacionID = usuarioLogueadoID;
            ContenedorEmbarque.Embarque.Activo = EstatusEnum.Inactivo;
            var costoEmbarqueDetallePL = new CostoEmbarqueDetallePL();
            ContenedorEmbarque.Embarque.ListaEscala.ToList().ForEach(esc =>
                {
                    esc.Activo = EstatusEnum.Inactivo;
                    esc.UsuarioModificacionID = usuarioLogueadoID;


                    esc.ListaCostoEmbarqueDetalle =
                        costoEmbarqueDetallePL.ObtenerPorEmbarqueDetalleID(esc.EmbarqueDetalleID)
                        ?? new List<CostoEmbarqueDetalleInfo>();

                    esc.ListaCostoEmbarqueDetalle.ForEach(cos =>
                        {
                            cos.Activo = EstatusEnum.Inactivo;
                            cos.UsuarioModificacionID = usuarioLogueadoID;
                        });
                });
            var embarquePL = new EmbarquePL();
            var folioEmbarque = embarquePL.GuardarEmbarque(ContenedorEmbarque.Embarque);
            if (
                SkMessageBox.Show(this, string.Format(Properties.Resources.RegistroProgramacionEmbarque_CanceladoExito, ConstantesVista.SaltoLinea, folioEmbarque), MessageBoxButton.OK, MessageImage.Correct) ==
                MessageBoxResult.OK)
            {
                Close();
            }
        }

        /// <summary>
        /// Obtiene el Proveedor, Origen y Destino asignados en la pantalla
        /// </summary>
        /// <returns></returns>
        public EmbarqueInfo ObtenerInformacionDeEmbarque()
        {
            EmbarqueInfo embarque = new EmbarqueInfo();
            embarque.TipoEmbarque = new TipoEmbarqueInfo(){ TipoEmbarqueID = (int)CmbTipoEmbarque.SelectedValue }; 
            EmbarqueDetalleInfo embarqueDetalleInfo = new EmbarqueDetalleInfo();
            embarqueDetalleInfo.Proveedor = skAyudaTransportista.Info;
            embarqueDetalleInfo.OrganizacionOrigen = skAyudaOrigen.Info;
            embarqueDetalleInfo.OrganizacionDestino = skAyudaDestino.Info;

            IList<EmbarqueDetalleInfo> listaEmbarques = new List<EmbarqueDetalleInfo>();
            listaEmbarques.Add(embarqueDetalleInfo);
            if (embarque.TipoEmbarque.TipoEmbarqueID == (int)TipoEmbarque.Ruteo)
            {
                if (ContenedorEmbarque.Embarque.ListaEscala.Count == 1)
                {
                    listaEmbarques.Add(ContenedorEmbarque.Embarque.ListaEscala[0]);
                    listaEmbarques.Add(ContenedorEmbarque.Embarque.ListaEscala[0]);
                }
                else if (ContenedorEmbarque.Embarque.ListaEscala.Count > 1)
                {
                    foreach (EmbarqueDetalleInfo escala in ContenedorEmbarque.Embarque.ListaEscala)
                    {
                        listaEmbarques.Add(escala);
                    }
                }
            }
            embarque.ListaEscala = listaEmbarques;
            return embarque;
        }

        #endregion METODOS

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ContenedorEmbarque = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cargar la funcionalidad
        /// </summary>
        protected void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarHorasDefault();
            CargarComboTiposEmbarque();
            CargarComboTipoOrganizacion();

            if (ContenedorEmbarque.Embarque.Organizacion.OrganizacionID == 0)
            {
                skAyudaOrganizacion.AsignarFoco();
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Guardar
        /// </summary>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (esModificacion)
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_MensajeBotonAgregar,
                                         MessageBoxButton.OK, MessageImage.Stop);
                    return;
                }
                if (!ContenedorEmbarque.Embarque.ListaEscala.Any())
                {
                    if (!ValidarCamposObligatorios())
                    {
                        return;
                    }
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_FavorAgregarDetalle,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    return;
                }
                if (!ValidarEmbarqueSinEscalas())
                {
                    return;
                }
                Guardar();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Agregar
        /// </summary>
        private bool ValidarEmbarqueSinEscalas()
        {
            if (ContenedorEmbarque.Embarque.ListaEscala.All(escala => escala.Activo != EstatusEnum.Activo))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_SinEscalas,
                                          MessageBoxButton.OK, MessageImage.Stop);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Agregar
        /// </summary>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (esModificacion)
                {
                    if (!AgregarEmbarqueDetalleModificacion())
                    {
                        return;
                    }
                    CargarGridEmbarques();
                    if (tiposEmbarquesMismosDatos.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID))
                    {
                        EscalaSiguienteRuteo();
                    }
                    else
                    {
                        LimpiarControles();
                    }
                    BtnAgregar.Content = Properties.Resources.btnAgregar;
                    BtnAgregar.IsEnabled = ContenedorEmbarque.Embarque.Organizacion.OrganizacionID != ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen.OrganizacionID;

                    return;
                }
                if (tiposEmbarqueValidaUnicoDetalle.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID)
                    && ContenedorEmbarque.Embarque.ListaEscala.Any())
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_EmbarqueDirecto,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    return;
                }
                if (!AgregarEmbarqueDetalle())
                {
                    return;
                }
                CargarEntidadAyudaOrganizacion();
                CargarGridEmbarques();
                if (tiposEmbarquesMismosDatos.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID))
                {
                    EscalaSiguienteRuteo();
                }
                else
                {
                    LimpiarControles();
                }
                BloquearControles();
                BtnAgregar.Content = Properties.Resources.btnAgregar;
                BtnAgregar.IsEnabled = ContenedorEmbarque.Embarque.Organizacion.OrganizacionID != ContenedorEmbarque.EmbarqueDetalle.OrganizacionOrigen.OrganizacionID;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar el valor del Tipo de Origen
        /// </summary>
        private void CmbTipoOrigen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AsignaDependenciasAyudaOrganizacion(skAyudaOrigen, CmbTipoOrigen);

                dudKms.IsEnabled = false;
                dudHoras.IsEnabled = false;
                if (CmbTipoOrigen.SelectedValue != null)
                {
                    tipoOrganizacion = (TipoOrganizacion)CmbTipoOrigen.SelectedValue;
                    if (tipoOrganizacion == TipoOrganizacion.CompraDirecta)
                    {
                        dudKms.IsEnabled = true;
                        dudHoras.IsEnabled = true;
                    }
                }

                if (modificacionPorCodigo)
                {
                    modificacionPorCodigo = false;
                    return;
                }
                if (ContenedorEmbarque.EmbarqueDetalle != null)
                {
                    ContenedorEmbarque.EmbarqueDetalle.Horas = 0;
                }
                skAyudaOrigen.LimpiarCampos();

            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar el valor del Estatus
        /// </summary>
        private void CmbEstatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CmbEstatus.SelectedValue != null && CmbEstatus.SelectedValue.Equals(Estatus.Cancelado.GetHashCode()))
                {
                    BtnAgregar.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar una tecla en el control del Folio Embarque
        /// </summary>
        private void TxtFolioEmbarque_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter)
                {
                    return;
                }
                if (String.IsNullOrWhiteSpace(TxtFolioEmbarque.Text))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(skAyudaOrganizacion.Descripcion))
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_OrganizacionRequerida,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    return;
                }
                int embarqueId = Extensor.ValorEntero(TxtFolioEmbarque.Text);
                int organizacionId = Extensor.ValorEntero(skAyudaOrganizacion.Clave);
                ConsultarEmbarque(embarqueId, organizacionId);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Costos
        /// </summary>
        private void BtnCostos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tiposEmbarquesUnicoCosto.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID)
                    && ContenedorEmbarque.Embarque.ListaEscala.Any())
                {
                    var primerEscala = ContenedorEmbarque.Embarque.ListaEscala
                        .OrderBy(x => x.Orden)
                        .FirstOrDefault(x => x.Activo == EstatusEnum.Activo);

                    if (primerEscala == null)
                    {
                        return;
                    }

                    if (ContenedorEmbarque.Embarque.ListaEscala.Any() && ContenedorEmbarque.EmbarqueDetalle.Orden != primerEscala.Orden)
                    {
                        SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_CostosUnico,
                                          MessageBoxButton.OK, MessageImage.Stop);
                        return;
                    }
                }
                if (ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID != 0 &&
                    (ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle == null ||
                     !ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle.Any()))
                {
                    var costoEmbarqueDetallePL = new CostoEmbarqueDetallePL();

                    ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle =
                        costoEmbarqueDetallePL.ObtenerPorEmbarqueDetalleID(
                            ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID) ??
                        new List<CostoEmbarqueDetalleInfo>();
                }
                var ventanaCostos =
                    new RegistroProgramacionEmbarquesCostos(ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle)
                        {
                            Owner = this
                        };

                ventanaCostos.ShowDialog();
                if (ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID != 0)
                {
                    int embarqueDetalleId =
                        ContenedorEmbarque.EmbarqueDetalle.EmbarqueDetalleID;
                    int orden = ContenedorEmbarque.EmbarqueDetalle.Orden;
                    ContenedorEmbarque.EmbarqueDetalle.ListaCostoEmbarqueDetalle.ForEach(n =>
                        {
                            n.EmbarqueDetalleID = embarqueDetalleId;
                            n.Orden = orden;
                        });
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Eliminar del Grid
        /// </summary>
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (esModificacion)
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_EliminarConModificacion,
                                         MessageBoxButton.OK, MessageImage.Stop);
                    return;
                }


                if (
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_Eliminar,
                                      MessageBoxButton.YesNo, MessageImage.Warning) != MessageBoxResult.Yes)
                {
                    return;
                }
                var btn = (Button)e.Source;
                dynamic detalle = btn.CommandParameter;
                EmbarqueDetalleInfo elementoBorrar =
                    ContenedorEmbarque.Embarque.ListaEscala.FirstOrDefault(
                        n => n.Orden == detalle.Orden && n.Activo == EstatusEnum.Activo);

                if (elementoBorrar == null)
                {
                    return;
                }

                if (elementoBorrar.EmbarqueDetalleID != 0)
                {
                    elementoBorrar.Activo = EstatusEnum.Inactivo;
                }
                else
                {
                    ContenedorEmbarque.Embarque.ListaEscala.Remove(elementoBorrar);
                }
                int orden = elementoBorrar.Orden;
                BorrarDetallesSiguientes(orden);
                CargarGridEmbarques();
                if (tiposEmbarquesMismosDatos.Contains(ContenedorEmbarque.Embarque.TipoEmbarque.TipoEmbarqueID))
                {
                    var datosEscala = ContenedorEmbarque.Embarque.ListaEscala
                        .FirstOrDefault(x => x.Activo == EstatusEnum.Activo);
                    if (datosEscala == null)
                    {
                        LimpiarControles();
                        BloquearControlesRuteo(true);
                        return;
                    }
                    skAyudaTransportista.Clave = datosEscala.Proveedor.CodigoSAP;
                    skAyudaTransportista.Descripcion = datosEscala.Proveedor.Descripcion;
                    skAyudaTransportista.Id = datosEscala.Proveedor.ProveedorID.ToString(CultureInfo.InvariantCulture);
                    skAyudaTransportista.Info = datosEscala.Proveedor;

                    skAyudaChofer.Clave = datosEscala.Chofer.ChoferID.ToString(CultureInfo.InvariantCulture);
                    skAyudaChofer.Descripcion = datosEscala.Chofer.NombreCompleto;
                    skAyudaChofer.Info = datosEscala.Chofer;

                    CargarJaulasyCamionesProveedor(
                        skAyudaTransportista.Info.ProveedorID.ToString(CultureInfo.InvariantCulture));

                    skAyudaCamion.Info = datosEscala.Camion;
                    skAyudaCamion.Clave = datosEscala.Camion.CamionID.ToString(CultureInfo.InvariantCulture);
                    skAyudaCamion.Descripcion = datosEscala.Camion.PlacaCamion;

                    skAyudaJaula.Info = datosEscala.Jaula;
                    skAyudaJaula.Clave = datosEscala.Jaula.JaulaID.ToString(CultureInfo.InvariantCulture);
                    skAyudaJaula.Descripcion = datosEscala.Jaula.PlacaJaula;

                    EscalaSiguienteRuteo();
                }
                else
                {
                    LimpiarControles();
                }
                BtnAgregar.Content = Properties.Resources.btnAgregar;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar el valor del de la Fecha de Salida
        /// </summary>
        private void DtpFechaSalida_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ConfiguracionEmbarque == null)
                {
                    return;
                }
                CalcularDistanciasEntreOrganizaciones();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar al presionar una tecla en el control Hora de Salida
        /// </summary>
        private void DtuHoraSalida_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.OemPeriod)
                {
                    return;
                }

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    if (dudKms.IsEnabled)
                    {
                        dudKms.Focus();
                        e.Handled = true;
                    }
                    else
                    {
                        TxtComentarios.Focus();
                    }
                    return;
                }

                if ((int)e.Key == 58)
                {
                    e.Handled = false;
                }
                else if ((int)e.Key > 43 || (int)e.Key < 34)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar al presionar una tecla en el control Tipo de Embarque
        /// </summary>
        private void CmbTipoEmbarque_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    skAyudaTransportista.AsignarFoco();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar al presionar una tecla en el control Tipo de Destino
        /// </summary>
        private void CmbTipoOrigen_KeyDown(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// Evento que se ejecuta al en el control Hora Salida, al Perder el foco
        /// </summary>
        private void DtuHoraSalida_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConfiguracionEmbarque == null)
                {
                    return;
                }
                CalcularDistanciasEntreOrganizaciones();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Cancelar
        /// </summary>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Cancelar
        /// </summary>
        private void BtnCancelarEmbarque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarquesCosto_CancelarEmbarque,
                                      MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {
                    DesactivarRegistros();
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón Editar del Grid
        /// </summary>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (esModificacion)
                {
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_EditandoRegistro,
                                         MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }


                if (
                    SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_ModificarRegistro,
                                      MessageBoxButton.YesNo, MessageImage.Warning) != MessageBoxResult.Yes)
                {
                    return;
                }
                var btn = (Button)e.Source;
                dynamic detalle = btn.CommandParameter;

                EmbarqueDetalleInfo elementoModificar =
                    ContenedorEmbarque.Embarque.ListaEscala.FirstOrDefault(
                        n => n.Orden == detalle.Orden);

                if (elementoModificar == null)
                {
                    return;
                }
                if (esModificacion)
                {
                    if (elementoModificar.Orden.Equals(ContenedorEmbarque.EmbarqueDetalle.Orden))
                    {
                        SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_EditandoIgual,
                                          MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                }
                if (elementoModificar.ListaCostoEmbarqueDetalle == null)
                {
                    elementoModificar.ListaCostoEmbarqueDetalle = new List<CostoEmbarqueDetalleInfo>();
                }
                ContenedorEmbarque.EmbarqueDetalle = elementoModificar;
                decimal? kilometros = elementoModificar.Kilometros;
                decimal horas = elementoModificar.Horas;
                object dataContext = DataContext;
                DataContext = null;
                DataContext = dataContext;
                ContenedorEmbarque.EmbarqueDetalle.Kilometros = kilometros;
                ContenedorEmbarque.EmbarqueDetalle.Horas = horas;
                CargarDatosModificacion();
                esModificacion = true;
                BtnAgregar.Content = Properties.Resources.RegistroProgramacionEmbarque_botonActualizar;
                BtnCostos.IsEnabled = true;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void Fechas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                e.Handled = true;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar al presionar una tecla en el control Kilometros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DudKms_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.OemPeriod)
                {
                    return;
                }

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    if (dudHoras.IsEnabled)
                    {
                        dudHoras.Focus();
                    }
                    else
                    {
                        BtnAgregar.Focus();
                    }
                    e.Handled = true;
                    return;
                }

                if ((int)e.Key == 58)
                {
                    e.Handled = false;
                }
                else if ((int)e.Key > 43 || (int)e.Key < 34)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cambiar al presionar una tecla en el control Horas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DudHoras_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.OemPeriod)
                {
                    return;
                }

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    BtnAgregar.Focus();
                    e.Handled = true;
                    return;
                }

                if ((int)e.Key == 58)
                {
                    e.Handled = false;
                }
                else if ((int)e.Key > 43 || (int)e.Key < 34)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS
    }
}