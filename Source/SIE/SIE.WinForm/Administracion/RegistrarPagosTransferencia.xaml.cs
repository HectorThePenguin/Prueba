using System;
using System.Windows;
using SIE.Services.Info.Constantes;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SIE.Services.Info.Info;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.PL;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.ComponentModel;

namespace SIE.WinForm.Administracion
{
    public partial class RegistrarPagosTransferencia
    {
        #region VARIABLES LOCALES
        private PagoTransferenciaInfo _pago = new PagoTransferenciaInfo();
        public int folio = 0;
        private bool confirmar = true;
        #endregion VARIABLES LOCALES

        #region CONSTRUCTOR
        public RegistrarPagosTransferencia(PagoTransferenciaInfo pago)
        {
            InitializeComponent();
            _pago = pago;
            dtpFecha.SelectedDate = _pago.Fecha;
            dtpFechaPago.SelectedDate = DateTime.Now.Date;
            txtBanco.Text = _pago.BancoDescripcion;
            txtCentroAcopio.Text = _pago.CentroAcopioDescripcion;
            txtFolioEntrada.Text = _pago.FolioEntrada.ToString();
            txtProveedor.Text = _pago.ProveedorDescripcion;
            txtImporte.Text = _pago.Importe.ToString();
            _pago.UsuarioId = AuxConfiguracion.ObtenerUsuarioLogueado();
        }
        #endregion CONSTRUCTOR

        #region EVENTOS
        private void btnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            Salir();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmar)
            {
                if (btnGuardar.IsEnabled)
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
        }

        private void btnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            if(ValidarCampos())
            {
                _pago.FechaPago = dtpFechaPago.SelectedDate.Value.Date;
                _pago.CodigoAutorizacion = txtCodigoAutorizacion.Text.Trim();
                Guardar(_pago);
            }
        }
        #endregion EVENTOS

        #region MÉTODOS
        private void Salir()
        {
            MessageBoxResult result = SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                          MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                confirmar = false;
                Close();
            }
        }

        private bool ValidarCampos()
        {
            var result = true;
            if (txtCodigoAutorizacion.Text.Trim() == string.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RegistrarPagosTransferencia_ErrorAutorizacion,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                txtCodigoAutorizacion.Focus();
                result = false;
            }

            return result;
        }

        private int Guardar(PagoTransferenciaInfo pago)
        {
            int result = 0;
            try
            {
                var pagoPl = new PagoTransferenciaPL();
                result = pagoPl.GuardarPago(pago);
                if(result > 0 )
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],string.Format(Properties.Resources.RealizarPagosTransferencia_ExitoGuardar,result), MessageBoxButton.OK, MessageImage.Correct);
                    folio = result;
                    confirmar = false;
                    Close();
                }
                else
                {
                    if(result == -1)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], string.Format(Properties.Resources.RealizarPagosTransferencia_Existe, pago.PagoId), MessageBoxButton.OK, MessageImage.Warning);
                        folio = pago.PagoId;
                        confirmar = false;
                        Close();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RealizarPagosTransferencia_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);   
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RealizarPagosTransferencia_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RealizarPagosTransferencia_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
            return result;
        }

        #endregion MÉTODOS

    }
}
