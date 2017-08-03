using System;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;


namespace SIE.Web.Seguridad
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod(EnableSession=true)]
        public static UsuarioInfo VerificarUsuario(string usuario, string contra)
        {
            UsuarioInfo usuarioInfo = null;
            try
            {
                var usuarioPL = new UsuarioPL();                
                UsuarioInfo usuarioActiveDirectory = usuarioPL.ValidarUsuario(usuario.Trim(), contra, null);
                if (usuarioActiveDirectory != null)
                {                    
                    usuarioInfo = usuarioPL.ObtenerPorActiveDirectory(usuario.Trim());
                    if (usuarioInfo != null)
                    {                        
                        var seguridadInfo = new SeguridadInfo
                            {
                                Usuario = usuarioInfo
                            };
                        //usuarioValido = true;
                        HttpContext.Current.Session["Seguridad"] = seguridadInfo;
                        HttpContext.Current.Session["Usuario"] = seguridadInfo.Usuario.Nombre;
                        HttpContext.Current.Session["Organizacion"] = seguridadInfo.Usuario.Organizacion.Descripcion;
                    }                    
                }                                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                    throw;
            }
            return usuarioInfo;
        }
    }
}