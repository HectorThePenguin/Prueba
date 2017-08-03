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
    public class AuxEntradaProductoDetalleDAL 
    {
        /// <summary>
        /// Obtiene los parametros para consultar el detalle de los indicadores para cada entrada producto Id
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaProductoDetalleProductoPorIdEntrada(int entradaProductoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoId", entradaProductoId}
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
        /// Obtiene los parametros necesarios para almacenar el detalle de una entrada de producto.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarEntradaProductoDetalle(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                       from entradaDetalle in  entradaProducto.ProductoDetalle
                       select 
                       new XElement("EntradaProductoDetalle",
                               new XElement("EntradaProductoID", entradaProducto.EntradaProductoId),
                               new XElement("IndicadorID", entradaDetalle.Indicador.IndicadorId),
                               new XElement("UsuarioCreacionID", entradaProducto.UsuarioCreacionID )
                           )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLDetalle", xml.ToString()}
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
        /// Obtiene los parametros necesarios para actualizar el detalle de una entrada de producto.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEntradaProductoDetalle(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();

                ContratoInfo contratoInfo = entradaProducto.Contrato;

                var contratoDetalleInfo = (ContratoDetalleInfo)(from contratoDetalle in contratoInfo.ListaContratoDetalleInfo
                                                                select contratoDetalle).First();

                var entradaDetalle = (EntradaProductoDetalleInfo)(from detalle in entradaProducto.ProductoDetalle
                                                                  select detalle).First();

                var xml =
                   new XElement("ROOT",
                       new XElement("EntradaProductoDetalle",
                               new XElement("EntradaProductoDetalleID",entradaDetalle.EntradaProductoDetalleId),
                               new XElement("EntradaProductoID", entradaProducto.EntradaProductoId),
                               new XElement("IndicadorID", contratoDetalleInfo.Indicador.IndicadorId),
                               new XElement("Activo",(int) entradaDetalle.Activo),
                               new XElement("UsuarioModificacionID", entradaProducto.UsuarioModificacionID)
                           )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLDetalle", xml.ToString()}
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
