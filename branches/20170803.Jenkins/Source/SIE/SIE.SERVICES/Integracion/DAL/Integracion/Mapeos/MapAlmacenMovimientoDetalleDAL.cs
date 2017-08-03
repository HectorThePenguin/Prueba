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
    internal class MapAlmacenMovimientoDetalleDAL
    {
        /// <summary>
        /// Obtiene un registro por contratoid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoDetalle ObtenerPorContratoId(DataSet ds)
        {
            AlmacenMovimientoDetalle lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoDetalle
                         {
                             AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID"),
                             ContratoId = info["ContratoID"] == DBNull.Value ? 0 : info.Field<int>("ContratoID"),
                             Piezas = info.Field<int>("Piezas"),
                             TratamientoID = info["TratamientoID"] == DBNull.Value ? 0 : info.Field<int>("TratamientoID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Precio = info.Field<decimal>("Precio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene una lista de almacenmovimientodetalle por loteid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorLoteId(DataSet ds)
        {
            List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaAlmacenMovimientoDetalle = (from info in dt.AsEnumerable()
                                                 select new AlmacenMovimientoDetalle
                                                 {
                                                     AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                     AlmacenInventarioLoteId = info["AlmacenInventarioLoteID"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteID"),
                                                     ContratoId = info["ContratoID"] == DBNull.Value ? 0 : info.Field<int>("ContratoID"),
                                                     Piezas = info.Field<int>("Piezas"),
                                                     TratamientoID = info["TratamientoID"] == DBNull.Value ? 0 : info.Field<int>("TratamientoID"),
                                                     ProductoID = info.Field<int>("ProductoID"),
                                                     Precio = info.Field<decimal>("Precio"),
                                                     Cantidad = info.Field<decimal>("Cantidad"),
                                                     Importe = info.Field<decimal>("Importe"),
                                                     FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                                     UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAlmacenMovimientoDetalle;
        }

        /// <summary>
        /// Obtiene una entidad de Almacen Movimiento Detalle por 
        /// Almacen Movimiento ID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoDetalle ObtenerAlmacenMovimientoDetallePorAlmacenMovimientoID(DataSet ds)
        {
            AlmacenMovimientoDetalle almacenMovimientoDetalle;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                almacenMovimientoDetalle = (from info in dt.AsEnumerable()
                                            select new AlmacenMovimientoDetalle
                                                       {
                                                           AlmacenMovimientoDetalleID =
                                                               info.Field<long>("AlmacenMovimientoDetalleID"),
                                                           AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                           AlmacenInventarioLoteId =
                                                               info.Field<int?>("AlmacenInventarioLoteID") ?? 0,
                                                           ContratoId = info.Field<int?>("ContratoID") ?? 0,
                                                           Piezas = info.Field<int>("Piezas"),
                                                           TratamientoID = info.Field<int?>("TratamientoID") ?? 0,
                                                           ProductoID = info.Field<int>("ProductoID"),
                                                           Precio = info.Field<decimal>("Precio"),
                                                           Cantidad = info.Field<decimal>("Cantidad"),
                                                           Importe = info.Field<decimal>("Importe"),
                                                           Producto = new ProductoInfo
                                                                          {
                                                                              ProductoId = info.Field<int>("ProductoID"),
                                                                              Descripcion =
                                                                                  info.Field<string>("Producto"),
                                                                          }
                                                       }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return almacenMovimientoDetalle;
        }

        /// <summary>
        /// Obtiene una lista con los movimientos entregados
        /// a planta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetalleEntregadosPlanta(DataSet ds)
        {
            List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaAlmacenMovimientoDetalle = (from info in dt.AsEnumerable()
                                                 select new AlmacenMovimientoDetalle
                                                 {
                                                     AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                                     AlmacenInventarioLoteId = info.Field<int?>("AlmacenInventarioLoteID") ?? 0,
                                                     ProductoID = info.Field<int>("ProductoID"),
                                                     Precio = info.Field<decimal>("Precio"),
                                                     Cantidad = info.Field<decimal>("Cantidad"),
                                                     Importe = info.Field<decimal>("Importe"),
                                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAlmacenMovimientoDetalle;
        }
    }
}
