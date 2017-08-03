using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Auxiliar
{
    internal class AuxAccionDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina">Informacion de paginacion usada en la consulta</param>
        /// <param name="filtro">filtro de busqueda que se usara</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, AdministrarAccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
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
        /// Obtiene parametros para insertar un registro
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns>Regresa los parametros necesarios para la insercion</returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AdministrarAccionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                     new Dictionary<string, object>
                        {
							{"@Descripcion", info.Descripcion.Trim()},
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
        /// Se obtiene la descripcion en caso de existir
        /// </summary>
        /// <param name="Descripcion">Descripcion con la que se buscara una accion</param>
        /// <returns>Regresa los parametros necesarios para la busqueda por descripcion</returns>
        internal static Dictionary<string, object> ObtenerPorDescripcion(String Descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                     new Dictionary<string, object>
                        {							
                            {"@Descripcion", Descripcion.Trim()},
							
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
        /// <returns>Regresa los parametros necesarios para la actualizacion</returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(AdministrarAccionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@AccionID", info.AccionID},
							{"@Descripcion", info.Descripcion.Trim()},
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
        /// Obtiene los parametros necesarios para la validacion de la accion
        /// </summary>
        /// <param name="accionId">ID de la accion que se verificara</param>
        /// <returns>Regresa los parametros necesarios para la validacion</returns>
        internal static Dictionary<string, object> ValidarAsignacionesAsignadasById(int accionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ID", accionId}
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
