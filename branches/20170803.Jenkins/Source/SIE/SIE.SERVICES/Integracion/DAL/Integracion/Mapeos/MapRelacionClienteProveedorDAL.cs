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
    internal class MapRelacionClienteProveedorDAL
    {
        internal static List<RelacionClienteProveedorInfo> ObtenerPorProveedorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];          
                var lista = new List<RelacionClienteProveedorInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    var info = new RelacionClienteProveedorInfo();
                    info.CreditoID = Convert.ToInt32(row["CreditoID"].ToString());
                    info.Proveedor = new ProveedorInfo { 
                        Descripcion = row["Proveedor"].ToString(), 
                        ProveedorID = Convert.ToInt32(row["ProveedorID"].ToString()) 
                    };
                    info.Credito = new ImportarSaldosSOFOMInfo { 
                        Saldo = Convert.ToDouble(row["Saldo"].ToString()), 
                        CreditoID = Convert.ToInt32(row["CreditoID"].ToString()),
                        TipoCredito = new TipoCreditoInfo { 
                            TipoCreditoID = Convert.ToInt32(row["TipoCreditoID"].ToString()),
                            Descripcion = row["TipoCredito"].ToString()
                        } 
                    };
                    info.Centro = new OrganizacionInfo {
                        OrganizacionID = Convert.ToInt32(row["CentroID"].ToString()),
                        Descripcion = row["Centro"].ToString()
                    };
                    info.Ganadera = new OrganizacionInfo {
                        OrganizacionID = Convert.ToInt32(row["GanaderaID"].ToString()),
                        Descripcion = row["Ganadera"].ToString()
                    };
                    info.Editable = false;
                    lista.Add(info);
                }

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
