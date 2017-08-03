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
    public class ConfiguracionEmbarquePL
    {
        /// <summary>
        ///     Metodo que guarda un ConfiguracionEmbarque
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(ConfiguracionEmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                configuracionEmbarqueBL.Guardar(info);
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
        ///     Obtiene una lista de los Configuracion de embarque
        /// </summary>
        /// <returns> </returns>
        public IList<ConfiguracionEmbarqueInfo> ObtenerTodos()
        {
            IList<ConfiguracionEmbarqueInfo> lista;
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                lista = configuracionEmbarqueBL.ObtenerTodos();
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
        ///  Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns> </returns>
        public IList<ConfiguracionEmbarqueInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                IList<ConfiguracionEmbarqueInfo> lista = configuracionEmbarqueBL.ObtenerTodos(estatus);

                return lista;
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
        ///      Obtiene un ConfiguracionEmbarque por su Id
        /// </summary>
        /// <param name="filtro">Identificador de la configuración de embarque</param>
        /// <returns> </returns>
        public ConfiguracionEmbarqueInfo ObtenerPorID(int filtro)
        {
            ConfiguracionEmbarqueInfo info;
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                info = configuracionEmbarqueBL.ObtenerPorID(filtro);
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
            return info;
        }

        /// <summary>
        ///     Obtiene los ConfiguracionEmbarquees de un Proveedor
        /// </summary>
        /// <param name="organizacionOrigenId">Organización origen de la configuración</param>
        /// <param name="organizacionDestinoId">Organización destino de la configuración</param>
        /// <returns></returns>
        public ConfiguracionEmbarqueInfo ObtenerPorOrganizacion(int organizacionOrigenId, int organizacionDestinoId)
        {
            ConfiguracionEmbarqueInfo result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                result = configuracionEmbarqueBL.ObtenerPorOrganizacion(organizacionOrigenId, organizacionDestinoId);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ConfiguracionEmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionEmbarqueInfo filtro)
        {
            ResultadoInfo<ConfiguracionEmbarqueInfo> result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                result = configuracionEmbarqueBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene la lista de las Rutas
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorId(ConfiguracionEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                List<ConfiguracionEmbarqueDetalleInfo> lista = configuracionEmbarqueBL.ObtenerRutasPorId(filtro);
                return lista;
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
        /// Obtiene la lista de las Rutas
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorDescripcion(ConfiguracionEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var configuracionEmbarqueBL = new ConfiguracionEmbarqueBL();
                List<ConfiguracionEmbarqueDetalleInfo> lista = configuracionEmbarqueBL.ObtenerRutasPorDescripcion(filtro);
                return lista;
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
