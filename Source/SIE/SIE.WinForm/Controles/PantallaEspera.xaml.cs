using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIE.WinForm.Controles
{
    /// <summary>
    /// Lógica de interacción para PantallaEspera.xaml
    /// </summary>
    public partial class PantallaEspera : UserControl
    {
        public PantallaEspera()
        {
            InitializeComponent();
        }

        public string Mensaje { get; set; }
        public string Titulo { get; set; }

        public void Cerrar()
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void PantallaEspera_load(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
