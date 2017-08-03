using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE.Services.Info.Info;

namespace SIE.Web
{
    public partial class Acceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if(usuario != null)
            {
                hfUsuario.Value = usuario.Usuario.UsuarioActiveDirectory;
            }
        }
    }
}