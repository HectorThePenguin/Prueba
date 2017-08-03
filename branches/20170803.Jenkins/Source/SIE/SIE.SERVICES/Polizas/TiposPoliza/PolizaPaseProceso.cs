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
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaPaseProceso : PolizaAbstract
    {
        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;
        private IList<CostoInfo> costos;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                var datosPolizaPaseProceso = datosPoliza as List<PolizaPaseProcesoModel>;

                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.PaseProceso);

                int organizacionID =
                    datosPolizaPaseProceso.Select(org => org.Organizacion.OrganizacionID).FirstOrDefault();
                int folioPedido = datosPolizaPaseProceso.Select(folio => folio.Pedido.FolioPedido).FirstOrDefault();
                int ticket = datosPolizaPaseProceso.Select(folio => folio.PesajeMateriaPrima.Ticket).FirstOrDefault();
                DateTime fechaPedido = datosPolizaPaseProceso.Select(folio => folio.Pedido.FechaPedido).FirstOrDefault();

                OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);
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
                                                          Descripcion = "Entrada De Almacen por Producción en Proceso",
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
                                                                            fechaPedido.ToShortDateString()),
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}-{2}", "FOLIO No.", folioPedido, ticket),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Folio");

                GeneraLineaEncabezadoDetalle();
                GeneraLineaDetalle(datosPolizaPaseProceso);
                polizaImpresion.GenerarDetalles("Detalle");
                polizaImpresion.GenerarLineaEnBlanco("Detalle", 12);

                GeneraLineaTotalDetalle(datosPolizaPaseProceso);

                GeneraLinea(12);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaObservaciones(datosPolizaPaseProceso);
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Observaciones");

                GeneraLineaOtrosCosto();
                GeneraLineaOtrosCostosDetalle(datosPolizaPaseProceso, organizacion);
                polizaImpresion.GeneraCostos("OtrosCostos");

                GeneraLineaOtrosCostosTotales(datosPolizaPaseProceso, organizacion);
                polizaImpresion.GeneraCostos("OtrosCostos");

                polizaImpresion.GenerarLineaEnBlanco("OtrosCostos", 9);
                GeneraLineaLabPlanta(datosPolizaPaseProceso);

                GeneraLinea(9);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "OtrosCostos");
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(folioPedido);
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
            var datosPolizaPaseProceso = datosPoliza as List<PolizaPaseProcesoModel>;
            IList<PolizaInfo> polizas = ObtenerPolizas(datosPolizaPaseProceso);
            return polizas;
        }

        protected override void GeneraLineaRegistroContable(IList<PolizaInfo> polizas, out IList<PolizaInfo> cargos, out IList<PolizaInfo> abonos)
        {
            base.GeneraLineaRegistroContable(polizas, out cargos, out abonos);
            const int CADENA_LARGA = 29;

            if (costos == null)
            {
                costos = ObtenerCostos();
            }

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

        private void GeneraLineaLabPlanta(List<PolizaPaseProcesoModel> datosPolizaPaseProceso)
        {
            decimal importes = datosPolizaPaseProceso.Sum(imp => imp.AlmacenMovimientoDetalle.Precio);
            decimal pesoOrigen =
                datosPolizaPaseProceso.Sum(peso => peso.ProgramacionMateriaPrima.CantidadEntregada);

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
                                                                        Math.Round(
                                                                            importes/pesoOrigen == 0 ? 1 : pesoOrigen, 2)
                                                                            .ToString("N")),
                                                      Alineacion = "left",
                                                      Desplazamiento = 8
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");
        }

        private void GeneraLineaOtrosCostosTotales(List<PolizaPaseProcesoModel> datosPolizaPaseProceso, OrganizacionInfo organizacion)
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "left",
                                                      Desplazamiento = 4
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
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");

            PolizaModel.Costos = new List<PolizaCostoModel>();          

            decimal parcial = datosPolizaPaseProceso.SelectMany(pol => pol.ListaAlmacenMovimientoCosto).Sum(cost => cost.Importe);
            decimal costoIva = parcial * (organizacion.Iva.TasaIva / 100);            
            var costoModel = new PolizaCostoModel
            {
                Descripcion = "Total",
                Parcial = parcial.ToString("N2"),
                Iva = costoIva.ToString("N2"),
                Observaciones = "0",
                Total = (parcial + costoIva).ToString("N2")
            };
            PolizaModel.Costos.Add(costoModel);
        }

        private void GeneraLineaOtrosCostosDetalle(List<PolizaPaseProcesoModel> datosPolizaPaseProceso, OrganizacionInfo organizacion)
        {
            if (costos == null)
            {
                costos = ObtenerCostos();
            }
           
            CostoInfo costo;
            PolizaModel.Costos = new List<PolizaCostoModel>();
            PolizaCostoModel costoModel;
            for (int indexPoliza = 0; indexPoliza < datosPolizaPaseProceso.Count; indexPoliza++)
            {
                foreach (var costoMovimiento in datosPolizaPaseProceso[indexPoliza].ListaAlmacenMovimientoCosto)
                {
                    costo =
                    costos.FirstOrDefault(
                        clave => clave.CostoID == costoMovimiento.Costo.CostoID);
                    decimal parcial = costoMovimiento.Importe;
                    decimal iva = Math.Round(parcial * (organizacion.Iva.TasaIva / 100), 2);

                    costoModel = new PolizaCostoModel
                    {
                        Descripcion = costo.Descripcion,
                        Clave = costo.CostoID.ToString(),
                        Parcial = parcial.ToString("N2"),
                        Iva = iva.ToString("N2"),
                        Observaciones = "0",
                        Total = (parcial + iva).ToString("N2")
                    };
                    PolizaModel.Costos.Add(costoModel);

                }                          
            }
        }

        private void GeneraLineaOtrosCosto()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "OTROS COSTOS:",
                                                      Alineacion = "left",
                                                      Desplazamiento = 9
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");

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
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "60", "10", "20", "20", "5", "20", "20" }, "OtrosCostos");
            GeneraLinea(9);
            polizaImpresion.GeneraCabecero(new[] { "100" }, "OtrosCostos");
        }

        private void GeneraLineaObservaciones(List<PolizaPaseProcesoModel> datosPolizaPaseProceso)
        {
            string[] observaciones =
                datosPolizaPaseProceso.Select(obs => obs.ProgramacionMateriaPrima.Observaciones).Distinct().ToArray();
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

        private void GeneraLineaTotalDetalle(List<PolizaPaseProcesoModel> datosPolizaPaseProceso)
        {
            decimal importesIntegrado = 0;
            datosPolizaPaseProceso.ForEach(dato =>
                                               {
                                                   decimal importeCosto;

                                                   if (dato.FleteInternoCosto.TipoTarifaID == TipoTarifaEnum.Tonelada.GetHashCode())
                                                   {
                                                       importeCosto = dato.FleteInternoCosto.Tarifa *
                                                                      (dato.ProgramacionMateriaPrima.CantidadEntregada /
                                                                       1000);
                                                   }
                                                   else
                                                   {
                                                       importeCosto = dato.FleteInternoCosto.Tarifa;
                                                   }
                                                   importesIntegrado += importeCosto;
                                               });
            decimal importe = 0;
            datosPolizaPaseProceso.ForEach(dato =>
                                               {
                                                   importe += (dato.AlmacenMovimientoDetalle.Precio *
                                                               (dato.ProgramacionMateriaPrima.CantidadEntregada));
                                               });
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
                                                      Descripcion = "Total",
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
                                                      Descripcion = importe.ToString("N2"),
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
                                                      Descripcion = importesIntegrado.ToString("N2"),
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "17", "10", "10", "10", "10", "12", "10", "10", "10", "10" },
                                           "Detalle");
        }

        private void GeneraLineaDetalle(List<PolizaPaseProcesoModel> datosPolizaPaseProceso)
        {
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel polizaDetalleModel;
            PolizaPaseProcesoModel polizaPaseProcesoModel;
            IList<UnidadMedicionInfo> unidadesMedicion = ObtenerUnidadesMedicion();
            for (int indexPoliza = 0; indexPoliza < datosPolizaPaseProceso.Count; indexPoliza++)
            {
                polizaPaseProcesoModel = datosPolizaPaseProceso[indexPoliza];
                //decimal costoProducto =
                //    datosPolizaPaseProceso.Where(
                //        prod => prod.Producto.ProductoId == polizaPaseProcesoModel.Producto.ProductoId).Sum(
                //            costo => costo.FleteInternoCosto.Tarifa);

                decimal importeCosto;

                var fleteInterno =
                    datosPolizaPaseProceso.Where(
                        prod => prod.Producto.ProductoId == polizaPaseProcesoModel.Producto.ProductoId).Select(
                            costo => costo.FleteInternoCosto).FirstOrDefault();

                if (fleteInterno.TipoTarifaID == TipoTarifaEnum.Tonelada.GetHashCode())
                {
                    importeCosto = (polizaPaseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada / 1000) *
                                   fleteInterno.Tarifa;
                }
                else
                {
                    importeCosto = fleteInterno.Tarifa;
                }

                polizaDetalleModel = new PolizaDetalleModel
                                         {
                                             CantidadCabezas = polizaPaseProcesoModel.Producto.ProductoId.ToString(),
                                             TipoGanado = polizaPaseProcesoModel.Producto.Descripcion,
                                             Lote = polizaPaseProcesoModel.AlmacenInventarioLote.Lote.ToString(),
                                             PesoOrigen =
                                                 (polizaPaseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada).
                                                 ToString("N0"),
                                             PesoLlegada =
                                                 (polizaPaseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada).
                                                 ToString("N0"),
                                             Corral =
                                                 unidadesMedicion.Where(
                                                     uni => uni.UnidadID == polizaPaseProcesoModel.Producto.UnidadId).
                                                 Select(uni => uni.ClaveUnidad).FirstOrDefault(),
                                             PrecioPromedio =
                                                 polizaPaseProcesoModel.AlmacenMovimientoDetalle.Precio.ToString("N0"),
                                             ImportePromedio = (
                                                                   (polizaPaseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada) *
                                                                   polizaPaseProcesoModel.AlmacenMovimientoDetalle.
                                                                       Precio).ToString("N2"),
                                             PrecioVenta =
                                                 polizaPaseProcesoModel.AlmacenMovimientoDetalle.Precio +
                                                  (importeCosto / polizaPaseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada).ToString("N0"),
                                             ImporteVenta = ((polizaPaseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada *
                                                                polizaPaseProcesoModel.AlmacenMovimientoDetalle.Precio) +
                                                                 importeCosto).ToString("N2"),
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
                                                      Desplazamiento = 3
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
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "17", "10", "10", "10", "10", "12", "10", "10", "10", "10" },
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
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "17", "10", "10", "10", "10", "12", "10", "10", "10", "10" },
                                           "Detalle");
        }

        private IList<PolizaInfo> ObtenerPolizas(List<PolizaPaseProcesoModel> datosPolizaPaseProceso)
        {
            var polizasPaseProceso = new List<PolizaInfo>();

            long folioPedido =
                datosPolizaPaseProceso.Select(folio => folio.Pedido.FolioPedido).FirstOrDefault();
            int organizacionID =
                datosPolizaPaseProceso.Select(folio => folio.Organizacion.OrganizacionID).FirstOrDefault();
            DateTime fechaPedido =
                datosPolizaPaseProceso.Select(folio => folio.Pedido.FechaPedido).FirstOrDefault();

            int miliSegunda = DateTime.Now.Millisecond;

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            IList<ClaseCostoProductoInfo> claseCostoProducto;
            IList<CostoInfo> costos = ObtenerCostos();

            string archivoFolio = ObtenerArchivoFolio(fechaPedido);

            OrganizacionInfo organizacion = ObtenerOrganizacionIVA(organizacionID);
            PolizaPaseProcesoModel paseProcesoModel;
            CostoInfo costo;
            PolizaInfo poliza;

            string division = organizacion.Division;
            string sociendad = organizacion.Sociedad;

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.PaseProceso.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.PaseProceso));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            ClaseCostoProductoInfo costoProductoMateriaPrima;
            CuentaSAPInfo cuentaSapMateriaPrima;
            ClaseCostoProductoInfo costoProductoPlataAlimentos;
            CuentaSAPInfo cuentaSapPlantaAlimentos;
            CuentaSAPInfo cuentaIva;

            IList<UnidadMedicionInfo> unidadesMedicion = ObtenerUnidadesMedicion();

            var renglon = 1;

            datosPolizaPaseProceso.ForEach(datos =>
                                               {
                                                   if (datos.FleteInternoCosto == null)
                                                   {
                                                       datos.FleteInternoCosto = new FleteInternoCostoInfo();
                                                   }
                                                   if (datos.FleteInternoCosto.Costo == null)
                                                   {
                                                       datos.FleteInternoCosto.Costo = new CostoInfo();
                                                   }
                                               });

            var datosAgrupados =
                datosPolizaPaseProceso
                    .GroupBy(grupo => new { grupo.Producto.ProductoId })
                    .Select(datos =>
                            new PolizaPaseProcesoModel
                                {
                                    AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                                                    {
                                                        Lote = datos.Select(lot => lot.AlmacenInventarioLote.Lote).FirstOrDefault()
                                                    },
                                    Producto = new ProductoInfo
                                                   {
                                                       Descripcion = datos.Select(prod => prod.Producto.Descripcion).FirstOrDefault(),
                                                       ProductoId = datos.Key.ProductoId,
                                                       UnidadId = datos.Select(uni => uni.Producto.UnidadId).FirstOrDefault()
                                                   },
                                    PesajeMateriaPrima = new PesajeMateriaPrimaInfo
                                                    {
                                                        Ticket = datos.Select(matPri => matPri.PesajeMateriaPrima.Ticket).FirstOrDefault(),
                                                        PesoTara = datos.Select(peso => peso.PesajeMateriaPrima.PesoTara).FirstOrDefault(),
                                                        PesoBruto = datos.Select(peso => peso.PesajeMateriaPrima.PesoBruto).FirstOrDefault()
                                                    },
                                    Almacen = new AlmacenInfo
                                                  {
                                                      AlmacenID = datos.Select(alma => alma.Almacen.AlmacenID).FirstOrDefault(),
                                                      Descripcion = datos.Select(alma => alma.Almacen.Descripcion).FirstOrDefault()
                                                  },
                                    Pedido = new PedidoInfo
                                                 {
                                                     FolioPedido = datos.Select(ped => ped.Pedido.FolioPedido).FirstOrDefault(),
                                                     FechaPedido = datos.Select(ped => ped.Pedido.FechaPedido).FirstOrDefault()
                                                 },
                                    Organizacion = new OrganizacionInfo
                                                {
                                                    OrganizacionID = datos.Select(org => org.Organizacion.OrganizacionID).FirstOrDefault(),
                                                    Descripcion = datos.Select(org => org.Organizacion.Descripcion).FirstOrDefault()
                                                },
                                    Proveedor = new ProveedorInfo
                                                {
                                                    CodigoSAP = datos.Select(prov => prov.Proveedor.CodigoSAP).FirstOrDefault(),
                                                    Descripcion = datos.Select(prov => prov.Proveedor.Descripcion).FirstOrDefault()
                                                },
                                    AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle
                                                {
                                                    Precio = datos.Select(almDet => almDet.AlmacenMovimientoDetalle.Precio).FirstOrDefault(),
                                                    Importe = datos.Select(almDet => almDet.AlmacenMovimientoDetalle.Importe).FirstOrDefault()
                                                },
                                    FleteInternoCosto = new FleteInternoCostoInfo
                                                {
                                                    Costo = new CostoInfo
                                                                {
                                                                    CostoID = datos.Select(cos => cos.FleteInternoCosto.Costo.CostoID).FirstOrDefault()
                                                                },
                                                    Tarifa = datos.Select(flete => flete.FleteInternoCosto.Tarifa).FirstOrDefault(),
                                                    TipoTarifaID = datos.Select(flete => flete.FleteInternoCosto.TipoTarifaID).FirstOrDefault()
                                                },
                                    ProgramacionMateriaPrima = new ProgramacionMateriaPrimaInfo
                                                {
                                                    Observaciones = datos.Select(prog => prog.ProgramacionMateriaPrima.Observaciones).FirstOrDefault(),
                                                    CantidadEntregada = datos.Select(prog => prog.ProgramacionMateriaPrima.CantidadEntregada).FirstOrDefault(),
                                                    Almacen = new AlmacenInfo
                                                                {
                                                                    AlmacenID = datos.Select(prog => prog.ProgramacionMateriaPrima.Almacen.AlmacenID).FirstOrDefault(),
                                                                    Descripcion = datos.Select(prog => prog.ProgramacionMateriaPrima.Almacen.Descripcion).FirstOrDefault()
                                                                }
                                                },
                                    ListaAlmacenMovimientoCosto = datos.Select(prog => prog.ListaAlmacenMovimientoCosto).FirstOrDefault()
                                }).ToList();
            decimal cantidadEntregada;
            DatosPolizaInfo datosPoliza;
            StringBuilder numeroReferencia;
            for (int indexPaseProceso = 0; indexPaseProceso < datosAgrupados.Count; indexPaseProceso++)
            {
                var ref3 = new StringBuilder();
                ref3.Append("03");
                ref3.Append(Convert.ToString(folioPedido).PadLeft(10, ' '));
                ref3.Append(new Random(10).Next(10, 20));
                ref3.Append(new Random(30).Next(30, 40));
                ref3.Append(miliSegunda++);
                ref3.Append(postFijoRef3);

                paseProcesoModel = datosAgrupados[indexPaseProceso];

                if (paseProcesoModel.ListaAlmacenMovimientoCosto == null)
                {
                    paseProcesoModel.ListaAlmacenMovimientoCosto = new List<AlmacenMovimientoCostoInfo>();
                }

                claseCostoProducto = ObtenerCostosProducto(paseProcesoModel.Almacen.AlmacenID);
                if (claseCostoProducto == null || !claseCostoProducto.Any())
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA EL ALMACEN",
                                                              paseProcesoModel.Almacen.Descripcion));
                }
                costo = costos.FirstOrDefault(clave => clave.CostoID == paseProcesoModel.FleteInternoCosto.Costo.CostoID);
                if (costo == null)
                {
                    costo = new CostoInfo();
                }
                costoProductoMateriaPrima =
                    claseCostoProducto.FirstOrDefault(clave => clave.ProductoID == paseProcesoModel.Producto.ProductoId);
                if (costoProductoMateriaPrima == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA EL PRODUCTO",
                                                              paseProcesoModel.Producto.Descripcion));
                }
                cuentaSapMateriaPrima =
                    cuentasSap.FirstOrDefault(clave => clave.CuentaSAPID == costoProductoMateriaPrima.CuentaSAPID);
                if (cuentaSapMateriaPrima == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA EL COSTO",
                                                              costo.Descripcion));
                }

                claseCostoProducto = ObtenerCostosProducto(paseProcesoModel.ProgramacionMateriaPrima.Almacen.AlmacenID);
                costoProductoPlataAlimentos =
                    claseCostoProducto.FirstOrDefault(clave => clave.ProductoID == paseProcesoModel.Producto.ProductoId);
                if (costoProductoPlataAlimentos == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA EL PRODUCTO",
                                                              paseProcesoModel.Producto.Descripcion));
                }
                cuentaSapPlantaAlimentos =
                    cuentasSap.FirstOrDefault(clave => clave.CuentaSAPID == costoProductoPlataAlimentos.CuentaSAPID);
                if (cuentaSapPlantaAlimentos == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA EL COSTO",
                                                              costo.Descripcion));
                }

                cuentaIva = cuentasSap.FirstOrDefault(
                    clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                if (cuentaIva == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACIONES PARA LA CUENTA",
                                                              organizacion.Iva.Descripcion));
                }

                cantidadEntregada = paseProcesoModel.ProgramacionMateriaPrima.CantidadEntregada;

               
                numeroReferencia = new StringBuilder(string.Format("{0}-{1}", folioPedido, paseProcesoModel.PesajeMateriaPrima.Ticket));
                decimal importeIva;
                foreach (AlmacenMovimientoCostoInfo costoMovimiento in paseProcesoModel.ListaAlmacenMovimientoCosto)
                {
                    importeIva = Math.Round((costoMovimiento.Importe) * (organizacion.Iva.TasaIva / 100), 2);
                    datosPoliza = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia.ToString(),
                        FechaEntrada = fechaPedido,
                        Folio = folioPedido.ToString(),
                        Division = division,
                        ClaseDocumento = postFijoRef3,
                        Importe = string.Format("{0}", Math.Round(costoMovimiento.Importe, 2).ToString("F2")),
                        Renglon = Convert.ToString(renglon++),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = cuentaSapPlantaAlimentos.CuentaSAP,
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = cuentaSapPlantaAlimentos.Descripcion,
                        PesoOrigen =
                            Math.Round(cantidadEntregada, 0),
                        TipoDocumento = textoDocumento,
                        Concepto = String.Format("{0}-{1}-{2} {3} {4} {5}",
                                                 tipoMovimiento
                                                 , folioPedido
                                                 , paseProcesoModel.PesajeMateriaPrima.Ticket
                                                 , cantidadEntregada.ToString("N0")
                                                 , unidadesMedicion.Where(uni => uni.UnidadID == paseProcesoModel.Producto.UnidadId).
                                                     Select(uni => uni.ClaveUnidad).FirstOrDefault(),
                                                 costoMovimiento.Costo.Descripcion),
                        Sociedad = sociendad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, sociendad),
                    };
                    poliza = GeneraRegistroPoliza(datosPoliza);
                    polizasPaseProceso.Add(poliza);

                    datosPoliza = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia.ToString(),
                        FechaEntrada = fechaPedido,
                        Folio = folioPedido.ToString(),
                        ClaseDocumento = postFijoRef3,
                        Cuenta = organizacion.Iva.CuentaRecuperar.ClaveCuenta,
                        Division = organizacion.Division,
                        Importe = string.Format("{0}", Math.Round(importeIva, 2).ToString("F2")),
                        Renglon = Convert.ToString(renglon++),
                        ImporteIva = (costoMovimiento.Importe).ToString("F2"),
                        Ref3 = ref3.ToString(),
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = cuentaIva.Descripcion,
                        IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                        ClaveImpuesto = ClaveImpuesto,
                        CondicionImpuesto = CondicionImpuesto,
                        PesoOrigen = Math.Round(cantidadEntregada, 0),
                        TipoDocumento = textoDocumento,
                        Concepto = String.Format("{0}-{1}-{2} {3} {4} {5}",
                                                 tipoMovimiento
                                                 , folioPedido
                                                 , paseProcesoModel.PesajeMateriaPrima.Ticket,
                                                 cantidadEntregada.ToString("N0")
                                                 , unidadesMedicion.Where(uni => uni.UnidadID == paseProcesoModel.Producto.UnidadId).
                                                       Select(uni => uni.ClaveUnidad).FirstOrDefault(),
                                                 (costoMovimiento.Importe).ToString("C2")),
                        Sociedad = sociendad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, sociendad),
                    };
                    poliza = GeneraRegistroPoliza(datosPoliza);
                    polizasPaseProceso.Add(poliza);

                    datosPoliza = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia.ToString(),
                        FechaEntrada = fechaPedido,
                        Folio = folioPedido.ToString(),
                        CabezasRecibidas = string.Empty,
                        NumeroDocumento = string.Empty,
                        Division = division,
                        ClaseDocumento = postFijoRef3,
                        Importe =
                            string.Format("-{0}",Math.Round(importeIva + (costoMovimiento.Importe), 2).ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        CentroCosto = string.Empty,
                        Renglon = Convert.ToString(renglon++),
                        Cabezas = string.Empty,
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        CodigoRetencion = string.Empty,
                        TipoRetencion = string.Empty,
                        ArchivoFolio = archivoFolio,
                        DescripcionCosto = paseProcesoModel.Proveedor.Descripcion,
                        ClaveProveedor = paseProcesoModel.Proveedor.CodigoSAP,
                        PesoOrigen = Math.Round(cantidadEntregada, 0),
                        TipoDocumento = textoDocumento,
                        ComplementoRef1 = string.Empty,
                        Concepto = String.Format("{0}-{1}-{2} {3}",
                                                 tipoMovimiento
                                                 , folioPedido
                                                 , paseProcesoModel.PesajeMateriaPrima.Ticket
                                                 , costoMovimiento.Costo.Descripcion),
                        Sociedad = sociendad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, sociendad),
                    };
                    poliza = GeneraRegistroPoliza(datosPoliza);
                    polizasPaseProceso.Add(poliza);
                }
                datosPoliza = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroReferencia.ToString(),
                                    FechaEntrada = fechaPedido,
                                    Folio = folioPedido.ToString(),
                                    Division = division,
                                    ClaseDocumento = postFijoRef3,
                                    Importe =
                                        string.Format("{0}", paseProcesoModel.AlmacenMovimientoDetalle.Importe.ToString("F2")),
                                    Renglon = Convert.ToString(renglon++),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = cuentaSapPlantaAlimentos.CuentaSAP,
                                    ArchivoFolio = archivoFolio,
                                    DescripcionCosto = cuentaSapPlantaAlimentos.Descripcion,
                                    PesoOrigen = Math.Round(cantidadEntregada, 0),
                                    TipoDocumento = textoDocumento,
                                    Concepto = String.Format("{0}-{1}-{2} {3} {4} {5}",
                                                             tipoMovimiento
                                                             , folioPedido
                                                             , paseProcesoModel.PesajeMateriaPrima.Ticket,
                                                             cantidadEntregada.ToString("N0")
                                                             , unidadesMedicion.Where(uni =>uni.UnidadID == paseProcesoModel.Producto.UnidadId).
                                                                   Select(uni => uni.ClaveUnidad).FirstOrDefault(),
                                                             paseProcesoModel.Producto.Descripcion),
                                    Sociedad = sociendad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, sociendad),
                                };
                poliza = GeneraRegistroPoliza(datosPoliza);
                polizasPaseProceso.Add(poliza);

                datosPoliza = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroReferencia.ToString(),
                                FechaEntrada = fechaPedido,
                                Folio = folioPedido.ToString(),
                                CabezasRecibidas = string.Empty,
                                NumeroDocumento = string.Empty,
                                Division = division,
                                ClaseDocumento = postFijoRef3,
                                ClaveProveedor = String.Empty,
                                Importe =
                                    string.Format("-{0}",paseProcesoModel.AlmacenMovimientoDetalle.Importe.ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                CentroCosto = string.Empty,
                                Renglon = Convert.ToString(renglon++),
                                Cabezas = string.Empty,
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSapMateriaPrima.CuentaSAP,
                                CodigoRetencion = string.Empty,
                                TipoRetencion = string.Empty,
                                ArchivoFolio = archivoFolio,
                                DescripcionCosto = cuentaSapMateriaPrima.Descripcion,
                                PesoOrigen = Math.Round(cantidadEntregada, 0),
                                TipoDocumento = textoDocumento,
                                ComplementoRef1 = string.Empty,
                                Concepto = String.Format("{0}-{1}-{2} {3} {4} {5}",
                                                         tipoMovimiento
                                                         , folioPedido
                                                         , paseProcesoModel.PesajeMateriaPrima.Ticket,
                                                         cantidadEntregada.ToString("N0")
                                                         , unidadesMedicion.Where(uni =>uni.UnidadID == paseProcesoModel.Producto.UnidadId).
                                                               Select(uni => uni.ClaveUnidad).FirstOrDefault(),
                                                         paseProcesoModel.Producto.Descripcion),
                                Sociedad = sociendad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, sociendad),
                            };
                poliza = GeneraRegistroPoliza(datosPoliza);
                polizasPaseProceso.Add(poliza);
            }
            return polizasPaseProceso;
        }

        #endregion METODOS PRIVADOS
    }
}
