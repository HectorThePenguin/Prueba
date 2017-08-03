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
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class SupervisionDetectoresDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de SupervisionDetectores
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(SupervisionDetectoresInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSupervisionDetectoresDAL.ObtenerParametrosCrear(info);
                int result = Create("SupervisionDetectores_Crear", parameters);
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
        /// Metodo para actualizar un registro de SupervisionDetectores
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(SupervisionDetectoresInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSupervisionDetectoresDAL.ObtenerParametrosActualizar(info);
                Update("SupervisionDetectores_Actualizar", parameters);
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
        /// Obtiene una lista de SupervisionDetectores
        /// </summary>
        /// <returns></returns>
        internal IList<SupervisionDetectoresInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("SupervisionDetectores_ObtenerTodos");
                IList<SupervisionDetectoresInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSupervisionDetectoresDAL.ObtenerTodos(ds);
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
        internal IList<SupervisionDetectoresInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSupervisionDetectoresDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("SupervisionDetectores_ObtenerTodos", parameters);
                IList<SupervisionDetectoresInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSupervisionDetectoresDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de SupervisionDetectores
        /// </summary>
        /// <param name="supervisionDetectoresID">Identificador de la SupervisionDetectores</param>
        /// <returns></returns>
        internal SupervisionDetectoresInfo ObtenerPorID(int supervisionDetectoresID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSupervisionDetectoresDAL.ObtenerParametrosPorID(supervisionDetectoresID);
                DataSet ds = Retrieve("SupervisionDetectores_ObtenerPorID", parameters);
                SupervisionDetectoresInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSupervisionDetectoresDAL.ObtenerPorID(ds);
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
        /// Obtiene la impresion de la Supervision de Detectores
        /// </summary>
        /// <param name="filtro">filtros de la pantalla</param>
        /// <returns></returns>
        internal List<ImpresionSupervisionDetectoresModel> ObtenerSupervisionDetectoresImpresion(FiltroImpresionSupervisionDetectores filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSupervisionDetectoresDAL.ObtenerParametrosSupervisionDetectoresImpresion(filtro);
                DataSet ds = Retrieve("SupervisionDetectores_ObtenerImpresion", parameters);
                List<ImpresionSupervisionDetectoresModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSupervisionDetectoresDAL.ObtenerSupervisionDetectoresImpresion(ds);
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

