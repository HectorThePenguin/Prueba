using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIE.Base.Vista;

namespace SIE.Web.Alimentacion
{
    public partial class PantallaImpresion : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgImpresion.DataSource = Session["DatosImprimir"];
                dgImpresion.DataBind();
            }
        }
    }
}