using System.ComponentModel;
using System.Windows;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CentroCostoEdicionUsuario.xaml
    /// </summary>
    public partial class CentroCostoEdicionUsuario
    {
        /// <summary>
        /// Propiedad donde se almacenan los objetos que utiliza la pantalla
        /// </summary>
        private CentroCostoUsuarioInfo CentroCostoUsuario
        {
            get
            {
                return (CentroCostoUsuarioInfo)DataContext;
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

        public CentroCostoEdicionUsuario(CentroCostoUsuarioInfo centroCostoUsuario)
        {
            CentroCostoUsuario = centroCostoUsuario;
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
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private bool ValidarUsuario()
        {
            if (CentroCostoUsuario.Usuario == null || CentroCostoUsuario.Usuario.UsuarioID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.CentroCostoEdicionUsuario_CampoUsuario, MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }
    }
}
