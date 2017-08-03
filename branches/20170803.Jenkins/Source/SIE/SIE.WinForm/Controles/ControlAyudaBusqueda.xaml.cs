using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using System.Reflection;
using SIE.Base.Log;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Controles
{
    /// <summary>
    /// Lógica de interacción para ControlAyudaBusqueda.xaml
    /// </summary>
    public partial class ControlAyudaBusqueda
    {
        #region CONSTRUCTORES

        internal ControlAyudaBusqueda()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTORES

        #region VARIABLES



        public int LongitudMaximaCampoDescripcion
        {
            get
            {
                return (int)GetValue(LongitudMaximaCampoDescripcionProperty);
            }
            set
            {
                txtBusqueda.MaxLength = value;
                SetValue(LongitudMaximaCampoDescripcionProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for LongitudMaximaCampoDescripcion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LongitudMaximaCampoDescripcionProperty =
            DependencyProperty.Register("LongitudMaximaCampoDescripcion", typeof(int), typeof(ControlAyudaBusqueda), new PropertyMetadata(50));



        private bool confirmaSalir = true;
        private string descripcionAnterior = string.Empty;

        #endregion VARIABLES

        #region PROPIEDADES

        internal string ConceptoBusqueda { get; set; }

        internal string TituloBusqueda { get; set; }

        internal string CampoClave { get; set; }

        internal string CampoDescripcion { get; set; }

        internal object Contexto
        {
            get { return DataContext; }
            set { DataContext = value; }
        }

        internal string MetodoInvocacion { get; set; }

        internal object ObjetoNegocio { get; set; }

        internal string MensajeCerrar { get; set; }

        internal string MensajeAgregar { get; set; }

        internal string EncabezadoClaveGrid { get; set; }

        internal string EncabezadoDescripcionGrid { get; set; }

        internal bool Cancelado { get; set; }

        #endregion PROPIEDADES

        #region EVENTOS

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            lblConcepto.Content = ConceptoBusqueda;
            ucTitulo.TextoTitulo = TituloBusqueda;

            BindearCampos();
            InicializarPaginador();
            GeneraColumnasGrid();

            Buscar();
            txtBusqueda.Focus();
        }

        private void BusquedaKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Buscar();
            }
        }

        private void BuscarClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
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

        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, MensajeCerrar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Cancelado = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void WindowKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                confirmaSalir = true;
                Close();
            }
        }

        private void GridPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.SystemKey == Key.Enter)
            {
                AsignarValoresSeleccionadosGrid();
            }
        }

        private void GridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AsignarValoresSeleccionadosGrid();
        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            Cancelado = true;
            confirmaSalir = true;
            Close();
        }

        private void AgregarClick(object sender, RoutedEventArgs e)
        {
            bool elementoSeleccionando = AsignarValoresSeleccionados();
            if (elementoSeleccionando)
            {
                confirmaSalir = false;
                Close();
            }
            else
            {
                SkMessageBox.Show(this, MensajeAgregar, MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        private void BusquedaPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion EVENTOS

        #region METODOS

        private void InicializarPaginador()
        {
            ucPaginacion.AsignarValoresIniciales();
            ucPaginacion.DatosDelegado += ObtenerValoresBusqueda;
            ucPaginacion.Contexto = Contexto;
        }

        private void BindearCampos()
        {
            var bind = new Binding(CampoDescripcion)
            {
                TargetNullValue = string.Empty,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Mode = BindingMode.TwoWay
            };
            txtBusqueda.SetBinding(TextBox.TextProperty, bind);
        }

        private void Buscar()
        {
            ObtenerValoresBusqueda(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void ObtenerValoresBusqueda(int inicio, int limite)
        {
            try
            {
                if (dgConsulta.Items.Count > 0)
                {
                    dgConsulta.ItemsSource = null;
                    dgConsulta.Items.Clear();
                }

                var tiposParametros = new List<Type>();
                var valoresParametros = new List<Object>();

                if (string.Compare(txtBusqueda.Text.Trim(), descripcionAnterior.Trim(), StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    descripcionAnterior = txtBusqueda.Text;
                    ucPaginacion.Inicio = 1;
                    inicio = 1;
                }

                var paginacionInfo = new PaginacionInfo { Inicio = inicio, Limite = limite };
                tiposParametros.Add(paginacionInfo.GetType());
                valoresParametros.Add(paginacionInfo);

                Contexto.GetType().GetProperty(CampoDescripcion).SetValue(Contexto, txtBusqueda.Text, null);
                tiposParametros.Add(Contexto.GetType());
                valoresParametros.Add(Contexto);

                MethodInfo metodo = ObjetoNegocio.GetType().GetMethod(MetodoInvocacion, tiposParametros.ToArray());
                if (metodo != null)
                {
                    dynamic resultadoInvocacion = metodo.Invoke(ObjetoNegocio, valoresParametros.ToArray());
                    if (resultadoInvocacion != null && resultadoInvocacion.Lista != null
                        && resultadoInvocacion.Lista.Count > 0)
                    {
                        ucPaginacion.TotalRegistros = resultadoInvocacion.TotalRegistros;
                        dgConsulta.ItemsSource = resultadoInvocacion.Lista;
                    }
                    else
                    {
                        ucPaginacion.TotalRegistros = 0;
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, SuKarne.Controls.Properties.Resources.Ayuda_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, SuKarne.Controls.Properties.Resources.Ayuda_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
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

        private void AsignarValoresSeleccionadosGrid()
        {
            dynamic renglonSeleccionado = dgConsulta.SelectedItem;
            if (renglonSeleccionado != null)
            {
                AuxAyuda.AsignaValoresInfo(Contexto, renglonSeleccionado);
                confirmaSalir = false;
                Close();
            }
        }

        private void GeneraColumnasGrid()
        {
            var columnID = new DataGridTextColumn
            {
                Header = EncabezadoClaveGrid,
                Binding = new Binding(CampoClave),
                Width = new DataGridLength(100)
            };
            dgConsulta.Columns.Add(columnID);

            var columnDescripcion = new DataGridTextColumn
            {
                Header = EncabezadoDescripcionGrid,
                Binding = new Binding(CampoDescripcion),
                Width = new DataGridLength(547)
            };
            dgConsulta.Columns.Add(columnDescripcion);
        }

        /// <summary>
        /// Asiga los Valores que se han Seleccionado
        /// en el Grid
        /// </summary>
        /// <returns></returns>
        private bool AsignarValoresSeleccionados()
        {
            var elementoSeleccionado = false;
            dynamic renglonSeleccionado = dgConsulta.SelectedItem;
            if (renglonSeleccionado != null)
            {
                elementoSeleccionado = true;
                AuxAyuda.AsignaValoresInfo(Contexto, renglonSeleccionado);
            }
            return elementoSeleccionado;
        }

        #endregion METODOS        

    }
}
