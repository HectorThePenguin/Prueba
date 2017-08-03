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
    internal class AuxGrupoFormularioDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, GrupoFormularioInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {
                                "@GrupoDescripcion",
                                string.IsNullOrWhiteSpace(filtro.Grupo.Descripcion)
                                    ? string.Empty
                                    : filtro.Grupo.Descripcion
                            },
                            {"@Activo", filtro.Grupo.Activo},
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(GrupoFormularioInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@GrupoID", info.Grupo.GrupoID},
							{"@AccesoID", info.Acceso.AccesoId},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(GrupoFormularioInfo info)
        {
            try
            {
                Logger.Info();                
             
                int grupoID = info.Grupo.GrupoID;
                var xml =
                    new XElement("ROOT",
                                 from grupoFormulario in info.ListaFormulario
                                 select new XElement("GrupoFormulario",
                                                     new XElement("GrupoID",
                                                                  grupoID),
                                                     new XElement("FormularioID",
                                                                  grupoFormulario.FormularioID),
                                                     new XElement("AccesoID",
                                                                  grupoFormulario.Acceso.AccesoId)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlGrupoFormulario", xml.ToString()},
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
        /// <param name="grupoFormularioID">Identificador de la entidad GrupoFormulario</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int grupoFormularioID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@GrupoID", grupoFormularioID}
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
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
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
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@GrupoID", descripcion}
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
        /// Obtiene Parametro pora filtrar por Grupo ID
        /// </summary> 
        /// <param name="grupoID">Grupo de la Entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorGrupoID(int grupoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@GrupoID", grupoID}
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

