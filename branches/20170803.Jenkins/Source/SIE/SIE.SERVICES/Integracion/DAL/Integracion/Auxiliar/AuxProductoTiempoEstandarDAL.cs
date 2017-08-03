using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProductoTiempoEstandarDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ProductoTiempoEstandarInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", filtro.Producto.ProductoId},
                            {"@Activo", filtro.Estatus.GetHashCode()},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardar(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", productoTiempoEstandarInfo.Producto.ProductoId},
                            {"@Estatus", productoTiempoEstandarInfo.Estatus.GetHashCode()},
                            {"@Tiempo", productoTiempoEstandarInfo.Tiempo},
                            {"@UsuarioCreacion", productoTiempoEstandarInfo.UsuarioCreacionID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosActualizar(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoTiempoEstandarID", productoTiempoEstandarInfo.ProductoTiempoEstandarID},
                            {"@ProductoID", productoTiempoEstandarInfo.Producto.ProductoId},
                            {"@Estatus", productoTiempoEstandarInfo.Estatus.GetHashCode()},
                            {"@Tiempo", productoTiempoEstandarInfo.Tiempo},
                            {"@UsuarioCreacion", productoTiempoEstandarInfo.UsuarioCreacionID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerPorProductoID(ProductoTiempoEstandarInfo productoTiempoEstandarInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProductoID", productoTiempoEstandarInfo.Producto.ProductoId}
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
