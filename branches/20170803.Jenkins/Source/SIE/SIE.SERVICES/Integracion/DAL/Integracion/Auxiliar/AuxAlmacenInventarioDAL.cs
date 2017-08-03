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
    public class AuxAlmacenInventarioDAL
    {
        /// <summary>
        /// Obtiene los parametros para consultar el almacen inventario
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAlmacenInventarioId(int almacenInventarioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioId", almacenInventarioId}
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
        /// Obtiene los parametros para obtener un listado de almacen inventario por AlmacenID
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenId(AlmacenInfo almacenInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenInfo.AlmacenID}
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
        /// Obtiene parametros para insertar un almacen inventario
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenInventarioInfo.AlmacenID},
                            {"@ProductoID", almacenInventarioInfo.ProductoID},
                            {"@Minimo", almacenInventarioInfo.Minimo},
                            {"@Maximo", almacenInventarioInfo.Maximo},
                            {"@PrecioPromedio", almacenInventarioInfo.PrecioPromedio},
                            {"@Cantidad", almacenInventarioInfo.Cantidad},
                            {"@Importe", almacenInventarioInfo.Importe},
                            {"@UsuarioCreacionID", almacenInventarioInfo.UsuarioCreacionID}
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
        /// Obtiene el diccionario para actualizar por producto
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPorProductoId(AlmacenInventarioInfo almacenInventario)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenInventario.AlmacenID},
                            {"@ProductoID", almacenInventario.ProductoID},
                            {"@PrecioPromedio",almacenInventario.PrecioPromedio},
                            {"@Cantidad", almacenInventario.Cantidad},
                            {"@Importe", almacenInventario.Importe},
                            {"@UsuarioModificacionID", almacenInventario.UsuarioModificacionID}
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
        /// Actualiza un registro de almacen inventario
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioID", almacenInventarioInfo.AlmacenInventarioID},
                            {"@Minimo", almacenInventarioInfo.Minimo},
                            {"@Maximo", almacenInventarioInfo.Maximo},
                            {"@PrecioPromedio", almacenInventarioInfo.PrecioPromedio},
                            {"@Cantidad", almacenInventarioInfo.Cantidad},
                            {"@Importe", almacenInventarioInfo.Importe},
                            {"@UsuarioModificacionID", almacenInventarioInfo.UsuarioModificacionID}
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
        /// Obtiene parametros para obtener inventario por almacenid y productoid
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenInventarioInfo.AlmacenID},
                            {"@ProductoID", almacenInventarioInfo.ProductoID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
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
        /// Obtiene parametros para obtener inventario por almacenid y productoid
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacionIDAlmacenIdProductoId(AlmacenInventarioInfo almacenInventarioInfo, int OrganizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenInventarioInfo.AlmacenID},
                            {"@ProductoID", almacenInventarioInfo.ProductoID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()},
                            {"@OrganizacionId", OrganizacionId}
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
        /// Obtiene parametros para obtener inventario por almacenid y productoid
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorOrganizacionIdAlmacenIdProductoIdParaPlantaCentroCadisDesc(AlmacenInventarioInfo almacenInventarioInfo, int OrganizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", almacenInventarioInfo.ProductoID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()},
                            {"@OrganizacionId", OrganizacionId}
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
        /// Obtiene los parametros para obtener un listado de almacen inventario por AlmacenID
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosCierreDiaInventarioPlantaAlimentos(int organizacionID, int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@AlmacenID", almacenID}
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
        /// Obtiene los parametros para actualizar los datos del lote despues de haber guardado la entrada del producto
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAjustarAlmacenInventario(List<AlmacenInventarioInfo> listaAlmacenInventario)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in listaAlmacenInventario
                                              select
                                                  new XElement("AlmacenInventario",
                                                               new XElement("AlmacenID", info.AlmacenID),
                                                               new XElement("ProductoID", info.ProductoID),
                                                               new XElement("DiferenciaCantidad", info.Cantidad),
                                                               new XElement("EsEntrada", info.EsEntrada),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioXML", xml.ToString()}
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
        /// Obtiene lod parámetros para obtener la lista de de AlmacenInventario 
        /// por un conjunto de productos
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosExistenciaPorProductos(int almacenId, IList<ProductoInfo> productos)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from producto in productos
                                select new XElement("Producto",
                                        new XElement("AlmacenID", almacenId),
                                        new XElement("ProductoID", producto.ProductoId)
                                    ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlProductos", xml.ToString()}
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
        /// Obtiene lod parámetros para obtener la lista de de AlmacenInventario 
        /// por un conjunto de productos
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPorProductos(IList<AlmacenInventarioInfo> info)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from row in info
                                select new XElement("AlmacenInventario",
                                        new XElement("AlmacenInventarioID", row.AlmacenInventarioID),
                                        new XElement("AlmacenID", row.AlmacenID),
                                        new XElement("PrecioPromedio", row.PrecioPromedio),
                                        new XElement("ProductoID", row.ProductoID),
                                        new XElement("Cantidad", row.Cantidad),
                                        new XElement("Importe", row.Importe),
                                        new XElement("UsuarioModificacionID", row.UsuarioModificacionID)
                                    ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioXML", xml.ToString()}
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
        /// Obtiene los parametros para actualizar los datos del inventario despues de haber guardado la entrada del producto
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDescontarAlmacenInventarioProduccionDiaria(ProduccionDiariaInfo produccionDiaria)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in produccionDiaria.ListaProduccionDiariaDetalle
                                              select
                                                  new XElement("AlmacenInventario",
                                                               new XElement("PesajeMateriaPrimaID", info.PesajeMateriaPrimaID),
                                                               new XElement("KilosNeto", info.KilosNeto),
                                                               new XElement("UsuarioModificacionID", info.UsuarioCreacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioXML", xml.ToString()}
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
        /// Obtiene lod parámetros para obtener la lista de de AlmacenInventario 
        /// por un conjunto de productos
        /// </summary>
        /// <param name="almacenes">Almacenes</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorAlmacenXML(List<AlmacenInfo> almacenes)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from almacen in almacenes
                                select new XElement("Almacenes",
                                        new XElement("AlmacenID", almacen.AlmacenID)
                                    ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlAlmacen", xml.ToString()}
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
