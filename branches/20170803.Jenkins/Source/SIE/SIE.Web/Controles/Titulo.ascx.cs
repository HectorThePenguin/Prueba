using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIE.Web.Controles
{
    public partial class Titulo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void AgregarTitulo(String titulo)
        {
            lblTitulo.Text = titulo;
        }
    }
}