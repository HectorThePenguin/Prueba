using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AdministracionRuteo.xaml
    /// </summary>
    public partial class AdministracionRuteo
    {
        #region Propiedades

        private bool Recargar;

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private RuteoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (RuteoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AdministracionRuteo()
        {
            InitializeComponent();
            InicializaContexto();
            Recargar = true;
        }
        #endregion 

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Recargar)
                {
                    CargarCboEstatus();
                    ucPaginacion.DatosDelegado += ObtenerListaRuteo;
                    ucPaginacion.AsignarValoresIniciales();
                    Contexto.NombreRuteo = string.Empty;
                    Buscar();
                    CargarAyudas();
                    Recargar = false;
                }
                
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.AdministracionRuteo_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.AdministracionRuteo_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Actualiza el contexto de la aplicacion cuando cambia el origen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="textChangedEventArgs"></param>
        private void TxtClaveOrigenOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Contexto.OrganizacionOrigen = (OrganizacionInfo)skAyudaOrganizacionOrigen.Contexto;
        }
        
        /// <summary>
        /// Actualiza el contexto de la aplicacion cuando cambia el destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="textChangedEventArgs"></param>
        private void TxtClaveDestinoOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Contexto.OrganizacionDestino = (OrganizacionInfo)skAyudaOrganizacionDestino.Contexto;
        }

        /// <summary>
        /// Actualiza el contexto de la aplicacion cuando cambia el destino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="textChangedEventArgs"></param>
        private void TxtClaveRuteoOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Contexto.RuteoID = ((RuteoInfo) skAyudaRuteo.Contexto).RuteoID;
        }
        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
           Buscar();
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            EditarRegistro(btn);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Metodo para cargar las ayudas.
        /// </summary>
        private void CargarAyudas()
        {
            
                var tRequest = new TraversalRequest(FocusNavigationDirection.First);
                skAyudaOrganizacionOrigen.MoveFocus(tRequest);
                skAyudaRuteo.ObjetoNegocio = new AdministracionRuteoPL();
                skAyudaRuteo.Contexto = Extensor.ClonarInfo(Contexto);
                skAyudaRuteo.AyudaConDatos += SkAyudaRuteoAyudaConDatos;
                skAyudaRuteo.AyudaLimpia += skAyudaRuteo_AyudaLimpia;
                skAyudaRuteo.txtClave.TextChanged += TxtClaveRuteoOnTextChanged;
                skAyudaOrganizacionOrigen.ObjetoNegocio = new OrganizacionPL();
                skAyudaOrganizacionOrigen.Contexto = Contexto.OrganizacionOrigen;
                skAyudaOrganizacionOrigen.txtClave.TextChanged += TxtClaveOrigenOnTextChanged;

                skAyudaOrganizacionOrigen.AyudaConDatos += (o, args) => skAyudaOrigen_AyudaConDatos();
                skAyudaOrganizacionOrigen.AyudaLimpia += skAyudaOrigen_AyudaLimpia;
                skAyudaOrganizacionDestino.ObjetoNegocio = new OrganizacionPL();
                skAyudaOrganizacionDestino.Contexto = Contexto.OrganizacionDestino;
                skAyudaOrganizacionDestino.txtClave.TextChanged += TxtClaveDestinoOnTextChanged;
                skAyudaOrganizacionDestino.AyudaConDatos += skAyudaDestino_AyudaConDatos;
                skAyudaOrganizacionDestino.AyudaLimpia += skAyudaDestino_AyudaLimpia;
                skAyudaOrganizacionOrigen.AsignarFoco();
            
        }

        /// <summary>
        /// Metodo para reiniciar la paginacion
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }
        
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new RuteoInfo
            {
                OrganizacionOrigen = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    },
                OrganizacionDestino = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    },
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
            };
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = EstatusEnum.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        private void Buscar()
        {
            ObtenerListaRuteo(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Método para ver el detalle del ruteo.
        /// </summary>
        /// <param name="boton"></param>
        private void EditarRegistro(Button boton)
        {
            try
            {
                AdministracionRuteoEdicion administracionRuteoEdicion = null;
                if (boton != null)
                {
                    var ruteoInfoSelecionado =
                        (RuteoInfo) Extensor.ClonarInfo(boton.CommandParameter);
                    if (ruteoInfoSelecionado != null)
                    {
                        administracionRuteoEdicion =
                            new AdministracionRuteoEdicion(ruteoInfoSelecionado)
                                {
                                    ucTitulo = {TextoTitulo = Properties.Resources.AdministracionRuteoEdicion_Titulo}
                                };
                    }
                }
                if (administracionRuteoEdicion != null)
                {
                    MostrarCentrado(administracionRuteoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministracionRuteoEdicion_ErrorVerDetalle, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private RuteoInfo ObtenerFiltros()
        {
            RuteoInfo nuevoFiltroBusqueda = new RuteoInfo();
            nuevoFiltroBusqueda.RuteoID = Contexto.RuteoID;
            nuevoFiltroBusqueda.OrganizacionOrigen = Contexto.OrganizacionOrigen;
            nuevoFiltroBusqueda.OrganizacionDestino = Contexto.OrganizacionDestino;
            nuevoFiltroBusqueda.Activo = Contexto.Activo;
            return nuevoFiltroBusqueda;
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaRuteo(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }

                var ruteoPL = new AdministracionRuteoPL();
                RuteoInfo filtros = ObtenerFiltros();
                filtros.NombreRuteo = string.Empty;
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<RuteoInfo> resultadoInfo = ruteoPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<RuteoInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionRuteo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionRuteo_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
 
        /// <summary>
        /// Método que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private void ValidaOrigenYdestino()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrganizacionOrigen.Clave) || skAyudaOrganizacionOrigen.Clave == "0")
            {
                return;
            }

            if (skAyudaOrganizacionOrigen.Clave.Equals(skAyudaOrganizacionDestino.Clave))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AdministracionRuteo_Busqueda_ErrorDestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);
                
                skAyudaOrganizacionDestino.LimpiarCampos();
                var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                skAyudaOrganizacionDestino.MoveFocus(tRequest);
            }
        }

       

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaRuteo_AyudaLimpia(object sender, EventArgs e)
        {
            skAyudaRuteo.AsignarFoco();
        }

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaOrigen_AyudaLimpia(object sender, EventArgs e)
        {
            skAyudaOrganizacionOrigen.AsignarFoco();
          
        }

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaDestino_AyudaLimpia(object sender, EventArgs e)
        {
            skAyudaOrganizacionDestino.AsignarFoco();
        }


        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SkAyudaRuteoAyudaConDatos(object sender, EventArgs e)
        {
            Contexto.RuteoID = ((RuteoInfo)skAyudaRuteo.Contexto).RuteoID;
            ((RuteoInfo)skAyudaRuteo.Contexto).OrganizacionOrigen=new OrganizacionInfo();
            ((RuteoInfo)skAyudaRuteo.Contexto).OrganizacionDestino = new OrganizacionInfo();
        }

        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaOrigen_AyudaConDatos()
        {
            if (((OrganizacionInfo)skAyudaOrganizacionOrigen.Contexto).Activo == EstatusEnum.Activo)
            {
                ValidaOrigenYdestino();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Origen_CodigoInvalido,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrganizacionOrigen.LimpiarCampos();
                skAyudaOrganizacionOrigen.AsignarFoco();
            }

        }

        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaDestino_AyudaConDatos(object sender, EventArgs e)
        {
            if (((OrganizacionInfo) skAyudaOrganizacionDestino.Contexto).Activo==EstatusEnum.Activo)
            {
                ValidaOrigenYdestino(); 
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AyudaOrganizacion_CodigoDestinoInvalido,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrganizacionDestino.LimpiarCampos();
                skAyudaOrganizacionDestino.AsignarFoco();
            }
        }
        #endregion
     }
    
}
