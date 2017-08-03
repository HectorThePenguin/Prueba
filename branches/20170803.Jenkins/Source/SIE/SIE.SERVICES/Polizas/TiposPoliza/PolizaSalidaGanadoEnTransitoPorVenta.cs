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
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSalidaGanadoEnTransitoPorVenta : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion

        #region VARIABLES PRIVADAS
        
        LoteInfo loteVenta;
        CorralInfo corralVenta;

        const string CONCEPTO = "CABEZA(S)";
        const string MONEDA = "MXN";
        const decimal TIPOCAMBIO = 0;
        const string BUSACT = "RFBU";

        private IList<CostoInfo> costos;
        private DatosPolizaSalidaGanadoTransitoInfo datos;
        private PolizaImpresion<PolizaModel> polizaImpresion;
        private ClienteInfo clienteSap;
        private List<SalidaGanadoEnTransitoDetalleInfo> detalles;

        #endregion

        #region METODOS SOBREESCRITOS


        #endregion

        #region METODOS PRIVADOS

        #region IMPRESION

        /// <summary>
        /// metodo que se usa para imprimir la poliza
        /// </summary>
        /// <param name="contenedorSalidaPorVenta"></param>
        /// <param name="polizas"></param>
        /// <returns></returns>
        public override MemoryStream ImprimePoliza(object contenedorSalidaPorVenta, IList<PolizaInfo> polizas)
        {
            PolizaModel = new PolizaModel();
            polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaVentaEnTransito);
            var info = contenedorSalidaPorVenta as SalidaGanadoEnTransitoInfo;
            var firstOrDefault = polizas.FirstOrDefault();
            if (firstOrDefault != null) firstOrDefault.Cuenta = firstOrDefault.Cliente;

            if (info == null) return new MemoryStream();

            #region cabecera
            var organizacionInfo = ObtenerOrganizacionSociedadDivision(info.OrganizacionID, SociedadEnum.SuKarne);
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
            {
                new PolizaEncabezadoModel
                {
                    Descripcion = organizacionInfo.TituloPoliza,
                    Desplazamiento = 0
                },
            };
            polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
            {
                new PolizaEncabezadoModel
                {
                    Descripcion = "Salida x Venta  En tránsito",
                    Desplazamiento = 0
                },
            };
            polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
            {
                new PolizaEncabezadoModel
                {
                    Descripcion = string.Format("{0} {1}", "FECHA:", info.Fecha.ToShortDateString()),
                    Desplazamiento = 0
                },
                new PolizaEncabezadoModel
                {
                    Descripcion =
                        string.Format("{0} {1}", "FOLIO No.",
                            info.Folio),
                    Desplazamiento = 0
                },
            };

            polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");
            polizaImpresion.GenerarLineaEnBlanco("Folio", 2);

            #endregion

            var codigoSap = info.Cliente.CodigoSAP;
            clienteSap = new ClienteInfo { CodigoSAP = codigoSap };
            var clienteBl = new ClienteBL();
            clienteSap = clienteBl.ObtenerClientePorCliente(clienteSap);

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
            {
                new PolizaEncabezadoModel
                {
                    Descripcion =
                        string.Format("{0}:{1}", "CLIENTE",
                            clienteSap.Descripcion),
                    Desplazamiento = 0
                },
                new PolizaEncabezadoModel
                {
                    Descripcion =
                        string.Format("{0}:{1}", "REFERENCIA",
                            organizacionInfo.Descripcion),
                    Desplazamiento = 0
                },
            };

            polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Proveedor");

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
            {
                new PolizaEncabezadoModel
                {
                    Descripcion =
                        string.Format("{0}:{1}", "VENDEDOR",
                            string.Empty),
                    Desplazamiento = 0
                },
                new PolizaEncabezadoModel
                {
                    Descripcion =
                        string.Format("{0}:{1}", "FECHA",
                            info.Fecha.ToShortDateString()),
                    Desplazamiento = 0
                },
            };
            polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Comprador");
            GeneraLinea(2);
            polizaImpresion.GeneraCabecero(new[] { "100" }, "Comprador");

            GeneraLineaEncabezadoDetalle();
            GeneraLineasDetalle(info);
            GeneraLinea(12);
            polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

            GeneraLineaEncabezadoCostos();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            GeneraLineaCostos(info.DetallesSalida, string.Empty);
            polizaImpresion.GeneraCostos("Costos");

            GeneraLineaCostosTotales();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            GeneraLineaTotalCostos(info.DetallesSalida);

            GeneraLinea(5);
            polizaImpresion.GeneraCabecero(new[] { "100" }, "Costos");
            polizaImpresion.GenerarLineaEnBlanco();
            GeneraLineaEncabezadoRegistroContable(info.Folio);
            polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");
            GeneraLineaSubEncabezadoRegistroContable(false, "No DE CUENTA", "CARGOS", "ABONOS");
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

        /// <summary>
        /// metodo que genera el encabezado
        /// </summary>
        /// <param name="salidaVenta">objeto de la venta</param>
        private void GeneraLineasDetalle(SalidaGanadoEnTransitoInfo salidaVenta)
        {
            //var corralOrganizacion = CorralBL.ObtenerCorralesPorOrganizacionID(salidaVenta.OrganizacionID).FirstOrDefault(corral => corral.LoteID == salidaVenta.LoteID);

            var corralDal = new CorralDAL();
            var loteDal = new LoteDAL();

            
                corralVenta = corralDal.ObtenerPorId(salidaVenta.CorralID);
                loteVenta = loteDal.ObtenerPorID(salidaVenta.LoteID);
            

            var entradaGanado = new EntradaGanadoTransitoInfo();
            if (loteVenta != null)
            {
                entradaGanado.Lote = loteVenta;
            }

            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            {
                    var detalleModel = new PolizaDetalleModel
                    {
                        CantidadCabezas = salidaVenta.NumCabezas.ToString("F0"),
                        TipoGanado = CONCEPTO,
                        PesoTotal = (salidaVenta.Kilos).ToString("F0"),
                        PesoPromedio = (salidaVenta.Kilos/salidaVenta.NumCabezas).ToString("F0"),
                        PrecioPromedio = (salidaVenta.DetallesSalida.Sum(costo => costo.ImporteCosto) / (salidaVenta.Kilos / salidaVenta.NumCabezas) / salidaVenta.NumCabezas).ToString("N", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                        ImportePromedio = Math.Abs(salidaVenta.DetallesSalida.Sum(costo => costo.ImporteCosto)).ToString("N", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                        
                        Corral = corralVenta.Codigo,
                        PrecioVenta = Math.Abs(salidaVenta.Importe / salidaVenta.NumCabezas).ToString("N", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                        ImporteVenta = Math.Abs(salidaVenta.Importe).ToString("N", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                        Lote = entradaGanado.Lote.Lote
                    };
                    PolizaModel.Detalle.Add(detalleModel);
                
            }
            polizaImpresion.GenerarDetalles("Detalle");
        }

        /// <summary>
        /// Metodo que genera las lineas de encabezado (detalle)
        /// </summary>
        private void GeneraLineaEncabezadoDetalle()
        {
            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "CANT.",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PESO",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "A COSTO PROMEDIO",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "A PRECIO DE VENTA",
                                                      Alineacion = "center",
                                                      Desplazamiento = 2
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty,
                                                      Desplazamiento = 2
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "17", "10", "10", "10", "10", "10", "10", "3", "10", "10" }, "Detalle");

            PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                          {
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "CABEZAS",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                    Descripcion  = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "CONCEPTO",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "TOTAL",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PROMEDIO",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PRECIO",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "IMPORTE",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "PRECIO",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "IMPORTE",
                                                      Alineacion = "right"
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = string.Empty
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "CORRAL",
                                                  },
                                              new PolizaEncabezadoModel
                                                  {
                                                      Descripcion = "LOTE",
                                                  },
                                          };
            polizaImpresion.GeneraCabecero(new[] { "10", "3", "17", "10", "10", "10", "10", "10", "10", "3", "10", "10" }, "Detalle");
        }

        /// <summary>
        /// metodo que genera las lineas de los registros contables
        /// </summary>
        /// <param name="polizas">lista de polizas</param>
        /// <param name="cargos">lista de cargos</param>
        /// <param name="abonos">lista de abonos</param>
        protected override void GeneraLineaRegistroContable(IList<PolizaInfo> polizas, out IList<PolizaInfo> cargos, out IList<PolizaInfo> abonos)
        {
            base.GeneraLineaRegistroContable(polizas, out cargos, out abonos);

            var obtenerCostos = ObtenerCostos();
            var costoLista = (from costo in obtenerCostos let tempCosto = detalles.FirstOrDefault(detalle => detalle.CostoId == costo.CostoID) where tempCosto != null select new CostoInfo {CostoID = tempCosto.CostoId, ClaveContable = costo.ClaveContable, Descripcion = costo.Descripcion}).ToList();

            obtenerCostos.Clear();
            obtenerCostos = costoLista;

            cargos.ToList().ForEach(cliente =>
            {
                if (string.IsNullOrWhiteSpace(cliente.Cuenta))
                {
                    cliente.Cuenta = string.Empty;
                }
            });

            const int cadenaLarga = 50;

            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();

            PolizaRegistroContableModel registroContable;

            foreach (var cargo in cargos)
            {
                var descripcionCosto =
                    obtenerCostos.FirstOrDefault(
                        costo => cargo.Cuenta.EndsWith(costo.ClaveContable) && string.IsNullOrWhiteSpace(cargo.Cliente)) ??
                    new CostoInfo
                        {
                            Descripcion = clienteSap.Descripcion
                        };

                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(descripcionCosto.Descripcion.Length > cadenaLarga
                                         ? descripcionCosto.Descripcion.Substring(0, cadenaLarga - 1).Trim()
                                         : descripcionCosto.Descripcion.Trim());

                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta =
                        string.IsNullOrWhiteSpace(cargo.Cuenta) ? cargo.Cliente : cargo.Cuenta,
                    Descripcion = sbDescripcion.ToString(),
                    Cargo =
                        Convert.ToDecimal(cargo.Importe.Replace("-", string.Empty)).ToString(
                            "N", CultureInfo.CurrentCulture)
                };
                PolizaModel.RegistroContable.Add(registroContable);
            }
            var cuentaSapBl = new CuentaSAPBL();
            var cuentaSap = cuentaSapBl.ObtenerTodos();
            foreach (var abono in abonos)
            {
                var cuenta = cuentaSap.FirstOrDefault(x => x.CuentaSAP == abono.Cuenta) ?? new CuentaSAPInfo();

                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(cuenta.Descripcion.Length > cadenaLarga
                                         ? cuenta.Descripcion.Substring(0, cadenaLarga - 1).Trim()
                                         : cuenta.Descripcion.Trim());
                registroContable = new PolizaRegistroContableModel
                {
                    Cuenta = abono.Cuenta,
                    Descripcion = sbDescripcion.ToString(),
                    Abono =
                        Convert.ToDecimal(abono.Importe.Replace("-", string.Empty)).ToString(
                            "N", CultureInfo.CurrentCulture)
                };
                PolizaModel.RegistroContable.Add(registroContable);
            }
        }

        /// <summary>
        /// Metodo que genera las lineas de costos
        /// </summary>
        /// <param name="listaCostos">lista de costos</param>
        /// <param name="observacion">Observaciones</param>
        private void GeneraLineaCostos(List<SalidaGanadoEnTransitoDetalleInfo> listaCostos, string observacion)
        {
            var observaciones = new HashSet<string>();
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(observacion))
            {
                var index = 0;
                foreach (var obs in observacion)
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
            const int numeroLinea = 0;
            var observacionesImpresas = new List<string>(observaciones);

            var costosAgrupados = listaCostos;

            if (costos == null)
            {
                costos = ObtenerCostos();
            }

            PolizaModel.Costos = new List<PolizaCostoModel>();
            PolizaCostoModel costoModel;
            foreach (var costo in costosAgrupados)
            {
                if (!observacionesImpresas.Any())
                {
                    observacionesImpresas.Add(string.Empty);
                }
                var costoDescripcion =
                    costos.Where(clave => clave.CostoID == costo.CostoId).Select(desc => desc.Descripcion).
                        FirstOrDefault();
                costoModel = new PolizaCostoModel
                {
                    Descripcion = costoDescripcion,
                    Parcial = costo.ImporteCosto.ToString("N", CultureInfo.CurrentCulture),
                    Total = costo.ImporteCosto.ToString("N", CultureInfo.CurrentCulture),
                    Observaciones = observacionesImpresas[numeroLinea].Trim()
                };
                PolizaModel.Costos.Add(costoModel);
                if (!string.IsNullOrWhiteSpace(observacionesImpresas[numeroLinea]))
                {
                    observacionesImpresas.RemoveAt(numeroLinea);
                }
            }
            if (observacionesImpresas.Any()
                && !string.IsNullOrWhiteSpace(observacionesImpresas[numeroLinea]))
            {
                foreach (var obs in observacionesImpresas)
                {
                    costoModel = new PolizaCostoModel
                    {
                        Observaciones = obs
                    };
                    PolizaModel.Costos.Add(costoModel);
                }
            }
        }

        /// <summary>
        /// metodo que genera la linea total de costos
        /// </summary>
        /// <param name="costosList">lista de costos</param>
        private void GeneraLineaTotalCostos(IEnumerable<SalidaGanadoEnTransitoDetalleInfo> costosList)
        {
            var totalCosto = costosList.Sum(imp => imp.ImporteCosto);
            GenerarLineaTotalCosto(totalCosto, true);

            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);

            GenerarLineaElaboro();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
        }
        #endregion

        #region XML

        /// <summary>
        /// Genera los parametros para la consulta que trae los datos faltantes para la generacion de la poliza
        /// </summary>
        /// <param name="input">Objeto que lleva el folio y muerte(activo)</param>
        /// <returns></returns>
        DatosPolizaSalidaGanadoTransitoInfo obtenerDatosPolizaSalidaPorVenta(SalidaGanadoEnTransitoInfo input)
        {
            try
            {
                var salida = new SalidaGanadoEnTransitoDAL();
                var result = salida.ObtenerDatosPolizaSalidaPorMuerte(input);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new DatosPolizaSalidaGanadoTransitoInfo();
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Metodo para armar los objetos para generar poliza
        /// </summary>
        /// <param name="datosPoliza">Objeto salidaGanadotransitoInfo</param>
        /// <returns>poliza</returns>
        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            IList<PolizaInfo> polizaSalida = new List<PolizaInfo>();

            var noLinea = 1;
            var costosPl = new CostoPL();
            var cliente = new ClientePL();
            var cuentasSap = ObtenerCuentasSAP();
            var interfaceBl = new InterfaceSalidaBL();
            var parOrganizacion = new ParametroOrganizacionBL();
            var parametroCentro = new ParametroOrganizacionInfo();
            var input = (SalidaGanadoEnTransitoInfo)datosPoliza;
            var costosClaves = costosPl.ObtenerTodos(EstatusEnum.Activo);
            var costoAbono = new CostoInfo { ClaveContable = string.Empty };
            var costoGanado = input.Costos.FirstOrDefault(x => x.CostoID == Costo.CostoGanado.GetHashCode());
            var claveCuenta = parOrganizacion.ObtenerPorOrganizacionIDClaveParametro(input.OrganizacionID, ParametrosEnum.CTAFALTANTESOBRANTE.ToString());

            ClaveContableInfo clavecontablecargo;

            detalles = input.DetallesSalida;
            datos = obtenerDatosPolizaSalidaPorVenta(input);
            input.Cliente = cliente.ObtenerPorID(input.Cliente.ClienteID);

            //la var datos debe contener datos para poder acceder a su valor en la propiedad PostFijoRef3
            var ref3 = Ref3(input);
            var archivoFolio = ArchivoFolio(input);
 
            #region linea1(+) Proveedor
            var linea1 = InicializarPolizaInfo(input, ref3, archivoFolio, noLinea);
            linea1.Importe = input.Importe.ToString("N", CultureInfo.InvariantCulture);
            linea1.Cliente = input.Cliente.CodigoSAP;
            //linea1.Cuenta = input.Cliente.CodigoSAP;
            polizaSalida.Add(linea1);
            noLinea++;
            #endregion

            #region linea2(-) Beneficiario
            var linea2 = InicializarPolizaInfo(input, ref3, archivoFolio, noLinea);
            //linea2.Cliente = input.Cliente.CodigoSAP;
            linea2.Importe = "-" + input.Importe.ToString("N", CultureInfo.InvariantCulture);
            var claveContableAbono = interfaceBl.ObtenerCuentaInventario(costoAbono, input.OrganizacionID, ClaveCuenta.CuentaBeneficioInventario);

            if (claveContableAbono != null)
            {
                var cuentaSap = cuentasSap.FirstOrDefault(clave => clave.CuentaSAP == claveContableAbono.Valor);
                claveContableAbono.Descripcion = cuentaSap == null ? string.Empty : cuentaSap.Descripcion;
            }
            else
            {
                claveContableAbono = new ClaveContableInfo
                {
                    Valor = string.Empty
                };
            }

            var beneficio = false;
            if (claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroBeneficio))
            {
                parametroCentro = ObtenerParametroOrganizacionPorClave(input.OrganizacionID, ParametrosEnum.CTACENTROBENEFICIOENG.ToString());

                if (parametroCentro == null)
                {
                    throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE BENEFICIO NO CONFIGURADO"));
                }
                beneficio = true;
            }
            else
            {
                if (claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroCosto) || claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroGasto))
                {
                    parametroCentro = ObtenerParametroOrganizacionPorClave(input.OrganizacionID, ParametrosEnum.CTACENTROCOSTOENG.ToString());

                    if (parametroCentro == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE BENEFICIO NO CONFIGURADO"));
                    }
                }
            }
            if (beneficio)
            {
                linea2.CentroBeneficio = parametroCentro.Valor;
            }
            else
            {
                linea2.CentroCosto = parametroCentro.Valor;
            }

            linea2.Cuenta = claveContableAbono.Valor;
            polizaSalida.Add(linea2);
            noLinea++;
            #endregion

            #region linea3 cargo a cuenta costo ganado(-)
            //se elimino esta linea
            #endregion
             
            #region linea4 cargo a costo corral(-)
            if (costoGanado == null) return polizaSalida;

            var linea4 = InicializarPolizaInfo(input, ref3, archivoFolio, noLinea);
            linea4.Cuenta = claveCuenta.Valor;

            if (claveCuenta.Valor.Substring(0, 4) == "5001")
            {
                linea4.CentroCosto = datos.ParametroOrganizacionValor;
            }

            var importeCostos = input.DetallesSalida.Sum(costo => costo.ImporteCosto);
                
            linea4.Importe = "-" + importeCostos.ToString(CultureInfo.InvariantCulture);
            polizaSalida.Add(linea4);
            noLinea++;
            #endregion

            #region linea5 cargoACuentaResultados (-)(importe total)
            var linea5 = InicializarPolizaInfo(input, ref3, archivoFolio, noLinea);

            if (claveCuenta.Valor.Substring(0, 4) == "5001")
            {
                linea5.CentroCosto = datos.ParametroOrganizacionValor;
            }

            var salidaVentaTransito = input.DetallesSalida.FirstOrDefault(detalle => detalle.CostoId == Costo.CostoGanado.GetHashCode());
            if (salidaVentaTransito != null)
            {
                linea5.Importe = salidaVentaTransito.ImporteCosto.ToString(CultureInfo.InvariantCulture);
            }
            
            costos = ObtenerCostos();
            var firstOrDefault = costos.FirstOrDefault(x => x.CostoID == costoGanado.CostoID);

            if (firstOrDefault != null)
            {
                costoAbono.ClaveContable = firstOrDefault.ClaveContable;
                clavecontablecargo = ObtenerCuentaInventario(costoAbono, input.OrganizacionID, TipoPoliza.PolizaMuerteTransito);
                if (clavecontablecargo != null) linea5.Cuenta = clavecontablecargo.Valor;
            }

            polizaSalida.Add(linea5);
            noLinea++;
            #endregion

            #region linea6 abono a cuenta Resultados a la misma cuenta que el punto anterior en foreach
            var linea6 = new List<PolizaInfo>();

            foreach (var costoCargo in input.DetallesSalida.SkipWhile(costo => costo.CostoId == Costo.CostoGanado.GetHashCode()))
            {
                var temp = InicializarPolizaInfo(input, ref3, archivoFolio, noLinea);
                temp.Importe = costoCargo.ImporteCosto.ToString(CultureInfo.InvariantCulture);

                var claveCosto = costosClaves.FirstOrDefault(x => x.CostoID == costoCargo.CostoId);
                if (claveCosto != null)
                {
                    costoAbono.ClaveContable = claveCosto.ClaveContable;
                    clavecontablecargo = ObtenerCuentaInventario(costoAbono, input.OrganizacionID, TipoPoliza.PolizaMuerteTransito);
                    if (clavecontablecargo != null) temp.Cuenta = clavecontablecargo.Valor;
                }
                
                if (claveCuenta.Valor.Substring(0, 4) == "5001")
                {
                    temp.CentroCosto = datos.ParametroOrganizacionValor;
                }
                
                linea6.Add(temp);
                noLinea++;
            }

            foreach (var polizaItem in linea6)
            {
                polizaSalida.Add(polizaItem);
            }
            #endregion
           
            return polizaSalida;
        }

        /// <summary>
        /// Metodo que inicializa las polizas con datos generales apra cada linea
        /// </summary>
        /// <param name="input">SalidaGanadoEnTransitoInfo</param>
        /// <param name="ref3">StringBuilder</param>
        /// <param name="archivoFolio">StringBuilder</param>
        /// <param name="noLinea">Numero de linea</param>
        /// <returns>linea de la poliza inicializada</returns>
        private PolizaInfo InicializarPolizaInfo(SalidaGanadoEnTransitoInfo input, StringBuilder ref3, StringBuilder archivoFolio, int noLinea)
        {
            var lista = new PolizaInfo
            {
                FechaCreacion = DateTime.Now,
                OrganizacionID = input.OrganizacionID,
                UsuarioCreacionID = input.UsuarioCreacionID,
                NumeroReferencia = input.Folio.ToString(CultureInfo.InvariantCulture),
                FechaDocumento = input.Fecha.ToString("yyyyMMdd"),
                ClaseDocumento = datos.PostFijoRef3,
                Sociedad = datos.Sociedad,
                Moneda = MONEDA,
                TipoCambio = TIPOCAMBIO,
                TextoDocumento = datos.TipoPolizaDescripcion,
                Mes = input.FechaCreacion.Month.ToString(CultureInfo.InvariantCulture),
                TextoAsignado = input.Folio.ToString(CultureInfo.InvariantCulture),
                Concepto = string.Format("VT-{0} {1} CABEZAS {2} kgs", input.Folio, input.NumCabezas, input.Kilos),
                Division = datos.Division,
                BusAct = BUSACT,
                Periodo = input.FechaCreacion.Year.ToString(CultureInfo.InvariantCulture),
                NumeroLinea = noLinea.ToString(CultureInfo.InvariantCulture),
                Referencia1 = input.NumCabezas.ToString(CultureInfo.InvariantCulture),
                Referencia3 = ref3.ToString(),
                FechaImpuesto = input.Fecha.ToString("yyyyMMdd"),
                ImpuestoRetencion = 0,
                ImpuestoIva = 0.ToString(CultureInfo.InvariantCulture),
                ArchivoFolio = archivoFolio.ToString(),
                Segmento = "S" + datos.Sociedad,
                FechaContabilidad = input.Fecha.ToString("yyyyMMdd"),
            };
            return lista;
        }

        /// <summary>
        /// Metodo que arma el campo ref3 de la poliza
        /// </summary>
        /// <param name="input">SalidaGanadoEnTransitoInfo</param>
        /// <returns>ref3 (StringBuilder)</returns>
        private StringBuilder Ref3(SalidaGanadoEnTransitoInfo input)
        {
            /*
             * esto sirve para el armado del segmento <ref3> que se usa en cada linea y debe ser el mismo para todas
             * se arma poniento inicialmente 03 + 10 espacios en blanco + 1 num aleatorio del 10 al 20 + 1 numero aleatorio del 30 al 40 + 
             * el milisegundo en que se está generando el documento y el posfijo configurado en el campo “PostFijoRef3” TB TipoPoliza.
             * Ejem: "03       1211933561GF"
             */

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(input.Folio).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture));
            ref3.Append(datos.PostFijoRef3);
            return ref3;
        }

        /// <summary>
        /// Metodo que arma el campo archivofolio en la poliza
        /// </summary>
        /// <param name="input">SalidaGanadoEnTransitoInfo</param>
        /// <returns>archivoFolio (StringBuilder)</returns>
        private StringBuilder ArchivoFolio(SalidaGanadoEnTransitoInfo input)
        {
            /*
             * esto sirve para el armado del segmento <archifolio> que se usa en cada linea y debe ser el mismo para todas las lineas,
             * se arma poniento el valor dijo "P01" + la decha del documento en formato año/mes/dia + el minuto + el segundo y un numero aleatorio del 1 al 9
             * Ejem: "P01-20160303-54-11-2.xml"
             */

            var archivoFolio = new StringBuilder();
            archivoFolio.Append("P01");
            archivoFolio.Append(input.Fecha.ToString("yyyyMMdd"));
            archivoFolio.Append(DateTime.Now.Minute);
            archivoFolio.Append(DateTime.Now.Second);
            archivoFolio.Append(new Random(1).Next(1, 9));
            return archivoFolio;
        }
    }
}
