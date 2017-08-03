using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class UsuarioGrupoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad UsuarioGrupo
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(UsuarioGrupoInfo info)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                int result = usuarioGrupoBL.Guardar(info);
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
        public void Guardar(IEnumerable<UsuarioGrupoInfo> listaUsuarioGrupo)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                usuarioGrupoBL.Guardar(listaUsuarioGrupo);
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
        public ResultadoInfo<UsuarioGrupoInfo> ObtenerPorPagina(PaginacionInfo pagina, UsuarioGrupoInfo filtro)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                ResultadoInfo<UsuarioGrupoInfo> result = usuarioGrupoBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<UsuarioGrupoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                IList<UsuarioGrupoInfo> result = usuarioGrupoBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<UsuarioGrupoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                IList<UsuarioGrupoInfo> result = usuarioGrupoBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="usuarioGrupoID"></param>
        /// <returns></returns>
        public UsuarioGrupoInfo ObtenerPorID(int usuarioGrupoID)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                UsuarioGrupoInfo result = usuarioGrupoBL.ObtenerPorID(usuarioGrupoID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public UsuarioGrupoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                UsuarioGrupoInfo result = usuarioGrupoBL.ObtenerPorDescripcion(descripcion);
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
        public IList<UsuarioGrupoInfo> ObtenerPorUsuarioID(int usuarioId)
        {
            try
            {
                Logger.Info();
                var usuarioGrupoBL = new UsuarioGrupoBL();
                IList<UsuarioGrupoInfo> result = usuarioGrupoBL.ObtenerPorUsuarioID(usuarioId);
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

