using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// Clase auxiliar para la capa de datos de Muerte
    /// </summary>
    internal class AuxMuerteDAL
    {
        /// <summary>
        /// Parametro organizacionid
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacion(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionId", organizacionId},
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
        /// Parametros por organizacionid y numero de arete
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="numeroArete"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorOrganizacionArete(int organizacionId, string numeroArete)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionId", organizacionId},
                                     {"@Arete", numeroArete},
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
        /// Parametros para guardar salida por necropsia
        /// </summary>
        /// <param name="muerte"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarSalidaPorMuerteNecropsia(MuerteInfo muerte)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@MuerteId", muerte.MuerteId},
                                     {"@ProblemaId", muerte.ProblemaId},
                                     {"@Observaciones", muerte.Observaciones},
                                     {"@FotoNecropsia", muerte.FotoNecropsia},
                                     {"@OperadorNecropsiaId", muerte.OperadorNecropsiaId},
                                     {"@UsuarioModificacionId", muerte.UsuarioCreacionID},
                                     {"@EstatusID", muerte.EstatusId},
                                     {"@AnimalID", muerte.AnimalId}
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
        /// Parametros para cancelar movimiento de muerte
        /// </summary>
        /// <param name="muerteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCancelarMovimientoMuerte(MuerteInfo muerteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@MuerteID", muerteInfo.MuerteId},
                        {"@MotivoCancelacion", muerteInfo.MotivoCancelacion},
                        {"@OperadorCancelacion", muerteInfo.OperadorCancelacionInfo.OperadorID},
                        {"@UsuarioModificacionID", muerteInfo.UsuarioCreacionID},
                        {"@EstatusID", muerteInfo.EstatusId}

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
        /// Parametros para muerteid y operadorid
        /// </summary>
        /// <param name="muerte"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMuerteId(MuerteInfo muerte, int operadorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@MuerteId", muerte.MuerteId},
                                     {"@OperadorId", operadorId}
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
        /// Parametros para guardado de muerte
        /// </summary>
        /// <param name="muerte"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarMuerte(MuerteInfo muerte)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                    new XElement("MuerteGrabar",
                                                 new XElement("Organizacion", muerte.OrganizacionId),
                                                 new XElement("CorralID", muerte.CorralId),
                                                 new XElement("Arete", muerte.Arete),
                                                 new XElement("AreteMetalico", muerte.AreteMetalico),
                                                 new XElement("Observaciones", muerte.Observaciones),
                                                 new XElement("LoteID", muerte.LoteId),
                                                 new XElement("OperadorDeteccion", muerte.OperadorDeteccionId),
                                                 new XElement("FotoDeteccion", muerte.FotoDeteccion),
                                                 new XElement("EstatusID", muerte.EstatusId),
                                                 new XElement("Activo", muerte.Activo),
                                                 new XElement("UsuarioID",muerte.UsuarioCreacionID)
                                    )
                );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlMuerte", xml.ToString()}
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
        /// Obtiene los parametros para la recoleccion de ganado muerto
        /// </summary>
        /// <param name="muerte"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMuerteRecoleccion(MuerteInfo muerte, int operadorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@MuerteId", muerte.MuerteId},
                                     {"@OperadorId", operadorId},
                                     {"@FechaRecoleccion", muerte.FechaRecoleccion}
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
        /// Obtiene los los parametros para obtener las muertes por fecha necropsia
        /// </summary>
        /// <param name="muerteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMuertesFechaNecropsia(MuerteInfo muerteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID",muerteInfo.OrganizacionId},
                        {"@FechaNecropsia", muerteInfo.FechaNecropsia}
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
        /// Parametro organizacionid
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorLoteID(int loteID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
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
        /// Parametros por organizacionid y numero de arete
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="numeroArete"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMuertoPorArete(int organizacionId, string numeroArete)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionId", organizacionId},
                                     {"@Arete", numeroArete},
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
