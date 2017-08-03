using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;


namespace SIE.Services.Servicios.PL
{
    public class GanadoIntensivoPL
    {
        
        /// <summary>
        /// Obtener muerte de ganado intensivo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        public GanadoIntensivoInfo ObtenerMuerteGanadoIntensivo(CorralInfo corral)
        {
            GanadoIntensivoInfo ganadoIntensivoInfo = null;
            
            try
            {
                Logger.Info();
                var muerteGanadoIntensivoBL = new GanadoIntensivoBL();
                 ganadoIntensivoInfo =
                    muerteGanadoIntensivoBL.ObtenerMuerteGanadoIntensivo(corral);
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
            return ganadoIntensivoInfo;
        }

        public GanadoIntensivoInfo CalcularCostosDeCabezas(GanadoIntensivoInfo contexto)
        {
            GanadoIntensivoInfo ganadoIntensivoInfo = null;
            try
            {
                var muerteGanadoIntensivoBL = new GanadoIntensivoBL();
                ganadoIntensivoInfo = muerteGanadoIntensivoBL.CalcularCostosDeCabezas(contexto);

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
            return ganadoIntensivoInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        public GanadoIntensivoInfo MuerteGanadoIntensivo_Guardar(GanadoIntensivoInfo ganadoIntensivo)
        {
            try
            {
                var ganadoIntensivoBL = new GanadoIntensivoBL();
                var result = ganadoIntensivoBL.MuerteGanadoIntensivo_Guardar(ganadoIntensivo);
                return result;
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
    }
}
