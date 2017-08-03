using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxServicioAlimentoDAL
    {
        /// <summary>
        ///     Obtiene parametros por corralID
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorCorralID(int organizacionID, int corralID)
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
        ///     Obtiene parametros para guardar ServicioAlimento
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardar(IList<ServicioAlimentoInfo> info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in info
                                 select new XElement("ServicioAlimento",
                                        new XElement("OrganizacionID", detalle.OrganizacionID),
                                        new XElement("CorralID", detalle.CorralID),
                                        new XElement("Fecha", detalle.Fecha),
                                        new XElement("FormulaID", detalle.FormulaID),
                                        new XElement("KilosProgramados", detalle.KilosProgramados),
                                        new XElement("Comentarios", detalle.Comentarios),
                                        new XElement("Activo", detalle.Estatus),
                                        new XElement("FechaCreacion", detalle.FechaCreacion),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID),
                                        new XElement("FechaModificacion", detalle.FechaModificacion),
                                        new XElement("UsuarioModificacionID", detalle.UsuarioModificacionID),
                                        new XElement("ServicioID", detalle.ServicioID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlServicioAlimento", xml.ToString()}
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
        ///     Obtiene parametros para actualizar ServicioAlimento
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(ServicioAlimentoInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@KilosProgramados", info.KilosProgramados},
                                     {"@CorralID", info.CorralID},
                                     {"@FormulaID", info.FormulaID},
                                     {"@Comentarios", info.Comentarios},
                                     {"@OrganizacionID", info.OrganizacionID},
                                     {"@ServicioID", info.ServicioID}
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
        /// Obtiene el parametro de la organizacionId para obtener la informacion diaria.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerInformacionDiariaAlimento(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@organizacionId", organizacionId},
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
        /// Obtiene el parametro del corral para elimianr la programacion de servicio de alimento
        /// </summary>
        /// <param name="corralGrid"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEliminar(CorralRangoInfo corralGrid)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CorralID", corralGrid.CorralID},
                                     {"@UsuarioModificacionID", corralGrid.UsuarioModificacionId},
                                     
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
        /// Obtiene el parametro del corral para elimianr la programacion de servicio de alimento
        /// </summary>
        /// <param name="corralesEliminar"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerEliminarXML(List<CorralInfo> corralesEliminar)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                     new XElement("ROOT",
                                  from corral in corralesEliminar
                                  select new XElement("ServicioAlimento",
                                         new XElement("CorralID", corral.CorralID),
                                         new XElement("UsuarioModificacionID", corral.UsuarioModificacionID)
                                      ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlServicioAlimento", xml.ToString()}
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
