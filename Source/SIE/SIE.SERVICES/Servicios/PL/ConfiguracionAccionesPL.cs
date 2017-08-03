using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionAccionesPL
    {
        /// <summary>
        /// Obtiene la configuracion de la base de datos para acciones a ejecutar
        /// </summary>
        /// <returns></returns>
        public IList<ConfiguracionAccionesInfo> ObtenerTodos()
        {
            IList<ConfiguracionAccionesInfo> lista;
            try
            {
                Logger.Info();
                var accionesDal = new ConfiguracionAccionesBL();
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
        public ConfiguracionAccionesInfo ObtenerAccion(AccionesSIAPEnum codigo)
        {
            ConfiguracionAccionesInfo retorno;
            try
            {
                Logger.Info();
                var accionBL = new ConfiguracionAccionesBL();
                retorno = accionBL.ObtenerAccion(codigo);
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
        /// Actualiza el tiempo de ejecucion de la tarea
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        public bool ActualizarEjecucionTarea(ConfiguracionAccionesInfo tarea)
        {
            bool retVal = false;
            try
            {
                Logger.Info();
                var accionBL = new ConfiguracionAccionesBL();
                retVal = accionBL.ActualizarEjecucionTarea(tarea);
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
            return retVal;
        }
        /// <summary>
        /// Actualiza la fecha ejecucion del servicio
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        public bool ActualizarFechaEjecucion(ConfiguracionAccionesInfo tarea)
        {
            bool retVal = false;
            try
            {
                Logger.Info();
                var accionBL = new ConfiguracionAccionesBL();
                retVal = accionBL.ActualizarFechaEjecucion(tarea);
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
            return retVal;
        }
    }
}
