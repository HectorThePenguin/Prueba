using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    public class SociedadBL
    {
        public IList<SociedadInfo> ObtenerTodas()
        {
            try
            {
                Logger.Info();
                var sociedadDAL = new SociedadDAL();
                var result = sociedadDAL.ObtenerTodas();
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
