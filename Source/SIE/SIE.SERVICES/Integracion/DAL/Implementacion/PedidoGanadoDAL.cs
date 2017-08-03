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
    internal class PedidoGanadoDAL : DALBase
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal PedidoGanadoInfo GuardarPedidoGanado(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoGanadoDAL.ObtenerParametrosCrearPedidoGanado(pedidoGanadoInfo);
                var ds = Retrieve("PedidoGanado_Guardar", parameters);
                PedidoGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPedidoGanadoDAL.ObtenerPedidoGanado(ds);
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
        /// Metodo para obtener pedido ganado semanal
        /// </summary>
        internal PedidoGanadoInfo ObtenerPedidoGanadoSemanal(PedidoGanadoInfo pedidoGanadoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoGanadoDAL.ObtenerParametrosObtenerPedidoGanadoSemanal(pedidoGanadoInfo);
                var ds = Retrieve("PedidoGanado_ObtenerPorSemana", parameters);
                PedidoGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPedidoGanadoDAL.ObtenerPedidoGanado(ds);
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
        /// Obtiene los pedidos de ganado espejo por PedidoGanadoID
        /// </summary>
        /// <returns></returns>
        internal List<PedidoGanadoEspejoInfo> ObtenerPedidoGanadoEspejoPorPedidoGanadoID(PedidoGanadoInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoGanadoDAL.ObtenerParametrosObtenerPedidoGanadoEspejo(corralInfo);
                var ds = Retrieve("PedidoGanadoEspejo_ObtenerPorPedidoGanadoIDFiltro", parameters);
                List<PedidoGanadoEspejoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPedidoGanadoDAL.ObtenerPedidoGanadoEspejoPorPedidoGanadoID(ds);
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
        /// Metrodo Para Guardar en en la tabla PedidoGanadoEspejo
        /// </summary>
        internal PedidoGanadoEspejoInfo GuardarPedidoGanadoEspejo(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoGanadoDAL.ObtenerParametrosCrearPedidoGanadoEspejo(pedidoGanadoEspejoInfo);
                var ds = Retrieve("PedidoGanadoEspejo_Guardar", parameters);
                PedidoGanadoEspejoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPedidoGanadoDAL.ObtenerPedidoGanadoEspejo(ds);
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
        /// Metodo Para actuaualizar el estatus PedidoGanadoEspejo
        /// </summary>
        internal void ActualizarPedidoGanadoEspejoEstatus(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoGanadoDAL.ObtenerParametrosActualizarPedidoGanadoEspejoEstatus(pedidoGanadoEspejoInfo);
                Update("PedidoGanadoEspejo_ActualizarEstatus", parameters);
               
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
        /// Metodo Para actualizar la tabla PedidoGanado con PedidoGanadoEspejo
        /// </summary>
        internal void ActualizarPedidoGanado(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPedidoGanadoDAL.ObtenerParametrosActualizarPedidoGanado(pedidoGanadoEspejoInfo);
                Update("PedidoGanado_Actualizar", parameters);

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
