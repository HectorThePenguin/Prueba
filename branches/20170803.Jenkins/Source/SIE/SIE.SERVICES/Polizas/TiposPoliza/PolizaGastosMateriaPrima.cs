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
    public class PolizaGastosMateriaPrima : PolizaAbstract
    {
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
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.ProduccionAlimento);

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
            var gastoMateriaPrima = datosPoliza as GastoMateriaPrimaInfo;
            IList<PolizaInfo> poliza = ObtenerPoliza(gastoMateriaPrima);
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
        private IList<PolizaInfo> ObtenerPoliza(GastoMateriaPrimaInfo gastoMateriaPrima)
        {
            var polizasGastosMateriaPrima = new List<PolizaInfo>();

            IList<ClaseCostoProductoInfo> cuentasAlmacenProducto =
               ObtenerCostosProducto(gastoMateriaPrima.AlmacenID);
            IList<CuentaAlmacenSubFamiliaInfo> cuentasSubfamilia =
                ObtenerCostosSubFamilia(gastoMateriaPrima.AlmacenID);
           
            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            TipoPolizaInfo tipoPoliza;

            if(gastoMateriaPrima.EsEntrada)
            {
                tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.EntradaAjuste.GetHashCode());
            }
            else
            {
                tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaAjuste.GetHashCode());
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

            var almacenMovimientoBL = new AlmacenMovimientoBL();

            AlmacenMovimientoInfo almacenMovimientoGenerado =
                almacenMovimientoBL.ObtenerPorId(gastoMateriaPrima.AlmacenMovimientoID.HasValue ? gastoMateriaPrima.AlmacenMovimientoID.Value : 0);
            long folioMovimiento = gastoMateriaPrima.FolioGasto;
            if (almacenMovimientoGenerado != null)
            {
                folioMovimiento = almacenMovimientoGenerado.FolioMovimiento;
                gastoMateriaPrima.FolioMovimiento = folioMovimiento;
            }
            //string numeroReferencia = ObtenerNumeroReferencia;
            string numeroReferencia = ObtenerNumeroReferenciaFolio(folioMovimiento);

            DateTime fecha = gastoMateriaPrima.Fecha;
            string archivoFolio = ObtenerArchivoFolio(fecha);

            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(gastoMateriaPrima.Organizacion.OrganizacionID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }
            bool esProveedor = gastoMateriaPrima.Proveedor != null && gastoMateriaPrima.Proveedor.ProveedorID > 0;
            var renglon = 0;

            dynamic cuentaSapProducto;
            AlmacenInfo almacen = ObtenerAlmacen(gastoMateriaPrima.AlmacenID);
            IList<ProductoInfo> productos = ObtenerProductos();

            ProductoInfo producto =
                productos.FirstOrDefault(id => id.ProductoId == gastoMateriaPrima.Producto.ProductoId);
            bool afectaCosto = ValidarAfectacionCuentaCosto(producto);
            if (!afectaCosto && (almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.Enfermeria.GetHashCode()
                || almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ManejoGanado.GetHashCode()
                || almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode()))
            {
                cuentaSapProducto = cuentasSubfamilia.FirstOrDefault(
                    cuenta => cuenta.SubFamiliaID == producto.SubFamilia.SubFamiliaID);
            }
            else
            {
                cuentaSapProducto = cuentasAlmacenProducto.FirstOrDefault(
                       cuenta => cuenta.ProductoID == producto.ProductoId);
            }
            if (cuentaSapProducto == null)
            {
                cuentaSapProducto = new ClaseCostoProductoInfo();
            }
            var claveContableProducto = cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapProducto.CuentaSAPID);
            if (claveContableProducto == null)
            {
                throw new ExcepcionServicio(string.Format("No se encontró configurada la cuenta del producto {0}",
                                                          gastoMateriaPrima.Producto.Descripcion));
            }
            CuentaSAPInfo cuentaSapProvision = null;
            if (!esProveedor)
            {
                cuentaSapProvision =
                    cuentasSap.FirstOrDefault(
                        clave => clave.CuentaSAPID == gastoMateriaPrima.CuentaSAP.CuentaSAPID);
                if (cuentaSapProvision == null)
                {
                    throw new ExcepcionServicio("No se encuentra configurada la cuenta de provisión.");
                }
            }
            PolizaInfo polizaEntrada;

            ParametroOrganizacionInfo parametroCentroCosto =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOMP.ToString());
            if (parametroCentroCosto == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
            }

            gastoMateriaPrima.Importe = Math.Round(gastoMateriaPrima.Importe, 2);
            if (gastoMateriaPrima.EsEntrada)
            {
                #region PolizaEntrada
                if (esProveedor)
                {
                    if (!gastoMateriaPrima.Iva)
                    {
                        renglon++;
                        var datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = gastoMateriaPrima.Fecha,
                                Folio = folioMovimiento.ToString(),
                                ClaseDocumento = postFijoRef3,
                                Importe =
                                    string.Format("{0}",
                                                  Cancelacion ? (gastoMateriaPrima.Importe * -1).ToString("F2") 
                                                              : gastoMateriaPrima.Importe.ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                Division = organizacion.Division,
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = claveContableProducto.CuentaSAP,
                                ArchivoFolio = archivoFolio,
                                TipoDocumento = textoDocumento,
                                Concepto =
                                     String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                   folioMovimiento
                                                   , gastoMateriaPrima.Producto.Descripcion
                                                   , gastoMateriaPrima.Importe.ToString("C2")),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosMateriaPrima.Add(polizaEntrada);

                        renglon++;
                        datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = gastoMateriaPrima.Fecha,
                                Folio = folioMovimiento.ToString(),
                                Division = organizacion.Division,
                                ClaveProveedor = gastoMateriaPrima.Proveedor.CodigoSAP,
                                ClaseDocumento = postFijoRef3,
                                Importe =
                                    string.Format("{0}", Cancelacion ? gastoMateriaPrima.Importe.ToString("F2")
                                                                     : (gastoMateriaPrima.Importe* -1).ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = gastoMateriaPrima.Proveedor.Descripcion,
                                TipoDocumento = textoDocumento,
                                ComplementoRef1 = string.Empty,
                                Concepto =
                                    String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                  folioMovimiento
                                                  , gastoMateriaPrima.Producto.Descripcion
                                                  , gastoMateriaPrima.Importe.ToString("C2")),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosMateriaPrima.Add(polizaEntrada);

                    }
                    else
                    {
                        if (gastoMateriaPrima.Iva)
                        {
                            CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                                clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                            if (cuentaIva == null)
                            {
                                throw new ExcepcionServicio(
                                    string.Format("No se encuentra configurada la cuenta de iva, para la organización."));
                            }
                            renglon++;
                            var importeIva = gastoMateriaPrima.Importe*(organizacion.Iva.TasaIva/100);
                            var datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoMateriaPrima.Fecha,
                                    Folio = folioMovimiento.ToString(),
                                    Division = organizacion.Division,
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}",
                                                            Cancelacion ? (gastoMateriaPrima.Importe * -1).ToString("F2") 
                                                                        : gastoMateriaPrima.Importe.ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = claveContableProducto.CuentaSAP,
                                    ArchivoFolio = archivoFolio,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                    String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                  folioMovimiento
                                                  , gastoMateriaPrima.Producto.Descripcion
                                                  , gastoMateriaPrima.Importe.ToString("C2")),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosMateriaPrima.Add(polizaEntrada);
                            renglon++;
                            datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoMateriaPrima.Fecha,
                                    Folio = folioMovimiento.ToString(),
                                    Division = organizacion.Division,
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}",
                                                            Cancelacion ? (importeIva * -1).ToString("F2") 
                                                                        : importeIva.ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                                    ClaveImpuesto = ClaveImpuesto,
                                    CondicionImpuesto = CondicionImpuesto,
                                    ImporteIva = gastoMateriaPrima.Importe.ToString("F2"),
                                    Ref3 = ref3.ToString(),
                                    Cuenta = organizacion.Iva.CuentaRecuperar.ClaveCuenta,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = cuentaIva.Descripcion,
                                    PesoOrigen = 0,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                     String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                   folioMovimiento
                                                   , gastoMateriaPrima.Producto.Descripcion
                                                   , gastoMateriaPrima.Importe.ToString("C2")),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosMateriaPrima.Add(polizaEntrada);
                            renglon++;
                            datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoMateriaPrima.Fecha,
                                    Folio = folioMovimiento.ToString(),
                                    Division = organizacion.Division,
                                    ClaveProveedor = gastoMateriaPrima.Proveedor.CodigoSAP,
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}",
                                                            Cancelacion ? (gastoMateriaPrima.Importe + importeIva).ToString("F2") 
                                                                        : ((gastoMateriaPrima.Importe + importeIva)*-1).ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = gastoMateriaPrima.Proveedor.Descripcion,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                    String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                  folioMovimiento
                                                  , gastoMateriaPrima.Producto.Descripcion
                                                  , gastoMateriaPrima.Importe.ToString("C2")),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosMateriaPrima.Add(polizaEntrada);
                        }
                    }
                }
                else
                {
                    renglon++;
                    var datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = gastoMateriaPrima.Fecha,
                            Folio = folioMovimiento.ToString(),
                            Division = organizacion.Division,
                            ClaseDocumento = postFijoRef3,
                            Importe = string.Format("{0}", Cancelacion ? (gastoMateriaPrima.Importe*-1).ToString("F2") 
                                                                       : gastoMateriaPrima.Importe.ToString("F2")),
                            Renglon = Convert.ToString(renglon),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = claveContableProducto.CuentaSAP,
                            CentroCosto =
                                        claveContableProducto.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto) ||
                                        claveContableProducto.CuentaSAP.StartsWith(PrefijoCuentaCentroGasto)
                                            ? parametroCentroCosto.Valor
                                            : string.Empty,
                            ArchivoFolio = archivoFolio,
                            PesoOrigen = 0,
                            TipoDocumento = textoDocumento,
                            Concepto =
                                    String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                  folioMovimiento
                                                  , gastoMateriaPrima.Producto.Descripcion
                                                  , gastoMateriaPrima.Importe.ToString("C2")),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                    polizaEntrada = GeneraRegistroPoliza(datos);
                    polizasGastosMateriaPrima.Add(polizaEntrada);

                    renglon++;
                    datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoMateriaPrima.Fecha,
                                    Folio = folioMovimiento.ToString(),
                                    Division = organizacion.Division,
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}", Cancelacion ? gastoMateriaPrima.Importe.ToString("F2")
                                                                               : (gastoMateriaPrima.Importe*-1).ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = cuentaSapProvision.CuentaSAP,
                                    CentroCosto =
                                        cuentaSapProvision.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto) ||
                                        cuentaSapProvision.CuentaSAP.StartsWith(PrefijoCuentaCentroGasto)
                                            ? parametroCentroCosto.Valor
                                            : string.Empty,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = cuentaSapProvision.Descripcion,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                      folioMovimiento
                                                      , gastoMateriaPrima.Producto.Descripcion
                                                      , gastoMateriaPrima.Importe.ToString("C2")),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                    polizaEntrada = GeneraRegistroPoliza(datos);
                    polizasGastosMateriaPrima.Add(polizaEntrada);
                }
                #endregion PolizaEntrada
            }
            else
            {
                #region PolizaSalida
                if (esProveedor)
                {
                    if (!gastoMateriaPrima.Iva)
                    {
                        renglon++;
                        var datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = gastoMateriaPrima.Fecha,
                            Folio = folioMovimiento.ToString(),
                            ClaveProveedor = gastoMateriaPrima.Proveedor.CodigoSAP,
                            ClaseDocumento = postFijoRef3,
                            Importe =
                                string.Format("{0}",
                                              Cancelacion ? (gastoMateriaPrima.Importe*-1).ToString("F2") 
                                                          : gastoMateriaPrima.Importe.ToString("F2")),
                            Renglon = Convert.ToString(renglon),
                            Division = organizacion.Division,
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),

                            ArchivoFolio = archivoFolio,
                            TipoDocumento = textoDocumento,
                            Concepto =
                                String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                              folioMovimiento
                                              , gastoMateriaPrima.Producto.Descripcion
                                              , gastoMateriaPrima.Importe.ToString("C2")),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosMateriaPrima.Add(polizaEntrada);

                        renglon++;
                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = gastoMateriaPrima.Fecha,
                            Folio = folioMovimiento.ToString(),
                            Division = organizacion.Division,
                            Cuenta = claveContableProducto.CuentaSAP,
                            ClaseDocumento = postFijoRef3,
                            Importe =
                                string.Format("{0}", Cancelacion ? gastoMateriaPrima.Importe.ToString("F2") 
                                                                 : (gastoMateriaPrima.Importe*-1).ToString("F2")),
                            Renglon = Convert.ToString(renglon),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            ArchivoFolio = archivoFolio,
                            DescripcionCosto = gastoMateriaPrima.Proveedor.Descripcion,
                            TipoDocumento = textoDocumento,
                            Concepto =
                                String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                              folioMovimiento
                                              , gastoMateriaPrima.Producto.Descripcion
                                              , gastoMateriaPrima.Importe.ToString("C2")),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasGastosMateriaPrima.Add(polizaEntrada);
                    }
                    else
                    {
                        if (gastoMateriaPrima.Iva)
                        {
                            CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                                clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                            if (cuentaIva == null)
                            {
                                throw new ExcepcionServicio(
                                    string.Format("No se encuentra configurada la cuenta de iva, para la organización."));
                            }
                            var importeIva = gastoMateriaPrima.Importe * (organizacion.Iva.TasaIva / 100);
                            renglon++;
                            var datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = gastoMateriaPrima.Fecha,
                                Folio = folioMovimiento.ToString(),
                                Division = organizacion.Division,
                                ClaveProveedor = gastoMateriaPrima.Proveedor.CodigoSAP,
                                ClaseDocumento = postFijoRef3,
                                Importe = string.Format("{0}",
                                                        Cancelacion ? ((gastoMateriaPrima.Importe + importeIva)*-1).ToString("F2")
                                                                    : (gastoMateriaPrima.Importe + importeIva).ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = gastoMateriaPrima.Proveedor.Descripcion,
                                TipoDocumento = textoDocumento,
                                Concepto =
                                String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                              folioMovimiento
                                              , gastoMateriaPrima.Producto.Descripcion
                                              , gastoMateriaPrima.Importe.ToString("C2")),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosMateriaPrima.Add(polizaEntrada);

                            renglon++;
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = gastoMateriaPrima.Fecha,
                                Folio = folioMovimiento.ToString(),
                                Division = organizacion.Division,
                                ClaseDocumento = postFijoRef3,
                                Importe = string.Format("{0}",
                                                        Cancelacion ? gastoMateriaPrima.Importe.ToString("F2") 
                                                                    : (gastoMateriaPrima.Importe*-1).ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = claveContableProducto.CuentaSAP,
                                ArchivoFolio = archivoFolio,
                                TipoDocumento = textoDocumento,
                                Concepto =
                                String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                              folioMovimiento
                                              , gastoMateriaPrima.Producto.Descripcion
                                              , gastoMateriaPrima.Importe.ToString("C2")),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosMateriaPrima.Add(polizaEntrada);
                            renglon++;
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = gastoMateriaPrima.Fecha,
                                Folio = folioMovimiento.ToString(),
                                Division = organizacion.Division,
                                ClaseDocumento = postFijoRef3,
                                Importe = string.Format("{0}",
                                                        Cancelacion ? importeIva.ToString("F2") 
                                                                    : (importeIva*-1).ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = gastoMateriaPrima.Importe.ToString("F2"),
                                IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                                Ref3 = ref3.ToString(),
                                Cuenta = organizacion.Iva.CuentaRecuperar.ClaveCuenta,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaIva.Descripcion,
                                CondicionImpuesto = CondicionImpuesto,
                                ClaveImpuesto = ClaveImpuesto,
                                PesoOrigen = 0,
                                TipoDocumento = textoDocumento,
                                Concepto =
                                 String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                               folioMovimiento
                                               , gastoMateriaPrima.Producto.Descripcion
                                               , gastoMateriaPrima.Importe.ToString("C2")),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasGastosMateriaPrima.Add(polizaEntrada);
                        }
                    }
                }
                else
                {
                    renglon++;
                    var datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroReferencia,
                                        FechaEntrada = gastoMateriaPrima.Fecha,
                                        Folio = folioMovimiento.ToString(),
                                        Division = organizacion.Division,
                                        ClaseDocumento = postFijoRef3,
                                        Importe = string.Format("{0}", Cancelacion ? (gastoMateriaPrima.Importe*-1).ToString("F2")
                                                                                   : gastoMateriaPrima.Importe.ToString("F2")),
                                        Renglon = Convert.ToString(renglon),
                                        Cuenta = cuentaSapProvision.CuentaSAP,
                                        CentroCosto =
                                            cuentaSapProvision.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto) ||
                                            cuentaSapProvision.CuentaSAP.StartsWith(PrefijoCuentaCentroGasto)
                                                ? parametroCentroCosto.Valor
                                                : string.Empty,
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        ArchivoFolio = archivoFolio,
                                        PesoOrigen = 0,
                                        TipoDocumento = textoDocumento,
                                        Concepto =
                                            String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                          folioMovimiento
                                                          , gastoMateriaPrima.Producto.Descripcion
                                                          , gastoMateriaPrima.Importe.ToString("C2")),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                    polizaEntrada = GeneraRegistroPoliza(datos);
                    polizasGastosMateriaPrima.Add(polizaEntrada);

                    renglon++;
                    datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = gastoMateriaPrima.Fecha,
                                    Folio = folioMovimiento.ToString(),
                                    Division = organizacion.Division,
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}", Cancelacion ? gastoMateriaPrima.Importe.ToString("F2")
                                                                               : (gastoMateriaPrima.Importe*-1).ToString("F2")),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = claveContableProducto.CuentaSAP,
                                    CentroCosto =
                                        claveContableProducto.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto) ||
                                        claveContableProducto.CuentaSAP.StartsWith(PrefijoCuentaCentroGasto)
                                            ? parametroCentroCosto.Valor
                                            : string.Empty,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = cuentaSapProvision.Descripcion,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1},{2} {3}", tipoMovimiento,
                                                      folioMovimiento
                                                      , gastoMateriaPrima.Producto.Descripcion
                                                      , gastoMateriaPrima.Importe.ToString("C2")),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                    polizaEntrada = GeneraRegistroPoliza(datos);
                    polizasGastosMateriaPrima.Add(polizaEntrada);
                }

                #endregion PolizaSalida
            }

            return polizasGastosMateriaPrima;
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
