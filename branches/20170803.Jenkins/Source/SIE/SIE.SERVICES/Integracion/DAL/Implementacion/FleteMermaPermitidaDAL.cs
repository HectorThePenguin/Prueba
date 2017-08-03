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
    internal class FleteMermaPermitidaDAL : DALBase
    {
        /// <summary>
        /// Obtiene diferencias de inventario
        /// </summary>
        /// <returns></returns>
        internal FleteMermaPermitidaInfo ObtenerConfiguracion(FleteMermaPermitidaInfo fleteMermaPermitidaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxFleteMermaPermitidaDAL.ObtenerParametrosObtenerConfiguracion(fleteMermaPermitidaInfo);
                var ds = Retrieve("FleteMermaPermitida_ObtenerConfiguracion", parameters);
                FleteMermaPermitidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteMermaPermitidaDAL.ObtenerConfiguracion(ds);
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
        /// Metodo para Crear un registro de FleteMermaPermitida
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(FleteMermaPermitidaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteMermaPermitidaDAL.ObtenerParametrosCrear(info);
                int result = Create("FleteMermaPermitida_Crear", parameters);
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
        /// Metodo para actualizar un registro de FleteMermaPermitida
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(FleteMermaPermitidaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteMermaPermitidaDAL.ObtenerParametrosActualizar(info);
                Update("FleteMermaPermitida_Actualizar", parameters);
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
        internal ResultadoInfo<FleteMermaPermitidaInfo> ObtenerPorPagina(PaginacionInfo pagina, FleteMermaPermitidaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxFleteMermaPermitidaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("FleteMermaPermitida_ObtenerPorPagina", parameters);
                ResultadoInfo<FleteMermaPermitidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteMermaPermitidaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de FleteMermaPermitida
        /// </summary>
        /// <returns></returns>
        internal IList<FleteMermaPermitidaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("FleteMermaPermitida_ObtenerTodos");
                IList<FleteMermaPermitidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteMermaPermitidaDAL.ObtenerTodos(ds);
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
        internal IList<FleteMermaPermitidaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteMermaPermitidaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("FleteMermaPermitida_ObtenerTodos", parameters);
                IList<FleteMermaPermitidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteMermaPermitidaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de FleteMermaPermitida
        /// </summary>
        /// <param name="fleteMermaPermitidaID">Identificador de la FleteMermaPermitida</param>
        /// <returns></returns>
        internal FleteMermaPermitidaInfo ObtenerPorID(int fleteMermaPermitidaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteMermaPermitidaDAL.ObtenerParametrosPorID(fleteMermaPermitidaID);
                DataSet ds = Retrieve("FleteMermaPermitida_ObtenerPorID", parameters);
                FleteMermaPermitidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteMermaPermitidaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de FleteMermaPermitida
        /// </summary>
        /// <param name="fleteMermaPermitida">Descripción de la FleteMermaPermitida</param>
        /// <returns></returns>
        internal FleteMermaPermitidaInfo ObtenerPorDescripcion(FleteMermaPermitidaInfo fleteMermaPermitida)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxFleteMermaPermitidaDAL.ObtenerParametrosPorDescripcion(fleteMermaPermitida);
                DataSet ds = Retrieve("FleteMermaPermitida_ObtenerPorDescripcion", parameters);
                FleteMermaPermitidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteMermaPermitidaDAL.ObtenerPorDescripcion(ds);
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
