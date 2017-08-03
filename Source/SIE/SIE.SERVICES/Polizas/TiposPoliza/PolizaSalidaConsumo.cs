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
    public class PolizaSalidaConsumo : PolizaAbstract
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
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaConsumo);

                var solicitudProducto = datosPoliza as SolicitudProductoInfo;

                if (solicitudProducto == null)
                {
                    return null;
                }

                long folioVenta = solicitudProducto.FolioSolicitud; //almacenInventarioLote.Select(folio => folio.AnimalID).FirstOrDefault();
                int organizacionID = solicitudProducto.OrganizacionID;
                DateTime fechaVenta = solicitudProducto.FechaEntrega.HasValue ? solicitudProducto.FechaEntrega.Value : DateTime.MinValue;

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
										  Descripcion = "Por Consumo",
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
										  Descripcion = "CARGO A: GASTO",
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

                GeneraLineasDetalle(solicitudProducto);
                GeneraLinea(6);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");


                GeneraLineaTotales(solicitudProducto);
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
            var solicitud = datosPoliza as SolicitudProductoInfo;
            IList<PolizaInfo> poliza = ObtenerPoliza(solicitud);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        #region Poliza XML
        private IList<PolizaInfo> ObtenerPoliza(SolicitudProductoInfo solicitud)
        {
            var polizasSalidaConsumo = new List<PolizaInfo>();

            IList<ClaseCostoProductoInfo> cuentasAlmacenProductoSalida =
                 ObtenerCostosProducto(solicitud.AlmacenGeneralID);

            if (cuentasAlmacenProductoSalida == null)
            {
                throw new ExcepcionServicio("No se encuentran cuentas configuradas, para productos del almacén general");
            }
            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaConsumo.GetHashCode());

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

            string numeroReferencia = ObtenerNumeroReferencia;

            DateTime fecha = solicitud.FechaEntrega.HasValue ? solicitud.FechaEntrega.Value : DateTime.MinValue;
            string archivoFolio = ObtenerArchivoFolio(fecha);

            var organizacionBL = new OrganizacionBL();
            OrganizacionInfo organizacion = organizacionBL.ObtenerPorAlmacenID(solicitud.AlmacenGeneralID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }
            string claveParametro;
            var centrosCosto = ObtenerCentrosCosto();
            var camionesReparto = ObtenerCamionesRepartoPorOrganizacion(organizacion.OrganizacionID); //camionRepartoBL.ObtenerPorOrganizacionID(organizacion.OrganizacionID);

            var primerDetalle = solicitud.Detalle.FirstOrDefault();
            bool esCamionReparto;

            if (primerDetalle != null && primerDetalle.CamionRepartoID.HasValue && primerDetalle.CamionRepartoID.Value > 0)
            {
                claveParametro = ParametrosEnum.CuentaCostosDiesel.ToString();
                esCamionReparto = true;
            }
            else
            {
                claveParametro = ParametrosEnum.CuentaCostosProductosAlmacen.ToString();
                esCamionReparto = false;
            }
            ParametroOrganizacionInfo parametroCuenta = ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                                               claveParametro);
            if (parametroCuenta == null)
            {
                throw new ExcepcionServicio(string.Format("No se encuentró valor de la cuenta, para el parámetro {0}, en la organización", claveParametro));
            }

            foreach (var solicitudDetalle in solicitud.Detalle)
            {
                int centroCostoID;
                if (esCamionReparto)
                {
                    CamionRepartoInfo camionReparto =
                        camionesReparto.FirstOrDefault(
                            camion => camion.CamionRepartoID == solicitudDetalle.CamionRepartoID);
                    if (camionReparto == null)
                    {
                        camionReparto = new CamionRepartoInfo();
                    }
                    centroCostoID = camionReparto.CentroCosto.CentroCostoID;
                }
                else
                {
                    centroCostoID = solicitud.CentroCostoID;
                }

                CentroCostoInfo centroCostoCargo =
                    centrosCosto.FirstOrDefault(centro => centro.CentroCostoID == centroCostoID);

                if (centroCostoCargo == null)
                {
                    throw new ExcepcionServicio("No se encuentró el centro de costo");
                }

                var datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = solicitud.FechaEntrega.HasValue ? solicitud.FechaEntrega.Value : DateTime.MinValue,
                    Folio = solicitud.FolioSolicitud.ToString(CultureInfo.InvariantCulture),
                    CabezasRecibidas = string.Empty,
                    NumeroDocumento = string.Empty,
                    ClaseDocumento = postFijoRef3,
                    ClaveProveedor = string.Empty,
                    Importe = string.Format("{0}", Math.Round(solicitudDetalle.Cantidad * solicitudDetalle.PrecioPromedio, 2).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    CentroCosto = string.Format("{0}{1}{2}",
                                                "SA0",
                                                organizacion.OrganizacionID,
                                                centroCostoCargo.CentroCostoSAP),
                    Renglon = Convert.ToString(linea++),
                    Cabezas = string.Empty,
                    ImporteIva = "0",
                    Ref3 = ref3.ToString(),
                    Cuenta = parametroCuenta.Valor,
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
                                             solicitud.FolioSolicitud,
                                             solicitudDetalle.Cantidad.ToString("N2"),
                                             solicitudDetalle.Producto.Descripcion,
                                             (Math.Round(solicitudDetalle.Cantidad * solicitudDetalle.PrecioPromedio, 2).ToString("C2"))),
                    Sociedad = organizacion.Sociedad,
                    DescripcionProducto = solicitudDetalle.Producto.Descripcion,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalidaConsumo.Add(polizaSalida);

                var cuentaSapSale = cuentasAlmacenProductoSalida.FirstOrDefault(
                      cuenta => cuenta.ProductoID == solicitudDetalle.Producto.ProductoId);

                if (cuentaSapSale == null)
                {
                    cuentaSapSale = new ClaseCostoProductoInfo();
                }
                var claveContableAbono = cuentasSap.FirstOrDefault(sap => sap.CuentaSAPID == cuentaSapSale.CuentaSAPID);

                if (claveContableAbono == null)
                {
                    throw new ExcepcionServicio(string.Format("No se encontró configurada la cuenta del producto {0}", solicitudDetalle.Producto.Descripcion));
                }

                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroReferencia,
                    FechaEntrada = solicitud.FechaEntrega.HasValue ? solicitud.FechaEntrega.Value : DateTime.MinValue,
                    Folio = solicitud.FolioSolicitud.ToString(CultureInfo.InvariantCulture),
                    CabezasRecibidas = string.Empty,
                    NumeroDocumento = string.Empty,
                    ClaseDocumento = postFijoRef3,
                    ClaveProveedor = string.Empty,
                    Importe = string.Format("{0}", (Math.Round(solicitudDetalle.Cantidad * solicitudDetalle.PrecioPromedio, 2)*-1).ToString("F2")),
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
                                             solicitud.FolioSolicitud,
                                             solicitudDetalle.Producto.Descripcion,
                                             solicitudDetalle.Cantidad.ToString("N2"),
                                             (Math.Round(solicitudDetalle.Cantidad * solicitudDetalle.PrecioPromedio, 2).ToString("C2"))),
                    Sociedad = organizacion.Sociedad,
                    DescripcionProducto = solicitudDetalle.Producto.Descripcion,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalidaConsumo.Add(polizaSalida);
            }
            return polizasSalidaConsumo;
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
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="solicitudProducto"></param>
        private void GeneraLineasDetalle(SolicitudProductoInfo solicitudProducto)
        {

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;

            foreach (var detalle in solicitudProducto.Detalle)
            {
                detalleModel = new PolizaDetalleModel
                {
                    CantidadCabezas = detalle.Producto.ProductoId.ToString(CultureInfo.InvariantCulture),
                    PesoPromedio = string.Empty,
                    TipoGanado = detalle.Producto.Descripcion,
                    PesoTotal = string.Format("{0} {1}.", detalle.Cantidad, detalle.Producto.UnidadMedicion.ClaveUnidad),
                    ImportePromedio = detalle.PrecioPromedio.ToString("N2"),
                    PrecioVenta = Math.Round(detalle.Cantidad * detalle.PrecioPromedio, 2).ToString("N2"),
                };
                PolizaModel.Detalle.Add(detalleModel);
            }
            polizaImpresion.GenerarDetalles("Detalle");
        }

        /// <summary>
        /// Genera los totales por Detalle
        /// </summary>
        /// <param name="solicitud"></param>
        private void GeneraLineaTotales(SolicitudProductoInfo solicitud)
        {
            var sumaImporte = Math.Round(solicitud.Detalle.Sum(det => det.Cantidad * det.PrecioPromedio), 2);

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
