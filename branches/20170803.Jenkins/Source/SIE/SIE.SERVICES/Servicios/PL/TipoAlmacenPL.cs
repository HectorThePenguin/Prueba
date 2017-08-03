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
    public class TipoAlmacenPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoAlmacen
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TipoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                int result = tipoAlmacenBL.Guardar(info);
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
        public ResultadoInfo<TipoAlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoAlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                ResultadoInfo<TipoAlmacenInfo> result = tipoAlmacenBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<TipoAlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                IList<TipoAlmacenInfo> result = tipoAlmacenBL.ObtenerTodos();
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
        public IList<TipoAlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                IList<TipoAlmacenInfo> result = tipoAlmacenBL.ObtenerTodos(estatus);
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
        /// <param name="tipoAlmacenID"></param>
        /// <returns></returns>
        public TipoAlmacenInfo ObtenerPorID(int tipoAlmacenID)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                TipoAlmacenInfo result = tipoAlmacenBL.ObtenerPorID(tipoAlmacenID);
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
        /// <param name="tipoAlmacen"></param>
        /// <returns></returns>
        public TipoAlmacenInfo ObtenerPorID(TipoAlmacenInfo tipoAlmacen)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                TipoAlmacenInfo result = tipoAlmacenBL.ObtenerPorID(tipoAlmacen.TipoAlmacenID);
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
        public TipoAlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                TipoAlmacenInfo result = tipoAlmacenBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<TipoAlmacenInfo> ObtenerPorPaginaTiposAlmacen(PaginacionInfo pagina, TipoAlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenBL = new TipoAlmacenBL();
                ResultadoInfo<TipoAlmacenInfo> result = tipoAlmacenBL.ObtenerPorPaginaTiposAlmacen(pagina, filtro);
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

