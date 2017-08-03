using System.Globalization;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using System.IO;
using System.Threading;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaEntradaGanadoIntensivo : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private PolizaImpresion<PolizaModel> polizaImpresion;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS
        /// <summary>
        /// Genera el XML de la Poliza
        /// </summary>
        public override IList<PolizaInfo> GeneraPoliza(object contenedorCosteoEntrada)
        {
            var contenedor = contenedorCosteoEntrada as ContenedorCosteoEntradaGanadoInfo;
            List<EntradaGanadoCostoInfo> costosEntrada = contenedor.EntradaGanadoCosteo.ListaCostoEntrada;
            List<EntradaDetalleInfo> detalleEntradaCostos = contenedor.EntradaGanadoCosteo.ListaEntradaDetalle;
            EntradaGanadoInfo entradaGanadoInfo = contenedor.EntradaGanado;
            EntradaGanadoTransitoInfo entradaGanadoTransito = contenedor.EntradaGanadoTransito;

            var polizaEntradaGanado = GeneraPoliza(costosEntrada, detalleEntradaCostos, entradaGanadoInfo,
                                                   entradaGanadoTransito);
            return polizaEntradaGanado;
        }

        /// <summary>
        /// Genera la Poliza
        /// </summary>
        public override MemoryStream ImprimePoliza(object contenedorCosteoEntrada, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.EntradaGanadoDurango);

                var contenedor = contenedorCosteoEntrada as ContenedorCosteoEntradaGanadoInfo;
                OrganizacionInfo organizacionOrigen =
                    ObtenerOrganizacionIVA(contenedor.EntradaGanado.OrganizacionOrigenID);

                OrganizacionInfo organizacionDestino = ObtenerOrganizacionIVA(contenedor.EntradaGanado.OrganizacionID);
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = organizacionDestino.Descripcion,
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = "Nota De Entrada De Ganado",
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              organizacionOrigen.TipoOrganizacion.DescripcionTipoProceso,
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0} {1}", "FOLIO No.",
                                                                            contenedor.EntradaGanado.FolioEntrada),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Folio");

                ProveedorInfo proveedor;

                const int CENTROS = 4;
                const int PRADERAS = 5;
                const int DESCANSO = 6;
                const int CADIS = 7;

                switch (organizacionOrigen.TipoOrganizacion.TipoOrganizacionID)
                {
                    case CENTROS:
                    case PRADERAS:
                    case DESCANSO:
                    case CADIS:
                        proveedor = new ProveedorInfo { Descripcion = organizacionOrigen.Descripcion };
                        break;
                    default:
                        proveedor = (from costos in contenedor.EntradaGanadoCosteo.ListaCostoEntrada
                                     where "001".Equals(costos.Costo.ClaveContable)
                                     select costos.Proveedor).FirstOrDefault();
                        break;
                }
                if (proveedor == null)
                {
                    var cuentaProvision = (from costos in contenedor.EntradaGanadoCosteo.ListaCostoEntrada
                                           where "001".Equals(costos.Costo.ClaveContable)
                                           select costos.DescripcionCuenta).FirstOrDefault();
                    proveedor = new ProveedorInfo
                    {
                        Descripcion =
                            string.IsNullOrWhiteSpace(cuentaProvision) ? string.Empty : cuentaProvision
                    };
                }
                ProveedorInfo comprador = (from costos in contenedor.EntradaGanadoCosteo.ListaCostoEntrada
                                           where "003".Equals(costos.Costo.ClaveContable) && costos.Proveedor != null
                                           select costos.Proveedor).FirstOrDefault();
                if (comprador == null)
                {
                    var cuentaComprador = (from costos in contenedor.EntradaGanadoCosteo.ListaCostoEntrada
                                           where "003".Equals(costos.Costo.ClaveContable)
                                           select costos.CuentaProvision).FirstOrDefault();
                    comprador = new ProveedorInfo
                    {
                        Descripcion =
                            string.IsNullOrWhiteSpace(cuentaComprador) ? string.Empty : cuentaComprador
                    };
                }

                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0}:{1}", "PROVEEDOR",
                                                                            proveedor.Descripcion),
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0}:{1}", "REFERENCIA",
                                                                            organizacionOrigen.Descripcion),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Proveedor");

                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0}:{1}", "COMPRADOR",
                                                                            comprador.Descripcion),
                                                          Desplazamiento = 0
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion =
                                                              string.Format("{0}:{1}", "FECHA",
                                                                            contenedor.EntradaGanado.FechaEntrada.
                                                                                ToShortDateString()),
                                                          Desplazamiento = 0
                                                      },
                                              };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Comprador");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Comprador");

                GeneraLineaEncabezadoDetalle();
                GeneraLineasDetalle(contenedor.EntradaGanadoCosteo.ListaEntradaDetalle, contenedor.EntradaGanado);
                polizaImpresion.GenerarDetalles("Detalle");

                GeneraLinea(11);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");
                GeneraLineaTotales(contenedor.EntradaGanadoCosteo.ListaEntradaDetalle);
                polizaImpresion.GenerarDetalles("Detalle");

                GeneraLinea(11);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                var pesoNeto = contenedor.EntradaGanado.PesoBruto -
                               contenedor.EntradaGanado.PesoTara;
                GeneraLineasEncabezadoMerma(contenedor.EntradaGanadoCosteo.ListaEntradaDetalle, pesoNeto);
                polizaImpresion.GeneraCabecero(
                    new[] { "13", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" },
                    "Merma");
                GeneraLineaEncabezadoCalidad(contenedor);
                polizaImpresion.GeneraCabecero(
                    new[] { "13", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" },
                    "Merma");
                GeneraLineaCalidad(contenedor.EntradaGanadoCosteo.ListaCalidadGanado, organizacionOrigen.Descripcion);
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoCostos();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaCostos(contenedor.EntradaGanadoCosteo.ListaCostoEntrada,
                                  contenedor.EntradaGanadoCosteo.Observacion);
                polizaImpresion.GeneraCostos("Costos");

                GeneraLineaCostosTotales();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaTotalCostos(contenedor.EntradaGanadoCosteo.ListaCostoEntrada);

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Costos");
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(contenedor.EntradaGanado.FolioEntrada);
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                GeneraLineaSubEncabezadoRegistroContable(true, "No DE CUENTA", "CARGOS", "ABONOS");
                polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                IList<PolizaInfo> cargos;
                IList<PolizaInfo> abonos;
                GeneraLineaRegistroContable(polizas, out cargos, out abonos);
                polizaImpresion.GenerarRegistroContable("RegistroContable");

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");
                GenerarLineaSumaRegistroContable(polizas, "*= SUMAS -=>");
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

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

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
            const int CADENA_LARGA = 29;

            IList<CostoInfo> costos = ObtenerCostos();
            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                if (cargo.Descripcion == null)
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
                if (abono.Descripcion == null)
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

        /// <summary>
        /// Genera linea para el total de costo
        /// </summary>
        /// <param name="costos"></param>
        private void GeneraLineaTotalCostos(IEnumerable<EntradaGanadoCostoInfo> costos)
        {
            decimal totalCosto = costos.Sum(cos => cos.Importe);
            GenerarLineaTotalCosto(totalCosto, false);

            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);

            GenerarLineaElaboro();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
        }

        /// <summary>
        /// Genera linea para Costo
        /// </summary>
        /// <param name="costos"></param>
        /// <param name="observacion"> </param>
        private void GeneraLineaCostos(IList<EntradaGanadoCostoInfo> costos, string observacion)
        {
            var observaciones = new HashSet<string>();
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(observacion))
            {
                var index = 0;
                foreach (char obs in observacion)
                {
                    index++;
                    if (index == 120)
                    {
                        break;
                    }
                    sb.Append(obs);
                    if (index % 27 == 0)
                    {
                        observaciones.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
            }
            const int NUMERO_LINEA = 0;
            var observacionesImpresas = new List<string>(observaciones);
            PolizaModel.Costos = new List<PolizaCostoModel>();
            PolizaCostoModel polizaCosto;
            foreach (var costo in costos)
            {
                if (!observacionesImpresas.Any())
                {
                    observacionesImpresas.Add(string.Empty);
                }
                var indice = costo.Costo.Descripcion.IndexOf("FLETE", StringComparison.OrdinalIgnoreCase);
                polizaCosto = new PolizaCostoModel
                {
                    Descripcion = costo.Costo.Descripcion,
                    Parcial =
                        indice >= 0
                            ? costo.Importe.ToString("N", CultureInfo.CurrentCulture)
                            : string.Empty,
                    Total = costo.Importe.ToString("N", CultureInfo.CurrentCulture),
                    Observaciones = observacionesImpresas[NUMERO_LINEA].Trim()
                };
                PolizaModel.Costos.Add(polizaCosto);
                if (!string.IsNullOrWhiteSpace(observacionesImpresas[NUMERO_LINEA]))
                {
                    observacionesImpresas.RemoveAt(NUMERO_LINEA);
                }
            }
            if (observacionesImpresas.Any()
                && !string.IsNullOrWhiteSpace(observacionesImpresas[NUMERO_LINEA]))
            {
                foreach (var obs in observacionesImpresas)
                {
                    sb = new StringBuilder();
                    sb.AppendFormat("{0}{1}", string.Empty.PadRight(72)
                                    , obs);
                    polizaCosto = new PolizaCostoModel
                    {
                        Descripcion = string.Empty,
                        Parcial = string.Empty,
                        Total = string.Empty,
                        Observaciones = sb.ToString()
                    };
                    PolizaModel.Costos.Add(polizaCosto);
                }
            }
        }

        /// <summary>
        /// Genera linea de Merma
        /// </summary>
        /// <param name="detalles"></param>
        /// <param name="pesoLlegada"></param>
        private void GeneraLineasEncabezadoMerma(IEnumerable<EntradaDetalleInfo> detalles, decimal pesoLlegada)
        {
            var pesoOrigenTotal = detalles.Sum(pesoOrigen => pesoOrigen.PesoOrigen);
            var porcentajeMerma = Math.Round(((pesoOrigenTotal - pesoLlegada) / pesoOrigenTotal) * 100, 2);
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "MERMA DE TRANSPORTE:",
                                                      Desplazamiento = 4
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0} {1}",
                                                                        Math.Round(pesoOrigenTotal - pesoLlegada, 2).
                                                                            ToString("N", CultureInfo.CurrentCulture),
                                                                        "kg"),
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Format("{0}{1}", porcentajeMerma, "%"),
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Desplazamiento = 8
                                                  },
                                          };
        }

        /// <summary>
        /// Genera la Linea para Encabezado de Calidad
        /// </summary>
        /// <param name="contenedorCosteoEntrada"></param>
        private void GeneraLineaEncabezadoCalidad(ContenedorCosteoEntradaGanadoInfo contenedorCosteoEntrada)
        {
            IList<String> calidades =
                contenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Where(
                    sexo => Sexo.Macho.Equals(sexo.CalidadGanado.Sexo)).Select(calidad => calidad.CalidadGanado.Calidad)
                    .Distinct().ToList();
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "CALIDAD",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "Sexo",
                                                  },
                                          };
            PolizaEncabezadoModel encabezado;
            foreach (var calidad in calidades)
            {
                if (calidad != null)
                {
                    encabezado = new PolizaEncabezadoModel
                    {
                        Descripcion = calidad.Trim().Replace(" ", string.Empty)
                    };
                    PolizaModel.Encabezados.Add(encabezado);
                }
            }
        }

        /// <summary>
        /// Genera Linea de Calidad
        /// </summary>
        /// <param name="calidades"></param>
        /// <param name="organizacionOrigen"></param>
        private void GeneraLineaCalidad(ICollection<EntradaGanadoCalidadInfo> calidades, string organizacionOrigen)
        {
            IList<Sexo> sexos =
                Enum.GetValues(typeof(Sexo)).Cast<Sexo>().OrderByDescending(desx => desx.ToString()).ToList();
            foreach (var sexo in sexos)
            {
                IList<EntradaGanadoCalidadInfo> calidadMachos =
                    calidades.Where(sex => sexo.Equals(sex.CalidadGanado.Sexo)).Select(calidad => calidad).Distinct().
                        ToList();
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                              {
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = string.Empty
                                                      },
                                                  new PolizaEncabezadoModel
                                                      {
                                                          Descripcion = sexo.ToString()
                                                      }
                                              };
                GeneraValoresCalidadGanado(calidadMachos);
                polizaImpresion.GeneraCabecero(
                    new[] { "13", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" },
                    "Merma");
            }

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion =
                                                          string.Format("{0}{1}", "ORIGEN -->", organizacionOrigen),
                                                      Desplazamiento = 14
                                                  }
                                          };
            polizaImpresion.GeneraCabecero(
                new[] { "13", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10", "10" },
                "Merma");
        }

        /// <summary>
        /// Genera la linea para los
        /// valores de las calidades
        /// </summary>
        /// <param name="calidades"></param>
        private void GeneraValoresCalidadGanado(IList<EntradaGanadoCalidadInfo> calidades)
        {
            PolizaEncabezadoModel encabezado;
            for (var indexMachos = 0; indexMachos < calidades.Count; indexMachos++)
            {
                var calidad = calidades[indexMachos];
                encabezado = new PolizaEncabezadoModel
                {
                    Descripcion = calidad.Valor.ToString()
                };
                PolizaModel.Encabezados.Add(encabezado);
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
                                                      Descripcion =
                                                          string.Format("{0}{1}{2}", "---", "PESO DEL GANADO", "---"),
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "CABEZAS",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Alineacion = "right",
                                                      Desplazamiento = 6
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PROMEDIO",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "10", "10", "3", "17", "10", "12", "3", "15", "10", "10" },
                                           "Detalle");

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "ORIGEN",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "LLEGADA",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "TOTAL",
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
                                                      Descripcion = "TIPO GANADO",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PRECIO",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "IMPORTE",
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
                                                      Descripcion = "LOTE-CORRAL",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PESO",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "IMPORTE",
                                                      Alineacion = "right",
                                                      Desplazamiento = 0
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "10", "10", "3", "17", "10", "12", "3", "20", "10", "10" },
                                           "Detalle");
        }

        /// <summary>
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="detalles"></param>
        /// <param name="entradaGanado"></param>
        private void GeneraLineasDetalle(IList<EntradaDetalleInfo> detalles, EntradaGanadoInfo entradaGanado)
        {
            var pesoLlegada = (entradaGanado.PesoBruto - entradaGanado.PesoTara);
            var pesoOrigenTotal = detalles.Sum(pesoOrigen => pesoOrigen.PesoOrigen);
            var porcentajeMerma = Math.Round(((pesoOrigenTotal - pesoLlegada) / pesoOrigenTotal) * 100, 2);

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel polizaDetalle;
            foreach (var detalle in detalles)
            {
                var pesoPromedio = decimal.Round(detalle.PesoOrigen / detalle.Cabezas, 2);
                var importePromedio = decimal.Round(detalle.Importe / detalle.Cabezas, 2);

                var peso = detalle.PesoOrigen - Math.Round(detalle.PesoOrigen * (porcentajeMerma / 100), 2);

                detalle.PesoLlegada = peso;

                polizaDetalle = new PolizaDetalleModel
                {
                    PesoOrigen = Convert.ToInt32(detalle.PesoOrigen)
                        .ToString("N", CultureInfo.CurrentCulture)
                        .Replace(".00", string.Empty),
                    PesoLlegada = Convert.ToInt32(peso).ToString("N", CultureInfo.CurrentCulture)
                        .Replace(".00", string.Empty),
                    CantidadCabezas = detalle.Cabezas.ToString(),
                    TipoGanado = detalle.TipoGanado.Descripcion,
                    PrecioVenta = detalle.PrecioKilo.ToString("N", CultureInfo.CurrentCulture),
                    ImporteVenta = detalle.Importe.ToString("N", CultureInfo.CurrentCulture),
                    Lote =
                        string.Format("{0}-{1}", entradaGanado.CodigoLote,
                                      entradaGanado.CodigoCorral),
                    PesoPromedio = Convert.ToInt32(pesoPromedio)
                        .ToString("N", CultureInfo.CurrentCulture)
                        .Replace(".00", string.Empty),
                    ImportePromedio = importePromedio.ToString("N", CultureInfo.CurrentCulture)
                };
                PolizaModel.Detalle.Add(polizaDetalle);
            }
        }

        /// <summary>
        /// Genera los totales por Detalle
        /// </summary>
        /// <param name="detalles"></param>
        private void GeneraLineaTotales(ICollection<EntradaDetalleInfo> detalles)
        {
            var sumaPesoOrigen = Math.Round(detalles.Sum(pesoOrigen => pesoOrigen.PesoOrigen), 0);
            var sumaPesoLlegada = Math.Round(detalles.Sum(llegada => llegada.PesoLlegada), 0);
            var sumaCabezas = Math.Round((decimal)detalles.Sum(cabezas => cabezas.Cabezas), 2);
            var promedioPrecio = Math.Round(detalles.Sum(precio => precio.PrecioKilo) / detalles.Count, 2);
            var sumaImporte = Math.Round(detalles.Sum(importe => importe.Importe), 2);
            var promedioPeso = Math.Round(sumaPesoOrigen / sumaCabezas, 2);
            var promedioImporte = Math.Round(sumaImporte / sumaCabezas, 2);

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            var polizaDetalle = new PolizaDetalleModel
            {
                PesoOrigen = sumaPesoOrigen.ToString("N", CultureInfo.CurrentCulture).Replace(".00", string.Empty),
                PesoLlegada = sumaPesoLlegada.ToString("N", CultureInfo.CurrentCulture).Replace(".00", string.Empty),
                CantidadCabezas = sumaCabezas.ToString(),
                TipoGanado = string.Empty,
                PrecioVenta = promedioPrecio.ToString("N", CultureInfo.CurrentCulture),
                ImporteVenta = sumaImporte.ToString("N", CultureInfo.CurrentCulture),
                Lote = string.Empty,
                PesoPromedio = Convert.ToInt32(promedioPeso).ToString(),
                ImportePromedio = promedioImporte.ToString("N", CultureInfo.CurrentCulture)
            };
            PolizaModel.Detalle.Add(polizaDetalle);
        }

        #endregion IMPRESION

        #region XML

        /// <summary>
        /// Genera el XML para la Poliza
        /// </summary>
        /// <param name="costos"></param>
        /// <param name="detalleEntradaCostos"></param>
        /// <param name="entradaGanado"></param>
        /// <param name="entradaGanadoTransito"></param>
        private IList<PolizaInfo> GeneraPoliza(IList<EntradaGanadoCostoInfo> costos
                                             , IList<EntradaDetalleInfo> detalleEntradaCostos
                                             , EntradaGanadoInfo entradaGanado
                                             , EntradaGanadoTransitoInfo entradaGanadoTransito)
        {
            var polizasEntrada = new List<PolizaInfo>();

            var retencionBL = new RetencionBL();
            var retenciones = retencionBL.ObtenerRetencionesConCosto(costos.Select(co => co.Costo.CostoID).ToList());

            var milisegundo = DateTime.Now.Millisecond;

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.EntradaGanadoDurango.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.EntradaGanadoDurango));
            }
            string textoDocumento = tipoPoliza.TextoDocumento;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;
            string prefijoConcepto = tipoPoliza.ClavePoliza;

            OrganizacionInfo organizacionDestino = ObtenerOrganizacionIVA(entradaGanado.OrganizacionID);
            string division = organizacionDestino.Division;
            var sociedad = "300";

            //OrganizacionInfo organizacionIntensivo = ObtenerOrganizacionIVA(entradaGanado.OrganizacionOrigenID);

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(entradaGanado.FolioEntrada).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(milisegundo++);
            ref3.Append(postFijoRef3);

            var archivoFolio = new StringBuilder(ObtenerArchivoFolio(entradaGanado.FechaEntrada));
            var numeroDocumento = ObtenerNumeroReferenciaFolio(entradaGanado.FolioEntrada);
            //var numeroDocumento =
            //    new StringBuilder(string.Format("{0}{1}", entradaGanado.FolioEntrada, ObtenerNumeroReferencia));

            costos = costos.OrderBy(prov => !prov.TieneCuenta).ToList();
            var renglon = 0;

            int cabezasRecibidas = detalleEntradaCostos.Sum(cab => cab.Cabezas);
            decimal importeCosto;
            foreach (var entradaCostoEntrada in costos)
            {
                var esProveedor = !entradaCostoEntrada.TieneCuenta;
                var tieneRetencion = entradaCostoEntrada.Retencion;

                importeCosto = entradaCostoEntrada.Importe;

                if (esProveedor)
                {
                    Thread.Sleep(900);
                    ref3 = new StringBuilder();
                    ref3.Append("03");
                    ref3.Append(Convert.ToString(entradaGanado.FolioEntrada).PadLeft(10, ' '));
                    ref3.Append(new Random(10).Next(10, 20));
                    ref3.Append(new Random(30).Next(30, 40));
                    ref3.Append(milisegundo++);
                    ref3.Append(postFijoRef3);

                    archivoFolio = new StringBuilder(ObtenerArchivoFolio(entradaGanado.FechaEntrada));
                    numeroDocumento = ObtenerNumeroReferenciaFolio(entradaGanado.FolioEntrada);
                    //numeroDocumento =
                    //    new StringBuilder(string.Format("{0}{1}", entradaGanado.FolioEntrada, ObtenerNumeroReferencia));
                    renglon = 0;
                }

                ClaveContableInfo claveContableInfo = ObtenerCuentaInventario(entradaCostoEntrada.Costo
                                                                              , organizacionDestino.OrganizacionID
                                                                              , TipoPoliza.EntradaGanadoDurango);
                CuentaSAPInfo cuentaSap =
                    cuentasSap.FirstOrDefault(clave => clave.CuentaSAP.Equals(claveContableInfo.Valor));
                if (cuentaSap == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "NO SE CUENTA CON CONFIGURACIÓN PARA EL COSTO",
                                                              entradaCostoEntrada.Costo.Descripcion));
                }
                CuentaSAPInfo cuentaSapProvision =
                    cuentasSap.FirstOrDefault(
                        clave => clave.CuentaSAP.Equals(entradaCostoEntrada.CuentaProvision));
                if (cuentaSapProvision == null && !esProveedor)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}",
                                                              "NO SE CUENTA CON CONFIGURACIÓN PARA LA CUENTA DE PROVISION",
                                                              entradaCostoEntrada.CuentaProvision));
                }
                decimal pesoOrigen = Math.Round(detalleEntradaCostos.Sum(peso => peso.PesoOrigen), 0);

                const string COMPLEMENTO_REF1 = "czas.";
                const string UNIDAD_MOVIMIENTO = "Kgs.";
                const string DESCRIPCION_MOVIMIENTO = "CABEZAS";
                foreach (var entradaDetalle in detalleEntradaCostos)
                {
                    Thread.Sleep(900);

                    PolizaInfo polizaEntrada;
                    if (esProveedor)
                    {
                        if (!entradaCostoEntrada.Iva && !tieneRetencion)
                        {
                            renglon++;
                            var datos = new DatosPolizaInfo
                            {
                                NumeroReferencia = numeroDocumento,
                                FechaEntrada = entradaGanado.FechaEntrada,
                                Folio = entradaGanado.FolioEntrada.ToString(),
                                CabezasRecibidas = cabezasRecibidas.ToString(),
                                NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                ClaveProveedor = entradaCostoEntrada.Proveedor.CodigoSAP,
                                ClaseDocumento = postFijoRef3,
                                Importe =
                                    string.Format("{0}", Cancelacion ? importeCosto.ToString("F2") 
                                                                     : (importeCosto * -1).ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                ArchivoFolio = archivoFolio.ToString(),
                                DescripcionCosto = entradaCostoEntrada.Proveedor.Descripcion,
                                PesoOrigen = pesoOrigen,
                                Division = division,
                                TipoDocumento = textoDocumento,
                                ComplementoRef1 = COMPLEMENTO_REF1,
                                Concepto =
                                    String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                  entradaGanado.FolioEntrada,
                                                  cabezasRecibidas, DESCRIPCION_MOVIMIENTO,
                                                  pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                                Sociedad = sociedad,
                                Segmento = "S300",
                            };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasEntrada.Add(polizaEntrada);

                            renglon++;
                            datos = new DatosPolizaInfo
                            {
                                FechaEntrada = entradaGanado.FechaEntrada,
                                NumeroReferencia = numeroDocumento,
                                Folio = entradaGanado.FolioEntrada.ToString(),
                                CabezasRecibidas = cabezasRecibidas.ToString(),
                                NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                ClaveProveedor = String.Empty,
                                ClaseDocumento = postFijoRef3,
                                Importe =
                                    string.Format("{0}",
                                                  Cancelacion ? (importeCosto * -1).ToString("F2") 
                                                              : importeCosto.ToString("F2")),
                                IndicadorImpuesto = String.Empty,
                                Renglon = Convert.ToString(renglon),
                                Division = division,
                                Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                ImporteIva = "0",
                                Ref3 = ref3.ToString(),
                                Cuenta = claveContableInfo.Valor,
                                ArchivoFolio = archivoFolio.ToString(),
                                DescripcionCosto = cuentaSap.Descripcion,
                                PesoOrigen = pesoOrigen,
                                TipoDocumento = textoDocumento,
                                ComplementoRef1 = COMPLEMENTO_REF1,
                                Concepto =
                                    String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                  entradaGanado.FolioEntrada,
                                                  cabezasRecibidas, DESCRIPCION_MOVIMIENTO,
                                                  pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                                Sociedad = sociedad,
                                Segmento = "S300",
                            };
                            polizaEntrada = GeneraRegistroPoliza(datos);
                            polizasEntrada.Add(polizaEntrada);
                        }
                        else
                        {
                            if (entradaCostoEntrada.Iva)
                            {
                                CuentaSAPInfo cuentaIva = cuentasSap.FirstOrDefault(
                                    clave => clave.CuentaSAP.Equals(organizacionDestino.Iva.CuentaRecuperar.ClaveCuenta));
                                if (cuentaIva == null)
                                {
                                    cuentaIva = new CuentaSAPInfo { Descripcion = string.Empty };
                                }
                                renglon++;
                                var importeIva = entradaCostoEntrada.Importe * (organizacionDestino.Iva.TasaIva / 100);
                                var datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroDocumento,
                                    FechaEntrada = entradaGanado.FechaEntrada,
                                    Folio = entradaGanado.FolioEntrada.ToString(),
                                    CabezasRecibidas = cabezasRecibidas.ToString(),
                                    Division = division,
                                    NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                    ClaseDocumento = postFijoRef3,
                                    ClaveProveedor = String.Empty,
                                    Importe = string.Format("{0}",
                                                            Cancelacion ? (importeCosto * -1).ToString("F2") 
                                                                        : importeCosto.ToString("F2")),
                                    IndicadorImpuesto = String.Empty,
                                    Renglon = Convert.ToString(renglon),
                                    Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    Cuenta = claveContableInfo.Valor,
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = cuentaSap.Descripcion,
                                    PesoOrigen = pesoOrigen,
                                    TipoDocumento = textoDocumento,
                                    ComplementoRef1 = COMPLEMENTO_REF1,
                                    Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                             prefijoConcepto,
                                                             entradaGanado.FolioEntrada
                                                             ,
                                                             entradaGanado.CabezasRecibidas
                                                             ,
                                                             DESCRIPCION_MOVIMIENTO
                                                             ,
                                                             pesoOrigen.ToString("N0"),
                                                             UNIDAD_MOVIMIENTO),
                                    Sociedad = sociedad,
                                    Segmento = "S300",
                                };
                                polizaEntrada = GeneraRegistroPoliza(datos);
                                polizasEntrada.Add(polizaEntrada);
                                renglon++;
                                datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroDocumento,
                                    FechaEntrada = entradaGanado.FechaEntrada,
                                    Folio = entradaGanado.FolioEntrada.ToString(),
                                    CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                                    NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                    ClaseDocumento = postFijoRef3,
                                    ClaveProveedor = String.Empty,
                                    Importe = string.Format("{0}",
                                                            Cancelacion ? (importeIva * -1).ToString("F2") 
                                                                        : importeIva.ToString("F2"))
                                    ,
                                    IndicadorImpuesto = "V2",
                                    Division = division,
                                    Renglon = Convert.ToString(renglon),
                                    Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                    ImporteIva = entradaCostoEntrada.Importe.ToString("F2"),
                                    Ref3 = ref3.ToString(),
                                    Cuenta = "1140101003",
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = cuentaIva.Descripcion,
                                    PesoOrigen = pesoOrigen,
                                    TipoDocumento = textoDocumento,
                                    ClaveImpuesto = ClaveImpuesto,
                                    CondicionImpuesto = CondicionImpuesto,
                                    ComplementoRef1 = COMPLEMENTO_REF1,
                                    Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                             prefijoConcepto,
                                                             entradaGanado.FolioEntrada
                                                             , entradaGanado.CabezasRecibidas
                                                             , DESCRIPCION_MOVIMIENTO
                                                             , pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                                    Sociedad = sociedad,
                                    Segmento = "S300",
                                };
                                polizaEntrada = GeneraRegistroPoliza(datos);
                                polizasEntrada.Add(polizaEntrada);
                                renglon++;
                                datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroDocumento,
                                    FechaEntrada = entradaGanado.FechaEntrada,
                                    Folio = entradaGanado.FolioEntrada.ToString(),
                                    CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                                    NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                    Division = division,
                                    ClaseDocumento = postFijoRef3,
                                    ClaveProveedor = entradaCostoEntrada.Proveedor.CodigoSAP,
                                    Importe = string.Format("{0}",
                                                            Cancelacion ? (importeCosto + importeIva).ToString("F2")
                                                                        : ((importeCosto + importeIva) * -1).ToString("F2")),
                                    IndicadorImpuesto = String.Empty,
                                    Renglon = Convert.ToString(renglon),
                                    Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = entradaCostoEntrada.Proveedor.Descripcion,
                                    PesoOrigen = pesoOrigen,
                                    TipoDocumento = textoDocumento,
                                    ComplementoRef1 = COMPLEMENTO_REF1,
                                    Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                             prefijoConcepto,
                                                             entradaGanado.FolioEntrada
                                                             , entradaGanado.CabezasRecibidas
                                                             , DESCRIPCION_MOVIMIENTO
                                                             , pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                                    Sociedad = sociedad,
                                    Segmento = "S300",
                                };
                                polizaEntrada = GeneraRegistroPoliza(datos);
                                polizasEntrada.Add(polizaEntrada);
                            }
                            if (tieneRetencion)
                            {
                                RetencionInfo retencion = null;
                                if (retenciones != null && retenciones.Any())
                                {
                                    retencion =
                                        retenciones.Where(
                                            costo => costo.CostoID.Equals(entradaCostoEntrada.Costo.CostoID)).
                                            Select(ret => ret).FirstOrDefault();
                                }
                                if (retencion != null)
                                {
                                    var parametrosRetencion = new StringBuilder();
                                    parametrosRetencion.Append(String.Format("{0}{1}"
                                                                             , retencion.IndicadorRetencion
                                                                             , retencion.TipoRetencion));
                                    var datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = entradaGanado.FechaEntrada,
                                        Folio = entradaGanado.FolioEntrada.ToString(),
                                        CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                                        NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                        ClaveProveedor = entradaCostoEntrada.Proveedor.CodigoSAP,
                                        Division = division,
                                        ClaseDocumento = postFijoRef3,
                                        IndicadorImpuesto = parametrosRetencion.ToString(),
                                        Importe = string.Format("{0}{1}",
                                                                Cancelacion ? string.Empty : "-", "0"),
                                        Renglon = Convert.ToString(renglon),
                                        Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        CodigoRetencion = retencion.IndicadorImpuesto,
                                        TipoRetencion = retencion.IndicadorRetencion,
                                        ArchivoFolio = archivoFolio.ToString(),
                                        DescripcionCosto = entradaCostoEntrada.Proveedor.Descripcion,
                                        PesoOrigen = pesoOrigen,
                                        TipoDocumento = textoDocumento,
                                        ComplementoRef1 = COMPLEMENTO_REF1,
                                        Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                                 prefijoConcepto,
                                                                 entradaGanado.FolioEntrada
                                                                 , entradaGanado.CabezasRecibidas
                                                                 , DESCRIPCION_MOVIMIENTO
                                                                 , pesoOrigen.ToString("N0"),
                                                                 UNIDAD_MOVIMIENTO),
                                        Sociedad = sociedad,
                                        Segmento = "S300",
                                    };
                                    polizaEntrada = GeneraRegistroPoliza(datos);
                                    polizasEntrada.Add(polizaEntrada);
                                    if (!entradaCostoEntrada.Iva)
                                    {
                                        renglon++;
                                        datos = new DatosPolizaInfo
                                        {
                                            NumeroReferencia = numeroDocumento,
                                            FechaEntrada = entradaGanado.FechaEntrada,
                                            Folio = entradaGanado.FolioEntrada.ToString(),
                                            CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                                            NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                            ClaveProveedor = entradaCostoEntrada.Proveedor.CodigoSAP,
                                            Division = division,
                                            ClaseDocumento = postFijoRef3,
                                            Importe = string.Format("{0}",
                                                                    Cancelacion
                                                                        ? importeCosto.ToString("F2")
                                                                        : (importeCosto * -1).ToString("F2"))
                                            ,
                                            IndicadorImpuesto = String.Empty,
                                            Renglon = Convert.ToString(renglon),
                                            Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                            ImporteIva = "0",
                                            Ref3 = ref3.ToString(),
                                            ArchivoFolio = archivoFolio.ToString(),
                                            DescripcionCosto = entradaCostoEntrada.Proveedor.Descripcion,
                                            PesoOrigen = pesoOrigen,
                                            TipoDocumento = textoDocumento,
                                            ComplementoRef1 = COMPLEMENTO_REF1,
                                            Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                                     prefijoConcepto,
                                                                     entradaGanado.FolioEntrada
                                                                     ,
                                                                     entradaGanado.
                                                                         CabezasRecibidas
                                                                     , DESCRIPCION_MOVIMIENTO
                                                                     , pesoOrigen.ToString("N0"),
                                                                     UNIDAD_MOVIMIENTO),
                                            Sociedad = sociedad,
                                            Segmento = "S300",
                                        };
                                        polizaEntrada = GeneraRegistroPoliza(datos);
                                        polizasEntrada.Add(polizaEntrada);
                                        renglon++;
                                        datos = new DatosPolizaInfo
                                        {
                                            NumeroReferencia = numeroDocumento,
                                            FechaEntrada = entradaGanado.FechaEntrada,
                                            Folio = entradaGanado.FolioEntrada.ToString(),
                                            CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                                            NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                                            ClaveProveedor = String.Empty,
                                            Division = division,
                                            ClaseDocumento = postFijoRef3,
                                            Importe = string.Format("{0}",
                                                                    Cancelacion
                                                                        ? (importeCosto * -1).ToString("F2")
                                                                        : importeCosto.ToString("F2")),
                                            IndicadorImpuesto = String.Empty,
                                            Renglon = Convert.ToString(renglon),
                                            Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                                            ImporteIva = "0",
                                            Ref3 = ref3.ToString(),
                                            Cuenta = claveContableInfo.Valor,
                                            ArchivoFolio = archivoFolio.ToString(),
                                            DescripcionCosto = cuentaSap.Descripcion,
                                            PesoOrigen = pesoOrigen,
                                            TipoDocumento = textoDocumento,
                                            ComplementoRef1 = COMPLEMENTO_REF1,
                                            Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                                     prefijoConcepto,
                                                                     entradaGanado.FolioEntrada
                                                                     ,
                                                                     entradaGanado.
                                                                         CabezasRecibidas
                                                                     , DESCRIPCION_MOVIMIENTO
                                                                     , pesoOrigen.ToString("N0"),
                                                                     UNIDAD_MOVIMIENTO),
                                            Sociedad = sociedad,
                                            Segmento = "S300",
                                        };
                                        polizaEntrada = GeneraRegistroPoliza(datos);
                                        polizasEntrada.Add(polizaEntrada);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        renglon++;
                        var datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = entradaGanado.FechaEntrada,
                            Folio = entradaGanado.FolioEntrada.ToString(),
                            CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                            NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                            ClaveProveedor = String.Empty,
                            Division = division,
                            ClaseDocumento = postFijoRef3,
                            Importe = string.Format("{0}", Cancelacion ? importeCosto.ToString("F2") 
                                                                       : (importeCosto * -1).ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = entradaCostoEntrada.CuentaProvision,
                            ArchivoFolio = archivoFolio.ToString(),
                            DescripcionCosto = cuentaSapProvision.Descripcion,
                            PesoOrigen = pesoOrigen,
                            TipoDocumento = textoDocumento,
                            ComplementoRef1 = COMPLEMENTO_REF1,
                            Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                     prefijoConcepto,
                                                     entradaGanado.FolioEntrada
                                                     , entradaGanado.CabezasRecibidas
                                                     , DESCRIPCION_MOVIMIENTO
                                                     , pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                            Sociedad = sociedad,
                            Segmento = "S300",
                        };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasEntrada.Add(polizaEntrada);
                        renglon++;
                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroDocumento,
                            FechaEntrada = entradaGanado.FechaEntrada,
                            Folio = entradaGanado.FolioEntrada.ToString(),
                            CabezasRecibidas = entradaGanado.CabezasRecibidas.ToString(),
                            NumeroDocumento = entradaCostoEntrada.NumeroDocumento,
                            ClaveProveedor = String.Empty,
                            Division = division,
                            ClaseDocumento = postFijoRef3,
                            Importe = string.Format("{0}", Cancelacion ? (importeCosto * -1).ToString("F2") 
                                                                       : importeCosto.ToString("F2")),
                            IndicadorImpuesto = String.Empty,
                            Renglon = Convert.ToString(renglon),
                            Cabezas = Convert.ToString(entradaDetalle.Cabezas),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = claveContableInfo.Valor,
                            ArchivoFolio = archivoFolio.ToString(),
                            DescripcionCosto = cuentaSap.Descripcion,
                            PesoOrigen = pesoOrigen,
                            TipoDocumento = textoDocumento,
                            ComplementoRef1 = COMPLEMENTO_REF1,
                            Concepto = String.Format("{0}-{1} ,{2} {3}, {4} {5}",
                                                     prefijoConcepto,
                                                     entradaGanado.FolioEntrada
                                                     , entradaGanado.CabezasRecibidas
                                                     , DESCRIPCION_MOVIMIENTO
                                                     , pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                            Sociedad = sociedad,
                            Segmento = "S300",
                        };
                        polizaEntrada = GeneraRegistroPoliza(datos);
                        polizasEntrada.Add(polizaEntrada);
                    }
                    break;
                }
            }
            return polizasEntrada;
        }        

        #endregion XML

        #endregion METODOS PRIVADOS

    }
}
