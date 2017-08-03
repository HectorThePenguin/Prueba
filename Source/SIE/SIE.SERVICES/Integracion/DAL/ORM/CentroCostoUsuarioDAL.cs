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

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class CentroCostoUsuarioDAL : BaseDAL
    {
        CentroCostoUsuarioAccessor centroCostoUsuarioAccessor;

        protected override void inicializar()
        {
            centroCostoUsuarioAccessor = da.inicializarAccessor<CentroCostoUsuarioAccessor>();
        }

        protected override void destruir()
        {
            centroCostoUsuarioAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CentroCostoUsuario
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CentroCostoUsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, CentroCostoUsuarioInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<CentroCostoUsuarioInfo>();
                var condicion = da.Tabla<CentroCostoUsuarioInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CentroCostoUsuarioID > 0)
                {
                    condicion = condicion.Where(e=> e.CentroCostoID == filtro.CentroCostoUsuarioID);
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
                                .OrderBy(e => e.CentroCostoUsuarioID)
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
        /// Obtiene una lista de CentroCostoUsuario
        /// </summary>
        /// <returns></returns>
        public IQueryable<CentroCostoUsuarioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var usuarios = da.Tabla<UsuarioInfo>().ToList();
                var centrosCostoUsuario = da.Tabla<CentroCostoUsuarioInfo>();

                if(centrosCostoUsuario.Any())
                {
                    foreach (var centroCostoUsuarioInfo in centrosCostoUsuario)
                    {
                        var usuarioCentro =
                            usuarios.FirstOrDefault(cen => cen.UsuarioID == centroCostoUsuarioInfo.UsuarioID);
                        if(usuarioCentro != null)
                        {
                            centroCostoUsuarioInfo.Usuario = usuarioCentro;
                        }
                    }
                }
                return centrosCostoUsuario;

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
        /// Obtiene una lista de CentroCostoUsuario
        /// </summary>
        /// <returns></returns>
        public IList<CentroCostoUsuarioInfo> ObtenerTodosCompleto()
        {
            try
            {
                Logger.Info();
                var usuarios = da.Tabla<UsuarioInfo>().ToList();
                var centrosCostoUsuario = da.Tabla<CentroCostoUsuarioInfo>().ToList();

                if (centrosCostoUsuario.Any())
                {
                    foreach (var centroCostoUsuarioInfo in centrosCostoUsuario)
                    {
                        var usuarioCentro =
                            usuarios.FirstOrDefault(cen => cen.UsuarioID == centroCostoUsuarioInfo.UsuarioID);
                        if (usuarioCentro != null)
                        {
                            centroCostoUsuarioInfo.Usuario = usuarioCentro;
                        }
                    }
                }
                return centrosCostoUsuario;

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
        /// Obtiene una lista de CentroCostoUsuario filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CentroCostoUsuarioInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CentroCostoUsuario por su Id
        /// </summary>
        /// <param name="centroCostoUsuarioId">Obtiene una entidad CentroCostoUsuario por su Id</param>
        /// <returns></returns>
        public CentroCostoUsuarioInfo ObtenerPorID(int centroCostoUsuarioId)
        {
            try
            {
                Logger.Info();
                var query = ObtenerTodos().Where(c => c.CentroCostoUsuarioID == centroCostoUsuarioId);
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
        /// Obtiene una entidad de CentroCostoUsuario por su descripcion
        /// </summary>
        /// <param name="centroCostoId">Obtiene una entidad CentroCostoUsuario por su descripcion</param>
        /// <returns></returns>
        public IList<CentroCostoUsuarioInfo> ObtenerPorCentroCostoID(int centroCostoId)
        {
            try
            {
                Logger.Info();
                var results = ObtenerTodos().Where(c => c.CentroCostoID == centroCostoId);
                return results.ToList();
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
        /// Metodo para Guardar/Modificar una entidad CentroCostoUsuario
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CentroCostoUsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CentroCostoUsuarioID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<CentroCostoUsuarioInfo>(info);
                }
                else
                {
                    id = da.Insertar<CentroCostoUsuarioInfo>(info);
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
