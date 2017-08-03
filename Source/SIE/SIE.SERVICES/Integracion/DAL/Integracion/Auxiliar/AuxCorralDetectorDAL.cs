using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;



namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxCorralDetectorDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CorralDetectorInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {
                                "@NombreOperador",
                                string.IsNullOrWhiteSpace(filtro.Operador.Nombre) ? string.Empty : filtro.Operador.Nombre
                            },
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
        /// <param name="corralesMarcados"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(CorralDetectorInfo info, List<CorralInfo> corralesMarcados)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from corral in corralesMarcados
                                 select new XElement("Corrales",
                                                     new XElement("CorralID", corral.CorralID),
                                                     new XElement("CorralDetectorID", corral.CorralDetectorID),
                                                     new XElement("Activo", (int)corral.Activo)
                                     ));

                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OperadorID", info.Operador.OperadorID},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@CorralesXML", xml.ToString()},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CorralDetectorInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CorralDetectorID", info.CorralDetectorID},
							{"@OperadorID", info.Operador.OperadorID},
							//{"@CorralID", info.Corral.CorralID},
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
        /// <param name="corralDetectorID">Identificador de la entidad CorralDetector</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int corralDetectorID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@CorralDetectorID", corralDetectorID}
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
        /// Obtiene Parametro para buscar los operadores por
        /// </summary> 
        /// <param name="operadorID">Representa el operador </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodosPorDetector(int operadorID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OperadorID", operadorID}
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
								{"@CorralDetectorID", descripcion}
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
        /// Obtiene un diccionario para la ejecucion del procedimiento
        /// almacenado CorralDetector_ObtenerPorOperadorCorral
        /// </summary>
        /// <param name="operadorID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOperadorCorral(int operadorID, int corralID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OperadorID", operadorID},
                                {"@CorralID", corralID}
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

