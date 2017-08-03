using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapFleteInternoCostoDAL
    {
        /// <summary>
        /// Obtiene un listado de flete interno costo por flete interno detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FleteInternoCostoInfo> ObtenerPorFleteInternoDetalleId(DataSet ds)
        {
            List<FleteInternoCostoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new FleteInternoCostoInfo
                         {
                             FleteInternoCostoId = info.Field<int>("FleteInternoCostoID"),
                             FleteInternoDetalleId = info.Field<int>("FleteInternoDetalleID"),
                             Costo = new CostoInfo(){CostoID = info.Field<int>("CostoID"), Descripcion = info.Field<string>("Descripcion"), 
                                 ClaveContable = info.Field<string>("ClaveContable"), 
                                 TipoCosto = new TipoCostoInfo()
                             {   TipoCostoID = info.Field<int>("TipoCostoID")}
                             },
                             Tarifa = info.Field<decimal>("Tarifa"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Guardado = true
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
