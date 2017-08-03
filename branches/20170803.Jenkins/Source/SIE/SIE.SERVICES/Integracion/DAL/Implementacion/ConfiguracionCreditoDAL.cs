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
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ConfiguracionCreditoDAL : DALBase
    {
        internal ResultadoInfo<ConfiguracionCreditoInfo> ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(PaginacionInfo pagina, ConfiguracionCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parameters = AuxConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(pagina, filtro);
                DataSet ds = Retrieve("ConfiguracionCredito_ObtenerConfiguracionCreditoPorPagina", parameters);
                ResultadoInfo<ConfiguracionCreditoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(ds);
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

        internal int ConfiguracionCredito_ObtenerPorTipoCreditoYMes(ConfiguracionCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parameters = AuxConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerPorTipoCreditoYMes(filtro);
                DataSet ds = Retrieve("ConfiguracionCredito_ObtenerPorTipoCreditoYMes", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerPorTipoCreditoYMes(ds);
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

        internal bool ConfiguracionCredito_Crear(ConfiguracionCreditoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerParametrosCrear(info);
                Create("[dbo].[ConfiguracionCredito_Crear]", parameters);
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

            return true;
        }

        internal bool ConfiguracionCredito_Actualizar(ConfiguracionCreditoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerParametrosActualizar(info);
                Update("[dbo].[ConfiguracionCredito_Actualizar]", parameters);
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

            return true;
        }

        internal List<ConfiguracionCreditoRetencionesInfo> ConfiguracionCredito_ObtenerRetencionesPorID(int configuracionCreditoID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerRetencionesPorID(configuracionCreditoID);
                DataSet ds = Retrieve("ConfiguracionCredito_ObtenerRetencionesPorID", parameters);
                var result = new List<ConfiguracionCreditoRetencionesInfo>();
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionCreditoDAL.ConfiguracionCredito_ObtenerRetencionesPorID(ds);
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