
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
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
using Button = System.Windows.Controls.Button;
using ComboBox = System.Windows.Controls.ComboBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using TextBox = System.Windows.Controls.TextBox;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para PremezclasDistribucionIngredientes.xaml
    /// </summary>
    public partial class PremezclasDistribucionIngredientes
    {

        #region CONSTRUCTORES
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public PremezclasDistribucionIngredientes()
        {
            InitializeComponent();

            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);

            listaDistribucionOrganizacion = new List<DistribucionDeIngredientesOrganizacionInfo>();
            distribucionDeIngredientes = new DistribucionDeIngredientesInfo();
            CargarAyudaPremezclas();
            CargarAyudaProveedor();
        }
        #endregion CONSTRUCTORES

        #region VARIABLES

        ///// <summary>
        ///// Control para la ayuda de Proveedor
        ///// </summary>
        //private SKAyuda<ProveedorInfo> skAyudaProveedor;
        ///// <summary>
        ///// Control para la ayuda de Organización
        ///// </summary>
        //private SKAyuda<CuentaSAPInfo> skAyudaCuenta;

        List<DistribucionDeIngredientesOrganizacionInfo> listaDistribucionOrganizacion;
        private DistribucionDeIngredientesInfo distribucionDeIngredientes;

        private SKAyuda<ProductoInfo> skAyudaPremezcla;
        private SKAyuda<ProveedorInfo> skAyudaProveedores;
        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<CuentaSAPInfo> skAyudaCuenta;
        private int usuarioId = 0;
        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        /// <summary>
        /// Constante para manejar el Costo default
        /// </summary>
        private List<TipoCostoInfo> listaTipoCosto;

        /// <summary>
        /// Control para la ayuda de Costo
        /// </summary>
        private SKAyuda<CostoInfo> skAyudaCosto;
        private StackPanel panelNuevoCosto;
        /// <summary>
        /// Variable para manejar los costos que solo levanta la ayuda
        /// </summary>
        private static readonly List<int> CostosValidosCaptura = new List<int> { 2, 3, 5, 6, 7, 4, 8 };

        #endregion VARIABLES



        #region PROPIEDADES
        /// <summary>
        /// Propiedad donde se almacena el DataContext de la funcionalidad
        /// </summary>
        private PremezclaDistribucionInfo PremezclaDistribucion
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PremezclaDistribucionInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
        #endregion PROPIEDADES

        #region METODOS


        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            PremezclaDistribucion = new PremezclaDistribucionInfo();
        }

        /// <summary>
        /// Cargar el Grid de Costo
        /// </summary>
        private void CargarGridCostos()
        {
            DgCostoDistribucion.ItemsSource = null;
            var listaObservable = PremezclaDistribucion.ListaPremezclaDistribucionCosto.ConvertirAObservable();
            DgCostoDistribucion.ItemsSource = listaObservable;
        }

        /// <summary>
        /// Método que obtiene los costos del grid y los pasa a una lista
        /// </summary>

        private List<PremezclaDistribucionCostoInfo> ObtenerCostosDeGrid()
        {
            var lineasCosto = DgCostoDistribucion.Items.Count;
            var listaCostos = new List<PremezclaDistribucionCostoInfo>();
            for (int i = 0; i < lineasCosto; i++)
            {
                var filaCosto = (PremezclaDistribucionCostoInfo)GetRow(i).Item;
                filaCosto.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                listaCostos.Add(filaCosto);
            }
            return listaCostos;
        }

        /// <summary>
        /// Método que valida la captura de costos del grid
        /// </summary>
        private bool ValidarCostosDeGrid()
        {
            var lineasCosto = DgCostoDistribucion.Items.Count;
            if (lineasCosto > 0)
            {
                for (int i = 0; i < lineasCosto; i++)
                {
                    var filaCosto = (PremezclaDistribucionCostoInfo)GetRow(i).Item;
                    if (!(filaCosto.Costo != null && filaCosto.Costo.CostoID > 0 && !String.IsNullOrEmpty(filaCosto.Costo.Descripcion)))
                        return false;
                    if (filaCosto.TieneCuenta)
                    {
                        if (!(filaCosto.CuentaSAP != null && filaCosto.CuentaSAP.CuentaSAPID > 0 &&
                            !String.IsNullOrEmpty(filaCosto.CuentaSAP.CuentaSAP))) return false;
                    }
                    else
                    {
                        if (!(filaCosto.Proveedor != null && filaCosto.Proveedor.ProveedorID > 0 &&
                            !String.IsNullOrEmpty(filaCosto.Proveedor.Descripcion))) return false;
                    }
                    if (filaCosto.Importe <= 0) return false;
                }
            }
            return true;
        }

        private void CargarAyudaPremezclas()
        {
            var productoInfo = new ProductoInfo
            {
                FamiliaId = FamiliasEnum.Premezclas.GetHashCode(),
                SubfamiliaId = SubFamiliasEnum.SubProductos.GetHashCode(),
                SubFamilia = new SubFamiliaInfo() { SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode() },
                Activo = EstatusEnum.Activo
            };
            skAyudaPremezcla = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadClaveConfiguracionPremezclas"
                                                   , "PropiedadDescripcionConfiguracionPremezclas",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProducto_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProducto_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaProducto_Busqueda,
                MensajeAgregar = Properties.Resources.Producto_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProducto,
                TituloPantalla = Properties.Resources.BusquedaProdcuto_Titulo,
            };

            skAyudaPremezcla.ObtenerDatos += ObtenerOrganizaciones;
            skAyudaPremezcla.AsignaTabIndex(0);
            SplAyudaPremezcla.Children.Clear();
            SplAyudaPremezcla.Children.Add(skAyudaPremezcla);
        }

        /// <summary>
        /// Agrega la Ayuda de Cuenta
        /// </summary>
        private void AgregarAyudaCuenta(StackPanel stackPanel)
        {
            skAyudaCuenta = new SKAyuda<CuentaSAPInfo>(160, false, 
                new CuentaSAPInfo()
                {
                    ListaTiposCuenta = new List<TipoCuentaInfo>
                    {
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() }, 
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode() },
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode() },
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode() },
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode() }
                    },
                    Activo = EstatusEnum.Activo
                }
                , "PropiedadClaveCostoDistribucion"
                , "PropiedadDescripcionCostoDistribucion"
                , "PropiedadOcultaCostoDistribucion"
                , true, 80, 10, true)
            {
                AyudaPL = new CuentaSAPPL(),
                MensajeClaveInexistente = Properties.Resources.Cuenta_CodigoInvalido,
                MensajeAgregar = Properties.Resources.Cuenta_Seleccionar,
                MensajeBusqueda = Properties.Resources.Cuenta_Busqueda,
                MensajeBusquedaCerrar = Properties.Resources.Cuenta_SalirSinSeleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCuenta,
                TituloPantalla = Properties.Resources.BusquedaCuenta_Titulo
            };
            skAyudaCuenta.ObtenerDatos = ObtenerDatosCuenta;
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCuenta);
            
        }

        /// <summary>
        /// Onbiene los datos que se muestran con el folio obtenido
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCuenta(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                var cuentaSapPl = new CuentaSAPPL();
                var cuentaSAP = new CuentaSAPInfo()
                {
                    ListaTiposCuenta = new List<TipoCuentaInfo>
                    {
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Provision.GetHashCode()}
                    },
                    Activo = EstatusEnum.Activo
                };
                cuentaSAP = cuentaSapPl.ObtenerPorFiltro(cuentaSAP);
                if (cuentaSAP == null)
                {
                    return;
                }
                skAyudaCuenta.Info = cuentaSAP;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene un Renglon en base al Indice del mismo
        /// </summary>
        /// <param name="index">Indice del Renglon a Buscar</param>
        /// <returns>DataGridRow</returns>
        public DataGridRow GetRow(int index)
        {
            var row = (DataGridRow)DgCostoDistribucion.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                DgCostoDistribucion.UpdateLayout();
                DgCostoDistribucion.ScrollIntoView(DgCostoDistribucion.Items[index]);
                row = (DataGridRow)DgCostoDistribucion.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }
        /// <summary>
        /// Obtiene un Renglon en base al Indice del mismo
        /// </summary>
        /// <param name="row">Indice del Renglon a Buscar</param>
        /// <param name="column">Indice de la Columna a Buscar</param>
        /// <returns>DataGridCell</returns>
        public DataGridCell GetCell(int row, int column)
        {
            var rowContainer = GetRow(row);
            if (rowContainer != null)
            {
                var presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    DgCostoDistribucion.ScrollIntoView(rowContainer, DgCostoDistribucion.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }
        /// <summary>
        /// Obtiene el Control Hijo del tipo que se especifica
        /// </summary>
        /// <param name="parent">Control donde se buscará el Tipo de Control especificado</param>
        /// <returns>T</returns>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            var child = default(T);
            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        /// Método que obtiene las organizaciones que tiene asignadas el producto
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerOrganizaciones(string clave)
        {
            try
            {
                DataGridOrganizaciones.ItemsSource = null;
                listaDistribucionOrganizacion.Clear();
                TxtCantidadExistente.Value = 0;
                TxtCostoUnitario.Value = 0;

                int productoId = int.Parse(clave);

                var productoInfo = new ProductoInfo
                {
                    FamiliaId = FamiliasEnum.Premezclas.GetHashCode(),
                    SubfamiliaId = SubFamiliasEnum.SubProductos.GetHashCode(),
                    SubFamilia = new SubFamiliaInfo() { SubFamiliaID = SubFamiliasEnum.SubProductos.GetHashCode() },
                    Activo = EstatusEnum.Activo
                };
                skAyudaPremezcla.Info = productoInfo;

                var organizacionPl = new OrganizacionPL();
                List<OrganizacionInfo> listaOrganizaciones = organizacionPl.ObtenerOrganizacionesProductoPremezcla(productoId);
                var inventarioLotePl = new AlmacenInventarioLotePL();

                if (listaOrganizaciones != null)
                {
                    var usuarioEsCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                    var organzacionUsuario = AuxConfiguracion.ObtenerOrganizacionUsuario();
                    //Si el usuario no es corporativo se filtran solo la organizacion del usuario
                    if (!usuarioEsCorporativo)
                    {
                        listaOrganizaciones = listaOrganizaciones.Where(lo => lo.OrganizacionID == organzacionUsuario).ToList();
                    }
                    

                    foreach (OrganizacionInfo organizacioPremezcla in listaOrganizaciones)
                    {
                        var distribucion = new DistribucionDeIngredientesOrganizacionInfo();
                        distribucion.Organizacion = organizacioPremezcla;
                        distribucion.Lote = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = 0, Lote = 0 };
                        distribucion.LotesOrganizacion = inventarioLotePl
                            .ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto
                            (new ParametrosOrganizacionTipoAlmacenProductoActivo
                            {
                                Activo = 1,
                                OrganizacionId = distribucion.Organizacion.OrganizacionID,
                                ProductoId = productoId,
                                TipoAlmacenId = (int)TipoAlmacenEnum.MateriasPrimas
                            }) ?? new List<AlmacenInventarioLoteInfo>();

                        distribucion.LotesOrganizacion.Insert(0,
                            new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = 0, Lote = 0 });

                        listaDistribucionOrganizacion.Add(distribucion);
                    }

                    DataGridOrganizaciones.ItemsSource = listaDistribucionOrganizacion;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PremezclasDistribucionIngredientes_MsgNoCuentaConOrganizaciones, MessageBoxButton.OK, MessageImage.Warning);
                    skAyudaPremezcla.LimpiarCampos();
                    skAyudaPremezcla.AsignarFoco();
                }

                #region Costos
                //PremezclaDistribucion.ListaPremezclaDistribucionCosto.Clear();
                //var premezclaDistribucionPl = new PremezclaDistribucionPL();


                //List<PremezclaDistribucionCostoInfo> listaDistribucionCostoInfo = premezclaDistribucionPl.ObtenerPremezclaDistribucionCosto(productoId);
                ////var inventarioLotePl = new AlmacenInventarioLotePL();

                //if (listaDistribucionCostoInfo != null)
                //{
                //    foreach (PremezclaDistribucionCostoInfo PremezclaDistribucionCosto in listaDistribucionCostoInfo)
                //    {
                //        PremezclaDistribucion.ListaPremezclaDistribucionCosto.Add(PremezclaDistribucionCosto);
                //    }

                //    //DgCostoDistribucion.ItemsSource = PremezclaDistribucion.ListaPremezclaDistribucionCosto;
                //}
                //else
                //{
                //    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PremezclasDistribucionIngredientes_MsgNoCuentaConOrganizaciones, MessageBoxButton.OK, MessageImage.Warning);
                //    skAyudaPremezcla.LimpiarCampos();
                //    skAyudaPremezcla.AsignarFoco();
                //}

                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.PremezclasDistribucionIngredientes_MsgErrorAlConsultarLasOrganizaciones, MessageBoxButton.OK, MessageImage.Warning);
                skAyudaPremezcla.LimpiarCampos();
                skAyudaPremezcla.AsignarFoco();
            }
        }
        /// <summary>
        /// Metodo para agregar el Control Ayuda Proveedor
        /// </summary>
        private void AgregarAyudaProveedor(StackPanel stackPanel)
        {
            skAyudaProveedor = new SKAyuda<ProveedorInfo>(160, false, new ProveedorInfo()
                                                        , "PropiedadClaveCosteoGanado"
                                                        , "PropiedadDescripcionCosteoGanado"
                                                        , "PropiedadOcultaCosteo"
                                                        , true, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Proveedor_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            skAyudaProveedor.Info = new ProveedorInfo();
            //skAyudaProveedor.ObtenerDatos += GenerarComisionAutomatica;
            skAyudaProveedor.LimpiarCampos();
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);
        }

        /// <summary>
        /// Carga los datos para la ayuda del proveedor
        /// </summary>
        private void CargarAyudaProveedor()
        {
            var proveedorInfo = new ProveedorInfo
            {
                Activo = EstatusEnum.Activo
            };
            skAyudaProveedores = new SKAyuda<ProveedorInfo>(200, false, proveedorInfo
                                                   , "PropiedadClavePremezclaDistribucionIngredientes"
                                                   , "PropiedadDescripcionPremezclaDistribucionIngredientes",
                                                   "", false, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.PremezclasDistribucionIngredientes_AyudaProveedorNoExiste,
                MensajeBusquedaCerrar = Properties.Resources.PremezclasDistribucionIngredientes_AyudaProveedorSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.PremezclasDistribucionIngredientes_AyudaProveedorSeleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            skAyudaProveedores.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaProveedores.AsignaTabIndex(1);
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaProveedores);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProveedor(string clave)
        {
            try
            {
                skAyudaProveedores.Info = new ProveedorInfo
                {
                    Activo = EstatusEnum.Activo
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Inicializa los datos para empezar la captura
        /// </summary>
        private void InicializarDatos()
        {
            FechaPL fechaPl = new FechaPL();
            var fecha = fechaPl.ObtenerFechaActual();
            TxtFechaEntrada.Text = fecha.FechaActual.ToShortDateString().ToString(CultureInfo.InvariantCulture);
            TxtCantidadExistente.Value = 0;
            TxtCostoUnitario.Value = 0;
            TxtSumaCostoTotal.Value = 0;
            TxtSumaCantidadSurtir.Value = 0;
            DataGridOrganizaciones.ItemsSource = null;
            HabilitarControles(true);
            skAyudaProveedores.LimpiarCampos();
            ckbSalida.IsChecked = false;
            skAyudaPremezcla.LimpiarCampos();
            skAyudaPremezcla.AsignaTabIndex(0);
            skAyudaPremezcla.AsignarFoco();
            DgCostoDistribucion.ItemsSource = null;
            PremezclaDistribucion.ListaPremezclaDistribucionCosto.Clear();
        }
        /// <summary>
        /// Habilita la cantidad existente y costo unitario
        /// </summary>
        /// <param name="habilitar"></param>
        private void HabilitarControles(bool habilitar)
        {
            TxtCantidadExistente.IsEnabled = habilitar;
            TxtCostoUnitario.IsEnabled = habilitar;
            skAyudaPremezcla.IsEnabled = habilitar;
        }

        /// <summary>
        /// Suma los totales de los costos
        /// </summary>
        private void SumarTotales()
        {
            TxtSumaCantidadSurtir.Value = listaDistribucionOrganizacion.Sum(x => x.CantidadSurtir);
            TxtSumaCostoTotal.Value = listaDistribucionOrganizacion.Sum(x => x.CostoTotal);
        }
        /// <summary>
        /// Resta la cantidad existente que se tecleo en el grid
        /// </summary>
        private void RestarCantidadExistente()
        {
            try
            {
                int cantidadExistente = 0, cantidadCostoTotal = 0, restaCantidades = 0;

                cantidadExistente = int.Parse(TxtCantidadExistenteAuxiliar.Value.ToString());
                cantidadCostoTotal = int.Parse(TxtSumaCantidadSurtir.Value.ToString());

                restaCantidades = cantidadExistente - cantidadCostoTotal;
                TxtCantidadExistente.Value = restaCantidades;
                foreach (var distribucion in listaDistribucionOrganizacion)
                {
                    distribucion.CantidadExistente = int.Parse(TxtCantidadExistente.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Valida lo capturado en el grid, cantidades lotes
        /// </summary>
        /// <returns></returns>
        private bool ValidarCantidadesGrid()
        {
            int contadorCapturados = 0;

            foreach (DistribucionDeIngredientesOrganizacionInfo distribucionDeIngredientesOrganizacionInfo in listaDistribucionOrganizacion)
            {
                if (distribucionDeIngredientesOrganizacionInfo.CantidadSurtir > 0 &&
                    distribucionDeIngredientesOrganizacionInfo.Lote.AlmacenInventarioLoteId > 0)
                {
                    contadorCapturados++;
                }

                if (distribucionDeIngredientesOrganizacionInfo.CantidadSurtir > 0 &&
                    distribucionDeIngredientesOrganizacionInfo.Lote.AlmacenInventarioLoteId == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    String.Format(Properties.Resources.PremezclasDistribucionIngredientes_MsgFavorDeSeleccionarLote,
                    distribucionDeIngredientesOrganizacionInfo.Organizacion.Descripcion), MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (distribucionDeIngredientesOrganizacionInfo.CantidadSurtir == 0 &&
                    distribucionDeIngredientesOrganizacionInfo.Lote.AlmacenInventarioLoteId != 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    String.Format(Properties.Resources.PremezclasDistribucionIngredientes_MsgFavorDeCapturarCantidad,
                    distribucionDeIngredientesOrganizacionInfo.Organizacion.Descripcion), MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }

            if (skAyudaPremezcla.Clave == "")
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                   Properties.Resources.PremezclasDistribucionIngredientes_MsgNoHasSeleccionadoProducto, MessageBoxButton.OK, MessageImage.Warning);
                skAyudaPremezcla.AsignarFoco();
                return false;
            }

            if (String.IsNullOrEmpty(skAyudaProveedores.Clave.Trim()) && String.IsNullOrEmpty(skAyudaProveedores.Descripcion.Trim()))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_ValidarProveedor, MessageBoxButton.OK, MessageImage.Warning);
                skAyudaProveedores.AsignarFoco();
                return false;
            }

            if (TxtCantidadExistenteAuxiliar.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_MsgNoHasCapturadoCantidadExistente, MessageBoxButton.OK, MessageImage.Warning);
                TxtCantidadExistente.Focus();
                return false;
            }

            if (TxtCostoUnitario.Value == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_MsgNoHasCapturadoCostoUnitario, MessageBoxButton.OK, MessageImage.Warning);
                TxtCostoUnitario.Focus();
                return false;
            }

            if (TxtCantidadExistenteAuxiliar.Value > 0 && TxtCantidadExistente.Value > 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_MsgDebeCubrirLaCantidadExistente, MessageBoxButton.OK, MessageImage.Warning);
                TxtCantidadExistente.Focus();
                return false;
            }

            if (contadorCapturados == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_MsgTienesQueCapturarUnaOrganizacion, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Agrega control de Ayuda para Costo
        /// </summary>
        private void AgregarAyudaCosto(StackPanel stackPanel)
        {
            var listaTipoCostos = CostosValidosCaptura.Select(costoID => new TipoCostoInfo {TipoCostoID = costoID}).ToList();
            skAyudaCosto = new SKAyuda<CostoInfo>
                (
                250,
                false, 
                new CostoInfo{ ListaTipoCostos = listaTipoCostos },
                "PropiedadClaveCosteoPremezcla",
                "PropiedadDescripcionCosteoPremezcla",
                "PropiedadOcultaCosteoPremezcla",
                true,
                true
                )
            {

                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_CostoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };

            skAyudaCosto.ObtenerDatos += ObtenerDatosCostos;
            skAyudaCosto.AsignaTabIndex(0);
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCosto);

        }

        /// <summary>
        /// Onbiene los datos que se muestran con el folio obtenido
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCostos(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                var costoPl = new CostoPL();
                var costo = new CostoInfo { CostoID = int.Parse(clave) };
                costo = costoPl.ObtenerCostoPorID(costo);
                costo.ListaTipoCostos = CostosValidosCaptura.Select(costoID => new TipoCostoInfo { TipoCostoID = costoID }).ToList();
                if (costo == null)
                {
                    return;
                }
                skAyudaCosto.Info = costo;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void CargarListaTipoCosto()
        {
            var listaTipoCostos = (from costosValidos in CostosValidosCaptura
                                   select new TipoCostoInfo
                                   {
                                       TipoCostoID = costosValidos
                                   }).ToList();
            listaTipoCosto = listaTipoCostos;
        }

        #endregion METODOS

        #region EVENTOS
        /// <summary>
        /// Inicializador de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InicializarDatos();
            CargarListaTipoCosto();
        }
        /// <summary>
        /// Evento que se ejecuta cuando el Control es creado
        /// </summary>
        private void stpAyudaProveedorCuenta_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stackPanel = (StackPanel)sender;
                if (stackPanel.Children.Count == 0)
                {
                    AgregarAyudaProveedor(stackPanel);
                    panelNuevoCosto = stackPanel;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Eliminar del Grid Costo de Entrada
        /// </summary>
        private void btnEliminarCostoGanado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var elementoBorrar = (PremezclaDistribucionCostoInfo)btn.CommandParameter;
                PremezclaDistribucion.ListaPremezclaDistribucionCosto.Remove(elementoBorrar);
                CargarGridCostos();

                if (PremezclaDistribucion.ListaPremezclaDistribucionCosto != null)
                {
                    switch (PremezclaDistribucion.ListaPremezclaDistribucionCosto.Count)
                    {
                        case 0:
                            btnCancelarCostoGanado.IsEnabled = false;
                            break;
                        default:
                            DgCostoDistribucion.ScrollIntoView(
                                PremezclaDistribucion.ListaPremezclaDistribucionCosto[
                                    PremezclaDistribucion.ListaPremezclaDistribucionCosto.Count - 1]);
                            break;
                    }
                }
                else
                {
                    btnCancelarCostoGanado.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.MensajeCancelarPantalla,
                MessageBoxButton.YesNo, MessageImage.Question) == MessageBoxResult.Yes)
            {
                InicializarDatos();
            }
        }
        /// <summary>
        /// Evento que se ejecuta cuando el Valor del CheckBox cambia
        /// </summary>
        private void chkCuenta_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;

                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var costo = (PremezclaDistribucionCostoInfo)checkBox.CommandParameter;
                var rowIndex = DgCostoDistribucion.Items.IndexOf(costo);
                var cell = GetCell(rowIndex, 2);
                
                //Si la ayuda no cuenta con una descripcion inicializa el info de la cuenta SAP
                if (costo.TieneCuenta && costo.CuentaSAP != null && String.IsNullOrEmpty(costo.CuentaSAP.Descripcion))
                {
                    costo.CuentaSAP = new CuentaSAPInfo
                    {
                        ListaTiposCuenta = new List<TipoCuentaInfo>
                    {
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                        new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Provision.GetHashCode()}
                    },
                        Activo = EstatusEnum.Activo

                    };    
                }
                
                var cellTextImporte = GetCell(rowIndex, 3);
                var textBox = GetVisualChild<TextBox>(cellTextImporte);
                var stackPanel = GetVisualChild<StackPanel>(cell);
                if (costo.TieneCuenta)
                {
                    costo.EditarIvaRetencion = false;
                    costo.Iva = false;
                    costo.Retencion = false;
                    AgregarAyudaCuenta(stackPanel);

                }
                else
                {
                    costo.EditarIvaRetencion = true;
                    costo.Iva = false;
                    costo.Retencion = false;
                    costo.Proveedor = null;
                    AgregarAyudaProveedor(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se activa cuando se modifica el combo de lotes para asignarselo a la organizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox) sender;
            var distribucion = (DistribucionDeIngredientesOrganizacionInfo) DataGridOrganizaciones.SelectedItem;
            distribucion.Lote = (AlmacenInventarioLoteInfo)combo.SelectedItem;
        }
        /// <summary>
        /// Evento que se activa cuando se presiona el boton de crear nuevo lote.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregarLote_OnClick(object sender, RoutedEventArgs e)
        {
            var botonAgregarLote = (Button)e.Source;
            try
            {
                var distribucion = (DistribucionDeIngredientesOrganizacionInfo)botonAgregarLote.CommandParameter;

                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                var almacenPl = new AlmacenPL();

                var listaTipoAlmacen = new List<TipoAlmacenEnum>();
                listaTipoAlmacen.Add(TipoAlmacenEnum.MateriasPrimas);

                var almacenMateriaPrima =
                    almacenPl.ObtenerAlmacenPorTiposAlmacen(listaTipoAlmacen, new OrganizacionInfo{OrganizacionID = distribucion.Organizacion.OrganizacionID});

                if (almacenMateriaPrima != null)
                {
                    var loteinfo =
                        almacenInventarioLotePl.CrearLotePorOrganizacionTipoAlmacenProducto(new ParametrosOrganizacionTipoAlmacenProductoActivo
                        {
                            Activo = (int) EstatusEnum.Activo,
                            ProductoId = int.Parse(skAyudaPremezcla.Clave),
                            OrganizacionId = distribucion.Organizacion.OrganizacionID,
                            TipoAlmacenId = (int) TipoAlmacenEnum.MateriasPrimas,
                            UsuarioId = usuarioId
                        });

                    if (loteinfo != null)
                    {
                        var inventarioLotePl = new AlmacenInventarioLotePL();
                        distribucion.LotesOrganizacion = inventarioLotePl
                            .ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto
                            (new ParametrosOrganizacionTipoAlmacenProductoActivo
                            {
                                Activo = 1,
                                OrganizacionId = distribucion.Organizacion.OrganizacionID,
                                ProductoId = int.Parse(skAyudaPremezcla.Clave),
                                TipoAlmacenId = (int) TipoAlmacenEnum.MateriasPrimas
                            }) ?? new List<AlmacenInventarioLoteInfo>();

                        distribucion.LotesOrganizacion.Insert(0,
                            new AlmacenInventarioLoteInfo {AlmacenInventarioLoteId = 0, Lote = 0});

                        distribucion.Lote = loteinfo;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.PremezclasDistribucionIngredientes_MsgErrorAlGenerarLote,
                            MessageBoxButton.OK, MessageImage.Error);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.MensajeLaOrganizacionNoCuentaConALmacenMateriaPrima,
                        MessageBoxButton.OK, MessageImage.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_MsgErrorAlGenerarLote, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento que se activa cuando se pierde el foco del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCantidadSurtir_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                SumarTotales();
                RestarCantidadExistente();

                var distribucionSeleccionada = (DistribucionDeIngredientesOrganizacionInfo)DataGridOrganizaciones.SelectedItem;
                if (distribucionSeleccionada.CantidadNueva != distribucionSeleccionada.CantidadSurtir)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.PremezclasDistribucionIngredientes_MsgCantidadMayorAExistente, MessageBoxButton.OK, MessageImage.Warning);

                    distribucionSeleccionada.CantidadNueva = distribucionSeleccionada.CantidadSurtir;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento que se activa cuando se pierde el foco del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCostoUnitario_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var distribucion in listaDistribucionOrganizacion)
                {
                    distribucion.CostoUnitario = decimal.Parse(TxtCostoUnitario.Value.ToString());
                }
                SumarTotales();
                if (TxtCantidadExistente.Value > 0 && TxtCostoUnitario.Value > 0)
                {
                    foreach (var distribucion in listaDistribucionOrganizacion)
                    {
                        distribucion.Habilitado = true;
                    }
                    HabilitarControles(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento al presionar el boton guardar, este guarda la distribucion de ingredientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarCantidadesGrid())
                {
                    if (!ValidarCostosDeGrid())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                          Properties.Resources.PremezclaDistribucion_CostosPendietes, MessageBoxButton.OK,
                          MessageImage.Warning);
                        return;
                    }
                    distribucionDeIngredientes.Iva = ckbSalida.IsChecked == true ? IvaPremezclaDistribucionIngredientesEnum.LlevaIva.GetHashCode() : IvaPremezclaDistribucionIngredientesEnum.NoLlevaIva.GetHashCode();
                    distribucionDeIngredientes.CantidadExistente = int.Parse(TxtCantidadExistente.Value.ToString());
                    distribucionDeIngredientes.CantidadTotal = int.Parse(TxtSumaCantidadSurtir.Value.ToString());
                    distribucionDeIngredientes.CostoTotal = decimal.Parse(TxtSumaCostoTotal.Value.ToString());
                    distribucionDeIngredientes.CostoUnitario = decimal.Parse(TxtCostoUnitario.Value.ToString());
                    distribucionDeIngredientes.ListaOrganizaciones = listaDistribucionOrganizacion;
                    distribucionDeIngredientes.UsuarioId = usuarioId;
                    //distribucionDeIngredientes.ListaPremezclaDistribucionCosto = 

                    var productoPl = new ProductoPL();
                    distribucionDeIngredientes.Producto =
                        productoPl.ObtenerPorID(new ProductoInfo { ProductoId = int.Parse(skAyudaPremezcla.Clave) });

                    var proveedorPl = new ProveedorPL();
                    distribucionDeIngredientes.Proveedor = proveedorPl.ObtenerPorCodigoSAP(new ProveedorInfo(){CodigoSAP = skAyudaProveedores.Clave});

                    //METODO PARA OBTENER LA LISTA DE COSTOS AGREGADOS
                    distribucionDeIngredientes.ListaPremezclaDistribucionCosto = ObtenerCostosDeGrid();
                    //

                    var premezclaDistribucionPl = new PremezclaDistribucionPL();
                    var distribucionCreada = premezclaDistribucionPl.GuardarPremezclaDistribucion(distribucionDeIngredientes);



                    if (distribucionCreada != null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MensajeDatosGuardadosConExito,
                            MessageBoxButton.OK, MessageImage.Correct);
                        InicializarDatos();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MensajeErrorAlGuardar,
                            MessageBoxButton.OK, MessageImage.Error);
                    }
                }
            }
            catch(ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message,
                                  MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MensajeErrorAlGuardar,
                            MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando el Control es creado
        /// </summary>
        private void stpAyudaCosto_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stackPanel = (StackPanel)sender;

                if (stackPanel.Children.Count == 0)
                {
                    AgregarAyudaCosto(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se activa cuadno se pierde el foco del control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCantidadExistente_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var distribucion in listaDistribucionOrganizacion)
                {
                    distribucion.CantidadExistente = int.Parse(TxtCantidadExistente.Value.ToString());
                }
                TxtCantidadExistenteAuxiliar.Value = TxtCantidadExistente.Value;

                if (TxtCantidadExistente.Value > 0 && TxtCostoUnitario.Value > 0)
                {
                    foreach (var distribucion in listaDistribucionOrganizacion)
                    {
                        distribucion.Habilitado = true;
                    }
                    HabilitarControles(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento que se activa cuando el mouse entra en un control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCantidadSurtir_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            var txt = (TextBox)sender;
            txt.SelectAll();
        }
        /// <summary>
        /// Evento que entra el presionar una tecla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCantidadSurtir_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Evento que entra al presionar la tecla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridOrganizaciones_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Send(Key.Tab);
            }
        }

        /// <summary>
        /// Envia tecla
        /// </summary>
        /// <param name="key"></param>
        public static void Send(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    };
                    InputManager.Current.ProcessInput(e);

                }
            }
        }

        private void btnAgregarCostoDistribucionPremezcla_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Se obtiene la informacion del grid actual
                PremezclaDistribucion.ListaPremezclaDistribucionCosto = ObtenerCostosDeGrid();

                //Se crea el nuevo renglon
                var distribucionCosto = new PremezclaDistribucionCostoInfo
                {
                    Costo = new CostoInfo(),
                    Proveedor = new ProveedorInfo(),
                    CuentaSAP = new CuentaSAPInfo
                    {
                        ListaTiposCuenta = new List<TipoCuentaInfo>
                    {
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() }, 
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode() },
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode() },
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode() },
                        new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode() }
                    },
                        Activo = EstatusEnum.Activo
                        
                    },
                    CostoEmbarque = true,
                    EditarTieneCuenta = true,
                    EditarCuentaProveedor = true,
                    EditarIvaRetencion = true
                };
                PremezclaDistribucion.ListaPremezclaDistribucionCosto.Add(distribucionCosto);
                // Cargar el Grid de Costo con la lista observable
                CargarGridCostos();
                btnCancelarCostoGanado.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        private void btnCancelarCostoGanado_Click(object sender, RoutedEventArgs e)
        {
            DgCostoDistribucion.ItemsSource = null;
            PremezclaDistribucion.ListaPremezclaDistribucionCosto.Clear();
            btnCancelarCostoGanado.IsEnabled = false;
        }

        #endregion EVENTOS


    }
}
