using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Data.SqlClient;
using System.Data;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class LoteDAL : BaseDAL
    {
        LoteAccessor loteAccessor;

        protected override void inicializar()
        {
            loteAccessor = da.inicializarAccessor<LoteAccessor>();
        }

        protected override void destruir()
        {
            loteAccessor = null;
        }

        private IQueryable<LoteInfo> consultaPaginador(int? loteid, string descripcion, EstatusEnum? estatus, int? corralid)
        {
            var condicion = da.Tabla<LoteInfo>();
            condicion = loteid.HasValue && loteid.Value != 0 ? condicion.Where(e => e.LoteID == loteid.Value) : condicion;
            condicion = descripcion != null ? condicion.Where(e => e.Lote.Contains(descripcion)) : condicion;
            condicion = estatus.HasValue ? condicion.Where(e => e.Activo == estatus.Value) : condicion;
            condicion = corralid.HasValue ? condicion.Where(e => e.CorralID == corralid.Value) : condicion;
            return condicion;
        }

        /// <summary>
        /// Obtiene una lista paginada de Lote
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<LoteInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<LoteInfo>();

                var condicion = consultaPaginador(
                    filtro.LoteID
                    , filtro.Lote == string.Empty ? null : filtro.Lote
                    , filtro.Activo
                    , null);

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
                                .OrderBy(e => e.Lote)
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
        /// Obtiene una lista de Lote
        /// </summary>
        /// <returns></returns>
        public IQueryable<LoteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<LoteInfo>();
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
        /// Obtiene una lista de Lote filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<LoteInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de Lote por su Id
        /// </summary>
        /// <param name="loteId">Obtiene una entidad Lote por su Id</param>
        /// <returns></returns>
        public LoteInfo ObtenerPorID(int loteId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.LoteID == loteId).FirstOrDefault();
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
        /// Obtiene una entidad de Lote por su descripcion
        /// </summary>
        /// <param name="loteId">Obtiene una entidad Lote por su descripcion</param>
        /// <returns></returns>
        public LoteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Lote.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad Lote
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(LoteInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.LoteID > 0)
                {
                    id = da.Actualizar<LoteInfo>(info);
                }
                else
                {
                    id = da.Insertar<LoteInfo>(info);
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

        internal ResultadoInfo<LoteInfo> ObtenerLotesCorralPorPagina(PaginacionInfo paginacion, LoteInfo filtro)
        {
            try
            {
                Logger.Info();

                ResultadoInfo<LoteInfo> result = new ResultadoInfo<LoteInfo>();

                var consulta = consultaPaginador(
                    filtro.LoteID
                    , filtro.Lote == string.Empty ? null : filtro.Lote
                    , null
                    , filtro.CorralID);

                consulta = consulta.Where(e => e.CorralID == filtro.CorralID);
                result.TotalRegistros = consulta.Count();

                var paginado = consulta
                                .OrderByDescending(e => e.LoteID)
                                .Skip((paginacion.Inicio - 1))
                                .Take(paginacion.Limite);

                result.Lista = paginado.ToList();

                return result;

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal DateTime? ObtenerFechaUltimoConsumo(int loteId)
        {
            try
            {
                Logger.Info();

                var repartos = da.Tabla<RepartoInfo>();
                var repartosDetalle = da.Tabla<RepartoDetalleInfo>();

                var consulta = from r in repartos
                               join rd in repartosDetalle on r.RepartoID equals rd.RepartoID
                               where rd.Servido && r.LoteID == loteId
                                     &&
                                     (rd.TipoServicioID == (int) TipoServicioEnum.Matutino ||
                                      rd.TipoServicioID == (int) TipoServicioEnum.Vespertino)
                               orderby r.Fecha descending , rd.TipoServicioID descending
                               select new {r.Fecha, rd.TipoServicioID, FechaServer = AuxGeneral.GetDate()};

                var ultimoConsumo = consulta.FirstOrDefault();
                DateTime? fechaUltimoConsumo = null;
                if (ultimoConsumo != null)
                {
                    if (ultimoConsumo.Fecha.SoloFecha() == ultimoConsumo.FechaServer.SoloFecha()
                        && ultimoConsumo.TipoServicioID == (int)TipoServicioEnum.Matutino)
                    {
                        ultimoConsumo = consulta.Where(e => e.Fecha < ultimoConsumo.Fecha.SoloFecha()).FirstOrDefault();
                    }
                    if (ultimoConsumo != null)
                    {
                        fechaUltimoConsumo = ultimoConsumo.Fecha;
                    }
                }
                return fechaUltimoConsumo;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal LoteInfo ObtenerLoteDeCorralPorLoteID(LoteInfo info)
        {
            try
            {
                Logger.Info();

                var lotes = da.Tabla<LoteInfo>();

                var consulta = from l in lotes
                               where l.LoteID == info.LoteID && l.CorralID == info.CorralID
                               select l;

                var result = consulta.FirstOrDefault();

                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal LoteInfo ObtenerLotePorCorral(LoteInfo info)
        {
            try
            {
                Logger.Info();
                var lotes = da.Tabla<LoteInfo>();
                var consulta = from l in lotes
                               where l.CorralID == info.Corral.CorralID
                                    //&& l.Activo == EstatusEnum.Activo
                               select l;
                var result = consulta.FirstOrDefault();
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
