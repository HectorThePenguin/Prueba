using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para RegistrarHumedad.xaml
    /// </summary>
    public partial class RegistrarHumedad
    {
        #region Atributos
        public List<ContratoHumedadInfo> ListaContratoHumedad = new List<ContratoHumedadInfo>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RegistrarHumedad(List<ContratoHumedadInfo> listaContratoHumedad)
        {
            InitializeComponent();
            ListaContratoHumedad = listaContratoHumedad;
        }
        #endregion

        #region Propiedades
        public List<ContratoHumedadInfo> ListaContratoHumedadR
        {
            get { return ListaContratoHumedad; }
            set { ListaContratoHumedad = value; }
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Salir de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSalir_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Cargar lista de humedades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarHumedad_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ListaContratoHumedad != null)
            {
                GridContratoHumedad.ItemsSource = null;
                GridContratoHumedad.ItemsSource = ListaContratoHumedad;
            }
            TxtPorcentajeHumedad.Focus();
        }

        /// <summary>
        /// Evento keydown para la forma en general
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarHumedad_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Agrega un registro de humedad a la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAgregar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultadoValidacion = ValidarCampos();
                if (resultadoValidacion.Resultado)
                {
                    var contratoHumedad = new ContratoHumedadInfo()
                    {
                        PorcentajeHumedad = TxtPorcentajeHumedad.Value.HasValue ? TxtPorcentajeHumedad.Value.Value : 0,
                        FechaInicio = Convert.ToDateTime(DpFechaInicio.Text)
                    };
                    ListaContratoHumedad.Add(contratoHumedad);
                    GridContratoHumedad.ItemsSource = null;
                    GridContratoHumedad.ItemsSource = ListaContratoHumedad;
                    LimpiarCampos();
                }
                else
                {
                    var mensaje = "";
                    mensaje = string.IsNullOrEmpty(resultadoValidacion.Mensaje) ? Properties.Resources.CrearContrato_MensajeValidacionDatosEnBlanco : resultadoValidacion.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Seleccionar una fecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DpFechaInicio_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            if (picker == null) return;
            DateTime? date = picker.SelectedDate;
            Title = date == null ? "" : date.Value.ToString("dd'/'MM'/'yyyy");
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Avanza al siguiente control en la pantalla
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
        /// Validar que los campos esten ingresados
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCampos()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(TxtPorcentajeHumedad.Text) || !TxtPorcentajeHumedad.Value.HasValue)
            {
                TxtPorcentajeHumedad.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.RegistrarHumedad_ValidacionHumedad;
                return resultado;
            }

            if (String.IsNullOrEmpty(DpFechaInicio.Text))
            {
                DpFechaInicio.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.RegistrarHumedad_ValidacionFecha;
                return resultado;
            }

            //Validar fechas
            if (ListaContratoHumedad.Any(contratoHumedadInfo => Convert.ToDateTime(DpFechaInicio.Text) == contratoHumedadInfo.FechaInicio))
            {
                DpFechaInicio.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.RegistrarHumedad_ValidacionFechaRegistrada;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Limpiar campos de la pantalla
        /// </summary>
        private void LimpiarCampos()
        {
            TxtPorcentajeHumedad.ClearValue(DecimalUpDown.ValueProperty);
            DpFechaInicio.Text = string.Empty;
        }
        #endregion
    }
}
