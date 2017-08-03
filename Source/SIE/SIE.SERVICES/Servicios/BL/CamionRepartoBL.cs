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
    public class CamionRepartoBL : IDisposable
    {
        CamionRepartoDAL camionRepartoDAL;

        public CamionRepartoBL()
        {
            camionRepartoDAL = new CamionRepartoDAL();
        }

        public void Dispose()
        {
            camionRepartoDAL.Disposed += (s, e) =>
            {
                camionRepartoDAL = null;
            };
            camionRepartoDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CamionReparto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CamionRepartoInfo>ObtenerPorPagina(PaginacionInfo pagina, CamionRepartoInfo filtro)
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public IList<CamionRepartoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CamionReparto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CamionRepartoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CamionReparto por su Id
        /// </summary>
        /// <param name="camionRepartoId">Obtiene una entidad CamionReparto por su Id</param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorID(int camionRepartoId)
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.ObtenerTodos().Where(e=> e.CamionRepartoID == camionRepartoId).FirstOrDefault();
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
        /// Obtiene una entidad de CamionReparto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CamionReparto por su descripcion</param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorDescripcion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                return
                    camionRepartoDAL.ObtenerTodos().FirstOrDefault(
                        e => e.NumeroEconomico.ToLower() == descripcion.ToLower() && e.OrganizacionID == organizacionID);
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
        /// Metodo para Guardar/Modificar una entidad CamionReparto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CamionRepartoInfo info)
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.Guardar(info);
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
        /// Obtiene una entidad de CamionReparto por su descripcion
        /// </summary>
        /// <param name="numeroEconomico">Obtiene una entidad CamionReparto por su numero economico</param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorNumeroEconomico(string numeroEconomico, int organizacionID)
        {
            try
            {
                Logger.Info();
                return
                    camionRepartoDAL.ObtenerPorNumeroEconomico(numeroEconomico, organizacionID);
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
        /// Obtiene una entidad de CamionReparto por su descripcion
        /// </summary>
        /// <param name="numeroEconomico">Obtiene una entidad CamionReparto por su numero economico</param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public IList<CamionRepartoInfo> ObtenerPorNumeroEconomicoBusqueda(string numeroEconomico, int organizacionID)
        {
            try
            {
                Logger.Info();
                return
                    camionRepartoDAL.ObtenerPorNumeroEconomicoBusqueda(numeroEconomico, organizacionID).ToList();
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
        /// Obtiene una entidad de CamionReparto por su Id
        /// </summary>
        /// <param name="camionReparto">Obtiene una entidad CamionReparto por su Id</param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorID(CamionRepartoInfo camionReparto)
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.ObtenerTodos().Where(e => e.CamionRepartoID == camionReparto.CamionRepartoID).FirstOrDefault();
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
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public IList<CamionRepartoInfo> ObtenerPorOrganizacionID(int organizacionID)
        {
            try
            {
                Logger.Info();
                return camionRepartoDAL.ObtenerPorOrganizacionID(organizacionID).ToList();
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

        internal CamionRepartoInfo ObtenerCamionRepartoPorID(CamionRepartoInfo camionReparto)
        {
            CamionRepartoInfo info;
            try
            {
                Logger.Info();
                var camionRepartoDAL = new CamionRepartoDAL();
                info = camionRepartoDAL.ObtenerCamionRepartoPorID(camionReparto);
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
            return info;
        }
    }
}
