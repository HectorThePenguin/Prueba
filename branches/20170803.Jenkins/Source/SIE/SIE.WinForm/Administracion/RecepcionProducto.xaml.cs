using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using System.Windows.Controls;
using System.IO;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Base.Exepciones;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecepcionProducto.xaml
    /// </summary>
    public partial class RecepcionProducto
    {
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        private SKAyuda<ProductoInfo> skAyudaProducto;
        private RecepcionProductoInfo recepcionProducto;
        private RecepcionProductoInfo recepcionProductoCompra;
        private int organizacionId,usuarioId,contador,renglon;
        private string servidor, baseDatos;

        private IList<TipoCambioInfo> tiposCambio;
        private TipoCambioInfo tipoCambio;

        public RecepcionProducto()
        {
            InitializeComponent();
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioId = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
            ConsultaAlmacenGeneral();
            AgregarAyudaProveedor();
            AgregarAyudaProducto();
            CargarDatos();
            recepcionProducto = new RecepcionProductoInfo();
            recepcionProducto.ListaRecepcionProductoDetalle = new List<RecepcionProductoDetalleInfo>();
            contador = 0;
            renglon = -1;
        }

        /// <summary>
        /// Consulta el almacen general de la organizacion del usuario logueado
        /// </summary>
        private void ConsultaAlmacenGeneral()
        {
            try
            {
                var almacenPl = new AlmacenPL();
                var listaAlmacen = almacenPl.ObtenerAlmacenPorTiposAlmacen(new List<TipoAlmacenEnum>()
                {
                    TipoAlmacenEnum.GeneralGanadera
                }, new OrganizacionInfo()
                {
                    OrganizacionID = organizacionId
                });

                if (listaAlmacen != null && listaAlmacen.Count > 0 )
                {
                    txtAlmacenGeneral.Text = listaAlmacen[0].AlmacenID.ToString();
                    txtDescripcionAlmacen.Text = listaAlmacen[0].Descripcion;
                }
                else
                {
                    BloquearPantalla();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void CargarDatos()
        {
            try
            {
                ConfiguracionParametrosPL configuracionParametrosPl = new ConfiguracionParametrosPL();
                ConfiguracionParametrosInfo configuracion = new ConfiguracionParametrosInfo();
                configuracion.OrganizacionID = organizacionId;
                configuracion.TipoParametro = (int)TiposParametrosEnum.InterfazComprasWEB;
                configuracion.Clave = string.Format("ServidorBD");
                configuracion = configuracionParametrosPl.ObtenerPorOrganizacionTipoParametroClave(configuracion);
                servidor = configuracion.Valor;

                configuracion = new ConfiguracionParametrosInfo();
                configuracion.OrganizacionID = organizacionId;
                configuracion.TipoParametro = (int)TiposParametrosEnum.InterfazComprasWEB;
                configuracion.Clave = string.Format("NombreBD");
                configuracion = configuracionParametrosPl.ObtenerPorOrganizacionTipoParametroClave(configuracion);
                baseDatos = configuracion.Valor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BloquearPantalla()
        {
            txtOrdenCompra.IsEnabled = false;
            btnBuscar.IsEnabled = false;
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RecepcionProducto_ValidacionAlmacenGeneral,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
        }

        private void AgregarAyudaProveedor()
        {
            try
            {
                skAyudaProveedor = new SKAyuda<ProveedorInfo>(200,
                    false,
                    new ProveedorInfo()
                    {
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadCodigoSapRecepcionProducto",
                    "PropiedadDescripcionRecepcionProducto",
                    true,
                    80,10,
                    true)
                {
                    AyudaPL = new ProveedorPL(),
                    MensajeClaveInexistente = Properties.Resources.RecepcionProducto_ProveedorNoExiste,
                    MensajeBusquedaCerrar = Properties.Resources.RecepcionProducto_ProveedorSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.RecepcionProducto_ProveedorBusqueda,
                    MensajeAgregar = Properties.Resources.RecepcionProducto_ProveedorSeleccionar,
                    TituloEtiqueta = Properties.Resources.RecepcionProducto_ProveedorLeyendaBusqueda,
                    TituloPantalla = Properties.Resources.RecepcionProducto_ProveedorTituloBusqueda
                };

                skAyudaProveedor.ObtenerDatos += ObtenerDatosProveedor;
                skAyudaProveedor.LlamadaMetodosNoExistenDatos += LimpiarTodoProveedor;
                
                skAyudaProveedor.AsignaTabIndex(2);
                splAyudaProveedor.Children.Clear();
                splAyudaProveedor.Children.Add(skAyudaProveedor);
                skAyudaProveedor.AsignarFoco();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarTodoProveedor()
        {
            skAyudaProveedor.LimpiarCampos();
        }

        private void ObtenerDatosProveedor(string filtro)
        {
            skAyudaProveedor.Info = new ProveedorInfo()
            {
                Activo = EstatusEnum.Activo
            };
        }

        private void AgregarAyudaProducto()
        {
            try
            {
                skAyudaProducto = new SKAyuda<ProductoInfo>(173,
                    false,
                    new ProductoInfo() 
                    {
                        FiltroFamilia = (int)FamiliasEnum.Medicamento + "|" + (int)FamiliasEnum.MaterialEmpaque,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadClaveRecepcionProducto",
                    "PropiedadDescripcionRecepcionProducto",
                    true,
                    30,
                    true)
                {
                    AyudaPL = new ProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.RecepcionProducto_ProductoNoExiste,
                    MensajeBusquedaCerrar = Properties.Resources.RecepcionProducto_ProductoSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.RecepcionProducto_ProductoBusqueda,
                    MensajeAgregar = Properties.Resources.RecepcionProducto_ProductoSeleccionar,
                    TituloEtiqueta = Properties.Resources.RecepcionProducto_ProductoLeyendaBusqueda,
                    TituloPantalla = Properties.Resources.RecepcionProducto_ProductoTituloBusqueda
                };

                skAyudaProducto.ObtenerDatos += ObtenerDatosProducto;
                skAyudaProducto.LlamadaMetodosNoExistenDatos += LimpiarTodoProductos;
                
                skAyudaProducto.AsignaTabIndex(3);
                splAyudaProducto.Children.Clear();
                splAyudaProducto.Children.Add(skAyudaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarTodoProductos()
        {
            skAyudaProducto.LimpiarCampos();
            txtUnidad.Clear();
        }

        private void ObtenerDatosProducto(string filtro)
        {
            try
            {
                var productoPl = new ProductoPL();
                ProductoInfo producto = productoPl.ObtenerPorID(new ProductoInfo(){ProductoId = Convert.ToInt32(filtro)});

                if (producto != null)
                {
                    if (producto.Familia.FamiliaID == (int) FamiliasEnum.MaterialEmpaque ||
                        producto.Familia.FamiliaID == (int) FamiliasEnum.Medicamento)
                    {
                        txtUnidad.Text = producto.DescripcionUnidad;
                        skAyudaProducto.Info = new ProductoInfo()
                        {
                            FiltroFamilia = (int) FamiliasEnum.Medicamento + "|" + (int) FamiliasEnum.MaterialEmpaque,
                            Activo = EstatusEnum.Activo
                        };
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RecepcionProducto_ProductoFamiliaInvalida,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                        LimpiarTodoProductos();
                        skAyudaProducto.AsignarFoco();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            if (contador == 0)
            {
                skAyudaProveedor.LimpiarCampos();
                txtFactura.Clear();
            }

            skAyudaProducto.LimpiarCampos();
            txtCantidad.Text = "";
            txtImporte.Text = "";
            txtUnidad.Clear();
            renglon = -1;
            btnAgregar.Content = Properties.Resources.RecepcionProducto_btnAgregar;
        }

        private void LimpiarTodo()
        {
            txtOrdenCompra.Text = "";
            dgRecepcionCompra.ItemsSource = new List<RecepcionProductoDetalleInfo>();
            dgRecepcion.ItemsSource = new List<RecepcionProductoDetalleInfo>();
            txtProveedor.Clear();
            txtFecha.Clear();
            txtObservaciones.Clear();
            contador = 0;
            skAyudaProveedor.IsEnabled = true;
            txtFactura.IsEnabled = true;

            recepcionProducto = new RecepcionProductoInfo();
            recepcionProducto.ListaRecepcionProductoDetalle = new List<RecepcionProductoDetalleInfo>();
            recepcionProductoCompra = new RecepcionProductoInfo();
            recepcionProductoCompra.ListaRecepcionProductoDetalle = new List<RecepcionProductoDetalleInfo>();

            gbDatos.IsEnabled = false;

            LimpiarCampos();

            txtOrdenCompra.Focus();
            btnGuardar.IsEnabled = false;
            chkDll.IsEnabled = true;
            chkDll.IsChecked = false;
        }

        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            if (skAyudaProveedor.Clave != "")
            {
                if (txtFactura.Text != "")
                {
                    if (skAyudaProducto.Clave != "")
                    {
                        if (txtCantidad.Value != null)
                        {
                            if (txtCantidad.Value != 0)
                            {
                                if (txtImporte.Value != null)
                                {
                                    if (txtImporte.Value != 0)
                                    {
                                        if (renglon < 0)
                                        {
                                            skAyudaProveedor.IsEnabled = false;
                                            txtFactura.IsEnabled = false;
                                            contador++;

                                            var recepcionProductoDetalle = new RecepcionProductoDetalleInfo
                                            {
                                                Producto =
                                                    new ProductoInfo() {ProductoId = int.Parse(skAyudaProducto.Clave)}
                                            };
                                            var productoPl = new ProductoPL();
                                            recepcionProductoDetalle.Producto =
                                                productoPl.ObtenerPorID(recepcionProductoDetalle.Producto);
                                            recepcionProductoDetalle.Cantidad = (decimal) txtCantidad.Value;

                                            decimal importe = ObtenerImporte();
                                            if (importe == 0)
                                            {
                                                MensajeTipoCambio();
                                                return;
                                            }

                                            recepcionProductoDetalle.Importe = importe;
                                            recepcionProductoDetalle.PrecioPromedio = recepcionProductoDetalle.Importe/
                                                                                      recepcionProductoDetalle.Cantidad;
                                            recepcionProducto.ListaRecepcionProductoDetalle.Add(recepcionProductoDetalle);
                                            dgRecepcion.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                                            dgRecepcion.ItemsSource = recepcionProducto.ListaRecepcionProductoDetalle;
                                            btnGuardar.IsEnabled = true;
                                        }
                                        else
                                        {
                                            var recepcionProductoDetalle = new RecepcionProductoDetalleInfo
                                            {
                                                Producto =
                                                    new ProductoInfo() {ProductoId = int.Parse(skAyudaProducto.Clave)}
                                            };
                                            var productoPl = new ProductoPL();
                                            recepcionProductoDetalle.Producto =
                                                productoPl.ObtenerPorID(recepcionProductoDetalle.Producto);
                                            recepcionProductoDetalle.Cantidad = (decimal) txtCantidad.Value;

                                            decimal importe = ObtenerImporte();
                                            if (importe == 0)
                                            {
                                                MensajeTipoCambio();
                                                return;
                                            }
                                            recepcionProductoDetalle.Importe = importe;
                                            recepcionProductoDetalle.PrecioPromedio = recepcionProductoDetalle.Importe/
                                                                                      recepcionProductoDetalle.Cantidad;
                                            recepcionProducto.ListaRecepcionProductoDetalle[renglon] =
                                                recepcionProductoDetalle;
                                            dgRecepcion.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                                            dgRecepcion.ItemsSource = recepcionProducto.ListaRecepcionProductoDetalle;
                                        }
                                        chkDll.IsEnabled = false;
                                        LimpiarCampos();
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecepcionProducto_IngreseImporte,
                                            MessageBoxButton.OK,
                                            MessageImage.Stop);
                                        txtImporte.Focus();
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.RecepcionProducto_IngreseImporte,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                    txtImporte.Focus();
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.RecepcionProducto_IngreseCantidad,
                               MessageBoxButton.OK,
                               MessageImage.Stop);
                                txtCantidad.Focus();
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.RecepcionProducto_IngreseCantidad,
                               MessageBoxButton.OK,
                               MessageImage.Stop);
                            txtCantidad.Focus();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RecepcionProducto_IngreseProducto,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                        skAyudaProducto.AsignarFoco();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RecepcionProducto_IngresarFactura,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                    txtFactura.Focus();
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.RecepcionProducto_IngresarProveedor,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                skAyudaProveedor.AsignarFoco();
            }
        }

        private void MensajeTipoCambio()
        {
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.RecepcionProducto_TipoCambioNoConfigurado,
                              MessageBoxButton.OK,
                              MessageImage.Stop);
        }

        private decimal ObtenerImporte()
        {
            decimal importe;
            decimal.TryParse(txtImporte.Text, out importe);
            if (chkDll.IsChecked.Value)
            {
                if (tipoCambio == null)
                {
                    importe = 0;
                }
                else
                {
                    importe *= tipoCambio.Cambio;
                }
            }
            return importe;
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(
                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.RecepcionProducto_MsgCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarTodo();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var recepcion = (RecepcionProductoDetalleInfo) botonEditar.CommandParameter;
                skAyudaProducto.Clave = recepcion.Producto.ProductoId.ToString();
                skAyudaProducto.Descripcion = recepcion.Producto.ProductoDescripcion;
                txtUnidad.Text = recepcion.Producto.DescripcionUnidad;
                txtCantidad.Value = recepcion.Cantidad;
                txtImporte.Value = recepcion.Importe;
                btnAgregar.Content = Properties.Resources.RecepcionProducto_btnActualizar;

                renglon = dgRecepcion.SelectedIndex;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOrdenCompra.Text))
                {
                    if (recepcionProducto.ListaRecepcionProductoDetalle != null)
                    {
                        if (recepcionProducto.ListaRecepcionProductoDetalle.Count > 0)
                        {
                                    var recepcionProductoPl = new RecepcionProductoPL();
                                    recepcionProducto.Almacen = new AlmacenInfo()
                                    {
                                        AlmacenID = int.Parse(txtAlmacenGeneral.Text),
                                        Organizacion = new OrganizacionInfo() {OrganizacionID = organizacionId}
                                    };
                                    recepcionProducto.FolioOrdenCompra = txtOrdenCompra.Text;
                                    recepcionProducto.FechaSolicitud = recepcionProductoCompra.FechaSolicitud;
                                    var proveedorPl = new ProveedorPL();
                                    recepcionProducto.Proveedor =
                                        proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo()
                                        {
                                            CodigoSAP = skAyudaProveedor.Clave
                                        });
                                    recepcionProducto.Factura = txtFactura.Text;
                                    recepcionProducto.UsuarioCreacion = new UsuarioInfo() {UsuarioID = usuarioId};
                                    recepcionProducto.Observaciones = txtObservaciones.Text;
                                    MemoryStream retorno = recepcionProductoPl.Guardar(recepcionProducto);
                                    if (retorno != null)
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecepcionProducto_msgDatosGuardados,
                                            MessageBoxButton.OK,
                                            MessageImage.Correct);

                                        var exportarPoliza = new ExportarPoliza();
                                        exportarPoliza.ImprimirPoliza(retorno,
                                                                      string.Format("{0} {1}",
                                                                                    "Poliza de Entrada Compra Materia Prima Folio",
                                                                                    txtOrdenCompra.Text));
                                        LimpiarTodo();
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecepcionProducto_msgOcurrioErrorGuardar,
                                            MessageBoxButton.OK,
                                            MessageImage.Stop);
                                    }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RecepcionProducto_msgIngresarProductos,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RecepcionProducto_msgIngresarProductos,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RecepcionProducto_IngresarFolioOrdenCompra,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    txtOrdenCompra.Focus();
                }
            }
            catch (ExcepcionServicio ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void Buscar()
        {
            if (txtOrdenCompra.Text != "")
            {
                try
                {
                    if (!string.IsNullOrEmpty(servidor) && !string.IsNullOrEmpty(baseDatos))
                    {
                        string conexion =
                            string.Format(
                                @"Initial Catalog={0};Data Source={1};User ID=usrsoporte;Password=usrsoporte", baseDatos,
                                servidor);

                        var recepcionProductoPl = new RecepcionProductoPL();
                        recepcionProductoCompra = new RecepcionProductoInfo();
                        recepcionProductoCompra.FolioOrdenCompra = txtOrdenCompra.Text;

                        recepcionProductoCompra = recepcionProductoPl.ObtenerRecepcionVista(recepcionProductoCompra,
                            organizacionId, conexion);

                        if (recepcionProductoCompra != null)
                        {
                                txtProveedor.Text = recepcionProductoCompra.Proveedor.Descripcion;
                                txtFecha.Text =
                                    recepcionProductoCompra.FechaSolicitud.ToShortDateString()
                                        .ToString(CultureInfo.InvariantCulture);
                                gbDatos.IsEnabled = true;
                                dgRecepcionCompra.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                                dgRecepcionCompra.ItemsSource = recepcionProductoCompra.ListaRecepcionProductoDetalle;
                        }
                        else
                        {
                            gbDatos.IsEnabled = false;
                            renglon = -1;
                            LimpiarCampos();
                            recepcionProducto = new RecepcionProductoInfo();
                            recepcionProducto.ListaRecepcionProductoDetalle = new List<RecepcionProductoDetalleInfo>();
                            recepcionProductoCompra = new RecepcionProductoInfo();
                            recepcionProductoCompra.ListaRecepcionProductoDetalle =
                                new List<RecepcionProductoDetalleInfo>();

                            dgRecepcionCompra.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                            dgRecepcion.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RecepcionProducto_CompraInvalida,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            txtOrdenCompra.Focus();
                        }
                    }
                    else
                    {
                        renglon = -1;
                        LimpiarCampos();
                        recepcionProducto = new RecepcionProductoInfo();
                        recepcionProducto.ListaRecepcionProductoDetalle = new List<RecepcionProductoDetalleInfo>();
                        recepcionProductoCompra = new RecepcionProductoInfo();
                        recepcionProductoCompra.ListaRecepcionProductoDetalle =
                            new List<RecepcionProductoDetalleInfo>();

                        dgRecepcionCompra.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                        dgRecepcion.ItemsSource = new List<RecepcionProductoDetalleInfo>();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.RecepcionProducto_msgErrorAlObtenerParametros,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                        txtOrdenCompra.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RecepcionProducto_IngresarFolioOrdenCompra,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                txtOrdenCompra.Focus();
            }
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void TxtOrdenCompra_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuion(e.Text);
        }

        private void TxtFactura_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuion(e.Text);
        }

        private void TxtOrdenCompra_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                txtProveedor.Clear();
                txtFecha.Clear();
                dgRecepcionCompra.ItemsSource = null;
            }
        }

        private void TxtOrdenCompra_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Buscar();
            }
        }

        private void ButtonBaseEliminar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgRecepcion.SelectedIndex > -1)
                {
                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        string.Format(Properties.Resources.SolicitudMateriaPrima_MsgEliminarProducto,
                            recepcionProducto.ListaRecepcionProductoDetalle[dgRecepcion.SelectedIndex].Producto.ProductoDescripcion),
                        MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                    {
                        recepcionProducto.ListaRecepcionProductoDetalle.RemoveAt(dgRecepcion.SelectedIndex);
                        dgRecepcion.ItemsSource = new List<PedidoDetalleInfo>();
                        dgRecepcion.ItemsSource = recepcionProducto.ListaRecepcionProductoDetalle;
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void RecepcionProductoLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var tipoCambioPL = new TipoCambioPL();
                tiposCambio = tipoCambioPL.ObtenerTodos();

                string fecha = DateTime.Now.ToShortDateString();
                tipoCambio =
                    tiposCambio.OrderByDescending(tipo => tipo.TipoCambioId).FirstOrDefault(
                        tipo =>
                        tipo.Descripcion.Equals(Moneda.DOLAR.ToString()) &&
                        tipo.Fecha.ToShortDateString().Equals(fecha));
            }
            catch (Exception)
            {                
                tiposCambio = new List<TipoCambioInfo>();
            }
        }
    }
}
