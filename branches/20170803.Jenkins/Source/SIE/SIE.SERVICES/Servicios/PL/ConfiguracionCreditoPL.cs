using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionCreditoPL
    {
        public ResultadoInfo<ConfiguracionCreditoInfo> ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(PaginacionInfo pagina, ConfiguracionCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var bl = new ConfiguracionCreditoBL();
                var result = bl.ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(pagina, filtro);
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

        public int ConfiguracionCredito_ObtenerPorTipoCreditoYMes(ConfiguracionCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var bl = new ConfiguracionCreditoBL();
                var result = bl.ConfiguracionCredito_ObtenerPorTipoCreditoYMes(info);
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

        public bool ConfiguracionCredito_Guardar(ConfiguracionCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var bl = new ConfiguracionCreditoBL();
                var result = bl.ConfiguracionCredito_Guardar(info);
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

        public List<ConfiguracionCreditoRetencionesInfo> ConfiguracionCredito_ObtenerRetencionesPorID(int configuracionCreditoID)
        {
            try
            {
                Logger.Info();
                var bl = new ConfiguracionCreditoBL();
                var result = bl.ConfiguracionCredito_ObtenerRetencionesPorID(configuracionCreditoID);
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
