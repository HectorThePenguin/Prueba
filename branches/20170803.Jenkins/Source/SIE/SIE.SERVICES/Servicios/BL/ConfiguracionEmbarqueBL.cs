using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ConfiguracionEmbarqueBL
    {
        /// <summary>
        ///     Metodo que guarda un ConfiguracionEmbarque
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(ConfiguracionEmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                if (info.ConfiguracionEmbarqueID != 0)
                {
                    configuracionEmbarqueDAL.Actualizar(info);
                }
                else
                {
                    configuracionEmbarqueDAL.Crear(info);
                }
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
        ///     Obtiene una lista de los ConfiguracionEmbarquees
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<ConfiguracionEmbarqueInfo> ObtenerTodos()
        {
            IList<ConfiguracionEmbarqueInfo> result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                result = configuracionEmbarqueDAL.ObtenerTodos();
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
        ///   Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<ConfiguracionEmbarqueInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                IList<ConfiguracionEmbarqueInfo> result = configuracionEmbarqueDAL.ObtenerTodos(estatus);

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
        ///     Obtiene un camión por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="filtro"> </param>
        /// <returns></returns>
        internal ConfiguracionEmbarqueInfo ObtenerPorID(int filtro)
        {
            ConfiguracionEmbarqueInfo result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                result = configuracionEmbarqueDAL.ObtenerPorID(filtro);
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
        ///     Obtiene los ConfiguracionEmbarquees de un Proveedor
        /// </summary>
        /// <param name="organizacionOrigenId">Organización origen de la configuración</param>
        /// <param name="organizacionDestinoId">Organización destino de la configuración</param>
        /// <returns></returns>
        internal ConfiguracionEmbarqueInfo ObtenerPorOrganizacion(int organizacionOrigenId, int organizacionDestinoId)
        {
            ConfiguracionEmbarqueInfo result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                result = configuracionEmbarqueDAL.ObtenerPorOrganizacion(organizacionOrigenId, organizacionDestinoId);
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
        internal ResultadoInfo<ConfiguracionEmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionEmbarqueInfo filtro)
        {
            ResultadoInfo<ConfiguracionEmbarqueInfo> result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                result = configuracionEmbarqueDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene la lista de las Rutas filtrado por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorId(ConfiguracionEmbarqueInfo filtro)
        {
            List<ConfiguracionEmbarqueDetalleInfo> result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                result = configuracionEmbarqueDAL.ObtenerRutasPorId(filtro);
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
        ///     Obtiene la lista de las Rutas filtrado por Origen y Destino
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorDescripcion(ConfiguracionEmbarqueInfo filtro)
        {
            List<ConfiguracionEmbarqueDetalleInfo> result;
            try
            {
                Logger.Info();
                var configuracionEmbarqueDAL = new ConfiguracionEmbarqueDAL();
                result = configuracionEmbarqueDAL.ObtenerRutasPorDescripcion(filtro);
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
