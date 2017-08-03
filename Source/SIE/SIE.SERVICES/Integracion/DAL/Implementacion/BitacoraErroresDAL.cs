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
    public class BitacoraErroresDAL : DALBase
    {
        /// <summary>
        /// Guarda un error en la bitacora
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        internal int GuardarError(BitacoraErroresInfo errorInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBitacoraErroresDAL.ObtenerParametrosGuardarError(errorInfo);
                int result = Create("BitacoraErrores_Guardar", parameters);
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
        /// Obtiene la lista a los usuarios que se les notificara por la accion
        /// </summary>
        /// <param name="accionSiap">Accion siap</param>
        /// <returns></returns>
        internal List<NotificacionesInfo> ObtenerNotificacionesPorAcciones(AccionesSIAPEnum accionSiap)
        {
            List<NotificacionesInfo> resultado = null;
            try
            {

                Logger.Info();
                Dictionary<string, object> parameters = AuxBitacoraErroresDAL.ObtenerParametrosNotificacionesPorAcciones(accionSiap);

                DataSet ds = Retrieve("Notificacion_ObtenerNotificacionesPorAccion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapBitacoraErroresDAL.ObtnerNotificacionesPorAccion(ds);
                }
                return resultado;
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
        /// 
        /// </summary>
        /// <param name="bitacora"></param>
        /// <returns></returns>
        internal int GuardarErrorIncidencia(BitacoraIncidenciaInfo bitacora)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxBitacoraErroresDAL.ObtenerParametrosGuardarErrorIncidencia(bitacora);
                int result = Create("BitacoraIncidencias_Guardar", parameters);
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
