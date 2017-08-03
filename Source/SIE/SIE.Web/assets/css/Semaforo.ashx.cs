using System;
using System.Web;
using SIE.Services.Servicios.BL;

namespace SIE.Web.assets.css
{
    /// <summary>
    /// Descripción breve de Semaforo
    /// </summary>
    public class Semaforo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var colorObjetivoBL = new ColorObjetivoBL();
                string semaforo = colorObjetivoBL.ObtenerSemaforo();

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