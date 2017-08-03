using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxGenerarArchivoDatalinkDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosObtenerRepartoDetalle(RepartoInfo repartoInfo, int tipoServicioID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", repartoInfo.OrganizacionID},
								{"@TipoServicioID", tipoServicioID},
                                {"@FechaReparto",repartoInfo.Fecha}
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
