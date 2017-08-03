using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class ProductoDAL : BaseDAL
    {
        ProductoAccessor productoAccessor;

        protected override void inicializar()
        {
            productoAccessor = da.inicializarAccessor<ProductoAccessor>();
        }

        protected override void destruir()
        {
            productoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Producto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<ProductoInfo>();
                var condicion = da.Tabla<ProductoInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.ProductoId > 0)
                {
                    condicion = condicion.Where(e=> e.ProductoId == filtro.ProductoId);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
                }
                result.TotalRegistros = condicion.Count();
                
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.Descripcion)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();

                return result;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Producto
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<ProductoInfo>();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Producto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<ProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e=> e.Activo == estatus);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        /// <summary>
        /// Obtiene una lista de Productos con la entidad de unidad cargada
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductoInfo> ConUnidad(IQueryable<ProductoInfo> query)
        {
            try
            {
                Logger.Info();
                var tblUnidad = base.da.Tabla<UnidadMedicionInfo>();
                query = from p in query
                        join u in tblUnidad on p.UnidadId equals u.UnidadID
                        select new ProductoInfo(p, u);

                return query;
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
        /// Obtiene una entidad de Producto por su Id
        /// </summary>
        /// <param name="productoId">Obtiene una entidad Producto por su Id</param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorID(int productoId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.ProductoId == productoId);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Producto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Producto por su descripcion</param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Producto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.ProductoId > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<ProductoInfo>(info);
                }
                else
                {
                    id = da.Insertar<ProductoInfo>(info);
                }
                return id;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Producto por su descripcion
        /// </summary>
        /// <param name="filtro">Obtiene una entidad Producto por su descripcion o Id</param>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerPorFamilia(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();

                List<ProductoInfo> listaProductos = productoAccessor.ObtenerPorFamilia(filtro.ProductoId, filtro.Descripcion,
                                                                        FamiliasEnum.MateriaPrimas.GetHashCode());
                return listaProductos;
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
        /// Obtiene una entidad de Producto por su descripcion
        /// </summary>
        /// <param name="filtro">Obtiene una entidad Producto por su descripcion o Id</param>
        /// <returns></returns>
        public List<ProductoInfo> ObtenerPorSubFamilia(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();

                List<ProductoInfo> listaProductos = productoAccessor.ObtenerPorSubFmilia(filtro.ProductoId, filtro.Descripcion,
                                                                        filtro.SubfamiliaId);
                return listaProductos;
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
        /// Obtiene una entidad de Producto por su descripcion
        /// </summary>
        /// <param name="filtro">Obtiene una entidad Producto por su descripcion o Id</param>
        /// <returns></returns>
        public List<FiltroProductoProduccionMolino> ObtenerValoresProduccionMolino(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                List<FiltroProductoProduccionMolino> valoresProduccion = productoAccessor.ObtenerValoresProduccionMolino(filtro.ProductoId);
                return valoresProduccion;
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
                var xml =
                   new XElement("ROOT",
                                from producto in productos
                                select new XElement("Producto",
                                        new XElement("AlmacenID", almacenId),
                                        new XElement("ProductoID", producto.ProductoId)
                                    ));

                return productoAccessor.ObtenerExistencia(xml.ToString());
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
        /// Obtiene una lista paginada de Producto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorFamiliaPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<ProductoInfo>();
                var condicion = da.Tabla<ProductoInfo>().Where(e => e.Activo == filtro.Activo);
                if (filtro.ProductoId > 0)
                {
                    condicion = condicion.Where(e => e.ProductoId == filtro.ProductoId);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e => e.Descripcion.Contains(filtro.Descripcion));
                }


                result.TotalRegistros = condicion.Count();

                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.Descripcion)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();

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
