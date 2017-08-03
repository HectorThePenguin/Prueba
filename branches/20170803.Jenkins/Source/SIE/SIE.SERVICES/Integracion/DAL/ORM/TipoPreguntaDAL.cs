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
    internal class TipoPreguntaDAL : BaseDAL
    {
        TipoPreguntaAccessor tipoPreguntaAccessor;

        protected override void inicializar()
        {
            tipoPreguntaAccessor = da.inicializarAccessor<TipoPreguntaAccessor>();
        }

        protected override void destruir()
        {
            tipoPreguntaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de TipoPregunta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoPreguntaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPreguntaInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<TipoPreguntaInfo> result = new ResultadoInfo<TipoPreguntaInfo>();
                var condicion = da.Tabla<TipoPreguntaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.TipoPreguntaID > 0)
                {
                    condicion = condicion.Where(e=> e.TipoPreguntaID == filtro.TipoPreguntaID);
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
        /// Obtiene una lista de TipoPregunta
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoPreguntaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoPreguntaInfo>();
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
        /// Obtiene una lista de TipoPregunta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<TipoPreguntaInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de TipoPregunta por su Id
        /// </summary>
        /// <param name="tipoPreguntaId">Obtiene una entidad TipoPregunta por su Id</param>
        /// <returns></returns>
        public TipoPreguntaInfo ObtenerPorID(int tipoPreguntaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.TipoPreguntaID == tipoPreguntaId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoPregunta por su descripcion
        /// </summary>
        /// <param name="tipoPreguntaId">Obtiene una entidad TipoPregunta por su descripcion</param>
        /// <returns></returns>
        public TipoPreguntaInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad TipoPregunta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoPreguntaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.TipoPreguntaID > 0)
                {
                    id = da.Actualizar<TipoPreguntaInfo>(info);
                    tipoPreguntaAccessor.ActualizaFechaModificacion(info.TipoPreguntaID);
                }
                else
                {
                    id = da.Insertar<TipoPreguntaInfo>(info);
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
