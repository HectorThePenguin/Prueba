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
    public class ObservacionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Observacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ObservacionInfo info)
        {
            try
            {
                Logger.Info();
                var observacionBL = new ObservacionBL();
                int result = observacionBL.Guardar(info);
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
        public ResultadoInfo<ObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ObservacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var observacionBL = new ObservacionBL();
                ResultadoInfo<ObservacionInfo> result = observacionBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<ObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var observacionBL = new ObservacionBL();
                IList<ObservacionInfo> result = observacionBL.ObtenerTodos();
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
        public IList<ObservacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var observacionBL = new ObservacionBL();
                IList<ObservacionInfo> result = observacionBL.ObtenerTodos(estatus);
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
        /// <param name="observacionID"></param>
        /// <returns></returns>
        public ObservacionInfo ObtenerPorID(int observacionID)
        {
            try
            {
                Logger.Info();
                var observacionBL = new ObservacionBL();
                ObservacionInfo result = observacionBL.ObtenerPorID(observacionID);
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
        public ObservacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var observacionBL = new ObservacionBL();
                ObservacionInfo result = observacionBL.ObtenerPorDescripcion(descripcion);
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

