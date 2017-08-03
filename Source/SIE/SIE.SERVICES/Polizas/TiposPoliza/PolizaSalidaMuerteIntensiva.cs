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

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSalidaMuerteIntensiva : PolizaAbstract
    {

        private IList<CostoInfo> costos;
        private PolizaImpresion<PolizaModel> polizaImpresion;
        private OrganizacionInfo organizacionInfo;

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaMuerteIntensiva);

                var muerteIntesiva = datosPoliza as GanadoIntensivoInfo;

                if (muerteIntesiva != null)
                {
                    int organizacionID = muerteIntesiva.Organizacion.OrganizacionID;
                    DateTime fechaMuerte = DateTime.Today;

                    organizacionInfo = ObtenerOrganizacionSociedadDivision(muerteIntesiva.Organizacion.OrganizacionID, SociedadEnum.SuKarne);

                    //PENDIENTE PARA OBTENER LA SOCIEDAD
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
                                              Descripcion = "Salida x Muerte Ganado Intensivo",
                                              Desplazamiento = 0
                                          },
                                  };
                    polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                    PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                  {
                                      new PolizaEncabezadoModel
                                          {
                                              Descripcion = string.Format("{0} {1}", "FECHA:", fechaMuerte.ToShortDateString()),
                                              Desplazamiento = 0
                                          },
                                      new PolizaEncabezadoModel
                                          {
                                              Descripcion =
                                                  string.Format("{0} {1}", "FOLIO No.",
                                                                muerteIntesiva.FolioTicket),
                                              Desplazamiento = 0
                                          },
                                  };

                    polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");
                    polizaImpresion.GenerarLineaEnBlanco("Folio", 2);


                    PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                  {
                                      new PolizaEncabezadoModel
                                          {
                                              Descripcion =
                                                  string.Format("{0}:{1}", "REFERENCIA",
                                                                organizacionInfo.Descripcion),
                                          },
                                      new PolizaEncabezadoModel
                                          {
                                              Descripcion =
                                                  string.Format("{0}:{1}", "FECHA",
                                                                fechaMuerte.ToShortDateString()),
                                          },
                                  };

                    polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");
                    GeneraLinea(2);
                    polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");

                    GeneraLineaEncabezadoDetalle();

                    
                    GeneraLineasDetalle(muerteIntesiva);

                    GeneraLinea(12);
                    polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                    GeneraLineaEncabezadoCostos();

                    polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                    GeneraLineaCostos(muerteIntesiva.ListaGanadoIntensivoCosto, string.Empty);
                    polizaImpresion.GeneraCostos("Costos");

                    GeneraLineaCostosTotales();
                    polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                    GeneraLineaTotalCostos(muerteIntesiva.ListaGanadoIntensivoCosto);

                    GeneraLinea(5);
                    polizaImpresion.GeneraCabecero(new[] { "100" }, "Costos");
                    polizaImpresion.GenerarLineaEnBlanco();

                    GeneraLineaEncabezadoRegistroContable(muerteIntesiva.FolioTicket);
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


                }
                
                return polizaImpresion.GenerarArchivo();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

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

            cargos = cargos.Where(cuenta => !string.IsNullOrWhiteSpace(cuenta.Cuenta)
                                    || !string.IsNullOrWhiteSpace(cuenta.Cliente))
                           .GroupBy(grupo => grupo.Cuenta)
                           .Select(datos => new PolizaInfo
                           {
                               Cuenta = datos.Key,
                               Cliente = datos.Select(cli => cli.Cliente).FirstOrDefault(),
                               Importe = Convert.ToString(datos.Sum(imp => Convert.ToDecimal(imp.Importe)))
                           }).ToList();
            abonos = abonos.Where(cuenta => !string.IsNullOrWhiteSpace(cuenta.Cuenta)).GroupBy(grupo => grupo.Cuenta)
               .Select(datos => new PolizaInfo
               {
                   Cuenta = datos.Key,
                   Cliente = datos.Select(cli => cli.Cliente).FirstOrDefault(),
                   Importe = Convert.ToString(datos.Sum(imp => Convert.ToDecimal(imp.Importe)))
               }).ToList();
            const int CADENA_LARGA = 50;

            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                CostoInfo descripcionCosto =
                    costos.FirstOrDefault(
                        costo => cargo.Cuenta.EndsWith(costo.ClaveContable) && string.IsNullOrWhiteSpace(cargo.Cliente));
                if (descripcionCosto == null)
                {
                    descripcionCosto = new CostoInfo
                    {
                        Descripcion = organizacionInfo.Descripcion
                    };
                }

                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(descripcionCosto.Descripcion.Length > CADENA_LARGA
                                         ? descripcionCosto.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
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

            foreach (var abono in abonos)
            {
                CostoInfo descripcionCosto =
                    costos.FirstOrDefault(
                        costo => abono.Cuenta.EndsWith(costo.ClaveContable));
                if (descripcionCosto == null)
                {
                    descripcionCosto = new CostoInfo();
                }
                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(descripcionCosto.Descripcion.Length > CADENA_LARGA
                                         ? descripcionCosto.Descripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : descripcionCosto.Descripcion.Trim());
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



        private void GeneraLineaTotalCostos(List<GanadoIntensivoCostoInfo> listaGanadoIntensivoCosto)
        {
            decimal totalCosto = listaGanadoIntensivoCosto.Sum(imp => imp.Importe);
            GenerarLineaTotalCosto(totalCosto, true);

            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);

            GenerarLineaElaboro();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
        }

        private void GeneraLineaCostos(List<GanadoIntensivoCostoInfo> listaGanadoIntensivoCosto, string observacion)
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
            
            var costosAgrupados = listaGanadoIntensivoCosto;

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
                string costoDescripcion =
                    costos.Where(clave => clave.CostoID == costo.Costos.CostoID).Select(desc => desc.Descripcion).
                        FirstOrDefault();
                costoModel = new PolizaCostoModel
                {
                    Descripcion = costoDescripcion,
                    Parcial = costo.Importe.ToString("N", CultureInfo.CurrentCulture),
                    Total = costo.Importe.ToString("N", CultureInfo.CurrentCulture),
                    Observaciones = observacionesImpresas[NUMERO_LINEA].Trim()
                };
                PolizaModel.Costos.Add(costoModel);
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
                    costoModel = new PolizaCostoModel
                    {
                        Observaciones = obs
                    };
                    PolizaModel.Costos.Add(costoModel);
                }
            }
        }

        private void GeneraLineasDetalle(GanadoIntensivoInfo muerteIntesiva)
        {

            EntradaGanadoInfo entradaGanado = muerteIntesiva.EntradaGanado;

            var pesoMuertePromedio = Math.Round((entradaGanado.PesoBruto - entradaGanado.PesoTara)/entradaGanado.CabezasRecibidas,0);
            int cantidadCabezas = muerteIntesiva.Cabezas;
            decimal importe = muerteIntesiva.Importe;

            var concepto = muerteIntesiva.EntradaGanadoCosteo.ListaEntradaDetalle.Select(des => des.TipoGanado.Descripcion).FirstOrDefault();


            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;
    
            detalleModel = new PolizaDetalleModel
            {
                CantidadCabezas = cantidadCabezas.ToString("F0"),
                TipoGanado = concepto,
                PesoTotal = (pesoMuertePromedio * cantidadCabezas).ToString("F0"),
                PesoPromedio = pesoMuertePromedio.ToString("F0"),
                PrecioPromedio = Math.Abs(importe / cantidadCabezas).ToString("N2"),
                ImportePromedio = Math.Abs(importe).ToString("N2"),
                //PrecioVenta = (detalle.Precio).ToString("N2"),
                //ImporteVenta =
                //    ((detalle.Precio * (Math.Round(detalle.Peso * detalle.Cabezas, 0)))).ToString
                //    ("N2"),
                Corral = muerteIntesiva.Corral.Codigo,
                Lote = muerteIntesiva.Lote.Lote
            };
            PolizaModel.Detalle.Add(detalleModel);
            
            polizaImpresion.GenerarDetalles("Detalle");
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var ganadoIntensivo = datosPoliza as GanadoIntensivoInfo;
            IList<PolizaInfo> poliza = ObtenerPoliza(ganadoIntensivo);
            return poliza;
        }

        private IList<PolizaInfo> ObtenerPoliza(GanadoIntensivoInfo ganadoIntensivo)
        {
            var polizasSalidaMuerteIntensiva = new List<PolizaInfo>();

            if(ganadoIntensivo.ListaGanadoIntensivoCosto != null && ganadoIntensivo.ListaGanadoIntensivoCosto.Any())
            {
                TipoPolizaInfo tipoPoliza =
                        TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaMuerteIntensiva.GetHashCode());
                if (tipoPoliza == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                              TipoPoliza.SalidaMuerteIntensiva));
                }
                string textoDocumento = tipoPoliza.TextoDocumento;
                string tipoMovimiento = tipoPoliza.ClavePoliza;
                string postFijoRef3 = tipoPoliza.PostFijoRef3;

                int linea = 1;

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

                if (costos == null)
                {
                    costos = ObtenerCostos();
                }

                foreach (var ganadoIntensivoCostoInfo in ganadoIntensivo.ListaGanadoIntensivoCosto)
                {
                    CostoInfo costo =
                        costos.FirstOrDefault(tipo => tipo.CostoID == ganadoIntensivoCostoInfo.Costos.CostoID);
                    if (costo == null)
                    {
                        costo = new CostoInfo();
                    }
                     //= ObtenerOrganizacionIVA(ganadoIntensivo.Organizacion.OrganizacionID);


                    OrganizacionInfo organizacion = ObtenerOrganizacionSociedadDivision(ganadoIntensivo.Organizacion.OrganizacionID,SociedadEnum.SuKarne);
                    if (organizacion == null)
                    {
                        organizacion = new OrganizacionInfo
                        {
                            TipoOrganizacion = new TipoOrganizacionInfo()
                        };
                    }

                    const int Corral_Intensivo = 4;
                    const int Corral_Maquila = 6;

                    int tipoOrganizacioID = 0;

                    switch (ganadoIntensivo.Lote.TipoCorralID)
                    {
                        case Corral_Intensivo:
                            tipoOrganizacioID = TipoOrganizacion.Intensivo.GetHashCode();
                            break;
                        case Corral_Maquila:
                            tipoOrganizacioID = TipoOrganizacion.Maquila.GetHashCode();
                            break;
                    }
                    if (tipoOrganizacioID > 0)
                    {
                        organizacion.TipoOrganizacion.TipoOrganizacionID = tipoOrganizacioID;    
                    }
                    
                    
                    ClaveContableInfo claveContableAbono = ObtenerCuentaInventario(costo, organizacion.OrganizacionID,
                                                                organizacion.TipoOrganizacion.TipoOrganizacionID);
                     if (claveContableAbono == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA EL COSTO",
                                                                  costo.Descripcion));
                    }

                     ClaveContableInfo claveContableCargo = ObtenerCuentaInventario(costo, organizacion.OrganizacionID,
                                                                                    TipoPoliza.SalidaMuerteIntensiva);
                     if (claveContableCargo == null)
                     {
                         throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA EL COSTO",
                                                                   costo.Descripcion));
                     }
                     ParametroOrganizacionInfo parametroCentroCosto =
                         ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                              ParametrosEnum.CTACENTROCTOINT.ToString());
                     if (parametroCentroCosto == null)
                     {
                         throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
                     }
                    EntradaGanadoInfo entradaGanado = ganadoIntensivo.EntradaGanado;
                    var pesoMuerteTotal = Math.Round(((entradaGanado.PesoBruto - entradaGanado.PesoTara)/entradaGanado.CabezasRecibidas)*ganadoIntensivo.Cabezas,0);
                    //VALIDAR FECHA
                    DateTime fecha = DateTime.Today;
                    if (fecha != null)
                    {
                        string archivoFolio = ObtenerArchivoFolio(fecha);
                        var datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = fecha,
                            Folio = ganadoIntensivo.FolioTicket.ToString(),
                            Importe = string.Format("{0}", ganadoIntensivoCostoInfo.Importe.ToString("F2")),
                            Renglon = Convert.ToString(linea++),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = claveContableCargo.Valor,
                            Division = organizacion.Division,
                            ArchivoFolio = archivoFolio,
                            CentroCosto =
                                claveContableCargo.Valor.StartsWith(PrefijoCuentaCentroCosto) ||
                                claveContableCargo.Valor.StartsWith(PrefijoCuentaCentroGasto)
                                    ? parametroCentroCosto.Valor
                                    : string.Empty,
                            //PesoOrigen = peso,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            ComplementoRef1 = string.Empty,
                            Concepto = String.Format("{0}-{1} CABEZAS {2} kgs",
                                                     tipoMovimiento,
                                                     ganadoIntensivo.Cabezas, pesoMuerteTotal),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                            
                        };
                        PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
                        polizasSalidaMuerteIntensiva.Add(polizaSalida);

                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = fecha,
                            Folio = ganadoIntensivo.FolioTicket.ToString(),
                            Importe = string.Format("{0}", (ganadoIntensivoCostoInfo.Importe * -1).ToString("F2")),
                            Renglon = Convert.ToString(linea++),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = claveContableAbono.Valor,
                            ArchivoFolio = archivoFolio,
                            Division = organizacion.Division,
                           // PesoOrigen = peso,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            Concepto = String.Format("{0}-{1}, CABEZAS, {2} kgs",
                                                     tipoMovimiento,
                                                     ganadoIntensivo.Cabezas, pesoMuerteTotal),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        polizaSalida = GeneraRegistroPoliza(datos);
                        polizasSalidaMuerteIntensiva.Add(polizaSalida);
                    }
                }

            }

            return polizasSalidaMuerteIntensiva;
        }

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

    }
}
