using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;
namespace SIE.Services.Servicios.PL
{
    public class SintomaPL
    {
        public IList<SintomaInfo> ObtenerPorProblema(int problema)
        {
            IList<SintomaInfo> resultado;

            try
            {
                Logger.Info();
                using (var sintomaBL = new SintomaBL())
                {
                    resultado = sintomaBL.ObtenerPorProblema(problema);
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

            return resultado;
        }
    }
}
