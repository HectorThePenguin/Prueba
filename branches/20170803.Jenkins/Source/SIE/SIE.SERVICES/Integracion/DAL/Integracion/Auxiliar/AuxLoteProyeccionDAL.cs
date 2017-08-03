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
    internal class AuxLoteProyeccionDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, LoteProyeccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteProyeccionID", filtro.LoteProyeccionID},
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@LoteID", info.LoteID},
							{"@OrganizacionID", info.OrganizacionID},
							{"@Frame", info.Frame},
							{"@GananciaDiaria", info.GananciaDiaria},
							{"@ConsumoBaseHumeda", info.ConsumoBaseHumeda},
							{"@Conversion", info.Conversion},
							{"@PesoMaduro", info.PesoMaduro},
							{"@PesoSacrificio", info.PesoSacrificio},
							{"@DiasEngorda", info.DiasEngorda},
							{"@FechaEntradaZilmax", info.FechaEntradaZilmax},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@Revision", info.RequiereRevision}
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
        internal static Dictionary<string, object> ObtenerParametrosCrearBitacora(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteProyeccionID", info.LoteProyeccionID}
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@LoteProyeccionID", info.LoteProyeccionID},
							{"@LoteID", info.LoteID},
							{"@OrganizacionID", info.OrganizacionID},
							{"@Frame", info.Frame},
							{"@GananciaDiaria", info.GananciaDiaria},
							{"@ConsumoBaseHumeda", info.ConsumoBaseHumeda},
							{"@Conversion", info.Conversion},
							{"@PesoMaduro", info.PesoMaduro},
							{"@PesoSacrificio", info.PesoSacrificio},
							{"@DiasEngorda", info.DiasEngorda},
							{"@FechaEntradaZilmax", info.FechaEntradaZilmax},
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
        /// <param name="loteProyeccionID">Identificador de la entidad LoteProyeccion</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteProyeccionID", loteProyeccionID}
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
        /// Obtiene parametros para obtener la proyeccion por lote
        /// </summary> 
        /// <param name="lote">Lote del cual se obtendra la proyeccion </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorLote(LoteInfo lote)
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
        /// Obtiene un diccionario con los parametros necesarios
        /// para la ejecucion del procedimiento almacenado
        /// LoteProyeccion_ObtenerPorLoteXML
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlCondiciones = new XElement("ROOT",
                                                  from lote in lotes
                                                  select new XElement("Lotes",
                                                                      new XElement("LoteID", lote.LoteID)));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlLote", xmlCondiciones.ToString()},
                        {"@OrganizacionID", organizacionId},
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
        /// Obtiene parametros para obtener la proyeccion por lote
        /// </summary> 
        /// <param name="lote">Lote del cual se obtendra la proyeccion </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorLoteCompleto(LoteInfo lote)
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="loteProyeccionID">Identificador de la entidad LoteProyeccion</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLoteProyeccionID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteProyeccionID", loteProyeccionID}
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

