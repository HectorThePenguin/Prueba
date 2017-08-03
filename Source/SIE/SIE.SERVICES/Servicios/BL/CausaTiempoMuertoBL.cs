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
    public class CausaTiempoMuertoBL : IDisposable
    {
        CausaTiempoMuertoDAL causaTiempoMuertoDAL;

        public CausaTiempoMuertoBL()
        {
            causaTiempoMuertoDAL = new CausaTiempoMuertoDAL();
        }

        public void Dispose()
        {
            causaTiempoMuertoDAL.Disposed += (s, e) =>
            {
                causaTiempoMuertoDAL = null;
            };
            causaTiempoMuertoDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CausaTiempoMuerto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CausaTiempoMuertoInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaTiempoMuertoInfo filtro)
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CausaTiempoMuerto
        /// </summary>
        /// <returns></returns>
        public IList<CausaTiempoMuertoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CausaTiempoMuerto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CausaTiempoMuertoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.ObtenerTodos().Where(e=> e.Activo == estatus).OrderBy(cau => cau.Descripcion).ToList();
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
        /// Obtiene una entidad de CausaTiempoMuerto por su Id
        /// </summary>
        /// <param name="causaTiempoMuertoId">Obtiene una entidad CausaTiempoMuerto por su Id</param>
        /// <returns></returns>
        public CausaTiempoMuertoInfo ObtenerPorID(int causaTiempoMuertoId)
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.ObtenerTodos().Where(e=> e.CausaTiempoMuertoID == causaTiempoMuertoId).FirstOrDefault();
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
        /// Obtiene una entidad de CausaTiempoMuerto por su descripcion
        /// </summary>
        /// <param name="causaTiempoMuertoId">Obtiene una entidad CausaTiempoMuerto por su descripcion</param>
        /// <returns></returns>
        public CausaTiempoMuertoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CausaTiempoMuerto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CausaTiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.Guardar(info);
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
        /// Obtiene una lista de CausaTiempoMuerto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="tipoCausa"></param>
        /// <returns></returns>
        public IList<CausaTiempoMuertoInfo> ObtenerPorTipoCausa(int tipoCausa)
        {
            try
            {
                Logger.Info();
                return causaTiempoMuertoDAL.ObtenerTodos().Where(e => e.Activo == EstatusEnum.Activo && e.TipoCausa == tipoCausa).OrderBy(cau => cau.Descripcion).ToList();
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
