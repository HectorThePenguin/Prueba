using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIE.Services.Info.Modelos;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para CierreDiaInventarioPAMensajes.xaml
    /// </summary>
    public partial class CierreDiaInventarioPAMensajes
    {
        private List<CierreDiaInventarioPAMensajesModel> listaProductosMensajes;

        public CierreDiaInventarioPAMensajes(List<CierreDiaInventarioPAMensajesModel> listaProductos)
        {
            InitializeComponent();
            listaProductosMensajes = listaProductos;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CierreDiaInventarioPAMensajesModel elemento = listaProductosMensajes.FirstOrDefault();
            if(elemento != null && elemento.EsAutorizacion)
            {
                gridSinConfiguracion.Visibility = Visibility.Hidden;
                gridSuperaMerma.Visibility = Visibility.Visible;
                gridSuperaMerma.ItemsSource = listaProductosMensajes;
            }
            else
            {
                gridSinConfiguracion.Visibility = Visibility.Visible;
                gridSuperaMerma.Visibility = Visibility.Hidden;
                gridSinConfiguracion.ItemsSource = listaProductosMensajes;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
