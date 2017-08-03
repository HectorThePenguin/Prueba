using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Auxiliar;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxClienteCreditoExcelDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ClienteCreditoExcelInfo filtro)
        {
            Dictionary<string, object> parametros;

            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                 {"@Nombre", filtro.Nombre ?? string.Empty},
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

        internal static Dictionary<string, object> ObtenerParametrosPorID(ClienteCreditoExcelInfo clienteCreditoExcelInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                            {
                                {"@CreditoID", clienteCreditoExcelInfo.CreditoID}
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
