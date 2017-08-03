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
    public class AuxTiempoMuertoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, TiempoMuertoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TiempoMuertoID", filtro.TiempoMuertoID},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(TiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ProduccionDiariaID", info.ProduccionDiaria.ProduccionDiariaID},
							{"@RepartoAlimentoID", info.RepartoAlimento.RepartoAlimentoID},
							{"@HoraInicio", info.HoraInicio},
							{"@HoraFin", info.HoraFin},
							{"@CausaTiempoMuertoID", info.CausaTiempoMuerto.CausaTiempoMuertoID},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(TiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TiempoMuertoID", info.TiempoMuertoID},
							{"@ProduccionDiariaID", info.ProduccionDiaria.ProduccionDiariaID},
							{"@RepartoAlimentoID", info.RepartoAlimento.RepartoAlimentoID},
							{"@HoraInicio", info.HoraInicio},
							{"@HoraFin", info.HoraFin},
							{"@CausaTiempoMuertoID", info.CausaTiempoMuerto.CausaTiempoMuertoID},
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
        /// <param name="tiempoMuertoID">Identificador de la entidad TiempoMuerto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int tiempoMuertoID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@TiempoMuertoID", tiempoMuertoID}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="produccionDiaria">Valores de la entidad</param>
        ///  <param name="produccionDiariaID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerGuardarTiempoMuerto(ProduccionDiariaInfo produccionDiaria, int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in produccionDiaria.ListaTiempoMuerto
                                              select
                                                  new XElement("TiempoMuerto",
                                                               new XElement("TiempoMuertoID", info.TiempoMuertoID),
                                                               new XElement("ProduccionDiariaID", produccionDiariaID),
                                                               new XElement("HoraInicio", info.HoraInicio),
                                                               new XElement("HoraFin", info.HoraFin),
                                                               new XElement("CausaTiempoMuertoID", info.CausaTiempoMuertoID),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlTiempoMuerto", xml.ToString()}
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
        /// <param name="tiempoMuerto">Valores de la entidad</param>
        ///  <param name="repartoAlimentoID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerGuardarTiempoMuertoReparto(List<TiempoMuertoInfo> tiempoMuerto, int repartoAlimentoID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in tiempoMuerto
                                              select
                                                  new XElement("TiempoMuerto",
                                                               new XElement("TiempoMuertoID", info.TiempoMuertoID),
                                                               new XElement("RepartoAlimentoID", repartoAlimentoID),
                                                               new XElement("HoraInicio", info.HoraInicio),
                                                               new XElement("HoraFin", info.HoraFin),
                                                               new XElement("CausaTiempoMuertoID", info.CausaTiempoMuertoID),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlTiempoMuerto", xml.ToString()}
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

