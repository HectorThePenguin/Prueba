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
    internal class CausaTiempoMuertoDAL : BaseDAL
    {
        CausaTiempoMuertoAccessor causaTiempoMuertoAccessor;

        protected override void inicializar()
        {
            causaTiempoMuertoAccessor = da.inicializarAccessor<CausaTiempoMuertoAccessor>();
        }

        protected override void destruir()
        {
            causaTiempoMuertoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CausaTiempoMuerto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CausaTiempoMuertoInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaTiempoMuertoInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CausaTiempoMuertoInfo> result = new ResultadoInfo<CausaTiempoMuertoInfo>();
                var condicion = da.Tabla<CausaTiempoMuertoInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CausaTiempoMuertoID > 0)
                {
                    condicion = condicion.Where(e=> e.CausaTiempoMuertoID == filtro.CausaTiempoMuertoID);
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
        /// Obtiene una lista de CausaTiempoMuerto
        /// </summary>
        /// <returns></returns>
        public IQueryable<CausaTiempoMuertoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CausaTiempoMuertoInfo>();
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
        /// Obtiene una lista de CausaTiempoMuerto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CausaTiempoMuertoInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CausaTiempoMuerto por su Id
        /// </summary>
        /// <param name="causaTiempoMuertoId">Obtiene una entidad CausaTiempoMuerto por su Id</param>
        /// <returns></returns>
        public CausaTiempoMuertoInfo ObtenerPorID(int causaTiempoMuertoId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CausaTiempoMuertoID == causaTiempoMuertoId).FirstOrDefault();
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
        /// Obtiene una entidad de CausaTiempoMuerto por su descripcion
        /// </summary>
        /// <param name="descripción">Obtiene una entidad CausaTiempoMuerto por su descripcion</param>
        /// <returns></returns>
        public CausaTiempoMuertoInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad CausaTiempoMuerto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CausaTiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CausaTiempoMuertoID > 0)
                {
                    id = da.Actualizar<CausaTiempoMuertoInfo>(info);
                    causaTiempoMuertoAccessor.ActualizarFechaModificacion(info.CausaTiempoMuertoID);
                }
                else
                {
                    id = da.Insertar<CausaTiempoMuertoInfo>(info);
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
