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
    public class TipoObservacionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoObservacion
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(TipoObservacionInfo info)
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                int result = info.TipoObservacionID;
                if (info.TipoObservacionID == 0)
                {
                    result = tipoObservacionDAL.Crear(info);
                }
                else
                {
                    tipoObservacionDAL.Actualizar(info);
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
        public ResultadoInfo<TipoObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoObservacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                ResultadoInfo<TipoObservacionInfo> result = tipoObservacionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoObservacion
        /// </summary>
        /// <returns></returns>
        public IList<TipoObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                IList<TipoObservacionInfo> result = tipoObservacionDAL.ObtenerTodos();
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
        public IList<TipoObservacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                IList<TipoObservacionInfo> result = tipoObservacionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoObservacion por su Id
        /// </summary>
        /// <param name="tipoObservacionID">Obtiene una entidad TipoObservacion por su Id</param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorID(int tipoObservacionID)
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                TipoObservacionInfo result = tipoObservacionDAL.ObtenerPorID(tipoObservacionID);
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
        /// Obtiene una entidad TipoObservacion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                TipoObservacionInfo result = tipoObservacionDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad de Observacion
        /// </summary>
        /// <param name="tipoObservacion">Obtiene una entidad Observacion</param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorTipoObservacion(TipoObservacionInfo tipoObservacion)
        {
            try
            {
                Logger.Info();
                var tipoObservacionDAL = new TipoObservacionDAL();
                TipoObservacionInfo result = tipoObservacionDAL.ObtenerPorID(tipoObservacion.TipoObservacionID);
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

