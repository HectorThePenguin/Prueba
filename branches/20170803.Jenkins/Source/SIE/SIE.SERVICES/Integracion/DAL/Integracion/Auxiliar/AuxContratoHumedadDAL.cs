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
    internal class AuxContratoHumedadDAL 
    {
        /// <summary>
        /// Crea porcentajes humedad a partir de una lista
        /// </summary>
        /// <param name="listaContratoHumedad"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearContratoHumedad(List<ContratoHumedadInfo> listaContratoHumedad)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaContratoHumedad
                                 select new XElement("XmlContratoHumedad",
                                        new XElement("ContratoID", detalle.ContratoID),
                                        new XElement("FechaInicio", detalle.FechaInicio),
                                        new XElement("PorcentajeHumedad", detalle.PorcentajeHumedad),
                                        new XElement("Activo", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlContratoHumedad", xml.ToString()}
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
        /// Obtiene los parametros para consultar por contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorContratoId(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contrato.ContratoId},
                            {"@Activo", EstatusEnum.Activo}
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
