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
    public class ProblemaTratamientoBL : IDisposable
    {
        ProblemaTratamientoDAL problemaTratamientoDAL;

        public ProblemaTratamientoBL()
        {
            problemaTratamientoDAL = new ProblemaTratamientoDAL();
        }

        public void Dispose()
        {
            problemaTratamientoDAL.Disposed += (s, e) =>
            {
                problemaTratamientoDAL = null;
            };
            problemaTratamientoDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de ProblemaTratamiento
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProblemaTratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, ProblemaTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                return problemaTratamientoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de ProblemaTratamiento
        /// </summary>
        /// <returns></returns>
        public IList<ProblemaTratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return problemaTratamientoDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de ProblemaTratamiento filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<ProblemaTratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return problemaTratamientoDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de ProblemaTratamiento por su Id
        /// </summary>
        /// <param name="problemaTratamientoId">Obtiene una entidad ProblemaTratamiento por su Id</param>
        /// <returns></returns>
        public ProblemaTratamientoInfo ObtenerPorID(int problemaTratamientoId)
        {
            try
            {
                Logger.Info();
                return problemaTratamientoDAL.ObtenerTodos().Where(e=> e.ProblemaTratamientoID == problemaTratamientoId).FirstOrDefault();
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
        /// Obtiene un Tratamiento Problema para ver si existe
        /// </summary>
        /// <param name="filtro">Objeto Info que contiene los filtros para buscar el Problema Tratamiento</param>
        /// <returns></returns>
        public ProblemaTratamientoInfo ObtenerProblemaTratamientoExiste(ProblemaTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                return problemaTratamientoDAL.ObtenerProblemaTratamientoExiste(filtro);
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
        /// Metodo para Guardar/Modificar una entidad ProblemaTratamiento
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ProblemaTratamientoInfo info)
        {
            try
            {
                Logger.Info();
                return problemaTratamientoDAL.Guardar(info);
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
