using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteMedicamentosAplicadosPL
    {
        /// <summary>
        /// Obtiene los datos al informe de medicamentos aplicados
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="tipoTratamiento"></param>
        /// <returns></returns>
        public List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentosAplicados(ReporteEncabezadoInfo encabezado, int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int tipoTratamiento)
        {
            List<ReporteMedicamentosAplicadosModel> lista;
            try
            {
                Logger.Info();
                var reporteMedicamentosAplicadosBL = new ReporteMedicamentosAplicadosBL();
                lista = reporteMedicamentosAplicadosBL.ObtenerReporteMedicamentosAplicados(encabezado,
                    organizacionID, fechaInicial, fechaFinal, tipoTratamiento);

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
        /// Obtiene la informacion del reporte
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionId"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="tipoTratamiento"></param>
        /// <returns></returns>
        public List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentosAplicadosSanidad(ReporteEncabezadoInfo encabezado, int organizacionId, int almacenId, DateTime fechaInicial, DateTime fechaFinal, int tipoTratamiento)
        {
            List<ReporteMedicamentosAplicadosModel> lista;
            try
            {
                Logger.Info();
                var reporteMedicamentosAplicadosBL = new ReporteMedicamentosAplicadosBL();
                lista = reporteMedicamentosAplicadosBL.ObtenerReporteMedicamentosAplicadosSanidad(encabezado,
                    organizacionId, almacenId, fechaInicial, fechaFinal, tipoTratamiento);

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
        /// Obtiene Reporte de Medicamentos Aplicados exclusivamente para el repoorte de Medicamentos Aplicados
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="tipoTratamiento"></param>
        /// <returns></returns>
        public List<ReporteMedicamentosAplicadosModel> ObtenerReporteMedicamentoCabezasAplicados(ReporteEncabezadoInfo encabezado, int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int tipoTratamiento)
        {
            List<ReporteMedicamentosAplicadosModel> lista;
            try
            {
                Logger.Info();
                var reporteMedicamentosAplicadosBL = new ReporteMedicamentosAplicadosBL();
                lista = reporteMedicamentosAplicadosBL.ObtenerReporteMedicamentoCabezasAplicados(encabezado,
                    organizacionID, fechaInicial, fechaFinal, tipoTratamiento);

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
