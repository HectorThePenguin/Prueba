using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ReporteMuertesGanadoDAL : DALBase
    {
      
        /// <summary>
        /// Obtiene el reporte de las muertes de ganado.
        /// </summary>
        /// <returns></returns>
        public IList<ReporteMuertesGanadoInfo> Generar(FiltroFechasInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteMuertesGanadoDAL.Generar(filtro);
                DataSet ds = Retrieve("ReporteMuertesGanado_ObtenerPorFecha", parameters);
                IList<ReporteMuertesGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteMuertesGanadoDAL.Generar(ds);
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

