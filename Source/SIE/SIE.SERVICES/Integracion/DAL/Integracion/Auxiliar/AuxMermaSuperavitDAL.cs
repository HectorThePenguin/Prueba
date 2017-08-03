using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMermaSuperavitDAL
    {
        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenIdProductoId(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenID", almacenInfo.AlmacenID},
                                {"@Activo", EstatusEnum.Activo.GetHashCode()},
                                {"@ProductoID", productoInfo.ProductoId}
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
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenID", almacenID},
                                {"@Activo", EstatusEnum.Activo.GetHashCode()}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, MermaSuperavitInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtro.Almacen.AlmacenID},
                            {"@OrganizacionID", filtro.Almacen.Organizacion.OrganizacionID},
                            {"@Activo", filtro.Activo},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(MermaSuperavitInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@AlmacenID", info.Almacen.AlmacenID},
							{"@ProductoID", info.Producto.ProductoId},
							{"@Merma", info.Merma},
							{"@Superavit", info.Superavit},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(MermaSuperavitInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@MermaSuperavitID", info.MermaSuperavitId},
							{"@AlmacenID", info.Almacen.AlmacenID},
							{"@ProductoID", info.Producto.ProductoId},
							{"@Merma", info.Merma},
							{"@Superavit", info.Superavit},
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="mermaSuperavitID">Identificador de la entidad MermaSuperavit</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int mermaSuperavitID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@MermaSuperavitID", mermaSuperavitID}
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
        /// Obtiene Parametro pora filtrar por estatus 
        /// </summary> 
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Activo", estatus}
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="mermaSuperAvit">Descripción de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(MermaSuperavitInfo mermaSuperAvit)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenID", mermaSuperAvit.Almacen.AlmacenID},
                                {"@ProductoID", mermaSuperAvit.Producto.ProductoId}
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
