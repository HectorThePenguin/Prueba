using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
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
    internal class ContratoDetalleDAL : DALBase
    {
        /// <summary>
        /// Metodo para obtener contrato detalle por contratoid
        /// </summary>
        /// <returns></returns>
        internal List<ContratoDetalleInfo> ObtenerPorContratoId(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxContratoDetalleDAL.ObtenerParametrosObtenerPorContratoId(contratoInfo);
                var ds = Retrieve("ContratoDetalle_ObtenerPorContratoID", parameters);
                List<ContratoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapContratoDetalleDAL.ObtenerPorContratoId(ds);
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
        /// Crea un contrato detalle
        /// </summary>
        /// <returns></returns>
        internal int Crear(List<ContratoDetalleInfo> listaContratoDetalleInfo, ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxContratoDetalleDAL.ObtenerParametrosCrearContratoDetalle(listaContratoDetalleInfo, contratoInfo);
                int result = Create("ContratoDetalle_Crear", parameters);
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
        /// Actualiza el estado de un contrato detalle
        /// </summary>
        /// <param name="contratoDetalleInfo"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstado(ContratoDetalleInfo contratoDetalleInfo, EstatusEnum estatus)
        {
            try
            {
                Dictionary<string, object> parameters = AuxContratoDetalleDAL.ObtenerParametrosActualizarEstado(contratoDetalleInfo, estatus);
                Update("ContratoDetalle_ActualizarEstado", parameters);
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
