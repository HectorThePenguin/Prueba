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
    public class TipoObjetivoCalidadBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoObjetivoCalidad
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(TipoObjetivoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                var tipoObjetivoCalidadDAL = new TipoObjetivoCalidadDAL();
                int result = info.TipoObjetivoCalidadID;
                if (info.TipoObjetivoCalidadID == 0)
                {
                    result = tipoObjetivoCalidadDAL.Crear(info);
                }
                else
                {
                    tipoObjetivoCalidadDAL.Actualizar(info);
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
        public ResultadoInfo<TipoObjetivoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoObjetivoCalidadInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoObjetivoCalidadDAL = new TipoObjetivoCalidadDAL();
                ResultadoInfo<TipoObjetivoCalidadInfo> result = tipoObjetivoCalidadDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoObjetivoCalidad
        /// </summary>
        /// <returns></returns>
        public IList<TipoObjetivoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoObjetivoCalidadDAL = new TipoObjetivoCalidadDAL();
                IList<TipoObjetivoCalidadInfo> result = tipoObjetivoCalidadDAL.ObtenerTodos();
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
        public IList<TipoObjetivoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoObjetivoCalidadDAL = new TipoObjetivoCalidadDAL();
                IList<TipoObjetivoCalidadInfo> result = tipoObjetivoCalidadDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoObjetivoCalidad por su Id
        /// </summary>
        /// <param name="tipoObjetivoCalidadID">Obtiene una entidad TipoObjetivoCalidad por su Id</param>
        /// <returns></returns>
        public TipoObjetivoCalidadInfo ObtenerPorID(int tipoObjetivoCalidadID)
        {
            try
            {
                Logger.Info();
                var tipoObjetivoCalidadDAL = new TipoObjetivoCalidadDAL();
                TipoObjetivoCalidadInfo result = tipoObjetivoCalidadDAL.ObtenerPorID(tipoObjetivoCalidadID);
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
        /// Obtiene una entidad TipoObjetivoCalidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoObjetivoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoObjetivoCalidadDAL = new TipoObjetivoCalidadDAL();
                TipoObjetivoCalidadInfo result = tipoObjetivoCalidadDAL.ObtenerPorDescripcion(descripcion);
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

