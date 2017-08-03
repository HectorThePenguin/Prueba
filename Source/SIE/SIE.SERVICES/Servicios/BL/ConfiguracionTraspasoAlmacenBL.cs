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
    public class ConfiguracionTraspasoAlmacenBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(ConfiguracionTraspasoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var configuracionTraspasoAlmacenDAL = new ConfiguracionTraspasoAlmacenDAL();
                int result = info.ConfiguracionTraspasoAlmacenID;
                if (info.ConfiguracionTraspasoAlmacenID == 0)
                {
                    result = configuracionTraspasoAlmacenDAL.Crear(info);
                }
                else
                {
                    configuracionTraspasoAlmacenDAL.Actualizar(info);
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
        public ResultadoInfo<ConfiguracionTraspasoAlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionTraspasoAlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionTraspasoAlmacenDAL = new ConfiguracionTraspasoAlmacenDAL();
                ResultadoInfo<ConfiguracionTraspasoAlmacenInfo> result = configuracionTraspasoAlmacenDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <returns></returns>
        public IList<ConfiguracionTraspasoAlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var configuracionTraspasoAlmacenDAL = new ConfiguracionTraspasoAlmacenDAL();
                IList<ConfiguracionTraspasoAlmacenInfo> result = configuracionTraspasoAlmacenDAL.ObtenerTodos();
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
        public IList<ConfiguracionTraspasoAlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var configuracionTraspasoAlmacenDAL = new ConfiguracionTraspasoAlmacenDAL();
                IList<ConfiguracionTraspasoAlmacenInfo> result = configuracionTraspasoAlmacenDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ConfiguracionTraspasoAlmacen por su Id
        /// </summary>
        /// <param name="configuracionTraspasoAlmacenID">Obtiene una entidad ConfiguracionTraspasoAlmacen por su Id</param>
        /// <returns></returns>
        public ConfiguracionTraspasoAlmacenInfo ObtenerPorID(int configuracionTraspasoAlmacenID)
        {
            try
            {
                Logger.Info();
                var configuracionTraspasoAlmacenDAL = new ConfiguracionTraspasoAlmacenDAL();
                ConfiguracionTraspasoAlmacenInfo result = configuracionTraspasoAlmacenDAL.ObtenerPorID(configuracionTraspasoAlmacenID);
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
        /// Obtiene una entidad ConfiguracionTraspasoAlmacen por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ConfiguracionTraspasoAlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var configuracionTraspasoAlmacenDAL = new ConfiguracionTraspasoAlmacenDAL();
                ConfiguracionTraspasoAlmacenInfo result = configuracionTraspasoAlmacenDAL.ObtenerPorDescripcion(descripcion);
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

