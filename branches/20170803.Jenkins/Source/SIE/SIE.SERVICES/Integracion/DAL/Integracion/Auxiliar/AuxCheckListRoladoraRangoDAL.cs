using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxCheckListRoladoraRangoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CheckListRoladoraRangoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraRangoID", filtro.CheckListRoladoraRangoID},
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
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(CheckListRoladoraRangoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Activo", info.Activo},
							{"@Activo", info.Activo},
							{"@CheckListRoladoraRangoID", info.CheckListRoladoraRangoID},
							{"@PreguntaID", info.Pregunta.PreguntaID},
							{"@Descripcion", info.Descripcion},
							{"@CodigoColor", info.CodigoColor},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(CheckListRoladoraRangoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Activo", info.Activo},
							{"@Activo", info.Activo},
							{"@CheckListRoladoraRangoID", info.CheckListRoladoraRangoID},
							{"@PreguntaID", info.Pregunta.PreguntaID},
							{"@Descripcion", info.Descripcion},
							{"@CodigoColor", info.CodigoColor},
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
        /// <param name="checkListRoladoraRangoID">Identificador de la entidad CheckListRoladoraRango</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int checkListRoladoraRangoID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraRangoID", checkListRoladoraRangoID}
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
        public static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
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
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
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

