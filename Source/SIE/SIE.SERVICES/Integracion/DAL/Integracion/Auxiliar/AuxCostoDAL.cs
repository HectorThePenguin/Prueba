using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxCostoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CostoID", filtro.CostoID},
                                     {
                                         "@ClaveContable",
                                         string.IsNullOrWhiteSpace(filtro.ClaveContable)
                                             ? String.Empty
                                             : filtro.ClaveContable
                                     },
                                     {
                                         "@Descripcion",
                                         string.IsNullOrWhiteSpace(filtro.Descripcion)
                                             ? string.Empty
                                             : filtro.Descripcion
                                     },
                                     {"@TipoCostoID", filtro.TipoCosto.TipoCostoID},
                                     {"@Activo", filtro.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaSinId(PaginacionInfo pagina, CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CostoID", 0},
                                     {"@ClaveContable",""},
                                     {
                                         "@Descripcion",
                                         string.IsNullOrWhiteSpace(filtro.Descripcion)
                                             ? string.Empty
                                             : filtro.Descripcion
                                     },
                                     {"@TipoCostoID", filtro.TipoCosto.TipoCostoID},
                                     {"@Activo", filtro.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(CostoInfo costoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Descripcion", costoInfo.Descripcion},
                        {"@Activo", costoInfo.Activo},
                        {"@TipoCostoID", costoInfo.TipoCosto.TipoCostoID},
                        {"@TipoProrrateoID", costoInfo.TipoProrrateo.TipoProrrateoID},
                        {"@ClaveContable", costoInfo.ClaveContable},
                        {"@RetencionID", costoInfo.Retencion.RetencionID == 0 ? null : costoInfo.Retencion.RetencionID},
                        {"@AbonoA", costoInfo.AbonoA.ToString()},
                        {"@UsuarioCreacionID", costoInfo.UsuarioCreacionID},
                        {"@CompraIndividual", costoInfo.CompraIndividual},
                        {"@Compra",costoInfo.Compra},
                        {"@Recepcion",costoInfo.Recepcion},
                        {"@Gasto",costoInfo.Gasto},
                        {"@Costo",costoInfo.Costo},
                        {"@TipoCostoIDCentro",costoInfo.TipoCostoCentro.TipoCostoCentroID == 0 ? null : costoInfo.TipoCostoCentro.TipoCostoCentroID}

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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="costoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int costoID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoID", costoID}
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
        ///     Obtiene parametros para actualizar
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CostoInfo costoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoID", costoInfo.CostoID},
                        {"@Descripcion", costoInfo.Descripcion},
                        {"@Activo", costoInfo.Activo},
                        {"@TipoCostoID", costoInfo.TipoCosto.TipoCostoID},
                        {"@TipoProrrateoID", costoInfo.TipoProrrateo.TipoProrrateoID},
                        {"@ClaveContable", costoInfo.ClaveContable},
                        {"@RetencionID", costoInfo.Retencion.RetencionID == 0 ? null : costoInfo.Retencion.RetencionID},
                        {"@AbonoA", costoInfo.AbonoA.ToString()},
                        {"@UsuarioModificacionID", costoInfo.UsuarioModificacionID},
                        {"@CompraIndividual",costoInfo.CompraIndividual},
                        {"@Compra",costoInfo.Compra},
                        {"@Recepcion",costoInfo.Recepcion},
                        {"@Gasto",costoInfo.Gasto},
                        {"@Costo",costoInfo.Costo},
                        {"@TipoCostoIDCentro",costoInfo.TipoCostoCentro.TipoCostoCentroID == 0 ? null : costoInfo.TipoCostoCentro.TipoCostoCentroID}
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
        ///     Obtiene Parametros pora filtrar por estatus
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
                            {"@Activo", estatus.GetHashCode()}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in filtro.ListaTipoCostos
                                 select
                                     new XElement("TiposCosto",
                                                  new XElement("TipoCostoID", info.TipoCostoID))
                                     );
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoID", filtro.CostoID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@XmlTiposCosto", xml.ToString()},
                        {"@ClaveContable", filtro.ClaveContable},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorClaveContableTipoCosto(CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in filtro.ListaTipoCostos
                                 select
                                     new XElement("TiposCosto",
                                                  new XElement("TipoCostoID", info.TipoCostoID))
                                     );
                parametros = new Dictionary<string, object>
                    {
                        {"@ClaveContable", filtro.ClaveContable},
                        {"@XmlTiposCosto", xml.ToString()},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaClaveContable(PaginacionInfo pagina, CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Descripcion", filtro.Descripcion},
                        {"@ClaveContable", filtro.ClaveContable},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorClaveContable(CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ClaveContable", filtro.ClaveContable}
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

        public static Dictionary<string, object> ObtenerParametrosPorPaginaIDTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CostoID", filtro.CostoID},
                                     {
                                         "@ClaveContable",
                                         string.IsNullOrWhiteSpace(filtro.ClaveContable)
                                             ? String.Empty
                                             : filtro.ClaveContable
                                     },
                                     {
                                         "@Descripcion",
                                         string.IsNullOrWhiteSpace(filtro.Descripcion)
                                             ? string.Empty
                                             : filtro.Descripcion
                                     },
                                     {"@TipoCostoID", Costo.Fletes.GetHashCode()},
                                     {"@Activo", filtro.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        /// Obtiene flete por id
        /// </summary>
        /// <param name="idFlete"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorFleteID(int idFlete)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@FleteID",idFlete}
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
        /// Obtiene los parametros necesario para obtener un costo por id
        /// </summary>
        /// <param name="costo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroCostoPorID(CostoInfo costo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoID", costo.CostoID}
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
        /// Obtiene los parametros para obtener los costos por tipos de gasto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaCostoPorTiposGasto(PaginacionInfo pagina, CostoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                  
                                 from info in filtro.ListaTipoCostos
                                 select
                                       new XElement("TiposCosto",
                                                  new XElement("TipoCostoID", info.TipoCostoID))
                                     );
                parametros = new Dictionary<string, object>
                    {
                        {"@DescripcionCosto", filtro.Descripcion},
                        {"@XmlTipoCostos", xml.ToString()},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        /// Obtiene los parametros de costoid para obtener el centro de costo sap
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroCentroCostoSAPPorCosto(CostoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CostoID", filtro.CostoID},
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