using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteConsumoCorralDAL
    {
        internal static void MetodoBase(Action accion)
        {
            try
            {
                Logger.Info();
                accion();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static void Generar(DataSet ds, Info.Modelos.AlimentacionConsumoCorralReporte modelo)
        {
            //ds.WriteXml("C:\\Reportes\\ConsumoCorral.xml", XmlWriteMode.WriteSchema);
            MetodoBase(() =>
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtEncabezado];
                DataRow2Encabezado(dt, modelo);

                dt = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtFormulas = ds.Tables[ConstantesDAL.DtFormulas];
                modelo.Detalle = new System.Collections.ObjectModel.ObservableCollection<Info.Modelos.AlimentacionConsumoCorralDetalle>(DataRow2Detalle(dt, dtFormulas));

                DataTable dtMovimientos = ds.Tables[ConstantesDAL.DtMovimientosTraspaso];
                modelo.Totales = new System.Collections.ObjectModel.ObservableCollection<Info.Modelos.AlimentacionConsumoCorralTotal>(DataRow2Totales(modelo.Detalle, dtMovimientos));
            });
        }

        private static IEnumerable<Info.Modelos.AlimentacionConsumoCorralTotal> DataRow2Totales(IEnumerable<Info.Modelos.AlimentacionConsumoCorralDetalle> detalle, DataTable dtMovimientos)
        {
            var detallesAlimentacionConsumoTotal = new List<AlimentacionConsumoCorralTotal>();
            if (detalle != null && detalle.Any())
            {
                var grupoMovimiento =
                    dtMovimientos.AsEnumerable().GroupBy(
                        formula => formula.Field<int>("FormulaIDServida")).Select(
                            totales =>
                            new
                                {
                                    FormulaId = totales.Key,
                                    TotalDiasTransferidos = totales.Sum(dias => dias.Field<int>("DIAS_ANIMAL_TRANSFERIDOS")),
                                    Cabezas = totales.Sum(cabezas => cabezas.Field<int>("Cabezas")),
                                    Peso = totales.Sum(kilos => kilos.Field<decimal>("Peso"))
                                });
                var grupoDetalle = detalle.GroupBy(
                    formula => new { formula.FormulaId, formula.Formula, formula.RepartoID }).Select
                    (
                        detalles => new Info.Modelos.AlimentacionConsumoCorralTotal
                            {
                                FormulaId = detalles.Key.FormulaId,
                                TotalKilos = detalles.Sum(cantidad => cantidad.KilosDia),
                                TotalDiasAcumulado = detalles.Sum(dias => dias.DiasAnimal),
                                //TotalCosto = Convert.ToInt32(detalles.Sum(costo => costo.Importe)),
                                TotalCosto = detalles.Sum(costo => costo.Importe),
                                Formula = detalles.Key.Formula,
                                RepartoID = detalles.Key.RepartoID
                            }
                    );
                int? totalDiasAcomulado = grupoDetalle.Sum(dias => dias.TotalDiasAcumulado);
                int totalDiasAcomuladoTransferido = grupoMovimiento.Sum(dias => dias.TotalDiasTransferidos == 0 ? 0 : dias.TotalDiasTransferidos);
                int? totalKilos = Convert.ToInt32(grupoDetalle.Sum(kilos => kilos.TotalKilos));
                decimal? totalCosto = grupoDetalle.Sum(costo => costo.TotalCosto);
                detallesAlimentacionConsumoTotal = (from det in grupoDetalle
                                                    join mov in grupoMovimiento on det.FormulaId equals mov.FormulaId
                                                        into leftJoin
                                                    from relacion in leftJoin.DefaultIfEmpty()
                                                    select new AlimentacionConsumoCorralTotal
                                                    {
                                                        KilosTrans = det.RepartoID == 0 ? det.TotalKilos : null, //Convert.ToInt32((relacion == null ? 0 : relacion.Peso / relacion.Cabezas)),
                                                        CostoTrans = det.RepartoID == 0 ? det.TotalCosto : 0,
                                                        TotalDiasAcumulado = det.TotalDiasAcumulado,
                                                        TotalDiasAcumuladoTransferidos = det.TotalDiasAcumuladoTransferidos,
                                                        TotalKilos = det.RepartoID == 0 ? 0 : det.TotalKilos,
                                                        TotalCosto = det.TotalCosto,
                                                        Formula = det.Formula,
                                                        SumatoriaDiasAcumulados = totalDiasAcomulado,
                                                        SumatoriaDiasAcumuladosTransferidos = totalDiasAcomuladoTransferido,
                                                        SumatoriaKilos = totalKilos,
                                                        SumatoriaCosto = totalCosto
                                                    }).ToList();
            }
            return detallesAlimentacionConsumoTotal;
        }

        private static IEnumerable<Info.Modelos.AlimentacionConsumoCorralDetalle> DataRow2Detalle(DataTable dtDetalle, DataTable dtFormulas)
        {
            var detallesAlimentacionConsumoCorral = new List<AlimentacionConsumoCorralDetalle>();
            if (dtDetalle != null && dtDetalle.Rows.Count > 0)
            {
                decimal cantidadServida = 0;
                int CabezasSumaAcumulados = 0;
                detallesAlimentacionConsumoCorral = (from detalle in dtDetalle.AsEnumerable()
                                                     join formulas in dtFormulas.AsEnumerable() on
                                                         detalle.Field<int>("FormulaIDServida") equals
                                                         formulas.Field<int>("FormulaID")
                                                     let formula = formulas.Field<string>("Descripcion")
                                                     let formulaID = formulas.Field<int>("FormulaID")
                                                     let servicioAcomulado = (cantidadServida += Math.Round(detalle.Field<decimal>("CantidadServida")))
                                                     let cabezasAcumulados = (CabezasSumaAcumulados += detalle.Field<int>("Cabezas"))
                                                     select new Info.Modelos.AlimentacionConsumoCorralDetalle
                                                                {
                                                                    Cabezas = detalle.Field<int>("Cabezas"),
                                                                    ConsumoDia = detalle.Field<decimal>("CONSUMO_DIA"),
                                                                    DiasAnimal = detalle.Field<int>("DIAS_ANIMAL"),
                                                                    Fecha = detalle.Field<DateTime>("Fecha").ToShortDateString(),
                                                                    Formula = formula,
                                                                    FormulaId = formulaID,
                                                                    Importe = detalle.Field<decimal>("Importe"),
                                                                    KilosDia =Convert.ToInt32(detalle.Field<decimal>("CantidadServida")),
                                                                    Precio = detalle.Field<decimal>("PrecioPromedio"),
                                                                    ServidosAcomulados =Convert.ToInt32(servicioAcomulado),
                                                                    RepartoID = detalle.Field<long>("RepartoID"),
                                                                    CabezasAcumulados = Convert.ToInt32(cabezasAcumulados)
                                                                }).ToList();
                dynamic promedios = (from detalle in dtDetalle.AsEnumerable()
                                     select new
                                                {
                                                    Fecha = detalle.Field<DateTime>("Fecha"),
                                                    ConsumoPorDia = detalle.Field<decimal>("CantidadServida"),
                                                    FormulaID = detalle.Field<int>("FormulaIDServida")
                                                }
                                    ).OrderByDescending(fecha => fecha.Fecha).ToList();
                //ObtenerPromediosAcomulados(promedios, detallesAlimentacionConsumoCorral);
            }
            return detallesAlimentacionConsumoCorral;
        }

        private static void ObtenerPromediosAcomulados(dynamic promedios, List<AlimentacionConsumoCorralDetalle> detallesAlimentacionConsumoCorral)
        {
            /*var consumos = new List<dynamic>();            
            ObtenerConsumos(promedios, consumos);
            detallesAlimentacionConsumoCorral.ForEach(detalle =>
                                                          {
                                                              decimal promedio =
                                                                  consumos.Where(
                                                                      formula =>
                                                                      formula.FormulaID == detalle.FormulaId).
                                                                      Select(prom => prom.PromedioAcomulado).
                                                                      FirstOrDefault();

                                                              detalle.PromedioAcomulado = promedio;
                                                          });*/
        }

        private static void ObtenerConsumos(dynamic promedios, List<dynamic> consumos)
        {
            decimal promedioAcomulado = 0;
            for (int indexPromedios = 0; indexPromedios < promedios.Count; indexPromedios++)
            {
                dynamic promedioSiguiente;
                try
                {
                    promedioSiguiente = promedios[indexPromedios + 1];
                }
                catch (Exception)
                {
                    promedioSiguiente = null;
                }
                if (promedioSiguiente != null)
                {
                    if (promedios[indexPromedios].FormulaID == promedioSiguiente.FormulaID
                        && promedioSiguiente.Fecha <= promedios[indexPromedios].Fecha)
                    {
                        promedioAcomulado += promedios[indexPromedios].ConsumoPorDia;
                    }
                    else
                    {
                        consumos.Add(
                            new
                                {
                                    promedios[indexPromedios].FormulaID,
                                    PromedioAcomulado =
                                (promedios[indexPromedios].ConsumoPorDia + promedioAcomulado) / 2
                                });
                        promedioAcomulado = 0;
                    }
                }
                else
                {
                    consumos.Add(
                        new
                            {
                                promedios[indexPromedios].FormulaID,
                                PromedioAcomulado = (promedioAcomulado + promedios[indexPromedios].ConsumoPorDia) / 2
                            });
                }
            }
        }

        private static void DataRow2Encabezado(DataTable dt, Info.Modelos.AlimentacionConsumoCorralReporte modelo)
        {
            var entidad = (from datos in dt.AsEnumerable()
                           select new Info.Modelos.AlimentacionConsumoCorralReporte
                                      {
                                          Corral = datos.Field<string>("Corral"),
                                          Proveedor = datos.Field<string>("Encabezado"),
                                          TipoGanado = datos.Field<string>("TipoGanado"),
                                          Proceso = datos.Field<string>("TipoProceso")
                                      }).FirstOrDefault();
            if (entidad != null)
            {
                modelo.Corral = entidad.Corral;
                modelo.Proceso = entidad.Proceso;
                modelo.Proveedor = entidad.Proveedor;
                modelo.TipoGanado = entidad.TipoGanado;
            }            
        }
    }
}
