using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class PremezclaPL
    {
        /// <summary>
        /// Obtiene la lista de premezclas por organizacion
        /// </summary>
        /// <param name="organizacion"></param>
        public List<PremezclaInfo> ObtenerPorOrganizacion(OrganizacionInfo organizacion)
        {
            try
            {
                var premezclaBl = new PremezclaBL();
                return premezclaBl.ObtenerPorOrganizacion(organizacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene una premezcla por productoid y organizacionid
        /// </summary>
        /// <returns></returns>
        public PremezclaInfo ObtenerPorProductoIdOrganizacionId(PremezclaInfo premezclaInfo)
        {
            try
            {
                var premezclaBl = new PremezclaBL();
                return premezclaBl.ObtenerPorProductoIdOrganizacionId(premezclaInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
