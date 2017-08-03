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
    internal class ObservacionDAL : BaseDAL
    {
        ObservacionAccessor observacionAccessor;

        protected override void inicializar()
        {
            observacionAccessor = da.inicializarAccessor<ObservacionAccessor>();
        }

        protected override void destruir()
        {
            observacionAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Observacion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ObservacionInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<ObservacionInfo> result = new ResultadoInfo<ObservacionInfo>();
                var condicion = da.Tabla<ObservacionInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.ObservacionID > 0)
                {
                    condicion = condicion.Where(e=> e.ObservacionID == filtro.ObservacionID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
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

        /// <summary>
        /// Obtiene una lista de Observacion
        /// </summary>
        /// <returns></returns>
        public IList<ObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                IQueryable<ObservacionInfo> listaObservaciones = da.Tabla<ObservacionInfo>();

                var lista = listaObservaciones.ToList();

                var tipoObservacionDAL = new TipoObservacionDAL();

                IQueryable<TipoObservacionInfo> listaTiposProblema = tipoObservacionDAL.ObtenerTodos();

                foreach (var observacion in lista)
                {
                    TipoObservacionInfo tipoObservacion =
                        listaTiposProblema.FirstOrDefault(
                            tipo => tipo.TipoObservacionID == observacion.TipoObservacionID);
                    if(tipoObservacion== null)
                    {
                        continue;
                    }
                    observacion.TipoObservacion = tipoObservacion;
                }

                return lista;
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
        /// Obtiene una lista de Observacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<ObservacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de Observacion por su Id
        /// </summary>
        /// <param name="observacionId">Obtiene una entidad Observacion por su Id</param>
        /// <returns></returns>
        public ObservacionInfo ObtenerPorID(int observacionId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.ObservacionID == observacionId).FirstOrDefault();
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
        /// Obtiene una entidad de Observacion por su descripcion
        /// </summary>
        /// <param name="observacionId">Obtiene una entidad Observacion por su descripcion</param>
        /// <returns></returns>
        public ObservacionInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad Observacion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ObservacionInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.ObservacionID > 0)
                {
                    id = da.Actualizar<ObservacionInfo>(info);
                    observacionAccessor.ActualizarFechaModificacion(info.ObservacionID);
                }
                else
                {
                    id = da.Insertar<ObservacionInfo>(info);
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
