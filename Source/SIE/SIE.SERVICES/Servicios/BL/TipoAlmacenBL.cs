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
    internal class TipoAlmacenBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoAlmacen
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                int result = info.TipoAlmacenID;
                if (info.TipoAlmacenID == 0)
                {
                    result = tipoAlmacenDAL.Crear(info);
                }
                else
                {
                    tipoAlmacenDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoAlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoAlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                ResultadoInfo<TipoAlmacenInfo> result = tipoAlmacenDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoAlmacen
        /// </summary>
        /// <returns></returns>
        internal IList<TipoAlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                IList<TipoAlmacenInfo> result = tipoAlmacenDAL.ObtenerTodos();
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
        internal IList<TipoAlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                IList<TipoAlmacenInfo> result = tipoAlmacenDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoAlmacen por su Id
        /// </summary>
        /// <param name="tipoAlmacenID">Obtiene una entidad TipoAlmacen por su Id</param>
        /// <returns></returns>
        internal TipoAlmacenInfo ObtenerPorID(int tipoAlmacenID)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                TipoAlmacenInfo result = tipoAlmacenDAL.ObtenerPorID(tipoAlmacenID);
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
        /// Obtiene una entidad TipoAlmacen por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoAlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                TipoAlmacenInfo result = tipoAlmacenDAL.ObtenerPorDescripcion(descripcion);
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
        internal ResultadoInfo<TipoAlmacenInfo> ObtenerPorPaginaTiposAlmacen(PaginacionInfo pagina, TipoAlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoAlmacenDAL = new TipoAlmacenDAL();
                ResultadoInfo<TipoAlmacenInfo> result = tipoAlmacenDAL.ObtenerPorPaginaTiposAlmacen(pagina, filtro);
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

