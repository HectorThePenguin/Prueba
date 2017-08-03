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
    internal class AuxAnimalCostoDAL
    {
        /// <summary>
        /// Obtener parametros para metodo para validar si el animal Tiene AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <param name="costoGanado"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosValdiarTieneCostoGanadoAnimal(AnimalInfo animalInactivo, int costoGanado)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", animalInactivo.AnimalID},
                            {"@CostoID", costoGanado}
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
        /// Mapeo de AnimalID
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosAnimalCostoID(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", animalInactivo.AnimalID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosCostosAnimal(List<AnimalInfo> animalesGenerarPoliza)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from animal in animalesGenerarPoliza
                                 select new XElement("Animales",
                                        new XElement("AnimalID", animal.AnimalID),
                                        new XElement("OrganizacionID", animal.OrganizacionIDEntrada)));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlAnimales", xml.ToString()}
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
