using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxConfiguracionEmbarqueDAL
    {
        /// <summary>
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(ConfiguracionEmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionOrigenID", info.OrganizacionOrigen.OrganizacionID},
                                     {"@OrganizacionDestinoID", info.OrganizacionDestino.OrganizacionID},
                                     {"@Kilometros", info.Kilometros},
                                     {"@Horas", info.Horas},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(ConfiguracionEmbarqueInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ConfiguracionEmbarqueID", info.ConfiguracionEmbarqueID},
                                     {"@OrganizacionOrigenID", info.OrganizacionOrigen.OrganizacionID},
                                     {"@OrganizacionDestinoID", info.OrganizacionDestino.OrganizacionID},
                                     {"@Kilometros", info.Kilometros},
                                     {"@Horas", info.Horas},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID},
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
        /// <param name="filtro">Identificador del registro de configuración de embarque</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ConfiguracionEmbarqueID", filtro},
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
        ///     Obtiene Parametros por organizacion 
        /// </summary>
        /// <param name="organizacionOrigenId">Organización origen de la configuración</param>
        /// <param name="organizacionDestinoId">Organización destino de la configuración</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacion(int organizacionOrigenId, int organizacionDestinoId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionOrigenID", organizacionOrigenId},
                                     {"@OrganizacionDestinoID", organizacionDestinoId}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ConfiguracionEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ConfiguracionEmbarqueID", filtro.ConfiguracionEmbarqueID},
                            {"@OrganizacionOrigenID", filtro.OrganizacionOrigen.OrganizacionID},
                            {"@OrganizacionDestinoID", filtro.OrganizacionDestino.OrganizacionID},
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
        /// Obtiene parametros para obtener una lista de las Rutas
        /// </summary>
        /// <param name="proveedorId"></param>
        /// /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerProveedorOrigenDestinoTieneVariasRutas(int proveedorId, ConfiguracionEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", proveedorId},
                            {"@OrigenID", filtro.OrganizacionOrigen.OrganizacionID},
                            {"@DestinoID", filtro.OrganizacionDestino.OrganizacionID},
                            {"@Activo", filtro.Activo.GetHashCode()},
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
        /// Obtiene parametros para obtener una lista de las Rutas
        /// </summary>
        /// /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerRutasPorId(ConfiguracionEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ConfiguracionEmbarqueDetalleID", filtro.ConfiguracionEmbarqueDetalle.ConfiguracionEmbarqueDetalleID},
                            {"@OrigenID", filtro.OrganizacionOrigen.OrganizacionID},
                            {"@DestinoID", filtro.OrganizacionDestino.OrganizacionID},
                            {"@Activo", filtro.Activo.GetHashCode()},
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
        /// Obtiene parametros para obtener una lista de las Rutas
        /// </summary>
        /// /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosRutasPorDescripcion(ConfiguracionEmbarqueInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrigenID", filtro.OrganizacionOrigen.OrganizacionID},
                            {"@DestinoID", filtro.OrganizacionDestino.OrganizacionID},
                            {"@Activo", filtro.Activo.GetHashCode()},
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


