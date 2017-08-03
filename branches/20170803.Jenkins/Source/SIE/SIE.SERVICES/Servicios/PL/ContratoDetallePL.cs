using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ContratoDetallePL
    {
        /// <summary>
        /// Obtiene un listado de contrato detalle por contrato id
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public List<ContratoDetalleInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            List<ContratoDetalleInfo> contratoDetalleLista;
            try
            {
                Logger.Info();
                var contratoDetalleBl = new ContratoDetalleBL();
                contratoDetalleLista = contratoDetalleBl.ObtenerPorContratoId(contrato);
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
            return contratoDetalleLista;
        }
    }
}
