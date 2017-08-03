using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxBoletaRecepcionForrajeDAL 
    {
        /// <summary>
        /// Metodos que obtiene el rango de humedad permito para el producto determinado
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosRangosProducto(RegistroVigilanciaInfo registroVigilanciaInfo, IndicadoresEnum indicador)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IndicadorID", indicador},
                            {"@productoID", registroVigilanciaInfo.Producto.ProductoId},
                            {"@OrganizacionID", registroVigilanciaInfo.Organizacion.OrganizacionID},
                            {"@activo", EstatusEnum.Activo}
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
        /// Método que agrega un registro a la tabla "EntradaProductoMuestra" 
        /// Se agrega un solo registro por tratarse de un folio que cuya CalidadOrigen= 1
        /// </summary>
        internal static Dictionary<string, object> AgregarNuevoRegistro(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var entradaDetalle = (EntradaProductoDetalleInfo)(from detalle in entradaProducto.ProductoDetalle
                                                                  select detalle).First();

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoDetalleID", entradaDetalle.EntradaProductoDetalleId},
                            {"@Porcentaje", entradaProducto.datosOrigen.humedadOrigen},
                            {"@Descuento", entradaDetalle.ProductoMuestras[0].Descuento},
                            {"@Rechazo", entradaDetalle.ProductoMuestras[0].Rechazo},
                            {"@Activo", EstatusEnum.Activo},
                            {"@UsuarioCreacionID", entradaProducto.UsuarioCreacionID},
                            {"@EsOrigen", entradaProducto.Contrato.CalidadOrigen }
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
