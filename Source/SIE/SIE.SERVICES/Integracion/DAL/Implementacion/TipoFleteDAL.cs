using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TipoFleteDAL : DALBase
    {
        /// <summary>
        /// Metrodo Para obtener Peso
        /// </summary>
        internal List<TipoFleteInfo> ObtenerTiposFleteTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parameters = AuxTipoFleteDAL.ObtenerTodos(estatus);
                var ds = Retrieve("TipoFlete_ObtenerTodos", parameters);
                List<TipoFleteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoFleteDAL.ObtenerTodos(ds);
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal TipoFleteInfo ObtenerPorId(int tipoFleteId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxTipoFleteDAL.ObtenerPorId(tipoFleteId);
                var ds = Retrieve("TipoFlete_ObtenerPorID", parameters);
                TipoFleteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoFleteDAL.ObtenerPorId(ds);
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
