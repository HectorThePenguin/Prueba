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
    public class PolizaSalidaVentaIntensiva : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private IList<CostoInfo> costos;
        private ClienteInfo clienteSAP;
        private PolizaImpresion<PolizaModel> polizaImpresion;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaVentaIntensiva);

                var ventasGanado = datosPoliza as List<ContenedorVentaGanado>;


                int folioVenta = ventasGanado.Select(folio => folio.VentaGanado.FolioTicket).FirstOrDefault();
                int organizacionId = ventasGanado[0].OrganizacionId;
                DateTime fechaVenta = ventasGanado.Select(fecha => fecha.VentaGanado.FechaVenta).FirstOrDefault();

                var animalCostoBl = new AnimalCostoBL();

                OrganizacionInfo organizacionOrigen = ObtenerOrganizacionSociedadDivision(organizacionId, SociedadEnum.SuKarne);
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = organizacionOrigen.TituloPoliza,
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = "Salida x Venta Ganado Intensivo",
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = string.Format("{0} {1}", "FECHA:", fechaVenta),
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
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");
                GeneraLinea(2);

                string codigoSAP = ventasGanado.Select(cliente => cliente.Cliente.CodigoSAP).FirstOrDefault();
                clienteSAP = new ClienteInfo { CodigoSAP = codigoSAP };
                var clienteBL = new ClienteBL();
                clienteSAP = clienteBL.ObtenerClientePorCliente(clienteSAP);

                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0}:{1}", "CLIENTE",
															clienteSAP.Descripcion),
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
											  string.Format("{0}:{1}", "VENDEDOR",
															string.Empty),
										  Desplazamiento = 0
									  },
								  new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0}:{1}", "FECHA",
															fechaVenta.ToShortDateString()),
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Comprador");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Comprador");
                GeneraLineaEncabezadoDetalle();
                var tipoGanadoBL = new TipoGanadoBL();

                ContenedorTipoGanadoPoliza tipoPolazCont = new ContenedorTipoGanadoPoliza();
                List<ContenedorTipoGanadoPoliza> tipoGanadoPolizas = new List<ContenedorTipoGanadoPoliza>();
                TipoGanadoBL tipoGanadoBl = new TipoGanadoBL();
                tipoPolazCont.Animal = new AnimalInfo();
                tipoPolazCont.Animal.TipoGanado = new TipoGanadoInfo();
                tipoPolazCont.Animal.TipoGanado = tipoGanadoBl.ObtenerTipoGanadoIDPorEntradaGanado(ventasGanado[0].EntradaGandoId);
                tipoGanadoPolizas.Add(tipoPolazCont);
                GeneraLineasDetalleIntensivo(tipoPolazCont, ventasGanado);
                GeneraLinea(12);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaEncabezadoCostos();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaCostosIntensivo(ventasGanado, string.Empty);
                polizaImpresion.GeneraCostos("Costos");

                GeneraLineaCostosTotales();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaTotalCostosIntensivo(ventasGanado);

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Costos");
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(folioVenta);
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
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var ventasGanado = datosPoliza as List<ContenedorVentaGanado>;
            IList<PolizaInfo> poliza = ObtenerPoliza(ventasGanado);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        #region POLIZA XML

        private IList<PolizaInfo> ObtenerPoliza(List<ContenedorVentaGanado> ventasGanado)
        {
            var polizasSalidaVenta = new List<PolizaInfo>();

            List<AnimalCostoInfo> costosAnimal = new List<AnimalCostoInfo>();
            var costoGen = new AnimalCostoInfo();
            foreach (var contenedorVentaGanado in ventasGanado)
            {
                costoGen = new AnimalCostoInfo();
                costoGen.Importe = contenedorVentaGanado.Importe;
                costoGen.CostoID = contenedorVentaGanado.CostoId;
                costoGen.OrganizacionID = contenedorVentaGanado.OrganizacionId;
                costosAnimal.Add(costoGen);
            }

            var costosAgrupados = costosAnimal;
            
            if (costosAgrupados != null && costosAgrupados.Any())
            {
                TipoPolizaInfo tipoPoliza =
                    TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaVentaIntensiva.GetHashCode());
                if (tipoPoliza == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                              TipoPoliza.SalidaVentaIntensiva));
                }

                string textoDocumento = tipoPoliza.TextoDocumento;
                string tipoMovimiento = tipoPoliza.ClavePoliza;
                string postFijoRef3 = tipoPoliza.PostFijoRef3;

                var linea = 1;
                costos = ObtenerCostos();

                ClaveContableInfo claveContableAbono;
                DatosPolizaInfo datos;
                PolizaInfo polizaSalida;

                int folioTicket = ventasGanado[0].VentaGanado.FolioTicket;

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

                DateTime fecha = ventasGanado.Select(fechaVenta => fechaVenta.VentaGanado.FechaVenta).FirstOrDefault();
                string archivoFolio = ObtenerArchivoFolio(fecha);
                var division = string.Empty;
                OrganizacionInfo organizacion = null;

                int organizacionID = costosAnimal.Select(org => org.OrganizacionID).FirstOrDefault();

                ParametroOrganizacionInfo parametroCentroCosto =
                ObtenerParametroOrganizacionPorClave(organizacionID,
                                                     ParametrosEnum.CTACENTROCTOINT.ToString());
                if (parametroCentroCosto == null)
                {
                    throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
                }

                ParametroOrganizacionInfo parametroCentroBeneficio =
                    ObtenerParametroOrganizacionPorClave(organizacionID,
                                                         ParametrosEnum.CTOBENEFINT.ToString());
                if (parametroCentroBeneficio == null)
                {
                    throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE BENEFICIO NO CONFIGURADO"));
                }
                for (var indexCostos = 0; indexCostos < costosAgrupados.Count; indexCostos++)
                {
                    var costoAnimal = costosAgrupados[indexCostos];
                    
                    CostoInfo costo =
                        costos.FirstOrDefault(
                            tipo =>
                            tipo.CostoID == costoAnimal.CostoID);
                    if (costo == null)
                    {
                        costo = new CostoInfo();
                    }

                    organizacion = ObtenerOrganizacionSociedadDivision(costoAnimal.OrganizacionID, SociedadEnum.SuKarne);
                    if (organizacion == null)
                    {
                        organizacion = new OrganizacionInfo
                        {
                            TipoOrganizacion = new TipoOrganizacionInfo()
                        };
                    }
                    division = organizacion.Division;

                    //const int GANADO_INTENSIVO = 8;
                    //const int GANADO_MAQUILA = 9;
                    int iTipoOrganizacion = 0;
                    iTipoOrganizacion = (TipoCorral) ventasGanado[0].Lote.TipoCorralID == TipoCorral.Intensivo ? TipoOrganizacion.Intensivo.GetHashCode()
                                                                                                                      : TipoOrganizacion.Maquila.GetHashCode();
                    

                    //ventasGanado[0].Lote.TipoCorralID == GANADO_INTENSIVO
                    
                    claveContableAbono = ObtenerCuentaInventario(costo, organizacion.OrganizacionID,
                                                                iTipoOrganizacion);
                    if (claveContableAbono == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}",
                                                                  "NO EXISTE CONFIGURACION PARA LA CLAVE CONTABLE DEL ABONO, PARA EL COSTO",
                                                                  costo.Descripcion));
                    }
                    ClaveContableInfo claveContableCargo = ObtenerCuentaInventario(costo, organizacion.OrganizacionID,
                                                                                   TipoPoliza.SalidaVentaIntensiva);
                    if (claveContableCargo == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}",
                                                                  "NO EXISTE CONFIGURACION PARA LA CLAVE CONTABLE DEL CARGO, PARA EL COSTO",
                                                                  costo.Descripcion));
                    }

                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia,
                        FechaEntrada = fecha,
                        Folio = folioTicket.ToString(),
                        Importe = string.Format("{0}", costoAnimal.Importe.ToString("F2")),
                        Renglon = Convert.ToString(linea++),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = claveContableCargo.Valor,
                        CentroCosto =
                            claveContableCargo.Valor.StartsWith(PrefijoCuentaCentroCosto) ||
                            claveContableCargo.Valor.StartsWith(PrefijoCuentaCentroGasto)
                                ? parametroCentroCosto.Valor
                                : string.Empty,
                        CentroBeneficio =
                            claveContableCargo.Valor.StartsWith(PrefijoCuentaCentroBeneficio)
                                ? parametroCentroBeneficio.Valor
                                : string.Empty,
                        ArchivoFolio = archivoFolio,
                        Division = division,
                        PesoOrigen = 0,
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        Concepto = String.Format("{0}-{1}",
                                                 tipoMovimiento,
                                                 folioTicket),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    polizaSalida = GeneraRegistroPoliza(datos);
                    polizasSalidaVenta.Add(polizaSalida);

                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroReferencia,
                        FechaEntrada = fecha,
                        Folio = folioTicket.ToString(),
                        Importe = string.Format("{0}", (costoAnimal.Importe * -1).ToString("F2")),
                        Renglon = Convert.ToString(linea++),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        Cuenta = claveContableAbono.Valor,
                        CentroCosto =
                            claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroCosto) ||
                            claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroGasto)
                                ? parametroCentroCosto.Valor
                                : string.Empty,
                        CentroBeneficio =
                            claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroBeneficio)
                                ? parametroCentroBeneficio.Valor
                                : string.Empty,
                        ArchivoFolio = archivoFolio,
                        PesoOrigen = 0,
                        Division = division,
                        TipoDocumento = textoDocumento,
                        ClaseDocumento = postFijoRef3,
                        ComplementoRef1 = string.Empty,
                        Concepto = String.Format("{0}-{1}",
                                                 tipoMovimiento,
                                                 folioTicket),
                        Sociedad = organizacion.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                    };
                    polizaSalida = GeneraRegistroPoliza(datos);
                    polizasSalidaVenta.Add(polizaSalida);
                }

                datos = GenerarBeneficios(ventasGanado, true);
                datos.Folio = folioTicket.ToString();
                datos.Concepto = String.Format("{0}-{1}",
                                               tipoMovimiento,
                                               folioTicket);
                datos.TipoDocumento = textoDocumento;
                datos.ClaseDocumento = postFijoRef3;
                datos.NumeroReferencia = numeroReferencia;
                datos.Renglon = Convert.ToString(linea++);
                datos.FechaEntrada = fecha;
                datos.Division = division;
                datos.Ref3 = ref3.ToString();
                datos.ArchivoFolio = archivoFolio;
                datos.Sociedad = organizacion.Sociedad;
                datos.Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad);
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalidaVenta.Add(polizaSalida);

                datos = GenerarBeneficios(ventasGanado, false);
                datos.Folio = folioTicket.ToString();
                datos.Concepto = String.Format("{0}-{1}",
                                               tipoMovimiento,
                                               folioTicket);
                datos.TipoDocumento = textoDocumento;
                datos.NumeroReferencia = numeroReferencia;
                datos.ClaseDocumento = postFijoRef3;
                datos.Renglon = Convert.ToString(linea);
                datos.FechaEntrada = fecha;
                datos.Division = division;
                datos.Ref3 = ref3.ToString();
                datos.Sociedad = organizacion.Sociedad;
                datos.ArchivoFolio = archivoFolio;
                datos.Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad);
                polizaSalida = GeneraRegistroPoliza(datos);
                polizasSalidaVenta.Add(polizaSalida);
            }
            return polizasSalidaVenta;
        }

        private DatosPolizaInfo GenerarBeneficios(List<ContenedorVentaGanado> ventasGanado, bool cargo)
        {

            var costoAbono = new CostoInfo
            {
                ClaveContable = "001"
            };
            ClaveContableInfo claveContableAbono;
            var organizacionID = 0;
            if (cargo)
            {
                string cuentaCliente = ventasGanado.Select(cliente => cliente.Cliente.CodigoSAP).FirstOrDefault();
                claveContableAbono = new ClaveContableInfo
                {
                    Valor = cuentaCliente
                };
            }
            else
            {
                organizacionID =
                    ventasGanado.Select(org => org.OrganizacionId).FirstOrDefault();
                //claveContableAbono = ObtenerCuentaInventario(costoAbono, organizacionID, TipoPoliza.SalidaVenta);

                //Cuenta beneficio a la que se carga el abono del monto a pagar en el cargo del cliente
                var interfaceBl = new InterfaceSalidaBL();
                claveContableAbono = interfaceBl.ObtenerCuentaInventario(costoAbono, organizacionID,  ClaveCuenta.CuentaBeneficioIntensivo);
                if (claveContableAbono == null)
                {
                    claveContableAbono = new ClaveContableInfo
                    {
                        Valor = string.Empty
                    };
                }
                else
                {
                    IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
                    CuentaSAPInfo cuentaSap =
                        cuentasSap.FirstOrDefault(clave => clave.CuentaSAP == claveContableAbono.Valor);
                    if (cuentaSap == null)
                    {
                        claveContableAbono.Descripcion = string.Empty;
                    }
                    else
                    {
                        claveContableAbono.Descripcion = cuentaSap.Descripcion;
                    }
                }
            }
            var parametroCentro = new ParametroOrganizacionInfo();
            var beneficio = false;
            if (claveContableAbono != null)
            {
                if (claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroBeneficio))
                {
                    parametroCentro =
                        ObtenerParametroOrganizacionPorClave(organizacionID,
                                                             ParametrosEnum.CTACENTROBENEFICIOENG.ToString());
                    if (parametroCentro == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE BENEFICIO NO CONFIGURADO"));
                    }
                    beneficio = true;
                }
                else
                {
                    if (claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroCosto)
                            || claveContableAbono.Valor.StartsWith(PrefijoCuentaCentroGasto))
                    {
                        parametroCentro =
                            ObtenerParametroOrganizacionPorClave(organizacionID, ParametrosEnum.CTACENTROCOSTOENG.ToString());
                        if (parametroCentro == null)
                        {
                            throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE BENEFICIO NO CONFIGURADO"));
                        }
                    }
                }
            }

            int cantidadCabezas = ventasGanado[0].TotalCabezas;

            ContenedorVentaGanado contenido = new ContenedorVentaGanado();
            
            
            var beneficios =
                ventasGanado.GroupBy(
                    tipo => new { tipo.CausaPrecio.CausaPrecioID})
                    .Select(
                        pesos => new
                        {
                            Peso =

                            ((pesos.Select(bruto => bruto.VentaGanado.PesoBruto - bruto.VentaGanado.PesoTara)
                                .
                                FirstOrDefault())),
                            Precio = pesos.Select(pre => pre.CausaPrecio.Precio).FirstOrDefault(),
                            Cabezas = cantidadCabezas
                        }).ToList();
            
            decimal importe = 0;
            decimal pesoTotal =
                ventasGanado.Select(peso => peso.VentaGanado.PesoBruto - peso.VentaGanado.PesoTara).FirstOrDefault();
            decimal pesoBeneficios = (ventasGanado[0].VentaGanado.PesoBruto - ventasGanado[0].VentaGanado.PesoTara);
            decimal diferencia = pesoBeneficios - pesoTotal;
            bool existeDiferencia = diferencia != 0;
            if ((Math.Abs(diferencia) * cantidadCabezas) == pesoTotal)
            {
                existeDiferencia = false;
            }
            beneficios.ForEach(imp =>
            {
                if (existeDiferencia)
                {
                    if (imp.Peso - Math.Abs(diferencia) > 0)
                    {
                        importe += (imp.Precio * (imp.Peso + diferencia));
                    }
                    else
                    {
                        importe += (imp.Precio * (imp.Peso));
                    }
                }
                else
                {
                    importe += (imp.Precio * imp.Peso);
                }
                existeDiferencia = false;
            });
            var datos = new DatosPolizaInfo
            {
                Importe = string.Format("{0}{1}", cargo ? string.Empty : "-", Math.Abs(importe).ToString("F2")),
                ImporteIva = "0",
                Cliente = cargo ? claveContableAbono.Valor : string.Empty,
                Cuenta = cargo ? string.Empty : claveContableAbono.Valor,
                PesoOrigen = 0,
            };
            if (beneficio)
            {
                datos.CentroBeneficio = parametroCentro.Valor;
            }
            else
            {
                datos.CentroCosto = parametroCentro.Valor;
            }
            return datos;
        }

        #endregion POLIZA XML

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
                        Descripcion = clienteSAP.Descripcion
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

        /// <summary>
        /// Genera linea para el total de costo
        /// </summary>
        /// <param name="costos"></param>
        private void GeneraLineaTotalCostos(IList<AnimalCostoInfo> costos)
        {
            decimal totalCosto = costos.Sum(imp => imp.Importe);
            GenerarLineaTotalCosto(totalCosto, true);

            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);

            GenerarLineaElaboro();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
        }

        /// <summary>
        /// Genera linea para el total de costo
        /// </summary>
        /// <param name="costos"></param>
        private void GeneraLineaTotalCostosIntensivo(IList<ContenedorVentaGanado> costos)
        {
            decimal totalCosto = costos.Sum(imp => imp.Importe);
            GenerarLineaTotalCosto(totalCosto, true);

            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);
            polizaImpresion.GenerarLineaEnBlanco("Costos", 5);

            GenerarLineaElaboro();
            polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
        }

        /// <summary>
        /// Genera linea para Costo
        /// </summary>
        /// <param name="animalesCosto"></param>
        /// <param name="ventasGanado"> </param>
        /// <param name="observacion"> </param>
        private void GeneraLineaCostos(IList<AnimalCostoInfo> animalesCosto
                                     , List<ContenedorVentaGanado> ventasGanado, string observacion)
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
            var costosAgrupados = (from ca in animalesCosto
                                   from vg in ventasGanado
                                   where ca.AnimalID == vg.VentaGanadoDetalle.Animal.AnimalID
                                   group ca by new { ca.CostoID }
                                       into grupo
                                       select new
                                       {
                                           Importe = grupo.Sum(imp => imp.Importe),
                                           grupo.Key.CostoID,
                                           AnimalID = grupo.Select(animal => animal.AnimalID).FirstOrDefault(),
                                           FolioReferencia =
                                grupo.Select(folio => folio.FolioReferencia).FirstOrDefault(),
                                           FechaCosto = grupo.Select(fecha => fecha.FechaCosto).FirstOrDefault(),
                                           OrganizacionID =
                                grupo.Select(org => org.OrganizacionID).FirstOrDefault()
                                       }).OrderBy(id => id.CostoID).ToList();
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
                    costos.Where(clave => clave.CostoID == costo.CostoID).Select(desc => desc.Descripcion).
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

        /// <summary>
        /// Genera linea para Costo
        /// </summary>
        /// <param name="ventasGanado"> </param>
        /// <param name="observacion"> </param>
        private void GeneraLineaCostosIntensivo(List<ContenedorVentaGanado> ventasGanado, string observacion)
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
            
            var costosAgrupados = ventasGanado;

            
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
                    costos.Where(clave => clave.CostoID == costo.CostoId).Select(desc => desc.Descripcion).
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

        /// <summary>
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="tiposGanado"></param>
        /// <param name="costosAnimal"> </param>
        /// <param name="ventasGanado"> </param>
        private void GeneraLineasDetalle(IList<ContenedorTipoGanadoPoliza> tiposGanado
                                       , List<AnimalCostoInfo> costosAnimal, List<ContenedorVentaGanado> ventasGanado)
        {
            var detalles = (from tg in tiposGanado
                            join ca in costosAnimal on tg.Animal.AnimalID equals ca.AnimalID
                            join vg in ventasGanado on ca.AnimalID equals vg.VentaGanadoDetalle.Animal.AnimalID
                            select new
                            {
                                TipoGanado = tg.Animal.TipoGanado.Descripcion,
                                tg.Animal.TipoGanado.TipoGanadoID,
                                vg.VentaGanado.PesoBruto,
                                vg.VentaGanado.PesoTara,
                                vg.CausaPrecio.Precio,
                                ca.Importe,
                                tg.Lote.Lote,
                                tg.Lote.Corral.Codigo,
                                vg.CausaPrecio.CausaPrecioID
                            }).ToList();
            int cantidadCabezas = ventasGanado[0].TotalCabezas;
            decimal pesoVenta = detalles.Select(peso => peso.PesoBruto - peso.PesoTara).FirstOrDefault();
            var beneficios =
                ventasGanado.GroupBy(
                    tipo => new { tipo.CausaPrecio.CausaPrecioID, tipo.VentaGanadoDetalle.Animal.TipoGanado.Descripcion })
                    .Select(
                        pesos => new
                        {
                            Peso =
                        Math.Round(
                            pesos.Select(bruto => bruto.VentaGanado.PesoBruto - bruto.VentaGanado.PesoTara)
                                .
                                FirstOrDefault() / cantidadCabezas, 0),
                            Precio = pesos.Select(pre => pre.CausaPrecio.Precio).FirstOrDefault(),
                            Cabezas = pesos.Count(),
                            TipoGanado = pesos.Key.Descripcion,
                            Corral = detalles.Where(tipo => tipo.TipoGanado.Equals(pesos.Key.Descripcion)).Select(tipo => tipo.Codigo).FirstOrDefault(),
                            Lote = detalles.Where(tipo => tipo.TipoGanado.Equals(pesos.Key.Descripcion)).Select(tipo => tipo.Lote).FirstOrDefault(),
                            ImporteCosto = detalles.Where(tipo => tipo.TipoGanado.Equals(pesos.Key.Descripcion)).Sum(tipo => tipo.Importe),
                            PesoVenta = Math.Round((pesoVenta / cantidadCabezas), 0),
                        }).OrderBy(desc => desc.TipoGanado).ToList();
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;
            foreach (var detalle in beneficios)
            {
                detalleModel = new PolizaDetalleModel
                {
                    CantidadCabezas = detalle.Cabezas.ToString("F0"),
                    TipoGanado = detalle.TipoGanado,
                    PesoTotal = (detalle.Peso * detalle.Cabezas).ToString("F0"),
                    PesoPromedio = detalle.Peso.ToString("F0"),
                    PrecioPromedio = Math.Abs(detalle.ImporteCosto / detalle.PesoVenta).ToString("N2"),
                    ImportePromedio = Math.Abs(detalle.ImporteCosto).ToString("N2"),
                    PrecioVenta = (detalle.Precio).ToString("N2"),
                    ImporteVenta =
                        ((detalle.Precio * (Math.Round(detalle.Peso * detalle.Cabezas, 0)))).ToString
                        ("N2"),
                    Corral = detalle.Corral,
                    Lote = detalle.Lote
                };
                PolizaModel.Detalle.Add(detalleModel);
            }
            if (pesoVenta != PolizaModel.Detalle.Sum(peso => Convert.ToDecimal(peso.PesoTotal)))
            {
                decimal diferencia = pesoVenta - PolizaModel.Detalle.Sum(peso => Convert.ToDecimal(peso.PesoTotal));
                bool existeDiferencia = diferencia != 0;
                if ((Math.Abs(diferencia) * cantidadCabezas) == pesoVenta)
                {
                    existeDiferencia = false;
                }
                string pesoMinimo = PolizaModel.Detalle.Min(peso => peso.PesoTotal);
                detalleModel =
                    PolizaModel.Detalle.FirstOrDefault(peso => peso.PesoTotal.Equals(pesoMinimo));
                if (existeDiferencia && detalleModel != null)
                {
                    if (Convert.ToDecimal(detalleModel.PesoTotal) - Math.Abs(diferencia) > 0)
                    {
                        decimal pesoDiferencia = Convert.ToDecimal(detalleModel.PesoTotal) - Math.Abs(diferencia);
                        detalleModel.PesoTotal = (pesoDiferencia).ToString("F0");
                        detalleModel.PesoPromedio =
                            (pesoDiferencia / Convert.ToInt32(detalleModel.CantidadCabezas)).ToString("F0");
                        detalleModel.ImporteVenta = (Convert.ToDecimal(detalleModel.PrecioVenta) *
                                                     (pesoDiferencia *
                                                      Convert.ToInt32(detalleModel.CantidadCabezas))).ToString("N2");
                    }
                }
            }
            polizaImpresion.GenerarDetalles("Detalle");
        }

        /// <summary>
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="tiposGanado"></param>
        /// <param name="ventasGanado"></param>
        private void GeneraLineasDetalleIntensivo(ContenedorTipoGanadoPoliza tiposGanado, List<ContenedorVentaGanado> ventasGanado)
        {
            LoteInfo lote = new LoteInfo();
            LoteBL loteBl = new LoteBL();

            lote = loteBl.ObtenerPorID(ventasGanado[0].Lote.LoteID);
            
            var detalles = (from  vg in ventasGanado
                            select new
                            {
                                tiposGanado = tiposGanado.Animal.TipoGanado.Descripcion,
                                TipoGanado = tiposGanado.Animal.TipoGanado.TipoGanadoID,
                                vg.VentaGanado.PesoBruto,
                                vg.VentaGanado.PesoTara,
                                vg.CausaPrecio.Precio,
                                vg.Importe,
                                lote.Lote,
                                lote.Corral.Codigo,
                                vg.CausaPrecio.CausaPrecioID
                            }).ToList();
            int cantidadCabezas = ventasGanado[0].TotalCabezas;
            decimal pesoVenta = detalles.Select(peso => peso.PesoBruto - peso.PesoTara).FirstOrDefault();
            var beneficios =
                ventasGanado.GroupBy(
                    tipo => new { tipo.CausaPrecio.CausaPrecioID })
                    .Select(
                        pesos => new
                        {
                            Peso =
                        Math.Round(
                            pesos.Select(bruto => bruto.VentaGanado.PesoBruto - bruto.VentaGanado.PesoTara)
                                .
                                FirstOrDefault() / cantidadCabezas, 0),
                            Precio = pesos.Select(pre => pre.CausaPrecio.Precio).FirstOrDefault(),
                            Cabezas = cantidadCabezas,
                            TipoGanado = tiposGanado.Animal.TipoGanado.Descripcion,
                            Corral = lote.Corral,
                            Lote = lote,
                            ImporteCosto = detalles.Sum(tipo => tipo.Importe),
                            PesoVenta = Math.Round((pesoVenta / cantidadCabezas), 0),
                        }).OrderBy(desc => desc.TipoGanado).ToList();
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;
            foreach (var detalle in beneficios)
            {
                detalleModel = new PolizaDetalleModel
                {
                    CantidadCabezas = detalle.Cabezas.ToString("F0"),
                    TipoGanado = detalle.TipoGanado,
                    PesoTotal = (detalle.Peso * detalle.Cabezas).ToString("F0"),
                    PesoPromedio = detalle.Peso.ToString("F0"),
                    PrecioPromedio = Math.Abs(detalle.ImporteCosto / detalle.PesoVenta).ToString("N2"),
                    ImportePromedio = Math.Abs(detalle.ImporteCosto).ToString("N2"),
                    PrecioVenta = (detalle.Precio).ToString("N2"),
                    ImporteVenta =
                        ((detalle.Precio * (Math.Round(detalle.Peso * detalle.Cabezas, 0)))).ToString
                        ("N2"),
                    Corral = detalle.Corral.Codigo,
                    Lote = detalle.Lote.Lote
                };
                PolizaModel.Detalle.Add(detalleModel);
            }
            if (pesoVenta != PolizaModel.Detalle.Sum(peso => Convert.ToDecimal(peso.PesoTotal)))
            {
                decimal diferencia = pesoVenta - PolizaModel.Detalle.Sum(peso => Convert.ToDecimal(peso.PesoTotal));
                bool existeDiferencia = diferencia != 0;
                if ((Math.Abs(diferencia) * cantidadCabezas) == pesoVenta)
                {
                    existeDiferencia = false;
                }
                string pesoMinimo = PolizaModel.Detalle.Min(peso => peso.PesoTotal);
                detalleModel =
                    PolizaModel.Detalle.FirstOrDefault(peso => peso.PesoTotal.Equals(pesoMinimo));
                if (existeDiferencia && detalleModel != null)
                {
                    if (Convert.ToDecimal(detalleModel.PesoTotal) - Math.Abs(diferencia) > 0)
                    {
                        decimal pesoDiferencia = Convert.ToDecimal(detalleModel.PesoTotal) - Math.Abs(diferencia);
                        detalleModel.PesoTotal = (pesoDiferencia).ToString("F0");
                        detalleModel.PesoPromedio =
                            (pesoDiferencia / Convert.ToInt32(detalleModel.CantidadCabezas)).ToString("F0");
                        detalleModel.ImporteVenta = (Convert.ToDecimal(detalleModel.PrecioVenta) *
                                                     (pesoDiferencia *
                                                      Convert.ToInt32(detalleModel.CantidadCabezas))).ToString("N2");
                    }
                }
            }
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

        #endregion IMPRESION

        #endregion METODOS PRIVADOS
    }
}
