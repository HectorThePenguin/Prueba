using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    class ModuloDAL : DALBase
    {
        /// <summary>
        /// Obtiene una lista de alertas filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<ModuloInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                //Dictionary<string, object> parameters = AuxModuloDAL.ObtenerParametrosTodos();
                DataSet ds = Retrieve("Modulo_ObtenerTodos", null);
                ResultadoInfo<ModuloInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapModuloDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista de modulos como una lista (para cargar un comboBox)
        /// </summary>
        /// <returns></returns>
        internal IList<ModuloInfo> ObtenerTodosAsList()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Modulo_ObtenerTodos", null);
                IList<ModuloInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapModuloDAL.ObtenerTodosAsList(ds);
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
        /// Busca la información de un modulo
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ModuloInfo> ObtenerPorId(ModuloInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxModuloDAL.ObtenerParametrosPorId(filtro);
                DataSet ds = Retrieve("Modulo_ObtenerPorId", parameters);
                ResultadoInfo<ModuloInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapModuloDAL.ObtenerPorId(ds);
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
