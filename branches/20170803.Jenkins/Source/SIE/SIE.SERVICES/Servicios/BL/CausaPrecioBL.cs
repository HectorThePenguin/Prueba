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
    internal class CausaPrecioBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CausaPrecio
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(CausaPrecioInfo info)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                int result = info.CausaPrecioID;
                if (info.CausaPrecioID == 0)
                {
                    result = causaPrecioDAL.Crear(info);
                }
                else
                {
                    causaPrecioDAL.Actualizar(info);
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
        internal ResultadoInfo<CausaPrecioInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaPrecioInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                ResultadoInfo<CausaPrecioInfo> result = causaPrecioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CausaPrecio
        /// </summary>
        /// <returns></returns>
        internal IList<CausaPrecioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                IList<CausaPrecioInfo> result = causaPrecioDAL.ObtenerTodos();
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
        internal IList<CausaPrecioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                IList<CausaPrecioInfo> result = causaPrecioDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad CausaPrecio por su Id
        /// </summary>
        /// <param name="causaPrecioID">Obtiene una entidad CausaPrecio por su Id</param>
        /// <returns></returns>
        internal CausaPrecioInfo ObtenerPorID(int causaPrecioID)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                CausaPrecioInfo result = causaPrecioDAL.ObtenerPorID(causaPrecioID);
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
        /// Obtiene una entidad CausaPrecio por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal CausaPrecioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                CausaPrecioInfo result = causaPrecioDAL.ObtenerPorDescripcion(descripcion);
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
        internal ResultadoInfo<CausaPrecioInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, CausaPrecioInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                ResultadoInfo<CausaPrecioInfo> result = causaPrecioDAL.ObtenerPorFiltroPagina(pagina, filtro);
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
        /// Obtiene una entidad por los Filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CausaPrecioInfo ObtenerPorCausaSalidaPrecioGanado(CausaPrecioInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                CausaPrecioInfo result = causaPrecioDAL.ObtenerPorCausaSalidaPrecioGanado(filtro);
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

        internal List<CausaPrecioInfo> ObtenerPorPrecioPorCausaSalida(int causa, int organizacionID)
        {
            try
            {
                Logger.Info();
                var causaPrecioDAL = new CausaPrecioDAL();
                List<CausaPrecioInfo> result = causaPrecioDAL.ObtenerPorPrecioPorCausaSalida(causa, organizacionID);
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

