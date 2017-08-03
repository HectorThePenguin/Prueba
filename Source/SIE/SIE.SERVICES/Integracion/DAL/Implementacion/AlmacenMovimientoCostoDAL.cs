using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AlmacenMovimientoCostoDAL : DALBase
    {
        /// <summary>
        /// Crear un registro un registro en almacen movimiento costo
        /// </summary>
        /// <param name="almacenMovimientoCostoInfo"></param>
        /// <returns></returns>
        internal int Crear(AlmacenMovimientoCostoInfo almacenMovimientoCostoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoCostoDAL.ObtenerParametrosCrear(almacenMovimientoCostoInfo);
                int result = Create("AlmacenMovimientoCosto_Crear", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Crea registros en AlmacenMovimientoCosto por Xml
        /// </summary>
        /// <param name="almacenMovimientoCostoInfo"></param>
        /// <returns></returns>
        internal int CrearCostos(List<AlmacenMovimientoCostoInfo> almacenMovimientoCostoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoCostoDAL.ObtenerParametrosCrearCostos(almacenMovimientoCostoInfo);
                int result = Create("AlmacenMovimientoCosto_CrearCostos", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de almacen movimiento costo
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoCostoInfo> ObtenerPorAlmacenMovimientoId(AlmacenMovimientoInfo almacenMovimientoId)
        {
            List<AlmacenMovimientoCostoInfo> almacenMovimientoCostoInfo = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenMovimientoCostoDAL.ObtenerParametrosObtenerPorAlmacenMovimientoId(almacenMovimientoId);
                DataSet ds = Retrieve("AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoId", parametros);
                if (ValidateDataSet(ds))
                {
                    almacenMovimientoCostoInfo = MapAlmacenMovimientoCostoDAL.ObtenerPorAlmacenMovimientoId(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenMovimientoCostoInfo;
        }

        /// <summary>
        /// Obtiene una lista de movimientos costos por almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<AlmacenMovimientoCostoInfo> ObtenerAlmacenMovimientoCostoPorContratoXML(List<ContratoInfo> contratosParciales)
        {
            try
            {
                string parametro = AuxAlmacenMovimientoCostoDAL.ObtenerParametrosAlmacenMovimientoCostoPorContratoXML(contratosParciales);
                IMapBuilderContext<AlmacenMovimientoCostoInfo> mapeo = MapAlmacenMovimientoCostoDAL.ObtenerAlmacenMovimientoCostoPorContratoXML();
                IEnumerable<AlmacenMovimientoCostoInfo> almacenMovimientoCostoPorAlmacenMovimiento = GetDatabase().
                    ExecuteSprocAccessor
                    <AlmacenMovimientoCostoInfo>(
                        "AlmacenMovimientoCosto_ObtenerPorContratoXML", mapeo.Build(),
                        new object[] {parametro});
                return almacenMovimientoCostoPorAlmacenMovimiento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de almacen movimiento costo
        /// </summary>
        /// <param name="contratoID"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoCostoInfo> ObtenerPorContratoID(int contratoID)
        {
            List<AlmacenMovimientoCostoInfo> almacenMovimientoCostoInfo = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenMovimientoCostoDAL.ObtenerParametrosObtenerPorContratoID(contratoID);
                DataSet ds = Retrieve("AlmacenMovimientoCosto_ObtenerPorContratoID", parametros);
                if (ValidateDataSet(ds))
                {
                    almacenMovimientoCostoInfo = MapAlmacenMovimientoCostoDAL.ObtenerPorContratoID(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenMovimientoCostoInfo;
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
                IMapBuilderContext<AlmacenMovimientoCostoInfo> mapeo =
                    MapAlmacenMovimientoCostoDAL.ObtenerMapeoAlmacenMovimientoCosto();
                string parametro =
                    AuxAlmacenMovimientoCostoDAL.ObtenerParametrosObtenerAlmacenMovimientoXML(movimientosAlmacen);
                IEnumerable<AlmacenMovimientoCostoInfo> corrales = GetDatabase().ExecuteSprocAccessor
                    <AlmacenMovimientoCostoInfo>(
                        "AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML", mapeo.Build(),
                        new object[] { parametro });
                return corrales;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
