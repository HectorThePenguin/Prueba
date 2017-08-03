using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxRepartoAlimentoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, RepartoAlimentoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoAlimentoID", filtro.RepartoAlimentoID},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(RepartoAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TipoServicioID", info.TipoServicioID},
							{"@CamionRepartoID", info.CamionRepartoID},
							{"@UsuarioIDReparto", info.UsuarioIDReparto},
							{"@HorometroInicial", info.HorometroInicial},
							{"@HorometroFinal", info.HorometroFinal},
							{"@OdometroInicial", info.OdometroInicial},
							{"@OdometroFinal", info.OdometroFinal},
							{"@LitrosDiesel", info.LitrosDiesel},
							{"@FechaReparto", info.FechaReparto},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(RepartoAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@RepartoAlimentoID", info.RepartoAlimentoID},
							{"@TipoServicioID", info.TipoServicioID},
							{"@CamionRepartoID", info.CamionRepartoID},
							{"@UsuarioIDReparto", info.UsuarioIDReparto},
							{"@HorometroInicial", info.HorometroInicial},
							{"@HorometroFinal", info.HorometroFinal},
							{"@OdometroInicial", info.OdometroInicial},
							{"@OdometroFinal", info.OdometroFinal},
							{"@LitrosDiesel", info.LitrosDiesel},
							{"@FechaReparto", info.FechaReparto},
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
        /// <param name="repartoAlimentoID">Identificador de la entidad RepartoAlimento</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int repartoAlimentoID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@RepartoAlimentoID", repartoAlimentoID}
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
								{"@RepartoAlimentoID", descripcion}
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
        /// Obtiene la Lista de los valores para el Grid de Repartos
        /// </summary> 
        /// <param name="filtro">filtros de la busqueda</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosConsultarReparto(FiltroCheckListReparto filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@TipoServicioID", filtro.TipoServicioID},
                                {"@OperadorID", filtro.OperadorID},
                                {"@Fecha", filtro.Fecha},
                                {"@CamionRepartoID", filtro.CamionRepartoID},
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
        /// Obtiene la Lista de los valores para el Grid de Repartos
        /// </summary> 
        /// <param name="filtro">filtros de la busqueda</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosImprimirReparto(FiltroCheckListReparto filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OperadorID", filtro.OperadorID},
                                {"@Fecha", filtro.Fecha},
                                {"@CamionRepartoID", filtro.CamionRepartoID},
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

