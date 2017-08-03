using System.Collections.Generic;
using System.Windows;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TrazabilidadAnimalCostosAbasto.xaml
    /// </summary>
    public partial class TrazabilidadAnimalCostosAbasto : Window
    { 
        /// <summary>
        /// Inicializa la información a mostrar en la pantalla
        /// </summary>
        /// <param name="listaConsumoAbastoAnimal">Lista de costos aplicados al animal que se mostrara en pantalla</param>
        public TrazabilidadAnimalCostosAbasto(List<AnimalCostoInfo> listaCostosAbastoAnimal)
        {
            InitializeComponent();
            DgCostosAnimal.ItemsSource = listaCostosAbastoAnimal;
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
