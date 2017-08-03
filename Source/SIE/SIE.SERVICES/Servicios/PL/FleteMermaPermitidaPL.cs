using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class FleteMermaPermitidaPL
    {
        /// <summary>
        /// Obtiene configuracion de flete merma permitida
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public FleteMermaPermitidaInfo ObtenerConfiguracion(FleteMermaPermitidaInfo almacenMovimientoInfo)
        {
            FleteMermaPermitidaInfo info;
            try
            {
                Logger.Info();
                var fleteMermaPermitidaBl = new FleteMermaPermitidaBL();
                info = fleteMermaPermitidaBl.ObtenerConfiguracion(almacenMovimientoInfo);
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
            return info;
        }
    }
}
