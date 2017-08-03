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
    internal class GrupoFormularioDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de GrupoFormulario
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(GrupoFormularioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerParametrosCrear(info);
                int result = Create("GrupoFormulario_Crear", parameters);
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
        /// Metodo para actualizar un registro de GrupoFormulario
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(GrupoFormularioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerParametrosActualizar(info);
                Update("GrupoFormulario_Actualizar", parameters);
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
        internal ResultadoInfo<GrupoFormularioInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoFormularioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("GrupoFormulario_ObtenerPorPagina", parameters);
                ResultadoInfo<GrupoFormularioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoFormularioDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de GrupoFormulario
        /// </summary>
        /// <returns></returns>
        internal IList<GrupoFormularioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("GrupoFormulario_ObtenerTodos");
                IList<GrupoFormularioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoFormularioDAL.ObtenerTodos(ds);
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
        internal IList<GrupoFormularioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("GrupoFormulario_ObtenerTodos", parameters);
                IList<GrupoFormularioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoFormularioDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de GrupoFormulario
        /// </summary>
        /// <param name="grupoFormularioID">Identificador de la GrupoFormulario</param>
        /// <returns></returns>
        internal GrupoFormularioInfo ObtenerPorID(int grupoFormularioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerParametrosPorID(grupoFormularioID);
                DataSet ds = Retrieve("GrupoFormulario_ObtenerPorID", parameters);
                GrupoFormularioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoFormularioDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de GrupoFormulario
        /// </summary>
        /// <param name="descripcion">Descripción de la GrupoFormulario</param>
        /// <returns></returns>
        internal GrupoFormularioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("GrupoFormulario_ObtenerPorDescripcion", parameters);
                GrupoFormularioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoFormularioDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una entidad por su Grupo Id
        /// </summary>
        /// <param name="grupoID"></param>
        /// <returns></returns>
        internal GrupoFormularioInfo ObtenerPorGrupoID(int grupoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGrupoFormularioDAL.ObtenerParametrosPorGrupoID(grupoID);
                DataSet ds = Retrieve("GrupoFormulario_ObtenerPorGrupoID", parameters);
                GrupoFormularioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGrupoFormularioDAL.ObtenerPorGrupoID(ds);
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

