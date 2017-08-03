using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    class MapAlertaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada de alertas
        /// </summary>
        /// <param name="ds">dataset con el resultado de la consulta de alertas</param>
        /// <returns>Regresa la lista de alertas que se envio en el dataset</returns>
        internal static ResultadoInfo<AlertaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlertaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlertaInfo()
                         {
                             AlertaID = info.Field<int>("AlertaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             HorasRespuesta = info.Field<int>("HorasRespuesta"),
                             TerminadoAutomatico = info.Field<bool>("TerminadoAutomatico").BoolAEnum(),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Modulo = new ModuloInfo
                             {
                                 ModuloID = info.Field<int>("ModuloID"),
                                 Descripcion = info.Field<string>("Modulo")
                             }
                         }).ToList();
                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);//numero de registros obtenidos en la consulta
               
                var resultado =
                    new ResultadoInfo<AlertaInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

       
    }
}
