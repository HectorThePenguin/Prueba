using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteInventarioMateriaPrimaDAL
    {
        public static List<ReporteInventarioMateriaPrimaInfo> ObtenerReporte(DataSet ds)
        {
            List<ReporteInventarioMateriaPrimaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteInventarioMateriaPrimaInfo
                         {
                             Producto = info.Field<string>("Producto").Trim(),
                             UnidadMedida = info.Field<string>("Unidad").Trim(),
                             Almacen = info.Field<string>("Almacen").Trim(),
                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                             Lote = info.Field<int>("Lote"),
                             CodigoMovimiento = info.Field<string>("ClaveCodigo").Trim(),
                             TipoMovimiento = info.Field<string>("TipoMovimiento").Trim(),
                             Folio = info.Field<long>("FolioMovimiento").ToString(CultureInfo.InvariantCulture),
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             Piezas = info.Field<int>("Piezas"),
                             PiezasEntrada = info.Field<int>("PiezasEntrada"),
                             PiezasSalida = info.Field<int>("PiezasSalida"),
                             Precio = info.Field<decimal>("Precio"),
                             CantidadEntrada = info["CantidadEntrada"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("CantidadEntrada"),
                             CantidadSalida = info["CantidadSalida"] == DBNull.Value ? Decimal.Zero : info.Field<decimal>("CantidadSalida"),
                             AlmacenMovimientoDetalleId = info["AlmacenMovimientoDetalleID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoDetalleID"),
                             AlmacenMovimientoID = info["AlmacenMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoID"),
                             CostoId = info["CostoID"] == DBNull.Value ? 0 : info.Field<int>("CostoID"),
                             Costo = info.Field<string>("Costo"),
                             FechaCosto = info.Field<DateTime>("FechaCosto"),
                             ImporteCosto = info.Field<decimal>("ImporteCosto"),
                             CantidadCosto = info.Field<decimal>("CantidadCosto"),
                             Cargos = info.Field<decimal>("Cargos"),
                             Abonos = info.Field<decimal>("Abonos"),
                             ExistenciaInicial = info.Field<decimal>("ExistenciaInicial"),
                             CostoInicialInventario = info.Field<decimal>("CostoInicialInventario"),
                             ExistenciaFinalMesAnterior = info.Field<decimal>("ExistenciaFinalMesAnterior"),
                             ExistenciaPiezasInicial = info.Field<int>("ExistenciaPiezasInicial"),
                             PiezasFinalMesAnterior = info.Field<int>("ExistenciaPiezasMesAnterior"),
                             AlmacenMovimientoCostoId = info.Field<int>("AlmacenMovimientoCostoID"),
                             CostoFinalMesAnterior = info.Field<decimal>("CostoFinalMesAnterior"),
                             TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             ImporteSubProductos = 0,
                             DescripcionSubProductos = "SubProductos"
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
