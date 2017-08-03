using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ConfiguracionAccionesBL
    {
        /// <summary>
        /// Obtiene la configuracion de la base de datos para acciones a ejecutar
        /// </summary>
        /// <returns></returns>
        internal IList<ConfiguracionAccionesInfo> ObtenerTodos()
        {
            IList<ConfiguracionAccionesInfo> lista;
            try
            {
                Logger.Info();
                var accionesDal = new ConfiguracionAccionesDAL();
                lista = accionesDal.ObtenerTodos();
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
            return lista;
        }

       /// <summary>
       /// Obtiene la configuracion de la accion en base al codigo
       /// </summary>
       /// <param name="codigo"></param>
       /// <returns></returns>
        internal ConfiguracionAccionesInfo ObtenerAccion(AccionesSIAPEnum codigo)
        {
            ConfiguracionAccionesInfo retorno;
            try
            {
                Logger.Info();
                var accionesDal = new ConfiguracionAccionesDAL();
                retorno = accionesDal.ObtenerPorCodigo(codigo);
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
            return retorno;
        }

        /// <summary>
        /// Actualiza la ejecucion de una tarea
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        internal bool ActualizarEjecucionTarea(ConfiguracionAccionesInfo tarea)
        {
            bool retorno;
            try
            {
                Logger.Info();
                var accionesDal = new ConfiguracionAccionesDAL();
                retorno = accionesDal.ActualizarEjecucionTarea(tarea);
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
            return retorno;
        }
        /// <summary>
        /// Actualiza la fecha ejecion del servicio
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        internal bool ActualizarFechaEjecucion(ConfiguracionAccionesInfo tarea)
        {
            bool retorno;
            try
            {
                Logger.Info();
                var accionesDal = new ConfiguracionAccionesDAL();
                retorno = accionesDal.ActualizarFechaEjecucion(tarea);
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
            return retorno;
        }
    }
}
