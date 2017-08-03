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
    internal class AuxContratoDAL
    {
        /// <summary>
        ///   Obtiene una lista de contratos filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerContratosPorEstado(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", estatus.GetHashCode()}
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
        /// Parametros para obtener por id
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorId(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoInfo.ContratoId},
                            {"@OrganizacionID", contratoInfo.Organizacion.OrganizacionID}
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
        /// Parametros para obtener por id
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFolio(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Folio", contratoInfo.Folio},
                            {"@Organizacion", contratoInfo.Organizacion.OrganizacionID}
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
        /// Obtiene los parametros para consultar por proveedorid
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerContratosPorProveedorId(int proveedorId,int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", proveedorId},
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
        /// Obtiene los parametros para insertar un nuevo contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearContrato(ContratoInfo contratoInfo, int tipoFolio)
        {
            try
            {
                Logger.Info();

                contratoInfo.Cuenta = contratoInfo.Cuenta ?? new CuentaSAPInfo();

                var parametros =
                    new Dictionary<string, object>
                    {
                        { "@OrganizacionID", contratoInfo.Organizacion.OrganizacionID },
                        { "@ProductoID", contratoInfo.Producto.ProductoId },
                        { "@TipoContratoID", contratoInfo.TipoContrato.TipoContratoId },
                        { "@TipoFlete", contratoInfo.TipoFlete.TipoFleteId },
                        { "@ProveedorID", contratoInfo.Proveedor.ProveedorID },
                        { "@Precio", contratoInfo.Precio },
                        { "@TipoCambio", contratoInfo.TipoCambio.TipoCambioId },
                        { "@Cantidad", contratoInfo.Cantidad },
                        { "@Tolerancia", contratoInfo.Tolerancia },
                        { "@Parcial", contratoInfo.Parcial },
                        { "@CuentaSAPID", contratoInfo.Cuenta.CuentaSAPID },
                        { "@EstatusID", contratoInfo.Estatus.EstatusId },
                        { "@Merma", contratoInfo.Merma },
                        { "@PesoNegociar", contratoInfo.PesoNegociar },
                        { "@Activo", contratoInfo.Activo },
                        { "@UsuarioCreacionID", contratoInfo.UsuarioCreacionId },
                        { "@TipoFolio", tipoFolio },
                        { "@FechaVigencia", contratoInfo.FechaVigencia },
                        { "@FolioAserca", contratoInfo.FolioAserca },
                        { "@FolioCobertura", contratoInfo.FolioCobertura },
                        { "@CalidadOrigen", contratoInfo.CalidadOrigen },
                        { "@CostoSecado", contratoInfo.CostoSecado },
                        { "@AplicaDescuento", contratoInfo.AplicaDescuento }
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
        /// Obtiene los parametros para actualizar el estado del contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstado(ContratoInfo contratoInfo, EstatusEnum estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ContratoID", contratoInfo.ContratoId},
                                     {"@EstatusID", contratoInfo.Estatus.EstatusId},
                                     {"@Activo", estatus},
                                     {"@UsuarioModificacionID", contratoInfo.UsuarioModificacionId},
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
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ContratoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                filtro.PesoNegociar = filtro.PesoNegociar ?? "";
                filtro.Proveedor = filtro.Proveedor ?? new ProveedorInfo(){CodigoSAP = ""};
                filtro.Proveedor.CodigoSAP = filtro.Proveedor.CodigoSAP ?? "";
                filtro.TipoCambio = filtro.TipoCambio ?? new TipoCambioInfo();
                filtro.Organizacion = filtro.Organizacion ?? new OrganizacionInfo();
                filtro.Producto = filtro.Producto ?? new ProductoInfo();
                filtro.TipoContrato = filtro.TipoContrato ?? new TipoContratoInfo();
                filtro.TipoFlete = filtro.TipoFlete ?? new TipoFleteInfo();
                filtro.Proveedor = filtro.Proveedor ?? new ProveedorInfo();
                filtro.Estatus = filtro.Estatus ?? new EstatusInfo();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@ContratoID", filtro.ContratoId},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Folio", filtro.Folio},
                                     {"@ProductoDescripcion",filtro.ProductoDescripcion},
                                     {"@ProductoID", filtro.Producto.ProductoId},
                                     {"@TipoContratoID", filtro.TipoContrato.TipoContratoId},
                                     {"@TipoFleteID", filtro.TipoFlete.TipoFleteId},
                                     {"@ProveedorID", filtro.Proveedor.ProveedorID},
                                     {"@CodigoSAP", filtro.Proveedor.CodigoSAP},
                                     {"@EstatusID", filtro.Estatus.EstatusId},
                                     {"@TipoCambioID", filtro.TipoCambio.TipoCambioId},
                                     {"@PesoNegociar", filtro.PesoNegociar},
                                     {"@Activo", filtro.Activo},
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

        internal static Dictionary<string, object> ObtenerParametrosPorContenedor(ContratoInfo contenedor)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Folio", contenedor.Folio},
                                     {"@OrganizacionID", contenedor.Organizacion.OrganizacionID},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorContenedorPaginado(PaginacionInfo pagina, ContratoInfo contenedor)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Folio", contenedor.Folio},
                                     {"@OrganizacionID", contenedor.Organizacion.OrganizacionID},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite},
                                     {
                                         "@Proveedor",
                                         string.IsNullOrWhiteSpace(contenedor.Proveedor.Descripcion)
                                             ? string.Empty
                                             : contenedor.Proveedor.Descripcion
                                     },
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static string ObtenerParametrosPorXML(List<int> contratosId)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in contratosId
                                 select new XElement("Contrato",
                                                     new XElement("ContratoID", detalle)
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
        /// Obtiene los parametros necesarios para la 
        /// ejecucion del procedimiento almacenado
        /// Contrato_ObtenerConciliacionMovimientosSIAP
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFechasConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FechaInicial", fechaInicio},
                                     {"@FechaFinal", fechaFin},
                                     {"@OrganizacionID", organizacionID},
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
        /// Obtiene los parametros para actualizar la cantidad del contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCantidadContrato(ContratoInfo contratoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ContratoID", contratoInfo.ContratoId},
                                     {"@Cantidad", contratoInfo.Cantidad},
                                     {"@Precio", contratoInfo.Precio},
                                     {"@UsuarioModificacionID", contratoInfo.UsuarioModificacionId},
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
        /// Obtiene los parametros para consultar por proveedorid
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerContratosPorProveedorAlmacenID(FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", filtro.Proveedor.ProveedorID},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@AlmacenID", filtro.Almacen.AlmacenID }
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
        /// Contrato_ObtenerPorProveedorConEntradaProducto
        /// </summary>
        /// <param name="proveedorInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosContratoPorProveedorEntradaProducto(ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", proveedorInfo.ProveedorID},
                            {"@OrganizacionID", proveedorInfo.OrganizacionID},
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
        /// Obtiene los parametros para actualizar el contrato
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarContrato(ContratoInfo contratoInfo, EstatusEnum estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ContratoID", contratoInfo.ContratoId},
                                     {"@EstatusID", contratoInfo.Estatus.EstatusId},
                                     {"@Activo", estatus},
                                     {"@Tolerancia", contratoInfo.Tolerancia},
                                     {"@Cantidad", contratoInfo.Cantidad},
                                     {"@PesoNegociar", contratoInfo.PesoNegociar},
                                     {"@FolioAserca", contratoInfo.FolioAserca},
                                     {"@FolioCobertura", contratoInfo.FolioCobertura},
                                     {"@CalidadOrigen", contratoInfo.CalidadOrigen},
                                     {"@AplicaDescuento", contratoInfo.AplicaDescuento},
                                     {"@TipoFleteID", contratoInfo.TipoFlete.TipoFleteId},
                                     {"@UsuarioModificacionID", contratoInfo.UsuarioModificacionId},
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
