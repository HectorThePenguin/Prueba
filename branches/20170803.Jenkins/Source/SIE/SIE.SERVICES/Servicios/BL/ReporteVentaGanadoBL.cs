using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using System;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;
using SIE.Services.Info.Enums;

namespace SIE.Services.Servicios.BL
{
    public class ReporteVentaGanadoBL
    {
        /// <summary>
        /// Obtiene una lista con los datos del reporte
        /// de venta de ganado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<ReporteVentaGanado> GenerarReporteVentaGanado(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteVentaGanado> lista = null;
            try
            {
                Logger.Info();
                var reporteVentaGanadoDAL = new ReporteVentaGanadoDAL();
                ReporteVentaGanadoDatos datosReporte = reporteVentaGanadoDAL.GenerarReporteVentaGanado(organizacionID,
                                                                                                       fechaInicial,
                                                                                                       fechaFinal);
                if (datosReporte != null)
                {
                    lista = GenerarReporte(datosReporte);
                    lista = lista.Where(tipo => !string.IsNullOrWhiteSpace(tipo.TipoGanado)).ToList();
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
        /// Obtiene los datos que seran
        /// mostrados en el reporte
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        private List<ReporteVentaGanado> GenerarReporte(ReporteVentaGanadoDatos datosReporte)
        {
            var resultado = new List<ReporteVentaGanado>();

            var foliosVentaPeso = datosReporte.Animales
                .GroupBy(grp => grp.FolioTicket)
                .Select(dato => new
                                    {
                                        FolioTicket = dato.Key,
                                        PesoAnimal =
                                    Convert.ToInt32(Math.Round(
                                        Convert.ToDecimal(dato.Select(peso => peso.Peso).FirstOrDefault()/dato.Count()),
                                        0, MidpointRounding.AwayFromZero))
                                    }).ToList();

            datosReporte.MovimientosSalida.ForEach(salida =>
                                                       {
                                                           var repoteVenta = new ReporteVentaGanado
                                                                                 {
                                                                                     Enfermeria = salida.Enfermeria,
                                                                                     LugarDestino = salida.Corral,
                                                                                     Fecha = salida.FechaMovimiento.ToShortDateString()
                                                                                 };
                                                           repoteVenta.Codigo =
                                                               datosReporte.MovimientosProduccion.Where(
                                                                   condicion => condicion.AnimalID == salida.AnimalID).
                                                                   Select(destino => destino.Corral).FirstOrDefault();

                                                           ReporteVentaGanadoAnimal animal =
                                                               datosReporte.Animales.FirstOrDefault(condicion => condicion.AnimalID == salida.AnimalID);
                                                           if (animal != null)
                                                           {
                                                               repoteVenta.TipoGanado = animal.TipoGanado;
                                                               repoteVenta.Arete = animal.Arete;
                                                               repoteVenta.Sexo = animal.Sexo;
                                                               repoteVenta.Causa = animal.CausaSalida;
                                                               repoteVenta.Partida = animal.FolioEntrada;
                                                               repoteVenta.Peso =
                                                                   foliosVentaPeso.Where(
                                                                       folioTicket =>
                                                                       folioTicket.FolioTicket == animal.FolioTicket)
                                                                       .Select(peso => peso.PesoAnimal).FirstOrDefault();

                                                               ReporteVentaGanadoFolio folio =
                                                                   datosReporte.Folios.FirstOrDefault(
                                                                       condicion =>
                                                                       condicion.FolioEntrada == animal.FolioEntrada
                                                                       && condicion.AnimalID == animal.AnimalID);
                                                               if (folio != null)
                                                               {
                                                                   repoteVenta.FechaLlegada =
                                                                       folio.FechaEntrada.ToShortDateString();
                                                                   TimeSpan diasEngorda = salida.FechaMovimiento -
                                                                                          folio.FechaEntrada;
                                                                   repoteVenta.DiasEngorda = diasEngorda.Days;
                                                                   var tipoOrganizacion =
                                                                       (TipoOrganizacion) folio.TipoOrganizacionID;
                                                                   switch (tipoOrganizacion)
                                                                   {
                                                                       case TipoOrganizacion.CompraDirecta:
                                                                           repoteVenta.Origen = folio.Proveedor;
                                                                           break;
                                                                       case TipoOrganizacion.Centro:
                                                                       case TipoOrganizacion.Praderas:
                                                                       case TipoOrganizacion.Descanso:
                                                                       case TipoOrganizacion.Cadis:
                                                                           repoteVenta.Origen = folio.Organizacion;
                                                                           break;
                                                                   }
                                                               }
                                                               List<ReporteVentaGanadoTratamiento> tratamientos =
                                                                   datosReporte.Tratamientos.Where(
                                                                       condicion =>
                                                                       condicion.AnimalID == salida.AnimalID).
                                                                       Select(
                                                                           tratamiento => tratamiento)
                                                                       .ToList();
                                                               if (tratamientos.Any())
                                                               {
                                                                   AsignarTratamientos(tratamientos, repoteVenta);
                                                               }
                                                           }
                                                           repoteVenta.LugarDestino = salida.Corral;                                                           

                                                           resultado.Add(repoteVenta);
                                                       });
            if (resultado.Any())
            {
                resultado = resultado.OrderBy(registro => registro.Fecha).ToList();
            }
            return resultado;
        }

        /// <summary>
        /// Asigna los tratamientos al animal
        /// </summary>
        /// <param name="tratamientos"></param>
        /// <param name="reporteVentaGanado"></param>
        private void AsignarTratamientos(List<ReporteVentaGanadoTratamiento> tratamientos, ReporteVentaGanado reporteVentaGanado)
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
                    if (string.IsNullOrWhiteSpace(reporteVentaGanado.PrimerMedicamento))
                    {
                        reporteVentaGanado.PrimerMedicamento =
                            string.Join(",", aplicaciones.ToArray());
                        reporteVentaGanado.FechaPrimerMedicamento = fechasTratamientos[indexFechas].ToShortDateString();
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(reporteVentaGanado.SegundoMedicamento))
                        {
                            reporteVentaGanado.SegundoMedicamento =
                                string.Join(",", aplicaciones.ToArray());
                            reporteVentaGanado.FechaSegundoMedicamento =
                                fechasTratamientos[indexFechas].ToShortDateString();
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(reporteVentaGanado.TercerMedicamento))
                            {
                                reporteVentaGanado.TercerMedicamento =
                                    string.Join(",", aplicaciones.ToArray());
                                reporteVentaGanado.FechaTercerMedicamento =
                                    fechasTratamientos[indexFechas].ToShortDateString();
                            }
                        }
                    }
                }
            }
        }
    }
}
