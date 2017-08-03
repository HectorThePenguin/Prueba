using System.Windows;
using System.Windows.Input;

namespace SIE.WinForm.Controles
{
    /// <summary>
    /// Lógica de interacción para Titulo.xaml
    /// </summary>
    public partial class Titulo
    {        
        public Titulo()
        {
            InitializeComponent();
        }

        public Visibility VisibleCerrar { get; set; }    

        public string TextoTitulo { get; set; }    

        private void Titulo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window ventanaPadre = Window.GetWindow(this);
            if (ventanaPadre != null)
            {
                ventanaPadre.DragMove();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Window ventanaPadre = Window.GetWindow(this);
            if (ventanaPadre != null)
            {
                ventanaPadre.Close();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnCerrar.Visibility = VisibleCerrar;
            lblTitulo.Content = TextoTitulo;
        }
    }
}