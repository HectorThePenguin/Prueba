using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProveedorAlmacenDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorProveedorId(ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", proveedorInfo.ProveedorID}
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
        /// 
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenID}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(ProveedorAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ProveedorID", info.Proveedor.ProveedorID},
							{"@AlmacenID", info.Almacen.AlmacenID},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizar(ProveedorAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ProveedorAlmacenID", info.ProveedorAlmacenId},
							{"@ProveedorID", info.Proveedor.ProveedorID},
							{"@AlmacenID", info.Almacen.AlmacenID},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        /// 
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorProveedorTipoAlmacen(ProveedorAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", info.Proveedor.ProveedorID},
                            {"@TipoAlmacenID", info.Almacen.TipoAlmacenID},
                            {"@Activo", info.Activo.GetHashCode()},
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
