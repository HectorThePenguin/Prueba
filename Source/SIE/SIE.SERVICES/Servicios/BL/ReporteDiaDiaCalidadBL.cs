
//--*********** BL *************
using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteDiaDiaCalidadBL
    {
        /// <summary>
        /// Obtiene los datos al reporte ReporteDiaDiaCalidad
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IList<ReporteDiaDiaCalidadInfo> ObtenerReporteDiaDiaCalidad(int organizacionID, DateTime fecha)
        {
            IList<ReporteDiaDiaCalidadInfo> lista;
            try
            {
                Logger.Info();
                var vReporteDiaDiaCalidadDAL = new ReporteDiaDiaCalidadDAL();
                IList<ReporteDiaDiaCalidadInfo> datosReporte =
                    vReporteDiaDiaCalidadDAL.ObtenerParametrosReporteDiaDiaCalidad(organizacionID, fecha);
                if (datosReporte == null)
                {
                    return null;
                }
                lista = datosReporte;

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
                var dal = new DataAccess();
                valor = dal.FechaServidor();

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
        internal IList<ReporteDiaDiaCalidadAnalisisInfo> ReporteDiaDiaCalidadAnalisis(int organizacionId, DateTime fecha)
        {
            IList<ReporteDiaDiaCalidadAnalisisInfo> lista = null;
            try
            {
                Logger.Info();
               var reporteDal = new ReporteDiaDiaCalidadDAL();
               lista = reporteDal.ReporteDiaDiaCalidadAnalisis(organizacionId, fecha);
               
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
