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
using System.Threading;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaEntradaCompra : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;

        private IList<ProductoInfo> productos;
        private IList<UnidadMedicionInfo> unidadesMedicion;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.EntradaCompra);

                var contenedor = datosPoliza as ContenedorEntradaMateriaPrimaInfo;
                OrganizacionInfo organizacionOrigen =
                    ObtenerOrganizacionIVA(contenedor.Contrato.Organizacion.OrganizacionID);

                var tipoContrato = (TipoContratoEnum)contenedor.Contrato.TipoContrato.TipoContratoId;
                var tipoPoliza = "Compra";
                switch (tipoContrato)
                {
                    case TipoContratoEnum.BodegaTercero:
                        tipoPoliza = "Traspaso";
                        break;
                }
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = organizacionOrigen.Descripcion,
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = string.Format("Entrada De Almacen por {0}", tipoPoliza),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "Fecha",
                                                                            contenedor.EntradaProducto.Fecha.
                                                                                ToShortDateString()),
                                                          //organizacionOrigen.TipoOrganizacion.DescripcionTipoProceso,
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "FOLIO No.",
                                                                            contenedor.Contrato.Folio),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Folio");

                GeneraLineaEncabezadoDetalle();

                ProductoInfo producto = contenedor.EntradaProducto.Producto;
                GeneraLineaDetalle(contenedor, producto);
                polizaImpresion.GenerarDetalles("Detalle");
                polizaImpresion.GenerarLineaEnBlanco("Detalle", 11);
                GeneraLineaTotalDetalle(contenedor, producto);
                GeneraLinea(11);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                string observaciones =
                    contenedor.ListaCostoEntradaMateriaPrima.Select(obs => obs.Observaciones).Distinct().FirstOrDefault();
                GeneraLineaObservaciones(string.IsNullOrWhiteSpace(observaciones)
                                             ? string.Empty
                                             : observaciones);
                polizaImpresion.GenerarLineaEnBlanco();
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Observaciones");

                GeneraLineaEncabezadoRegistroContable(contenedor.Contrato.Folio);
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
            List<PolizaInfo> polizaEntradaCompra;
            if (datosPoliza is ContenedorEntradaMateriaPrimaInfo)
            {
                var entradaMateriaPrima = datosPoliza as ContenedorEntradaMateriaPrimaInfo;
                polizaEntradaCompra = ObtenerPoliza(entradaMateriaPrima) as List<PolizaInfo>;
            }
            else
            {
                polizaEntradaCompra = new List<PolizaInfo>();
                var entradaMateriaPrima = datosPoliza as List<ContenedorEntradaMateriaPrimaInfo>;
                for (int i = 0; i < entradaMateriaPrima.Count; i++)
                {
                    polizaEntradaCompra.AddRange(ObtenerPoliza(entradaMateriaPrima[i]));
                }
            }
            return polizaEntradaCompra;
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

            IList<CuentaSAPInfo> cuentasSAP = ObtenerCuentasSAP();
            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                if (cargo.Descripcion == null)
                {
                    CuentaSAPInfo descripcionCosto =
                        cuentasSAP.FirstOrDefault(
                            costo =>
                            cargo.Cuenta.Equals(costo.CuentaSAP));
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
                if (abono.Descripcion == null)
                {
                    CuentaSAPInfo descripcionCosto =
                        cuentasSAP.FirstOrDefault(
                            costo =>
                            abono.Cuenta.Equals(costo.CuentaSAP));
                    if (descripcionCosto == null)
                    {
                        var proveedorBL = new ProveedorBL();
                        var proveedor = new ProveedorInfo
                        {
                            CodigoSAP = abono.Proveedor
                        };
                        proveedor = proveedorBL.ObtenerPorCodigoSAP(proveedor);
                        if (proveedor == null)
                        {
                            abono.Descripcion = string.Empty;
                        }
                        else
                        {
                            abono.Descripcion = proveedor.Descripcion;
                        }
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
                                                      Descripcion = observaciones,
                                                      Alineacion = "left",
                                                      Desplazamiento = 0
                                                  }
                                          };
            polizaImpresion.GeneraCabecero(new[] { "15", "90" }, "Observaciones");
            GeneraLinea(2);
            polizaImpresion.GenerarLineaEnBlanco("Observaciones", 2);
        }

        private void GeneraLineaTotalDetalle(ContenedorEntradaMateriaPrimaInfo entradas, ProductoInfo producto)
        {
            IList<AlmacenMovimientoDetalle> detalles =
                    entradas.EntradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle;
            decimal importeDetalle =
                detalles.Where(prod => prod.Producto.ProductoId == producto.ProductoId).Sum(imp => imp.Importe);
            decimal importeCosto = entradas.ListaCostoEntradaMateriaPrima.Sum(imp => imp.Importe);
            decimal importe = (importeDetalle + importeCosto);
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
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 4
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = Math.Round(importeDetalle, 2).ToString("N"),
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
                                                      Descripcion = Math.Round(importe, 2).ToString("N"),
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "12", "10", "12" },
                                           "Detalle");
        }

        /// <summary>
        /// Genera los detalles
        /// </summary>
        /// <param name="entradas"></param>
        /// <param name="producto"> </param>
        private void GeneraLineaDetalle(ContenedorEntradaMateriaPrimaInfo entradas, ProductoInfo producto)
        {
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            if (entradas != null)
            {
                if (productos == null)
                {
                    productos = ObtenerProductos();
                }
                if (unidadesMedicion == null)
                {
                    unidadesMedicion = ObtenerUnidadesMedicion();
                }
                IList<AlmacenMovimientoDetalle> detalles =
                    entradas.EntradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle;
                var entradaAgrupada = detalles.FirstOrDefault(prod => prod.Producto.ProductoId == producto.ProductoId);
                producto =
                    productos.FirstOrDefault(clave => clave.ProductoId == entradaAgrupada.Producto.ProductoId);
                decimal importeDetalle =
                    detalles.Where(prod => prod.Producto.ProductoId == producto.ProductoId).Sum(imp => imp.Importe);
                decimal importeCosto = entradas.ListaCostoEntradaMateriaPrima.Sum(imp => imp.Importe);
                decimal peso = Convert.ToDecimal((entradas.EntradaProducto.PesoBruto - entradas.EntradaProducto.PesoTara) - entradas.EntradaProducto.PesoDescuento);
                decimal pesoOrigen = "Origen".Equals(entradas.Contrato.PesoNegociar,
                                                     StringComparison.InvariantCultureIgnoreCase)
                                         ? Convert.ToDecimal(entradas.EntradaProducto.PesoBonificacion)
                                         : peso;
                if (producto != null)
                {
                    if (producto.SubfamiliaId ==
                    (int)SubFamiliasEnum.MicroIngredientes)
                    {
                        peso = entradas.EntradaProducto.PesoOrigen;
                        pesoOrigen = entradas.EntradaProducto.PesoOrigen;
                    }
                }
                decimal pesoDescuento = entradas.EntradaProducto.PesoBonificacion;
                decimal importeIntegrado = importeDetalle + importeCosto;
                decimal merma = 0;
                if (entradas.aplicaRestriccionDescuento)
                {
                    merma = entradas.PorcentajeRestriccionDescuento;
                }
                else
                {
                    merma = pesoDescuento > 0
                                        ? Convert.ToDecimal((pesoOrigen - (entradas.EntradaProducto.PesoBruto -
                                                                           entradas.EntradaProducto.PesoTara))) / pesoOrigen
                                        : 0;
                }
                var polizaDetalle = new PolizaDetalleModel
                {
                    TipoGanado = producto.ProductoDescripcion,
                    CantidadCabezas = producto.ProductoId.ToString(),
                    PesoTotal = string.Format("{0} %", merma.ToString("F2")),
                    Lote = Convert.ToString(entradas.EntradaProducto.AlmacenInventarioLote.Lote),
                    Corral =
                        unidadesMedicion.Where(clave => clave.UnidadID == producto.UnidadId).Select(
                            uni => uni.ClaveUnidad).
                        FirstOrDefault(),
                    PesoOrigen = Math.Round(pesoOrigen, 2).ToString("N0"),
                    PesoLlegada = Math.Round(peso, 2).ToString("N0"),
                    PrecioPromedio =
                        (Math.Round(importeDetalle / peso, 2)).ToString
                        ("N"),
                    ImportePromedio =
                        (Math.Round(importeDetalle, 2)).ToString
                        ("N"),
                    PrecioVenta =
                        (Math.Round(importeIntegrado / peso, 2)).ToString
                        ("N"),
                    ImporteVenta =
                        Math.Round(importeIntegrado, 2).ToString("N")
                };
                PolizaModel.Detalle.Add(polizaDetalle);
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
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "12", "10", "12" },
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
            polizaImpresion.GeneraCabecero(new[] { "10", "17", "10", "10", "10", "10", "12", "10", "12", "10", "12" },
                                           "Detalle");
        }

        private IList<PolizaInfo> ObtenerPoliza(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima)
        {
            var polizasConsumo = new List<PolizaInfo>();
            OrganizacionInfo organizacion =
                ObtenerOrganizacionIVA(entradaMateriaPrima.EntradaProducto.Organizacion.OrganizacionID);

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            IList<CostoInfo> costos = ObtenerCostos();
            if (unidadesMedicion == null)
            {
                unidadesMedicion = ObtenerUnidadesMedicion();
            }
            CuentaSAPInfo cuentaSap;
            PolizaInfo poliza;
            CostoInfo costo;

            TipoPolizaInfo tipoPoliza = 
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.EntradaCompra.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA", TipoPoliza.EntradaCompra));
            }

            string textoDocumento = tipoPoliza.TextoDocumento;
            string tipoMovimiento = tipoPoliza.ClavePoliza;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;

            IList<ClaseCostoProductoInfo> almacenesProductosCuentas =
                ObtenerCostosProducto(entradaMateriaPrima.EntradaProducto.AlmacenMovimiento.Almacen.AlmacenID);

            var tipoContrato = (TipoContratoEnum)entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId;
            ProductoInfo producto;

            decimal importeTotal;
            IList<CostoEntradaMateriaPrimaInfo> costrosEntrada = entradaMateriaPrima.ListaCostoEntradaMateriaPrima;
            CostoEntradaMateriaPrimaInfo costoEntrada;
            ClaseCostoProductoInfo almacenCierreDiaInventario;
            DatosPolizaInfo datos;
            var unidad = string.Empty;

            decimal kilosOrigen =
                Math.Round(
                    entradaMateriaPrima.EntradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle.Select(
                        kilos => kilos.Cantidad).FirstOrDefault(), 2);
            decimal  importeDetalle =
                    Math.Round(entradaMateriaPrima.EntradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle.Sum(
                        imp => imp.Importe), 2);
            
            decimal kilosBonificacion;
            AlmacenInfo almacen = null;
            var bodegaTerceros = false;

            decimal costoUnitario = importeDetalle / kilosOrigen;

            int renglon;
            int milisegundo;
            StringBuilder ref3;
            StringBuilder archivoFolio;

            switch (tipoContrato)
            {
                case TipoContratoEnum.BodegaTercero:
                    ProveedorAlmacenInfo proveedorAlmacen =
                        ObtenerProveedorAlmacen(entradaMateriaPrima.Contrato.Proveedor);
                    if (proveedorAlmacen != null)
                    {
                        almacen = ObtenerAlmacen(proveedorAlmacen.AlmacenId);
                    }
                    bodegaTerceros = true;
                    break;
            }

            IList<int> costosConRetencion = costrosEntrada.Select(cos => cos.Costos.CostoID).ToList();
            string numeroDocumento;
            for (var indexCostos = 0; indexCostos < costrosEntrada.Count; indexCostos++)
            {
                renglon = 0;
                milisegundo = DateTime.Now.Millisecond;

                ref3 = new StringBuilder();
                ref3.Append("03");
                ref3.Append(Convert.ToString(entradaMateriaPrima.EntradaProducto.Folio).PadLeft(10, ' '));
                ref3.Append(new Random(10).Next(10, 20));
                ref3.Append(new Random(30).Next(30, 40));
                ref3.Append(milisegundo);
                ref3.Append(postFijoRef3);

                Thread.Sleep(900);
                archivoFolio = new StringBuilder(ObtenerArchivoFolio(entradaMateriaPrima.EntradaProducto.Fecha));
                Thread.Sleep(900);

                //numeroDocumento = new StringBuilder(ObtenerNumeroReferencia);
                numeroDocumento = ObtenerNumeroReferenciaFolio(entradaMateriaPrima.EntradaProducto.Folio);

                costoEntrada = costrosEntrada[indexCostos];
                producto = entradaMateriaPrima.EntradaProducto.Producto;

                unidad =
                    unidadesMedicion.Where(clave => clave.UnidadID == producto.UnidadMedicion.UnidadID).Select(
                        uni => uni.ClaveUnidad).FirstOrDefault();

                bool esProveedor = !costoEntrada.TieneCuenta;
                bool tieneRetencion = costoEntrada.Retencion;
                bool tieneIva = costoEntrada.Iva;

                importeTotal = costoEntrada.Importe;

                almacenCierreDiaInventario =
                    almacenesProductosCuentas.FirstOrDefault(
                        prod => prod.ProductoID == producto.ProductoId);
                if (almacenCierreDiaInventario == null)
                {
                    almacenCierreDiaInventario = new ClaseCostoProductoInfo();
                }
                cuentaSap =
                    cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenCierreDiaInventario.CuentaSAPID);
                if (cuentaSap == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO HAY CONFIGURACION PARA EL PRODUCTO",
                                                              producto.Descripcion));
                }
                costo = costos.FirstOrDefault(clave => clave.CostoID == costoEntrada.Costos.CostoID);
                if (costo == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO HAY CONFIGURACION PARA EL COSTO",
                                                              costoEntrada.DescripcionCosto));
                }
                if (esProveedor)
                {
                    //costoEntrada.Provedor =
                    //    entradaMateriaPrima.ListaCostoEntradaMateriaPrima.Select(prov => prov.Provedor).
                    //    FirstOrDefault();

                    if (!tieneIva && !tieneRetencion)
                    {
                        renglon++;
                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                            Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                            Importe =
                                string.Format("{0}", importeTotal.ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = cuentaSap.CuentaSAP,
                            ArchivoFolio = archivoFolio.ToString(),
                            DescripcionCosto = producto.Descripcion,
                            PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                            Division = organizacion.Division,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                 tipoMovimiento,
                                                 entradaMateriaPrima.EntradaProducto.Folio,
                                                 kilosOrigen.ToString("N0"),
                                                 unidad, producto.Descripcion,
                                                 costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim(): ""),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        poliza = GeneraRegistroPoliza(datos);
                        polizasConsumo.Add(poliza);

                        kilosBonificacion = kilosOrigen;
                        if (entradaMateriaPrima.EntradaProducto.PesoBonificacion > 0)
                        {
                            kilosBonificacion = entradaMateriaPrima.EntradaProducto.PesoOrigen;
                        }
                        renglon++;
                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                            Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                            ClaveProveedor = costoEntrada.Provedor.CodigoSAP,
                            Importe =
                                string.Format("{0}", (importeTotal*-1).ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            Division = organizacion.Division,
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            ArchivoFolio = archivoFolio.ToString(),
                            DescripcionCosto = costoEntrada.Provedor.Descripcion,
                            PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                            ClaseDocumento = postFijoRef3,
                            TipoDocumento = textoDocumento,
                            Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                 tipoMovimiento,
                                                 entradaMateriaPrima.EntradaProducto.Folio,
                                                 kilosBonificacion.ToString("N0"),
                                                 unidad, costo.Descripcion,
                                                 costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        poliza = GeneraRegistroPoliza(datos);
                        polizasConsumo.Add(poliza);
                    }
                    else
                    {
                        if (tieneIva)
                        {
                            CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                                clave => clave.CuentaSAP.Equals(organizacion.Iva.CuentaRecuperar.ClaveCuenta));
                            if (cuentaIva == null)
                            {
                                cuentaIva = new CuentaSAPInfo { Descripcion = string.Empty };
                            }
                            renglon++;
                            var importeIva = (costoEntrada.Importe) * (organizacion.Iva.TasaIva / 100);
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                                Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                                Division = organizacion.Division,
                                ClaveProveedor = String.Empty,
                                Importe = string.Format("{0}", importeTotal.ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = cuentaSap.CuentaSAP,
                                ArchivoFolio = archivoFolio.ToString(),
                                DescripcionCosto = producto.Descripcion,
                                PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                     tipoMovimiento,
                                                     entradaMateriaPrima.EntradaProducto.Folio,
                                                     kilosOrigen.ToString("N0"),
                                                     unidad, producto.Descripcion,
                                                     costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizasConsumo.Add(poliza);

                            renglon++;
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                                Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                                ClaveProveedor = String.Empty,
                                Importe = string.Format("{0}", importeIva.ToString("F2")),
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = costoEntrada.Importe.ToString("F2"),
                                IndicadorImpuesto = organizacion.Iva.IndicadorIvaRecuperar,
                                ClaveImpuesto = ClaveImpuesto,
                                CondicionImpuesto = CondicionImpuesto,
                                Ref3 = ref3.ToString(),
                                Division = organizacion.Division,
                                Cuenta = organizacion.Iva.CuentaRecuperar.ClaveCuenta,
                                ArchivoFolio = archivoFolio.ToString(),
                                DescripcionCosto = cuentaIva.Descripcion,
                                PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                     tipoMovimiento,
                                                     entradaMateriaPrima.EntradaProducto.Folio,
                                                     kilosOrigen.ToString("N0"),
                                                     unidad, producto.Descripcion,
                                                     costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizasConsumo.Add(poliza);

                            renglon++;
                            kilosBonificacion = kilosOrigen;
                            if (entradaMateriaPrima.EntradaProducto.PesoBonificacion > 0)
                            {
                                kilosBonificacion = entradaMateriaPrima.EntradaProducto.PesoOrigen;
                            }
                            datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                                Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                                Division = organizacion.Division,
                                ClaveProveedor = costoEntrada.Provedor.CodigoSAP,
                                Importe = string.Format("{0}", ((importeTotal + importeIva)*-1).ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                ArchivoFolio = archivoFolio.ToString(),
                                DescripcionCosto = costoEntrada.Provedor.Descripcion,
                                PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                                TipoDocumento = textoDocumento,
                                ClaseDocumento = postFijoRef3,
                                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                     tipoMovimiento,
                                                     entradaMateriaPrima.EntradaProducto.Folio,
                                                     kilosBonificacion.ToString("N0"),
                                                     unidad, costo.Descripcion,
                                                     costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                                Sociedad = organizacion.Sociedad,
                                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            };
                            poliza = GeneraRegistroPoliza(datos);
                            polizasConsumo.Add(poliza);
                        }
                        if (tieneRetencion)
                        {
                            var retencionBL = new RetencionBL();
                            var retenciones = retencionBL.ObtenerRetencionesConCosto(costosConRetencion);
                            RetencionInfo retencion = null;
                            if (retenciones != null && retenciones.Any())
                            {
                                retencion =
                                    retenciones.FirstOrDefault(
                                        costoRet => costoRet.CostoID == costoEntrada.Costos.CostoID);
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
                                    FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                                    Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                                    Division = organizacion.Division,
                                    ClaveProveedor = costoEntrada.Provedor.CodigoSAP,
                                    Importe = string.Format("-{0}", "0"),
                                    IndicadorImpuesto = parametrosRetencion.ToString(),
                                    Renglon = Convert.ToString(renglon),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    CodigoRetencion = retencion.IndicadorImpuesto,
                                    TipoRetencion = retencion.IndicadorRetencion,
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = costoEntrada.Provedor.Descripcion,
                                    PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                                    TipoDocumento = textoDocumento,
                                    ClaseDocumento = postFijoRef3,
                                    Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                         tipoMovimiento,
                                                         entradaMateriaPrima.EntradaProducto.Folio,
                                                         kilosOrigen.ToString("N0"),
                                                         unidad, costo.Descripcion,
                                                         costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                                    Sociedad = organizacion.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                };
                                poliza = GeneraRegistroPoliza(datos);
                                polizasConsumo.Add(poliza);
                                if (!tieneIva)
                                {
                                    renglon++;
                                    datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                                        Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                                        Division = organizacion.Division,
                                        ClaveProveedor = costoEntrada.Provedor.CodigoSAP,
                                        Importe = string.Format("{0}", (importeTotal*-1).ToString("F2")),
                                        Renglon = Convert.ToString(renglon),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        ArchivoFolio = archivoFolio.ToString(),
                                        DescripcionCosto = costoEntrada.Provedor.Descripcion,
                                        PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                                        TipoDocumento = textoDocumento,
                                        ClaseDocumento = postFijoRef3,
                                        Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                             tipoMovimiento,
                                                             entradaMateriaPrima.EntradaProducto.Folio,
                                                             kilosOrigen.ToString("N0"),
                                                             unidad, costo.Descripcion,
                                                             costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                                    poliza = GeneraRegistroPoliza(datos);
                                    polizasConsumo.Add(poliza);
                                    renglon++;
                                    datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                                        Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                                        Division = organizacion.Division,
                                        ClaveProveedor = String.Empty,
                                        Importe = string.Format("{0}", importeTotal.ToString("F2")),
                                        IndicadorImpuesto = String.Empty,
                                        Renglon = Convert.ToString(renglon),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        Cuenta = cuentaSap.CuentaSAP,
                                        ArchivoFolio = archivoFolio.ToString(),
                                        DescripcionCosto = producto.Descripcion,
                                        PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                                        TipoDocumento = textoDocumento,
                                        ClaseDocumento = postFijoRef3,
                                        ComplementoRef1 = string.Empty,
                                        Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                                             tipoMovimiento,
                                                             entradaMateriaPrima.EntradaProducto.Folio,
                                                             kilosOrigen.ToString("N0"),
                                                             unidad, producto.Descripcion,
                                                             costoUnitario.ToString("N2"), postFijoRef3,
                                                 entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : ""),
                                        Sociedad = organizacion.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                                    };
                                    poliza = GeneraRegistroPoliza(datos);
                                    polizasConsumo.Add(poliza);
                                }
                            }
                        }
                    }
                }
                else
                {
                    kilosBonificacion = kilosOrigen;
                    if (entradaMateriaPrima.EntradaProducto.PesoBonificacion > 0)
                    {
                        kilosBonificacion = entradaMateriaPrima.EntradaProducto.PesoOrigen;
                    }
                    renglon++;
                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                        Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                        Division = organizacion.Division,
                        ClaveProveedor = String.Empty,
                        Importe = string.Format("{0}", (importeTotal*-1).ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(renglon),
                        Cabezas = string.Empty,
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = costoEntrada.CuentaSap,
                        ArchivoFolio = archivoFolio.ToString(),
                        DescripcionCosto = costoEntrada.DescripcionCuenta,
                        PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                             tipoMovimiento,
                                             entradaMateriaPrima.EntradaProducto.Folio,
                                             kilosBonificacion.ToString("N0"),
                                             unidad, costo.Descripcion,
                                             costoUnitario.ToString("N2"), postFijoRef3,
                                             entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    poliza = GeneraRegistroPoliza(datos);
                    polizasConsumo.Add(poliza);
                    renglon++;
                    if (entradaMateriaPrima.EntradaProducto.PesoBonificacion > 0)
                    {
                        kilosOrigen = entradaMateriaPrima.EntradaProducto.PesoBruto -
                                      entradaMateriaPrima.EntradaProducto.PesoTara;
                    }
                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                        Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                        Division = organizacion.Division,
                        Importe = string.Format("{0}", importeTotal.ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(renglon),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = cuentaSap.CuentaSAP,
                        ArchivoFolio = archivoFolio.ToString(),
                        DescripcionCosto = producto.Descripcion,
                        PesoOrigen = Math.Round(costoEntrada.KilosOrigen, 0),
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                             tipoMovimiento,
                                             entradaMateriaPrima.EntradaProducto.Folio,
                                             kilosOrigen.ToString("N0"),
                                             unidad, producto.Descripcion,
                                             costoUnitario.ToString("N2"), postFijoRef3,
                                             entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    poliza = GeneraRegistroPoliza(datos);
                    polizasConsumo.Add(poliza);
                }
            }

            producto = entradaMateriaPrima.EntradaProducto.Producto;
            if (producto == null)
            {
                producto = new ProductoInfo();
            }
            almacenCierreDiaInventario =
                    almacenesProductosCuentas.FirstOrDefault(
                        prod => prod.ProductoID == producto.ProductoId);
            if (almacenCierreDiaInventario == null)
            {
                almacenCierreDiaInventario = new ClaseCostoProductoInfo();
            }
            cuentaSap =
                cuentasSap.FirstOrDefault(cuenta => cuenta.CuentaSAPID == almacenCierreDiaInventario.CuentaSAPID);
            if (cuentaSap == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "NO HAY CONFIGURACION PARA EL PRODUCTO",
                                                          producto.Descripcion));
            }
            unidad =
                unidadesMedicion.Where(clave => clave.UnidadID == producto.UnidadMedicion.UnidadID).Select(
                    uni => uni.ClaveUnidad).FirstOrDefault();
            
            Thread.Sleep(900);
            milisegundo = DateTime.Now.Millisecond;
            ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(entradaMateriaPrima.EntradaProducto.Folio).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(milisegundo);
            ref3.Append(postFijoRef3);

            Thread.Sleep(900);
            archivoFolio = new StringBuilder(ObtenerArchivoFolio(entradaMateriaPrima.EntradaProducto.Fecha));

            //numeroDocumento = new StringBuilder(ObtenerNumeroReferencia);
            numeroDocumento = ObtenerNumeroReferenciaFolio(entradaMateriaPrima.EntradaProducto.Folio);

            renglon = 1;
            kilosBonificacion = kilosOrigen;
            if (entradaMateriaPrima.EntradaProducto.PesoBonificacion > 0)
            {
                kilosBonificacion = entradaMateriaPrima.EntradaProducto.PesoBruto -
                                    entradaMateriaPrima.EntradaProducto.PesoTara;
            }
            datos = new DatosPolizaInfo
            {
                NumeroReferencia = numeroDocumento,
                FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                Importe =
                    string.Format("{0}", importeDetalle.ToString("F2")),
                Renglon = Convert.ToString(renglon),
                ImporteIva = "0",
                Ref3 = ref3.ToString(),
                Cuenta = cuentaSap.CuentaSAP,
                ArchivoFolio = archivoFolio.ToString(),
                DescripcionCosto = producto.Descripcion,
                PesoOrigen = Math.Round(kilosBonificacion, 0),
                Division = organizacion.Division,
                TipoDocumento = textoDocumento,
                ClaseDocumento = postFijoRef3,
                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                     tipoMovimiento,
                                     entradaMateriaPrima.EntradaProducto.Folio,
                                     kilosBonificacion.ToString("N0"),
                                     unidad, producto.Descripcion,
                                     costoUnitario.ToString("N2"), postFijoRef3,
                                     entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                Sociedad = organizacion.Sociedad,
                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
            };
            poliza = GeneraRegistroPoliza(datos);
            polizasConsumo.Add(poliza);

            ProveedorInfo proveedor;
            bool esCuenta = false;
            if (bodegaTerceros)
            {
                proveedor =
                    entradaMateriaPrima.ListaCostoEntradaMateriaPrima.Select(prov => prov.Provedor).FirstOrDefault();
                cuentaSap = cuentasSap.FirstOrDefault(
                    cuenta => cuenta.CuentaSAPID == entradaMateriaPrima.Contrato.Cuenta.CuentaSAPID);
                if (cuentaSap != null)
                {
                    almacen.CuentaInventario = cuentaSap.CuentaSAP;
                }
            }
            else
            {
                proveedor = entradaMateriaPrima.Contrato.Proveedor;
                if (!proveedor.CodigoSAP.StartsWith("0"))
                {
                    esCuenta = true;
                }
            }
            if (esCuenta)
            {
                if (almacen == null)
                {
                    almacen = new AlmacenInfo();
                }
                almacen.CuentaInventario = proveedor.CodigoSAP;
                almacen.Descripcion = proveedor.Descripcion;
            }
            renglon++;
            if (entradaMateriaPrima.EntradaProducto.PesoBonificacion > 0)
            {
                kilosOrigen = entradaMateriaPrima.EntradaProducto.PesoBonificacion;
            }
            datos = new DatosPolizaInfo
            {
                NumeroReferencia = numeroDocumento,
                FechaEntrada = entradaMateriaPrima.EntradaProducto.Fecha,
                Folio = entradaMateriaPrima.EntradaProducto.Folio.ToString(),
                ClaveProveedor = bodegaTerceros || esCuenta ? string.Empty : proveedor.CodigoSAP,
                Cuenta = bodegaTerceros ? almacen.CuentaInventario : string.Empty,
                Importe =
                    string.Format("{0}", (importeDetalle*-1).ToString("F2")),
                Renglon = Convert.ToString(renglon),
                Division = organizacion.Division,
                ImporteIva = "0",
                Ref3 = ref3.ToString(),
                ArchivoFolio = archivoFolio.ToString(),
                DescripcionCosto = bodegaTerceros ? almacen.Descripcion : proveedor.Descripcion,
                PesoOrigen = Math.Round(kilosOrigen, 0),
                TipoDocumento = textoDocumento,
                ClaseDocumento = postFijoRef3,
                Concepto = String.Format("{0}-{1} {2} {3} {4} ${5} {6} {7}",
                                     tipoMovimiento,
                                     entradaMateriaPrima.EntradaProducto.Folio,
                                     kilosOrigen.ToString("N0"),
                                     unidad, producto.Descripcion,
                                     costoUnitario.ToString("N2"), postFijoRef3,
                                     entradaMateriaPrima.EntradaProducto.NotaDeVenta != null ? entradaMateriaPrima.EntradaProducto.NotaDeVenta.Trim() : "").Trim(),
                Sociedad = organizacion.Sociedad,
                Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
            };
            if (esCuenta)
            {
                if (string.IsNullOrWhiteSpace(datos.Cuenta))
                {
                    datos.Cuenta = almacen.CuentaInventario;
                }
            }
            poliza = GeneraRegistroPoliza(datos);
            polizasConsumo.Add(poliza);
            return polizasConsumo;
        }

        #endregion METODOS PRIVADOS
    }
}
