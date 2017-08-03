using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxSolicitudProductoDetalleDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, SolicitudProductoDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SolicitudProductoDetalleID", filtro.SolicitudProductoDetalleID},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(SolicitudProductoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@SolicitudProductoDetalleID", info.SolicitudProductoDetalleID},
							{"@SolicitudProductoID", info.SolicitudProducto.SolicitudProductoID},
							{"@ProductoID", info.Producto.ProductoId},
							{"@Cantidad", info.Cantidad},
							{"@CamionRepartoID", info.CamionReparto.CamionRepartoID},
							{"@EstatusID", info.Estatus.EstatusId},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(SolicitudProductoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@SolicitudProductoDetalleID", info.SolicitudProductoDetalleID},
							{"@SolicitudProductoID", info.SolicitudProducto.SolicitudProductoID},
							{"@ProductoID", info.Producto.ProductoId},
							{"@Cantidad", info.Cantidad},
							{"@CamionRepartoID", info.CamionReparto.CamionRepartoID},
							{"@EstatusID", info.Estatus.EstatusId},
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
        /// <param name="solicitudProductoDetalleID">Identificador de la entidad SolicitudProductoDetalle</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int solicitudProductoDetalleID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@SolicitudProductoDetalleID", solicitudProductoDetalleID}
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
        public static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
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
        public static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@SolicitudProductoDetalleID", descripcion}
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
        /// <param name="lista">Valores de la entidad</param>
        ///  <param name="solicitudProductoID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> GuardarSolicitudDetalle(List<SolicitudProductoDetalleInfo> lista, int solicitudProductoID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in lista
                                              select
                                                  new XElement("SolicitudProductoDetalle",
                                                               new XElement("SolicitudProductoDetalleID", info.SolicitudProductoDetalleID),
                                                               new XElement("SolicitudProductoID", info.SolicitudProductoID),
                                                               new XElement("ProductoID", info.Producto.ProductoId),
                                                               new XElement("Cantidad", info.Cantidad),
                                                               new XElement("CamionRepartoID", info.CamionRepartoID),
                                                               new XElement("EstatusID", info.EstatusID),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SolicitudProductoDetalleXML", xml.ToString()}
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
        /// <param name="lista">Valores de la entidad</param>
        ///  <param name="solicitudProductoID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> MarcarProductosRecibidos(List<SolicitudProductoReplicaDetalleInfo> lista, long folioSolicitud)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in lista
                                              select
                                                  new XElement("SolicitudProductoDetalle",
                                                               new XElement("InterfaceTraspasoSAPID", info.InterfaceTraspasoSAPID),
                                                               new XElement("FolioSolicitud", folioSolicitud),
                                                               new XElement("ProductoID", info.Producto.ProductoId),
                                                               new XElement("AlmacenMovimientoID", info.AlmacenMovimientoID),
                                                               new XElement("Cantidad", info.Cantidad),
                                                               new XElement("EstatusID", info.EstatusID),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SolicitudProductoDetalleXML", xml.ToString()}
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

