using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class FleteInternoDetallePL
    {
        /// <summary>
        /// Obtiene un listado de flete interno detalle por flete interno id
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <returns></returns>
        public List<FleteInternoDetalleInfo> ObtenerPorFleteInternoId(FleteInternoInfo fleteInternoInfo)
        {
            List<FleteInternoDetalleInfo> fleteInternoDetalleLista;
            try
            {
                Logger.Info();
                var fleteInternoDetalleBl = new FleteInternoDetalleBL();
                fleteInternoDetalleLista = fleteInternoDetalleBl.ObtenerPorFleteInternoId(fleteInternoInfo);
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
            return fleteInternoDetalleLista;
        }
    }
}
