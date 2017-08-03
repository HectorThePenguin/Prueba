using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPremezclaDistribucionDAL
    {
        /// <summary>
        /// Ontiene los parametros para crear una distribucion
        /// </summary>
        /// <param name="premezcla"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarPremezclaDistribucion(PremezclaDistribucionInfo premezcla)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoId", premezcla.ProductoId},
                            {"@CantidadExistente", premezcla.CantidadExistente},
                            {"@CostoUnitario", premezcla.CostoUnitario},
                            {"@ProveedorID", premezcla.ProveedorId},
                            {"@Iva", premezcla.Iva},
                            {"@UsuarioCreacionId", premezcla.UsuarioCreacionId}
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
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almacenado
        /// PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPremezclaDistribucionConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FechaInicial", fechaInicio},
                            {"@FechaFinal", fechaFinal},
                            {"@OrganizacionID", organizacionID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerPremezclaDistribucionCosto(int productoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@productoID", productoID},
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
