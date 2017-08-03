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
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteResumenInventarioDAL : DALBase
    {
       /// <summary>
        /// Obtiene los datos del reporte desde la base de datos
       /// </summary>
       /// <param name="organizacionID"></param>
       /// <param name="familiaID"></param>
       /// <param name="fechaInicial"></param>
       /// <param name="fechaFinal"></param>
       /// <returns></returns>
        internal IList<ReporteResumenInventarioInfo> ObtenerParametrosReporteResumenInventario(int organizacionID, int familiaID, DateTime fechaInicial, DateTime fechaFinal )
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteResumenInventarioDAL.ObtenerParametrosReporteResumenInventario(organizacionID,familiaID,fechaInicial,fechaFinal);
                DataSet ds = Retrieve("ReporteResumenInventario", parameters);
                IList<ReporteResumenInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteResumenInventarioDAL.Generar(ds);
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

