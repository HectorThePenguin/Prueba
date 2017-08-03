using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CuentaSAPDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de CuentaSAP
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal void Crear(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosCrear(info);
                Create("CuentaSAP_Crear", parameters);
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
        ///     Metodo para Actualizar un nuevo registro de CuentaSAP
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosActualizar(info);
                Update("CuentaSAP_Actualizar", parameters);
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
        internal ResultadoInfo<CuentaSAPInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[CuentaSAP_ObtenerPorPagina]", parameters);
                ResultadoInfo<CuentaSAPInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CuentaSAPInfo> ObtenerPorPaginaSinId(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorPaginaSinId(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[CuentaSAP_ObtenerPorPagina]", parameters);
                ResultadoInfo<CuentaSAPInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorPagina(ds);
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
        internal IList<CuentaSAPInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                using (IDataReader reader = RetrieveReader("CuentaSAP_ObtenerTodos"))
                {
                    IList<CuentaSAPInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapCuentaSAPDAL.ObtenerTodos(reader);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaSAPInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CuentaSAP_ObtenerTodos", parameters);
                IList<CuentaSAPInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de CuentaSAP
        /// </summary>
        /// <param name="cuentaSAPID">Identificador de la CuentaSAP</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorID(int cuentaSAPID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorID(cuentaSAPID);
                DataSet ds = Retrieve("CuentaSAP_ObtenerPorID", parameters);
                CuentaSAPInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorID(ds);
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
        ///     Obtiene un registro de CuentaSAP
        /// </summary>
        /// <param name="cuentaSAP">Identificador de la CuentaSAP</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorCuentaSAP(string cuentaSAP)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorCuentaSAP(cuentaSAP);
                DataSet ds = Retrieve("CuentaSAP_ObtenerPorCuentaSAP", parameters);
                CuentaSAPInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorID(ds);
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
        ///     Obtiene una lista de CuentasSAP con filtro
        /// </summary>
        /// <param name="cuentaSAP">Identificador de la CuentaSAP</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorFiltro(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorFiltro(cuentaSAP);
                DataSet ds = Retrieve("CuentaSAP_ObtenerPorFiltro", parameters);
                CuentaSAPInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorFiltro(ds);
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
        /// Obtiene todas las cuentas sap
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CuentaSAPInfo> ObtenerPorPaginaCuentasSap(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorPaginaCuentasSap(pagina, filtro);
                DataSet ds = Retrieve("[CuentaSAP_ObtenerTodasPorPagina]", parameters);
                ResultadoInfo<CuentaSAPInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorPagina(ds);
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
        /// Obtiene por filtro sin tipo
        /// </summary>
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorFiltroSinTipo(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCuentaSAPDAL.ObtenerParametrosPorFiltroSinTipo(cuentaSAP);
                DataSet ds = Retrieve("CuentaSAP_ObtenerPorFiltroSinTipo", parameters);
                CuentaSAPInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaSAPDAL.ObtenerPorFiltro(ds);
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

