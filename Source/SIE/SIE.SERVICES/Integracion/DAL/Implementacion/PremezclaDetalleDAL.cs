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
    internal class PremezclaDetalleDAL : DALBase
    {
        /// <summary>
        /// Crea premezcla detalle a partir de una lista
        /// </summary>
        /// <param name="listaPremezclaDetalle"></param>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        internal int Crear(List<PremezclaDetalleInfo> listaPremezclaDetalle, PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPremezclaDetalleDAL.ObtenerParametrosCrearPremezclaDetalle(listaPremezclaDetalle, premezclaInfo);
                int result = Create("PremezclaDetalle_Crear", parameters);
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
        /// Actualizar premezcla detalle
        /// </summary>
        /// <param name="listaPremezclaDetalle"></param>
        /// <returns></returns>
        internal void Actualizar(List<PremezclaDetalleInfo> listaPremezclaDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPremezclaDetalleDAL.ObtenerParametrosActualizarPremezclaDetalle(listaPremezclaDetalle);
                Update("PremezclaDetalle_Actualizar", parameters);
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
        /// Obtiene premezcla detalle
        /// </summary>
        /// <returns></returns>
        internal List<PremezclaDetalleInfo> ObtenerPremezclaDetallePorPremezclaId(PremezclaInfo premezclaInfo)
        {
            try
            {
                List<PremezclaDetalleInfo> result = null;
                try
                {
                    Logger.Info();
                    Dictionary<string, object> parametros =
                        AuxPremezclaDetalleDAL.ObtenerPremezclaDetallePorPremezclaId(premezclaInfo);
                    DataSet ds = Retrieve("PremezclaDetalle_ObtenerPorPremezclaID", parametros);
                    if (ValidateDataSet(ds))
                    {
                        result = MapPremezclaDetalleDAL.ObtenerPremezclaDetallePorPremezclaId(ds);
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
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
