using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapFleteInternoDetalleDAL
    {
        /// <summary>
        /// Obtiene un listado de flete interno detalle por flete interno id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FleteInternoDetalleInfo> ObtenerPorFleteInternoId(DataSet ds)
        {
            List<FleteInternoDetalleInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new FleteInternoDetalleInfo
                         {
                             FleteInternoDetalleId = info.Field<int>("FleteInternoDetalleID"),
                             FleteInternoId = info.Field<int>("FleteInternoID"),
                             Proveedor = new ProveedorInfo(){ProveedorID = info.Field<int>("ProveedorID"), Descripcion = info.Field<string>("Descripcion"), CodigoSAP = info.Field<string>("CodigoSAP")},
                             MermaPermitida = info.Field<decimal>("MermaPermitida"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Guardado = true,
                             TipoTarifa = info.Field<string>("TipoTarifa").ToString()
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}
