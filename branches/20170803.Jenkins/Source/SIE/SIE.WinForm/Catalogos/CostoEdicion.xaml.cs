using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CostoEdicion.xaml
    /// </summary>
    public partial class CostoEdicion
    {
        #region PROPIEDADES

        private CostoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CostoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public CostoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        public CostoEdicion(CostoInfo costo)
        {
            InitializeComponent();
            Contexto = costo;
            CambiarLeyendaCombo();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        /// <summary>
        /// Iniciliaza el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CostoInfo();
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
        /// Guardar un Nuevo Costo
        /// </summary>
        private void Guardar()
        {
            try
            {
                bool guardar = ValidaGuardar();
                if (guardar)
                {
                    var costoPL = new CostoPL();
                    costoPL.Crear(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.CostoID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        var contextoClone = Extensor.ClonarInfo(Contexto) as CostoInfo;
                        InicializaContexto();
                        AsignarValoresContexto(contextoClone);
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
        /// Asigna valores al contexto despues
        /// de que se realice un guardado
        /// </summary>
        /// <param name="contextoClone"></param>
        private void AsignarValoresContexto(CostoInfo contextoClone)
        {
            if (contextoClone != null)
            {
                Contexto.ListaRetencion = contextoClone.ListaRetencion;
                Contexto.ListaTipoCostos = contextoClone.ListaTipoCostos;
                Contexto.ListaTipoProrrateo = contextoClone.ListaTipoProrrateo;
                Contexto.ListaTipoCostoCentro = contextoClone.ListaTipoCostoCentro;
                Contexto.UsuarioCreacionID = contextoClone.UsuarioCreacionID;
                Contexto.UsuarioModificacionID = contextoClone.UsuarioModificacionID;

                RetencionInfo retencion =
                    contextoClone.ListaRetencion.FirstOrDefault(ret => ret.RetencionID == 0);
                if (retencion != null)
                {
                    Contexto.Retencion = retencion;
                }

                TipoProrrateoInfo tipoProrrateo =
                    contextoClone.ListaTipoProrrateo.FirstOrDefault(tip => tip.TipoProrrateoID == 0);
                if (tipoProrrateo != null)
                {
                    Contexto.TipoProrrateo = tipoProrrateo;
                }

                TipoCostoInfo tipoCosto =
                    contextoClone.ListaTipoCostos.FirstOrDefault(costo => costo.TipoCostoID == 0);
                if (tipoCosto != null)
                {
                    Contexto.TipoCosto = tipoCosto;
                }

                TipoCostoCentroInfo tipoCostoCentro =
                    contextoClone.ListaTipoCostoCentro.FirstOrDefault(CostoCentro => CostoCentro.TipoCostoCentroID == 0);
                if (tipoCostoCentro != null)
                {
                    Contexto.TipoCostoCentro = tipoCostoCentro;
                }

                cboRetencion.ItemsSource = Contexto.ListaRetencion;
                cboTipoCosto.ItemsSource = Contexto.ListaTipoCostos;
                cboTipoProrrateo.ItemsSource = Contexto.ListaTipoProrrateo;
                cboTipoCostoCentro.ItemsSource = Contexto.ListaTipoCostoCentro;

                cboTipoCosto.SelectedItem = Contexto.TipoCosto;
                cboRetencion.SelectedItem = Contexto.Retencion;
                cboTipoProrrateo.SelectedItem = Contexto.TipoProrrateo;
                cboTipoCostoCentro.SelectedItem = Contexto.TipoCostoCentro;

                txtClaveContable.Focus();
            }
        }

        /// <summary>
        /// Valida los campos para su guardado
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            var guardar = true;
            var mensaje = string.Empty;            

            if (string.IsNullOrWhiteSpace(Contexto.ClaveContable))
            {
                guardar = false;
                txtClaveContable.Focus();
                mensaje = Properties.Resources.CostoEdicion_ClaveContable_Requerida;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    guardar = false;
                    txtDescripcion.Focus();
                    mensaje = Properties.Resources.CostoEdicion_Descripcion_Requerida;
                }
                else
                {
                    if (Contexto.TipoCosto.TipoCostoID == 0)
                    {
                        guardar = false;
                        cboTipoCosto.Focus();
                        mensaje = Properties.Resources.CostoEdicion_TipoCosto_Requerida;
                    }
                    else
                    {
                        if (Contexto.TipoProrrateo.TipoProrrateoID == 0)
                        {
                            guardar = false;
                            cboTipoProrrateo.Focus();
                            mensaje = Properties.Resources.CostoEdicion_TipoProrrateo_Requerida;
                        }
                    }
                }
            }
            if (guardar)
            {
                var costoPL = new CostoPL();
                CostoInfo costo = costoPL.ObtenerPorDescripcion(Contexto.Descripcion);
                if (costo != null && Contexto.CostoID != costo.CostoID)
                {
                    mensaje = string.Format(Properties.Resources.CostoEdicion_Descripcion_Existente,
                                            costo.CostoID);
                    txtDescripcion.Focus();
                    guardar = false;
                }
                else
                {
                    costo = costoPL.ObtenerPorClaveContable(Contexto);
                    if (costo != null && Contexto.CostoID != costo.CostoID)
                    {
                        mensaje = Properties.Resources.CostoEdicion_ClaveConable_Existente;
                        txtClaveContable.Focus();
                        guardar = false;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return guardar;
        }

        /// <summary>
        /// Cambiar leyenda de Todos por Seleccione
        /// </summary>
        private void CambiarLeyendaCombo()
        {
            TipoCostoInfo tipoCosto =
                Contexto.ListaTipoCostos.FirstOrDefault(
                    desc => desc.Descripcion.Equals(Properties.Resources.cbo_Seleccionar));
            if (tipoCosto != null)
            {
                tipoCosto.Descripcion = Properties.Resources.cbo_Seleccione;
            }
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
            txtClaveContable.Focus();
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
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
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

        private void TxtDescripcionAceptaNumerosLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        private void TxtClaveContableAceptaNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Extensor.ValidarNumeros(e.Text);
        }

        #endregion EVENTOS
    }
}
