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
    public class TipoPACBL : IDisposable
    {
        TipoPACDAL tipoPACDAL;

        public TipoPACBL()
        {
            tipoPACDAL = new TipoPACDAL();
        }

        public void Dispose()
        {
            tipoPACDAL.Disposed += (s, e) =>
            {
                tipoPACDAL = null;
            };
            tipoPACDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de TipoPAC
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoPACInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPACInfo filtro)
        {
            try
            {
                Logger.Info();
                return tipoPACDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de TipoPAC
        /// </summary>
        /// <returns></returns>
        public IList<TipoPACInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return tipoPACDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de TipoPAC filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<TipoPACInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return tipoPACDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de TipoPAC por su Id
        /// </summary>
        /// <param name="tipoPACId">Obtiene una entidad TipoPAC por su Id</param>
        /// <returns></returns>
        public TipoPACInfo ObtenerPorID(int tipoPACId)
        {
            try
            {
                Logger.Info();
                return tipoPACDAL.ObtenerTodos().Where(e=> e.TipoPACID == tipoPACId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoPAC por su descripcion
        /// </summary>
        /// <param name="tipoPACId">Obtiene una entidad TipoPAC por su descripcion</param>
        /// <returns></returns>
        public TipoPACInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return tipoPACDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad TipoPAC
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoPACInfo info)
        {
            try
            {
                Logger.Info();
                return tipoPACDAL.Guardar(info);
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
