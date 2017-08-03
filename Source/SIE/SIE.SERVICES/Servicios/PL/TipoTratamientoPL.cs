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
    public class TipoTratamientoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoTratamiento
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoTratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoBL = new TipoTratamientoBL();
                int result = tipoTratamientoBL.Guardar(info);
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
        public ResultadoInfo<TipoTratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoBL = new TipoTratamientoBL();
                ResultadoInfo<TipoTratamientoInfo> result = tipoTratamientoBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoTratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoTratamientoBL = new TipoTratamientoBL();
                IList<TipoTratamientoInfo> result = tipoTratamientoBL.ObtenerTodos();
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
        public IList<TipoTratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoBL = new TipoTratamientoBL();
                IList<TipoTratamientoInfo> result = tipoTratamientoBL.ObtenerTodos(estatus);
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
        /// <param name="tipoTratamientoID"></param>
        /// <returns></returns>
        public TipoTratamientoInfo ObtenerPorID(int tipoTratamientoID)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoBL = new TipoTratamientoBL();
                TipoTratamientoInfo result = tipoTratamientoBL.ObtenerPorID(tipoTratamientoID);
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
        public TipoTratamientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoBL = new TipoTratamientoBL();
                TipoTratamientoInfo result = tipoTratamientoBL.ObtenerPorDescripcion(descripcion);
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

