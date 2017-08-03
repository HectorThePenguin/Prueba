using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ParametroPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Parametro
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ParametroInfo info)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                int result = parametroBL.Guardar(info);
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
        public ResultadoInfo<ParametroInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                ResultadoInfo<ParametroInfo> result = parametroBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<ParametroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                IList<ParametroInfo> result = parametroBL.ObtenerTodos();
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
        public IList<ParametroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                IList<ParametroInfo> result = parametroBL.ObtenerTodos(estatus);
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
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ParametroInfo ObtenerPorID(ParametroInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                ParametroInfo result = parametroBL.ObtenerPorID(filtro.ParametroID);
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
        /// <param name="parametroID"></param>
        /// <returns></returns>
        public ParametroInfo ObtenerPorID(int parametroID)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                ParametroInfo result = parametroBL.ObtenerPorID(parametroID);
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
        public ParametroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                ParametroInfo result = parametroBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad por su Clave
        /// </summary>
        /// <param name="parametroInfo"></param>
        /// <returns></returns>
        public ParametroInfo ObtenerPorParametroTipoParametro(ParametroInfo parametroInfo)
        {
            try
            {
                Logger.Info();
                var parametroBL = new ParametroBL();
                ParametroInfo result = parametroBL.ObtenerPorParametroTipoParametro(parametroInfo);
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

