using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class InterfaceSalidaTraspasoCostoDAL : DALBase
    {
        /// <summary>
        /// Genera los costos del ganado transferido
        /// </summary>
        /// <param name="costosGanadoTransferido"></param>
        internal void Crear(List<InterfaceSalidaTraspasoCostoInfo> costosGanadoTransferido)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxInterfaceSalidaTraspasoCostoDAL.ObtenerParametrosCrear(costosGanadoTransferido);
                Create("InterfaceSalidaTraspasoCosto_CrearXML", parameters);
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
        /// Obtiene los costos de los animales enviados
        /// a sacrificio
        /// </summary>
        /// <param name="interfaceSalidaTraspasoDetalle"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoCostoInfo> ObtenerCostosInterfacePorDetalle(List<InterfaceSalidaTraspasoDetalleInfo> interfaceSalidaTraspasoDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxInterfaceSalidaTraspasoCostoDAL.ObtenerParametrosCostosInterfacePorDetalle(
                        interfaceSalidaTraspasoDetalle);
                DataSet ds = Retrieve("InterfaceSalidaTraspasoCosto_ObtenerCostosPorInterfaceDetalle", parameters);
                List<InterfaceSalidaTraspasoCostoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoCostoDAL.ObtenerCostosInterfacePorDetalle(ds);
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
        /// Actualiza bandera de facturado
        /// </summary>
        /// <param name="animalesAFacturar"></param>
        internal void ActualizarFacturado(List<long> animalesAFacturar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxInterfaceSalidaTraspasoCostoDAL.ObtenerParametrosActualizarFacturado(animalesAFacturar);
                Create("InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML", parameters);
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
