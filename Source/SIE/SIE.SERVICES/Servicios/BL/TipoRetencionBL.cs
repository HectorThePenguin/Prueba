using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class TipoRetencionBL
    {
        internal IList<TipoRetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoRetencionDAL = new TipoRetencionDAL();
                IList<TipoRetencionInfo> result = tipoRetencionDAL.ObtenerTodos(estatus);

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
