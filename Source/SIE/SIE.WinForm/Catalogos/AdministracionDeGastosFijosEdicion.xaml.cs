using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AdministracionDeGastosFijosEdicion.xaml
    /// </summary>
    public partial class AdministracionDeGastosFijosEdicion
    {

        #region Propiedades

        /// <summary>
        /// Contexto de información de la ventana
        /// </summary>
        private AdministracionDeGastosFijosInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AdministracionDeGastosFijosInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Variable para definir si se mostrará un mensaje de confirmación al salir de la ventana
        /// </summary>
        private bool confirmarCierre = true;

        #endregion

        #region Constructores

        /// <summary>
        /// Inicializador de la pantalla de registro
        /// </summary>
        public AdministracionDeGastosFijosEdicion()
        {
            InitializeComponent();
            InicializaContexto();

        }

        /// <summary>
        /// Inicializador de la pantalla de edición
        /// </summary>
        /// <param name="gastosFijos"></param>
        public AdministracionDeGastosFijosEdicion(AdministracionDeGastosFijosInfo gastosFijos)
        {
            InitializeComponent();
            InicializaContexto();
            Contexto = gastosFijos;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento para cuando se carga la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Validar número decimal limitado a 6 enteros y 2 decimales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSoloNumerosDecimalesPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var importeTxt = (TextBox) sender;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, importeTxt.Text);
        }

        /// <summary>
        /// Validar solo letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSoloLetrasYNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        /// <summary>
        /// Evento para el botón de guardado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardarClick(object sender, RoutedEventArgs e)
        {
            Contexto.Importe = Convert.ToDecimal(Contexto.Importe.ToString().Replace(" ", string.Empty));
            if (ValidarCampos())
            {
                List<AdministracionDeGastosFijosInfo> gastos = ValidarDescripcion();
                if (gastos != null)
                {
                    if (gastos[0].GastoFijoID == Convert.ToInt32(txtGastosId.Text))
                    {
                        Guardar();
                    }
                    else
                    {
                        string msg = String.Format(
                            Properties.Resources.AdministracionDeGastosFijosEdicion_MsgGastoExistente, gastos[0].GastoFijoID);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], msg,
                            MessageBoxButton.OK, MessageImage.Warning);
                        txtDescripcion.Focus();
                    }
                }
                else
                {
                    Guardar();
                }
            }
        }

        /// <summary>
        /// Evento para el botón de cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelarClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmarCierre)
            {
                MessageBoxResult result = SkMessageBox.Show(
                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo, MessageImage.Question);
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

        #endregion

        #region Metodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
               new AdministracionDeGastosFijosInfo
               {
                   GastoFijoID = 0,
                   Descripcion = string.Empty,
                   Importe = 0,
                   Activo = EstatusEnum.Activo
               };
        }

        /// <summary>
        /// Valida que no existan campos requeridos vacíos o con formatos incorrectos.
        /// </summary>
        /// <returns></returns>
        public bool ValidarCampos()
        {
            if (txtDescripcion.Text.Trim() == string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijosEdicion_MsgDescripcion, MessageBoxButton.OK,
                    MessageImage.Warning);
                txtDescripcion.Focus();
                return false;
            }
            if (txtImporte.Text.Trim() == string.Empty || txtImporte.Text.Trim() == ".")
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijosEdicion_MsgImporte, MessageBoxButton.OK,
                    MessageImage.Warning);
                txtImporte.Focus();
                return false;
            }
            if (Contexto.Importe <= 0 || Contexto.Importe >= 1000000)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijosEdicion_MsgImporteMenor, MessageBoxButton.OK,
                    MessageImage.Warning);
                txtImporte.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Crea o Actualiza un gasto fijo
        /// </summary>
        public void Guardar()
        {
            Contexto.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            try
            {
                var gastosPL = new AdministracionDeGastosFijosPL();
                gastosPL.Guardar(Contexto);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijosEdicion_MsgExito, MessageBoxButton.OK,
                    MessageImage.Correct);
                if (Contexto.GastoFijoID != 0)
                {
                    confirmarCierre = false;
                    Close();
                }
                else
                {
                    LimpiarPantalla();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijosEdicion_MsgErrorDescripcion, MessageBoxButton.OK,
                    MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpia los campos de la pantalla y asigna el focus al campo descripcion
        /// </summary>
        public void LimpiarPantalla()
        {
            txtDescripcion.Text = string.Empty;
            txtImporte.Text = string.Empty;
            cmbEstatus.SelectedItem = EstatusEnum.Activo;
            Contexto.Descripcion = string.Empty;
            Contexto.Importe = 0;
            Contexto.Activo = EstatusEnum.Activo;
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Valida que la descripción del gasto fijo a registrar/editar no exista en la bd
        /// </summary>
        /// <returns></returns>
        public List<AdministracionDeGastosFijosInfo> ValidarDescripcion()
        {
            List<AdministracionDeGastosFijosInfo> resultado = null;
            try
            {
                Logger.Info();
                var gastosPL = new AdministracionDeGastosFijosPL();

                resultado = gastosPL.ValidarDescripcion(Contexto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AdministracionDeGastosFijosEdicion_MsgErrorDescripcion, MessageBoxButton.OK,
                    MessageImage.Error);
            }
            

            return resultado;
        }

        #endregion
    }
}
