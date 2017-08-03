using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using BLToolkit.Mapping;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class IncidenciasDAL : DALBase
    {
        internal List<AlertaInfo> ObtenerConfiguracionAlertas(EstatusEnum activo)
        {
            List<AlertaInfo> listaAlertaInfos = null;
            try
            {
                Dictionary<string, object> parameters = AuxIncidenciasDAL.ObtenerParametrosConfiguracionAlertas(activo);
                DataSet ds = Retrieve("AlertasConfiguracion_ObtenerTodasActivas", parameters);
                if (ValidateDataSet(ds))
                {
                    listaAlertaInfos = MapIncidenciasDAL.ObtenerConfiguracionAlertas(ds);
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
            return listaAlertaInfos;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal XDocument EjecutarQuery(string query)
        {
            XDocument xml = new XDocument();
            try
            {
                //Dictionary<string, object> parameters = AuxIncidenciasDAL.ObtenerParametrosConfiguracionAlertas(activo);
                DataSet ds = RetrieveConsulta(query);
                if (ValidateDataSet(ds))
                {
                    xml = MapIncidenciasDAL.ObtenerIncidenciasXML(ds);
                    //xml = ds.GetXml();
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
            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="usuarioCorporativo"></param>
        /// <returns></returns>
        public List<IncidenciasInfo> ObtenerIncidenciasPorOrganizacionID(int organizacionID, bool usuarioCorporativo)
        {
            List<IncidenciasInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIncidenciasDAL.ObtenerIncidenciasPorOrganizacionID(organizacionID, usuarioCorporativo);
                DataSet ds = Retrieve("Alertas_ObtenerIncidenciasPorOrganizacionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapIncidenciasDAL.ObtenerIncidenciasPorOrganizacionID(ds);
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IncidenciasInfo> ObtenerIncidenciasActivas()
        {
            List<IncidenciasInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIncidenciasDAL.ObtenerIncidenciasActivas();
                DataSet ds = Retrieve("Incidencia_ObtenerActivas", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapIncidenciasDAL.ObtenerIncidenciasActivas(ds);
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
        /// 
        /// </summary>
        /// <param name="listaNuevasIncidencias"></param>
        /// <param name="TipoFolioID"></param> 
        internal void GuardarNuevasIncidencias(List<IncidenciasInfo> listaNuevasIncidencias, int TipoFolioID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIncidenciasDAL.ObtenerParametrosGuardarNuevasIncidencias(listaNuevasIncidencias, TipoFolioID);
                Create("Incidencia_GuardarNuevasIncidencias", parameters);
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
        /// Proceso para actualizar la incidencia
        /// </summary>
        /// <returns></returns>
        public IncidenciasInfo ActualizarIncidencia(IncidenciasInfo incidencia)
        {
            IncidenciasInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIncidenciasDAL.ActualizarIncidencia(incidencia);
                Update("Incidencia_Actualizar", parameters);
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
        /// Proceso para rechazar la incidencia
        /// </summary>
        /// <returns></returns>
        public IncidenciasInfo RechazarIncidencia(IncidenciasInfo incidencia)
        {
            IncidenciasInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIncidenciasDAL.RechazarIncidencia(incidencia);
                Update("Incidencia_Rechazar", parameters);
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
        /// Proceso para autorizar la incidencia
        /// </summary>
        /// <returns></returns>
        public IncidenciasInfo AutorizarIncidencia(IncidenciasInfo incidencia)
        {
            IncidenciasInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIncidenciasDAL.AutorizarIncidencia(incidencia);
                Update("Incidencia_Cerrar", parameters);
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
        /// Proceso para obtener el seguimiento de la incidencia
        /// </summary>
        public List<IncidenciaSeguimientoInfo> ObtenerSeguimientoPorIncidenciaID(int incidenciaID)
        {
            List<IncidenciaSeguimientoInfo> listaSeguimiento = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIncidenciasDAL.ObtenerSeguimientoPorIncidenciaID(incidenciaID);
                DataSet ds = Retrieve("Incidencia_ObtenerSeguimiento", parameters);
                if (ValidateDataSet(ds))
                {
                    listaSeguimiento = MapIncidenciasDAL.ObtenerSeguimientoPorIncidenciaID(ds);
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
            return listaSeguimiento;
        }


        /// <summary>
        /// Proceso para cerrar una incidencia
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        internal void CerrarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIncidenciasDAL.ObtenerParametrosCerrarIncidencia(incidenciaInfo);
                Update("Incidencia_CerrarIncidencia", parameters);
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
        /// <param name="incidenciaInfo"></param>
        internal void IncidenciaVencida(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIncidenciasDAL.ObtenerParametrosIncidenciaVencida(incidenciaInfo);
                Update("Incidencia_VencidaIncidencia", parameters);
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
        /// <param name="incidenciaInfo"></param>
        internal void RegistrarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIncidenciasDAL.ObtenerParametrosRegistrarIncidencia(incidenciaInfo);
                Update("Incidencia_RegistarIncidencia", parameters);
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
