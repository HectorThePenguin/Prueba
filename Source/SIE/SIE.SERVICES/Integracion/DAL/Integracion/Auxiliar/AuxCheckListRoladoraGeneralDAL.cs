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
    public class AuxCheckListRoladoraGeneralDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CheckListRoladoraGeneralInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraGeneralID", filtro.CheckListRoladoraGeneralID},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(CheckListRoladoraGeneralInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CheckListRoladoraGeneralID", info.CheckListRoladoraGeneralID},
							{"@Turno", info.Turno},
							{"@FechaInicio", info.FechaInicio},
							{"@Observaciones", info.Observaciones},
							{"@SurfactanteInicio", info.SurfactanteInicio},
							{"@SurfactanteFin", info.SurfactanteFin},
							{"@ContadorAguaInicio", info.ContadorAguaInicio},
							{"@ContadorAguaFin", info.ContadorAguaFin},
							{"@GranoEnteroFinal", info.GranoEnteroFinal},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", (int?)info.UsuarioCreacionID == 0 ? null : (int?)info.UsuarioCreacionID},
                            {"@UsuarioIDSupervisor", (int?)info.UsuarioIDSupervisor == 0 ? null : (int?)info.UsuarioIDSupervisor},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(CheckListRoladoraGeneralInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CheckListRoladoraGeneralID", info.CheckListRoladoraGeneralID},
							{"@Turno", info.Turno},
							{"@Observaciones", info.Observaciones},
							{"@SurfactanteInicio", info.SurfactanteInicio},
							{"@SurfactanteFin", info.SurfactanteFin},
							{"@ContadorAguaInicio", info.ContadorAguaInicio},
							{"@ContadorAguaFin", info.ContadorAguaFin},
							{"@GranoEnteroFinal", info.GranoEnteroFinal},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
                            {"@UsuarioIDSupervisor", info.UsuarioIDSupervisor.HasValue ? info.UsuarioIDSupervisor : null},
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
        /// <param name="checkListRoladoraGeneralID">Identificador de la entidad CheckListRoladoraGeneral</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int checkListRoladoraGeneralID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraGeneralID", checkListRoladoraGeneralID}
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
								{"@CheckListRoladoraGeneralID", descripcion}
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

