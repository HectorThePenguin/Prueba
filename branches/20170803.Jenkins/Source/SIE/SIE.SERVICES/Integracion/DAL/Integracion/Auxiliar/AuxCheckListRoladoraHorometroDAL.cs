using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxCheckListRoladoraHorometroDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CheckListRoladoraHorometroInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraHorometroID", filtro.CheckListRoladoraHorometroID},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(CheckListRoladoraHorometroInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CheckListRoladoraGeneralID", info.CheckListRoladoraGeneral.CheckListRoladoraGeneralID},
							{"@RoladoraID", info.Roladora.RoladoraID},
							{"@HorometroInicial", info.HorometroInicial},
							{"@HorometroFinal", info.HorometroFinal},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(CheckListRoladoraHorometroInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CheckListRoladoraHorometroID", info.CheckListRoladoraHorometroID},
							{"@CheckListRoladoraGeneralID", info.CheckListRoladoraGeneral.CheckListRoladoraGeneralID},
							{"@RoladoraID", info.Roladora.RoladoraID},
							{"@HorometroInicial", info.HorometroInicial},
							{"@HorometroFinal", info.HorometroFinal},
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
        /// <param name="checkListRoladoraHorometroID">Identificador de la entidad CheckListRoladoraHorometro</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int checkListRoladoraHorometroID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@CheckListRoladoraHorometroID", checkListRoladoraHorometroID}
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
								{"@CheckListRoladoraHorometroID", descripcion}
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
        /// <param name="checkListRoladoraHorometro">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(List<CheckListRoladoraHorometroInfo> checkListRoladoraHorometro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from horometro in checkListRoladoraHorometro
                                 select new XElement("CheckListRoladoraHorometro",
                                                     new XElement("CheckListRoladoraHorometroID", horometro.CheckListRoladoraHorometroID),
                                                     new XElement("CheckListRoladoraGeneralID", horometro.CheckListRoladoraGeneralID),
                                                     new XElement("RoladoraID", horometro.Roladora.RoladoraID),
                                                     new XElement("HorometroInicial", string.IsNullOrWhiteSpace(horometro.HorometroInicial) ? string.Empty : horometro.HorometroInicial),
                                                     new XElement("HorometroFinal", horometro.HorometroFinal),
                                                     new XElement("Activo", horometro.Activo.GetHashCode()),
                                                     new XElement("UsuarioCreacionID", horometro.UsuarioCreacionID),
                                                     new XElement("UsuarioModificacionID", horometro.UsuarioModificacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlCheckListRoladoraHorometro", xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        /// <summary> 
        /// Obtiene los registros de Horometros por su CheckListRoladoraGeneralID
        /// </summary> 
        /// <param name="checkListRoladoraGeneralID">Representa el ID del Check List Roladora General </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerPorCheckListRoladoraGeneralID(int checkListRoladoraGeneralID)
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
    }
}

