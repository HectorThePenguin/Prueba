using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteCronicosRecuperacionBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Cronicos en Recuperacion
        /// </summary>
        /// <returns> </returns>
        public List<ReporteCronicosRecuperacionInfo> GenerarReporteCronicosRecuperacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteCronicosRecuperacionInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteCronicosRecuperacionDAL = new ReporteCronicosRecuperacionDAL();
                ReporteCronicosRecuperacionDatos datosReporte = reporteCronicosRecuperacionDAL.GenerarReporteCronicosRecuperacion(organizacionID, fechaInicial,
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
        private List<ReporteCronicosRecuperacionInfo> GenerarReporte(ReporteCronicosRecuperacionDatos datosReporte)
        {
            var resultado = new List<ReporteCronicosRecuperacionInfo>();

            datosReporte.MovimientosEnfermeria.ForEach(animalEnfermo =>
            {
                var reporteCronicosRecuperacion = new ReporteCronicosRecuperacionInfo
                    {
                        Arete = animalEnfermo.Arete,
                        Causa = animalEnfermo.Causa,
                        Enfermeria =
                            animalEnfermo.
                                Enfermeria,
                        Sexo = animalEnfermo.Sexo.ToString(CultureInfo.InvariantCulture),
                        FechaGenerado = animalEnfermo.Fecha,
                        DateAlta = animalEnfermo.FechaAlta.ToString(),
                        LugarDestino = animalEnfermo.CorralEnfermeria,
                        Detector = animalEnfermo.Detector,
                        CorralOrigen = datosReporte.MovimientosProduccion.Where(
                            condicion =>
                            condicion.AnimalID == animalEnfermo.AnimalID).
                            Select(corral => corral.CorralProduccion).
                            FirstOrDefault()
                    };

                reporteCronicosRecuperacion.LugarGeneracion =
                    reporteCronicosRecuperacion.CorralOrigen;

                int mes = animalEnfermo.Fecha.Month;
                int semana = GetWeekOfYear(animalEnfermo.Fecha);

                reporteCronicosRecuperacion.Mes = mes;
                reporteCronicosRecuperacion.Semana = semana;

                List<ReporteCronicosRecuperacionTratamiento> tratamientos =
                    datosReporte.Tratamientos.Where(
                        condicion =>
                        condicion.AnimalID == animalEnfermo.AnimalID).
                        Select(
                            tratamiento => tratamiento)
                        .ToList();
                if (tratamientos.Any())
                {
                    AsignarTratamientos(tratamientos, reporteCronicosRecuperacion);
                }

                ReporteCronicosRecuperacionFolio folio =
                    datosReporte.Folios.FirstOrDefault(
                        condicion =>
                        condicion.AnimalID == animalEnfermo.AnimalID);
                if (folio != null)
                {
                    reporteCronicosRecuperacion.Partida = folio.FolioEntrada;
                    reporteCronicosRecuperacion.DiasEngorda = folio.DiasEngorda;
                    reporteCronicosRecuperacion.DateLlegada = folio.FechaLlegada.ToShortDateString();
                }

                resultado.Add(reporteCronicosRecuperacion);
            });
            return resultado;
        }

        /// <summary>
        /// Asigna los tratamientos al animal
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <param name="reporteCronicosRecuperacion"></param>
        private void AsignarTratamientos(List<ReporteCronicosRecuperacionTratamiento> tratamientos, ReporteCronicosRecuperacionInfo reporteCronicosRecuperacion)
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
                    if (string.IsNullOrWhiteSpace(reporteCronicosRecuperacion.PrimerTratamiento))
                    {
                        reporteCronicosRecuperacion.PrimerTratamiento =
                            string.Join(",", aplicaciones.ToArray());
                        reporteCronicosRecuperacion.DatePrimerTratamiento = fechasTratamientos[indexFechas].ToShortDateString();
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(reporteCronicosRecuperacion.SegundoTratamiento))
                        {
                            reporteCronicosRecuperacion.SegundoTratamiento =
                                string.Join(",", aplicaciones.ToArray());
                            reporteCronicosRecuperacion.DateSegundoTratamiento = fechasTratamientos[indexFechas].ToShortDateString();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(reporteCronicosRecuperacion.TercerTratamiento))
                            {
                                reporteCronicosRecuperacion.TercerTratamiento =
                                    string.Join(",", aplicaciones.ToArray());
                                reporteCronicosRecuperacion.DateTercerTratamiento = fechasTratamientos[indexFechas].ToShortDateString();
                            }
                        }
                    }
                }
            }
        }
        
        public static int GetWeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
