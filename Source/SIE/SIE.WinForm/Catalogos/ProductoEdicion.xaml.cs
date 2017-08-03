using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System.Collections.Generic;
using System.Windows;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Base.Log;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ProductoEdicion.xaml
    /// </summary>
    public partial class ProductoEdicion
    {
        #region PROPIEDADES

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        private ProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto(true);
                }
                return (ProductoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public ProductoEdicion()
        {
            InitializeComponent();
            InicializaContexto(true);
        }

        public ProductoEdicion(ProductoInfo productoInfo)
        {
            InitializeComponent();
            Contexto = productoInfo;
            InicializaContexto(productoInfo.ProductoId == 0);
            CambiarLeyendaCombo();
            ValidarExistenciaProductosAlmacen();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        private void BloquearDesbloquearModificacion(bool habilitado)
        {
            cboFamilia.IsEnabled = habilitado;
            cboSubFamilia.IsEnabled = habilitado;
            cboUnidad.IsEnabled = habilitado;
            chkManejaLote.IsEnabled = habilitado;
            cmbEstatus.IsEnabled = habilitado;
        }

        private void ValidarExistenciaProductosAlmacen()
        {
            int productoID = Contexto.ProductoId;
            var almacenPL = new AlmacenPL();
            bool tieneExistencias = almacenPL.ValidarExistenciasProductoEnAlmacen(productoID);
            if (tieneExistencias)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ProductoEdicion_MsgProductosAlmacen, MessageBoxButton.OK,
                                      MessageImage.Warning);
                BloquearDesbloquearModificacion(false);

            }
        }

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        /// <param name="nuevo"></param>
        private void InicializaContexto(bool nuevo)
        {
            if (nuevo)
            {
                Contexto.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            }
            else
            {
                Contexto.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                txtProductoId.IsEnabled = false;
            }
        }

        /// <summary>
        /// Cambiar leyenda de Todos por Seleccione
        /// </summary>
        private void CambiarLeyendaCombo()
        {
            UnidadMedicionInfo unidadTodos =
                Contexto.UnidadesMedidcion.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccionar));
            if (unidadTodos != null)
            {
                unidadTodos.Descripcion = Properties.Resources.cbo_Seleccione;
            }

            FamiliaInfo familiaTodos =
                Contexto.Familias.FirstOrDefault(desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccionar));
            if (familiaTodos != null)
            {
                familiaTodos.Descripcion = Properties.Resources.cbo_Seleccione;
            }
        }

        /// <summary>
        /// Asigna valores al combo de 
        /// subfamilias dependendiendo
        /// de la familia seleccionada
        /// </summary>
        private void AsignaSubfamilias(bool cambioCombo)
        {
            if (Contexto.SubFamilias != null)
            {
                IList<SubFamiliaInfo> subFamilias =
                    Contexto.SubFamilias.Where(fam => fam.Familia.FamiliaID == Contexto.FamiliaId).ToList();
                if (subFamilias.Any())
                {
                    cboSubFamilia.ItemsSource = subFamilias;
                    subFamilias.Insert(0, new SubFamiliaInfo { SubFamiliaID = 0, Descripcion = Properties.Resources.cbo_Seleccione });
                    if (Contexto.SubfamiliaId == 0 || cambioCombo)
                    {
                        cboSubFamilia.SelectedIndex = 0;
                    }
                }
                else
                {
                    DeshabilitaComboSubfamilia();
                }
            }
        }

        /// <summary>
        /// Deshabilita el combo de subfamilia
        /// </summary>
        private void DeshabilitaComboSubfamilia()
        {
            var subFamilias = new List<SubFamiliaInfo>();
            var subFamiliaInfo = new SubFamiliaInfo { SubFamiliaID = 0, Descripcion = Properties.Resources.cbo_Seleccione };
            subFamilias.Insert(0, subFamiliaInfo);
            cboSubFamilia.ItemsSource = subFamilias;
            cboSubFamilia.SelectedItem = subFamiliaInfo;
        }

        /// <summary>
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Guarda un Producto
        /// </summary>
        private void Guardar()
        {
            try
            {
                bool guardar = ValidaCamposGuardar();
                if (guardar)
                {
                    int productoId = Contexto.ProductoId;
                    var productoPL = new ProductoPL();
                    productoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (productoId != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        //InicializaContexto(true);  
                        Contexto.ProductoId = 0;
                        Contexto.ManejaLote = false;
                        Contexto.SubfamiliaId = 0;
                        Contexto.UnidadId = 0;
                        Contexto.ProductoDescripcion = string.Empty;

                        cboFamilia.SelectedIndex = 0;
                        cboSubFamilia.SelectedIndex = 0;
                        cboUnidad.SelectedIndex = 0;
                        chkManejaLote.IsChecked = false;
                        txtDescripcion.Focus();

                        //Contexto = new ProductoInfo { UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado() };
                        BloquearDesbloquearModificacion(true);
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, Properties.Resources.Producto_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.Producto_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida que los campos requeridos contengan valor
        /// </summary>
        /// <returns></returns>
        private bool ValidaCamposGuardar()
        {
            var guardar = true;
            var mensaje = string.Empty;


            if (Contexto.ProductoId == 0)
            {
                guardar = false;
                txtProductoId.Focus();
                mensaje = Properties.Resources.ProductoEdicion_CodigoMayorCero;
            }
            else
                if (string.IsNullOrWhiteSpace(Contexto.ProductoDescripcion))
                {
                    guardar = false;
                    txtDescripcion.Focus();
                    mensaje = Properties.Resources.ProductoEdicion_Descripcion_Requerida;
                }
                else
                {
                    int familiaId = Extensor.ValorEntero(Convert.ToString(cboFamilia.SelectedValue));
                    if (familiaId == 0)
                    {
                        guardar = false;
                        cboFamilia.Focus();
                        mensaje = Properties.Resources.ProductoEdicion_Familia_Requerida;
                    }
                    else
                    {
                        int subFamiliaId = Extensor.ValorEntero(Convert.ToString(cboSubFamilia.SelectedValue));
                        if (subFamiliaId == 0)
                        {
                            guardar = false;
                            cboSubFamilia.Focus();
                            mensaje = Properties.Resources.ProductoEdicion_SubFamilia_Requeria;
                        }
                        else
                        {
                            int unidadId = Extensor.ValorEntero(Convert.ToString(cboUnidad.SelectedValue));
                            if (unidadId == 0)
                            {
                                guardar = false;
                                cboUnidad.Focus();
                                mensaje = Properties.Resources.ProductoEdicion_Unidad_Requerida;
                            }
                        }
                    }
                }
            if (guardar)
            {
                var productoPL = new ProductoPL();
                ProductoInfo producto = productoPL.ObtenerPorDescripcion(Contexto.ProductoDescripcion);
                if (producto != null && Contexto.UsuarioModificacionID == null)
                {
                    mensaje = string.Format(Properties.Resources.ProductoEdicion_Descripcion_Existente,
                                            producto.ProductoId);
                    txtDescripcion.Focus();
                    guardar = false;
                }
                if (producto == null)
                {
                    producto = productoPL.ObtenerPorIDSinActivo(Contexto);
                }
                if (producto != null && Contexto.UsuarioModificacionID == null)
                {
                    mensaje = string.Format(Properties.Resources.ProductoEdicion_Codigo_Existente,
                                            producto.Descripcion);
                    txtProductoId.Focus();
                    guardar = false;
                }

                producto = productoPL.ObtenerPorMaterialSAP(Contexto.MaterialSAP);
                if (producto != null && Contexto.ProductoId != producto.ProductoId)
                {
                    mensaje = string.Format(Properties.Resources.ProductoEdicion_Material_Existente,
                                            producto.ProductoId);
                    txtMaterialSAP.Focus();
                    guardar = false;
                }
            }
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return guardar;
        }

        /// <summary>
        /// Valida que el control solo acepte números y letras.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }


        #endregion METODOS

        #region EVENTOS

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
            cboFamilia.ItemsSource = Contexto.Familias;
            cboUnidad.ItemsSource = Contexto.UnidadesMedidcion;
            AsignaSubfamilias(false);
            cboFamilia.SelectionChanged += CboFamilia_OnSelectionChanged;
        }

        /// <summary>
        /// evento cuando se preciona cualquier tecla en el formulario 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar
                                                          , MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Se ejecuta al presionar el boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Se ejecuta al presionar el boton Guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_OnClick(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Se ejecuta cuando el valor del
        /// combo ha cambiado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboFamilia_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AsignaSubfamilias(true);
        }

        /// <summary>
        /// Perdida de foco del campo cabezas a sacrificar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaterialSAP_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Contexto.MaterialSAP))
            {
                Contexto.MaterialSAP = Contexto.MaterialSAP.PadLeft(18, '0');
            }
        }

        #endregion EVENTOS
    }
}
