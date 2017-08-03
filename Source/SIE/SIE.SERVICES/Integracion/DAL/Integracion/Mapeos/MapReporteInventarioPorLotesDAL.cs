using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteInventarioPorLotesDAL
    {
        public static IList<ReporteInventarioPorLotesInfo> Generar(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ReporteInventarioPorLotesInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ReporteInventarioPorLotesInfo
                         {

                             AlmacenId = info.Field<int>("AlmacenId"),
                             Codigoalmacen = info.Field<string>("CodigoAlmacen").Trim(),
                             Lote = info.Field<int>("Lote"),
                             FechaInicio = info.Field<DateTime>("FechaInicioLote"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             CostoPromedio = info.Field<decimal>("CostoPromedio"),
                             CantidadInventario = info.Field<decimal>("CantidadInventario"),
                             FamiliaId = info.Field<int>("FamiliaID"),
                             Familia = info.Field<string>("Familia").Trim(),
                             TipoAlmacen = info.Field<string>("TipoAlmacen").Trim(),
                             ProductoId = info.Field<int>("ProductoID"),
                             Producto = info.Field<string>("Producto").Trim(),
                             UnidadEntrada = info.Field<Decimal>("UnidadEntrada"),
                             UnidadSalida = info.Field<decimal>("UnidadSalida"),
                             TipoAlmancenId = info.Field<int>("TipoAlmacenId"),
                             TamanioLote = info.Field<decimal>("TAMLote")
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
