using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxCorralDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                filtro.Organizacion = filtro.Organizacion ?? new OrganizacionInfo();
                filtro.TipoCorral = filtro.TipoCorral ?? new TipoCorralInfo(); 

                parametros = new Dictionary<string, object>
                {
                        {"@Codigo", filtro.Codigo},
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@TipoCorralID", filtro.TipoCorral.TipoCorralID},
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
        /// Obtiene Parametros para Crear un Corral
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@Codigo", filtro.Codigo},
                            {"@TipoCorralID", filtro.TipoCorral.TipoCorralID},
                            {"@Capacidad", filtro.Capacidad},
                            {"@MetrosLargo", filtro.MetrosLargo},
                            {"@MetrosAncho", filtro.MetrosAncho},
                            {"@Seccion", filtro.Seccion},
                            {"@Orden", filtro.Orden},
                            {"@Activo", filtro.Activo},
                            {"@UsuarioCreacionID", filtro.UsuarioCreacionID}
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
        /// Obtiene Parametros para Actualizar un Corral
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@CorralID", filtro.CorralID},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@Codigo", filtro.Codigo},
                            {"@TipoCorralID", filtro.TipoCorral.TipoCorralID},
                            {"@Capacidad", filtro.Capacidad},
                            {"@MetrosLargo", filtro.MetrosLargo},
                            {"@MetrosAncho", filtro.MetrosAncho},
                            {"@Seccion", filtro.Seccion},
                            {"@Orden", filtro.Orden},
                            {"@Activo", filtro.Activo},
                            {"@UsuarioModificacionID", filtro.UsuarioModificacionID}
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
        /// Obtiene Parametros para Obtener un Corral por Filtro y Dependencia
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolio(PaginacionInfo pagina,
                                                                            CorralInfo filtro
                                                                            )
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                    {
                            {"@Codigo", filtro.Codigo},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@FormulaID", filtro.FormulaInfo.FormulaId},
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

        internal static Dictionary<string, object> ObtenerParametrosPorId(int corralID, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@CorralID", corralID}
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
        /// Obtiene los Parametros Necesarios para ejecutar el Procedimiento
        /// Corral_ObtenerCorralRuteo
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralEnRuteo(int embarqueID, int corralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                        {"@EmbarqueID", embarqueID},
                        {"@CorralID", corralID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorFolio(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@Codigo", corralInfo.Codigo.Trim()},
                            {"@OrganizacionID", corralInfo.Organizacion.OrganizacionID},
                            {"@TipoCorralID", corralInfo.TipoCorral.TipoCorralID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorId(int corralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@CorralID", corralID}
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
        /// Obtiene los parametros necesarios para ejecutar el store
        /// Corral_ObtenerValidacionCorral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="codigoCorral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidacion(int organizacionID, string codigoCorral)

        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@OrganizacionID", organizacionID},
                                     {"@Codigo", codigoCorral}
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
        /// Obtiene los parametros necesarios para ejecutar el store
        /// Corral_ObtenerValidacionCorral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidacion(CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@Codigo", corralInfo.Codigo},
                                     {"@OrganizacionID", corralInfo.Organizacion.OrganizacionID},
                                     {"@TipoCorralID", corralInfo.TipoCorral.TipoCorralID}
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
        /// Obtiene los parametros necesarios para ejecutar el store
        /// Corral_ObtenerValidacionCorral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidacionCorraleta(CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@Codigo", corralInfo.Codigo},
                                     {"@OrganizacionID", corralInfo.Organizacion.OrganizacionID},
                                     {"@TipoCorralID", corralInfo.TipoCorral.TipoCorralID},
                                     {"@GrupoCorralID",(int)GrupoCorralEnum.Corraleta}
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
        /// Obtiene los parametros necesarios para ejecutar el store
        /// Corral_ObtenerValidacionCorral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAretesCorral(CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@Codigo", corralInfo.Codigo},
                                     {"@OrganizacionID", corralInfo.Organizacion.OrganizacionID},
                                     {"@GrupoCorralID",corralInfo.GrupoCorral},
                                     {"@Activo",EstatusEnum.Activo}
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
        /// Obtiene la lista de parametros por @OrganizacionID
        /// </summary>
        /// <param name="organizacionId">organizacionId</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionId(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@OrganizacionID", organizacionId}
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
        /// Obtiene la lista de parametros por @OrganizacionID
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionId(PaginacionInfo pagina, CorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                filtro.Organizacion = filtro.Organizacion ?? new OrganizacionInfo();
                filtro.TipoCorral = filtro.TipoCorral ?? new TipoCorralInfo();

                parametros = new Dictionary<string, object>
                {
                        {"@OrganizacionID", filtro.OrganizacionId},
                        {"@Codigo", filtro.Codigo},
                        {"@CodigoOrigen", filtro.CodigoOrigen},
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

        internal static Dictionary<string, object> ObtenerParametrosPorEmbarqueRuteo(int folioEmbarque, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@FolioEmbarque", folioEmbarque},
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
        /// Obtiene Parametro pora obtener el corral por grupo
        /// </summary> 
        /// <param name="corral">Corral con el codigo, grupo y organizacion</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCodigoGrupo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@Codigo", corral.Codigo},
                                {"@OrganizacionID", corral.Organizacion.OrganizacionID}
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
        /// Obtiene los parametros necesarios para la obtencion de corral por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <param name="listaTiposCorral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerCorralesPorTipo(CorralInfo corral, List<int> listaTiposCorral)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from tipoCorral in listaTiposCorral
                                select
                                    new XElement("TiposCorral",
                                                 new XElement("TipoCorralID", tipoCorral))
                                    );
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@GrupoCorral", corral.GrupoCorral},
                            {"@OrganizacionID",corral.Organizacion.OrganizacionID},
                            {"@XmlTiposCorral", xml.ToString()}
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
        /// Obtiene los parametros necesarios para la obtencion de corral por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerCorralesPorTipo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@OrganizacionID",corral.Organizacion.OrganizacionID},
                        {"@TipoCorralID", corral.TipoCorral.TipoCorralID}
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
        /// Obtiene los parametros necesarios para la obtencion de corral por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerCorralesPorTipoCorralDetector(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@OrganizacionID",corral.Organizacion.OrganizacionID},
                        {"@TipoCorralID", corral.TipoCorral.TipoCorralID},
                        {"@OperadorID", corral.Operador.OperadorID}
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
        /// Obtiene los parametros necesario para obtener el corrra por codigo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralPorCodigo(int organizacionId,string corralCodigo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@Codigo", corralCodigo},
                                {"@OrganizacionID",organizacionId}
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
        /// Obtiene los parametros necesarios para obtener la partida del corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPartidaCorral(int organizacionId, int corralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@OrganizacionID",organizacionId},
                                     {"@CorralID", corralID}
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
        /// Obtiene los parametros necesario para obtener el corral de enfermeria
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="enfermeria"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCodigoCorralEnfermeria(string codigoCorral, int enfermeria, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@Codigo", codigoCorral},
                                {"@EnfermeriaID", enfermeria},
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
        /// <summary>
        /// Obtiene los parametros necesario para actualizar el corral
        /// </summary>
        /// <param name="animalMovimiento"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCorrales(AnimalMovimientoInfo animalMovimiento, int loteOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@LoteDestino",animalMovimiento.LoteID},
                                     {"@LoteOrigen", loteOrigen},
                                     {"@UsuarioModifica", animalMovimiento.UsuarioCreacionID}
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
        /// Obtiene los parametros necesario para valida la existencia de un corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerExistenciaCorral(int organizacionId, string corralCodigo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@OrganizacionID",organizacionId},
                                     {"@Codigo", corralCodigo}
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
        /// Obtiene los Parámetros para generar el Reporte Proyector
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteProyectorComportamiento(int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@OrganizacionID", organizacionID}
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
        /// Obtiene los parametros necesarios para contar las cabezas de un corral
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosContarCabezas(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@CorralID", corral.CorralID},
                                {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                                {"@Estatus", EstatusEnum.Activo}
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
        /// Obtiene los parametros necesarios para obtener los aretes de una corraleta
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAretesCorraleta(CorralInfo corralInfo, LoteInfo loteOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@Codigo", corralInfo.Codigo},
                                     {"@LoteID",loteOrigen.LoteID},
                                     {"@OrganizacionID",loteOrigen.OrganizacionID},
                                     {"@Activo",EstatusEnum.Activo}
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
        /// Obtiene los parametros para obtner los animales en salida
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalSalidaPorCodigo(CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@Codigo", corralInfo.Codigo},
                                     {"@OrganizacionID",corralInfo.Organizacion.OrganizacionID },
                                     {"@activo",corralInfo.Activo}
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
        /// Obtiene un diccionario con los paramentros
        /// necesarios para la ejecucion del procedimiento
        /// almacendo Corral_ObtenerCorralPorCodigo
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCodigoOrganizacion(CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@Codigo", corralInfo.Codigo.Trim()},
                                     {"@OrganizacionID",corralInfo.Organizacion.OrganizacionID },
                                     {"@activo",(int)corralInfo.Activo}
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
        /// Obtiene los parametros necesarios
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCodigoGrupoCorral(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@Codigo", corral.Codigo},
                                {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                                {"@GrupoCorral", corral.GrupoCorral}
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
        /// Obtiene los parametros necesario para verificar la existencia de interfaz salida
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralCodigo"></param>
        /// <param name="arete"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerExisteInterfaceSalida(int organizacionID, string corralCodigo, string arete)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@CodigoCorral", corralCodigo},
                                {"@OrganizacionID", organizacionID},
                                {"@Arete", arete},
                                
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerCorralPorLoteID(int loteID, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                                     {"@OrganizacionID",organizacionID},
                                     {"@LoteId", loteID}
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
        /// Obtiene los parametros para traspasar animal salida enfermeria.
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosTraspasarAnimalSalidaEnfermeria(int corralInfo, int loteOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Corraleta", corralInfo},
                    {"@LoteID", loteOrigen}
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
        /// Obtiene los parametros necesarios para obtener los corrales para reparto
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralesParaReparto(int organizacionId)
		{
		    try
		    {
		        Logger.Info();
		        var parametros =
		            new Dictionary<string, object>
		                {
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

        /// <summary>
        /// Obtiene los parametros necesarios para
        /// la ejecucion del procedimiento almacenado
        /// Corral_AuxiliarInventario
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralAuxiliarInventario(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
		                {
		                    {"@OrganizacionID", corralInfo.Organizacion.OrganizacionID},
                            {"@Corral", corralInfo.Codigo},
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
        /// Obtiene los parametros para ejecutar el SP Corral_ObtenerCorralesPorGrupoCorral
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralesPorGrupoCorral(GrupoCorralInfo grupo, int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                        {"@GrupoCorralID", grupo.GrupoCorralID},
                        {"@OrganizacionID", organizacionId}
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
        /// almacenado Corral_ObtenerPorCodigoGrupoCorral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorGrupoCorral(CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                        {"@Codigo", corralInfo.Codigo},
                        {"@OrganizacionID", corralInfo.Organizacion.OrganizacionID},
                        {"@GrupoCorralID", corralInfo.TipoCorral.GrupoCorral.GrupoCorralID},
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
        /// Obtiene un diccionario de parametros para
        /// la ejecucion del procedimiento almacenado
        /// Corral_ObtenerPorPaginaGrupoCorral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaGrupoCorral(PaginacionInfo pagina, CorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                filtro.Organizacion = filtro.Organizacion ?? new OrganizacionInfo();
                filtro.TipoCorral = filtro.TipoCorral ?? new TipoCorralInfo {GrupoCorral = new GrupoCorralInfo()};

                parametros = new Dictionary<string, object>
                {
                        {"@Codigo", filtro.Codigo},
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@GrupoCorralID", filtro.TipoCorral.GrupoCorral.GrupoCorralID},
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
        /// Obtiene los parametros necesarios para
        /// la ejecucion del procedimiento almacenado
        /// Corral_ObtenerPorDescripcionOrganizacion
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcionOrganizacion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@Descripcion", descripcion},
                                {"@OrganizacionID", organizacionID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerCorralesPorTipoCorral(int tipoCorralID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
								{"@TipoCorralID", tipoCorralID},
                                {"@OrganizacionID", organizacionID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosCorralesPorCodigosCorral(List<string> codigosCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from codigo in codigosCorral
                                select
                                    new XElement("Codigos",
                                                 new XElement("CodigoCorral", codigo))
                                    );
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@OrganizacionID",organizacionID},
                            {"@XmlCodigosCorral", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosPorPaginaGruposCorrales(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                 
                                from grupo in filtro.ListaGrupoCorral
                                select
                                            new XElement("Grupos",
                                                 new XElement("GrupoCorral", grupo.GrupoCorralID))
                                    );
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@Codigo", filtro.Codigo},
                            {"@OrganizacionID",filtro.OrganizacionId},
                            {"@XmlCodigosCorral", xml.ToString()},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado Corral_ObtenerPorCorralIDXML
        /// </summary>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorIdXML(List<CorralInfo> corrales)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from corral in corrales
                                 select
                                     new XElement("Corrales",
                                                  new XElement("CorralID", corral.CorralID))
                        );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlCorral", xml.ToString()}
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
        /// del procedimiento almacenado Corral_ObtenerValidaCorralConLoteConExistenciaActivo
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidaCorralConLoteConExistenciaActivo(int corralID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
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
        /// <summary>
        /// Obtiene los parametros necesario para verificar la existencia de dias engorda de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDiasEngordaPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
							{"@LoteID", lote.LoteID}
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
        /// Obtiene los parametros necesario para verificar las secciones de los corrales
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerSeccionesCorral(int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
							{"@OrganizacionID", organizacionID}
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
        /// Obtiene Parametros para Obtener los corrales improductivos para la pantalla Corte por Transferencia
        /// </summary>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralesImproductivos(int tipoCorralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                    {
                            {"@TipoCorralID", tipoCorralID},
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametrosObtenerFormulaCorralPorID(CorralInfo corral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                    {
                            {"@CorralID", corral.CorralID},
                            {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                            {"@FormulaID", corral.FormulaInfo.FormulaId}
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
        /// Obtiene los parametros necesario para verificar la existencia de dias engorda de un lote
        /// </summary>
        /// <param name="lotesXml"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDiasEngordaPorLoteXML(IList<LoteInfo> lotesXml)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from lote in lotesXml
                                select
                                    new XElement("Lotes",
                                                 new XElement("LoteID", lote.LoteID)
                                                 ));

                var parametros =
                        new Dictionary<string, object>
                        {
							{"@XmlLote", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacion(PaginacionInfo pagina, CorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                    {
                            {"@Codigo", filtro.Codigo},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@TipoCorralID", filtro.TipoCorral.TipoCorralID},
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
        /// Obtiene los parametros para obtener corral por codigo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCorralesPorTipos(CorralInfo corral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                    {
                            {"@Codigo", corral.Codigo},
                            {"@OrganizacionID", corral.Organizacion.OrganizacionID},
                            
                            {"@Activo", corral.Activo.GetHashCode()},
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
        /// Se obtiene los parametros para consulta de tipos corrales
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorTiposCorrales(PaginacionInfo paginacion, CorralInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                    from lista in filtro.ListaTipoCorral
                    select new XElement("TiposCorral", 
                        new XElement("TipoCorralID", lista.TipoCorralID))
                    );
                parametros = new Dictionary<string, object>
                {
                    {"@Codigo", filtro.Codigo},
                    {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                    {"@XmlTiposCorral", xml.ToString()},
                    {"@Activo", filtro.Activo},
                    {"@Inicio", paginacion.Inicio},
                    {"@Limite",paginacion.Limite}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametrosPorUsuarioId(int usuarioId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>()
                    {
                        {"@UsuarioID", usuarioId}
                    };       
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametrosPorEntradaGanadoTransitoID(int entradaGanadoTransitoId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>()
                    {
                        {"@EntradaGanadoTransitoID", entradaGanadoTransitoId}
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
