using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ClaseCostoProductoDAL : DALBase
    {
        /// <summary>
        /// Obtiene una lista de Clase Costo Producto por Almacen
        /// </summary>
        /// <param name="almacenId"></param>
        /// <returns></returns>
        internal IList<ClaseCostoProductoInfo> ObtenerPorAlmacen(int almacenId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerParametrosPorAlmacen(almacenId);
                using (IDataReader reader = RetrieveReader("ClaseCostoProducto_ObtenerPorAlmacen", parameters))
                {
                    IList<ClaseCostoProductoInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapClaseCostoProductoDAL.ObtenerClaseCostoProductoPorAlmacen(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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
        }

        /// <summary>
        /// Obtiene el producto por almacen
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="almacenId"></param>
        /// <returns></returns>
        internal ClaseCostoProductoInfo ObtenerPorProductoAlmacen(int productoId, int almacenId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxClaseCostoProductoDAL.ObtenerParametrosPorProductoAlmacen(productoId, almacenId);
                using (IDataReader reader = RetrieveReader("ClaseCostoProducto_ObtenerPorProductoAlmacen", parameters))
                {
                    ClaseCostoProductoInfo result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapClaseCostoProductoDAL.ObtenerClaseCostoPorProductoAlmacen(reader);
                    }
                    return result;
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
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Metodo para Crear un registro de ClaseCostoProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(ClaseCostoProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerParametrosCrear(info);
                int result = Create("ClaseCostoProducto_Crear", parameters);
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
        /// Metodo para actualizar un registro de ClaseCostoProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(ClaseCostoProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerParametrosActualizar(info);
                Update("ClaseCostoProducto_Actualizar", parameters);
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
        public ResultadoInfo<ClaseCostoProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, ClaseCostoProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ClaseCostoProducto_ObtenerPorPagina", parameters);
                ResultadoInfo<ClaseCostoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClaseCostoProductoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ClaseCostoProducto
        /// </summary>
        /// <returns></returns>
        public IList<ClaseCostoProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ClaseCostoProducto_ObtenerTodos");
                IList<ClaseCostoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClaseCostoProductoDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<ClaseCostoProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ClaseCostoProducto_ObtenerTodos", parameters);
                IList<ClaseCostoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClaseCostoProductoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ClaseCostoProducto
        /// </summary>
        /// <param name="claseCostoProductoID">Identificador de la ClaseCostoProducto</param>
        /// <returns></returns>
        public ClaseCostoProductoInfo ObtenerPorID(int claseCostoProductoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerParametrosPorID(claseCostoProductoID);
                DataSet ds = Retrieve("ClaseCostoProducto_ObtenerPorID", parameters);
                ClaseCostoProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClaseCostoProductoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ClaseCostoProducto
        /// </summary>
        /// <param name="descripcion">Descripción de la ClaseCostoProducto</param>
        /// <returns></returns>
        public ClaseCostoProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClaseCostoProductoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ClaseCostoProducto_ObtenerPorDescripcion", parameters);
                ClaseCostoProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapClaseCostoProductoDAL.ObtenerPorDescripcion(ds);
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
