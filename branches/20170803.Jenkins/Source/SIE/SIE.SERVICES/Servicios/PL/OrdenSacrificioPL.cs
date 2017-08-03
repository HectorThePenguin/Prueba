using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class OrdenSacrificioPL
    {
        /// <summary>
        /// Método Para Guardar en en la Tabla Animal
        /// </summary>
        public OrdenSacrificioInfo GuardarOrdenSacrificio(OrdenSacrificioInfo ordenSacrificio, IList<OrdenSacrificioDetalleInfo> detalleOrden, int organizacionId)
        {
            OrdenSacrificioInfo result;
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new OrdenSacrificioBL();
                result = ordenSacrificioBl.GuardarOrdenSacrificio(ordenSacrificio, detalleOrden, organizacionId);

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;

        }
        /// <summary>
        /// Imprime la orden de sacrificio
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="detalleOrden"></param>
        public void ImprimirOrdenSacrificio(ImpresionOrdenSacrificioInfo etiquetas,IList<OrdenSacrificioDetalleInfo> detalleOrden )
        {
            Logger.Info();
            var ordenSacrificioBl = new OrdenSacrificioBL();
            ordenSacrificioBl.ImprimirOrdenDeSacrificio(etiquetas, detalleOrden);
        }
        /// <summary>
        /// Obtiene la orden de sacrificio del un dia en especifico
        /// </summary>
        /// <param name="orden"></param>
        /// <returns></returns>
        public OrdenSacrificioInfo ObtenerOrdenSacrificioDelDia(OrdenSacrificioInfo orden)
        {
            OrdenSacrificioInfo result;
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new OrdenSacrificioBL();
                result = ordenSacrificioBl.ObtenerOrdenSacrificioDelDia(orden);

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Cambia el estatus del detalle de una orden de sacrificio
        /// </summary>
        /// <param name="detalleOrden"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public int EliminarDetalleOrdenSacrificio(IList<OrdenSacrificioDetalleInfo> detalleOrden, int idUsuario)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new OrdenSacrificioBL();
                return ordenSacrificioBl.EliminarDetalleOrdenSacrificio(detalleOrden, idUsuario);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
        /// <summary>
        /// Obtiene los dias de engorda 70 del lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public int ObtnerDiasEngorda70(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new OrdenSacrificioBL();
                return ordenSacrificioBl.ObtnerDiasEngorda70(lote);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Obtiene el numero de cabezas de un lote en ordenes diferentes 
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        public int ObtnerCabezasDiferentesOrdenes(LoteInfo lote, int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new OrdenSacrificioBL();
                return ordenSacrificioBl.ObtnerCabezasDiferentesOrdenes(lote, ordenSacrificioId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Obtiene el numero de cabezas programadas en otras ordenes para el lote determinado
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        public  bool ValidarLoteOrdenSacrificio(int loteID)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioBl = new OrdenSacrificioBL();
                return ordenSacrificioBl.ValidarLoteOrdenSacrificio(loteID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// obtiene el detalle de  laorden de sacrificio  por la fecha  y organizacion
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public List<OrdenSacrificioDetalleInfo> DetalleOrden(string fecha, int organizacionId)
        {
            List<OrdenSacrificioDetalleInfo> detalle;
            try
            {
                Logger.Info();
                var ordenSacrificioBL = new OrdenSacrificioBL();
                detalle = ordenSacrificioBL.DetalleOrden(fecha, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return detalle;
        }

        public OrdenSacrificioInfo OrdenSacrificioFecha(string fecha, int organizacionId)
        {
            OrdenSacrificioInfo ordenSacrificio;
            try
            {
                var bl = new OrdenSacrificioBL();
                ordenSacrificio = bl.OrdenSacrificioFecha(fecha, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ordenSacrificio;
        }

        public IList<OrdenSacrificioDetalleInfo> ObtenerDetalleOrden(int ordenSacrificioId)
        {
            try
            {
                return OrdenSacrificioBL.ObtenerOrdenSacrificioDetalle(ordenSacrificioId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        public List<string> ConexionOrganizacion(int organizacionId)
        {
            List<string> conexion;
            try
            {
                conexion = OrdenSacrificioBL.ConexionOrganizacion(organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return conexion;
        }

        public List<string> ValidaLoteNoqueado(string con, string xml)
        {
            List<string> listLoteNoqueado;
            try
            {
                var orden = new OrdenSacrificioBL();
                listLoteNoqueado = orden.ValidaLoteNoqueado(con, xml);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listLoteNoqueado;
        }

        public bool ValidaCancelacionCabezasSIAP(string xml, int ordenSacrificioId, int usuarioId)
        {
            bool result;
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var orden = new OrdenSacrificioBL();
                    result = orden.ValidaCancelacionCabezasSIAP(xml, ordenSacrificioId, usuarioId);
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;

        }

        public bool CancelacionOrdenSacrificioMarel(string xml, int organizacionId)
        {
            bool result;
            try
            {
                var orden = new OrdenSacrificioBL();
                result = orden.CancelacionOrdenSacrificioMarel(xml, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;

        }

        public bool CancelacionOrdenSacrificioSCP(string xmlOrdenesDetalle, string fechaSacrificio, int organizacionId)
        {
            bool result;
            try
            {
                var orden = new OrdenSacrificioBL();
                result = orden.CancelacionOrdenSacrificioSCP(xmlOrdenesDetalle, fechaSacrificio, organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;

        }

        public List<SalidaSacrificioInfo> ConsultarEstatusOrdenSacrificioEnMarel(int organizacionId, string fechaSacrificio, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                var bl = new OrdenSacrificioBL();
                return bl.ConsultarEstatusOrdenSacrificioEnMarel(organizacionId, fechaSacrificio, detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<OrdenSacrificioDetalleInfo> ValidarCabezasActualesVsCabezasSacrificar(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                var bl = new OrdenSacrificioBL();
                return bl.ValidarCabezasActualesVsCabezasSacrificar(organizacionId, detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> ValidarCorralConLotesActivos(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                var bl = new OrdenSacrificioBL();
                return bl.ValidarCorralConLotesActivos(organizacionId, detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ControlSacrificioInfo> ObtenerResumenSacrificioScp(string fechaSacrificio, int organizacionId)
        {
            try
            {
                var bl = new OrdenSacrificioBL();
                return bl.ObtenerResumenSacrificioScp(fechaSacrificio, organizacionId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ControlSacrificioInfo> ObtenerResumenSacrificioSiap(List<ControlSacrificioInfo> resumenSacrificio)
        {
            try
            {
                var bl = new OrdenSacrificioBL();
                return bl.ObtenerResumenSacrificioSiap(resumenSacrificio);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
