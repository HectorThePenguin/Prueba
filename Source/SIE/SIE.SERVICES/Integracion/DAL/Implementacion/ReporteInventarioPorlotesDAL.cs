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
    internal class ReporteInventarioPorlotesDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos del reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="familiaId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IList<ReporteInventarioPorLotesInfo> ObtenerParametrosReporteInventarioPorlotes(int organizacionId, int familiaId, DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteInventarioPorLotesDAL.ObtenerParametrosReporteInventarioPorlotes(organizacionId, familiaId, fecha);
                DataSet ds = Retrieve("ReporteInventarioPorlotes", parameters);
                IList<ReporteInventarioPorLotesInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteInventarioPorLotesDAL.Generar(ds);
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