using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class TipoCambioBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoCambio
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(TipoCambioInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                int result = info.TipoCambioId;
                if (info.TipoCambioId == 0)
                {
                    result = tipoCambioDAL.Crear(info);
                }
                else
                {
                    tipoCambioDAL.Actualizar(info);
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
        public ResultadoInfo<TipoCambioInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCambioInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                ResultadoInfo<TipoCambioInfo> result = tipoCambioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoCambio
        /// </summary>
        /// <returns></returns>
        public IList<TipoCambioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                IList<TipoCambioInfo> result = tipoCambioDAL.ObtenerTodos();
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
        public IList<TipoCambioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                IList<TipoCambioInfo> result = tipoCambioDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoCambio por su Id
        /// </summary>
        /// <param name="tipoCambioID">Obtiene una entidad TipoCambio por su Id</param>
        /// <returns></returns>
        public TipoCambioInfo ObtenerPorID(int tipoCambioID)
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                TipoCambioInfo result = tipoCambioDAL.ObtenerPorID(tipoCambioID);
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
        /// Obtiene una entidad TipoCambio por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TipoCambioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                TipoCambioInfo result = tipoCambioDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene tipos de cambio por estado
        /// </summary>
        /// <returns></returns>
        internal List<TipoCambioInfo> ObtenerPorEstado(EstatusEnum estatus)
        {
            List<TipoCambioInfo> result;
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                result = tipoCambioDAL.ObtenerPorEstado(estatus);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene tipo de cambio por id
        /// </summary>
        /// <param name="tipoCambioInfo"></param>
        /// <returns></returns>
        internal TipoCambioInfo ObtenerPorId(int tipoCambioInfo)
        {
            TipoCambioInfo result;
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                result = tipoCambioDAL.ObtenerPorId(tipoCambioInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un listado de tipo cambio por fecha actual
        /// </summary>
        /// <returns></returns>
        internal List<TipoCambioInfo> ObtenerPorFechaActual()
        {
            List<TipoCambioInfo> result;
            try
            {
                Logger.Info();
                var tipoCambioDal = new TipoCambioDAL();
                result = tipoCambioDal.ObtenerPorFechaActual();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una entidad TipoCambio por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public TipoCambioInfo ObtenerPorDescripcionFecha(string descripcion, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var tipoCambioDAL = new TipoCambioDAL();
                TipoCambioInfo result = tipoCambioDAL.ObtenerPorDescripcionFecha(descripcion, fecha);
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
