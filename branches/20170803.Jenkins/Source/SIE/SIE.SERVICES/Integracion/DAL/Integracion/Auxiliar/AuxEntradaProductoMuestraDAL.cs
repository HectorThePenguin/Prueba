using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxEntradaProductoMuestraDAL 
    {
        /// <summary>
        /// Obtiene los parametros para consultar el detalle de los indicadores para cada entrada producto Id
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaProductoMuestraPorIdEntradaDetalle(int entradaProductoDetalleId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoDetalleID", entradaProductoDetalleId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Devuelve los parametros necesarios para guardar la muestra de una entrada producto.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarEntradaProductoMuestra(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();

                var entradaDetalle = (EntradaProductoDetalleInfo)(from detalle in entradaProducto.ProductoDetalle
                                                                  select detalle).First();
                var xml =
                   new XElement("ROOT", from entradaMuestra in entradaDetalle.ProductoMuestras
                                 select new XElement("EntradaProductoMuestra",
                                            new XElement("EntradaProductoDetalleID", entradaDetalle.EntradaProductoDetalleId),
                                            new XElement("Porcentaje", entradaMuestra.Porcentaje),
                                            new XElement("Descuento",entradaMuestra.Descuento),
                                            new XElement("Rechazo",(int)entradaMuestra.Rechazo),
                                            new XElement("UsuarioCreacionID", entradaProducto.UsuarioCreacionID),
                                            new XElement("esOrigen", entradaProducto.EsOrigen)
                                        )
                                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLMuestras", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        internal static Dictionary<string, object> ObtenerParametrosGuardarActualizacionProductos(EntradaProductoDetalleInfo indicadores)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT", from entradaMuestra in indicadores.ProductoMuestras
                                        select new XElement("EntradaProductoMuestra",
                                                   new XElement("EntradaProductoDetalleID", entradaMuestra.EntradaProductoDetalleId),
                                                   new XElement("Descuento", entradaMuestra.Descuento),
                                                   new XElement("UsuarioModificacionID", entradaMuestra.UsuarioModificacion.UsuarioID),
                                                   new XElement("EsOrigen", entradaMuestra.EsOrigen)
                                               )
                                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLMuestras", xml.ToString()}
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
