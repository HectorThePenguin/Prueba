using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    class MapPremezclaDistribucionCostoDAL
    {
        internal static List<PremezclaDistribucionCostoInfo> ObtenerPremezclaDistribucionCosto(DataSet ds)
        {
            List<PremezclaDistribucionCostoInfo> premezclaDistribucionCosto;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                premezclaDistribucionCosto = (from info in dt.AsEnumerable()
                                                select new PremezclaDistribucionCostoInfo
                                                {
                                                    PremezcaDistribucionCostoID = info.Field<Int64>("PremezcaDistribucionCostoID"),
                                                    PremezclaDistribucionID = info.Field<int>("PremezclaDistribucionID"),
                                                    Costo = new CostoInfo { CostoID = info.Field<int>("CostoID") },
                                                    TieneCuenta = info.Field<bool>("TieneCuenta"),
                                                    Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("DescripcionProveedor"),CodigoSAP = info.Field<string>("CodigoSap") },
                                                    //CuentaProvision = new CuentaSAPInfo {CuentaSAP = info.Field<string>("CuentaProvision"),Descripcion = info.Field<string>("DescripcionCuenta")},
                                                    CuentaProvision = info.Field<string>("CuentaProvision"),
                                                    DescripcionCuenta = info.Field<string>("DescripcionCuenta"),
                                                    Importe = info.Field<decimal>("Importe"),
                                                    Iva = info.Field<bool>("Iva"),
                                                    Retencion = info.Field<bool>("Retencion"),
                                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                    UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                                    UsuarioModificacionID = info.Field<int>("UsuarioModificacionID")
                                                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return premezclaDistribucionCosto;
        }
    }
}
