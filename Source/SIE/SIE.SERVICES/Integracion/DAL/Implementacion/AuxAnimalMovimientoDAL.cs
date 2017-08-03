using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Services.Info.Info;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AuxAnimalMovimientoDAL
    {
        /// <summary>
        /// Se envia el animal de AnimalMovimiento a AnimalMovimientoHistorico
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosAnimalID(AnimalInfo animalInactivo)
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

        /// <summary>
        ///     Metodo para obtener los parametros para guardr un AnimalMovimiento
        /// </summary>
        /// <param name="animalMovimientoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearAnimalMovimiento(AnimalMovimientoInfo animalMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", animalMovimientoInfo.AnimalID},
                            {"@OrganizacionID", animalMovimientoInfo.OrganizacionID},
                            {"@CorralID", animalMovimientoInfo.CorralID},
                            {"@LoteID", animalMovimientoInfo.LoteID},
                            {"@Peso", animalMovimientoInfo.Peso},
                            {"@Temperatura", animalMovimientoInfo.Temperatura},
                            {"@TipoMovimientoID", animalMovimientoInfo.TipoMovimientoID},
                            {"@TrampaID", animalMovimientoInfo.TrampaID},
                            {"@OperadorID", animalMovimientoInfo.OperadorID},
                            {"@Observaciones", animalMovimientoInfo.Observaciones},
                            {"@Activo", animalMovimientoInfo.Activo},
                            {"@UsuarioCreacionID", animalMovimientoInfo.UsuarioCreacionID}
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
        /// Obtiene los ultimos movimientos de los animales
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosUltimoMovimientoAnimal(List<AnimalInfo> animales)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from animal in animales
                                 select new XElement("Animales",
                                                     new XElement("AnimalID", animal.AnimalID)
                                     ));
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

        /// <summary>
        /// Obtiene los ultimos movimientos de los animales
        /// </summary>
        ///  <param name="organizacionID"></param>
        /// <param name="arete"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosPorArete(int organizacionID, string arete)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@Arete", arete}
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
        ///     Metodo para obtener los parametros para guardr un XMl con los Animales Movimiento
        /// </summary>
        /// <param name="listaAnimalesMovimiento"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearAnimalMovimientoXML(List<AnimalMovimientoInfo> listaAnimalesMovimiento)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                                 from animalMovimiento in listaAnimalesMovimiento
                                 select new XElement("AnimalMovimiento",
                                                     new XElement("AnimalID", animalMovimiento.AnimalID),
                                                     new XElement("OrganizacionID", animalMovimiento.OrganizacionID),
                                                     new XElement("CorralID", animalMovimiento.CorralID),
                                                     new XElement("LoteID", animalMovimiento.LoteID),
                                                     new XElement("Peso", animalMovimiento.Peso),
                                                     new XElement("Temperatura", animalMovimiento.Temperatura),
                                                     new XElement("TipoMovimientoID", animalMovimiento.TipoMovimientoID),
                                                     new XElement("TrampaID", animalMovimiento.TrampaID),
                                                     new XElement("OperadorID", animalMovimiento.OperadorID),
                                                     new XElement("Observaciones", animalMovimiento.Observaciones),
                                                     new XElement("Activo", animalMovimiento.Activo.GetHashCode()),
                                                     new XElement("UsuarioCreacionID", animalMovimiento.UsuarioCreacionID)
                                     ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalMovimientoXML", xml.ToString()}
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
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalesNoReimplantadosXML(int organizacionID, List<LoteInfo> lotes)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID)
                                     ));
                parametros = new Dictionary<string, object>
                                 {
                                     {"@LoteXML", xml.ToString()},
                                     {"@OrganizacionID", organizacionID}
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
        /// Metodo para mapear 
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTrazabilidadAnimalMovimiento(AnimalInfo animal)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@AnimalID", animal.AnimalID},
                                     {"@Historico", animal.Historico}
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
