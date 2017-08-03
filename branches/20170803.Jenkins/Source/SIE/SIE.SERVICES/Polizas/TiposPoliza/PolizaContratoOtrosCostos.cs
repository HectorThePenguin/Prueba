using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaContratoOtrosCostos : PolizaAbstract
    {
        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var otrosCostos = datosPoliza as PolizaContratoModel;
            IList<PolizaInfo> polizas = ObtenerPoliza(otrosCostos);
            return polizas;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(PolizaContratoModel datosContrato)
        {
            var polizasContrato = new List<PolizaInfo>();

            long folioPedido = datosContrato.AlmacenMovimiento.FolioMovimiento;
            int organizacionID = datosContrato.Contrato.Organizacion.OrganizacionID;
            DateTime fechaPedido = datosContrato.Contrato.Fecha;

            int miliSegunda = DateTime.Now.Millisecond;
            string archivoFolio = ObtenerArchivoFolio(fechaPedido);

            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(
                    clave => clave.TipoPolizaID == TipoPoliza.PolizaContratoOtrosCostos.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.PolizaContratoOtrosCostos));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            PolizaInfo poliza;
            var renglon = 0;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(folioPedido).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(miliSegunda);
            ref3.Append(postFijoRef3);

            //string numeroDocumento = ObtenerNumeroReferencia;
            string numeroDocumento = ObtenerNumeroReferenciaFolio(datosContrato.AlmacenMovimiento.FolioMovimiento);

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            List<CostoInfo> costos = datosContrato.OtrosCostos;
            CostoInfo costo;
            IList<UnidadMedicionInfo> unidadesMedicion = ObtenerUnidadesMedicion();
            UnidadMedicionInfo unidadMedicion;
            IList<ProductoInfo> productos = ObtenerProductos();
            ProductoInfo producto;
            DatosPolizaInfo datos;
            IList<int> costosConRetencion = costos.Select(cos => cos.CostoID).ToList();
            for (var indexCostos = 0; indexCostos < costos.Count; indexCostos++)
            {
                costo = costos[indexCostos];
                bool esProveedor = costo.Proveedor != null && !string.IsNullOrWhiteSpace(costo.Proveedor.CodigoSAP);
                bool tieneIva = costo.AplicaIva;
                bool tieneRetencion = costo.AplicaRetencion;

                fechaPedido = costo.FechaCosto != DateTime.MinValue
                                  ? costo.FechaCosto
                                  : fechaPedido;

                producto = productos.FirstOrDefault(id => id.ProductoId == datosContrato.Contrato.Producto.ProductoId);
                unidadMedicion =
                    unidadesMedicion.FirstOrDefault(uni => uni.UnidadID == producto.UnidadId);
                #region Es Proveedor
                if (esProveedor)
                {
                    #region Sin IVA ni Retencion
                    if (!tieneIva && !tieneRetencion)
                    {
                        renglon++;
                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = fechaPedido,
                            Folio = folioPedido.ToString(),
                            Importe = string.Format("{0}", costo.ImporteCosto.ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = datosContrato.Contrato.Cuenta.CuentaSAP,
                            ArchivoFolio = archivoFolio,
                            DescripcionCosto = datosContrato.Contrato.Cuenta.Descripcion,
                            PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                            Division = organizacion.Division,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                        tipoMovimiento,
                                        folioPedido,
                                        costo.ToneladasCosto.ToString("N0"),
                                        unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                        costo.ImporteCosto.ToString("C2"), postFijoRef3),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        poliza = GeneraRegistroPoliza(datos);
                        polizasContrato.Add(poliza);

                        renglon++;
                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = fechaPedido,
                            Folio = folioPedido.ToString(),
                            ClaveProveedor = costo.Proveedor.CodigoSAP,
                            Importe = string.Format("{0}", (costo.ImporteCosto*-1).ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            Division = organizacion.Division,
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            ArchivoFolio = archivoFolio,
                            DescripcionCosto = costo.Proveedor.Descripcion,
                            PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                            ClaseDocumento = postFijoRef3,
                            TipoDocumento = textoDocumento,
                            Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                       tipoMovimiento,
                                       folioPedido,
                                       costo.ToneladasCosto.ToString("N0"),
                                       unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                       costo.ImporteCosto.ToString("C2"), postFijoRef3),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        poliza = GeneraRegistroPoliza(datos);
                        polizasContrato.Add(poliza);
                    }
                    #endregion Sin IVA ni Retencion
                    else
                    {
                        #region Con IVA

                        if (tieneIva)
                        {
                            CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                                clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                            if (cuentaIva == null)
                            {
                                cuentaIva = new CuentaSAPInfo { Descripcion = string.Empty };
                            }
                            renglon++;
                            var importeIva = (costo.ImporteCosto) * (organizacion.Iva.TasaIva / 100);
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = fechaPedido,
                                Folio = folioPedido.ToString(),
                                Division = organizacion.Division,
                                ClaveProveedor = String.Empty,
                                Importe = string.Format("{0}", costo.ImporteCosto.ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = datosContrato.Contrato.Cuenta.CuentaSAP,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = producto.Descripcion,
                                PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                        tipoMovimiento,
                                        folioPedido,
                                        costo.ToneladasCosto.ToString("N0"),
                                        unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                        costo.ImporteCosto.ToString("C2"), postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizasContrato.Add(poliza);

                            renglon++;
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = fechaPedido,
                                Folio = folioPedido.ToString(),
                                ClaveProveedor = String.Empty,
                                Importe = string.Format("{0}", importeIva.ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = costo.ImporteCosto.ToString("F2"),
                                IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                                ClaveImpuesto = ClaveImpuesto,
                                CondicionImpuesto = CondicionImpuesto,
                                Ref3 = ref3.ToString(),
                                Division = organizacion.Division,
                                Cuenta = organizacion.Iva.CuentaRecuperar.ClaveCuenta,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaIva.Descripcion,
                                PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                        tipoMovimiento,
                                        folioPedido,
                                        costo.ToneladasCosto.ToString("N0"),
                                        unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                        costo.ImporteCosto.ToString("C2"), postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizasContrato.Add(poliza);

                            renglon++;

                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento.ToString(),
                                FechaEntrada = fechaPedido,
                                Folio = folioPedido.ToString(),
                                Division = organizacion.Division,
                                ClaveProveedor = costo.Proveedor.CodigoSAP,
                                Importe = string.Format("{0}", ((costo.ImporteCosto + importeIva)*-1).ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = costo.Proveedor.Descripcion,
                                PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                       tipoMovimiento,
                                       folioPedido,
                                       costo.ToneladasCosto.ToString("N0"),
                                       unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                       costo.ImporteCosto.ToString("C2"), postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizasContrato.Add(poliza);
                        }
                        #endregion Con IVA

                        #region Con Retencion

                        if (tieneRetencion)
                        {
                            var retencionBL = new RetencionBL();
                            var retenciones = retencionBL.ObtenerRetencionesConCosto(costosConRetencion);
                            RetencionInfo retencion = null;
                            if (retenciones != null && retenciones.Any())
                            {
                                retencion =
                                    retenciones.FirstOrDefault(
                                        costoRet => costoRet.CostoID == costo.CostoID);
                            }
                            if (retencion != null)
                            {
                                var parametrosRetencion = new StringBuilder();
                                parametrosRetencion.Append(String.Format("{0}{1}"
                                                                         , retencion.IndicadorRetencion
                                                                         , retencion.TipoRetencion));
                                datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroDocumento,
                                    FechaEntrada = fechaPedido,
                                    Folio = folioPedido.ToString(CultureInfo.InvariantCulture),
                                    Division = organizacion.Division,
                                    ClaveProveedor = costo.Proveedor.CodigoSAP,
                                    Importe = string.Format("-{0}", "0"),
                                    IndicadorImpuesto = parametrosRetencion.ToString(),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    CodigoRetencion = retencion.IndicadorImpuesto,
                                    TipoRetencion = retencion.IndicadorRetencion,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = costo.Proveedor.Descripcion,
                                    PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                                    TipoDocumento = textoDocumento,
                                    ClaseDocumento = postFijoRef3,
                                    Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                      tipoMovimiento,
                                      folioPedido,
                                      costo.ToneladasCosto.ToString("N0"),
                                      unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                      costo.ImporteCosto.ToString("C2"), postFijoRef3),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                                poliza = GeneraRegistroPoliza(datos);
                                polizasContrato.Add(poliza);
                                if (!tieneIva)
                                {
                                    renglon++;
                                    datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = fechaPedido,
                                        Folio = folioPedido.ToString(CultureInfo.InvariantCulture),
                                        Division = organizacion.Division,
                                        ClaveProveedor = costo.Proveedor.CodigoSAP,
                                        Importe = string.Format("{0}", (costo.ImporteCosto*-1).ToString("F2")),
                                        Renglon = Convert.ToString(renglon),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = costo.Proveedor.Descripcion,
                                        PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                                        TipoDocumento = textoDocumento,
                                        ClaseDocumento = postFijoRef3,
                                        Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                            tipoMovimiento,
                                            folioPedido,
                                            costo.ToneladasCosto.ToString("N0"),
                                            unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                            costo.ImporteCosto.ToString("C2"), postFijoRef3),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                                    poliza = GeneraRegistroPoliza(datos);
                                    polizasContrato.Add(poliza);
                                    renglon++;
                                    datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = fechaPedido,
                                        Folio = folioPedido.ToString(CultureInfo.InvariantCulture),
                                        Division = organizacion.Division,
                                        ClaveProveedor = String.Empty,
                                        Importe = string.Format("{0}", costo.ImporteCosto.ToString("F2")),
                                        IndicadorImpuesto = String.Empty,
                                        Renglon = Convert.ToString(renglon),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        Cuenta = datosContrato.Contrato.Cuenta.CuentaSAP,
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = datosContrato.Contrato.Cuenta.Descripcion,
                                        PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                                        TipoDocumento = textoDocumento,
                                        ClaseDocumento = postFijoRef3,
                                        ComplementoRef1 = string.Empty,
                                        Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                             tipoMovimiento,
                                             folioPedido,
                                             costo.ToneladasCosto.ToString("N0"),
                                             unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                             costo.ImporteCosto.ToString("C2"), postFijoRef3),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                                    poliza = GeneraRegistroPoliza(datos);
                                    polizasContrato.Add(poliza);
                                }
                            }
                        }

                        #endregion Con Retencion
                    }
                }
                #endregion Es Proveedor

                #region Es Cuenta
                else
                {
                    renglon++;
                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = fechaPedido,
                        Folio = folioPedido.ToString(CultureInfo.InvariantCulture),
                        Division = organizacion.Division,
                        ClaveProveedor = String.Empty,
                        Importe = string.Format("{0}", (costo.ImporteCosto*-1).ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(renglon),
                        Cabezas = string.Empty,
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = costo.CuentaSap.CuentaSAP,
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = costo.CuentaSap.Descripcion,
                        PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                       tipoMovimiento,
                                       folioPedido,
                                       costo.ToneladasCosto.ToString("N0"),
                                       unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                       costo.ImporteCosto.ToString("C2"), postFijoRef3),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    poliza = GeneraRegistroPoliza(datos);
                    polizasContrato.Add(poliza);

                    renglon++;
                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = fechaPedido,
                        Folio = folioPedido.ToString(CultureInfo.InvariantCulture),
                        Division = organizacion.Division,
                        Importe = string.Format("{0}", costo.ImporteCosto.ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(renglon),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = datosContrato.Contrato.Cuenta.CuentaSAP,
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = producto.Descripcion,
                        PesoOrigen = Math.Round(Convert.ToDecimal(datosContrato.Contrato.Cantidad), 0),
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        Concepto = String.Format("{0}-{1} {2} {3} {4} {5} {6}",
                                       tipoMovimiento,
                                       folioPedido,
                                       costo.ToneladasCosto.ToString("N0"),
                                       unidadMedicion.ClaveUnidad, string.IsNullOrWhiteSpace(producto.Descripcion) ? producto.ProductoDescripcion : producto.Descripcion,
                                       costo.ImporteCosto.ToString("C2"), postFijoRef3),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    poliza = GeneraRegistroPoliza(datos);
                    polizasContrato.Add(poliza);
                }
                #endregion Es Cuenta
            }
            return polizasContrato;
        }

        #endregion METODOS PRIVADOS
    }
}
