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
    internal class ObservacionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Observacion
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ObservacionInfo info)
        {
            try
            {
                Logger.Info();
                var observacionDAL = new ObservacionDAL();
                int result = info.ObservacionID;
                if (info.ObservacionID == 0)
                {
                    result = observacionDAL.Crear(info);
                }
                else
                {
                    observacionDAL.Actualizar(info);
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
        internal ResultadoInfo<ObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ObservacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var observacionDAL = new ObservacionDAL();
                ResultadoInfo<ObservacionInfo> result = observacionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Observacion
        /// </summary>
        /// <returns></returns>
        internal IList<ObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var observacionDAL = new ObservacionDAL();
                IList<ObservacionInfo> result = observacionDAL.ObtenerTodos();
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
        internal IList<ObservacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var observacionDAL = new ObservacionDAL();
                IList<ObservacionInfo> result = observacionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Observacion por su Id
        /// </summary>
        /// <param name="observacionID">Obtiene una entidad Observacion por su Id</param>
        /// <returns></returns>
        internal ObservacionInfo ObtenerPorID(int observacionID)
        {
            try
            {
                Logger.Info();
                var observacionDAL = new ObservacionDAL();
                ObservacionInfo result = observacionDAL.ObtenerPorID(observacionID);
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
        /// Obtiene una entidad Observacion por su descripcion
        /// </summary>
        /// <param name="observacionID">Obtiene una entidad Observacion por su Id</param>
        /// <returns></returns>
        internal ObservacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var observacionDAL = new ObservacionDAL();
                ObservacionInfo result = observacionDAL.ObtenerPorDescripcion(descripcion);
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

