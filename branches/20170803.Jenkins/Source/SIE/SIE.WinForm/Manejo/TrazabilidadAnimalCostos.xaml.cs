using System.Collections.Generic;
using System.Windows;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TrazabilidadAnimalCostos.xaml
    /// </summary>
    public partial class TrazabilidadAnimalCostos
    {
        public TrazabilidadAnimalCostos(List<AnimalCostoInfo> listaCostosAnimal)
        {
            InitializeComponent();
            DgCostosAnimal.ItemsSource = listaCostosAnimal;
        }

        public TrazabilidadAnimalCostos()
        {
            InitializeComponent();
        }

        private void BtnCerrar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
