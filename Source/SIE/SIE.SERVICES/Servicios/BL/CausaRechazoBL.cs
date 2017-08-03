using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class CausaRechazoBL : IDisposable
    {
        CausaRechazoDAL causaRechazoDAL;

        public CausaRechazoBL()
        {
            causaRechazoDAL = new CausaRechazoDAL();
        }

        public void Dispose()
        {
            causaRechazoDAL.Disposed += (s, e) =>
            {
                causaRechazoDAL = null;
            };
            causaRechazoDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CausaRechazo
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CausaRechazoInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaRechazoInfo filtro)
        {
            try
            {
                Logger.Info();
                return causaRechazoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CausaRechazo
        /// </summary>
        /// <returns></returns>
        public IList<CausaRechazoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return causaRechazoDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CausaRechazo filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CausaRechazoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return causaRechazoDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CausaRechazo por su Id
        /// </summary>
        /// <param name="causaRechazoId">Obtiene una entidad CausaRechazo por su Id</param>
        /// <returns></returns>
        public CausaRechazoInfo ObtenerPorID(int causaRechazoId)
        {
            try
            {
                Logger.Info();
                return causaRechazoDAL.ObtenerTodos().Where(e=> e.CausaRechazoID == causaRechazoId).FirstOrDefault();
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
        /// Obtiene una entidad de CausaRechazo por su descripcion
        /// </summary>
        /// <param name="causaRechazoId">Obtiene una entidad CausaRechazo por su descripcion</param>
        /// <returns></returns>
        public CausaRechazoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return causaRechazoDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CausaRechazo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CausaRechazoInfo info)
        {
            try
            {
                Logger.Info();
                return causaRechazoDAL.Guardar(info);
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
