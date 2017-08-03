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
    public class AuxRepartoAlimentoDetalleDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, RepartoAlimentoDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoAlimentoDetalleID", filtro.RepartoAlimentoDetalleID},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(RepartoAlimentoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@RepartoAlimentoDetalleID", info.RepartoAlimentoDetalleID},
							{"@RepartoAlimentoID", info.RepartoAlimento.RepartoAlimentoID},
							{"@FolioReparto", info.FolioReparto},
							{"@FormulaIDRacion", info.Formula.FormulaId},
							{"@Tolva", info.Tolva},
							{"@KilosEmbarcados", info.KilosEmbarcados},
							{"@KilosRepartidos", info.KilosRepartidos},
							{"@Sobrante", info.Sobrante},
                            {"@PesoFinal", info.PesoFinal},
							{"@CorralIDInicio", info.CorralInicio.CorralID},
							{"@CorralIDFinal", info.CorralFinal.CorralID},
							{"@HoraRepartoInicio", info.HoraRepartoInicio},
							{"@HoraRepartoFinal", info.HoraRepartoFinal},
							{"@Observaciones", info.Observaciones},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(RepartoAlimentoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@RepartoAlimentoDetalleID", info.RepartoAlimentoDetalleID},
							{"@RepartoAlimentoID", info.RepartoAlimento.RepartoAlimentoID},
							{"@FolioReparto", info.FolioReparto},
							{"@FormulaIDRacion", info.Formula.FormulaId},
							{"@Tolva", info.Tolva},
							{"@KilosEmbarcados", info.KilosEmbarcados},
							{"@KilosRepartidos", info.KilosRepartidos},
							{"@Sobrante", info.Sobrante},
                            {"@PesoFinal", info.PesoFinal},
							{"@CorralIDInicio", info.CorralInicio.CorralID},
							{"@CorralIDFinal", info.CorralFinal.CorralID},
							{"@HoraRepartoInicio", info.HoraRepartoInicio},
							{"@HoraRepartoFinal", info.HoraRepartoFinal},
							{"@Observaciones", info.Observaciones},
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
        /// <param name="repartoAlimentoDetalleID">Identificador de la entidad RepartoAlimentoDetalle</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int repartoAlimentoDetalleID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@RepartoAlimentoDetalleID", repartoAlimentoDetalleID}
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
								{"@RepartoAlimentoDetalleID", descripcion}
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
        /// <param name="listaDetalles">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosGuardar(List<RepartoAlimentoDetalleInfo> listaDetalles)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from detalle in listaDetalles
                                select
                                    new XElement("RepartoAlimentoDetalle",
                                                 new XElement("RepartoAlimentoDetalleID", detalle.RepartoAlimentoDetalleID),
                                                 new XElement("RepartoAlimentoID", detalle.RepartoAlimentoID),
                                                 new XElement("FolioReparto", detalle.FolioReparto),
                                                 new XElement("FormulaIDRacion", detalle.FormulaIDRacion),
                                                 new XElement("Tolva", detalle.Tolva),
                                                 new XElement("KilosEmbarcados", detalle.KilosEmbarcados),
                                                 new XElement("KilosRepartidos", detalle.KilosRepartidos),
                                                 new XElement("Sobrante", detalle.Sobrante),
                                                 new XElement("PesoFinal", detalle.PesoFinal),
                                                 new XElement("CorralIDInicio", detalle.CorralIDInicio),
                                                 new XElement("CorralIDFinal", detalle.CorralIDFinal),
                                                 new XElement("HoraRepartoInicio", detalle.HoraRepartoInicio),
                                                 new XElement("HoraRepartoFinal", detalle.HoraRepartoFinal),
                                                 new XElement("Observaciones", detalle.Observaciones),
                                                 new XElement("Activo", detalle.Activo.GetHashCode()),
                                                 new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID),
                                                 new XElement("UsuarioModificacionID", detalle.UsuarioModificacionID)
                                                 )
                                    );
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@XmlRepartoAlimentoDetalle", xml.ToString()}
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
        /// Obtiene Parametro pora filtrar por el Id del Reparto de Alimento 
        /// </summary> 
        /// <param name="repartoAlimentoID">Id de la tabla RepartoAlimento </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorRepartoAlimentoID(int repartoAlimentoID)
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
    }
}

