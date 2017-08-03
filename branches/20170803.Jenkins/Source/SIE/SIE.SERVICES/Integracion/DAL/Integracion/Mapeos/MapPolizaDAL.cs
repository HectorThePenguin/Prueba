using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPolizaDAL
    {
        /// <summary>
        /// Obtiene las polizas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<PolizaInfo> ObtenerPoliza(DataSet ds)
        {
            var polizas = new List<PolizaInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var xDocument = XDocument.Parse(Convert.ToString(dt.Rows[i]["XmlPoliza"]));
                    polizas.AddRange(xDocument.Descendants("Datos")
                                         .Select(element => new PolizaInfo
                                                                {
                                                                    NumeroReferencia = element.Element("noref").Value,
                                                                    FechaContabilidad =
                                                                        element.Element("fecha_cont").Value,
                                                                    FechaDocumento = element.Element("fecha_doc").Value,
                                                                    ClaseDocumento = element.Element("clase_doc").Value,
                                                                    Sociedad = element.Element("sociedad").Value,
                                                                    Moneda = element.Element("moneda").Value,
                                                                    TipoCambio =
                                                                        Convert.ToDecimal(
                                                                            element.Element("tipocambio").Value),
                                                                    TextoDocumento = element.Element("texto_doc").Value,
                                                                    Mes = element.Element("mes").Value,
                                                                    Cuenta = element.Element("cuenta").Value,
                                                                    Proveedor = element.Element("proveedor").Value,
                                                                    Cliente = element.Element("cliente").Value,
                                                                    IndicaCme = element.Element("indica_cme").Value,
                                                                    Importe = element.Element("importe").Value,
                                                                    IndicaImp = element.Element("indica_imp").Value,
                                                                    CentroCosto = element.Element("centro_cto").Value,
                                                                    OrdenInt = element.Element("orden_int").Value,
                                                                    CentroBeneficio =
                                                                        element.Element("centro_ben").Value,
                                                                    TextoAsignado = element.Element("texto_asig").Value,
                                                                    Concepto = element.Element("concepto").Value,
                                                                    Division = element.Element("division").Value,
                                                                    ClaseMovimiento =
                                                                        element.Element("clase_movt").Value,
                                                                    BusAct = element.Element("bus_act").Value,
                                                                    Periodo = element.Element("periodo").Value,
                                                                    NumeroLinea = element.Element("nolinea").Value,
                                                                    Referencia1 = element.Element("ref1").Value,
                                                                    Referencia2 = element.Element("ref2").Value,
                                                                    Referencia3 = element.Element("ref3").Value,
                                                                    FechaImpuesto = element.Element("fecha_imto").Value,
                                                                    CondicionImpuesto =
                                                                        element.Element("cond_imto").Value,
                                                                    ClaveImpuesto = element.Element("clave_imto").Value,
                                                                    TipoRetencion = element.Element("tipo_ret").Value,
                                                                    CodigoRetencion = element.Element("cod_ret").Value,
                                                                    ImpuestoRetencion =
                                                                        Convert.ToInt32(element.Element("imp_ret").Value),
                                                                    ImpuestoIva = element.Element("imp_iva").Value,
                                                                    ArchivoFolio = element.Element("archifolio").Value,
                                                                    PolizaID = Convert.ToInt32(dt.Rows[i]["PolizaID"]),
                                                                    TipoPolizaID =
                                                                        Convert.ToInt32(dt.Rows[i]["TipoPolizaID"])
                                                                }).ToList());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        /// <summary>
        /// Obtiene una cola con una lista de polizas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static Queue<IList<PolizaInfo>> ObtenerPolizasPendientes(DataSet ds)
        {
            var polizas = new Queue<IList<PolizaInfo>>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    try
                    {
                        var xDocument = XDocument.Parse(Convert.ToString(dt.Rows[index]["XmlPoliza"]));
                        var polizasXML = xDocument.Descendants("Datos")
                            .Select(element => new PolizaInfo
                            {
                                NumeroReferencia = element.Element("noref").Value,
                                FechaContabilidad = element.Element("fecha_cont").Value,
                                FechaDocumento = element.Element("fecha_doc").Value,
                                ClaseDocumento = element.Element("clase_doc").Value,
                                Sociedad = element.Element("sociedad").Value,
                                Moneda = element.Element("moneda").Value,
                                TipoCambio = Convert.ToDecimal(element.Element("tipocambio").Value),
                                TextoDocumento = element.Element("texto_doc").Value,
                                Mes = element.Element("mes").Value,
                                Cuenta = element.Element("cuenta").Value,
                                Proveedor = element.Element("proveedor").Value,
                                Cliente = element.Element("cliente").Value,
                                IndicaCme = element.Element("indica_cme").Value,
                                Importe = element.Element("importe").Value,
                                IndicaImp = element.Element("indica_imp").Value,
                                CentroCosto = element.Element("centro_cto").Value,
                                OrdenInt = element.Element("orden_int").Value,
                                CentroBeneficio = element.Element("centro_ben").Value,
                                TextoAsignado = element.Element("texto_asig").Value,
                                Concepto = element.Element("concepto").Value,
                                Division = element.Element("division").Value,
                                ClaseMovimiento = element.Element("clase_movt").Value,
                                BusAct = element.Element("bus_act").Value,
                                Periodo = element.Element("periodo").Value,
                                NumeroLinea = element.Element("nolinea").Value,
                                Referencia1 = element.Element("ref1").Value,
                                Referencia2 = element.Element("ref2").Value,
                                Referencia3 = element.Element("ref3").Value,
                                FechaImpuesto = element.Element("fecha_imto").Value,
                                CondicionImpuesto = element.Element("cond_imto").Value,
                                ClaveImpuesto = element.Element("clave_imto").Value,
                                TipoRetencion = element.Element("tipo_ret").Value,
                                CodigoRetencion = element.Element("cod_ret").Value,
                                ImpuestoRetencion = Convert.ToInt32(element.Element("imp_ret").Value),
                                ImpuestoIva = element.Element("imp_iva").Value,
                                ArchivoFolio = element.Element("archifolio").Value,
                                OrganizacionID = Convert.ToInt32(dt.Rows[index]["OrganizacionID"]),
                                PolizaID = Convert.ToInt32(dt.Rows[index]["PolizaID"]),
                                TipoPolizaID = Convert.ToInt32(dt.Rows[index]["TipoPolizaID"]),
                                Segmento = element.Element("Segmento").Value,
                            }).ToList();
                        polizas.Enqueue(polizasXML);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        /// <summary>
        /// Obtiene las polizas
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<PolizaInfo> ObtenerPolizasConciliacion()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaInfo> polizasConciliacion = MapBuilder<PolizaInfo>.MapAllProperties();
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Activo);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.FechaCreacion);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.FechaModificacion);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.UsuarioCreacionID);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.UsuarioModificacionID);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Generar);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.HabilitarCheck);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Inconcistencia);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Faltante);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.ArchivoEnviadoServidor);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.DocumentoSAP);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.DocumentoCancelacionSAP);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Segmento);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Corral);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Procesada);
                polizasConciliacion = polizasConciliacion.Map(x => x.ArchivoFolio).ToColumn("archifolio");
                polizasConciliacion = polizasConciliacion.Map(x => x.BusAct).ToColumn("bus_act");
                polizasConciliacion = polizasConciliacion.Map(x => x.CentroBeneficio).ToColumn("centro_ben");
                polizasConciliacion = polizasConciliacion.Map(x => x.CentroCosto).ToColumn("centro_cto");
                polizasConciliacion = polizasConciliacion.Map(x => x.ClaseDocumento).ToColumn("clase_doc");
                polizasConciliacion = polizasConciliacion.Map(x => x.ClaseMovimiento).ToColumn("clase_movt");
                polizasConciliacion = polizasConciliacion.Map(x => x.ClaveImpuesto).ToColumn("clave_imto");
                polizasConciliacion = polizasConciliacion.Map(x => x.Cliente).ToColumn("cliente");
                polizasConciliacion = polizasConciliacion.Map(x => x.CodigoRetencion).ToColumn("cod_ret");
                polizasConciliacion = polizasConciliacion.Map(x => x.Concepto).ToColumn("concepto");
                polizasConciliacion = polizasConciliacion.Map(x => x.CondicionImpuesto).ToColumn("cond_imto");
                polizasConciliacion = polizasConciliacion.Map(x => x.Cuenta).ToColumn("cuenta");
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Descripcion);
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.DescripcionProducto);
                polizasConciliacion = polizasConciliacion.Map(x => x.Division).ToColumn("division");
                polizasConciliacion = polizasConciliacion.Map(x => x.FechaContabilidad).ToColumn("fecha_cont");
                polizasConciliacion = polizasConciliacion.Map(x => x.FechaDocumento).ToColumn("fecha_doc");
                polizasConciliacion = polizasConciliacion.Map(x => x.FechaImpuesto).ToColumn("fecha_imto");
                polizasConciliacion = polizasConciliacion.Map(x => x.Importe).ToColumn("importe");
                polizasConciliacion = polizasConciliacion.Map(x => x.ImpuestoIva).ToColumn("imp_iva");
                polizasConciliacion = polizasConciliacion.Map(x => x.ImpuestoRetencion).ToColumn("imp_ret");
                polizasConciliacion = polizasConciliacion.Map(x => x.IndicaCme).ToColumn("indica_cme");
                polizasConciliacion = polizasConciliacion.Map(x => x.IndicaImp).ToColumn("indica_imp");
                polizasConciliacion = polizasConciliacion.Map(x => x.Mes).ToColumn("mes");
                polizasConciliacion = polizasConciliacion.Map(x => x.Moneda).ToColumn("moneda");
                polizasConciliacion = polizasConciliacion.Map(x => x.NumeroLinea).ToColumn("nolinea");
                polizasConciliacion = polizasConciliacion.Map(x => x.NumeroReferencia).ToColumn("noref");
                polizasConciliacion = polizasConciliacion.DoNotMap(x => x.Observaciones);
                polizasConciliacion = polizasConciliacion.Map(x => x.Periodo).ToColumn("periodo");
                polizasConciliacion = polizasConciliacion.Map(x => x.Proveedor).ToColumn("proveedor");
                polizasConciliacion = polizasConciliacion.Map(x => x.Referencia1).ToColumn("ref1");
                polizasConciliacion = polizasConciliacion.Map(x => x.Referencia2).ToColumn("ref2");
                polizasConciliacion = polizasConciliacion.Map(x => x.Referencia3).ToColumn("ref3");
                polizasConciliacion = polizasConciliacion.Map(x => x.Sociedad).ToColumn("sociedad");
                polizasConciliacion = polizasConciliacion.Map(x => x.TextoAsignado).ToColumn("texto_asig");
                polizasConciliacion = polizasConciliacion.Map(x => x.TextoDocumento).ToColumn("texto_doc");
                polizasConciliacion = polizasConciliacion.Map(x => x.TipoCambio).ToColumn("tipocambio");
                polizasConciliacion = polizasConciliacion.Map(x => x.TipoRetencion).ToColumn("tipo_ret");
                polizasConciliacion = polizasConciliacion.Map(x => x.OrdenInt).ToColumn("orden_int");
                polizasConciliacion = polizasConciliacion.Map(x => x.Conciliada).ToColumn("Conciliada");
                return polizasConciliacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene las polizas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<PolizaInfo> ObtenerPolizaConDocumentoSAP(DataSet ds)
        {
            var polizas = new List<PolizaInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var xDocument = XDocument.Parse(Convert.ToString(dt.Rows[i]["XmlPoliza"]));
                    polizas.AddRange(xDocument.Descendants("Datos")
                                         .Select(element => new PolizaInfo
                                         {
                                             NumeroReferencia = element.Element("noref").Value,
                                             FechaContabilidad =
                                                 element.Element("fecha_cont").Value,
                                             FechaDocumento = element.Element("fecha_doc").Value,
                                             ClaseDocumento = element.Element("clase_doc").Value,
                                             Sociedad = element.Element("sociedad").Value,
                                             Moneda = element.Element("moneda").Value,
                                             TipoCambio =
                                                 Convert.ToDecimal(
                                                     element.Element("tipocambio").Value),
                                             TextoDocumento = element.Element("texto_doc").Value,
                                             Mes = element.Element("mes").Value,
                                             Cuenta = element.Element("cuenta").Value,
                                             Proveedor = element.Element("proveedor").Value,
                                             Cliente = element.Element("cliente").Value,
                                             IndicaCme = element.Element("indica_cme").Value,
                                             Importe = element.Element("importe").Value,
                                             IndicaImp = element.Element("indica_imp").Value,
                                             CentroCosto = element.Element("centro_cto").Value,
                                             OrdenInt = element.Element("orden_int").Value,
                                             CentroBeneficio =
                                                 element.Element("centro_ben").Value,
                                             TextoAsignado = element.Element("texto_asig").Value,
                                             Concepto = element.Element("concepto").Value,
                                             Division = element.Element("division").Value,
                                             ClaseMovimiento =
                                                 element.Element("clase_movt").Value,
                                             BusAct = element.Element("bus_act").Value,
                                             Periodo = element.Element("periodo").Value,
                                             NumeroLinea = element.Element("nolinea").Value,
                                             Referencia1 = element.Element("ref1").Value,
                                             Referencia2 = element.Element("ref2").Value,
                                             Referencia3 = element.Element("ref3").Value,
                                             FechaImpuesto = element.Element("fecha_imto").Value,
                                             CondicionImpuesto =
                                                 element.Element("cond_imto").Value,
                                             ClaveImpuesto = element.Element("clave_imto").Value,
                                             TipoRetencion = element.Element("tipo_ret").Value,
                                             CodigoRetencion = element.Element("cod_ret").Value,
                                             ImpuestoRetencion =
                                                 Convert.ToInt32(element.Element("imp_ret").Value),
                                             ImpuestoIva = element.Element("imp_iva").Value,
                                             ArchivoFolio = element.Element("archifolio").Value,
                                             PolizaID = Convert.ToInt32(dt.Rows[i]["PolizaID"]),
                                             TipoPolizaID =
                                                 Convert.ToInt32(dt.Rows[i]["TipoPolizaID"]),
                                             DocumentoSAP = element.Element("DocumentoSAP").Value
                                         }).ToList());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        /// <summary>
        /// Obtiene una cola con una lista de polizas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static Queue<IList<PolizaInfo>> ObtenerPolizasCanceladas(DataSet ds)
        {
            var polizas = new Queue<IList<PolizaInfo>>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    var xDocument = XDocument.Parse(Convert.ToString(dt.Rows[index]["XmlPoliza"]));
                    var polizasXML = xDocument.Descendants("Datos")
                        .Select(element => new PolizaInfo
                        {
                            NumeroReferencia = element.Element("noref").Value,
                            FechaContabilidad = element.Element("fecha_cont").Value,
                            FechaDocumento = element.Element("fecha_doc").Value,
                            ClaseDocumento = element.Element("clase_doc").Value,
                            Sociedad = element.Element("sociedad").Value,
                            Moneda = element.Element("moneda").Value,
                            TipoCambio = Convert.ToDecimal(element.Element("tipocambio").Value),
                            TextoDocumento = element.Element("texto_doc").Value,
                            Mes = element.Element("mes").Value,
                            Cuenta = element.Element("cuenta").Value,
                            Proveedor = element.Element("proveedor").Value,
                            Cliente = element.Element("cliente").Value,
                            IndicaCme = element.Element("indica_cme").Value,
                            Importe = element.Element("importe").Value,
                            IndicaImp = element.Element("indica_imp").Value,
                            CentroCosto = element.Element("centro_cto").Value,
                            OrdenInt = element.Element("orden_int").Value,
                            CentroBeneficio = element.Element("centro_ben").Value,
                            TextoAsignado = element.Element("texto_asig").Value,
                            Concepto = element.Element("concepto").Value,
                            Division = element.Element("division").Value,
                            ClaseMovimiento = element.Element("clase_movt").Value,
                            BusAct = element.Element("bus_act").Value,
                            Periodo = element.Element("periodo").Value,
                            NumeroLinea = element.Element("nolinea").Value,
                            Referencia1 = element.Element("ref1").Value,
                            Referencia2 = element.Element("ref2").Value,
                            Referencia3 = element.Element("ref3").Value,
                            FechaImpuesto = element.Element("fecha_imto").Value,
                            CondicionImpuesto = element.Element("cond_imto").Value,
                            ClaveImpuesto = element.Element("clave_imto").Value,
                            TipoRetencion = element.Element("tipo_ret").Value,
                            CodigoRetencion = element.Element("cod_ret").Value,
                            ImpuestoRetencion = Convert.ToInt32(element.Element("imp_ret").Value),
                            ImpuestoIva = element.Element("imp_iva").Value,
                            ArchivoFolio = element.Element("archifolio").Value,
                            DocumentoSAP = element.Element("DocumentoSAP").Value,
                            DocumentoCancelacionSAP = element.Element("DocumentoCancelacionSAP").Value,
                            Segmento = element.Element("Segmento").Value,
                            OrganizacionID = Convert.ToInt32(dt.Rows[index]["OrganizacionID"]),
                            PolizaID = Convert.ToInt32(dt.Rows[index]["PolizaID"]),
                            TipoPolizaID = Convert.ToInt32(dt.Rows[index]["TipoPolizaID"]),
                        }).ToList();
                    polizas.Enqueue(polizasXML);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        internal static List<int> ObtenerPolizasCuentasMal(DataSet ds)
        {
            var polizas = new List<int>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                polizas = (from d in dt.AsEnumerable()
                           select d.Field<int>("PolizaID")).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        internal static List<TiposCuentaConciliacionInfo> ObtenerTiposCuenta(DataSet ds)
        {
            var tiposCuenta = new List<TiposCuentaConciliacionInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                tiposCuenta = (from d in dt.AsEnumerable()
                               select new TiposCuentaConciliacionInfo
                               {
                                   Descripcion = d.Field<string>("Descripcion"),
                                   Prefijo = d.Field<string>("Prefijo")
                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return tiposCuenta;
        }

        /// <summary>
        /// Método que mapea los datos de la consulta con los datos de la póliza
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<PolizaInfo> ObtenerPolizasConciliacionSapSiap()
        {
            try
            {
                Logger.Info();
                var polizas = MapBuilder<PolizaInfo>.MapAllProperties();
                polizas = polizas.DoNotMap(x => x.FechaContabilidad);
                polizas = polizas.DoNotMap(x => x.Activo);
                polizas = polizas.DoNotMap(x => x.FechaCreacion);
                polizas = polizas.DoNotMap(x => x.FechaModificacion);
                polizas = polizas.DoNotMap(x => x.UsuarioCreacionID);
                polizas = polizas.DoNotMap(x => x.UsuarioModificacionID);
                polizas = polizas.DoNotMap(x => x.Generar);
                polizas = polizas.DoNotMap(x => x.HabilitarCheck);
                polizas = polizas.DoNotMap(x => x.Inconcistencia);
                polizas = polizas.DoNotMap(x => x.Faltante);
                polizas = polizas.DoNotMap(x => x.ArchivoEnviadoServidor);
                polizas = polizas.DoNotMap(x => x.DocumentoSAP);
                polizas = polizas.DoNotMap(x => x.DocumentoCancelacionSAP);
                polizas = polizas.DoNotMap(x => x.Segmento);
                polizas = polizas.DoNotMap(x => x.Corral);
                polizas = polizas.DoNotMap(x => x.Procesada);
                polizas = polizas.DoNotMap(x => x.ArchivoFolio);
                polizas = polizas.DoNotMap(x => x.BusAct);
                polizas = polizas.DoNotMap(x => x.CentroBeneficio);
                polizas = polizas.DoNotMap(x => x.CentroCosto);
                polizas = polizas.DoNotMap(x => x.ClaseDocumento);
                polizas = polizas.DoNotMap(x => x.ClaseMovimiento);
                polizas = polizas.DoNotMap(x => x.ClaveImpuesto);
                polizas = polizas.DoNotMap(x => x.Cliente);
                polizas = polizas.DoNotMap(x => x.CodigoRetencion);
                polizas = polizas.DoNotMap(x => x.Conciliada);
                polizas = polizas.DoNotMap(x => x.CondicionImpuesto);
                polizas = polizas.DoNotMap(x => x.OrganizacionID);
                polizas = polizas.DoNotMap(x => x.FechaImpuesto);
                polizas = polizas.DoNotMap(x => x.ImpuestoIva);
                polizas = polizas.DoNotMap(x => x.ImpuestoRetencion);
                polizas = polizas.DoNotMap(x => x.IndicaCme);
                polizas = polizas.DoNotMap(x => x.IndicaImp);
                polizas = polizas.DoNotMap(x => x.Mes);
                polizas = polizas.DoNotMap(x => x.Moneda);
                polizas = polizas.DoNotMap(x => x.NumeroLinea);
                polizas = polizas.DoNotMap(x => x.NumeroReferencia);
                polizas = polizas.DoNotMap(x => x.Periodo);
                polizas = polizas.DoNotMap(x => x.Proveedor);
                polizas = polizas.DoNotMap(x => x.Referencia1);
                polizas = polizas.DoNotMap(x => x.Referencia2);
                polizas = polizas.DoNotMap(x => x.Sociedad);
                polizas = polizas.DoNotMap(x => x.TextoAsignado);
                polizas = polizas.DoNotMap(x => x.TextoDocumento);
                polizas = polizas.DoNotMap(x => x.TipoCambio);
                polizas = polizas.DoNotMap(x => x.TipoRetencion);
                polizas = polizas.DoNotMap(x => x.OrdenInt);
                polizas = polizas.DoNotMap(x => x.Descripcion);
                polizas = polizas.DoNotMap(x => x.DescripcionProducto);
                polizas = polizas.DoNotMap(x => x.Observaciones);
                polizas = polizas.DoNotMap(x => x.TipoPolizaID);
                

                polizas = polizas.Map(x => x.Cuenta).ToColumn("cuenta");
                polizas = polizas.Map(x => x.Importe).ToColumn("importe");
                polizas = polizas.Map(x => x.Concepto).ToColumn("concepto");
                polizas = polizas.Map(x => x.Division).ToColumn("division");
                polizas = polizas.Map(x => x.Referencia3).ToColumn("ref3");
                polizas = polizas.Map(x => x.PolizaID).ToColumn("PolizaID");
                polizas = polizas.Map(x => x.NumeroReferencia).ToColumn("Folio");
                polizas = polizas.Map(x => x.FechaDocumento).ToColumn("FechaDocumento");
                polizas = polizas.Map(x => x.FechaContabilidad).ToColumn("FechaContabilidad");
                polizas = polizas.Map(x => x.DocumentoSAP).ToColumn("DocumentoSAP");

                return polizas;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
