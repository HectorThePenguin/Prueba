using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Xml.Linq;
using SIE.Base.Infos;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPagoTransferenciaDAL
    {
        /// <summary>
        ///     Obtiene parametros para crear, guardar.
        /// </summary>
        /// <param name="pago"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPago(PagoTransferenciaInfo pago)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                        new XElement("Pago",
                                            new XElement("PagoId", pago.PagoId),
                                            new XElement("CentroAcopioId", pago.CentroAcopioId),
                                            new XElement("ProveedorId", pago.ProveedorId),
                                            new XElement("BancoId", pago.BancoId),
                                            new XElement("Fecha", pago.Fecha),
                                            new XElement("FechaPago", pago.FechaPago),
                                            new XElement("CodigoAutorizacion", pago.CodigoAutorizacion),
                                            new XElement("Importe", pago.Importe),
                                            new XElement("FolioEntrada", pago.FolioEntrada),
                                            new XElement("UsuarioId", pago.UsuarioId)
                            ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlPago", xml.ToString()},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPagoConsulta(PaginacionInfo pagina, int centro, int folio)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CentroId", centro},
                        {"@FolioId", folio},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
