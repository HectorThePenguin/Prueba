using System;
using System.Collections.Generic;
using System.Web;
using SIE.Base.Vista;
using SIE.Services.Info.Info;
using System.Web.Services;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Administracion
{
    public partial class Notificaciones : PageBase
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion EVENTOS

        #region METODOS WEB

        /// <summary>
        /// Obtiene las notificaciones autorizadas
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<EntradaProductoInfo> ObtenerNotificacionesAutorizadas()
        {
            try
            {
                var entradaProductoPL = new EntradaProductoPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                List<EntradaProductoInfo> notificacionesAutorizadas = null;
                if (seguridad != null)
                {
                    notificacionesAutorizadas =
                        entradaProductoPL.ObtenerNotificacionesAutorizadas(seguridad.Usuario.Organizacion.OrganizacionID);
                }
                return notificacionesAutorizadas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion METODOS WEB
    }
}
