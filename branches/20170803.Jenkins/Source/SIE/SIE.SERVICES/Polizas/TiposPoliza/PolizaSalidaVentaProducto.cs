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
    public class PolizaSalidaVentaProducto : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;
        private IList<UnidadMedicionInfo> unidades;
        private IList<ProductoInfo> productos;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaVentaProducto);

                var contenedor = datosPoliza as SalidaProductoInfo;
                int organizacionID = contenedor.Almacen.Organizacion.OrganizacionID;

                OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);
                DateTime fecha = contenedor.FechaSalida;
                long folio = contenedor.FolioSalida;

                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = organizacion.Descripcion,
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] {"100"}, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} de Producto",
                                                                            contenedor.TipoMovimiento.Descripcion),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] {"100"}, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("FECHA: {0}", fecha.ToShortDateString()),
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "FOLIO No.", folio),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] {"50", "50"}, "Folio");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] {"100"}, "Folio");

                GeneraLineaEncabezadoDetalle();
                GeneraLineaDetalle(contenedor);
                polizaImpresion.GenerarDetalles("Detalle");
                polizaImpresion.GenerarLineaEnBlanco("Detalle", 9);

                GeneraLineaTotalDetalle(contenedor);
                polizaImpresion.GenerarDetalles("Detalle");
                GeneraLinea(9);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaObservaciones(contenedor.Observaciones);
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Observaciones");

                GeneraLineaEncabezadoRegistroContable(folio);
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                GeneraLineaSubEncabezadoRegistroContable(true, "Código", "Debe", "Haber");
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");

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
            var salidaProducto = datosPoliza as SalidaProductoInfo;
            IList<PolizaInfo> polizas = ObtenerPoliza(salidaProducto);
            return polizas;
        }

        protected override void GeneraLineaRegistroContable(IList<PolizaInfo> polizas, out IList<PolizaInfo> cargos, out IList<PolizaInfo> abonos)
        {
            base.GeneraLineaRegistroContable(polizas, out cargos, out abonos);
            const int CADENA_LARGA = 29;

            IList<CuentaSAPInfo> cuentasSAP = ObtenerCuentasSAP();
            CuentaSAPInfo cuentaSAP;

            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                if (!(cargo.Cuenta.StartsWith(PrefijoCuentaCentroCosto)
                        || cargo.Cuenta.StartsWith(PrefijoCuentaCentroBeneficio)
                        || cargo.Cuenta.StartsWith(PrefijoCuentaCentroGasto)))
                {
                    cuentaSAP = cuentasSAP.FirstOrDefault(clave => clave.CuentaSAP.Equals(cargo.Cuenta));
                    if (cuentaSAP != null)
                    {
                        cargo.Descripcion = cuentaSAP.Descripcion;
                    }
                }
                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(cargo.Descripcion.Length > CADENA_LARGA
                                         ? cargo.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : cargo.Descripcion.Trim());
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta = string.IsNullOrWhiteSpace(cargo.Cliente) ? cargo.Cuenta : cargo.Cliente,
                    Descripcion = sbDescripcion.ToString(),
                    Concepto = cargo.Concepto,
                    Cargo =
                        Convert.ToDecimal(cargo.Importe.Replace("-", string.Empty)).ToString(
                            "N", CultureInfo.CurrentCulture)
                };
                PolizaModel.RegistroContable.Add(registroContable);
            }

            foreach (var abono in abonos)
            {
                if (!(abono.Cuenta.StartsWith(PrefijoCuentaCentroCosto)
                        || abono.Cuenta.StartsWith(PrefijoCuentaCentroBeneficio)
                        || abono.Cuenta.StartsWith(PrefijoCuentaCentroGasto)))
                {
                    cuentaSAP = cuentasSAP.FirstOrDefault(clave => clave.CuentaSAP.Equals(abono.Cuenta));
                    if (cuentaSAP == null)
                    {
                        var proveedorBL = new ProveedorBL();
                        var proveedor = new ProveedorInfo
                                            {
                                                CodigoSAP = abono.Proveedor
                                            };
                        proveedor = proveedorBL.ObtenerPorCodigoSAP(proveedor);
                        abono.Descripcion = proveedor.Descripcion;
                    }
                    else
                    {
                        abono.Descripcion = cuentaSAP.Descripcion;
                    }
                }
                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(abono.Descripcion.Length > CADENA_LARGA
                                         ? abono.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : abono.Descripcion.Trim());
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta =
                        (string.IsNullOrWhiteSpace(abono.Cliente)
                             ? abono.Cuenta
                             : abono.Cliente),
                    Descripcion = sbDescripcion.ToString(),
                    Concepto = abono.Concepto,
                    Abono =
                        Convert.ToDecimal(abono.Importe.Replace("-", string.Empty)).ToString(
                            "N", CultureInfo.CurrentCulture)
                };
                PolizaModel.RegistroContable.Add(registroContable);
            }
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        /// <summary>
        /// Genera linea con las observaciones
        /// </summary>
        /// <param name="observaciones"></param>
        private void GeneraLineaObservaciones(string observaciones)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "OBSERVACIONES:",
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Join(" ", observaciones),
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  }
                                          };
            polizaImpresion.GeneraCabecero(new[] { "15", "90" }, "Observaciones");
            GeneraLinea(2);
            polizaImpresion.GenerarLineaEnBlanco("Observaciones", 2);
        }

        /// <summary>
        /// Genera linea de totales para detalle
        /// </summary>
        /// <param name="salidaProducto"></param>
        private void GeneraLineaTotalDetalle(SalidaProductoInfo salidaProducto)
        {
            PolizaModel.Detalle = new List<PolizaDetalleModel>();

            decimal cantidad = salidaProducto.PesoBruto - salidaProducto.PesoTara;
            var polizaDetalleModel = new PolizaDetalleModel
            {
                PrecioVenta = (salidaProducto.Importe / cantidad).ToString("N2"),
                ImporteVenta = salidaProducto.Importe.ToString("N2"),
            };
            PolizaModel.Detalle.Add(polizaDetalleModel);
        }

        /// <summary>
        /// Genera los detalles de la salida
        /// </summary>
        /// <param name="salidaProducto"></param>
        private void GeneraLineaDetalle(SalidaProductoInfo salidaProducto)
        {
            PolizaModel.Detalle = new List<PolizaDetalleModel>();

            if (unidades == null)
            {
                unidades = ObtenerUnidadesMedicion();
            }
            if (productos == null)
            {
                productos = ObtenerProductos();
            }
            ProductoInfo producto =
                productos.FirstOrDefault(clave => clave.ProductoId == salidaProducto.Producto.ProductoId);
            if (producto == null)
            {
                producto = new ProductoInfo();
            }
            decimal cantidad = salidaProducto.PesoBruto - salidaProducto.PesoTara;
            var polizaDetalleModel = new PolizaDetalleModel
                                         {
                                             CantidadCabezas = producto.ProductoId.ToString(),
                                             TipoGanado = producto.ProductoDescripcion,
                                             PesoOrigen = cantidad.ToString("N0"),
                                             PesoLlegada = cantidad.ToString("N0"),
                                             Corral =
                                                 unidades.Where(
                                                     clave => clave.UnidadID == producto.UnidadId).Select
                                                 (uni => uni.ClaveUnidad).FirstOrDefault(),
                                             Lote = salidaProducto.AlmacenInventarioLote.Lote.ToString(),
                                             PrecioPromedio = (salidaProducto.Importe/cantidad).ToString("N2"),
                                             ImportePromedio = salidaProducto.Importe.ToString("N2"),
                                             PrecioVenta = (salidaProducto.Importe/cantidad).ToString("N2"),
                                             ImporteVenta = salidaProducto.Importe.ToString("N2"),
                                         };
            PolizaModel.Detalle.Add(polizaDetalleModel);
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
                                                      Alineacion = "left",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 5
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Costo",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "10"},
                                           "Detalle");

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Código",
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Descripción",
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Lote",
                                                      Alineacion = "right",
                                                      Desplazamiento = 1
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Origen",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Entrada",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Merma",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Uni",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Unitario",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Importe",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  }
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "10" },
                                           "Detalle");
        }

        private IList<PolizaInfo> ObtenerPoliza(SalidaProductoInfo salidaProducto)
        {
            var polizasSalida = new List<PolizaInfo>();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaVentaProducto.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.SalidaVentaProducto));
            }

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

            //string numeroReferencia = string.Format("{0}{1}", salidaProducto.FolioSalida, ObtenerNumeroReferencia);
            string numeroReferencia = ObtenerNumeroReferenciaFolio(salidaProducto.FolioSalida);

            IList<CuentaSAPInfo> cuentasSAP = ObtenerCuentasSAP();
            IList<ClaseCostoProductoInfo> almacenesProductos = ObtenerCostosProducto(salidaProducto.Almacen.AlmacenID);
            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(salidaProducto.Organizacion.OrganizacionID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }

            ClaseCostoProductoInfo almacenProducto =
                almacenesProductos.FirstOrDefault(prod => prod.ProductoID == salidaProducto.Producto.ProductoId);
            if (almacenProducto == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1} {2}", "CUENTA PARA PRODUCTO",
                                                          salidaProducto.Producto.Descripcion, "NO CONFIGURADA"));
            }
            CuentaSAPInfo cuentaSAP =
                cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenProducto.CuentaSAPID);
            if (cuentaSAP == null)
            {
                cuentaSAP = new CuentaSAPInfo
                {
                    Descripcion = string.Empty,
                    CuentaSAP = string.Empty
                };
            }

            ClaveContableInfo claveContableCosto = ObtenerCuentaInventario(salidaProducto.Organizacion.
                                                                               OrganizacionID,
                                                                           TipoPoliza.ConsumoAlimento);
            if (claveContableCosto == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CUENTA DE COSTO NO CONFIGURADA"));
            }
            ClaveContableInfo claveContableBeneficios = ObtenerCuentaInventario(salidaProducto.Organizacion.
                                                                                    OrganizacionID,
                                                                                TipoPoliza.SalidaVentaProducto);
            if (claveContableBeneficios == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CUENTA DE BENEFICIOS NO CONFIGURADA"));
            }

            ParametroOrganizacionInfo parametroCentroCosto =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOMP.ToString());
            if (parametroCentroCosto == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
            }

            ParametroOrganizacionInfo parametroCentroBeneficio =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                     ParametrosEnum.CTACENTROBENEFICIOMP.ToString());
            if (parametroCentroBeneficio == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE BENEFICIO NO CONFIGURADO"));
            }

            string archivoFolio = ObtenerArchivoFolio(salidaProducto.FechaSalida);
            int cantidad = salidaProducto.PesoBruto - salidaProducto.PesoTara;

            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();

            AlmacenMovimientoDetalle detalleMovimiento =
                almacenMovimientoDetalleBL.ObtenerPorAlmacenMovimientoID(
                    salidaProducto.AlmacenMovimiento.AlmacenMovimientoID);

            if (detalleMovimiento == null)
            {
                detalleMovimiento = new AlmacenMovimientoDetalle();
            }

            if (unidades == null)
            {
                unidades = ObtenerUnidadesMedicion();
            }
            if (productos == null)
            {
                productos = ObtenerProductos();
            }
            ProductoInfo producto =
                productos.FirstOrDefault(clave => clave.ProductoId == salidaProducto.Producto.ProductoId);
            if (producto == null)
            {
                producto = new ProductoInfo();
            }
            switch ((SubFamiliasEnum)producto.SubfamiliaId)
            {
                case SubFamiliasEnum.Granos:
                    claveContableBeneficios.Valor = string.Concat(claveContableBeneficios.Valor,
                                                                  PostFijoSubFamiliaGranos);
                    claveContableCosto.Valor = string.Concat(claveContableCosto.Valor, PostFijoSubFamiliaGranos);
                    break;
                default:
                    claveContableBeneficios.Valor = string.Concat(claveContableBeneficios.Valor,
                                                                  PostFijoSubFamiliaNoGranos);
                    claveContableCosto.Valor = string.Concat(claveContableCosto.Valor, PostFijoSubFamiliaNoGranos);
                    break;
            }
            var traspaso = false;
            if (salidaProducto.TipoMovimiento.TipoMovimientoID == TipoMovimiento.ProductoSalidaTraspaso.GetHashCode()
                || salidaProducto.TipoMovimiento.Descripcion.Equals("salida por traspaso", StringComparison.InvariantCultureIgnoreCase))
            {
                salidaProducto.Cliente = new ClienteInfo
                                             {
                                                 CodigoSAP = salidaProducto.CuentaSAP.CuentaSAP,
                                                 Descripcion = salidaProducto.CuentaSAP.Descripcion
                                             };
                traspaso = true;
            }
            string unidad = unidades.Where(clave => clave.UnidadID == producto.UnidadId).
                Select(uni => uni.ClaveUnidad).FirstOrDefault();
            var datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = salidaProducto.FechaSalida,
                                Folio = salidaProducto.FolioSalida.ToString(),
                                Cliente = traspaso ? string.Empty : salidaProducto.Cliente.CodigoSAP,
                                Cuenta = traspaso ? salidaProducto.Cliente.CodigoSAP : string.Empty,
                                Importe = string.Format("{0}", salidaProducto.Importe.ToString("F2")),
                                Renglon = Convert.ToString(linea++),
                                ImporteIva = "0",
                                ClaseDocumento = postFijoRef3,
                                Ref3 = ref3.ToString(),
                                Division = organizacion.Division,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = salidaProducto.Cliente.Descripcion,
                                PesoOrigen = cantidad,
                                TipoDocumento = textoDocumento,
                                Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                         tipoMovimiento,
                                                         salidaProducto.FolioSalida, cantidad.ToString("N0"),
                                                         unidad,
                                                         cuentaSAP.Descripcion),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad)
                            };
            PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
            polizasSalida.Add(polizaSalida);

            datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = salidaProducto.FechaSalida,
                            Folio = salidaProducto.FolioSalida.ToString(),
                            Importe = (detalleMovimiento.Importe * -1).ToString("F2"),
                            Renglon = Convert.ToString(linea++),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = cuentaSAP.CuentaSAP,
                            DescripcionCosto = cuentaSAP.Descripcion,
                            Division = organizacion.Division,
                            ArchivoFolio = archivoFolio,
                            PesoOrigen = cantidad,
                            ClaseDocumento = postFijoRef3,
                            TipoDocumento = textoDocumento,
                            Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                     tipoMovimiento,
                                                     salidaProducto.FolioSalida, cantidad.ToString("N0"),
                                                     unidad,
                                                     cuentaSAP.Descripcion),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad)
                        };
            polizaSalida = GeneraRegistroPoliza(datos);
            polizasSalida.Add(polizaSalida);

            if (!traspaso)
            {
                cuentaSAP = cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAP == claveContableCosto.Valor);
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio("CUENTA DE COSTO NO CONFIGURADA");
                }
                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = salidaProducto.FechaSalida,
                                Folio = salidaProducto.FolioSalida.ToString(),
                                Importe = detalleMovimiento.Importe.ToString("F2"),
                                Renglon = Convert.ToString(linea++),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSAP.CuentaSAP,
                                Division = organizacion.Division,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaSAP.Descripcion,
                                CentroCosto =
                                   claveContableCosto.Valor.StartsWith(PrefijoCuentaCentroCosto) ||
                                   claveContableCosto.Valor.StartsWith(PrefijoCuentaCentroGasto)
                                       ? parametroCentroCosto.Valor
                                       : string.Empty,
                                //CentroBeneficio =
                                //    claveContableBeneficios.Valor.StartsWith(PrefijoCuentaCentroBeneficio)
                                //        ? parametroCentroBeneficio.Valor
                                //        : string.Empty,
                                PesoOrigen = cantidad,
                                ClaseDocumento = postFijoRef3,
                                TipoDocumento = textoDocumento,
                                Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                         tipoMovimiento,
                                                         salidaProducto.FolioSalida, cantidad.ToString("N0"),
                                                         unidad,
                                                         cuentaSAP.Descripcion),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad)
                            };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalida.Add(polizaSalida);

                cuentaSAP = cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAP == claveContableBeneficios.Valor);
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio("CUENTA DE BENEFICIOS NO CONFIGURADA");
                }
                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = salidaProducto.FechaSalida,
                                Folio = salidaProducto.FolioSalida.ToString(),
                                Importe = string.Format("{0}", (salidaProducto.Importe*-1).ToString("F2")),
                                Renglon = Convert.ToString(linea),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSAP.CuentaSAP,
                                Division = organizacion.Division,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaSAP.Descripcion,
                                //CentroCosto =
                                //    claveContableCosto.Valor.StartsWith(PrefijoCuentaCentroCosto) ||
                                //    claveContableCosto.Valor.StartsWith(PrefijoCuentaCentroGasto)
                                //        ? parametroCentroCosto.Valor
                                //        : string.Empty,
                                CentroBeneficio =
                                    claveContableBeneficios.Valor.StartsWith(PrefijoCuentaCentroBeneficio)
                                        ? parametroCentroBeneficio.Valor
                                        : string.Empty,
                                PesoOrigen = cantidad,
                                ClaseDocumento = postFijoRef3,
                                TipoDocumento = textoDocumento,
                                Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                         tipoMovimiento,
                                                         salidaProducto.FolioSalida, cantidad.ToString("N0"),
                                                         unidad,
                                                         cuentaSAP.Descripcion),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad)
                            };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalida.Add(polizaSalida);
            }

            return polizasSalida;
        }

        #endregion METODOS PRIVADOS
    }
}
