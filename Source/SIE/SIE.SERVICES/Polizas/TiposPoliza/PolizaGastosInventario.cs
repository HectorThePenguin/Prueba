using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaGastosInventario : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;
        private string formulaGenerada;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.GastosInventario);

                var almacenesInventarioLote = datosPoliza as List<AlmacenInventarioLoteInfo>;

                IList<FormulaInfo> formulas = ObtenerFormulas();

                AlmacenInventarioLoteInfo almacenFormula =
                    almacenesInventarioLote.FirstOrDefault(
                        alm =>
                        alm.AlmacenInventario.Producto.SubfamiliaId == SubFamiliasEnum.AlimentoFormulado.GetHashCode());
                if (almacenFormula != null)
                {
                    FormulaInfo formulaProducida =
                        formulas.FirstOrDefault(
                            form => form.Producto.ProductoId == almacenFormula.AlmacenInventario.Producto.ProductoId);

                    if (formulaProducida != null)
                    {
                        formulaGenerada = formulaProducida.Descripcion;
                    }
                }

                if (almacenesInventarioLote == null)
                {
                    return null;
                }

                var primerAlmacenInventarioLote = almacenesInventarioLote.FirstOrDefault();
                if (primerAlmacenInventarioLote == null)
                {
                    return null;
                }

                long folioVenta = 1; //almacenInventarioLote.Select(folio => folio.AnimalID).FirstOrDefault();
                int organizacionID = primerAlmacenInventarioLote.AlmacenInventario.Almacen.Organizacion.OrganizacionID;
                DateTime fechaVenta = primerAlmacenInventarioLote.FechaProduccionFormula;

                OrganizacionInfo organizacionOrigen = ObtenerOrganizacionIVA(organizacionID);
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = organizacionOrigen.Descripcion,
										  Desplazamiento = 0
									  },
                                      new PolizaEncabezadoModel
									  {
										  Descripcion = "Por Producción",
										  Desplazamiento = 0
									  }
							  };
                polizaImpresion.GeneraCabecero(new[] { "100", "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = "Nota de Salida de almacen",
										  Desplazamiento = 0
									  },
                                        new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0} {1}", "FOLIO No.",
															folioVenta),
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "100", "100" }, "NombreGanadera");
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = "CARGO A:",
										  Desplazamiento = 0
									  },
                                      new PolizaEncabezadoModel
									  {
										   Descripcion =
											  string.Format("{0}:{1}", "FECHA",
															fechaVenta.ToShortDateString()),
									  },
								
							  };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
                                  new PolizaEncabezadoModel
									  {
										  Descripcion = "TRANSPORTE:"
									  },
								  new PolizaEncabezadoModel
									  {
										  Descripcion = "REFERENCIA:"
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");


                GeneraLineaEncabezadoDetalle();

                GeneraLineasDetalle(almacenesInventarioLote);
                GeneraLinea(9);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");


                GeneraLineaTotales(almacenesInventarioLote.Where(alm => alm.AlmacenInventario.Producto.SubfamiliaId != SubFamiliasEnum.AlimentoFormulado.GetHashCode()));
                GeneraLinea(9);
                polizaImpresion.GenerarDetalles("Detalle");


                GeneraLinea(5);
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(folioVenta);
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                GeneraLineaSubEncabezadoRegistroContable(true, "Codigo", "Debe", "Abono");
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                IList<PolizaInfo> cargos;
                IList<PolizaInfo> abonos;
                GeneraLineaRegistroContable(polizas, out cargos, out abonos);
                polizaImpresion.GenerarRegistroContable("RegistroContable");

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");
                GenerarLineaSumaRegistroContable(polizas, "Financiero");
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");


                polizaImpresion.GenerarLineaEnBlanco("RegistroContable", 5);
                polizaImpresion.GenerarLineaEnBlanco("RegistroContable", 5);
                polizaImpresion.GenerarLineaEnBlanco("RegistroContable", 5);
                GenerarLineaRevisoRecibio();
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25" }, "RegistroContable");


                return polizaImpresion.GenerarArchivo();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var gastoInventario = datosPoliza as GastoInventarioInfo;
            IList<PolizaInfo> poliza = ObtenerPoliza(gastoInventario);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        /// <summary>
        /// Genera los totales por Detalle
        /// </summary>
        /// <param name="detalles"></param>
        private void GeneraLineaTotales(IEnumerable<AlmacenInventarioLoteInfo> detalles)
        {
            var sumaImporte = Math.Round(detalles.Sum(importe => importe.Importe), 2);

            PolizaModel.Detalle = new List<PolizaDetalleModel>();

            var detalleModel = new PolizaDetalleModel
            {
                CantidadCabezas = string.Empty,
                PesoPromedio = string.Empty,
                TipoGanado = string.Empty,
                Lote = string.Empty,
                PesoTotal = string.Empty,
                ImportePromedio = sumaImporte.ToString("N2", CultureInfo.CurrentCulture),
                PrecioVenta = string.Empty,
                ImporteVenta = string.Empty,
                Corral = string.Empty
            };

            PolizaModel.Detalle.Add(detalleModel);
        }

        #region Poliza XML
        private IList<PolizaInfo> ObtenerPoliza(GastoInventarioInfo gastoInventario)
        {
            var polizasGastosInventario = new List<PolizaInfo>();

            var costos = new List<int> { gastoInventario.Costo.CostoID };
            var retencionBL = new RetencionBL();
            var retenciones = retencionBL.ObtenerRetencionesConCosto(costos);
            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.GastosInventario.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.GastosInventario));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(
                string.Format("{0}{1}{2}", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year).PadLeft(
                    10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(DateTime.Now.Millisecond);
            ref3.Append(postFijoRef3);

            //string numeroReferencia = ObtenerNumeroReferencia;
            string numeroReferencia = ObtenerNumeroReferenciaFolio(gastoInventario.FolioGasto);

            DateTime fecha = gastoInventario.FechaGasto;
            string archivoFolio = ObtenerArchivoFolio(fecha);

            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(gastoInventario.Organizacion.OrganizacionID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }

            bool esProveedor = gastoInventario.Proveedor != null && gastoInventario.Proveedor.ProveedorID > 0;
            bool tieneRetencion = gastoInventario.Retencion;
            var renglon = 0;

            ClaveContableInfo claveContableInfo = ObtenerCuentaInventario(gastoInventario.Costo
                                                               , gastoInventario.Organizacion.OrganizacionID
                                                               , 0);

            CuentaSAPInfo cuentaSapProvision = null;
            if (!esProveedor)
            {
                cuentaSapProvision =
                    cuentasSap.FirstOrDefault(
                        clave => clave.CuentaSAPID == gastoInventario.CuentaSAP.CuentaSAPID);
                if (cuentaSapProvision == null)
                {
                    cuentaSapProvision =
                        cuentasSap.FirstOrDefault(
                            clave =>
                            clave.CuentaSAP.Equals(gastoInventario.CuentaGasto,
                                                   StringComparison.InvariantCultureIgnoreCase));

                    if (cuentaSapProvision == null)
                    {
                        throw new ExcepcionServicio(
                            string.Format("No se encuentra configurada la cuenta de provisión, para el costo {0}",
                                          gastoInventario.Costo.Descripcion));
                    }
                }
            }
            string complementoConcepto;
            if (gastoInventario.Corral != null)
            {
                complementoConcepto = string.Format("Corral {0}", gastoInventario.Corral.Codigo);
            }
            else
            {
                complementoConcepto = string.Format("{0} Corrales", gastoInventario.TotalCorrales);
            }
            PolizaInfo polizaEntrada;
            if (esProveedor)
            {
                if (!gastoInventario.IVA && !tieneRetencion)
                {
                    renglon++;
                    var datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroReferencia,
                                        FechaEntrada = gastoInventario.FechaGasto,
                                        Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                        ClaseDocumento = postFijoRef3,
                                        Importe =
                                            string.Format("{0}",
                                                          Cancelacion ? (gastoInventario.Importe* -1).ToString("F2")
                                                                      : gastoInventario.Importe.ToString("F2")),
                                        Renglon = Convert.ToString(renglon),
                                        Division = organizacion.Division,
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        Cuenta = claveContableInfo.Valor,
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = claveContableInfo.Descripcion,
                                        PesoOrigen = 0,
                                        TipoDocumento = textoDocumento,
                                        Concepto =
                                            String.Format("{0}-{1},{2}", tipoMovimiento,
                                                          gastoInventario.FolioGasto
                                                          , complementoConcepto),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                    polizaEntrada = GeneraRegistroPoliza(datos);
                    polizasGastosInventario.Add(polizaEntrada);

                    renglon++;
                    datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoInventario.FechaGasto,
                                    Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                    ClaveProveedor = gastoInventario.Proveedor.CodigoSAP,
                                    ClaseDocumento = postFijoRef3,
                                    Importe =
                                        string.Format("{0}", Cancelacion ? gastoInventario.Importe.ToString("F2") 
                                                                         : (gastoInventario.Importe * -1).ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = gastoInventario.Proveedor.Descripcion,
                                    PesoOrigen = 0,
                                    Division = organizacion.Division,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1},{2}", tipoMovimiento,
                                                      gastoInventario.FolioGasto
                                                      , complementoConcepto),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                    polizaEntrada = GeneraRegistroPoliza(datos);
                    polizasGastosInventario.Add(polizaEntrada);
                }
                else
                {
                    if (gastoInventario.IVA)
                    {
                        CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                            clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                        if (cuentaIva == null)
                        {
                            throw new ExcepcionServicio(string.Format("No se encuentra configurada la cuenta de iva, para la organización."));
                        }
                        renglon++;
                        var importeIva = gastoInventario.Importe * (organizacion.Iva.TasaIva / 100);
                        var datos = new DatosPolizaInfo
                                        {
                                            NumeroReferencia = numeroReferencia,
                                            FechaEntrada = gastoInventario.FechaGasto,
                                            Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                            Division = organizacion.Division,
                                            ClaseDocumento = postFijoRef3,
                                            Importe = string.Format("{0}",
                                                                    Cancelacion ? (gastoInventario.Importe* -1).ToString("F2") 
                                                                                : gastoInventario.Importe.ToString("F2")),
                                            Renglon = Convert.ToString(renglon),
                                            ImporteIva = "0",
                                            Ref3 = ref3.ToString(),
                                            Cuenta = claveContableInfo.Valor,
                                            ArchivoFolio = archivoFolio,
                                            DescripcionCosto = claveContableInfo.Descripcion,
                                            PesoOrigen = 0,
                                            TipoDocumento = textoDocumento,
                                            Concepto =
                                                String.Format("{0}-{1},{2}", tipoMovimiento,
                                                              gastoInventario.FolioGasto
                                                              , complementoConcepto),
                                            Sociedad = organizacion.Sociedad,
                                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                        };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosInventario.Add(polizaEntrada);
                        renglon++;
                        datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroReferencia,
                                        FechaEntrada = gastoInventario.FechaGasto,
                                        Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                        ClaseDocumento = postFijoRef3,
                                        Importe = string.Format("{0}",
                                                                Cancelacion ? (importeIva * -1).ToString("F2") 
                                                                            : importeIva.ToString("F2"))
                                        ,
                                        Renglon = Convert.ToString(renglon),
                                        ImporteIva = gastoInventario.Importe.ToString("F2"),
                                        ClaveImpuesto = ClaveImpuesto,
                                        CondicionImpuesto = CondicionImpuesto,
                                        IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                                        Ref3 = ref3.ToString(),
                                        Division = organizacion.Division,
                                        Cuenta = organizacion.Iva.CuentaRecuperar.ClaveCuenta,
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = cuentaIva.Descripcion,
                                        PesoOrigen = 0,
                                        TipoDocumento = textoDocumento,
                                        Concepto =
                                            String.Format("{0}-{1},{2}", tipoMovimiento,
                                                          gastoInventario.FolioGasto
                                                          , complementoConcepto),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosInventario.Add(polizaEntrada);
                        renglon++;
                        datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroReferencia,
                                        FechaEntrada = gastoInventario.FechaGasto,
                                        Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                        Division = organizacion.Division,
                                        ClaveProveedor = gastoInventario.Proveedor.CodigoSAP,
                                        ClaseDocumento = postFijoRef3,
                                        Importe = string.Format("{0}",
                                                                Cancelacion ? (gastoInventario.Importe + importeIva).ToString("F2") 
                                                                            : ((gastoInventario.Importe + importeIva) * -1).ToString("F2")),
                                        Renglon = Convert.ToString(renglon),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        ArchivoFolio = archivoFolio,
                                        DescripcionCosto = gastoInventario.Proveedor.Descripcion,
                                        PesoOrigen = 0,
                                        TipoDocumento = textoDocumento,
                                        Concepto =
                                            String.Format("{0}-{1},{2}", tipoMovimiento,
                                                          gastoInventario.FolioGasto
                                                         , complementoConcepto),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosInventario.Add(polizaEntrada);
                    }
                    if (tieneRetencion)
                    {
                        RetencionInfo retencion;
                        if (retenciones != null && retenciones.Any())
                        {
                            retencion =
                                retenciones.Where(
                                    costo => costo.CostoID.Equals(gastoInventario.Costo.CostoID)).
                                    Select(ret => ret).FirstOrDefault();

                            if (retencion == null)
                            {
                                throw new ExcepcionServicio(string.Format("No se encuentra configurada retención para el costo {0}", gastoInventario.Costo.Descripcion));
                            }
                        }
                        else
                        {
                            throw new ExcepcionServicio(string.Format("No se encuentra configurada retención para el costo {0}", gastoInventario.Costo.Descripcion));
                        }
                        var parametrosRetencion = new StringBuilder();
                        parametrosRetencion.Append(String.Format("{0}{1}"
                                                                 , retencion.IndicadorRetencion
                                                                 , retencion.TipoRetencion));
                        var datos = new DatosPolizaInfo
                                        {
                                            NumeroReferencia = numeroReferencia,
                                            FechaEntrada = gastoInventario.FechaGasto,
                                            Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                            Division = organizacion.Division,
                                            ClaveProveedor = gastoInventario.Proveedor.CodigoSAP,
                                            ClaseDocumento = postFijoRef3,
                                            IndicadorImpuesto = parametrosRetencion.ToString(),
                                            Importe = string.Format("{0}{1}",
                                                                    Cancelacion ? string.Empty : "-",
                                                                    "0"),
                                            Renglon = Convert.ToString(renglon),
                                            ImporteIva = "0",
                                            Ref3 = ref3.ToString(),
                                            CodigoRetencion = retencion.IndicadorImpuesto,
                                            TipoRetencion = retencion.IndicadorRetencion,
                                            ArchivoFolio = archivoFolio,
                                            DescripcionCosto = gastoInventario.Proveedor.Descripcion,
                                            PesoOrigen = 0,
                                            TipoDocumento = textoDocumento,
                                            Concepto =
                                                String.Format("{0}-{1},{2}", tipoMovimiento,
                                                              gastoInventario.FolioGasto
                                                             , complementoConcepto),
                                            Sociedad = organizacion.Sociedad,
                                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                        };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosInventario.Add(polizaEntrada);
                        if (!gastoInventario.IVA)
                        {
                            renglon++;
                            datos = new DatosPolizaInfo
                                        {
                                            NumeroReferencia = numeroReferencia,
                                            FechaEntrada = gastoInventario.FechaGasto,
                                            Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                            Division = organizacion.Division,
                                            ClaveProveedor = gastoInventario.Proveedor.CodigoSAP,
                                            ClaseDocumento = postFijoRef3,
                                            Importe = string.Format("{0}",
                                                                    Cancelacion
                                                                        ? gastoInventario.Importe.ToString("F2")
                                                                        : (gastoInventario.Importe*-1).ToString("F2"))
                                            ,
                                            Renglon = Convert.ToString(renglon),
                                            ImporteIva = "0",
                                            Ref3 = ref3.ToString(),
                                            ArchivoFolio = archivoFolio,
                                            DescripcionCosto = gastoInventario.Proveedor.Descripcion,
                                            PesoOrigen = 0,
                                            TipoDocumento = textoDocumento,
                                            Concepto =
                                                String.Format("{0}-{1},{2}", tipoMovimiento,
                                                              gastoInventario.FolioGasto
                                                              , complementoConcepto),
                                            Sociedad = organizacion.Sociedad,
                                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                        };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosInventario.Add(polizaEntrada);
                            renglon++;
                            datos = new DatosPolizaInfo
                                        {
                                            NumeroReferencia = numeroReferencia,
                                            FechaEntrada = gastoInventario.FechaGasto,
                                            Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                            Division = organizacion.Division,
                                            ClaseDocumento = postFijoRef3,
                                            Importe = string.Format("{0}",
                                                                    Cancelacion
                                                                        ? (gastoInventario.Importe * -1).ToString("F2")
                                                                        : gastoInventario.Importe.ToString("F2")),
                                            Renglon = Convert.ToString(renglon),
                                            ImporteIva = "0",
                                            Ref3 = ref3.ToString(),
                                            Cuenta = claveContableInfo.Valor,
                                            ArchivoFolio = archivoFolio,
                                            DescripcionCosto = claveContableInfo.Descripcion,
                                            PesoOrigen = 0,
                                            TipoDocumento = textoDocumento,
                                            Concepto =
                                                String.Format("{0}-{1},{2}", tipoMovimiento,
                                                              gastoInventario.FolioGasto
                                                             , complementoConcepto),
                                            Sociedad = organizacion.Sociedad,
                                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                        };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosInventario.Add(polizaEntrada);
                        }
                    }
                }
            }
            else
            {
                renglon++;
                var datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoInventario.FechaGasto,
                                    Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                    Division = organizacion.Division,
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}", Cancelacion ? (gastoInventario.Importe*-1).ToString("F2") 
                                                                               : gastoInventario.Importe.ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = claveContableInfo.Valor,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = claveContableInfo.Descripcion,
                                    PesoOrigen = 0,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1},{2}", tipoMovimiento,
                                                      gastoInventario.FolioGasto
                                                      , complementoConcepto),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                polizaEntrada = GeneraRegistroPoliza(datos);
                polizasGastosInventario.Add(polizaEntrada);

                renglon++;

                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = gastoInventario.FechaGasto,
                                Folio = gastoInventario.FolioGasto.ToString(CultureInfo.InvariantCulture),
                                Division = organizacion.Division,
                                ClaseDocumento = postFijoRef3,
                                Importe = string.Format("{0}", Cancelacion ? gastoInventario.Importe.ToString("F2") 
                                                                           : (gastoInventario.Importe * -1).ToString("F2")),
                                CentroCosto = gastoInventario.CentroCosto,
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSapProvision.CuentaSAP,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaSapProvision.Descripcion,
                                PesoOrigen = 0,
                                TipoDocumento = textoDocumento,
                                Concepto =
                                    String.Format("{0}-{1},{2}", tipoMovimiento,
                                                  gastoInventario.FolioGasto
                                                 , complementoConcepto),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                polizaEntrada = GeneraRegistroPoliza(datos);
                polizasGastosInventario.Add(polizaEntrada);
            }
            return polizasGastosInventario;
        }

        #endregion Poliza XML

        #region IMPRESION

        /// <summary>
        /// Genera linea para los registros
        /// contable
        /// </summary>
        /// <param name="polizas"> </param>
        /// <param name="cargos"> </param>
        /// <param name="abonos"> </param>
        protected override void GeneraLineaRegistroContable(IList<PolizaInfo> polizas, out IList<PolizaInfo> cargos
                                                          , out IList<PolizaInfo> abonos)
        {
            base.GeneraLineaRegistroContable(polizas, out cargos, out abonos);
            cargos.ToList().ForEach(cliente =>
            {
                if (string.IsNullOrWhiteSpace(cliente.Cuenta))
                {
                    cliente.Cuenta = string.Empty;
                }
            });
            cargos = cargos.OrderBy(cliente => cliente.Cuenta).ToList();
            abonos = abonos.OrderBy(cliente => cliente.Cuenta).ToList();

            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta =
                        string.IsNullOrWhiteSpace(cargo.Cuenta) ? cargo.Cliente : cargo.Cuenta,

                    Descripcion = formulaGenerada,
                    Concepto = cargo.Concepto,
                    Cargo =
                        Convert.ToDecimal(cargo.Importe.Replace("-", string.Empty)).ToString(
                            "N2", CultureInfo.CurrentCulture)
                };
                PolizaModel.RegistroContable.Add(registroContable);
            }

            foreach (var abono in abonos)
            {
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta = abono.Cuenta,
                    Descripcion = abono.DescripcionProducto,
                    Concepto = abono.Concepto,
                    Abono =
                        Convert.ToDecimal(abono.Importe.Replace("-", string.Empty)).ToString(
                            "N2", CultureInfo.CurrentCulture)
                };
                PolizaModel.RegistroContable.Add(registroContable);
            }
        }



        /// <summary>
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="listaAlmacenInventario"></param>
        private void GeneraLineasDetalle(List<AlmacenInventarioLoteInfo> listaAlmacenInventario)
        {

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;

            foreach (var detalle in listaAlmacenInventario.Where(alm => alm.AlmacenInventario.Producto.SubfamiliaId != SubFamiliasEnum.AlimentoFormulado.GetHashCode()))
            {
                detalleModel = new PolizaDetalleModel
                {
                    CantidadCabezas = detalle.AlmacenInventario.Producto.ProductoId.ToString(CultureInfo.InvariantCulture),
                    PesoPromedio = string.Empty,
                    TipoGanado = detalle.AlmacenInventario.Producto.ProductoDescripcion,
                    Lote = detalle.Lote.ToString(CultureInfo.InvariantCulture),
                    PesoTotal = string.Format("{0} KGS.", detalle.Cantidad),
                    ImportePromedio = detalle.Importe.ToString("N2"),
                    PrecioVenta = detalle.Importe.ToString("N2"),
                    ImporteVenta = "0.00",
                    Corral = "0.00"
                };
                PolizaModel.Detalle.Add(detalleModel);
            }
            polizaImpresion.GenerarDetalles("Detalle");
        }

        /// <summary>
        /// Genera Linea de Encabezados para el Detalle
        /// </summary>
        private void GeneraLineaEncabezadoDetalle()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Producto",
                                                      Desplazamiento = 4
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "A Precio de Costo",
                                                      Alineacion = "right",
                                                      Desplazamiento = 3
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "A Precio de Salida",
                                                      Alineacion = "right",
                                                      Desplazamiento = 2
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "5", "3", "17", "5", "10", "8", "10", "8", "8" }, "Detalle");

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Cod.",
                                                      Alineacion = "right"
                                                  },
                                                   new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "left"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                    Descripcion = "Descripción",
                                                    Alineacion = "left"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Lote",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Unidades Tip.",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Precio",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Importe",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Precio",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Importe",
                                                      Alineacion = "right"
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "5", "3", "17", "5", "10", "8", "10", "8", "8" }, "Detalle");

        }

        #endregion IMPRESION

        #endregion METODOS PRIVADOS
    }
}
