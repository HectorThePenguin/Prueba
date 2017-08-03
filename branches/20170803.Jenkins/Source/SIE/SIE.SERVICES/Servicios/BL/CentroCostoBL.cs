using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class CentroCostoBL : IDisposable
    {
        CentroCostoDAL centroCostoDAL;

        public CentroCostoBL()
        {
            centroCostoDAL = new CentroCostoDAL();
        }

        public void Dispose()
        {
            centroCostoDAL.Disposed += (s, e) =>
            {
                centroCostoDAL = null;
            };
            centroCostoDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CentroCosto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CentroCostoInfo> ObtenerPorPagina(PaginacionInfo pagina, CentroCostoInfo filtro)
        {
            try
            {
                Logger.Info();
                return centroCostoDAL.ObtenerPorPagina(pagina, filtro);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CentroCosto
        /// </summary>
        /// <returns></returns>
        public IList<CentroCostoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return centroCostoDAL.ObtenerTodos().ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CentroCosto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CentroCostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return centroCostoDAL.ObtenerTodos(estatus).ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de CentroCosto por su Id
        /// </summary>
        /// <param name="centroCostoId">Obtiene una entidad CentroCosto por su Id</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorID(int centroCostoId)
        {
            try
            {
                Logger.Info();
                return centroCostoDAL.ObtenerPorID(centroCostoId);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de CentroCosto por su Id
        /// </summary>
        /// <param name="centroCosto">Obtiene una entidad CentroCosto por su Id</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorCentroCostoSAP(CentroCostoInfo centroCosto)
        {
            try
            {
                Logger.Info();

                if (string.IsNullOrWhiteSpace(centroCosto.CentroCostoSAP))
                {
                    centroCosto.CentroCostoSAP = centroCosto.Descripcion;
                }

                return centroCostoDAL.ObtenerPorCentroCostoSAP(centroCosto);
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
        /// Obtiene una entidad de CentroCosto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CentroCosto por su descripcion</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return centroCostoDAL.ObtenerPorDescripcion(descripcion);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CentroCosto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CentroCostoInfo info)
        {
            try
            {
                Logger.Info();
                return centroCostoDAL.Guardar(info);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
