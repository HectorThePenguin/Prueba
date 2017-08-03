using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteLlegadaLogisticaBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Recuperacion de Merma
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteLlegadaLogisticaDatos> GenerarReporteLlegadaLogistica(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteLlegadaLogisticaDatos> lista;
            try
            {
                Logger.Info();
                var reporteLlegadaLogisticaDAL = new ReporteLlegadaLogisticaDAL();
                lista = reporteLlegadaLogisticaDAL.ObtenerParametrosDatosLlegadaLogistica(organizacionID, fechaInicial, fechaFinal);

                if (lista == null)
                {
                    return null;
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
