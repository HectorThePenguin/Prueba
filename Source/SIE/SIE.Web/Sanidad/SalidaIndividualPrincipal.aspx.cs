using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE.Web.Sanidad
{
    public partial class SalidaIndividualPrincipal : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int rolIdUsuario = 0;
            SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                if (seguridad.Usuario.Operador != null)
                {
                    rolIdUsuario = seguridad.Usuario.Operador.Rol.RolID;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }
                /* Se elimina esta validacion para dejarla por permisos de sistema 
                if (rolIdUsuario == (int)Roles.JefeSanidad || rolIdUsuario == (int)Roles.SupervisorSanidad)
                {

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }*/
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
            }
        }
    }
}