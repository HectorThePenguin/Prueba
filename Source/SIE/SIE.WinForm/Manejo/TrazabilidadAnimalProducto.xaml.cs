using System.Windows;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TrazabilidadAnimalProducto.xaml
    /// </summary>
    public partial class TrazabilidadAnimalProducto
    {
        private AnimalMovimientoInfo Contexto
        {
            set { DataContext = value; }
        }

        public TrazabilidadAnimalProducto(AnimalMovimientoInfo animalMovimientoInfo)
        {
            InitializeComponent();
            Contexto = animalMovimientoInfo;
            DGConsumoAnimal.ItemsSource = animalMovimientoInfo.ListaTratamientosAplicados;
        }

        public TrazabilidadAnimalProducto()
        {
            InitializeComponent();
        }

        private void BtnCerrar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
