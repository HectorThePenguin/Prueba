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
    internal class MapReporteDetalleReimplanteDAL
    {
        /// <summary>
        /// Obtiene una lista de detalle de reimplante
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        //internal static List<ReporteDetalleReimplanteInfo> ObtenerDetalleReimplante(DataSet ds)
        internal static DataTable ObtenerDetalleReimplante(DataSet ds)
        {
            //List<ReporteDetalleReimplanteInfo> lista;
            DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
            try
            {
                Logger.Info();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return dt;
        }
    }
}
