using System.Collections.Generic;
using System.Windows;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TrazabilidadAnimalConsumo.xaml
    /// </summary>
    public partial class TrazabilidadAnimalConsumo
    {
        public TrazabilidadAnimalConsumo()
        {
            InitializeComponent();
        }

        public TrazabilidadAnimalConsumo(List<AnimalConsumoInfo> listaConsumoAnimal)
        {
            InitializeComponent();
            DgConsumosAnimal.ItemsSource = listaConsumoAnimal;
        }

        private void BtnCerrar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
