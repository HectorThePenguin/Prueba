using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class ProblemaDAL : BaseDAL
    {
        ProblemaAccessor problemaAccessor;

        protected override void inicializar()
        {
            problemaAccessor = da.inicializarAccessor<ProblemaAccessor>();
        }

        protected override void destruir()
        {
            problemaAccessor = null;
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
                ResultadoInfo<ProblemaInfo> result = new ResultadoInfo<ProblemaInfo>();
                var condicion = da.Tabla<ProblemaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.ProblemaID > 0)
                {
                    condicion = condicion.Where(e => e.ProblemaID == filtro.ProblemaID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
                }
                if(filtro.TipoProblema != null && filtro.TipoProblema.TipoProblemaId > 0)
                {
                    condicion = condicion.Where(e => e.TipoProblemaID == filtro.TipoProblema.TipoProblemaId);
                }
                result.TotalRegistros = condicion.Count();
                
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.Descripcion)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);


                result.Lista = paginado.ToList();

                CargarTiposProblema(result.Lista);

                return result;
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

        private void CargarTiposProblema(IList<ProblemaInfo> lista)
        {
            var tipoProblemaDAL = new TipoProblemaDAL();
            List<TipoProblemaInfo> listaTipoProblemas = tipoProblemaDAL.ObtenerTodos().ToList();
            foreach (var problema in lista)
            {
                TipoProblemaInfo tipoProblema =
                    listaTipoProblemas.FirstOrDefault(tipo => tipo.TipoProblemaId == problema.TipoProblemaID);
                if(tipoProblema == null)
                {
                    continue;
                }
                problema.TipoProblema = tipoProblema;
            }
        }

        /// <summary>
        /// Obtiene una lista de Problema
        /// </summary>
        /// <returns></returns>
        public IList<ProblemaInfo> ObtenerTodosCompleto()
        {
            try
            {
                Logger.Info();
                var listaProblemas = da.Tabla<ProblemaInfo>().ToList();
                CargarTiposProblema(listaProblemas);
                return listaProblemas;
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
        /// Obtiene una lista de Problema
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProblemaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<ProblemaInfo>();
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
        public IQueryable<ProblemaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus);
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
                return this.ObtenerTodos().Where(e => e.ProblemaID == problemaId).FirstOrDefault();
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
        /// Obtiene una entidad de Problema por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Problema por su descripcion</param>
        /// <returns></returns>
        public ProblemaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
                var id = 0;
                if (info.ProblemaID > 0)
                {
                    id = da.Actualizar<ProblemaInfo>(info);
                    problemaAccessor.ActualizaFechaModificacion(info.ProblemaID);
                }
                else
                {
                    id = da.Insertar<ProblemaInfo>(info);
                }
                return id;
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
