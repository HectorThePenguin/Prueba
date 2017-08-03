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
    internal class EstatusDAL : BaseDAL
    {
        EstatusAccessor estatusAccessor;

        protected override void inicializar()
        {
            estatusAccessor = da.inicializarAccessor<EstatusAccessor>();
        }

        protected override void destruir()
        {
            estatusAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Estatus
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EstatusInfo> ObtenerPorPagina(PaginacionInfo pagina, EstatusInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<EstatusInfo>();
                var condicion = da.Tabla<EstatusInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.EstatusId > 0)
                {
                    condicion = condicion.Where(e=> e.EstatusId == filtro.EstatusId);
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
        /// Obtiene una lista de Estatus
        /// </summary>
        /// <returns></returns>
        public IQueryable<EstatusInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<EstatusInfo>();
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
        /// Obtiene una lista de Estatus filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<EstatusInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e=> e.Activo == estatus);
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
        /// Obtiene una entidad de Estatus por su Id
        /// </summary>
        /// <param name="estatusId">Obtiene una entidad Estatus por su Id</param>
        /// <returns></returns>
        public EstatusInfo ObtenerPorID(int estatusId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.EstatusId == estatusId);
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
        /// Obtiene una entidad de Estatus por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Estatus por su descripcion</param>
        /// <returns></returns>
        public EstatusInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
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
        /// Metodo para Guardar/Modificar una entidad Estatus
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(EstatusInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.EstatusId > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<EstatusInfo>(info);
                }
                else
                {
                    id = da.Insertar<EstatusInfo>(info);
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
