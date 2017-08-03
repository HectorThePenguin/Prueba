using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class ConfiguracionSemanaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ConfiguracionSemana
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ConfiguracionSemanaInfo info)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                int result = info.ConfiguracionSemanaID;
                if (info.ConfiguracionSemanaID == 0)
                {
                    result = configuracionSemanaDAL.Crear(info);
                }
                else
                {
                    configuracionSemanaDAL.Actualizar(info);
                }
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
        internal ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionSemanaInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                ResultadoInfo<ConfiguracionSemanaInfo> result = configuracionSemanaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ConfiguracionSemana
        /// </summary>
        /// <returns></returns>
        internal IList<ConfiguracionSemanaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                IList<ConfiguracionSemanaInfo> result = configuracionSemanaDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<ConfiguracionSemanaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                IList<ConfiguracionSemanaInfo> result = configuracionSemanaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ConfiguracionSemana por su Id
        /// </summary>
        /// <param name="configuracionSemanaID">Obtiene una entidad ConfiguracionSemana por su Id</param>
        /// <returns></returns>
        internal ConfiguracionSemanaInfo ObtenerPorID(int configuracionSemanaID)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                ConfiguracionSemanaInfo result = configuracionSemanaDAL.ObtenerPorID(configuracionSemanaID);
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
        /// Obtiene una entidad ConfiguracionSemana por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ConfiguracionSemanaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                ConfiguracionSemanaInfo result = configuracionSemanaDAL.ObtenerPorDescripcion(descripcion);
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
        internal ConfiguracionSemanaInfo ObtenerPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                ConfiguracionSemanaInfo result = configuracionSemanaDAL.ObtenerPorOrganizacion(organizacionID);
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
        internal ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, ConfiguracionSemanaInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionSemanaDAL = new ConfiguracionSemanaDAL();
                ResultadoInfo<ConfiguracionSemanaInfo> result = configuracionSemanaDAL.ObtenerPorFiltroPagina(pagina, filtro);
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

