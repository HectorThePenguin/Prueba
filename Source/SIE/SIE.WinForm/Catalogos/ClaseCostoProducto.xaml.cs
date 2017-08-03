using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ClaseCostoProducto.xaml
    /// </summary>
    public partial class ClaseCostoProducto
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ClaseCostoProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ClaseCostoProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ClaseCostoProducto()
        {
            InitializeComponent();
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
                skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
                skAyudaAlmacen.AyudaConDatos += (o, args) => Contexto.Almacen.TipoAlmacenID = 0;
                ucPaginacion.DatosDelegado += ObtenerListaClaseCostoProducto;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ClaseCostoProducto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ClaseCostoProducto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var claseCostoProductoEdicion = new ClaseCostoProductoEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.ClaseCostoProducto_Nuevo_Titulo }
                };
                MostrarCentrado(claseCostoProductoEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ClaseCostoProducto_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var claseCostoProductoInfoSelecionado = (ClaseCostoProductoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (claseCostoProductoInfoSelecionado != null)
                {
                    var claseCostoProductoEdicion = new ClaseCostoProductoEdicion(claseCostoProductoInfoSelecionado)
                        {
                            ucTitulo = { TextoTitulo = Properties.Resources.ClaseCostoProducto_Editar_Titulo }
                        };
                    MostrarCentrado(claseCostoProductoEdicion);
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ClaseCostoProducto_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ClaseCostoProductoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                CuentaSAP = new CuentaSAPInfo(),
                Almacen = new AlmacenInfo
                    {
                        Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario(),
                            },
                        TipoAlmacen = new TipoAlmacenInfo()
                    },
                Producto = new ProductoInfo()
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaClaseCostoProducto(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ClaseCostoProductoInfo ObtenerFiltros()
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
        private void ObtenerListaClaseCostoProducto(int inicio, int limite)
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
                var claseCostoProductoBL = new ClaseCostoProductoBL();
                ClaseCostoProductoInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ClaseCostoProductoInfo> resultadoInfo = claseCostoProductoBL.ObtenerPorPagina(pagina, filtros);
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
                    gridDatos.ItemsSource = new List<ClaseCostoProducto>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ClaseCostoProducto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ClaseCostoProducto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

