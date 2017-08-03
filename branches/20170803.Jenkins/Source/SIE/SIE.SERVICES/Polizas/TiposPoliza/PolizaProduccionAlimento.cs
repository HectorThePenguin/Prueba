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
    public class PolizaProduccionAlimento : PolizaAbstract
    {
        #region CONSTRUCTORES
        
        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;
        private string formulaGenerada;
        //const int CADENA_LARGA = 50;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.ProduccionAlimento);

                var produccionFormula = datosPoliza as ProduccionFormulaInfo;

                if (produccionFormula == null)
                {
                    return null;
                }

                formulaGenerada = produccionFormula.Formula.Descripcion;

                long folioVenta = produccionFormula.FolioFormula;
                int organizacionID = produccionFormula.Organizacion.OrganizacionID;
                DateTime fechaVenta = produccionFormula.FechaProduccion;

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
										  Descripcion = string.Format("CARGO A: {0}",produccionFormula.Almacen.Descripcion),
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
                //PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                //              {
                //                  new PolizaEncabezadoModel
                //                      {
                //                          Descripcion = "TRANSPORTE:"
                //                      },
                //                  new PolizaEncabezadoModel
                //                      {
                //                          Descripcion = "REFERENCIA:"
                //                      },
                //              };
                //polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");


                GeneraLineaEncabezadoDetalle();

                GeneraLineasDetalle(produccionFormula);
                GeneraLinea(6);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");


                GeneraLineaTotales(produccionFormula);
                GeneraLinea(6);
                polizaImpresion.GenerarDetalles("Detalle");


                GeneraLinea(5);
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(folioVenta);
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                GeneraLineaSubEncabezadoRegistroContable(true, "Código", "Debe", "Haber");
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                IList<PolizaInfo> cargos;
                IList<PolizaInfo> abonos;
                GeneraLineaRegistroContable(polizas, out cargos, out abonos);
                polizaImpresion.GenerarRegistroContable("RegistroContable");

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");
                GenerarLineaSumaRegistroContable(polizas, "Total=====>");
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
            var produccionFormula = datosPoliza as ProduccionFormulaInfo;
            IList<PolizaInfo> poliza = ObtenerPoliza(produccionFormula);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        /// <summary>
        /// Genera los totales por Detalle
        /// </summary>
        /// <param name="produccionFormula"></param>
        private void GeneraLineaTotales(ProduccionFormulaInfo produccionFormula)
        {
            var sumaImporte = Math.Round(produccionFormula.ProduccionFormulaDetalle.Sum(importe => importe.CantidadProducto * importe.PrecioPromedio), 2);

            PolizaModel.Detalle = new List<PolizaDetalleModel>();

            var detalleModel = new PolizaDetalleModel
             {
                 CantidadCabezas = string.Empty,
                 PesoPromedio = string.Empty,
                 TipoGanado = string.Empty,
                 Lote = string.Empty,
                 PesoTotal = string.Empty,
                 ImportePromedio = string.Empty,
                 PrecioVenta = sumaImporte.ToString("N2", CultureInfo.CurrentCulture),
                 ImporteVenta = string.Empty,
                 Corral = string.Empty
             };

            PolizaModel.Detalle.Add(detalleModel);
        }

        #region Poliza XML
        private IList<PolizaInfo> ObtenerPoliza(ProduccionFormulaInfo produccionFormula)
        {
            var polizasProduccionAlimento = new List<PolizaInfo>();

            IList<ClaseCostoProductoInfo> cuentasAlmacenProductoSalida =
                ObtenerCostosProducto(produccionFormula.Almacen.AlmacenID);

            if (cuentasAlmacenProductoSalida == null || !cuentasAlmacenProductoSalida.Any())
            {
                throw new ExcepcionServicio("No se encuentran cuentas configuradas, para productos del almacén  de planta de alimentos");
            }
            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.ProduccionAlimento.GetHashCode());

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            var linea = 1;
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

            AlmacenMovimientoInfo almacenMovimiento =
                almacenMovimientoBL.ObtenerPorId(produccionFormula.AlmacenMovimientoEntradaID);

            //string numeroReferencia = ObtenerNumeroReferencia;
            string numeroReferencia = ObtenerNumeroReferenciaFolio(almacenMovimiento.FolioMovimiento);

            DateTime fecha = produccionFormula.FechaProduccion;
            string archivoFolio = ObtenerArchivoFolio(fecha);

            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(produccionFormula.Organizacion.OrganizacionID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }

            foreach (var produccionDetalle in produccionFormula.ProduccionFormulaDetalle)
            {
                CuentaSAPInfo claveContableCargo;
                var cuentaSapEntrada = cuentasAlmacenProductoSalida.FirstOrDefault(
                   cuenta => cuenta.ProductoID == produccionFormula.Formula.Producto.ProductoId);

                if (cuentaSapEntrada == null)
                {
                    cuentaSapEntrada = new ClaseCostoProductoInfo();
                }

                claveContableCargo = cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapEntrada.CuentaSAPID);

                if (claveContableCargo == null)
                {
                    throw new ExcepcionServicio(string.Format("No se encontró configurada la cuenta del producto {0}", produccionFormula.Formula.Producto.ProductoDescripcion));
                }
                
                var datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = produccionFormula.FechaProduccion,
                    Folio = numeroReferencia,
                    CabezasRecibidas = string.Empty,
                    NumeroDocumento = string.Empty,
                    ClaseDocumento = postFijoRef3,
                    ClaveProveedor = string.Empty,
                    Importe 
                        = string.Format("{0}", Math.Round(produccionDetalle.CantidadProducto * produccionDetalle.PrecioPromedio,2).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    CentroCosto = string.Empty,
                    Renglon = Convert.ToString(linea++),
                    Cabezas = string.Empty,
                    ImporteIva = "0",
                    Ref3 = ref3.ToString(),
                    Cuenta = claveContableCargo.CuentaSAP,
                    CodigoRetencion = string.Empty,
                    Division = organizacion.Division,
                    TipoRetencion = string.Empty,
                    ArchivoFolio = archivoFolio,
                    DescripcionCosto = string.Empty,
                    PesoOrigen = 0,
                    TipoDocumento = textoDocumento,
                    ComplementoRef1 = string.Empty,
                    Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                             tipoMovimiento,
                                             numeroReferencia,
                                             produccionDetalle.Producto.ProductoDescripcion,
                                             (string.Format("{0} {1}.", produccionDetalle.CantidadProducto.ToString("N2"), produccionDetalle.Producto.UnidadMedicion.ClaveUnidad)),
                                             produccionDetalle.PrecioPromedio.ToString("C2")),
                    Sociedad = organizacion.Sociedad,
                    DescripcionProducto = produccionDetalle.Producto.ProductoDescripcion,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
                polizasProduccionAlimento.Add(polizaSalida);

                var cuentaSapSale = cuentasAlmacenProductoSalida.FirstOrDefault(
                        cuenta => cuenta.ProductoID == produccionDetalle.Producto.ProductoId);

                if (cuentaSapSale == null)
                {
                    cuentaSapSale = new ClaseCostoProductoInfo();
                }
                var claveContableAbono = cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapSale.CuentaSAPID);

                if (claveContableAbono == null)
                {
                    throw new ExcepcionServicio(string.Format("No se encontró configurada la cuenta del producto {0}", produccionDetalle.Producto.ProductoDescripcion));
                }

                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = produccionFormula.FechaProduccion,
                    Folio = numeroReferencia,
                    CabezasRecibidas = string.Empty,
                    NumeroDocumento = string.Empty,
                    ClaseDocumento = postFijoRef3,
                    ClaveProveedor = string.Empty,
                    Importe = string.Format("{0}", (Math.Round(produccionDetalle.CantidadProducto * produccionDetalle.PrecioPromedio, 2)*-1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    CentroCosto = string.Empty,
                    Renglon = Convert.ToString(linea++),
                    Cabezas = string.Empty,
                    ImporteIva = "0",
                    Ref3 = ref3.ToString(),
                    Cuenta = claveContableAbono.CuentaSAP,
                    CodigoRetencion = string.Empty,
                    TipoRetencion = string.Empty,
                    ArchivoFolio = archivoFolio,
                    DescripcionCosto = string.Empty,
                    Division = organizacion.Division,
                    PesoOrigen = 0,
                    TipoDocumento = textoDocumento,
                    ComplementoRef1 = string.Empty,
                    Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                            tipoMovimiento,
                                            numeroReferencia,
                                            produccionDetalle.Producto.ProductoDescripcion,
                                            (string.Format("{0} {1}.", produccionDetalle.CantidadProducto.ToString("N2"), produccionDetalle.Producto.UnidadMedicion.ClaveUnidad)),
                                            produccionDetalle.PrecioPromedio.ToString("C2")),
                    Sociedad = organizacion.Sociedad,
                    DescripcionProducto = produccionDetalle.Producto.ProductoDescripcion,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasProduccionAlimento.Add(polizaSalida);
            }
            return polizasProduccionAlimento;
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

            //if (costos == null || !costos.Any())
            //{
            //    costos = ObtenerCostos();
            //}

            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                //CostoInfo descripcionCosto =
                //    costos.FirstOrDefault(
                //        costo => cargo.Cuenta.EndsWith(costo.ClaveContable));
                //if (descripcionCosto == null)
                //{
                //    descripcionCosto = new CostoInfo
                //    {
                //        Descripcion = string.Empty
                //    };
                //}

                //var sbDescripcion = new StringBuilder();
                //sbDescripcion.Append(descripcionCosto.Descripcion.Length > CADENA_LARGA
                //                         ? descripcionCosto.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                //                         : descripcionCosto.Descripcion.Trim());
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
                //CostoInfo descripcionCosto =
                //    costos.FirstOrDefault(
                //        costo => abono.Cuenta.EndsWith(costo.ClaveContable));
                //if (descripcionCosto == null)
                //{
                //    descripcionCosto = new CostoInfo
                //    {
                //        Descripcion = string.Empty
                //    };
                //}
                //var sbDescripcion = new StringBuilder();
                //sbDescripcion.Append(descripcionCosto.Descripcion.Length > CADENA_LARGA
                //                         ? descripcionCosto.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                //                         : descripcionCosto.Descripcion.Trim());
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
        /// <param name="produccionFormula"></param>
        private void GeneraLineasDetalle(ProduccionFormulaInfo produccionFormula)
        {

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;

            foreach (var detalle in produccionFormula.ProduccionFormulaDetalle)
            {
                detalleModel = new PolizaDetalleModel
                {
                    CantidadCabezas = detalle.Producto.ProductoId.ToString(CultureInfo.InvariantCulture),
                    PesoPromedio = string.Empty,
                    TipoGanado = detalle.Producto.ProductoDescripcion,
                    PesoTotal = string.Format("{0} {1}.", detalle.CantidadProducto.ToString("N2"), detalle.Producto.UnidadMedicion.ClaveUnidad),
                    ImportePromedio = detalle.PrecioPromedio.ToString("N2"),
                    PrecioVenta = Math.Round(detalle.CantidadProducto * detalle.PrecioPromedio, 2).ToString("N2"),
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
                                                      Alineacion = "Center",
                                                      Desplazamiento = 2
                                                  },
                                             
                                          };
            polizaImpresion.GeneraCabecero(new[] { "5", "3", "17", "10", "8", "10" }, "Detalle");

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
                                          };
            polizaImpresion.GeneraCabecero(new[] { "5", "3", "17", "10", "8", "10" }, "Detalle");

        }

        #endregion IMPRESION

        #endregion METODOS PRIVADOS
    }
}
