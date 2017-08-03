using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxProgramacionCorte
    {
        /// <summary>
        /// Metodo que obtiene los parametros para guardar las programaciones de corte
        /// </summary>
        /// <param name="programacionCorte"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(IList<ProgramacionCorteInfo> programacionCorte)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in programacionCorte
                                 select new XElement("ProgramacionCorte",
                                                     new XElement("OrganizacionID", detalle.OrganizacionID),
                                                     new XElement("FolioEntradaID", detalle.FolioEntradaID),
                                                     new XElement("FolioProgramacionID", detalle.FolioProgramacionID),
                                                     new XElement("Activo", (int)detalle.Activo),
                                                     new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                                    ));

                parametros = new Dictionary<string, object>
                    {
                        {"@XmlProgramacionCorte", xml.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosEliminarProgramacionCorte(ProgramacionCorte programacionCorte, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var lista = programacionCorte.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("PartidasCorte",
                                                new XElement("NoPartida", partida))
                                   );
                parametros = new Dictionary<string, object>
                        {
                            {"@noPartida", xml.ToString()},
                            {"@organizacionID ", organizacionID }
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
        /// Obtener los parametros para actualizar la fecha inicio de la programacion corte
        /// </summary>
        /// <param name="programacionCorte"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFechaInicioProgramacionCorte(ProgramacionCorte programacionCorte)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var lista = programacionCorte.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                               from partida in lista
                               select
                                   new XElement("PartidasCorte",
                                                new XElement("NoPartida", partida))
                                   );
                parametros = new Dictionary<string, object>
                        {
                            {"@noPartida", xml.ToString()},
                            {"@organizacionID ", programacionCorte.OrganizacionID },
                            {"@UsuarioModificacionID ", programacionCorte.UsuarioID }
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
