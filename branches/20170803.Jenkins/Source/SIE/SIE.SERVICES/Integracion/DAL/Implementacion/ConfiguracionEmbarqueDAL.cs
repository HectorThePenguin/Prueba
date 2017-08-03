using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
    internal class ConfiguracionEmbarqueDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear un registro de ConfiguracionEmbarque
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(ConfiguracionEmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerParametrosCrear(info);
                Create("ConfiguracionEmbarque_Crear", parameters);
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
        ///     Metodo que actualiza un ConfiguracionEmbarque
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(ConfiguracionEmbarqueInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerParametrosActualizar(info);
                Update("ConfiguracionEmbarque_Actualizar", parameters);
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
        ///     Obtiene una lista de todos los ConfiguracionEmbarquees
        /// </summary>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueInfo> ObtenerTodos()
        {
            List<ConfiguracionEmbarqueInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("[dbo].[ConfiguracionEmbarque_ObtenerTodos]");
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionEmbarqueDAL.ObtenerTodos(ds);
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
        ///  Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("[dbo].[ConfiguracionEmbarque_ObtenerTodos]", parameters);
                List<ConfiguracionEmbarqueInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionEmbarqueDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de la Configuracion Embarque por Id
        /// </summary>
        /// <returns></returns>
        internal ConfiguracionEmbarqueInfo ObtenerPorID(int id)
        {
            ConfiguracionEmbarqueInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerParametroPorID(id);
                DataSet ds = Retrieve("[dbo].[ConfiguracionEmbarque_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionEmbarqueDAL.ObtenerPorID(ds);
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
        ///     Obtiene la configuración de embarque
        /// </summary>
        /// <param name="organizacionOrigenId">Organización origen de la configuración</param>
        /// <param name="organizacionDestinoId">Organización destino de la configuración</param>
        /// <returns></returns>
        internal ConfiguracionEmbarqueInfo ObtenerPorOrganizacion(int organizacionOrigenId, int organizacionDestinoId)
        {
            ConfiguracionEmbarqueInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerParametrosPorOrganizacion(organizacionOrigenId, organizacionDestinoId);
                DataSet ds = Retrieve("[dbo].[ConfiguracionEmbarque_ObtenerPorOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionEmbarqueDAL.ObtenerPorID(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<ConfiguracionEmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionEmbarqueInfo filtro)
        {
            ResultadoInfo<ConfiguracionEmbarqueInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ConfiguracionEmbarque_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapConfiguracionEmbarqueDAL.ObtenerPorPagina(ds);
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
            return lista;
        }

        /// <summary>
        ///     Obtiene una lista de las Rutas
        /// </summary>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorId(ConfiguracionEmbarqueInfo filtro)
        {
            List<ConfiguracionEmbarqueDetalleInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerRutasPorId(filtro);
                DataSet ds = Retrieve("[dbo].[ProgramacionEmbarque_ObtenerRutaPorId]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionEmbarqueDAL.ObtenerRutasPorDescripcion(ds);
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
        ///     Obtiene una lista de las Rutas
        /// </summary>
        /// <returns></returns>
        internal List<ConfiguracionEmbarqueDetalleInfo> ObtenerRutasPorDescripcion(ConfiguracionEmbarqueInfo filtro)
        {
            List<ConfiguracionEmbarqueDetalleInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionEmbarqueDAL.ObtenerParametrosRutasPorDescripcion(filtro);
                DataSet ds = Retrieve("[dbo].[ProgramacionEmbarque_ObtenerRutaPorOrigenDestino]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionEmbarqueDAL.ObtenerRutasPorDescripcion(ds);
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