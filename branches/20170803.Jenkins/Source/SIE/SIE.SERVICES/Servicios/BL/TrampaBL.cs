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
    internal class TrampaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Trampa
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TrampaInfo info)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                int result = info.TrampaID;
                if (info.TrampaID == 0)
                {
                    result = trampaDAL.Crear(info);
                }
                else
                {
                    trampaDAL.Actualizar(info);
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
        internal ResultadoInfo<TrampaInfo> ObtenerPorPagina(PaginacionInfo pagina, TrampaInfo filtro)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                ResultadoInfo<TrampaInfo> result = trampaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Trampa
        /// </summary>
        /// <returns></returns>
        internal IList<TrampaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                IList<TrampaInfo> result = trampaDAL.ObtenerTodos();
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
        internal IList<TrampaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                IList<TrampaInfo> result = trampaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Trampa por su Id
        /// </summary>
        /// <param name="trampaID">Obtiene una entidad Trampa por su Id</param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPorID(int trampaID)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                TrampaInfo result = trampaDAL.ObtenerPorID(trampaID);
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
        /// Obtiene una entidad Trampa por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                TrampaInfo result = trampaDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene trampas por su Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<TrampaInfo> ObtenerPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                List<TrampaInfo> result = trampaDAL.ObtenerPorOrganizacion(organizacionID);
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
        /// Obtiene una entidad Trampa por su hostName
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPorHostName(string hostName)
        {
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                TrampaInfo result = trampaDAL.ObtenerPorHostName(hostName);
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
        /// Metrodo Para obtener la trampa
        /// </summary>
        internal TrampaInfo ObtenerTrampa(TrampaInfo trampaInfo)
        {
            TrampaInfo result;
            try
            {
                Logger.Info();
                var trampaDAL = new TrampaDAL();
                result = trampaDAL.ObtenerTrampa(trampaInfo);
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

