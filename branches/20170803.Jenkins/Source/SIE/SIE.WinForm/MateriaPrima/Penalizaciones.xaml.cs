using System.Collections.Generic;
using System.Windows;
using SIE.Services.Info.Info;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para Penalizaciones.xaml
    /// </summary>
    public partial class Penalizaciones : Window
    {
        public List<PenalizacionesInfo> listaPenalizacion;

        public Penalizaciones()
        {
            InitializeComponent();
        }

        private void Penalizaciones_OnLoaded(object sender, RoutedEventArgs e)
        {
            dgPenalizaciones.ItemsSource = listaPenalizacion;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
