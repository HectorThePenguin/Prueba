using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.WinForm.Controles.Ayuda;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.Base.Log;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;
using SuKarne.Controls.Enum;
using SIE.WinForm.Auxiliar;
using System.Linq;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para SalidaVentaTraspasoAutorizar.xaml
    /// </summary>
    public partial class SalidaVentaTraspasoAutorizar
    {
        public AutorizacionInfo autorizacionInfo { get; set; }
        private RolInfo rolRequeridoInfo;
        
        public SalidaVentaTraspasoAutorizar(RolInfo rolInfo)
        {
            InitializeComponent();
            txtUsuario.Focus();
            rolRequeridoInfo = rolInfo;
            autorizacionInfo = new AutorizacionInfo();
            autorizacionInfo.Autorizado = false;
        }

        private bool ValidarDatos()
        {
            if (txtUsuario.Text == String.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.Autorizacion_RequiereUsuario,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtUsuario.Focus();
                return false;
            }
            if (txtContrasenia.Password == String.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.Autorizacion_RequiereContrasenia,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtContrasenia.Focus();
                return false;
            }
            if (txtJustificacion.Text == String.Empty)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.Autorizacion_RequiereJustificacion,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
                txtJustificacion.Focus();
                return false;
            }
            return true;
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidarDatos())
                {
                    var usuario = txtUsuario.Text.ToString().Trim();
                    var contrasenia = txtContrasenia.Password.ToString().Trim();
                   
                    var usuarioPL = new UsuarioPL();
                    UsuarioInfo usuarioInfo = usuarioPL.ObtenerPorActiveDirectory(txtUsuario.Text.Trim());

                    if (usuarioInfo == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.Login_ErrorUsuarioLocal,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                        txtUsuario.Focus();
                    }
                    else
                    {
                        if (rolRequeridoInfo.RolID == usuarioInfo.Operador.Rol.RolID)
                        {
                            autorizacionInfo.Usuario = usuarioInfo;
                            autorizacionInfo.Justificacion = txtJustificacion.Text.Trim();
                            autorizacionInfo.Autorizado = true;
                            Close();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.Autorizacion_MsjSinPrivilegios,
                             MessageBoxButton.OK,
                             MessageImage.Stop);
                             txtUsuario.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Autorizacion_MsjErrorAutorizacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(
                     Application.Current.Windows[ConstantesVista.WindowPrincipal],
                     Properties.Resources.Autorizacion_MsjCancelarAutorizacion,
                     MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {

                Close();
            }
        }

        private void TxtInput_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarCaracterValidoTexo(e.Text);
        }
    }
}
