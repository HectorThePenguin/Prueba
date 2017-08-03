using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using System.Xml.Linq;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMonitoreoSiloDAL
    {
        /// Obtiene parametros 
        internal static Dictionary<string, object> ObtenerTemperaturaMax(int organizacionID, string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@Descripcion", descripcion}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Obtiene parametros para obtener un Almacen por su Id
        internal static Dictionary<string, object> ObtenerSilosParaMonitoreo(int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Obtiene parametros 
        internal static Dictionary<string, object> Guardar(List<MonitoreoSiloInfo> listaMonitoreoSilo, MonitoreoSiloInfo silosInfo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                new XElement("ROOT",
               from datos in listaMonitoreoSilo
               select
                   new XElement("MonitoreoSilos",
                                new XElement("TemperaturaCelda", datos.TemperaturaCelda),
                                new XElement("AlturaSilo", datos.AlturaSilo),
                                new XElement("UbicacionSensor", datos.UbicacionSensor)
                                )
                   );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlGuardarDetalleMonitoreoSilo", xml.ToString()},
                            {"@Temperatura", silosInfo.TemperaturaAmbiente},
                            {"@SiloDescripcion", silosInfo.SiloDescripcion},
                            {"@ProductoID", silosInfo.ProductoID},
                            {"@HR", silosInfo.HR},
                            {"@Observaciones", silosInfo.Observaciones},
                            {"@UsuarioCreacionID", silosInfo.UsuarioCreacionId},
                            {"@OrganizacionID", organizacionID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
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
