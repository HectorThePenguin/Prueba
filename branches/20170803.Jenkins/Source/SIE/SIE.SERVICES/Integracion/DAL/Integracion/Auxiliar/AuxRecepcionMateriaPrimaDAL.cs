using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxRecepcionMateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para obtener los pedidos parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPedidosParciales(PedidoInfo pedido)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                        {"@FolioPedido", pedido.FolioPedido},
                        {"@Activo", (int)EstatusEnum.Activo}
                        
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para obtener el surtido de un pedido
        /// </summary>
        /// <param name="pedido">Folio del pedido a consultar</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosSurtidoPedidos(PedidoInfo pedido)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT", new XElement("TiposPesajes",
                                        new XElement("TipoPesajeID", (int)TipoPesajeEnum.Bascula)),
                                        new XElement("TiposPesajes",
                                        new XElement("TipoPesajeID", (int)TipoPesajeEnum.Tolva))
                                     );

                parametros = new Dictionary<string, object>
                {
                    {"@FolioPedido", pedido.FolioPedido},
                    {"@TipoProveedor", (int)TipoProveedorEnum.ProveedoresFletes},
                    {"@XmlTiposPesajes", xml.ToString()},
                    {"@Activo", (int)EstatusEnum.Activo},
                    {"@OrganizacionID", pedido.Organizacion.OrganizacionID}
                        
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Obtiene los parametros para actualiza el pesaje
        /// </summary>
        /// <param name="listaSurtido"></param>
        /// <param name="usuarioModificacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPesajeMateriaPrima(List<SurtidoPedidoInfo> listaSurtido, int usuarioModificacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                   new XElement("ROOT",
                                from surtido in listaSurtido
                                select
                                    new XElement("PesajeMateriaPrima",
                                                 new XElement("PesajeMateriaPrimaID", surtido.PesajeMateriaPrima.PesajeMateriaPrimaID),
                                                 new XElement("EstatusID", surtido.CantidadPendiente ==0 ? (int)Estatus.PedidoCompletado :surtido.PesajeMateriaPrima.EstatusID),
                                                 new XElement("UsuarioModificacionID", usuarioModificacionId))
                                    );

                parametros = new Dictionary<string, object>
                {
                    {"@XmlPesajeDetalle", xml.ToString()}
                        
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
