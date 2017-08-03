using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAlmacenMovimientoDAL
    {
        /// <summary>
        /// Crea un registro almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                //Validar si la observacion es nula
                almacenMovimientoInfo.Observaciones = almacenMovimientoInfo.Observaciones ?? "";
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenMovimientoInfo.AlmacenID},
                            {"@TipoMovimientoID", almacenMovimientoInfo.TipoMovimientoID},
                            {"@ProveedorID", almacenMovimientoInfo.ProveedorId},
                            {"@Status", almacenMovimientoInfo.Status},
                            {"@UsuarioCreacionID", almacenMovimientoInfo.UsuarioCreacionID},
                            {"@Observaciones", almacenMovimientoInfo.Observaciones},
                            {"@EsEnvioAlimento", almacenMovimientoInfo.EsEnvioAlimento ? 1 : 0 },
                            {"@OrganizacionOrigenID", almacenMovimientoInfo.OrganizacionID }
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
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorId(long almacenMovimientoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoId}
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
        /// Actualiza el estatus del registro
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstatus(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoInfo.AlmacenMovimientoID},
                            {"@Status", almacenMovimientoInfo.Status},
                            {"@Observaciones", almacenMovimientoInfo.Observaciones},
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
        /// Crea un registro almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarMovimientoCierreDiaPA(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenMovimientoInfo.AlmacenID},
                            {"@TipoMovimientoID", almacenMovimientoInfo.TipoMovimientoID},
                            {"@FolioMovimiento", almacenMovimientoInfo.FolioMovimiento},
                            {"@Status", almacenMovimientoInfo.Status},
                            {"@UsuarioCreacionID", almacenMovimientoInfo.UsuarioCreacionID},
                            {"@Observaciones", almacenMovimientoInfo.Observaciones}
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
        /// Crea un registro almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidarEjecucionCierreDia(int almacenID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenID},
                            {"@FechaMovimiento", DateTime.Now}
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
        /// AlmacenMovimiento_ObtenerMovimientosCierreDia
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosInventario(int almacenID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenID},
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

        internal static Dictionary<string, object> ObtenerParametrosActualizarGeneracionPoliza(List<ContenedorAlmacenMovimientoCierreDia> movimientos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from movs in movimientos
                                 select new XElement("AlmacenMovimiento",
                                        new XElement("AlmacenMovimientoID", movs.AlmacenMovimiento.AlmacenMovimientoID),
                                        new XElement("UsuarioModificacionID", movs.Almacen.UsuarioModificacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAlmacenMovimiento", xml.ToString()}
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
        /// Crea un registro almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerMovimientosPendientesAutorizar(FiltrosAutorizarCierreDiaInventarioPA filtrosAutorizarCierreDia)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtrosAutorizarCierreDia.AlmacenID},
                            {"@TipoMovimientoID", filtrosAutorizarCierreDia.TipoMovimientoID},
                            {"@EstatusMovimiento", filtrosAutorizarCierreDia.EstatusMovimiento},
                            {"@FechaMovimiento", filtrosAutorizarCierreDia.FechaMovimiento}
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
        /// Actualiza el estatus del registro
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstatusAlmacenMovimiento(FiltroCambiarEstatusInfo filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtros.AlmacenID},
                            {"@FolioMovimiento", filtros.FolioMovimiento},
                            {"@EstatusAnterior", filtros.EstatusAnterior},
                            {"@EstatusNuevo", filtros.EstatusNuevo},
                            {"@UsuarioModificacionID", filtros.UsuarioModificacionID}
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
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado AlmacenMovimiento_ObtenerMovimientoXML
        /// </summary>
        /// <param name="almancenMovimientosDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorClaveDetalle(List<AlmacenMovimientoDetalle> almancenMovimientosDetalle)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from movs in almancenMovimientosDetalle
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("AlmacenMovimientoDetalleID",
                                                                  movs.AlmacenMovimientoDetalleID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAlmacenMovimiento", xml.ToString()}
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
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorIDCompleto(long almacenMovimientoId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoID", almacenMovimientoId}
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
        /// AlmacenMovimiento_ObtenerMovimientosCierreDia
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosInventarioFiltros(FiltroAlmacenMovimientoInfo filtros)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", filtros.AlmacenID},
                            {"@OrganizacionID", filtros.OrganizacionID},
                            {"@FolioMovimiento", filtros.FolioMovimiento},
                            {"@TipoMovimientoID", filtros.TipoMovimientoID},
                            {"@EstatusID", filtros.EstatusID},
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
        /// AlmacenMovimiento_ObtenerMovimientosCierreDia
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosInventario(int almacenID, int organizacionID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenID", almacenID},
                            {"@OrganizacionID", organizacionID},
                            {"@FechaMovimiento", fecha},
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
        /// del procedimiento almacenado AlmacenMoviento_ObtenerSubProductos
        /// </summary>
        /// <param name="productosPremezcla"></param>
        /// <returns></returns>
        internal static string ObtenerParametrosMovimientosSubProductos(IEnumerable<AlmacenMovimientoSubProductosModel> productosPremezcla)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from movs in productosPremezcla
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("ProductoID",
                                                                  movs.ProductoID),
                                                     new XElement("FechaMovimiento",
                                                                  movs.FechaMovimiento)
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
        /// Obtiene los parametros 
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerMovimientosPorContrato(ContratoInfo contrato)
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
    }
}
