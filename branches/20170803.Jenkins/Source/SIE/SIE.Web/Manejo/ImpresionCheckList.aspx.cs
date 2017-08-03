using System;
using System.IO;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Manejo
{
    public partial class ImpresionCheckList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var lote = Request.QueryString["LoteID"];
            var organizacion = Request.QueryString["OrganizacionID"];
            if (string.IsNullOrWhiteSpace(lote) || string.IsNullOrWhiteSpace(organizacion))
            {
                return;
            }
            int loteID;
            int organizacionID;
            int.TryParse(lote, out loteID);
            int.TryParse(organizacion, out organizacionID);

            var checkListCorralPL = new CheckListCorralPL();
            var checkListCorralInfo = checkListCorralPL.ObtenerPorLote(organizacionID, loteID);
            if(checkListCorralInfo == null)
            {
                Session["ErrorCheckList"] = 1;
                return;
            }

            var output = new MemoryStream(checkListCorralInfo.PDF);
            Session["ErrorCheckList"] = 0;

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=CheckList.pdf");
            Response.BinaryWrite(output.ToArray());
        }
    }
}