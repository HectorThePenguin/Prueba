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
    public class TipoObservacionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoObservacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoObservacionInfo info)
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                int result = tipoObservacionBL.Guardar(info);
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
        public ResultadoInfo<TipoObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoObservacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                ResultadoInfo<TipoObservacionInfo> result = tipoObservacionBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                IList<TipoObservacionInfo> result = tipoObservacionBL.ObtenerTodos();
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
        public IList<TipoObservacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                IList<TipoObservacionInfo> result = tipoObservacionBL.ObtenerTodos(estatus);
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
        /// <param name="tipoObservacionID"></param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorID(int tipoObservacionID)
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                TipoObservacionInfo result = tipoObservacionBL.ObtenerPorID(tipoObservacionID);
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
        /// <param name="tipoObservacion"></param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorTipoObservacion(TipoObservacionInfo tipoObservacion)
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                TipoObservacionInfo result = tipoObservacionBL.ObtenerPorTipoObservacion(tipoObservacion);
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
        public TipoObservacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoObservacionBL = new TipoObservacionBL();
                TipoObservacionInfo result = tipoObservacionBL.ObtenerPorDescripcion(descripcion);
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

