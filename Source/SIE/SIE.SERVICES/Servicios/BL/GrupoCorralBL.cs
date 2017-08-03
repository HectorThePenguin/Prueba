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
    public class GrupoCorralBL : IDisposable
    {
        GrupoCorralDAL grupoCorralDAL;

        public GrupoCorralBL()
        {
            grupoCorralDAL = new GrupoCorralDAL();
        }

        public void Dispose()
        {
            grupoCorralDAL.Disposed += (s, e) =>
            {
                grupoCorralDAL = null;
            };
            grupoCorralDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de GrupoCorral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<GrupoCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoCorralInfo filtro)
        {
            try
            {
                Logger.Info();
                return grupoCorralDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de GrupoCorral
        /// </summary>
        /// <returns></returns>
        public IList<GrupoCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return grupoCorralDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de GrupoCorral filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<GrupoCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return grupoCorralDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de GrupoCorral por su Id
        /// </summary>
        /// <param name="grupoCorralId">Obtiene una entidad GrupoCorral por su Id</param>
        /// <returns></returns>
        public GrupoCorralInfo ObtenerPorID(int grupoCorralId)
        {
            try
            {
                Logger.Info();
                return grupoCorralDAL.ObtenerTodos().Where(e=> e.GrupoCorralID == grupoCorralId).FirstOrDefault();
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
        /// Obtiene una entidad de GrupoCorral por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad GrupoCorral por su descripcion</param>
        /// <returns></returns>
        public GrupoCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return grupoCorralDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad GrupoCorral
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(GrupoCorralInfo info)
        {
            try
            {
                Logger.Info();
                return grupoCorralDAL.Guardar(info);
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
