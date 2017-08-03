using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Linq;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Base.Log;
using System.Globalization;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSalidaMuerte : PolizaAbstract
    {
        #region CONSTRUCTORES

        #endregion CONSTRUCTORES

        #region VARIABLES PRIVADAS

        private IList<CostoInfo> costos;
        private PolizaImpresion<PolizaModel> polizaImpresion;
        const int CADENA_LARGA = 50;

        #endregion VARIABLES PRIVADAS

        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            try
            {
                PolizaModel = new PolizaModel();
                polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaMuerte);

                var costosAnimal = datosPoliza as List<AnimalCostoInfo>;
                var animalCostoAgrupado = (from costo in costosAnimal
                                           group costo by new { costo.AnimalID, costo.CostoID }
                                               into agrupado
                                               select new AnimalCostoInfo
                                               {
                                                   AnimalID = agrupado.Key.AnimalID,
                                                   Arete = agrupado.Select(ani=> ani.Arete).FirstOrDefault(),
                                                   CostoID = agrupado.Key.CostoID,
                                                   Importe = agrupado.Sum(cos => cos.Importe),
                                                   FolioReferencia =
                                                       agrupado.Select(cos => cos.FolioReferencia).FirstOrDefault(),
                                                   FechaCosto = agrupado.Select(cos => cos.FechaCosto).FirstOrDefault(),
                                                   OrganizacionID = agrupado.Select(org => org.OrganizacionID).FirstOrDefault()
                                               }).ToList();
                costosAnimal = animalCostoAgrupado;

                long folioVenta = Convert.ToInt64(costosAnimal.Select(folio => folio.Arete).FirstOrDefault());
                int organizacionID = costosAnimal.Select(org => org.OrganizacionID).FirstOrDefault();
                DateTime fechaVenta = DateTime.Today;
                
                OrganizacionInfo organizacionOrigen = ObtenerOrganizacionIVA(organizacionID);
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
										  Descripcion = "Salida De Ganado x Muerte",
										  Desplazamiento = 0
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
								  new PolizaEncabezadoModel
									  {
										  Descripcion = string.Format("{0} {1}", "FECHA:", fechaVenta.ToShortDateString()),
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
                polizaImpresion.GenerarLineaEnBlanco("Folio", 2);

                PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
							  {
                                  new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0}:{1}", "REFERENCIA",
															organizacionOrigen.Descripcion),
									  },
								  new PolizaEncabezadoModel
									  {
										  Descripcion =
											  string.Format("{0}:{1}", "FECHA",
															fechaVenta.ToShortDateString()),
									  },
							  };
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");
                GeneraLinea(2);
                polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");

                GeneraLineaEncabezadoDetalle();
                var tipoGanadoBL = new TipoGanadoBL();
                List<long> animalId = costosAnimal.Select(id => id.AnimalID).Distinct().ToList();
                List<AnimalInfo> animales = animalId.Select(ani => new AnimalInfo
                                                                           {
                                                                               AnimalID = ani
                                                                           }).Distinct().ToList();
                List<ContenedorTipoGanadoPoliza> tipoGanadoPolizas = tipoGanadoBL.ObtenerTipoPorAnimal(animales,
                                                                                                       TipoMovimiento.
                                                                                                           Muerte);
                List<ContenedorVentaGanado> ventasGanado = costosAnimal
                                                .Select(venta => new ContenedorVentaGanado
                                                {
                                                    CausaPrecio = new CausaPrecioInfo(),
                                                    VentaGanado = new VentaGanadoInfo(),
                                                    VentaGanadoDetalle = new VentaGanadoDetalleInfo
                                                                            {
                                                                                Animal = new AnimalInfo
                                                                                            {
                                                                                                AnimalID = venta.AnimalID
                                                                                            }
                                                                            }
                                                }).ToList();
                GeneraLineasDetalle(tipoGanadoPolizas, costosAnimal, ventasGanado);
                GeneraLinea(12);
                polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");

                GeneraLineaEncabezadoCostos();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaCostos(costosAnimal, string.Empty);
                polizaImpresion.GeneraCostos("Costos");

                GeneraLineaCostosTotales();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                GeneraLineaTotalCostos(costosAnimal);

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
            var costosAnimales = datosPoliza as List<AnimalCostoInfo>;
            IList<PolizaInfo> poliza = ObtenerPoliza(costosAnimales);
            return poliza;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        #region Poliza XML
        private IList<PolizaInfo> ObtenerPoliza(List<AnimalCostoInfo> costosAnimales)
        {
            var polizasSalidaMuerte = new List<PolizaInfo>();

            var costosAgrupados =
                costosAnimales.Where(imp => imp.Importe > 0).GroupBy(costo => new {costo.CostoID, costo.AnimalID})
                    .Select(animal => new
                                          {
                                              Importe = animal.Sum(imp => imp.Importe),
                                              animal.Key.CostoID,
                                              animal.Key.AnimalID,
                                              Arete = animal.Select(ani=> ani.Arete).FirstOrDefault(),
                                              FolioReferencia =
                                          animal.Select(folio => folio.FolioReferencia).FirstOrDefault(),
                                              FechaCosto = animal.Select(fecha => fecha.FechaCosto).FirstOrDefault(),
                                              OrganizacionID = animal.Select(org => org.OrganizacionID).FirstOrDefault()
                                          }).ToList();
            if (costosAgrupados != null && costosAgrupados.Any())
            {
                TipoPolizaInfo tipoPoliza =
                    TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaMuerte.GetHashCode());
                if (tipoPoliza == null)
                {
                    throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                              TipoPoliza.SalidaMuerte));
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

                //string numeroReferencia = ObtenerNumeroReferencia;

                if (costos == null)
                {
                    costos = ObtenerCostos();
                }

                List<AnimalInfo> animales = costosAgrupados.Select(ani => new AnimalInfo
                                                                              {
                                                                                  AnimalID = ani.AnimalID
                                                                              }).ToList();
                List<AnimalMovimientoInfo> animalesMovimiento = ObtenerUltimoMovimiento(animales);
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

                    string numeroReferencia = costoAnimal.Arete;

                    OrganizacionInfo organizacion = ObtenerOrganizacionIVA(costoAnimal.OrganizacionID);
                    if (organizacion == null)
                    {
                        organizacion = new OrganizacionInfo
                                           {
                                               TipoOrganizacion = new TipoOrganizacionInfo()
                                           };
                    }
                    ClaveContableInfo claveContableAbono = ObtenerCuentaInventario(costo, organizacion.OrganizacionID,
                                                                organizacion.TipoOrganizacion.TipoOrganizacionID);
                    if (claveContableAbono == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA EL COSTO",
                                                                  costo.Descripcion));
                    }
                    ClaveContableInfo claveContableCargo = ObtenerCuentaInventario(costo, organizacion.OrganizacionID,
                                                                                   TipoPoliza.SalidaMuerte);
                    if (claveContableCargo == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0} {1}", "NO EXISTE CONFIGURACION PARA EL COSTO",
                                                                  costo.Descripcion));
                    }
                    ParametroOrganizacionInfo parametroCentroCosto =
                        ObtenerParametroOrganizacionPorClave(organizacion.OrganizacionID,
                                                             ParametrosEnum.CTACENTROCOSTOENG.ToString());
                    if (parametroCentroCosto == null)
                    {
                        throw new ExcepcionServicio(string.Format("{0}", "CENTRO DE COSTO NO CONFIGURADO"));
                    }
                    int peso =
                        animalesMovimiento.Where(kilos => kilos.AnimalID == costoAnimal.AnimalID
                                                          &&
                                                          kilos.TipoMovimientoID == TipoMovimiento.Muerte.GetHashCode())
                            .Select(proyeccion => proyeccion.Peso).FirstOrDefault();
                    DateTime fecha =
                        animalesMovimiento.Where(kilos => kilos.AnimalID == costoAnimal.AnimalID
                                                          &&
                                                          kilos.TipoMovimientoID == TipoMovimiento.Muerte.GetHashCode())
                            .Select(proyeccion => proyeccion.FechaMovimiento).FirstOrDefault();
                    if (fecha != null && fecha > default(DateTime))
                    {
                        string archivoFolio = ObtenerArchivoFolio(fecha);
                        var datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = fecha,
                            Folio = numeroReferencia,
                            Importe = string.Format("{0}", costoAnimal.Importe.ToString("F2")),
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
                            PesoOrigen = peso,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            ComplementoRef1 = string.Empty,
                            Concepto = String.Format("{0}-{1} {2} kgs",
                                                     tipoMovimiento,
                                                     numeroReferencia,
                                                     peso),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        PolizaInfo polizaSalida = GeneraRegistroPoliza(datos);
                        polizasSalidaMuerte.Add(polizaSalida);

                        datos = new DatosPolizaInfo
                        {
                            NumeroReferencia = numeroReferencia,
                            FechaEntrada = fecha,
                            Folio = numeroReferencia,
                            Importe = string.Format("{0}", (costoAnimal.Importe*-1).ToString("F2")),
                            Renglon = Convert.ToString(linea++),
                            ImporteIva = "0",
                            Ref3 = ref3.ToString(),
                            Cuenta = claveContableAbono.Valor,
                            ArchivoFolio = archivoFolio,
                            Division = organizacion.Division,
                            PesoOrigen = peso,
                            TipoDocumento = textoDocumento,
                            ClaseDocumento = postFijoRef3,
                            Concepto = String.Format("{0}-{1} {2} kgs",
                                                     tipoMovimiento,
                                                     numeroReferencia,
                                                     peso),
                            Sociedad = organizacion.Sociedad,
                            Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacion.Sociedad),
                        };
                        polizaSalida = GeneraRegistroPoliza(datos);
                        polizasSalidaMuerte.Add(polizaSalida);
                    }
                }
            }
            return polizasSalidaMuerte;
        }

        #endregion Poliza XML

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

            if (costos == null || !costos.Any())
            {
                costos = ObtenerCostos();
            }

            PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
            PolizaRegistroContableModel registroContable;
            foreach (var cargo in cargos)
            {
                CostoInfo descripcionCosto =
                    costos.FirstOrDefault(
                        costo => cargo.Cuenta.EndsWith(costo.ClaveContable));
                if (descripcionCosto == null)
                {
                    descripcionCosto = new CostoInfo
                    {
                        Descripcion = string.Empty
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
                    descripcionCosto = new CostoInfo
                    {
                        Descripcion = string.Empty
                    };
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
        /// Genera linea para Costo
        /// </summary>
        /// <param name="animalesCosto"></param>
        /// <param name="observacion"> </param>
        private void GeneraLineaCostos(IList<AnimalCostoInfo> animalesCosto, string observacion)
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

            if (costos == null)
            {
                costos = ObtenerCostos();
            }
            PolizaModel.Costos = new List<PolizaCostoModel>();
            PolizaCostoModel costoModel;
            foreach (var costo in animalesCosto)
            {
                if (!observacionesImpresas.Any())
                {
                    observacionesImpresas.Add(string.Empty);
                }
                string costoDescripcion =
                    costos.Where(clave => clave.CostoID == costo.CostoID).Select(desc => desc.Descripcion).
                        FirstOrDefault();
                var sbDescripcion = new StringBuilder();
                sbDescripcion.Append(costoDescripcion.Length > CADENA_LARGA
                                         ? costoDescripcion.Substring(0, CADENA_LARGA - 1).Trim()
                                         : costoDescripcion.Trim());
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
                            from ca in costosAnimal
                            from vg in ventasGanado
                            where tg.Animal.AnimalID == ca.AnimalID
                                  && ca.AnimalID == vg.VentaGanadoDetalle.Animal.AnimalID
                            select new
                                       {
                                           TipoGanado = tg.Animal.TipoGanado.Descripcion,
                                           vg.VentaGanado.PesoBruto,
                                           vg.VentaGanado.PesoTara,
                                           vg.CausaPrecio.Precio,
                                           ca.Importe,
                                           tg.Lote.Lote,
                                           tg.Lote.Corral.Codigo
                                       }).ToList();
            var tiposMovimiento = tiposGanado.GroupBy(tipo => tipo.Animal.TipoGanado.Descripcion)
                                             .Select(tipo => new
                                                                {
                                                                    Descripcion = tipo.Key
                                                                }).Distinct().ToList();
            var detalleAgrupado = detalles.GroupBy(desc => desc.TipoGanado)
                .Select(datos => new
                                     {
                                         Cabezas = tiposMovimiento.Count(tipo => tipo.Descripcion.Equals(datos.Key)),
                                         TipoGanado = datos.Key,
                                         PesoVenta = datos.Sum(pb => pb.PesoBruto - pb.PesoTara),
                                         PrecioVenta = datos.Select(pre => pre.Precio).FirstOrDefault(),
                                         Corral = datos.Select(corr => corr.Codigo).FirstOrDefault(),
                                         Lote = datos.Select(lote => lote.Lote).FirstOrDefault(),
                                         //ImporteCosto = datos.Sum(imp => imp.Importe)
                                     });
            decimal importe = costosAnimal.Sum(imp => imp.Importe);
            PolizaModel.Detalle = new List<PolizaDetalleModel>();
            PolizaDetalleModel detalleModel;

            List<AnimalInfo> animales = costosAnimal.Select(ani => new AnimalInfo
                                                                       {
                                                                           AnimalID = ani.AnimalID
                                                                       }).ToList();
            List<AnimalMovimientoInfo> animalesMovimiento = ObtenerUltimoMovimiento(animales);
            foreach (var detalle in detalleAgrupado)
            {
                int peso =
                    animalesMovimiento.Where(tipo => tipo.TipoMovimientoID == TipoMovimiento.Muerte.GetHashCode()).
                        Sum(ultimoPeso => ultimoPeso.Peso);
                if (peso == 0)
                {
                    peso = 1;
                }
                detalleModel = new PolizaDetalleModel
                                   {
                                       CantidadCabezas = detalle.Cabezas.ToString("N").Replace(".00", string.Empty),
                                       TipoGanado = detalle.TipoGanado,
                                       PesoTotal = peso.ToString("N").Replace(".00", string.Empty),
                                       PesoPromedio =
                                           (peso / detalle.Cabezas).ToString("N").Replace(".00", string.Empty),
                                       PrecioPromedio = (importe / peso).ToString("N2"),
                                       ImportePromedio = importe.ToString("N2"),
                                       Corral = detalle.Corral,
                                       Lote = detalle.Lote
                                   };
                PolizaModel.Detalle.Add(detalleModel);
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
                                                      Alineacion = "left"
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

        #endregion METODOS PRIVADOS
    }
}
