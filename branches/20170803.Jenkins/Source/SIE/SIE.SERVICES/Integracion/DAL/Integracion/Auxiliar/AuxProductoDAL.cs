using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Auxiliar;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProductoDAL
    {
        /// <summary>
        /// Obtiene la lista de parametros para buscar Productos por estado
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorEstatus(EstatusEnum estatus)
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
        /// Obtiene los parametros para buscar productos por subfamilia
        /// </summary>
        /// <param name="subfamiliaId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorSubFamiliaId(int subfamiliaId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@subfamiliaId", subfamiliaId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerProductosPorTratamiento(TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionId", tratamiento.OrganizacionId},
                            {"@CodigoTratamiento", tratamiento.CodigoTratamiento},
                            {"@Sexo", tratamiento.Sexo.ToString()[0]},
                            {"@TipoTratamiento", tratamiento.TipoTratamiento}
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", info.ProductoId},
							{"@Descripcion", info.ProductoDescripcion},
							{"@SubFamiliaID", info.SubfamiliaId},
							{"@UnidadID", info.UnidadId},
                            {"@ManejaLote", info.ManejaLote},
                            {"@MaterialSAP", info.MaterialSAP},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ProductoID", info.ProductoId},
							{"@Descripcion", info.ProductoDescripcion},
							{"@SubFamiliaID", info.SubfamiliaId},
							{"@UnidadID", info.UnidadId},
                            {"@ManejaLote", info.ManejaLote},
                            {"@MaterialSAP", info.MaterialSAP},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
                            {"@Activo", info.Activo},
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
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", filtro.ProductoId},
                            {"@Descripcion", filtro.ProductoDescripcion},
                            {"@FamiliaID", filtro.FamiliaId},
                            {"@SubFamiliaID", filtro.SubfamiliaId},
                            {"@UnidadID", filtro.UnidadId},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaLoteExistencia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", filtro.ProductoDescripcion},
                            {"@Activo", filtro.Activo},
                            {"@AlmacenID", filtro.AlmacenID},
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="producto">Descripción de la entidad </param>
        ///  <param name="dependencias">Dependencias de la busqueda </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProductoIDSubFamilia(ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@ProductoID", producto.ProductoId}
                            };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
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
        /// <param name="producto">Descripción de la entidad </param>
        /// <param name="pagina">Descripción de la entidad </param>
        ///  <param name="dependencias">Dependencias de la busqueda </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", producto.ProductoId},
								{"@Descripcion", producto.ProductoDescripcion},
                                {"@Activo", producto.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite}
                            };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
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
        /// <param name="producto">Descripción de la entidad </param>
        /// <param name="pagina">Descripción de la entidad </param>
        ///  <param name="dependencias">Dependencias de la busqueda </param>
        /// <returns></returns>
        internal static Dictionary<string, object> Centros_ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", producto.ProductoId},
								{"@Descripcion", producto.ProductoDescripcion},
                                {"@Activo", producto.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite}
                            };
                AuxDAL.Centros_ObtenerDependencias(parametros, dependencias);
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
        /// para la ejecucion del procedimiento almancenado
        /// Producto_ObtenerPorProductoID
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProductoID(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", producto.ProductoId},
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
        /// para la ejecucion del procedimiento almancenado
        /// Producto_ObtenerPorProductoIDLoteExistencia
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProductoIDLoteExistencia(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", producto.ProductoId},
                            {"@Activo", producto.Activo},
                            {"@FamiliaID", producto.Familia == null ? 0 : producto.Familia.FamiliaID},
                            {"@AlmacenID", producto.AlmacenID}
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
        /// Metodo para obtener los paramentros necesarios
        /// para la ejecucion del procedimiento Producto_ObtenerPorFolioPedido
        /// </summary>
        /// <param name="pedidoID"></param>
        /// <param name="productoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorPedidoID(int pedidoID, int productoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@PedidoID", pedidoID},
                                {"@ProductoID", productoID},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerPorPedidoPaginado(PaginacionInfo pagina, ProductoInfo filtro, int pedidoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@PedidoID", pedidoID},
                                {"@Descripcion", filtro.ProductoDescripcion},
                                {"@Activo", filtro.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
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
        /// Obtiene los parametros para obtenero un producto por folio de salida
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@FolioSalida", salida.FolioSalida},
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
        /// Obtiene los parametros por familia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorFamiliaPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@FamiliaID", filtro.Familia.FamiliaID},
                                {"@Descripcion", filtro.ProductoDescripcion},
                                {"@Activo", filtro.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
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
        /// Obtiene el parametro para consultar por id sin activo
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorIDSinActivo(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", productoActual.ProductoId},
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
        /// Obtiene parametros para obtener un producto por id y familia id
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProductoIdSubFamiliaId(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", producto.ProductoId},
                                {"@SubFamiliaID", producto.SubfamiliaId},
                                {"@Activo", producto.Activo.GetHashCode()}
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
        /// Obtiene el parametro para consultar por id sin activo
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerCompletoPorFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                filtro.Descripcion = filtro.ProductoDescripcion ?? string.Empty;

                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", filtro.ProductoId},
                                {"@Descripcion", filtro.Descripcion},
                                {"@Activo", filtro.Activo},
                                {"@FiltroFamilia", filtro.FiltroFamilia},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
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
        /// Obtiene un lista paginada de productos existentes en inventario
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorPaginaFiltroAlmacen(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", filtro.ProductoId},
                                {"@Descripcion", filtro.ProductoDescripcion},
                                {"@AlmacenID", filtro.AlmacenID},
                                {"@Activo", filtro.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
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
        /// Obtiene un producto existente en inventario
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProductoIdAlmacenId(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", filtro.ProductoId},
                                {"@AlmacenID", filtro.AlmacenID},
                                {"@Activo", filtro.Activo}
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
        /// Obtiene parametros para obtener lista paginada de productos con programacion fletes interna
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaTengaProgramacionFletesInterna(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", filtro.ProductoId},
                            {"@Descripcion", filtro.ProductoDescripcion},
                            {"@FamiliaID", filtro.FamiliaId},
                            {"@SubFamiliaID", filtro.SubfamiliaId},
                            {"@UnidadID", filtro.UnidadId},
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
        /// Obtiene un producto existente en inventario
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProductoIdTengaProgramacionFleteInterna(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", filtro.ProductoId},
                                {"@Activo", filtro.Activo}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerPorFamiliasPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();

                var xml = new XElement("ROOT", 
                                from familia in filtro.Familias
                                select 
                                new XElement("Familias",
                                                new XElement("FamiliaID",
                                                            familia.FamiliaID)));

                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@XMLFamilias",xml.ToString()},
                                {"@Descripcion", filtro.ProductoDescripcion},
                                {"@Activo", filtro.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerPorProductoIDFamilias(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();

                var xml = new XElement("ROOT",
                                from familia in filtro.Familias
                                select
                                new XElement("Familias",
                                                new XElement("FamiliaID",
                                                            familia.FamiliaID)));

                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@XMLFamilias",xml.ToString()},
                                {"@ProductoID", filtro.ProductoId},
                                {"@Activo", filtro.Activo},
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
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroFamiliaSubfamilias(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", filtro.ProductoId},
                            {"@Descripcion", filtro.ProductoDescripcion},
                            {"@FamiliaID", filtro.FamiliaId},
                            {"@SubFamiliaID", filtro.SubfamiliaId},
                            {"@UnidadID", filtro.UnidadId},
                            {"@ParametroDescripcion", ParametrosEnum.SubProductosCrearContrato.ToString()},
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
        /// Obtiene el parametro para consultar por id 
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorIDFamiliaIdSubFamiliaId(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ProductoID", productoActual.ProductoId},
                                {"@FamiliaID", productoActual.FamiliaId},
                                {"@SubFamiliaID", productoActual.SubfamiliaId},
                                {"@Activo", productoActual.SubfamiliaId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerPorDescripcionSubFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();

                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@Descripcion", filtro.ProductoDescripcion},
                                {"@Activo", filtro.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
                                {"@FamiliaID", filtro.FamiliaId},
                                {"@SubFamiliaID", filtro.SubfamiliaId},
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
        /// del procedimeinto almacenado Producto_ObtenerPorIndicador
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorProductoIndicador(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@IndicadorID", producto.IndicadorID},
                                {"@ProductoID", producto.ProductoId}
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
        /// Obtiene los parametros necesarios
        /// para la ejecucion del procedimiento
        /// almacenado Producto_ObtenerPorIndicadorPagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorIndicadorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IndicadorID", filtro.IndicadorID},
                            {
                                "@Descripcion",
                                string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion
                            },
                            {"@Activo", filtro.Activo},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerProductosPorTratamientoPorXML(List<TratamientoInfo> tratamientos)
        {
            try
            {
                var xml = new XElement("ROOT",
                                       from tratamiento in tratamientos
                                       select new XElement("Tratamientos",
                                                           new XElement("OrganizacionID",
                                                                        tratamiento.OrganizacionId),
                                                           new XElement("CodigoTratamiento",
                                                                        tratamiento.CodigoTratamiento),
                                                           new XElement("Sexo",
                                                                        tratamiento.Sexo.ToString()),
                                                           new XElement("TipoTratamiento",
                                                                        tratamiento.TipoTratamiento)));
                var parametros = new Dictionary<string, object>
                                     {
                                         {"@xmlTratamientos", xml.ToString()}
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
        /// <param name="materialSAP">Material del SAP </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorMaterialSAP(string materialSAP)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@MaterialSAP", materialSAP}
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
        /// Genera parametros para la busqueda de productos de una organizacion
        /// </summary>
        /// <param name="pagina">Informacion de paginacion</param>
        /// <param name="filtro">Filtro de la busqueda</param>
        /// <returns>Regresa la lista de parametros para la busqueda</returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroSubFamiliaParaEnvioAlimento(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", filtro.ProductoId},
                            {"@Descripcion", filtro.ProductoDescripcion},
                            {"@SubFamiliaID", filtro.SubfamiliaId},
                            {"@UsuarioID ", filtro.UsuarioModificacionID},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorPaginaLoteExistenciaCantidadCero(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", filtro.ProductoDescripcion},
                            {"@Activo", filtro.Activo},
                            {"@AlmacenID", filtro.AlmacenID},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
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
        /// Obtiene un diccionario con los parametros necesarios
        /// para la ejecucion del procedimiento almancenado
        /// Producto_ObtenerPorProductoIDLoteExistencia
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorIDLoteExistenciaCantidadCero(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", producto.ProductoId},
                            {"@Activo", producto.Activo},
                            {"@OrganizacionID", producto.Organizacion.OrganizacionID},
                            {"@AlmacenID", producto.AlmacenID},
                            {"@FamiliaID", producto.Familia == null ? 0 : producto.Familia.FamiliaID}
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
