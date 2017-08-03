using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    class ConfiguracionAlertasDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos de la consulta al procedmiento almacenado ConfigurarAlertasConsulta 
        /// </summary>
        /// <param name="paginas"></param>
        /// <param name="filtros"></param>
        /// <returns>regresa los datos consultados</returns>
        internal ResultadoInfo<ConfiguracionAlertasGeneraInfo> ConsultarConfiguracionAlertas(PaginacionInfo paginas, ConfiguracionAlertasGeneraInfo filtros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.ConsultarConfiguracionAlertas(paginas, filtros);
                DataSet ds = Retrieve("ConfiguracionAlertasConsulta", parameters);

                ResultadoInfo<ConfiguracionAlertasGeneraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionAlertasDAL.ConsultarConfiguracionAlertas(ds);
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
        /// Registra una nueva configuracion alerta en la tabla AlertaConfiguracion
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>regresa los datos consultados</returns>
        internal bool InsertarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.InsertarConfiguracionAlerta(filtros);
                DataSet ds = Retrieve("ConfiguracionAlertas_CrearNueva", parameters);
                return true;
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
        /// Edita alguna configuracion alerta en la tabla AlertaConfiguracion
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal bool EditarConfiguracionAlerta(ConfiguracionAlertasGeneraInfo filtros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.EditarConfiguracionAlerta(filtros);
                DataSet ds = Retrieve("ConfiguracionAlertas_Update", parameters);
                return true;
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
        /// Obtiene un Folio por Id
        /// </summary>
        /// <returns>regresa el Las alertas consultadas</returns>
        internal List<AlertaInfo> ObtenerTodasLasAlertasActivas()
        {
            List<AlertaInfo> result;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.ObtenerTodasLasAlertasActivas();
                DataSet ds = Retrieve("Alertas_ObtenerTodas", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionAlertasDAL.ObtenerTodasLasAlertasActivas(ds);
                }
                else
                {
                    result = new List<AlertaInfo>();
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
        /// Obtene todas las acciones activas
        /// </summary>
        /// <returns>regresa el Acciones consultadas</returns>
        internal List<AccionInfo> ObtenerTodasLasAccionesActivas()
        {
            List<AccionInfo> result;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.ObtenerTodasLasAccionesActivas();
                DataSet ds = Retrieve("Acciones_ObtenerTodasLasActivas", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionAlertasDAL.ObtenerTodasLasAccionesActivas(ds);
                }
                else
                {
                    result = new List<AccionInfo>();
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
        /// Metodo que obtiene las acciones que estan ligadas a una IDAlerta en especifico
        /// para cargar las acciones al editar una configuración alerta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista Alerta Accion</returns>
        internal List<AlertaAccionInfo> ObtenerListaAcciones(int id)
        {
            List<AlertaAccionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.ObtenerListaAcciones(id);
                DataSet ds = Retrieve("ConfiguracionAlertasConsultaAccion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionAlertasDAL.ObtenerListaAcciones(ds);
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
        /// obtiene una alerta por medio de su ID
        /// </summary>
        /// <param name="idAlerta"></param>
        /// <returns>regresa el folio consultado</returns>
        internal AlertaInfo ObtenerAlertaPorId(long idAlerta)
        {
            AlertaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.ConsultaAlerta(idAlerta);
                DataSet ds = Retrieve("ConfiguracionAlerta_ObtenerAlertaPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionAlertasDAL.ObtenerAlertaPorId(ds);
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
        /// Obtiene por paginado las alertas activas para la ayuda de configuracion alerta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns>una lista de folios del dia</returns>
        internal ResultadoInfo<AlertaInfo> ObtenerPorPaginaFiltroAlertas(PaginacionInfo pagina, AlertaInfo filtro)
        {
            ResultadoInfo<AlertaInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionAlertasDAL.ObtenerParametrosPorPaginaFiltroAlertas(pagina, filtro);
                DataSet ds = Retrieve("ConfiguracionAlertas_ObtenerAlertasPorPaginas", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapConfiguracionAlertasDAL.ObtenerAlertaPorPaginaCompleto(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return lista;
        }

        /// <summary>
        /// este metodo recibe un query que se validara por medio de su ejecucion 
        /// si este se ejecuta bien entonces podra registrar una nueva configuracion alerta
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool EjecutarQuery(string query)
        {
            try
            {
                DataSet ds = RetrieveConsulta(query);
                    return true;
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
