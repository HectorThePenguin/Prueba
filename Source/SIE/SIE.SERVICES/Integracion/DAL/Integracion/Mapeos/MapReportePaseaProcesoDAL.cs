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
    class MapReportePaseaProcesoDAL
    {
        /// <summary>
        /// Obtiene una lista de Reporte Produccion Vs Consumo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReportePaseaProcesoInfo> Generar(DataSet ds)
        {
            List<ReportePaseaProcesoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReportePaseaProcesoInfo
                         {
                             Ticket = info.Field<string>("Ticket"),
                             FechaSurtido = info.Field<DateTime>("FechaSurtido"),
                             PesoNeto = info.Field<int>("PesoNeto"),
                             ProductoId = info.Field<int>("ProductoId"),
                             Descripcion = info.Field<string>("Descripcion"),
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}
