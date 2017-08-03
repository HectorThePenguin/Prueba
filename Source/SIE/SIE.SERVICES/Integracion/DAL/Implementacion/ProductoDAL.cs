using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProductoDAL : DALBase
    {
        /// <summary>
        /// Regresa una lista de productos filtrados por subfamilia
        /// </summary>
        /// <param name="idSubfamilia"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorSubFamilia(int idSubfamilia)
        {
            ResultadoInfo<ProductoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorSubFamiliaId(idSubfamilia);
                DataSet ds = Retrieve("Producto_ObtenerPorSubFamilia", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProductoDAL.ObtenerTodos(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
       
        /// <summary>
        /// Regresa una lista de Productos filtrados por estado
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorEstado(EstatusEnum estatus)
        {
            ResultadoInfo<ProductoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorEstatus(estatus);
                DataSet ds = Retrieve("Producto_ObtenerPorEstado", parameters);
               
                if (ValidateDataSet(ds))
                {
                    lista = MapProductoDAL.ObtenerTodos(ds);
                }

                return lista;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de productos de un tratamiento especificado
        /// </summary>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerProductosPorTratamiento(TratamientoInfo tratamiento)
        {
            ResultadoInfo<ProductoInfo> lista = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerProductosPorTratamiento(tratamiento);

                DataSet ds = Retrieve("Producto_ObtenerPorTratamiento", parameters);

                if (ValidateDataSet(ds))
                {
                    lista = MapProductoDAL.ObtenerDesdeTratamientos(ds);
                }
                return lista;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Crear un registro de Producto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosCrear(info);
                int result = Create("Producto_Crear", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para actualizar un registro de Producto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosActualizar(info);
                Update("Producto_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorPagina", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorPaginaLoteExistencia(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorDescripcionLoteExistencia", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPaginaLoteExistencia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorPaginaLoteExistenciaCantidadCero(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorDescripcionLoteExistenciaCantidadCero", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPaginaLoteExistencia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="descripcion">Descripción de la Producto</param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Producto_ObtenerPorDescripcion", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="pagina">contenedor con los parámetros de paginación</param>
        /// <param name="producto">contenedor con los parámetros de busqueda</param>
        /// <param name="dependencias">contenedor con las dependencias de busqueda</param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorPaginaSubFamilia(pagina, producto, dependencias);
                DataSet ds = Retrieve("Producto_ObtenerPorPaginaSubFamilia", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPaginaSubFamilia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="pagina">contenedor con los parámetros de paginación</param>
        /// <param name="producto">contenedor con los parámetros de busqueda</param>
        /// <param name="dependencias">contenedor con las dependencias de busqueda</param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> Centros_ObtenerPorPaginaSubFamilia(PaginacionInfo pagina, ProductoInfo producto, IList<IDictionary<IList<String>, Object>> dependencias)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.Centros_ObtenerPorPaginaSubFamilia(pagina, producto, dependencias);
                DataSet ds = Retrieve("ProductoCentros_ObtenerPorPaginaSubFamilia", parameters);                                       
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.Centros_ObtenerPorPaginaSubFamilia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoIDSubFamilia(producto, dependencias);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDSubFamilia", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDSubFamilia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoID(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoID", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoIDLoteExistencia(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDLoteExistencia", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDLoteExistencia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorIDLoteExistenciaCantidadCero(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDLoteExistenciaCantidadCero", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDLoteExistencia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Regresa una lista de Productos filtrados por estado
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal List<ProductoInfo> ObtenerPorEstados(EstatusEnum estatus)
        {
            List<ProductoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorEstatus(estatus);
                using (IDataReader reader = RetrieveReader("Producto_ObtenerPorEstado", parameters))
                {
                    if (ValidateDataReader(reader))
                    {
                        lista = MapProductoDAL.ObtenerProductosEstado(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }
                return lista;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de productos con su entidad completa
        /// </summary>
        /// <returns></returns>
        public IList<ProductoInfo> ObtenerTodosCompleto()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Producto_ObtenerTodos");
                IList<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerTodosCompleto(ds);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorPedidoID(pedidoID, productoID);
                DataSet ds = Retrieve("Producto_ObtenerPorFolioPedido", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPedidoID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorPedidoPaginado(pagina, filtro, pedidoID);
                DataSet ds = Retrieve("Producto_ObtenerPorFolioPedidoPaginado", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPedidoPaginado(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene un producto del folio
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorFolioSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorFolioSalida(salida);
                DataSet ds = Retrieve("Producto_ObtenerPorFolioSalida", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorFolioSalida(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal ResultadoInfo<ProductoInfo> ObtenerPorFamiliaPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorFamiliaPaginado(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorFamiliaPaginado", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPedidoPaginado(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto sin importar si esta activo
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorIDSinActivo(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorIDSinActivo(productoActual);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDSinActivo", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="pagina"> </param>
        /// <param name="filtro">Obtiene una entidad Producto por su descripcion o Id</param>
        /// <returns></returns>
        public ResultadoInfo<ProductoInfo> ObtenerCompletoPorFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerCompletoPorFamilia(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerCompletoPorFamilia", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result  = MapProductoDAL.ObtenerCompletoPorFamilia(ds);
                }

                return  result;
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

        ///// <summary>
        ///// Obtiene una entidad de Producto por su descripcion
        ///// </summary>
        ///// <param name="filtro">Obtiene una entidad Producto por su descripcion o Id</param>
        ///// <returns></returns>
        //public ProductoInfo ObtenerCompletoPorID(ProductoInfo filtro)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoID(filtro);
        //        DataSet ds = Retrieve("Producto_ObtenerPorProductoID", parameters);
        //        ProductoInfo result = null;
        //        if (ValidateDataSet(ds))
        //        {
        //            result = MapProductoDAL.ObtenerPorProductoID(ds);
        //        }

        //        return result;
        //    }
        //    catch (ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}

        /// <summary>
        /// Obtiene un producto por id y subfamiliaid
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorIdSubFamiliaId(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoIdSubFamiliaId(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDSubFamiliaID", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroAlmacen(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorPaginaFiltroAlmacen(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorPaginaFiltroAlmacen", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPaginaSubFamilia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="producto"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorProductoIdAlmacenId(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoIdAlmacenId(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDAlmacenID", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDSubFamilia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada de productos que tengan programacion fletes interna
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorPaginaTengaProgramacionFletesInterna(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorPaginaTengaProgramacionFletesInterna(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorPaginaTenganProgramacionFletesInterna", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por id que tenga programacion fletes interna
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorProductoIdTengaProgramacionFleteInterna(ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoIdTengaProgramacionFleteInterna(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDTengaProgramacionFleteInterna", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDSubFamilia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal ResultadoInfo<ProductoInfo> ObtenerPorFamiliasPaginado(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorFamiliasPaginado(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorFamiliasPaginado", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPedidoPaginado(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal ProductoInfo ObtenerPorProductoIDFamilias(ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorProductoIDFamilias(filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDFamilias", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDFamilias(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorPaginaFiltroFamiliaSubfamilias(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorPaginaFiltroFamiliaSubfamilias", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por ID, de familia Materia Primas SubFamilia Granos
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorIDFamiliaIdSubFamiliaId(ProductoInfo productoActual)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorIDFamiliaIdSubFamiliaId(productoActual);
                DataSet ds = Retrieve("Producto_ObtenerPorProductoIDFamiliaIDSubFamiliaID", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorProductoIDLoteExistencia(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un producto por Descripcion, de familia Materia Primas SubFamilia Granos
        /// </summary>
        /// <param name="productoActual"></param>
        /// <returns></returns>
        internal ResultadoInfo<ProductoInfo> ObtenerPorDescripcionSubFamilia(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerPorDescripcionSubFamilia(pagina, filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorDescricionPaginado", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPedidoPaginado(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorProductoIndicador(producto);
                DataSet ds = Retrieve("Producto_ObtenerPorIndicador", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorIndicador(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            ResultadoInfo<ProductoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorIndicadorPagina(pagina,
                                                                                                           filtro);
                DataSet ds = Retrieve("Producto_ObtenerPorIndicadorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProductoDAL.ObtenerPorIndicadorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;

        }

        internal ResultadoInfo<ProductoInfo> ObtenerProductosPorTratamientoPorXML(List<TratamientoInfo> tratamientos)
        {
            ResultadoInfo<ProductoInfo> lista = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerProductosPorTratamientoPorXML(tratamientos);
                DataSet ds = Retrieve("Producto_ObtenerPorTratamientoPorXML", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapProductoDAL.ObtenerDesdeTratamientos(ds);
                }
                return lista;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="materialSAP">Material SAP</param>
        /// <returns></returns>
        internal ProductoInfo ObtenerPorMaterialSAP(string materialSAP)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorMaterialSAP(materialSAP);
                DataSet ds = Retrieve("Producto_ObtenerPorMaterialSAP", parameters);
                ProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorMaterialSAP(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Muestra las productos activos, con existencias en algun almacen de la organizacion al que pertenezca un usuario de la subfamilia indicada, de forma paginada
        /// </summary>
        /// <param name="pagina">Informacion de paginacion</param>
        /// <param name="filtro">Filtro de busqueda</param>
        /// <returns>Regresa una lista paginada de productos que cumpla con los criterios solicitados.</returns>
        public ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(PaginacionInfo pagina, ProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProductoDAL.ObtenerParametrosPorPaginaFiltroSubFamiliaParaEnvioAlimento(pagina, filtro);
                DataSet ds = Retrieve("EnvioAlimento_ObtenerProductoDisponiblePorPagina", parameters);
                ResultadoInfo<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProductoDAL.ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
 