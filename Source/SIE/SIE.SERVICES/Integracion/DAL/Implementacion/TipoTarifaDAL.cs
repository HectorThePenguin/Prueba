
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
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TipoTarifaDAL :DALBase
    {
        /// <summary>
        /// Obtiene una lista de tipotarifainfo
        /// </summary>
        /// <returns></returns>
        internal List<TipoTarifaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var ds = Retrieve("TipoTarifa_ObtenerTodos");
                List<TipoTarifaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoTarifaDAL.ObtenerTodos(ds);
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
