using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Impresion;
using SIE.Services.Polizas.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Polizas.TiposPoliza
{
    //corregir el tipo de poliza a SalidaGanadoEnTransitoMuerte
    //corregir la cuenta de resultados
    public class PolizaSalidaPorMuerteEnTransito : PolizaAbstract
    {
        #region VARIABLES PRIVADAS

        const string MONEDA = "MXN";
        const decimal TIPOCAMBIO = 0;
        const string BUSACT = "RFBU";
        const string CONCEPTO = "CABEZA(S)";

        private IList<CostoInfo> costos;
        private LoteInfo loteMuerte;
        private CorralInfo corralMuerte;
        private DatosPolizaSalidaGanadoTransitoInfo datos;
        private PolizaImpresion<PolizaModel> polizaImpresion;
        private List<SalidaGanadoEnTransitoDetalleInfo> detalles;

        #endregion

        #region METODOS SOBREESCRITOS
       
        /// <summary>
        /// Genera los nodos correspondientes a la poliza de una salida de ganado en transito por muerte
        /// </summary>
        /// <param name="datosPoliza">El objeto SalidaGanadoEnTransitoInfo con la informacion de la salida de ganado en transito por muerte al que se le generara la poliza</param>
        /// <returns>Regresa la lista de nodos de la poliza generada por la salida de ganado en transito por muerte</returns>
        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            try
            {
                IList<PolizaInfo> polizaSalida = new List<PolizaInfo>();
                var input = (SalidaGanadoEnTransitoInfo)datosPoliza;
                detalles = input.DetallesSalida;
                datos = ObtenerDatosPolizaSalidaPorMuerte(input);

                var noLinea = 1;

                //genera el ref3:
                var ref3 = new StringBuilder();
                ref3.Append("03");
                ref3.Append(Convert.ToString(input.Folio).PadLeft(10, ' '));
                ref3.Append(new Random(10).Next(10, 20));
                ref3.Append(new Random(30).Next(30, 40));
                ref3.Append(DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture));
                ref3.Append(datos.PostFijoRef3);
                //

                //genera el folio de archivo:
                var archivoFolio = new StringBuilder();
                archivoFolio.Append("P01");
                archivoFolio.Append(input.Fecha.ToString("yyyyMMdd"));
                archivoFolio.Append(DateTime.Now.Minute);
                archivoFolio.Append(DateTime.Now.Second);
                archivoFolio.Append(new Random(1).Next(1, 9));
                //

                //busca el costo de ganado
                var costoGanado = input.Costos.FirstOrDefault(costo => costo.CostoID == Costo.CostoGanado.GetHashCode());
                //

                if (costoGanado != null)
                {
                    //calcula el importe total sumando los importes de los detalles
                    if (input.DetallesSalida != null )
                    {
                        input.Importe = input.DetallesSalida.Sum(x => x.ImporteCosto);

                        #region Primer nodo : Total de costos de salida

                        //genera un nodo generico de la poliza por salida de ganado en transito por muerte
                        var nodo1 = GenerarNodoPlantillaPolizaMuerteTransito(input, noLinea, ref3.ToString(),
                            archivoFolio.ToString());
                        //

                        //busca la cuenta de Faltantes / Sobrantes de la organizacion que registro la salida de ganado en transito
                        var parOrganizacion = new ParametroOrganizacionBL();
                        var claveCuenta = parOrganizacion.ObtenerPorOrganizacionIDClaveParametro(input.OrganizacionID,
                            ParametrosEnum.CTAFALTANTESOBRANTE.ToString());
                        nodo1.Cuenta = claveCuenta.Valor;
                        if (claveCuenta.Valor.Substring(0, 4) == "5001")
                        {
                            nodo1.CentroCosto = datos.ParametroOrganizacionValor;
                        }
                        //

                        var importeCostos = input.DetallesSalida.Sum(costo => costo.ImporteCosto);
                        nodo1.Importe = "-" + importeCostos.ToString(CultureInfo.InvariantCulture);
                        polizaSalida.Add(nodo1);

                        #endregion

                        #region segundo nodo : Costo de salida correspondiente a salida de ganado

                        noLinea++;
                        ClaveContableInfo clavecontablecargo;
                        var nodo2 = GenerarNodoPlantillaPolizaMuerteTransito(input, noLinea, ref3.ToString(),
                            archivoFolio.ToString());

                        if (claveCuenta.Valor.Substring(0, 4) == "5001")
                        {
                            nodo2.CentroCosto = datos.ParametroOrganizacionValor;
                        }
                        var salidaPorMuerteEnTransitoDetalle =
                            input.DetallesSalida.FirstOrDefault(
                                detalle => detalle.CostoId == Costo.CostoGanado.GetHashCode());
                        if (salidaPorMuerteEnTransitoDetalle != null)
                        {
                            nodo2.Importe =
                                salidaPorMuerteEnTransitoDetalle.ImporteCosto.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            var ex = new Exception(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostosGanadoNoEncontrado);
                            Logger.Error(ex);
                            throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                        }

                        costos = ObtenerCostos();
                        var firstOrDefault = costos.FirstOrDefault(x => x.CostoID == costoGanado.CostoID);
                        if (firstOrDefault != null)
                        {
                            var costoAbonos = new CostoInfo
                            {
                                ClaveContable = firstOrDefault.ClaveContable
                            };
                            clavecontablecargo = ObtenerCuentaInventario(costoAbonos, input.OrganizacionID,
                                TipoPoliza.SalidaMuerteEnTransito);
                            if (clavecontablecargo != null) nodo2.Cuenta = clavecontablecargo.Valor;
                        }
                        else
                        {
                            var ex = new Exception(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostosGanadoNoEncontrado);
                            Logger.Error(ex);
                            throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                        }
                        polizaSalida.Add(nodo2);

                        #endregion

                        #region nodos de detalles importes costo corral

                        var nodosDetalles = new List<PolizaInfo>();
                        var costosPl = new CostoPL();
                        var costosclaves = costosPl.ObtenerTodos(EstatusEnum.Activo);

                        //agrega todos los costos de los detalles excepto el costo de ganado (por que ya se encuentra agregado)
                        foreach (
                            var costoCargo in
                                input.DetallesSalida.SkipWhile(costo => costo.CostoId == Costo.CostoGanado.GetHashCode())
                            )
                        {
                            noLinea++;

                            var temp = GenerarNodoPlantillaPolizaMuerteTransito(input, noLinea, ref3.ToString(),
                                archivoFolio.ToString());
                            if(costosclaves == null)
                            {
                                var ex = new Exception(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostosNoEncontrados);
                                Logger.Error(ex);
                                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                            }
                            var claveCosto = costosclaves.FirstOrDefault(x => x.CostoID == costoCargo.CostoId);
                            if (claveCosto != null)
                            {
                                var costosAbono = new CostoInfo
                                {
                                    ClaveContable = claveCosto.ClaveContable
                                };
                                //busca la clave contable para el costo correspondiente
                                clavecontablecargo = ObtenerCuentaInventario(costosAbono, input.OrganizacionID,
                                    TipoPoliza.SalidaMuerteEnTransito);
                                if (clavecontablecargo != null) temp.Cuenta = clavecontablecargo.Valor;
                            }
                            else
                            {
                                var ex = new Exception(
                                string.Format(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostoIdNoEncontrado, costoCargo.CostoId));
                                Logger.Error(ex);
                                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);   
                            }
                            if (claveCuenta.Valor.Substring(0, 4) == "5001")
                            {
                                temp.CentroCosto = datos.ParametroOrganizacionValor;
                            }
                            temp.Importe = costoCargo.ImporteCosto.ToString(CultureInfo.InvariantCulture);
                            nodosDetalles.Add(temp);
                        }

                        foreach (var polizaItem in nodosDetalles)
                        {
                            polizaSalida.Add(polizaItem);
                        }

                        #endregion

                    }
                    else
                    {
                        var ex = new Exception(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_DetalleSalidaGanadoTransitoNoProporcionados);
                        Logger.Error(ex);
                        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);      
                    }
                }
                else
                {
                    var ex = new Exception(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostosGanadoNoEncontrado);
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);   
                }
                return polizaSalida;
            }
             catch (Exception ex)
             {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }
        }

        #endregion 

        #region METODOS PRIVADOS

        #region IMPRESION

        /// <summary>
        /// genera el archivo PDF de la poliza
        /// </summary>
        /// <param name="contenedorSalidaPorMuerte">Objeto SalidaGanadoEnTransitoInfo que contiene la informacion de la salida de ganado en transito por muerte</param>
        /// <param name="polizas">Nodos de la poliza generada para la salida de ganado en transito</param>
        /// <returns>Regresa el archivo PDF que se guardara</returns>
        public override MemoryStream ImprimePoliza(object contenedorSalidaPorMuerte, IList<PolizaInfo> polizas)
        {
            try 
            {
                    PolizaModel = new PolizaModel();
                    polizaImpresion = new PolizaImpresion<PolizaModel>(PolizaModel, TipoPoliza.SalidaMuerteEnTransito);
                    var info = contenedorSalidaPorMuerte as SalidaGanadoEnTransitoInfo;

                if (info != null)
                    {
                        //genera los encabezados del PDF:
                        #region cabecera
                        var organizacionInfo = ObtenerOrganizacionSociedadDivision(info.OrganizacionID, SociedadEnum.SuKarne);
                   
                        PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                      {
                                          new PolizaEncabezadoModel
                                            {
                                                Descripcion = organizacionInfo.TituloPoliza,
                                                Desplazamiento = 0
                                            }
                                      };
                        polizaImpresion.GeneraCabecero(new[] { "100" }, "NombreGanadera");
                        PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                      {
                                          new PolizaEncabezadoModel
                                              {
                                                  Descripcion = "Salida x Muerte  En tránsito",
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

                        PolizaModel.Encabezados = new List<PolizaEncabezadoModel>
                                      {
                                          new PolizaEncabezadoModel
                                              {
                                                  Descripcion =
                                                      string.Format("{0}: {1}", "REFERENCIA",
                                                                    organizacionInfo.Descripcion),
                                              },
                                          new PolizaEncabezadoModel
                                              {
                                                  Descripcion =
                                                      string.Format("{0}: {1}", "FECHA",
                                                                    info.Fecha.ToShortDateString()),
                                              },
                                      };

                        polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");
                        GeneraLinea(2);
                        polizaImpresion.GeneraCabecero(new[] { "50", "50" }, "FECHA");

                        #endregion

                        //Genera las lineas de encabezado del detalle:
                        GeneraLineaEncabezadoDetalle();

                        //Genera las lineas de detalle del PDF de la poliza
                        GeneraLineasDetalle(info);
                        GeneraLinea(12);//genera un salto de linea vacio
                        polizaImpresion.GeneraCabecero(new[] { "100" }, "Detalle");
                        GeneraLineaEncabezadoCostos();//genera encabezados de los costos

                        polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                        GeneraLineaCostos(info.DetallesSalida, info.Observaciones);//genera las lineas de costos de la poliza PDF
                        polizaImpresion.GeneraCostos("Costos");//anexa las lineas generadas de costos al modelo PDF que se generara

                        GeneraLineaCostosTotales();//genera las lineas de sumatorias de los costos
                        polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                        GeneraLineaTotalCostos(info.DetallesSalida);

                        GeneraLinea(5);
                        polizaImpresion.GeneraCabecero(new[] { "100" }, "Costos");
                        polizaImpresion.GenerarLineaEnBlanco();
                        GeneraLineaEncabezadoRegistroContable(info.Folio);//genera el encabezado para los registros contables
                        polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");
                        GeneraLineaSubEncabezadoRegistroContable(false, "No DE CUENTA", "CARGOS", "ABONOS");
                        polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                        IList<PolizaInfo> cargos;
                        IList<PolizaInfo> abonos;
                        GeneraLineaRegistroContable(polizas, out cargos, out abonos);//genera los registros contables
                        polizaImpresion.GenerarRegistroContable("RegistroContable");//anexa las lineas generadas de registros contables al modelo PDF que se generara
                        GeneraLinea(5);

                        polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");

                        GenerarLineaSumaRegistroContable(polizas, "*= SUMAS -=>");//genera la linea de sumatoria de los registros contables
                        polizaImpresion.GeneraCabecero(new[] { "30", "60", "65", "25", "25" }, "RegistroContable");

                        GeneraLinea(5);
                        polizaImpresion.GeneraCabecero(new[] { "100" }, "RegistroContable");
                    return polizaImpresion.GenerarArchivo();
                }
                return new MemoryStream();
            }
             catch (Exception ex)
             {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }
        }
     
        private void GeneraLineasDetalle(SalidaGanadoEnTransitoInfo salidaMuerte)
        {
            try
            {
                //salidaMuerte.LoteID;
                //var corralOrganizacion = CorralBL.ObtenerCorralesPorOrganizacionID(salidaMuerte.OrganizacionID).FirstOrDefault(corral => corral.LoteID == salidaMuerte.LoteID);
         
                var corralDal=new CorralDAL();
                var loteDal=new LoteDAL();

                    //corralMuerte = corralDal.ObtenerPorId(corralOrganizacion.CorralID);
                    loteMuerte = loteDal.ObtenerPorID(salidaMuerte.LoteID);
                    corralMuerte = corralDal.ObtenerPorId(salidaMuerte.CorralID);

                var entradaGanado = new EntradaGanadoTransitoInfo();
                if(loteMuerte!=null)
                {
                    entradaGanado.Lote = loteMuerte;
                }
       
                PolizaModel.Detalle = new List<PolizaDetalleModel>();
                {
                    
                        //var pesoMuertePromedio = corralOrganizacion.PesoPromedio;       
                        var detalleModel = new PolizaDetalleModel
                        {
                            CantidadCabezas = salidaMuerte.NumCabezas.ToString(CultureInfo.InvariantCulture),
                            TipoGanado = CONCEPTO,
                            PesoTotal = salidaMuerte.Kilos.ToString("N"),
                            PesoPromedio = (salidaMuerte.Kilos/salidaMuerte.NumCabezas).ToString("N"),
                            //PrecioPromedio = Math.Abs(salidaMuerte.Importe / salidaMuerte.NumCabezas).ToString("N", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                            //PrecioPromedio = Math.Abs(salidaMuerte.Importe / corralOrganizacion.PesoPromedio/salidaMuerte.NumCabezas).ToString("N2", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                            PrecioPromedio = (salidaMuerte.Importe / (salidaMuerte.Kilos / salidaMuerte.NumCabezas) / salidaMuerte.NumCabezas).ToString("N2", CultureInfo.CurrentCulture).Replace("$", string.Empty),
                            //precio promedio por kilo=costo total/Peso total
                            //precio promedio kilo*peso total*num cabezas=importe
                            ImportePromedio = (salidaMuerte.Importe).ToString("N2", CultureInfo.CurrentCulture),//estaba en :"N2"
                            Corral = corralMuerte.Codigo,
                            Lote = entradaGanado.Lote.Lote
                        };
                        PolizaModel.Detalle.Add(detalleModel);
                    
                }
                polizaImpresion.GenerarDetalles("Detalle");
            }
             catch (Exception ex)
             {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }

        }

        /// <summary>
        /// Genera la linea de descripcion del detalle de la poliza
        /// </summary>
        private void GeneraLineaEncabezadoDetalle()
        {
            try
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
             catch (Exception ex)
             {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }
        }

        /// <summary>
        /// Separa los nodos de las polizas en cargos y abonos
        /// </summary>
        /// <param name="polizas">nodos de la poliza que se separaran como registros</param>
        /// <param name="cargos">cargos encontrados en los nodos de la poliza</param>
        /// <param name="abonos">abonos encontrados en los nodos de la poliza</param>
        protected override void GeneraLineaRegistroContable(IList<PolizaInfo> polizas, out IList<PolizaInfo> cargos , out IList<PolizaInfo> abonos)
        {
            try
            {
                base.GeneraLineaRegistroContable(polizas, out cargos, out abonos);
                const int cadenaLarga = 50;

                //obtiene todos los costos activos:
                var costosActivos = ObtenerCostos();

                if (costosActivos == null)
                {
                    var ex = new Exception(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostosNoEncontrados);
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
                //filtra la lista de todos los costos activos, solo conservando los costos que se incluyen en la poliza
                var costoLista= (from costo in costosActivos let tempCosto = detalles.FirstOrDefault(detalle => detalle.CostoId == costo.CostoID) 
                                 where tempCosto != null select new CostoInfo {CostoID = tempCosto.CostoId, ClaveContable = costo.ClaveContable, Descripcion = costo.Descripcion}).ToList();
                costosActivos.Clear();
                costosActivos = costoLista;
                //

                PolizaModel.RegistroContable = new List<PolizaRegistroContableModel>();
                PolizaRegistroContableModel registroContable;
                foreach (var cargo in cargos)
                {
                    if (cargo.Descripcion == null)
                    {
                        //al nodo de la poliza le encuentra la descripcion del costo usando la clave contable del mismo:
                        var descripcionCosto =
                            costosActivos.FirstOrDefault(
                                costo =>
                                    cargo.Cuenta.EndsWith(costo.ClaveContable));
                        //en caso de no encontrarse, no se asigna descripcion, en caso de encontrarse se asigna la descripcion del costo
                        cargo.Descripcion = descripcionCosto == null ? string.Empty : descripcionCosto.Descripcion;
                    }

                    //si la descripcion es demasiado larga, se solo muestra la cantidad de letras que cabe en la linea
                    var sbDescripcion = new StringBuilder();
                    sbDescripcion.Append(cargo.Descripcion.Length > cadenaLarga
                        ? cargo.Descripcion.Substring(0, cadenaLarga - 1).Trim()
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
                        Cargo =
                            Convert.ToDecimal(cargo.Importe.Replace("-", string.Empty)).ToString(
                                "N", CultureInfo.InvariantCulture)
                    };
                    PolizaModel.RegistroContable.Add(registroContable);
                }

                var cuentaSapBL = new CuentaSAPBL();
                var cuentaSap = cuentaSapBL.ObtenerTodos();
           
                foreach (var abono in abonos)
                {
                    var cuenta = cuentaSap.FirstOrDefault(x => x.CuentaSAP == abono.Cuenta);

                    if (cuenta == null)
                    {
                        cuenta = new CuentaSAPInfo();
                    }
                    //al nodo de la poliza le encuentra la descripcion del costo usando la clave contable del mismo:
                        
                    var sbDescripcion = new StringBuilder();
                    sbDescripcion.Append(cuenta.Descripcion.Length > cadenaLarga
                        ? cuenta.Descripcion.Substring(0, cadenaLarga - 1).Trim()
                        : cuenta.Descripcion.Trim());
                    registroContable = new PolizaRegistroContableModel
                    {
                        Cuenta =
                            (string.IsNullOrWhiteSpace(abono.Proveedor)
                                ? abono.Cuenta
                                : abono.Proveedor),
                        Descripcion = sbDescripcion.ToString(),
                        Concepto = string.Empty,
                        Abono =
                            Convert.ToDecimal(abono.Importe.Replace("-", string.Empty)).ToString(
                                "N", CultureInfo.CurrentCulture)
                    };
                    PolizaModel.RegistroContable.Add(registroContable);
                }
            }
             catch (Exception ex)
             {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }
        }

        /// <summary>
        /// Genera las lineas de costos correspondientes a la poliza generada
        /// </summary>
        /// <param name="listaCostos">Lista de costos relacionados a la poliza</param>
        /// <param name="observacion">Observaciones de la salida de ganado en transito</param>
        private void GeneraLineaCostos(List<SalidaGanadoEnTransitoDetalleInfo> listaCostos, string observacion)
        {
            try
            {
                //var observaciones = new HashSet<string>();
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
                            //observaciones.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                    }
                }
                //const int numeroLinea = 0;
                //var observacionesImpresas = new List<string>(observaciones);

                var costosAgrupados = listaCostos;

                if (costos == null)
                {
                    costos = ObtenerCostos();
                }

                PolizaModel.Costos = new List<PolizaCostoModel>();
                PolizaCostoModel costoModel;
                foreach (var costo in costosAgrupados)
                {
                    //if (!observacionesImpresas.Any())
                    //{
                    //    observacionesImpresas.Add(string.Empty);
                    //}
                    var costoDescripcion =
                        costos.Where(clave => clave.CostoID == costo.CostoId ).Select(desc => desc.Descripcion).FirstOrDefault();
                    if (costoDescripcion != null)
                    {

                        costoModel = new PolizaCostoModel
                        {
                            Descripcion = costoDescripcion,
                            Parcial = costo.ImporteCosto.ToString("N", CultureInfo.CurrentCulture),
                            Total = costo.ImporteCosto.ToString("N", CultureInfo.CurrentCulture),
                            //Observaciones = observacionesImpresas[numeroLinea].Trim()
                        };
                        PolizaModel.Costos.Add(costoModel);

                    }
                    else
                    {
                        var ex = new Exception(string.Format(Properties.ResourceServices.PolizaSalidaMuerteGanadoTransito_CostoIdNoEncontrado,costo.CostoId));
                        Logger.Error(ex);
                        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
                    }
                    //if (!string.IsNullOrWhiteSpace(observacionesImpresas[numeroLinea]))
                    //{
                    //    observacionesImpresas.RemoveAt(numeroLinea);
                    //}
                }
                //if (observacionesImpresas.Any()
                //    && !string.IsNullOrWhiteSpace(observacionesImpresas[numeroLinea]))
                //{
                //    foreach (var obs in observacionesImpresas)
                //    {
                //        costoModel = new PolizaCostoModel
                //        {
                //            Observaciones = obs
                //        };
                //        PolizaModel.Costos.Add(costoModel);
                //    }
                //}
             }
             catch (Exception ex)
                {
                    Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
                }
        }

        /// <summary>
        /// Genera la linea de total de costos 
        /// </summary>
        /// <param name="Costos"></param>
        private void GeneraLineaTotalCostos(List<SalidaGanadoEnTransitoDetalleInfo> Costos)
        {
            try
            {
                var totalCosto = Costos.Sum(imp => imp.ImporteCosto);
                GenerarLineaTotalCosto(totalCosto, true);

                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
                polizaImpresion.GenerarLineaEnBlanco("Costos", 5);
                polizaImpresion.GenerarLineaEnBlanco("Costos", 5);

                GenerarLineaElaboro();
                polizaImpresion.GeneraCabecero(new[] { "30", "20", "20", "5", "40" }, "Costos");
            }
             catch (Exception ex)
              {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
              }
        }

        #endregion

        #region XML

        /// <summary>
        /// consulta los datos faltantes para la generacion de la poliza de salida por muerte en transito
        /// </summary>
        /// <param name="input">Salida de ganado en transito al que le buscaran los datos faltantes para la generacion de la poliza</param>
        /// <returns>Regresa los datos faltantes para la poliza de salida de ganado en transito por muerte</returns>
        DatosPolizaSalidaGanadoTransitoInfo ObtenerDatosPolizaSalidaPorMuerte(SalidaGanadoEnTransitoInfo input)
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
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera un nodo generico de una salida de ganado en transito por muerte
        /// </summary>
        /// <param name="input">Salida de ganado en transito al cual se le generara el nodo para la poliza</param>
        /// <param name="noLinea">Numero de linea correspondiente al nodo</param>
        /// <param name="ref3"></param>
        /// <param name="archivoFolio">Folio del archivo de la poliza de salida de ganado en transito</param>
        /// <returns>Regresa un nodo de poliza de salida de ganado en transito parcialmente inicializado</returns>
        PolizaInfo GenerarNodoPlantillaPolizaMuerteTransito(SalidaGanadoEnTransitoInfo input,int noLinea,string ref3,string archivoFolio)
        {
            try
            {
                var result = new PolizaInfo
                {
                    OrganizacionID = input.OrganizacionID,
                    UsuarioCreacionID = input.UsuarioCreacionID,
                    NumeroReferencia = input.Folio.ToString(CultureInfo.InvariantCulture),
                    FechaCreacion = DateTime.Now,
                    FechaDocumento = input.Fecha.ToString("yyyyMMdd"),
                    FechaContabilidad = input.Fecha.ToString("yyyyMMdd"),
                    ClaseDocumento = datos.PostFijoRef3,
                    Sociedad = datos.Sociedad,
                    Moneda = MONEDA,
                    TipoCambio = TIPOCAMBIO,
                    TextoDocumento = datos.TipoPolizaDescripcion,
                    Mes = input.FechaCreacion.Month.ToString(CultureInfo.InvariantCulture),
                    TextoAsignado = input.Folio.ToString(CultureInfo.InvariantCulture),
                    Concepto = string.Format("MT-{0} {1} CABEZAS {2} kgs", input.Folio, input.NumCabezas, input.Kilos),
                    Division = datos.Division,
                    BusAct = BUSACT,
                    Periodo = input.FechaCreacion.Year.ToString(CultureInfo.InvariantCulture),
                    NumeroLinea = noLinea.ToString(CultureInfo.InvariantCulture),
                    Referencia1 = input.NumCabezas.ToString(CultureInfo.InvariantCulture),
                    Referencia3 = ref3,
                    FechaImpuesto = input.Fecha.ToString("yyyyMMdd"),
                    ImpuestoRetencion = 0,
                    ImpuestoIva = 0.ToString(CultureInfo.InvariantCulture),
                    ArchivoFolio = archivoFolio,
                    Segmento = "S" + datos.Sociedad
                };

                return result;
            }
             catch (Exception ex)
             {
                 Logger.Error(ex);
                 throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
             }
        }

        #endregion

        #endregion   
    }

}
