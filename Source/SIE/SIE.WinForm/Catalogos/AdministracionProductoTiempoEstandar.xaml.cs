//using SIE.Base.Auxiliar;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AdministracionProductoTiempoEstandar.xaml
    /// </summary>
    public partial class AdministracionProductoTiempoEstandar
    {
        private SKAyuda<ProductoInfo> skAyudaProductos;
        public AdministracionProductoTiempoEstandar()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudaProductos();
            cmbEstatus.ItemsSource = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>();
            cmbEstatus.SelectedIndex = 1;
            ucPaginacion.DatosDelegado += ObtenerListaProductosTiempoEstandar;
            ucPaginacion.AsignarValoresIniciales();
            ObtenerListaProductosTiempoEstandar(1, 15);
        }

        private ProductoTiempoEstandarInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProductoTiempoEstandarInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new ProductoTiempoEstandarInfo
            {
                Producto = new ProductoInfo(),
                Tiempo = string.Empty,
                ProductoTiempoEstandarID = 0,
                Estatus = Services.Info.Enums.EstatusEnum.Activo
            };
        }

        /// <summary>
        /// Carga ayudas de productos
        /// </summary>
        private void CargarAyudaProductos()
        {
            var productoInfo = new ProductoInfo
            {
                ProductoId = 0,
                Activo = EstatusEnum.Activo
            };
            skAyudaProductos = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadProductoID"
                                                   , "PropiedadDescripcionMuestreoTamanoFibra",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaLeyendaProducto,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoTitulo,
            };
            skAyudaProductos.ObtenerDatos += ObtenerDatosProducto;
            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaProductos);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(string clave)
        {
            try
            {
                skAyudaProductos.Info = new ProductoInfo
                {
                    ProductoId = 0,
                    Activo = EstatusEnum.Activo
                };
                //TxtDestino.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosProducto, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }


        private void ObtenerListaProductosTiempoEstandar(int inicio, int limite)
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
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };

                var poductoTiempoEstandarPL = new ProductoTiempoEstandarPL();
                ProductoTiempoEstandarInfo filtros = ObtenerDatosSeleccionados();
                ResultadoInfo<ProductoTiempoEstandarInfo> resultadoInfo = poductoTiempoEstandarPL.ObtenerPorPagina(pagina, filtros);
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

        private ProductoTiempoEstandarInfo ObtenerDatosSeleccionados()
        {
            ProductoTiempoEstandarInfo producto = new ProductoTiempoEstandarInfo();

            if (skAyudaProductos.Clave.Trim().Length > 0)
            { 
                producto.Producto.ProductoId = int.Parse(skAyudaProductos.Clave);
            }
            else
            {
                producto.Producto.ProductoId = 0;
            }
            producto.Estatus = (EstatusEnum)cmbEstatus.SelectedItem;
            return producto;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerListaProductosTiempoEstandar(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            var tiempoEstandarEdicion = new AdministrarProductoTiempoEstandarEdicion()
            {
                ucTitulo = { TextoTitulo = Properties.Resources.AdministracionProductoTiempoEstandar_EdicionTitulo }
            };
            MostrarCentrado(tiempoEstandarEdicion);
            skAyudaProductos.LimpiarCampos();
            skAyudaProductos.AsignarFoco();
            //cmbEstatus.SelectedIndex = 0;
            //ObtenerListaProductosTiempoEstandar(1, 15);
            InicializarPaginado();

            cmbEstatus.SelectedIndex = 1;
            ucPaginacion.AsignarValoresIniciales();
            ObtenerListaProductosTiempoEstandar(1, 15);
        }

        private void InicializarPaginado()
        {
            ucPaginacion.Inicio = 1;
            ucPaginacion.Limite = 15;
        }
        private void ProductoTiempoEditar_Click(object sender, RoutedEventArgs e)
        {
            var productoTiempoEditar = (Button)e.Source;
            try
            {
                var tiempoEstandarInfoSelecionado = (ProductoTiempoEstandarInfo)Extensor.ClonarInfo(productoTiempoEditar.CommandParameter);
                if (tiempoEstandarInfoSelecionado != null)
                {
                    var tiempoEstandarEdicion = new AdministrarProductoTiempoEstandarEdicion(tiempoEstandarInfoSelecionado)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.AdministracionProductoTiempoEstandar_EdicionTitulo }
                    };
                    MostrarCentrado(tiempoEstandarEdicion);
                    InicializarPaginado();
                    skAyudaProductos.LimpiarCampos();
                    skAyudaProductos.AsignarFoco();
                    cmbEstatus.SelectedIndex = 1;
                    ucPaginacion.AsignarValoresIniciales();
                    ObtenerListaProductosTiempoEstandar(1, 15);
                    skAyudaProductos.LimpiarCampos();
                    skAyudaProductos.AsignarFoco();
                    //ReiniciarValoresPaginador();
                    //Buscar();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Banco_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }

        }
    }
}
