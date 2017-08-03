using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEnfermeriaCorralDAL
    {
        /// <summary>
        /// Metodo para obtener los parametros para obtener Peso
        /// </summary>
        /// <param name="enfermeriaID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerCorralesEnfermeria(int enfermeriaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EnfermeriaID", enfermeriaID},
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
        /// <param name="listaEnfermeriaCorral">Valores de la entidad</param>
        ///  <param name="enfermeriaID">Id de la tabla Enfermeria</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarEnfermeriaCorral(List<EnfermeriaCorralInfo> listaEnfermeriaCorral, int enfermeriaID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in listaEnfermeriaCorral
                                              select
                                                  new XElement("EnfermeriaCorral",
                                                               new XElement("EnfermeriaCorralID", info.EnfermeriaCorralID),
                                                               new XElement("EnfermeriaID", enfermeriaID),
                                                               new XElement("CorralID", info.CorralID),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlEnfermeriaCorral", xml.ToString()}
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
        /// Metodo para obtener los parametros para obtener Peso
        /// </summary>
        /// <param name="enfermeriaID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInactivarEnfermeriaCorralYSupervisorEnfermeria(int enfermeriaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EnfermeriaID", enfermeriaID},
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
