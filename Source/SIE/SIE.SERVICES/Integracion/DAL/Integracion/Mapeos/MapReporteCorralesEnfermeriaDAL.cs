using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapReporteCorralesEnfermeriaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<ReporteCorralesEnfermeriaInfo> Generar(DataSet ds)
        {
            try
            {
                string formato = string.Format("{{0:{0}}}", ConstantesDAL.FormatoFechaReportes);
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ReporteCorralesEnfermeriaInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ReporteCorralesEnfermeriaInfo
                             {
                                 CorralEnfermeria = info.Field<string>("CorralEnfermeria"),
                                 Arete = info["Arete"] == DBNull.Value ? string.Empty : info.Field<string>("Arete"),
                                 FechaEnfermeria = info["FechaEnfermeria"] == DBNull.Value ? string.Empty : string.Format(formato, info.Field<DateTime>("FechaEnfermeria")),
                                 PesoOrigen = info.Field<int>("PesoOrigen"),
                                 TipoGanado = info.Field<string>("TipoGanado"),
                                 Problema = info["Problema"] == DBNull.Value ? string.Empty : info.Field<string>("Problema").Trim(),
                                 CorralOrigen = info.Field<string>("CorralOrigen"),
                                 DiasEngorda = info.Field<int>("DiasEngorda"),
                                 DiasEnfermeria = info.Field<int>("DiasEnfermeria"),
                                 Partida = info.Field<int>("Partida"),
                                 FechaLlegada = info["FechaLlegada"] == DBNull.Value ? string.Empty : string.Format(formato, info.Field<DateTime>("FechaLlegada")),
                                 Origen = info.Field<string>("Origen"),
                                 CorralID = info.Field<int>("CorralID"),
                             }).ToList();

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


