using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteResumenInventarioDAL
    {
        public static IList<ReporteResumenInventarioInfo> Generar(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ReporteResumenInventarioInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ReporteResumenInventarioInfo
                         {
                             CodigoFamilia = info.Field<int>("FamiliaId"),
                             Familia = info.Field<string>("Familia").Trim(),
                             CodigoSubFamilia = info.Field<int>("SubFamiliaID"),
                             SubFamilia = info.Field<string>("SubFamilia").Trim(),
                             Codigo = info.Field<int>("ProductoId"),
                             Producto = info.Field<string>("Producto").Trim(),
                             Almacen = info.Field<int>("AlmacenId"),
                             TipoAlmacen = info.Field<string>("TipoAlmacen").Trim(),
                             CantidadInventario = info.Field<decimal>("CantidadInventario"),
                             CantidadInventarioInicial =  info.Field<decimal>("InventarioInicial"),
                             ImporteMovimiento = info.Field<decimal>("ImporteInventario"),
                             ImporteInventarioInicial = info.Field<decimal>("ImporteInventarioInicial") ,
                             //TipoMovimiento = info.Field<string>("TipoMovimiento").Trim(),
                             //TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             UnidadEntrada = info.Field<decimal>("UnidadEntrada"),
                             UnidadSalida = info.Field<decimal>("UnidadSalida"),
                             ImporteEntrada = info.Field<decimal>("ImporteEntrada"),
                             ImporteSalida = info.Field<decimal>("ImporteSalida")
                         }).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
