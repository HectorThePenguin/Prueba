using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxGanadoIntensivoDAL
    {
        public static Dictionary<string, object> ObtenerParametrosMuerteGanadoIntensivo(CorralInfo corral)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                        {"@CorralID", corral.CorralID},
                        {"@Activo", corral.Activo.GetHashCode()},
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
        /// Se obtienen parametros para guardar ganado intensivo
        /// </summary>
        /// <param name="ganadoIntensivo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosGuardar(GanadoIntensivoInfo ganadoIntensivo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                  new XElement("ROOT",
                               from costo in ganadoIntensivo.ListaGanadoIntensivoCosto
                               select
                                   new XElement("Costos",
                                                new XElement("CostoID", costo.Costos.CostoID),
                                                new XElement("Importe", costo.Importe))
                                   );
                parametros = new Dictionary<string, object>
                                 {
                                     {"@XMLCostos", xml.ToString()},
                                     {"@TipoMovimientoID",ganadoIntensivo.TipoMovimientoID.GetHashCode()},
                                     {"@LoteID",ganadoIntensivo.Lote.LoteID},
                                     {"@Cabezas",ganadoIntensivo.Cabezas},
                                     {"@CabezasAnterior",ganadoIntensivo.Lote.Cabezas},
                                     {"@Importe",ganadoIntensivo.Importe},
                                     {"@Observaciones",ganadoIntensivo.Observaciones},
                                     {"@OrganizacionID",ganadoIntensivo.Organizacion.OrganizacionID},
                                     {"@TipoFolio",ganadoIntensivo.TipoFolio.GetHashCode()},
                                     {"@Activo",ganadoIntensivo.Activo.GetHashCode()},
                                     {"@UsuarioID",ganadoIntensivo.UsuarioCreacionID},
                                     {"@FolioTicket", ganadoIntensivo.FolioTicket},
                                     {"@PesoBruto", ganadoIntensivo.PesoBruto}
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
