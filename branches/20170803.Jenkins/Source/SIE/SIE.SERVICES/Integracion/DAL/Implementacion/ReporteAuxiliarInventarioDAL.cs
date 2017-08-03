using System;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using System.Collections.Generic;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteAuxiliarInventarioDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Auxiliar de Inventario
        /// </summary>
        /// <returns> </returns>
        internal CorralReporteAuxiliarInventarioInfo ObtenerDatosCorral(string codigoCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteAuxiliarInventarioDAL.ObtenerParametrosAuxiliarInventario(codigoCorral, organizacionID);
                DataSet ds = Retrieve("Corral_AuxiliarInventario", parameters);
                CorralReporteAuxiliarInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteAuxiliarInventarioDAL.ObtenerCorralReporteAuxiliarInventario(ds);
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
        /// Obtiene la informacion del reporte
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal ReporteAuxiliarInventarioInfo ObtenerDatosReporteAuxiliarInventario(int loteID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteAuxiliarInventarioDAL.ObtenerParametrosReporteAuxiliarInventario(loteID);
                DataSet ds = Retrieve("ReporteAuxiliarInventario_ObtenerReporte", parameters);
                ReporteAuxiliarInventarioInfo result = null;
                if (ValidateDataSetMultiTabla(ds))
                {
                    result = MapReporteAuxiliarInventarioDAL.ObtenerReporteAuxuliarInventario(ds);
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
