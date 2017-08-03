using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxAutoFacturacionDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosAutoFacturacion(PaginacionInfo pagina, AutoFacturacionInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionId", info.OrganizacionId},
                        {"@FolioId", info.FolioCompra},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@Estatus", info.EstatusId},
                        {"@FormaPago", info.FormaPagoId},
                        {"@ChequeId", info.ChequeId},
                        {"@FechaInicio", info.FechaInicio},
                        {"@FechaFin", info.FechaFin}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardar(AutoFacturacionInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", info.OrganizacionId},
                        {"@Folio", info.FolioCompra},
                        {"@TipoCompra", info.TipoCompra},
                        {"@Factura", info.Factura},
                        {"@UsuarioId", info.UsuarioId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosImagenes(AutoFacturacionInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionId", info.OrganizacionId},
                        {"@FolioId", info.FolioCompra},
                        {"@Estatus", info.EstatusId},
                        {"@FormaPago", info.FormaPagoId},
                        {"@ChequeId", info.ChequeId},
                        {"@FechaInicio", info.FechaInicio},
                        {"@FechaFin", info.FechaFin}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
