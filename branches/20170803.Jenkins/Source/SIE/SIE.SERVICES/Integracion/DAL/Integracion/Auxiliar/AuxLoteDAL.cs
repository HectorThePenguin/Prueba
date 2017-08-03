using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxLoteDAL
    {
        /// <summary>
        /// Obtiene los Parametros con los Cuales se Invocara
        /// el Procedimiento para Obtener el Lote por Corral
        /// </summary>
        /// <param name="organizacionID">Clave de la Organizacion a la cual pertenece el Corral</param>
        /// <param name="corralID">Clave del Corral</param>        
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorCorral(int organizacionID, int corralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
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
        /// Obtiene los Parametros con los Cuales se Invocara
        /// el Procedimiento para Obtener el Lote por Corral
        /// </summary>
        /// <param name="organizacionID">Clave de la Organizacion a la cual pertenece el Corral</param>
        /// <param name="corralID">Clave del Corral</param>        
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerLotesActivos(int organizacionID, int corralID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@CorralID", corralID},
                        {"@Activo",(int)EstatusEnum.Activo}
                       
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
        /// Metodo que Obtiene los parametros para la ejecucion del Procedimiento
        /// almacenado Lote_ObtenerPorID
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorID(int loteID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", loteID}
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
        /// Obtiene los Parametros necesarios para la ejecucion del Procedimiento
        /// Almacenado Lote_ObtenerPorLoteIdOrganizacionID
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorIdOrganizacionId(int organizacionID,
                                                                                             int corralID,
                                                                                             int embarqueID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@CorralID", corralID},
                        {"@EmbarqueID", embarqueID}
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
        /// Obtiene los Parametros para Generar un Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardar(LoteInfo loteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Activo", loteInfo.Activo},
                        {"@Cabezas", loteInfo.Cabezas},
                        {"@CabezasInicio", loteInfo.LoteID == 0 ? loteInfo.Cabezas : loteInfo.CabezasInicio},
                        {"@CorralID", loteInfo.CorralID},
                        {"@DisponibilidadManual", loteInfo.DisponibilidadManual},
                        {"@OrganizacionID", loteInfo.OrganizacionID},
                        {"@TipoCorralID", loteInfo.TipoCorralID},
                        {"@TipoProcesoID", loteInfo.TipoProcesoID},
                        {"@UsuarioCreacionID", loteInfo.UsuarioCreacionID},
                        {"@TipoFolioID", TipoFolio.Lote.GetHashCode()},
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
        /// Genera los Parametros para la Acutalizacion
        /// de las Cabezas que tiene un Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaCabezas(LoteInfo loteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Cabezas", loteInfo.Cabezas},
                        {"@UsuarioModificacionID", loteInfo.UsuarioModificacionID},
                        {"@LoteID", loteInfo.LoteID},
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
        /// Genera los Parametros para Obtener un
        /// Lote por Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacionIdLote(int organizacionID,
                                                                                               string lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@Lote", lote},
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
        /// Genera los Parametros para Actualizar
        /// la Fecha del Cierre del Lote
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="usuarioModificacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosFechaCierre(int loteID, int? usuarioModificacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", loteID},
                        {"@UsuarioModificacionID", usuarioModificacionID},
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
        /// Genera los Parametros para la Acutalizacion
        /// de las Cabezas que tiene un Lote
        /// </summary>
        /// <param name="loteInfoDestino"></param>
        /// <param name="loteInfoOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaNoCabezasEnLote(LoteInfo loteInfoDestino, LoteInfo loteInfoOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Cabezas", loteInfoDestino.Cabezas},
                        {"@CabezasInicio", loteInfoDestino.CabezasInicio},
                        {"@UsuarioModificacionID", loteInfoDestino.UsuarioModificacionID},
                        {"@LoteIDDestino", loteInfoDestino.LoteID},
                        {"@LoteIDOrigen", loteInfoOrigen.LoteID},
                        {"@CabezasOrigen", loteInfoOrigen.Cabezas}
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
        /// Genera los Parametros para la Acutalizacion
        /// del Lote que tiene un Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaLoteALote(LoteInfo loteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var fecha = new DateTime(1, 1, 1);
                parametros = new Dictionary<string, object>
                    {
                        {"@Lote", loteInfo.Lote},
                        {"@FechaDisponibilidad", loteInfo.FechaDisponibilidad > fecha ? loteInfo.FechaDisponibilidad : new DateTime(1800, 1, 1)},
                        {"@FechaCierre", loteInfo.FechaCierre >= fecha ? loteInfo.FechaCierre : new DateTime(1800, 1, 1)},
                        {"@LoteID", loteInfo.LoteID}
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
        /// Genera los Parametros para la Acutalizacion
        /// del Activo que tiene un Lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaActivoALote(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Activo", lote.Activo},
                        {"@LoteID", lote.LoteID}
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
        /// Genera los Parametros para la Actualizacion
        /// del fecha salida que tiene un Lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFechaSalidaEnLote(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", lote.LoteID}
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
        /// Genera los Parametros para obtener el Check List de Corral
        /// </summary>
        /// <param name="filtroCierreCorral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCheckListCorral(FiltroCierreCorral filtroCierreCorral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtroCierreCorral.OrganizacionID},
                        {"@FechaEjecucion", filtroCierreCorral.FechaEjecucion}
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
        /// Genera los Parametros para obtener el Check List de Corral Completo
        /// </summary>
        /// <param name="filtroCierreCorral"></param>

        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCheckListCorralCompleto(FiltroCierreCorral filtroCierreCorral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", filtroCierreCorral.OrganizacionID},
                        {"@LoteID", filtroCierreCorral.LoteID}
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
        /// Genera los Parametros para obtener el Check List de Corral Completo
        /// </summary>
        /// <param name="filtroCierreCorral"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaFechaCierre(FiltroCierreCorral filtroCierreCorral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", filtroCierreCorral.LoteID},
                        {"@FechaCerrado",filtroCierreCorral.FechaCierre},
                        {"@UsuarioModificacionID",filtroCierreCorral.UsuarioModificacionID}
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
        /// Obtiene un Diccionario con parametros para la ejecicion del
        /// procedimiento almacenado Lote_ObtenerPorDisponibilidad
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID",filtroDisponilidadInfo.OrganizacionId},
                        {"@TipoCorralProduccion", (int)TipoCorral.Produccion}
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
        /// Obtiene un Diccionario con parametros para la ejecicion del
        /// procedimiento almacenado Lote_ObtenerPorDisponibilidad
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarLoteDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@UsuarioID", filtroDisponilidadInfo.UsuarioId},
                        };
                var xml =
                    new XElement("ROOT",
                                 from lotes in filtroDisponilidadInfo.ListaLoteDisponibilidad
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lotes.LoteId),
                                                     new XElement("FechaDisponibilidad", lotes.FechaDisponibilidad),
                                                     new XElement("DisponibilidadManual", lotes.DisponibilidadManual)

                                     ));
                parametros.Add("@XmlLotes", xml.ToString());

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerPorCorralID(LoteInfo loteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", loteInfo.OrganizacionID},
                        {"@CorralID", loteInfo.CorralID},
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosActualizarSalidaEnfermeria(AnimalMovimientoInfo resultadoLoteOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", resultadoLoteOrigen.LoteID},
                        {"@AnimalID", resultadoLoteOrigen.AnimalID},
                        {"@UsuarioModificacion", resultadoLoteOrigen.UsuarioCreacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosEliminarSalidaEnfermeria(AnimalMovimientoInfo loteCorralOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", loteCorralOrigen.LoteID}
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
        /// Obtiene los parametros necesarios para obtener los lotes de una corraleta
        /// </summary>
        /// <param name="corraleta"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCarrelata(CorralInfo corraleta)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CorraletaID", corraleta.CorralID},
                        {"@GrupoCorralEnfermeria", (int)GrupoCorralEnum.Enfermeria},
                        {"@OrganizacionID", corraleta.Organizacion.OrganizacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }
        internal static Dictionary<string, object> ObtenerParametrosActualizaNoCabezasEnLoteOrigen(LoteInfo resultadoLoteOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteIDOrigen", resultadoLoteOrigen.LoteID},
                        {"@CabezasOrigen", resultadoLoteOrigen.Cabezas},
                        {"@UsuarioModificacionID",resultadoLoteOrigen.UsuarioModificacionID},
                        {"@Activo",resultadoLoteOrigen.Activo}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }



        internal static Dictionary<string, object> ObtenerParametrosExisteLoteAnimalSalida(LoteInfo loteOrigen, CorralInfo corralInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", loteOrigen.LoteID},
                        {"@OrganizacionID",loteOrigen.OrganizacionID},
                        {"@CorralID",corralInfo.CorralID},
                        {"ActivoID",EstatusEnum.Activo}
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
        /// del procedimiento almacenado Lote_ObtenerPorOrganizacionLoteXML
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionLoteXML(int organizacionId, List<CorralInfo> corrales)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                        };
                var xml =
                    new XElement("ROOT",
                                 from corral in corrales
                                 select new XElement("Corrales",
                                                     new XElement("CorralID", corral.CorralID)
                                     ));
                parametros.Add("@XmlCorral", xml.ToString());
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
        /// del procedimiento almacenado Lote_ObtenerPorOrganizacionCorralCerradoXML
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionCorralCerradoXML(int organizacionId, List<CorralInfo> corrales)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                        };
                var xml =
                    new XElement("ROOT",
                                 from corral in corrales
                                 select new XElement("Corrales",
                                                     new XElement("CorralID", corral.CorralID)
                                     ));
                parametros.Add("@XmlCorral", xml.ToString());
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
        /// del procedimiento almacenado Lote_ObtenerDatosDescargaDataLink
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLoteDataLink(int organizacionId, int loteID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                             {"@LoteID", loteID},
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
        /// Obtiene los parametros para actualizar el corral a un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ActualizarCorral(LoteInfo lote, CorralInfo corralInfoDestino, UsuarioInfo usuario)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", lote.LoteID},
                            {"@CorralID",corralInfoDestino.CorralID},
                            {"@UsuarioModificacionID",usuario.UsuarioID}
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
        /// Obtiene los parametros para obtener los lotes activos con proyeccion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosLotesActivosConProyeccion(int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
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
        /// Obtiene el lote anterior en donde estuvo el ganado
        /// </summary>
        /// <param name="loteID">Id del lote donde se encuentra actualmente el ganado</param>
        internal static Dictionary<string, object> ObtenerLoteAnteriorAnimal(int loteID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", loteID}
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
        /// Obtiene el lote anterior en donde estuvo el ganado
        /// </summary>
        /// <param name="lote">Id del lote donde se encuentra actualmente el ganado</param>
        internal static Dictionary<string, object> ObtenerDiasEngordaGrano(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", lote.LoteID},
                            {"@OrganizacionID", lote.OrganizacionID}
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
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almacendo
        /// Lote_ActualizarCabezasLoteXML
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCabezasLoteXML(List<LoteInfo> lotes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID),
                                                     new XElement("Cabezas", lote.Cabezas),
                                                     new XElement("UsuarioModificacionID", lote.UsuarioModificacionID)
                                     ));
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LotesXML", xml.ToString()},
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
        /// la ejecucion del procedimiento almancenado
        /// Lote_DesactivarLoteXML
        /// </summary>
        /// <param name="lotesDesactivar"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDesactivarLoteXML(List<LoteInfo> lotesDesactivar)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotesDesactivar
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID),
                                                     new XElement("UsuarioModificacionID", lote.UsuarioModificacionID)
                                     ));
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LotesXML", xml.ToString()},
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
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionEstatus(int organizacionId, EstatusEnum estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionId},
                            {"@Activo", estatus.GetHashCode()}
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
        /// Obtiene los parametros necesarios para actualizar
        /// la fecha zilmax del lote
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFechaZilmax(List<LoteInfo> lotes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID),
                                                     new XElement("FechaEntradaZilmax", lote.FechaEntradaZilmax),
                                                     new XElement("FechaSalidaZilmax", lote.FechaSalidaZilmax),
                                                     new XElement("UsuarioModificacionID", lote.UsuarioModificacionID)
                                     ));
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LotesZilmaxXML", xml.ToString()},
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
        /// para la ejecucion del procedimiento
        /// almacenado Lote_ObtenerPorLoteXML
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static string ObtenerParametrosLoteXML(List<LoteInfo> lotes)
        {
            string lotesSIAP;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID)
                                     ));
                lotesSIAP = xml.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lotesSIAP;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// Lote_ActualizarNoCabezasEnLoteXML
        /// </summary>
        /// <param name="lotesDestino"></param>
        /// <param name="lotesOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaCabezasEnLoteXML(List<LoteInfo> lotesDestino, List<LoteInfo> lotesOrigen)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlDestino =
                    new XElement("ROOT",
                                 from lote in lotesDestino
                                 select new XElement("LotesDestino",
                                                     new XElement("LoteID", lote.LoteID),
                                                     new XElement("Cabezas", lote.Cabezas),
                                                     new XElement("CabezasInicio", lote.CabezasInicio),
                                                     new XElement("UsuarioModificacionID", lote.UsuarioModificacionID)
                                     ));
                var xmlOrigen =
                    new XElement("ROOT",
                                 from lote in lotesOrigen
                                 select new XElement("LotesOrigen",
                                                     new XElement("LoteID", lote.LoteID),
                                                     new XElement("Cabezas", lote.Cabezas),
                                                     new XElement("CabezasInicio", lote.CabezasInicio),
                                                     new XElement("UsuarioModificacionID", lote.UsuarioModificacionID)
                                     ));
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LotesDestinoXML", xmlDestino.ToString()},
                            {"@LotesOrigenXML", xmlOrigen.ToString()},
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
        /// Obtiene el lote filtrando por CorralID, y Codigo de Lote
        /// </summary>
        /// <param name="lote">Objeto que contiene los parametros del Lote</param>
        internal static Dictionary<string, object> ObtenerParametrosLotePorCodigoLote(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", lote.OrganizacionID},
                            {"@CorralID", lote.CorralID},
                            {"@Lote", lote.Lote}
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
        /// Obtiene el lote filtrando por CorralID, y Codigo de Lote
        /// </summary>
        /// <param name="lote">Objeto que contiene los parametros del Lote</param>
        internal static Dictionary<string, object> ObtenerParametrosPesoCompraPorLote(LoteInfo lote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", lote.LoteID}
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
        /// Genera los Parametros para la Acutalizacion
        /// de las Cabezas que tiene un Lote
        /// </summary>
        /// <param name="filtroActualizarCabezasLote"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarCabezasProcesadas(FiltroActualizarCabezasLote filtroActualizarCabezasLote)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteIDOrigen", filtroActualizarCabezasLote.LoteIDOrigen},
                        {"@LoteIDDestino", filtroActualizarCabezasLote.LoteIDDestino},
                        {"@CabezasProcesadas", filtroActualizarCabezasLote.CabezasProcesadas},
                        {"@UsuarioModificacionID", filtroActualizarCabezasLote.UsuarioModificacionID}
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
        /// Obtiene el lote filtrando por LoteId
        /// </summary>
        /// <param name="loteId">Objeto que contiene los parametros del Lote</param>
        internal static Dictionary<string, object> ObtenerParametrosEstatusPorLoteId(int loteId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", loteId}
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosValidarCorralCompletoParaSacrificio(int loteId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", loteId}
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerAretesCorralPorLoteId(int loteId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", loteId}
                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosLotesConAnimalesDisponibles(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionId", organizacionId}
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
 