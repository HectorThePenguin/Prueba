using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxFleteInternoDAL
    {
        /// <summary>
        /// Obtiene parametros para crear un flete interno
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearFleteInterno(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", fleteInternoInfo.Organizacion.OrganizacionID},
                            {"@TipoMovimientoID", fleteInternoInfo.TipoMovimiento.TipoMovimientoID},
                            {"@AlmacenIDOrigen", fleteInternoInfo.AlmacenOrigen.AlmacenID},
                            {"@AlmacenIDDestino", fleteInternoInfo.AlmacenDestino.AlmacenID},
                            {"@ProductoID", fleteInternoInfo.Producto.ProductoId},
                            {"@Activo", fleteInternoInfo.Activo.GetHashCode()},
                            {"@UsuarioCreacionID", fleteInternoInfo.UsuarioCreacionId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerFletesPorEstado(EstatusEnum estatus)
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
            }
            return null;
        }

        /// <summary>
        /// Obtiene parametros para obtener un listado de fletes internos
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorPaginaFiltroDescripcionOrganizacion(PaginacionInfo pagina, FleteInternoInfo filtro)
        {
            try
            {
                Logger.Info();

                filtro.Organizacion = filtro.Organizacion ?? new OrganizacionInfo();
                filtro.TipoMovimiento = filtro.TipoMovimiento ?? new TipoMovimientoInfo();
                filtro.AlmacenOrigen = filtro.AlmacenOrigen ?? new AlmacenInfo();
                filtro.AlmacenDestino = filtro.AlmacenDestino ?? new AlmacenInfo();
                filtro.Producto = filtro.Producto ?? new ProductoInfo();

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FleteInternoID", filtro.FleteInternoId},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@DescripcionOrganizacion", filtro.Organizacion.Descripcion},
                            {"@ParametroTipoMovimientoID", TipoMovimiento.PaseProceso},
                            {"@TipoMovimientoID", filtro.TipoMovimiento.TipoMovimientoID},
                            {"@AlmacenIDOrigen", filtro.AlmacenOrigen.AlmacenID},
                            {"@AlmacenIDDestino", filtro.AlmacenDestino.AlmacenID},
                            {"@ProductoID", filtro.Producto.ProductoId},
                            {"@Activo", filtro.Activo.GetHashCode()},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Actualizar flete interno detalle
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstado(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FleteInternoID", fleteInternoInfo.FleteInternoId},
                            {"@Activo", fleteInternoInfo.Activo.GetHashCode()},
                            {"@UsuarioModificacionID", fleteInternoInfo.UsuarioModificacionId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene un flete interno por configuracion ingresada
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorConfiguracion(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", fleteInternoInfo.Organizacion.OrganizacionID},
                            {"@TipoMovimientoID", fleteInternoInfo.TipoMovimiento.TipoMovimientoID},
                            {"@AlmacenIDOrigen", fleteInternoInfo.AlmacenOrigen.AlmacenID},
                            {"@AlmacenIDDestino", fleteInternoInfo.AlmacenDestino.AlmacenID},
                            {"@ProductoID", fleteInternoInfo.Producto.ProductoId},
                            {"@Activo", fleteInternoInfo.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtener costos por flete configuracion
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerCostosPorFleteConfiguracion(FleteInternoInfo fleteInternoInfo, ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"ProveedorID", proveedorInfo.ProveedorID},
                            {"@OrganizacionID", fleteInternoInfo.Organizacion.OrganizacionID},
                            {"@AlmacenIDOrigen", fleteInternoInfo.AlmacenOrigen.AlmacenID},
                            {"@ProductoID", fleteInternoInfo.Producto.ProductoId},
                            {"@Activo", EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
