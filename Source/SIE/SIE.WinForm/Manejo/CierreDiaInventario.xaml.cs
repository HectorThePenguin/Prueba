using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para CierrreDiaInventario.xaml
    /// </summary>
    public partial class CierreDiaInventario
    {
        #region Atributos

        private int usuarioID;
        private int organizacionId;
        private IList<AlmacenInfo> listaAlmacenInfo;
        private IList<AlmacenCierreDiaInventarioInfo> datosGrid;
        private bool bandFoco;
        private int index;
        private bool EsKeyDown;
        private bool llenadoComboInicio;
        private bool manejarSeleccion;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CierreDiaInventario()
        {
            bandFoco = false;
            index = -1;
            InitializeComponent();
            usuarioID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            DesahabilitarControles(false);
            llenadoComboInicio = false;
            manejarSeleccion = true;
        }
        #endregion end

        #region Eventos
        /// <summary>
        /// Cargar combo almacenes inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CierreDiaInventario_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CboAlmacenes.SelectedIndex <= 0)
                {
                    CargarCboAlmacenes();
                    CboAlmacenes.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Key down combo almacenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAlmacenes_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                try
                {
                    if (CboAlmacenes.SelectedIndex > 0)
                    {
                        CargarCampos();
                        if (gridProductosInventario.Items.Count == 0)
                        {
                            CargarGridProductos();
                        }
                        if (txtObservaciones.IsEnabled)
                        {
                            txtObservaciones.Focus();
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.AutorizarAjuste_SeccioneAlmacen,
                        MessageBoxButton.OK, MessageImage.Warning);
                        LimpiarCaptura();
                        e.Handled = true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
            }
        }

        /// <summary>
        /// Se calcula cuando se pierde el foco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAlmacenes_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bandFoco)
                {
                    if (CboAlmacenes.SelectedIndex > 0)
                    {
                        CargarCampos();
                        if (gridProductosInventario.Items.Count == 0)
                        {
                            CargarGridProductos();
                        }
                        e.Handled = true;
                        bandFoco = true;
                    }
                }
                else
                {
                    bandFoco = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        /// <summary>
        /// Validar solo numeros    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCandidadReal_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosConPunto(e.Text);
        }

        /// <summary>
        /// evento para calcular el importe real.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTextBlock_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                try
                {
                    EsKeyDown = true;
                    CalcularProducto(sender);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
            }
            else if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Send(Key.Tab);
            }
        }

        /// <summary>
        /// Evento para guardar cierre dia inventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GuardarCierreDiaInventario())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CierreDiaInventario_GuardarExito,
                        MessageBoxButton.OK, MessageImage.Correct);
                    LimpiarCaptura();
                    DesahabilitarControles(false);
                    btnGuardar.IsEnabled = false;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CierreDiaInventario_GuardarError,
                        MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (InvalidPortNameException ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CierreDiaInventario_GuardarError + ex.Message,
                    MessageBoxButton.OK, MessageImage.Error);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CierreDiaInventario_GuardarError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CierreDiaInventario_GuardarError, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Cancelar el cierre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CierreDiaInventario_Cancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarCaptura();
                DesahabilitarControles(false);
                CboAlmacenes.Focus();
            }
        }

        /// <summary>
        /// Selecciona el texto del texbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAll(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            try
            {
                if (tb == null)
                {
                    return;
                }
                if (!tb.IsKeyboardFocusWithin)
                {
                    index = gridProductosInventario.SelectedIndex;
                    gridProductosInventario.UnselectAll();
                    gridProductosInventario.UpdateLayout();
                    gridProductosInventario.SelectedIndex = index;
                    tb.Focus();
                    tb.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Agrega evento con el key
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

        /// <summary>
        /// se carga el grid al cambio de almacen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAlmacenes_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (manejarSeleccion)
                {
                    if (llenadoComboInicio && gridProductosInventario.Items.Count > 0)
                    {
                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CierreDiaInventario_MensajeCancelar,
                            MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                        {
                            if (CboAlmacenes.SelectedIndex > 0)
                            {
                                CargarCampos();
                                CargarGridProductos();
                                if (txtObservaciones.IsEnabled)
                                {
                                    e.Handled = true;
                                }
                            }
                            else
                            {
                                //SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                //    Properties.Resources.AutorizarAjuste_SeccioneAlmacen,
                                //    MessageBoxButton.OK, MessageImage.Warning);
                                LimpiarCaptura();
                                btnGuardar.IsEnabled = false;
                            }
                        }
                        else
                        {
                            ComboBox combo = (ComboBox)sender;
                            manejarSeleccion = false;
                            combo.SelectedItem = e.RemovedItems[0];
                            return;
                        }
                    }
                    else
                    {
                        if (CboAlmacenes.SelectedIndex > 0)
                        {
                            CargarCampos();
                            CargarGridProductos();
                            if (txtObservaciones.IsEnabled)
                            {
                                e.Handled = true;
                            }
                        }
                        else
                        {
                            //SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            //    Properties.Resources.AutorizarAjuste_SeccioneAlmacen,
                            //    MessageBoxButton.OK, MessageImage.Warning);
                            LimpiarCaptura();
                            btnGuardar.IsEnabled = false;
                        }
                    }
                }
                manejarSeleccion = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// key up del gridProductosInventario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void key_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Tab || e.Key == Key.Enter )
                {
                    TextBox tb = (sender as TextBox);
                    if (tb != null)
                    {
                        if (tb.Text == "0.00")
                        {
                            tb.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }     
        }

        /// <summary>
        /// Key down de observaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtObservaciones_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                index = 0;
                gridProductosInventario.SelectedIndex = 0;
             }
        }

        /// <summary>
        /// Se valida cuando se pierde el foco del texbox de la cantidad.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Key_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (EsKeyDown)
            {
                var text = (sender as TextBox);
                if (text != null)
                {
                    if (text.Text == "")
                    {
                        AlmacenCierreDiaInventarioInfo seleccion =
                            (AlmacenCierreDiaInventarioInfo) gridProductosInventario.SelectedItem;
                        if (seleccion != null)
                        {
                            foreach (
                                var almacenCierreDiaInventarioInfo in
                                    datosGrid.Where(
                                        almacenCierreDiaInventarioInfo =>
                                            seleccion.ProductoID == almacenCierreDiaInventarioInfo.ProductoID))
                            {
                                if (almacenCierreDiaInventarioInfo.ImporteReal > 0)
                                {
                                    text.Text =
                                        almacenCierreDiaInventarioInfo.CantidadReal.ToString(
                                            CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    text.Text = "0.00";
                                }
                                gridProductosInventario.UpdateLayout();
                            }
                        }
                    }
                }
            }
            else
            {
                CalcularProductoClick(sender, e);
            }
            EsKeyDown = false;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Carga datos combo Almacenes
        /// </summary>
        private void CargarCboAlmacenes()
        {
            try
            {
                AlmacenPL almacenPl = new AlmacenPL();
                listaAlmacenInfo = almacenPl.ObtenerAlmacenPorOrganizacion(organizacionId);
                if (listaAlmacenInfo != null)
                {
                    var almacenes = new AlmacenInfo { Descripcion = "Seleccione", AlmacenID = 0 };
                    listaAlmacenInfo.Insert(0, almacenes);
                    CboAlmacenes.ItemsSource = listaAlmacenInfo;
                    CboAlmacenes.SelectedValue = 0;
                    llenadoComboInicio = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CierreDiaInventario_NoAlmacenesOrganizacionUsuario,
                            MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCaptura();
                    DesahabilitarControles(false);
                    CboAlmacenes.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private IList<AlmacenCierreDiaInventarioInfo> FiltrarProductosAlmacen(IList<AlmacenCierreDiaInventarioInfo> inventariosAlmacenCierres)
        {
            var parametroGeneralPL = new ParametroGeneralPL();
            ParametroGeneralInfo parametroGeneral =
                parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.PRODUCTOAJUSTAR.ToString());

            var productosEliminados = new List<int>();
            if (parametroGeneral != null)
            {
                List<string> productos = parametroGeneral.Valor.Split('|').ToList();
                productosEliminados = productos.Select(p => Convert.ToInt32(p)).ToList();
            }
            List<int> productosActuales = inventariosAlmacenCierres.Select(prod => prod.ProductoID).ToList();
            List<int> productosPorMostrar = productosActuales.Except(productosEliminados).ToList();
            inventariosAlmacenCierres =
                inventariosAlmacenCierres.Join(productosPorMostrar, x => x.ProductoID, i => i, (inv, prod) => inv).ToList();
            return inventariosAlmacenCierres;
        }
       
        /// <summary>
        /// Carga los productos del grid.
        /// </summary>
        private void CargarGridProductos()
        {
            try
            {
                var almacenPl = new AlmacenPL();
                var almacenId = (int)CboAlmacenes.SelectedValue;
                datosGrid = almacenPl.ObtenerProductosAlamcen(almacenId, organizacionId);
                if (datosGrid != null)
                {
                    datosGrid = FiltrarProductosAlmacen(datosGrid);
                    gridProductosInventario.ItemsSource = datosGrid;
                    txtObservaciones.IsEnabled = true;
                    btnGuardar.IsEnabled = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CierreDiaInventario_NoAlmacenesNoProductos,
                            MessageBoxButton.OK, MessageImage.Warning);
                    LimpiarCaptura();
                    DesahabilitarControles(false);
                    CboAlmacenes.Focus();
                    btnGuardar.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Validar formato decimal
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool ValidarDecimal(string text)
        {
            try 
            {
                var dato = text.Split('.');
                if (dato.Count() > 2)
                {
                    return false;
                }
                return true;
            }catch(Exception Ex){
                Logger.Error(Ex);
                return false;
            }
        }

        /// <summary>
        /// Limpia captura de pantalla
        /// </summary>
        private void LimpiarCaptura()
        {
            txtEstatus.Clear();
            txtFolio.Clear();
            txtObservaciones.Clear();
            dtpFecha.SelectedDate = DateTime.Now;
            gridProductosInventario.ItemsSource = null;
            CboAlmacenes.SelectedIndex = 0;
            CboAlmacenes.Focus();
            index = -1;
        }

        /// <summary>
        /// Deshabilita controles de pantalla
        /// </summary>
        /// <param name="habilitar"></param>
        private void DesahabilitarControles(bool habilitar)
        {
            txtEstatus.IsEnabled = habilitar;
            txtFolio.IsEnabled = habilitar;
            dtpFecha.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
            txtObservaciones.IsEnabled = habilitar;
        }

        /// <summary>
        /// Cargar Campos
        /// </summary>
        private void CargarCampos()
        {
            var almacenPl = new AlmacenPL();
            var almacenId = (int)CboAlmacenes.SelectedValue;
            var cierreInventarioInfo = new AlmacenCierreDiaInventarioInfo
            {
                Almacen = new AlmacenInfo(){AlmacenID = almacenId},
                OrganizacionId = organizacionId,
                TipoMovimiento = (int) TipoMovimiento.InventarioFisico
            };
            var resultadoAlmacenes = almacenPl.ObtenerDatosAlmacenInventario(cierreInventarioInfo);
            if (resultadoAlmacenes != null)
            {
                txtFolio.Text = (resultadoAlmacenes.FolioAlmacen).ToString(CultureInfo.InvariantCulture);
                txtEstatus.Text = EstatusInventario.Nuevo.ToString();
                dtpFecha.SelectedDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Guardar 
        /// </summary>
        /// <returns></returns>
        private bool GuardarCierreDiaInventario()
        {
            bool regreso = false;
            try
            {
                var almacenInfo = (AlmacenInfo)CboAlmacenes.SelectedItem;
                var cierreInventarioPl = new CierreDiaInventarioPL();
                var almacenCierreInventarioInfo = new AlmacenCierreDiaInventarioInfo
                {
                    UsuarioCreacionId = usuarioID,
                    OrganizacionId = organizacionId,
                    Observaciones = txtObservaciones.Text,
                    FolioAlmacen = Convert.ToInt64(txtFolio.Text),
                    Almacen = almacenInfo
                };
                int resultadoGuardar = cierreInventarioPl.GuardarCierreDiaInventario(datosGrid, almacenCierreInventarioInfo);
                if (resultadoGuardar >= 0)
                {
                    regreso = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = Properties.Resources.ConciliacionSAP_ErrorObtenerPolizas;
                bool tiempoEspera = ValidaExcepcionTiempoEspera(ex);
                if (tiempoEspera)
                {
                    mensaje = "Ocurrió un error con la conexión";
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal]
                                      , mensaje, MessageBoxButton.OK, MessageImage.Error);
                }
                Logger.Error(ex);
            }
            return regreso;
        }

        /// <summary>
        /// Calcular el producto
        /// </summary>
        /// <param name="sender"></param>
        private void CalcularProducto(object sender)
        {
            AlmacenCierreDiaInventarioInfo seleccion =
                   (AlmacenCierreDiaInventarioInfo)gridProductosInventario.SelectedItem;
            TextBox text = (TextBox)sender;
            
            if (seleccion != null && text.Text != "")
            {
                text.Text = text.Text.Replace(" ", "");
                if (ValidarDecimal(text.Text))
                {
                    var cantidad = decimal.Parse(text.Text.Trim(),
                    NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    if (cantidad > 0)
                    {
                        foreach (var almacenCierreDiaInventarioInfo in datosGrid.Where(almacenCierreDiaInventarioInfo => seleccion.ProductoID == almacenCierreDiaInventarioInfo.ProductoID))
                        {
                            almacenCierreDiaInventarioInfo.ImporteReal =
                                almacenCierreDiaInventarioInfo.PrecioPromedio * cantidad;
                            almacenCierreDiaInventarioInfo.FolioAlmacen = long.Parse(txtFolio.Text);
                            almacenCierreDiaInventarioInfo.CantidadReal = cantidad;
                            almacenCierreDiaInventarioInfo.Observaciones = txtObservaciones.Text;
                            btnGuardar.IsEnabled = true;
                            break;
                        }
                    }
                    else
                    {
                        foreach (var almacenCierreDiaInventarioInfo in datosGrid)
                        {
                            if (seleccion.ProductoID == almacenCierreDiaInventarioInfo.ProductoID)
                            {
                                almacenCierreDiaInventarioInfo.ImporteReal = 0;
                                almacenCierreDiaInventarioInfo.CantidadReal = 0;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    index = gridProductosInventario.SelectedIndex;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CierreDiaInventario_CantidadInsertadaError,
                    MessageBoxButton.OK, MessageImage.Warning);
                    gridProductosInventario.CurrentCell = new DataGridCellInfo(
                    gridProductosInventario.Items[index], gridProductosInventario.Columns[4]);
                    gridProductosInventario.BeginEdit();
                    gridProductosInventario.SelectedIndex = index;
                    text.SelectAll();
                    return;
                }
            }
            index = gridProductosInventario.SelectedIndex;

            gridProductosInventario.ItemsSource = null;
            gridProductosInventario.ItemsSource = datosGrid;
            gridProductosInventario.Focus();

            if (gridProductosInventario.Items.Count > (index + 1))
            {
                gridProductosInventario.CurrentCell = new DataGridCellInfo(
                gridProductosInventario.Items[index + 1], gridProductosInventario.Columns[4]);
                gridProductosInventario.BeginEdit();
                gridProductosInventario.SelectedIndex = index + 1;
            }
            else
            {
                btnGuardar.Focus();
            }
            text.Focus();
            text.SelectAll();
        }

        private TextBox text;
        public void CalcularProductoClick(object sender, RoutedEventArgs e)
        {
            var seleccion =
                   (AlmacenCierreDiaInventarioInfo)gridProductosInventario.SelectedItem;
            text = (TextBox)sender;
            if (seleccion != null && text.Text != "")
            {
                text.Text = text.Text.Replace(" ", "");
                if (ValidarDecimal(text.Text))
                {
                    var cantidad = decimal.Parse(text.Text.Trim(),
                    NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    if (cantidad > 0)
                    {
                        foreach (var almacenCierreDiaInventarioInfo in datosGrid.Where(almacenCierreDiaInventarioInfo => seleccion.ProductoID == almacenCierreDiaInventarioInfo.ProductoID))
                        {
                            almacenCierreDiaInventarioInfo.ImporteReal =
                                almacenCierreDiaInventarioInfo.PrecioPromedio * cantidad;
                            almacenCierreDiaInventarioInfo.FolioAlmacen = long.Parse(txtFolio.Text);
                            almacenCierreDiaInventarioInfo.CantidadReal = cantidad;
                            almacenCierreDiaInventarioInfo.Observaciones = txtObservaciones.Text;
                            btnGuardar.IsEnabled = true;
                            break;
                        }
                        
                    }
                    else
                    {
                        foreach (var almacenCierreDiaInventarioInfo in datosGrid)
                        {
                            if (seleccion.ProductoID == almacenCierreDiaInventarioInfo.ProductoID)
                            {
                                almacenCierreDiaInventarioInfo.ImporteReal = 0;
                                almacenCierreDiaInventarioInfo.CantidadReal = 0;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CierreDiaInventario_CantidadInsertadaError,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            gridProductosInventario.ItemsSource = null;
            gridProductosInventario.ItemsSource = datosGrid;
            gridProductosInventario.Focus();
        }
        #endregion
    }
}
