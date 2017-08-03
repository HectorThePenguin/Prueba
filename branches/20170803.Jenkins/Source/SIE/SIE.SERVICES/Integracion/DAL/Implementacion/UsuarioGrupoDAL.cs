using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class UsuarioGrupoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de UsuarioGrupo
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(UsuarioGrupoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosCrear(info);
                int result = Create("UsuarioGrupo_Crear", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para actualizar un registro de UsuarioGrupo
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(UsuarioGrupoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosActualizar(info);
                Update("UsuarioGrupo_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("UsuarioGrupo_ObtenerPorPagina", parameters);
                ResultadoInfo<UsuarioGrupoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioGrupoDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex); 
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de UsuarioGrupo
        /// </summary>
        /// <returns></returns>
        internal IList<UsuarioGrupoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("UsuarioGrupo_ObtenerTodos");
                IList<UsuarioGrupoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioGrupoDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("UsuarioGrupo_ObtenerTodos", parameters);
                IList<UsuarioGrupoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioGrupoDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de UsuarioGrupo
        /// </summary>
        /// <param name="usuarioGrupoID">Identificador de la UsuarioGrupo</param>
        /// <returns></returns>
        internal UsuarioGrupoInfo ObtenerPorID(int usuarioGrupoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosPorID(usuarioGrupoID);
                DataSet ds = Retrieve("UsuarioGrupo_ObtenerPorID", parameters);
                UsuarioGrupoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioGrupoDAL.ObtenerPorID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de UsuarioGrupo
        /// </summary>
        /// <param name="descripcion">Descripción de la UsuarioGrupo</param>
        /// <returns></returns>
        internal UsuarioGrupoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("UsuarioGrupo_ObtenerPorDescripcion", parameters);
                UsuarioGrupoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioGrupoDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosPorUsuarioID(usuarioId);
                DataSet ds = Retrieve("UsuarioGrupo_ObtenerPorUsuarioID", parameters);
                IList<UsuarioGrupoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioGrupoDAL.ObtenerPorUsuarioID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxUsuarioGrupoDAL.ObtenerParametrosGuardar(listaUsuarioGrupo);
                Create("UsuarioGrupo_Guardar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

