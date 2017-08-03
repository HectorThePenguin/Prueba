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
    public class TipoServicioBL : IDisposable
    {
        TipoServicioDAL tipoServicioDAL;

        public TipoServicioBL()
        {
            tipoServicioDAL = new TipoServicioDAL();
        }

        public void Dispose()
        {
            tipoServicioDAL.Disposed += (s, e) =>
            {
                tipoServicioDAL = null;
            };
            tipoServicioDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de TipoServicio
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoServicioInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoServicioInfo filtro)
        {
            try
            {
                Logger.Info();
                return tipoServicioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de TipoServicio
        /// </summary>
        /// <returns></returns>
        public IList<TipoServicioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return tipoServicioDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de TipoServicio filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<TipoServicioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return tipoServicioDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de TipoServicio por su Id
        /// </summary>
        /// <param name="tipoServicioId">Obtiene una entidad TipoServicio por su Id</param>
        /// <returns></returns>
        public TipoServicioInfo ObtenerPorID(int tipoServicioId)
        {
            try
            {
                Logger.Info();
                return tipoServicioDAL.ObtenerTodos().Where(e=> e.TipoServicioId == tipoServicioId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoServicio por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad TipoServicio por su descripcion</param>
        /// <returns></returns>
        public TipoServicioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return tipoServicioDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad TipoServicio
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoServicioInfo info)
        {
            try
            {
                Logger.Info();
                return tipoServicioDAL.Guardar(info);
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
