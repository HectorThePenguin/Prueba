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
    internal class AuxSupervisionGanadoDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosGuardarSupervisionGanado(List<SupervisionGanadoInfo> supervision, int organizacionId)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from ganadoDetectado in supervision
                                select
                                    new XElement("Detectados",
                                                 new XElement("SupervisionGanadoID", ganadoDetectado.SupervisionGanadoID),
                                                 new XElement("CodigoCorral", ganadoDetectado.CodigoCorral),
                                                 new XElement("LoteID", ganadoDetectado.LoteID),
                                                 new XElement("Arete", ganadoDetectado.Arete),
                                                 new XElement("AreteMetalico", ganadoDetectado.AreteMetalico),
                                                 new XElement("FechaDeteccion", ganadoDetectado.FechaDeteccion),
                                                 new XElement("ConceptoDeteccionID", ganadoDetectado.ConceptoDeteccionID),
                                                 new XElement("Acuerdo", ganadoDetectado.Acuerdo),
                                                 new XElement("Notificacion", ganadoDetectado.Notificacion),
                                                 new XElement("Activo", ganadoDetectado.Activo),
                                                 new XElement("FotoSupervision", ganadoDetectado.FotoSupervision == "" ? "" : TipoFoto.Supervision.ToString() + "/" + ganadoDetectado.FotoSupervision))
                                    );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlSupervisionGanado", xml.ToString()},
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

        internal static Dictionary<string, object> ObtenerParametrosValidarAretesDetectados(string arete, string areteTestigo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", arete},
                            {"@AreteTestigo", areteTestigo}
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
