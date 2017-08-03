using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ContratoParcialPL
    {
        /// <summary>
        /// Obtiene una lista de parcialidades de compra
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public List<ContratoParcialInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                var contratoParcialBl = new ContratoParcialBL();
                return contratoParcialBl.ObtenerPorContratoId(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        public List<ContratoParcialInfo> ObtenerFaltantePorContratoId(ContratoInfo contratoInfo)
        {
            try
            {
                var contratoParcialBl = new ContratoParcialBL();
                return contratoParcialBl.ObtenerFaltantePorContratoId(contratoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
