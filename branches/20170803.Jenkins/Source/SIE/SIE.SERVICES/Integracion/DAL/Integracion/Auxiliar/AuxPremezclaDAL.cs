
using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPremezclaDAL
    {
        /// <summary>
        /// Obtiene una lista de parametros para obtener por organizacion
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacion(OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacion.OrganizacionID},
                            {"@Activo",organizacion.Activo.GetHashCode()}
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
        /// Obtiene un registro de premezcla por productoid y organizacionid
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorProductoIdPorOrganizacionId(PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", premezclaInfo.Producto.ProductoId},
                            {"@OrganizacionID", premezclaInfo.Organizacion.OrganizacionID},
                            {"@Activo", premezclaInfo.Activo.GetHashCode()}
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
        /// Obtiene parametros para crear una premezcla
        /// </summary>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearPremezcla(PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", premezclaInfo.Organizacion.OrganizacionID},
                            {"@Descripcion", premezclaInfo.Descripcion},
                            {"@ProductoID", premezclaInfo.Producto.ProductoId},
                            {"@Activo", premezclaInfo.Activo.GetHashCode()},
                            {"@UsuarioCreacionID", premezclaInfo.UsuarioCreacion.UsuarioCreacionID}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Premezcla_ObtenerPorOrganizacion
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionDetalle(OrganizacionInfo organizacion)
        {
            Dictionary<string, object> parametros = null;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacion.OrganizacionID},
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return parametros;
        }
    }
}
