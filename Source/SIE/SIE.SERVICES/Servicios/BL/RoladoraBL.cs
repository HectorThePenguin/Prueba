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
    public class RoladoraBL : IDisposable
    {
        RoladoraDAL roladoraDAL;

        public RoladoraBL()
        {
            roladoraDAL = new RoladoraDAL();
        }

        public void Dispose()
        {
            roladoraDAL.Disposed += (s, e) =>
            {
                roladoraDAL = null;
            };
            roladoraDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de Roladora
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<RoladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, RoladoraInfo filtro)
        {
            try
            {
                Logger.Info();
                return roladoraDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de Roladora
        /// </summary>
        /// <returns></returns>
        public IList<RoladoraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return roladoraDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de Roladora filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<RoladoraInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return roladoraDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de Roladora por su Id
        /// </summary>
        /// <param name="roladoraId">Obtiene una entidad Roladora por su Id</param>
        /// <returns></returns>
        public RoladoraInfo ObtenerPorID(int roladoraId)
        {
            try
            {
                Logger.Info();
                return roladoraDAL.ObtenerTodos().Where(e=> e.RoladoraID == roladoraId).FirstOrDefault();
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
        /// Obtiene una entidad de Roladora por su descripcion
        /// </summary>
        /// <param name="roladoraId">Obtiene una entidad Roladora por su descripcion</param>
        /// <returns></returns>
        public RoladoraInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return roladoraDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad Roladora
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(RoladoraInfo info)
        {
            try
            {
                Logger.Info();
                return roladoraDAL.Guardar(info);
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
