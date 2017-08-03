using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AlmacenMovimientoCostoPL
    {
        /// <summary>
        /// Obtener costos por almacen movimiento id
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public List<AlmacenMovimientoCostoInfo> ObtenerPorAlmacenMovimientoId(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            List<AlmacenMovimientoCostoInfo> info;
            try
            {
                Logger.Info();
                var almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                info = almacenMovimientoCostoBl.ObtenerPorAlmacenMovimientoId(almacenMovimientoInfo);
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
            return info;
        }

        /// <summary>
        /// Obtiene una lista de movimientos costos por almacen movimiento
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AlmacenMovimientoCostoInfo> ObtenerAlmacenMovimientoCostoPorContratoXML(List<ContratoInfo> contratosParciales)
        {
            IEnumerable<AlmacenMovimientoCostoInfo> info;
            try
            {
                Logger.Info();
                var almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                info = almacenMovimientoCostoBl.ObtenerAlmacenMovimientoCostoPorContratoXML(contratosParciales);
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
            return info;
        }

        /// <summary>
        /// Obtener costos por contrato id
        /// </summary>
        /// <param name="contratoID"></param>
        /// <returns></returns>
        public List<AlmacenMovimientoCostoInfo> ObtenerPorContratoID(int contratoID)
        {
            List<AlmacenMovimientoCostoInfo> info;
            try
            {
                Logger.Info();
                var almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                info = almacenMovimientoCostoBl.ObtenerPorContratoID(contratoID);
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
            return info;
        }
    }
}
