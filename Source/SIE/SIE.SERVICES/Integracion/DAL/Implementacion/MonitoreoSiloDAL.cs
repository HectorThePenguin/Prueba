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
    internal class MonitoreoSiloDAL : DALBase
    {
        //Obtener la temperatura maxima
        internal int ObtenerTemperaturaMax(int organizacionID, string descripcion)
        {
            try
            {
                Logger.Info();
                var parameters = AuxMonitoreoSiloDAL.ObtenerTemperaturaMax(organizacionID, descripcion);
                var result = Create("ParametroOrganizacion_ObtenerValor", parameters);

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

        internal List<MonitoreoSiloInfo> ObtenerSilosParaMonitoreo(int organizacionID)
        {
            List<MonitoreoSiloInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonitoreoSiloDAL.ObtenerSilosParaMonitoreo(organizacionID);
                DataSet ds = Retrieve("Silo_ObtenerPorOrganizacionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapMonitoreoSiloDAL.ObtenerSilosParaMonitoreo(ds);
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

        internal List<MonitoreoSiloInfo> ObtenerOrdenAlturaMonitoreoGrid(int organizacionID)
        {
            List<MonitoreoSiloInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonitoreoSiloDAL.ObtenerSilosParaMonitoreo(organizacionID);
                DataSet ds = Retrieve("Silo_ObtenerAlturaUbicacionSensor", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapMonitoreoSiloDAL.ObtenerOrdenAlturaMonitoreoGrid(ds);
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

        // Guarda un analisis de grasa executando el sp
        internal bool Guardar(List<MonitoreoSiloInfo> listaMonitoreoSilo, MonitoreoSiloInfo silosInfo, int organizacionID)
        {
            bool result = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonitoreoSiloDAL.Guardar(listaMonitoreoSilo, silosInfo, organizacionID);
                DataSet ds = Retrieve("MonitoreoSilo_GuardarMonitoreoSiloDetalle", parameters);
                if (ValidateDataSet(ds))
                {
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
