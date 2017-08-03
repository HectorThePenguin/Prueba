using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteOperacionSanidadBL
    {
        internal List<ReporteOperacionSanidadInfo> GenerarReporteOperacionSanidad(int organizacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            var reporteOperacionSanidadDAL = new ReporteOperacionSanidadDAL();
            List<ContenedorReporteOperacionSanidadInfo> listaConceptos =
                reporteOperacionSanidadDAL.ObtenerConceptosOperacionSanidad(organizacionID, fechaInicio,
                                                                            fechaFin);

            if(listaConceptos == null)
            {
                return null;
            }

            ContenedorReporteOperacionSanidadInfo contenedorReporteOperacionSanidadInfo = listaConceptos.FirstOrDefault();
            int totalEntradas = 0;
            int totalMedicados = 0;
            if (contenedorReporteOperacionSanidadInfo != null)
            {
                totalEntradas = contenedorReporteOperacionSanidadInfo.TotalEntradas;
                totalMedicados = contenedorReporteOperacionSanidadInfo.TotalMedicados;
            }

            List<ReporteOperacionSanidadInfo> reporte = (from sanidad in listaConceptos
                                                         group sanidad by sanidad.Concepto into sanidadAgrupada
                                                         let dias13 = ObtenerConteoDias(1, 3, sanidadAgrupada)
                                                         let dias410 = ObtenerConteoDias(4, 10, sanidadAgrupada)
                                                         let diasMas10 = ObtenerConteoDias(11, 999, sanidadAgrupada)
                                                         let diasMas30 = ObtenerConteoDias(30, 999, sanidadAgrupada)
                                                         select new ReporteOperacionSanidadInfo
                                                             {
                                                                 Concepto = sanidadAgrupada.Key,
                                                                 Dias13 = dias13,
                                                                 PorcentajeDias13 = ObtenerPorcentaje(dias13,totalEntradas,totalMedicados,sanidadAgrupada.Key),
                                                                 Dias410 = dias410,
                                                                 PorcentajeDias410 = ObtenerPorcentaje(dias410, totalEntradas, totalMedicados, sanidadAgrupada.Key),
                                                                 DiasMas10 = sanidadAgrupada.Key.Equals(ConstantesBL.Cronicos) ? 0 : diasMas10,
                                                                 PorcentajeDiasMas10 = ObtenerPorcentaje(diasMas10, totalEntradas, totalMedicados, sanidadAgrupada.Key),
                                                                 TotalPeriodo = sanidadAgrupada.Key.Equals(ConstantesBL.Cronicos) ? diasMas30 : dias13 + dias410 + diasMas10
                                                             }).ToList();

            
            if (reporte.Sum(ceros => ceros.TotalPeriodo) == 0)
            {
                return null;
            }
            return reporte;
        }
        /// <summary>
        /// Obtiene el porcentaje del concepto
        /// </summary>
        /// <param name="dias"></param>
        /// <param name="totalEntradas"></param>
        /// <param name="totalMedicados"></param>
        /// <param name="tipoOperacion"></param>
        /// <returns></returns>
        private decimal ObtenerPorcentaje(int dias, int totalEntradas, int totalMedicados, string tipoOperacion)
        {
            decimal porcentaje = decimal.Zero;
            var operacion = Regex.Replace(tipoOperacion, @"[^a-zA-z0-9 ]+", "");
            var opCronicos = Regex.Replace(ConstantesBL.Cronicos, @"[^a-zA-z0-9 ]+", "");
            var opCronicosRec = Regex.Replace(ConstantesBL.CronicosEnRecuperacion, @"[^a-zA-z0-9 ]+", "");
            var opCronicosVoM = Regex.Replace(ConstantesBL.CronicoVentaMuerte, @"[^a-zA-z0-9 ]+", "");

            porcentaje = Math.Round(((decimal) dias/totalEntradas)*100, 2);
            
            if(operacion.Equals(ConstantesBL.Recaidos))
            {
                porcentaje = Math.Round(((decimal)dias / totalMedicados) * 100, 2);
            }
            if (opCronicos.Contains(operacion) ||
                opCronicosRec.Contains(operacion) ||
                opCronicosVoM.Contains(operacion))
            {
                porcentaje = decimal.Zero;
            }
            return porcentaje;
        }

        private static int ObtenerConteoDias(int inicio, int fin, IEnumerable<ContenedorReporteOperacionSanidadInfo> agrupado)
        {
            return agrupado.Count(sanidad => sanidad.DiferenciaDias >= inicio && sanidad.DiferenciaDias <= fin);
        }

    }
}
