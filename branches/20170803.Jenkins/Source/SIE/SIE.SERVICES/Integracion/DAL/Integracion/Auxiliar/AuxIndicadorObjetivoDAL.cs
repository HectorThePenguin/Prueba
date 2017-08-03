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
    internal class AuxIndicadorObjetivoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, IndicadorObjetivoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IndicadorObjetivoID", filtro.IndicadorObjetivoID},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@IndicadorID", filtro.Indicador.IndicadorId},
                            {"@ProductoID", filtro.Producto.ProductoId},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(IndicadorObjetivoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@IndicadorProductoCalidadID", info.IndicadorProductoCalidad.IndicadorProductoCalidadID},
							{"@TipoObjetivoCalidadID", info.TipoObjetivoCalidad.TipoObjetivoCalidadID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@ObjetivoMinimo", info.ObjetivoMinimo},
							{"@ObjetivoMaximo", info.ObjetivoMaximo},
							{"@Tolerancia", info.Tolerancia},
							{"@Medicion", info.Medicion},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(IndicadorObjetivoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@IndicadorObjetivoID", info.IndicadorObjetivoID},
							{"@IndicadorProductoCalidadID", info.IndicadorProductoCalidad.IndicadorProductoCalidadID},
							{"@TipoObjetivoCalidadID", info.TipoObjetivoCalidad.TipoObjetivoCalidadID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@ObjetivoMinimo", info.ObjetivoMinimo},
							{"@ObjetivoMaximo", info.ObjetivoMaximo},
							{"@Tolerancia", info.Tolerancia},
							{"@Medicion", info.Medicion},
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
        /// <param name="indicadorObjetivoID">Identificador de la entidad IndicadorObjetivo</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int indicadorObjetivoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IndicadorObjetivoID", indicadorObjetivoID}
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
								{"@IndicadorObjetivoID", descripcion}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado IndicadorObjetivo_ObtenerSemaforo
        /// </summary>
        /// <param name="pedidoID"></param>
        /// <param name="productoID"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerSemaforo(int pedidoID, int productoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PedidoID", pedidoID},
                            {"@ProductoID", productoID},
                            {"@OrganizacionID", organizacionID}
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
        /// <param name="filtros">Descripción de la entidad </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorFiltros(IndicadorObjetivoInfo filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@IndicadorProductoCalidadID", filtros.IndicadorProductoCalidad.IndicadorProductoCalidadID},
                                {"@OrganizacionID", filtros.Organizacion.OrganizacionID},
                                {"@Activo", filtros.Activo.GetHashCode()},
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

