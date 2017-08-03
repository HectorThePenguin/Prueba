using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxAlmacenInventarioLoteDAL
    {
        /// <summary>
        /// Obtiene los parametros para consultar el almacen inventario lote por id
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAlmacenInventarioLotePorId(int almacenInventarioLoteId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioLoteId", almacenInventarioLoteId}
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
        /// Obtiene los parametros para insertar un registro almacen inventario lote
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlmacenInventarioLoteInfo almacenInventarioLoteInfo, AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioID", almacenInventarioLoteInfo.AlmacenInventario.AlmacenInventarioID},
                            {"@Cantidad", almacenInventarioLoteInfo.Cantidad},
                            {"@PrecioPromedio", almacenInventarioLoteInfo.PrecioPromedio},
                            {"@Piezas", almacenInventarioLoteInfo.Piezas},
                            {"@Importe", almacenInventarioLoteInfo.Importe},
                            {"@Activo", almacenInventarioLoteInfo.Activo.GetHashCode()},
                            {"@UsuarioCreacionID", almacenInventarioLoteInfo.UsuarioCreacionId},
                            {"@AlmacenID", almacenInventarioInfo.AlmacenID},
                            {"@ProductoID", almacenInventarioInfo.ProductoID}
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
        /// Obtener parametros almacen inventario por organizacion, tipo de almacen y producto
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo datos)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",datos.OrganizacionId},
                                {"@TipoAlmacen", datos.TipoAlmacenId},
                                {"@ProductoId", datos.ProductoId}
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
        /// Obtiene los parametros para actualizar los datos del lote
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioLoteID", almacenInventarioLote.AlmacenInventarioLoteId},
                            {"@Cantidad", almacenInventarioLote.Cantidad},
                            {"@PrecioPromedio", almacenInventarioLote.PrecioPromedio},
                            {"@Piezas", almacenInventarioLote.Piezas},
                            {"@Importe", almacenInventarioLote.Importe},
                            {"@UsuarioModificacionID", almacenInventarioLote.UsuarioModificacionId}
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
        /// Obtiene los parametros para consultar por almaceninventarioid
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerPorAlmacenInventarioID(AlmacenInventarioInfo almacenInventario)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioID", almacenInventario.AlmacenInventarioID}
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
        /// Devuelve los parametros necesarios para consultar los lotes en los que se encuentra un producto.
        /// </summary>
        /// <param name="almacen"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorAlmacenProducto(AlmacenInfo almacen,ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacen.AlmacenID},
                            {"@ProductoID",producto.ProductoId},
                            {"@Activo",(int) EstatusEnum.Activo}
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
        internal static Dictionary<string, object> ObtenerParametrosDescontarAlmacenInventarioLoteProduccionDiaria(ProduccionDiariaInfo produccionDiaria)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in produccionDiaria.ListaProduccionDiariaDetalle
                                              select
                                                  new XElement("AlmacenInventarioLote",
                                                               new XElement("PesajeMateriaPrimaID", info.PesajeMateriaPrimaID),
                                                               new XElement("KilosNeto", info.KilosNeto),
                                                               new XElement("UsuarioModificacionID", info.UsuarioCreacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioLotesXML", xml.ToString()}
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
        /// Obtiene una lista de parametros para consultar
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",almacenInventarioLote.OrganizacionId},
                                {"@TipoAlmacen", almacenInventarioLote.TipoAlmacenId},
                                {"@ProductoId", almacenInventarioLote.ProductoId},
                                {"@Lote",almacenInventarioLote.Lote},
                                {"@Activo", almacenInventarioLote.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite}
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
        /// Desactiva un lote
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDesactivarLote(AlmacenInventarioLoteInfo almacenInventarioLoteInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioLoteID", almacenInventarioLoteInfo.AlmacenInventarioLoteId},
                            {"@Activo",(int) EstatusEnum.Inactivo},
                            {"@UsuarioModificacionID", almacenInventarioLoteInfo.UsuarioModificacionId}
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
        /// Obtiene un almaceninventariolote por lote
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerAlmacenInventarioLotePorLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",almacenInventarioLote.OrganizacionId},
                                {"@TipoAlmacen", almacenInventarioLote.TipoAlmacenId},
                                {"@ProductoId", almacenInventarioLote.ProductoId},
                                {"@Lote",almacenInventarioLote.Lote},
                                {"@Activo", almacenInventarioLote.Activo}
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
        internal static Dictionary<string, object> ObtenerParametrosAjustarAlmacenInventarioLote(List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in listaAlmacenInventarioLote
                                              select
                                                  new XElement("AlmacenInventarioLote",
                                                               new XElement("AlmacenInventarioLoteID", info.AlmacenInventarioLoteId),
                                                               new XElement("DiferenciaCantidad", info.Cantidad),
                                                               new XElement("DiferenciaPiezas", info.Piezas),
                                                               new XElement("EsEntrada", info.EsEntrada),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionId)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioLotesXML", xml.ToString()}
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
        /// Obtiene un diccionario con los parametros necesarios para
        /// la ejecucion del procedimiento almacenado AlmacenInventarioLote_ObtenerLotesProducto
        /// </summary>
        /// <param name="filtroAyudaLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerAlmacenInventarioLotePorLote(FiltroAyudaLotes filtroAyudaLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtroAyudaLote.OrganizacionID},
                            {"@Lote", filtroAyudaLote.Lote},
                            {"@ProductoID", filtroAyudaLote.ProductoID},
                            {"@AlmacenID", filtroAyudaLote.AlmacenID},
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
        /// Obtiene un lote por almacen
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerAlmacenInventarioLotePorFolioLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",almacenInventarioLote.OrganizacionId},
                                {"@TipoAlmacen", almacenInventarioLote.TipoAlmacenId},
                                {"@ProductoId", almacenInventarioLote.ProductoId},
                                {"@Lote",almacenInventarioLote.Lote},
                                {"@AlmacenID",almacenInventarioLote.AlmacenInventario.AlmacenID},
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
        /// Devuelve los parametros necesarios para consultar los lotes en los que se encuentra un producto.
        /// </summary>
        /// <param name="almacen"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorAlmacenProductoEnCeros(AlmacenInfo almacen, ProductoInfo producto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacen.AlmacenID},
                            {"@ProductoID",producto.ProductoId},
                            {"@Activo",(int) EstatusEnum.Activo}
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
        /// Obtiene parametros de lote en uso
        /// </summary>
        /// <param name="datosLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerLotesUso(AlmacenInventarioLoteInfo datosLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", datosLote.OrganizacionId},
                            {"@TipoAlmacen",datosLote.TipoAlmacenId},
                            {"@ProductoId",datosLote.ProductoId},
                            {"@Activo",datosLote.Activo}
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
        /// Obtiene un almacen inventario lote por su contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerAlmacenInventarioLotePorContratoID(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@ContratoID",contrato.ContratoId},
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
        /// Obtiene un almaceninventariolote por lote
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerAlmacenInventarioLoteAlmacenCodigo(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",almacenInventarioLote.OrganizacionId},
                                {"@AlmacenID", almacenInventarioLote.AlmacenInventario.Almacen.AlmacenID},
                                {"@ProductoID", almacenInventarioLote.ProductoId},
                                {"@Lote",almacenInventarioLote.Lote},
                                {"@Activo", almacenInventarioLote.Activo}
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
        /// Obtiene una lista de parametros para consultar
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAlmacenInventarioLoteAlmacenPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",almacenInventarioLote.OrganizacionId},
                                {"@AlmacenID", almacenInventarioLote.AlmacenInventario.Almacen.AlmacenID},
                                {"@ProductoId", almacenInventarioLote.ProductoId},
                                {"@Lote",almacenInventarioLote.Lote},
                                {"@Activo", almacenInventarioLote.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite}
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
        /// Obtiene parametros de lote en uso
        /// </summary>
        /// <param name="almacenesInventario"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosLotesPorAlmacenInventarioXML(List<AlmacenInventarioInfo> almacenesInventario)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from almacenInventario in almacenesInventario
                                select new XElement("AlmacenesInventario",
                                        new XElement("AlmacenInventarioID", almacenInventario.AlmacenInventarioID)
                                    ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlAlmacenInventario", xml.ToString()}
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
        /// Obtiene los parametros para insertar un registro almacen inventario lote
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearConTodosParametros(AlmacenInventarioLoteInfo almacenInventarioLoteInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenInventarioID", almacenInventarioLoteInfo.AlmacenInventario.AlmacenInventarioID},
                            {"@Cantidad", almacenInventarioLoteInfo.Cantidad},
                            {"@Lote", almacenInventarioLoteInfo.Lote},
                            {"@PrecioPromedio", almacenInventarioLoteInfo.PrecioPromedio},
                            {"@Piezas", almacenInventarioLoteInfo.Piezas},
                            {"@Importe", almacenInventarioLoteInfo.Importe},
                            {"@Activo", almacenInventarioLoteInfo.Activo.GetHashCode()},
                            {"@FechaInicio", almacenInventarioLoteInfo.FechaInicio},
                            {"@UsuarioCreacionID", almacenInventarioLoteInfo.UsuarioCreacionId},
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
        /// Genera el diccionario de datos para los parametros del procedimiento almacenado.
        /// </summary>
        /// <param name="almacenInventarioLoteInfo">Información del almácen lote - inventario</param>
        /// <returns>Dictionary<string, object></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEnvioAlimento(AlmacenInventarioLoteInfo almacenInventarioLoteInfo)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { 
                    {"@AlmacenInventarioLoteID", almacenInventarioLoteInfo.AlmacenInventarioLoteId},
                    {"@Cantidad", almacenInventarioLoteInfo.Cantidad},
                    {"@PrecioPromedio", almacenInventarioLoteInfo.PrecioPromedio},
                    {"@Piezas", almacenInventarioLoteInfo.Piezas},
                    {"@Importe", almacenInventarioLoteInfo.Importe},
                    {"@Activo", almacenInventarioLoteInfo.Activo},
                    {"@UsuarioModificacionID", almacenInventarioLoteInfo.UsuarioModificacionId}
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
