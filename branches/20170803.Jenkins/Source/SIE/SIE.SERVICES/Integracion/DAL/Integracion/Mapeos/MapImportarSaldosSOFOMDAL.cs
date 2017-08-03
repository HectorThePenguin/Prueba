using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapImportarSaldosSOFOMDAL
    {
        public List<ImportarSaldosSOFOMInfo> ObtenerProveedor(DataSet ds)
        {
            var listProveInfo = new List<ImportarSaldosSOFOMInfo>();
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    var proveInfo = new ImportarSaldosSOFOMInfo
                    {
                        CreditoID = Convert.ToInt32(dr["CreditoID"].ToString()),
                        Nombre = dr["Nombre"].ToString(),
                        Existe = Convert.ToBoolean(dr["Existe"]),
                        TipoCredito = new TipoCreditoInfo { TipoCreditoID = Convert.ToInt32(dr["TipoCreditoID"].ToString()), Descripcion = dr["TipoCredito"].ToString() },
                        Proveedor = dr["Proveedor"].ToString(),
                        Centro = dr["Centro"].ToString(),
                        Saldo = Convert.ToDouble(dr["Saldo"].ToString()),
                        Ganadera = dr["Ganadera"].ToString()
                    };

                    listProveInfo.Add(proveInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listProveInfo;
        }

        public ImportarSaldosSOFOMInfo CreditoSOFOM_ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var info = new ImportarSaldosSOFOMInfo
                {
                    CreditoID = Convert.ToInt32(dt.Rows[0]["CreditoID"].ToString()),
                    TipoCredito = new TipoCreditoInfo { TipoCreditoID = Convert.ToInt32(dt.Rows[0]["TipoCreditoID"].ToString()), Descripcion = dt.Rows[0]["TipoCredito"].ToString() },
                    Saldo = Convert.ToDouble(dt.Rows[0]["Saldo"].ToString())
                };

                return info;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
