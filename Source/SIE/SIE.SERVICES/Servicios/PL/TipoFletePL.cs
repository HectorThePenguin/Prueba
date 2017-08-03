using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoFletePL
    {
        /// <summary>
        /// Metodo para obtener un listado de tipos flete por estatus
        /// </summary>
        public List<TipoFleteInfo> ObtenerTiposFleteTodos(EstatusEnum estatus)
        {
            List<TipoFleteInfo> result;
            try
            {
                Logger.Info();
                var tipoFleteBL = new TipoFleteBL();
                result = tipoFleteBL.ObtenerTiposFleteTodos(estatus);
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
