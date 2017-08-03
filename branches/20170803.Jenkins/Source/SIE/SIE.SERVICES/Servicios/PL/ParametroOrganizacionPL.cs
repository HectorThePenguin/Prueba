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
    public class ParametroOrganizacionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ParametroOrganizacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ParametroOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                int result = parametroOrganizacionBL.Guardar(info);
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
        public ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                ResultadoInfo<ParametroOrganizacionInfo> result = parametroOrganizacionBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<ParametroOrganizacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                IList<ParametroOrganizacionInfo> result = parametroOrganizacionBL.ObtenerTodos();
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
        public IList<ParametroOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                IList<ParametroOrganizacionInfo> result = parametroOrganizacionBL.ObtenerTodos(estatus);
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
        /// <param name="parametroOrganizacionID"></param>
        /// <returns></returns>
        public ParametroOrganizacionInfo ObtenerPorID(int parametroOrganizacionID)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                ParametroOrganizacionInfo result = parametroOrganizacionBL.ObtenerPorID(parametroOrganizacionID);
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
        public ParametroOrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                ParametroOrganizacionInfo result = parametroOrganizacionBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                ResultadoInfo<ParametroOrganizacionInfo> result = parametroOrganizacionBL.ObtenerPorFiltroPagina(pagina, filtro);
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
        public ParametroOrganizacionInfo ObtenerPorParametroOrganizacionID(ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                ParametroOrganizacionInfo result = parametroOrganizacionBL.ObtenerPorParametroOrganizacionID(filtro);
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
        public ParametroOrganizacionInfo ObtenerPorOrganizacionIDClaveParametro(int organizacionID, string claveParametro)
        {
            try
            {
                Logger.Info();
                var parametroOrganizacionBL = new ParametroOrganizacionBL();
                ParametroOrganizacionInfo result = parametroOrganizacionBL.ObtenerPorOrganizacionIDClaveParametro(organizacionID, claveParametro);
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

