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
    internal class MapReporteSolicitudPaseProcesoDAL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Solicitus Pase Proceso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ReporteSolicitudPaseProcesoInfo> ObtenerDatosReporteSolicitudPaseProceso(DataSet ds)
        {
          
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ReporteSolicitudPaseProcesoInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ReporteSolicitudPaseProcesoInfo
                         {
                             Codigo = info.Field<int>("Codigo"),
                             Ingrediente = info.Field<string>("Ingrediente"),
                             TMExistenciaAlmacenPA = info["TMExistenciaAlmacenPA"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("TMExistenciaAlmacenPA"),
                             TMConsumoDia = info["TMConsumoDia"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("TMConsumoDia"),
                             TMCapacidadAlmacenaje = info["TMCapacidadAlmacenaje"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("TMCapacidadAlmacenaje"),
                             TMSugeridasSolicitar = info["TMSugeridasSolicitar"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("TMSugeridasSolicitar"),
                             TMRequeridasProduccion = info["TMRequeridasProduccion"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("TMRequeridasProduccion"),
                             Fecha = info.Field<DateTime>("Fecha")
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
