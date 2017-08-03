using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxFleteInternoDetalleDAL
    {
        /// <summary>
        /// Obtiene parametros para crear un flete interno
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object>  ObtenerParametrosCrearFleteInternoDetalle(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FleteInternoID", fleteInternoDetalleInfo.FleteInternoId},
                            {"@ProveedorID", fleteInternoDetalleInfo.Proveedor.ProveedorID},
                            {"@MermaPermitida", fleteInternoDetalleInfo.MermaPermitida},
                            {"@Observaciones", fleteInternoDetalleInfo.Observaciones},
                            {"@Activo", fleteInternoDetalleInfo.Activo.GetHashCode()},
                            {"@UsuarioCreacionID", fleteInternoDetalleInfo.UsuarioCreacionId},
                            {"@TipoTarifaID", fleteInternoDetalleInfo.TipoTarifaID}
                            
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Actualiza un flete interno detalle
        /// </summary>
        /// <param name="fleteInternoDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFleteInternoDetalle(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FleteInternoDetalleID", fleteInternoDetalleInfo.FleteInternoDetalleId},
                            {"@MermaPermitida", fleteInternoDetalleInfo.MermaPermitida},
                            {"@Observaciones", fleteInternoDetalleInfo.Observaciones},
                            {"@Activo", fleteInternoDetalleInfo.Activo.GetHashCode()},
                            {"@UsuarioModificacionID", fleteInternoDetalleInfo.UsuarioModificacionId},
                            {"@TipoTarifaID", fleteInternoDetalleInfo.TipoTarifaID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtener listado de fleteinternodetalle por fleteinterno
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFleteInternoId(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FleteInternoID", fleteInternoInfo.FleteInternoId},
                            {"@Activo", fleteInternoInfo.Activo.GetHashCode()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
