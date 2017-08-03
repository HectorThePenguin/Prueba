using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPremezclaDetalleDAL
    {
        /// <summary>
        /// Metodo que obtiene parametros para crear contrato detalle
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearPremezclaDetalle(List<PremezclaDetalleInfo> listaPremezclaDetalle, PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaPremezclaDetalle
                                 select new XElement("XmlPremezclaDetalle",
                                        new XElement("PremezclaID", premezclaInfo.PremezclaId),
                                        new XElement("ProductoID", detalle.Producto.ProductoId),
                                        new XElement("Porcentaje", detalle.Porcentaje),
                                        new XElement("Activo", (int)EstatusEnum.Activo),
                                        new XElement("UsuarioCreacionID", premezclaInfo.UsuarioCreacion.UsuarioCreacionID)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlPremezclaDetalle", xml.ToString()}
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
        /// Actualiza registros en premezcla detalle
        /// </summary>
        /// <param name="listaPremezclaDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarPremezclaDetalle(List<PremezclaDetalleInfo> listaPremezclaDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaPremezclaDetalle
                                 select new XElement("XmlPremezclaDetalle",
                                        new XElement("PremezclaDetalleID", detalle.PremezclaDetalleID),
                                        new XElement("Porcentaje", detalle.Porcentaje),
                                        new XElement("Activo", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioModificacionID", detalle.UsuarioModificacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlPremezclaDetalle", xml.ToString()}
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
        /// Obtiene parametros para obtener el detalle de 
        /// </summary>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPremezclaDetallePorPremezclaId(PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PremezclaID", premezclaInfo.PremezclaId},
                            {"@Activo", premezclaInfo.Activo.GetHashCode()}
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
