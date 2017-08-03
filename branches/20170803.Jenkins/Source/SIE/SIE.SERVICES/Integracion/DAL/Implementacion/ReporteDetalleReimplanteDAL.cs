using System;
using System.Collections.Generic;
using System.Data;
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
    internal class ReporteDetalleReimplanteDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Inventario
        /// </summary>
        /// <returns> </returns>
        internal DataTable GenerarReporteDetalleReimplante(int organizacionID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteDetalleReimplanteDAL.ObtenerParametrosReporteDetalleReimplante(organizacionID, fecha);
                DataSet ds = Retrieve("ReporteDetalleReimplante", parameters);
                DataTable result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteDetalleReimplanteDAL.ObtenerDetalleReimplante(ds);
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
