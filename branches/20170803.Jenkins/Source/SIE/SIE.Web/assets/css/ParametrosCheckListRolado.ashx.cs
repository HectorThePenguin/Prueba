using System;
using System.Web;
using SIE.Services.Servicios.BL;

namespace SIE.Web.assets.css
{
    /// <summary>
    /// Descripción breve de ParametrosCheckListRolado
    /// </summary>
    public class ParametrosCheckListRolado : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string semaforo;
                using (var checkListRoladoraAccionBL = new CheckListRoladoraAccionBL())
                {
                    semaforo = checkListRoladoraAccionBL.GenerarEstilosParametros();
                }

                context.Response.ContentType = "text/css";
                context.Response.Write(semaforo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}