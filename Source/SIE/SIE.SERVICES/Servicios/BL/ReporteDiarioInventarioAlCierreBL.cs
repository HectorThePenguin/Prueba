using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Data;

namespace SIE.Services.Servicios.BL
{
    public class ReporteDiarioInventarioAlCierreBL
    {

        public List<ReporteDiarioInventarioAlCierreInfo> GenerarReporte(int organizacionID, DateTime fecha)
        {
            List<ReporteDiarioInventarioAlCierreInfo> lista = null;

            try
            {
                Logger.Info();
                var reportediarioinventariocierreDAL = new ReporteDiarioInventarioAlCierreDAL();
                ReporteDiarioInventarioAlCierreDatos datosReporte = reportediarioinventariocierreDAL.GenerarReporteDiarioInventarioAlCierre(organizacionID, fecha);

                if (datosReporte != null)
                {
                    lista = GenerarReporte(datosReporte);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }


        private List<ReporteDiarioInventarioAlCierreInfo> GenerarReporte(ReporteDiarioInventarioAlCierreDatos datosReporte)
        {
            var resultado = new List<ReporteDiarioInventarioAlCierreInfo>();


            datosReporte.ListaReporteInventario.ForEach(Dato =>
            {

                var reporteDiarioinventariocierreInfo = new ReporteDiarioInventarioAlCierreInfo
                {
                    Ingrediente = Dato.Ingrediente,
                    TMExisAlmacenPA = Dato.TMExisAlmacenPA,
                    TMExisAlmacenMP = Dato.TMExisAlmacenMP,
                    TMInvTotalPAyMA = Dato.TMInvTotalPAyMA,
                    TMConsumoDia = Dato.TMConsumoDia,
                    DiasCobertura = Dato.DiasCobertura,
                    CapacidadAlamacenajeDias = Dato.CapacidadAlamacenajeDias,
                    DiasCoberturaFaltante = Dato.DiasCoberturaFaltante,
                    MinimoDiasReorden = Dato.MinimoDiasReorden,
                    TMCapacidadAlmacenaje = Dato.TMCapacidadAlmacenaje,
                    EstatusReorden = Dato.EstatusReorden
                };
                resultado.Add(Dato);
            });
          
            return resultado;
        }

    }
}
