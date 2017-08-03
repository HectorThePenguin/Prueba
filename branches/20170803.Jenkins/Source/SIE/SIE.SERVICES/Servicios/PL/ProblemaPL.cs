using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIE.Services.Servicios.PL
{
    public class ProblemaPL
    {
        public IList<ProblemaInfo> ObtenerListaProblemas()
        {
            try
            {
                Logger.Info();
                using(var problemaBL = new ProblemaBL())
                {
                    return problemaBL.ObtenerListaProblemas();
                }
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
