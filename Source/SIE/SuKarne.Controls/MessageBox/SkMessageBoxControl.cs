using System.Windows;
using System.Windows.Controls;

namespace SuKarne.Controls.MessageBox
{
    public class SkMessageBoxControl : Control
    {
        /// <summary>
        /// Constructo por fefecto
        /// </summary>
        static SkMessageBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SkMessageBoxControl),
                                                     new FrameworkPropertyMetadata(typeof (SkMessageBoxControl)));
        }
    }
}