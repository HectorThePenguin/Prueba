using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class SociedadPL
    {
        public IList<SociedadInfo> ObtenerTodas()
        {
            try
            {
                Logger.Info();
                var sociedadBL = new SociedadBL();
                var result = sociedadBL.ObtenerTodas();
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
