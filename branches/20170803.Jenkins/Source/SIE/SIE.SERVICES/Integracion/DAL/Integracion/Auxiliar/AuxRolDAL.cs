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
    internal class AuxRolDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, RolInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RolID", filtro.RolID},
                            {"@Descripcion", filtro.Descripcion},
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
        /// <modificacion>
        /// Se a�adio el campo NivelAlerta
        /// </modificacion>
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(RolInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Descripcion", info.Descripcion},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@NivelAlertaID", info.NivelAlerta.NivelAlertaId},
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
        /// Obtiene parametros para actualizar
        /// <modificacion>
        /// Se a�adio el campo NivelAlerta
        /// </modificacion>
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(RolInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@RolID", info.RolID},
							{"@Descripcion", info.Descripcion},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
                            {"@NivelAlertaID", info.NivelAlerta.NivelAlertaId},
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
        /// <param name="rolID">Identificador de la entidad Rol</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int rolID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@RolID", rolID}
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
        /// Obtiene Parametro pora filtrar por descripci�n 
        /// </summary> 
        /// <param name="descripcion">Descripci�n de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@Descripcion", descripcion}
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
