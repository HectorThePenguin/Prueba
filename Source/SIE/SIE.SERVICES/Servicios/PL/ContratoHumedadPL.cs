using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ContratoHumedadPL 
    {
        /// <summary>
        /// Obtiene un listado de humedades por contratoid
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public List<ContratoHumedadInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                var contratoParcialBl = new ContratoHumedadBL();
                return contratoParcialBl.ObtenerPorContratoId(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene la humedad a la fecha
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public ContratoHumedadInfo ObtenerHumedadAlaFecha(ContratoInfo contrato)
        {
            try
            {
                var contratoParcialBl = new ContratoHumedadBL();
                return contratoParcialBl.ObtenerHumedadAlaFecha(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
