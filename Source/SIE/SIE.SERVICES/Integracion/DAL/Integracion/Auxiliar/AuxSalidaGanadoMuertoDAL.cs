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
    internal class AuxSalidaGanadoMuertoDAL 
    {
        /// <summary>
        /// Obtiene los parametros para el procedimiento SalidaGandoMuerte_ObtenerFolio
        /// </summary>
        /// <param name="salidaGanadoMuerteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerFolio(SalidaGanadoMuertoInfo salidaGanadoMuerteInfo)
        {
            Dictionary<string, object> parametros;
            try 
	        {	        
		        Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", salidaGanadoMuerteInfo.OrganizacionID},
                        {"@TipoFolioID", salidaGanadoMuerteInfo.TipoFolio}        
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
        /// Obtiene los parametros para asignar el folio en una lista de ganado por muerte
        /// </summary>
        /// <param name="listaSalidaGanadoMuerto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosListaAsignarFolio(IList<SalidaGanadoMuertoInfo> listaSalidaGanadoMuerto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlMuerte =
                  new XElement("ROOT",
                               from muerte  in listaSalidaGanadoMuerto
                               select new XElement("Muertes",
                                      new XElement("Muerte", (int)muerte.MuerteID)));
                parametros = new Dictionary<string, object>
                    {
                        {"@Folio", listaSalidaGanadoMuerto[0].FolioSalida},
                        {"@UsuarioModificacionID", listaSalidaGanadoMuerto[0].UsuarioModificacionID},
                        {"@MuertesXML", xmlMuerte.ToString()}        
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
