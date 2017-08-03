using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIE.Base.Vista
{
    public abstract class ControlBase : UserControl
    {
        BackgroundWorker worker;
        Action doWork;
        Action complete;
        protected ControlBase()
        {
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            KeyDown += Window_KeyDown;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            doWork();
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (complete != null)
            {
                complete();
            }
        }

        protected void EjecutarBackground(Action doWork, Action complete = null)
        {
            this.doWork = doWork;
            this.complete = complete;
            worker.RunWorkerAsync();
        }
        
        protected void MostrarCentrado(Window ventana)
        {
            ventana.Left = (ActualWidth - ventana.Width)/2;
            ventana.Top = ((ActualHeight - ventana.Height)/2) + 132;
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