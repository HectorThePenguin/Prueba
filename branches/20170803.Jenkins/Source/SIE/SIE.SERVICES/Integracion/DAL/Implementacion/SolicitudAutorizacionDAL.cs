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
    internal class SolicitudAutorizacionDAL : DALBase
    {
        /// Obtiene los datos de la solicitud de autorizacion
        internal SolicitudAutorizacionInfo ObtenerDatosSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerDatosSolicitudAutorizacion(solicitudInfo);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerDatosSolicitud", parameters);
                if (ValidateDataSet(ds))
                {
                    solicitudInfo = MapSolicitudAutorizacionDAL.ObtenerDatosSolicitudAutorizacion(ds);
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
            return solicitudInfo;
        }

        /// Valida si el precio venta menor al costo capturado ya fue rechazado para el folio salida
        internal int ConsultarPrecioRechazadoSolicitud(int folioSalida, decimal precioVenta, int organizacionID)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ConsultarPrecioRechazadoSolicitud(folioSalida, precioVenta, organizacionID);
                result = Create("AutorizacionMateriaPrima_ConsultarPrecioSolicitud", parameters);
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

        /// Se genera la solicitud de auorizacion para el precio venta menos capturado
        internal int GenerarSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.GenerarSolicitudAutorizacion(solicitudInfo);
                int result = Create("AutorizacionMateriaPrima_GenerarSolicitud", parameters);

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
        /// Obtener solicitud de autorizacion de materia prima
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        internal AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizacionProgramacionMP(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            AutorizacionMateriaPrimaInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerParametrosObtenerDatosSolicitudAutorizacionProgramacionMP(autorizacionInfo);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerPorFolioLoteTipoAutorizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapSolicitudAutorizacionDAL.ObtenerDatosSolicitudAutorizacionProgramacionMP(ds);
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
            return resultado;
        }

        /// Se obtiene las lista de solicitudes de tipo precio de venta
        internal List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesPrecioVenta(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerSolicitudesPendientes(organizacionID, tipoAutorizacionID);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerSolicitudesPrecioVentaPendientes", parameters);
                if (ValidateDataSet(ds))
                {
                    listaSolicitudesPendientes = MapSolicitudAutorizacionDAL.ObtenerSolicitudesPendientesPrecioVenta(ds);
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
            return listaSolicitudesPendientes;
        }
        /// <summary>
        /// Obtener solicitud por estatus
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        internal AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizada(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            AutorizacionMateriaPrimaInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerParametrosObtenerDatosSolicitudAutorizada(autorizacionInfo);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerPorFolioEstatusTipoAutorizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapSolicitudAutorizacionDAL.ObtenerDatosSolicitudAutorizacionProgramacionMP(ds);
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
            return resultado;
        }

        /// Se obtiene las lista de solicitudes de tipo uso de lote
        internal List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesUsoLote(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerSolicitudesPendientes(organizacionID, tipoAutorizacionID);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerSolicitudesUsoLotePendientes", parameters);
                if (ValidateDataSet(ds))
                {
                    listaSolicitudesPendientes = MapSolicitudAutorizacionDAL.ObtenerSolicitudesPendientesUsoLote(ds);
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
            return listaSolicitudesPendientes;
        }

        /// Se obtiene las lista de solicitudes de tipo ajuste de inventario
        internal List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesAjusteInventario(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerSolicitudesAjusteInventarioPendientes(organizacionID, tipoAutorizacionID);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerSolicitudesAjusteInventarioPendientes", parameters);
                if (ValidateDataSet(ds))
                {
                    listaSolicitudesPendientes = MapSolicitudAutorizacionDAL.ObtenerSolicitudesPendientesAjusteInventario(ds);
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
            return listaSolicitudesPendientes;
        }

        /// Se guardan las respuesta a las solicitudes de materia prima
        internal bool GuardarRespuestasSolicitudes(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID, int tipoAutorizacionID, int usuarioID)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.GuardarRespuestasSolicitudes(respuestaSolicitudes, organizacionID, tipoAutorizacionID, usuarioID);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_GuardarAutorizacionMovimientos", parameters);
                result = true;
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
        /// Se guardan las respuesta a las solicitudes de materia prima
        internal List<DiferenciasDeInventariosInfo> GuardarAjusteInventario(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.GuardarAjusteInventario(respuestaSolicitudes, organizacionID);
                var ds = Retrieve("AlmacenMovimiento_GuardarAutorizacionMovimientos", parameters);
                List<DiferenciasDeInventariosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudAutorizacionDAL.ObtenerDiferenciasInventario(ds);
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

        /// Se guardan las respuesta a las solicitudes de materia prima
        internal List<DiferenciasDeInventariosInfo> ObtenerMovimientosAutorizacion(List<AutorizacionMovimientosInfo> respuestaSolicitudes)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerParametrosMovimientosAutorizacion(respuestaSolicitudes);
                var ds = Retrieve("AlmacenMovimiento_ObtenerAutorizacionMovimientos", parameters);
                List<DiferenciasDeInventariosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudAutorizacionDAL.ObtenerMovimientosAutorizacion(ds);
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
        /// Se obtienen los datos de la solicitud de autorización para el folio
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        internal AutorizacionMateriaPrimaInfo ObtenerSolicitudAutorizacion(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            AutorizacionMateriaPrimaInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudAutorizacionDAL.ObtenerParametrosObtenerSolicitudAutorizacion(autorizacionInfo);
                DataSet ds = Retrieve("AutorizacionMateriaPrima_ObtenerPorFolioTipoAutorizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapSolicitudAutorizacionDAL.ObtenerDatosSolicitudAutorizacionProgramacionMP(ds);
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
            return resultado;
        }
    }
}
