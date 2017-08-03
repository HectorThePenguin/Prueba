using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Text;
using System.Threading;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSacrificio : PolizaAbstract
    {
        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var datos = datosPoliza as List<PolizaSacrificioModel>;
            IList<PolizaInfo> polizas = ObtenerPoliza(datos);
            return polizas;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(List<PolizaSacrificioModel> datosPoliza)
        {
            var polizasSacrificio = new List<PolizaInfo>();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.PolizaSacrificio.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA", TipoPoliza.PolizaSacrificio));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;


            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            int organizacionID = datosPoliza.Select(org => org.OrganizacionID).FirstOrDefault();
            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);

            IList<ProveedorInfo> proveedores = ObtenerProveedores();

            string claveParametro = datosPoliza.Select(clave => clave.ParametroProveedor).FirstOrDefault();
            ParametroGeneralInfo parametroGeneral = ObtenerParametroGeneralPorClave(claveParametro);
            if (parametroGeneral == null)
            {
                parametroGeneral = new ParametroGeneralInfo();
            }

            var poliza212 = true;
            if (string.Compare(claveParametro, ParametrosEnum.PolizaSacrificio300.ToString()
                               , StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                ParametroGeneralInfo parametro300 =
                    ObtenerParametroGeneralPorClave(ParametrosEnum.SociedadPolizaSacrificio.ToString());
                organizacion.Sociedad = parametro300.Valor;
                poliza212 = false;
            }

            datosPoliza = datosPoliza.GroupBy(grupo => new { grupo.Serie, grupo.Folio, grupo.Corral, grupo.LoteID })
                .Select(dato => new PolizaSacrificioModel
                                    {
                                        OrganizacionID = dato.Select(org => org.OrganizacionID).FirstOrDefault(),
                                        Canales = dato.Select(can => can.Canales).Sum(),
                                        Lote = dato.Select(lot => lot.Lote).FirstOrDefault(),
                                        Peso = dato.Select(peso => peso.Peso).Sum(),
                                        Fecha = dato.Select(fech => fech.Fecha).FirstOrDefault(),
                                        Folio = dato.Key.Folio,
                                        LoteID = dato.Select(lot => lot.LoteID).FirstOrDefault(),
                                        Codigo = dato.Select(cod => cod.Codigo).FirstOrDefault(),
                                        ImporteCanal = dato.Select(imp => imp.ImporteCanal).Sum(),
                                        Serie = dato.Select(ser => ser.Serie).FirstOrDefault(),
                                        ImportePiel = dato.Select(imp => imp.ImportePiel).Sum(),
                                        ImporteViscera = dato.Select(imp => imp.ImporteViscera).Sum(),
                                        Corral = dato.Key.Corral,
                                    }).ToList();
            List<PolizaInfo> polizasGeneradas;

            ParametroOrganizacionInfo parametroOrganizacion = ObtenerParametroOrganizacionPorClave(organizacionID,
                                                                                                   ParametrosEnum.
                                                                                                       CTACENTROBENEFICIOENG
                                                                                                       .ToString());
            if (poliza212)
            {
                polizasGeneradas = ObtenerPoliza212(datosPoliza, cuentasSap, proveedores,
                                                    parametroGeneral, organizacion, textoDocumento,
                                                    tipoMovimiento, postFijoRef3, parametroOrganizacion);
            }
            else
            {
                polizasGeneradas = ObtenerPoliza300(datosPoliza, cuentasSap, proveedores,
                                                    parametroGeneral, organizacion, textoDocumento,
                                                    tipoMovimiento, postFijoRef3);
            }
            polizasSacrificio.AddRange(polizasGeneradas);
            //}
            return polizasSacrificio;
        }

        /// <summary>
        /// Genera la estructura de la poliza de la 212
        /// </summary>
        /// <param name="polizaSacrificioModel"></param>
        /// <param name="cuentasSap"></param>
        /// <param name="proveedores"></param>
        /// <param name="parametroGeneral"></param>
        /// <param name="organizacion"></param>
        /// <param name="textoDocumento"></param>
        /// <param name="tipoMovimiento"></param>
        /// <param name="postFijoRef3"></param>
        /// <param name="parametroOrganizacion"> </param>
        /// <returns></returns>
        private List<PolizaInfo> ObtenerPoliza212(List<PolizaSacrificioModel> datosPoliza
                                                , IList<CuentaSAPInfo> cuentasSap
                                                , IList<ProveedorInfo> proveedores
                                                , ParametroGeneralInfo parametroGeneral
                                                , OrganizacionInfo organizacion
                                                , string textoDocumento, string tipoMovimiento
                                                , string postFijoRef3, ParametroOrganizacionInfo parametroOrganizacion)
        {
            var polizasSacrificio = new List<PolizaInfo>();
            DatosPolizaInfo datoCliente;
            bool datoClienteRegistrado = false;
            int linea = 0;
            int canales = 0;
            decimal peso = 0;
            PolizaInfo polizaSacrificioCliente = new PolizaInfo();


            var sbRef3 = new StringBuilder();
            sbRef3.Append("03");
            sbRef3.Append(
                string.Format("{0}{1}{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year).PadLeft(
                    10, ' '));
            sbRef3.Append(new Random(10).Next(10, 20));
            sbRef3.Append(new Random(30).Next(30, 40));
            sbRef3.Append(DateTime.Now.Millisecond);
            sbRef3.Append(postFijoRef3);
            PolizaSacrificioModel polizaSacrificioModel = new PolizaSacrificioModel();

            var sbArchivo = new StringBuilder(ObtenerArchivoFolio(datosPoliza.FirstOrDefault().Fecha));
            Thread.Sleep(1000);

            for (var indexSacrificio = 0; indexSacrificio < datosPoliza.Count; indexSacrificio++)
            {
                polizaSacrificioModel = datosPoliza[indexSacrificio];
                long folio = 0;
                long.TryParse(polizaSacrificioModel.Folio, out folio);
                //var numeroReferencia = new StringBuilder(string.Format("{0}{1}", polizaSacrificioModel.Folio, ObtenerNumeroReferencia));
                var numeroReferencia = ObtenerNumeroReferenciaFolio(Convert.ToInt64(folio));

                CuentaSAPInfo cuentaSAP = cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAP.Equals(ObtenerCuentaCanal));
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio("CUENTA PARA CANAL NO CONFIGURADA");
                }

                var datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = polizaSacrificioModel.Fecha,
                    Folio = polizaSacrificioModel.Folio.ToString(CultureInfo.InvariantCulture),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? polizaSacrificioModel.ImporteCanal.ToString("F2")
                                                         : (polizaSacrificioModel.ImporteCanal * -1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(++linea),
                    Ref3 = sbRef3.ToString(),
                    Cuenta = cuentaSAP.CuentaSAP,
                    CentroBeneficio = parametroOrganizacion.Valor,
                    DescripcionCosto = cuentaSAP.Descripcion,
                    Division = organizacion.Division,
                    ArchivoFolio = sbArchivo.ToString(),
                    ImporteIva = "0",
                    PesoOrigen = polizaSacrificioModel.Peso,
                    TipoDocumento = textoDocumento,
                    Concepto = String.Format("{0}-{1} {2}-{3} {4} CANALES {5}",
                                             polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                             tipoMovimiento, polizaSacrificioModel.Lote
                                             , polizaSacrificioModel.Canales
                                             , polizaSacrificioModel.Peso),
                    Sociedad = organizacion.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                PolizaInfo polizaSacrificio = GeneraRegistroPoliza(datos);
                polizaSacrificio.Corral = polizaSacrificioModel.Corral;
                polizasSacrificio.Add(polizaSacrificio);

                cuentaSAP = cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAP.Equals(ObtenerCuentaPiel));
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio("CUENTA PARA PIEL NO CONFIGURADA");
                }
                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = polizaSacrificioModel.Fecha,
                    Folio = polizaSacrificioModel.Folio.ToString(CultureInfo.InvariantCulture),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? polizaSacrificioModel.ImportePiel.ToString("F2")
                                                         : (polizaSacrificioModel.ImportePiel * -1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(++linea),
                    Ref3 = sbRef3.ToString(),
                    Cuenta = cuentaSAP.CuentaSAP,
                    ImporteIva = "0",
                    CentroBeneficio = parametroOrganizacion.Valor,
                    DescripcionCosto = cuentaSAP.Descripcion,
                    Division = organizacion.Division,
                    ArchivoFolio = sbArchivo.ToString(),
                    PesoOrigen = polizaSacrificioModel.Peso,
                    TipoDocumento = textoDocumento,
                    Concepto = String.Format("{0}-{1} {2}-{3} {4} CANALES {5}",
                                             polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                             tipoMovimiento, polizaSacrificioModel.Lote
                                             , polizaSacrificioModel.Canales
                                             , polizaSacrificioModel.Peso),
                    Sociedad = organizacion.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                polizaSacrificio = GeneraRegistroPoliza(datos);
                polizaSacrificio.Corral = polizaSacrificioModel.Corral;
                polizasSacrificio.Add(polizaSacrificio);

                cuentaSAP = cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAP.Equals(ObtenerCuentaViscera));
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio("CUENTA PARA VISCERA NO CONFIGURADA");
                }
                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = polizaSacrificioModel.Fecha,
                    Folio = polizaSacrificioModel.Folio.ToString(CultureInfo.InvariantCulture),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? polizaSacrificioModel.ImporteViscera.ToString("F2")
                                                         : (polizaSacrificioModel.ImporteViscera * -1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(++linea),
                    Ref3 = sbRef3.ToString(),
                    ImporteIva = "0",
                    Cuenta = cuentaSAP.CuentaSAP,
                    CentroBeneficio = parametroOrganizacion.Valor,
                    DescripcionCosto = cuentaSAP.Descripcion,
                    Division = organizacion.Division,
                    ArchivoFolio = sbArchivo.ToString(),
                    PesoOrigen = polizaSacrificioModel.Peso,
                    TipoDocumento = textoDocumento,
                    Concepto = String.Format("{0}-{1} {2}-{3} {4} CANALES {5}",
                                             polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                             tipoMovimiento, polizaSacrificioModel.Lote
                                             , polizaSacrificioModel.Canales
                                             , polizaSacrificioModel.Peso),
                    Sociedad = organizacion.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                polizaSacrificio = GeneraRegistroPoliza(datos);
                polizaSacrificio.Corral = polizaSacrificioModel.Corral;
                polizasSacrificio.Add(polizaSacrificio);

                ProveedorInfo proveedor = proveedores.FirstOrDefault(codigo => codigo.CodigoSAP.Equals(parametroGeneral.Valor));
                if (proveedor == null)
                {
                    throw new ExcepcionServicio(string.Format("EL PROVEEDOR CON CODIGO {0} SE ENCUENTRA INACTIVO",
                                                              parametroGeneral.Valor));
                }

                if (datoClienteRegistrado)
                {
                    polizaSacrificioCliente.Importe =
                        string.Format("{0}", Cancelacion ? (Convert.ToDecimal(polizaSacrificioCliente.Importe) + ((polizaSacrificioModel.ImporteViscera
                                                               + polizaSacrificioModel.ImporteCanal
                                                               + polizaSacrificioModel.ImportePiel) * -1)).ToString("F2")
                                                            : (Convert.ToDecimal(polizaSacrificioCliente.Importe) + (polizaSacrificioModel.ImporteViscera
                                                               + polizaSacrificioModel.ImporteCanal
                                                               + polizaSacrificioModel.ImportePiel)).ToString("F2"));

                    canales = canales + polizaSacrificioModel.Canales;
                    peso = peso + polizaSacrificioModel.Peso;
                    polizaSacrificioCliente.Concepto = String.Format("{0}-{1} {2} {3} CANALES {4} Fecha:{5}",
                                                polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                                tipoMovimiento
                                                , canales
                                                , peso, polizaSacrificioModel.Fecha.ToString("yyyyMMdd"));
                    
                }
                else
                {
                    datoCliente = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia,
                        FechaEntrada = polizaSacrificioModel.Fecha,
                        Folio = polizaSacrificioModel.Folio.ToString(CultureInfo.InvariantCulture),
                        ClaseDocumento = postFijoRef3,
                        Importe =
                            string.Format("{0}", Cancelacion ? ((polizaSacrificioModel.ImporteViscera
                                                                   + polizaSacrificioModel.ImporteCanal
                                                                   + polizaSacrificioModel.ImportePiel) * -1).ToString("F2")
                                                                : (polizaSacrificioModel.ImporteViscera
                                                                   + polizaSacrificioModel.ImporteCanal
                                                                   + polizaSacrificioModel.ImportePiel).ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        //Renglon = Convert.ToString(++linea),
                        Ref3 = sbRef3.ToString(),
                        Cliente = proveedor.CodigoSAP,
                        DescripcionCosto = proveedor.Descripcion,
                        Division = organizacion.Division,
                        ArchivoFolio = sbArchivo.ToString(),
                        PesoOrigen = polizaSacrificioModel.Peso,
                        ImporteIva = "0",
                        TipoDocumento = textoDocumento,
                        Concepto = String.Format("{0}-{1} {2} {3} CANALES {4} Fecha:{5}",
                                                 polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                                 tipoMovimiento
                                                 , polizaSacrificioModel.Canales
                                                 , polizaSacrificioModel.Peso, polizaSacrificioModel.Fecha.ToString("yyyyMMdd")),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    datoClienteRegistrado = true;
                    polizaSacrificioCliente = GeneraRegistroPoliza(datoCliente);
                    peso = polizaSacrificioModel.Peso;
                    canales = polizaSacrificioModel.Canales;
                }
            }
            polizaSacrificioCliente.NumeroLinea = (++linea).ToString();
            polizaSacrificioCliente.Corral = "A";
            polizasSacrificio.Add(polizaSacrificioCliente);

            return polizasSacrificio;
        }

        /// <summary>
        /// Genera la estructura de la poliza de la 300
        /// </summary>
        /// <param name="polizaSacrificioModel"></param>
        /// <param name="cuentasSap"></param>
        /// <param name="proveedores"></param>
        /// <param name="parametroGeneral"></param>
        /// <param name="organizacion"></param>
        /// <param name="textoDocumento"></param>
        /// <param name="tipoMovimiento"></param>
        /// <param name="postFijoRef3"></param>
        /// <returns></returns>
        private List<PolizaInfo> ObtenerPoliza300(List<PolizaSacrificioModel> datosPoliza
                                        , IList<CuentaSAPInfo> cuentasSap
                                        , IList<ProveedorInfo> proveedores
                                        , ParametroGeneralInfo parametroGeneral
                                        , OrganizacionInfo organizacion
                                        , string textoDocumento, string tipoMovimiento
                                        , string postFijoRef3)
        {
            var polizasSacrificio = new List<PolizaInfo>();
            decimal importe = 0;
            int linea = 0;

            var sbRef3 = new StringBuilder();
            sbRef3.Append("03");
            sbRef3.Append(
                string.Format("{0}{1}{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year).PadLeft(
                    10, ' '));
            sbRef3.Append(new Random(10).Next(10, 20));
            sbRef3.Append(new Random(30).Next(30, 40));
            sbRef3.Append(DateTime.Now.Millisecond);
            sbRef3.Append(postFijoRef3);

            var sbArchivo = new StringBuilder(ObtenerArchivoFolio(datosPoliza.FirstOrDefault().Fecha));
            Thread.Sleep(1000);

            PolizaSacrificioModel polizaSacrificioModel = new PolizaSacrificioModel();

            datosPoliza = datosPoliza.GroupBy(grupo => new { grupo.Serie, grupo.Folio })
                .Select(dato => new PolizaSacrificioModel
                {
                    OrganizacionID = dato.Select(org => org.OrganizacionID).FirstOrDefault(),
                    Canales = dato.Select(can => can.Canales).Sum(),
                    //Lote = dato.Select(lot => lot.Lote).FirstOrDefault(),
                    Peso = dato.Select(peso => peso.Peso).Sum(),
                    Fecha = dato.Select(fech => fech.Fecha).FirstOrDefault(),
                    Folio = dato.Key.Folio,
                    //LoteID = dato.Select(lot => lot.LoteID).FirstOrDefault(),
                    //Codigo = dato.Select(cod => cod.Codigo).FirstOrDefault(),
                    ImporteCanal = dato.Select(imp => imp.ImporteCanal).Sum(),
                    Serie = dato.Key.Serie,
                    ImportePiel = dato.Select(imp => imp.ImportePiel).Sum(),
                    ImporteViscera = dato.Select(imp => imp.ImporteViscera).Sum()
                }).ToList();


            for (var indexSacrificio = 0; indexSacrificio < datosPoliza.Count; indexSacrificio++)
            {
                polizaSacrificioModel = datosPoliza[indexSacrificio];

                importe = polizaSacrificioModel.ImporteViscera +
                                  polizaSacrificioModel.ImporteCanal +
                                  polizaSacrificioModel.ImportePiel;


                //var numeroReferencia = new StringBuilder(string.Format("{0}{1}", polizaSacrificioModel.Folio, ObtenerNumeroReferencia));
                var numeroReferencia = ObtenerNumeroReferenciaFolio(Convert.ToInt64(polizaSacrificioModel.Folio));

                CuentaSAPInfo cuentaSAP = cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAP.Equals(ObtenerCuentaPoliza300));
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio("CUENTA PARA CANAL NO CONFIGURADA");
                }
                var datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = polizaSacrificioModel.Fecha,
                    Folio = polizaSacrificioModel.Folio.ToString(CultureInfo.InvariantCulture),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? (importe * -1).ToString("F2")
                                                         : importe.ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(++linea),
                    ImporteIva = "0",
                    Ref3 = sbRef3.ToString(),
                    Cuenta = cuentaSAP.CuentaSAP,
                    Segmento = "S300",
                    DescripcionCosto = cuentaSAP.Descripcion,
                    Division = organizacion.Division,
                    ArchivoFolio = sbArchivo.ToString(),
                    PesoOrigen = polizaSacrificioModel.Peso,
                    TipoDocumento = textoDocumento,
                    Concepto = String.Format("{0}-{1} {2} {3} CANALES {4} Fecha: {5}",
                                             polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                             tipoMovimiento
                                             , polizaSacrificioModel.Canales
                                             , polizaSacrificioModel.Peso
                                             , polizaSacrificioModel.Fecha.ToString("yyyyMMdd")),
                    Sociedad = organizacion.Sociedad,
                    ArchivoEnviadoServidor = 1
                };
                PolizaInfo polizaSacrificio = GeneraRegistroPoliza(datos);
                polizasSacrificio.Add(polizaSacrificio);

                ProveedorInfo proveedor = proveedores.FirstOrDefault(codigo => codigo.CodigoSAP.Equals(parametroGeneral.Valor));
                if (proveedor == null)
                {
                    throw new ExcepcionServicio(string.Format("EL PROVEEDOR CON CODIGO {0} SE ENCUENTRA INACTIVO",
                                                              parametroGeneral.Valor));
                }
                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = polizaSacrificioModel.Fecha,
                    Folio = polizaSacrificioModel.Folio.ToString(CultureInfo.InvariantCulture),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? (importe).ToString("F2")
                                                         : (importe * -1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(++linea),
                    Ref3 = sbRef3.ToString(),
                    ImporteIva = "0",
                    ClaveProveedor = proveedor.CodigoSAP,
                    DescripcionCosto = proveedor.Descripcion,
                    Division = organizacion.Division,
                    ArchivoFolio = sbArchivo.ToString(),
                    PesoOrigen = polizaSacrificioModel.Peso,
                    TipoDocumento = textoDocumento,
                    Concepto = String.Format("{0}-{1} {2} {3} CANALES {4} Fecha: {5}",
                                             polizaSacrificioModel.Serie, polizaSacrificioModel.Folio,
                                             tipoMovimiento
                                             , polizaSacrificioModel.Canales
                                             , polizaSacrificioModel.Peso
                                             , polizaSacrificioModel.Fecha.ToString("yyyyMMdd")),
                    Sociedad = organizacion.Sociedad,
                    ArchivoEnviadoServidor = 1,
                    Segmento = "S300",
                };
                polizaSacrificio = GeneraRegistroPoliza(datos);
                polizasSacrificio.Add(polizaSacrificio);
            }
            return polizasSacrificio;
        }

        #endregion METODOS PRIVADOS
    }
}
