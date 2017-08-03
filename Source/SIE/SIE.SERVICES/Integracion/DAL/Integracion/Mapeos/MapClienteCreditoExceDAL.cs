using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapClienteCreditoExceDAL
    {
        
        internal static ResultadoInfo<ClienteCreditoExcelInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<ClienteCreditoExcelInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ClienteCreditoExcelInfo> lista = (from clienteCreditoExcelInfo in dt.AsEnumerable()
                                                       select new ClienteCreditoExcelInfo
                                                       {
                                                           CreditoID = clienteCreditoExcelInfo.Field<int>("CreditoID"),
                                                           Nombre = clienteCreditoExcelInfo.Field<string>("Nombre")
                                                       }).ToList();
                resultado = new ResultadoInfo<ClienteCreditoExcelInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
      
        internal static ClienteCreditoExcelInfo ObtenerPorID(DataSet ds)
        {
            ClienteCreditoExcelInfo clienteCreditoExcelInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                clienteCreditoExcelInfo = (from clienteCreditoExcel in dt.AsEnumerable()
                                           select new ClienteCreditoExcelInfo
                                           {
                                               CreditoID = clienteCreditoExcel.Field<int>("CreditoID"),
                                               Nombre = clienteCreditoExcel.Field<string>("Nombre")
                                           }).First();
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return clienteCreditoExcelInfo;
        }
        
    }
}
