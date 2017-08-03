using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Abasto
{
    public partial class TratamientoCentros
    {
        #region CONSTRUCTORES
        /// <summary>
        /// Constructor por default de la pantalla
        /// </summary>
        public TratamientoCentros()
        {
            try
            {
                InitializeComponent();
                InicializaContexto();
                AgregarAyudaOrganizacion();
                organizacionIdLogin = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                CargarComboTipoOrganizacion();
                //primerConsulta = true;
                CargarComboEstatus();
                ObtenerOrganizacionDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion CONSTRUCTORES

        #region PROPIEDADES
        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private FiltroTratamientoInfo FiltroTratamientoInfo
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroTratamientoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion PROPIEDADES

        #region VARIABLES
        /// <summary>
        /// Variable para tener la Organización Logueada
        /// </summary>
        private readonly int organizacionIdLogin;

        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Variable para manejar los eventos disparados en codebehind
        /// </summary>
        private bool modificacionCodigo;

        //private bool primerConsulta;

        #endregion VARIABLES

        #region METODOS

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void ObtenerOrganizacionDefault()
        {
            var organizacionPL = new OrganizacionPL();
            OrganizacionInfo organizacion = organizacionPL.ObtenerPorID(organizacionIdLogin);
            FiltroTratamientoInfo.Organizacion = organizacion;
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboTipoOrganizacion()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();
            var tipoOrganizacionTodos = new TipoOrganizacionInfo
                {
                    TipoOrganizacionID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccionar
                };
            IEnumerable<TipoOrganizacionInfo> tiposOrganizacion = tipoOrganizacionPL.ObtenerTodos(EstatusEnum.Activo);
            IEnumerable<TipoOrganizacionInfo> tiposOrganizacionFiltrada = tiposOrganizacion.Where(item => item.TipoOrganizacionID == TipoOrganizacion.Centro.GetHashCode() || item.TipoOrganizacionID == TipoOrganizacion.Descanso.GetHashCode() || item.TipoOrganizacionID == TipoOrganizacion.Cadis.GetHashCode()).ToList();
            var listaOrdenada = tiposOrganizacionFiltrada.OrderBy(tipo => tipo.Descripcion).ToList();
            cboTipoOrganizacion.ItemsSource = listaOrdenada;
        }

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            FiltroTratamientoInfo = new FiltroTratamientoInfo
            {
                Organizacion = new OrganizacionInfo
                                   {
                                       TipoOrganizacion = new TipoOrganizacionInfo
                                                              {
                                                                  TipoProceso = new TipoProcesoInfo()
                                                              }
                                   },
                TipoTratamiento = new TipoTratamientoInfo(),
                Estatus = EstatusEnum.Activo
            };
        }

        /// <summary>
        /// Metodo para consultar los Tratamientos
        /// </summary>
        public void Buscar()
        {
            ObtenerListaTratamientos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarComboEstatus()
        {
            IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
            cmbEstatus.ItemsSource = estatusEnums;
            cmbEstatus.SelectedItem = EstatusEnum.Activo;
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaTratamientos(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(FiltroTratamientoInfo, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var tratamientoPL = new TratamientoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                var resultadoInfo = tratamientoPL.Centros_ObtenerTratamientosPorFiltro(pagina, FiltroTratamientoInfo);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<TratamientoInfo>();
                    //if (!primerConsulta)
                    //{
                    //    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_SinInformacionEnBusqueda, MessageBoxButton.OK, MessageImage.Warning);                    
                    //}
                    //primerConsulta = false;
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {

            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, FiltroTratamientoInfo.Organizacion
                                                                , "PropiedadClaveCatalogoTratamiento"
                                                                , "PropiedadDescripcionCatalogoTratamiento"
                                                                , true, true) { AyudaPL = new OrganizacionPL() };

            stpAyudaOrganizacion.Children.Clear();
            stpAyudaOrganizacion.Children.Add(skAyudaOrganizacion);

            skAyudaOrganizacion.MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido;
            skAyudaOrganizacion.MensajeAgregar = Properties.Resources.Organizacion_Seleccionar;
            skAyudaOrganizacion.MensajeBusqueda = Properties.Resources.Organizacion_Busqueda;
            skAyudaOrganizacion.MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar;
            skAyudaOrganizacion.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion;
            skAyudaOrganizacion.TituloPantalla = Properties.Resources.BusquedaOrganizacion_Titulo;

            skAyudaOrganizacion.MensajeDependencias = null;
            IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
            mensajeDependencias.Add("TipoOrganizacionID",
                                    Properties.Resources.RegistroProgramacionEmbarques_SeleccionarTipoOrganizacion);
            skAyudaOrganizacion.MensajeDependencias = mensajeDependencias;
            skAyudaOrganizacion.AsignaTabIndex(1);

            AsignaDependenciasAyudaOrganizacion(skAyudaOrganizacion, cboTipoOrganizacion);
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
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
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

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion METODOS

        #region EVENTOS
        
        /// <summary>
        /// Evento que se ejecuta cuando carga la pantalla
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                AsignaDependenciasAyudaOrganizacion(skAyudaOrganizacion, cboTipoOrganizacion);
                ucPaginacion.DatosDelegado += ObtenerListaTratamientos;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = FiltroTratamientoInfo;
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se captura una tecla en la pantalla
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el botón Buscar
        /// </summary>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el botón Nuevo
        /// </summary>
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tratamientoCentrosEdicion =
                    new TratamientoCentrosEdicion
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.Tratamiento_Nuevo_Titulo }
                    };
                MostrarCentrado(tratamientoCentrosEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se da click en el botón Editar del Grid
        /// </summary>
        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var tratamientoCentrosEditar = (TratamientoInfo)btn.CommandParameter;
                var tratamientoCentrosEdicion =
                    new TratamientoCentrosEdicion(tratamientoCentrosEditar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.TratamientoEdicion_Edicion }
                    };
                MostrarCentrado(tratamientoCentrosEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tratamiento_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se cambia de valor el Combo de Tipo de Organización
        /// </summary>
        private void CboTipoOrganizacion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (modificacionCodigo)
                {
                    modificacionCodigo = false;
                    return;
                }
                FiltroTratamientoInfo.Organizacion.OrganizacionID = 0;
                AsignaDependenciasAyudaOrganizacion(skAyudaOrganizacion, cboTipoOrganizacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Tratamiento_ErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion EVENTOS
    }
}
