using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ContratoHumedadBL 
    {
        /// <summary>
        /// Crea registros en contrato humedad
        /// </summary>
        /// <returns></returns>
        internal int CrearHumedadParcial(List<ContratoHumedadInfo> listaContratoHumedadInfo)
        {
            try
            {
                Logger.Info();
                var contratoHumedadDal = new ContratoHumedadDAL();
                int result = contratoHumedadDal.CrearContratoHumedad(listaContratoHumedadInfo);
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

        /// <summary>
        /// Obtiene una lista de humedades por contratoid
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<ContratoHumedadInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                var contratoHumedadDal = new ContratoHumedadDAL();
                return contratoHumedadDal.ObtenerPorContratoId(contrato);
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
        internal ContratoHumedadInfo ObtenerHumedadAlaFecha(ContratoInfo contrato)
        {
            try
            {
                var contratoHumedadDal = new ContratoHumedadDAL();
                return contratoHumedadDal.ObtenerHumedadAlaFecha(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
