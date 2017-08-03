using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class ConfiguracionCreditoBL
    {
        internal ResultadoInfo<ConfiguracionCreditoInfo> ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(PaginacionInfo pagina, ConfiguracionCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var dal = new ConfiguracionCreditoDAL();
                var result = dal.ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
                var dal = new ConfiguracionCreditoDAL();
                var result = dal.ConfiguracionCredito_ObtenerPorTipoCreditoYMes(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal bool ConfiguracionCredito_Guardar(ConfiguracionCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var dal = new ConfiguracionCreditoDAL();
                var result = true;
                if (info.ConfiguracionCreditoID == 0)
                {
                    result = dal.ConfiguracionCredito_Crear(info);
                }
                else
                {
                    result = dal.ConfiguracionCredito_Actualizar(info);
                }
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<ConfiguracionCreditoRetencionesInfo> ConfiguracionCredito_ObtenerRetencionesPorID(int configuracionCreditoID)
        {
            try
            {
                Logger.Info();
                var dal = new ConfiguracionCreditoDAL();
                var result = dal.ConfiguracionCredito_ObtenerRetencionesPorID(configuracionCreditoID);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}