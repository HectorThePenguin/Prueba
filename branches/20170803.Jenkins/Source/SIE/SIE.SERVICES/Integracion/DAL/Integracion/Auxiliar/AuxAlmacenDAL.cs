using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAlmacenDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener un Almacen por su Id
        /// </summary>
        /// <param name="almacenId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int almacenId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenId", almacenId}
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
        /// Generar parametros para validar existencia del producto
        /// </summary>
        /// <param name="itemProducto"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerCantidadProductoEnInventario(ProductoInfo itemProducto, int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenID},
                            {"@ProductoID", itemProducto.ProductoId}
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
        /// Genera parametros para guardar almacen Movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroGuardarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenMovimientoInfo.AlmacenID},
                            {"@AnimalMovimientoID", almacenMovimientoInfo.AnimalMovimientoID},
                            {"@TipoMovimientoID", almacenMovimientoInfo.TipoMovimientoID},
                            {"@Status", almacenMovimientoInfo.Status},
                            {"@Observaciones", almacenMovimientoInfo.Observaciones},
                            {"@UsuarioCreacionID", almacenMovimientoInfo.UsuarioCreacionID},
                            {"@ProveedorID", almacenMovimientoInfo.ProveedorId}
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
        /// Obtiene los prametros para mandar almacenar el detalle de almacen movimiento
        /// </summary>
        /// <param name="listaAlmacenMovimientoDetalle"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroGuardarAlmacenMovimientoDetalle(
            List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle, int almacenID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaAlmacenMovimientoDetalle
                                 select new XElement("AlmacenMovimientoDetalle",
                                        new XElement("AlmacenID", almacenID),
                                        new XElement("TratamientoID", detalle.TratamientoID),
                                        new XElement("ProductoID", detalle.ProductoID),
                                        new XElement("Precio", detalle.Precio),
                                        new XElement("Cantidad", detalle.Cantidad),
                                        new XElement("Importe", detalle.Importe),
                                        new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAlmacenMovimientoDetalle", xml.ToString()}
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
        /// Obtiene los prametros para mandar almacenar el detalle de almacen movimiento, solo se ejecuta cuando no trae tratamiento
        /// </summary>
        /// <param name="listaAlmacenMovimientoDetalle"></param>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroGuardarAlmacenMovimientoDetalleTratamientoNulo(
            List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle, int almacenID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaAlmacenMovimientoDetalle
                                 select new XElement("AlmacenMovimientoDetalle",
                                        new XElement("AlmacenID", almacenID),
                                        new XElement("TratamientoID", null),
                                        new XElement("ProductoID", detalle.ProductoID),
                                        new XElement("Precio", detalle.Precio),
                                        new XElement("Cantidad", detalle.Cantidad),
                                        new XElement("Importe", detalle.Importe),
                                        new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAlmacenMovimientoDetalle", xml.ToString()}
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
        /// Generar parametros para validar si existen ajustes pendientes por aplicar en almacen
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroExistenAjustesPendientesParaAlmacen(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenMovimientoInfo.AlmacenID},
                            {"@TipoMovimientoID", almacenMovimientoInfo.TipoMovimientoID},
                            {"@Status", almacenMovimientoInfo.Status}
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
        /// Obtiene parametros para obtener lista paginada
        /// filtrandpo por varios tipos de almacén.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtro.AlmacenID},
                            {"@Descripcion", filtro.Descripcion ?? string.Empty},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@TipoAlmacenID", filtro.TipoAlmacen != null ? filtro.TipoAlmacen.TipoAlmacenID : filtro.TipoAlmacenID},
                            {"@Activo", filtro.Activo},
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoAlmacen", info.CodigoAlmacen},
							{"@Descripcion", info.Descripcion},
							{"@TipoAlmacenID", info.TipoAlmacen.TipoAlmacenID},
							{"@CuentaInventario", info.CuentaInventario},
							{"@CuentaInventarioTransito", info.CuentaInventarioTransito},
							{"@CuentaDiferencias", info.CuentaDiferencias},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@AlmacenID", info.AlmacenID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoAlmacen", info.CodigoAlmacen},
							{"@Descripcion", info.Descripcion},
							{"@TipoAlmacenID", info.TipoAlmacen.TipoAlmacenID},
							{"@CuentaInventario", info.CuentaInventario},
							{"@CuentaInventarioTransito", info.CuentaInventarioTransito},
							{"@CuentaDiferencias", info.CuentaDiferencias},
							{"@Activo", info.Activo},
                            {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        /// <param name="almacenID">Identificador de la entidad Almacen</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
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
        /// Obtiene Parametro pora filtrar por estatus 
        /// </summary> 
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@Activo", estatus}
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Descripcion", descripcion}
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
        /// Obtiene parametros para obtener un registro de almacen movimiento
        /// </summary> 
        /// <param name="almacenMovimientoInfo">Descripción de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenID", almacenMovimientoInfo.AlmacenID},
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
        /// Obtener parametros almacen por organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAlmacenPorOrganizacion(int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",organizacionId},
                                {"@Activo",(int)EstatusEnum.Activo}
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
        /// Obtener parametros para obtener cierreDiaInventarioInfo
        /// </summary>
        /// <param name="almacenId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerObtenerDatosAlmacenInventario(AlmacenCierreDiaInventarioInfo cierreInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",cierreInventarioInfo.OrganizacionId},
                                {"@AlmacenID",cierreInventarioInfo.Almacen.AlmacenID},
                                {"@Activo",(int)EstatusEnum.Activo},
                                {"@TipoMovimiento",cierreInventarioInfo.TipoMovimiento}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosobtenerFolioAlmacen(AlmacenCierreDiaInventarioInfo almacenCierreFolio)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenID",almacenCierreFolio.Almacen.AlmacenID},
                                {"@TipoMovimientoID",almacenCierreFolio.TipoMovimiento},
                                {"@Folio",almacenCierreFolio.FolioAlmacen}
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
        /// Obtener parametros de datos del almacen
        /// </summary>
        /// <param name="almacenId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerObtenerDatosAlmacenProductos(int almacenId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@Activo",(int)EstatusEnum.Activo},
                                {"@OrganizacionID",organizacionId},
                                {"@AlmacenID",almacenId}
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
        /// Obtener parametros para obtener actualizar almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenMovimientoID", almacenMovimientoInfo.AlmacenMovimientoID},
                                {"@Observaciones", almacenMovimientoInfo.Observaciones},
                                {"@Estatus", almacenMovimientoInfo.Status},
                                {"@UsuarioModificacionID", almacenMovimientoInfo.UsuarioModificacionID}
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
        /// Obtener parametros para obtener actualizar almacen inventario
        /// </summary>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizarAlmacenInventario(AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenInventarioID", almacenInventarioInfo.AlmacenInventarioID},
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
        /// Obtener parametros para eliminar almacen movimiento detalle
        /// </summary>
        /// <param name="almacenMovimientoDetalle"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosEliminarAlmacenMovimientoDetalle(AlmacenMovimientoDetalle almacenMovimientoDetalle)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenMovimientoDetalleID", almacenMovimientoDetalle.AlmacenMovimientoDetalleID},
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
        /// Obtiene los parametros para obtener la lista de movimientos del almacen
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosObtenerobtenerListaAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo, int activo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenID", almacenMovimientoInfo.AlmacenID},
                                {"@TipoMovimientoID",almacenMovimientoInfo.TipoMovimientoID},
                                {"@Activo",activo}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerProductosAlmacenInventario(AlmacenInfo almacen, OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenID", almacen.AlmacenID},
                                {"@OrganizacionID",organizacion.OrganizacionID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerAlmacenMovimientoPorAlmacenID(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenID", almacen.AlmacenID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardarConsumoAlimento(List<AlmacenInventarioInfo> listaActualizadaProductos, OrganizacionInfo organizacion)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaActualizadaProductos
                                 select new XElement("AlmacenInventario",
                                        new XElement("OrganizacionID", organizacion.OrganizacionID),
                                        new XElement("AlmacenID", detalle.AlmacenID),
                                        new XElement("ProductoID", detalle.ProductoID),
                                        new XElement("Cantidad", detalle.Cantidad),
                                        new XElement("Importe", detalle.Importe),
                                        new XElement("UsuarioCreacionID", organizacion.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
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
        /// Obtener parametros para guardar el almacen movimiento detalle
        /// </summary>
        /// <param name="almacenMovimientoDetalle"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosGuardarAlmacenMovimientoDetalleProducto(AlmacenMovimientoDetalle almacenMovimientoDetalle)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@AlmacenMovimientoID", almacenMovimientoDetalle.AlmacenMovimientoID},
                                {"@ProductoID", almacenMovimientoDetalle.ProductoID},
                                {"@Precio", almacenMovimientoDetalle.Precio},
                                {"@Cantidad", almacenMovimientoDetalle.Cantidad},
                                {"@Importe", almacenMovimientoDetalle.Importe},
                                {"@AlmacenInventarioLoteID", almacenMovimientoDetalle.AlmacenInventarioLoteId},
                                {"@ContratoID", almacenMovimientoDetalle.ContratoId},
                                {"@Piezas", almacenMovimientoDetalle.Piezas},
                                {"@UsuarioCreacionID", almacenMovimientoDetalle.UsuarioCreacionID},
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
        /// Obtiene los parametros necesarios para obtener los movimientos por contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAlmacenMovimientoPorContrato(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@ContratoID", contrato.ContratoId}
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
        internal static Dictionary<string, object> ObtenerAlmacenInventarioPorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo datos)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@Activo",datos.Activo},
                                {"@OrganizacionID",datos.OrganizacionId},
                                {"@TipoAlmacen", datos.TipoAlmacenId},
                                {"@ProductoId", datos.ProductoId},
                                {"@AlmacenID", datos.AlmacenID},
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
        /// Obtiene los parametros necesarios para obtener los movimientos por contrato
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFolioAlmacenConsulta(FiltroCierreDiaInventarioInfo filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@AlmacenID", filtros.AlmacenID},
                        {"@TipoMovimientoID", filtros.TipoMovimientoID},
                        {"@Folio", filtros.Folio}
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
        /// Obtiene los parametros necesarios para obtener los movimientos por contrato
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAlmacenesOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@Activo", EstatusEnum.Activo.GetHashCode()},
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
        /// Obtiene los parametros necesarios para obtener los movimientos por contrato
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFolioAlmacen(FiltroCierreDiaInventarioInfo filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@AlmacenID", filtros.AlmacenID},
                        {"@TipoMovimientoID", filtros.TipoMovimientoID}
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
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento Almacen_ObtenerPorPaginaPoliza
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaPoliza(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtro.AlmacenID},
                            {"@Descripcion", filtro.Descripcion},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@TipoAlmacenID", filtro.TipoAlmacen.TipoAlmacenID},
                            {"@Activo", filtro.Activo},
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

        public static Dictionary<string, object> ObtenerPorAlmacenPoliza(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacen.AlmacenID},
                            {"@OrganizacionID", almacen.Organizacion.OrganizacionID},
                            {"@TipoAlmacenID", almacen.TipoAlmacen.TipoAlmacenID},
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
        /// Obtiene parametros para obtener lista paginada
        /// filtrandpo por varios tipos de almacén.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionTipoAlmacenPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtro.AlmacenID},
                            {"@Descripcion", filtro.Descripcion},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@FiltroTipoAlmacen", filtro.FiltroTipoAlmacen},
                            {"@Activo", filtro.Activo},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaTipoAlmacen(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in filtro.ListaTipoAlmacen
                                 select
                                     new XElement("TiposAlmacen",
                                                  new XElement("TipoAlmacenID", info.TipoAlmacenID))
                                     );
                parametros = new Dictionary<string, object>
                    {
                        {"@AlmacenID", filtro.AlmacenID},
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@XmlTiposAlmacen", xml.ToString()}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerPorIdFiltroTipoAlmacen(AlmacenInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from info in filtro.ListaTipoAlmacen
                                 select
                                     new XElement("TiposAlmacen",
                                                  new XElement("TipoAlmacenID", info.TipoAlmacenID))
                                     );
                parametros = new Dictionary<string, object>
                    {
                        {"@AlmacenID", filtro.AlmacenID},
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@Activo", filtro.Activo},
                        {"@XmlTiposAlmacen", xml.ToString()}
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
        /// Obtiene un almacen por tipo y organizacion
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorTipoAlmacenIdOrganizacionId(AlmacenInfo almacenInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TipoAlmacenID", almacenInfo.TipoAlmacen.TipoAlmacenID},
                            {"@ProductoID", almacenInfo.Organizacion.OrganizacionID},
                            {"@Activo", almacenInfo.Activo}
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
        /// Generar parametros para validar si existen ajustes pendientes por aplicar en almacen
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ValidarProductosEnAlmacen(AlmacenInfo almacenInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenInfo.AlmacenID},
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
        /// Generar parametros para validar si existen ajustes pendientes por aplicar en almacen
        /// </summary>
        /// <param name="productoID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidarExistenciasProductoEnAlmacen(int productoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", productoID},
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
        /// Genera parametros para guardar almacen Movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroGuardarAlmacenMovimientoConFecha(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenMovimientoInfo.AlmacenID},
                            {"@AnimalMovimientoID", almacenMovimientoInfo.AnimalMovimientoID},
                            {"@TipoMovimientoID", almacenMovimientoInfo.TipoMovimientoID},
                            {"@FechaMovimiento", almacenMovimientoInfo.FechaMovimiento},
                            {"@Status", almacenMovimientoInfo.Status},
                            {"@Observaciones", almacenMovimientoInfo.Observaciones},
                            {"@UsuarioCreacionID", almacenMovimientoInfo.UsuarioCreacionID},
                            {"@ProveedorID", almacenMovimientoInfo.ProveedorId}
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
        /// Genera los parametros para consultar un almacen por su id y su organizacion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorIDOrganizacion(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", info.AlmacenID},
                            {"@OrganizacionID", info.Organizacion.OrganizacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        internal static Dictionary<string, object> ObtenerParmetrosPorProducto(FiltroAlmacenProductoEnvio filtroEnvio)
        {
             try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                             {"@UsuarioID", filtroEnvio.UsaurioID},
                            {"@ProductoID", filtroEnvio.ProductoID},
                            {"@Cantidad", filtroEnvio.Cantidad},
                            {"@Activo", filtroEnvio.Activo}
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
