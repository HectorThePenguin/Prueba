using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Lógica de interacción para TrazabilidadAnimalDuplicado.xaml
    /// </summary>
    public partial class TrazabilidadAnimalDuplicado
    {
        public AnimalInfo AnimalSeleccionado = null;
        public TrazabilidadAnimalDuplicado(List<AnimalInfo> animalesDuplicados)
        {
            InitializeComponent();
            DGAreteDuplicado.ItemsSource = animalesDuplicados;
        }

        public TrazabilidadAnimalDuplicado()
        {
            InitializeComponent();
        }

        private void BtnAceptar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGAreteDuplicado.SelectedIndex == -1)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.TrazabilidadAnimal_AreteDuplicadoSeleccionar,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                }
                else
                {
                    var row = (DataGridRow)DGAreteDuplicado.ItemContainerGenerator.ContainerFromIndex(DGAreteDuplicado.SelectedIndex);
                    if (row.IsSelected)
                    {
                        AnimalSeleccionado = new AnimalInfo();
                        AnimalSeleccionado = ((AnimalInfo)(row.Item));
                    }
                    Close();
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.TrazabilidadAnimal_ErrorConsumo,
                                MessageBoxButton.OK,
                                MessageImage.Error);
            }
        }

        /// <summary>
        /// Cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
