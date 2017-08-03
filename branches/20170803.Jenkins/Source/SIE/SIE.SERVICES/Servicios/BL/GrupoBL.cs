using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Transactions;

namespace SIE.Services.Servicios.BL
{
    internal class GrupoBL 
    {
        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<GrupoInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoInfo filtro)
        {
            ResultadoInfo<GrupoInfo> result;
            try
            {
                Logger.Info();
                var grupoDAL = new GrupoDAL();
                result = grupoDAL.ObtenerPorPagina(pagina, filtro);
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
            return result;
        }

        /// <summary>
        /// Método que crear un grupo
        /// </summary>
        /// <param name="grupoInfo"></param>
        internal void Guardar(GrupoInfo grupoInfo)
        {
            try
            {
                Logger.Info();
                using (var scope = new TransactionScope())
                {
                    var grupoDAL = new GrupoDAL();
                    if (grupoInfo.GrupoID != 0)
                    {
                        grupoDAL.Actualizar(grupoInfo);
                    }
                    else
                    {
                        int grupoID = grupoDAL.Guardar(grupoInfo);
                        grupoInfo.GrupoID = grupoID;
                        grupoInfo.GrupoFormularioInfo.Grupo = grupoInfo;
                    }
                    var grupoFormularioBL = new GrupoFormularioBL();
                    grupoFormularioBL.Guardar(grupoInfo.GrupoFormularioInfo);
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

        /// <summary>
        /// Obtiene un grupo por Id
        /// </summary>
        /// <param name="grupoID"></param>
        /// <returns></returns>
        internal GrupoInfo ObtenerPorID(int grupoID)
        {
            GrupoInfo result;
            try
            {
                Logger.Info();
                var grupoDAL = new GrupoDAL();
                result = grupoDAL.ObtenerPorID(grupoID);
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
            return result;
        }

        /// <summary>
        /// Método que inactiva un grupo
        /// </summary>
        /// <param name="grupoID"></param>
        internal void BorrarGrupo(int grupoID)
        {
            try
            {
                Logger.Info();
                var grupoDAL = new GrupoDAL();
                grupoDAL.BorrarGrupo(grupoID);
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
        /// Obtiene una entidad Grupo por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal GrupoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var grupoDAL = new GrupoDAL();
                GrupoInfo result = grupoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un lista de grupos.
        /// </summary>
        /// <returns></returns>
        internal IList<GrupoInfo> ObtenerTodos()
        {
           try
            {
                Logger.Info();
                var grupoDAL = new GrupoDAL();
                IList<GrupoInfo> lista = grupoDAL.ObtenerTodos();
                return lista;
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
        /// Obtiene un lista de grupos.
        /// </summary>
        /// <returns></returns>
        internal IList<GrupoInfo> ObtenerTodos(EstatusEnum activo)
        {
            try
            {
                Logger.Info();
                var grupoDAL = new GrupoDAL();
                IList<GrupoInfo> lista = grupoDAL.ObtenerTodos(activo);
                return lista;
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