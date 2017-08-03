using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for ParametroTrampa.xaml
    /// </summary>
    public partial class ParametroTrampa
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ParametroTrampaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ParametroTrampaInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Ayuda de Parametro
        /// </summary>
        private SKAyuda<ParametroInfo> skAyudaParametro;

        #region Constructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ParametroTrampa()
        {
            InitializeComponent();
            InicializaContexto();
            ObtenerTiposParametro();
            AgregarAyudaParametro();
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
                skAyudaTrampa.ObjetoNegocio = new TrampaPL();
                ucPaginacion.DatosDelegado += ObtenerListaParametroTrampa;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                //cmbTipoParametro.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorCargar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorCargar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parametroTrampaEdicion = new ParametroTrampaEdicion
                                                 {
                                                     ucTitulo =
                                                         {
                                                             TextoTitulo =
                                                                 Properties.Resources.ParametroTrampa_Nuevo_Titulo
                                                         }
                                                 };
                MostrarCentrado(parametroTrampaEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorNuevo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button) e.Source;
            try
            {
                var parametroTrampaInfoSelecionado =
                    (ParametroTrampaInfo) Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (parametroTrampaInfoSelecionado != null)
                {
                    var parametroTrampaEdicion = new ParametroTrampaEdicion(parametroTrampaInfoSelecionado)
                                                     {
                                                         ucTitulo =
                                                             {
                                                                 TextoTitulo =
                                                                     Properties.Resources.ParametroTrampa_Editar_Titulo
                                                             }
                                                     };
                    MostrarCentrado(parametroTrampaEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorEditar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Se manda llamar al cambiar el tipo de parametro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipoParametroChaged(object sender, SelectionChangedEventArgs e)
        {
            if (Contexto.Parametro.ParametroID > 0)
            {
                skAyudaParametro.LimpiarCampos();
            }
        }

        /// <summary>
        /// Evento para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClonar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parametroTrampaClonar = new ParametroTrampaClonar();
                MostrarCentrado(parametroTrampaClonar);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorNuevo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ParametroTrampaInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Trampa = new TrampaInfo
                                            {
                                                Organizacion = new OrganizacionInfo
                                                                   {
                                                                       OrganizacionID =
                                                                           AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                                   }
                                            },
                               Parametro = new ParametroInfo
                                               {
                                                   TipoParametro = new TipoParametroInfo()
                                               }
                           };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaParametroTrampa(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ParametroTrampaInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaParametroTrampa(int inicio, int limite)
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
                var pagina = new PaginacionInfo {Inicio = inicio, Limite = limite};

                var parametroTrampaPL = new ParametroTrampaPL();
                ParametroTrampaInfo filtros = ObtenerFiltros();
                ResultadoInfo<ParametroTrampaInfo> resultadoInfo = parametroTrampaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<ParametroTrampa>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Agrega una ayuda de parametro
        /// </summary>
        private void AgregarAyudaParametro()
        {
            skAyudaParametro = new SKAyuda<ParametroInfo>(180, false, Contexto.Parametro, "ClaveAyudaCatalogoParametro",
                                                          "DescripcionAyudaCatalogoParametro", true, true)
                                   {
                                       AyudaPL = new ParametroPL(),
                                       MensajeClaveInexistente =
                                           Properties.Resources.Parametro_CodigoInvalidoSeleccionarTipoParametro,
                                       MensajeBusquedaCerrar = Properties.Resources.Parametro_SalirSinSeleccionar,
                                       MensajeBusqueda = Properties.Resources.Parametro_Busqueda,
                                       MensajeAgregar = Properties.Resources.Parametro_Seleccionar,
                                       TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaParametro,
                                       TituloPantalla = Properties.Resources.BusquedaParametro_Titulo,
                                   };
            skAyudaParametro.LlamadaMetodos += InicializarPropiedadTipoParametro;
            skAyudaParametro.AsignaTabIndex(1);
            skAyudaParametro.IsTabStop = false;
            //stpParametro.Children.Clear();
            //stpParametro.Children.Add(skAyudaParametro);
        }

        /// <summary>
        /// Inicializa la propiedad del tipo Parametro
        /// </summary>
        private void InicializarPropiedadTipoParametro()
        {
            //if (cmbTipoParametro.SelectedIndex == 0)
            //{
            //    Contexto.Parametro.TipoParametro = new TipoParametroInfo();
            //}
        }

        /// <summary>
        /// Obtiene los tipos parametro
        /// </summary>
        private void ObtenerTiposParametro()
        {
            try
            {
                var tipoParametroPL = new TipoParametroPL();
                IList<TipoParametroInfo> tiposParametros = tipoParametroPL.ObtenerTodos(EstatusEnum.Activo);
                var tipoParametroSeleccione = new TipoParametroInfo
                                                  {
                                                      Descripcion = Properties.Resources.cbo_Seleccionar
                                                  };
                tiposParametros.Insert(0, tipoParametroSeleccione);
                //cmbTipoParametro.ItemsSource = tiposParametros;
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ParametroTrampa_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion
    }
}
