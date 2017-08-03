//--*********** BL *************
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
namespace SIE.Services.Servicios.BL
{
    public class ReporteEntradasSinCosteoBl
    {
        /// <summary>
        /// Obtiene los datos al reporte ReporteEntradasSinCosteo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<ReporteEntradasSinCosteoInfo> ObtenerParametrosReporteEntradasSinCosteo(int organizacionId)
        {
            IList<ReporteEntradasSinCosteoInfo> lista;
            try
            {
                Logger.Info();
                var reporteDal = new ReporteEntradasSinCosteoDal();
                lista = reporteDal.ObtenerParametrosReporteEntradasSinCosteo(organizacionId);
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
