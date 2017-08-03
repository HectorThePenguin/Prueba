using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ReporteSalidaConCostosDAL : DALBase
    {

        public IList<ReporteSalidaConCostosInfo> obtenerReporte(ReporteSalidasConCostoParametrosInfo parametros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteSalidaConCostosDAL.obtenerReporte(parametros);
                DataSet ds = Retrieve("ReporteSalidasConCosto", parameters);
                IList<ReporteSalidaConCostosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteSalidaConCostosDAL.obtenerReporte(ds);
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
