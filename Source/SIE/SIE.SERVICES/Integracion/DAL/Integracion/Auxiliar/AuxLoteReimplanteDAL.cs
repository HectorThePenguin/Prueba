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
    internal class AuxLoteReimplanteDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, LoteReimplanteInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteReimplanteID", filtro.LoteReimplanteID},
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(LoteReimplanteInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@LoteProyeccionID", info.LoteProyeccionID},
							{"@NumeroReimplante", info.NumeroReimplante},
							{"@FechaProyectada", info.FechaProyectada},
							{"@PesoProyectado", info.PesoProyectado},
							{"@FechaReal", info.FechaReal},
							{"@PesoReal", info.PesoReal},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID}
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(LoteReimplanteInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@LoteProyeccionID", info.LoteProyeccionID},
							{"@LoteProyeccionID", info.LoteProyeccionID},
							{"@NumeroReimplante", info.NumeroReimplante},
							{"@FechaProyectada", info.FechaProyectada},
							{"@PesoProyectado", info.PesoProyectado},
							{"@FechaReal", info.FechaReal},
							{"@PesoReal", info.PesoReal},
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
        /// <param name="loteReimplanteID">Identificador de la entidad LoteReimplante</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int loteReimplanteID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@LoteReimplanteID", loteReimplanteID}
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
        /// <param name="descripcion">Descripción de la entidad </param>
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

        /// <summary>
        ///     Obtiene parametros para crear el detalle de la entrada
        /// </summary>
        /// <param name="listaReimplantes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoLoteReimplantes(
            List<LoteReimplanteInfo> listaReimplantes)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in listaReimplantes
                                 select
                                     new XElement("LoteReimplante",
                                                  new XElement("LoteReimplanteID", info.LoteReimplanteID),
                                                  new XElement("LoteProyeccionID", info.LoteProyeccionID),
                                                  new XElement("FechaProyectada", info.FechaProyectada),
                                                  new XElement("PesoProyectado", info.PesoProyectado),
                                                  new XElement("NumeroReimplante", info.NumeroReimplante),
                                                  new XElement("UsuarioCreacionID", info.UsuarioCreacionID)
                                     ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlReimplante", xml.ToString()}
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
        /// Obtiene los parametros necesario para obtener el lote reimplante de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@LoteID", lote.LoteID},
                                {"@OrganizacionID", lote.OrganizacionID}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado LoteReimplante_ObtenerPorLoteXML
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in lotes
                                 select
                                     new XElement("Lotes",
                                                  new XElement("LoteID", info.LoteID)));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlLote", xml.ToString()},
                            {"@OrganizacionID", organizacionId},
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
