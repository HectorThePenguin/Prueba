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
using System;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxCuentaGastosDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosGuardar(CuentaGastosInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", info.Organizacion.OrganizacionID},
                        {"@CuentaSAP", info.CuentaSAP.CuentaSAP},
                        {"@CostoID", info.Costos.CostoID},
                        {"@Activo", (int)info.Activo},
                        {"@UsuarioCreacionID", info.UsuarioCreacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosActualizar(CuentaGastosInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaGastoID", info.CuentaGastoID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
                            {"@CuentaSAP", info.CuentaSAP.CuentaSAP},
                            {"@CostoID", info.Costos.CostoID},
                            {"@Activo", (int)info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificaID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CuentaGastosInfo filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtros.Organizacion.OrganizacionID},
                            {"@Descripcion", filtros.Organizacion.Descripcion ?? string.Empty},
                            {"@Activo", filtros.Activo.GetHashCode()},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
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
