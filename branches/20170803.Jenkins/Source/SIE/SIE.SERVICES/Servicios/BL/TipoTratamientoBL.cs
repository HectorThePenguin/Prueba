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
    internal class TipoTratamientoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoTratamiento
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoTratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoDAL = new TipoTratamientoDAL();
                int result = info.TipoTratamientoID;
                if (info.TipoTratamientoID == 0)
                {
                    result = tipoTratamientoDAL.Crear(info);
                }
                else
                {
                    tipoTratamientoDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoTratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoDAL = new TipoTratamientoDAL();
                ResultadoInfo<TipoTratamientoInfo> result = tipoTratamientoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoTratamiento
        /// </summary>
        /// <returns></returns>
        internal IList<TipoTratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoTratamientoDAL = new TipoTratamientoDAL();
                IList<TipoTratamientoInfo> result = tipoTratamientoDAL.ObtenerTodos();
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
        internal IList<TipoTratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoDAL = new TipoTratamientoDAL();
                IList<TipoTratamientoInfo> result = tipoTratamientoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoTratamiento por su Id
        /// </summary>
        /// <param name="tipoTratamientoID">Obtiene una entidad TipoTratamiento por su Id</param>
        /// <returns></returns>
        internal TipoTratamientoInfo ObtenerPorID(int tipoTratamientoID)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoDAL = new TipoTratamientoDAL();
                TipoTratamientoInfo result = tipoTratamientoDAL.ObtenerPorID(tipoTratamientoID);
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
        /// Obtiene una entidad TipoTratamiento por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad TipoTratamiento por su Id</param>
        /// <returns></returns>
        internal TipoTratamientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoTratamientoDAL = new TipoTratamientoDAL();
                TipoTratamientoInfo result = tipoTratamientoDAL.ObtenerPorDescripcion(descripcion);
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

