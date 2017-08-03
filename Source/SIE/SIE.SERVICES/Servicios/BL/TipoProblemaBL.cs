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
    public class TipoProblemaBL : IDisposable
    {
        TipoProblemaDAL tipoProblemaDAL;

        public TipoProblemaBL()
        {
            tipoProblemaDAL = new TipoProblemaDAL();
        }

        public void Dispose()
        {
            tipoProblemaDAL.Disposed += (s, e) =>
            {
                tipoProblemaDAL = null;
            };
            tipoProblemaDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de TipoProblema
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoProblemaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProblemaInfo filtro)
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de TipoProblema
        /// </summary>
        /// <returns></returns>
        public IList<TipoProblemaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de TipoProblema filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<TipoProblemaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de TipoProblema por su Id
        /// </summary>
        /// <param name="tipoProblemaId">Obtiene una entidad TipoProblema por su Id</param>
        /// <returns></returns>
        public TipoProblemaInfo ObtenerPorID(int tipoProblemaId)
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.ObtenerTodos().Where(e=> e.TipoProblemaId == tipoProblemaId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoProblema por su descripcion
        /// </summary>
        /// <param name="tipoProblemaId">Obtiene una entidad TipoProblema por su descripcion</param>
        /// <returns></returns>
        public TipoProblemaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad TipoProblema
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoProblemaInfo info)
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.Guardar(info);
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

        public List<TipoProblemaInfo> ObtenerPorEstatus(EstatusEnum estatusEnum)
        {
            try
            {
                Logger.Info();
                return tipoProblemaDAL.ObtenerTodos().Where(e => e.Activo == estatusEnum).ToList();
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
