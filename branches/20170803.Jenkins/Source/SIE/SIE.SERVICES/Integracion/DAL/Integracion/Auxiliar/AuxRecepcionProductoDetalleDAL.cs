
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
    internal class AuxRecepcionProductoDetalleDAL
    {
        internal static Dictionary<string, object> ObtenerParametroGuardar(List<RecepcionProductoDetalleInfo> listaRecepcionProductoDetalle)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                       from recepcionProductoDetalle in listaRecepcionProductoDetalle
                            select
                                    new XElement("RecepcionProductoDetalle",
                                                 new XElement("RecepcionProductoID", recepcionProductoDetalle.RecepcionProductoId),
                                                 new XElement("ProductoID", recepcionProductoDetalle.Producto.ProductoId),
                                                 new XElement("Cantidad", recepcionProductoDetalle.Cantidad),
                                                 new XElement("PrecioPromedio", recepcionProductoDetalle.PrecioPromedio),
                                                 new XElement("Importe", recepcionProductoDetalle.Importe),
                                                 new XElement("Activo", (int)recepcionProductoDetalle.Activo),
                                                 new XElement("UsuarioCreacionID", recepcionProductoDetalle.UsuarioCreacion.UsuarioID)
                                    )
                );

                parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLRecepcionProductoDetalle", xml.ToString()}
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
