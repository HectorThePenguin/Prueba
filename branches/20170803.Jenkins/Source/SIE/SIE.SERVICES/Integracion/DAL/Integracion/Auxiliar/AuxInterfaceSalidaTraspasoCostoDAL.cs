using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxInterfaceSalidaTraspasoCostoDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para la 
        /// ejecucion del procedimiento almanceado 
        /// InterfaceSalidaTraspasoCosto_CrearXML
        /// </summary>
        /// <param name="costosGanadoTransferido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(List<InterfaceSalidaTraspasoCostoInfo> costosGanadoTransferido)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from costo in costosGanadoTransferido
                                 select new XElement("InterfaceSalidaTraspasoCosto",
                                                     new XElement("AnimalID", costo.AnimalID),
                                                     new XElement("CostoID", costo.Costo.CostoID),
                                                     new XElement("Importe", costo.Importe),
                                                     new XElement("InterfaceSalidaTraspasoDetalleID",
                                                                  costo.InterfaceSalidaTraspasoDetalle.
                                                                      InterfaceSalidaTraspasoDetalleID),
                                                     new XElement("UsuarioCreacionID", costo.UsuarioCreacionID),
                                                     new XElement("Activo", costo.Activo.GetHashCode())
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CostosXML", xml.ToString()},
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
        /// InterfaceSalidaTraspasoCosto_ObtenerCostosPorInterfaceDetalle
        /// </summary>
        /// <param name="interfaceSalidaTraspasoDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCostosInterfacePorDetalle(List<InterfaceSalidaTraspasoDetalleInfo> interfaceSalidaTraspasoDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in interfaceSalidaTraspasoDetalle
                                 select new XElement("InterfaceSalidaTraspasoCosto",
                                                     new XElement("InterfaceSalidaTraspasoDetalleID",
                                                                  detalle.InterfaceSalidaTraspasoDetalleID),
                                                     new XElement("Cabezas",
                                                                  detalle.Cabezas)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@InterfaceSalidaTraspasoDetalleXML", xml.ToString()},
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado
        /// InterfaceSalidaTraspasoCosto_ActualizarFacturadoXML
        /// </summary>
        /// <param name="animalesAFacturar"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFacturado(List<long> animalesAFacturar)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from animal in animalesAFacturar
                                 select new XElement("InterfaceSalidaTraspasoCosto",
                                                     new XElement("AnimalID", animal)
                                     ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalesXML", xml.ToString()},
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
