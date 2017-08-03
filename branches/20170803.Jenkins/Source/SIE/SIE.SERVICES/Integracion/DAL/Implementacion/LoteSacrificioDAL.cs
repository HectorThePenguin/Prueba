using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class LoteSacrificioDAL :DALBase
    {
        /// <summary>
        /// Obtiene los datos del lote sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificio(DateTime fecha, int organizacionID)
        {
            LoteSacrificioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametroObtenerLoteSacrificio(fecha, organizacionID);
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDatosFolioSacrificioPorFecha", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteSacrificioDAL.ObtenerLoteSacrificio(ds);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;

        }

        /// <summary>
        /// Obtiene los datos del lote sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificioLucero(DateTime fecha, int organizacionID)
        {
            LoteSacrificioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametroObtenerLoteSacrificio(fecha, organizacionID);
                DataSet ds = Retrieve("LoteSacrificioLucero_ObtenerDatosFolioSacrificioPorFecha", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteSacrificioDAL.ObtenerLoteSacrificio(ds);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Actualiza el lote sacrificio seleccionado
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        internal void ActualizarLoteSacrificio(LoteSacrificioInfo loteSacrificioInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametroActualizarLoteSacrificio(loteSacrificioInfo);
                Update("LoteSacrificio_ActualizarFacturacion", parameters);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Actualiza el lote sacrificio seleccionado
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        /// <param name="foliosTraspaso"> </param>
        internal void ActualizarLoteSacrificioLucero(LoteSacrificioInfo loteSacrificioInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteSacrificioDAL.ObtenerParametroActualizarLoteSacrificioLucero(loteSacrificioInfo);
                Update("LoteSacrificioLucero_ActualizarFacturacion", parameters);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el lote sacrificio a cancelar la factura
        /// </summary>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificioACancelar(int organizacionID)
        {
            LoteSacrificioInfo result = null;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> {{"@OrganizacionID", organizacionID}};
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDatosFacturasACancelar", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteSacrificioDAL.ObtenerLoteSacrificioACancelar(ds);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }
        /// <summary>
        /// Obtiene las facturas generadas a los lotes de sacrificio
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelar(int ordenSacrificioId)
        {
            List<LoteSacrificioInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametroObtenerFacturasPorOrdenSacrificioACancelar(ordenSacrificioId);
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDetalleFacturasACancelar", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteSacrificioDAL.ObtenerFacturasPorOrdenSacrificioACancelar(ds);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }
        /// <summary>
        /// Obtiene los datos de la factura por orden de sacrificio
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        /// <returns></returns>
        internal List<FacturaInfo> ObtenerDatosFacturaPorOrdenSacrificio(LoteSacrificioInfo loteSacrificioInfo)
        {
            List<FacturaInfo> factura = null;
            try
            {
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametrosObtenerDatosFacturaPorOrdenSacrificio(loteSacrificioInfo);
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificio", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosFacturaPorOrdenSacrificio(ds);
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
            return factura;
        }

        /// <summary>
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosFacturaSacrificio(int ordenSacrificioId)
        {
            List<PolizaSacrificioModel> factura = null;
            try
            {
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametrosDatosPolizaSacrificio(ordenSacrificioId);
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDatosPoliza", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosPolizaSacrificio(ds);
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
            return factura;
        }

        /// <summary>
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosFacturaSacrificioLucero(int organizacionID, DateTime fecha, List<PolizaSacrificioModel> foliosLoteSacrificio)
        {
            List<PolizaSacrificioModel> factura = null;
            try
            {
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametrosDatosPolizaSacrificioLucero(organizacionID, fecha, foliosLoteSacrificio);
                DataSet ds = Retrieve("LoteSacrificioLucero_ObtenerDatosPoliza", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosPolizaSacrificioLucero(ds);
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
            return factura;
        }

        /// <summary>
        /// Obtiene una lista con
        /// los sacrificios generados
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal IEnumerable<PolizaSacrificioModel> ObtenerPolizasSacrificioConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                IMapBuilderContext<PolizaSacrificioModel> mapeo = MapLoteSacrificioDAL.ObtenerMapeoPolizasSacrificioConciliacion();
                IEnumerable<PolizaSacrificioModel> almacenMovimientoCostoPorAlmacenMovimiento = GetDatabase().
                    ExecuteSprocAccessor
                    <PolizaSacrificioModel>(
                        "SalidaSacrificio_ObtenerPolizaConciliacion", mapeo.Build(),
                        new object[] { organizacionID, fechaInicial, fechaFinal });
                return almacenMovimientoCostoPorAlmacenMovimiento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosSacrificioLucero(int organizacionID, DateTime fecha)
        {
            List<PolizaSacrificioModel> factura = null;
            try
            {
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametrosDatosPolizaSacrificioLucero(organizacionID, fecha);
                DataSet ds = Retrieve("LoteSacrificioLucero_ObtenerDatosFacturacion", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosSacrificioLucero(ds);
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
            return factura;
        }

        internal List<FacturaInfo> ObtenerDatosFacturaPorOrdenSacrificioLucero(LoteSacrificioInfo loteSacrificioInfo, List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            List<FacturaInfo> factura = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxLoteSacrificioDAL.ObtenerParametrosObtenerDatosFacturaPorOrdenSacrificioLucero(
                        loteSacrificioInfo, lotesSacrificioFolios);
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDatosFacturaPorOrdenSacrificioLucero", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosFacturaPorOrdenSacrificio(ds);
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
            return factura;
        }

        /// <summary>
        /// Obtiene los datos en caso de que se sacrifico con anterioridad
        /// de ese lote de manera incompleta
        /// </summary>
        /// <param name="lotesSacrificioFolios"></param>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerSacrificiosAnteriores(List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            List<PolizaSacrificioModel> factura = null;
            try
            {
                Dictionary<string, object> parameters = AuxLoteSacrificioDAL.ObtenerParametrosSacrificiosLucero(lotesSacrificioFolios);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerCabezasSacrificadas", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosSacrificadosLucero(ds);
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
            return factura;
        }

        /// <summary>
        /// Obtiene el lote sacrificio a cancelar la factura
        /// </summary>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificioACancelarLucero(int organizacionID)
        {
            LoteSacrificioInfo result = null;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@OrganizacionID", organizacionID } };
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDatosFacturasACancelarLucero", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteSacrificioDAL.ObtenerLoteSacrificioACancelar(ds);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene las facturas generadas a los lotes de sacrificio
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelarLucero(int organizacionID)
        {
            List<LoteSacrificioInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteSacrificioDAL.ObtenerParametroObtenerFacturasPorOrdenSacrificioACancelarLucero(organizacionID);
                DataSet ds = Retrieve("LoteSacrificio_ObtenerDetalleFacturasACancelarLucero", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteSacrificioDAL.ObtenerFacturasPorOrdenSacrificioACancelar(ds);
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Actualiza los importes de Canal, Piel, Viscerea
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        internal void ActualizarImportesSacrificioLucero(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteSacrificioDAL.ObtenerParametroActualizarImportesSacrificio(lotesSacrificio);
                Update("LoteSacrificioLucero_ActualizarImportes", parameters);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los datos para conciliacion
        /// de sacrificios de ganado por traspaso
        /// </summary>
        /// <param name="interfaceSalidaTraspasos"></param>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosConciliacionSacrificioTraspasoGanado(List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspasos)
        {
            List<PolizaSacrificioModel> factura = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxLoteSacrificioDAL.ObtenerParametrosConciliacionSacrificioTraspaso(interfaceSalidaTraspasos);
                DataSet ds = Retrieve("LoteSacrificioLucero_ObtenerDatosConciliacion", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapLoteSacrificioDAL.ObtenerDatosSacrificadosLucero(ds);
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
            return factura;
        }

        /// <summary>
        /// Obtiene los datos para Generar
        /// las polizas de sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IEnumerable<PolizaSacrificioModel> ObtenerDatosGenerarPolizaSacrificio(DateTime fecha)
        {
            try
            {
                IMapBuilderContext<PolizaSacrificioModel> mapeo =
                    MapLoteSacrificioDAL.ObtenerMapeoPolizasSacrificioServicio();
                IEnumerable<PolizaSacrificioModel> datosPolizasSacrificio = GetDatabase().
                    ExecuteSprocAccessor
                    <PolizaSacrificioModel>(
                        "LoteSacrificio_ObtenerDatosPorFecha", mapeo.Build(),
                        new object[] { fecha });
                return datosPolizasSacrificio;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Actualiza bandera de poliza generada
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        internal void ActualizarPolizaGenerada(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteSacrificioDAL.ObtenerParametroActualizarPolizaGenerada(lotesSacrificio);
                Update("LoteSacrificio_ActualizarPolizaGenerada", parameters);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los datos para cancelar
        /// las polizas de sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IEnumerable<PolizaSacrificioModel> ObtenerDatosPolizaSacrificioCanceladas(DateTime fecha)
        {
            try
            {
                IMapBuilderContext<PolizaSacrificioModel> mapeo =
                    MapLoteSacrificioDAL.ObtenerMapeoPolizasSacrificioServicio();
                IEnumerable<PolizaSacrificioModel> datosPolizasSacrificio = GetDatabase().
                    ExecuteSprocAccessor
                    <PolizaSacrificioModel>(
                        "LoteSacrificio_ObtenerDatosPorFechaCancelacion", mapeo.Build(),
                        new object[] { fecha });
                return datosPolizasSacrificio;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
