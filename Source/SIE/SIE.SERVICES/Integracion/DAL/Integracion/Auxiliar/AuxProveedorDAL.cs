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

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxProveedorDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID", filtro.ProveedorID },
                        {"@CodigoSAP", filtro.CodigoSAP},
                        {"@Descripcion", filtro.Descripcion ?? string.Empty },
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

        /// <summary>
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(ProveedorInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Descripcion", info.Descripcion},
                                     {"@CodigoSAP", info.CodigoSAP},
                                     {"@TipoProveedorID", info.TipoProveedor.TipoProveedorID},
                                     {"@ImporteComision", info.ImporteComision},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID},
                                     {"@CorreoElectronico", info.Correo},
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
        ///     Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(ProveedorInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", info.ProveedorID},
                                     {"@CodigoSAP", info.CodigoSAP},
                                     {"@Descripcion", info.Descripcion},
                                     {"@TipoProveedorID", info.TipoProveedor.TipoProveedorID},
                                     {"@ImporteComision", info.ImporteComision},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID},
                                     {"@CorreoElectronico", info.Correo}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="proveedorID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int proveedorID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", proveedorID}
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
        ///     Obtiene Parametros pora filtrar por estatus
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
        ///     Obtiene Parametros por Id CodigoSAP
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorCodigoSAP(ProveedorInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CodigoSAP", info.CodigoSAP}
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
        /// Obtiene Parametro pora obtener el proveedor del lote
        /// </summary> 
        /// <param name="lote">Lote</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@LoteID", lote.LoteID},
                                {"@OrganizacionID", lote.OrganizacionID},
                                {"@Estatus", (int)EstatusEnum.Activo},

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
        /// Obtiene Parametro para obtener proveedores por tipo proveedor id
        /// </summary>
        /// <param name="estatus"></param>
        /// <param name="tipoProveedorID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorTipoProveedorID(int estatus, int tipoProveedorID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
							{
								{"@Activo", estatus},
                                {"@TipoProveedorID", tipoProveedorID}
							};
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosPorPaginaTipoProveedor(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID", filtro.ProveedorID},
                        {"@CodigoSAP", filtro.CodigoSAP},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@TipoProveedorID",(int)TipoProveedorEnum.ProveedoresFletes},
                        { "@TipoProveedorMateriaPrima", (int)TipoProveedorEnum.ProveedoresDeMateriaPrima }
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametrosPorPaginaFiltros(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID", filtro.ProveedorID},
                        {"@CodigoSAP", filtro.CodigoSAP},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@TipoProveedorID", filtro.TipoProveedor.TipoProveedorID}
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
        /// Obtiene los parametros para obtener por pagina una lista de proveedores por tipo de proveedor
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaTiposProveedores(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var element = new XElement("ROOT",
                                                from tipoProveedor in filtro.ListaTiposProveedor
                                                select new XElement("Datos",
                                                                    new XElement("tipoProveedor",
                                                                                 tipoProveedor.TipoProveedorID)));

                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID", filtro.ProveedorID},
                        {"@CodigoSAP", filtro.CodigoSAP ?? ""},
                        {"@Descripcion", filtro.Descripcion},
                        {"@xmlTipoCuenta", element.ToString()},
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

        /// <summary>
        /// Obtiene los parametros para consultar los proveedores que tiene asignado un producto en la tabla fletes internos
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerProveedoresEnFletesInternos(int productoId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
							{
								{"@ProductoId", productoId},
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

       
        public static Dictionary<string, object> ObtenerParametrosPorPaginaFletesInternos(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CodigoSAP", filtro.CodigoSAP},
                        {"@Descripcion", filtro.Descripcion},
                        {"@ProductoId", filtro.ProductoID},
                        {"@OrganizacionID", filtro.OrganizacionID},
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado Proveedor_ObtenerPorProductoContratoCodigoSAP
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorContratoCodigoSAP(ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CodigoSAP", filtro.CodigoSAP.PadLeft(10, '0')},
                        {"@ProductoID", filtro.ProductoID},
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Activo", filtro.Activo},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Proveedor_ObtenerPorPaginaProductoContrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaProductoContrato(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Descripcion", filtro.Descripcion},
                        {"@ProductoID", filtro.ProductoID},
                        {"@OrganizacionID", filtro.OrganizacionID},
                        {"@Activo", filtro.Activo},
                        {"@Inicio ", pagina.Inicio},
                        {"@Limite", pagina.Limite},
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorIDGanaderas(ProveedorInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", info.ProveedorID},
                                     {"@OrganizacionID", info.OrganizacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorPaginaGanaderas(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProveedorID", filtro.ProveedorID},
                        {"@CodigoSAP", filtro.CodigoSAP},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@OrganizacionId", filtro.OrganizacionID}
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
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado Proveedor_ObtenerPorProductoContratoCodigoSAP
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCodigoSAPEmbarque(ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CodigoSAP", filtro.CodigoSAP.PadLeft(10, '0')},
                        {"@EmbarqueID", filtro.EmbarqueID}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Proveedor_ObtenerPorPaginaProductoContrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaEmbarque(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Descripcion", filtro.Descripcion},
                        {"@EmbarqueID", filtro.EmbarqueID},
                        {"@Activo", filtro.Activo},
                        {"@Inicio ", pagina.Inicio},
                        {"@Limite", pagina.Limite},
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
        ///     Obtiene Parametros por proveedorID, OrigeinID, DestinoID, Activo
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerProveedorConfiguradoOrigenDestino(EmbarqueDetalleInfo embarque)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", embarque.Proveedor.ProveedorID},
                                     {"@OrigenID",embarque.OrganizacionOrigen.OrganizacionID},
                                     {"@DestinoID",embarque.OrganizacionDestino.OrganizacionID},
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
        /// Obtiene Parametro para obtener proveedores por ruta
        /// </summary>
        /// <param name="estatus"></param>
        /// <param name="tipoProveedorID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerProveedoresPorRuta(int estatus, int tipoProveedorID, int @ConfiguracionEmbarqueDetalleID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
							{
								{"@Activo", estatus},
                                {"@TipoProveedorID", tipoProveedorID},
                                {"@ConfiguracionEmbarqueDetalleID", ConfiguracionEmbarqueDetalleID},
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
