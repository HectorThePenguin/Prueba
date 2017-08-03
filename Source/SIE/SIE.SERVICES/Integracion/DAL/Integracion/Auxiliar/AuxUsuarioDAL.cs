using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Auxiliar;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxUsuarioDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, UsuarioInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Nombre", filtro.Nombre ?? string.Empty},
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int usuarioID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@UsuarioID", usuarioID}
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
        /// Obtiene parametros para la ejecucion del SP 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, UsuarioInfo filtro,
                                                                            IList<IDictionary<IList<string>, object>>
                                                                                dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Nombre", filtro.Nombre},
                        {"@Inicio", pagina.Inicio},
                        {"@Activo", filtro.Activo},
                        {"@Limite", pagina.Limite}
                    };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        ///     Obtiene Parametros por ActiveDirectory
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorActiveDirectory(string usuario)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@UsuarioActiveDirectory", usuario}
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Nombre", info.Nombre},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@UsuarioActiveDirectory", info.UsuarioActiveDirectory},
                            {"@Corporativo", info.Corporativo},
                            {"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@NivelAcceso", info.NivelAcceso}
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@UsuarioID", info.UsuarioID},
							{"@Nombre", info.Nombre},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@UsuarioActiveDirectory", info.UsuarioActiveDirectory},
                            {"@Corporativo", info.Corporativo},
                            {"@Activo", info.Activo},
                            {"@NivelAcceso", info.NivelAcceso}
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
        /// Método para obtener los parametros para obtener los usuarios a los que se enviarán correos
        /// </summary>
        /// <param name="rolesXML">XML con un listado de roles</param>
        /// <returns>Diccionario con los parametros requeridos</returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCorreos(String rolesXML)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@XmlRoles", rolesXML}
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