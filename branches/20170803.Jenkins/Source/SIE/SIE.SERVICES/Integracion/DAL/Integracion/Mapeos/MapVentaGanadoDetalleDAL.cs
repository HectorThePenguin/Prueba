using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapVentaGanadoDetalleDAL
    {
        internal static List<VentaGanadoDetalleInfo> ObtenerVentaGanadoDetallePorID(DataSet ds)
        {
            try
            {
                List<VentaGanadoDetalleInfo> lista = new List<VentaGanadoDetalleInfo>();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                VentaGanadoDetalleInfo item;

                foreach (DataRow info in dt.Rows)
                {
                    item = new VentaGanadoDetalleInfo();
                    item.VentaGanadoDetalleID = info.Field<int>("VentaGanadoDetalleID");
                    item.VentaGanadoID = info.Field<int>("VentaGanadoID");
                    item.FotoVenta = info.Field<string>("FotoVenta");
                    item.CausaPrecioID = info.Field<int>("CausaPrecioID");
                    item.Arete = info.Field<string>("Arete");
                    item.AreteMetalico = info.Field<string>("AreteMetalico");
                    item.Activo = info.Field<bool>("Activo");
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Mapear el objeto de venta ganado detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static VentaGanadoDetalleInfo ObtenerVentaGanadoDetalle(DataSet ds)
        {
            try
            {
                VentaGanadoDetalleInfo ventaDetalle = null;
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow info in dt.Rows)
                {
                    ventaDetalle = new VentaGanadoDetalleInfo
                    {
                        VentaGanadoDetalleID = info.Field<int>("VentaGanadoDetalleID"),
                        VentaGanadoID = info.Field<int>("VentaGanadoID"),
                        FotoVenta = (info["FotoVenta"] == DBNull.Value ? "" : info.Field<string>("FotoVenta")),
                        CausaPrecioID = info.Field<int>("CausaPrecioID"),
                        Animal = new AnimalInfo
                        {
                            AnimalID = info.Field<long>("AnimalID")
                        },
                        Activo = info.Field<bool>("Activo")
                    };
                }
                return ventaDetalle;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
