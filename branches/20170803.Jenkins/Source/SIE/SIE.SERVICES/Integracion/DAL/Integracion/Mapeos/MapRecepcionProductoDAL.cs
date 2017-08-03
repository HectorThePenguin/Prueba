using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapRecepcionProductoDAL
    {
        internal static RecepcionProductoInfo ObtenerRecepcionProducto(DataSet ds)
        {
            RecepcionProductoInfo recepcionProducto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                recepcionProducto = (from info in dt.AsEnumerable()
                              select new RecepcionProductoInfo()
                              {
                                  RecepcionProductoId = info.Field<int>("RecepcionProductoID"),
                                  Almacen = new AlmacenInfo(){AlmacenID = info.Field<int>("AlmacenID")},
                                  FolioRecepcion = info.Field<int>("FolioRecepcion"),
                                  FolioOrdenCompra = info.Field<string>("FolioOrdenCompra"),
                                  FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                  Proveedor = new ProveedorInfo(){ProveedorID = info.Field<int>("ProveedorID")},
                                  FechaRecepcion = info.Field<DateTime>("FechaRecepcion"),
                                  AlmacenMovimientoId = info.Field<long>("AlmacenMovimientoID"),
                                  Factura = info.Field<string>("Factura")
                              }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return recepcionProducto;
        }

        internal static RecepcionProductoInfo ObtenerRecepcionProductoVista(DataSet ds)
        {
            RecepcionProductoInfo recepcion;
            List<RecepcionProductoDetalleInfo> recepcionProducto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                recepcionProducto = (from info in dt.AsEnumerable()
                                     select new RecepcionProductoDetalleInfo()
                                     {
                                         Producto = new ProductoInfo() { ProductoDescripcion = info.Field<string>("Producto"), DescripcionUnidad = info.Field<string>("Unidad") },
                                         Cantidad = info.Field<decimal>("Cantidad") ,
                                         PrecioPromedio =  info.Field<decimal>("CostoUnitario"),
                                         Importe = info.Field<decimal>("Importe")
                                     }).ToList();

                recepcion = (from info in dt.AsEnumerable()
                             select new RecepcionProductoInfo()
                             {
                                 FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                 FolioOrdenCompra = info.Field<string>("FolioSolicitud"),
                                 Proveedor = new ProveedorInfo() { Descripcion = info.Field<string>("Proveedor") },
                                 ListaRecepcionProductoDetalle = recepcionProducto
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return recepcion;
        }

        /// <summary>
        /// Obtiene una recepcion producto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RecepcionProductoInfo ObtenerRecepcionPorFolioOrganizacion(DataSet ds)
        {
            RecepcionProductoInfo recepcionProducto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                recepcionProducto = (from info in dt.AsEnumerable()
                                     select new RecepcionProductoInfo
                                                {
                                                    RecepcionProductoId = info.Field<int>("RecepcionProductoID"),
                                                    Almacen =
                                                        new AlmacenInfo
                                                            {
                                                                AlmacenID = info.Field<int>("AlmacenID"),
                                                                Descripcion = info.Field<string>("Almacen"),
                                                                CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                                                                CuentaInventario =
                                                                    info.Field<string>("CuentaInventario"),
                                                                CuentaDiferencias =
                                                                    info.Field<string>("CuentaDiferencias"),
                                                                CuentaInventarioTransito =
                                                                    info.Field<string>("CuentaInventarioTransito")
                                                            },
                                                    FolioRecepcion = info.Field<int>("FolioRecepcion"),
                                                    FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                                    Proveedor =
                                                        new ProveedorInfo
                                                            {
                                                                ProveedorID = info.Field<int>("ProveedorID"),
                                                                Descripcion = info.Field<string>("Proveedor"),
                                                                CodigoSAP = info.Field<string>("CodigoSAP")
                                                            },
                                                    FechaRecepcion = info.Field<DateTime>("FechaRecepcion"),
                                                    AlmacenMovimientoId = info.Field<long>("AlmacenMovimientoID"),
                                                    Factura = info.Field<string>("Factura")
                                                }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return recepcionProducto;
        }

        internal static ResultadoInfo<RecepcionProductoInfo> ObtenerRecepcionPorFolioOrganizacionPaginado(DataSet ds)
        {
            ResultadoInfo<RecepcionProductoInfo> recepcionProducto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<RecepcionProductoInfo> lista = (from info in dt.AsEnumerable()
                                                      select new RecepcionProductoInfo
                                                                 {
                                                                     RecepcionProductoId =
                                                                         info.Field<int>("RecepcionProductoID"),
                                                                     Almacen =
                                                                         new AlmacenInfo
                                                                             {
                                                                                 AlmacenID =
                                                                                     info.Field<int>("AlmacenID"),
                                                                                 Descripcion =
                                                                                     info.Field<string>("Almacen"),
                                                                                 CodigoAlmacen =
                                                                                     info.Field<string>("CodigoAlmacen"),
                                                                                 CuentaInventario =
                                                                                     info.Field<string>(
                                                                                         "CuentaInventario"),
                                                                                 CuentaDiferencias =
                                                                                     info.Field<string>(
                                                                                         "CuentaDiferencias"),
                                                                                 CuentaInventarioTransito =
                                                                                     info.Field<string>(
                                                                                         "CuentaInventarioTransito")
                                                                             },
                                                                     FolioRecepcion = info.Field<int>("FolioRecepcion"),
                                                                     FechaSolicitud =
                                                                         info.Field<DateTime>("FechaSolicitud"),
                                                                     Proveedor =
                                                                         new ProveedorInfo
                                                                             {
                                                                                 ProveedorID =
                                                                                     info.Field<int>("ProveedorID"),
                                                                                 Descripcion =
                                                                                     info.Field<string>("Proveedor"),
                                                                                 CodigoSAP =
                                                                                     info.Field<string>("CodigoSAP")
                                                                             },
                                                                     FechaRecepcion =
                                                                         info.Field<DateTime>("FechaRecepcion"),
                                                                     AlmacenMovimientoId =
                                                                         info.Field<long>("AlmacenMovimientoID"),
                                                                     Factura = info.Field<string>("Factura")
                                                                 }).ToList();
                recepcionProducto = new ResultadoInfo<RecepcionProductoInfo>
                                        {
                                            Lista = lista,
                                            TotalRegistros =
                                                Convert.ToInt32(ds.Tables[ConstantesDAL.DtDetalle].Rows[0]["TotalReg"])
                                        };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return recepcionProducto;
        }

        /// <summary>
        /// Obtiene una lista de Recepcion Producto
        /// con su detalle
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<RecepcionProductoInfo> ObtenerRecepcionProductoConciliacionPorAlmacenMovimientoXML(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var recepcionProductos = new List<RecepcionProductoInfo>();
                RecepcionProductoInfo recepcionProducto;
                while (reader.Read())
                {
                    recepcionProducto = new RecepcionProductoInfo
                                            {
                                                Almacen = new AlmacenInfo
                                                              {
                                                                  AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                                                                  Organizacion = new OrganizacionInfo
                                                                                     {
                                                                                         OrganizacionID = Convert.ToInt32(reader["OrganizacionID"])
                                                                                     }
                                                              },
                                                RecepcionProductoId = Convert.ToInt32(reader["RecepcionProductoID"]),
                                                AlmacenMovimientoId = Convert.ToInt64(reader["AlmacenMovimientoID"]),
                                                Factura = Convert.ToString(reader["Factura"]),
                                                FechaRecepcion = Convert.ToDateTime(reader["FechaRecepcion"]),
                                                FolioRecepcion = Convert.ToInt32(reader["FolioRecepcion"]),
                                                FolioOrdenCompra = Convert.ToString(reader["FolioOrdenCompra"]),
                                                FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"]),
                                                Proveedor = new ProveedorInfo
                                                                {
                                                                    ProveedorID = Convert.ToInt32(reader["ProveedorID"]),
                                                                    Descripcion = Convert.ToString(reader["Proveedor"]),
                                                                    CodigoSAP = Convert.ToString(reader["CodigoSAP"])
                                                                }
                                            };
                    recepcionProductos.Add(recepcionProducto);
                }
                reader.NextResult();
                var recepcionProductoDetalles = new List<RecepcionProductoDetalleInfo>();
                RecepcionProductoDetalleInfo recepcionProductoDetalle;
                while (reader.Read())
                {
                    recepcionProductoDetalle = new RecepcionProductoDetalleInfo
                                                   {
                                                       Cantidad = Convert.ToDecimal(reader["Cantidad"]),
                                                       Importe = Convert.ToDecimal(reader["Importe"]),
                                                       PrecioPromedio = Convert.ToDecimal(reader["PrecioPromedio"]),
                                                       Producto = new ProductoInfo
                                                                      {
                                                                          ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                                                          Descripcion = Convert.ToString(reader["Producto"]),
                                                                          Familia = new FamiliaInfo
                                                                                        {
                                                                                            FamiliaID = Convert.ToInt32(reader["FamiliaID"]),
                                                                                            Descripcion = Convert.ToString(reader["Familia"])
                                                                                        },
                                                                          SubFamilia = new SubFamiliaInfo
                                                                                           {
                                                                                               SubFamiliaID = Convert.ToInt32(reader["SubFamiliaID"]),
                                                                                               Descripcion = Convert.ToString(reader["SubFamilia"]),
                                                                                               Familia = new FamiliaInfo
                                                                                               {
                                                                                                   FamiliaID = Convert.ToInt32(reader["FamiliaID"]),
                                                                                                   Descripcion = Convert.ToString(reader["Familia"])
                                                                                               },
                                                                                           },
                                                                          UnidadMedicion = new UnidadMedicionInfo
                                                                                               {
                                                                                                   UnidadID = Convert.ToInt32(reader["UnidadID"])
                                                                                               }
                                                                      },
                                                       RecepcionProductoId = Convert.ToInt32(reader["RecepcionProductoID"]),
                                                       RecepcionProductoDetalleId = Convert.ToInt32(reader["RecepcionProductoDetalleID"])
                                                   };
                    recepcionProductoDetalles.Add(recepcionProductoDetalle);
                }
                recepcionProductos.ForEach(datos =>
                                               {
                                                   datos.ListaRecepcionProductoDetalle =
                                                       recepcionProductoDetalles.Where(
                                                           id => id.RecepcionProductoId == datos.RecepcionProductoId).
                                                           ToList();
                                               });
                return recepcionProductos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
