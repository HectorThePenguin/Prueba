using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;
using System.ComponentModel;


namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecibirProductoAlmacenReplicaRango.xaml
    /// </summary>
    public partial class RecibirProductoAlmacenReplicaRango : Window
    {
        #region VARIABLES

        public List<string> Aretes;
        private bool _confirmar = true;
        public bool _guardar = false;
        private int _esSukarne;

        #endregion VARIABLES

        #region CONSTRUCTOR

        public RecibirProductoAlmacenReplicaRango(int esSukarne)
        {
            InitializeComponent();
            Aretes = new List<string>();
            _esSukarne = esSukarne;
            txtAreteInicial.Focus();
        }

        #endregion CONSTRUCTOR

        #region METODOS

        private void Salir()
        {
                MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                          MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _confirmar = false;
                    Close();
                }
        }

        private bool ValidaEstructuraArete(string arete)
        {
            var result = false;
            var correctoLongitud = true;
            var correctoDigitos = true;

            try
            {
                if (_esSukarne == 1)
                {
                    var logic = new ParametroGeneralPL();
                    var paramLongitud = logic.ObtenerPorClaveParametro(ParametrosEnum.LongitudAreteSK.ToString());
                    if (paramLongitud != null)
                    {
                        if (int.Parse(paramLongitud.Valor) != 0 && arete.Length != int.Parse(paramLongitud.Valor))
                        {
                            correctoLongitud = false;
                        }
                    }

                    if (correctoLongitud)
                    {
                        var paramDigito = logic.ObtenerPorClaveParametro(ParametrosEnum.DigitosIniAreteSK.ToString());
                        if (paramDigito != null)
                        {
                            if (paramDigito.Valor.Length > arete.Length || !paramDigito.Valor.Trim().Equals(arete.Substring(0, 4)) && !string.IsNullOrEmpty(paramDigito.Valor))
                            {
                                correctoDigitos = false;
                            }
                        }
                    }

                    if (correctoLongitud && correctoDigitos)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.RecibirProductoAlmacenReplicaAretes_ErrorValEstructArete, MessageBoxButton.OK,
                                MessageImage.Error);
            }

            return result;
        }

        private bool ValidarAretes()
        {
            var result = false;
            if (txtAreteInicial.Text != string.Empty)
            {
                if (txtAreteFinal.Text != string.Empty)
                {
                    if (Convert.ToInt64(txtAreteInicial.Text) <= Convert.ToInt64(txtAreteFinal.Text))
                    {
                        if ( ((Convert.ToInt64(txtAreteFinal.Text) - Convert.ToInt64(txtAreteInicial.Text)) + 1) <= 100000)
                        {
                            result = true;
                        }
                        else
                        {
                            SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.RecibirProductoAlmacenReplicaRango_MsjRangoInvalido, MessageBoxButton.OK,
                                                            MessageImage.Warning);
                            txtAreteInicial.Focus();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecibirProductoAlmacenReplicaRango_MsjRangoInvalido, MessageBoxButton.OK,
                                                        MessageImage.Warning);
                        txtAreteInicial.Focus();
                    }
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecibirProductoAlmacenReplicaRango_MsjAreteFinal, MessageBoxButton.OK,
                                                        MessageImage.Warning);
                    txtAreteFinal.Focus();
                }
            }
            else
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.RecibirProductoAlmacenReplicaRango_MsjAreteInicial, MessageBoxButton.OK,
                                                        MessageImage.Warning);
                txtAreteInicial.Focus();
            }

            return result;
        }

        private void CalcularCantidadAretes()
        {
            var cantidad = string.Empty;
            if (txtAreteInicial.Text != string.Empty)
            {
                if (txtAreteFinal.Text != string.Empty)
                {
                    if (Convert.ToInt64(txtAreteInicial.Text) <= Convert.ToInt64(txtAreteFinal.Text))
                    {
                        cantidad =
                            ((Convert.ToInt64(txtAreteFinal.Text) - Convert.ToInt64(txtAreteInicial.Text))+1).ToString();
                    }
                }
            }

            txtTotal.Text = cantidad;
        }

        private void Guardar()
        {

            try
            {
                var pagoEdicion = new RecibirProductoAlmacenReplicaConfirmacion(Convert.ToInt64(txtAreteInicial.Text), Convert.ToInt64(txtAreteFinal.Text));
                pagoEdicion.Left = (ActualWidth - pagoEdicion.Width) / 2;
                pagoEdicion.Top = ((ActualHeight - pagoEdicion.Height) / 2);
                pagoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                pagoEdicion.ShowDialog();
                if (pagoEdicion._esCorrecto)
                {
                    var inicio = Convert.ToInt64(txtAreteInicial.Text.Trim());
                    var fin = Convert.ToInt64(txtAreteFinal.Text.Trim());

                    for (long i = inicio; i <= fin; i++)
                    {
                        Aretes.Add(i.ToString());
                    }
                    _guardar = true;
                    _confirmar = false;
                    Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion METODOS

        #region EVENTOS

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(ValidarAretes())
            {
                if (ValidaEstructuraArete(txtAreteInicial.Text.Trim()))
                {
                    Guardar();
                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RecibirProductoAlmacenReplicaAretes_ValEstructArete, MessageBoxButton.OK,
                            MessageImage.Warning);
                }
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Salir();
        }

        private void txtAreteInicial_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int64 valor = 0;
                bool result = Int64.TryParse(txtAreteInicial.Text, out valor);
                if (!result)
                {
                    txtAreteInicial.Text = string.Empty;
                }
            }
            catch (Exception)
            {
               txtAreteInicial.Text = string.Empty;
               txtAreteInicial.Focus();
            }
            CalcularCantidadAretes();
        }

        private void txtAreteFinal_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int64 valor = 0;
                bool result = Int64.TryParse(txtAreteFinal.Text, out valor);
                if (!result)
                {
                    txtAreteFinal.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                txtAreteFinal.Text = string.Empty;
                txtAreteFinal.Focus();
            }
            CalcularCantidadAretes();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_confirmar)
            {
                MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                          MessageImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion EVENTOS

    }
}
