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
    internal class EstadoComederoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad EstadoComedero
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(EstadoComederoInfo info)
        {
            try
            {
                Logger.Info();
                var estadoComederoDAL = new EstadoComederoDAL();
                int result = info.EstadoComederoID;
                if (info.EstadoComederoID == 0)
                {
                    result = estadoComederoDAL.Crear(info);
                }
                else
                {
                    estadoComederoDAL.Actualizar(info);
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
        internal ResultadoInfo<EstadoComederoInfo> ObtenerPorPagina(PaginacionInfo pagina, EstadoComederoInfo filtro)
        {
            try
            {
                Logger.Info();
                var estadoComederoDAL = new EstadoComederoDAL();
                ResultadoInfo<EstadoComederoInfo> result = estadoComederoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de EstadoComedero
        /// </summary>
        /// <returns></returns>
        internal IList<EstadoComederoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var estadoComederoDAL = new EstadoComederoDAL();
                IList<EstadoComederoInfo> result = estadoComederoDAL.ObtenerTodos();
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
        internal IList<EstadoComederoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var estadoComederoDAL = new EstadoComederoDAL();
                IList<EstadoComederoInfo> result = estadoComederoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad EstadoComedero por su Id
        /// </summary>
        /// <param name="estadoComederoID">Obtiene una entidad EstadoComedero por su Id</param>
        /// <returns></returns>
        internal EstadoComederoInfo ObtenerPorID(int estadoComederoID)
        {
            try
            {
                Logger.Info();
                var estadoComederoDAL = new EstadoComederoDAL();
                EstadoComederoInfo result = estadoComederoDAL.ObtenerPorID(estadoComederoID);
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
        /// Obtiene una entidad EstadoComedero por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal EstadoComederoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var estadoComederoDAL = new EstadoComederoDAL();
                EstadoComederoInfo result = estadoComederoDAL.ObtenerPorDescripcion(descripcion);
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

