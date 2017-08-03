using System;
using System.Configuration;
using System.Web;
using SIE.Base.Log;

namespace SIE.Base.Vista
{
    public abstract class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// OnPrerender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            AutoRedirect();
        }

        /// <summary>
        /// Método para redirecionar al login
        /// </summary>
        public void AutoRedirect()
        {
            int milliSecondsTimeOut = (Session.Timeout*60000);
            string script = @"
                        <script type='text/javascript'>
                            intervalset = window.setInterval('Redirect()'," +
                            string.Format("{0}", milliSecondsTimeOut) + @");
                            function Redirect()
                            {
                                alert('Su sesión ha expirado y el sistema redirige a la página de login.!\n\n');
                                window.location.href='../Seguridad/login.aspx';
                            }
                        </script>";
            ClientScript.RegisterClientScriptBlock(GetType(), "Redirect", script);
        }

        /// <summary>
        /// Obtiene la sessión  de la seguridad
        /// </summary>
        /// <returns></returns>
        public static object ObtenerSeguridad()
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"];
                return seguridad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}