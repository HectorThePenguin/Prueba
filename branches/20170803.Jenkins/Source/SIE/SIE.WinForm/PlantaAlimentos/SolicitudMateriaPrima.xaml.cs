using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
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


namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para SolicitudMateriaPrima.xaml
    /// </summary>
    public partial class SolicitudMateriaPrima
    {
        #region Atributos

        private PedidoInfo pedidoInfo = new PedidoInfo();
        private SKAyuda<PedidoInfo> skAyudaFolioPedido;
        private SKAyuda<ProductoInfo> skAyudaProducto;
        private SKAyuda<AlmacenInventarioLoteInfo> skAyudaAlmacenInventarioLote; 
        private int organizacionId;
        private int usuarioId;
        private int almacenId;
        private int renglonActualizado = -1;
        private bool permiteModificar = true;

        public SolicitudMateriaPrima()
        {
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            pedidoInfo.DetallePedido = new List<PedidoDetalleInfo>();
            InitializeComponent();
            CargarAyudas();
            CargarAlmacen();
            skAyudaAlmacenInventarioLote.IsEnabled = false;
            btnAgregarLote.IsEnabled = false;
        }

        private void CargarAlmacen()
        {
            try
            {
                var almacenPl = new AlmacenPL();
                List<AlmacenInfo> listaAlmacen = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionId).ToList();
                if (listaAlmacen.Count > 0)
                {
                    var almacen = listaAlmacen.FirstOrDefault(
                        registro => registro.TipoAlmacen.TipoAlmacenID == (int) TipoAlmacenEnum.PlantaDeAlimentos);
                    if (almacen != null)
                        almacenId = almacen.AlmacenID;
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SolicitudMateriaPrima_MsgSinAlmacen,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                        skAyudaFolioPedido.IsEnabled = false;
                        skAyudaProducto.IsEnabled = false;
                        txtObservaciones.IsEnabled = false;
                        txtCantidadSolicitada.IsEnabled = false;
                        btnAgregar.IsEnabled = false;
                        btnLimpiar.IsEnabled = false;
                        BtnGuardar.IsEnabled = false;
                        BtnCancelar.IsEnabled = false;
                    }

                     almacen = listaAlmacen.FirstOrDefault(
                        registro => registro.TipoAlmacen.TipoAlmacenID == (int) TipoAlmacenEnum.MateriasPrimas);
                    if (almacen == null){
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SolicitudMateriaPrima_MsgSinAlmacenMateriaPrima,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                        skAyudaFolioPedido.IsEnabled = false;
                        skAyudaProducto.IsEnabled = false;
                        txtObservaciones.IsEnabled = false;
                        txtCantidadSolicitada.IsEnabled = false;
                        btnAgregarLote.IsEnabled = false;
                        btnAgregar.IsEnabled = false;
                        btnLimpiar.IsEnabled = false;
                        BtnGuardar.IsEnabled = false;
                        BtnCancelar.IsEnabled = false;
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SolicitudMateriaPrima_MsgSinAlmacen,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    skAyudaFolioPedido.IsEnabled = false;
                    skAyudaProducto.IsEnabled = false;
                    txtObservaciones.IsEnabled = false;
                    txtCantidadSolicitada.IsEnabled = false;
                    skAyudaAlmacenInventarioLote.IsEnabled = false;
                    btnAgregarLote.IsEnabled = false;
                    btnAgregar.IsEnabled = false;
                    btnLimpiar.IsEnabled = false;
                    BtnGuardar.IsEnabled = false;
                    BtnCancelar.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        #region Metodos

        private void CargarAyudas()
        {
            AgregarAyudaFolio();
            AgregarAyudaProducto();
            AgregarAyudaLote();
            skAyudaFolioPedido.AsignarFoco();
        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de folio
        /// </summary>
        private void AgregarAyudaFolio()
        {
            try
            {
                skAyudaFolioPedido = new SKAyuda<PedidoInfo>(0,
                    false,
                    new PedidoInfo() 
                    {
                        FolioPedido = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        ListaEstatusPedido = new List<EstatusInfo>()
                        {
                            new EstatusInfo(){EstatusId = (int)Estatus.PedidoSolicitado},
                            new EstatusInfo(){EstatusId = (int)Estatus.PedidoProgramado}
                        },
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadFolioSolicitud",
                    "PropiedadDescripcionOrganizacionSolicitud",
                    true,
                    250,
                    true)
                {
                    AyudaPL = new PedidosPL(),
                    MensajeClaveInexistente = Properties.Resources.EntradaMateriaPrima_AyudaFolioInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.EntradaMateriaPrima_Busqueda,
                    MensajeAgregar = Properties.Resources.EntradaMateriaPrima_Seleccionar,
                    TituloEtiqueta = Properties.Resources.SolicitudMateriaPrima_LeyendaBusqueda,
                    TituloPantalla = Properties.Resources.SolicitudMateriaPrima_Busqueda_Titulo,
                    MetodoPorDescripcion = "ObtenerPedidosPorFiltro"

                };

                skAyudaFolioPedido.ObtenerDatos += ObtenerDatosFolio;
                skAyudaFolioPedido.LlamadaMetodosNoExistenDatos += LimpiarTodoFolio;

                skAyudaFolioPedido.AsignaTabIndex(0);
                splAyudaFolioPedido.Children.Clear();
                splAyudaFolioPedido.Children.Add(skAyudaFolioPedido);
                skAyudaFolioPedido.TabIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarTodoFolio()
        {
            try
            {
                HabilitarCampos(1,true);
                HabilitarCampos(2, true);
                skAyudaFolioPedido.LimpiarCampos();
                LimpiarDatos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de folio
        /// </summary>
        private void AgregarAyudaProducto()
        {
            try
            {
                skAyudaProducto = new SKAyuda<ProductoInfo>(200,
                    false,
                    new ProductoInfo()
                    {
                        ProductoId = 0,
                        Familia = new FamiliaInfo(){FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                        Familias = new List<FamiliaInfo>()
                        {
                            new FamiliaInfo(){FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                            new FamiliaInfo(){FamiliaID = (int) FamiliasEnum.Premezclas}
                        },
                        Activo = EstatusEnum.Activo
                        
                    },
                    "PropiedadProductoSolicitudIDProgramacion",
                    "PropiedadProductoSolicitudDescripcion",
                    true,
                    50,
                    true)
                {
                    AyudaPL = new ProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.SolicitudMateriaPrimaProducto_MsgProductoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.SolicitudMateriaPrimaProducto_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.SolicitudMateriaPrimaProducto_Busqueda,
                    MensajeAgregar = Properties.Resources.SolicitudMateriaPrimaProducto_Seleccionar,
                    TituloEtiqueta = Properties.Resources.SolicitudMateriaPrimaProducto_LeyandaBuscar,
                    TituloPantalla = Properties.Resources.SolicitudMateriaPrimaProducto_Busqueda_Titulo,
                    MetodoPorDescripcion = "ObtenerPorFamiliaPaginado"

                };

                skAyudaProducto.ObtenerDatos += ObtenerDatosProducto;
                skAyudaFolioPedido.LlamadaMetodosNoExistenDatos += LimpiarTodoProductos;

                skAyudaProducto.AsignaTabIndex(1);
                splAyudaProducto.Children.Clear();
                splAyudaProducto.Children.Add(skAyudaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// Agrega la ayuda para la busqueda de folio
        /// </summary>
        private void AgregarAyudaLote()
        {
            try
            {
                int productoId = 0;
                if (skAyudaProducto.Clave != "")
                {
                    productoId = Convert.ToInt32(skAyudaProducto.Clave);
                }
                skAyudaAlmacenInventarioLote = new SKAyuda<AlmacenInventarioLoteInfo>(0,
                    false,
                    new AlmacenInventarioLoteInfo()
                    {
                        ProductoId = productoId,
                        OrganizacionId = organizacionId,
                        TipoAlmacenId = (int)TipoAlmacenEnum.PlantaDeAlimentos,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadLote",
                    "PropiedadCantidad",
                    true,
                    70,
                    true)
                {
                    AyudaPL = new AlmacenInventarioLotePL(),
                    MensajeClaveInexistente = Properties.Resources.SolicitudMateriaPrima_LoteInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.SolicitudMateriaPrima_LoteSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.SolicitudMateriaPrimaProducto_Busqueda,
                    MensajeAgregar = Properties.Resources.SolicitudMateriaPrima_SeleccionarLote,
                    TituloEtiqueta = Properties.Resources.SolicitudMateriaPrimaProducto_LoteLeyendaBuscar,
                    TituloPantalla = Properties.Resources.SolicitudMateriaPrimaProducto_BusquedaLote_Titulo,
                    MetodoPorDescripcion = "ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado"
                };

                skAyudaAlmacenInventarioLote.ObtenerDatos += ObtenerDatosLote;
                skAyudaAlmacenInventarioLote.LlamadaMetodosNoExistenDatos += LimpiarTodoLote;

                skAyudaAlmacenInventarioLote.AsignaTabIndex(3);
                splAyudaLote.Children.Clear();
                splAyudaLote.Children.Add(skAyudaAlmacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        private void LimpiarTodoLote()
        {
            skAyudaAlmacenInventarioLote.LimpiarCampos();
        }

        private void ObtenerDatosLote(string filtro)
        {
            try
            {
                if (skAyudaProducto.Clave != "")
                {
                    var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                    AlmacenInventarioLoteInfo almacenInventarioLote = skAyudaAlmacenInventarioLote.Info;
                    almacenInventarioLote.OrganizacionId = organizacionId;
                    almacenInventarioLote.ProductoId = int.Parse(skAyudaProducto.Clave);
                    almacenInventarioLote.Lote = Convert.ToInt32(filtro);
                    almacenInventarioLote =
                        almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorFolio(
                            almacenInventarioLote);

                    if (almacenInventarioLote != null)
                    {
                        if (almacenInventarioLote.TipoAlmacenId == (int) TipoAlmacenEnum.PlantaDeAlimentos)
                        {
                            almacenInventarioLote.TipoAlmacenId = (int) TipoAlmacenEnum.PlantaDeAlimentos;
                            almacenInventarioLote.ProductoId = Convert.ToInt32(skAyudaProducto.Clave);
                            almacenInventarioLote.OrganizacionId = organizacionId;
                            btnAgregarLote.Focus();
                        }
                        else
                        {
                            skAyudaAlmacenInventarioLote.LimpiarCampos();
                            skAyudaAlmacenInventarioLote.Info = new AlmacenInventarioLoteInfo()
                            {
                                ProductoId = int.Parse(skAyudaProducto.Clave),
                                OrganizacionId = organizacionId,
                                TipoAlmacenId = (int) TipoAlmacenEnum.PlantaDeAlimentos,
                                Activo = EstatusEnum.Activo
                            };
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SolicitudMateriaPrima_TipoAlmacen,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            skAyudaAlmacenInventarioLote.AsignarFoco();
                        }
                    }
                    else
                    {
                        skAyudaAlmacenInventarioLote.LimpiarCampos();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_LoteInvalido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        int productoId = 0;
                        if (skAyudaProducto.Clave != "")
                        {
                            productoId = Convert.ToInt32(skAyudaProducto.Clave);
                        }
                        skAyudaAlmacenInventarioLote.Info = new AlmacenInventarioLoteInfo()
                        {
                            ProductoId = productoId,
                            OrganizacionId = organizacionId,
                            TipoAlmacenId = (int) TipoAlmacenEnum.PlantaDeAlimentos,
                            Activo = EstatusEnum.Activo
                        };
                        skAyudaAlmacenInventarioLote.AsignarFoco();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarTodoProductos()
        {
            skAyudaProducto.LimpiarCampos();
            txtCantidadSolicitada.Text = "";
            skAyudaAlmacenInventarioLote.LimpiarCampos();
            skAyudaAlmacenInventarioLote.IsEnabled = false;
            btnAgregarLote.IsEnabled = false;
        }


        /// <summary>
        /// Obtiene los datos del producto seleccionado
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosProducto(string filtro)
        {
            try
            {
                var productoPl = new ProductoPL();
                ProductoInfo productoActual = skAyudaProducto.Info;
                productoActual = productoPl.ObtenerPorIDSinActivo(productoActual);

                if (productoActual != null)
                {
                    if (productoActual.Familia.FamiliaID == (int) FamiliasEnum.MateriaPrimas ||
                        productoActual.Familia.FamiliaID == (int)FamiliasEnum.Premezclas)
                    {
                        if (productoActual.Activo == EstatusEnum.Activo)
                        {
                            HabilitarCampos(1, true);
                            HabilitarCampos(2,false);
                            skAyudaProducto.Info = productoActual;
                            skAyudaProducto.Info.Familias = new List<FamiliaInfo>()
                            {
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                            };
                            AgregarAyudaLote();
                            skAyudaAlmacenInventarioLote.IsEnabled = true;
                            btnAgregarLote.IsEnabled = true;
                            txtObservaciones.IsEnabled = true;
                            permiteModificar = true;
                        }
                        else
                        {
                            skAyudaProducto.LimpiarCampos();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SolicitudMateriaPrimaProducto_ProductoInactivo,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            skAyudaProducto.Info = new ProductoInfo()
                            {
                                ProductoId = 0,
                                Familia = new FamiliaInfo() { FamiliaID = (int)FamiliasEnum.MateriaPrimas },
                                Familias = new List<FamiliaInfo>()
                                {
                                    new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                                    new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                                },
                                Activo = EstatusEnum.Activo

                            };
                            skAyudaAlmacenInventarioLote.IsEnabled = false;
                            btnAgregarLote.IsEnabled = false;
                        }
                    }
                    else
                    {
                        skAyudaProducto.LimpiarCampos();
                        skAyudaProducto.Info = new ProductoInfo()
                        {
                            ProductoId = 0,
                            Familia = new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                            Familias = new List<FamiliaInfo>()
                            {
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                                new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                            },
                            Activo = EstatusEnum.Activo

                        };
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.SolicitudMateriaPrimaProducto_MsgProductoInvalido,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                        skAyudaAlmacenInventarioLote.IsEnabled = false;
                        btnAgregarLote.IsEnabled = false;
                    }
                }
                else
                {
                    skAyudaProducto.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.SolicitudMateriaPrimaProducto_MsgProductoInvalido,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    skAyudaProducto.Info = new ProductoInfo()
                    {
                        ProductoId = 0,
                        Familia = new FamiliaInfo() { FamiliaID = (int)FamiliasEnum.MateriaPrimas },
                        Familias = new List<FamiliaInfo>()
                        {
                            new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.MateriaPrimas},
                            new FamiliaInfo() {FamiliaID = (int) FamiliasEnum.Premezclas}
                        },
                        Activo = EstatusEnum.Activo
                    };
                    skAyudaAlmacenInventarioLote.IsEnabled = false;
                    btnAgregarLote.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del folio obtenido en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosFolio(string clave)
        {
            try
            {
                PedidoInfo pedidoActual = skAyudaFolioPedido.Info;
                var pedidoPl = new PedidosPL();
                pedidoActual = pedidoPl.ObtenerPedidoPorFolio(pedidoActual.FolioPedido,
                   organizacionId);

                if (pedidoActual != null)
                {
                    if (pedidoActual.EstatusPedido.EstatusId == (int) Estatus.PedidoSolicitado ||
                        pedidoActual.EstatusPedido.EstatusId == (int)Estatus.PedidoProgramado)
                    {
                        pedidoActual.ListaEstatusPedido = new List<EstatusInfo>()
                        {
                            new EstatusInfo(){EstatusId = (int) Estatus.PedidoSolicitado},
                            new EstatusInfo(){EstatusId = (int) Estatus.PedidoProgramado}
                        };
                        HabilitarCampos(1, false);
                        HabilitarCampos(2, true);
                        pedidoInfo = pedidoActual;
                        if (pedidoInfo.DetallePedido != null)
                        {
                            pedidoInfo.DetallePedido =
                                pedidoInfo.DetallePedido.Where(
                                    pedidoDetalleInfo => pedidoDetalleInfo.Producto != null).ToList();
                        }
                        gridDatosPedido.ItemsSource = pedidoInfo.DetallePedido;
                        txtObservaciones.Text = pedidoInfo.Observaciones;
                        BtnImprimirTicket.IsEnabled = true;
                        BtnGuardar.IsEnabled = false;
                        txtObservaciones.IsEnabled = false;
                        permiteModificar = false;
                        skAyudaFolioPedido.Info = pedidoActual;
                    }
                    else
                    {
                        skAyudaFolioPedido.LimpiarCampos();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_FolioInvalido,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        skAyudaFolioPedido.Info = new PedidoInfo()
                        {
                            FolioPedido = 0,
                            Organizacion = new OrganizacionInfo {OrganizacionID = organizacionId},
                            ListaEstatusPedido = new List<EstatusInfo>()
                            {
                                new EstatusInfo() {EstatusId = (int) Estatus.PedidoSolicitado},
                                new EstatusInfo() {EstatusId = (int) Estatus.PedidoProgramado}
                            },
                            Activo = EstatusEnum.Activo
                        };
                    }
                }
                else
                {
                    skAyudaFolioPedido.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.SolicitudMateriaPrima_FolioInvalido,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                    skAyudaFolioPedido.Info = new PedidoInfo()
                    {
                        FolioPedido = 0,
                        Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                        ListaEstatusPedido = new List<EstatusInfo>()
                            {
                                new EstatusInfo() {EstatusId = (int) Estatus.PedidoSolicitado},
                                new EstatusInfo() {EstatusId = (int) Estatus.PedidoProgramado}
                            },
                        Activo = EstatusEnum.Activo
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void GenerarNuevoInventarioLote()
        {
            try
            {
                skAyudaAlmacenInventarioLote.AsignarFoco();
                AlmacenInventarioLoteInfo almacenInventarioLote = null;
                int loteCreado = 0;
                if (skAyudaProducto.Clave != "")
                {
                    int productoId = int.Parse(skAyudaProducto.Clave);

                    if (productoId > 0)
                    {
                        var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                        var almacenPl = new AlmacenPL();
                        var almacenInventarioPl = new AlmacenInventarioPL();

                        var almacenInventario =
                            almacenPl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(new ParametrosOrganizacionTipoAlmacenProductoActivo
                            {
                                OrganizacionId = organizacionId,
                                TipoAlmacenId = (int) TipoAlmacenEnum.PlantaDeAlimentos,
                                Activo = (int) EstatusEnum.Activo,
                                ProductoId = productoId
                            });

                        // Si el producto no se encuentra en el almacen inventario, lo insertamos
                        if (almacenInventario == null)
                        {
                            var listaAlmacenOrganizacion = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionId);
                            if (listaAlmacenOrganizacion != null)
                            {
                                // Obtenemos el almacen y validamos que sea del mismo tipo Almacen
                                foreach (AlmacenInfo almacenInfo in listaAlmacenOrganizacion)
                                {
                                    // Aqui se valida que el almacen sea del tipo seleccionado en pantalla
                                    if (almacenInfo.TipoAlmacen.TipoAlmacenID == (int) TipoAlmacenEnum.PlantaDeAlimentos)
                                    {
                                        almacenInventario = new AlmacenInventarioInfo
                                        {
                                            AlmacenInventarioID =
                                                almacenInventarioPl.Crear(new AlmacenInventarioInfo
                                                {
                                                    AlmacenID = almacenInfo.AlmacenID,
                                                    ProductoID = productoId,
                                                    UsuarioCreacionID = usuarioId
                                                }),
                                            AlmacenID = almacenInfo.AlmacenID
                                        };
                                        break;
                                    }
                                }
                            }
                        }

                        if (almacenInventario != null)
                        {
                            int loteIdCreado = almacenInventarioLotePl.Crear(new AlmacenInventarioLoteInfo
                            {
                                AlmacenInventarioLoteId = 0,
                                AlmacenInventario =
                                    new AlmacenInventarioInfo
                                    {
                                        AlmacenInventarioID = almacenInventario.AlmacenInventarioID
                                    },
                                Cantidad = 0,
                                PrecioPromedio = 0,
                                Piezas = 0,
                                Importe = 0,
                                Activo = EstatusEnum.Activo,
                                UsuarioCreacionId = usuarioId,
                            }, new AlmacenInventarioInfo
                            {
                                AlmacenID = almacenInventario.AlmacenID,
                                ProductoID = productoId
                            });

                            almacenInventarioLote =
                                almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(loteIdCreado);
                            if (almacenInventarioLote != null)
                            {
                                skAyudaAlmacenInventarioLote.Clave = almacenInventarioLote.Lote.ToString(CultureInfo.InvariantCulture);
                                skAyudaAlmacenInventarioLote.Info = almacenInventarioLote;
                                loteCreado = almacenInventarioLote.Lote;
                            }
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_CapturarProducto,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    skAyudaProducto.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }

        private void LimpiarDatos()
        {
            try
            {
                pedidoInfo = new PedidoInfo();
                pedidoInfo.DetallePedido = new List<PedidoDetalleInfo>();
                gridDatosPedido.ItemsSource = pedidoInfo.DetallePedido;
                BtnImprimirTicket.IsEnabled = false;
                BtnGuardar.IsEnabled = true;
                txtObservaciones.Clear();
                skAyudaAlmacenInventarioLote.IsEnabled = false;
                btnAgregarLote.IsEnabled = false;
                skAyudaAlmacenInventarioLote.LimpiarCampos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        private void HabilitarCampos(int opcion, Boolean habilitar)
        {
            if (opcion == 1)
            {
                skAyudaProducto.IsEnabled = habilitar;
                skAyudaAlmacenInventarioLote.IsEnabled = habilitar;
                txtCantidadSolicitada.IsEnabled = habilitar;
                btnAgregar.IsEnabled = habilitar;
                btnLimpiar.IsEnabled = habilitar;
            }
            else
            {
                skAyudaFolioPedido.IsEnabled = habilitar;
            }
        }

        private void TxtCantidadSolicitada_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            LimpiarCamposProducto();
        }

        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (skAyudaProducto.Clave != "")
                {
                    if (txtCantidadSolicitada.Text!= "" && txtCantidadSolicitada.Text != "0")
                    {
                        //if (Convert.ToInt32(txtCantidadSolicitada.GetHashCode()) > 0)
                        //{
                            if (skAyudaAlmacenInventarioLote.Clave != "")
                            {
                                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                                var parametrosOrganizacion = new ParametrosOrganizacionTipoAlmacenProductoActivo();
                                var productoPl = new ProductoPL();

                                parametrosOrganizacion.OrganizacionId = organizacionId;
                                parametrosOrganizacion.ProductoId = Convert.ToInt32(skAyudaProducto.Clave);
                                parametrosOrganizacion.TipoAlmacenId = (int) TipoAlmacenEnum.PlantaDeAlimentos;
                                int lote = Convert.ToInt32(skAyudaAlmacenInventarioLote.Clave);
                                AlmacenInventarioLoteInfo almacenInventarioLote =
                                    almacenInventarioLotePl.ObtenerPorLoteOrganizacionTipoAlmacenProducto(lote,
                                        parametrosOrganizacion);

                                if (almacenInventarioLote != null)
                                {
                                    if (almacenInventarioLote.TipoAlmacenId == (int) TipoAlmacenEnum.PlantaDeAlimentos)
                                    {
                                        if (renglonActualizado == -1)
                                        {
                                            var detallePedido = new PedidoDetalleInfo();
                                            detallePedido.Producto = new ProductoInfo()
                                            {
                                                ProductoId = skAyudaProducto.Info.ProductoId
                                            };
                                            detallePedido.Producto = productoPl.ObtenerPorID(detallePedido.Producto);
                                            if (
                                                pedidoInfo.DetallePedido.Where(
                                                    registro =>
                                                        registro.InventarioLoteDestino.AlmacenInventarioLoteId ==
                                                        almacenInventarioLote.AlmacenInventarioLoteId)
                                                    .ToList()
                                                    .Count >
                                                0)
                                            {
                                                var pedidoDetalleInfo = pedidoInfo.DetallePedido.FirstOrDefault(
                                                    registro =>
                                                        registro.InventarioLoteDestino.AlmacenInventarioLoteId ==
                                                        almacenInventarioLote.AlmacenInventarioLoteId);
                                                if (pedidoDetalleInfo != null)
                                                    SkMessageBox.Show(
                                                        Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        (string.Format(
                                                            Properties.Resources
                                                                .SolicitudMateriaPrima_LoteRegistrado,
                                                            almacenInventarioLote.Lote,
                                                            pedidoDetalleInfo.Producto.ProductoDescripcion)),
                                                        MessageBoxButton.OK,
                                                        MessageImage.Stop);
                                                return;
                                            }
                                            if (
                                                pedidoInfo.DetallePedido.Where(
                                                    registro =>
                                                        registro.Producto.ProductoId ==
                                                        Convert.ToInt32(skAyudaProducto.Clave))
                                                    .ToList()
                                                    .Count == 0)
                                            {
                                                detallePedido.CantidadSolicitada =
                                                    Convert.ToDecimal(txtCantidadSolicitada.Text);
                                                detallePedido.InventarioLoteDestino = almacenInventarioLote;
                                                pedidoInfo.DetallePedido.Add(detallePedido);
                                                gridDatosPedido.ItemsSource = new List<PedidoDetalleInfo>();
                                                gridDatosPedido.ItemsSource = pedidoInfo.DetallePedido;
                                                LimpiarCamposProducto();
                                            }
                                            else
                                            {

                                                SkMessageBox.Show(
                                                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                    (string.Format(
                                                        Properties.Resources
                                                            .SolicitudMateriaPrima_ProductoRegistrado,
                                                        detallePedido.Producto.ProductoDescripcion)),
                                                    MessageBoxButton.OK,
                                                    MessageImage.Stop);
                                            }
                                        }
                                        else
                                        {
                                            pedidoInfo.DetallePedido[renglonActualizado].CantidadSolicitada =
                                                Convert.ToDecimal(txtCantidadSolicitada.Text);
                                            gridDatosPedido.ItemsSource = new List<PedidoDetalleInfo>();
                                            gridDatosPedido.ItemsSource = pedidoInfo.DetallePedido;
                                            LimpiarCamposProducto();
                                        }
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.SolicitudMateriaPrima_TipoAlmacen,
                                            MessageBoxButton.OK,
                                            MessageImage.Stop);
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.SolicitudMateriaPrima_LoteInvalido,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.SolicitudMateriaPrima_CapturarLote,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                skAyudaAlmacenInventarioLote.AsignarFoco();
                            }
                        /*}
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_CapturarCantidad,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                            txtCantidadSolicitada.Focus();
                        }*/
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_CapturarCantidad,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        txtCantidadSolicitada.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_CapturarProducto,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                    skAyudaProducto.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarCamposProducto()
        {
            skAyudaProducto.IsEnabled = true;
            skAyudaProducto.LimpiarCampos();
            txtCantidadSolicitada.Text = "";
            skAyudaAlmacenInventarioLote.LimpiarCampos();
            skAyudaAlmacenInventarioLote.IsEnabled = false;
            btnAgregarLote.IsEnabled = false;
            renglonActualizado = -1;
            btnAgregar.Content = Properties.Resources.SolicitudMateriaPrima_btnAgregar;
            skAyudaProducto.AsignarFoco();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (permiteModificar)
                {
                    if (gridDatosPedido.SelectedIndex > -1)
                    {
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            string.Format(Properties.Resources.SolicitudMateriaPrima_MsgEliminarProducto,
                                pedidoInfo.DetallePedido[gridDatosPedido.SelectedIndex].Producto.ProductoDescripcion),
                            MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                        {
                            pedidoInfo.DetallePedido.RemoveAt(gridDatosPedido.SelectedIndex);
                            gridDatosPedido.ItemsSource = new List<PedidoDetalleInfo>();
                            gridDatosPedido.ItemsSource = pedidoInfo.DetallePedido;
                            LimpiarCamposProducto();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BtbEdidar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (permiteModificar)
                {
                    if (gridDatosPedido.SelectedIndex > -1)
                    {
                        skAyudaProducto.Clave =
                            pedidoInfo.DetallePedido[gridDatosPedido.SelectedIndex].Producto.ProductoId.ToString();
                        skAyudaProducto.Descripcion =
                            pedidoInfo.DetallePedido[gridDatosPedido.SelectedIndex].Producto.ProductoDescripcion;
                        txtCantidadSolicitada.Text =
                            pedidoInfo.DetallePedido[gridDatosPedido.SelectedIndex].CantidadSolicitada.ToString();
                        skAyudaAlmacenInventarioLote.Clave =
                            pedidoInfo.DetallePedido[gridDatosPedido.SelectedIndex].InventarioLoteDestino.Lote.ToString();
                        gridDatosPedido.ItemsSource = pedidoInfo.DetallePedido;
                        renglonActualizado = gridDatosPedido.SelectedIndex;
                        skAyudaProducto.IsEnabled = false;
                        skAyudaAlmacenInventarioLote.IsEnabled = false;
                        btnAgregarLote.IsEnabled = false;
                        btnAgregar.Content = Properties.Resources.SolicitudMateriaPrima_btnActualizar;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pedidoInfo.DetallePedido.Count > 0)
                {
                    var pedidoPl = new PedidosPL();
                    pedidoInfo.Organizacion = new OrganizacionInfo() {OrganizacionID = organizacionId};
                    pedidoInfo.EstatusPedido = new EstatusInfo() {EstatusId = (int) Estatus.PedidoSolicitado};
                    pedidoInfo.Almacen = new AlmacenInfo() {AlmacenID = almacenId};
                    pedidoInfo.UsuarioCreacion = new UsuarioInfo() {UsuarioCreacionID = usuarioId};
                    pedidoInfo.Observaciones = txtObservaciones.Text;
                    pedidoInfo.Activo = EstatusEnum.Activo;
                    var pedido = pedidoPl.Crear(pedidoInfo, TipoFolio.SolicitudMateriaPrima);
                    if (pedido != null)
                    {

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            (string.Format(Properties.Resources.SolicitudMateriaPrima_FolioPedidoGuardado,
                                pedido.FolioPedido)),
                            MessageBoxButton.OK,
                            MessageImage.Correct);
                        LimpiarDatos();
                        LimpiarTodoFolio();
                        LimpiarTodoProductos();
                        txtObservaciones.Clear();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_ErrorGuardar,
                            MessageBoxButton.OK,
                            MessageImage.Error);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitudMateriaPrima_SinDatos,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.SolicitudMateriaPrima_MsgCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarDatos();
                LimpiarTodoFolio();
                LimpiarTodoProductos();
                txtObservaciones.Clear();
                btnAgregar.Content = Properties.Resources.SolicitudMateriaPrima_btnAgregar;
                permiteModificar = true;
            }
        }

        private void BtnImprimirTicket_OnClick(object sender, RoutedEventArgs e)
        {
            string nombreArchivo = string.Format("PaseAProceso-{0}-{1}-{2}.PDF", pedidoInfo.FechaPedido.Day.ToString("N0").PadLeft(2, '0'), pedidoInfo.FechaPedido.Month.ToString("N0").PadLeft(2, '0'), pedidoInfo.FechaPedido.Year);

            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "*.PDF(*.PDF)|*",
                FileName = nombreArchivo
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var pedidoPl = new SolicitudMateriaPrimaReportePL();
                if (pedidoPl.ImprimirPedidoMateriaPrima(pedidoInfo, dialog.FileName))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudMateriaPrima_msgImpresionCorrecta,
                        MessageBoxButton.OK,
                        MessageImage.Correct);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudMateriaPrima_msgArchivoOcupado,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                }
            }
        }

        private void BtnAgregarLote_OnClick(object sender, RoutedEventArgs e)
        {
            GenerarNuevoInventarioLote();
        }

        private void TxtCantidadSolicitada_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (skAyudaProducto.Clave == "")
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SolicitudMateriaPrima_CapturarProducto,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                skAyudaProducto.AsignarFoco();
            }
        }
    }
}
