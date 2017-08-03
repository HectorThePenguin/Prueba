
//--*********** PL *************
using System;
using System.Data;
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
    public class ReporteDiaDiaCalidadPL
    {
        /// <summary>
        /// Obtiene los datos al informe de ReporteDiaDiaCalidad
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public IList<ReporteDiaDiaCalidadInfo> ObtenerReporteDiaDiaCalidad(int organizacionID, DateTime fecha)
        {
            IList<ReporteDiaDiaCalidadInfo> lista;
            try
            {
                Logger.Info();
                var vReporteDiaDiaCalidadBL = new ReporteDiaDiaCalidadBL();
                lista = vReporteDiaDiaCalidadBL.ObtenerReporteDiaDiaCalidad(organizacionID, fecha);

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

        public DateTime? ObtenerFechaServidor()
        {
            DateTime? valor = null;
            try
            {
                Logger.Info();
                var vReporteDiaDiaCalidadBL = new ReporteDiaDiaCalidadBL();
                valor = vReporteDiaDiaCalidadBL.ObtenerFechaServidor();

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

            return valor;
        }

        /// <summary>
        /// Obtiene la informacion de otros analisis
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public IList<ReporteDiaDiaCalidadAnalisisInfo> ObtenerReporteDiaDiaCalidadAnalisis(int organizacionId, DateTime fecha)
        {
            IList<ReporteDiaDiaCalidadAnalisisInfo> lista;
            try
            {
                Logger.Info();
                var vReporteDiaDiaCalidadBL = new ReporteDiaDiaCalidadBL();
                lista = vReporteDiaDiaCalidadBL.ReporteDiaDiaCalidadAnalisis(organizacionId, fecha);

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
