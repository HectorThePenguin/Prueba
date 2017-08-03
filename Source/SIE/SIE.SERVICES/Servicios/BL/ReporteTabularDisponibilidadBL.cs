using System;
using System.Collections.Generic;
using System.Reflection;
using BLToolkit.TypeBuilder;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteTabularDisponibilidadBL
    {
        /// <summary>
        /// Regresa el reporte de disponibilidad
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal List<ReporteTabularDisponibilidadSemanaInfo> GenerarReporteTabularDisponibilidad(int organizacionId, DateTime fecha)
        {
            List<ReporteTabularDisponibilidadSemanaInfo> lista = new List<ReporteTabularDisponibilidadSemanaInfo>();
            try
            {
                Logger.Info();
                var reporteDal = new ReporteTabularDisponibilidadDAL();
                var listaCorrales = reporteDal.ObtenerReporteTabularDisponibilidad(organizacionId, TipoCorral.Produccion, fecha);
                if (listaCorrales != null && listaCorrales.Count > 0)
                {
                    //lista = new List<ReporteTabularDisponibilidadSemanaInfo>();
                    List<int> semanas = listaCorrales.AsEnumerable().Select(e => e.Semana).Distinct().ToList();

                    var semanasAuxiliar = new List<int>();

                    for (int i = 0; i < semanas.Count; i++)
                    {
                       semanasAuxiliar.Add(i);
                    }
                    semanas = semanasAuxiliar;

                    int numeroSemana = 0;
                    var ultimaFecha = new DateTime();
                    foreach (var semana in semanas.OrderBy(sem => sem))
                    {
                        List<ReporteTabularDisponibilidadInfo> listaCorralesSemana;

                        listaCorralesSemana = listaCorrales.Where(corral => corral.Semana == semana).ToList();


                        if (!listaCorralesSemana.Any())
                        {
                            listaCorralesSemana = new List<ReporteTabularDisponibilidadInfo>
                                                      {
                                                          new ReporteTabularDisponibilidadInfo
                                                              {
                                                                  Codigo = string.Empty,
                                                                  LoteID = 0,
                                                                  Cabezas = 0,
                                                                  Descripcion = string.Empty,
                                                                  FechaCierre = DateTime.MinValue,
                                                                  FechaDisponibilidadProyectada = DateTime.MinValue,
                                                                  DisponibilidadManual = 0,
                                                                  PesoTotalLote = 0,
                                                                  PesoPromedio = 0,
                                                                  Sexo = string.Empty,
                                                                  FormulaIDServida = string.Empty,
                                                                  Semana = numeroSemana,
                                                                  FechaInicioSemana = ultimaFecha.AddDays(7)
                                                              }
                                                      };
                        }

                        var valorSemana = new ReporteTabularDisponibilidadSemanaInfo();

                        valorSemana.Corrales = listaCorralesSemana;
                        valorSemana.NumeroSemana = numeroSemana++;
                        valorSemana.SemanaAnio = semana;
                        valorSemana.TotalCabezas = listaCorralesSemana.Sum(valor => valor.Cabezas);
                        valorSemana.FechaInicioSemana = listaCorralesSemana[0] != null ? listaCorralesSemana[0].FechaInicioSemana : new DateTime(1900, 1, 1);
                        ultimaFecha = valorSemana.FechaInicioSemana;
                        lista.Add(valorSemana);
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

    }
}
