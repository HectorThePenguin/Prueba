using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteEjecutivoBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Ejecutivo
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteEjecutivoResultadoInfo> GenerarReporteEjecutivo(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteEjecutivoResultadoInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteEjecutivoDAL = new ReporteEjecutivoDAL();

                List<TipoGanadoReporteEjecutivoInfo> tipoGanadoReporteEjecutivo =
                    reporteEjecutivoDAL.ObtenerTipoGanadoReporteEjecutivo(organizacionId, fechaInicial, fechaFinal);

                List<CostosReporteEjecutivoInfo> costoReporteEjecutivo =
                    reporteEjecutivoDAL.ObtenerCostosReporteEjecutivo(organizacionId, fechaInicial, fechaFinal);

                if ((tipoGanadoReporteEjecutivo != null && tipoGanadoReporteEjecutivo.Any()) 
                        && (costoReporteEjecutivo != null && costoReporteEjecutivo.Any()))
                {
                    lista = ObtenerValoresReporteEjecutivo(tipoGanadoReporteEjecutivo, costoReporteEjecutivo);
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
        /// Genera los valores para el reporte ejecutivo
        /// </summary>
        /// <param name="tipoGanadoReporteEjecutivo"></param>
        /// <param name="costoReporteEjecutivo"> </param>
        /// <returns></returns>
        private List<ReporteEjecutivoResultadoInfo> ObtenerValoresReporteEjecutivo(List<TipoGanadoReporteEjecutivoInfo> tipoGanadoReporteEjecutivo, List<CostosReporteEjecutivoInfo> costoReporteEjecutivo)
        {
            var reporteEjecutivo = tipoGanadoReporteEjecutivo.GroupBy(grp => grp.TipoGanado)
                .Select(tipo => new ReporteEjecutivoInfo
                                    {
                                        TipoGanado = tipo.Key,
                                        NumeroCabezas = tipo.Sum(cab => cab.Cabezas),
                                        PesoPromedio = Convert.ToInt32((tipo.Sum(peso => peso.PesoOrigen)/
                                                        tipo.Sum(cab => cab.Cabezas))),
                                        CabezasMachos =
                                            tipo.Where(sex => sex.Sexo == Sexo.Macho).Sum(cab => cab.Cabezas),
                                        CabezasHembras =
                                            tipo.Where(sex => sex.Sexo == Sexo.Hembra).Sum(cab => cab.Cabezas),
                                        TotalCostoIntegrado = tipo.Sum(cab => cab.CostoIntegrado),
                                       
                                    }).ToList();

            int cabezasMachos = reporteEjecutivo.Sum(cabMacho => cabMacho.CabezasMachos);
            int cabezasHembras = reporteEjecutivo.Sum(cabMacho => cabMacho.CabezasHembras);
            int totalCabezas = reporteEjecutivo.Sum(cab => cab.CabezasHembras + cab.CabezasMachos);

            reporteEjecutivo.ForEach(totales =>
                                         {
                                             decimal pesoOrigen =
                                                 tipoGanadoReporteEjecutivo.Where(
                                                     tipo => tipo.TipoGanado.Equals(totales.TipoGanado)).Sum(
                                                         total => total.PesoOrigen);

                                             if (totales.CabezasMachos > 0)
                                             {
                                                 totales.PesoPromedioMachos = pesoOrigen/totales.CabezasMachos;
                                             }
                                             else
                                             {
                                                 totales.PesoPromedioHembras = pesoOrigen/totales.CabezasHembras;
                                             }

                                             decimal precioKilosTipoGanado = tipoGanadoReporteEjecutivo.Where(
                                                 tipo => tipo.TipoGanado.Equals(totales.TipoGanado)).Sum(
                                                     compra => compra.PrecioKilo);

                                             int cantidadTipo = 
                                                 tipoGanadoReporteEjecutivo.Count(
                                                     tipo => tipo.TipoGanado.Equals(totales.TipoGanado));

                                             var totalMermaEntradas = tipoGanadoReporteEjecutivo.Where(
                                                     tipo => tipo.TipoGanado.Equals(totales.TipoGanado)).Sum(
                                                         total => total.Merma);

                                             totales.CantidadTipo = cantidadTipo;
                                             totales.PorcentajeMerma = totalMermaEntradas / totales.NumeroCabezas;

                                            
                                             precioKilosTipoGanado = precioKilosTipoGanado / cantidadTipo; //costo de compra
                                             totales.CostoCompra = Math.Round(precioKilosTipoGanado, 2);

                                             totales.CostoIntegrado = Math.Round(totales.TotalCostoIntegrado / totales.NumeroCabezas, 2);
                                         });

            decimal costoCompraHembra = 0;
            decimal costoIntegradoHembra = 0;
            int pesoPromedioHembra = 0;
            decimal porcentajeMermaHembras = 0;
            if (cabezasHembras > 0)
            {
                costoCompraHembra = Math.Round(reporteEjecutivo.Where(hembra => hembra.CabezasHembras > 0).Sum(
                    costo => costo.CabezasHembras * costo.CostoCompra) / cabezasHembras, 2);

                costoIntegradoHembra = Math.Round(reporteEjecutivo.Where(hembra => hembra.CabezasHembras > 0).Sum(
                    costo => costo.CabezasHembras * costo.CostoIntegrado) / cabezasHembras, 2);

                pesoPromedioHembra = Convert.ToInt32(reporteEjecutivo.Where(hembra => hembra.CabezasHembras > 0).Sum(
                    peso => peso.CabezasHembras * peso.PesoPromedioHembras) / cabezasHembras);

                porcentajeMermaHembras =
                    Math.Round(reporteEjecutivo.Where(hembra => hembra.CabezasHembras > 0).Sum(
                        merma => merma.CabezasHembras * merma.PorcentajeMerma) / cabezasHembras, 2);
            }


            decimal costoCompraMachos = 0;
            decimal costoIntegradoMachos = 0;
            int pesoPromedioMachos = 0;
            decimal porcentajeMermaMachos = 0;
            if (cabezasMachos > 0)
            {
                costoCompraMachos = Math.Round(reporteEjecutivo.Where(macho => macho.CabezasMachos > 0).Sum(
                    costo => costo.CabezasMachos*costo.CostoCompra)/cabezasMachos, 2);
                costoIntegradoMachos = Math.Round(reporteEjecutivo.Where(macho => macho.CabezasMachos > 0).Sum(
                    costo => costo.CabezasMachos * costo.CostoIntegrado) / cabezasMachos, 2);
                pesoPromedioMachos = Convert.ToInt32(reporteEjecutivo.Where(macho => macho.CabezasMachos > 0).Sum(
                    peso => peso.CabezasMachos * peso.PesoPromedioMachos) / cabezasMachos);
                porcentajeMermaMachos = Math.Round(reporteEjecutivo.Where(macho => macho.CabezasMachos > 0).Sum(
                    merma => merma.CabezasMachos * merma.PorcentajeMerma) / cabezasMachos, 2);    
            }

            int totalPesoPromedio =
                Convert.ToInt32( ((pesoPromedioHembra * cabezasHembras) + (pesoPromedioMachos * cabezasMachos)) / totalCabezas);

            decimal totalPorcentajeMerma =
                Math.Round( ((porcentajeMermaMachos * cabezasMachos) +  (porcentajeMermaHembras * cabezasHembras)) / totalCabezas, 2);

            decimal totalCostoCompra =
               Math.Round(((costoCompraMachos * cabezasMachos) + (costoCompraHembra * cabezasHembras)) / totalCabezas, 2);
            
            decimal totalCostoCompraIntegrado =
                Math.Round(((costoIntegradoMachos * cabezasMachos) + (costoIntegradoHembra * cabezasHembras)) / totalCabezas, 2);

            var resultado = new List<ReporteEjecutivoResultadoInfo>();
            for (var indexReporte = 0; indexReporte < reporteEjecutivo.Count; indexReporte++)
            {
                var ejecutivo = new ReporteEjecutivoResultadoInfo
                                    {
                                        CostoCompra = reporteEjecutivo[indexReporte].CostoCompra,
                                        CostoIntegrado = reporteEjecutivo[indexReporte].CostoIntegrado,
                                        NumeroCabezas = reporteEjecutivo[indexReporte].NumeroCabezas,
                                        PesoPromedio = reporteEjecutivo[indexReporte].PesoPromedio,
                                        PorcentajeMerma = reporteEjecutivo[indexReporte].PorcentajeMerma,
                                        TipoGanado = reporteEjecutivo[indexReporte].TipoGanado
                                    };
                resultado.Add(ejecutivo);
            }

            var machos = new ReporteEjecutivoResultadoInfo
                             {
                                 TipoGanado = "MACHOS ==>",
                                 NumeroCabezas = cabezasMachos,
                                 PesoPromedio = pesoPromedioMachos,
                                 PorcentajeMerma = porcentajeMermaMachos,
                                 CostoCompra = costoCompraMachos,
                                 CostoIntegrado = costoIntegradoMachos
                             };
            resultado.Add(new ReporteEjecutivoResultadoInfo());
            resultado.Add(machos);

            var hembras = new ReporteEjecutivoResultadoInfo
            {
                TipoGanado = "HEMBRAS ==>",
                NumeroCabezas = cabezasHembras,
                PesoPromedio = pesoPromedioHembra,
                PorcentajeMerma = porcentajeMermaHembras,
                CostoCompra = costoCompraHembra,
                CostoIntegrado = costoIntegradoHembra
            };
            resultado.Add(hembras);
            resultado.Add(new ReporteEjecutivoResultadoInfo());

            var totalGeneral = new ReporteEjecutivoResultadoInfo
            {
                TipoGanado = "TOTAL ==>",
                NumeroCabezas = totalCabezas,
                PesoPromedio = totalPesoPromedio,
                PorcentajeMerma = totalPorcentajeMerma,
                CostoCompra = totalCostoCompra,
                CostoIntegrado = totalCostoCompraIntegrado
            };
            resultado.Add(totalGeneral);            

            return resultado;
        }
    }
}
