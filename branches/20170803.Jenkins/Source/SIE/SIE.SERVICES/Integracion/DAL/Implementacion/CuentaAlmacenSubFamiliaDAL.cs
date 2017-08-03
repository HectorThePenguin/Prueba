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
    internal class CuentaAlmacenSubFamiliaDAL : DALBase
    {
        /// <summary>
        /// Obtiene una lista de Costos por SubFamilia
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal IList<CuentaAlmacenSubFamiliaInfo> ObtenerCostosSubFamilia(int almacenID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCuentaAlmacenSubFamiliaDAL.ObtenerParametrosPorAlmacen(almacenID);
                using (IDataReader reader = RetrieveReader("CuentaAlmacenSubFamilia_ObtenerPorAlmacen", parameters))
                {
                    IList<CuentaAlmacenSubFamiliaInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapCuentaAlmacenSubFamiliaDAL.ObtenerCuentaAlmacenSubFamiliaPorAlmacen(reader);
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
        /// Metodo para Crear un registro de CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CuentaAlmacenSubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaAlmacenSubFamiliaDAL.ObtenerParametrosCrear(info);
                int result = Create("CuentaAlmacenSubFamilia_Crear", parameters);
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
        /// Metodo para actualizar un registro de CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CuentaAlmacenSubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaAlmacenSubFamiliaDAL.ObtenerParametrosActualizar(info);
                Update("CuentaAlmacenSubFamilia_Actualizar", parameters);
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
        public ResultadoInfo<CuentaAlmacenSubFamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaAlmacenSubFamiliaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaAlmacenSubFamiliaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CuentaAlmacenSubFamilia_ObtenerPorPagina", parameters);
                ResultadoInfo<CuentaAlmacenSubFamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaAlmacenSubFamiliaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CuentaAlmacenSubFamilia
        /// </summary>
        /// <returns></returns>
        public IList<CuentaAlmacenSubFamiliaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CuentaAlmacenSubFamilia_ObtenerTodos");
                IList<CuentaAlmacenSubFamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaAlmacenSubFamiliaDAL.ObtenerTodos(ds);
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
        public IList<CuentaAlmacenSubFamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaAlmacenSubFamiliaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CuentaAlmacenSubFamilia_ObtenerTodos", parameters);
                IList<CuentaAlmacenSubFamiliaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaAlmacenSubFamiliaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="cuentaAlmacenSubFamiliaID">Identificador de la CuentaAlmacenSubFamilia</param>
        /// <returns></returns>
        public CuentaAlmacenSubFamiliaInfo ObtenerPorID(int cuentaAlmacenSubFamiliaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaAlmacenSubFamiliaDAL.ObtenerParametrosPorID(cuentaAlmacenSubFamiliaID);
                DataSet ds = Retrieve("CuentaAlmacenSubFamilia_ObtenerPorID", parameters);
                CuentaAlmacenSubFamiliaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaAlmacenSubFamiliaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="descripcion">Descripción de la CuentaAlmacenSubFamilia</param>
        /// <returns></returns>
        public CuentaAlmacenSubFamiliaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaAlmacenSubFamiliaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CuentaAlmacenSubFamilia_ObtenerPorDescripcion", parameters);
                CuentaAlmacenSubFamiliaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaAlmacenSubFamiliaDAL.ObtenerPorDescripcion(ds);
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
