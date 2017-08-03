using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CrearContratoPL
    {
        /// <summary>
        /// Metodo que guarda un contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="listaOtrosCostos"></param>
        /// <returns></returns>
        public ContratoInfo Guardar(ContratoInfo contratoInfo, List<CostoInfo> listaOtrosCostos)
        {
            try
            {
                Logger.Info();
                var contratoBl = new CrearContratoBL();
                var result = contratoBl.Guardar(contratoInfo, listaOtrosCostos);
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
        /// Metodo que actualiza un estado
        /// </summary>
        /// <param name="contratoInfo"></param>
        public void ActualizarEstado(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var crearContratoBl = new CrearContratoBL();
                crearContratoBl.ActualizarEstado(contratoInfo);
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
        /// Imprime el ticket 
        /// </summary>
        public void ImprimirContrato(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var crearContratoBL = new CrearContratoBL();
                crearContratoBL.ImprimirContrato(contratoInfo);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
