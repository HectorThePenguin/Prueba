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
    public class PolizaEntradaTraspaso : PolizaAbstract
    {

        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.EntradaTraspaso);

                var traspasoMp = datosPoliza as TraspasoMpPaMedInfo;

                if (traspasoMp == null)
                {
                    return null;
                }

                long folioTraspaso = traspasoMp.FolioTraspaso;
                //int organizacionOrigenID = traspasoMp.OrganizacionOrigen.OrganizacionID;
                int organizacionDestinoID = traspasoMp.OrganizacionDestino.OrganizacionID;
                DateTime fechaVenta = traspasoMp.FechaTraspaso;

                OrganizacionInfo organizacionDestino = ObtenerOrganizacionIVA(organizacionDestinoID);
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = organizacionDestino.Descripcion,
										  Desplazamiento = 0
									  },
                                      new PolizaEncabezadoModel
									  {
										  Descripcion = "Por Traspaso",
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
															folioTraspaso),
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
										  Descripcion = string.Format("CARGO A: {0}",traspasoMp.AlmacenDestino.Descripcion),
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
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");


                GeneraLineaEncabezadoDetalle();

                GeneraLineasDetalle(traspasoMp);
                GeneraLinea(6);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");


                GeneraLineaTotales(traspasoMp);
                GeneraLinea(6);
                polizaImpresion.GenerarDetalles("Detalle");


                GeneraLinea(5);
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(folioTraspaso);
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
            var traspasoMp = datosPoliza as TraspasoMpPaMedInfo;
            IList<PolizaInfo> poliza = ObtenerPoliza(traspasoMp);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        #region Poliza XML
        private IList<PolizaInfo> ObtenerPoliza(TraspasoMpPaMedInfo traspasoMp)
        {
            var polizasSalidaTraspaso = new List<PolizaInfo>();

            IList<ClaseCostoProductoInfo> cuentasAlmacenProductoSalida =
                ObtenerCostosProducto(traspasoMp.AlmacenOrigen.AlmacenID);
            IList<ClaseCostoProductoInfo> cuentasAlmacenProductoEntrada =
                ObtenerCostosProducto(traspasoMp.AlmacenDestino.AlmacenID);

            if (cuentasAlmacenProductoSalida == null)
            {
                throw new ExcepcionServicio("No se encuentran cuentas configuradas, para productos del almacén origen");
            }
            if (cuentasAlmacenProductoEntrada == null)
            {
                throw new ExcepcionServicio("No se encuentran cuentas configuradas, para productos del almacén  de destino");
            }

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.EntradaTraspaso.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.EntradaTraspaso));
            }

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            var linea = 1;

            //TO DO REVISAR SI CAMBIARA EL REF 3
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
            AlmacenMovimientoInfo almacenMovimiento = almacenMovimientoBL.ObtenerPorId(traspasoMp.AlmacenMovimientoID);

            string numeroReferencia = ObtenerNumeroReferenciaFolio(almacenMovimiento.FolioMovimiento);
            //string numeroReferencia = ObtenerNumeroReferencia;

            DateTime fecha = traspasoMp.FechaTraspaso;
            string archivoFolio = ObtenerArchivoFolio(fecha);

            var organizacionBL = new OrganizacionBL();
            OrganizacionInfo organizacionOrigen = organizacionBL.ObtenerPorAlmacenID(traspasoMp.AlmacenOrigen.AlmacenID);
            if (organizacionOrigen == null)
            {
                organizacionOrigen = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }
            OrganizacionInfo organizacionDestino = organizacionBL.ObtenerPorAlmacenID(traspasoMp.AlmacenDestino.AlmacenID);
            if (organizacionDestino == null)
            {
                organizacionDestino = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }
            IList<CuentaAlmacenSubFamiliaInfo> cuentasSubFamiliaSalida =
                ObtenerCostosSubFamilia(traspasoMp.AlmacenOrigen.AlmacenID);

            IList<CuentaAlmacenSubFamiliaInfo> cuentasSubFamiliaEntrada =
                ObtenerCostosSubFamilia(traspasoMp.AlmacenDestino.AlmacenID);

            ParametroOrganizacionInfo parametroCentroCostoOrigen =
                ObtenerParametroOrganizacionPorClave(organizacionOrigen.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOMP.ToString());
            if (parametroCentroCostoOrigen == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
            }
            ParametroOrganizacionInfo parametroCentroCostoDestino =
               ObtenerParametroOrganizacionPorClave(organizacionDestino.OrganizacionID,
                                                    ParametrosEnum.CTACENTROCOSTOMP.ToString());
            if (parametroCentroCostoDestino == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
            }
            bool afectaCosto;
            CuentaSAPInfo claveContableCargo;
            afectaCosto = ValidarAfectacionCuentaCosto(traspasoMp.ProductoOrigen);
            if (!afectaCosto && (traspasoMp.AlmacenDestino.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.Enfermeria.GetHashCode()
                || traspasoMp.AlmacenDestino.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ManejoGanado.GetHashCode()
                || traspasoMp.AlmacenDestino.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode())
                || traspasoMp.AlmacenDestino.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.GeneralGanadera.GetHashCode())
            {
                var cuentaSapSubFamiliaEntrada = cuentasSubFamiliaEntrada.FirstOrDefault(
                   cuenta => cuenta.SubFamiliaID == traspasoMp.ProductoOrigen.SubFamilia.SubFamiliaID);

                if (cuentaSapSubFamiliaEntrada == null)
                {
                    cuentaSapSubFamiliaEntrada = new CuentaAlmacenSubFamiliaInfo();
                }
                claveContableCargo =
                    cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapSubFamiliaEntrada.CuentaSAPID);
            }
            else
            {
                var cuentaSapEntrada = cuentasAlmacenProductoEntrada.FirstOrDefault(
                   cuenta => cuenta.ProductoID == traspasoMp.ProductoOrigen.ProductoId);
                if (cuentaSapEntrada == null)
                {
                    cuentaSapEntrada = new ClaseCostoProductoInfo();
                }
                claveContableCargo =
                    cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapEntrada.CuentaSAPID);
            }

            if (traspasoMp.AlmacenDestino.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.CentroAcopio.GetHashCode())
            {
                ParametroOrganizacionInfo cuentaMedicamento = ObtenerParametroOrganizacionPorClave(organizacionDestino.OrganizacionID,
                                                     ParametrosEnum.CuentaMedicamentoTransito.ToString());

                if (cuentaMedicamento == null)
                {
                    cuentaMedicamento = new ParametroOrganizacionInfo
                    {
                        Valor = string.Empty
                    };
                }

                claveContableCargo =
                    cuentasSap.FirstOrDefault(
                        sap =>
                        sap.CuentaSAP.Equals(cuentaMedicamento.Valor, StringComparison.InvariantCultureIgnoreCase));
            }
            if (claveContableCargo == null)
            {
                throw new ExcepcionServicio(string.Format("No se encontró configurada la cuenta del producto {0}",
                                                          traspasoMp.ProductoOrigen.ProductoDescripcion));
            }
            linea = 0;
            linea++;
            var datos = new DatosPolizaInfo
            {
                NumeroReferencia = numeroReferencia,
                FechaEntrada = fecha,
                Folio = numeroReferencia,
                ClaseDocumento = postFijoRef3,
                Importe = string.Format("{0}", traspasoMp.ImporteTraspaso.ToString("F2")),
                Renglon = Convert.ToString(linea),
                ImporteIva = "0",
                Ref3 = ref3.ToString(),
                Cuenta = claveContableCargo.CuentaSAP,
                CentroCosto =
                    claveContableCargo.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                        ? parametroCentroCostoDestino.Valor
                        : string.Empty,
                Division = organizacionDestino.Division,
                ArchivoFolio = archivoFolio,
                PesoOrigen = 0,
                TipoDocumento = textoDocumento,
                Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                         tipoMovimiento,
                                         numeroReferencia,
                                         traspasoMp.ProductoOrigen.ProductoDescripcion,
                                         string.Format("{0} {1}.",
                                                       traspasoMp.CantidadTraspasarDestino.ToString("N2"),
                                                       traspasoMp.ProductoOrigen.UnidadMedicion.ClaveUnidad),
                                         traspasoMp.PrecioTraspasoDestino.ToString("C2")),
                Sociedad = organizacionDestino.Sociedad,
                DescripcionProducto = claveContableCargo.Descripcion,
                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacionDestino.Sociedad),
            };
            PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
            polizasSalidaTraspaso.Add(polizaSalida);

            #region CuentaAbono

            CuentaSAPInfo claveContableAbono = null;
            afectaCosto = ValidarAfectacionCuentaCosto(traspasoMp.ProductoOrigen);
            if (!afectaCosto && (traspasoMp.AlmacenOrigen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.Enfermeria.GetHashCode()
                || traspasoMp.AlmacenOrigen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ManejoGanado.GetHashCode()
                || traspasoMp.AlmacenOrigen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode())
                || traspasoMp.AlmacenDestino.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.GeneralGanadera.GetHashCode())
            {
                var cuentaSapSubFamiliaSalida = cuentasSubFamiliaSalida.FirstOrDefault(
                   cuenta => cuenta.SubFamiliaID == traspasoMp.ProductoOrigen.SubFamilia.SubFamiliaID);

                if (cuentaSapSubFamiliaSalida == null)
                {
                    cuentaSapSubFamiliaSalida = new CuentaAlmacenSubFamiliaInfo();
                }
                claveContableAbono =
                    cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapSubFamiliaSalida.CuentaSAPID);
            }
            else
            {
                var cuentaSapSalida = cuentasAlmacenProductoSalida.FirstOrDefault(
                   cuenta => cuenta.ProductoID == traspasoMp.ProductoOrigen.ProductoId);
                if (cuentaSapSalida == null)
                {
                    cuentaSapSalida = new ClaseCostoProductoInfo();
                }
                claveContableAbono =
                    cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapSalida.CuentaSAPID);
            }

            if (traspasoMp.AlmacenOrigen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.CentroAcopio.GetHashCode())
            {
                ParametroOrganizacionInfo cuentaMedicamento = ObtenerParametroOrganizacionPorClave(organizacionOrigen.OrganizacionID,
                                                     ParametrosEnum.CuentaMedicamentoTransito.ToString());

                if (cuentaMedicamento == null)
                {
                    cuentaMedicamento = new ParametroOrganizacionInfo
                    {
                        Valor = string.Empty
                    };
                }

                claveContableAbono =
                    cuentasSap.FirstOrDefault(
                        sap =>
                        sap.CuentaSAP.Equals(cuentaMedicamento.Valor, StringComparison.InvariantCultureIgnoreCase));
            }
            if (claveContableAbono == null)
            {
                throw new ExcepcionServicio(string.Format("No se encontró configurada la cuenta del producto {0}",
                                                          traspasoMp.ProductoOrigen.ProductoDescripcion));
            }
            #endregion CuentaAbono

            linea++;
            datos = new DatosPolizaInfo
            {
                NumeroReferencia = numeroReferencia,
                FechaEntrada = fecha,
                Folio = numeroReferencia,
                ClaseDocumento = postFijoRef3,
                Importe =
                    string.Format("{0}", (traspasoMp.ImporteTraspaso*-1).ToString("F2")),
                Renglon = Convert.ToString(linea),
                ImporteIva = "0",
                Ref3 = ref3.ToString(),
                Cuenta = claveContableAbono.CuentaSAP,
                ArchivoFolio = archivoFolio,
                CentroCosto =
                    claveContableAbono.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                        ? parametroCentroCostoOrigen.Valor
                        : string.Empty,
                Division = organizacionOrigen.Division,
                PesoOrigen = 0,
                TipoDocumento = textoDocumento,
                Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                        tipoMovimiento,
                                        numeroReferencia,
                                        traspasoMp.ProductoOrigen.ProductoDescripcion,
                                        string.Format("{0} {1}.",
                                                      traspasoMp.CantidadTraspasarOrigen.ToString("N2"),
                                                      traspasoMp.ProductoOrigen.UnidadMedicion.ClaveUnidad),
                                        traspasoMp.PrecioTraspasoOrigen.ToString("C2")),
                Sociedad = organizacionOrigen.Sociedad,
                DescripcionProducto = claveContableAbono.Descripcion,
                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacionOrigen.Sociedad),
            };
            polizaSalida = GeneraRegistroPoliza(datos);
            polizasSalidaTraspaso.Add(polizaSalida);

            return polizasSalidaTraspaso;
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

                    Descripcion = cargo.DescripcionProducto,
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
        /// Genera los totales por Detalle
        /// </summary>
        /// <param name="traspasoMp"></param>
        private void GeneraLineaTotales(TraspasoMpPaMedInfo traspasoMp)
        {
            var sumaImporte = traspasoMp.ImporteTraspaso;

            PolizaModel.Detalle = new List<PolizaDetalleModel>();

            var detalleModel = new PolizaDetalleModel
            {
                CantidadCabezas = string.Empty,
                PesoPromedio = string.Empty,
                TipoGanado = string.Empty,
                PesoTotal = string.Empty,
                ImportePromedio = string.Empty,
                PrecioVenta = sumaImporte.ToString("N2", CultureInfo.CurrentCulture),
            };

            PolizaModel.Detalle.Add(detalleModel);
        }

        /// <summary>
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="traspasoMp"></param>
        private void GeneraLineasDetalle(TraspasoMpPaMedInfo traspasoMp)
        {
            PolizaModel.Detalle = new List<PolizaDetalleModel>();

            var detalleModel = new PolizaDetalleModel
                {
                    CantidadCabezas = traspasoMp.ProductoOrigen.ProductoId.ToString(CultureInfo.InvariantCulture),
                    PesoPromedio = string.Empty,
                    TipoGanado = traspasoMp.ProductoOrigen.ProductoDescripcion,
                    PesoTotal = string.Format("{0} {1}.", traspasoMp.CantidadTraspasarOrigen.ToString("N0"), traspasoMp.ProductoOrigen.UnidadMedicion.ClaveUnidad),
                    ImportePromedio = traspasoMp.PrecioTraspasoOrigen.ToString("N2"),
                    PrecioVenta = traspasoMp.ImporteTraspaso.ToString("N2"),
                };
            PolizaModel.Detalle.Add(detalleModel);

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
