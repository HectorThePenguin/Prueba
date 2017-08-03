using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxDeteccionDAL
    {

        internal static Dictionary<string, object> ObtenerParametrosGrabar(DeteccionInfo deteccionGrabar)
        {
            try
            {
                Logger.Info();
                List<SintomaInfo> Sintomas = null;
                List<ProblemaInfo> Problemas = null;
                Sintomas = deteccionGrabar.Sintomas;
                Problemas = deteccionGrabar.Problemas;
                var xml =
                   new XElement("ROOT",
                                    new XElement("DeteccionGrabar",
                                                 new XElement("CorralCodigo", deteccionGrabar.CorralCodigo),
                                                 new XElement("CorralID", deteccionGrabar.CorralID),
                                                 new XElement("Arete", deteccionGrabar.Arete),
                                                 new XElement("AreteMetalico", deteccionGrabar.AreteMetalico),
                                                 new XElement("Observaciones", deteccionGrabar.Observaciones),
                                                 new XElement("LoteID", deteccionGrabar.LoteID),
                                                 new XElement("GradoID", deteccionGrabar.GradoID),
                                                 new XElement("TipoDeteccionID", deteccionGrabar.GrupoCorral),
                                                 new XElement("OperadorID", deteccionGrabar.OperadorID),
                                                 new XElement("FotoDeteccion", deteccionGrabar.FotoDeteccion),
                                                 new XElement("NoFierro", deteccionGrabar.NoFierro),
                                                 new XElement("Observaciones", deteccionGrabar.Observaciones),
                                                 new XElement("DescripcionGanado", deteccionGrabar.DescripcionGanado),
                                                 new XElement("DescripcionGanadoID", deteccionGrabar.DescripcionGanadoID),
                                                 new XElement("UsuarioCreacionID", deteccionGrabar.UsuarioCreacionID),
                                                 new XElement("Sintomas",
                                                             from sintoma in Sintomas
                                                                select
                                                                    new XElement("SintomasNodo",
                                                                            new XElement("SintomaID", sintoma.SintomaID)
                                                                    )
                                                                    
                                                     ),
                                                 new XElement("Problemas",
                                                       from problema in Problemas
                                                        select
                                                            new XElement("ProblemasNodo",
                                                                    new XElement("ProblemaID", problema.ProblemaID)
                                                                )
                                                     )
                                    )
                );
        
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlDeteccion", xml.ToString()}
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
        /// Parametros para la actualizacion de la deteccion con foto
        /// </summary>
        /// <param name="animalDetectado"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizadDeteccionFoto(AnimalDeteccionInfo animalDetectado)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@DeteccionId", animalDetectado.DeteccionID},
                            {"@Arete", animalDetectado.Animal.Arete},
                            {"@FotoDeteccion", animalDetectado.RutaFotoDeteccion}
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
