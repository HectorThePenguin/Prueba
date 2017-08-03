using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ParametroTrampaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ParametroTrampa
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ParametroTrampaInfo info)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                int result = parametroTrampaBL.Guardar(info);
                return result;
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
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ParametroTrampaInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroTrampaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                ResultadoInfo<ParametroTrampaInfo> result = parametroTrampaBL.ObtenerPorPagina(pagina, filtro);
                return result;
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
        }

        /// <summary>
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<ParametroTrampaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                IList<ParametroTrampaInfo> result = parametroTrampaBL.ObtenerTodos();
                return result;
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
        }

        /// <summary>
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<ParametroTrampaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                IList<ParametroTrampaInfo> result = parametroTrampaBL.ObtenerTodos(estatus);
                return result;
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
        }

        /// <summary>
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="parametroTrampaID"></param>
        /// <returns></returns>
        public ParametroTrampaInfo ObtenerPorID(int parametroTrampaID)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                ParametroTrampaInfo result = parametroTrampaBL.ObtenerPorID(parametroTrampaID);
                return result;
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
        }

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ParametroTrampaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                ParametroTrampaInfo result = parametroTrampaBL.ObtenerPorDescripcion(descripcion);
                return result;
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
        }

        /// <summary>
        /// Obtiene un Parametro trampa por parametro y trampa
        /// </summary>
        /// <param name="parametroID"></param>
        /// <param name="trampaID"></param>
        /// <returns></returns>
        public ParametroTrampaInfo ObtenerPorParametroTrampa(int parametroID, int trampaID)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                ParametroTrampaInfo result = parametroTrampaBL.ObtenerPorParametroTrampa(parametroID, trampaID);
                return result;
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
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ParametroTrampa
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int ClonarParametroTrampa(FiltroClonarParametroTrampa info)
        {
            try
            {
                Logger.Info();
                var parametroTrampaBL = new ParametroTrampaBL();
                int result = parametroTrampaBL.ClonarParametroTrampa(info);
                return result;
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
        }
    }
}
