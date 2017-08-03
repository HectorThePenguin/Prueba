using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SIE.Services.Info.Reportes;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    class MapReporteLectorComederosDAL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Lector de comederos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteLectorComederosInfo> ObtenerParametrosDatosReporteLectorComederos(DataSet ds)
        {
            List<ReporteLectorComederosInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteLectorComederosInfo
                         {
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Fecha = info.Field<DateTime>("Fecha"),
                             Diferencia = info.Field<string>("Diferencia"),
                             Codigo = info.Field<string>("Codigo"),
                             EstadoComedero = info.Field<int>("EstadoComedero"),
                             FormulaManiana = info.Field<string>("Formula_Mañana"),
                             CantidadManiana = info.Field<int>("Cantidad_Mañana"),
                             FormulaTarde = info.Field<string>("Formula_Tarde"),
                             CantididadTarde = info.Field<int>("Cantidad_Tarde"),
                             TotalHoy = info.Field<int>("Total_Hoy"),
                             EstadoComederoAyer = info.Field<int>("EstadoComedero_Ayer"),
                             FormulaManianaAyer = info.Field<string>("Formula_Mañana_Ayer"),
                             CantidadManianaAyer = info.Field<int>("Cantidad_Mañana_Ayer"),
                             FormulaTardeAyer = info.Field<string>("Formula_Tarde_Ayer"),
                             CantidadTardeAyer = info.Field<int>("Cantidad_Tarde_Ayer"),
                             TotalAyer = info.Field<int>("Total_Ayer")
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
