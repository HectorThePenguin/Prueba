using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class PesajeMateriaPrimaDAL : BaseDAL
    {
        PesajeMateriaPrimaAccessor pesajeMateriaPrimaAccessor;

        protected override void inicializar()
        {
            pesajeMateriaPrimaAccessor = da.inicializarAccessor<PesajeMateriaPrimaAccessor>();
        }

        protected override void destruir()
        {
            pesajeMateriaPrimaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de PesajeMateriaPrima
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PesajeMateriaPrimaInfo> ObtenerPorPagina(PaginacionInfo pagina, PesajeMateriaPrimaInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<PesajeMateriaPrimaInfo> result = new ResultadoInfo<PesajeMateriaPrimaInfo>();
                var condicion = da.Tabla<PesajeMateriaPrimaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.PesajeMateriaPrimaID > 0)
                {
                    condicion = condicion.Where(e=> e.PesajeMateriaPrimaID == filtro.PesajeMateriaPrimaID);
                }
                //if (!string.IsNullOrEmpty(filtro.PesajeMateriaPrimaID))
                //{
                //    condicion = condicion.Where(e=> e.PesajeMateriaPrimaID.Contains(filtro.PesajeMateriaPrimaID));
                //}
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
                                .OrderBy(e => e.PesajeMateriaPrimaID)
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
        /// Obtiene una lista de PesajeMateriaPrima
        /// </summary>
        /// <returns></returns>
        public IQueryable<PesajeMateriaPrimaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<PesajeMateriaPrimaInfo>();
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
        /// Obtiene una lista de PesajeMateriaPrima filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<PesajeMateriaPrimaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos();//.Where(e=> e.Activo == estatus);
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
        /// Obtiene una entidad de PesajeMateriaPrima por su Id
        /// </summary>
        /// <param name="pesajeMateriaPrimaId">Obtiene una entidad PesajeMateriaPrima por su Id</param>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo ObtenerPorID(int pesajeMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.PesajeMateriaPrimaID == pesajeMateriaPrimaId).FirstOrDefault();
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

        ///// <summary>
        ///// Obtiene una entidad de PesajeMateriaPrima por su descripcion
        ///// </summary>
        ///// <param name="descripción">Obtiene una entidad PesajeMateriaPrima por su descripcion</param>
        ///// <returns></returns>
        //public PesajeMateriaPrimaInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return this.ObtenerTodos().Where(e=> e.PesajeMateriaPrimaID.ToLower() == descripcion.ToLower()).FirstOrDefault();
        //    }
        //    catch(ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad PesajeMateriaPrima
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(PesajeMateriaPrimaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.PesajeMateriaPrimaID > 0)
                {
                    id = da.Actualizar<PesajeMateriaPrimaInfo>(info);
                    pesajeMateriaPrimaAccessor.ActualizarFechaModificacion(info.PesajeMateriaPrimaID);
                }
                else
                {
                    id = da.Insertar<PesajeMateriaPrimaInfo>(info);
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

        /// <summary>
        /// Obtiene una lista de PesajeMateriaPrima
        /// </summary>
        /// <returns></returns>
        public List<FiltroTicketInfo> ObtenerPorFiltro(FiltroTicketInfo filtro)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaAccessor.ObtenerTicketsFiltros(filtro.OrganizacionID, filtro.Ticket ?? string.Empty);
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
        /// Obtiene una lista de PesajeMateriaPrima
        /// </summary>
        /// <returns></returns>
        public List<FiltroTicketProduccionMolino> ObtenerValoresTicketProduccion(FiltroTicketInfo filtro)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaAccessor.ObtenerValoresTicketProduccion(filtro.OrganizacionID, filtro.Ticket, filtro.IndicadorID);
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
