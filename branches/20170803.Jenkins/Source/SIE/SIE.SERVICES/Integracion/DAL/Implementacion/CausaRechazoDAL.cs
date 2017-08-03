using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CausaRechazoDAL : BaseDAL
    {
        CausaRechazoAccessor causaRechazoAccessor;

        protected override void inicializar()
        {
            causaRechazoAccessor = da.inicializarAccessor<CausaRechazoAccessor>();
        }

        protected override void destruir()
        {
            causaRechazoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CausaRechazo
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CausaRechazoInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaRechazoInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CausaRechazoInfo> result = new ResultadoInfo<CausaRechazoInfo>();
                var condicion = da.Tabla<CausaRechazoInfo>().Where(e => e.Activo == filtro.Activo);
                if (filtro.CausaRechazoID > 0)
                {
                    condicion = condicion.Where(e => e.CausaRechazoID == filtro.CausaRechazoID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e => e.Descripcion.Contains(filtro.Descripcion));
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
        /// Obtiene una lista de CausaRechazo
        /// </summary>
        /// <returns></returns>
        public IQueryable<CausaRechazoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CausaRechazoInfo>();
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
        /// Obtiene una lista de CausaRechazo filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CausaRechazoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.Activo == estatus);
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
        /// Obtiene una entidad de CausaRechazo por su Id
        /// </summary>
        /// <param name="causaRechazoId">Obtiene una entidad CausaRechazo por su Id</param>
        /// <returns></returns>
        public CausaRechazoInfo ObtenerPorID(int causaRechazoId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.CausaRechazoID == causaRechazoId).FirstOrDefault();
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
        /// Obtiene una entidad de CausaRechazo por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CausaRechazo por su descripcion</param>
        /// <returns></returns>
        public CausaRechazoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CausaRechazo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CausaRechazoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CausaRechazoID > 0)
                {
                    id = da.Actualizar<CausaRechazoInfo>(info);
                    causaRechazoAccessor.ActualizaFechaModificacion(info.CausaRechazoID);
                }
                else
                {
                    id = da.Insertar<CausaRechazoInfo>(info);
                }
                return id;
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
    }
}
