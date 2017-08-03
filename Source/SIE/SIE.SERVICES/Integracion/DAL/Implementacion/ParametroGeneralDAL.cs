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
    internal class ParametroGeneralDAL : DALBase
    {
        /// <summary>
        /// Obtiene una entidad por organizacion y clave parametro
        /// </summary>
        /// <param name="claveParametro"></param>
        /// <returns></returns>
        internal ParametroGeneralInfo ObtenerPorClaveParametro(string claveParametro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroGeneralDAL.ObtenerParametrosPorClaveParametro(claveParametro);
                using (IDataReader reader = RetrieveReader("ParametroGeneral_ObtenerPorClave", parameters))
                {
                    ParametroGeneralInfo result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapParametroGeneralDAL.ObtenerPorClaveParametro(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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
        }

        /// <summary>
        /// Obtiene una entidad por organizacion y clave parametro
        /// </summary>
        /// <param name="claveParametro"></param>
        /// <returns></returns>
        internal ParametroGeneralInfo ObtenerPorClaveParametroActivo(string claveParametro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroGeneralDAL.ObtenerParametrosPorClaveParametro(claveParametro);
                using (IDataReader reader = RetrieveReader("ParametroGeneral_ObtenerPorClaveActivo", parameters))
                {
                    ParametroGeneralInfo result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapParametroGeneralDAL.ObtenerPorClaveParametro(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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
        }
    }
}
