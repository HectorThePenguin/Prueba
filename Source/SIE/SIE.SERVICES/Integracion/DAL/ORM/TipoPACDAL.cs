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
    internal class TipoPACDAL : BaseDAL
    {
        TipoPACAccessor tipoPACAccessor;

        protected override void inicializar()
        {
            tipoPACAccessor = da.inicializarAccessor<TipoPACAccessor>();
        }

        protected override void destruir()
        {
            tipoPACAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de TipoPAC
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoPACInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPACInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<TipoPACInfo> result = new ResultadoInfo<TipoPACInfo>();
                var condicion = da.Tabla<TipoPACInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.TipoPACID > 0)
                {
                    condicion = condicion.Where(e=> e.TipoPACID == filtro.TipoPACID);
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
        /// Obtiene una lista de TipoPAC
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoPACInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoPACInfo>();
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
        /// Obtiene una lista de TipoPAC filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<TipoPACInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de TipoPAC por su Id
        /// </summary>
        /// <param name="tipoPACId">Obtiene una entidad TipoPAC por su Id</param>
        /// <returns></returns>
        public TipoPACInfo ObtenerPorID(int tipoPACId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.TipoPACID == tipoPACId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoPAC por su descripcion
        /// </summary>
        /// <param name="descripción">Obtiene una entidad TipoPAC por su descripcion</param>
        /// <returns></returns>
        public TipoPACInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad TipoPAC
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoPACInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.TipoPACID > 0)
                {
                    id = da.Actualizar<TipoPACInfo>(info);
                    tipoPACAccessor.ActualizarFechaModificacion(info.TipoPACID);
                }
                else
                {
                    id = da.Insertar<TipoPACInfo>(info);
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
