using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class ParametroTrampaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ParametroTrampa
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ParametroTrampaInfo info)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                int result = info.ParametroTrampaID;
                if (info.ParametroTrampaID == 0)
                {
                    result = parametroTrampaDAL.Crear(info);
                }
                else
                {
                    parametroTrampaDAL.Actualizar(info);
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
        internal ResultadoInfo<ParametroTrampaInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroTrampaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                ResultadoInfo<ParametroTrampaInfo> result = parametroTrampaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ParametroTrampa
        /// </summary>
        /// <returns></returns>
        internal IList<ParametroTrampaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                IList<ParametroTrampaInfo> result = parametroTrampaDAL.ObtenerTodos();
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
        internal IList<ParametroTrampaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                IList<ParametroTrampaInfo> result = parametroTrampaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ParametroTrampa por su Id
        /// </summary>
        /// <param name="parametroTrampaID">Obtiene una entidad ParametroTrampa por su Id</param>
        /// <returns></returns>
        internal ParametroTrampaInfo ObtenerPorID(int parametroTrampaID)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                ParametroTrampaInfo result = parametroTrampaDAL.ObtenerPorID(parametroTrampaID);
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
        /// Obtiene una entidad ParametroTrampa por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ParametroTrampaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                ParametroTrampaInfo result = parametroTrampaDAL.ObtenerPorDescripcion(descripcion);
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
        internal ParametroTrampaInfo ObtenerPorParametroTrampa(int parametroID, int trampaID)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                ParametroTrampaInfo result = parametroTrampaDAL.ObtenerPorParametroTrampa(parametroID, trampaID);
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
        /// <param name="info"></param>
        internal int ClonarParametroTrampa(FiltroClonarParametroTrampa info)
        {
            try
            {
                Logger.Info();
                var parametroTrampaDAL = new ParametroTrampaDAL();
                return parametroTrampaDAL.ClonarParametroTrampa(info);
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
