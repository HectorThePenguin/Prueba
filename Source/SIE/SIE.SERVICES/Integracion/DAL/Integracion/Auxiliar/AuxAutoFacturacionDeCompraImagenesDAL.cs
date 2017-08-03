using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAutoFacturacionDeCompraImagenesDAL
    {

        internal static Dictionary<string, object> ObtenerParametrosObtenerImagenDocumento(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@OrganizacionId", info.OrganizacionId},
                        {"@FolioCompra", info.FolioCompra}
                    };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }     
        }
        internal static Dictionary<string, object> ObtenerParametrosObtenerImagenIneCurp(AutoFacturacionInfo info)
        {
            try 
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@OrganizacionId",info.OrganizacionId},
                        {"@ProveedorId",info.ProveedorId},
                        {"@UltimoIne",info.ImgINEId},
                        {"@UltimoCurp",info.ImgCURPId}
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
