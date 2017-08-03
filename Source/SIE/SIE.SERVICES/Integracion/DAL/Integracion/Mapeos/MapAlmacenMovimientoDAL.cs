using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAlmacenMovimientoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoInfo ObtenerPorId(DataSet ds)
        {
            AlmacenMovimientoInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new AlmacenMovimientoInfo
                                 {
                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                     AlmacenID = info.Field<int>("AlmacenID"),
                                     TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                     ProveedorId = info["ProveedorID"] == DBNull.Value ? 0 : info.Field<int>("ProveedorID"),
                                     FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                     Observaciones = info.Field<string>("Observaciones"),
                                     FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                     Status = info.Field<int>("Status"),
                                     AnimalMovimientoID = info["AnimalMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AnimalMovimientoID"),
                                     FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                     UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con los Movimientos por almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContenedorAlmacenMovimientoCierreDia>  ObtenerMovimientosInventario(DataSet ds)
        {
            List<ContenedorAlmacenMovimientoCierreDia> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ContenedorAlmacenMovimientoCierreDia
                                     {
                                         Almacen = new AlmacenInfo
                                                       {
                                                           AlmacenID = info.Field<int>("AlmacenID"),
                                                           Descripcion = info.Field<string>("Almacen"),
                                                           Organizacion = new OrganizacionInfo
                                                                              {
                                                                                  OrganizacionID =
                                                                                      info.Field<int>("OrganizacionID")
                                                                              }
                                                       },
                                         AlmacenMovimiento = new AlmacenMovimientoInfo
                                                                 {
                                                                     AlmacenMovimientoID =
                                                                         info.Field<long>("AlmacenMovimientoID"),
                                                                     FolioMovimiento =
                                                                     info.Field<long>("FolioMovimiento"),
                                                                     TipoMovimientoID =
                                                                         info.Field<int>("TipoMovimientoID"),
                                                                     FechaMovimiento =
                                                                         info.Field<DateTime>("FechaMovimiento"),
                                                                     Observaciones = info.Field<string>("Observaciones"),
                                                                 },
                                         AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle
                                                                        {
                                                                            AlmacenMovimientoDetalleID =
                                                                                info.Field<long>(
                                                                                    "AlmacenMovimientoDetalleID"),
                                                                            Cantidad = info.Field<decimal>("Cantidad"),
                                                                            Importe = info.Field<decimal>("Importe"),
                                                                            Piezas = info.Field<int>("Piezas"),
                                                                            Precio = info.Field<decimal>("Precio"),
                                                                            Tratamiento = new TratamientoInfo
                                                                                              {
                                                                                                  TratamientoID =
                                                                                                      info.Field<int>(
                                                                                                          "TratamientoID"),
                                                                                                  CodigoTratamiento =
                                                                                                      info.Field<int>(
                                                                                                          "CodigoTratamiento"),
                                                                                                  TipoTratamientoInfo =
                                                                                                      new TipoTratamientoInfo
                                                                                                          {
                                                                                                              TipoTratamientoID
                                                                                                                  =
                                                                                                                  info.
                                                                                                                  Field
                                                                                                                  <int>(
                                                                                                                      "TipoTratamientoID"),
                                                                                                              Descripcion
                                                                                                                  =
                                                                                                                  info.
                                                                                                                  Field
                                                                                                                  <
                                                                                                                  string
                                                                                                                  >(
                                                                                                                      "TipoTratamiento")
                                                                                                          }
                                                                                              }
                                                                        },
                                         Producto = new ProductoInfo
                                                        {
                                                            ProductoId = info.Field<int>("ProductoID"),
                                                            Descripcion = info.Field<string>("Producto"),
                                                            SubFamilia = new SubFamiliaInfo
                                                                             {
                                                                                 SubFamiliaID =
                                                                                     info.Field<int>("SubFamiliaID"),
                                                                                 Descripcion =
                                                                                     info.Field<string>("SubFamilia"),
                                                                                 Familia = new FamiliaInfo
                                                                                               {
                                                                                                   FamiliaID =
                                                                                                       info.Field<int>(
                                                                                                           "FamiliaID"),
                                                                                                   Descripcion =
                                                                                                       info.Field
                                                                                                       <string>(
                                                                                                           "Familia")
                                                                                               }
                                                                             }
                                                        }
                                     }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<MovimientosAutorizarCierreDiaPAModel> ObtenerPorMovimientosPendientesAutorizar(DataSet ds)
        {
            List<MovimientosAutorizarCierreDiaPAModel> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new MovimientosAutorizarCierreDiaPAModel
                                 {
                                     ProductoID = info.Field<int>("ProductoID"),
                                     Producto = info.Field<string>("Producto"),
                                     FolioMovimiento = info.Field<int>("FolioMovimiento"),
                                     FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                     Observaciones = info.Field<string>("Observaciones"),
                                     ManejaLote = info.Field<bool>("ManejaLote"),
                                     AlmacenInventarioLoteID = info.Field<int>("AlmacenInventarioLoteID"),
                                     Lote = info.Field<int>("Lote"),
                                     TamanioLote = info.Field<int>("TamanioLote"),
                                     InventarioTeorico = info.Field<int>("InventarioTeorico"),
                                     InventarioFisico = info.Field<int>("InventarioFisico"),
                                     CostoUnitario = info.Field<decimal>("CostoUnitario")
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un Almacen Movimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoInfo ObtenerMovimientoPorClaveDetalle(DataSet ds)
        {
            AlmacenMovimientoInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new AlmacenMovimientoInfo
                                 {
                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                     AlmacenID = info.Field<int>("AlmacenID"),
                                     TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                     FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                     FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                     Observaciones = info.Field<string>("Observaciones"),
                                     Status = info.Field<int>("Status"),
                                     AnimalMovimientoID = info.Field<long?>("AnimalMovimientoID") ?? 0,
                                     ProveedorId = info.Field<int?>("ProveedorID") ?? 0,
                                 }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoInfo ObtenerPorIDCompleto(DataSet ds)
        {
            AlmacenMovimientoInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new AlmacenMovimientoInfo
                                 {
                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                     AlmacenID = info.Field<int>("AlmacenID"),
                                     Almacen = new AlmacenInfo
                                         {
                                             AlmacenID = info.Field<int>("AlmacenID"),
                                             TipoAlmacenID = info.Field<int>("TipoAlmacenID")
                                         },
                                     TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                     ProveedorId = info.Field<int?>("ProveedorID") != null ? info.Field<int>("ProveedorID") : 0,
                                     FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                     Observaciones = info.Field<string>("Observaciones"),
                                     FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                     Status = info.Field<int>("Status"),
                                     AnimalMovimientoID = info.Field<int?>("AnimalMovimientoID") != null ? info.Field<int>("AnimalMovimientoID") : 0,
                                     ListaAlmacenMovimientoDetalle = (from detalle in dtDetalle.AsEnumerable()
                                                                      select new AlmacenMovimientoDetalle
                                                                          {
                                                                              AlmacenMovimientoDetalleID = detalle.Field<long>("AlmacenMovimientoDetalleID"),
                                                                              AlmacenMovimientoID = detalle.Field<long>("AlmacenMovimientoID"),
                                                                              AlmacenInventarioLoteId = detalle.Field<int?>("AlmacenInventarioLoteID") != null ? detalle.Field<int>("AlmacenInventarioLoteID") : 0,
                                                                              ContratoId = detalle.Field<int?>("ContratoID") != null ? detalle.Field<int>("ContratoID") : 0,
                                                                              Piezas = detalle.Field<int>("Piezas"),
                                                                              TratamientoID = detalle.Field<int?>("TratamientoID") != null ? detalle.Field<int>("TratamientoID") : 0,
                                                                              Producto = new ProductoInfo
                                                                                  {
                                                                                      ProductoId = detalle.Field<int>("ProductoID"),
                                                                                      Descripcion = detalle.Field<string>("Producto"),
                                                                                      SubFamilia = new SubFamiliaInfo
                                                                                          {
                                                                                              SubFamiliaID = detalle.Field<int>("SubFamiliaID"),
                                                                                              Descripcion = detalle.Field<string>("SubFamilia")
                                                                                          },
                                                                                      Familia = new FamiliaInfo
                                                                                          {
                                                                                              FamiliaID = detalle.Field<int>("FamiliaID"),
                                                                                              Descripcion = detalle.Field<string>("Familia")
                                                                                          }
                                                                                  },
                                                                              Precio = detalle.Field<decimal>("Precio"),
                                                                              Cantidad = detalle.Field<decimal>("Cantidad"),
                                                                              Importe = detalle.Field<decimal>("Importe"),
                                                                          }).ToList()
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con los Movimientos por almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventarioFiltros(DataSet ds)
        {
            List<ContenedorAlmacenMovimientoCierreDia> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ContenedorAlmacenMovimientoCierreDia
                                 {
                                     Almacen = new AlmacenInfo
                                     {
                                         AlmacenID = info.Field<int>("AlmacenID"),
                                         Descripcion = info.Field<string>("Almacen"),
                                         Organizacion = new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID")
                                         }
                                     },
                                     AlmacenMovimiento = new AlmacenMovimientoInfo
                                     {
                                         AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                         TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                         FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                         Observaciones = info.Field<string>("Observaciones"),
                                     },
                                     AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle
                                     {
                                         AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                         AlmacenInventarioLoteId = info.Field<int?>("AlmacenInventarioLoteID") != null ? info.Field<int>("AlmacenInventarioLoteID") : 0,
                                         Cantidad = info.Field<decimal>("Cantidad"),
                                         Importe = info.Field<decimal>("Importe"),
                                         Piezas = info.Field<int>("Piezas"),
                                         Precio = info.Field<decimal>("Precio"),
                                       
                                     },
                                     Producto = new ProductoInfo
                                     {
                                         ProductoId = info.Field<int>("ProductoID"),
                                         Descripcion = info.Field<string>("Producto"),
                                         SubFamilia = new SubFamiliaInfo
                                         {
                                             SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                             Descripcion = info.Field<string>("SubFamilia"),
                                             Familia = new FamiliaInfo
                                             {
                                                 FamiliaID = info.Field<int>("FamiliaID"),
                                                 Descripcion = info.Field<string>("Familia")
                                             }
                                         }
                                     }
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un mapeo para AlmacenMovimientoSubProductoModel
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<AlmacenMovimientoSubProductosModel> ObtenerMapeoAlmacenMovimientoSubProductos()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AlmacenMovimientoSubProductosModel> mapeoAlmacenMovimientoSubProductos =
                    MapBuilder<AlmacenMovimientoSubProductosModel>.MapNoProperties();
                mapeoAlmacenMovimientoSubProductos.Map(x => x.AlmacenID).ToColumn("AlmacenID");
                mapeoAlmacenMovimientoSubProductos.Map(x => x.AlmacenMovimientoID).ToColumn("AlmacenMovimientoID");
                mapeoAlmacenMovimientoSubProductos.Map(x => x.Cantidad).ToColumn("Cantidad");
                mapeoAlmacenMovimientoSubProductos.Map(x => x.FechaMovimiento).ToColumn("FechaMovimiento");
                mapeoAlmacenMovimientoSubProductos.Map(x => x.Importe).ToColumn("Importe");
                mapeoAlmacenMovimientoSubProductos.Map(x => x.Precio).ToColumn("Precio");
                mapeoAlmacenMovimientoSubProductos.Map(x => x.ProductoID).ToColumn("ProductoID");
                return mapeoAlmacenMovimientoSubProductos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenMovimientoInfo> ObtenerMovimientosPorContrato(DataSet ds)
        {
            List<AlmacenMovimientoInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new AlmacenMovimientoInfo
                                 {
                                     AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                                     AlmacenID = info.Field<int>("AlmacenID"),
                                     Almacen = new AlmacenInfo
                                     {
                                         AlmacenID = info.Field<int>("AlmacenID"),
                                         Descripcion = info.Field<string>("Almacen"),
                                         TipoAlmacen =  new TipoAlmacenInfo
                                                            {
                                                                TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                                                Descripcion = info.Field<string>("TipoAlmacen")
                                                            }
                                     },
                                     TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                     ProveedorId = info.Field<int?>("ProveedorID") != null ? info.Field<int>("ProveedorID") : 0,
                                     FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                     Observaciones = info.Field<string>("Observaciones"),
                                     FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                     Status = info.Field<int>("Status"),
                                     AnimalMovimientoID = info.Field<int?>("AnimalMovimientoID") != null ? info.Field<int>("AnimalMovimientoID") : 0,
                                     ListaAlmacenMovimientoDetalle = (from detalle in dtDetalle.AsEnumerable()
                                                                      select new AlmacenMovimientoDetalle
                                                                      {
                                                                          AlmacenMovimientoDetalleID = detalle.Field<long>("AlmacenMovimientoDetalleID"),
                                                                          AlmacenMovimientoID = detalle.Field<long>("AlmacenMovimientoID"),
                                                                          AlmacenInventarioLoteId = detalle.Field<int?>("AlmacenInventarioLoteID") != null ? detalle.Field<int>("AlmacenInventarioLoteID") : 0,
                                                                          ContratoId = detalle.Field<int?>("ContratoID") != null ? detalle.Field<int>("ContratoID") : 0,
                                                                          Piezas = detalle.Field<int>("Piezas"),
                                                                          TratamientoID = detalle.Field<int?>("TratamientoID") != null ? detalle.Field<int>("TratamientoID") : 0,
                                                                          Producto = new ProductoInfo
                                                                          {
                                                                              ProductoId = detalle.Field<int>("ProductoID"),
                                                                              Descripcion = detalle.Field<string>("Producto"),
                                                                              SubFamilia = new SubFamiliaInfo
                                                                              {
                                                                                  SubFamiliaID = detalle.Field<int>("SubFamiliaID"),
                                                                                  Descripcion = detalle.Field<string>("SubFamilia")
                                                                              },
                                                                              Familia = new FamiliaInfo
                                                                              {
                                                                                  FamiliaID = detalle.Field<int>("FamiliaID"),
                                                                                  Descripcion = detalle.Field<string>("Familia")
                                                                              }
                                                                          },
                                                                          Precio = detalle.Field<decimal>("Precio"),
                                                                          Cantidad = detalle.Field<decimal>("Cantidad"),
                                                                          Importe = detalle.Field<decimal>("Importe"),
                                                                      }).ToList()
                                 }).ToList();
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
