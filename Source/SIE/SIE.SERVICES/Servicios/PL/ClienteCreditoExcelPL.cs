using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ClienteCreditoExcelPL
    {
        public ResultadoInfo<ClienteCreditoExcelInfo> ObtenerPorPagina(PaginacionInfo pagina, ClienteCreditoExcelInfo filtro)
        {
            ResultadoInfo<ClienteCreditoExcelInfo> resultado;
            try
            {
                Logger.Info();
                var clienteCreditoExcelBL = new ClienteCreditoExcelBL();
                resultado = clienteCreditoExcelBL.ObtenerPorPagina(pagina,filtro);
            
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        public ClienteCreditoExcelInfo ObtenerPorID(ClienteCreditoExcelInfo clienteCreditoExcelInfo)
        {
            ClienteCreditoExcelInfo clienCreditoExcelInfo;
            try
            {
                Logger.Info();
                var clienteCreditoExceBL = new ClienteCreditoExcelBL();
                clienCreditoExcelInfo = clienteCreditoExceBL.ObtenerPorID(clienteCreditoExcelInfo);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            return clienCreditoExcelInfo;
        }
            
    }
}
