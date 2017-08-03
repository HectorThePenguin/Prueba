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
    internal class ReporteDetalleCorteDAL : DALBase
    {
        /// <summary>
        ///  Obtiene la informacion del Reporte Medicamentos Aplicados
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteDetalleCorteDatos> ObtenerParametrosDatosReporteDetalleCorte(int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int idUsuario,int TipoMovimientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteDetalleCorteDAL.ObtenerParametrosDatosReporteDetalleCorte(organizacionID, fechaInicial, fechaFinal, idUsuario,TipoMovimientoID);
                DataSet ds = Retrieve("ReporteDetalleCorte", parameters);
                List<ReporteDetalleCorteDatos> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteDetalleCorteDAL.ObtenerParametrosDatosReporteDetalleCorte(ds);
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
