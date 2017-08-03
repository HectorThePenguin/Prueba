
using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar 
{
    internal class AuxEntradaProductoDAL 
    {
        /// <summary>
        /// Obtiene los parametros para consultar por tipo de estatus
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerListaPorEstatusId(int estatusId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EstatusID", estatusId},
                            {"@OrganizacionID", organizacionId}
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
        /// Obtiene los parametros para consultar por organizacion y activo
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerListaPorOrganizacionId(int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId}
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
        /// Obtiene los parametros para consultar por organizacion y activo
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerListaPorOrganizacionId(int organizacionId, int activo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@Activo", activo}
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
        /// Obtiene los parametros para actualizar la entrada de producto
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEntradaProductoLlegada(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoId", entradaProducto.EntradaProductoId},
                            {"@PesoOrigen", entradaProducto.PesoOrigen},
                            {"@PesoBonificacion", entradaProducto.PesoBonificacion},
                            {"@PesoBruto", entradaProducto.PesoBruto},
                            {"@PesoTara", entradaProducto.PesoTara},
                            {"@OperadorIdBascula", entradaProducto.OperadorBascula.OperadorID},
                            {"@PesoDescuento", entradaProducto.PesoDescuento},
                            {"@NotaVenta", entradaProducto.NotaDeVenta == null ? "" : entradaProducto.NotaDeVenta},
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
        /// 
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarEntradaProducto(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                       from entradaProductoDetalle in entradaProducto.ProductoDetalle 
                        select
                            new XElement("EntradaProducto",
                                new XElement("TipoFolio", tipoFolio),
                                new XElement("ContratoID", entradaProducto.Contrato.ContratoId),
                                new XElement("TipoContratoID", entradaProducto.TipoContrato.TipoContratoId),
                                new XElement("OrganizacionID", entradaProducto.Organizacion.OrganizacionID),
                                new XElement("ProductoID", entradaProducto.Producto.ProductoId),
                                new XElement("RegistroVigilanciaID", entradaProducto.RegistroVigilancia.RegistroVigilanciaId),
                                new XElement("UsuarioCreacionID", entradaProducto.UsuarioCreacionID),
                                new XElement("OperadorIDAnalista", entradaProducto.OperadorAnalista.OperadorID),
                                new XElement("EstatusID", entradaProducto.Estatus.EstatusId),
                                new XElement("IndicadorID", entradaProductoDetalle.Indicador.IndicadorId),
                                new XElement("Porcentaje", entradaProductoDetalle.ProductoMuestras[entradaProductoDetalle.ProductoMuestras.Count - 1].Porcentaje),
                                new XElement("Descuento", entradaProductoDetalle.ProductoMuestras[entradaProductoDetalle.ProductoMuestras.Count - 1].Descuento),
                                new XElement("Rechazo", 0),
                                new XElement("FechaEmbarque", entradaProducto.FechaEmbarque),
                                new XElement("FolioOrigen", entradaProducto.FolioOrigen),
                                new XElement("EsOrigen", (int)entradaProductoDetalle.ProductoMuestras[entradaProductoDetalle.ProductoMuestras.Count - 1].EsOrigen)
                            )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLEntrada", xml.ToString()}
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
        /// Parametros para autorizar una entrada
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAutorizaEntrada(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                           new XElement("EntradaProducto",
                               new XElement("Folio", entradaProducto.Folio),
                               new XElement("OrganizacionID", entradaProducto.Organizacion.OrganizacionID),
                               new XElement("Justificacion", entradaProducto.Justificacion),
                               new XElement("UsuarioModificacionID", entradaProducto.UsuarioModificacionID),
                               new XElement("OperadorIDAutoriza", entradaProducto.OperadorAutoriza.OperadorID),
                               new XElement("EstatusID", entradaProducto.Estatus.EstatusId)
                           )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLEntrada", xml.ToString()}
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
        /// Obtiene los parametros para consultar por organizacion y folio para el filtro
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerEntradaProductoValido(int organizacionId, int folio)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@Folio", folio}
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
        /// Obtiene parametros para obtener lista paginada de los
        /// </summary>
        /// <param name="pagina">Configuración de paginacion</param>
        /// <param name="filtro">Filtro se requiere folio, organizacion,etatus </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFoliosPorPaginaParaEntradaMateriaPrima(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                if (filtro.Producto == null)
                {
                    filtro.Producto = new ProductoInfo();
                }
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Folio", filtro.FolioBusqueda},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@DescripcionProducto",filtro.DescripcionProducto},
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        /// Obtiene parametros para obtener lista paginada de los
        /// </summary>
        /// <param name="filtro">Filtro se requiere folio, organizacion,etatus </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorFolioEntradaMateriaPrima(EntradaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Folio", filtro.FolioBusqueda},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Activo", EstatusEnum.Activo}
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
        /// Obtiene una lista paginada de los folios por producto de entrada de tipo Materias Primas y EstatusID
        /// </summary>
        /// <param name="pagina">Configuración de paginacion</param>
        /// <param name="filtro">Filtro se requiere folio, organizacion,etatus </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFoliosPorPaginaParaEntradaMateriaPrimaEstatus(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                if (filtro.Producto == null)
                {
                    filtro.Producto = new ProductoInfo();
                }
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Folio", filtro.FolioBusqueda},
                                     {"@DescripcionProducto",filtro.DescripcionProducto},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@FamiliaID", filtro.Producto.FamiliaId},
                                     {"@EstatusID", filtro.Estatus.EstatusId},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        /// Devuelve los parametros necesarios para almacenar la entrada de producto sin detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarEntradaProductoSinDetalle(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            try
            {
                Logger.Info();

                var contratoID = (entradaProducto.Contrato != null && 
                                  entradaProducto.Contrato.ContratoId > 0) ? entradaProducto.Contrato.ContratoId : 0;
                var tipoContratoID = (entradaProducto.TipoContrato != null && 
                                      entradaProducto.TipoContrato.TipoContratoId > 0) ? entradaProducto.TipoContrato.TipoContratoId : 0;
                var operadorIDAnalista = (entradaProducto.OperadorAnalista != null &&
                                          entradaProducto.OperadorAnalista.OperadorID > 0) ? entradaProducto.OperadorAnalista.OperadorID : 0;

                var xml =
                   new XElement("ROOT",
                       new XElement("EntradaProducto",
                               new XElement("TipoFolio", tipoFolio),
                               new XElement("ContratoID", contratoID),
                               new XElement("TipoContratoID", tipoContratoID),
                               new XElement("OrganizacionID", entradaProducto.Organizacion.OrganizacionID),
                               new XElement("ProductoID", entradaProducto.Producto.ProductoId),
                               new XElement("RegistroVigilanciaID", entradaProducto.RegistroVigilancia.RegistroVigilanciaId),
                               new XElement("UsuarioCreacionID", entradaProducto.UsuarioCreacionID),
                               new XElement("OperadorIDAnalista", operadorIDAnalista),
                               new XElement("EstatusID", entradaProducto.Estatus.EstatusId),
                               new XElement("Observaciones",entradaProducto.Observaciones),
                               new XElement("FolioOrigen", entradaProducto.datosOrigen == null ? 0 : entradaProducto.datosOrigen.folioOrigen),
                               new XElement("FechaEmbarque", entradaProducto.datosOrigen == null ? new DateTime(1900, 1, 1) : Convert.ToDateTime(entradaProducto.datosOrigen.fechaOrigen))
                           )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLEntrada", xml.ToString()}
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
        /// Devuelve los parametros necesarios para almacenar la entrada de producto sin detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEntradaProductoSinDetalle(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                       new XElement("EntradaProducto",
                               new XElement("EntradaProductoID", entradaProducto.EntradaProductoId),
                               new XElement("ContratoID", entradaProducto.Contrato.ContratoId),
                               new XElement("TipoContratoID", entradaProducto.TipoContrato.TipoContratoId),
                               new XElement("OrganizacionID", entradaProducto.Organizacion.OrganizacionID),
                               new XElement("ProductoID", entradaProducto.Producto.ProductoId),
                               new XElement("RegistroVigilanciaID", entradaProducto.RegistroVigilancia.RegistroVigilanciaId),
                               new XElement("UsuarioModificacionID", entradaProducto.UsuarioModificacionID),
                               new XElement("OperadorIDAnalista", entradaProducto.OperadorAnalista.OperadorID),
                               new XElement("EstatusID", entradaProducto.Estatus.EstatusId),
                               new XElement("Observaciones", entradaProducto.Observaciones)
                           )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLEntrada", xml.ToString()}
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
        /// Obtiene los parametros actulizar el lote y las piezas de la entrada
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaLoteEnPatio(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoId", entradaProducto.EntradaProductoId},
                            {"@AlmacenInventaioLoteId", entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId}
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
        /// Método para actualizar la fecha de inicio y fin de descarga
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaFechaDescargaPiezasEnPatio(EntradaProductoInfo entrada)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoId", entrada.EntradaProductoId},
                            {"@Piezas", entrada.Piezas}
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
        /// Obtiene los parametros para actualizar el movimiento de la entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarAlmacenMovimiento(EntradaProductoInfo entradaProducto, AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                long almacenMovimientoSalidaId = 0;
                if (entradaProducto.AlmacenMovimiento != null)
                {
                    almacenMovimientoSalidaId = entradaProducto.AlmacenMovimiento.AlmacenMovimientoID;
                }
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoID", entradaProducto.EntradaProductoId},
                            {"@AlmacenMovimientoID", almacenMovimientoInfo.AlmacenMovimientoID},
                            {"@AlmacenMovimientoSalidaID",almacenMovimientoSalidaId}
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
        /// Obtiene los parametros para obtener la entrada producto por su identificador
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerEntradaProductoPorId(int entradaProductoId)
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
        /// Obtiene los parametros necesarios para la 
        /// ejecucion del procedimiento almacenado
        /// EntradaProducto_ObtenerCostosAlmacen
        /// </summary>
        /// <param name="contratoID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosContratoOrganizacion(int contratoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoID},
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

        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// EntradaProducto_ObtenerFolioEntradaContrato
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="contratoId"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFolioEntradaContrato(int folioEntrada, int contratoId, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoId},
                            {"@Folio", folioEntrada},
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

        /// <summary>
        /// Obtiene los parametros para consultar por organizacion, producto y contrato
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerEntradaPorContratoId(EntradaProductoInfo entradaProductoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", entradaProductoInfo.Contrato.ContratoId},
                            {"@OrganizacionID", entradaProductoInfo.Organizacion.OrganizacionID},
                            {"@ProductoID", entradaProductoInfo.Producto.ProductoId},
                            {"@Activo", entradaProductoInfo.Activo}
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
        /// Obtiene los parametrso para consultar por contratoid
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEntradaProductoPorContrato(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoInfo.ContratoId}
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
        /// Obtiene los parametros para consultar por organizacion y folio para el filtro Ayuda
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerEntradaProductoValidoAyuda(EntradaProductoInfo entrada)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", entrada.Organizacion.OrganizacionID},
                            {"@Folio", entrada.Folio},
                            {"@TipoContrato", entrada.TipoContrato.TipoContratoId}
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
        /// del procedimiento almacenado EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML
        /// </summary>
        /// <param name="movimientosEntrada"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradasPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> movimientosEntrada)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from movs in movimientosEntrada
                                 select
                                     new XElement("AlmacenMovimiento",
                                                  new XElement("AlmacenMovimientoID", movs.AlmacenMovimientoID)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoXML", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                if (filtro.Producto == null)
                {
                    filtro.Producto = new ProductoInfo();
                }

                var xmlFamilia =
                    new XElement("ROOT",
                                 from familia in filtro.Producto.Familias
                                 select
                                     new XElement("Familia",
                                                  new XElement("FamiliaID", familia.FamiliaID)
                                     ));

                var xmlSubFamilia =
                    new XElement("ROOT",
                                 from subFamilia in filtro.Producto.SubFamilias
                                 select
                                     new XElement("SubFamilia",
                                                  new XElement("SubFamiliaID", subFamilia.SubFamiliaID)
                                     ));


                parametros = new Dictionary<string, object>
                                 {
                                     {"@TipoMovimientoID",TipoMovimiento.EntradaPorCompra},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Fecha",filtro.Fecha},
                                     {"@Descripcion",filtro.DescripcionProducto},
                                     {"@XMLFamilias",xmlFamilia.ToString()},
                                     {"@XMLSubFamilias",xmlSubFamilia.ToString()},
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                if (filtro.Producto == null)
                {
                    filtro.Producto = new ProductoInfo();
                }

                var xmlFamilia =
                    new XElement("ROOT",
                                 from familia in filtro.Producto.Familias
                                 select
                                     new XElement("Familia",
                                                  new XElement("FamiliaID", familia.FamiliaID)
                                     ));

                var xmlSubFamilia =
                    new XElement("ROOT",
                                 from subFamilia in filtro.Producto.SubFamilias
                                 select
                                     new XElement("SubFamilia",
                                                  new XElement("SubFamiliaID", subFamilia.SubFamiliaID)
                                     ));


                parametros = new Dictionary<string, object>
                                 {
                                     {"@TipoMovimientoID",TipoMovimiento.EntradaAlmacen},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Fecha",filtro.Fecha},
                                     {"@Descripcion",filtro.DescripcionProducto},
                                     {"@XMLFamilias",xmlFamilia.ToString()},
                                     {"@XMLSubFamilias",xmlSubFamilia.ToString()},
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametroPorFolioPorEntradaTraspasoCancelacion(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                if (filtro.Producto == null)
                {
                    filtro.Producto = new ProductoInfo();
                }

                var xmlFamilia =
                    new XElement("ROOT",
                                 from familia in filtro.Producto.Familias
                                 select
                                     new XElement("Familia",
                                                  new XElement("FamiliaID", familia.FamiliaID)
                                     ));

                var xmlSubFamilia =
                    new XElement("ROOT",
                                 from subFamilia in filtro.Producto.SubFamilias
                                 select
                                     new XElement("SubFamilia",
                                                  new XElement("SubFamiliaID", subFamilia.SubFamiliaID)
                                     ));


                parametros = new Dictionary<string, object>
                                 {
                                     {"@TipoMovimientoID",TipoMovimiento.EntradaAlmacen},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Descripcion",filtro.DescripcionProducto},
                                     {"@XMLFamilias",xmlFamilia.ToString()},
                                     {"@XMLSubFamilias",xmlSubFamilia.ToString()},
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        /// Obtiene los parametros actulizar el lote y las piezas de la entrada
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaOperadorFechaDescargaEnPatio(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoId", entradaProducto.EntradaProductoId},
                            {"@OperadorIdAlmacen", entradaProducto.OperadorAlmacen.OperadorID}
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
        /// Obtiene los parametros para cancelar una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCancelar(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoID",entradaProducto.EntradaProductoId},
                            {"@EstatusID", Estatus.CanceladoEntPro.GetHashCode()},
                            {"@UsuarioModificacionID", entradaProducto.UsuarioCreacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerHumedadOrigen(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoID",entradaProducto.EntradaProductoId},
                            {"@OrganizacionID",entradaProducto.Organizacion.OrganizacionID},
                            {"@Activo",entradaProducto.Activo.GetHashCode()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ActualizarDescuentoEntradaProductoMuestra(EntradaProductoInfo entradaProducto, decimal descuento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaProductoID",entradaProducto.EntradaProductoId},
                            {"@Activo",(int)EstatusEnum.Activo},
                            {"@descuento",descuento},

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
