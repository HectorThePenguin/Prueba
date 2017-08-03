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
    internal class FleteInternoDetalleDAL : DALBase
    {
        /// <summary>
        /// Crea un registro flete interno detalle
        /// </summary>
        /// <returns></returns>
        internal int Crear(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteInternoDetalleDAL.ObtenerParametrosCrearFleteInternoDetalle(fleteInternoDetalleInfo);
                int result = Create("FleteInternoDetalle_Crear", parameters);
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
        /// Actualizar flete interno detalle
        /// </summary>
        internal void Actualizar(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteInternoDetalleDAL.ObtenerParametrosActualizarFleteInternoDetalle(fleteInternoDetalleInfo);
                Update("FleteInternoDetalle_Actualizar", parameters);
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
        /// Metodo para obtener flete interno detalle por flete interno id
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoDetalleInfo> ObtenerPorFleteInternoId(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxFleteInternoDetalleDAL.ObtenerParametrosObtenerPorFleteInternoId(fleteInternoInfo);
                var ds = Retrieve("FleteInternoDetalle_ObtenerPorFleteInternoID", parameters);
                List<FleteInternoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteInternoDetalleDAL.ObtenerPorFleteInternoId(ds);
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
