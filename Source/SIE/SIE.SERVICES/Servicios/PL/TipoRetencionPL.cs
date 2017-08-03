using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Servicios.BL;
using SIE.Base.Exepciones;

namespace SIE.Services.Servicios.PL
{
    public class TipoRetencionPL
    {
        public IList<TipoRetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoRetencionBL = new TipoRetencionBL();
                IList<TipoRetencionInfo> result = tipoRetencionBL.ObtenerTodos(estatus);
                
                return result;            
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
