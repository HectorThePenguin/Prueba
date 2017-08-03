using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAutoFacturacionDeCompraImagenesDAL
    {
        internal static AutoFacturacionInfo ObtenerImagenDocumento(DataSet ds)
        {
            AutoFacturacionInfo autoFacturacionInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                autoFacturacionInfo = new AutoFacturacionInfo();
                foreach(DataRow dr in dt.Rows)
                {
                    if (dr["Imagen"].ToString() != "")
                    {
                        autoFacturacionInfo.ImgDocmento = (byte[])dr["Imagen"]; 
                    }
                }
            
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            
            }
            return autoFacturacionInfo;
        }
        internal static AutoFacturacionInfo ObtenerImagenIneCurp(DataSet ds)
        {
            AutoFacturacionInfo autoFacturacionInfo;
            try 
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                autoFacturacionInfo = new AutoFacturacionInfo();
                foreach(DataRow dr in dt.Rows)
                {
                    if(dr["INE"].ToString() != "")
                    {
                        autoFacturacionInfo.ImgINE = (byte[])dr["Ine"] ;
                    }
                    autoFacturacionInfo.ImgINECount = Convert.ToInt32(dr["IneCount"].ToString());
                    autoFacturacionInfo.ImgINEId = Convert.ToInt32(dr["IneId"].ToString());
                    autoFacturacionInfo.ImgINEMax = Convert.ToInt32(dr["IneMax"].ToString());
                    if (dr["CURP"].ToString() != "")
                    {
                        autoFacturacionInfo.ImgCURP = (byte[])dr["Curp"];    
                    }
                    autoFacturacionInfo.ImgCURPCount = Convert.ToInt32(dr["CurpCount"].ToString());
                    autoFacturacionInfo.ImgCURPId = Convert.ToInt32(dr["CurpId"].ToString());
                    autoFacturacionInfo.ImgCURPMax = Convert.ToInt32(dr["CurpMax"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);

            }
            return autoFacturacionInfo;
        
        }
    }
}
