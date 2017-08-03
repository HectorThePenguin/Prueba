using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class SolicitudPremezclaPL
    {
        /// <summary>
        /// Guarda los datos para una solicitud de premezclas
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public bool Guardar(SolicitudPremezclaInfo solicitud)
        {
            try
            {
                Logger.Info();
                var solicitudBl = new SolicitudPremezclaBL();
                return solicitudBl.Guardar(solicitud);
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
