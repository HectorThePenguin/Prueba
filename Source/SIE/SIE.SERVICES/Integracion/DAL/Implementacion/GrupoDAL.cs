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
    internal class GrupoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<GrupoInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoInfo filtro)
        {
            ResultadoInfo<GrupoInfo> grupoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxGrupoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Grupo_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    grupoLista = MapGrupoDAL.ObtenerPorPagina(ds);
                }
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
            return grupoLista;
        }

        /// <summary>
        ///     Metodo que crear un grupo
        /// </summary>
        /// <param name="grupoInfo"></param>
        internal int Guardar(GrupoInfo grupoInfo)
        {
            try
            {                
                Logger.Info();
                Dictionary<string,object> parameters = AuxGrupoDAL.ObtenerParametrosCrear(grupoInfo);
                return Create("Grupo_Crear", parameters);
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
        ///     Obtiene un grupo por Id
        /// </summary>
        /// <param name="grupoID"></param>
        /// <returns></returns>
        internal GrupoInfo ObtenerPorID(int grupoID)
        {
            GrupoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoDAL.ObtenerParametroPorID(grupoID);
                DataSet ds = Retrieve("Grupo_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoDAL.ObtenerPorID(ds);
                }
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
            return result;
        }

        /// <summary>
        ///     Metodo que actualiza un grupo
        /// </summary>
        /// <param name="grupoInfo"></param>
        internal void Actualizar(GrupoInfo grupoInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxGrupoDAL.ObtenerParametrosActualizar(grupoInfo);
                Update("Grupo_Actualizar", parameters);
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
        ///     Metodo que inactiva un grupo
        /// </summary>
        /// <param name="grupoID"></param>
        internal void BorrarGrupo(int grupoID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxGrupoDAL.ObtenerParametroPorID(grupoID);
                Delete("Grupo_Borrar", parameters);
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
        /// Obtiene un registro de Grupo
        /// </summary>
        /// <param name="descripcion">Descripción de la Grupo</param>
        /// <returns></returns>
        internal GrupoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Grupo_ObtenerPorDescripcion", parameters);
                GrupoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un lista de grupos.
        /// </summary>
        /// <returns></returns>
        internal IList<GrupoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Grupo_ObtenerTodos");
                IList<GrupoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoDAL.ObtenerTodos(ds);
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
        /// Obtiene un lista de grupos.
        /// </summary>
        /// <returns></returns>
        internal IList<GrupoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Grupo_ObtenerTodos", parameters);
                IList<GrupoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoDAL.ObtenerTodos(ds);
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

    }
}