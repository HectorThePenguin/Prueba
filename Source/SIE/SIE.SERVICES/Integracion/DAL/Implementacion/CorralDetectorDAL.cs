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
    internal class CorralDetectorDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CorralDetector
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        /// <param name="corralesMarcados"></param>
        internal int Crear(CorralDetectorInfo info, List<CorralInfo> corralesMarcados)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerParametrosCrear(info, corralesMarcados);
                int result = Create("CorralDetector_Crear", parameters);
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
        /// Metodo para actualizar un registro de CorralDetector
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CorralDetectorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerParametrosActualizar(info);
                Update("CorralDetector_Actualizar", parameters);
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
        internal ResultadoInfo<OperadorInfo> ObtenerPorPagina(PaginacionInfo pagina, CorralDetectorInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CorralDetector_ObtenerPorPagina", parameters);
                ResultadoInfo<OperadorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOperadorDAL.ObtenerPorPagina(ds);
                    //result = MapCorralDetectorDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CorralDetector
        /// </summary>
        /// <returns></returns>
        internal IList<CorralDetectorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CorralDetector_ObtenerTodos");
                IList<CorralDetectorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDetectorDAL.ObtenerTodos(ds);
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
        internal IList<CorralDetectorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CorralDetector_ObtenerTodos", parameters);
                IList<CorralDetectorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDetectorDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el detector
        /// </summary>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerTodosPorDetector(int operadorID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerTodosPorDetector(operadorID);
                DataSet ds = Retrieve("CorralDetector_ObtenerTodosPorDetector", parameters);
                CorralDetectorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDetectorDAL.ObtenerTodosPorDetector(ds);
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
        /// Obtiene un registro de CorralDetector
        /// </summary>
        /// <param name="corralDetectorID">Identificador de la CorralDetector</param>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerPorID(int corralDetectorID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerParametrosPorID(corralDetectorID);
                DataSet ds = Retrieve("CorralDetector_ObtenerPorID", parameters);
                CorralDetectorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDetectorDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CorralDetector
        /// </summary>
        /// <param name="descripcion">Descripción de la CorralDetector</param>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CorralDetector_ObtenerPorDescripcion", parameters);
                CorralDetectorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDetectorDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de CorralDetector
        /// </summary>
        /// <param name="operadorID">Clave del Operador</param>
        /// <param name="corralID">Clave del COrral</param>
        /// <returns></returns>
        internal CorralDetectorInfo ObtenerPorOperadorCorral(int operadorID, int corralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDetectorDAL.ObtenerParametrosPorOperadorCorral(operadorID, corralID);
                DataSet ds = Retrieve("CorralDetector_ObtenerPorOperadorCorral", parameters);
                CorralDetectorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDetectorDAL.ObtenerPorOperadorCorral(ds);
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

