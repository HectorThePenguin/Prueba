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
    internal class CostoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Costo_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapCostoDAL.ObtenerPorPagina(ds);
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
            return costoLista;
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoInfo> ObtenerPorPaginaSinId(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorPaginaSinId(pagina, filtro);
                DataSet ds = Retrieve("Costo_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapCostoDAL.ObtenerPorPagina(ds);
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
            return costoLista;
        }

        /// <summary>
        ///     Metodo que actualiza un Costo
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CostoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[Costo_Actualizar]", parameters);
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
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorID(int infoId)
        {
            CostoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametroPorID(infoId);
                DataSet ds = Retrieve("[dbo].[Costo_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerPorID(ds);
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
        ///     Metodo que crear un Costo
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(CostoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosGuardado(info);
                infoId = Create("[dbo].[Costo_Crear]", parameters);
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

            return infoId;
        }

        /// <summary>
        ///  Obtiene una lista de Costos filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerTodos(estatus);
                using (IDataReader reader = RetrieveReader("Costo_ObtenerTodos", parameters))
                {
                    IList<CostoInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapCostoDAL.ObtenerTodos(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoInfo> ObtenerPorPaginaTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorPaginaTipoCosto(pagina, filtro);
                DataSet ds = Retrieve("Costo_ObtenerPorPaginaTipoCosto", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapCostoDAL.ObtenerPorPaginaTipoCosto(ds);
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
            return costoLista;
        }

        /// <summary>
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorClaveContableTipoCosto(CostoInfo filtro)
        {
            CostoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametroPorClaveContableTipoCosto(filtro);
                DataSet ds = Retrieve("[dbo].[Costo_ObtenerPorClaveContableTipoCosto]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerPorClaveContableTipoCosto(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoInfo> ObtenerPorPaginaClaveContable(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorPaginaClaveContable(pagina, filtro);
                DataSet ds = Retrieve("Costo_ObtenerPorPaginaClaveContable", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapCostoDAL.ObtenerPorPaginaClaveContable(ds);
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
            return costoLista;
        }

        /// <summary>
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorClaveContable(CostoInfo filtro)
        {
            CostoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametroPorClaveContable(filtro);
                DataSet ds = Retrieve("[dbo].[Costo_ObtenerPorClaveContable]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Costo
        /// </summary>
        /// <param name="descripcion">Descripción de la Costo</param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Costo_ObtenerPorDescripcion", parameters);
                CostoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerPorDescripcion(ds);
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

        public ResultadoInfo<CostoInfo> ObtenerPorPaginaIDTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorPaginaIDTipoCosto(pagina, filtro);
                DataSet ds = Retrieve("Costo_ObtenerPorPaginaFiltroTipoCosto", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapCostoDAL.ObtenerPorPaginaTipoCosto(ds);
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
            return costoLista;
        }
        /// <summary>
        /// Obtiene el flete por id
        /// </summary>
        /// <param name="idFlete"></param>
        /// <returns></returns>
        public List<FleteDetalleInfo> ObtenerPorFleteID(int idFlete)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorFleteID(idFlete);
                DataSet ds = Retrieve("ProgramacionFletes_ObtenerFletesDetallePorFleteID", parameters);
                List<FleteDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerPorFleteID(ds);
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
        /// Obtiene el costo por id
        /// </summary>
        /// <param name="costo"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerCostoPorID(CostoInfo costo)
        {
            CostoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametroCostoPorID(costo);
                DataSet ds = Retrieve("[dbo].[Costo_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerCostoPorID(ds);
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
        /// Obtiene la informacion de costos por tipos de gasto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoInfo> ObtenerPorPaginaCostoPorTiposGasto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametrosPorPaginaCostoPorTiposGasto(pagina, filtro);
                DataSet ds = Retrieve("Costo_ObtenerPorPaginaCostoPorTiposGasto", parameters);
                if (ValidateDataSet(ds))
                {
                    costoLista = MapCostoDAL.ObtenerPorPagina(ds);
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
            return costoLista;
        }

        /// <summary>
        /// Obtiene la informacion del centro de costo sap por costo id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CentroCostoInfo ObtenerCentroCostoSAPPorCosto(CostoInfo filtro)
        {
            CentroCostoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoDAL.ObtenerParametroCentroCostoSAPPorCosto(filtro);
                DataSet ds = Retrieve("[dbo].[Costo_ObtenerCentroCostoSAPPorCosto]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoDAL.ObtenerCentroCostoSAPPorCosto(ds);
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
    }
}
