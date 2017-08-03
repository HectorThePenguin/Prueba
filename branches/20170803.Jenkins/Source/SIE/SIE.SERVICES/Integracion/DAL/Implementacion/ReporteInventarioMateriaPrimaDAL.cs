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
    internal class ReporteInventarioMateriaPrimaDAL : DALBase
    {
        /// <summary>
        /// Obtiene la informacion del reporte desde la base de datos
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="productoId"></param>
        /// <param name="almacenId"></param>
        /// <param name="lote"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        internal List<ReporteInventarioMateriaPrimaInfo> GenerarReporteInventario(int organizacionId, int productoId, int almacenId, int lote, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteInventarioMateriaPrimaDAL.ObtenerParametrosTipoGanadoReporteInventario(organizacionId, productoId, almacenId, lote, fechaInicio, fechaFin);
                DataSet ds = Retrieve("ReporteInventarioMateriaPrima_Obtener", parameters);
                List<ReporteInventarioMateriaPrimaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteInventarioMateriaPrimaDAL.ObtenerReporte(ds);
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
