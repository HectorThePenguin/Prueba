using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapDiferenciasDeInventarioDAL
    {
        /// <summary>
        /// Obtiene los ajustes pendientes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<DiferenciasDeInventariosInfo> ObtenerDiferenciasInventario(DataSet ds)
        {
            List<DiferenciasDeInventariosInfo> ajusteDeInventarioDiferenciasInventarioInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                ajusteDeInventarioDiferenciasInventarioInfo = (from info in dt.AsEnumerable()
                                                               select new DiferenciasDeInventariosInfo
                                                               {
                                                                   AlmacenMovimiento = new AlmacenMovimientoInfo()
                                                                   {
                                                                       AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                                       FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                                                       TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                                                       Status = info.Field<int>("Status"),
                                                                       Observaciones = info.Field<string>("Observaciones")
                                                                   },
                                                                   Almacen = new AlmacenInfo()
                                                                   {
                                                                       AlmacenID = info.Field<int>("AlmacenID"),
                                                                       Descripcion = info.Field<string>("DescripcionAlmacen")
                                                                   },
                                                                   Producto = new ProductoInfo()
                                                                   {
                                                                       ProductoId = info.Field<int>("ProductoID"),
                                                                       Descripcion = info.Field<string>("ProductoDescripcion"),
                                                                       ProductoDescripcion = info.Field<string>("ProductoDescripcion")
                                                                   },
                                                                   AlmacenInventarioLote = new AlmacenInventarioLoteInfo()
                                                                   {
                                                                       AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID"),
                                                                       Lote = info.Field<int>("Lote"),
                                                                       Cantidad = info.Field<decimal>("Cantidad"),
                                                                       PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                                                                       Importe = info.Field<decimal>("Importe"),
                                                                       AlmacenInventario =  new AlmacenInventarioInfo
                                                                                                {
                                                                                                    AlmacenInventarioID = info.Field<int>("AlmacenInventarioID")
                                                                                                }
                                                                   },
                                                                   Estatus = new EstatusInfo()
                                                                   {
                                                                       EstatusId = info.Field<int>("Status"),
                                                                       Descripcion = info.Field<string>("DescripcionEstatus")
                                                                   },
                                                                   AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle()
                                                                   {
                                                                       AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                                                       Cantidad = info.Field<decimal>("Cantidad"),
                                                                       Precio = info.Field<decimal>("Precio")
                                                                   },
                                                                   Guardado = true
                                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ajusteDeInventarioDiferenciasInventarioInfo;
        }
    }
}
