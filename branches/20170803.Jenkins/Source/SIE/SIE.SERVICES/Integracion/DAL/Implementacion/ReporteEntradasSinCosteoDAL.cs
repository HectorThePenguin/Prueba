//--*********** DAL *************
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteEntradasSinCosteoDal : DALBase
    {
        /// <summary>
        /// Obtiene los datos del reporte desde la base de datos
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<ReporteEntradasSinCosteoInfo> ObtenerParametrosReporteEntradasSinCosteo(int organizacionId)
        {
            try
            {
                
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteEntradasSinCosteoDal.ObtenerParametrosReporteEntradasSinCosteo(organizacionId);
                DataSet ds = Retrieve("ReporteEntradaSinCosto", parameters);
                IList<ReporteEntradasSinCosteoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result =  MapReporteEntradasSinCosteoDal.Generar(ds);
                }
                return result;
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
