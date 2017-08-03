using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class DeteccionBL
    {

        internal int Guardar(DeteccionInfo deteccionGrabar, FlagCargaInicial esCargaInicial, AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                int resultado;
                var transactionOption = new TransactionOptions();
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    if (animal != null)
                    {
                        var animalBL = new AnimalBL();
                        // Se valida el flag de EsCargaInicial
                        switch (esCargaInicial)
                        {
                            case FlagCargaInicial.EsCargaInicial:
                                // Se intercambian aretes por encontrarse el animal en un corral distinto y ser carga inicial
                                animalBL.ReemplazarAretes(animal, deteccionGrabar);
                                break;
                            case FlagCargaInicial.EsAreteNuevo:
                                // Se Reemplaza arete nuevo sobre uno existente del lote
                                animalBL.ReemplazarAreteMismoCorral(animal);
                                break;
                        }
                    }
                    var deteccionDAL = new DeteccionDAL();
                    resultado = deteccionDAL.Guardar(deteccionGrabar);
                    // Se cierral la transaccion
                    transaction.Complete();
                }
                return resultado;
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

        public bool ActualizarDeteccionConFoto(AnimalDeteccionInfo animalDetectado)
        {
            bool retValue = false;

            try
            {
                Logger.Info();
                var deteccionDAL = new DeteccionDAL();
                retValue = deteccionDAL.ActualizarAreteDeteccionConFoto(animalDetectado);
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

            return retValue;
        }

        /// <summary>
        /// Metodo para crear ubna deteccion generica
        /// </summary>
        /// <param name="deteccionId"></param>
        /// <returns></returns>
        internal int GuardarDeteccionGenerica(int deteccionId)
        {
            try
            {
                Logger.Info();
                var deteccionDAL = new DeteccionDAL();
                int resultado = deteccionDAL.GuardarDeteccionGenerica(deteccionId);

                return resultado;
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
    }
}
