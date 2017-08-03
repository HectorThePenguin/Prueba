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
    public class AuxCierreDiaInventarioDAL
    {
        /// <summary>
        /// Obtener parametros para guardar animal salida
        /// </summary>
        /// <param name="datosGrid"></param>
        /// <param name="almacenCierreDiaInventarioInfo"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarProductosCierreDiaInventario(IList<AlmacenCierreDiaInventarioInfo> datosGrid, AlmacenCierreDiaInventarioInfo almacenCierreDiaInventarioInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from cierre in datosGrid
                               select
                                   new XElement("CierreDiaInventario",
                                                new XElement("AlmacenID", cierre.Almacen.AlmacenID),
                                                new XElement("ProductoID", cierre.ProductoID),
                                                new XElement("Precio", cierre.PrecioPromedio),
                                                new XElement("Cantidad", cierre.CantidadReal),
                                                new XElement("Importe", cierre.ImporteReal),
                                                new XElement("AlmacenMovimientoID", almacenCierreDiaInventarioInfo.AlmacenMovimientoID),
                                                new XElement("UsuarioCreacionID", almacenCierreDiaInventarioInfo.UsuarioCreacionId)
                                            
                                                )
                                   );

                parametros = new Dictionary<string, object>
                {
                        {"@XmlGuardarProductosCierreDiaInventario", xml.ToString()},
                       // {"@Activo",(int)EstatusEnum.Activo}
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
        /// Obtener parametros guardar almacen movimiento
        /// </summary>
        /// <param name="almacenCierreFolio"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerGuardarAlmacenMovimiento(AlmacenCierreDiaInventarioInfo almacenCierreFolio)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@AlmacenID",almacenCierreFolio.Almacen.AlmacenID},
                        {"@TipoMovimientoID",almacenCierreFolio.TipoMovimiento},
                        {"@Observaciones",almacenCierreFolio.Observaciones},
                        {"@EstatusID",almacenCierreFolio.Estatus},
                        {"@UsuarioCreacion",almacenCierreFolio.UsuarioCreacionId}
                    };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosObtenerObtenerProductoPorId(int productoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@ProductoID",productoId}
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
        /// CierreDiaInventario_DescontarAlmacenInventario
        /// </summary>
        /// <param name="listaDiferencia"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDescontarAlmacenInventario(IList<AlmacenCierreDiaInventarioInfo> listaDiferencia)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from cierre in listaDiferencia
                                 select
                                     new XElement("AlmacenInventario",
                                                  new XElement("AlmacenID", cierre.Almacen.AlmacenID),
                                                  new XElement("ProductoID", cierre.ProductoID),
                                                  new XElement("Precio", cierre.PrecioPromedio),
                                                  new XElement("Cantidad", cierre.CantidadReal),
                                                  new XElement("Importe", cierre.ImporteReal),
                                                  new XElement("AlmacenMovimientoID", cierre.AlmacenMovimientoID),
                                                  new XElement("UsuarioCreacionID", cierre.UsuarioCreacionId)
                                     )
                        );
                parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlDescontarAlmacenInventario", xml.ToString()},
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
