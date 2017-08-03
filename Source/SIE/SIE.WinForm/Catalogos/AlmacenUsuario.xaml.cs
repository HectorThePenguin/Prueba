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
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for AlmacenUsuario.xaml
    /// </summary>
    public partial class AlmacenUsuario
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private AlmacenUsuarioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AlmacenUsuarioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AlmacenUsuario()
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
                InicializaContexto();
                skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
                skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();

                skAyudaOrganizacion.AyudaConDatos += (o, args) =>
                    {
                        Contexto.Almacen.Organizacion = Contexto.Organizacion;
                        skAyudaAlmacen.LimpiarCampos();
                    };

                ucPaginacion.DatosDelegado += ObtenerListaAlmacenUsuario;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
        /// Evento para Editar un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var almacenUsuarioInfoSelecionado = (AlmacenInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (almacenUsuarioInfoSelecionado != null)
                {
                    var almacenUsuarioEdicion = new AlmacenUsuarioEdicion(almacenUsuarioInfoSelecionado)
                        {
                            ucTitulo = { TextoTitulo = Properties.Resources.AlmacenUsuario_Editar_Titulo }
                        };
                    MostrarCentrado(almacenUsuarioEdicion);
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new AlmacenUsuarioInfo
            {
                Almacen = new AlmacenInfo
                    {
                        Organizacion = new OrganizacionInfo()
                    },
                Organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo
                            {
                                TipoOrganizacionID = Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()
                            }
                    },
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaAlmacenUsuario(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private AlmacenUsuarioInfo ObtenerFiltros()
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
        private void ObtenerListaAlmacenUsuario(int inicio, int limite)
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

                //var almacenUsuarioBL = new AlmacenUsuarioBL();
                var almacenPL = new AlmacenPL();
                AlmacenUsuarioInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<AlmacenInfo> resultadoInfo = almacenPL.ObtenerPorPagina(pagina, filtros.Almacen);
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
                    gridDatos.ItemsSource = new List<AlmacenUsuario>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AlmacenUsuario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}

