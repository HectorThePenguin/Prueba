using System;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.ComponentModel;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecibirProductoAlmacenReplicaConfirmacion.xaml
    /// </summary>
    public partial class RecibirProductoAlmacenReplicaConfirmacion : Window
    {

        #region VARIABLES

        private bool _confirmar = true;
        public bool _esCorrecto = false;
        private readonly long _rangoInicial = 0;
        private readonly long _rangoFinal = 0;

        #endregion VARIABLES

        #region CONSTRUCTOR

        public RecibirProductoAlmacenReplicaConfirmacion(long rangoInicial, long rangoFinal)
        {
            InitializeComponent();
            _rangoInicial = rangoInicial;
            _rangoFinal = rangoFinal;
        }

        #endregion CONSTRUCTOR

        #region METODOS

        private bool ValidarArete()
        {
            var result = false;
            try
            {
                if (txtArete.Text.Trim() == string.Empty)
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.RecibirProductoAlmacenReplicaConfirmacion_CapturaArete, MessageBoxButton.OK,
                                 MessageImage.Warning);
                    txtArete.Focus();
                }
                else
                {
                    var arete = long.Parse(txtArete.Text);
                    if (arete < _rangoInicial || arete > _rangoFinal)
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RecibirProductoAlmacenReplicaConfirmacion_RangoInvalido, MessageBoxButton.OK,
                                MessageImage.Warning);
                        txtArete.Focus();
                    }
                    else
                    {
                        _esCorrecto = true;
                        _confirmar = false;
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        #endregion METODOS

        #region EVENTOS

        private void txtArete_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int64 valor = 0;
                bool result = Int64.TryParse(txtArete.Text, out valor);
                if (!result)
                {
                    txtArete.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                txtArete.Text = string.Empty;
                txtArete.Focus();
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if(ValidarArete())
            {
                Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_confirmar)
            {
                var result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.RecibirProductoAlmacenReplicaConfirmacion_CancelarConfirmacion, MessageBoxButton.OK,
                                MessageImage.Warning);
                }
            }
        }

        #endregion EVENTOS

    }
}
