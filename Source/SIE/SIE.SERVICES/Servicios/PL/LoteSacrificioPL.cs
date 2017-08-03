using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class LoteSacrificioPL
    {
        /// <summary>
        /// Obtiene los datos del lote sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public LoteSacrificioInfo ObtenerLoteSacrificio(DateTime fecha, int organizacionID)
        {
            LoteSacrificioInfo result;
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                result = ordenSacrificioBl.ObtenerLoteSacrificio(fecha, organizacionID);
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
        public LoteSacrificioInfo ObtenerLoteSacrificioLucero(DateTime fecha, int organizacionID)
        {
            LoteSacrificioInfo result;
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                result = ordenSacrificioBl.ObtenerLoteSacrificioLucero(fecha, organizacionID);
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
        /// <param name="ganadoPropio"></param>
        public void ActualizarLoteSacrificio(LoteSacrificioInfo loteSacrificioInfo, bool ganadoPropio)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                ordenSacrificioBl.ActualizarLoteSacrificio(loteSacrificioInfo, ganadoPropio);
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
        public LoteSacrificioInfo ObtenerLoteSacrificioACancelar(int organizacionID)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                return ordenSacrificioBl.ObtenerLoteSacrificioACancelar(organizacionID);
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
        /// Obtiene las facturas generadas a los lotes de sacrificio
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        public List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelar(int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                return ordenSacrificioBl.ObtenerFacturasPorOrdenSacrificioACancelar(ordenSacrificioId);
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
        /// Obtiene una lista con
        /// los sacrificios generados
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public IEnumerable<PolizaSacrificioModel> ObtenerPolizasSacrificioConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                IEnumerable<PolizaSacrificioModel> polizaSacrificio =
                    ordenSacrificioBl.ObtenerPolizasSacrificioConciliacion(organizacionID, fechaInicial, fechaFinal);
                return polizaSacrificio;
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
        public LoteSacrificioInfo ObtenerLoteSacrificioACancelarLucero(int organizacionID)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                return ordenSacrificioBl.ObtenerLoteSacrificioACancelarLucero(organizacionID);
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
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        public List<PolizaSacrificioModel> ObtenerDatosFacturaSacrificio(int ordenSacrificioId)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                result = loteSacrificioBL.ObtenerDatosFacturaSacrificio(ordenSacrificioId);
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
        public List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelarLucero(int organizacionID)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new LoteSacrificioBL();
                return ordenSacrificioBl.ObtenerFacturasPorOrdenSacrificioACancelarLucero(organizacionID);
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
        public List<PolizaSacrificioModel> ObtenerDatosConciliacionSacrificioTraspasoGanado(List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspasos)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                result = loteSacrificioBL.ObtenerDatosConciliacionSacrificioTraspasoGanado(interfaceSalidaTraspasos);
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
        /// Obtiene los datos para Generar
        /// las polizas de sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public IEnumerable<PolizaSacrificioModel> ObtenerDatosGenerarPolizaSacrificio(DateTime fecha)
        {
            IEnumerable<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                result = loteSacrificioBL.ObtenerDatosGenerarPolizaSacrificio(fecha);
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
        /// Obtiene los datos en caso de que se sacrifico con anterioridad
        /// de ese lote de manera incompleta
        /// </summary>
        /// <param name="lotesSacrificioFolios"></param>
        /// <returns></returns>
        public List<PolizaSacrificioModel> ObtenerSacrificiosAnteriores(List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                result = loteSacrificioBL.ObtenerSacrificiosAnteriores(lotesSacrificioFolios);
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
        public void ActualizarImportesSacrificioLucero(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                loteSacrificioBL.ActualizarImportesSacrificioLucero(lotesSacrificio);
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
        /// Actualiza bandera de poliza generada
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        public void ActualizarPolizaGenerada(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                loteSacrificioBL.ActualizarPolizaGenerada(lotesSacrificio);
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
        public IEnumerable<PolizaSacrificioModel> ObtenerDatosPolizaSacrificioCanceladas(DateTime fecha)
        {
            IEnumerable<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioBL = new LoteSacrificioBL();
                result = loteSacrificioBL.ObtenerDatosPolizaSacrificioCanceladas(fecha);
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
    }
}
