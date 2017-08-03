using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CostoEmbarqueDetalleDAL : DALBase
    {
        /// <summary>
        ///     Metodo que actualiza un Costo Embarque
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CostoEmbarqueDetalleInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[CostoEmbarqueDetalle_Actualizar]", parameters);
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
        ///     Obtiene un CostoEmbarqueInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal CostoEmbarqueDetalleInfo ObtenerPorID(int infoId)
        {
            CostoEmbarqueDetalleInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametroPorID(infoId);
                DataSet ds = Retrieve("[dbo].[CostoEmbarqueDetalle_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerPorID(ds);
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
        ///     Obtiene un CostoEmbarqueInfo por Id
        /// </summary>
        /// <param name="embarqueDetalleId"></param>
        /// <returns></returns>
        internal List<CostoEmbarqueDetalleInfo> ObtenerPorEmbarqueDetalleID(int embarqueDetalleId)
        {
            List<CostoEmbarqueDetalleInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametroPorEmbarqueDetalleID(embarqueDetalleId);
                DataSet ds = Retrieve("[dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueDetalleID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerPorEmbarqueDetalleID(ds);
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
        ///     Metodo que crear un Costo Embarque Detalle
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(CostoEmbarqueDetalleInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametrosGuardado(info);
                infoId = Create("[dbo].[CostoEmbarqueDetalle_Crear]", parameters);
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
        ///     Obtiene la Lista de Costos del Embarque
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueIDOrganizacionOrigen(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametroPorEmbarqueIDOrganizacionOrigen(entradaGanado);
                DataSet ds = Retrieve("[dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueIDOrganizacionOrigen]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerPorEmbarqueIDOrganizacionOrigen(ds);
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
        ///     Obtiene la Lista de Costos del Embarque
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametroPorEmbarqueID(entradaGanado);
                DataSet ds = Retrieve("[dbo].[CostoEmbarqueDetalle_ObtenerPorCostoPorEmbarque]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerPorEmbarqueID(ds);
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
        /// Valdiar si existen Compras directas en el ruteo
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        public List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametroPorEmbarqueID(entradaGanado);
                DataSet ds = Retrieve("[dbo].[CostoEmbarqueDetalle_ObtenerEntradasPorEmbarqueID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerEntradasPorEmbarqueID(ds);
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
        /// Obtiene los costos registrados por origen, destino y proveedor
        /// </summary>
        /// <param name="embarqueDetalleInfo"></param>
        /// <returns></returns>
        internal List<CostoInfo> ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID(EmbarqueInfo embarqueInfo)
        {
            List<CostoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerParametroPorProveedorIDOrigenIDDestinoIDCostoID(embarqueInfo);
                DataSet ds = Retrieve("CostoEmbarqueDetalle_ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID(ds);
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
        /// Obtiene el detalle de los embarques con la misma escala, proveedor, origen, destino y tipo de embarque
        /// </summary>
        /// <param name="embarqueInfo"></param>
        /// <returns></returns>
        internal List<EmbarqueDetalleInfo> ObtenerEmbarqueDetallesCostosPorProveedorIDOrigenIDDestinoIDCostoID(EmbarqueInfo embarqueInfo)
        {
            List<EmbarqueDetalleInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCostoEmbarqueDetalleDAL.ObtenerEmbarqueDetallesCostosPorProveedorIDOrigenIDDestinoIDCostoID(embarqueInfo);
                DataSet ds = Retrieve("EmbarqueDetalle_Costos_ObtenerDetallesYCostosPorProveedorIDOrigenIDDestinoIDCostoID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapCostoEmbarqueDetalleDAL.ObtenerEmbarqueDetallesCostosPorProveedorIDOrigenIDDestinoIDCostoID(ds);
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
