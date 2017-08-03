using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SIE.Base.Controles
{
    public class SkTextBox : TextBox
    {
        public static DependencyProperty PropiedadRequerido = DependencyProperty.Register("Requerido",
                                                                                          typeof (bool),
                                                                                          typeof (SkTextBox),
                                                                                          new PropertyMetadata(
                                                                                              OnRequeridoChanged));

        private bool _esMarcado;


        public SkTextBox()
        {
            //Loaded += (s, ea) => MuestraIncorrecto();
        }

        public bool Requerido
        {
            get { return (bool) GetValue(PropiedadRequerido); }
            set { SetValue(PropiedadRequerido, value); }
        }

        private static void OnRequeridoChanged(DependencyObject sender, DependencyPropertyChangedEventArgs ea)
        {
            //var textBox = sender as SkTextBox;
            //if (textBox == null) return;
            //textBox.MuestraIncorrecto();
        }

        private void MuestraIncorrecto()
        {
            if (Requerido)
            {
                if (Text == string.Empty)
                {
                    _esMarcado = true;
                    var linearBrush = new LinearGradientBrush();
                    linearBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
                    BorderBrush = linearBrush;
                }
            }
        }

        private void OcultaIncorrecto()
        {
            if (_esMarcado)
            {
                _esMarcado = false;
                var linearBrush = new LinearGradientBrush();
                linearBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
                linearBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.0));
                linearBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
                BorderBrush = linearBrush;
            }
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            OcultaIncorrecto();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            MuestraIncorrecto();
        }
    }
}