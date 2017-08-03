using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteEntradaSalidaZilmaxDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Auxiliar de Inventario
        /// </summary>
        /// <returns> </returns>
        /// 
        internal List<ReporteEntradaSalidaZilmaxInfo> ObtenerDatosReporte(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxReporteEntradaSalidaZilmaxDAL.ObtenerParametrosObtenerPorOrganizacion(organizacionID);
                DataSet ds = Retrieve("Zilmax_ObtenerTodasEntradasSalidas", parameters);
                List<ReporteEntradaSalidaZilmaxInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteEntradaSalidaZilmaxDAL.ObtenerTodos(ds);
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

        /// <summary>
        /// Obtiene las entradas/salidas zilmax
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaZilmax"> </param>
        /// <returns></returns>
        internal IList<ReporteEntradaSalidaZilmaxInfo> ObtenerDatosReporteSeleccionado(int organizacionId, DateTime fechaZilmax)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxReporteEntradaSalidaZilmaxDAL.ObtenerParametrosObtenerPorOrganizacion(organizacionId, fechaZilmax);
                DataSet ds = Retrieve("Zilmax_ObtenerTodasEntradasSalidasSeleccionadas", parameters);
                List<ReporteEntradaSalidaZilmaxInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteEntradaSalidaZilmaxDAL.ObtenerTodos(ds);
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
