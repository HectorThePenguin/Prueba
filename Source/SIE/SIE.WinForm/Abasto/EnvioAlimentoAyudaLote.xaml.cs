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
using System.Windows.Shapes;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Abasto
{
    /// <summary>
    /// Lógica de interacción para EnvioAlimentoAyudaLote.xaml
    /// </summary>
    public partial class EnvioAlimentoAyudaLote : Window
    {
        public List<AlmacenInventarioLoteInfo> listaLotes;

        public EnvioAlimentoAyudaLote()
        {
            InitializeComponent();
            this.listaLotes = new List<AlmacenInventarioLoteInfo>();
        }

        private void EnvioAlimentoAyudaLote_Loaded(object sender, RoutedEventArgs e)
        {
            dgLotes.ItemsSource = listaLotes;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
