using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteSolicitudPaseProcesoBL
    {
       /// <summary>
       /// Obtiene los datos al reporte de Solicitud de pase a proceso
       /// </summary>
       /// <param name="organizacionId"></param>
       /// <param name="fecha"></param>
       /// <returns></returns>
        internal IList<ReporteSolicitudPaseProcesoInfo> ObtenerReporteSolicitudPaseProceso(int organizacionId, DateTime fecha)
        {
            IList<ReporteSolicitudPaseProcesoInfo> lista = null;
             try
             {
                 Logger.Info();
                 var reporteDal = new ReporteSolicitudPaseProcesoDAL();
                 lista = reporteDal.ObtenerParametrosSolicitudPaseProceso(organizacionId, fecha, TipoAlmacenEnum.MateriasPrimas, FamiliasEnum.MateriaPrimas);
               
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
