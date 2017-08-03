using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Base.Integracion.DAL;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CierreDiaInventarioDAL : DALBase
    {
        /// <summary>
        /// Guardar productos de cierre dia inventario
        /// </summary>
        /// <param name="datosGrid"></param>
        /// <param name="almacenCierreDiaInventarioInfo"></param>
        /// <returns></returns>
        public int GuardarProductosCierreDiaInventario(IList<AlmacenCierreDiaInventarioInfo> datosGrid, AlmacenCierreDiaInventarioInfo almacenCierreDiaInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCierreDiaInventarioDAL.ObtenerParametrosGuardarProductosCierreDiaInventario(datosGrid, almacenCierreDiaInventarioInfo);
                var result = Create("CierreDiaInventario_GuardarAlmacenMovimientoDetalle", parameters);

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
        /// guardar almacen movimiento
        /// </summary>
        /// <param name="almacenCierreFolio"></param>
        /// <returns></returns>
        public AlmacenCierreDiaInventarioInfo GuardarAlmacenMovimiento(AlmacenCierreDiaInventarioInfo almacenCierreFolio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCierreDiaInventarioDAL.ObtenerParametrosObtenerGuardarAlmacenMovimiento(almacenCierreFolio);
                DataSet ds = Retrieve("CierreDiaInventario_GuardarAlmacenMovimiento", parameters);
                AlmacenCierreDiaInventarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCierreDiaInventarioDAL.ObtenerCierreAlmacenMovimientoInfo(ds);
                }
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
        /// Descuenta Almacen Inventario
        /// </summary>
        /// <param name="listaDiferencia"></param>
        internal void DescontarAlmacenInventario(IList<AlmacenCierreDiaInventarioInfo> listaDiferencia)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCierreDiaInventarioDAL.ObtenerParametrosDescontarAlmacenInventario(listaDiferencia);
                Create("CierreDiaInventario_DescontarAlmacenInventario", parameters);
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
