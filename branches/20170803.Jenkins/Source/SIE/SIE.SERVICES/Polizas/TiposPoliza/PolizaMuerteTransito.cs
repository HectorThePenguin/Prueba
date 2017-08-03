using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using System.Globalization;
using SIE.Base.Log;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaMuerteTransito : PolizaAbstract
    {

        #region VARIABLES PRIVADAS

        private IList<CostoInfo> costos;
        private ClienteInfo clienteSAP = null;
        private PolizaImpresion<PolizaModel> polizaImpresion = null;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            //throw new System.NotImplementedException();
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.PolizaMuerteTransito);

                var muertesGanado = datosPoliza as List<EntradaGanadoMuerteInfo>;

                /*List<AnimalInfo> animales = muertesGanado.Select(costo => new AnimalInfo
                {
                    AnimalID =
                        costo.Animal.
                        AnimalID,
                    OrganizacionIDEntrada =
                        costo.Animal.
                        OrganizacionIDEntrada 
                }).ToList(); */

                long folioMuerte = muertesGanado.Select(folio => folio.FolioMuerte).FirstOrDefault();
                //int organizacionID = animales.Select(org => org.OrganizacionIDEntrada).FirstOrDefault(); 
                int organizacionID = muertesGanado.Select(org => org.OrganizacionDestinoID).FirstOrDefault();
                DateTime fecha = muertesGanado.Select(muerte => muerte.Fecha).FirstOrDefault();

                if (fecha == new DateTime(1, 1, 1))
                {
                    fecha = DateTime.Now;
                }

                OrganizacionInfo organizacionDestino = ObtenerOrganizacionIVA(organizacionID);
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
										  Descripcion = "Salida De Ganado x Muerte En Transito",
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = string.Format("{0} {1}", "FECHA:", fecha),
										  Desplazamiento = 0
									  },
								  new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0} {1}", "FOLIO No.",
															folioMuerte),
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Folio");
                GeneraLinea(2);

                string codigoSAP = muertesGanado.Select(cliente => cliente.ProveedorFletes.CodigoSAP).FirstOrDefault();
                clienteSAP = new ClienteInfo { CodigoSAP = codigoSAP };
                var clienteBL = new ClienteBL();
                clienteSAP = clienteBL.ObtenerClientePorCliente(clienteSAP);

                OperadorBL operadorBl = new OperadorBL();
                OperadorInfo operador = operadorBl.ObtenerPorUsuarioId(muertesGanado.Select(vendedor => vendedor.UsuarioCreacionID).FirstOrDefault(), organizacionID);

                if (clienteSAP == null)
                {
                    throw new ExcepcionServicio(
                    string.Format("NO SE ENCONTRARON LOS DATOS DEL PROVEEDOR"));
                }

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
															organizacionDestino.Descripcion),
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
															operador.NombreCompleto),
										  Desplazamiento = 0
									  },
								  new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0}:{1}", "FECHA",
															fecha.ToShortDateString()),
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "Comprador");

                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Comprador");
                GeneraLineaEncabezadoDetalle();
                /*var tipoGanadoBL = new TipoGanadoBL();
                List<ContenedorTipoGanadoPoliza> tipoGanadoPolizas = tipoGanadoBL.ObtenerTipoPorAnimal(animales,
                                                                                                       TipoMovimiento.
                                                                                                          MuerteTransito);
                GeneraLineasDetalle(tipoGanadoPolizas, costosAnimal, muertesGanado); // TRES ANIMALES */
                GeneraLineasDetalle(muertesGanado, organizacionID);
                GeneraLinea(12);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaEncabezadoCostos();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaCostos(muertesGanado, string.Empty);
                polizaImpresion.GeneraCostos("Costos");

                GeneraLineaCostosTotales();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaTotalCostos(muertesGanado);

                GeneraLinea(5);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Costos");
                polizaImpresion.GenerarLineaEnBlanco();

                GeneraLineaEncabezadoRegistroContable(folioMuerte);
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
            var datosPolizaMuerteTransito = datosPoliza as List<EntradaGanadoMuerteInfo>;
            IList<PolizaInfo> polizas = ObtenerPolizas(datosPolizaMuerteTransito);
            return polizas;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPolizas(List<EntradaGanadoMuerteInfo> datosPolizaMuerteTransito)
        {
            var polizasMuertesTransito = new List<PolizaInfo>();

            int entradaGanadoID =
                datosPolizaMuerteTransito.Select(id => id.EntradaGanado.EntradaGanadoID).FirstOrDefault();
            ClienteInfo clienteVenta =
                datosPolizaMuerteTransito.Select(id => id.Cliente).FirstOrDefault();

            var entradaGanadoBL = new EntradaGanadoBL();
            EntradaGanadoInfo entradaGanado = entradaGanadoBL.ObtenerPorID(entradaGanadoID);

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.PolizaMuerteTransito.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.PolizaMuerteTransito));
            }
            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();
            OrganizacionInfo organizacionDestino = ObtenerOrganizacionIVA(entradaGanado.OrganizacionID);

            IList<CostoInfo> costos = ObtenerCostos();

            int organizacionID = organizacionDestino.OrganizacionID;

            string division = organizacionDestino.Division;
            string sociedad = organizacionDestino.Sociedad;
            string textoDocumento = tipoPoliza.TextoDocumento;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;
            string prefijoConcepto = tipoPoliza.ClavePoliza;

            var ref3 = new StringBuilder();
            ref3.Append("03");
            ref3.Append(Convert.ToString(entradaGanado.FolioEntrada).PadLeft(10, ' '));
            ref3.Append(new Random(10).Next(10, 20));
            ref3.Append(new Random(30).Next(30, 40));
            ref3.Append(DateTime.Now.Millisecond);
            ref3.Append(postFijoRef3);

            DateTime fecha = entradaGanado.FechaEntrada;

            var archivoFolio = new StringBuilder(ObtenerArchivoFolio(fecha));
            //var numeroDocumento =
            //    new StringBuilder(string.Format("{0}{1}", entradaGanado.FolioEntrada, ObtenerNumeroReferencia));
            var numeroDocumento = ObtenerNumeroReferenciaFolio(entradaGanado.FolioEntrada);

            decimal peso = datosPolizaMuerteTransito.Sum(pesoAnimal => pesoAnimal.Peso);
            decimal importe = 0;

            int cabezas = datosPolizaMuerteTransito.Count;
            CuentaSAPInfo cuentaSapCargo;
            ClaveContableInfo claveContableCargo;
            CuentaSAPInfo cuentaSapAbono;
            ClaveContableInfo claveContableAbono;

            const string COMPLEMENTO_REF1 = "czas.";
            const string UNIDAD_MOVIMIENTO = "Kgs.";
            const string DESCRIPCION_MOVIMIENTO = "CABEZAS";

            ParametroOrganizacionInfo parametroOrganizacion =
                ObtenerParametroOrganizacionPorClave(organizacionID, ParametrosEnum.CTACENTROCOSTOENG.ToString());

            DatosPolizaInfo datos;

            long folio = entradaGanado.FolioEntrada;
            var renglon = 1;

            PolizaInfo muerteTransito;

            var detalleMuertes = new List<EntradaGanadoMuerteDetalleInfo>();
            datosPolizaMuerteTransito.ForEach(det => detalleMuertes.AddRange(det.EntradaGanadMuerteDetalle));

            var detalleMuertesAgrupados = (from dm in detalleMuertes
                              group dm by new {dm.Costo}
                              into grupo
                              select new {
                                        grupo.Key.Costo.CostoID,
                                        Importe = grupo.Sum(imp => imp.Importe),
                                    }).OrderBy(id => id.CostoID).ToList();


            CostoInfo costo;
            const int FLETE = 4;
            for (int indexMuertes = 0; indexMuertes < detalleMuertesAgrupados.Count; indexMuertes++)
            {
                //costo = costos.FirstOrDefault(id => id.CostoID == detalleMuertes[indexMuertes].Costo.CostoID);
                costo = costos.FirstOrDefault(id => id.CostoID == detalleMuertesAgrupados[indexMuertes].CostoID);
                if (costo.CostoID == FLETE)
                {
                    continue;
                }

                importe = detalleMuertesAgrupados[indexMuertes].Importe;

                claveContableCargo = ObtenerCuentaInventario(costo, organizacionID,
                                             TipoPoliza.PolizaMuerteTransito);
                cuentaSapCargo =
                    cuentasSap.FirstOrDefault(clave => clave.CuentaSAP.Equals(claveContableCargo.Valor));
                if (cuentaSapCargo == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1} EN COSTO",
                                                              "NO SE CUENTA CON CONFIGURACIÓN PARA EL COSTO",
                                                              claveContableCargo.Valor));
                }
                claveContableAbono = ObtenerCuentaInventario(costo, organizacionID, entradaGanado.TipoOrigen);
                cuentaSapAbono =
                    cuentasSap.FirstOrDefault(clave => clave.CuentaSAP.Equals(claveContableAbono.Valor));
                if (cuentaSapAbono == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1} EN TRANSITO",
                                                              "NO SE CUENTA CON CONFIGURACIÓN PARA EL COSTO",
                                                              claveContableAbono.Valor));
                }
                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroDocumento,
                    FechaEntrada = fecha,
                    Folio = folio.ToString(),
                    CabezasRecibidas = cabezas.ToString(),
                    NumeroDocumento = folio.ToString(),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? (importe * -1).ToString("F2") : importe.ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(renglon++),
                    Cabezas = Convert.ToString(cabezas),
                    ImporteIva = "0",
                    Ref3 = ref3.ToString(),
                    ArchivoFolio = archivoFolio.ToString(),
                    DescripcionCosto = cuentaSapCargo.Descripcion,
                    Cuenta = cuentaSapCargo.CuentaSAP,
                    CentroCosto =
                        cuentaSapCargo.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                            ? parametroOrganizacion.Valor
                            : string.Empty,
                    PesoOrigen = peso,
                    Division = division,
                    ComplementoRef1 = COMPLEMENTO_REF1,
                    TipoDocumento = textoDocumento,
                    Concepto =
                        String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                      folio,
                                      cabezas, DESCRIPCION_MOVIMIENTO,
                                      peso.ToString("N0"), UNIDAD_MOVIMIENTO),
                    Sociedad = organizacionDestino.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, sociedad),
                };
                muerteTransito = GeneraRegistroPoliza(datos);
                polizasMuertesTransito.Add(muerteTransito);

                datos = new DatosPolizaInfo
                {
                    NumeroReferencia = numeroDocumento,
                    FechaEntrada = fecha,
                    Folio = folio.ToString(),
                    CabezasRecibidas = cabezas.ToString(),
                    NumeroDocumento = folio.ToString(),
                    ClaseDocumento = postFijoRef3,
                    Importe =
                        string.Format("{0}", Cancelacion ? importe.ToString("F2") : (importe * -1).ToString("F2")),
                    IndicadorImpuesto = String.Empty,
                    Renglon = Convert.ToString(renglon++),
                    Cabezas = Convert.ToString(cabezas),
                    ImporteIva = "0",
                    Ref3 = ref3.ToString(),
                    ArchivoFolio = archivoFolio.ToString(),
                    DescripcionCosto = cuentaSapAbono.Descripcion,
                    Cuenta = cuentaSapAbono.CuentaSAP,
                    PesoOrigen = peso,
                    Division = division,
                    ComplementoRef1 = COMPLEMENTO_REF1,
                    TipoDocumento = textoDocumento,
                    Concepto =
                        String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                      folio,
                                      cabezas, DESCRIPCION_MOVIMIENTO,
                                      peso.ToString("N0"), UNIDAD_MOVIMIENTO),
                    Sociedad = organizacionDestino.Sociedad,
                    Segmento = string.Format("{0}{1}", PrefijoSegmento, sociedad),
                };
                muerteTransito = GeneraRegistroPoliza(datos);
                polizasMuertesTransito.Add(muerteTransito);
            }
            var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
            EntradaGanadoCosteoInfo entradaGanadoCosteo =
                entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entradaGanado.EntradaGanadoID, EstatusEnum.Inactivo);
            if (entradaGanadoCosteo == null)
            {
                entradaGanadoCosteo = entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entradaGanado.EntradaGanadoID);
            }
            if (entradaGanadoCosteo != null)
            {
                List<EntradaGanadoCostoInfo> costosEntrada = entradaGanadoCosteo.ListaCostoEntrada;
                costosEntrada =
                    costosEntrada.Where(prov => !prov.TieneCuenta && prov.Costo.CostoID == FLETE).ToList();
                if (costosEntrada.Any())
                {

                    ParametroOrganizacionInfo parametroDescuentoCabezasGanado =
                        ObtenerParametroOrganizacionPorClave(organizacionID,
                                                             ParametrosEnum.
                                                                 DESCUENTOGANADOMUERTO.ToString());
                    if (parametroDescuentoCabezasGanado == null)
                    {
                        throw new ExcepcionServicio(
                            string.Format("NO EXISTE CONFIGURACION PARA DESCUENTO POR CABEZA MUERTA"));
                    }
                    decimal importeDescuento = Convert.ToDecimal(parametroDescuentoCabezasGanado.Valor) * cabezas;

                    decimal importeFlete = (costosEntrada.Sum(costoentrada => costoentrada.Importe) / entradaGanado.CabezasRecibidas)* cabezas;
                    parametroOrganizacion = ObtenerParametroOrganizacionPorClave(organizacionID,
                                                                                 ParametrosEnum.
                                                                                     CTACENTROBENEFICIOENG.ToString());
                    ProveedorInfo proveedor = costosEntrada.Select(prov => prov.Proveedor).FirstOrDefault();
                    costo = new CostoInfo
                                {
                                    CostoID = FLETE
                                };
                    claveContableAbono = ObtenerCuentaInventario(costo, organizacionID,
                                                                 TipoPoliza.SalidaVenta);
                    cuentaSapAbono =
                        cuentasSap.FirstOrDefault(clave => clave.CuentaSAP.Equals(claveContableAbono.Valor));
                    if (cuentaSapAbono == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1} EN COSTO",
                                                                  "NO SE CUENTA CON CONFIGURACIÓN PARA EL COSTO",
                                                                  claveContableAbono.Valor));
                    }
                    string codigoSAP = proveedor.CodigoSAP;
                    //clienteSAP = new ClienteInfo { CodigoSAP = codigoSAP };
                    //var clienteBL = new ClienteBL();
                    //clienteSAP = clienteBL.ObtenerClientePorCliente(clienteSAP);

                    //if (clienteSAP == null)
                    //{
                    //    throw new ExcepcionServicio(
                    //    string.Format("NO SE ENCOTRO LOS DATOS DEL PROVEEDOR"));
                    //}

                    datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroDocumento,
                                    FechaEntrada = fecha,
                                    Folio = folio.ToString(),
                                    CabezasRecibidas = cabezas.ToString(),
                                    NumeroDocumento = folio.ToString(),
                                    ClaseDocumento = postFijoRef3,
                                    Importe =
                                        string.Format("{0}", Cancelacion ? (importeDescuento * -1).ToString("F2") 
                                                                         : importeDescuento.ToString("F2")),
                                    IndicadorImpuesto = String.Empty,
                                    Renglon = Convert.ToString(renglon++),
                                    Cabezas = Convert.ToString(cabezas),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    //Cuenta = clienteSAP.CodigoSAP,
                                    //Cliente = clienteSAP.Descripcion,
                                    //Cuenta = clienteVenta.CodigoSAP,
                                    Cliente = clienteVenta.CodigoSAP,
                                    ArchivoFolio = archivoFolio.ToString(),
                                    //DescripcionCosto = proveedor.Descripcion,
                                    //ClaveProveedor = proveedor.CodigoSAP,
                                    PesoOrigen = peso,
                                    Division = division,
                                    ComplementoRef1 = COMPLEMENTO_REF1,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                      folio,
                                                      cabezas, DESCRIPCION_MOVIMIENTO,
                                                      peso.ToString("N0"), UNIDAD_MOVIMIENTO),
                                    Sociedad = organizacionDestino.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, sociedad),
                                };
                    muerteTransito = GeneraRegistroPoliza(datos);
                    polizasMuertesTransito.Add(muerteTransito);

                    datos = new DatosPolizaInfo
                                {
                                    NumeroReferencia = numeroDocumento,
                                    FechaEntrada = fecha,
                                    Folio = folio.ToString(),
                                    CabezasRecibidas = cabezas.ToString(),
                                    NumeroDocumento = folio.ToString(),
                                    ClaseDocumento = postFijoRef3,
                                    Importe =
                                        string.Format("{0}", Cancelacion ? importeDescuento.ToString("F2")
                                                                         : (importeDescuento * -1).ToString("F2")),
                                    IndicadorImpuesto = String.Empty,
                                    Renglon = Convert.ToString(renglon++),
                                    Cabezas = Convert.ToString(cabezas),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = cuentaSapAbono.Descripcion,
                                    Cuenta = cuentaSapAbono.CuentaSAP,
                                    CentroBeneficio =
                                        cuentaSapAbono.CuentaSAP.StartsWith(PrefijoCuentaCentroBeneficio)
                                            ? parametroOrganizacion.Valor
                                            : string.Empty,
                                    PesoOrigen = peso,
                                    Division = division,
                                    ComplementoRef1 = COMPLEMENTO_REF1,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                      folio,
                                                      cabezas, DESCRIPCION_MOVIMIENTO,
                                                      peso.ToString("N0"), UNIDAD_MOVIMIENTO),
                                    Sociedad = organizacionDestino.Sociedad,
                                    Segmento = string.Format("{0}{1}", PrefijoSegmento, sociedad),
                                };
                    muerteTransito = GeneraRegistroPoliza(datos);
                    polizasMuertesTransito.Add(muerteTransito);

                    //Fletes
                    costo.ClaveContable = costosEntrada.FirstOrDefault().Costo.ClaveContable;
                    claveContableCargo = ObtenerCuentaInventario(costo, organizacionID,
                                             TipoPoliza.SalidaMuerte);
                    cuentaSapCargo =
                        cuentasSap.FirstOrDefault(clave => clave.CuentaSAP.Equals(claveContableCargo.Valor));
                    if (cuentaSapCargo == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1} EN COSTO",
                                                                  "NO SE CUENTA CON CONFIGURACIÓN PARA EL COSTO",
                                                                  claveContableCargo.Valor));
                    }

                    parametroOrganizacion = ObtenerParametroOrganizacionPorClave(organizacionID,
                                                                                 ParametrosEnum.
                                                                                     CTACENTROCOSTOENG.ToString());


                    claveContableAbono = ObtenerCuentaInventario(costo, organizacionID, entradaGanado.TipoOrigen);
                    cuentaSapAbono =
                        cuentasSap.FirstOrDefault(clave => clave.CuentaSAP.Equals(claveContableAbono.Valor));
                    if (cuentaSapAbono == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1} EN TRANSITO",
                                                                  "NO SE CUENTA CON CONFIGURACIÓN PARA EL COSTO",
                                                                  claveContableAbono.Valor));
                    }

                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = fecha,
                        Folio = folio.ToString(),
                        CabezasRecibidas = cabezas.ToString(),
                        NumeroDocumento = folio.ToString(),
                        ClaseDocumento = postFijoRef3,
                        Importe =
                            string.Format("{0}", Cancelacion ? (importeFlete * -1).ToString("F2") : importeFlete.ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(renglon++),
                        Cabezas = Convert.ToString(cabezas),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        ArchivoFolio = archivoFolio.ToString(),
                        DescripcionCosto = cuentaSapCargo.Descripcion,
                        Cuenta = cuentaSapCargo.CuentaSAP,
                        CentroCosto =
                            cuentaSapCargo.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                                ? parametroOrganizacion.Valor
                                : string.Empty,
                        PesoOrigen = peso,
                        Division = division,
                        ComplementoRef1 = COMPLEMENTO_REF1,
                        TipoDocumento = textoDocumento,
                        Concepto =
                            String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                          folio,
                                          cabezas, DESCRIPCION_MOVIMIENTO,
                                          peso.ToString("N0"), UNIDAD_MOVIMIENTO),
                        Sociedad = organizacionDestino.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, sociedad),
                    };
                    muerteTransito = GeneraRegistroPoliza(datos);
                    polizasMuertesTransito.Add(muerteTransito);

                    datos = new DatosPolizaInfo
                    {
                        NumeroReferencia = numeroDocumento,
                        FechaEntrada = fecha,
                        Folio = folio.ToString(),
                        CabezasRecibidas = cabezas.ToString(),
                        NumeroDocumento = folio.ToString(),
                        ClaseDocumento = postFijoRef3,
                        Importe =
                            string.Format("{0}", Cancelacion ? importeFlete.ToString("F2") : (importeFlete * -1).ToString("F2")),
                        IndicadorImpuesto = String.Empty,
                        Renglon = Convert.ToString(renglon++),
                        Cabezas = Convert.ToString(cabezas),
                        ImporteIva = "0",
                        Ref3 = ref3.ToString(),
                        ArchivoFolio = archivoFolio.ToString(),
                        DescripcionCosto = cuentaSapAbono.Descripcion,
                        Cuenta = cuentaSapAbono.CuentaSAP,
                        PesoOrigen = peso,
                        Division = division,
                        ComplementoRef1 = COMPLEMENTO_REF1,
                        TipoDocumento = textoDocumento,
                        Concepto =
                            String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                          folio,
                                          cabezas, DESCRIPCION_MOVIMIENTO,
                                          peso.ToString("N0"), UNIDAD_MOVIMIENTO),
                        Sociedad = organizacionDestino.Sociedad,
                        Segmento = string.Format("{0}{1}", PrefijoSegmento, sociedad),
                    };
                    muerteTransito = GeneraRegistroPoliza(datos);
                    polizasMuertesTransito.Add(muerteTransito);
                }
            }
            return polizasMuertesTransito;
        }

        #endregion METODOS PRIVADOS

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
        /// <param name="muertesGanado"></param>
        private void GeneraLineaTotalCostos(List<EntradaGanadoMuerteInfo> muertesGanado)
        {

            var detalleMuertes = new List<EntradaGanadoMuerteDetalleInfo>();
            muertesGanado.ForEach(det => detalleMuertes.AddRange(det.EntradaGanadMuerteDetalle));
            var costosAgrupados = (from dm in detalleMuertes
                                   group dm by new { dm.Costo }
                                       into grupo
                                       select new
                                       {
                                           grupo.Key.Costo.CostoID,
                                           Importe = grupo.Sum(imp => imp.Importe),
                                       }).OrderBy(id => id.CostoID).ToList();

            decimal totalCosto = costosAgrupados.Sum(imp => imp.Importe);
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
        private void GeneraLineaCostos(List<EntradaGanadoMuerteInfo> muertesGanado, string observacion)
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
                    if (index%27 == 0)
                    {
                        observaciones.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
            }
            const int NUMERO_LINEA = 0;
            var observacionesImpresas = new List<string>(observaciones);
            /*var costosAgrupados = (from ca in animalesCosto
                                   from vg in ventasGanado
                                   where ca.AnimalID == vg.VentaGanadoDetalle.Animal.AnimalID
                                   group ca by new {ca.CostoID}
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
                                              }).OrderBy(id => id.CostoID).ToList();*/
            var detalleMuertes = new List<EntradaGanadoMuerteDetalleInfo>();
            muertesGanado.ForEach(det => detalleMuertes.AddRange(det.EntradaGanadMuerteDetalle));
            var costosAgrupados = (from dm in detalleMuertes
                                           group dm by new { dm.Costo }
                                               into grupo
                                               select new
                                               {
                                                   grupo.Key.Costo.CostoID,
                                                   Importe = grupo.Sum(imp => imp.Importe),
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
        /// Genera las Lineas del Detalle
        /// </summary>
        /// <param name="tiposGanado"></param>
        /// <param name="costosAnimal"> </param>
        /// <param name="ventasGanado"> </param>
        private void GeneraLineasDetalle(List<EntradaGanadoMuerteInfo> muertesGanado, int organizacionID)
        {
        /*private void GeneraLineasDetalle(IList<ContenedorTipoGanadoPoliza> tiposGanado
                                       , List<AnimalCostoInfo> costosAnimal, List<EntradaGanadoMuerteInfo> ventasGanado)
        {            
            var detalles = (from tg in tiposGanado
                            join ca in costosAnimal on tg.Animal.AnimalID equals ca.AnimalID
                            join vg in ventasGanado on ca.AnimalID equals  vg.VentaGanadoDetalle.Animal.AnimalID
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
            int cantidadCabezas = ventasGanado.Count;
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
                                       PesoTotal = (detalle.Peso*detalle.Cabezas).ToString("F0"),
                                       PesoPromedio = detalle.Peso.ToString("F0"),
                                       PrecioPromedio = Math.Abs(detalle.ImporteCosto/detalle.PesoVenta).ToString("N2"),
                                       ImportePromedio = Math.Abs(detalle.ImporteCosto).ToString("N2"),
                                       PrecioVenta = (detalle.Precio).ToString("N2"),
                                       ImporteVenta =
                                           ((detalle.Precio*(Math.Round(detalle.Peso*detalle.Cabezas, 0)))).ToString
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
                if ((Math.Abs(diferencia)*cantidadCabezas) == pesoVenta)
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
                            (pesoDiferencia/Convert.ToInt32(detalleModel.CantidadCabezas)).ToString("F0");
                        detalleModel.ImporteVenta = (Convert.ToDecimal(detalleModel.PrecioVenta)*
                                                     (pesoDiferencia *
                                                      Convert.ToInt32(detalleModel.CantidadCabezas))).ToString("N2");
                    }
                }
            }*/

            ParametroOrganizacionInfo parametroDescuentoCabezasGanado =
                        ObtenerParametroOrganizacionPorClave(organizacionID,
                                                             ParametrosEnum.
                                                                 DESCUENTOGANADOMUERTO.ToString());
            if (parametroDescuentoCabezasGanado == null)
            {
                throw new ExcepcionServicio(
                    string.Format("NO EXISTE CONFIGURACION PARA DESCUENTO POR CABEZA MUERTA"));
            }
            decimal cabezaMuertaGanado = decimal.Parse(parametroDescuentoCabezasGanado.Valor);
            
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;
            decimal pesoTotal = muertesGanado.Sum(registro => registro.Peso);
			decimal pesoPromedio = pesoTotal / muertesGanado.Count;

			List<EntradaGanadoMuerteDetalleInfo> detalle = new List<EntradaGanadoMuerteDetalleInfo>();
			foreach (EntradaGanadoMuerteInfo muerte in muertesGanado)
			{
				detalle.AddRange(muerte.EntradaGanadMuerteDetalle);
			}														 

			detalleModel = new PolizaDetalleModel
			{
				CantidadCabezas = muertesGanado.Count.ToString("F0"),
				TipoGanado = "ANIMALES",
				PesoTotal = pesoTotal.ToString("N0"),
				PesoPromedio = pesoPromedio.ToString("N0"),
				PrecioPromedio = ((detalle.Sum(registro => registro.Importe) - (muertesGanado.Count * cabezaMuertaGanado)) / 2).ToString("N2"),
				ImportePromedio = (detalle.Sum(registro => registro.Importe) - (muertesGanado.Count * cabezaMuertaGanado)).ToString("N2"),
				PrecioVenta = (cabezaMuertaGanado).ToString("N2"),
				ImporteVenta = (muertesGanado.Count * cabezaMuertaGanado).ToString("N2"),
				Corral = muertesGanado[0].EntradaGanado.CodigoCorral,
				Lote = muertesGanado[0].EntradaGanado.Lote.Lote
			};
            PolizaModel.Detalle.Add(detalleModel);
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
                                                      Descripcion = "TIPO GANADO",
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


    }
}
