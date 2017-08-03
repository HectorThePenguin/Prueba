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
    public class TipoProcesoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoProceso
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoProcesoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                int result = tipoProcesoBL.Guardar(info);
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
        public ResultadoInfo<TipoProcesoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProcesoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                ResultadoInfo<TipoProcesoInfo> result = tipoProcesoBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoProcesoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                IList<TipoProcesoInfo> result = tipoProcesoBL.ObtenerTodos();
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
        public IList<TipoProcesoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                IList<TipoProcesoInfo> result = tipoProcesoBL.ObtenerTodos(estatus);
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
        /// <param name="tipoProcesoID"></param>
        /// <returns></returns>
        public TipoProcesoInfo ObtenerPorID(int tipoProcesoID)
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                TipoProcesoInfo result = tipoProcesoBL.ObtenerPorID(tipoProcesoID);
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
        public TipoProcesoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                TipoProcesoInfo result = tipoProcesoBL.ObtenerPorDescripcion(descripcion);
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

        public int ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                Logger.Info();
                var tipoProcesoBL = new TipoProcesoBL();
                int result = tipoProcesoBL.ObtenerPorOrganizacion(organizacionId);
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

