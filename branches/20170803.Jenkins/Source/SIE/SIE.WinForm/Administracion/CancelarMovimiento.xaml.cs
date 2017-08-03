using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para CancelarMovimiento.xaml
    /// </summary>
    public partial class CancelarMovimiento
    {
        private FechaInfo fecha;
        private PedidoInfo pedido;
        SKAyuda<PedidoInfo> skAyudaPedidos;
        private SKAyuda<EntradaProductoInfo> skAyudaEntradaCompra;
        private EntradaProductoInfo entradaProducto;
        private SKAyuda<SalidaProductoInfo> skAyudaSalidaProducto;
        private SalidaProductoInfo salidaProducto;
        private List<PedidoCancelacionMovimientosInfo> listaPedidos;
        private int organizacionId,usuarioId;

        public CancelarMovimiento()
        {
            InitializeComponent();
        }

        #region Metodos
        private void LlenaComboTipoCancelacion(){
            var tipoCancelacionPl = new TipoCancelacionPL();
            List<TipoCancelacionInfo> listaTipoCancelacion = tipoCancelacionPl.ObtenerTodos();

            if (listaTipoCancelacion != null)
            {
                cmbMovimiento.ItemsSource = listaTipoCancelacion;
                cmbMovimiento.DisplayMemberPath = "Descripcion";
                cmbMovimiento.SelectedValuePath = "TipoCancelacionId";
            }
            else
            {
                cmbMovimiento.ItemsSource = new List<TipoCancelacionInfo>();
            }
        }

        /// <summary>
        /// Inicializa el grid con la lista del detalle del pedido
        /// </summary>
        private void InicializarGrid()
        {
            dgProductos.ItemsSource = null;
            if (pedido != null && (pedido.DetallePedido != null && pedido.DetallePedido.Count > 0))
            {
                dgProductos.ItemsSource = pedido.DetallePedido;
            }

        }

        /// <summary>
        /// Muestra la pantalla para buscar un folio.
        /// </summary>
        private void AyudaBuscarFoliosPedidos()
        {
            List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
            TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.Pedido.GetHashCode());
            skAyudaPedidos = new SKAyuda<PedidoInfo>(0, false, new PedidoInfo
                    {
                        FolioPedido = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        ListaEstatusPedido = new List<EstatusInfo>
                        {
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoProgramado },
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoParcial },
                        },
                        FechaPedido = fecha.FechaActual.AddDays(-1*tipoCancelacion.DiasPermitidos),
                        Activo = EstatusEnum.Activo
                    },
                    "FolioPedidoBusquedaPorFolio",
                    "PropiedadDescripcionOrganizacionCancelacionPedido", true, 250, true)
            {
                AyudaPL = new PedidosPL(),
                MensajeClaveInexistente = Properties.Resources.ProgramacionMateriaPrima_FolioInvalido,
                MensajeBusquedaCerrar = Properties.Resources.ProgramacionMateriaPrima_PedidoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.ProgramacionMateriaPrima_Busqueda,
                MensajeAgregar = Properties.Resources.ProgramacionMateriaPrima_Seleccionar,
                TituloEtiqueta = Properties.Resources.ProgramacionMateriaPrima_lblFolio,
                TituloPantalla = Properties.Resources.BusquedaPedido_Titulo
            };
            skAyudaPedidos.ObtenerDatos += ObtenerDatosPedido;
            skAyudaPedidos.LlamadaMetodosNoExistenDatos += LimpiarTodoPedido;
            skAyudaPedidos.AsignaTabIndex(0);
            splFolio.Children.Clear();
            splFolio.Children.Add(skAyudaPedidos);
        }

        /// <summary>
        /// Inicializa los valores en la pantalla para una captura nueva.
        /// </summary>
        private void InicializarDatos()
        {
            LimpiarTodoFolioEntradaCompra();
            LimpiarTodoFolioTraspaso();
            LimpiarTodoPedido();
            CargarAyudaFolioEntradaCompra();
            txtJustificacion.Clear();
            btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CancelarMovimiento;
            txtJustificacion.Visibility = Visibility.Visible;
            lblJustificacionRequerido.Visibility = Visibility.Visible;
            lblJustificacion.Visibility = Visibility.Visible;
            listaPedidos = new List<PedidoCancelacionMovimientosInfo>();
        }

        private void LimpiarTodoPedido()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.Pedido.GetHashCode());
                skAyudaPedidos.LimpiarCampos();
                skAyudaPedidos.Info = new PedidoInfo
                {
                    FolioPedido = 0,
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    ListaEstatusPedido = new List<EstatusInfo>
                        {
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoProgramado },
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoParcial },
                        },
                    FechaPedido = fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos),
                    Activo = EstatusEnum.Activo
                };
                InicializarGrid();
                pedido = skAyudaPedidos.Info;
                skAyudaPedidos.LimpiarCampos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del pedido que el usuario selecciono en la ayuda.
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosPedido(String clave)
        {
            pedido = skAyudaPedidos.Info;

            List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
            TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.Pedido.GetHashCode());
            skAyudaPedidos.Info = new PedidoInfo
            {
                FolioPedido = 0,
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                ListaEstatusPedido = new List<EstatusInfo>
                        {
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoProgramado },
                            new EstatusInfo { EstatusId = (int)Estatus.PedidoParcial },
                        },
                FechaPedido = fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos),
                Activo = EstatusEnum.Activo
            };

            if (pedido != null)
            {
                if (pedido.EstatusPedido.EstatusId == (int)Estatus.PedidoProgramado || pedido.EstatusPedido.EstatusId == (int)Estatus.PedidoParcial)
                {
                    if (pedido.DetallePedido != null && pedido.DetallePedido.Count > 0)
                    {
                        PedidosPL pedidoPl = new PedidosPL();
                        pedido = pedidoPl.ObtenerPedidosCompleto(pedido);

                        PedidoDetallePL pedidoDetallePl = new PedidoDetallePL();
                        pedido.DetallePedido = pedidoDetallePl.ObtenerDetallePedido(pedido);

                        ProgramacionMateriaPrimaPL programacionMateriaPrimaPl = new ProgramacionMateriaPrimaPL();
                        PesajeMateriaPrimaPL pesajeMateriaPrimaPl = new PesajeMateriaPrimaPL();

                        foreach(PedidoDetalleInfo pedidoDetalle in pedido.DetallePedido){
                            pedidoDetalle.ProgramacionMateriaPrima = programacionMateriaPrimaPl.ObtenerProgramacionMateriaPrima(pedidoDetalle);
                        }

                        List<PedidoCancelacionMovimientosInfo> listaPedido = new List<PedidoCancelacionMovimientosInfo>();

                        foreach (PedidoDetalleInfo pedidoDetalle in pedido.DetallePedido)
                        {
                            if (pedidoDetalle.ProgramacionMateriaPrima != null)
                            {
                                foreach (ProgramacionMateriaPrimaInfo programacion in pedidoDetalle.ProgramacionMateriaPrima)
                                {
                                    programacion.PesajeMateriaPrima = pesajeMateriaPrimaPl.ObtenerPesajesPorProgramacionIDSinActivo(programacion.ProgramacionMateriaPrimaId);
                                    if (programacion.PesajeMateriaPrima != null)
                                    {
                                        foreach (PesajeMateriaPrimaInfo pesaje in programacion.PesajeMateriaPrima)
                                        {
                                            PedidoCancelacionMovimientosInfo pedidoLista = new PedidoCancelacionMovimientosInfo();
                                            pedidoLista.Producto = pedidoDetalle.Producto;
                                            pedidoLista.CantidadSolicitada = pedidoDetalle.CantidadSolicitada;
                                            pedidoLista.CantidadProgramada = programacion.CantidadProgramada;
                                            pedidoLista.AlmacenInventarioLoteOrigen = programacion.InventarioLoteOrigen;
                                            pedidoLista.Ticket = pesaje.Ticket;
                                            pedidoLista.CantidadEntregada = pesaje.PesoBruto - pesaje.PesoTara;
                                            pedidoLista.AlmacenInventarioLoteDestino = pedidoDetalle.InventarioLoteDestino;
                                            pedidoLista.CantidadPendiente = programacion.CantidadProgramada - programacion.CantidadEntregada;
                                            pedidoLista.PedidoId = pedidoDetalle.PedidoId;
                                            pedidoLista.FolioPedido = pedido.FolioPedido;
                                            pedidoLista.PesajeMateriaPrimaId = pesaje.PesajeMateriaPrimaID;

                                            listaPedido.Add(pedidoLista);
                                        }
                                    }
                                    else
                                    {
                                        PedidoCancelacionMovimientosInfo pedidoLista = new PedidoCancelacionMovimientosInfo();
                                        pedidoLista.Producto = pedidoDetalle.Producto;
                                        pedidoLista.CantidadSolicitada = pedidoDetalle.CantidadSolicitada;
                                        pedidoLista.CantidadProgramada = programacion.CantidadProgramada;
                                        pedidoLista.AlmacenInventarioLoteOrigen = programacion.InventarioLoteOrigen;
                                        pedidoLista.ProgramacionMateriaPrimaId = programacion.ProgramacionMateriaPrimaId;
                                        pedidoLista.PedidoId = pedidoDetalle.PedidoId;
                                        pedidoLista.FolioPedido = pedido.FolioPedido;

                                        listaPedido.Add(pedidoLista);
                                    }
                                }
                            }
                        }

                        dgProductos.ItemsSource = listaPedido;
                        listaPedidos = listaPedido;
                    }
                    else
                    {
                        InicializarDatos();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ProgramacionMateriaPrima_PedidoSinDetalle,
                            MessageBoxButton.OK, MessageImage.Warning);
                        skAyudaPedidos.Clave = "";
                        skAyudaPedidos.AsignarFoco();
                    }
                }
                else
                {
                    pedido = null;
                    InicializarGrid();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.ProgramacionMateriaPrima_FolioInvalido,
                            MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaPedidos.Clave = "";
                    skAyudaPedidos.AsignarFoco();
                }
            }
            else
            {
                pedido = null;
                InicializarGrid();
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.ProgramacionMateriaPrima_FolioInvalido,
                        MessageBoxButton.OK, MessageImage.Warning);
                skAyudaPedidos.Clave = "";
                skAyudaPedidos.AsignarFoco();
            }
        }

        #endregion

        private void cmbMovimiento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                InicializarDatos();
                if(cmbMovimiento.SelectedIndex >= 0){
                    TipoCancelacionInfo tipoCancelacionSeleccionado = (TipoCancelacionInfo) cmbMovimiento.SelectedItem;

                    if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.EntradaCompra.GetHashCode())
                    {
                        //Funcionalidad entrada por compra
                        tbEntradaCompra.Visibility = System.Windows.Visibility.Visible;
                        dgProductos.Visibility = Visibility.Hidden;
                        CargarAyudaFolioEntradaCompra();
                        btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CancelarMovimiento;
                        txtJustificacion.Visibility = Visibility.Visible;
                        lblJustificacionRequerido.Visibility = Visibility.Visible;
                        lblJustificacion.Visibility = Visibility.Visible;
                    }
                    else if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.EntradaTraspaso.GetHashCode())
                    {
                        //Funcionalidad entrada por traspaso
                        tbEntradaCompra.Visibility = System.Windows.Visibility.Visible;
                        dgProductos.Visibility = Visibility.Hidden;
                        CargarAyudaFolioEntradaTraspaso();
                        btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CancelarMovimiento;
                        txtJustificacion.Visibility = Visibility.Visible;
                        lblJustificacionRequerido.Visibility = Visibility.Visible;
                        lblJustificacion.Visibility = Visibility.Visible;
                    }
                    else if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.VentaTraspaso.GetHashCode())
                    {
                        //Funcionalidad venta y traspaso
                        tbEntradaCompra.Visibility = System.Windows.Visibility.Visible;
                        dgProductos.Visibility = Visibility.Hidden;
                        CargarAyudaVentaTraspaso();
                        btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CancelarMovimiento;
                        txtJustificacion.Visibility = Visibility.Visible;
                        lblJustificacionRequerido.Visibility = Visibility.Visible;
                        lblJustificacion.Visibility = Visibility.Visible;
                    }
                    else if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.Pedido.GetHashCode())
                    {
                        //Funcionalidad pedido
                        tbEntradaCompra.Visibility = System.Windows.Visibility.Hidden;
                        dgProductos.Visibility = Visibility.Visible;
                        AyudaBuscarFoliosPedidos();
                        btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CerrarPedido;
                        txtJustificacion.Visibility = Visibility.Hidden;
                        lblJustificacionRequerido.Visibility = Visibility.Hidden;
                        lblJustificacion.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #region Salida

        private void CargarAyudaVentaTraspaso()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.VentaTraspaso.GetHashCode());
                skAyudaSalidaProducto = new SKAyuda<SalidaProductoInfo>
                (0, false, new SalidaProductoInfo
                    {
                        FolioSalida = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        FechaSalida = fecha.FechaActual.AddDays(-1*tipoCancelacion.DiasPermitidos),
                        listaTipoMovimiento = new List<TipoMovimientoInfo>(){
                            new TipoMovimientoInfo(){TipoMovimientoID =TipoMovimiento.ProductoSalidaVenta.GetHashCode()},
                            new TipoMovimientoInfo(){TipoMovimientoID = TipoMovimiento.ProductoSalidaTraspaso.GetHashCode()}
                        },
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadClaveCancelacion",
                    "PropiedadDescripcionCancelacion", true, 250, true)
                    {
                        AyudaPL = new SalidaProductoPL(),
                        MensajeClaveInexistente = Properties.Resources.CancelarMovimiento_AyudaFolioInvalidoEntradaCompra,
                        MensajeBusquedaCerrar = Properties.Resources.CancelarMovimiento_SalirSinSeleccionarEntradaCompra,
                        MensajeBusqueda = Properties.Resources.CancelarMovimiento_BusquedaEntradaCompra,
                        MensajeAgregar = Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra,
                        TituloEtiqueta = Properties.Resources.CancelarMovimiento_LeyendaBusquedaEntradaCompra,
                        TituloPantalla = Properties.Resources.CancelarMovimiento_Busqueda_TituloEntradaCompra
                    };
                skAyudaSalidaProducto.ObtenerDatos += ObtenerDatosSalida;
                skAyudaSalidaProducto.LlamadaMetodosNoExistenDatos += LimpiarTodoSalida;
                skAyudaSalidaProducto.AsignaTabIndex(0);
                splFolio.Children.Clear();
                splFolio.Children.Add(skAyudaSalidaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void ObtenerDatosSalida(string folio)
        {
            try
            {
                if (!String.IsNullOrEmpty(folio))
                {
                    SalidaProductoInfo salida = skAyudaSalidaProducto.Info;

                    SalidaProductoPL salidaProductoPl = new SalidaProductoPL();
                    salida.FolioSalida =int.Parse( folio);
                    salida.Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionId };
                    salida.Activo = EstatusEnum.Activo;
                    salida = salidaProductoPl.ObtenerPorFolioSalida(salida);

                    if (salida != null)
                    {
                        List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                        TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.VentaTraspaso.GetHashCode());
                        if (salida.FechaSalida >= fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos))
                        {
                            AlmacenPL almacenMovimientoPl = new AlmacenPL();
                            salida.AlmacenMovimiento = almacenMovimientoPl.ObtenerAlmacenMovimiento(salida.AlmacenMovimiento);
                            if (salida.AlmacenMovimiento != null)
                            {
                                txtProductoID.Text = salida.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Producto.ProductoId.ToString();
                                txtProductoDescripcion.Text = salida.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Producto.Descripcion;

                                if (salida.TipoMovimiento.TipoMovimientoID == TipoMovimiento.ProductoSalidaTraspaso.GetHashCode())
                                {
                                    CuentaSAPPL cuentaSapPl = new CuentaSAPPL();
                                    salida.CuentaSAP = cuentaSapPl.ObtenerPorID(salida.CuentaSAP.CuentaSAPID);
                                    txtCuentaProveedorID.Text = salida.CuentaSAP.CuentaSAP.ToString();
                                    txtCuentaProveedorDescripcion.Text = salida.CuentaSAP.Descripcion;

                                    OrganizacionPL organizacionPl = new OrganizacionPL();
                                    salida.OrganizacionDestino = organizacionPl.ObtenerPorID(salida.OrganizacionDestino.OrganizacionID);
                                    txtClienteDivisionID.Text = salida.OrganizacionDestino.OrganizacionID.ToString();
                                    txtClienteDivisionDescripcion.Text = salida.OrganizacionDestino.Descripcion;
                                }
                                else if (salida.TipoMovimiento.TipoMovimientoID == TipoMovimiento.ProductoSalidaVenta.GetHashCode())
                                {
                                    txtCuentaProveedorID.Clear();
                                    txtCuentaProveedorDescripcion.Clear();

                                    ClientePL clientePl = new ClientePL();
                                    salida.Cliente = clientePl.ObtenerPorID(salida.Cliente.ClienteID);
                                    txtClienteDivisionID.Text = salida.Cliente.CodigoSAP.ToString();
                                    txtClienteDivisionDescripcion.Text = salida.Cliente.Descripcion;
                                }

                                AlmacenInventarioLotePL almacenInventarioLotePl = new AlmacenInventarioLotePL();
                                salida.AlmacenInventarioLote = almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(salida.AlmacenInventarioLote.AlmacenInventarioLoteId);
                                txtLote.Text = salida.AlmacenInventarioLote.Lote.ToString();
                                txtCantidad.Text = (salida.PesoBruto - salida.PesoTara).ToString("N2",CultureInfo.GetCultureInfo("en-US"));

                                decimal costos = 0;
                                if (salida.AlmacenMovimiento.ListaAlmacenMovimientoCosto != null)
                                {
                                    costos = salida.AlmacenMovimiento.ListaAlmacenMovimientoCosto.Sum(registro=>registro.Importe);
                                }

                                txtImporte.Text = (salida.Importe + salida.AlmacenMovimiento.ListaAlmacenMovimientoDetalle.Sum(registro => registro.Importe) + costos).ToString("N2", CultureInfo.GetCultureInfo("en-US"));

                                salidaProducto = salida;
                                skAyudaSalidaProducto.Info = new SalidaProductoInfo
                                {
                                    FolioSalida = 0,
                                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                                    FechaSalida = fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos),
                                    listaTipoMovimiento = new List<TipoMovimientoInfo>(){
                                        new TipoMovimientoInfo(){TipoMovimientoID =TipoMovimiento.ProductoSalidaVenta.GetHashCode()},
                                        new TipoMovimientoInfo(){TipoMovimientoID = TipoMovimiento.ProductoSalidaTraspaso.GetHashCode()}
                                    },
                                    Activo = EstatusEnum.Activo
                                };
                            }
                            else
                            {
                                LimpiarTodoSalida();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                            }
                        }
                        else
                        {
                            LimpiarTodoSalida();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.CancelarMovimiento_FechaAntigua, MessageBoxButton.OK,
                                                    MessageImage.Stop);
                        }
                    }
                    else
                    {
                        LimpiarTodoSalida();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                MessageImage.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarTodoSalida()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.VentaTraspaso.GetHashCode());
                skAyudaSalidaProducto.LimpiarCampos();
                skAyudaSalidaProducto.Info = new SalidaProductoInfo
                    {
                        FolioSalida = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        FechaSalida = fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos),
                        listaTipoMovimiento = new List<TipoMovimientoInfo>(){
                            new TipoMovimientoInfo(){TipoMovimientoID =TipoMovimiento.ProductoSalidaVenta.GetHashCode()},
                            new TipoMovimientoInfo(){TipoMovimientoID = TipoMovimiento.ProductoSalidaTraspaso.GetHashCode()}
                        },
                        Activo = EstatusEnum.Activo
                    };
                txtProductoID.Clear();
                txtProductoDescripcion.Clear();

                txtClienteDivisionID.Clear();
                txtClienteDivisionDescripcion.Clear();

                txtCuentaProveedorID.Clear();
                txtCuentaProveedorDescripcion.Clear();

                txtLote.Clear();
                txtCantidad.Clear();
                txtImporte.Clear();

                txtJustificacion.Clear();
                salidaProducto = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion

        #region Ayudas

        /// <summary>
        /// Carga la ayuda de entrada por compra
        /// </summary>
        private void CargarAyudaFolioEntradaCompra()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.EntradaCompra.GetHashCode());
                skAyudaEntradaCompra = new SKAyuda<EntradaProductoInfo>(0,
                    false,
                    new EntradaProductoInfo()
                    {
                        Folio = 0,
                        Producto = new ProductoInfo{
                            Familias =new List<FamiliaInfo> {
                                new FamiliaInfo{FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()}
                            },
                            SubFamilias= new List<SubFamiliaInfo> {
                                new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode()}
                            }
                        },
                        Fecha = fecha.FechaActual.AddDays(-1*tipoCancelacion.DiasPermitidos),
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadFolioCancelacionEntradaCompra",
                    "PropiedadDescripcionProductoCancelacionEntradaCompra",
                    true,
                    250,
                    true)
                {
                    AyudaPL = new EntradaProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.CancelarMovimiento_AyudaFolioInvalidoEntradaCompra,
                    MensajeBusquedaCerrar = Properties.Resources.CancelarMovimiento_SalirSinSeleccionarEntradaCompra,
                    MensajeBusqueda = Properties.Resources.CancelarMovimiento_BusquedaEntradaCompra,
                    MensajeAgregar = Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra,
                    TituloEtiqueta = Properties.Resources.CancelarMovimiento_LeyendaBusquedaEntradaCompra,
                    TituloPantalla = Properties.Resources.CancelarMovimiento_Busqueda_TituloEntradaCompra
                };

                entradaProducto = null;
                skAyudaEntradaCompra.ObtenerDatos += ObtenerDatosFolioEntradaCompra;
                skAyudaEntradaCompra.LlamadaMetodosNoExistenDatos += LimpiarTodoFolioEntradaCompra;

                skAyudaEntradaCompra.AsignaTabIndex(0);
                splFolio.Children.Clear();
                splFolio.Children.Add(skAyudaEntradaCompra);
                skAyudaEntradaCompra.TabIndex = 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Carga la ayuda de entrada por compra
        /// </summary>
        private void CargarAyudaFolioEntradaTraspaso()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.EntradaTraspaso.GetHashCode());
                skAyudaEntradaCompra = new SKAyuda<EntradaProductoInfo>(0,
                    false,
                    new EntradaProductoInfo()
                    {
                        Folio = 0,
                        Producto = new ProductoInfo
                        {
                            Familias = new List<FamiliaInfo> {
                                new FamiliaInfo{FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()}
                            },
                            SubFamilias = new List<SubFamiliaInfo> {
                                new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode()},
                                new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode()}
                            }
                        },
                        Fecha = fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos),
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadFolioCancelacionEntradaCompra",
                    "PropiedadDescripcionProductoCancelacionEntradaTraspaso",
                    true,
                    250,
                    true)
                {
                    AyudaPL = new EntradaProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.CancelarMovimiento_AyudaFolioInvalidoEntradaCompra,
                    MensajeBusquedaCerrar = Properties.Resources.CancelarMovimiento_SalirSinSeleccionarEntradaCompra,
                    MensajeBusqueda = Properties.Resources.CancelarMovimiento_BusquedaEntradaCompra,
                    MensajeAgregar = Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra,
                    TituloEtiqueta = Properties.Resources.CancelarMovimiento_LeyendaBusquedaEntradaCompra,
                    TituloPantalla = Properties.Resources.CancelarMovimiento_Busqueda_TituloEntradaCompra
                };

                entradaProducto = null;
                skAyudaEntradaCompra.ObtenerDatos += ObtenerDatosFolioEntradaTraspaso;
                skAyudaEntradaCompra.LlamadaMetodosNoExistenDatos += LimpiarTodoFolioTraspaso;

                skAyudaEntradaCompra.AsignaTabIndex(0);
                splFolio.Children.Clear();
                splFolio.Children.Add(skAyudaEntradaCompra);
                skAyudaEntradaCompra.TabIndex = 1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Obtiene los datos a mostrar en pantalla
        /// </summary>
        /// <param name="folio"></param>
        private void ObtenerDatosFolioEntradaTraspaso(string folio)
        {
            try
            {
                if (!String.IsNullOrEmpty(folio))
                {
                    EntradaProductoInfo entrada = skAyudaEntradaCompra.Info;

                    EntradaProductoPL entradaProductoPl = new EntradaProductoPL();
                    entrada = entradaProductoPl.ObtenerEntradaProductoPorFolio(int.Parse(folio),organizacionId);

                    if (entrada != null)
                    {
                        List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                        TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.EntradaTraspaso.GetHashCode());
                        if (entrada.Fecha >= fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos))
                        {
                            if (entrada.AlmacenMovimiento != null)
                            {
                                AlmacenPL almacenPl = new AlmacenPL();
                                entrada.AlmacenMovimiento = almacenPl.ObtenerAlmacenMovimiento(entrada.AlmacenMovimiento);

                                if (entrada.AlmacenMovimiento != null)
                                {
                                    if (entrada.AlmacenMovimiento.TipoMovimiento.TipoMovimientoID == TipoMovimiento.EntradaAlmacen.GetHashCode())
                                    {
                                        ProductoPL productoPl = new ProductoPL();
                                        entrada.Producto = productoPl.ObtenerPorID(entrada.Producto);
                                        if (entrada.Producto != null)
                                        {
                                            txtProductoID.Text = entrada.Producto.ProductoId.ToString();
                                            txtProductoDescripcion.Text = entrada.Producto.Descripcion;

                                            if (entrada.Contrato.ContratoId > 0)
                                            {
                                                txtCuentaProveedorID.Text = entrada.Contrato.Cuenta.CuentaSAP;
                                                txtCuentaProveedorDescripcion.Text = entrada.Contrato.Cuenta.Descripcion;
                                            }
                                            if (entrada.AlmacenInventarioLote != null)
                                            {
                                                AlmacenInventarioLotePL almacenInventarioLotePl = new AlmacenInventarioLotePL();
                                                entrada.AlmacenInventarioLote = almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(entrada.AlmacenInventarioLote.AlmacenInventarioLoteId);
                                                if (entrada.AlmacenInventarioLote != null)
                                                {
                                                    txtLote.Text = entrada.AlmacenInventarioLote.Lote.ToString();

                                                    if (entrada.AlmacenMovimiento.ListaAlmacenMovimientoDetalle != null)
                                                    {
                                                        txtCantidad.Text = entrada.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad.ToString("N2", CultureInfo.InvariantCulture);
                                                        decimal costos = 0;
                                                        if (entrada.AlmacenMovimiento.ListaAlmacenMovimientoCosto != null)
                                                        {
                                                            costos = entrada.AlmacenMovimiento.ListaAlmacenMovimientoCosto.Sum(registro => registro.Importe);
                                                        }
                                                        txtImporte.Text = (entrada.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe + costos).ToString("N2", CultureInfo.InvariantCulture);
                                                    }

                                                    skAyudaEntradaCompra.Info.Producto = new ProductoInfo
                                                    {
                                                        Familias = new List<FamiliaInfo> {
                                                    new FamiliaInfo{FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()}
                                                },
                                                        SubFamilias = new List<SubFamiliaInfo> {
                                                    new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode()},
                                                    new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode()}
                                                }
                                                    };
                                                    skAyudaEntradaCompra.Info.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };
                                                    skAyudaEntradaCompra.Info.Activo = EstatusEnum.Activo;
                                                    skAyudaEntradaCompra.Info.Fecha = fecha.FechaActual.AddDays(-1*tipoCancelacion.DiasPermitidos);

                                                    TipoMovimientoPL tipoMovimientoPl = new TipoMovimientoPL();
                                                    entrada.AlmacenMovimiento.TipoMovimiento = tipoMovimientoPl.ObtenerPorID(entrada.AlmacenMovimiento.TipoMovimiento.TipoMovimientoID);
                                                    entradaProducto = entrada;
                                                }
                                                else
                                                {
                                                    LimpiarTodoFolioTraspaso();
                                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                                }
                                            }
                                            else
                                            {
                                                LimpiarTodoFolioTraspaso();
                                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                            }
                                        }
                                        else
                                        {
                                            LimpiarTodoFolioTraspaso();
                                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                        }
                                    }
                                    else
                                    {
                                        LimpiarTodoFolioTraspaso();
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                    }
                                }
                                else
                                {
                                    LimpiarTodoFolioTraspaso();
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                }
                            }
                            else
                            {
                                LimpiarTodoFolioTraspaso();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                            }
                        }
                        else
                        {
                            LimpiarTodoFolioTraspaso();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                        }
                    }
                    else
                    {
                        LimpiarTodoFolioTraspaso();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_FechaAntigua, MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
                else
                {
                    LimpiarTodoFolioTraspaso();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                    MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                LimpiarTodoFolioTraspaso();
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                    MessageImage.Stop);
            }
        }
        /// <summary>
        /// Limpia la informacion de la entrada por compra
        /// </summary>
        private void LimpiarTodoFolioEntradaCompra()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.EntradaCompra.GetHashCode());
                skAyudaEntradaCompra.LimpiarCampos();
                skAyudaEntradaCompra.Info = new EntradaProductoInfo()
                {
                    Folio = 0,
                    Producto = new ProductoInfo
                    {
                        Familias = new List<FamiliaInfo> {
                                        new FamiliaInfo{FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()}
                                    },
                        SubFamilias = new List<SubFamiliaInfo> {
                                        new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode()},
                                        new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode()}
                                    }
                    },
                    Fecha = fecha.FechaActual.AddDays(-1*tipoCancelacion.DiasPermitidos),
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    Activo = EstatusEnum.Activo
                };

                txtProductoID.Clear();
                txtProductoDescripcion.Clear();

                txtClienteDivisionID.Clear();
                txtClienteDivisionDescripcion.Clear();

                txtCuentaProveedorID.Clear();
                txtCuentaProveedorDescripcion.Clear();

                txtLote.Clear();
                txtCantidad.Clear();
                txtImporte.Clear();

                txtJustificacion.Clear();

                entradaProducto = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Limpia la informacion de la entrada por compra
        /// </summary>
        private void LimpiarTodoFolioTraspaso()
        {
            try
            {
                List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.EntradaCompra.GetHashCode());
                skAyudaEntradaCompra.LimpiarCampos();
                skAyudaEntradaCompra.Info = new EntradaProductoInfo()
                {
                    Folio = 0,
                    Producto = new ProductoInfo
                    {
                        Familias = new List<FamiliaInfo> {
                                        new FamiliaInfo{FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()}
                                    },
                        SubFamilias = new List<SubFamiliaInfo> {
                                        new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode()}
                                    }
                    },
                    Fecha = fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos),
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    Activo = EstatusEnum.Activo
                };

                txtProductoID.Clear();
                txtProductoDescripcion.Clear();

                txtClienteDivisionID.Clear();
                txtClienteDivisionDescripcion.Clear();

                txtCuentaProveedorID.Clear();
                txtCuentaProveedorDescripcion.Clear();

                txtLote.Clear();
                txtCantidad.Clear();
                txtImporte.Clear();

                txtJustificacion.Clear();

                entradaProducto = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// OBtiene los datos a mostrar en la pantalla
        /// </summary>
        /// <param name="folio"></param>
        private void ObtenerDatosFolioEntradaCompra(string folio)
        {
            try
            {
                if (!String.IsNullOrEmpty(folio))
                {
                    EntradaProductoInfo entrada = skAyudaEntradaCompra.Info;

                    EntradaProductoPL entradaProductoPl = new EntradaProductoPL();
                    entrada = entradaProductoPl.ObtenerEntradaProductoPorFolio(int.Parse(folio),organizacionId);

                    if (entrada != null)
                    {
                        List<TipoCancelacionInfo> listaTipoCancelacion = (List<TipoCancelacionInfo>)cmbMovimiento.ItemsSource;
                        TipoCancelacionInfo tipoCancelacion = listaTipoCancelacion.FirstOrDefault(registro => registro.TipoCancelacionId == TipoCancelacionEnum.EntradaTraspaso.GetHashCode());
                        if (entrada.Fecha >= fecha.FechaActual.AddDays(-1 * tipoCancelacion.DiasPermitidos))
                        {
                            if (entrada.AlmacenMovimiento != null)
                            {
                                AlmacenPL almacenPl = new AlmacenPL();
                                entrada.AlmacenMovimiento = almacenPl.ObtenerAlmacenMovimiento(entrada.AlmacenMovimiento);

                                if (entrada.AlmacenMovimiento != null)
                                {
                                    if (entrada.AlmacenMovimiento.TipoMovimiento != null)
                                    {
                                        if (entrada.AlmacenMovimiento.TipoMovimiento.TipoMovimientoID == TipoMovimiento.EntradaPorCompra.GetHashCode())
                                        {
                                            ProductoPL productoPl = new ProductoPL();
                                            entrada.Producto = productoPl.ObtenerPorID(entrada.Producto);
                                            if (entrada.Producto != null)
                                            {
                                                txtProductoID.Text = entrada.Producto.ProductoId.ToString();
                                                txtProductoDescripcion.Text = entrada.Producto.Descripcion;

                                                if (entrada.Contrato.ContratoId > 0)
                                                {
                                                    txtCuentaProveedorID.Text = entrada.Contrato.Proveedor.CodigoSAP;
                                                    txtCuentaProveedorDescripcion.Text = entrada.Contrato.Proveedor.Descripcion;
                                                }

                                                if (entrada.AlmacenInventarioLote != null)
                                                {
                                                    AlmacenInventarioLotePL almacenInventarioLotePl = new AlmacenInventarioLotePL();
                                                    entrada.AlmacenInventarioLote = almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(entrada.AlmacenInventarioLote.AlmacenInventarioLoteId);
                                                    if (entrada.AlmacenInventarioLote != null)
                                                    {
                                                        txtLote.Text = entrada.AlmacenInventarioLote.Lote.ToString();

                                                        if (entrada.AlmacenMovimiento.ListaAlmacenMovimientoDetalle != null)
                                                        {
                                                            txtCantidad.Text = entrada.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad.ToString("N2", CultureInfo.InvariantCulture);
                                                            decimal costos = 0;
                                                            if (entrada.AlmacenMovimiento.ListaAlmacenMovimientoCosto != null)
                                                            {
                                                                costos = entrada.AlmacenMovimiento.ListaAlmacenMovimientoCosto.Sum(registro => registro.Importe);
                                                            }
                                                            txtImporte.Text = (entrada.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe + costos).ToString("N2", CultureInfo.InvariantCulture);
                                                        }

                                                        skAyudaEntradaCompra.Info.Producto = new ProductoInfo
                                                        {
                                                            Familias = new List<FamiliaInfo> {
                                                    new FamiliaInfo{FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode()}
                                                },
                                                            SubFamilias = new List<SubFamiliaInfo> {
                                                    new SubFamiliaInfo{SubFamiliaID = SubFamiliasEnum.MicroIngredientes.GetHashCode()}
                                                }
                                                        };
                                                        skAyudaEntradaCompra.Info.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };
                                                        skAyudaEntradaCompra.Info.Activo = EstatusEnum.Activo;

                                                        TipoMovimientoPL tipoMovimientoPl = new TipoMovimientoPL();
                                                        entrada.AlmacenMovimiento.TipoMovimiento = tipoMovimientoPl.ObtenerPorID(entrada.AlmacenMovimiento.TipoMovimiento.TipoMovimientoID);
                                                        entradaProducto = entrada;
                                                    }
                                                    else
                                                    {
                                                        LimpiarTodoFolioEntradaCompra();
                                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                                    }
                                                }
                                                else
                                                {
                                                    LimpiarTodoFolioEntradaCompra();
                                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                                }
                                            }
                                            else
                                            {
                                                LimpiarTodoFolioEntradaCompra();
                                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                            }
                                        }
                                        else
                                        {
                                            LimpiarTodoFolioEntradaCompra();
                                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                        }
                                    }
                                    else
                                    {
                                        LimpiarTodoFolioEntradaCompra();
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                    }
                                }
                                else
                                {
                                    LimpiarTodoFolioEntradaCompra();
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                }
                            }
                            else
                            {
                                LimpiarTodoFolioEntradaCompra();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                        MessageImage.Stop);
                            }
                        }
                        else
                        {
                            LimpiarTodoFolioEntradaCompra();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CancelarMovimiento_FechaAntigua, MessageBoxButton.OK,
                            MessageImage.Stop);
                        }
                    }
                    else
                    {
                        LimpiarTodoFolioEntradaCompra();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                    MessageImage.Stop);
                    }
                }
                else
                {
                    LimpiarTodoFolioEntradaCompra();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                    MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    Properties.Resources.CancelarMovimiento_ErrorConsultaFolio, MessageBoxButton.OK,
                                                    MessageImage.Stop);
            }
        }

        #endregion

        #region Botones

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbMovimiento.SelectedIndex = -1;
                LimpiarTodoFolioEntradaCompra();
                LimpiarTodoFolioTraspaso();
                CargarAyudaFolioEntradaCompra();
                skAyudaEntradaCompra.IsEnabled = false;
                btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CancelarMovimiento;
                tbEntradaCompra.Visibility = Visibility.Visible;
                txtJustificacion.Visibility = Visibility.Visible;
                lblJustificacionRequerido.Visibility = Visibility.Visible;
                lblJustificacion.Visibility = Visibility.Visible;
                dgProductos.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void btnCancelarMovimiento_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbMovimiento.SelectedIndex >= 0)
                {
                    TipoCancelacionInfo tipoCancelacionSeleccionado = (TipoCancelacionInfo)cmbMovimiento.SelectedItem;

                    if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.EntradaCompra.GetHashCode())
                    {
                        //Funcionalidad entrada por compra
                        if (!string.IsNullOrEmpty(skAyudaEntradaCompra.Clave))
                        {
                            if (ValidarJustificacion())
                            {
                                if (entradaProducto.Fecha >= fecha.FechaActual.AddDays(-1 * tipoCancelacionSeleccionado.DiasPermitidos))
                                {
                                    if (SkMessageBox.Show(
                                       Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                       string.Format(Properties.Resources.CancelarMovimiento_PreguntaCancelaFolio,entradaProducto.AlmacenMovimiento.TipoMovimiento.Descripcion,entradaProducto.Folio),
                                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                                    {
                                        CancelarEntradaCompra();
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CancelarMovimiento_FechaAntigua, MessageBoxButton.OK,
                                    MessageImage.Stop);
                                }
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra, MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                    else if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.EntradaTraspaso.GetHashCode())
                    {
                        //Funcionalidad entrada por traspaso
                        //Funcionalidad entrada por compra
                        if (!string.IsNullOrEmpty(skAyudaEntradaCompra.Clave))
                        {
                            if (ValidarJustificacion())
                            {
                                if (entradaProducto.Fecha >= fecha.FechaActual.AddDays(-1 * tipoCancelacionSeleccionado.DiasPermitidos))
                                {
                                    if (SkMessageBox.Show(
                                       Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                       string.Format(Properties.Resources.CancelarMovimiento_PreguntaCancelaFolio, entradaProducto.AlmacenMovimiento.TipoMovimiento.Descripcion, entradaProducto.Folio),
                                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                                    {
                                        CancelarEntradaTraspaso();
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CancelarMovimiento_FechaAntigua, MessageBoxButton.OK,
                                    MessageImage.Stop);
                                }
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra, MessageBoxButton.OK,
                                MessageImage.Stop);
                        }

                    }
                    else if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.VentaTraspaso.GetHashCode())
                    {
                        //Funcionalidad venta y traspaso
                        if (!string.IsNullOrEmpty(skAyudaSalidaProducto.Clave))
                        {
                            if (ValidarJustificacion())
                            {
                                if (salidaProducto.FechaSalida >= fecha.FechaActual.AddDays(-1 * tipoCancelacionSeleccionado.DiasPermitidos))
                                {
                                    if (SkMessageBox.Show(
                                       Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                       string.Format(Properties.Resources.CancelarMovimiento_PreguntaCancelaFolio, salidaProducto.TipoMovimiento.Descripcion, salidaProducto.FolioSalida),
                                       MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                                    {
                                        CancelarVentaTraspaso();
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CancelarMovimiento_FechaAntigua, MessageBoxButton.OK,
                                    MessageImage.Stop);
                                }
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra, MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                    else if (tipoCancelacionSeleccionado.TipoCancelacionId == TipoCancelacionEnum.Pedido.GetHashCode())
                    {
                        //Funcionalidad pedido
                        if (!string.IsNullOrEmpty(skAyudaPedidos.Clave))
                        {
                            if (pedido.FechaPedido >= fecha.FechaActual.AddDays(-1 * tipoCancelacionSeleccionado.DiasPermitidos))
                            {
                                CerrarTicket();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.CancelacionMovimiento_FechaPedidoAntigua, MessageBoxButton.OK,
                                    MessageImage.Stop);
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CancelarMovimiento_SeleccionarEntradaCompra, MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CancelarMovimiento_SeleccionarTipoMovimiento, MessageBoxButton.OK,
                                MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void CerrarTicket()
        {
            try
            {
                CancelacionMovimientoJustificacion cancelacionJustificacion = new CancelacionMovimientoJustificacion();
                MostrarCentrado(cancelacionJustificacion);
                if(!string.IsNullOrWhiteSpace(cancelacionJustificacion.justificacion)){
                    PedidosPL pedidosPl = new PedidosPL();
                    pedido.EstatusPedido = new EstatusInfo() { EstatusId = Estatus.Recibido.GetHashCode()};
                    pedido.UsuarioModificacion = new UsuarioInfo() { UsuarioID = usuarioId};
                    pedido.Observaciones = cancelacionJustificacion.justificacion;
                    pedidosPl.ActualizarEstatusPedido(pedido);

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.CancelacionMovimiento_PedidoCerradoConExito, MessageBoxButton.OK,
                           MessageImage.Correct);
                    InicializarDatos();
                    txtJustificacion.Clear();
                    cmbMovimiento.SelectedIndex = 1;
                    cmbMovimiento.SelectedIndex = -1;
                }
            }catch(Exception ex){
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                       MessageImage.Stop);
            }
        }

        private void CancelarVentaTraspaso()
        {
            try
            {
                if (salidaProducto.AlmacenInventarioLote.Cantidad >= (salidaProducto.PesoBruto - salidaProducto.PesoTara))
                {
                    CancelacionMovimientoPL cancelacionMovimientoPl = new CancelacionMovimientoPL();
                    salidaProducto.UsuarioCreacionId = usuarioId;
                    if (cancelacionMovimientoPl.CancelarVentaTraspaso(salidaProducto,txtJustificacion.Text))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.CancelarMovimiento_GuardoExito, MessageBoxButton.OK,
                           MessageImage.Correct);
                        InicializarDatos();
                        txtJustificacion.Clear();
                        cmbMovimiento.SelectedIndex = -1;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_SinExistencia, MessageBoxButton.OK,
                        MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                       MessageImage.Stop);
            }
        }

        

        private bool ValidarJustificacion()
        {
            if (txtJustificacion.Text != "")
            {
                return true;
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CancelarMovimiento_CapturarJustificacion, MessageBoxButton.OK,
                MessageImage.Stop);
                return false;
            }
        }

        private void CancelarEntradaCompra()
        {
            try
            {
                if (entradaProducto.AlmacenInventarioLote.Cantidad >= entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad)
                {
                    CancelacionMovimientoPL cancelacionMovimientoPl = new CancelacionMovimientoPL();
                    entradaProducto.UsuarioCreacionID = usuarioId;
                    if (cancelacionMovimientoPl.CancelarEntradaCompra(entradaProducto, txtJustificacion.Text))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_GuardoExito, MessageBoxButton.OK,
                        MessageImage.Correct);
                        LimpiarTodoFolioEntradaCompra();
                        skAyudaEntradaCompra.IsEnabled = false;
                        txtJustificacion.Clear();
                        cmbMovimiento.SelectedIndex = -1;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CancelarMovimiento_SinExistencia, MessageBoxButton.OK,
                    MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                       MessageImage.Stop);
            }
        }

        private void CancelarEntradaTraspaso()
        {
            try
            {
                if (entradaProducto.AlmacenInventarioLote.Cantidad >= entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad)
                {
                    CancelacionMovimientoPL cancelacionMovimientoPl = new CancelacionMovimientoPL();
                    entradaProducto.UsuarioCreacionID = usuarioId;
                    if (cancelacionMovimientoPl.CancelarEntradaTraspaso(entradaProducto, txtJustificacion.Text))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_GuardoExito, MessageBoxButton.OK,
                        MessageImage.Correct);
                        LimpiarTodoFolioTraspaso();
                        txtJustificacion.Clear();
                        cmbMovimiento.SelectedIndex = -1;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CancelarMovimiento_SinExistencia, MessageBoxButton.OK,
                    MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                       MessageImage.Stop);
            }
        }

        #endregion

        private void ControlBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            FechaPL fechaPl = new FechaPL();
            fecha = fechaPl.ObtenerFechaActual();
            txtFecha.Text = fecha.FechaActual.ToShortDateString().ToString(CultureInfo.InvariantCulture);
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            LlenaComboTipoCancelacion();
            CargarAyudaFolioEntradaCompra();
            skAyudaEntradaCompra.IsEnabled = false;
            btnCancelarMovimiento.Content = Properties.Resources.CancelarMovimiento_Lbl_CancelarMovimiento;
            txtJustificacion.Visibility = Visibility.Visible;
            lblJustificacionRequerido.Visibility = Visibility.Visible;
            lblJustificacion.Visibility = Visibility.Visible;
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var check = (CheckBox)sender;

                if (check.IsChecked == true)
                {
                    var pedidoCancelar = (PedidoCancelacionMovimientosInfo)check.CommandParameter;
                    if (pedidoCancelar.AlmacenInventarioLoteDestino != null)
                    {
                        if (pedidoCancelar.AlmacenInventarioLoteDestino.Cantidad < pedidoCancelar.CantidadEntregada)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.CancelarMovimiento_SinExistencia, MessageBoxButton.OK,
                                                MessageImage.Stop);
                            return;
                        }
                    }

                    pedidoCancelar.CancelarProgramacion = false;
                    if (listaPedidos.Where(registro => registro.ProgramacionMateriaPrimaId == pedidoCancelar.ProgramacionMateriaPrimaId).ToList().Count == 1)
                    {
                        pedidoCancelar.CancelarProgramacion = true;
                    }

                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          string.Format(Properties.Resources.CancelacionMovimiento_PreguntaCancelaTicket, pedidoCancelar.FolioPedido),
                                          MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                    {
                        CancelacionMovimientoJustificacion cancelacionJustificacion = new CancelacionMovimientoJustificacion();
                        MostrarCentrado(cancelacionJustificacion);
                        if (!string.IsNullOrWhiteSpace(cancelacionJustificacion.justificacion))
                        {
                            CancelacionMovimientoPL cancelacionMovimientoPl = new CancelacionMovimientoPL();
                            pedidoCancelar.UsuarioID = usuarioId;
                            pedidoCancelar.OrganizacionId = organizacionId;
                            if (cancelacionMovimientoPl.CancelarPedidoTicket(pedidoCancelar, cancelacionJustificacion.justificacion))
                            {
                                if (pedidoCancelar.Ticket == 0)
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.CancelarMovimiento_GuardoExito, MessageBoxButton.OK,
                                                        MessageImage.Correct);
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        string.Format(Properties.Resources.CancelacionMovimiento_GuardoTicketExito,pedidoCancelar.Ticket,pedidoCancelar.Producto.Descripcion), MessageBoxButton.OK,
                                                        MessageImage.Correct);
                                }
                                InicializarDatos();
                                txtJustificacion.Clear();
                                cmbMovimiento.SelectedIndex = -1;
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                           MessageImage.Stop);
                            }
                        }
                        else
                        {
                            foreach(PedidoCancelacionMovimientosInfo pedidoCancelacion in listaPedidos){
                                pedidoCancelacion.Seleccionado = false;
                            }
                            dgProductos.ItemsSource = new List<PedidoCancelacionMovimientosInfo>();
                            dgProductos.ItemsSource = listaPedidos;
                        }
                    }
                    else
                    {
                        foreach (PedidoCancelacionMovimientosInfo pedidoCancelacion in listaPedidos)
                        {
                            pedidoCancelacion.Seleccionado = false;
                        }
                        dgProductos.ItemsSource = new List<PedidoCancelacionMovimientosInfo>();
                        dgProductos.ItemsSource = listaPedidos;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CancelarMovimiento_ErrorGuardar, MessageBoxButton.OK,
                       MessageImage.Stop);
            }
        }
    }
}
