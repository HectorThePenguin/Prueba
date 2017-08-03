using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ContratoParcialBL
    {
        /// <summary>
        /// Crea registros en AlmacenMovimientoCosto con un xml
        /// </summary>
        /// <returns></returns>
        internal int CrearContratoParcial(List<ContratoParcialInfo> listaContratoParcialInfo)
        {
            try
            {
                Logger.Info();
                var contratoParcialDal = new ContratoParcialDAL();
                int result = contratoParcialDal.CrearContratoParcial(listaContratoParcialInfo);
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
        /// Obtiene una lista de parcialidades
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<ContratoParcialInfo> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                var contratoParcialDal = new ContratoParcialDAL();
                return contratoParcialDal.ObtenerPorContratoId(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Metodo que actualiza el estado de un contrato detalle
        /// </summary>
        /// <param name="info"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstado(ContratoParcialInfo info, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var contratoParcialDal = new ContratoParcialDAL();
                contratoParcialDal.ActualizarEstado(info, estatus);
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

        internal List<ContratoParcialInfo> ObtenerFaltantePorContratoId(ContratoInfo contratoInfo)
        {
            try
            {
                var contratoParcialDal = new ContratoParcialDAL();
                return contratoParcialDal.ObtenerFaltantePorContratoId(contratoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
