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
    internal class ParametroOrganizacionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ParametroOrganizacion
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ParametroOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                int result = info.ParametroOrganizacionID;
                if (info.ParametroOrganizacionID == 0)
                {
                    result = parametroOrganizacionDAL.Crear(info);
                }
                else
                {
                    parametroOrganizacionDAL.Actualizar(info);
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
        internal ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                ResultadoInfo<ParametroOrganizacionInfo> result = parametroOrganizacionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ParametroOrganizacion
        /// </summary>
        /// <returns></returns>
        internal IList<ParametroOrganizacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                IList<ParametroOrganizacionInfo> result = parametroOrganizacionDAL.ObtenerTodos();
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
        internal IList<ParametroOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                IList<ParametroOrganizacionInfo> result = parametroOrganizacionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ParametroOrganizacion por su Id
        /// </summary>
        /// <param name="parametroOrganizacionID">Obtiene una entidad ParametroOrganizacion por su Id</param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorID(int parametroOrganizacionID)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                ParametroOrganizacionInfo result = parametroOrganizacionDAL.ObtenerPorID(parametroOrganizacionID);
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
        /// Obtiene una entidad ParametroOrganizacion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                ParametroOrganizacionInfo result = parametroOrganizacionDAL.ObtenerPorDescripcion(descripcion);
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
        internal ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                ResultadoInfo<ParametroOrganizacionInfo> result = parametroOrganizacionDAL.ObtenerPorFiltroPagina(pagina, filtro);
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
        /// Obtiene una entidad ParametroOrganizacion por su parámetro y Organizacioón Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorParametroOrganizacionID(ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                ParametroOrganizacionInfo result = parametroOrganizacionDAL.ObtenerPorParametroOrganizacionID(filtro);
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
        /// Obtiene una entidad por organizacion y clave parametro
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="claveParametro"></param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorOrganizacionIDClaveParametro(int organizacionID, string claveParametro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();
                ParametroOrganizacionInfo result = parametroOrganizacionDAL.ObtenerPorOrganizacionIDClaveParametro(organizacionID, claveParametro);
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

