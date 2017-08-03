using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    public class ReporteVentaMuerteBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Venta Muerte
        /// </summary>
        /// <returns> </returns>
        public List<ReporteVentaMuerteInfo> GenerarReporteVentaMuerte(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteVentaMuerteInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteVentaMuerteDAL = new ReporteVentaMuerteDAL();
                ReporteVentaMuerteDatos datosReporte = reporteVentaMuerteDAL.GenerarReporteVentaMuerte(organizacionID,
                                                                                                       fechaInicial,
                                                                                                       fechaFinal);
                if (datosReporte != null)
                {
                    lista = GenerarReporte(datosReporte);
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

        /// <summary>
        /// Genera los datos del reporte
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        private List<ReporteVentaMuerteInfo> GenerarReporte(ReporteVentaMuerteDatos datosReporte)
        {
            var resultado = new List<ReporteVentaMuerteInfo>();

            datosReporte.MovimientosEnfermeria.ForEach(animalEnfermo =>
                                                           {
                                                               var reporteVentaMuerte = new ReporteVentaMuerteInfo
                                                                                            {
                                                                                                Arete = animalEnfermo.Arete,
                                                                                                Causa = animalEnfermo.Causa,
                                                                                                Enfermeria =
                                                                                                    animalEnfermo.
                                                                                                    Enfermeria,
                                                                                                Sexo = animalEnfermo.Sexo,
                                                                                                Fecha = animalEnfermo.Fecha.ToShortDateString(),
                                                                                                Detector =  animalEnfermo.Detector
                                                                                            };

                                                               reporteVentaMuerte.CorralOrigen =
                                                                   datosReporte.MovimientosProduccion.Where(
                                                                       condicion =>
                                                                       condicion.AnimalID == animalEnfermo.AnimalID).
                                                                       Select(corral => corral.CorralProduccion).
                                                                       FirstOrDefault();

                                                               reporteVentaMuerte.LugarGeneracion =
                                                                   reporteVentaMuerte.CorralOrigen;

                                                               List<ReporteVentaMuerteTratamiento> tratamientos =
                                                                   datosReporte.Tratamientos.Where(
                                                                       condicion =>
                                                                       condicion.AnimalID == animalEnfermo.AnimalID).
                                                                       Select(
                                                                           tratamiento => tratamiento)
                                                                       .ToList();
                                                               if (tratamientos.Any())
                                                               {
                                                                   AsignarTratamientos(tratamientos, reporteVentaMuerte);
                                                               }

                                                               ReporteVentaMuerteFolio folio =
                                                                   datosReporte.Folios.FirstOrDefault(
                                                                       condicion =>
                                                                       condicion.AnimalID == animalEnfermo.AnimalID);
                                                               if (folio != null)
                                                               {
                                                                   reporteVentaMuerte.Partida = folio.FolioEntrada;
                                                                   reporteVentaMuerte.OrganizacionOrigen =
                                                                       folio.Organizacion;
                                                                   reporteVentaMuerte.DiasEngorda = folio.DiasEngorda;
                                                               }

                                                               resultado.Add(reporteVentaMuerte);
                                                           });
            return resultado;
        }

        /// <summary>
        /// Asigna los tratamientos al animal
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <param name="reporteVentaMuerte"></param>
        private void AsignarTratamientos(List<ReporteVentaMuerteTratamiento> tratamientos, ReporteVentaMuerteInfo reporteVentaMuerte)
        {
            List<DateTime> fechasTratamientos =
                tratamientos.Select(fecha => fecha.FechaMovimiento).OrderBy(aplicacion => aplicacion).Distinct().ToList();
            for (var indexFechas = 0; indexFechas < fechasTratamientos.Count; indexFechas++)
            {
                List<string> aplicaciones =
                    tratamientos.Where(fecha => fecha.FechaMovimiento.Equals(fechasTratamientos[indexFechas])).Select(
                        codigos => codigos.Producto).ToList();
                if (aplicaciones.Any())
                {
                    if (string.IsNullOrWhiteSpace(reporteVentaMuerte.PrimerTratamiento))
                    {
                        reporteVentaMuerte.PrimerTratamiento =
                            string.Join(", ", aplicaciones.ToArray());
                        reporteVentaMuerte.FechaPrimerTratamiento = fechasTratamientos[indexFechas].ToShortDateString();
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(reporteVentaMuerte.SegundoTratamiento))
                        {
                            reporteVentaMuerte.SegundoTratamiento =
                                string.Join(", ", aplicaciones.ToArray());
                            reporteVentaMuerte.FechaSegundoTratamiento = fechasTratamientos[indexFechas].ToShortDateString();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(reporteVentaMuerte.TercerTratamiento))
                            {
                                reporteVentaMuerte.TercerTratamiento =
                                    string.Join(", ", aplicaciones.ToArray());
                                reporteVentaMuerte.FechaTercerTratamiento = fechasTratamientos[indexFechas].ToShortDateString();
                            }
                        }
                    }
                }
            }
        }
    }
}
