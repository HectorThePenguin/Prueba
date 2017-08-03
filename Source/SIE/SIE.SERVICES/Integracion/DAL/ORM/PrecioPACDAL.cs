using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class PrecioPACDAL : BaseDAL
    {
        PrecioPACAccessor precioPACAccessor;

        protected override void inicializar()
        {
            precioPACAccessor = da.inicializarAccessor<PrecioPACAccessor>();
        }

        protected override void destruir()
        {
            precioPACAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de PrecioPAC
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PrecioPACInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioPACInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<PrecioPACInfo>();
                var condicion = da.Tabla<PrecioPACInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.PrecioPACID > 0)
                {
                    condicion = condicion.Where(e=> e.PrecioPACID == filtro.PrecioPACID);
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
                                .OrderBy(e => e.PrecioPACID)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();

                var tiposPAC = base.da.Tabla<TipoPACInfo>();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> organizaciones = organizacionBL.ObtenerTodos(EstatusEnum.Activo);
                result.Lista.ToList().ForEach(datos =>
                                                  {
                                                      datos.Organizacion =
                                                          organizaciones.FirstOrDefault(
                                                              id => id.OrganizacionID == datos.OrganizacionID);
                                                      datos.TipoPAC =
                                                          tiposPAC.FirstOrDefault(id => id.TipoPACID == datos.TipoPACID);
                                                  });
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
        /// Obtiene una lista de PrecioPAC
        /// </summary>
        /// <returns></returns>
        public IQueryable<PrecioPACInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<PrecioPACInfo>();
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
        /// Obtiene una lista de PrecioPAC filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<PrecioPACInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de PrecioPAC por su Id
        /// </summary>
        /// <param name="precioPACId">Obtiene una entidad PrecioPAC por su Id</param>
        /// <returns></returns>
        public PrecioPACInfo ObtenerPorID(int precioPACId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.PrecioPACID == precioPACId).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad PrecioPAC
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(PrecioPACInfo info)
        {
            try
            {
                Logger.Info();
                int id;

                info.OrganizacionID = info.Organizacion.OrganizacionID;
                info.TipoPACID = info.TipoPAC.TipoPACID;

                if (info.PrecioPACID > 0)
                {
                    id = da.Actualizar<PrecioPACInfo>(info);
                    precioPACAccessor.ActualizarFechaModificacion(info.PrecioPACID);
                }
                else
                {
                    id = da.Insertar<PrecioPACInfo>(info);
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
