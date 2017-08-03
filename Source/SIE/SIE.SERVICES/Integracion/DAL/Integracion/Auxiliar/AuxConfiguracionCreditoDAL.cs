using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxConfiguracionCreditoDAL
    {
        internal static Dictionary<string, object> ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(PaginacionInfo pagina, ConfiguracionCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", filtro.TipoCredito.Descripcion},
                            {"@Activo",filtro.Activo.GetHashCode()},
                            {"@Inicio",pagina.Inicio},
                            {"@Limite",pagina.Limite}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ConfiguracionCredito_ObtenerRetencionesPorID(int configuracionCreditoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ConfiguracionCreditoID",configuracionCreditoID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ConfiguracionCredito_ObtenerPorTipoCreditoYMes(ConfiguracionCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TipoCreditoID",filtro.TipoCredito.TipoCreditoID},
                            {"@PlazoCreditoID",filtro.PlazoCredito.PlazoCreditoID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ConfiguracionCredito_ObtenerParametrosCrear(ConfiguracionCreditoInfo info)
        {
            try
            {
                Logger.Info();

                var xml = new XElement("ROOT",
                            from retencion in info.Retenciones
                            select new XElement("ConfiguracionCredito",
                                new XElement("Mes", retencion.NumeroMes),
                                new XElement("Porcentaje", retencion.PorcentajeRetencion)
                                ));

                var parametros =
                    new Dictionary<string, object>
                        {
							{"@XML", xml.ToString()},
							{"@TipoCreditoID", info.TipoCredito.TipoCreditoID},
                            {"@PlazoCreditoID", info.PlazoCredito.PlazoCreditoID},
                            {"@Activo", info.Activo.GetHashCode()},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ConfiguracionCredito_ObtenerParametrosActualizar(ConfiguracionCreditoInfo info)
        {
            try
            {
                Logger.Info();

                var xml = new XElement("ROOT",
                            from retencion in info.Retenciones
                            select new XElement("ConfiguracionCredito",
                                new XElement("Mes", retencion.NumeroMes),
                                new XElement("Porcentaje", retencion.PorcentajeRetencion)
                                ));

                var parametros =
                    new Dictionary<string, object>
                        {
							{"@XML", xml.ToString()},
							{"@TipoCreditoID", info.TipoCredito.TipoCreditoID},
				    		{"@PlazoCreditoID", info.PlazoCredito.PlazoCreditoID},
							{"@Activo", info.Activo.GetHashCode()},
				    		{"@UsuarioCreacionID", info.UsuarioModificacionID},
                            {"@ConfiguracionCreditoID", info.ConfiguracionCreditoID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
