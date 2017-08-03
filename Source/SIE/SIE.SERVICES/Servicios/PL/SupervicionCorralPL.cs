using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class SupervicionCorralPL
    {
        public NombreDetectorEvaluadorInfo ObtenerNombreDetectorEvaluado()
        {
            NombreDetectorEvaluadorInfo result = null;
            try
            {
                Logger.Info();
                var superevicionCorralBL = new SupervicionCorralBL();
               result = superevicionCorralBL.ObtenerNombreDetectorEvaluado();
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

            return result;
        }
    }
}
