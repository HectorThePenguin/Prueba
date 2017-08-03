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
    public class PolizaEntradaCompraMateriaPrima : PolizaAbstract
    {
        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                var recepcionProducto = datosPoliza as RecepcionProductoInfo;

                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.EntradaCompraMateriaPrima);

                int organizacionID = recepcionProducto.Almacen.Organizacion.OrganizacionID;
                OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);
                if (organizacion == null)
                {
                    organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    };
                }
                int folio = recepcionProducto.FolioRecepcion;
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = organizacion.Descripcion,
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = "Entrada De Almacen por Compra",
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "FECHA:",
                                                                            recepcionProducto.FechaRecepcion),
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "FOLIO No.", folio),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Folio");

                GeneraLineaEncabezadoDetalle();
                GeneraLineaDetalle(recepcionProducto.ListaRecepcionProductoDetalle);
                polizaImpresion.GenerarDetalles("Detalle");
                polizaImpresion.GenerarLineaEnBlanco("Detalle", 11);

                GeneraLineaTotalDetalle(recepcionProducto.ListaRecepcionProductoDetalle);

                GeneraLinea(11);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaObservaciones(recepcionProducto.Observaciones);
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
            var recepcionProducto = datosPoliza as RecepcionProductoInfo;
            IList<PolizaInfo> polizas = ObtenerPolizas(recepcionProducto);
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
                cuentaSAP = cuentasSAP.FirstOrDefault(clave => clave.CuentaSAP.Equals(cargo.Cuenta));
                cargo.Descripcion = cuentaSAP.Descripcion;
                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(cargo.Descripcion.Length > CADENA_LARGA
                                         ? cargo.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : cargo.Descripcion.Trim());
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta = cargo.Cuenta,
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
                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(abono.Descripcion.Length > CADENA_LARGA
                                         ? abono.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : abono.Descripcion.Trim());
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta =
                        (string.IsNullOrWhiteSpace(abono.Proveedor)
                             ? abono.Cuenta
                             : abono.Proveedor),
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

        private void GeneraLineaTotalDetalle(List<RecepcionProductoDetalleInfo> detallesProducto)
        {
            decimal importes = detallesProducto.Sum(imp => imp.Importe);
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "left",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "*** Total ***",
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 5
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = Math.Round(importes, 2).ToString("N"),
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = Math.Round(importes, 2).ToString("N"),
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "10", "10", "10" },
                                           "Detalle");
        }

        private void GeneraLineaDetalle(List<RecepcionProductoDetalleInfo> detallesProducto)
        {
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel polizaDetalleModel;
            RecepcionProductoDetalleInfo detalle;
            for (int indexPoliza = 0; indexPoliza < detallesProducto.Count; indexPoliza++)
            {
                detalle = detallesProducto[indexPoliza];
                polizaDetalleModel = new PolizaDetalleModel
                                         {
                                             CantidadCabezas = detalle.Producto.ProductoId.ToString(),
                                             TipoGanado = detalle.Producto.Descripcion,
                                             PesoOrigen = detalle.Cantidad.ToString("N0"),
                                             PesoLlegada = detalle.Cantidad.ToString("N0"),
                                             Corral = string.Empty,
                                             PrecioPromedio = (detalle.Importe / detalle.Cantidad).ToString("N2"),
                                             ImportePromedio =  detalle.Importe.ToString("N2"),
                                             PrecioVenta = (detalle.Importe / detalle.Cantidad).ToString("N2"),
                                             ImporteVenta = detalle.Importe.ToString("N2"),
                                         };
                PolizaModel.Detalle.Add(polizaDetalleModel);
            }
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
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Integrado",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "10", "10", "10" },
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
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Precio",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Total",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "10", "10", "10" },
                                           "Detalle");
        }

        private IList<PolizaInfo> ObtenerPolizas(RecepcionProductoInfo recepcionProducto)
        {
            var polizaCompraMateriaPrima = new List<PolizaInfo>();

            int organizacionID = recepcionProducto.Almacen.Organizacion.OrganizacionID;
            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(
                    clave => clave.TipoPolizaID == TipoPoliza.EntradaCompraMateriaPrima.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.EntradaCompraMateriaPrima));
            }

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

            string numeroDocumento = ObtenerNumeroReferencia;

            IList<CuentaSAPInfo> cuentasSAP = ObtenerCuentasSAP();
            int almacenID = recepcionProducto.Almacen.AlmacenID;
            IList<ClaseCostoProductoInfo> almacenesProductos = ObtenerCostosProducto(almacenID);
            IList<UnidadMedicionInfo> unidadesMedicion = ObtenerUnidadesMedicion();

            var linea = 1;
            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;

            DateTime fecha = recepcionProducto.FechaRecepcion;
            string archivoFolio = ObtenerArchivoFolio(fecha);
            int folio = recepcionProducto.FolioRecepcion;

            ParametroOrganizacionInfo parametroCentroCosto =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOMP.ToString());
            if (parametroCentroCosto == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
            }

            IList<RecepcionProductoDetalleInfo> detallesProducto = recepcionProducto.ListaRecepcionProductoDetalle;
            RecepcionProductoDetalleInfo detalle;
            DatosPolizaInfo datos;
            PolizaInfo polizaSalida;
            CuentaSAPInfo cuentaSAP;
            ClaseCostoProductoInfo almacenProducto;
            CuentaSAPInfo cuentaIva;
            UnidadMedicionInfo unidadMedicion = null;
            for (int indexDetalle = 0; indexDetalle < detallesProducto.Count; indexDetalle++)
            {
                detalle = detallesProducto[indexDetalle];
                almacenProducto =
                    almacenesProductos.FirstOrDefault(prod => prod.ProductoID == detalle.Producto.ProductoId);
                if (almacenProducto == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1} {2}", "CUENTA PARA PRODUCTO",
                                                          detalle.Producto.Descripcion, "NO CONFIGURADA"));
                }
                cuentaSAP =
                    cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenProducto.CuentaSAPID);
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1} {2}", "CUENTA PARA PRODUCTO",
                                                              detalle.Producto.Descripcion, "NO CONFIGURADA"));
                }
                unidadMedicion =
                    unidadesMedicion.FirstOrDefault(clave => clave.UnidadID == detalle.Producto.UnidadMedicion.UnidadID);
                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = fecha,
                                Folio = folio.ToString(),
                                ClaseDocumento = postFijoRef3,
                                Importe = string.Format("{0}", detalle.Importe.ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(linea++),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Division = organizacion.Division,
                                ArchivoFolio = archivoFolio,
                                Cuenta = cuentaSAP.CuentaSAP,
                                DescripcionCosto = cuentaSAP.Descripcion,
                                CentroCosto =
                                    cuentaSAP.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                                        ? parametroCentroCosto.Valor
                                        : string.Empty,
                                PesoOrigen = Math.Round(detalle.Cantidad, 0),
                                TipoDocumento = textoDocumento,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} {5}",
                                                         tipoMovimiento,
                                                         folio, detalle.Cantidad.ToString("N0")
                                                         , unidadMedicion.ClaveUnidad
                                                         , detalle.Producto.Descripcion, postFijoRef3),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizaCompraMateriaPrima.Add(polizaSalida);
            }
            cuentaIva = cuentasSAP.FirstOrDefault(
                    clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
            if (cuentaIva == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA LA CUENTA",
                                                          organizacion.Iva.Descripcion));
            }
            ParametroGeneralInfo parametroGeneralProductos =
                ObtenerParametroGeneralPorClave(ParametrosEnum.PRODIVAALM.ToString());
            List<int> productosGeneranIVA =
                parametroGeneralProductos.Valor.Split('|').Select(x => Convert.ToInt32(x)).ToList();
            var medicamentosConIva = productosGeneranIVA.Select(x => new RecepcionProductoDetalleInfo
                                                                         {
                                                                             Producto = new ProductoInfo
                                                                                            {
                                                                                                ProductoId =
                                                                                                    x
                                                                                            }
                                                                         }).ToList();
            decimal importe =
                detallesProducto.Where(fam => fam.Producto.Familia.FamiliaID != FamiliasEnum.Medicamento.GetHashCode()).
                    Sum(imp => imp.Importe);
            decimal importeMedicamentosIva = (from det in detallesProducto
                                              join med in medicamentosConIva on det.Producto.ProductoId equals
                                                  med.Producto.ProductoId
                                              select det.Importe).Sum();
            importe = importe + importeMedicamentosIva;
            decimal importeIva = importe * (organizacion.Iva.TasaIva / 100);
            importe = importe + importeIva;
            decimal cantidad =
                detallesProducto.Where(fam => fam.Producto.Familia.FamiliaID != FamiliasEnum.Medicamento.GetHashCode()).
                    Intersect(medicamentosConIva).
                    Sum(kgs => kgs.Cantidad);
            decimal cantidadMedicamentosIva = (from det in detallesProducto
                                              join med in medicamentosConIva on det.Producto.ProductoId equals
                                                  med.Producto.ProductoId
                                              select det.Cantidad).Sum();
            cantidad += cantidadMedicamentosIva;
            if (importe > 0)
            {
                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroDocumento,
                    FechaEntrada = fecha,
                    Folio = folio.ToString(),
                    ClaseDocumento = postFijoRef3,
                    Importe = string.Format("{0}", importeIva.ToString("F2")),
                    IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                    Division = organizacion.Division,
                    Renglon = Convert.ToString(linea++),
                    ImporteIva = importe.ToString("F2"),
                    Ref3 = ref3.ToString(),
                    ArchivoFolio = archivoFolio,
                    Cuenta = cuentaIva.CuentaSAP,
                    DescripcionCosto = cuentaIva.Descripcion,
                    PesoOrigen = Math.Round(cantidad, 0),
                    TipoDocumento = textoDocumento,
                    ClaveImpuesto = ClaveImpuesto,
                    CondicionImpuesto = CondicionImpuesto,
                    Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                             tipoMovimiento,
                                             folio, cantidad.ToString("N0")
                                             , unidadMedicion.ClaveUnidad
                                             , postFijoRef3),
                    Sociedad = organizacion.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizaCompraMateriaPrima.Add(polizaSalida);
            }
            importe = detallesProducto.Sum(imp => imp.Importe);
            cantidad = detallesProducto.Sum(kgs => kgs.Cantidad);
            datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = fecha,
                            Folio = folio.ToString(),
                            ClaveProveedor = recepcionProducto.Proveedor.CodigoSAP,
                            ClaseDocumento = postFijoRef3,
                            Importe = string.Format("{0}", ((importe + importeIva)*-1).ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(linea),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Division = organizacion.Division,
                            ArchivoFolio = archivoFolio,
                            DescripcionCosto = recepcionProducto.Proveedor.Descripcion,
                            PesoOrigen = Math.Round(cantidad, 0),
                            TipoDocumento = textoDocumento,
                            Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                     tipoMovimiento,
                                                     folio, cantidad.ToString("N0"),
                                                     unidadMedicion.ClaveUnidad, postFijoRef3),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
            polizaSalida = GeneraRegistroPoliza(datos);
            polizaCompraMateriaPrima.Add(polizaSalida);

            return polizaCompraMateriaPrima;
        }

        #endregion METODOS PRIVADOS
    }
}
