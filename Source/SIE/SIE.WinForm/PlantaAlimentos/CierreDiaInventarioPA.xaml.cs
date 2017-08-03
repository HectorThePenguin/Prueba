using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para CierreDiaInventario.xaml
    /// </summary>
    public partial class CierreDiaInventarioPA
    {
        #region CONSTRUCTORES

        /// <summary>
        /// Constructor por default de la forma
        /// </summary>
        public CierreDiaInventarioPA()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTORES

        #region PROPIEDADES
        /// <summary>
        /// Contexto
        /// </summary>
        private ContextoCierreDiaInventarioModel Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContextoCierreDiaInventarioModel)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region VARIABLES

        /// <summary>
        /// Lista para manejar los tipos de Almacen que mostrará la pantalla
        /// </summary>
        private static readonly List<int> AlmacenesValidos = new List<int> { TipoAlmacenEnum.MateriasPrimas.GetHashCode(), TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode() };

        /// <summary>
        /// Lista que contiene todos los almacenes de una Organización
        /// </summary>
        private List<AlmacenesCierreDiaInventarioPAModel> almacenesOrganizacion;

        private int organizacionID;

        private List<CierreDiaInventarioPAInfo> listaProductos;

        private List<MermaSuperavitInfo> listaMermas;

        private const string ProductoMaiz = "MAIZ";

        private const string ProductoMelaza = "MELAZA";

        private List<int> subProductosValidos;

        private List<int> productosForraje;

        private List<int> productosSinPiezasPA = new List<int> { 109, 110 };

        #endregion VARIABLES

        #region EVENTOS

        private void OnCancelCommand(object sender, DataObjectEventArgs e)
        {
            e.CancelCommand();
        }

        /// <summary>
        /// Evento del boton Cancelar
        /// </summary>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_Cancelar, MessageBoxButton.YesNo,
                                                             MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LimpiarPantalla();
            }
        }

        /// <summary>
        /// Evento cuando carga la funcionalidad
        /// </summary>
        private void CierreDiaInventarioPA_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarAlmacenesOrganizacion();
                CargarComboAlmacenes();
                InicializaContexto();
                treeDatos.Items.Clear();
                GenerarTreeViewDefault();
                dpFecha.SelectedDate = DateTime.Now;
                CargarSubProductosValidos();
                CargarProductosForrajes();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_ErrorInicio, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento cuando se cambia el Almacen
        /// </summary>
        private void CboAlmacen_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboAlmacen.SelectedIndex > 0)
                {
                    ConsultarInformacionCierreDia();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento cuando cambia el inventario fisico
        /// </summary>
        private void iduInventarioFisico_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var elemento = (IntegerUpDown)sender;
                if (elemento == null)
                {
                    return;
                }
                var productoDetalle = (CierreDiaInventarioPADetalleInfo)elemento.DataContext;
                if (productoDetalle == null)
                {
                    return;
                }
                if (!productoDetalle.InventarioFisicoCaptura.HasValue || productoDetalle.InventarioTeorico == 0 || (productoDetalle.ManejaLote && productoDetalle.TamanioLote == 0))
                {
                    productoDetalle.PorcentajeLote = 0;
                    productoDetalle.PorcentajeMermaSuperavit = 0;
                    return;
                }
                productoDetalle.InventarioFisico = productoDetalle.InventarioFisicoCaptura.GetValueOrDefault();
                decimal porcentajeMermaSuperavit = (Convert.ToDecimal((productoDetalle.InventarioTeorico - productoDetalle.InventarioFisico)) /
                                                  productoDetalle.InventarioTeorico) * 100;

                productoDetalle.PorcentajeMermaSuperavit = Math.Round(porcentajeMermaSuperavit, 2);

                if (productoDetalle.Lote > 0)
                {
                    decimal porcentajeLote = Convert.ToDecimal(productoDetalle.InventarioFisico) / Convert.ToDecimal(productoDetalle.TamanioLote) * 100;
                    productoDetalle.PorcentajeLote = Math.Round(porcentajeLote, 2);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_ErrorCalcular, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Evento cuando cambia el inventario fisico
        /// </summary>
        private void iduPiezasFisicas_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var elemento = (IntegerUpDown)sender;
                if (elemento == null)
                {
                    return;
                }
                var productoDetalle = (CierreDiaInventarioPADetalleInfo)elemento.DataContext;
                if (productoDetalle == null)
                {
                    return;
                }
                if (productoDetalle.InventarioTeorico == 0 || (productoDetalle.ManejaLote && productoDetalle.TamanioLote == 0)
                    || productoDetalle.PiezasTeoricas == 0 || productoDetalle.PiezasFisicas == 0)
                {
                    productoDetalle.PorcentajeLote = 0;
                    productoDetalle.PorcentajeMermaSuperavit = 0;
                    productoDetalle.PiezasFisicas = 0;
                    return;
                }

                decimal pesoPromedio = Math.Round(productoDetalle.InventarioTeorico / (decimal)productoDetalle.PiezasTeoricas, 2);

                productoDetalle.InventarioFisicoCaptura = Convert.ToInt32(productoDetalle.PiezasFisicas * pesoPromedio);
                productoDetalle.InventarioFisico = productoDetalle.InventarioFisicoCaptura.Value;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_ErrorCalcular, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Evento del boton Guardar
        /// </summary>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarAntesGuardar())
                {
                    Guardar();
                }
            }
            catch (ExcepcionServicio ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }

        }


        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void CargarSubProductosValidos()
        {
            var parametroGeneralBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametro =
                parametroGeneralBL.ObtenerPorClaveParametro(ParametrosEnum.SubProductosCierreDiaPA.ToString());
            if (parametro == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.CierreDiaInventarioPA_ParametroGeneral, ParametrosEnum.SubProductosCierreDiaPA.ToString()),
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                BloquearAlmacen();
                return;
            }
            if (parametro.Valor.Contains('|'))
            {
                subProductosValidos = (from tipos in parametro.Valor.Split('|')
                                       select Convert.ToInt32(tipos)).ToList();
            }
            else
            {
                int subProducto = Convert.ToInt32(parametro.Valor);
                subProductosValidos.Add(subProducto);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void CargarProductosForrajes()
        {
            var parametroGeneralBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametro =
                parametroGeneralBL.ObtenerPorClaveParametro(ParametrosEnum.ProductosForraje.ToString());
            if (parametro == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.CierreDiaInventarioPA_ParametroGeneral, ParametrosEnum.SubProductosCierreDiaPA.ToString()),
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                BloquearAlmacen();
                return;
            }
            if (parametro.Valor.Contains('|'))
            {
                productosForraje = (from tipos in parametro.Valor.Split('|')
                                    select Convert.ToInt32(tipos)).ToList();
            }
            else
            {
                int forraje = Convert.ToInt32(parametro.Valor);
                productosForraje.Add(forraje);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ContextoCierreDiaInventarioModel
            {
                Almacen = new AlmacenInfo(),
                TipoAlmacen = new TipoAlmacenInfo()
            };
        }

        /// <summary>
        /// Carga el Combo de Almacenes
        /// </summary>
        private void CargarComboAlmacenes()
        {
            var almacenPL = new AlmacenPL();
            List<AlmacenesCierreDiaInventarioPAModel> listaAlmacen =
                almacenPL.ObtenerAlmacenesOrganizacion(organizacionID);
            List<AlmacenesCierreDiaInventarioPAModel> listaFiltrada =
                listaAlmacen.Where(tipo => AlmacenesValidos.Contains(tipo.TipoAlmacenID)).ToList();
            var almacenSeleccione = new AlmacenesCierreDiaInventarioPAModel
                {
                    AlmacenID = 0,
                    Almacen = Properties.Resources.cbo_Seleccione
                };
            listaFiltrada.Insert(0, almacenSeleccione);
            cboAlmacen.ItemsSource = listaFiltrada;
            cboAlmacen.SelectedIndex = 0;
        }

        /// <summary>
        /// Obtiene el Folio que le corresponden al Almacen
        /// </summary>
        private void ObtenerFolioAlmacen()
        {
            treeDatos.Items.Clear();
            GenerarTreeViewDefault();
            var almacenPL = new AlmacenPL();
            var almacenMovimientoPL = new AlmacenMovimientoPL();

            var almacen = (AlmacenesCierreDiaInventarioPAModel)cboAlmacen.SelectedItem;

            if (almacen == null)
            {
                return;
            }
            Contexto.Almacen = new AlmacenInfo
                {
                    AlmacenID = almacen.AlmacenID,
                    TipoAlmacenID = almacen.TipoAlmacenID
                };
            var filtros = new FiltroCierreDiaInventarioInfo
                {
                    AlmacenID = almacen.AlmacenID,
                    TipoMovimientoID = TipoMovimiento.DiferenciasDeInventario.GetHashCode()
                };
            if (almacenMovimientoPL.ValidarEjecucionCierreDia(Contexto.Almacen.AlmacenID))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_EjecucionCierreDia, MessageBoxButton.OK, MessageImage.Warning);
                cboAlmacen.SelectedIndex = 0;
                return;
            }
            int folio = almacenPL.ObtenerFolioAlmacenConsulta(filtros);
            iudFolio.Value = folio;
            GenerarTreeView();
            CargarMermasSuperavit();
        }

        /// <summary>
        /// Genera el TreeView sin datos
        /// </summary>
        private void GenerarTreeViewDefault()
        {
            StackPanel stack = GenerarEncabezado();
            var treeHeader = new TreeViewItem
            {
                Header = stack,
            };
            treeDatos.Items.Add(treeHeader);
        }

        /// <summary>
        /// Genera el TreeView con los valores de productos
        /// </summary>
        private void GenerarTreeView()
        {
            List<CierreDiaInventarioPAInfo> lista = ObtenerInformacion();

            if (lista == null || !lista.Any())
            {
                treeDatos.Items.Clear();
                GenerarTreeViewDefault();
                cboAlmacen.SelectedIndex = 0;
                iudFolio.ClearValue(IntegerUpDown.ValueProperty);
                return;
            }

            foreach (var producto in lista)
            {
                StackPanel stack = GenerarProducto(producto);
                var treeHeaderProducto = new TreeViewItem
                {
                    Header = stack,
                    Width = double.NaN
                };
                treeDatos.Items.Add(treeHeaderProducto);

                stack = GenerarEncabezadoSubGrid();
                var treeHeaderSubGrid = new TreeViewItem
                {
                    Header = stack,
                    Width = double.NaN
                };
                treeHeaderProducto.Items.Add(treeHeaderSubGrid);

                foreach (var detalle in producto.ListaCierreDiaInventarioPADetalle)
                {
                    stack = GenerarProductoDetalle(detalle);
                    var treeHeaderProductoDetalle = new TreeViewItem
                    {
                        Header = stack,
                        Width = double.NaN
                    };
                    treeHeaderProducto.Items.Add(treeHeaderProductoDetalle);
                }
                treeHeaderProducto.IsExpanded = false;
            }
        }
        /// <summary>
        /// Genera el StackPanel del cabecero del TreeView
        /// </summary>
        private StackPanel GenerarEncabezado()
        {
            var stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = double.NaN
            };
            var grdEncabezado = new Grid();
            var columnaProducto = new ColumnDefinition { Width = new GridLength(100) };
            var columnaDescripcion = new ColumnDefinition { Width = new GridLength(100) };
            var columnaUnidad = new ColumnDefinition { Width = new GridLength(100) };
            var columnaSubGrid1 = new ColumnDefinition { Width = new GridLength(100) };
            var columnaSubGrid2 = new ColumnDefinition { Width = new GridLength(100) };
            var columnaSubGrid3 = new ColumnDefinition { Width = new GridLength(100) };
            var columnaSubGrid4 = new ColumnDefinition { Width = new GridLength(500) };


            grdEncabezado.ColumnDefinitions.Add(columnaProducto);
            grdEncabezado.ColumnDefinitions.Add(columnaDescripcion);
            grdEncabezado.ColumnDefinitions.Add(columnaUnidad);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid1);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid2);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid3);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid4);


            grdEncabezado.RowDefinitions.Add(new RowDefinition());

            var lblProducto = new Label
            {
                Content = Properties.Resources.CierreDiaInventarioPA_Grid_Producto,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblProducto);
            Grid.SetColumn(lblProducto, 0);
            Grid.SetRow(lblProducto, 0);

            var lblDescripcion = new Label
            {
                Content = Properties.Resources.CierreDiaInventarioPA_Grid_Descripcion,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblDescripcion);
            Grid.SetColumn(lblDescripcion, 1);
            Grid.SetRow(lblDescripcion, 0);

            var lblUnidad = new Label
            {
                Content =
                    string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_Unidad),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblUnidad);
            Grid.SetColumn(lblUnidad, 2);
            Grid.SetRow(lblUnidad, 0);


            var lblSubGrid1 = new Label
            {
                Content = string.Empty,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblSubGrid1);
            Grid.SetColumn(lblSubGrid1, 3);
            Grid.SetRow(lblSubGrid1, 0);

            var lblSubGrid2 = new Label
            {
                Content = string.Empty,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblSubGrid2);
            Grid.SetColumn(lblSubGrid2, 4);
            Grid.SetRow(lblSubGrid2, 0);

            var lblSubGrid3 = new Label
            {
                Content = string.Empty,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblSubGrid3);
            Grid.SetColumn(lblSubGrid3, 5);
            Grid.SetRow(lblSubGrid3, 0);

            var lblSubGrid4 = new Label
            {
                Content = string.Empty,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblSubGrid4);
            Grid.SetColumn(lblSubGrid4, 6);
            Grid.SetRow(lblSubGrid4, 0);

            stack.Children.Add(grdEncabezado);

            return stack;
        }

        /// <summary>
        /// Genera el StackPanel del cabecero del SubGrid
        /// </summary>
        private StackPanel GenerarEncabezadoSubGrid()
        {
            var stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = double.NaN
            };
            var grdEncabezado = new Grid();
            var columnaLote = new ColumnDefinition { Width = new GridLength(60) };
            var columnaCostoUnitario = new ColumnDefinition { Width = new GridLength(100) };
            var columnaTamanioLote = new ColumnDefinition { Width = new GridLength(120) };
            var columnaInventarioTeorico = new ColumnDefinition { Width = new GridLength(150) };
            var columnaInventarioFisico = new ColumnDefinition { Width = new GridLength(150) };
            var columnaPiezasTeoricas = new ColumnDefinition { Width = new GridLength(150) };
            var columnaPiezasFisicas = new ColumnDefinition { Width = new GridLength(150) };
            var columnaMerma = new ColumnDefinition { Width = new GridLength(150) };
            var columnaPorcentajeLote = new ColumnDefinition { Width = new GridLength(120) };


            grdEncabezado.ColumnDefinitions.Add(columnaLote);
            grdEncabezado.ColumnDefinitions.Add(columnaCostoUnitario);
            grdEncabezado.ColumnDefinitions.Add(columnaTamanioLote);
            grdEncabezado.ColumnDefinitions.Add(columnaInventarioTeorico);
            grdEncabezado.ColumnDefinitions.Add(columnaInventarioFisico);
            grdEncabezado.ColumnDefinitions.Add(columnaPiezasTeoricas);
            grdEncabezado.ColumnDefinitions.Add(columnaPiezasFisicas);
            grdEncabezado.ColumnDefinitions.Add(columnaMerma);
            grdEncabezado.ColumnDefinitions.Add(columnaPorcentajeLote);


            grdEncabezado.RowDefinitions.Add(new RowDefinition());

            var lblLote = new Label
            {
                Content = Properties.Resources.CierreDiaInventarioPA_Grid_Lote,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblLote);
            Grid.SetColumn(lblLote, 0);
            Grid.SetRow(lblLote, 0);

            var lblCostoUnitario = new Label
            {
                Content = Properties.Resources.CierreDiaInventarioPA_Grid_CostoUnitario,
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblCostoUnitario);
            Grid.SetColumn(lblCostoUnitario, 1);
            Grid.SetRow(lblCostoUnitario, 0);

            var lblTamanioLote = new Label
            {
                Content =
                    string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_TamanioLote),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblTamanioLote);
            Grid.SetColumn(lblTamanioLote, 2);
            Grid.SetRow(lblTamanioLote, 0);


            var lblInventarioTeorico = new Label
            {
                Content = string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_InventarioTeorico),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblInventarioTeorico);
            Grid.SetColumn(lblInventarioTeorico, 3);
            Grid.SetRow(lblInventarioTeorico, 0);

            var lblInventarioFisico = new Label
            {
                Content = string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_InventarioFisico),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblInventarioFisico);
            Grid.SetColumn(lblInventarioFisico, 4);
            Grid.SetRow(lblInventarioFisico, 0);

            var lblPiezasTeoricas = new Label
            {
                Content = string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_PiezasTeoricas),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblPiezasTeoricas);
            Grid.SetColumn(lblPiezasTeoricas, 5);
            Grid.SetRow(lblPiezasTeoricas, 0);

            var lblPiezasFisicas = new Label
            {
                Content = string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_PiezasFisicas),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblPiezasFisicas);
            Grid.SetColumn(lblPiezasFisicas, 6);
            Grid.SetRow(lblPiezasFisicas, 0);

            var lblMerma = new Label
            {
                Content = string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_MermaSuperavit),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblMerma);
            Grid.SetColumn(lblMerma, 7);
            Grid.SetRow(lblMerma, 0);

            var lblPorcentajeLote = new Label
            {
                Content = string.Format("{0, 15}",
                                  Properties.Resources.CierreDiaInventarioPA_Grid_PorcentajeLote),
                Background =
                    (LinearGradientBrush)
                    Application.Current.FindResource("headerDataGridColor"),
                Foreground =
                    new LinearGradientBrush(Colors.White, Colors.White, 0),
                Style = (Style)Application.Current.FindResource("etiquetaBold")
            };
            grdEncabezado.Children.Add(lblPorcentajeLote);
            Grid.SetColumn(lblPorcentajeLote, 8);
            Grid.SetRow(lblPorcentajeLote, 0);

            stack.Children.Add(grdEncabezado);

            return stack;
        }

        /// <summary>
        /// Genera el StackPanel del Producto
        /// </summary>
        private StackPanel GenerarProducto(CierreDiaInventarioPAInfo producto)
        {
            var stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = double.NaN
            };
            var grdEncabezado = new Grid();
            var columnaProducto = new ColumnDefinition { Width = new GridLength(130) };
            var columnaDescripcion = new ColumnDefinition { Width = new GridLength(200) };
            var columnaUnidad = new ColumnDefinition { Width = new GridLength(130) };
            var columnaSubGrid1 = new ColumnDefinition { Width = new GridLength(130) };
            var columnaSubGrid2 = new ColumnDefinition { Width = new GridLength(130) };
            var columnaSubGrid3 = new ColumnDefinition { Width = new GridLength(130) };
            var columnaSubGrid4 = new ColumnDefinition { Width = new GridLength(130) };


            grdEncabezado.ColumnDefinitions.Add(columnaProducto);
            grdEncabezado.ColumnDefinitions.Add(columnaDescripcion);
            grdEncabezado.ColumnDefinitions.Add(columnaUnidad);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid1);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid2);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid3);
            grdEncabezado.ColumnDefinitions.Add(columnaSubGrid4);


            grdEncabezado.RowDefinitions.Add(new RowDefinition());

            var lblProducto = new Label
            {
                Content = producto.ProductoID,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            grdEncabezado.Children.Add(lblProducto);
            Grid.SetColumn(lblProducto, 0);
            Grid.SetRow(lblProducto, 0);

            var lblDescripcion = new Label
            {
                Content = producto.Producto
            };
            grdEncabezado.Children.Add(lblDescripcion);
            Grid.SetColumn(lblDescripcion, 1);
            Grid.SetRow(lblDescripcion, 0);

            var lblUnidad = new Label
            {
                Content = producto.UnidadMedicion
            };
            grdEncabezado.Children.Add(lblUnidad);
            Grid.SetColumn(lblUnidad, 2);
            Grid.SetRow(lblUnidad, 0);

            var lblSubGrid1 = new Label
            {
                Content = string.Empty
            };
            grdEncabezado.Children.Add(lblSubGrid1);
            Grid.SetColumn(lblSubGrid1, 3);
            Grid.SetRow(lblSubGrid1, 0);

            var lblSubGrid2 = new Label
            {
                Content = string.Empty
            };
            grdEncabezado.Children.Add(lblSubGrid2);
            Grid.SetColumn(lblSubGrid2, 4);
            Grid.SetRow(lblSubGrid2, 0);

            var lblSubGrid3 = new Label
            {
                Content = string.Empty
            };
            grdEncabezado.Children.Add(lblSubGrid3);
            Grid.SetColumn(lblSubGrid3, 5);
            Grid.SetRow(lblSubGrid3, 0);

            var lblSubGrid4 = new Label
            {
                Content = string.Empty
            };
            grdEncabezado.Children.Add(lblSubGrid4);
            Grid.SetColumn(lblSubGrid4, 6);
            Grid.SetRow(lblSubGrid4, 0);

            stack.Children.Add(grdEncabezado);

            return stack;
        }

        /// <summary>
        /// Genera el StackPanel del Producto Detalle (SubGrid)
        /// </summary>
        private StackPanel GenerarProductoDetalle(CierreDiaInventarioPADetalleInfo productoDetalle)
        {
            var stack = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = double.NaN
            };
            var grdEncabezado = new Grid();
            var columnaLote = new ColumnDefinition { Width = new GridLength(60) };
            var columnaCostoUnitario = new ColumnDefinition { Width = new GridLength(100) };
            var columnaTamanioLote = new ColumnDefinition { Width = new GridLength(120) };
            var columnaInventarioTeorico = new ColumnDefinition { Width = new GridLength(150) };
            var columnaInventarioFisico = new ColumnDefinition { Width = new GridLength(150) };
            var columnaPiezasTeoricas = new ColumnDefinition { Width = new GridLength(150) };
            var columnaPiezasFisicas = new ColumnDefinition { Width = new GridLength(150) };
            var columnaMermaSuperavit = new ColumnDefinition { Width = new GridLength(150) };
            var columnaPorcentajeLote = new ColumnDefinition { Width = new GridLength(120) };


            grdEncabezado.ColumnDefinitions.Add(columnaLote);
            grdEncabezado.ColumnDefinitions.Add(columnaCostoUnitario);
            grdEncabezado.ColumnDefinitions.Add(columnaTamanioLote);
            grdEncabezado.ColumnDefinitions.Add(columnaInventarioTeorico);
            grdEncabezado.ColumnDefinitions.Add(columnaInventarioFisico);
            grdEncabezado.ColumnDefinitions.Add(columnaPiezasTeoricas);
            grdEncabezado.ColumnDefinitions.Add(columnaPiezasFisicas);
            grdEncabezado.ColumnDefinitions.Add(columnaMermaSuperavit);
            grdEncabezado.ColumnDefinitions.Add(columnaPorcentajeLote);


            grdEncabezado.RowDefinitions.Add(new RowDefinition());

            var bindLote = new Binding("Lote")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblLote = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            lblLote.SetBinding(ContentProperty, bindLote);
            grdEncabezado.Children.Add(lblLote);
            Grid.SetColumn(lblLote, 0);
            Grid.SetRow(lblLote, 0);

            var bindCostoUnitario = new Binding("CostoUnitario")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblCostoUnitario = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            lblCostoUnitario.SetBinding(ContentProperty, bindCostoUnitario);
            grdEncabezado.Children.Add(lblCostoUnitario);
            Grid.SetColumn(lblCostoUnitario, 1);
            Grid.SetRow(lblCostoUnitario, 0);

            var bindTamanioLote = new Binding("TamanioLote")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblTamanioLote = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                ContentStringFormat = "N0"
            };
            lblTamanioLote.SetBinding(ContentProperty, bindTamanioLote);
            grdEncabezado.Children.Add(lblTamanioLote);
            Grid.SetColumn(lblTamanioLote, 2);
            Grid.SetRow(lblTamanioLote, 0);

            var bindInventarioTeorico = new Binding("InventarioTeorico")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblInventarioTeorico = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                ContentStringFormat = "N0"
            };
            lblInventarioTeorico.SetBinding(ContentProperty, bindInventarioTeorico);
            grdEncabezado.Children.Add(lblInventarioTeorico);
            Grid.SetColumn(lblInventarioTeorico, 3);
            Grid.SetRow(lblInventarioTeorico, 0);

            var bindInventarioFisico = new Binding("InventarioFisicoCaptura")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var iduInventarioFisico = new IntegerUpDown
                {
                    NumeroInteger = true,
                    AllowSpin = false,
                    ShowButtonSpinner = false,
                    DefaultValue = 0,
                    FormatString = "N0",
                    Width = 150,
                    MaxLength = 10,
                    DataContext = productoDetalle,
                };
            iduInventarioFisico.PreviewDrop += iduInventarioFisico_PreviewDrop;
            iduInventarioFisico.ValueChanged += iduInventarioFisico_ValueChanged;
            iduInventarioFisico.AllowDrop = false;
            DataObject.AddPastingHandler(iduInventarioFisico, OnCancelCommand);
            DataObject.AddCopyingHandler(iduInventarioFisico, OnCancelCommand);
            iduInventarioFisico.SetBinding(IntegerUpDown.ValueProperty, bindInventarioFisico);
            grdEncabezado.Children.Add(iduInventarioFisico);
            Grid.SetColumn(iduInventarioFisico, 4);
            Grid.SetRow(iduInventarioFisico, 0);

            var bindPiezasTeoricas = new Binding("PiezasTeoricas")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblPiezasTeoricas = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                ContentStringFormat = "N0"
            };
            lblPiezasTeoricas.SetBinding(ContentProperty, bindPiezasTeoricas);
            grdEncabezado.Children.Add(lblPiezasTeoricas);
            Grid.SetColumn(lblPiezasTeoricas, 5);
            Grid.SetRow(lblPiezasTeoricas, 0);

            var bindPiezasFisicas = new Binding("PiezasFisicas")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var iduPiezasFisicas = new IntegerUpDown
            {
                NumeroInteger = true,
                AllowSpin = false,
                ShowButtonSpinner = false,
                DefaultValue = 0,
                FormatString = "N0",
                Width = 150,
                MaxLength = 10,
                DataContext = productoDetalle,
            };
            iduPiezasFisicas.PreviewDrop += iduInventarioFisico_PreviewDrop;
            iduPiezasFisicas.ValueChanged += iduPiezasFisicas_ValueChanged;
            iduPiezasFisicas.AllowDrop = false;
            DataObject.AddPastingHandler(iduPiezasFisicas, OnCancelCommand);
            DataObject.AddCopyingHandler(iduPiezasFisicas, OnCancelCommand);
            iduPiezasFisicas.SetBinding(IntegerUpDown.ValueProperty, bindPiezasFisicas);
            grdEncabezado.Children.Add(iduPiezasFisicas);
            Grid.SetColumn(iduPiezasFisicas, 6);
            Grid.SetRow(iduPiezasFisicas, 0);

            var bindPorcentajeMermaSuperavit = new Binding("PorcentajeMermaSuperavit")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblMermaSuperavit = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            lblMermaSuperavit.SetBinding(ContentProperty, bindPorcentajeMermaSuperavit);
            grdEncabezado.Children.Add(lblMermaSuperavit);
            Grid.SetColumn(lblMermaSuperavit, 7);
            Grid.SetRow(lblMermaSuperavit, 0);

            var bindPorcentajeLote = new Binding("PorcentajeLote")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Source = productoDetalle
            };

            var lblPorcentajeLote = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };
            lblPorcentajeLote.SetBinding(ContentProperty, bindPorcentajeLote);
            grdEncabezado.Children.Add(lblPorcentajeLote);
            Grid.SetColumn(lblPorcentajeLote, 8);
            Grid.SetRow(lblPorcentajeLote, 0);

            stack.Children.Add(grdEncabezado);
            iduPiezasFisicas.IsEnabled = false;
            if (productoDetalle.SubFamiliaID == SubFamiliasEnum.MicroIngredientes.GetHashCode())
            {
                iduInventarioFisico.IsEnabled = false;
                iduPiezasFisicas.IsEnabled = true;
            }
            if (productoDetalle.SubFamiliaID == SubFamiliasEnum.Forrajes.GetHashCode())
            {
                int forrajePiezas = productosForraje.FirstOrDefault(pro => pro == productoDetalle.ProductoID);

                if (Contexto.Almacen.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode())
                {
                    int forrajeSinPiezas = productosSinPiezasPA.FirstOrDefault(pro => pro == forrajePiezas);
                    if (forrajeSinPiezas > 0)
                    {
                        iduInventarioFisico.IsEnabled = true;
                        iduPiezasFisicas.IsEnabled = false;
                    }
                }
                else
                {
                    if (forrajePiezas > 0)
                    {
                        iduInventarioFisico.IsEnabled = false;
                        iduPiezasFisicas.IsEnabled = true;
                    }
                }
            }

            return stack;
        }

        private void iduInventarioFisico_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        /// <summary>
        /// Obtiene la informacion del Cierre de Dia
        /// </summary>
        private List<CierreDiaInventarioPAInfo> ObtenerInformacion()
        {
            var almacenInventarioPL = new AlmacenInventarioPL();
            listaProductos =
                almacenInventarioPL.ObtenerDatosCierreDiaInventarioPlantaAlimentos(organizacionID,
                                                                                   Contexto.Almacen.AlmacenID);


            if (listaProductos == null || !listaProductos.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Contexto.Almacen.TipoAlmacenID == TipoAlmacenEnum.MateriasPrimas.GetHashCode()
                                      ? Properties.Resources.CierreDiaInventarioPA_SinProductosMateriaPrima
                                      : Properties.Resources.CierreDiaInventarioPA_SinProductosPlantaAlimento,
                                  MessageBoxButton.OK, MessageImage.Warning);
                return null;
            }

            var productosSubProductos =
                listaProductos.Where(pro => pro.SubFamiliaID == SubFamiliasEnum.SubProductos.GetHashCode()).ToList();
            if (productosSubProductos.Any())
            {
                var listaRemover = new List<CierreDiaInventarioPAInfo>();
                foreach (var subProducto in productosSubProductos)
                {
                    var subProductoValido = subProductosValidos.FirstOrDefault(pro => pro == subProducto.ProductoID);
                    if (subProductoValido == 0)
                    {
                        listaRemover.Add(subProducto);
                    }
                }
                if (listaRemover.Any())
                {
                    foreach (var remover in listaRemover)
                    {
                        listaProductos.Remove(remover);
                    }
                }
            }
            return listaProductos.ToList();
        }

        /// <summary>
        /// Cargar los almacenes de la Organizacion
        /// </summary>
        private void CargarAlmacenesOrganizacion()
        {
            organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            var almacenPL = new AlmacenPL();
            almacenesOrganizacion = almacenPL.ObtenerAlmacenesOrganizacion(organizacionID);

            if (almacenesOrganizacion == null || !almacenesOrganizacion.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_SinAlmacenMateriaPrima, MessageBoxButton.OK, MessageImage.Warning);
                BloquearAlmacen();
                return;
            }

            AlmacenesCierreDiaInventarioPAModel almacenMateriaPrima =
                almacenesOrganizacion.FirstOrDefault(
                    alm => alm.TipoAlmacenID == TipoAlmacenEnum.MateriasPrimas.GetHashCode());
            if (almacenMateriaPrima == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_SinAlmacenMateriaPrima, MessageBoxButton.OK, MessageImage.Warning);
                BloquearAlmacen();
                return;
            }
            AlmacenesCierreDiaInventarioPAModel almacenPlantaAlimentos =
                almacenesOrganizacion.FirstOrDefault(
                    alm => alm.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode());

            if (almacenPlantaAlimentos == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_SinAlmacenPlantaAlimentos, MessageBoxButton.OK, MessageImage.Warning);
                BloquearAlmacen();
            }

        }

        private void BloquearAlmacen()
        {
            cboAlmacen.IsEnabled = false;
        }

        /// <summary>
        /// Carga la informacion del Almacen seleccionado
        /// </summary>
        private void ConsultarInformacionCierreDia()
        {
            ObtenerFolioAlmacen();
        }

        /// <summary>
        /// Carga la configuracion de merma o superavit
        /// </summary>
        private void CargarMermasSuperavit()
        {
            var mermaSuperavitPL = new MermaSuperavitPL();
            listaMermas = mermaSuperavitPL.ObtenerPorAlmacenID(Contexto.Almacen.AlmacenID);
        }

        /// <summary>
        /// Guarda la informacion del Cierre de Dia de inventario PA
        /// </summary>
        private void Guardar()
        {
            int usuarioID = AuxConfiguracion.ObtenerUsuarioLogueado();

            List<CierreDiaInventarioPADetalleInfo> listaModificada =
                listaProductos.SelectMany(
                    prod => prod.ListaCierreDiaInventarioPADetalle.Where(det => det.InventarioFisicoCaptura.HasValue)).ToList();


            var listaProductosMensaje = new List<CierreDiaInventarioPAMensajesModel>();

            if (!listaModificada.Any())
            {
                return;
            }
            foreach (var elemento in listaModificada)
            {
                if (listaMermas == null || !listaMermas.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CierreDiaInventarioPA_SinConfiguracionMerma,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                MermaSuperavitInfo merma =
                    listaMermas.FirstOrDefault(mer => mer.Producto.ProductoId == elemento.ProductoID);
                if (merma == null)
                {
                    merma = new MermaSuperavitInfo
                                {
                                    Merma = 0,
                                    Superavit = 0
                                };
                }
                bool esMerma = elemento.PorcentajeMermaSuperavit > 0;
                decimal porcentaje = Math.Abs(elemento.PorcentajeMermaSuperavit);
                elemento.RequiereAutorizacion = false;
                if (esMerma)
                {
                    if (porcentaje > merma.Merma)
                    {
                        if (Contexto.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode() &&
                            !ValidarProductosSinMerma(elemento.Producto))
                        {
                            return;
                        }
                        listaProductosMensaje.Add(new CierreDiaInventarioPAMensajesModel
                                                      {
                                                          EsAutorizacion = true,
                                                          Producto = elemento.Producto,
                                                          MermaSuperavit = porcentaje,
                                                          Permitido = merma.Merma
                                                      });
                        elemento.RequiereAutorizacion = true;
                    }
                }
                else
                {
                    if (porcentaje > merma.Superavit)
                    {
                        listaProductosMensaje.Add(new CierreDiaInventarioPAMensajesModel
                                                      {
                                                          EsAutorizacion = true,
                                                          Producto = elemento.Producto,
                                                          MermaSuperavit = elemento.PorcentajeMermaSuperavit,
                                                          Permitido = merma.Merma
                                                      });
                        elemento.RequiereAutorizacion = true;
                    }
                }
            }

            if (listaProductosMensaje.Any())
            {
                var elementoMensaje = listaProductosMensaje.FirstOrDefault();
                if (elementoMensaje != null && !elementoMensaje.EsAutorizacion)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CierreDiaInventarioPA_SinProductosSinConfiguracion,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                    var cierreDiaInventarioPAMensajes = new CierreDiaInventarioPAMensajes(listaProductosMensaje);
                    MostrarCentrado(cierreDiaInventarioPAMensajes);

                    return;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CierreDiaInventarioPA_AutorizacionProductos,
                                      MessageBoxButton.OK,
                                      MessageImage.Warning);
                    var cierreDiaInventarioPAMensajes = new CierreDiaInventarioPAMensajes(listaProductosMensaje);
                    MostrarCentrado(cierreDiaInventarioPAMensajes);
                }
            }

            var cierreDiaInventarioPA = new CierreDiaInventarioPAInfo
                                            {
                                                FolioMovimiento = iudFolio.Value.HasValue ? iudFolio.Value.Value : 0,
                                                AlmacenID = Contexto.Almacen.AlmacenID,
                                                Observaciones = txtObservaciones.Text,
                                                ListaCierreDiaInventarioPADetalle = listaModificada,
                                                UsuarioCreacionID = usuarioID,
                                                OrganizacionID = organizacionID
                                            };
            var almacenMovimientoPL = new AlmacenMovimientoPL();
            IList<ResultadoPolizaModel> pdfs = almacenMovimientoPL.GuardarCierreDiaInventarioPA(cierreDiaInventarioPA);
            if (pdfs != null)
            {
                for (var i = 0; i < pdfs.Count; i++)
                {
                    if (pdfs[i].PDFs != null)
                    {
                        foreach (var memoryStream in pdfs[i].PDFs)
                        {
                            ImprimePoliza(memoryStream.Value, memoryStream.Key);
                        }
                    }
                }
            }
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.CierreDiaInventarioPA_GuardoExito, MessageBoxButton.OK,
                              MessageImage.Correct);
            LimpiarPantalla();
        }

        private void ImprimePoliza(MemoryStream pdf, TipoPoliza tipoPoliza)
        {
            var exportarPoliza = new ExportarPoliza();
            exportarPoliza.ImprimirPoliza(pdf, string.Format("{0} {1}", "Poliza", tipoPoliza));
        }

        /// <summary>
        /// Valida la informacíon antes de Guardar
        /// </summary>
        private bool ValidarAntesGuardar()
        {
            if (cboAlmacen.SelectedIndex == 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_AlmacenRequerido, MessageBoxButton.OK, MessageImage.Warning);
                cboAlmacen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtObservaciones.Text))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_ObservacionesRequerido, MessageBoxButton.OK, MessageImage.Warning);
                txtObservaciones.Focus();
                return false;
            }

            if (!listaProductos.SelectMany(
                    prod => prod.ListaCierreDiaInventarioPADetalle.Where(det => det.InventarioFisicoCaptura.HasValue)).ToList().Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CierreDiaInventarioPA_SinModificacion, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarPantalla()
        {
            listaProductos = new List<CierreDiaInventarioPAInfo>();
            listaMermas = new List<MermaSuperavitInfo>();
            InicializaContexto();
            iudFolio.ClearValue(IntegerUpDown.ValueProperty);
            txtObservaciones.Text = string.Empty;
            cboAlmacen.IsEnabled = true;
            cboAlmacen.SelectedIndex = 0;
            treeDatos.Items.Clear();
            GenerarTreeViewDefault();
        }

        private bool ValidarProductosSinMerma(string producto)
        {
            if (producto.ToUpper().Trim().Equals(ProductoMaiz) || producto.ToUpper().Trim().Equals(ProductoMelaza))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.CierreDiaInventarioPA_ProductoSinMerma, producto), MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }

        #endregion METODOS
    }
}
