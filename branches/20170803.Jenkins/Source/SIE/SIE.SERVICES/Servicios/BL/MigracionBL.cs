using System;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Info.Info;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    public class MigracionBL
    {
        /// <summary>
        /// Metodo para Crear la carga inicial de los animales en SIAP
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public MigracionCifrasControlInfo CrearCargaInicialAnimales(int organizacionId)
        {
            MigracionCifrasControlInfo resp;
            try
            {
                Logger.Info();
                
                /* Se obtienen los animales de control Individual */
                var controlIndividualDAL = new MigracionDAL(organizacionId);
                ControlIndividualInfo controlIndividualInfo = controlIndividualDAL.ObtenerAnimalesControlIndividual();
                // Se inicia la transaccion por Entrada validada
                //using (var transaction = new TransactionScope())
                TimeSpan ts1 = new TimeSpan(1, 00, 0);
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew, ts1))
                {
                    /* Crear Cargas Iniciales en SIAP*/
                    /* Se crea EntradaGanadoCargaInicial -- LoteCargaInicial -- AnimalCargaInicial -- AnimalMovimientoCargaInicial */
                    var siapDAL = new MigracionDAL();
                    resp = siapDAL.GuardarCargaInicialSIAP(controlIndividualInfo, organizacionId);

                    transaction.Complete();
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
        /// <summary>
        /// Metodo para obtener la informacion de los animales en el Control Individual
        /// </summary>
        /// <returns></returns>
        public bool ObtenerAnimalesControlIndividual()
        {
            bool resp;
            try
            {
                Logger.Info();
                var miControlIndividual = new MigracionDAL(2);
                miControlIndividual.ObtenerAnimalesControlIndividual();

                resp = true;
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
        /// Metodo para almacenar en la tabla resumen
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public MigracionCifrasControlInfo GuardarResumen(List<ResumenInfo> lista, int organizacionId)
        {
            MigracionCifrasControlInfo resp ;
            try
            {
                // Se inicia la transaccion por Entrada validada
                using (var transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0,0,60,0)))
                {
                    Logger.Info();
                    var migracionDal = new MigracionDAL();
                    resp = migracionDal.GuardarResumen(lista, organizacionId);
                    transaction.Complete();
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

        /// <summary>
        /// Metodo para meter los animales de las cargas iniciales a SIAP
        /// </summary>
        /// <param name="organizacionId"></param>
        public MigracionCifrasControlInfo GuardarAnimalesSIAP(int organizacionId)
        {
            MigracionCifrasControlInfo resp;
            try
            {
                Logger.Info();
                // Se inicia la transaccion por Entrada validada
                //using (var transaction = new TransactionScope())
                TimeSpan ts1 = new TimeSpan(1, 00, 0);
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew, ts1))
                {
                    var migracionDal = new MigracionDAL();
                    resp = migracionDal.GuardarAnimalesSIAP(organizacionId);
                    transaction.Complete();
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
