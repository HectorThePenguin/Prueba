using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class UsuarioGrupoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad UsuarioGrupo
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(UsuarioGrupoInfo info)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                int result = info.UsuarioGrupoID;
                if (info.UsuarioGrupoID == 0)
                {
                    result = usuarioGrupoDAL.Crear(info);
                }
                else
                {
                    usuarioGrupoDAL.Actualizar(info);
                }
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
        /// Método para guardar la lista de los grupos.
        /// </summary>
        /// <param name="listaUsuarioGrupo"></param>
        internal void Guardar(IEnumerable<UsuarioGrupoInfo> listaUsuarioGrupo)
         {
             try
             {
                 Logger.Info();
                 var usuarioGrupoDAL = new UsuarioGrupoDAL();
                 usuarioGrupoDAL.Guardar(listaUsuarioGrupo);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<UsuarioGrupoInfo> ObtenerPorPagina(PaginacionInfo pagina, UsuarioGrupoInfo filtro)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                ResultadoInfo<UsuarioGrupoInfo> result = usuarioGrupoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de UsuarioGrupo
        /// </summary>
        /// <returns></returns>
        internal IList<UsuarioGrupoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                IList<UsuarioGrupoInfo> result = usuarioGrupoDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<UsuarioGrupoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                IList<UsuarioGrupoInfo> result = usuarioGrupoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad UsuarioGrupo por su Id
        /// </summary>
        /// <param name="usuarioGrupoID">Obtiene una entidad UsuarioGrupo por su Id</param>
        /// <returns></returns>
        internal UsuarioGrupoInfo ObtenerPorID(int usuarioGrupoID)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                UsuarioGrupoInfo result = usuarioGrupoDAL.ObtenerPorID(usuarioGrupoID);
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
        /// Obtiene una entidad UsuarioGrupo por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal UsuarioGrupoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                UsuarioGrupoInfo result = usuarioGrupoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una lista grupos del usuario.
        /// </summary>
        /// <returns></returns>
        internal IList<UsuarioGrupoInfo> ObtenerPorUsuarioID(int usuarioId)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoDAL = new UsuarioGrupoDAL();
                IList<UsuarioGrupoInfo> result = usuarioGrupoDAL.ObtenerPorUsuarioID(usuarioId);
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
    }
}

