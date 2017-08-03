using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxConciliacionDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para
        /// la ejecucion del procedimiento almacenado
        /// Conciliacion_ObtenerPasesProceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosConciliacionPaseProceso(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@FechaInicial", fechaInicial},
                            {"@FechaFinal", fechaFinal},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Obtiene Parametro para filtrar por tipo poliza
        /// </summary>
        /// <param name="tipoPoliza"></param>
        /// <returns></returns>
        internal static Dictionary<string,object> ConciliacionTipoPoliza(int tipoPoliza)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@TipoPolizaID", tipoPoliza}
                    };
                return parametros;            
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimientos almacenado
        /// AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosAlmacenConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@FechaInicial", fechaInicial},
                            {"@FechaFinal", fechaFinal},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado EntradaProducto_ObtenerConciliacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaMateriaPrimaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@FechaInicial", fechaInicial},
                            {"@FechaFinal", fechaFinal},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
