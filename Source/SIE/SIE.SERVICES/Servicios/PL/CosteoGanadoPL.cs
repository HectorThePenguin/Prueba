using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CosteoGanadoPL
    {
        /// <summary>
        /// Metodo para generar el costeo de ganado para las entradas que no tienen
        /// los costos estan prorrateados.
        /// </summary>
        /// <returns></returns>
        public void GenerarCosteoGanado()
        {
            try
            {
                Logger.Info();
                var costeoGanadoBL = new CosteoGanadoBL();
                costeoGanadoBL.GenerarCosteoGanado();
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
        }

        /// <summary>
        /// Metodo para generar el costeo del consumo de forraje
        /// </summary>
        /// <param name="corteGanadoGuardarInfo"></param>
        public bool GenerarCosteoConsumoForraje(CorteGanadoGuardarInfo corteGanadoGuardarInfo)
        {
            bool generarCosteo;
            try
            {
                Logger.Info();
                var costeoGanadoBL = new CosteoGanadoBL();
                generarCosteo = costeoGanadoBL.GenerarCosteoConsumoForraje(corteGanadoGuardarInfo);
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
            return generarCosteo;
        }
    }
}
