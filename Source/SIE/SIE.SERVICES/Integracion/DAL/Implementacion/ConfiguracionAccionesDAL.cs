using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ConfiguracionAccionesDAL : DALBase
    {
        /// <summary>
        /// Obtiene todas las acciones registradas
        /// </summary>
        /// <returns></returns>
        internal IList<ConfiguracionAccionesInfo> ObtenerTodos()
        {
            IList<ConfiguracionAccionesInfo> lista = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ConfiguracionAcciones_ObtenerTodas");
                
                if (ValidateDataSet(ds))
                {
                    lista = MapConfiguracionAccionesDAL.ObtenerDatosConfiguracionAcciones(ds);
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
        /// Obtiene una configuracion de accion por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        internal ConfiguracionAccionesInfo ObtenerPorCodigo(AccionesSIAPEnum codigo)
        {
            ConfiguracionAccionesInfo valor = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAccionesDAL.ObtenerParametrosPorCodigo(codigo);
                DataSet ds = Retrieve("ConfiguracionAcciones_ObtenerPorCodigo");

                if (ValidateDataSet(ds))
                {
                    valor = MapConfiguracionAccionesDAL.ObtenerDatosConfiguracionAccion(ds);
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

            return valor;
        }

        /// <summary>
        /// Actualiza la fecha de ejecucion a la base de datos
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        internal bool ActualizarEjecucionTarea(ConfiguracionAccionesInfo tarea)
        {
            bool valor = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAccionesDAL.ObtenerParametrosParaActualizarEjecucion(tarea);
                Update("ConfiguracionAcciones_ActualizarEjecucionTarea", parameters);
                valor = true;
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

            return valor;
        }
        /// <summary>
        /// Actuzaliza la fecha de ejecucion del servicio alerta
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        internal bool ActualizarFechaEjecucion(ConfiguracionAccionesInfo tarea)
        {
            bool valor = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAccionesDAL.ObtenerParametrosActualizarFechaEjecucion(tarea);
                Update("ConfiguracionAcciones_ActualizaFechaEjecucion", parameters);
                valor = true;
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

            return valor;
        }
    }
}
