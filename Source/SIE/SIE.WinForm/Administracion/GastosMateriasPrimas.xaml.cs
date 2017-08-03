using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.Base.Log;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using SIE.WinForm.Auxiliar;
using System.Linq;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para GastosMateriasPrimas.xaml
    /// </summary>
    public partial class GastosMateriasPrimas
    {
        #region Propiedades
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        private SKAyuda<CuentaSAPInfo> skAyudaCuenta;
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        private IList<UnidadMedicionInfo> unidadesMedicion;
        private GastoMateriaPrimaInfo Contexto
        {
            get { return (GastoMateriaPrimaInfo)DataContext; }
            set { DataContext = value; }
        }
        private bool esAreteSukarne;
        #endregion Propiedades

        #region Constructor
        public GastosMateriasPrimas()
        {
            InitializeComponent();
            InicializarFormulario();
            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtFecha.IsReadOnly = true;
        }
        #endregion Constructor
        
        #region Metodos

        private void InicializaContexto()
        {
            Contexto = new GastoMateriaPrimaInfo
                           {
                               AlmacenID = 0,
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Proveedor = null,
                               CuentaSAP = null,
                               Organizacion = new OrganizacionInfo(),
                               Producto = new ProductoInfo(),
                               Importe = 0,
                               Activo = EstatusEnum.Activo,
                               Almacen = new AlmacenInfo
                                             {
                                                 TipoAlmacen = new TipoAlmacenInfo()
                                             }
                           };
        }

        //Inicializa el formulario
        private void InicializarFormulario()
        {
            AgregarTiposMovimiento();
            InicializaContexto();
            AgregarAyudaOrganizacion();
            AgregarAyudaProducto();
            AgregarAyudaProveedor(splAyudaCuentaProveedor);

            cmbLote.IsEnabled = true;
            cmbLote.ItemsSource = null;
            cmbLote.IsEnabled = false;
            txtImporte.Value = 0;
            chkCuenta.IsChecked = false;
            chkIva.IsChecked = false;
            txtObservaciones.Text = string.Empty;
            skAyudaOrganizacion.AsignarFoco();
        }

        //Agrega la ayuda para la organizacion
        private void AgregarAyudaOrganizacion()
        {
            try
            {
                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(240,
                    false,
                    new OrganizacionInfo
                    {
                        Activo = EstatusEnum.Activo,
                        TipoOrganizacion = new TipoOrganizacionInfo
                        {
                            TipoOrganizacionID = (int)TipoOrganizacion.Ganadera
                        },
                        ListaTiposOrganizacion = LLenarListadoTipoOrganizaciones(),
                    },
                    "PropiedadClaveGastosMateriaPrima",
                    "PropiedadDescripcionGastosMateriaPrima",
                    true,
                    50,
                    true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.GastosMateriasPrimas_OrganizacionNoValida,
                    MensajeBusquedaCerrar = Properties.Resources.GastosMateriasPrimas_AyudaOrganizacion_MsjCancelar,
                    MensajeBusqueda = Properties.Resources.EntradaMateriaPrima_Busqueda,
                    MensajeAgregar = Properties.Resources.GastosMateriasPrimas_AyudaOrganizacion_MsjAgregar,
                    TituloEtiqueta = Properties.Resources.GastosMateriasPrimas_lblOrganizacion,
                    TituloPantalla = Properties.Resources.GastosMateriasPrimas_AyudaOrganizacionTitulo,
                    MetodoPorDescripcion = "ObtenerPorIdFiltroTiposOrganizacion"
                };

                skAyudaOrganizacion.AyudaConDatos += (sender, args) => ObtenerDatosAyudaOrganizacion();
                skAyudaOrganizacion.AyudaLimpia += (sender, args) =>
                                                       {
                                                           Contexto.Almacen = new AlmacenInfo();
                                                           Contexto.Producto = new ProductoInfo();
                                                           btnAretes.IsEnabled = false;
                                                       };
                skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarTodoOrganizacion;

                skAyudaOrganizacion.AsignaTabIndex(0);
                splAyudaOrganizacion.Children.Clear();
                splAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
                skAyudaOrganizacion.TabIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        //Obtiene los datos de la organizacion seleccionada
        private void ObtenerDatosAyudaOrganizacion()
        {
            Contexto.Organizacion = skAyudaOrganizacion.Info;
            var organizacion = Extensor.ClonarInfo(Contexto.Organizacion) as OrganizacionInfo;
            Contexto.Almacen.Organizacion = organizacion;
            Contexto.Producto.Organizacion = organizacion;
            Contexto.Organizacion.ListaTiposOrganizacion = LLenarListadoTipoOrganizaciones();

            LimpiarTodoProductos();
            LimpiarTodoProveedor();
            LimpiarTodoCuenta();
        }

        //Limpia los datos cuando no existe la organizacion
        private void LimpiarTodoOrganizacion()
        {
            InicializarFormulario();
        }

        //Agrega la ayuda para el producto
        private void AgregarAyudaProducto()
        {
            try
            {
                Contexto.CuentaSAP = new CuentaSAPInfo();
                var familia = new FamiliaInfo
                                  {
                                      FamiliaID = 0
                                  };
                if (Contexto.Almacen != null && Contexto.Almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.GeneralGanadera.GetHashCode())
                {
                    familia.FamiliaID = FamiliasEnum.Medicamento.GetHashCode();
                }
                Contexto.Producto = new ProductoInfo
                                        {
                                            Activo = EstatusEnum.Activo,
                                            AlmacenID = Contexto.Almacen.AlmacenID,
                                            Familia = familia,
                                            Organizacion = Contexto.Organizacion
                                        };

                skAyudaProducto.AyudaConDatos += (sender, args) => ObtenerDatosAyudaProductos();
                skAyudaProducto.PuedeBuscar += () => Contexto.Almacen.AlmacenID > 0;
                skAyudaProducto.AyudaLimpia += (sender, args) =>
                                                   {
                                                       LimpiarTodoProductos();
                                                       Contexto.Producto.AlmacenID = Contexto.Almacen.AlmacenID;
                                                   };  
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void ValidarCapturaAretes() {
            var producto = 0;
            if (skAyudaProducto.Clave != string.Empty)
            {
                producto = Convert.ToInt32(skAyudaProducto.Clave);
            }
            else {
                producto = -1;
            }
            
            var tipoOrg = Contexto.Organizacion.TipoOrganizacion.TipoOrganizacionID;
            esAreteSukarne = false;
            btnAretes.IsEnabled = false;
            if (tipoOrg == TipoOrganizacion.Centro.GetHashCode() || tipoOrg == TipoOrganizacion.Cadis.GetHashCode() || tipoOrg == TipoOrganizacion.Descanso.GetHashCode())
            {
                btnAretes.IsEnabled = ValidaHabilitarCapturaAretes(producto);
            }
        }

        //Obtinee los datos de el producto seleccionado
        private void ObtenerDatosAyudaProductos()
        {
            var tipoOrg = Contexto.Organizacion.TipoOrganizacion.TipoOrganizacionID;
            cmbLote.IsEnabled = true;
            cmbLote.ItemsSource = null;
            cmbLote.IsEnabled = false;
            btnAretes.IsEnabled = false;
            Contexto.Producto.Organizacion = Contexto.Organizacion;
            if (Contexto.Producto.ProductoId > 0 && tipoOrg != TipoOrganizacion.Centro.GetHashCode() && tipoOrg != TipoOrganizacion.Descanso.GetHashCode() && tipoOrg != TipoOrganizacion.Cadis.GetHashCode())
            {
                UnidadMedicionInfo unidadMedicion =
                    unidadesMedicion.First(id => id.UnidadID == Contexto.Producto.UnidadMedicion.UnidadID);
                if (unidadMedicion != null)
                {
                    lblUnidadMedicion.Content = unidadMedicion.Descripcion;
                }
            }

            var producto = Convert.ToInt32(skAyudaProducto.Clave);
            esAreteSukarne = false;
            btnAretes.IsEnabled = false;
            
            if (tipoOrg == TipoOrganizacion.Centro.GetHashCode() || tipoOrg == TipoOrganizacion.Cadis.GetHashCode() || tipoOrg == TipoOrganizacion.Descanso.GetHashCode())
            {
                btnAretes.IsEnabled = ValidaHabilitarCapturaAretes(producto);
            }
            Contexto.Producto.AlmacenID = Contexto.Almacen.AlmacenID;

            Contexto.Producto.AlmacenID = Contexto.Almacen.AlmacenID;

            ObtenerLotes();
            //Contexto.Producto.AlmacenID = Contexto.Almacen.AlmacenID;

            
        }

        private bool ValidaHabilitarCapturaAretes(int producto)
        {
            var result = true;
            esAreteSukarne = false;
            var parametroGeneralPL = new ParametroGeneralPL();
            var parametroGeneral = parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.RecProdAlmRepSAP.ToString());
            var parametroGeneralNacional = parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.RecProdAlmRepSAPNac.ToString());
            if (parametroGeneral != null && parametroGeneralNacional != null)
            {
                var productos = parametroGeneral.Valor.Split('|').ToList();
                var productosNac = parametroGeneralNacional.Valor.Split('|').ToList();
                /*var productosCapturaAretes = productos.Select(p => Convert.ToInt32(p)).ToList();
                var productosCapturaAretesNacionales = productosNac.Select(p => Convert.ToInt32(p)).ToList();*/

                var productosCapturaAretes = productos.Select(p => p.ToString()).ToList();
                var productosCapturaAretesNacionales = productosNac.Select(p => p.ToString()).ToList();

                if (!productosCapturaAretesNacionales.Contains(producto.ToString()))
                {
                    if (!productosCapturaAretes.Contains(producto.ToString()))
                    {
                        result = false;
                    }
                    else
                    {
                        esAreteSukarne = true;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        //Limpia los datos cuando no existe el producto
        private void LimpiarTodoProductos()
        {
            Contexto.Producto.AlmacenID = Contexto.Almacen.AlmacenID;

            cmbLote.IsEnabled = true;
            cmbLote.ItemsSource = null;
            cmbLote.IsEnabled = false;
            btnAretes.IsEnabled = false;
            txtImporte.Value = 0;
            chkIva.IsChecked = false;
            chkCuenta.IsChecked = false;
            txtObservaciones.Text = string.Empty;
            if (skAyudaProducto != null)
            {
                if (Contexto.Producto != null && Contexto.Producto.ProductoId > 0)
                {
                    skAyudaProducto.LimpiarCampos();
                }
            }
        }

        //Agrega la ayuda para la cuenta
        private void AgregarAyudaCuenta(StackPanel stackPanel)
        {
            Contexto.Proveedor = new ProveedorInfo();
            var info = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() },
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Producto.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Gastos.GetHashCode()},
                },

                Activo = EstatusEnum.Activo
            };

            skAyudaCuenta = new SKAyuda<CuentaSAPInfo>(210, false, info,
                "PropuedadClaveGastoInventario",
                "PropuedadDescripcionGastoInventario", true, 80, 10, true)
                {
                    AyudaPL = new CuentaSAPPL(),
                    MetodoPorId = "ObtenerPorFiltro",
                    MetodoPorDescripcion = "ObtenerPorPagina",
                    MetodoPaginadoBusqueda = "ObtenerPorPagina",
                    MensajeClaveInexistente = Properties.Resources.GastosMateriasPrimas_CuentaSAPNoValida,
                    MensajeAgregar = Properties.Resources.GastosMateriasPrimas_AyudaCuenta_MsjAgregar,
                    MensajeBusqueda = Properties.Resources.Cuenta_Busqueda,
                    MensajeBusquedaCerrar = Properties.Resources.GastosMateriasPrimas_AyudaCuenta_MsjCerrar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCuenta,
                    TituloPantalla = Properties.Resources.BusquedaCuenta_Titulo
                };

            //skAyudaCuenta.CamposInfo = camposInfo;

            skAyudaCuenta.ObtenerDatos += ObtenerDatosCuenta;
            skAyudaCuenta.LlamadaMetodosNoExistenDatos += LimpiarTodoCuenta;

            skAyudaCuenta.AsignaTabIndex(8);
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCuenta);

        }

        //Obtiene los datos de la cuenta seleccionada
        private void ObtenerDatosCuenta(string filtro)
        {
            try
            {
                Contexto.CuentaSAP = skAyudaCuenta.Info;
                skAyudaCuenta.Info = new CuentaSAPInfo
                {
                    ListaTiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() },
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode()},
                },
                    Activo = EstatusEnum.Activo
                };

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        //Limpia los datos cuando no existe la cuenta
        private void LimpiarTodoCuenta()
        {
            Contexto.CuentaSAP = null;
            if (skAyudaCuenta != null)
            {
                skAyudaCuenta.LimpiarCampos();
            }
        }

        //Agrega la ayuda para el Proveedor
        private void AgregarAyudaProveedor(StackPanel stackPanel)
        {
            Contexto.CuentaSAP = new CuentaSAPInfo();
            var proveedorInfo = new ProveedorInfo
            {
                ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()},
                        new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.Comisionistas.GetHashCode()}
                    },

                Activo = EstatusEnum.Activo
            };

            skAyudaProveedor = new SKAyuda<ProveedorInfo>(210, false, proveedorInfo
                , "PropiedadCodigoSapEntradaMateriaPrima"
                , "PropiedadDescripcionEntradaMateriaPrima", true, 80, 10, true)
                {
                    AyudaPL = new ProveedorPL(),
                    MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_ProveedorSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                    MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                    TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo
                };

            skAyudaProveedor.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaProveedor.LlamadaMetodosNoExistenDatos += LimpiarTodoProveedor;

            skAyudaProveedor.AsignaTabIndex(8);
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);

        }

        //Obtiene los datos del proveedor selecionado
        private void ObtenerDatosProveedor(string filtro)
        {
            try
            {
                Contexto.Proveedor = skAyudaProveedor.Info;
                skAyudaProveedor.Info = new ProveedorInfo
                {
                    ListaTiposProveedor = new List<TipoProveedorInfo>
                    {
                        new TipoProveedorInfo{ TipoProveedorID = TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() },
                        new TipoProveedorInfo{TipoProveedorID = TipoProveedorEnum.ProveedoresFletes.GetHashCode()}
                    },
                    Activo = EstatusEnum.Activo
                };
                Contexto.CuentaSAP = new CuentaSAPInfo();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        //Limpia los datos cuando no existe el proveedor
        private void LimpiarTodoProveedor()
        {
            Contexto.Proveedor = null;
            if (skAyudaProveedor != null)
            {
                skAyudaProveedor.LimpiarCampos();
            }
        }

        //Agrega los Tipos de Movimientos para el gasto
        private void AgregarTiposMovimiento()
        {
            try
            {
                var tipoMoviminetoPL = new TipoMovimientoPL();
                IList<TipoMovimientoInfo> tiposMovimientos = new List<TipoMovimientoInfo>();

                tiposMovimientos.Add(tipoMoviminetoPL.ObtenerPorID((int)TipoMovimiento.EntradaPorAjuste));
                tiposMovimientos.Add(tipoMoviminetoPL.ObtenerPorID((int)TipoMovimiento.SalidaPorAjuste));
                if (tiposMovimientos.Count > 0)
                {
                    cmbTipoMovimiento.ItemsSource = tiposMovimientos;
                    cmbTipoMovimiento.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        //Asigna los lotes cuando se selecciona un producto
        private void ObtenerLotes()
        {
            try
            {
                var almacenPl = new AlmacenPL();
                AlmacenInventarioInfo almacenInvAux = new AlmacenInventarioInfo();
                almacenInvAux  = almacenPl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(
                    new ParametrosOrganizacionTipoAlmacenProductoActivo
                    {
                        OrganizacionId = Contexto.Organizacion.OrganizacionID,
                        Activo = (int)EstatusEnum.Activo,
                        TipoAlmacenId = Contexto.Almacen.TipoAlmacen.TipoAlmacenID,
                        ProductoId = Contexto.Producto.ProductoId,
                        AlmacenID = Contexto.Almacen.AlmacenID
                    });

                if(almacenInvAux != null)
                {
                    Contexto.AlmacenInventario = almacenInvAux;
                }
                else
                {
                    if(Contexto.AlmacenInventario == null)
                    {
                        Contexto.AlmacenInventario = new AlmacenInventarioInfo
                        {
                            AlmacenID = Contexto.Almacen.AlmacenID,
                            Producto = Contexto.Producto,
                            Almacen = Contexto.Almacen
                        };

                    }
                }
                

                var almacenInventarioLote = new AlmacenInventarioLotePL();
                List<AlmacenInventarioLoteInfo> lotes = almacenInventarioLote.ObtenerPorAlmacenInventarioID(Contexto.AlmacenInventario);
                if (lotes != null)
                {
                    if (Contexto.Organizacion.TipoOrganizacion.TipoOrganizacionID != TipoOrganizacion.Ganadera.GetHashCode())
                    {
                        lotes = lotes.Where(lote => lote.Cantidad > 0 && lote.Activo == EstatusEnum.Activo && (lote.FechaFin == new DateTime(1,1,1) || lote.FechaFin == null)).ToList();
                    }
                    else
                    {
                        lotes = lotes.Where(lote => lote.Activo == EstatusEnum.Activo && (lote.FechaFin == new DateTime(1, 1, 1) || lote.FechaFin == null)).ToList();
                    }
                    //lotes = lotes.Where(lote => lote.Cantidad > 0).ToList();
                    cmbLote.IsEnabled = true;
                    cmbLote.ItemsSource = lotes;
                    cmbLote.SelectedValuePath = "AlmacenInventarioLoteID";
                    cmbLote.DisplayMemberPath = "Lote";
                    cmbLote.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ErrorTipoOrganizacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        //Valida que se encuentren asignados los datos
        private bool ValidarDatos(AlmacenInventarioLoteInfo almacenInventarioInfobd)
        {
            if (Contexto.Organizacion == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidacionOrganizacion,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }

            if (Contexto.Producto == null || Contexto.Producto.ProductoId <= 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidacionProducto,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }

            if ((chkCuenta.IsChecked.HasValue && chkCuenta.IsChecked.Value) && (Contexto.CuentaSAP == null || Contexto.CuentaSAP.CuentaSAPID == 0))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidacionCuenta,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }

            if (Contexto.Almacen.AlmacenID == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidacionAlmacen,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }


            if ((chkCuenta.IsChecked.HasValue && !chkCuenta.IsChecked.Value) && Contexto.Proveedor == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidacionProveedor,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                return false;
            }

            if (Contexto.Organizacion.TipoOrganizacion.TipoOrganizacionID != TipoOrganizacion.Ganadera.GetHashCode())
            {
                if (Contexto.UnidadMedida && Contexto.Kilogramos == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.GastosMateriasPrimas_ValidacionCantidadUnidadMedida,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                    txtKilos.Focus();
                    return false;
                }

                if (txtImporte.Value > 0 && ValidarImporte())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.GastosMateriasPrimas_MsgImporteInvalido,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    txtImporte.Focus();
                    return false;
                }

                if (txtImporte.Value > 0 && ValidarCantidad())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.GastosMateriasPrimas_MsgCantidadInvalido,
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    txtKilos.Focus();
                    return false;
                }

            }


            if (Contexto.AretesCapturados == null)
            {
                Contexto.AretesCapturados = new List<AreteInfo>();
            }

            if (btnAretes.IsEnabled && Convert.ToBoolean(chkUnidadMedida.IsChecked) && Convert.ToDouble(txtKilos.Text) > 0 && Contexto.AretesCapturados.Count == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       string.Format(Properties.Resources.GastosMateriasPrimas_MsgCapturaAretes, Contexto.Producto.ProductoId),
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                btnAretes.Focus();
                return false;
            }

            if (btnAretes.IsEnabled && Contexto.AretesCapturados.Count <= Convert.ToDouble(txtKilos.Text) && Contexto.AretesCapturados.Count + 1 > Convert.ToDouble(txtKilos.Text))
            {
                var pl = new GastoMateriaPrimaPL();
                var tipoMovimiento = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedValue;
                var mensaje = string.Empty;
                IEnumerable<AreteInfo> nuevoListado = null;
                if(tipoMovimiento.EsEntrada)
                {
                    nuevoListado = pl.ConsultarAretes(Contexto.AretesCapturados.ToList(), Contexto.Organizacion.OrganizacionID, esAreteSukarne, true);
                    if (nuevoListado.Any())
                    {
                        mensaje = nuevoListado.Select(item => item.Arete).FirstOrDefault();
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               string.Format(Properties.Resources.GastosMateriasPrimas_MsgAretesActivos, mensaje), MessageBoxButton.OK,
                                    MessageImage.Warning);

                        return false;
                    } 
                }
                else{
                    nuevoListado = pl.ConsultarAretes(Contexto.AretesCapturados.ToList(), Contexto.Organizacion.OrganizacionID, esAreteSukarne, false);
                    if (nuevoListado.Any())
                    {
                        mensaje = nuevoListado.Select(item => item.Arete).FirstOrDefault();
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               string.Format(Properties.Resources.GastosMateriasPrimas_MsgAretesNoExistentes, mensaje), MessageBoxButton.OK,
                                    MessageImage.Warning);

                        return false;
                    } 
                }              
            }
            else {
                if (btnAretes.IsEnabled)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           string.Format(Properties.Resources.GastosMateriasPrimasAretes_ValidarCantidad, txtKilos.Text, Contexto.AretesCapturados.Count),
                           MessageBoxButton.OK,
                           MessageImage.Stop);
                    btnAretes.Focus();
                    return false;
                }         
            }

            //if (Contexto.Organizacion.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode())
            //{
                var tipoMovimientoInfo = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;
                if (tipoMovimientoInfo.EsSalida)
                {
                    if (almacenInventarioInfobd.Cantidad <= 0 && Contexto.Kilogramos > 0)
                    {
                        // Validar cantidad del producto en 0
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosMateriasPrimas_ValidaSalidaCantidadCero,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                        Contexto.Kilogramos = 0;
                        txtKilos.Text = "0";
                        return false;
                    }
                    else if (almacenInventarioInfobd.Importe <= 0 && Contexto.Importe > 0)
                    {
                        // Validar importe del producto en 0
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosMateriasPrimas_ValidaSalidaImporteCero,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                        Contexto.Importe = 0;
                        txtImporte.Text = "0";
                        return false;
                    }
                    else if (almacenInventarioInfobd.Importe == 0 && almacenInventarioInfobd.Cantidad > 0 && Contexto.Importe > 0)
                    {
                        // Validar importe en 0 cantidad mayor a 0
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidaSalidaImporteCero,
                       MessageBoxButton.OK,
                       MessageImage.Error);
                        Contexto.Importe = 0;
                        txtImporte.Text = "0";
                        return false;
                    }
                    else if (almacenInventarioInfobd.Importe == 0 && almacenInventarioInfobd.Cantidad < 0 && Contexto.Importe > 0 && Contexto.Kilogramos > 0)
                    {
                        // Validar importe en 0 cantidad menor a 0
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosMateriasPrimas_ValidaSalidaImporteCero,
                       MessageBoxButton.OK,
                       MessageImage.Error);
                        Contexto.Importe = 0;
                        txtImporte.Text = "0";
                        return false;
                    }
                    else if (almacenInventarioInfobd.Cantidad < Contexto.Kilogramos && Contexto.Kilogramos > 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       string.Format(Properties.Resources.GastosMateriasPrimas_ValidaSalidaCantidadMayorAlInventerio, almacenInventarioInfobd.Cantidad),
                       MessageBoxButton.OK,
                       MessageImage.Error);
                        Contexto.Kilogramos = 0;
                        txtKilos.Text = "0";
                        return false;
                    }
                    else if (almacenInventarioInfobd.Importe < Contexto.Importe && Contexto.Importe > 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       string.Format(Properties.Resources.GastosMateriasPrimas_ValidaSalidaImporteMayorAlInventerio, almacenInventarioInfobd.Importe),
                       MessageBoxButton.OK,
                       MessageImage.Error);
                        Contexto.Importe = 0;
                        txtImporte.Text = "0";
                        return false;
                    }
                    else if (almacenInventarioInfobd.Importe < 0 && almacenInventarioInfobd.Cantidad > 0 && Contexto.Importe == 0 && Contexto.Kilogramos > 0)
                    {
                        Contexto.Importe = almacenInventarioInfobd.Importe ;
                        txtImporte.Value = Contexto.Importe;
                    }
                    else if (almacenInventarioInfobd.Cantidad < 0 && almacenInventarioInfobd.Importe > 0 && Contexto.Importe > 0 && Contexto.Kilogramos == 0)
                    {
                        Contexto.Kilogramos = almacenInventarioInfobd.Cantidad;
                        txtKilos.Value = Contexto.Kilogramos;
                    }
                    else if(almacenInventarioInfobd.Importe < 0 && Contexto.Importe == 0 && Contexto.Kilogramos == 0)
                    {
                        if (almacenInventarioInfobd.Importe < 0)
                        {
                            Contexto.Importe = almacenInventarioInfobd.Importe;
                            txtImporte.Value = Contexto.Importe;
                        }
                        else
                        {
                            Contexto.Importe = almacenInventarioInfobd.Importe * -1;
                            txtImporte.Value = Contexto.Importe;
                        }
                    }
                    else if (Contexto.Importe == 0 && Contexto.Kilogramos == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosMateriasPrimas_ImporteCantidadEnCero,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                        return false;
                    }

                }
                else
                {
                    if (almacenInventarioInfobd.Cantidad <= 0 && Contexto.Kilogramos == 0 && Contexto.Importe > 0 && tipoMovimientoInfo.EsEntrada)
                    {
                        // Validar cantidad en 0 importe mayor a 0
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosMateriasPrimas_ValidaEntradaCantidadCero,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                        return false;
                    }
                    else if (almacenInventarioInfobd.Importe <= 0 && Contexto.Importe == 0 && Contexto.Kilogramos > 0 && tipoMovimientoInfo.EsEntrada)
                    {
                        // Validar cantidad en 0 importe mayor a 0
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosMateriasPrimas_ValidaEntradaImporteCero,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                        return false;
                    }
                    else if (Contexto.Importe == 0 && Contexto.Kilogramos == 0 && tipoMovimientoInfo.EsEntrada)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosMateriasPrimas_ImporteCantidadEnCero,
                        MessageBoxButton.OK,
                        MessageImage.Error);
                        return false;
                    }
                    
                }

            //}

            return true;
        }

        private bool ValidarCantidad()
        {
            var loteInfo = new AlmacenInventarioLoteInfo();
            if (cmbLote.IsEnabled)
            {
                loteInfo = (AlmacenInventarioLoteInfo)cmbLote.SelectedItem;
            }
            var tipoMovimientoInfo = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;

            if (tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste && loteInfo == null)
            {
                Contexto.Kilogramos = 0;
                return true;
            }

            if (cmbLote.IsEnabled && tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste
                && Contexto.Kilogramos > loteInfo.Cantidad)
            {
                Contexto.Kilogramos = loteInfo.Cantidad;
                return true;
            }

            if (tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste && Contexto.AlmacenInventario == null)
            {
                Contexto.Kilogramos = 0;
                return true;
            }

            if (tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste
                && Contexto.Kilogramos > Contexto.AlmacenInventario.Cantidad)
            {
                Contexto.Kilogramos = Contexto.AlmacenInventario.Cantidad;
                return true;
            }
            return false;
        }

        private bool ValidarImporte()
        {
            var loteInfo = new AlmacenInventarioLoteInfo();
            if (cmbLote.IsEnabled)
            {
                loteInfo = (AlmacenInventarioLoteInfo)cmbLote.SelectedItem;
            }

            var tipoMovimientoInfo = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;
            if (tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste && loteInfo == null)
            {
                txtImporte.Value = 0;
                return true;
            }
                        
            if (cmbLote.IsEnabled && tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste 
                && txtImporte.Value > loteInfo.Importe)
            {
                txtImporte.Value = loteInfo.Importe;
                return true;
            }

            if (tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste && Contexto.AlmacenInventario == null)
            {
                txtImporte.Value = 0;
                return true;
            }

            if (tipoMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.SalidaPorAjuste 
                && txtImporte.Value > Contexto.AlmacenInventario.Importe)
            {
                txtImporte.Value = Contexto.AlmacenInventario.Importe;
                return true;
            }
            return false;
        }

        //Llenar listado de tipo organizaciones que necesitamos
        private List<TipoOrganizacionInfo> LLenarListadoTipoOrganizaciones()
        {
            var listaTpoOrg = new List<TipoOrganizacionInfo>();

            var tpoGan = new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Ganadera };
            var tpoCen = new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Centro };
            var tpoPra = new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Praderas };
            var tpoCad = new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Cadis };
            var tpoDes = new TipoOrganizacionInfo { TipoOrganizacionID = (int)TipoOrganizacion.Descanso };

            listaTpoOrg.Add(tpoGan);
            listaTpoOrg.Add(tpoCen);
            listaTpoOrg.Add(tpoPra);
            listaTpoOrg.Add(tpoCad);
            listaTpoOrg.Add(tpoDes);

            return listaTpoOrg;
        }

        #endregion

        #region Eventos

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        // Ocurre cuando el control detecta un click
        private void ChkCuenta_OnClick(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check != null)
            {
                if (check.IsChecked.Value)
                {
                    AgregarAyudaCuenta(splAyudaCuentaProveedor);
                    Contexto.Proveedor = null;
                    chkIva.IsEnabled = false;
                    chkIva.IsChecked = false;
                }
                else
                {
                    AgregarAyudaProveedor(splAyudaCuentaProveedor);
                    Contexto.CuentaSAP = null;
                    chkIva.IsEnabled = true;
                    chkCuenta.IsChecked = false;
                }
            }
        }

        // Evento que se activa cuando se pierde el foco del control
        private void TxtImporte_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Contexto.Importe = decimal.Parse(txtImporte.Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        // Ocurre cuando se teclea en el control
        private void TxtObservaciones_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarCaracterValidoTexo(e.Text);
        }

        // Ocurre cuando le dan click al control
        private void btnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            AlmacenInventarioLotePL almacenInventarioLotePL = new AlmacenInventarioLotePL();
            AlmacenInventarioPL almacenInventarioPL = new AlmacenInventarioPL();
            AlmacenInventarioInfo almacenInventarioInfobd = new AlmacenInventarioInfo();
            AlmacenInventarioLoteInfo almacenInventarioLoteInfobd = new AlmacenInventarioLoteInfo();
            try
            {
                if (cmbLote.IsEnabled)
                {
                    Contexto.AlmacenInventarioLote = (AlmacenInventarioLoteInfo)cmbLote.SelectedItem;
                    almacenInventarioLoteInfobd = almacenInventarioLotePL.ObtenerAlmacenInventarioLotePorId(Contexto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                }
                else
                {
                    Contexto.AlmacenInventarioLote = new AlmacenInventarioLoteInfo();
                    if (Contexto.Organizacion.TipoOrganizacion.TipoOrganizacionID == TipoOrganizacion.Ganadera.GetHashCode())
                    {
                        almacenInventarioInfobd = almacenInventarioPL.ObtenerPorAlmacenIdProductoId(Contexto.AlmacenInventario);
                        //almacenInventarioInfobd = almacenInventarioPL.ObtenerPorOrganizacionIdAlmacenIdProductoId(Contexto.AlmacenInventario, Contexto.Organizacion.OrganizacionID);
                        almacenInventarioLoteInfobd.Importe = almacenInventarioInfobd.Importe;
                        almacenInventarioLoteInfobd.Cantidad = almacenInventarioInfobd.Cantidad;
                    }
                    else
                    {
                        almacenInventarioInfobd = almacenInventarioPL.ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(Contexto.AlmacenInventario, Contexto.Organizacion.OrganizacionID);
                        almacenInventarioLoteInfobd.Importe = almacenInventarioInfobd.Importe;
                        almacenInventarioLoteInfobd.Cantidad = almacenInventarioInfobd.Cantidad;
                    }
                }
                

                if (ValidarDatos(almacenInventarioLoteInfobd))
                {
                    
                    Contexto.TipoMovimiento = (TipoMovimientoInfo)cmbTipoMovimiento.SelectedItem;
                    Contexto.TieneCuenta = chkCuenta.IsChecked.HasValue && chkCuenta.IsChecked.Value;
                    if (chkCuenta.IsChecked.HasValue && chkCuenta.IsChecked.Value)
                    {
                        Contexto.Proveedor = new ProveedorInfo();
                    }
                    else
                    {
                        Contexto.CuentaSAP = new CuentaSAPInfo();
                    }

                    
                   
                        Contexto.Importe = decimal.Parse(txtImporte.Value.ToString());
                        Contexto.Iva = chkIva.IsChecked.HasValue && chkIva.IsChecked.Value;
                        Contexto.AlmacenID = Contexto.Almacen.AlmacenID;
                        Contexto.Observaciones = txtObservaciones.Text;
                        Contexto.GuardaAretes = btnAretes.IsEnabled;
                        Contexto.EsAreteSukarne = esAreteSukarne;

                        var gastoMateriaPrimaPL = new GastoMateriaPrimaPL();
                        long resultado = gastoMateriaPrimaPL.Guardar(Contexto);

                        if (resultado > 0)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                string.Format(Properties.Resources.EntradaMateriaPrima_GuardadoOk, resultado),
                                MessageBoxButton.OK,
                                MessageImage.Correct);
                            InicializarFormulario();
                        }
                    }
                
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.GastosMateriasPrimas_ErrorGuardar + " : "+ ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.GastosMateriasPrimas_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        // Ocurre cuando le dan click al control
        private void btnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.GastosMateriasPrimas_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializarFormulario();
            }
        }

        private void TxtKilosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string valor = ((DecimalUpDown)sender).Value.HasValue
                              ? ((DecimalUpDown)sender).Value.ToString()
                              : string.Empty;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, valor);
        }

        private void GastosMateriaPrimaLoaded(object sender, RoutedEventArgs e)
        {
            var unidadMedicionPL = new UnidadMedicionPL();
            unidadesMedicion = unidadMedicionPL.ObtenerTodos(EstatusEnum.Activo);

            skAyudaAlmacen.AyudaConDatos += (o, args) =>
            {
                AgregarAyudaProducto();
                Contexto.Almacen.TipoAlmacen = new TipoAlmacenInfo();
            };
            skAyudaAlmacen.ObjetoNegocio = new AlmacenPL();
            skAyudaAlmacen.AyudaLimpia += (o, args) => Contexto.Producto = new ProductoInfo();
            skAyudaAlmacen.PuedeBuscar += () => Contexto.Organizacion.OrganizacionID > 0;

            skAyudaProducto.ObjetoNegocio = new ProductoPL();
        }

        private void btnAretes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var edicion = new GastosMateriasPrimasAretes(19,4, esAreteSukarne);
                edicion.Left = (ActualWidth - edicion.Width) / 2;
                edicion.Top = ((ActualHeight - edicion.Height) / 2);
                edicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                edicion.ShowDialog();
                Contexto.AretesCapturados = new List<AreteInfo>();
                if (edicion.ListAretes != null)
                {
                    Contexto.AretesCapturados = edicion.ListAretes;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.GastosMateriasPrimasAretes_ErrorCapturarAretes, MessageBoxButton.OK, MessageImage.Error);
                Contexto.AretesCapturados = new List<AreteInfo>();
            }
        }

        private void skAyudaProducto_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidarCapturaAretes();
        }

        private void skAyudaAlmacen_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Contexto.Producto.ProductoId > 0)
            {
                ValidarCapturaAretes();
            }
            else {
                btnAretes.IsEnabled = false;
            }
        }
        #endregion        
    }
}
