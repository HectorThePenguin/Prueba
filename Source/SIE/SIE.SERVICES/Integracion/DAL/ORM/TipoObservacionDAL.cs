using System;
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
    internal class TipoObservacionDAL : BaseDAL
    {
        TipoObservacionAccessor tipoObservacionAccessor;

        protected override void inicializar()
        {
            tipoObservacionAccessor = da.inicializarAccessor<TipoObservacionAccessor>();
        }

        protected override void destruir()
        {
            tipoObservacionAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de TipoObservacion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoObservacionInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<TipoObservacionInfo> result = new ResultadoInfo<TipoObservacionInfo>();
                var condicion = da.Tabla<TipoObservacionInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.TipoObservacionID > 0)
                {
                    condicion = condicion.Where(e=> e.TipoObservacionID == filtro.TipoObservacionID);
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
        /// Obtiene una lista de TipoObservacion
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoObservacionInfo>();
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
        /// Obtiene una lista de TipoObservacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<TipoObservacionInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de TipoObservacion por su Id
        /// </summary>
        /// <param name="tipoObservacionId">Obtiene una entidad TipoObservacion por su Id</param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorID(int tipoObservacionId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.TipoObservacionID == tipoObservacionId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoObservacion por su descripcion
        /// </summary>
        /// <param name="tipoObservacionId">Obtiene una entidad TipoObservacion por su descripcion</param>
        /// <returns></returns>
        public TipoObservacionInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad TipoObservacion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoObservacionInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.TipoObservacionID > 0)
                {
                    id = da.Actualizar<TipoObservacionInfo>(info);
                    tipoObservacionAccessor.ActualizaFechaModificacion(info.TipoObservacionID);
                }
                else
                {
                    id = da.Insertar<TipoObservacionInfo>(info);
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
