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
    internal class AuxContratoParcialDAL
    {
        /// <summary>
        /// Crea registros en AlmacenMovimientoCosto usando un xml
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearContratoParcial(List<ContratoParcialInfo> listaContratoParcial)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaContratoParcial
                                 select new XElement("XmlContratoParcial",
                                        new XElement("ContratoID", detalle.ContratoId),
                                        new XElement("Cantidad", Convert.ToInt32(detalle.Cantidad)),
                                        new XElement("Importe", detalle.Importe),
                                        new XElement("TipoCambioID", detalle.TipoCambio.TipoCambioId),
                                        new XElement("Activo", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlContratoParcial", xml.ToString()}
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
                            {"@ContratoID", contrato.ContratoId}
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
        /// Metodo que actualiza el estado de un contrato detalle
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstado(ContratoParcialInfo contratoParcialInfo, EstatusEnum estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ContratoParcialID", contratoParcialInfo.ContratoParcialId},
                                     {"@Activo", estatus},
                                     {"@UsuarioModificacionID", contratoParcialInfo.UsuarioModificacionId}
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
