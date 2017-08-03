using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Services.Info.Info;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para DiferenciasDeInventarioProductosSinConfiguracion.xaml
    /// </summary>
    public partial class DiferenciasDeInventarioProductosSinConfiguracion
    {
        #region Propiedades
        List<DiferenciasDeInventariosInfo> listaProductosNoConfigurados = new List<DiferenciasDeInventariosInfo>();
        #endregion 

        #region Constructor
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="listaDiferenciasNoConfiguradas"></param>
        public DiferenciasDeInventarioProductosSinConfiguracion(List<DiferenciasDeInventariosInfo> listaDiferenciasNoConfiguradas)
        {
            InitializeComponent();
            listaProductosNoConfigurados = listaDiferenciasNoConfiguradas;
            CargarGrid();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento que cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Carga los productos sin configuracion en el grid
        /// </summary>
        private void CargarGrid()
        {
            GridDiferenciasDeInventarios.ItemsSource = listaProductosNoConfigurados;
        }
        #endregion
    }
}
