using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CausaRechazoPL
    {
        /// <summary>
        /// Metodo para obtener un listado de animales por Codigo de Corral
        /// </summary>
        public List<CausaRechazoInfo> ObtenerCatalogoCausaRechazo()
        {
            List<CausaRechazoInfo> result;
            try
            {
                Logger.Info();
                //var CausaRechazoBL = new CausaRechazoBL();
                result = null; // CausaRechazoBL.ObtenerCatalogoCausaRechazo();
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
