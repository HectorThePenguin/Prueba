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
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ReporteKardexGanadoDAL : DALBase
    {
        /// <summary>
        /// Obtiene el reporte kardex de ganado.
        /// </summary>
        /// <returns></returns>
        public IList<ReporteKardexGanadoInfo> Generar(FiltroParametrosKardexGanado filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteKardexGanadoDAL.Generar(filtro);
                DataSet ds = Retrieve("ReporteKardexDeGanado", parameters);
                IList<ReporteKardexGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteKardexGanadoDal.Generar(ds);
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
