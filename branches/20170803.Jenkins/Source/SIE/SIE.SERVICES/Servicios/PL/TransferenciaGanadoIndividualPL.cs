using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TransferenciaGanadoIndividualPL
    {
        /// <summary>
        /// Metodo para guardar la transferencia de ganado
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="corralDestino"></param>
        /// <param name="usuario"></param>
        /// <param name="decrementarCabezas"></param>
        /// <returns></returns>
        public bool GuardarTransferenciaGanado(AnimalInfo animal, CorralInfo corralDestino, int usuario, bool decrementarCabezas)
        {
            bool resultado = false;
            try
            {
                Logger.Info();
                var transferenciaBL = new TransferenciaGanadoIndividualBL();
                resultado = transferenciaBL.GuardarTransferenciaGanado(animal, corralDestino, usuario, decrementarCabezas);
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
            return resultado;
        }

        /// <summary>
        /// Metodo para guardar la transferencia de ganado con compensacion de animal
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="corralDestino"></param>
        /// <param name="animalCompensado"></param>
        /// <param name="corralOrigen"></param>
        /// <param name="usuario"></param>
        /// <param name="decrementarCabezas"></param>
        /// <returns></returns>
        public bool GuardarTransferenciaGanadoCompensacion(AnimalInfo animal, CorralInfo corralDestino, AnimalInfo animalCompensado, CorralInfo corralOrigen, int usuario, bool decrementarCabezas)
        {
            bool resultado = false;
            try
            {
                Logger.Info();
                var transferenciaBL = new TransferenciaGanadoIndividualBL();
                resultado = transferenciaBL.GuardarTransferenciaGanadoCompensacion(animal, 
                                                                                   corralDestino, 
                                                                                   animalCompensado, 
                                                                                   corralOrigen,
                                                                                   usuario,
                                                                                   decrementarCabezas);
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
            return resultado;
        }
    }
}
