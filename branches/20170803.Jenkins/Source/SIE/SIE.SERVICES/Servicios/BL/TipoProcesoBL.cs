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
    internal class TipoProcesoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoProceso
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoProcesoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();
                int result = info.TipoProcesoID;
                if (info.TipoProcesoID == 0)
                {
                    result = tipoProcesoDAL.Crear(info);
                }
                else
                {
                    tipoProcesoDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoProcesoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProcesoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();
                ResultadoInfo<TipoProcesoInfo> result = tipoProcesoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoProceso
        /// </summary>
        /// <returns></returns>
        internal IList<TipoProcesoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();
                IList<TipoProcesoInfo> result = tipoProcesoDAL.ObtenerTodos();
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
        internal IList<TipoProcesoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();
                IList<TipoProcesoInfo> result = tipoProcesoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoProceso por su Id
        /// </summary>
        /// <param name="tipoProcesoID">Obtiene una entidad TipoProceso por su Id</param>
        /// <returns></returns>
        internal TipoProcesoInfo ObtenerPorID(int tipoProcesoID)
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();
                TipoProcesoInfo result = tipoProcesoDAL.ObtenerPorID(tipoProcesoID);
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
        /// Obtiene una entidad TipoProceso por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoProcesoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();
                TipoProcesoInfo result = tipoProcesoDAL.ObtenerPorDescripcion(descripcion);
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

        internal int ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                Logger.Info();
                var tipoProcesoDAL = new TipoProcesoDAL();

                var result = tipoProcesoDAL.ObtenerPorOrganizacion(organizacionId);
                
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

