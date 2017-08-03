using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
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
using System.Windows.Shapes;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para CancelacionMovimientoJustificacion.xaml
    /// </summary>
    public partial class CancelacionMovimientoJustificacion
    {
        public string justificacion;
        public CancelacionMovimientoJustificacion()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtJustificacion.Text))
            {
                justificacion = txtJustificacion.Text;
                this.Close();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CancelarMovimiento_CapturarJustificacion, MessageBoxButton.OK,
                MessageImage.Stop);
            }
        }

        private void btnLimpiar_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
