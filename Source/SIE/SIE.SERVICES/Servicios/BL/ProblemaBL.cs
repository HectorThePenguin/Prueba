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
    public class ProblemaBL : IDisposable
    {
        ProblemaDAL problemaDAL;

        public ProblemaBL()
        {
            problemaDAL = new ProblemaDAL();
        }

        public void Dispose()
        {
            problemaDAL.Disposed += (s, e) =>
            {
                problemaDAL = null;
            };
            problemaDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de Problema
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProblemaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProblemaInfo filtro)
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de Problema
        /// </summary>
        /// <returns></returns>
        public IList<ProblemaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de Problema filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<ProblemaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de Problema por su Id
        /// </summary>
        /// <param name="problemaId">Obtiene una entidad Problema por su Id</param>
        /// <returns></returns>
        public ProblemaInfo ObtenerPorID(int problemaId)
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerTodos().Where(e=> e.ProblemaID == problemaId).FirstOrDefault();
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
        /// Obtiene una entidad de Problema por su Id
        /// </summary>
        /// <param name="filtro">Obtiene una entidad Problema por su Id</param>
        /// <returns></returns>
        public ProblemaInfo ObtenerPorID(ProblemaInfo filtro)
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerTodos().Where(e => e.ProblemaID == filtro.ProblemaID).FirstOrDefault();
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
        /// Obtiene una entidad de Problema por su Id
        /// </summary>
        /// <param name="filtro">Obtiene una entidad Problema por su Id</param>
        /// <returns></returns>
        public ProblemaInfo ObtenerPorIDTipoProblema(ProblemaInfo filtro)
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerTodosCompleto().FirstOrDefault(e => e.ProblemaID == filtro.ProblemaID && (e.TipoProblemaID == filtro.TipoProblema.TipoProblemaId || filtro.TipoProblema.TipoProblemaId == 0));
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
        /// Obtiene una entidad de Problema por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Problema por su descripcion</param>
        /// <returns></returns>
        public ProblemaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return problemaDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad Problema
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ProblemaInfo info)
        {
            try
            {
                Logger.Info();
                return problemaDAL.Guardar(info);
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

        internal IList<ProblemaInfo> ObtenerListaProblemas()
        {
            try
            {
                Logger.Info();
                var problemaDAL = new Integracion.DAL.Implementacion.ProblemaDAL();
                return problemaDAL.ObtenerListaProblemas();
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
