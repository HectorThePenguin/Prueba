using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class ProductoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Producto
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                int result = info.ProductoId;
                if (info.UsuarioModificacionID != null && info.UsuarioModificacionID != 0)
                {
                    productoDAL.Actualizar(info);
                }
                else
                {
                    result = productoDAL.Crear(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorPagina(pagina, filtro);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaLoteExistencia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorPaginaLoteExistencia(pagina, filtro);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaLoteExistenciaCantidadCero(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorPaginaLoteExistenciaCantidadCero(pagina, filtro);
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
        /// Obtiene una entidad Producto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Producto por su descripcion</param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorDescripcion(descripcion);
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
        internal ProductoInfo ObtenerPorProductoIDSubFamilia(ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorProductoIDSubFamilia(producto, dependencias);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorPaginaSubFamilia(pagina, producto,dependencias);
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
        internal ResultadoInfo<ProductoInfo> Centros_ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.Centros_ObtenerPorPaginaSubFamilia(pagina, producto, dependencias);
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
        internal ProductoInfo ObtenerPorID(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo productoInfo = productoDAL.ObtenerPorID(producto);
                return productoInfo;
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
        /// Obtiene una entidad por su ID con lote y existencia
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorIDLoteExistencia(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorIDLoteExistencia(producto);
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
        /// Obtiene una entidad por su ID con lote y existencia
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorIDLoteExistenciaCantidadCero(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorIDLoteExistenciaCantidadCero(producto);
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
        internal ProductoInfo ObtenerConUnidadMedidaPorID(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoDAL = new Integracion.DAL.ORM.ProductoDAL();
                var productos = productoDAL.ObtenerTodos().Where(p => p.ProductoId == producto.ProductoId);
                productos = productoDAL.ConUnidad(productos);
                ProductoInfo result = productos.FirstOrDefault();
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
        internal List<ProductoInfo> ObtenerPorEstados(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                List<ProductoInfo> result = productoDal.ObtenerPorEstados(estatus);
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
        internal ProductoInfo ObtenerPorPedidoID(int pedidoID, int productoID)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorPedidoID(pedidoID, productoID);
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
        /// Obtiene un listado con los filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<ProductoInfo> ObtenerPorFamilia(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDalOrm = new Integracion.DAL.ORM.ProductoDAL();
                List<ProductoInfo> result = productoDalOrm.ObtenerPorFamilia(filtro);
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
        /// Obtiene un listado con los filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<FiltroProductoProduccionMolino> ObtenerValoresProduccionMolino(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDalOrm = new Integracion.DAL.ORM.ProductoDAL();
                List<FiltroProductoProduccionMolino> result = productoDalOrm.ObtenerValoresProduccionMolino(filtro);
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

        internal ResultadoInfo<ProductoInfo> ObtenerPorPedidoPaginado(PaginacionInfo pagina, ProductoInfo filtro, int pedidoID)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorPedidoPaginado(pagina, filtro, pedidoID);
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
        internal ProductoInfo ObtenerPorFolioSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                return  productoDAL.ObtenerPorFolioSalida(salida);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorFamiliaPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorFamiliaPaginado(pagina, filtro);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorFamiliasPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorFamiliasPaginado(pagina, filtro);
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
        /// <param name="almacenId"></param>
        /// <param name="productos"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioInfo> ObtenerExistencia(int almacenId, IList<ProductoInfo> productos)
        {
            try
            {
                Logger.Info();
                var productoDAL = new Integracion.DAL.ORM.ProductoDAL();
                return productoDAL.ObtenerExistencia(almacenId, productos);
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
        internal ProductoInfo ObtenerPorIDSinActivo(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                return productoDAL.ObtenerPorIDSinActivo(productoActual);
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
        internal ProductoInfo ObtenerPorIdSubFamiliaId(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorIdSubFamiliaId(producto);
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
        /// Obtiene un listado con los filtros
        /// </summary>
        /// <param name="pagina"> </param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerCompletoPorFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerCompletoPorFamilia(pagina, filtro);
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
        /// Obtiene una lista de productos por almacen
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroAlmacen(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDal.ObtenerPorPaginaFiltroAlmacen(pagina, filtro);
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
        /// Obtiene una lista de productos por almacen
        /// </summary>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorProductoIdAlmacenId(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                ProductoInfo result = productoDal.ObtenerPorProductoIdAlmacenId(filtro);
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
        /// Obtiene un lista paginada de productos con programacion fletes interna
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaTengaProgramacionFletesInterna(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorPaginaTengaProgramacionFletesInterna(pagina, filtro);
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
        /// Obtiene producto con programacion flete interna
        /// </summary>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorProductoIdTengaProgramacionFleteInterna(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                ProductoInfo result = productoDal.ObtenerPorProductoIdTengaProgramacionFleteInterna(filtro);
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
        /// Obtiene los productos que tengan cuenta SAP
        /// </summary>
        /// <returns></returns>
        internal IList<ProductoInfo> ObtenerProductosConCuentaSAP()
        {
            try
            {
                Logger.Info();
                var productoDAL = new Integracion.DAL.ORM.ProductoDAL();
                var productos = productoDAL.ObtenerTodos().Where(p => p.CuentaSAPID > 0);
                return productos.ToList();
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
        /// Obtiene una lista de productos validos para forraje por organizacion
        /// </summary>
        /// <returns></returns>
        internal List<ProductoInfo> ObtenerProductosValidosForraje()
        {
            try
            {
                Logger.Info();
                var listaProductos = new List<ProductoInfo>();
                var configuracionParametrosPl = new ParametroGeneralBL();
                var configuracion = new ParametroGeneralInfo();
                configuracion =
                    configuracionParametrosPl.ObtenerPorClaveParametro(ParametrosEnum.ProductosForraje.ToString());

                if (configuracion != null)
                {
                    string productos = configuracion.Valor;

                    if (!string.IsNullOrEmpty(productos))
                    {
                        var arregloProductos = productos.Split('|');
                        foreach (var arregloProducto in arregloProductos)
                        {
                            if (arregloProducto != "")
                            {
                                var producto = new ProductoInfo();
                                producto.ProductoId = Convert.ToInt32(arregloProducto);
                                producto = ObtenerPorID(producto);

                                listaProductos.Add(producto);
                            }
                        }
                        return listaProductos;
                    }
                }
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
            return null;
        }

        /// <summary>
        /// Obtiene el producto configurado como forraje de la tabla parametro organizacion
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerProductoForraje(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var configuracionParametrosPl = new ParametroGeneralBL();
                
                var configuracion = new ParametroGeneralInfo();
                configuracion =
                    configuracionParametrosPl.ObtenerPorClaveParametro(ParametrosEnum.ProductosForraje.ToString());
                
                if (configuracion != null)
                {
                    string productos = configuracion.Valor;

                    if (!string.IsNullOrEmpty(productos))
                    {
                        var arregloProductos = productos.Split('|');
                        foreach (var arregloProducto in arregloProductos)
                        {
                            if (arregloProducto != "")
                            {
                                var productoResultado = new ProductoInfo();
                                productoResultado.ProductoId = Convert.ToInt32(arregloProducto);
                                if (producto.ProductoId == productoResultado.ProductoId)
                                {
                                    producto = ObtenerPorID(productoResultado);
                                    return producto;
                                }
                            }
                        }
                    }
                }
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
            return null;
        }

        /// <summary>
        /// Obtiene un listado con los filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<ProductoInfo> ObtenerPorSubFamilia(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDalOrm = new Integracion.DAL.ORM.ProductoDAL();
                List<ProductoInfo> result = productoDalOrm.ObtenerPorSubFamilia(filtro);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroFamiliaSubfamilias(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDal = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDal.ObtenerPorPaginaFiltroFamiliaSubfamilias(pagina, filtro);
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
        /// 
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorIdFiltroFamiliaSubfamilias(ProductoInfo producto)
        {
            try
            {
                var productosForraje = new List<int>();
                Logger.Info();
                var productoDal = new ProductoDAL();
                ProductoInfo productoInfo = productoDal.ObtenerPorID(producto);
                if (productoInfo != null)
                {
                    if (productoInfo.Familia.FamiliaID == FamiliasEnum.MateriaPrimas.GetHashCode())
                    {
                        return productoInfo;
                    }
                    if (productoInfo.SubFamilia.SubFamiliaID == SubFamiliasEnum.Pacas.GetHashCode())
                    {
                        return productoInfo;
                    }
                    var parametroGeneralBL = new ParametroGeneralBL();
                    ParametroGeneralInfo parametro =
                        parametroGeneralBL.ObtenerPorClaveParametro(ParametrosEnum.SubProductosCrearContrato.ToString());
                    if (parametro != null)
                    {

                        if (parametro.Valor.Contains('|'))
                        {
                            productosForraje = (from tipos in parametro.Valor.Split('|')
                                select Convert.ToInt32(tipos)).ToList();
                        }
                        else
                        {
                            int forraje = Convert.ToInt32(parametro.Valor);
                            productosForraje.Add(forraje);
                        }
                    }
                    if (productosForraje.Any(i => i == Convert.ToInt32(productoInfo.ProductoId)))
                    {
                        return productoInfo;
                    }
                }
                return null;
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
        internal ProductoInfo ObtieneRequiereContrato(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var configuracionParametrosPl = new ParametroGeneralBL();

                var configuracion = new ParametroGeneralInfo();
                configuracion =
                    configuracionParametrosPl.ObtenerPorClaveParametro(ParametrosEnum.SubProductosCrearContrato.ToString());

                if (configuracion != null)
                {
                    string productos = configuracion.Valor;

                    if (!string.IsNullOrEmpty(productos))
                    {
                        var arregloProductos = productos.Split('|');
                        foreach (var arregloProducto in arregloProductos)
                        {
                            if (arregloProducto != "")
                            {
                                var productoResultado = new ProductoInfo();
                                productoResultado.ProductoId = Convert.ToInt32(arregloProducto);
                                if (producto.ProductoId == productoResultado.ProductoId)
                                {
                                    producto = ObtenerPorID(productoResultado);
                                    return producto;
                                }
                            }
                        }
                    }
                }
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
            return null;
        }

        /// <summary>
        /// Obtiene una lista de Productos por familia paginado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorProductoIDFamilias(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorProductoIDFamilias(filtro);
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
        internal ProductoInfo ObtenerPorIDFamiliaIdSubFamiliaId(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                return productoDAL.ObtenerPorIDFamiliaIdSubFamiliaId(productoActual);
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
        /// Obtiene una lista de Productos por Descripcion(Familia Materia Primas, SubFamilia Granos)
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorDescripcionSubFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorDescripcionSubFamilia(pagina, filtro);
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
        internal ProductoInfo ObtenerPorIndicador(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                return productoDAL.ObtenerPorIndicador(producto);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorIndicadorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = productoDAL.ObtenerPorIndicadorPagina(pagina, filtro);
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
        /// Obtiene una entidad Producto por su descripcion
        /// </summary>
        /// <param name="materialSAP">Material SAP</param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorMaterialSAP(string materialSAP)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ProductoInfo result = productoDAL.ObtenerPorMaterialSAP(materialSAP);
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
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var productoDAL = new ProductoDAL();
                ResultadoInfo<ProductoInfo> result = new ResultadoInfo<ProductoInfo>()
                {
                    TotalRegistros = 0,
                    Lista = new List<ProductoInfo>()
                };

                result = productoDAL.ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(pagina, filtro);
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
