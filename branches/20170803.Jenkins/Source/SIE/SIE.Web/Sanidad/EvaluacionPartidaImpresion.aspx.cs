using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidad;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Sanidad
{
    public partial class EvaluacionPartidaImpresion : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <param name="filtroEvaluacion">filtros donde viene la OrganizacionID</param>
        /// <returns></returns>
        [WebMethod]
        public static List<EvaluacionCorralInfo> TraerCorralesEvaluados(string filtroEvaluacion)
        {
            List<EvaluacionCorralInfo> corrales = null;
            try
            {
                var seguridad = (SeguridadInfo)ObtenerSeguridad();
                var evaluacionPL = new EvaluacionCorralPL();
                var filtros = new EvaluacionCorralInfo();
                var fechaEvaluacion = DateTime.Parse(filtroEvaluacion);

                filtros = new EvaluacionCorralInfo
                {
                    FechaEvaluacion = fechaEvaluacion,
                    Organizacion = new OrganizacionInfo()
                    {
                        OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID
                    },
                };

                 corrales = evaluacionPL.ObtenerEvaluaciones(filtros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return corrales;
        }


         /// <summary>
        /// Metodo para Imprimir los Corrales evaluados
        /// </summary>
        /// <param name="evaluacionCorralInfo">filtros donde viene </param>
        /// <returns></returns>
        [WebMethod]
        public static Response<EvaluacionCorralInfo> ImprimirCorralesEvaluados(EvaluacionCorralInfo evaluacionCorralInfo)
        {
            try
            {
                if (evaluacionCorralInfo != null)
                {
                    var evaluacionCorralPL = new EvaluacionCorralPL();
                    evaluacionCorralPL.ImprimirEvaluacionPartida(evaluacionCorralInfo,true);
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                return Response<EvaluacionCorralInfo>.CrearResponseVacio<EvaluacionCorralInfo>(false, 
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Response<EvaluacionCorralInfo>.CrearResponseVacio<EvaluacionCorralInfo>(false,
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return Response<EvaluacionCorralInfo>.CrearResponseVacio<EvaluacionCorralInfo>(true, "OK");
        }
        /*
        public void DescargarArchivo()
        {
            string filename = "EvaluacionPartida.pdf";
            string path = AppDomain.CurrentDomain.BaseDirectory + filename;

            var toDownload = new FileInfo(path);

            if (toDownload.Exists)
            {
                var output = new MemoryStream(File.ReadAllBytes(path));
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.BinaryWrite(output.ToArray());
            }
        }*/
    }
}