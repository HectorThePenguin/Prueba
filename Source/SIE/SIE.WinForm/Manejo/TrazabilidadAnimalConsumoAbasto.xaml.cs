using System.Collections.Generic;
using System.Windows;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for TrazabilidadAnimalConsumoAbasto.xaml
    /// </summary>
    public partial class TrazabilidadAnimalConsumoAbasto : Window
    {
        /// <summary>
        /// Inicializa la información a mostrar en la pantalla
        /// </summary>
        /// <param name="listaConsumoAbastoAnimal">Lista de consumo del animal que se mostrara en pantalla</param>
        public TrazabilidadAnimalConsumoAbasto(List<AnimalConsumoInfo> listaConsumoAbastoAnimal)
        {
            InitializeComponent();
            DgConsumosAnimalAbasto.ItemsSource = listaConsumoAbastoAnimal;
        }

        /// <summary>
        /// Cierra la pantalla
        /// </summary>
        /// <param name="sender">Control que invoco el evento</param>
        /// <param name="e">Parametros del evento</param>
        private void BtnCerrar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
