using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CuentaGastosDAL : DALBase
    {
        internal void Guardar(CuentaGastosInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaGastosDAL.ObtenerParametrosGuardar(info);
                Create("[dbo].[CuentaGastos_Guardar]", parameters);
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
        /// Metodo que actualiza un una cuenta de gastos
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CuentaGastosInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaGastosDAL.ObtenerParametrosActualizar(info);
                Update("CuentaGastos_Actualizar", parameters);
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

        internal ResultadoInfo<CuentaGastosInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaGastosInfo filtros)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCuentaGastosDAL.ObtenerParametrosPorPagina(pagina, filtros);
                DataSet ds = Retrieve("[dbo].[CuentaGastos_ObtenerPorPagina]", parameters);
                ResultadoInfo<CuentaGastosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaGastosDAL.ObtenerPorPagina(ds);
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

        internal List<CuentaGastosInfo> ObtenerTodos()
        {
            try
            {
                DataSet ds = Retrieve("[dbo].[CuentaGastos_ObtenerTodos]");
                List<CuentaGastosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCuentaGastosDAL.ObtenerTodos(ds);
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
