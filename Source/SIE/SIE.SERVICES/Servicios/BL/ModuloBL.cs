using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class ModuloBL
    {
        /// <summary>
        /// Obtiene una lista de Modulos
        /// </summary>
        /// <returns></returns>
        public ResultadoInfo<ModuloInfo> ObtenerTodos()
        {
            ResultadoInfo<ModuloInfo> result;
            try
            {
                Logger.Info();
                var moduloDAL = new ModuloDAL();
                result = moduloDAL.ObtenerTodos();               
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
            return result;
        }

        /// <summary>
        /// Obtiene un modulo 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ModuloInfo>ObtenerPorId(ModuloInfo filtro)
        {
            ResultadoInfo<ModuloInfo> result;
            try
            {
                Logger.Info();
                var moduloDAL = new ModuloDAL();
                result = moduloDAL.ObtenerPorId(filtro);
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
            return result;
        }

        /// <summary>
        /// Obtiene un lista de modulos
        /// </summary>
        /// <returns></returns>
        public IList<ModuloInfo> ObtenerTodosAsList()
        {
            IList<ModuloInfo> result;
            try
            {
                Logger.Info();
                var moduloDAL = new ModuloDAL();
                result = moduloDAL.ObtenerTodosAsList();
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
            return result;
        }

      }
}
