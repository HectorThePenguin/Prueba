using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class TransferenciaGanadoIndividualBL
    {
        /// <summary>
        /// Metodo para guardar la transferencia de ganado
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="corralDestino"></param>
        /// <param name="usuario"></param>
        /// <param name="decrementaCabezas"></param>
        /// <returns></returns>
        internal bool GuardarTransferenciaGanado(AnimalInfo animal, CorralInfo corralDestino, int usuario, bool decrementaCabezas)
        {
            bool resp = false;
            try
            {
                Logger.Info();
                var transferenciaDAL = new TransferenciaGanadoIndividualDAL();
                resp = transferenciaDAL.GuardarTransferenciaGanado(animal, corralDestino, usuario, decrementaCabezas);

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
            return resp;
        }

        /// <summary>
        /// Transfiere un animal al LoteID especificado
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="corralDestino"></param>
        /// <param name="usuario"></param>
        /// <param name="decrementaCabezas"></param>
        /// <returns></returns>
        public bool GuardarTransferenciaGanado(long animalId, int loteId)
        {
            bool resp = false;
            try
            {
                Logger.Info();
                var transferenciaDAL = new TransferenciaGanadoIndividualDAL();
                resp = transferenciaDAL.GuardarTransferenciaGanado(animalId, loteId);
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
            return resp;
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
        internal bool GuardarTransferenciaGanadoCompensacion(AnimalInfo animal, CorralInfo corralDestino, AnimalInfo animalCompensado, CorralInfo corralOrigen, int usuario, bool decrementarCabezas)
        {
            bool resp = false;
            try
            {
                Logger.Info();
                var transferenciaDAL = new TransferenciaGanadoIndividualDAL();
                using (var transaccion = new TransactionScope())
                {
                    // Se envia el animal al destino
                    resp = transferenciaDAL.GuardarTransferenciaGanado(animal, corralDestino, usuario, decrementarCabezas);

                    // Se envia el animal compensado al origen
                    resp = transferenciaDAL.GuardarTransferenciaGanado(animalCompensado, corralOrigen, usuario, decrementarCabezas);

                    transaccion.Complete();
                }

                
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
            return resp;
        }
    }
}
