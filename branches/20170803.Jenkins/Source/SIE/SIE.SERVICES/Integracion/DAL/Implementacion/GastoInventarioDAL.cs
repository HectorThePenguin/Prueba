using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
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
    /// <summary>
    /// Calse que representa el acceso a datos de gasto al inventario
    /// </summary>
    internal class GastoInventarioDAL : DALBase
    {
        /// <summary>
        /// Metodo para almacenar el gasto al inventario
        /// </summary>
        /// <param name="gasto"></param>
        public int Guardar(GastoInventarioInfo gasto)
        {
            int gastoInventarioID;
            try
            {
                Logger.Info();
                var parameters = AuxGastoInventarioDAL.ObtenerParametrosGuardarGastoInventario(gasto);
                gastoInventarioID = Create("GastoInventario_GuardarGasto", parameters);
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
            return gastoInventarioID;
        }

        /// <summary>
        /// Metodo para almacenar el gasto al inventario
        /// </summary>
        /// <param name="gastoInventarioID"></param>
        public GastoInventarioInfo ObtenerPorID(int gastoInventarioID)
        {
            GastoInventarioInfo gastoInventario = null;
            try
            {
                Logger.Info();
                var parameters = AuxGastoInventarioDAL.ObtenerParametrosObtenerPorID(gastoInventarioID);
                DataSet ds = Retrieve("GastoInventario_ObtenerPorID", parameters);
                if(ValidateDataSet(ds))
                {
                    gastoInventario = MapGastoInventarioDAL.ObtenerPorID(ds);
                }
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
            return gastoInventario;
        }

        /// <summary>
        /// Obtiene una coleccion de gastos por inventario
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal IEnumerable<GastoInventarioInfo> ObtenerGastosInventarioPorFechaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<GastoInventarioInfo> mapeo = MapGastoInventarioDAL.ObtenerGastosInventarioConciliacion();
                IEnumerable<GastoInventarioInfo> foliosFaltantes = GetDatabase().ExecuteSprocAccessor
                    <GastoInventarioInfo>(
                        "GastoInventario_ObtenerPorFechaConciliacion", mapeo.Build(),
                        new object[] { organizacionID, fechaInicial, fechaFinal});
                return foliosFaltantes;
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
