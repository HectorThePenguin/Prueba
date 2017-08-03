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
    internal class MapReporteLlegadaLogisticaDAL
    {
        /// <summary>
        /// Mapea los datos para poder generar el dataset que será enviado a "RptReporteLlegadaLogistica.rpt" para obtener el reporte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteLlegadaLogisticaDatos> ObtenerDatosLlegadaLogistica(DataSet ds)
        {
            List<ReporteLlegadaLogisticaDatos> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteLlegadaLogisticaDatos
                         {
                             FolioEmbarque = info.Field<int>("FolioEmbarque"),
                             FechaEmbarque = info.Field<DateTime>("FechaEmbarque"),
                             TipoEmbarque = info.Field<string>("TipoEmbarque"),
                             Proveedor = info.Field<string>("Proveedor"),
                             Chofer = info.Field<string>("Chofer"),
                             Origen = info.Field<string>("Origen"),
                             Destino = info.Field<string>("Destino"),
                             Estatus = info.Field<string>("Estatus"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Importe = info.Field<decimal>("Importe"),
                             FolioEntrada = info.Field<int>("FolioEntrada"),
                             FechaEntrada = info.Field<DateTime>("FechaEntrada")
                         }).OrderBy(x=>x.FolioEmbarque).ToList();
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
