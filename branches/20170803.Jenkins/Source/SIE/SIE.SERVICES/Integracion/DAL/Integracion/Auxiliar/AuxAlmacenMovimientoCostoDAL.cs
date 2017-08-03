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
    internal class AuxAlmacenMovimientoCostoDAL
    {

        /// <summary>
        /// Crea un registro en almacen movimiento costo
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlmacenMovimientoCostoInfo almacenMovimientoCostoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoCostoInfo.AlmacenMovimientoId},
                            {"@ProveedorID", almacenMovimientoCostoInfo.ProveedorId},
                            {"@CuentaSAPID", almacenMovimientoCostoInfo.CuentaSAPID},
                            {"@CostoID", almacenMovimientoCostoInfo.CostoId},
                            {"@Importe", almacenMovimientoCostoInfo.Importe},
                            {"@Cantidad",almacenMovimientoCostoInfo.Cantidad},
                            {"@Iva", almacenMovimientoCostoInfo.Iva},
                            {"@Retencion",almacenMovimientoCostoInfo.Retencion},
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@UsuarioCreacionID", almacenMovimientoCostoInfo.UsuarioCreacionId}
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
        /// Crea registros en AlmacenMovimientoCosto usando un xml
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearCostos(List<AlmacenMovimientoCostoInfo> listaAlmacenMovimientoCostoInfo)
        {
            try
            {

                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaAlmacenMovimientoCostoInfo
                                 select new XElement("XmlAlmacenMovimientoCosto",
                                        new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoId),
                                        new XElement("ProveedorID", detalle.Proveedor != null ? detalle.Proveedor.ProveedorID : 0),
                                        new XElement("CuentaSAPID", detalle.CuentaSap != null ? detalle.CuentaSap.CuentaSAPID : 0),
                                        new XElement("CostoID", detalle.Costo.CostoID),
                                        new XElement("Importe", detalle.Importe),
                                        new XElement("Cantidad", detalle.Cantidad),
                                        new XElement("Activo", detalle.Activo.GetHashCode()),
                                        new XElement("TieneCuenta", detalle.TieneCuenta.GetHashCode()),
                                        new XElement("Iva", detalle.Iva),
                                        new XElement("Retencion", detalle.Retencion),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlAlmacenMovimientoCosto", xml.ToString()}
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
        /// Obtiene un listado de costos por almacen movimiento id
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenMovimientoId(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoInfo.AlmacenMovimientoID}
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML
        /// </summary>
        /// <returns></returns>
        internal static string ObtenerParametrosAlmacenMovimientoCostoPorContratoXML(List<ContratoInfo> contratos)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in contratos
                                 select new XElement("Contrato",
                                                     new XElement("ContratoID", detalle.ContratoId)
                                     ));
                return xml.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de costos por el contratoID
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorContratoID(int contratoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoID}
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
        /// del procedimiento almacenado AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML
        /// </summary>
        /// <param name="movimientosAlmacen"></param>
        /// <returns></returns>
        internal static string ObtenerParametrosObtenerAlmacenMovimientoXML(IEnumerable<AlmacenMovimientoInfo> movimientosAlmacen)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in movimientosAlmacen
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID)
                                     ));
                return xml.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
