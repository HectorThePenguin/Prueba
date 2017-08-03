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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIE.Base.Log;

namespace SIE.WinForm.Calidad
{
    /// <summary>
    /// Lógica de interacción para ChecklistAlmidonHeces.xaml
    /// </summary>
    public partial class ChecklistAlmidonHeces
    {
        public ChecklistAlmidonHeces()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento click del boton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var registrarChecklistAlmidonHeces = new RegistrarChecklistAlmidonHeces();
                MostrarCentrado(registrarChecklistAlmidonHeces);
                //Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
