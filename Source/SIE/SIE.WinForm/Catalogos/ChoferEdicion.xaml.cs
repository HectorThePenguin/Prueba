using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
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
    /// Interaction logic for ChoferEdicion.xaml
    /// </summary>
    public partial class ChoferEdicion
    {
        #region Propiedades

        private ChoferInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                   InicializaContexto();
                }
                return (ChoferInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;
        
        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ChoferEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad Chofer Existente
        /// </summary>
        /// <param name="choferInfo"></param>
        public ChoferEdicion(ChoferInfo choferInfo)
        {
            InitializeComponent();
            choferInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = choferInfo;
            if (Contexto.Boletinado)
            {
                txtObservacionesRegistro.IsEnabled = true;
                cmbEstatus.IsEnabled = false;
                LblObservacionesRequerida.Visibility = Visibility.Visible;
            }
        }

        #endregion Constructores

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Focus();
        }       
        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
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
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para regresar a la pantalla anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetras(e.Text);
        }

        /// <summary>
        /// Utilizaremos el evento TxtSoloLetrasYNumerosConPuntoPreviewTextInput para validar letras, acentos, numeros y puntos.
        /// </summary>
        /// <param name="sender">Objeto que implementa el método</param>
        /// <param name="e">Argumentos asociados</param>
        private void TxtSoloLetrasYNumerosConPuntoPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConPunto(e.Text);
        }

        /// <summary>
        /// Evento para controlar la propiedad Click del checkbox de observaciones
        /// </summary>
        /// <param name="sender">Objeto que implementa el método</param>
        /// <param name="e">Argumentos asociados</param>
        private void Boletinado_OnClick(object sender, RoutedEventArgs e)
        {
            if (chkBoletinado.IsChecked == true)
            {
                cmbEstatus.SelectedValue = EstatusEnum.Inactivo;
                cmbEstatus.IsEnabled = false;
                txtObservacionesRegistro.IsEnabled = true;
                txtObservacionesRegistro.Visibility = Visibility.Visible;
                LblObservacionesRequerida.Visibility = Visibility.Visible;
                txtObservacionesRegistro.Focus();
            }
            else if (chkBoletinado.IsChecked == false)
            {
                cmbEstatus.SelectedValue = EstatusEnum.Activo;
                cmbEstatus.IsEnabled = true;
                txtObservacionesRegistro.IsEnabled = false;
                txtObservacionesRegistro.Visibility = Visibility.Hidden;
                txtObservacionesRegistro.Text = string.Empty;
                LblObservacionesRequerida.Visibility = Visibility.Hidden;
            }
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
               new ChoferInfo
               {
                   UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                   Nombre = string.Empty,
                   ApellidoPaterno = string.Empty,
                   ApellidoMaterno = string.Empty,
                   Observaciones = string.Empty
               };
            txtObservacionesRegistro.IsEnabled = false;
            cmbEstatus.IsEnabled = true;
            LblObservacionesRequerida.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var choferPL = new ChoferPL();
                    choferPL.Guardar(Contexto);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.ChoferID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        InicializaContexto();
                        txtNombre.Focus();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Chofer_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Chofer_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ChoferEdicion_MsgNombreRequerida;
                    txtNombre.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtApellidoPaterno.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ChoferEdicion_MsgApellidoPaternoRequerida;
                    txtApellidoPaterno.Focus();
                }
                else if (chkBoletinado.IsChecked == true && string.IsNullOrWhiteSpace(txtObservacionesRegistro.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ChoferEdicion_MsgObservacionesRequerida;
                    txtObservacionesRegistro.Focus();
                }
                else
                {
                    int choferId = Extensor.ValorEntero(txtChoferId.Text);
                    string descripcion = string.Format("{0} {1} {2}", txtNombre.Text, txtApellidoPaterno.Text,
                                                       txtApellidoMaterno.Text);

                    var choferPL = new ChoferPL();
                    ChoferInfo chofer = choferPL.ObtenerPorDescripcion(descripcion);

                    if (chofer != null && (choferId == 0 || choferId != chofer.ChoferID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ChoferEdicion_MsgDescripcionExistente,
                                                chofer.ChoferID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }

        #endregion Métodos
    }
}

