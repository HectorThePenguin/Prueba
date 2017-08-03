using System;
using System.Collections.Generic;
using System.Linq;
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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CausaPrecio.xaml
    /// </summary>
    public partial class CausaPrecio
    {

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CausaPrecioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CausaPrecioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para almacenar las Causas Salidas, y llenar el combo sin estar llendo a BD
        /// </summary>
        private IList<CausaSalidaInfo> listaCausaSalida;

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CausaPrecio()
        {
            InitializeComponent();
            InicializaContexto();
            CargarCausasSalidas();
            CargarTiposMovimientos();
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
                ucPaginacion.DatosDelegado += ObtenerListaCausaPrecio;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
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
                var causaPrecioEdicion = new CausaPrecioEdicion
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.CausaPrecio_Nuevo_Titulo }
                };
                MostrarCentrado(causaPrecioEdicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
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
                var causaPrecioInfoSelecionado = (CausaPrecioInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (causaPrecioInfoSelecionado != null)
                {
                    var causaPrecioEdicion = new CausaPrecioEdicion(causaPrecioInfoSelecionado)
                        {
                            ucTitulo = { TextoTitulo = Properties.Resources.CausaPrecio_Editar_Titulo }
                        };
                    MostrarCentrado(causaPrecioEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se dispara cuando se cambia de elemento el Combo de Tipo Movimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTipoMovimiento_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CambiarTipoMovimiento();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorCambiarTipoMovimiento, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

        #region Métodos

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }


        /// <summary>
        /// Metodo para cambiar las Causas de Salida, cuando se cambia el Tipo de Movimiento
        /// </summary>
        private void CambiarTipoMovimiento()
        {
            var causaSalidaSeleccione = new CausaSalidaInfo
            {
                CausaSalidaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccionar
            };
            var tipoMovimientoSeleccionado = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;
            if (tipoMovimientoSeleccionado == null || tipoMovimientoSeleccionado.TipoMovimientoID == 0)
            {
                var listDefault = new List<CausaSalidaInfo>();
                listDefault.Add(causaSalidaSeleccione);
                cmbCausaSalida.ItemsSource = listDefault;
                Contexto.CausaSalidaFiltro = new CausaSalidaInfo();
                cmbCausaSalida.SelectedIndex = 0;
                return;
            }

            var listaOrdenada = listaCausaSalida.Where(causa => causa.TipoMovimiento.TipoMovimientoID == tipoMovimientoSeleccionado.TipoMovimientoID).OrderBy(causa => causa.Descripcion).ToList();
            listaOrdenada.Insert(0, causaSalidaSeleccione);

            cmbCausaSalida.ItemsSource = listaOrdenada;
            if (Contexto.CausaSalida == null || Contexto.CausaSalida.CausaSalidaID == 0)
            {
                cmbCausaSalida.SelectedIndex = 0;
            }
            if (listaOrdenada.Count <= 1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecioEdicion_MsgSinCausas, MessageBoxButton.OK, MessageImage.Warning);
                cmbTipoMovimiento.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CausaPrecioInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                CausaSalidaFiltro = new CausaSalidaInfo(),
                TipoMovimientoFiltro = new TipoMovimientoInfo(),
                CausaSalida = new CausaSalidaInfo(),
                Organizacion = new OrganizacionInfo
                                   {
                                       OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                   }
            };
        }

        /// <summary>
        /// Metodo para cargar en memoria las Causas de Salida
        /// </summary>
        private void CargarCausasSalidas()
        {
            var causaSalidaPL = new CausaSalidaPL();
            listaCausaSalida = causaSalidaPL.ObtenerTodos(EstatusEnum.Activo);
        }

        /// <summary>
        /// Metodo para cargar la Causa de Salida, por Default por todos.
        /// </summary>
        private void CargarCausaSalidaTodos()
        {
            IList<CausaSalidaInfo> listaDefault = new List<CausaSalidaInfo>();
            var causaSalidaTodos = new CausaSalidaInfo
                {
                    CausaSalidaID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccionar
                };
            listaDefault.Insert(0, causaSalidaTodos);
            cmbCausaSalida.ItemsSource = listaDefault;
            cmbCausaSalida.SelectedIndex = 0;
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private void CargarTiposMovimientos()
        {
            try
            {
                var tipoMovimientoDefault = new TipoMovimientoInfo
                {
                    TipoMovimientoID = 0,
                    EsGanado = true,
                    Descripcion = Properties.Resources.cbo_Seleccionar
                };

                var tipoMovimientoPL = new TipoMovimientoPL();
                var tiposMovimiento = tipoMovimientoPL.ObtenerTodos(EstatusEnum.Activo);
                tiposMovimiento.Insert(0, tipoMovimientoDefault);
                cmbTipoMovimiento.ItemsSource = tiposMovimiento.Where(tipo => tipo.EsGanado);
                if (Contexto.CausaSalida == null || Contexto.CausaSalida.CausaSalidaID == 0)
                {
                    cmbTipoMovimiento.SelectedIndex = 0;
                    CargarCausaSalidaTodos();
                }
                else
                {
                    cmbTipoMovimiento.SelectedValue = Contexto.CausaSalida.TipoMovimiento.TipoMovimientoID;
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaCausaPrecio(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaCausaPrecio(int inicio, int limite)
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

                var causaPrecioPL = new CausaPrecioPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CausaPrecioInfo> resultadoInfo = causaPrecioPL.ObtenerPorFiltroPagina(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<CausaPrecio>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CausaPrecio_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion
    }
}
