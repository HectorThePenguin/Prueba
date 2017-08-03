using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class SolicitudAutorizacionPL
    {
        /// Obtiene los datos de la solicitud de autorizacion
        public SolicitudAutorizacionInfo ObtenerDatosSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                solicitudInfo = solicitudBL.ObtenerDatosSolicitudAutorizacion(solicitudInfo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return solicitudInfo;
        }

        /// Valida si el precio venta menor al costo capturado ya fue rechazado para el folio salida
        public int ConsultarPrecioRechazadoSolicitud(int folioSalida, decimal precioVenta, int organizacionID)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                result = solicitudBL.ConsultarPrecioRechazadoSolicitud(folioSalida, precioVenta, organizacionID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        ///  Se genera la solicitud de autorizacion
        public int GenerarSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                int result = solicitudBL.GenerarSolicitudAutorizacion(solicitudInfo);

                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizacionProgramacionMP(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                AutorizacionMateriaPrimaInfo result = solicitudBL.ObtenerDatosSolicitudAutorizacionProgramacionMP(autorizacionInfo);

                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Se obtiene las lista de solicitudes de tipo precio de venta
        public List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesPrecioVenta(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                listaSolicitudesPendientes = solicitudBL.ObtenerSolicitudesPendientesPrecioVenta(organizacionID, tipoAutorizacionID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaSolicitudesPendientes;
        }

        public AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizada(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                AutorizacionMateriaPrimaInfo result = solicitudBL.ObtenerDatosSolicitudAutorizada(autorizacionInfo);

                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Se obtiene las lista de solicitudes de tipo uso de lote
        public List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesUsoLote(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                listaSolicitudesPendientes = solicitudBL.ObtenerSolicitudesPendientesUsoLote(organizacionID, tipoAutorizacionID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaSolicitudesPendientes;
        }

        /// Se obtiene las lista de solicitudes de tipo ajuste de inventario
        public List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesAjusteInventario(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                listaSolicitudesPendientes = solicitudBL.ObtenerSolicitudesPendientesAjusteInventario(organizacionID, tipoAutorizacionID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaSolicitudesPendientes;
        }
        /// <summary>
        /// Se guardan las respuesta a las solicitudes de materia prima
        /// </summary>
        /// <param name="respuestaSolicitudes"></param>
        /// <returns></returns>
        public ResultadoValidacion GuardarRespuestasSolicitudes(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID, int tipoAutorizacionID, int usuarioID)
        {
            var result = new ResultadoValidacion();
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                result = solicitudBL.GuardarRespuestasSolicitudes(respuestaSolicitudes, organizacionID, tipoAutorizacionID, usuarioID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
        /// <summary>
        /// Se obtienen los datos de la solicitud de autorización para el folio
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        public AutorizacionMateriaPrimaInfo ObtenerSolicitudAutorizacion(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var solicitudBL = new SolicitudAutorizacionBL();
                AutorizacionMateriaPrimaInfo result = solicitudBL.ObtenerSolicitudAutorizacion(autorizacionInfo);

                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
