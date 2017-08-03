
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteInventarioMateriaPrimaDAL
    {
        /// <summary>
        /// Genera los parametros del reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="productoId"></param>
        /// <param name="almacenId"></param>
        /// <param name="lote"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosTipoGanadoReporteInventario(int organizacionId, int productoId, int almacenId, int lote, DateTime fechaInicio, DateTime fechaFin)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionId", organizacionId},
                                     {"@ProductoId", productoId},
                                     {"@AlmacenId", almacenId},
                                     {"@LoteId", lote},
                                     {"@FechaInicio", fechaInicio},
                                     {"@FechaFin", fechaFin},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

    }
}
