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
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ConfiguracionEmbarque.xaml
    /// </summary>
    public partial class ConfiguracionEmbarque
    {
        #region Propiedades
        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrigen;

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaDestino;

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ConfiguracionEmbarqueInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ConfiguracionEmbarqueInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ConfiguracionEmbarque()
        {
            InitializeComponent();
            CargarAyudas();
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
                CargarCboEstatus();
                ucPaginacion.DatosDelegado += ObtenerListaConfiguracionEmbarque;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                var tRequest = new TraversalRequest(FocusNavigationDirection.First);
                skAyudaOrigen.MoveFocus(tRequest);

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ConfiguracionEmbarque_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.ConfiguracionEmbarque_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
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
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            EditarRegistro(null);
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

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConfiguracionEmbarqueInfo
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
            ObtenerListaConfiguracionEmbarque(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Método para editar/crear una ConfiguracionEmbarque
        /// </summary>
        /// <param name="boton"></param>
        private void EditarRegistro(Button boton)
        {
            try
            {
                ConfiguracionEmbarqueEdicion configuracionEmbarqueEdicion = null;
                if (boton == null)
                {
                    configuracionEmbarqueEdicion =
                        new ConfiguracionEmbarqueEdicion
                        {
                            ucTitulo = { TextoTitulo = Properties.Resources.ConfiguracionEmbarque_Nuevo_Titulo },
                        };

                }
                else
                {
                    var configuracionEmbarqueInfoSelecionado =
                        (ConfiguracionEmbarqueInfo) Extensor.ClonarInfo(boton.CommandParameter);
                    if (configuracionEmbarqueInfoSelecionado != null)
                    {
                        configuracionEmbarqueEdicion =
                            new ConfiguracionEmbarqueEdicion(configuracionEmbarqueInfoSelecionado)
                                {
                                    ucTitulo = {TextoTitulo = Properties.Resources.ConfiguracionEmbarque_Editar_Titulo}
                                };
                    }
                }
                if (configuracionEmbarqueEdicion != null)
                {
                    MostrarCentrado(configuracionEmbarqueEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ConfiguracionEmbarque_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ConfiguracionEmbarqueInfo ObtenerFiltros()
        {
            //ConfiguracionEmbarqueInfo filtro;
            //try
            //{
            //    filtro = new ConfiguracionEmbarqueInfo
            //                 {
            //                     OrganizacionOrigen = new OrganizacionInfo
            //                                              {
            //                                                  OrganizacionID = Extensor.ValorEntero(skAyudaOrigen.Clave),
            //                                                  Descripcion = skAyudaOrigen.Descripcion
            //                                              },
            //                     OrganizacionDestino = new OrganizacionInfo
            //                                               {
            //                                                   OrganizacionID =Extensor.ValorEntero(skAyudaDestino.Clave),
            //                                                   Descripcion = skAyudaDestino.Descripcion
            //                                               },
            //                     Activo = (EstatusEnum) cboEstatus.SelectedValue
            //                 };
            //}
            //catch (Exception ex)
            //{
            //    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            //}
            return Contexto;
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaConfiguracionEmbarque(int inicio, int limite)
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

                var configuracionEmbarquePL = new ConfiguracionEmbarquePL();
                ConfiguracionEmbarqueInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ConfiguracionEmbarqueInfo> resultadoInfo = configuracionEmbarquePL.ObtenerPorPagina(pagina, filtros);
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
                    gridDatos.ItemsSource = new List<ConfiguracionEmbarque>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionEmbarque_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfiguracionEmbarque_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacionOrigen();
            AgregarAyudaOrganizacionDestino();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaOrganizacionOrigen()
        {

            skAyudaOrigen = new SKAyuda<OrganizacionInfo>(200, false, Contexto.OrganizacionOrigen
                                                   , "PropiedadClaveProgramacionEmbarque"
                                                   , "PropiedadDescripcionProgramacionEmbarque", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Origen_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Origen_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Origen_Busqueda,
                MensajeAgregar = Properties.Resources.Origen_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrigen,
                TituloPantalla = Properties.Resources.BusquedaOrigen_Titulo,
            };
            skAyudaOrigen.LlamadaMetodos += ValidaOrigenYdestino;
            skAyudaOrigen.ObtenerDatos += ValidaOrganizacionesIguales;
            skAyudaOrigen.AsignaTabIndex(0);
            stpOrigen.Children.Clear();
            stpOrigen.Children.Add(skAyudaOrigen);
        }

        /// <summary>
        /// Configura la ayuda para ligarlo con la organización origen
        /// </summary>
        private void AgregarAyudaOrganizacionDestino()
        {
            skAyudaDestino = new SKAyuda<OrganizacionInfo>(200, false, Contexto.OrganizacionDestino
                                                        , "PropiedadClaveProgramacionEmbarque"
                                                        , "PropiedadDescripcionProgramacionEmbarque", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Destino_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Destino_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Destino_Busqueda,
                MensajeAgregar = Properties.Resources.Destino_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaDestino,
                TituloPantalla = Properties.Resources.BusquedaDestino_Titulo,
            };
            skAyudaDestino.LlamadaMetodos += ValidaOrigenYdestino;
            skAyudaDestino.ObtenerDatos += ValidaOrganizacionesIguales;

            skAyudaDestino.AsignaTabIndex(1);
            stpDestino.Children.Clear();
            stpDestino.Children.Add(skAyudaDestino);
        }
        
        /// <summary>
        /// Método que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private void ValidaOrigenYdestino()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Clave) || skAyudaOrigen.Clave == "0")
            {
                return;
            }

            if (skAyudaOrigen.Clave.Equals(skAyudaDestino.Clave))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RegistroProgramacionEmbarque_DestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);
                
                skAyudaDestino.LimpiarCampos();
                var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                skAyudaDestino.MoveFocus(tRequest);
            }
        }

        /// <summary>
        /// Carga la organización destino dependiendo del tipo de movimiento
        /// </summary>
        /// <returns></returns>
        private void ValidaOrganizacionesIguales(string clave)
        {
            bool destino = skAyudaOrigen.Clave == skAyudaDestino.Clave;

            if (destino)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProgramacionEmbarque_DestinoIgualOrigen, MessageBoxButton.OK,
                                  MessageImage.Stop);

                skAyudaDestino.LimpiarCampos();
                var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                skAyudaDestino.MoveFocus(tRequest);
            }
        }
        #endregion
     }
}
