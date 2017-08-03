using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System.Collections.ObjectModel;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Lógica de interacción para RecepcionGanadoPesosUnificados.xaml
    /// </summary>
    public partial class RecepcionGanadoPesosUnificados
    {
        #region VARIABLES

        /// <summary>
        /// Coleccion de pesos unificados
        /// </summary>
        public ObservableCollection<PesoUnificadoInfo> PesosUnificados { get; set; }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion VARIABLES

        #region CONSTRUCTORES

        public RecepcionGanadoPesosUnificados()
        {
            InitializeComponent();
        }

        public RecepcionGanadoPesosUnificados(ObservableCollection<PesoUnificadoInfo> pesosUnificados)
        {
            InitializeComponent();
            PesosUnificados = pesosUnificados;
        }

        #endregion CONSTRUCTORES

        #region METODOS

        private void AsignarColeccionGrid()
        {
            gridDatos.ItemsSource = PesosUnificados;
        }

        private void Guardar()
        {
            bool validaGuardar = ValidarCampos();
            if (!validaGuardar)
            {
                confirmaSalir = false;
                Close();
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = PesosUnificados.Any(x => x.PesoOrigen == 0);
            if (camposValidos)
            {
                SkMessageBox.Show(this, Properties.Resources.RecepcionGanadoPesosUnificados_PesoMayorCero,
                                  MessageBoxButton.OK,
                                  MessageImage.Stop);
            }
            return camposValidos;
        }

        #endregion METODOS

        #region EVENTOS

        private void RecepcionGanadoPesosUnificadosLoaded(object sender, RoutedEventArgs e)
        {
            AsignarColeccionGrid();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar,
                                                            MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    PesosUnificados = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void dudPesoOrigenValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (gridDatos.CurrentCell.Item == DependencyProperty.UnsetValue)
                {
                    return;
                }
                var elemento = (PesoUnificadoInfo)gridDatos.CurrentCell.Item;
                if (elemento.EntradaGanado.EntradaGanadoID == 0)
                {
                    return;
                }
                elemento.EntradaGanado.PesoTara = elemento.PesoOrigen - elemento.EntradaGanado.PesoBruto;
                elemento.EntradaGanado.PesoLlegada =
                    Convert.ToInt32(elemento.EntradaGanado.PesoBruto - elemento.EntradaGanado.PesoTara);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void txtPesoBruto_LostFocus(object sender, RoutedEventArgs e)
        {
            PesoUnificadoInfo pesoTotal =
                                    PesosUnificados.FirstOrDefault(id => id.EntradaGanado.EntradaGanadoID == 0);
            pesoTotal.PesoOrigen = 0;
            pesoTotal.PesoOrigen = PesosUnificados.Sum(peso => peso.PesoOrigen);
        }

        private void DecimalKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion EVENTOS

    }
}
 