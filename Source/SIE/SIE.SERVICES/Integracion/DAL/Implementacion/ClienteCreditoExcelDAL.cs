using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ClienteCreditoExcelDAL : DALBase
    {
        internal ResultadoInfo<ClienteCreditoExcelInfo> ObtenerPorPagina(PaginacionInfo pagina, ClienteCreditoExcelInfo filtro)
        {
            ResultadoInfo<ClienteCreditoExcelInfo> clienteCreditoExcelLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxClienteCreditoExcelDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ClienteCreditoExcel_ObtenerPorPagina", parameters);
                if(ValidateDataSet(ds))
                {
                    clienteCreditoExcelLista = MapClienteCreditoExceDAL.ObtenerPorPagina(ds);                
                }               
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return clienteCreditoExcelLista;
        }

        internal ClienteCreditoExcelInfo ObtenerPorID(ClienteCreditoExcelInfo clienteCreditoExcelInfo)
        {
            ClienteCreditoExcelInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxClienteCreditoExcelDAL.ObtenerParametrosPorID(clienteCreditoExcelInfo);
                DataSet ds = Retrieve("ClienteCreditoExcel_ObtenerPorID", parameters);
                if(ValidateDataSet(ds))
                {
                    result = MapClienteCreditoExceDAL.ObtenerPorID(ds);               
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;        
        }
    }
}
