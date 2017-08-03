using System.Linq;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Enums;
namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPolizaDAL
    {
        /// <summary>
        /// Obtiene los parametros para
        /// guardar la Poliza
        /// </summary>
        /// <param name="polizas"></param>
        /// <param name="tipoPoliza"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoPolizaEntradaGanado(IList<PolizaInfo> polizas, TipoPoliza tipoPoliza)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("MT_POLIZA_PERIFERICO",
                                 from poliza in polizas
                                 select new XElement("Datos",
                                                     new XElement("noref",
                                                                  poliza.NumeroReferencia),
                                                     new XElement("fecha_doc", poliza.FechaDocumento),
                                                     new XElement("fecha_cont", poliza.FechaContabilidad),
                                                     new XElement("clase_doc", poliza.ClaseDocumento),
                                                     new XElement("sociedad", poliza.Sociedad),
                                                     new XElement("moneda", poliza.Moneda),
                                                     new XElement("tipocambio", poliza.TipoCambio),
                                                     new XElement("texto_doc", poliza.TextoDocumento),
                                                     new XElement("mes", poliza.Mes),
                                                     new XElement("cuenta", poliza.Cuenta),
                                                     new XElement("proveedor", poliza.Proveedor),
                                                     new XElement("cliente", poliza.Cliente),
                                                     new XElement("indica_cme", poliza.IndicaCme),
                                                     new XElement("importe",
                                                                  "-0".Equals(poliza.Importe, StringComparison.
                                                                                                  InvariantCultureIgnoreCase)
                                                                  || "-0.00".Equals(poliza.Importe, StringComparison.
                                                                                                        InvariantCultureIgnoreCase)
                                                                      ? "0"
                                                                      : poliza.Importe),
                                                     new XElement("indica_imp", poliza.IndicaImp),
                                                     new XElement("centro_cto", poliza.CentroCosto),
                                                     new XElement("orden_int", poliza.OrdenInt),
                                                     new XElement("centro_ben", poliza.CentroBeneficio),
                                                     new XElement("texto_asig", poliza.TextoAsignado),
                                                     new XElement("concepto", poliza.Concepto),
                                                     new XElement("division", poliza.Division),
                                                     new XElement("clase_movt", poliza.ClaseMovimiento),
                                                     new XElement("bus_act", poliza.BusAct),
                                                     new XElement("periodo", poliza.Periodo),
                                                     new XElement("nolinea", poliza.NumeroLinea),
                                                     new XElement("ref1", poliza.Referencia1),
                                                     new XElement("ref2", poliza.Referencia2),
                                                     new XElement("ref3", poliza.Referencia3),
                                                     new XElement("fecha_imto", poliza.FechaImpuesto),
                                                     new XElement("cond_imto", poliza.CondicionImpuesto),
                                                     new XElement("clave_imto", poliza.ClaveImpuesto),
                                                     new XElement("tipo_ret", poliza.TipoRetencion),
                                                     new XElement("cod_ret", poliza.CodigoRetencion),
                                                     new XElement("imp_ret", poliza.ImpuestoRetencion),
                                                     new XElement("imp_iva", poliza.ImpuestoIva),
                                                     new XElement("archifolio",
                                                                  string.Concat(
                                                                      poliza.ArchivoFolio.Replace(".xml", string.Empty),
                                                                      ".xml"))
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoPolizaID", tipoPoliza},
                        {"@XmlPoliza", xml.ToString()},
                        {"@OrganizacionID", polizas[0].OrganizacionID},
                        {"@Estatus", polizas[0].Activo},
                        {"@UsuarioCreacionID", polizas[0].UsuarioCreacionID},
                        {"@Conciliada", polizas[0].Conciliada},
                        {"@ArchivoEnviadoServidor", polizas[0].ArchivoEnviadoServidor},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para
        /// la ejecucion del procedimiento almacenado
        /// Poliza_ObtenerXmlPoliza
        /// </summary>
        /// <param name="tipoPoliza"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <param name="clave"></param>
        /// <param name="concepto"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPoliza(TipoPoliza tipoPoliza, int organizacionID
                                                                         , DateTime fecha, string clave
                                                                         , string concepto, long estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoPolizaID", tipoPoliza.GetHashCode()},
                        {"@OrganizacionID", organizacionID},
                        {"@Fecha", fecha.ToString("yyyyMMdd")},
                        {"@Clave", clave},
                        {"@Concepto", concepto},
                        {"@Estatus", estatus},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almacenado
        /// Poliza_ActualizaArchivoEnviadoSAP
        /// </summary>
        /// <param name="polizaID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosArchivoEnviadoSAP(int polizaID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PolizaID", polizaID},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPolizaPorID(int polizaID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PolizaID", polizaID},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Poliza_ObtenerConciliacionPorFechas
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPolizaConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@FechaInicio", fechaInicio},
                        {"@FechaFin", fechaFin},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Poliza_ActualizaPolizaEstatus
        /// </summary>
        /// <param name="polizasCancelar"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDesactivarPolizas(List<PolizaInfo> polizasCancelar)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("MT_POLIZA_PERIFERICO",
                                 from poliza in polizasCancelar
                                 select new XElement("Datos",
                                                     new XElement("PolizaID",
                                                                  poliza.PolizaID)));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlPoliza", xml.ToString()},
                        {"@UsuarioModificacionID", polizasCancelar[0].UsuarioModificacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para
        /// guardar la Poliza
        /// </summary>
        /// <param name="polizas"></param>
        /// <param name="tipoPoliza"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardadoPolizaServicioPI(IList<PolizaInfo> polizas, TipoPoliza tipoPoliza)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("MT_POLIZA_PERIFERICO",
                                 from poliza in polizas
                                 select new XElement("Datos",
                                                     new XElement("noref",
                                                                  poliza.NumeroReferencia),
                                                     new XElement("fecha_doc", poliza.FechaDocumento),
                                                     new XElement("fecha_cont", poliza.FechaContabilidad),
                                                     new XElement("clase_doc", poliza.ClaseDocumento),
                                                     new XElement("sociedad", poliza.Sociedad),
                                                     new XElement("moneda", poliza.Moneda),
                                                     new XElement("tipocambio", poliza.TipoCambio),
                                                     new XElement("texto_doc", poliza.TextoDocumento),
                                                     new XElement("mes", poliza.Mes),
                                                     new XElement("cuenta", poliza.Cuenta),
                                                     new XElement("proveedor", poliza.Proveedor),
                                                     new XElement("cliente", poliza.Cliente),
                                                     new XElement("indica_cme", poliza.IndicaCme),
                                                     new XElement("importe",
                                                                  "-0".Equals(poliza.Importe, StringComparison.
                                                                                                  InvariantCultureIgnoreCase)
                                                                  || "-0.00".Equals(poliza.Importe, StringComparison.
                                                                                                        InvariantCultureIgnoreCase)
                                                                      ? "0"
                                                                      : poliza.Importe),
                                                     new XElement("indica_imp", poliza.IndicaImp),
                                                     new XElement("centro_cto", poliza.CentroCosto),
                                                     new XElement("orden_int", poliza.OrdenInt),
                                                     new XElement("centro_ben", poliza.CentroBeneficio),
                                                     new XElement("texto_asig", poliza.TextoAsignado),
                                                     new XElement("concepto", poliza.Concepto),
                                                     new XElement("division", poliza.Division),
                                                     new XElement("clase_movt", poliza.ClaseMovimiento),
                                                     new XElement("bus_act", poliza.BusAct),
                                                     new XElement("periodo", poliza.Periodo),
                                                     new XElement("nolinea", poliza.NumeroLinea),
                                                     new XElement("ref1", poliza.Referencia1),
                                                     new XElement("ref2", poliza.Referencia2),
                                                     new XElement("ref3", poliza.Referencia3),
                                                     new XElement("fecha_imto", poliza.FechaImpuesto),
                                                     new XElement("cond_imto", poliza.CondicionImpuesto),
                                                     new XElement("clave_imto", poliza.ClaveImpuesto),
                                                     new XElement("tipo_ret", poliza.TipoRetencion),
                                                     new XElement("cod_ret", poliza.CodigoRetencion),
                                                     new XElement("imp_ret", poliza.ImpuestoRetencion),
                                                     new XElement("imp_iva", poliza.ImpuestoIva),
                                                     new XElement("archifolio",
                                                                  string.Concat(
                                                                      poliza.ArchivoFolio.Replace(".xml", string.Empty),
                                                                      ".xml")),
                                                     new XElement("DocumentoSAP", poliza.DocumentoSAP),
                                                     new XElement("DocumentoCancelacionSAP", poliza.DocumentoCancelacionSAP),
                                                     new XElement("Segmento", poliza.Segmento)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoPolizaID", tipoPoliza},
                        {"@XmlPoliza", xml.ToString()},
                        {"@OrganizacionID", polizas[0].OrganizacionID},
                        {"@Estatus", polizas[0].Activo},
                        {"@UsuarioCreacionID", polizas[0].UsuarioCreacionID},
                        {"@Conciliada", polizas[0].Conciliada},
                        {"@ArchivoEnviadoServidor", polizas[0].ArchivoEnviadoServidor},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimimiento almacenado Poliza_ObtenerPorFechaTipoPoliza
        /// </summary>
        /// <param name="tipoPoliza"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPolizaConDocumentoSAP(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoPolizaID", tipoPoliza.GetHashCode()},
                        {"@OrganizacionID", organizacionID},
                        {"@Fecha", fecha.ToString("yyyyMMdd")},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la 
        /// ejecucion del procedimiento almacenado
        /// Poliza_ActualizaProcesadoSAP
        /// </summary>
        /// <param name="polizaID"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMensajeSAP(RespuestaServicioPI respuestaServicioPI)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PolizaID", respuestaServicioPI.PolizaID},
                        {"@Mensaje", respuestaServicioPI.Mensaje},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosProcesadoSAP(RespuestaServicioPI respuestaServicioPI)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("MT_POLIZA_PERIFERICO",
                                 from poliza in respuestaServicioPI.Polizas
                                 select new XElement("Datos",
                                                     new XElement("noref",
                                                                  poliza.NumeroReferencia),
                                                     new XElement("fecha_doc", poliza.FechaDocumento),
                                                     new XElement("fecha_cont", poliza.FechaContabilidad),
                                                     new XElement("clase_doc", poliza.ClaseDocumento),
                                                     new XElement("sociedad", poliza.Sociedad),
                                                     new XElement("moneda", poliza.Moneda),
                                                     new XElement("tipocambio", poliza.TipoCambio),
                                                     new XElement("texto_doc", poliza.TextoDocumento),
                                                     new XElement("mes", poliza.Mes),
                                                     new XElement("cuenta", poliza.Cuenta),
                                                     new XElement("proveedor", poliza.Proveedor),
                                                     new XElement("cliente", poliza.Cliente),
                                                     new XElement("indica_cme", poliza.IndicaCme),
                                                     new XElement("importe",
                                                                  "-0".Equals(poliza.Importe, StringComparison.
                                                                                                  InvariantCultureIgnoreCase)
                                                                  || "-0.00".Equals(poliza.Importe, StringComparison.
                                                                                                        InvariantCultureIgnoreCase)
                                                                      ? "0"
                                                                      : poliza.Importe),
                                                     new XElement("indica_imp", poliza.IndicaImp),
                                                     new XElement("centro_cto", poliza.CentroCosto),
                                                     new XElement("orden_int", poliza.OrdenInt),
                                                     new XElement("centro_ben", poliza.CentroBeneficio),
                                                     new XElement("texto_asig", poliza.TextoAsignado),
                                                     new XElement("concepto", poliza.Concepto),
                                                     new XElement("division", poliza.Division),
                                                     new XElement("clase_movt", poliza.ClaseMovimiento),
                                                     new XElement("bus_act", poliza.BusAct),
                                                     new XElement("periodo", poliza.Periodo),
                                                     new XElement("nolinea", poliza.NumeroLinea),
                                                     new XElement("ref1", poliza.Referencia1),
                                                     new XElement("ref2", poliza.Referencia2),
                                                     new XElement("ref3", poliza.Referencia3),
                                                     new XElement("fecha_imto", poliza.FechaImpuesto),
                                                     new XElement("cond_imto", poliza.CondicionImpuesto),
                                                     new XElement("clave_imto", poliza.ClaveImpuesto),
                                                     new XElement("tipo_ret", poliza.TipoRetencion),
                                                     new XElement("cod_ret", poliza.CodigoRetencion),
                                                     new XElement("imp_ret", poliza.ImpuestoRetencion),
                                                     new XElement("imp_iva", poliza.ImpuestoIva),
                                                     new XElement("archifolio",
                                                                  string.Concat(
                                                                      poliza.ArchivoFolio.Replace(".xml", string.Empty),
                                                                      ".xml")),
                                                     new XElement("DocumentoSAP", poliza.DocumentoSAP),
                                                     new XElement("DocumentoCancelacionSAP", poliza.DocumentoCancelacionSAP),
                                                     new XElement("Segmento", poliza.Segmento)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlPoliza", xml.ToString()},
                        {"@PolizaID", respuestaServicioPI.PolizaID},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios
        /// para la ejecucion del procedimiento
        /// almacenado Poliza_ActualizaCanceladoSAP
        /// </summary>
        /// <param name="polizasID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCanceladoSAP(List<int> polizasID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from id in polizasID
                                 select new XElement("PolizasID",
                                                     new XElement("PolizaID", id)));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlPoliza", xml.ToString()},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Genera los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Poliza_Actualizar
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPoliza(RespuestaServicioPI respuestaServicioPI)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("MT_POLIZA_PERIFERICO",
                                 from poliza in respuestaServicioPI.Polizas
                                 select new XElement("Datos",
                                                     new XElement("noref",
                                                                  poliza.NumeroReferencia),
                                                     new XElement("fecha_doc", poliza.FechaDocumento),
                                                     new XElement("fecha_cont", poliza.FechaContabilidad),
                                                     new XElement("clase_doc", poliza.ClaseDocumento),
                                                     new XElement("sociedad", poliza.Sociedad),
                                                     new XElement("moneda", poliza.Moneda),
                                                     new XElement("tipocambio", poliza.TipoCambio),
                                                     new XElement("texto_doc", poliza.TextoDocumento),
                                                     new XElement("mes", poliza.Mes),
                                                     new XElement("cuenta", poliza.Cuenta),
                                                     new XElement("proveedor", poliza.Proveedor),
                                                     new XElement("cliente", poliza.Cliente),
                                                     new XElement("indica_cme", poliza.IndicaCme),
                                                     new XElement("importe",
                                                                  "-0".Equals(poliza.Importe, StringComparison.
                                                                                                  InvariantCultureIgnoreCase)
                                                                  || "-0.00".Equals(poliza.Importe, StringComparison.
                                                                                                        InvariantCultureIgnoreCase)
                                                                      ? "0"
                                                                      : poliza.Importe),
                                                     new XElement("indica_imp", poliza.IndicaImp),
                                                     new XElement("centro_cto", poliza.CentroCosto),
                                                     new XElement("orden_int", poliza.OrdenInt),
                                                     new XElement("centro_ben", poliza.CentroBeneficio),
                                                     new XElement("texto_asig", poliza.TextoAsignado),
                                                     new XElement("concepto", poliza.Concepto),
                                                     new XElement("division", poliza.Division),
                                                     new XElement("clase_movt", poliza.ClaseMovimiento),
                                                     new XElement("bus_act", poliza.BusAct),
                                                     new XElement("periodo", poliza.Periodo),
                                                     new XElement("nolinea", poliza.NumeroLinea),
                                                     new XElement("ref1", poliza.Referencia1),
                                                     new XElement("ref2", poliza.Referencia2),
                                                     new XElement("ref3", poliza.Referencia3),
                                                     new XElement("fecha_imto", poliza.FechaImpuesto),
                                                     new XElement("cond_imto", poliza.CondicionImpuesto),
                                                     new XElement("clave_imto", poliza.ClaveImpuesto),
                                                     new XElement("tipo_ret", poliza.TipoRetencion),
                                                     new XElement("cod_ret", poliza.CodigoRetencion),
                                                     new XElement("imp_ret", poliza.ImpuestoRetencion),
                                                     new XElement("imp_iva", poliza.ImpuestoIva),
                                                     new XElement("archifolio",
                                                                  string.Concat(
                                                                      poliza.ArchivoFolio.Replace(".xml", string.Empty),
                                                                      ".xml")),
                                                     new XElement("DocumentoSAP", poliza.DocumentoSAP),
                                                     new XElement("DocumentoCancelacionSAP", poliza.DocumentoCancelacionSAP),
                                                     new XElement("Segmento", poliza.Segmento)
                                     ));
                int organizacionID = respuestaServicioPI.Polizas[0].OrganizacionID;
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlPoliza", xml.ToString()},
                        {"@PolizaID", respuestaServicioPI.PolizaID},
                        {"@OrganizacionID", organizacionID},
                        {"@Estatus", EstatusEnum.Inactivo},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento que regresa los datos de
        /// facturacion de lucero
        /// </summary>
        internal static string ObtenerParametrosObtenerPolizasConciliacionSapSiap(List<string> divisiones)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                       from detalle in divisiones
                                       select new XElement("Divisiones",
                                                           new XElement("Division", detalle)
                                           ));
                return xml.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento que regresa los datos de
        /// facturacion de lucero
        /// </summary>
        internal static string ObtenerParametrosObtenerPolizasConciliacionSapSiapClaveDoc(List<string> clasedocumento)
        {
            try
            {
                Logger.Info();
                var xmlCve = new XElement("ROOT",
                                       from clave in clasedocumento
                                       select new XElement("ClaveDocumentos",
                                                           new XElement("ClaveDocumento", clave)
                                           ));
                return xmlCve.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static object ObtenerParametrosObtenerPolizasConciliacionSapSiapClavePolizas(List<string> clavesPolizas)
        {
            try
            {
                Logger.Info();
                var xmlCve = new XElement("ROOT",
                                       from clavePoliza in clavesPolizas
                                       select new XElement("ClavePolizas",
                                                           new XElement("ClavePoliza", clavePoliza)
                                           ));
                return xmlCve.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public static object ObtenerParametrosObtenerPolizasConciliacionSapSiapCuentass(List<string> cuentas)
        {
            try
            {
                Logger.Info();
                var xmlCve = new XElement("ROOT",
                                       from cuenta in cuentas
                                       select new XElement("Cuentas",
                                                           new XElement("Cuenta", cuenta)
                                           ));
                return xmlCve.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
