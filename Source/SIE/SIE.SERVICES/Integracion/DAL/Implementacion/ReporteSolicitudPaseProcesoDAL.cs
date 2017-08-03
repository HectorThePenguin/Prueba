using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteSolicitudPaseProcesoDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos de la base para el reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <param name="tipoAlmacen"></param>
        /// <param name="familia"></param>
        /// <returns></returns>
        internal IList<ReporteSolicitudPaseProcesoInfo> ObtenerParametrosSolicitudPaseProceso(int organizacionId, DateTime fecha, TipoAlmacenEnum tipoAlmacen, FamiliasEnum familia)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteSolicitudPaseProcesoDAL.ObtenerParametrosReporteSolicitudPaseProceso(organizacionId, fecha, tipoAlmacen, familia);
                DataSet ds = Retrieve("ReporteSolicitudPaseProceso", parameters);
                IList<ReporteSolicitudPaseProcesoInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapReporteSolicitudPaseProcesoDAL.ObtenerDatosReporteSolicitudPaseProceso(ds);
                }
                return lista;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
