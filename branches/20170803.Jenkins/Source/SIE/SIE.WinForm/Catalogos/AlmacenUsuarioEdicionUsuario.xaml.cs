using System.ComponentModel;
using System.Windows;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para AlmacenUsuarioEdicionUsuario.xaml
    /// </summary>
    public partial class AlmacenUsuarioEdicionUsuario
    {
        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private AlmacenUsuarioInfo AlmacenUsuario
        {
            get
            {
                return (AlmacenUsuarioInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        public bool ConfirmaSalir = true;

        public AlmacenUsuarioEdicionUsuario(AlmacenUsuarioInfo almacenUsuario)
        {
            AlmacenUsuario = almacenUsuario;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            skAyudaUsuario.ObjetoNegocio = new UsuarioPL();
        }

        private void btnAceptar_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ValidarUsuario())
            {
                return;
            }
            ConfirmaSalir = false;
            Close();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (ConfirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Metodo para validar el Usuario
        /// </summary>
        private bool ValidarUsuario()
        {
            if (AlmacenUsuario.Usuario == null || AlmacenUsuario.Usuario.UsuarioID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.AlmacenUsuarioEdicionUsuario_CampoUsuario, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }
    }
}
