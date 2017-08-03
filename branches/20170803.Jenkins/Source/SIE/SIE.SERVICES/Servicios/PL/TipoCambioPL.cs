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
    public class TipoCambioPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoCambio
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoCambioInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                int result = tipoCambioBL.Guardar(info);
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
        public ResultadoInfo<TipoCambioInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCambioInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                ResultadoInfo<TipoCambioInfo> result = tipoCambioBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoCambioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                IList<TipoCambioInfo> result = tipoCambioBL.ObtenerTodos();
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
        public IList<TipoCambioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                IList<TipoCambioInfo> result = tipoCambioBL.ObtenerTodos(estatus);
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
        /// <param name="tipoCambioID"></param>
        /// <returns></returns>
        public TipoCambioInfo ObtenerPorID(int tipoCambioID)
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                TipoCambioInfo result = tipoCambioBL.ObtenerPorID(tipoCambioID);
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
        public TipoCambioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                TipoCambioInfo result = tipoCambioBL.ObtenerPorDescripcion(descripcion);
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

        public List<TipoCambioInfo> ObtenerPorEstado(EstatusEnum estatus)
        {
            List<TipoCambioInfo> result;
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                result = tipoCambioBL.ObtenerPorEstado(estatus);
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

        /// <summary>
        /// Obtiene un listado de tipo cambio por fecha
        /// </summary>
        /// <returns></returns>
        public List<TipoCambioInfo> ObtenerPorFechaActual()
        {
            List<TipoCambioInfo> result;
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                result = tipoCambioBL.ObtenerPorFechaActual();
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

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        ///  <param name="fecha"></param>
        /// <returns></returns>
        public TipoCambioInfo ObtenerPorDescripcionFecha(string descripcion, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var tipoCambioBL = new TipoCambioBL();
                TipoCambioInfo result = tipoCambioBL.ObtenerPorDescripcionFecha(descripcion, fecha);
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
