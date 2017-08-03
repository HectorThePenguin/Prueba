using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteDetalleCorteBL
    {
        /// <summary>
        /// Obtener reporte detale del corte
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        internal List<ReporteDetalleCorteModel> ObtenerReporteDetalleCorte(ReporteEncabezadoInfo encabezado, int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int idUsuario, int TipoMovimientoID)
        {
            List<ReporteDetalleCorteModel> lista;
            try
            {
                Logger.Info();
                var ReporteDetalleCorteDAL = new ReporteDetalleCorteDAL();
                List<ReporteDetalleCorteDatos> datosReporte =
                    ReporteDetalleCorteDAL.ObtenerParametrosDatosReporteDetalleCorte(organizacionID,
                                                                                                fechaInicial,
                                                                                                fechaFinal, idUsuario,TipoMovimientoID);
                if (datosReporte == null)
                {
                    return null;
                }
                lista = GenerarDatosReporteGeneral(datosReporte, encabezado);

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
        /// Generar reporte general
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="encabezado"></param>
        /// <returns></returns>
        private List<ReporteDetalleCorteModel> GenerarDatosReporteGeneral(IEnumerable<ReporteDetalleCorteDatos> datosReporte, ReporteEncabezadoInfo encabezado)
        {
            var lista = new List<ReporteDetalleCorteModel>();

            var productos = (from prod in datosReporte
                             group prod by prod.AreteID
                                 into prodAgru
                                 let reporteDetalleCorteDatos = prodAgru.FirstOrDefault()
                                 where reporteDetalleCorteDatos != null
                                 select new ReporteDetalleCorteModel
                                 {
                                     AreteId = reporteDetalleCorteDatos.AreteID,
                                     Descripcion = reporteDetalleCorteDatos.Descripcion,
                                     CorralOrigen = reporteDetalleCorteDatos.CorralOrigen,
                                     CorralDestino = reporteDetalleCorteDatos.CorralDestino,
                                     PesoOrigen = reporteDetalleCorteDatos.PesoOrigen,
                                     PesoCorte = reporteDetalleCorteDatos.PesoCorte,
                                     Merma = reporteDetalleCorteDatos.Merma,
                                     Temperatura = reporteDetalleCorteDatos.Temperatura,
                                     Titulo = encabezado.Titulo,
                                     TituloReporte = encabezado.TituloReporte,
                                     Organizacion = encabezado.Organizacion,
                                     Fecha = encabezado.TituloPeriodo,
                                     FechaInicio = reporteDetalleCorteDatos.FecInicial,
                                     FechaFin = reporteDetalleCorteDatos.FecFinal,
                                     FolioEntrada = reporteDetalleCorteDatos.FolioEntrada,
                                 }).ToList();
            lista.AddRange(productos);

            return lista;
        }
   
    }
}