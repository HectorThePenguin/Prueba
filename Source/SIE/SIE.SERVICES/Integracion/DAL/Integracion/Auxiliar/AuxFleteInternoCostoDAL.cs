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
    internal class AuxFleteInternoCostoDAL
    {
        /// <summary>
        /// Metodo que obtiene parametros para crear flete interno costo
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearFleteInternoCosto(List<FleteInternoCostoInfo> listaFleteInternoCosto, FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaFleteInternoCosto
                                 select new XElement("XmlFleteInternoCosto",
                                        new XElement("FleteInternoDetalleID", fleteInternoDetalleInfo.FleteInternoDetalleId),
                                        new XElement("CostoID", detalle.Costo.CostoID),
                                        new XElement("Tarifa", detalle.Tarifa),
                                        new XElement("Activo", (int)EstatusEnum.Activo),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlFleteInternoCosto", xml.ToString()}
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
        /// Actualiza registros en flete interno costo
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFleteInternoCosto(List<FleteInternoCostoInfo> listaFleteInternoCosto)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaFleteInternoCosto
                                 select new XElement("XmlFleteInternoCosto",
                                        new XElement("FleteInternoCostoID", detalle.FleteInternoCostoId),
                                        new XElement("Tarifa", detalle.Tarifa),
                                        new XElement("Activo", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioModificacionID", detalle.UsuarioModificacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlFleteInternoCosto", xml.ToString()}
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
        /// Obtener listado de fleteinternocosto por fleteinternodetalle
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFleteInternoDetalleId(FleteInternoDetalleInfo fleteInternoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FleteInternoDetalleID", fleteInternoDetalleInfo.FleteInternoDetalleId},
                            {"@Activo", fleteInternoDetalleInfo.Activo.GetHashCode()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
