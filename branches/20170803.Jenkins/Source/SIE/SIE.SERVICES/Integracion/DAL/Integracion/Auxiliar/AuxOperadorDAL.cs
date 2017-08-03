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
    internal static class AuxOperadorDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Nombre", filtro.NombreCompleto.Trim()},
                        {"@RolID", filtro.Rol.RolID},
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
        /// <param name="operadorID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int operadorID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OperadorID", operadorID}
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
        /// Obtiene Parametros por Id Rol
        /// </summary>
        /// <param name="rolId"> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorIdRol(int organizacionId, int rolId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID",organizacionId},
                        {"@IdRol", rolId}
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
        /// Obtiene Parametros por Id Operador
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorOperador(OperadorInfo operadorInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OperadorID", operadorInfo.OperadorID},
                                     {"@RolID", operadorInfo.Rol.RolID},
                                     {"@OrganizacionID", operadorInfo.Organizacion.OrganizacionID},
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(OperadorInfo info)
        {
            try
            {
                Logger.Info();

                int? usuarioId = (info.Usuario != null) ? info.Usuario.UsuarioID : (int?) null;

                var parametros =
                    new Dictionary<string, object>
                        {
							{"@Nombre", info.Nombre},
							{"@ApellidoPaterno", info.ApellidoPaterno},
							{"@ApellidoMaterno", info.ApellidoMaterno},
							{"@CodigoSAP", info.CodigoSAP},
							{"@RolID", info.Rol.RolID},
                            {"@UsuarioID", usuarioId},
                            {"@OrganizacionID", info.Organizacion.OrganizacionID},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(OperadorInfo info)
        {
            try
            {
                Logger.Info();
                int? usuarioId = (info.Usuario != null) ? info.Usuario.UsuarioID : (int?)null;

                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OperadorID", info.OperadorID},
							{"@Nombre", info.Nombre},
							{"@ApellidoPaterno", info.ApellidoPaterno},
							{"@ApellidoMaterno", info.ApellidoMaterno},
							{"@CodigoSAP", info.CodigoSAP},
							{"@RolID", info.Rol.RolID},
                            {"@UsuarioID", usuarioId},
                            {"@OrganizacionID", info.Organizacion.OrganizacionID},
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almancenado Operador_ObtenerPorUsuarioIdRol
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="organizacionId"> </param>
        /// <param name="basculista"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorUsuarioIdRol(int usuarioId, int organizacionId, Roles basculista)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@UsuarioID", usuarioId},
                                {"@OrganizacionID", organizacionId},
                                {"@RolID", basculista}
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
        ///     Obtiene Parametro por UsuarioID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorUsuarioID(int usuarioID, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@UsuarioID", usuarioID},
                        {"@OrganizacionID", organizacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametroObtenerDetectorCorral(int organizacionId, int corralID, int operadorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID",organizacionId},
                        {"@CorralID", corralID},
                        {"@OperadorID", operadorId},
                        
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
        ///     Obtiene Parametro por CorralId
        /// </summary>
        /// <param name="corralId"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorCodigoCorral(string corralId, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Codigo", corralId},
                        {"@OrganizacionID", organizacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametroNotificacionesDeteccionLista(int organizacionId, int operadorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID",organizacionId},
                        {"@OperadorID", operadorId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        internal static Dictionary<string, object> ObtenerParametrosPorSupervisorDetector(int organizacionId, int supervisorId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", organizacionId},
                                {"@IdSupervisor", supervisorId}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado Operador_ObtenerPorOrganizacion
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroOperadorPorOrganizacion(OperadorInfo operadorInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", operadorInfo.Organizacion.OrganizacionID},
                                {"@OperadorID", operadorInfo.OperadorID}
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
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado BasculaMultipesaje_Actualizar
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerBasculistaPorId(long folio)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                {
                    {"@OperadorID", folio},
                    {"@RolID",Roles.Basculista.GetHashCode()}
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
        /// Obtiene los parametros para obtener las organizaciones por centros, cadis, descansos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFiltroBasculista(PaginacionInfo pagina, OperadorInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Nombre",string.Empty},
                        {"@OrganizacionID",string.Empty},
                        {"@Activo", 1},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@RolID", Roles.Basculista.GetHashCode()}
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