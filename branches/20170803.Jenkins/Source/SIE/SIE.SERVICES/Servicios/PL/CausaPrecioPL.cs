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
    public class CausaPrecioPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CausaPrecio
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(CausaPrecioInfo info)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                int result = causaPrecioBL.Guardar(info);
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
        public ResultadoInfo<CausaPrecioInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaPrecioInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                ResultadoInfo<CausaPrecioInfo> result = causaPrecioBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<CausaPrecioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                IList<CausaPrecioInfo> result = causaPrecioBL.ObtenerTodos();
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
        public IList<CausaPrecioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                IList<CausaPrecioInfo> result = causaPrecioBL.ObtenerTodos(estatus);
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
        /// <param name="causaPrecioID"></param>
        /// <returns></returns>
        public CausaPrecioInfo ObtenerPorID(int causaPrecioID)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                CausaPrecioInfo result = causaPrecioBL.ObtenerPorID(causaPrecioID);
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
        public CausaPrecioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                CausaPrecioInfo result = causaPrecioBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<CausaPrecioInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, CausaPrecioInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                ResultadoInfo<CausaPrecioInfo> result = causaPrecioBL.ObtenerPorFiltroPagina(pagina, filtro);
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
        /// <param name="filtro"></param>
        /// <returns></returns>
        public CausaPrecioInfo ObtenerPorCausaSalidaPrecioGanado(CausaPrecioInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                CausaPrecioInfo result = causaPrecioBL.ObtenerPorCausaSalidaPrecioGanado(filtro);
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

        public List<CausaPrecioInfo> ObtenerPorPrecioPorCausaSalida(int causa, int organizacionID)
        {
            try
            {
                Logger.Info();
                var causaPrecioBL = new CausaPrecioBL();
                List<CausaPrecioInfo> result = causaPrecioBL.ObtenerPorPrecioPorCausaSalida(causa, organizacionID);
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

