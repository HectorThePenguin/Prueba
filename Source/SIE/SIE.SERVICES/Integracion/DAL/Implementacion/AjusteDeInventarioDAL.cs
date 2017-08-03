using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AjusteDeInventarioDAL : DALBase
    {
        /// <summary>
        /// Obtiene diferencias de inventario
        /// </summary>
        /// <returns></returns>
        internal List<AjusteDeInventarioDiferenciasInventarioInfo> ObtenerDiferenciasInventario(AlmacenMovimientoInfo almacenMovimientoInfo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAjusteDeInventarioDAL.ObtenerParametrosObtenerDiferenciasInventario(almacenMovimientoInfo, organizacionID);
                var ds = Retrieve("AjusteDeInventario_ObtenerDiferenciasInventarios", parameters);
                List<AjusteDeInventarioDiferenciasInventarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAjusteDeInventarioDAL.ObtenerDiferenciasInventario(ds);
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
    }
}
