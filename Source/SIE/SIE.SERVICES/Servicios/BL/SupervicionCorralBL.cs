using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class SupervicionCorralBL
    {
        internal NombreDetectorEvaluadorInfo ObtenerNombreDetectorEvaluado()
        {
            NombreDetectorEvaluadorInfo result;
            try
            {
                Logger.Info();
                var supervicionCorralDAL = new SupervicionCorralDAL();
                result = supervicionCorralDAL.ObtenerNombreDetectorEvaluado();
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
