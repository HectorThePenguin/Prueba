using System;
using System.Web.Services;
using Entidad;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Alimentacion
{
    /// <summary>
    /// Descripción breve de Reparto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
     [System.Web.Script.Services.ScriptService]
    public class Reparto : WebService
    {


        [WebMethod]
        public Response<RepartoAvanceInfo> ObtenerAvanceReparto(int idUsuario)
        {
            var respuesta = new RepartoAvanceInfo();
            try
            {
                var repartoPl = new RepartoPL();

                respuesta = repartoPl.ObtenerAvanceReparto(idUsuario);

                return Response<ResultadoOperacion>.CrearResponse(true,respuesta);
            }
            catch (Exception)
            {
                return Response<ResultadoOperacion>.CrearResponse(false,respuesta);
            }
        }
    }
}
