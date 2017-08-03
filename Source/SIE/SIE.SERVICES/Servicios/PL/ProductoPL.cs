using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProductoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Producto
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                int result = productoBL.Guardar(info);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPagina(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada de productos con existencia que se maneja en lote
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaLoteExistencia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPaginaLoteExistencia(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada de productos con existencia que se maneja en lote
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaLoteExistenciaCantidadCero(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPaginaLoteExistenciaCantidadCero(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorDescripcion(descripcion);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de Productos
        /// </summary>
        /// <param name="pagina">contenedor con los parámetros de paginación</param>
        /// <param name="producto">contenedor con los parámetros de busqueda</param>
        /// <param name="dependencias">contenedor con las dependencias de busqueda</param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto,
                                                                      IList<IDictionary<IList<String>, Object>>
                                                                          dependencias)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPaginaSubFamilia(pagina, producto,
                                                                                           dependencias);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de Productos
        /// </summary>
        /// <param name="pagina">contenedor con los parámetros de paginación</param>
        /// <param name="producto">contenedor con los parámetros de busqueda</param>
        /// <param name="dependencias">contenedor con las dependencias de busqueda</param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> Centros_ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto,
                                                                      IList<IDictionary<IList<String>, Object>>
                                                                          dependencias)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.Centros_ObtenerPorPaginaSubFamilia(pagina, producto,
                                                                                           dependencias);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de Producto
        /// </summary>
        /// <param name="producto">contenedor con los parámetros de busqueda</param>
        /// <param name="dependencias">contenedor con los parámetros de busqueda</param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorProductoIDSubFamilia(ProductoInfo producto,
                                                           IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorProductoIDSubFamilia(producto, dependencias);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de Producto
        /// </summary>
        /// <param name="producto">contenedor con los parámetros de busqueda</param>
        /// <param name="dependencias">contenedor con los parámetros de busqueda</param>
        /// <returns></returns>
        public ProductoInfo Centros_ObtenerPorProductoIDSubFamilia(ProductoInfo producto,
                                                           IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorProductoIDSubFamilia(producto, dependencias);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorID(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorID(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID que maneje lote y con existencias
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIDLoteExistencia(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorIDLoteExistencia(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID que maneje lote y con existencias
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIDLoteExistenciaCantidadCero(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorIDLoteExistenciaCantidadCero(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerConUnidadMedidaPorID(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerConUnidadMedidaPorID(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de productos por estado
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerPorEstados(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                List<ProductoInfo> result = productoBL.ObtenerPorEstados(estatus);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad por pedido id
        /// </summary>
        /// <param name="pedidoID"></param>
        /// <param name="productoID"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorPedidoID(int pedidoID, int productoID)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorPedidoID(pedidoID, productoID);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de productos por estado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerPorFiltro(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                List<ProductoInfo> result = productoBL.ObtenerPorFamilia(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de productos por estado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<FiltroProductoProduccionMolino> ObtenerValoresProduccionMolino(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                List<FiltroProductoProduccionMolino> result = productoBL.ObtenerValoresProduccionMolino(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="pedidoID"> </param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPedidoPaginado(PaginacionInfo pagina, ProductoInfo filtro,
                                                                    int pedidoID)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPedidoPaginado(pagina, filtro, pedidoID);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el producto de una salida
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorFolioSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                return productoBL.ObtenerPorFolioSalida(salida);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Productos por familia paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorFamiliaPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerPorFamiliaPaginado(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Productos por familia paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorFamiliasPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerPorFamiliasPaginado(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public ResultadoInfo<ProductoInfo> ObtenerIngredientesPorFamiliasPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();

                if (filtro.Familias == null)
                {
                    filtro.Familias = new List<FamiliaInfo>
                            {
                                new FamiliaInfo { FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Premezclas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Alimento.GetHashCode() },
                            };
                }

                filtro.ProductoDescripcion = filtro.Descripcion;
                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerPorFamiliasPaginado(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public ResultadoInfo<ProductoInfo> ObtenerIngredientesPorFamiliasBusquedaPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();

                if (filtro.Familias == null)
                {
                    filtro.Familias = new List<FamiliaInfo>
                            {
                                new FamiliaInfo { FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Premezclas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Alimento.GetHashCode() },
                            };
                }
              

                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerPorFamiliasPaginado(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public ProductoInfo ObtenerIngredientesPorIDFamilias(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                if (filtro.Familias == null)
                {
                    filtro.Familias = new List<FamiliaInfo>
                            {
                                new FamiliaInfo { FamiliaID = FamiliasEnum.MateriaPrimas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Premezclas.GetHashCode() },
                                new FamiliaInfo { FamiliaID = FamiliasEnum.Alimento.GetHashCode() },
                            };
                }
                ProductoInfo result = productoBl.ObtenerPorProductoIDFamilias(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Productos por familia paginado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorProductoIDFamilias(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerPorProductoIDFamilias(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Productos por familia paginado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIDFamilias(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerPorProductoIDFamilias(filtro);
                if (result != null)
                {
                    result.Familias = filtro.Familias;
                }
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la existencia de un almacén de un producto especifico.
        /// </summary>
        /// <param name="almacenId"> </param>
        /// <param name="productos"> </param>
        /// <returns></returns>
        public IList<AlmacenInventarioInfo> ObtenerExistencia(int almacenId, IList<ProductoInfo> productos)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                return productoBL.ObtenerExistencia(almacenId, productos);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el producto sin importar si esta activo
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIDSinActivo(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                return productoBL.ObtenerPorIDSinActivo(productoActual);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por id y familia id
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIdSubFamiliaId(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerPorIdSubFamiliaId(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Productos por familia paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerCompletoPorFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerCompletoPorFamilia(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroAlmacen(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerPorPaginaFiltroAlmacen(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por id y almacenid
        /// </summary>
        /// <returns></returns>
        public ProductoInfo ObtenerPorProductoIdAlmacenId(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerPorProductoIdAlmacenId(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada de productos que tengan programacion flete interna
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaTengaProgramacionFletesInterna(PaginacionInfo pagina,
                                                                                          ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPaginaTengaProgramacionFletesInterna(pagina,
                                                                                                               filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        /// <returns></returns>
        public ProductoInfo ObtenerPorProductoIdTengaProgramacionFleteInterna(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerPorProductoIdTengaProgramacionFleteInterna(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de productos validos para forraje
        /// </summary>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerProductosValidosForraje()
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                List<ProductoInfo> result = productoBl.ObtenerProductosValidosForraje();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto valido para forraje
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerProductoForraje(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerProductoForraje(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de productos por su SubFamilia
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerPorSubFamilia(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                List<ProductoInfo> result = productoBL.ObtenerPorSubFamilia(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada de productos filtrados por familia de materias primas y subfamilias
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroFamiliaSubfamilias(PaginacionInfo pagina,
                                                                                    ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPaginaFiltroFamiliaSubfamilias(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por id filtrando por familia y subfamilia
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIdFiltroFamiliaSubfamilias(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtenerPorIdFiltroFamiliaSubfamilias(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene si el producto requiere contrato
        /// Regresa el producto si esta en el parametro
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtieneRequiereContrato(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ProductoInfo result = productoBl.ObtieneRequiereContrato(producto);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el producto por ID de Familia Materia Primas, SubFamilia Granos
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIDFamiliaIdSubFamiliaId(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                return productoBL.ObtenerPorIDFamiliaIdSubFamiliaId(productoActual);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Productos por descripcion(Familia Materia Primas, Subfamilia Granos)
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorDescripcionSubFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBl = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBl.ObtenerPorDescripcionSubFamilia(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el producto por indicador
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorIndicador(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                return productoBL.ObtenerPorIndicador(producto);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el producto por indicador
        /// </summary>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorIndicadorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> resultado = productoBL.ObtenerPorIndicadorPagina(pagina, filtro);
                return resultado;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="materialSAP"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorMaterialSAP(string materialSAP)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                ProductoInfo result = productoBL.ObtenerPorMaterialSAP(materialSAP);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los productos filtrados por subfamilia y con paginación
        /// </summary>
        /// <param name="pagina"> Información de la paginación </param>
        /// <param name="filtro"> Información con la cual se realizará el filtrado </param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroSubFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoBL = new ProductoBL();
                filtro.FamiliaId = 0;
                filtro.UnidadId = 0;
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPagina(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que lista los productos disponibles para envio en la pantalla de envio de alimento según la subfamilia correspondiente
        /// </summary>
        /// <param name="pagina">Información de paginacion de la busqueda</param>
        /// <param name="filtro">Datos de la busqueda</param>
        /// <returns>Regresa la lista de productos encontrados pertenecientes a la familia seleccionada</returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                filtro.FamiliaId = 0;
                var productoBL = new ProductoBL();
                ResultadoInfo<ProductoInfo> result = productoBL.ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
