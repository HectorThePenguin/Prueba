using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para RegistrarCompraParcial.xaml
    /// </summary>
    public partial class RegistrarCompraParcial
    {

        #region Atributos
        private decimal toneladasContrato = 0;
        private decimal precioContrato = 0;
        private decimal toleranciaContrato = 0;
        public List<ContratoParcialInfo> listaContratoParcial = new List<ContratoParcialInfo>();
        public ContratoInfo contratoInfo = new ContratoInfo();
        public bool Nuevo;
        public int CostoId = 0;
        public int TipoCompraId;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public RegistrarCompraParcial(List<ContratoParcialInfo> listadoContratoParcial, ContratoInfo contratoInfoP, bool nuevoContrato, decimal toneladas, decimal precioC, decimal toleranciaC, int tipoCompraID)
        {
            InitializeComponent();
            toneladasContrato = toneladas;
            precioContrato = precioC;
            toleranciaContrato = toleranciaC;
            Nuevo = nuevoContrato;
            contratoInfo = contratoInfoP;
            listaContratoParcial = listadoContratoParcial;
            TipoCompraId = tipoCompraID;
        }
        #endregion

        #region Propiedades
        public List<ContratoParcialInfo> listaContratoParcialR
        {
            get { return listaContratoParcial; }
            set { listaContratoParcial = value; }
        }
        #endregion 

        #region Eventos

        #endregion

        #region Metodos

        #endregion

        /// <summary>
        /// Define la pantalla registrar compra parcial
        /// </summary>
        private void DefinirPantalla()
        {
            if (listaContratoParcial != null)
            {
                GridContratoParcial.ItemsSource = null;
                GridContratoParcial.ItemsSource = listaContratoParcial;
            }
            else
            {
                listaContratoParcial = new List<ContratoParcialInfo>();
            }

            if (TipoCompraId == TipoContratoEnum.BodegaTercero.GetHashCode())
            {
                TxtImporte.IsEnabled = false;
                TxtImporte.Value = precioContrato;
            }

            if (Nuevo) return;
            if (contratoInfo.Activo != EstatusEnum.Inactivo) return;
        }

        /// <summary>
        /// Evento click de BtnAgregar
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
                    listaContratoParcial.Add(new ContratoParcialInfo(){Importe =  TxtImporte.Value.HasValue ? TxtImporte.Value.Value : 0, Cantidad =  TxtToneladas.Value.HasValue ? TxtToneladas.Value.Value : 0, FechaCreacion = DateTime.Today});
                    GridContratoParcial.ItemsSource = null;
                    GridContratoParcial.ItemsSource = listaContratoParcial;
                    LimpiarCampos();
                    TxtToneladas.Focus();
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
        /// Valida los campos toneladas e importe
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCampos()
        {
            var resultado = new ResultadoValidacion();

            if ((String.IsNullOrEmpty(TxtToneladas.Text) || !TxtToneladas.Value.HasValue))
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.RegistrarCompraParcial_ValidacionToneladas;
                return resultado;
            }

            if (String.IsNullOrEmpty(TxtImporte.Text) || !TxtImporte.Value.HasValue)
            {
                TxtImporte.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.RegistrarCompraParcial_ValidacionImporte;
                return resultado;
            }

            //Validar toneladas contrato
            var toneladasIngresadas = TxtToneladas.Value.Value;
            var toneladasAgregadas = listaContratoParcial.Sum(contratoParcialInfo => contratoParcialInfo.Cantidad);
            var toneladasTotales = toneladasIngresadas + toneladasAgregadas;
            var toneladasPermitidas = toneladasContrato + ((toneladasContrato/100)*toleranciaContrato);
            if (toneladasPermitidas < toneladasTotales)
            {
                TxtToneladas.Focus();
                resultado.Resultado = false;
                resultado.Mensaje = Properties.Resources.CrearContrato_CapturarValidacionToneladasContrato;
                return resultado;
            }

            resultado.Resultado = true;
            return resultado;
        }

        //Limpiar campos
        private void LimpiarCampos()
        {
            TxtToneladas.Text = string.Empty;
            if (TipoCompraId != TipoContratoEnum.BodegaTercero.GetHashCode())
            {
                TxtImporte.Text = string.Empty;
            }
        }

        /// <summary>
        /// Salir de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSalir_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Se ejecuta al cargar el form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarCompraParcial_OnLoaded(object sender, RoutedEventArgs e)
        {
            DefinirPantalla();
            TxtToneladas.Focus();
        }

        /// <summary>
        /// Se ejecuta al dar enter en la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrarCompraParcial_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

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
    }
}
