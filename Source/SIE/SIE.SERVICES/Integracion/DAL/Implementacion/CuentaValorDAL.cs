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
    public class CuentaValorDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CuentaValor
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CuentaValorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerParametrosCrear(info);
                int result = Create("CuentaValor_Crear", parameters);
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
        /// Metodo para actualizar un registro de CuentaValor
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CuentaValorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerParametrosActualizar(info);
                Update("CuentaValor_Actualizar", parameters);
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
        public ResultadoInfo<CuentaValorInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaValorInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CuentaValor_ObtenerPorPagina", parameters);
                ResultadoInfo<CuentaValorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaValorDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CuentaValor
        /// </summary>
        /// <returns></returns>
        public IList<CuentaValorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CuentaValor_ObtenerTodos");
                IList<CuentaValorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaValorDAL.ObtenerTodos(ds);
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
        public IList<CuentaValorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CuentaValor_ObtenerTodos", parameters);
                IList<CuentaValorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaValorDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CuentaValor
        /// </summary>
        /// <param name="cuentaValorID">Identificador de la CuentaValor</param>
        /// <returns></returns>
        public CuentaValorInfo ObtenerPorID(int cuentaValorID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerParametrosPorID(cuentaValorID);
                DataSet ds = Retrieve("CuentaValor_ObtenerPorID", parameters);
                CuentaValorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaValorDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CuentaValor
        /// </summary>
        /// <param name="descripcion">Descripción de la CuentaValor</param>
        /// <returns></returns>
        public CuentaValorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CuentaValor_ObtenerPorDescripcion", parameters);
                CuentaValorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaValorDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de CuentaValor
        /// </summary>
        /// <param name="cuentaValor">Descripción de la CuentaValor</param>
        /// <returns></returns>
        public CuentaValorInfo ObtenerPorFiltros(CuentaValorInfo cuentaValor)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaValorDAL.ObtenerParametrosPorFiltros(cuentaValor);
                DataSet ds = Retrieve("CuentaValor_ObtenerPorFiltros", parameters);
                CuentaValorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaValorDAL.ObtenerPorDescripcion(ds);
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

