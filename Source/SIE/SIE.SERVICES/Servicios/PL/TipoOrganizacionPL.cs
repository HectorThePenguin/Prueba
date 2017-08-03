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
    public class TipoOrganizacionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoOrganizacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionBL = new TipoOrganizacionBL();
                int result = tipoOrganizacionBL.Guardar(info);
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
        public ResultadoInfo<TipoOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionBL = new TipoOrganizacionBL();
                ResultadoInfo<TipoOrganizacionInfo> result = tipoOrganizacionBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoOrganizacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionBL = new TipoOrganizacionBL();
                IList<TipoOrganizacionInfo> result = tipoOrganizacionBL.ObtenerTodos();
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
        public IList<TipoOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionBL = new TipoOrganizacionBL();
                IList<TipoOrganizacionInfo> result = tipoOrganizacionBL.ObtenerTodos(estatus);
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
        /// <param name="tipoOrganizacionID"></param>
        /// <returns></returns>
        public TipoOrganizacionInfo ObtenerPorID(int tipoOrganizacionID)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionBL = new TipoOrganizacionBL();
                TipoOrganizacionInfo result = tipoOrganizacionBL.ObtenerPorID(tipoOrganizacionID);
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
        public TipoOrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionBL = new TipoOrganizacionBL();
                TipoOrganizacionInfo result = tipoOrganizacionBL.ObtenerPorDescripcion(descripcion);
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

