using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxInterfaceSalidaAnimalDAL
    {
        /// <summary>
        /// Obtener los parametros para obtener el numero de arete.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerNumeroAreteIndividual(string arete, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Arete", arete},
                        {"@OrganizacionID", organizacionID},
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
        /// Obtener los parametros para obtener el numero de arete metalico.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerNumeroAreteMetalico(string arete, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@AreteMetalico", arete},
                        {"@OrganizacionID", organizacionID},
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
        /// Obtiene todos los aretes en base a la salida.
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerAretesInterfazSalidaAnimal(string codigoCorral, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CodigoCorral", codigoCorral},
                        {"@OrganizacionID", organizacionID},
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
        /// ejecucion del procedimiento almacenado
        /// InterfaceSalidaAnimal_ObtenerPorSalidaOrganizacion
        /// </summary>
        /// <param name="salida"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerInterfazSalidaAnimal(int salida, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@SalidaID", salida},
                        {"@OrganizacionID", organizacionID},
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
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado InterfaceSalidaAnimal_ObtenerPorOrganizacionOrigenDestino
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerInterfazSalidaAnimalPorEntradas(List<EntradaGanadoInfo> entradas)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from entrada in entradas
                                 select new XElement("Salidas",
                                                     new XElement("SalidaID", entrada.FolioOrigen),
                                                     new XElement("OrganizacionDestinoID", entrada.OrganizacionID),
                                                     new XElement("OrganizacionOrigenID", entrada.OrganizacionOrigenID)
                                     ));
                parametros = new Dictionary<string, object>
                                 {
                                     {"@SalidaXML", xml.ToString()}
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
        /// Guarda el AnimalID .
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> GuardarAnimalID(InterfaceSalidaAnimalInfo animalInterface, long AnimalID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@SalidaID", animalInterface.SalidaID},
                        {"@OrganizacionID", animalInterface.Organizacion.OrganizacionID},
                        {"@Arete", animalInterface.Arete},
                        {"@AreteMetalico", animalInterface.AreteMetalico},
                        {"@AnimalID", AnimalID},
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
        /// Obtener parametros para interfaz salida animal
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInterfazSalidaAnimalPorEntradaGanado(EntradaGanadoInfo entradaGanadoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@SalidaID",entradaGanadoInfo.FolioOrigen},
                                     {"@OrganizacionID",entradaGanadoInfo.OrganizacionOrigenID}
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
        /// Obtener los parametros para obtener el numero de arete.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerNumeroAreteIndividualPartidaActiva(string arete, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Arete", arete},
                        {"@OrganizacionID", organizacionID},
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
