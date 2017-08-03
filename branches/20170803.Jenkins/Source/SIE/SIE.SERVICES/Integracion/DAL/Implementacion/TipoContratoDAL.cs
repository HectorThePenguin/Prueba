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
    internal class TipoContratoDAL : DALBase
    {
        /// <summary>
        /// Obtiene todos los tipos de contrato
        /// </summary>
        /// <returns>Lista de TipoContratoInfo</returns>
        internal List<TipoContratoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>();
                var ds = Retrieve("TipoContrato_ObtenerTodos", parameters);
                List<TipoContratoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoContratoDAL.ObtenerTodos(ds);
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
        /// Obtiene un tipo de contrato por id
        /// </summary>
        /// <returns>Lista de TipoContratoInfo</returns>
        internal TipoContratoInfo ObtenerPorId(int tipoContratoId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoContrato.ObtenerParametrosObtenerTipoContratoPorId(tipoContratoId);
                var ds = Retrieve("TipoContrato_ObtenerPorID", parameters);
                TipoContratoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoContratoDAL.ObtenerPorId(ds);
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
