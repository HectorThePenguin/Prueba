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
    public class TipoPreguntaBL : IDisposable
    {
        TipoPreguntaDAL tipoPreguntaDAL;

        public TipoPreguntaBL()
        {
            tipoPreguntaDAL = new TipoPreguntaDAL();
        }

        public void Dispose()
        {
            tipoPreguntaDAL.Disposed += (s, e) =>
            {
                tipoPreguntaDAL = null;
            };
            tipoPreguntaDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de TipoPregunta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoPreguntaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPreguntaInfo filtro)
        {
            try
            {
                Logger.Info();
                return tipoPreguntaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de TipoPregunta
        /// </summary>
        /// <returns></returns>
        public IList<TipoPreguntaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return tipoPreguntaDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de TipoPregunta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<TipoPreguntaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return tipoPreguntaDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de TipoPregunta por su Id
        /// </summary>
        /// <param name="tipoPreguntaId">Obtiene una entidad TipoPregunta por su Id</param>
        /// <returns></returns>
        public TipoPreguntaInfo ObtenerPorID(int tipoPreguntaId)
        {
            try
            {
                Logger.Info();
                return tipoPreguntaDAL.ObtenerTodos().Where(e=> e.TipoPreguntaID == tipoPreguntaId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoPregunta por su descripcion
        /// </summary>
        /// <param name="tipoPreguntaId">Obtiene una entidad TipoPregunta por su descripcion</param>
        /// <returns></returns>
        public TipoPreguntaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return tipoPreguntaDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad TipoPregunta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoPreguntaInfo info)
        {
            try
            {
                Logger.Info();
                return tipoPreguntaDAL.Guardar(info);
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
