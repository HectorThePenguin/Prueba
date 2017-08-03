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
    internal class AuxProgramacionSacrificio
    {

        /// Obtener parametros para consultar corrales por tipo.
        internal static Dictionary<string, object> ObtenerParametrosObtenerCorralesSacrificio(int organizacionID)
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

        /// Obtener parametros para consultar corrales por tipo.
        internal static Dictionary<string, object> ObtenerParametrosObtenerExistenciaAnimal(AnimalInfo animalInfo, int loteID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", animalInfo.Arete},
                            {"@OrganizacionID", animalInfo.OrganizacionIDEntrada},
                            {"@LoteID", loteID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Metodo crea XML para guardar AnimalSalida
        internal static Dictionary<String, Object> ObtenerParametrosGuardarAnimalSalida(List<AnimalInfo> listaAnimalInfo,  ProgramacionSacrificioGuardadoInfo programacionSacrificioGuardadoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //var lista = cabezasCortadas.NoPartida.Split('|');
                var xml =
                  new XElement("ROOT",
                                   new XElement("AnimalesGuardar",
                                                new XElement("Arete",
                                                             from animalInfo in listaAnimalInfo
                                                             select
                                                                 new XElement("AreteNodo",
                                                                         new XElement("Arete", animalInfo.Arete)
                                                     )

                                                )
                                                )
                              );
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAnimales", xml.ToString()},
                        {"@OrganizacionID", programacionSacrificioGuardadoInfo.OrganizacionID},
                        {"@TipoMovimientoID", programacionSacrificioGuardadoInfo.TipoMovimiento},
                        {"@UsuarioCreacionID", programacionSacrificioGuardadoInfo.UsuarioID},
                        {"@LoteID", programacionSacrificioGuardadoInfo.LoteID},
                        {"@CorraletaID", programacionSacrificioGuardadoInfo.CorraletaID},
                        {"@OrdenSacrificioDetalleID", programacionSacrificioGuardadoInfo.OrdenSacrificioDetalleID}
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
