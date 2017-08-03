using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Base.Infos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapAutoFacturacionDAL
    {

        internal static ResultadoInfo<AutoFacturacionInfo> ObtenerAutoFacturacion(DataSet ds)
        {
            ResultadoInfo<AutoFacturacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var list = (from info in dt.AsEnumerable()
                         select new AutoFacturacionInfo
                         {
                             OrganizacionId = info.Field<int>("OrganizacionId"),
                             ProveedorId = info.Field<int>("ProveedorId"),
                             Organizacion = info.Field<string>("Organizacion"),
                             FechaCompra = info.Field<DateTime>("FechaCompra"),
                             FormasPago = info.Field<string>("FormaPago"),
                             Factura = info.Field<string>("Factura"),
                             Proveedor = info.Field<string>("Proveedor"),
                             FolioCompra = info.Field<int>("FolioCompra"),
                             Folio = info.Field<int>("PagoId"),
                             ProductoCabezas = info.Field<int>("ProductoCabezas"),
                             Importe = info.Field<decimal>("Cantidad"),
                             TipoCompra = info.Field<string>("TipoCompra"),
                             EstatusId = info.Field<int>("Estatus")
                         }).ToList();

                resultado = new ResultadoInfo<AutoFacturacionInfo>
                {
                    Lista = list,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static int ObtenerFolio(DataSet ds)
        {
            var result = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                result = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        internal static List<AutoFacturacionInfo> ObtenerImagenes(DataSet ds)
        {
            var resultado = new List<AutoFacturacionInfo>();
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];                
                foreach (DataRow dr in dt.Rows)
                {
                    var info = new AutoFacturacionInfo();
                    info.ImgDocmento = (byte[])dr["Imagen"];
                    info.OrganizacionId = Convert.ToInt32(dr["OrganizacionId"].ToString());
                    info.Organizacion = dr["Organizacion"].ToString();
                    info.FolioCompra = Convert.ToInt32(dr["FolioCompra"].ToString());
                    resultado.Add(info);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
