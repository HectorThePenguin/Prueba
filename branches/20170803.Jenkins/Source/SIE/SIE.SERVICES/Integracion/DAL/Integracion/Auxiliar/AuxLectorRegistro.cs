using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxLectorRegistro
    {
        /// <summary>
        /// Obtiene el lector registro de un lote y una fecha especifica
        /// </summary>
        /// <param name="lote">Lote del cual se obtendra el lector</param>
        /// <param name="fechaReparto">Fecha del lector</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroLectorRegistro(LoteInfo lote, DateTime fechaReparto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", lote.LoteID},
                            {"@OrganizacionID", lote.OrganizacionID},
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@Fecha", fechaReparto}
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
        /// Obtiene los parametros necesarios para obtener el detalle del lector registro
        /// </summary>
        /// <param name="lector">Lector de cual se quiere obtener el detalle</param>
        /// <returns>Diccionario de parametros</returns>
        internal static Dictionary<string, object> ObtenerParametroDetalleLectorRegistro(LectorRegistroInfo lector)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LectorRegistroID", lector.LectorRegistroID},
                            {"@Activo", (int)EstatusEnum.Activo},
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
        /// Obtiene el lector registro de un lote y una fecha especifica
        /// </summary>
        /// <param name="lotesXml">Lote del cual se obtendra el lector</param>
        /// <param name="fechaReparto">Fecha del lector</param>
        /// <param name="organizacionID">Fecha del lector</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroLectorRegistroXML(IList<LoteInfo> lotesXml, DateTime fechaReparto, int organizacionID)
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
                            {"@OrganizacionID", organizacionID},
                            {"@Activo", (int)EstatusEnum.Activo},
                            {"@Fecha", fechaReparto}
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
