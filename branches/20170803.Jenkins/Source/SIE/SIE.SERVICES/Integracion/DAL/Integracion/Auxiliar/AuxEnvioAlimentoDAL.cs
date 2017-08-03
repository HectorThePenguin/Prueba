using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEnvioAlimentoDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosRegistrarRecepcionProductoEnc(EnvioAlimentoInfo envio,TipoMovimiento tipo)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionDestinoID",envio.Destino.OrganizacionID},
                    {"@Folio",envio.FolioMovimientoAlmacen},
                    {"@OrganizacionOrigenID",envio.Origen.OrganizacionID},
                    {"@FechaTransferencia",envio.FechaEnvio},
                    {"@UsuarioCreacionID",envio.UsuarioCreacionID},
                    {"@TipoMovimientoID",tipo.GetHashCode()},
                    {"@AlmacenOrigenID",envio.Almacen.AlmacenID}
                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosRegistrarRecepcionProductoDet(EnvioAlimentoInfo envio,TipoMovimiento tipo)
        { 
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionDestinoID",envio.Destino.OrganizacionID},
                    {"@TransferenciaID",envio.FolioMovimientoAlmacen},
                    {"@ProductoID",envio.Producto.ProductoId},
                    {"@Cantidad",envio.Cantidad},
                    {"@UsuarioCreacionID",envio.UsuarioCreacionID},
                    {"@OrganizacionOrigenID",envio.Origen.OrganizacionID},
                    {"@Importe",envio.Importe},
                    {"@TipoMovimientoID",tipo.GetHashCode()},
                    {"@AlmacenOrigenID",envio.Almacen.AlmacenID}
                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosRegistrarEnvioAlimento(EnvioAlimentoInfo envio)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {

                    {"@OrganizacionID",envio.Origen.OrganizacionID},
                    {"@OrganizacionDestinoID",envio.Destino.OrganizacionID},
                    {"@Folio",envio.Folio},
                    {"@AlmacenMovimientoID",envio.AlmacenMovimientoId},
                    {"@UsuarioCreacionID",envio.UsuarioCreacionID}
                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosBuscarEnvioPorId(EnvioAlimentoInfo envio)
        { 
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@EnvioProductoId",envio.EnvioId}
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