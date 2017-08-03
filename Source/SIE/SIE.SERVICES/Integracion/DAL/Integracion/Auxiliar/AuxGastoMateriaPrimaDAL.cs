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
    internal class AuxGastoMateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene los parametros para crear una solicitud
        /// </summary>
        /// <param name="gasto"></param>
        /// <returns></returns>
        internal static Dictionary<string,object> ObtenerParametrosGuardar(GastoMateriaPrimaInfo gasto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID",gasto.AlmacenMovimientoID},
	                        {"@OrganizacionID",gasto.Organizacion.OrganizacionID},
	                        {"@TipoMovimientoID",gasto.TipoMovimiento.TipoMovimientoID},
	                        {"@ProductoID",gasto.Producto.ProductoId},
	                        {"@TieneCuenta",gasto.TieneCuenta},
	                        {"@CuentaSAPID",gasto.CuentaSAP.CuentaSAPID},
	                        {"@ProveedorID",gasto.Proveedor.ProveedorID},
	                        {"@AlmacenInventarioLoteID",gasto.AlmacenInventarioLote.AlmacenInventarioLoteId},
	                        {"@Importe",gasto.Importe},
	                        {"@IVA",gasto.Iva},
	                        {"@Observaciones",gasto.Observaciones},
	                        {"@Activo",gasto.Activo},
                            {"@Cantidad", gasto.UnidadMedida ? gasto.Kilogramos : 0},
                            {"@TipoFolio", gasto.TipoFolio.GetHashCode()},
	                        {"@UsuarioCreacionID",gasto.UsuarioCreacionID}
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="gastoMateriaPrimaID">Identificador de la entidad GastoMateriaPrima</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int gastoMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@GastoMateriaPrimaID", gastoMateriaPrimaID}
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
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almacenado
        /// GastoMateriaPrima_ObtenerConciliacionMovimientosSIAP
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in almacenesMovimiento
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID)
                                     ));
                var parametros = new Dictionary<string, object> {{"@XmlAlmacenMovimiento", xml.ToString()}};
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosValidarAretes(List<AreteInfo> aretes, int organizacionID, bool esAreteSukarne, bool esEntradaAlmacen)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from arete in aretes
                                 select new XElement("DATOS",
                                                     new XElement("Arete", arete.Arete)
                                     ));
                var parametros = new Dictionary<string, object> { 
                                                                    { "@XML", xml.ToString() },
                                                                    { "@OrganizacionId", organizacionID },
                                                                    { "@EsAreteSukarne", esAreteSukarne.GetHashCode() },
                                                                    { "@EsEntradaAlmacen", esEntradaAlmacen.GetHashCode() }
                                                                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardarAretes(GastoMateriaPrimaInfo gasto)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                 from arete in gasto.AretesCapturados
                                 select new XElement("DATOS",
                                                     new XElement("Arete", arete.Arete)
                                     ));

                var parametros = new Dictionary<string, object> { 
                                                                    { "@XML", xml.ToString() },
                                                                    { "@OrganizacionId", gasto.Organizacion.OrganizacionID },
                                                                    { "@ProductoId", gasto.Producto.ProductoId },
                                                                    { "@EsAreteSukarne", gasto.EsAreteSukarne.GetHashCode() },
                                                                    { "@EsEntradaAlmacen", gasto.TipoMovimiento.EsEntrada.GetHashCode()},
                                                                    { "@Cantidad", gasto.AretesCapturados.Count },
                                                                    { "@Importe", gasto.Importe },
                                                                    { "@UsuarioId", gasto.UsuarioCreacionID }
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
