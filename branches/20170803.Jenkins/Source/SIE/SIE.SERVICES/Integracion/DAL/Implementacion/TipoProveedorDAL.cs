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
    internal class TipoProveedorDAL : DALBase
    {
        /// <summary>
        ///     Obtiene una lista de todos los TipoProveedores
        /// </summary>
        /// <returns></returns>
        internal List<TipoProveedorInfo> ObtenerTodos()
        {
            List<TipoProveedorInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoProveedor_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProveedorDAL.ObtenerTodos(ds);
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
            return result;
        }

        /// <summary>
        ///  Obtiene una lista de TipoProveedor filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<TipoProveedorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<TipoProveedorInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProveedorDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoProveedor_ObtenerTodos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProveedorDAL.ObtenerTodos(ds);
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
            return result;
        }
    }
}

