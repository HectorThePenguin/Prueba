using SIE.Base.Exepciones;
using SIE.Base.Log;
using System;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class ReporteConsumoCorralBL
    {
        void MetodoBase(Action accion, Action completo = null)
        {
            try
            {
                Logger.Info();
                accion();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                if (completo != null)
                {
                    completo();
                }
            }
        }

        public void GenerarReporte(int loteId, DateTime fechaInicial, DateTime fechaFinal, Info.Modelos.AlimentacionConsumoCorralReporte alimentacionConsumoCorralReporte)
        {
            MetodoBase(() =>
            {
                var dal = new Services.Integracion.DAL.Implementacion.ReporteConsumoCorralDAL();
                dal.Generar(loteId, fechaInicial, fechaFinal, alimentacionConsumoCorralReporte);
            });
        }
    }
}
