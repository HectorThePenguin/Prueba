using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class PrecioPACBL : IDisposable
    {
        PrecioPACDAL precioPACDAL;

        public PrecioPACBL()
        {
            precioPACDAL = new PrecioPACDAL();
        }

        public void Dispose()
        {
            precioPACDAL.Disposed += (s, e) =>
            {
                precioPACDAL = null;
            };
            precioPACDAL.Dispose();
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
                return precioPACDAL.ObtenerPorPagina(pagina, filtro);
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
        public IList<PrecioPACInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return precioPACDAL.ObtenerTodos().ToList();
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
        public IList<PrecioPACInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return precioPACDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
                return precioPACDAL.ObtenerTodos().Where(e => e.PrecioPACID == precioPACId).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad PrecioPAC
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(PrecioPACInfo info)
        {
            try
            {
                Logger.Info();
                return precioPACDAL.Guardar(info);
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
        /// Obtiene un Precio Pac por Organizacion y Fecha
        /// </summary>
        /// <param name="organizaionID"></param>
        /// <param name="tipoPACID"> </param>
        /// <param name="fecha"> </param>
        /// <returns></returns>
        public PrecioPACInfo ObtenerPorOrganizacionFecha(int organizaionID, int tipoPACID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                return
                    precioPACDAL.ObtenerTodos().FirstOrDefault(e => e.OrganizacionID == organizaionID &&
                                                                    e.TipoPACID == tipoPACID &&
                                                                    e.Activo == EstatusEnum.Activo);
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
        /// Obtiene el Precio PAC que se encuentra
        /// activo para la sucursal
        /// </summary>
        /// <param name="organizacionDestinoID"></param>
        /// <returns></returns>
        internal PrecioPACInfo ObtenerPrecioPACActivo(int organizacionDestinoID)
        {
            try
            {
                Logger.Info();
                return
                    precioPACDAL.ObtenerTodos().FirstOrDefault(e => e.OrganizacionID == organizacionDestinoID &&
                                                                    e.Activo == EstatusEnum.Activo);
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
        /// Obtiene el Precio PAC que se encuentra
        /// activo para la sucursal
        /// </summary>
        /// <param name="organizacionDestinoID"></param>
        /// <returns></returns>
        public PrecioPACInfo ObtenerPrecioPACActivoDAL(int organizacionDestinoID)
        {
            try
            {
                Logger.Info();
                var pacDAL = new Integracion.DAL.Implementacion.PrecioPACDAL();
                return pacDAL.ObtenerPrecioPACActivo(organizacionDestinoID);
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
