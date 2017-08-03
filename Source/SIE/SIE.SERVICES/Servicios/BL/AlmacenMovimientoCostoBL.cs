using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    internal class AlmacenMovimientoCostoBL
    {

        /// <summary>
        /// Crea un registro en almacen movimiento costo
        /// </summary>
        /// <returns></returns>
        internal int Crear(AlmacenMovimientoCostoInfo almacenMovimientoCostoInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoCostoDal = new AlmacenMovimientoCostoDAL();
                int result = almacenMovimientoCostoDal.Crear(almacenMovimientoCostoInfo);
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
        /// Crea registros en AlmacenMovimientoCosto con un xml
        /// </summary>
        /// <returns></returns>
        internal int CrearCostos(List<AlmacenMovimientoCostoInfo> almacenMovimientoCostoInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoCostoDal = new AlmacenMovimientoCostoDAL();
                int result = almacenMovimientoCostoDal.CrearCostos(almacenMovimientoCostoInfo);
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
        /// Obtiene un listado por almacen movimiento id
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoCostoInfo> ObtenerPorAlmacenMovimientoId(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoCostoDAL = new AlmacenMovimientoCostoDAL();
                List<AlmacenMovimientoCostoInfo> result = almacenMovimientoCostoDAL.ObtenerPorAlmacenMovimientoId(almacenMovimientoInfo);
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
        /// Obtiene una lista de movimientos costos por almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<AlmacenMovimientoCostoInfo> ObtenerAlmacenMovimientoCostoPorContratoXML(List<ContratoInfo> contratosParciales)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoCostoDAL = new AlmacenMovimientoCostoDAL();
                IEnumerable<AlmacenMovimientoCostoInfo> result =
                    almacenMovimientoCostoDAL.ObtenerAlmacenMovimientoCostoPorContratoXML(contratosParciales);
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
        /// Obtiene un listado por contrato id
        /// </summary>
        /// <param name="contratoID"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoCostoInfo> ObtenerPorContratoID(int contratoID)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoCostoDAL = new AlmacenMovimientoCostoDAL();
                List<AlmacenMovimientoCostoInfo> result = almacenMovimientoCostoDAL.ObtenerPorContratoID(contratoID);
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
        /// Obtiene un enumerable de almacen movimiento costo
        /// </summary>
        /// <param name="movimientosAlmacen"></param>
        /// <returns></returns>
        internal IEnumerable<AlmacenMovimientoCostoInfo> ObtenerAlmacenMovimientoCostoPorAlmacenMovimientoXML(IEnumerable<AlmacenMovimientoInfo> movimientosAlmacen)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoCostoDAL = new AlmacenMovimientoCostoDAL();
                IEnumerable<AlmacenMovimientoCostoInfo> result =
                    almacenMovimientoCostoDAL.ObtenerAlmacenMovimientoCostoPorAlmacenMovimientoXML(movimientosAlmacen);
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
    }
}
