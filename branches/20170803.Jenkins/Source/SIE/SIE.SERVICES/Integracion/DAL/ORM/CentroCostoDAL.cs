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
    internal class CentroCostoDAL : BaseDAL
    {
        CentroCostoAccessor centroCostoAccessor;

        protected override void inicializar()
        {
            centroCostoAccessor = da.inicializarAccessor<CentroCostoAccessor>();
        }

        protected override void destruir()
        {
            centroCostoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CentroCosto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CentroCostoInfo> ObtenerPorPagina(PaginacionInfo pagina, CentroCostoInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<CentroCostoInfo>();
                var condicion = da.Tabla<CentroCostoInfo>();

                //if(filtro.AutorizadorID > 0)
                //{
                //    condicion = centroCostoAccessor.ObtenerPorAutorizador(filtro.CentroCostoID, filtro.AutorizadorID).AsQueryable();
                //}

                condicion = condicion.Where(e => e.Activo == filtro.Activo);

                if (filtro.CentroCostoID > 0)
                {
                    condicion = condicion.Where(e=> e.CentroCostoID == filtro.CentroCostoID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
                }
                if (!string.IsNullOrEmpty(filtro.CentroCostoSAP))
                {
                    condicion = condicion.Where(e => e.CentroCostoSAP.Contains(filtro.CentroCostoSAP));
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
        /// Obtiene una lista de CentroCosto
        /// </summary>
        /// <returns></returns>
        public IQueryable<CentroCostoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CentroCostoInfo>();
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
        /// Obtiene una lista de CentroCosto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CentroCostoInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CentroCosto por su Id
        /// </summary>
        /// <param name="centroCostoId">Obtiene una entidad CentroCosto por su Id</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorID(int centroCostoId)
        {
            try
            {
                Logger.Info();
                var query = ObtenerTodos().Where(c => c.CentroCostoID == centroCostoId);
                var results = query.ToList();
                return results.FirstOrDefault();
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
        /// Obtiene una entidad de CentroCosto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CentroCosto por su descripcion</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var query = ObtenerTodos().Where(c => c.Descripcion == descripcion);
                var results = query.ToList();
                return results.FirstOrDefault();
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
        /// Obtiene una entidad de CentroCosto por su CentroCostoSAP
        /// </summary>
        /// <param name="centroCosto">Obtiene una entidad CentroCosto por su cuenta contable</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorCentroCostoSAP(CentroCostoInfo centroCosto)
        {
            try
            {
                Logger.Info();
                centroCosto.CentroCostoSAP = centroCosto.CentroCostoSAP.PadLeft(6, '0');
                var query = ObtenerTodos().Where(c => c.CentroCostoSAP == centroCosto.CentroCostoSAP);
                var results = query.ToList();
                return results.FirstOrDefault();
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
        /// Obtiene una lista de CentroCosto
        /// </summary>
        /// <returns></returns>
        public IQueryable<CentroCostoInfo> ObtenerPorAutorizador(CentroCostoInfo filtro)
        {
            try
            {
                Logger.Info();
                var result =
                    centroCostoAccessor.ObtenerPorAutorizador(filtro.CentroCostoID, filtro.AutorizadorID).AsQueryable();
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
        /// Obtiene el autorizador del centro de costo
        /// </summary>
        /// <returns></returns>
        public CentroCostoUsuarioInfo ObtenerAutorizador(int filtro)
        {
            try
            {
                Logger.Info();
                var query = da.Tabla<CentroCostoUsuarioInfo>().Where(c => c.CentroCostoID == filtro);
                var results = query.ToList();
                return results.FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CentroCosto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CentroCostoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CentroCostoID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<CentroCostoInfo>(info);
                }
                else
                {
                    id = da.Insertar<CentroCostoInfo>(info);
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
