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
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para PrecioProducto.xaml
    /// </summary>
    public partial class PrecioProducto
    {
        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private PrecioProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PrecioProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }
        #endregion

        public PrecioProducto()
        {
            InitializeComponent();
            InicializaContexto();
        }

        #region Metodos
        /// <summary>
        /// Reinicia el paginado
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Metodo para buscar por el filtro seleccionado
        /// </summary>
        private void Buscar()
        {
            ObtenerListaPrecioProducto(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene la lista de cuenta de gastos de ganado
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerListaPrecioProducto(int inicio, int limite)
        {
            try
            {
                var precioProductoPL = new PrecioProductoPL();
                PrecioProductoInfo filtros = Contexto;
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<PrecioProductoInfo> resultadoInfo = precioProductoPL.ObtenerPorPagina(pagina, filtros);
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
                    gridDatos.ItemsSource = new List<PrecioProductoInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PrecioProducto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PrecioProducto_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new PrecioProductoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo(),
                PrecioMaximo = 0,
                Producto = new ProductoInfo(),
                Activo = EstatusEnum.Activo
            };
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buscar_Click(object sender, RoutedEventArgs e)
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
                var precioProductoEdicion = new PrecioProductoEdicion()
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.PrecioProducto_Nuevo_Titulo }
                };
                MostrarCentrado(precioProductoEdicion);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PrecioProducto_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento de carga de inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaPrecioProducto;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PrecioProducto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PrecioProducto_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para Editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var precioProductoInfoSelecionado = (PrecioProductoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (precioProductoInfoSelecionado != null)
                {
                    var precioProductoInfoSelecionadoEdicion = new PrecioProductoEdicion(precioProductoInfoSelecionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.PrecioProducto_Editar_Titulo }
                    };
                    MostrarCentrado(precioProductoInfoSelecionadoEdicion);
                    ReiniciarValoresPaginador();
                    Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PrecioProducto_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion

    }
}
