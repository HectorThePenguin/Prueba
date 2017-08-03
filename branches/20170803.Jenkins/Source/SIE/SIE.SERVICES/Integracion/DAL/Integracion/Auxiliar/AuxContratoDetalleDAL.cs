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
    internal class AuxContratoDetalleDAL
    {
        /// <summary>
        /// Metodo que obtiene parametros para obtener un contrato por id
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorContratoId(ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contratoInfo.ContratoId}
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
        /// Metodo que obtiene parametros para crear contrato detalle
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearContratoDetalle(List<ContratoDetalleInfo> listaContratoDetalleInfo, ContratoInfo contratoInfo)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaContratoDetalleInfo
                                 select new XElement("XmlContratoDetalle",
                                        new XElement("ContratoID", contratoInfo.ContratoId),
                                        new XElement("IndicadorID", detalle.Indicador.IndicadorId),
                                        new XElement("PorcentajePermitido", detalle.PorcentajePermitido),
                                        new XElement("Activo", (int)EstatusEnum.Activo),
                                        new XElement("UsuarioCreacionID", contratoInfo.UsuarioCreacionId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlContratoDetalle", xml.ToString()}
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
        /// <param name="contratoDetalleInfo"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstado(ContratoDetalleInfo contratoDetalleInfo, EstatusEnum estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ContratoDetalleID", contratoDetalleInfo.ContratoDetalleId},
                                     {"@Activo", estatus},
                                     {"@UsuarioModificacionID", contratoDetalleInfo.UsuarioModificacionId}
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
