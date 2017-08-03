using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSalidaGanadoEnTransitoDAL
    {
        /// <summary>
        /// Genera los parametros para un registro de una salida ya sea por muerte o por venta
        /// </summary>
        /// <param name="info">Salida de ganado al que se le generaran los parametros para registro</param>
        /// <returns>Regresa la lista de parametros para el registro</returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear_Salida(SalidaGanadoEnTransitoInfo info)
        {
            try
            {
                Logger.Info();
                //genera el xml de los detalles:
                var xml =
                    new XElement("ROOT",
                        from detalles in info.DetallesSalida
                        select new XElement("SalidaGanadoTransitoDetalle",
                            new XElement("CostoID", detalles.CostoId),
                            new XElement("ImporteCosto", detalles.ImporteCosto)));
                
                var parametros =
                     new Dictionary<string, object>
                        {
                            {"@OrganizacionId", info.OrganizacionID},
							{"@LoteId", info.LoteID},
							{"@NumCabezas", info.NumCabezas},
                            {"@Venta", info.Venta},
                            {"@Muerte", info.Muerte},
                            {"@Fecha", info.Fecha},
                            {"@Importe",info.Importe}, 
                            {"@ClienteID", info.Cliente.ClienteID},
                            {"@Kilos", info.Kilos},
                            {"@Folio", info.Folio},
                            {"@FolioFactura", info.FolioFactura},
                            {"@PolizaId", info.PolizaID},
                            {"@UsuarioCreacionId", info.UsuarioCreacionID},
                            {"@Observaciones", info.Observaciones},
                            {"@Detalles", xml.ToString()},
                         
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera los parametros para la asignacion de folio
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion para la cual se genera el folio</param>
        /// <param name="tipoFolioId">ID del tipo de folio que se generara</param>
        /// <returns>Regresa la lista de parametros para obtener el folio de la salida de ganado en transito</returns>
        public static Dictionary<string, object> Folio(int organizacionId, int tipoFolioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                     new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
							{"@TipoFolioID", tipoFolioId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera los parametros para actualizar las tablas de entrada de ganado en transito, entrada de ganado en transito detalle y el lote
        /// </summary>
        /// <param name="info">Salida de ganado al que se le generaran los parametros para actualizar las entradas de ganado en transito y el lote 
        /// correspondies a la salida</param>
        /// <param name="Importes"></param>
        /// <returns>Regresa la lista de parametros para actualizar las entradas de ganado y el lote correspondientes a la salida de ganado</returns>
        public static Dictionary<string, object> ObtenerParametrosActualizarEntradas(SalidaGanadoEnTransitoInfo info, List<CostoInfo> Importes)
        {
            try{

                var xml =
                    new XElement("ROOT",
                                 from accion in Importes
                                 select new XElement("Importes",
                                                     new XElement("CostoId", accion.CostoID),
                                                     new XElement("Importe",accion.ImporteCosto)
                                     ));

              var parametros =
                     new Dictionary<string, object>
                        {
                            {"@EntradaGanadoTransitoID",info.EntradaGanadoTransitoID},
							{"@LoteId", info.LoteID},
							{"@NumCabezas", info.NumCabezas},
                            {"@Fecha", info.Fecha},
                            {"@Importe",info.Importe}, 
                            {"@Kilos", info.Kilos},
                            {"@UsuarioModificacionID", info.UsuarioCreacionID},
                            {"@Importes",xml.ToString()}//XML DE importes
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera los parametros para la asignacion de folio de factura a la salida  de ganado registrada
        /// </summary>
        /// <param name="salida">Salida que tiene el folio de salida de ganado que se le asignara el folio de factura</param>
        /// <returns>Regresa la lista de parametros para asignar el folio de factura a una salida de ganado en transito por venta</returns>
        public static Dictionary<string, object> ObtenerParametrosAsignarFolioFactura(SalidaGanadoEnTransitoInfo salida)
        {
            try
            {
                var parametros =
                       new Dictionary<string, object>
                        {
                            {"@FolioSalida", salida.Folio},
                            {"@OrganizacionID", salida.OrganizacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera los parametros para la consulta que trae los datos faltantes para la generacion de la poliza
        /// </summary>
        /// <param name="salida">Salida  de ganado en transito que tiene el folio de salida de ganado y si es salida por muerte o por venta </param>
        /// <returns>Regresa la lista de parametros para obtener los datos faltantes para la poliza</returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerPoliza(SalidaGanadoEnTransitoInfo salida)
        {
            try
            {
                var parametros =
                       new Dictionary<string, object>
                        {
                            {"@Folio", salida.Folio},
                             {"@Muerte", salida.Muerte}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera los parametros para la asignacion de la poliza al registro
        /// </summary>
        /// <param name="info">Salida de ganado en transito que tiene el folio de salida y el PolizaID que se asignara a la salida de ganado en transito</param>
        /// <returns>Regresa la lista de parametros para asignar la poliza generada a la salida de ganado en transito</returns>
        public static Dictionary<string, object> ObtenerParametrosAsignarPolizaSalida(SalidaGanadoEnTransitoInfo info)
        {
            try
            {
                var parametros =
                       new Dictionary<string, object>
                        {
                            {"@Folio", info.Folio},
                             {"@PolizaID", info.PolizaID}, 
                             {"@EsMuerte", info.Muerte},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros nesesarios para consumir el SP SalidaGanadoTransito_ObtenerDatosFactura
        /// </summary>
        /// <param name="folio">Folio que se acava de registrar</param>
        /// <param name="activo">el SP valida que activo (Venta) sea igual a true(1)</param>
        /// <returns>Regresa la lista de parametros para obtener los datos de la factura</returns>
        internal static Dictionary<string, object> SalidaGanadoTransito_ObtenerDatosFactura(int folio, bool activo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Folio", folio},
                            {"@Activo", activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}