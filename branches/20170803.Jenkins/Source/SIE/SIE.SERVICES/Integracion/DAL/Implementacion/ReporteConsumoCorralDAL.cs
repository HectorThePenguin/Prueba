using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteConsumoCorralDAL : DALBase
    {
        void MetodoBase(Action accion)
        {
            try
            {
                Logger.Info();
                accion();
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

        internal void Generar(int loteId, DateTime fechaInicial, DateTime fechaFinal, Info.Modelos.AlimentacionConsumoCorralReporte alimentacionConsumoCorralReporte)
        {
            MetodoBase(() =>
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@LoteId", loteId);
                parametros.Add("@FechaInicio", fechaInicial);
                parametros.Add("@FechaFin", fechaFinal);
                DataSet ds = Retrieve("ReporteConsumoCorral_Generar", parametros);
                if (ValidateDataSet(ds))
                {
                    MapReporteConsumoCorralDAL.Generar(ds, alimentacionConsumoCorralReporte);
                }
            });
        }
    }
}
