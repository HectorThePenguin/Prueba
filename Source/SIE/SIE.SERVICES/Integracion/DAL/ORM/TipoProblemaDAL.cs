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
    internal class TipoProblemaDAL : BaseDAL
    {
        TipoProblemaAccessor tipoProblemaAccessor;

        protected override void inicializar()
        {
            tipoProblemaAccessor = da.inicializarAccessor<TipoProblemaAccessor>();
        }

        protected override void destruir()
        {
            tipoProblemaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de TipoProblema
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoProblemaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProblemaInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<TipoProblemaInfo> result = new ResultadoInfo<TipoProblemaInfo>();
                var condicion = da.Tabla<TipoProblemaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.TipoProblemaId > 0)
                {
                    condicion = condicion.Where(e => e.TipoProblemaId == filtro.TipoProblemaId);
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
        /// Obtiene una lista de TipoProblema
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoProblemaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoProblemaInfo>();
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
        /// Obtiene una lista de TipoProblema filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<TipoProblemaInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de TipoProblema por su Id
        /// </summary>
        /// <param name="tipoProblemaId">Obtiene una entidad TipoProblema por su Id</param>
        /// <returns></returns>
        public TipoProblemaInfo ObtenerPorID(int tipoProblemaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.TipoProblemaId == tipoProblemaId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoProblema por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad TipoProblema por su descripcion</param>
        /// <returns></returns>
        public TipoProblemaInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad TipoProblema
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoProblemaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.TipoProblemaId > 0)
                {
                    id = da.Actualizar<TipoProblemaInfo>(info);
                    tipoProblemaAccessor.ActualizaFechaModificacion(info.TipoProblemaId);
                }
                else
                {
                    id = da.Insertar<TipoProblemaInfo>(info);
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
