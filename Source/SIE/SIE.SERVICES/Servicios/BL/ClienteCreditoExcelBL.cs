using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ClienteCreditoExcelBL
    {
        internal ResultadoInfo<ClienteCreditoExcelInfo> ObtenerPorPagina(PaginacionInfo pagina, ClienteCreditoExcelInfo filtro)
        {
            ResultadoInfo<ClienteCreditoExcelInfo> result;
            
            try
            { 
                Logger.Info();
            var clienteCreditoExcelBL = new ClienteCreditoExcelDAL();
                result  = clienteCreditoExcelBL.ObtenerPorPagina(pagina,filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal ClienteCreditoExcelInfo ObtenerPorID(ClienteCreditoExcelInfo clienteCreditoExcelInfo)
        {
            ClienteCreditoExcelInfo clienCreditoExcelInfo;
            try
            { 
                Logger.Info();
                var clienteCreditoExcelDAL = new ClienteCreditoExcelDAL();
                clienCreditoExcelInfo = clienteCreditoExcelDAL.ObtenerPorID(clienteCreditoExcelInfo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return clienCreditoExcelInfo;
        }
    }
}
