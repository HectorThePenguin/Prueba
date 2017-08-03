using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAlmacenMovimientoDetalleDAL
    {
        /// <summary>
        /// Crea un registro en almacen movimiento detalle
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoDetalleInfo.AlmacenMovimientoID},
                            {"@AlmacenInventarioLoteID", almacenMovimientoDetalleInfo.AlmacenInventarioLoteId},
                            {"@ContratoID", almacenMovimientoDetalleInfo.ContratoId},
                            {"@Piezas", almacenMovimientoDetalleInfo.Piezas},
                            {"@TratamientoID", almacenMovimientoDetalleInfo.TratamientoID},
                            {"@ProductoID", almacenMovimientoDetalleInfo.ProductoID},
                            {"@Precio", almacenMovimientoDetalleInfo.Precio},
                            {"@Cantidad", almacenMovimientoDetalleInfo.Cantidad},
                            {"@Importe", almacenMovimientoDetalleInfo.Importe},
                            {"@UsuarioCreacionID", almacenMovimientoDetalleInfo.UsuarioCreacionID}
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
        /// Obtiene un almacen movimiento detalle por contrato id
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", almacenMovimientoDetalleInfo.ContratoId}
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
        /// Obtiene un listado de almacenmovimientodetalle por loteid
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerAlmacenMovimientoDetallePorLoteId(AlmacenMovimientoDetalle almacenMovimientoDetalle, List<TipoMovimientoInfo> listaTipoMovimiento)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var element = new XElement("ROOT",
                                                from tipoMovimientoInfo in listaTipoMovimiento
                                                select new XElement("Datos",
                                                                    new XElement("tipoMovimiento",
                                                                                 tipoMovimientoInfo.TipoMovimientoID)));

                parametros = new Dictionary<string, object>
                    {
                        {"@AlmacenInventarioLoteID", almacenMovimientoDetalle.AlmacenInventarioLoteId},
                        {"@XmlTiposMovimiento", element.ToString()},
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
        /// Obtener por almacenmovimientodetalleid
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenMovimientoDetalleId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoDetalleID", almacenMovimientoDetalleInfo.AlmacenMovimientoDetalleID}
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
        /// Obtiene parametros para actualizar almacen movimiento detalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ActualizarAlmacenMovimientoDetalle(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoDetalleID", almacenMovimientoDetalleInfo.AlmacenMovimientoDetalleID},
                            {"@Cantidad", almacenMovimientoDetalleInfo.Cantidad},
                            {"@Precio", almacenMovimientoDetalleInfo.Precio},
                            {"@Importe", almacenMovimientoDetalleInfo.Importe},
                            {"@UsuarioModificacionID", almacenMovimientoDetalleInfo.UsuarioModificacionID}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="listaAlmacenMovimientoDetalle">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerGuardarDetalleCierreDiaInventarioPA(List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in listaAlmacenMovimientoDetalle
                                              select
                                                  new XElement("AlmacenMovimientoDetalle",
                                                               new XElement("AlmacenMovimientoID", info.AlmacenMovimientoID),
                                                               new XElement("AlmacenInventarioLoteID", info.AlmacenInventarioLoteId),
                                                               new XElement("ProductoID", info.ProductoID),
                                                               new XElement("Precio", info.Precio),
                                                               new XElement("Cantidad", info.Cantidad),
                                                               new XElement("Importe", info.Importe),
                                                               new XElement("Piezas", info.Piezas),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlAlmacenMovimientoDetalle", xml.ToString()}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="listaAlmacenMovimientoDetalle">Valores de la entidad</param>
        /// <param name="almacenMovimientoID">Id del Almacen Movimiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerGuardarAlmacenMovimientoDetalle(List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle, long almacenMovimientoID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in listaAlmacenMovimientoDetalle
                                              select
                                                  new XElement("AlmacenMovimientoDetalle",
                                                               new XElement("AlmacenMovimientoID", almacenMovimientoID),
                                                               new XElement("AlmacenInventarioLoteID", info.AlmacenInventarioLoteId),
                                                               new XElement("ProductoID", info.ProductoID),
                                                               new XElement("Precio", info.Precio),
                                                               new XElement("Cantidad", info.Cantidad),
                                                               new XElement("Importe", info.Importe),
                                                               new XElement("Piezas", info.Piezas),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlAlmacenMovimientoDetalle", xml.ToString()}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID
        /// </summary>
        /// <param name="almacenMovimientoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorAlmacenMovimientoID(long almacenMovimientoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoID},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado AlmacenMovimientoDetalle_ObtenerGranoEntregado
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosEntregadosPlanta(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FechaInicio", fechaInicial},
                            {"@FechaFin", fechaFinal},
                            {"@OrganizacionID", organizacionID},
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
