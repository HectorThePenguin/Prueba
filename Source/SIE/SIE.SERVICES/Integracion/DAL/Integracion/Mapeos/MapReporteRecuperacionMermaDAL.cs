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
    internal class MapReporteRecuperacionMermaDAL
    {
        /// <summary>
        /// Obtiene una lista de Reporte Recuperacion Merma
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteRecuperacionMermaInfo> ObtenerReporteRecuperacionMerma(DataSet ds)
        {
            List<ReporteRecuperacionMermaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteRecuperacionMermaInfo
                         {
                             CabezasEnfermeria = info.Field<int>("CabezasEnfermeria"),
                             CabezasMuertas = info.Field<int>("CabezasMuertas"),
                             CabezasOrigen = info.Field<int>("CabezasOrigen"),
                             CabezasProduccion = info.Field<int>("CabezasProduccion"),
                             CabezasVenta = info.Field<int>("CabezasVenta"),
                             FechaEntrada = info.Field<string>("FechaEntrada"),
                             FolioEntrada = info.Field<int>("FolioEntrada"),
                             PesoCorte = info.Field<decimal>("KilosCorte"),
                             PesoLlegada = info.Field<decimal>("KilosLlegada"),
                             PesoOrigen = info.Field<decimal>("KilosOrigen"),
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
