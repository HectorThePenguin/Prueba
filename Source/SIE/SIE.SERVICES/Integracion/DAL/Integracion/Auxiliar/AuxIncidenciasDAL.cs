using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxIncidenciasDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosConfiguracionAlertas(EstatusEnum activo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", activo.GetHashCode()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerIncidenciasPorOrganizacionID(int organizacionID, bool usuarioCorporativo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@Corporativo", usuarioCorporativo}
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerIncidenciasActivas()
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", EstatusEnum.Activo},
                            {"@EstatusID",Estatus.CerrarAler}
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> ActualizarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidencia.IncidenciasID},
                            {"@Fecha", incidencia.FechaVencimiento},
                            {"@Comentarios", incidencia.Comentarios},
                            {"@NivelAlertaID", incidencia.NivelAlerta.NivelAlertaId},
                            {"@AccionID", incidencia.Accion.AccionID},
                            {"@UsuarioID", incidencia.UsuarioResponsable.UsuarioID}
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> RechazarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidencia.IncidenciasID},
                            {"@Comentarios", incidencia.Comentarios},
                            {"@NivelAlertaID", incidencia.NivelAlerta.NivelAlertaId},
                            {"@UsuarioID", incidencia.UsuarioResponsable.UsuarioID},
                            {"@EstatusID", incidencia.Estatus.EstatusId}
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> AutorizarIncidencia(IncidenciasInfo incidencia)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidencia.IncidenciasID},
                            {"@Fecha", incidencia.FechaVencimiento},
                            {"@Comentarios", incidencia.Comentarios},
                            {"@NivelAlertaID", incidencia.NivelAlerta.NivelAlertaId},
                            {"@AccionID", incidencia.Accion.AccionID},
                            {"@UsuarioID", incidencia.UsuarioResponsable.UsuarioID},
                            {"@EstatusID", incidencia.Estatus.EstatusId}
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerSeguimientoPorIncidenciaID(int incidenciaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidenciaID}
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
        /// 
        /// </summary>
        /// <param name="listaNuevasIncidencias"></param>
        /// <param name="TipoFolioID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarNuevasIncidencias(List<IncidenciasInfo> listaNuevasIncidencias, int TipoFolioID)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from incidencias in listaNuevasIncidencias
                                select
                                    new XElement("Incidencias",
                                                 new XElement("OrganizacionID", incidencias.Organizacion.OrganizacionID),
                                                 new XElement("AlertaID", incidencias.Alerta.AlertaID),
                                                 new XElement("HorasRespuesta", incidencias.Alerta.HorasRespuesta),
                                                 new XElement("NivelAlertaId", incidencias.Alerta.ConfiguracionAlerta.NivelAlerta.NivelAlertaId),
                                                 new XElement("XmlConsulta", incidencias.XmlConsulta.ToString()),
                                                 new XElement("EstatusId", incidencias.Estatus.EstatusId),
                                                 new XElement("UsuarioCreacionID", incidencias.UsuarioCreacionID),
                                                 new XElement("Activo", incidencias.Activo.GetHashCode()))
                                    );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlIncidencias", xml.ToString()},
                            {"@TipoFolioID", TipoFolioID}
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
        /// 
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCerrarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidenciaInfo.IncidenciasID},
                            {"@EstatusID",incidenciaInfo.Estatus.EstatusId},
                            {"@HorasRespuesta",incidenciaInfo.Alerta.HorasRespuesta},
                            {"@EstatusAnteriorID",incidenciaInfo.IncidenciaSeguimiento.EstatusAnterior.EstatusId},
                            {"@AccionAnteriorID",incidenciaInfo.IncidenciaSeguimiento.AccionAnterior.AccionID},
                            {"@Fecha",incidenciaInfo.Fecha},
                            {"@FechaVencimientoAnterior",incidenciaInfo.FechaVencimiento},
                            {"@NivelAlertaID",incidenciaInfo.IncidenciaSeguimiento.NivelAlertaAnterior.NivelAlertaId},
                            {"@UsuarioModificacionID",incidenciaInfo.UsuarioModificacionID},
                            {"@UsuarioResponsableAnteriorID",incidenciaInfo.IncidenciaSeguimiento.UsuarioResponsableAnterior.UsuarioID},
                            {"@Comentarios",incidenciaInfo.Comentarios}
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
        /// 
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosIncidenciaVencida(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidenciaInfo.IncidenciasID},
                            {"@EstatusID",incidenciaInfo.Estatus.EstatusId},
                            {"@HorasRespuesta",incidenciaInfo.Alerta.HorasRespuesta},
                            {"@Fecha",incidenciaInfo.Fecha},
                            {"@Comentarios",incidenciaInfo.Comentarios},
                            {"@UsuarioModificacionID",incidenciaInfo.UsuarioModificacionID},
                            {"@EstatusAnteriorID",incidenciaInfo.IncidenciaSeguimiento.EstatusAnterior.EstatusId},
                            {"@AccionAnteriorID",incidenciaInfo.IncidenciaSeguimiento.AccionAnterior.AccionID},
                            {"@UsuarioResponsableAnteriorID",incidenciaInfo.IncidenciaSeguimiento.UsuarioResponsableAnterior.UsuarioID},
                            {"@NivelAlertaID",incidenciaInfo.IncidenciaSeguimiento.NivelAlertaAnterior.NivelAlertaId},
                            {"@FechaVencimientoAnterior",incidenciaInfo.IncidenciaSeguimiento.FechaVencimientoAnterior},
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
        /// 
        /// </summary>
        /// <param name="incidenciaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosRegistrarIncidencia(IncidenciasInfo incidenciaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IncidenciaID", incidenciaInfo.IncidenciasID},
                            {"@EstatusID",incidenciaInfo.Estatus.EstatusId},
                            {"@HorasRespuesta",incidenciaInfo.Alerta.HorasRespuesta},
                            {"@EstatusAnteriorID",incidenciaInfo.IncidenciaSeguimiento.EstatusAnterior.EstatusId},
                            {"@AccionAnteriorID",incidenciaInfo.IncidenciaSeguimiento.AccionAnterior.AccionID},
                            {"@Fecha",incidenciaInfo.Fecha},
                            {"@FechaVencimientoAnterior",incidenciaInfo.IncidenciaSeguimiento.FechaVencimientoAnterior},
                            {"@NivelAlertaID",incidenciaInfo.IncidenciaSeguimiento.NivelAlertaAnterior.NivelAlertaId},
                            {"@UsuarioModificacionID",incidenciaInfo.UsuarioModificacionID},
                            {"@UsuarioResponsableAnteriorID",incidenciaInfo.IncidenciaSeguimiento.UsuarioResponsableAnterior.UsuarioID},
                            {"@Comentarios",incidenciaInfo.Comentarios}
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
