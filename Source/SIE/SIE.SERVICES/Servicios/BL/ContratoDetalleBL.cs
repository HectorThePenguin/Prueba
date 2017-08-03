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
    internal class ContratoDetalleBL
    {
        /// <summary>
        /// Metodo que obtiene un listado de contrato detalle por contrato id
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal List<ContratoDetalleInfo> ObtenerPorContratoId(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var contratoDetalleDAL = new ContratoDetalleDAL();
                List<ContratoDetalleInfo> result = contratoDetalleDAL.ObtenerPorContratoId(contratoInfo);
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
        /// Metodo que guarda un contrato detalle
        /// </summary>
        /// <returns></returns>
        internal int Guardar(List<ContratoDetalleInfo> listaContratoDetalleInfo, ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var contratoDetalleDal = new ContratoDetalleDAL();
                int result = contratoDetalleDal.Crear(listaContratoDetalleInfo, contratoInfo);
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
        /// Metodo que actualiza el estado de un contrato detalle
        /// </summary>
        /// <param name="info"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstado(ContratoDetalleInfo info, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var contratoDetalleDal = new ContratoDetalleDAL();
                contratoDetalleDal.ActualizarEstado(info, estatus);
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
