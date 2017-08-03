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
    internal class TipoServicioDAL : BaseDAL
    {
        TipoServicioAccessor tipoServicioAccessor;

        protected override void inicializar()
        {
            tipoServicioAccessor = da.inicializarAccessor<TipoServicioAccessor>();
        }

        protected override void destruir()
        {
            tipoServicioAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de TipoServicio
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoServicioInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoServicioInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<TipoServicioInfo> result = new ResultadoInfo<TipoServicioInfo>();
                var condicion = da.Tabla<TipoServicioInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.TipoServicioId > 0)
                {
                    condicion = condicion.Where(e=> e.TipoServicioId == filtro.TipoServicioId);
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
        /// Obtiene una lista de TipoServicio
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoServicioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoServicioInfo>();
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
        /// Obtiene una lista de TipoServicio filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<TipoServicioInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de TipoServicio por su Id
        /// </summary>
        /// <param name="tipoServicioId">Obtiene una entidad TipoServicio por su Id</param>
        /// <returns></returns>
        public TipoServicioInfo ObtenerPorID(int tipoServicioId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.TipoServicioId == tipoServicioId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoServicio por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad TipoServicio por su descripcion</param>
        /// <returns></returns>
        public TipoServicioInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad TipoServicio
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoServicioInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.TipoServicioId > 0)
                {
                    id = da.Actualizar<TipoServicioInfo>(info);
                    tipoServicioAccessor.ActualizaFechaModificacion(info.TipoServicioId);
                }
                else
                {
                    id = da.Insertar<TipoServicioInfo>(info);
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
