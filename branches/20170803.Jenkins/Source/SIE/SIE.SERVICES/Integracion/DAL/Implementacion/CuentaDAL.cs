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
    internal class CuentaDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de Cuenta
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal void Crear(CuentaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosCrear(info);
                Create("Cuenta_Crear", parameters);
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
        ///     Metodo para Actualizar un nuevo registro de Cuenta
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CuentaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosActualizar(info);
                Update("Cuenta_Actualizar", parameters);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Cuenta_ObtenerPorPagina]", parameters);
                ResultadoInfo<CuentaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Cuenta_ObtenerTodos");
                IList<CuentaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista de Cuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Cuenta_ObtenerTodos", parameters);
                IList<CuentaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de Cuenta
        /// </summary>
        /// <param name="cuentaID">Identificador de la Cuenta</param>
        /// <returns></returns>
        internal CuentaInfo ObtenerPorID(int cuentaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosPorID(cuentaID);
                DataSet ds = Retrieve("Cuenta_ObtenerPorID", parameters);
                CuentaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerPorID(ds);
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
        ///     Obtiene un registro de Cuenta
        /// </summary>
        /// <param name="cuenta">Identificador de la Cuenta</param>
        /// <returns></returns>
        internal CuentaInfo ObtenerPorIDGastosMateriasPrimas(CuentaInfo cuenta)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosPorID(cuenta);
                DataSet ds = Retrieve("Cuenta_ObtenerPorID", parameters);
                CuentaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerPorID(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <param name="entradaId">Identificador de la entrada</param>
        /// <returns></returns>
        internal IList<CuentaInfo> ObtenerPorTipoCuentaID(int entradaId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosPorTipoCuentaID(entradaId);
                DataSet ds = Retrieve("Cuenta_ObtenerPorTipoCuentaID", parameters);
                IList<CuentaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerPorTipoCuentaID(ds);
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
        ///     Obtiene una entidad Clave Contable por su Clave de Cuenta y su Organizacion
        /// </summary>
        /// <param name="claveCuenta">Clave de la Cuenta a buscar</param>
        /// /// <param name="organizacionID">Organizacion de la Cuenta a buscar</param>
        internal ClaveContableInfo ObtenerPorClaveCuentaOrganizacion(string claveCuenta, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerPorClaveCuentaOrganizacion(claveCuenta, organizacionID);
                DataSet ds = Retrieve("Cuenta_ObtenerCuentaPorClaveCuenta", parameters);
                ClaveContableInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerPorClaveCuentaOrganizacion(ds);
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
        /// Obtiene un registro de Cuenta
        /// </summary>
        /// <param name="descripcion">Descripción de la Cuenta</param>
        /// <returns></returns>
        public CuentaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Cuenta_ObtenerPorDescripcion", parameters);
                CuentaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaDAL.ObtenerPorDescripcion(ds);
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
