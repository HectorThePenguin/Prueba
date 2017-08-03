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
    public class ConfiguracionSemanaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ConfiguracionSemana
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ConfiguracionSemanaInfo info)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                int result = configuracionSemanaBL.Guardar(info);
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
        public ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionSemanaInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                ResultadoInfo<ConfiguracionSemanaInfo> result = configuracionSemanaBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<ConfiguracionSemanaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                IList<ConfiguracionSemanaInfo> result = configuracionSemanaBL.ObtenerTodos();
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
        public IList<ConfiguracionSemanaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                IList<ConfiguracionSemanaInfo> result = configuracionSemanaBL.ObtenerTodos(estatus);
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
        /// <param name="configuracionSemanaID"></param>
        /// <returns></returns>
        public ConfiguracionSemanaInfo ObtenerPorID(int configuracionSemanaID)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                ConfiguracionSemanaInfo result = configuracionSemanaBL.ObtenerPorID(configuracionSemanaID);
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
        public ConfiguracionSemanaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                ConfiguracionSemanaInfo result = configuracionSemanaBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un registro de ConfiguracionSemana
        /// </summary>
        /// <param name="organizacionID">Organización a la que pertenece la configuración</param>
        /// <returns></returns>
        public ConfiguracionSemanaInfo ObtenerPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                ConfiguracionSemanaInfo result = configuracionSemanaBL.ObtenerPorOrganizacion(organizacionID);
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
        public ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, ConfiguracionSemanaInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                ResultadoInfo<ConfiguracionSemanaInfo> result = configuracionSemanaBL.ObtenerPorFiltroPagina(pagina, filtro);
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

