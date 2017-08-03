using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class CentroCostoUsuarioBL : IDisposable
    {
        CentroCostoUsuarioDAL centroCostoUsuarioDAL;

        public CentroCostoUsuarioBL()
        {
            centroCostoUsuarioDAL = new CentroCostoUsuarioDAL();
        }

        public void Dispose()
        {
            centroCostoUsuarioDAL.Disposed += (s, e) =>
            {
                centroCostoUsuarioDAL = null;
            };
            centroCostoUsuarioDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CentroCostoAutorizador
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CentroCostoUsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, CentroCostoUsuarioInfo filtro)
        {
            try
            {
                Logger.Info();
                return centroCostoUsuarioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CentroCostoAutorizador
        /// </summary>
        /// <returns></returns>
        public IList<CentroCostoUsuarioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return centroCostoUsuarioDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CentroCostoAutorizador filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CentroCostoUsuarioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return centroCostoUsuarioDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CentroCostoAutorizador por su Id
        /// </summary>
        /// <param name="centroCostoAutorizadorId">Obtiene una entidad CentroCostoAutorizador por su Id</param>
        /// <returns></returns>
        public CentroCostoUsuarioInfo ObtenerPorID(int centroCostoAutorizadorId)
        {
            try
            {
                Logger.Info();
                return centroCostoUsuarioDAL.ObtenerTodos().Where(e=> e.CentroCostoUsuarioID == centroCostoAutorizadorId).FirstOrDefault();
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
        /// Obtiene una entidad CentroCostoAutorizador por centrocostoId
        /// </summary>
        /// <param name="centroCostoId"></param>
        /// <returns></returns>
        public List<CentroCostoUsuarioInfo> ObtenerPorCentroCostoID(int centroCostoId)
        {
            try
            {
                Logger.Info();
                var result =
                    centroCostoUsuarioDAL.ObtenerTodosCompleto().Where(e => e.CentroCostoID == centroCostoId).ToList();
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
        /// Obtiene una entidad CentroCostoAutorizador por centrocostoId
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="accion"> </param>
        /// <returns></returns>
        public CentroCostoUsuarioInfo ObtenerPorUsuarioAccion(int usuarioId, bool accion)
        {
            try
            {
                Logger.Info();
                var result =
                    centroCostoUsuarioDAL.ObtenerTodos().Where(e => e.UsuarioID == usuarioId && e.Autoriza == accion && e.Activo == EstatusEnum.Activo);
                return result.FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CentroCostoAutorizador
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CentroCostoUsuarioInfo info)
        {
            try
            {
                Logger.Info();
                return centroCostoUsuarioDAL.Guardar(info);
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
        /// Metodo para Guardar/Modificar una entidad CentroCostoAutorizador
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public void GuardarCentroCostoUsuarioLista(CentroCostoInfo info)
        {
            try
            {
                Logger.Info();
                using (var scope = new TransactionScope())
                {
                    foreach (var centroCostoUsuario in info.ListaCentroCostoUsuario)
                    {
                        centroCostoUsuarioDAL.Guardar(centroCostoUsuario);
                    }
                    scope.Complete();
                }
                
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
