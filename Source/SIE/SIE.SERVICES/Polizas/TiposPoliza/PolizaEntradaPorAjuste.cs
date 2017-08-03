using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaEntradaPorAjuste : PolizaAbstract
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
                var ajustesDeInventario = datosPoliza as List<PolizaEntradaSalidaPorAjusteModel>;
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.EntradaAjuste);

                var almacenMovimientoBL = new AlmacenMovimientoBL();
                List<AlmacenMovimientoDetalle> almancenMovimientosDetalle =
                    ajustesDeInventario.Select(mov => new AlmacenMovimientoDetalle
                    {
                        AlmacenMovimientoDetalleID =
                            mov.AlmacenMovimientoDetalleID
                    }).ToList();
                AlmacenMovimientoInfo almacenMovimiento =
                    almacenMovimientoBL.ObtenerMovimientoPorClaveDetalle(almancenMovimientosDetalle);
                if (almacenMovimiento == null)
                {
                    almacenMovimiento = new AlmacenMovimientoInfo();
                }
                AlmacenInfo almacen = ObtenerAlmacen(almacenMovimiento.AlmacenID);
                if (almacen == null)
                {
                    almacen = new AlmacenInfo
                    {
                        Organizacion = new OrganizacionInfo()
                    };
                }
                OrganizacionInfo organizacion = ObtenerOrganizacionIVA(almacen.Organizacion.OrganizacionID);
                if (organizacion == null)
                {
                    organizacion = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    };
                }
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
                                                          Descripcion = "Entrada De Almacen por Ajuste",
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
                                                                            almacenMovimiento.FechaMovimiento.ToShortDateString()),
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "FOLIO No.",
                                                                            almacenMovimiento.FolioMovimiento),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Folio");

                GenerarLineasEncabezadoDetalleEntrada();
                GenerarLineasDetalle(ajustesDeInventario);

                polizaImpresion.GenerarDetalles("Detalle");
                polizaImpresion.GenerarLineaEnBlanco("Detalle", 11);
                GeneraLineaTotalDetalle(ajustesDeInventario);
                GeneraLinea(11);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaObservaciones(ajustesDeInventario);
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Observaciones");
                
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(almacenMovimiento.FolioMovimiento);
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
            var ajustesDeInventario = datosPoliza as List<PolizaEntradaSalidaPorAjusteModel>;
            IList<PolizaInfo> polizas = ObtenerPoliza(ajustesDeInventario);
            return polizas;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

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
            const int CADENA_LARGA = 29;

            IList<CostoInfo> costos = ObtenerCostos();
            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                if (string.IsNullOrWhiteSpace(cargo.Descripcion))
                {
                    CostoInfo descripcionCosto =
                        costos.FirstOrDefault(
                            costo =>
                            cargo.Cuenta.EndsWith(costo.ClaveContable));
                    if (descripcionCosto == null)
                    {
                        cargo.Descripcion = string.Empty;
                    }
                    else
                    {
                        cargo.Descripcion = descripcionCosto.Descripcion;
                    }
                }

                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(cargo.Descripcion.Length > CADENA_LARGA
                                         ? cargo.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : cargo.Descripcion.Trim());
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta =
                        Cancelacion
                            ? string.IsNullOrWhiteSpace(cargo.Proveedor)
                                  ? cargo.Cuenta
                                  : cargo.Proveedor
                            : cargo.Cuenta,
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
                if (string.IsNullOrWhiteSpace(abono.Descripcion))
                {
                    CostoInfo descripcionCosto =
                        costos.FirstOrDefault(
                            costo =>
                            abono.Cuenta.EndsWith(costo.ClaveContable));
                    if (descripcionCosto == null)
                    {
                        abono.Descripcion = string.Empty;
                    }
                    else
                    {
                        abono.Descripcion = descripcionCosto.Descripcion;
                    }
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

        private void GeneraLineaLabPlanta(List<PolizaEntradaSalidaPorAjusteModel> polizaEntradaSalidaPorAjuste)
        {
            decimal precio = polizaEntradaSalidaPorAjuste.Sum(prec => prec.Precio);

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}   {1}", "LAB Planta -->",
                                                                        Math.Round(precio, 2).ToString("N")),
                                                      Alineacion = "left",
                                                      Desplazamiento = 7
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");
        }

        private void GeneraLineaOtrosCostoTotales()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "left",
                                                      Desplazamiento = 3
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "_____________",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "_____________",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion  = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },                                              
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "_____________",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "_____________",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Total",
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion  = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "0.00",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "0.00",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion  = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },                                              
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "0.00",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "0.00",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");
        }

        private void GeneraLineaOtrosCosto()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "OTROS COSTOS:",
                                                      Alineacion = "left",
                                                      Desplazamiento = 8
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");

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
                                                      Descripcion  = string.Empty,
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
                                                      Descripcion = "Iva",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion  = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },                                              
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Retención",
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
            polizaImpresion.GeneraCabecero(new[] { "10", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");
            GeneraLinea(8);
            polizaImpresion.GeneraCabecero(new[] { "100" }, "OtrosCostos");
        }

        private void GeneraLineaObservaciones(List<PolizaEntradaSalidaPorAjusteModel> ajustesDeInventario)
        {
            string[] observaciones = ajustesDeInventario.Select(obs => obs.Observaciones).Distinct().ToArray();
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

        private void GeneraLineaTotalDetalle(List<PolizaEntradaSalidaPorAjusteModel> ajustesDeInventario)
        {
            decimal total = ajustesDeInventario.Sum(imp => Math.Abs(Math.Round(imp.Cantidad * imp.Precio, 2)));
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            var polizaModel = new PolizaDetalleModel
            {
                ImportePromedio = total.ToString("N2"),
                PesoTotal = total.ToString("N2"),
                ImporteVenta = total.ToString("N2"),
            };
            PolizaModel.Detalle.Add(polizaModel);
            polizaImpresion.GenerarDetalles("Detalle");
        }

        private void GenerarLineasDetalle(List<PolizaEntradaSalidaPorAjusteModel> ajustesDeInventario)
        {
            IList<ProductoInfo> productos = ObtenerProductos();
            IList<UnidadMedicionInfo> unidades = ObtenerUnidadesMedicion();

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel polizaModel;
            for (int indexInventario = 0; indexInventario < ajustesDeInventario.Count; indexInventario++)
            {
                ProductoInfo producto =
                    productos.FirstOrDefault(prod => prod.ProductoId == ajustesDeInventario[indexInventario].ProductoID);
                if (producto == null)
                {
                    producto = new ProductoInfo();
                }
                polizaModel = new PolizaDetalleModel
                                  {
                                      CantidadCabezas = producto.ProductoId.ToString(),
                                      TipoGanado = producto.ProductoDescripcion,
                                      Lote = ajustesDeInventario[indexInventario].Lote == 0 ? string.Empty : ajustesDeInventario[indexInventario].Lote.ToString(),
                                      PesoOrigen =Math.Abs(ajustesDeInventario[indexInventario].Cantidad).ToString("N0"),
                                      PesoLlegada =Math.Abs(ajustesDeInventario[indexInventario].Cantidad).ToString("N0"),
                                      Corral =unidades.Where(clave => clave.UnidadID == producto.UnidadId).Select(uni => uni.ClaveUnidad).FirstOrDefault(),
                                      PrecioPromedio = ajustesDeInventario[indexInventario].Precio.ToString("N2"),
                                      ImportePromedio =Math.Abs(Math.Round(ajustesDeInventario[indexInventario].Cantidad * ajustesDeInventario[indexInventario].Precio, 2)).ToString("N2"),
                                  };
                PolizaModel.Detalle.Add(polizaModel);
            }
        }

        private void GenerarLineasEncabezadoDetalleEntrada()
        {
            var encabezados = new List<PolizaEncabezadoModel>
                                                          {
                                                              new PolizaEncabezadoModel
                                                                  {
                                                                      Descripcion = "Producto",
                                                                      Alineacion = "left",
                                                                      Desplazamiento = 3
                                                                  },
                                                              new PolizaEncabezadoModel
                                                                  {
                                                                      Descripcion = string.Empty,
                                                                      Alineacion = "right",
                                                                      Desplazamiento = 6
                                                                  },
                                                              new PolizaEncabezadoModel
                                                                  {
                                                                      Descripcion = "Costo",
                                                                      Alineacion = "center",
                                                                      Desplazamiento = 2
                                                                  }
                                                             
                                                          };
            GeneraLineaEncabezadoDetalle(encabezados);
            polizaImpresion.GeneraCabecero(new[] { "10", "2", "18", "10", "10", "3", "10", "10", "12", "12", "10" },"Detalle");
            encabezados = new List<PolizaEncabezadoModel>
                              {
                                  new PolizaEncabezadoModel
                                      {
                                          Descripcion = "Cod",
                                          Alineacion = "right",
                                          Desplazamiento = 0
                                      },
                                  new PolizaEncabezadoModel
                                      {
                                          Descripcion = string.Empty,
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
                                          Alineacion = "left",
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
                                          Descripcion = string.Empty,
                                          Alineacion = "left",
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
            GeneraLineaEncabezadoDetalle(encabezados);
            polizaImpresion.GeneraCabecero(new[] { "10", "2", "18", "10", "10", "3", "10", "10", "12", "12", "10" },"Detalle");
        }

        private IList<PolizaInfo> ObtenerPoliza(List<PolizaEntradaSalidaPorAjusteModel> ajustesDeInventario)
        {
            var polizasSalidaAjuste = new List<PolizaInfo>();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.EntradaAjuste.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.EntradaAjuste));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            var linea = 1;
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            List<AlmacenMovimientoDetalle> almancenMovimientosDetalle =
                ajustesDeInventario.Select(mov => new AlmacenMovimientoDetalle
                {
                    AlmacenMovimientoDetalleID = mov.AlmacenMovimientoDetalleID
                }).ToList();
            AlmacenMovimientoInfo almacenMovimiento =
                almacenMovimientoBL.ObtenerMovimientoPorClaveDetalle(almancenMovimientosDetalle);
            if (almacenMovimiento == null)
            {
                almacenMovimiento = new AlmacenMovimientoInfo();
            }
            string archivoFolio = ObtenerArchivoFolio(almacenMovimiento.FechaMovimiento);
            AlmacenInfo almacen = ObtenerAlmacen(almacenMovimiento.AlmacenID);
            if (almacen == null)
            {
                almacen = new AlmacenInfo
                {
                    Organizacion = new OrganizacionInfo()
                };
            }
            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(almacen.Organizacion.OrganizacionID);
            if (organizacion == null)
            {
                organizacion = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo()
                };
            }

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
            string numeroReferencia = ObtenerNumeroReferenciaFolio(almacenMovimiento.FolioMovimiento);

            IList<CuentaSAPInfo> cuentasSAP = ObtenerCuentasSAP();
            IList<ClaseCostoProductoInfo> almacenesProductos = ObtenerCostosProducto(almacen.AlmacenID);

            ParametroOrganizacionInfo parametroOrganizacionSuperavit;
            if (almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.Enfermeria.GetHashCode()
                || almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ManejoGanado.GetHashCode()
                || almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode())
            {
                parametroOrganizacionSuperavit =
                    ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                         ParametrosEnum.CTASUPERAVITENG.ToString());
            }
            else
            {
                parametroOrganizacionSuperavit =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID, ParametrosEnum.CTASUPERAVIT.ToString());
            }
            if (parametroOrganizacionSuperavit == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CUENTA DE SUPERAVIT NO CONFIGURADA"));
            }

            IList<ProductoInfo> productos = ObtenerProductos();
            IList<UnidadMedicionInfo> unidades = ObtenerUnidadesMedicion();
            IList<CuentaAlmacenSubFamiliaInfo> almacenesSubFamilia = ObtenerCostosSubFamilia(almacen.AlmacenID);
            ProductoInfo producto;
            PolizaEntradaSalidaPorAjusteModel ajuste;
            ClaseCostoProductoInfo almacenProducto;
            CuentaSAPInfo cuentaSAP;
            CuentaAlmacenSubFamiliaInfo almacenSubFamilia;
            ParametroOrganizacionInfo parametroCentroCosto =
                ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOMP.ToString());
            if (parametroCentroCosto == null)
            {
                throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
            }
            bool afectaCosto;
            for (var indexAjustes = 0; indexAjustes < ajustesDeInventario.Count; indexAjustes++)
            {
                ajuste = ajustesDeInventario[indexAjustes];
                cuentaSAP = null;

                producto = productos.FirstOrDefault(clave => clave.ProductoId == ajuste.ProductoID);
                afectaCosto = ValidarAfectacionCuentaCosto(producto);
                if (!afectaCosto && (almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.Enfermeria.GetHashCode()
                    || almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ManejoGanado.GetHashCode()
                    || almacen.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.ReimplanteGanado.GetHashCode()))
                {
                    almacenSubFamilia =
                        almacenesSubFamilia.FirstOrDefault(sub => sub.SubFamiliaID == producto.SubfamiliaId);
                    if (almacenSubFamilia != null)
                    {
                        cuentaSAP =
                            cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenSubFamilia.CuentaSAPID);
                    }
                }
                else
                {
                    almacenProducto =
                        almacenesProductos.FirstOrDefault(prod => prod.ProductoID == producto.ProductoId);
                    if (almacenProducto != null)
                    {
                        cuentaSAP =
                            cuentasSAP.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenProducto.CuentaSAPID);
                    }
                }
                if (cuentaSAP == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1} {2}", "CUENTA PARA PRODUCTO",
                                                              producto.ProductoDescripcion, "NO CONFIGURADA"));
                }
                UnidadMedicionInfo unidad = unidades.FirstOrDefault(uni => uni.UnidadID == producto.UnidadId);
                if (unidad == null)
                {
                    unidad = new UnidadMedicionInfo();
                }
                var datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia,
                                    FechaEntrada = almacenMovimiento.FechaMovimiento,
                                    Folio = almacenMovimiento.FolioMovimiento.ToString(),
                                    ClaseDocumento = postFijoRef3,
                                    Importe = string.Format("{0}", ajuste.Importe.ToString("F2")),
                                    Renglon = Convert.ToString(linea++),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = cuentaSAP.CuentaSAP,
                                    Division = organizacion.Division,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = cuentaSAP.Descripcion,
                                    CentroCosto =
                                        cuentaSAP.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto) ||
                                        cuentaSAP.CuentaSAP.StartsWith(PrefijoCuentaCentroGasto)
                                            ? parametroCentroCosto.Valor
                                            : string.Empty,
                                    PesoOrigen = Math.Round(ajuste.Cantidad, 0),
                                    TipoDocumento = textoDocumento,
                                    Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                             tipoMovimiento,
                                                             almacenMovimiento.FolioMovimiento,
                                                             producto.ProductoDescripcion,
                                                             string.Format("{0} {1}", ajuste.Cantidad.ToString("N2"),
                                                                           unidad.ClaveUnidad),
                                                             ajuste.Precio.ToString("C2")),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalidaAjuste.Add(polizaSalida);

                datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia,
                                FechaEntrada = almacenMovimiento.FechaMovimiento,
                                Folio = almacenMovimiento.FolioMovimiento.ToString(),
                                Importe = string.Format("{0}", (ajuste.Importe*-1).ToString("F2")),
                                Renglon = Convert.ToString(linea++),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = parametroOrganizacionSuperavit.Valor,
                                CentroCosto =
                                    parametroOrganizacionSuperavit.Valor.StartsWith(PrefijoCuentaCentroCosto) ||
                                    parametroOrganizacionSuperavit.Valor.StartsWith(PrefijoCuentaCentroGasto)
                                        ? parametroCentroCosto.Valor
                                        : string.Empty,
                                Division = organizacion.Division,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaSAP.Descripcion,
                                PesoOrigen = Math.Round(ajuste.Cantidad, 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4}",
                                                         tipoMovimiento,
                                                         almacenMovimiento.FolioMovimiento,
                                                         producto.ProductoDescripcion,
                                                         string.Format("{0} {1}", ajuste.Cantidad.ToString("N2"), unidad.ClaveUnidad),
                                                             ajuste.Precio.ToString("C2")),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalidaAjuste.Add(polizaSalida);
            }

            return polizasSalidaAjuste;
        }

        #endregion METODOS PRIVADOS
    }
}
