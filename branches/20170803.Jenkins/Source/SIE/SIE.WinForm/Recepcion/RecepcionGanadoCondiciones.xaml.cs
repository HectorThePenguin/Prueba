using System.Windows;
using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for RecepcionGanadoCondiciones.xaml
    /// </summary>
    public partial class RecepcionGanadoCondiciones
    {
        #region PROPIEDADES

        /// <summary>
        /// Lista de Las Condiciones en las que
        /// Entra el Ganado
        /// </summary>
        public IList<EntradaCondicionInfo> EntradasCondicion { get; set; }

        /// <summary>
        /// Cantidad de Cabezas Recibidas
        /// </summary>
        public int Cabezas { get; set; }

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public RecepcionGanadoCondiciones()
        {
            InitializeComponent();

            EntradasCondicion = new List<EntradaCondicionInfo>();
        }

        public RecepcionGanadoCondiciones(IList<EntradaCondicionInfo> entradasCondicion, int cabezasRecibidas)
        {
            InitializeComponent();

            EntradasCondicion = entradasCondicion;
            Cabezas = cabezasRecibidas;
            AsignaCondiciones();
        }

        #endregion CONSTRUCTORES

        #region METODOS

        /// <summary>
        /// Asigna la lista de condiciones al Grid
        /// </summary>
        private void AsignaCondiciones()
        {
            gridCondiciones.ItemsSource = EntradasCondicion;
        }

        #endregion METODOS

        #region EVENTOS
       
        /// <summary>
        /// Cierra la Ventana Actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion EVENTOS       
    }
}
