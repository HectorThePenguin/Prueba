using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Auxiliar;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Info.Enums;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TipoCostoCentroDAL : DALBase
    {
        /// <summary>
        /// Obtener los tipos de costos de centro
        /// </summary>
        /// <param name="estatusEnum"></param>
        /// <returns></returns>
        internal List<TipoCostoCentroInfo> ObtenerTodos(EstatusEnum estatusEnum)
        {
            try
            {
                Logger.Info();
                var parameters = AuxTipoCostoCentroDAL.ObtenerParametrosObtenerTodos(estatusEnum);
                var ds = Retrieve("TipoCostoCentro_ObtenerTodos", parameters);
                List<TipoCostoCentroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCostoCentroDAL.ObtenerTodos(ds);
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
