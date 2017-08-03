using System;
using System.Windows;
using System.Windows.Input;

namespace SIE.Base.Vista
{
    public class VistaBase : Window
    {

        protected VistaBase()
        {
            KeyDown += Window_KeyDown;
        }

        protected void MostrarCentrado(Window ventana)
        {
            ventana.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ventana.Owner = Application.Current.Windows[1];
            ventana.ShowDialog();
        }

        /// <summary>
        /// evento cuando se preciona cualquier tecla en el formulario 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        /// <summary>
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        protected bool ValidaExcepcionTiempoEspera(Exception exception)
        {
            var tiempoEspera =
                exception.InnerException.Message.IndexOf("conexión", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                exception.InnerException.Message.IndexOf("tiempo de espera del semáforo",
                                                         StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                exception.InnerException.Message.IndexOf("provider: TCP Provider, error: 0",
                                                         StringComparison.InvariantCultureIgnoreCase) >= 0;
            return tiempoEspera;
        }
    }
}