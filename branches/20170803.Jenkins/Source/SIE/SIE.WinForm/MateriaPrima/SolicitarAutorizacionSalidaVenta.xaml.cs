using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para SolicitarAutorizacionSalidaVenta.xaml
    /// </summary>
    public partial class SolicitarAutorizacionSalidaVenta
    {
        #region Atributos

        private SolicitudAutorizacionInfo solicitudInfo;
        public bool solicitudGenerada;

        #endregion

        public SolicitarAutorizacionSalidaVenta(SolicitudAutorizacionInfo solicitud)
        {
            InitializeComponent();
            solicitudInfo = solicitud;
            solicitudGenerada = false;
        }

        #region Eventos
        //Se activa cuando dan click sobre el boton Enviar
        private void BtnEnviar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if ( !string.IsNullOrEmpty(txtJustificacion.Text) )
                {
                    string mensaje = string.Empty;
                    var solicitudPL = new SolicitudAutorizacionPL();
                    solicitudInfo.Justificacion = txtJustificacion.Text;
                    int folioGenerado = solicitudPL.GenerarSolicitudAutorizacion(solicitudInfo);

                    mensaje = string.Format(Properties.Resources.SolicitarAutorizacionSalidaVenta_MsgEnviado, folioGenerado);
                    SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Correct);
                    
                    solicitudGenerada = true;
                    Close();
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SolicitarAutorizacionSalidaVenta_MsgDatosIncompletos,
                        MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        //Se activa cuando dan click sobre el boton Enviar
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
               Properties.Resources.SolicitarAutorizacionSalidaVenta_MsgCancelar,
               MessageBoxButton.YesNo, MessageImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        #endregion
    }
}
