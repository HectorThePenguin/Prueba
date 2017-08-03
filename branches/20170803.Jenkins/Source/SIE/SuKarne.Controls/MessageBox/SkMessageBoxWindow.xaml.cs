using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox.Helper;

namespace SuKarne.Controls.MessageBox
{
    /// <summary>
    /// Interaction logic for SkMessageBoxWindow.xaml
    /// </summary>
    public partial class SkMessageBoxWindow
    {
        [ThreadStatic] private static SkMessageBoxWindow _messageBoxWindow;

        private SkMessageBoxViewModel _viewModel;

        public SkMessageBoxWindow()
        {
            InitializeComponent();
        }

        public static MessageBoxResult Show(Action<Window> setOwner, string messageBoxText,
                                            MessageBoxButton button,
                                            MessageImage icon,
                                            MessageBoxResult defaultResult,
                                            MessageBoxOptions options)
        {
            _messageBoxWindow = new SkMessageBoxWindow();
            setOwner(_messageBoxWindow);
            PlayMessageBeep(icon);
            _messageBoxWindow._viewModel = new SkMessageBoxViewModel(_messageBoxWindow, messageBoxText, button, icon,
                                                                     defaultResult, options);
            _messageBoxWindow.DataContext = _messageBoxWindow._viewModel;
            _messageBoxWindow.ShowDialog();
            return _messageBoxWindow._viewModel.Result;
        }

        private static void PlayMessageBeep(MessageImage icon)
        {
            switch (icon)
            {
                case MessageImage.Close:
                case MessageImage.Error:
                    SystemSounds.Exclamation.Play();
                    break;
                case MessageImage.Question:
                    SystemSounds.Question.Play();
                    break;
                case MessageImage.Correct:
                    SystemSounds.Asterisk.Play();
                    break;
                case MessageImage.Stop:
                    SystemSounds.Hand.Play();
                    break;
                default:
                    SystemSounds.Beep.Play();
                    break;
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            // removes the application icon from the window top left corner
            // this is different than just hiding it
            WindowHelper.RemoveIcon(this);

            switch (_viewModel.Options)
            {
                case MessageBoxOptions.None:
                    break;

                case MessageBoxOptions.RightAlign:
                    WindowHelper.SetRightAligned(this);
                    break;

                case MessageBoxOptions.RtlReading:
                    break;

                case MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading:
                    break;
            }

            // disable close button if needed and remove resize menu items from the window system menu
            var systemMenuHelper = new SystemMenuHelper(this);

            if (_viewModel.ButtonOption == MessageBoxButton.YesNo)
            {
                systemMenuHelper.DisableCloseButton = true;
            }

            systemMenuHelper.RemoveResizeMenu = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                _viewModel.EscapeCommand.Execute(null);
            }
            else if (e.Key == Key.Enter)
            {
                _viewModel.EnterCommand.Execute(null);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _viewModel.CloseCommand.Execute(null);
        }
        /// <summary>
        /// Evento para carga de MessageBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }
        /// <summary>
        /// Evento para mover el Message Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}