using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAlmacenDAL
    {
        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInfo ObtenerPorID(DataSet ds)
        {
            AlmacenInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                                                {
                                                  OrganizacionID  = info.Field<int>("OrganizacionID"),
                                                  },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen = new TipoAlmacenInfo
                                               {
                                                 TipoAlmacenID  = info.Field<int>("TipoAlmacenID"),
                                               } ,
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioInfo ObtenerCantidadProductoEnInventario(DataSet ds)
        {
            AlmacenInventarioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInventarioInfo
                         {
                             AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Minimo = info.Field<int>("Minimo"),
                             Maximo = info.Field<int>("Maximo"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
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
        /// Obtienen los datos del almacen Movimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoInfo ObtenerAlmacenMovimiento(DataSet ds)
        {
            AlmacenMovimientoInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoInfo
                         {
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             ProveedorId = info["ProveedorId"] == DBNull.Value ? 0 : info.Field<int>("ProveedorId"),
                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Status = info.Field<int>("Status"),
                             AnimalMovimientoID = info["AnimalMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AnimalMovimientoID"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                             FechaModificacion = (info["FechaModificacion"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaModificacion")),
                             UsuarioModificacionID = (info["UsuarioModificacionID"] == DBNull.Value ? 0 : info.Field<int>("UsuarioModificacionID"))
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
        /// Se obtiene el costo total del movimiento de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static decimal ObtenerGuardarAlmacenMovimientoDetalle(DataSet ds)
        {
            decimal totalCosto = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                
                foreach (DataRow dr in dt.Rows)
                {
                    totalCosto = Convert.ToDecimal(dr["ImporteCosto"]);
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return totalCosto;

        }

        /// <summary>
        /// Validar que no queden ajustes pendientes por aplicar para el almacen(Diferencias de inventario)
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static bool ObtenerExistenAjustesPendientesParaAlmacen(DataSet ds)
        {
            int ajustesPendientes = 0;
            var resp = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    ajustesPendientes = Convert.ToInt32(dr["AjustesPendientes"]);
                }

                if (ajustesPendientes > 0)
                {
                    resp = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<AlmacenInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                             {
                                 AlmacenID = info.Field<int>("AlmacenID"),
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                                        Descripcion = info.Field<string>("Organizacion"),
                                                    },
                                 CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                 TipoAlmacen =
                                     new TipoAlmacenInfo
                                         {
                                             TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                             Descripcion = info.Field<string>("TipoAlmacen")
                                         },
                                 CuentaInventario = info.Field<string>("CuentaInventario"),
                                 CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                                 CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Proveedor = new ProveedorInfo
                                     {
                                         ProveedorID = info.Field<int?>("ProveedorID") != null ? info.Field<int>("ProveedorID") : 0 ,
                                         Descripcion = info.Field<string>("Proveedor"),
                                         CodigoSAP = info.Field<string>("CodigoSAP")
                                     }
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<AlmacenInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<AlmacenInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen = new TipoAlmacenInfo
                                               {
                                                   TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                                   Descripcion = info.Field<string>("TipoAlmacen")
                                               },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static AlmacenInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AlmacenInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen = new TipoAlmacenInfo
                                               {
                                                   TipoAlmacenID = info.Field<int>("TipoAlmacenID"), 
                                               },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Mapeo de obtener por organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInfo> ObtenerPorOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen = new TipoAlmacenInfo
                             {
                                 TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             },
                             TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

               
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Mapeo para ObtenerCierreAlmacenMovimientoInfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenCierreDiaInventarioInfo ObtenerCierreAlmacenMovimientoInfo(DataSet ds)
        {
            AlmacenCierreDiaInventarioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenCierreDiaInventarioInfo
                         {
                             FolioAlmacen = info.Field<int>("Valor")
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
        /// Map para obtener los productos de 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenCierreDiaInventarioInfo> ObtenerProductosAlamcen(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenCierreDiaInventarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenCierreDiaInventarioInfo
                         {
                             Almacen = new AlmacenInfo(){AlmacenID = info.Field<int>("AlmacenID")},
                             ProductoID = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             ClaveUnidad = info.Field<string>("ClaveUnidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             ImporteReal = 0,
                             CantidadReal = 0,
                         }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }



        /// <summary>
        /// Obtienen los datos del almacen Movimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<AlmacenMovimientoInfo> ObtenerListaAlmacenMovimiento(DataSet ds)
        {
            IList<AlmacenMovimientoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoInfo
                         {
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Status = info.Field<int>("Status"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                             NombreUsuarioCreacion = info.Field<string>("Nombre"),
                             NombreTipoMovimiento = info.Field<string>("Descripcion")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtienen los datos del almacen Movimiento por AlmacenMovimientoID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoInfo ObtenerAlmacenMovimientoPorID(DataSet ds)
        {
            AlmacenMovimientoInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoInfo
                         {
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             TipoMovimiento = new TipoMovimientoInfo(){TipoMovimientoID = info.Field<int>("TipoMovimientoID")},
                             TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Status = info.Field<int>("Status"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                             FechaModificacion = (info["FechaModificacion"] == DBNull.Value ? new DateTime(1492, 10, 12) : info.Field<DateTime>("FechaModificacion")),
                             UsuarioModificacionID = (info["UsuarioModificacionID"] == DBNull.Value ? 0 : info.Field<int>("UsuarioModificacionID")),
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }


        internal static List<AlmacenInventarioInfo> ObtenerProductosAlmacenInventario(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInventarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInventarioInfo
                         {
                             AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Minimo = info.Field<int>("Minimo"),
                             Maximo = info.Field<int>("Maximo"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe")
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorAlmacenID(DataSet ds)
        {
            List<AlmacenMovimientoDetalle> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoDetalle
                         {
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Precio = info.Field<decimal>("Precio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static AlmacenMovimientoDetalle ObtenerAlmacenMovimientoDetalleProducto(DataSet ds)
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
                             AlmacenInventarioLoteId = info["AlmacenInventarioLoteId"] == DBNull.Value ? 0 : info.Field<int>("AlmacenInventarioLoteId"),
                             ContratoId = info.Field<int>("ContratoId"),
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
        /// Obtiene los datos de los movimientos del almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorContrato(DataSet ds)
        {
            List<AlmacenMovimientoDetalle> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoDetalle
                         {
                             AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             AlmacenInventarioLoteId = info.Field<int?>("AlmacenInventarioLoteId") ?? 0,
                             ContratoId = info.Field<int>("ContratoId"),
                             Piezas = info.Field<int>("Piezas"),
                             TratamientoID = info["TratamientoID"] == DBNull.Value ? 0 : info.Field<int>("TratamientoID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Precio = info.Field<decimal>("Precio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Mapeo de obtener por organizacion y tipo de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInventarioInfo ObtenerAlmacenInventarioPorOrganizacionTipoAlmacenProducto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AlmacenInventarioInfo lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInventarioInfo
                         {
                             AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Minimo = info.Field<int>("Minimo"),
                             Maximo = info.Field<int>("Maximo"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Importe = info.Field<decimal>("Importe"),
                         }).First();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Mapeo de obtener por organizacion y tipo de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenesCierreDiaInventarioPAModel> ObtenerAlmacenesOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenesCierreDiaInventarioPAModel
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             Almacen = info.Field<string>("Almacen"),
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene resultado paginado por poliza
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<AlmacenInfo> ObtenerPorPaginaPoliza(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen =
                                 new TipoAlmacenInfo
                                 {
                                     TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                     Descripcion = info.Field<string>("TipoAlmacen")
                                 },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<AlmacenInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static AlmacenInfo ObtenerAlmacenPoliza(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AlmacenInfo resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen =
                                 new TipoAlmacenInfo
                                 {
                                     TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                     Descripcion = info.Field<string>("TipoAlmacen")
                                 },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).FirstOrDefault();
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<AlmacenInfo> ObtenerPorOrganizacionTipoAlmacenPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             TipoAlmacen =
                                 new TipoAlmacenInfo
                                 {
                                     TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                                     Descripcion = info.Field<string>("TipoAlmacen")
                                 },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<AlmacenInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<AlmacenInfo> ObtenerPorPaginaTipoAlmacen(DataSet ds)
        {
            ResultadoInfo<AlmacenInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<AlmacenInfo> lista = (from groupinfo in dt.AsEnumerable()
                                         select new AlmacenInfo
                                         {
                                             AlmacenID = groupinfo.Field<int>("AlmacenID"),
                                             Organizacion = new OrganizacionInfo(){OrganizacionID = groupinfo.Field<int>("OrganizacionID")},
                                             OrganizacionID = groupinfo.Field<int>("OrganizacionID"),
                                             CodigoAlmacen = groupinfo.Field<string>("CodigoAlmacen"),
                                             Descripcion = groupinfo.Field<string>("Descripcion"),
                                             TipoAlmacen = new TipoAlmacenInfo(){TipoAlmacenID = groupinfo.Field<int>("TipoAlmacenID")},
                                             TipoAlmacenID = groupinfo.Field<int>("TipoAlmacenID"),
                                             CuentaInventario = groupinfo.Field<string>("CuentaInventario"),
                                             CuentaInventarioTransito = groupinfo.Field<string>("CuentaInventarioTransito"),
                                             CuentaDiferencias = groupinfo.Field<string>("CuentaDiferencias"),
                                             Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                         }).ToList();
                resultado = new ResultadoInfo<AlmacenInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Metodo que obtiene un registro por id y tipo almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInfo ObtenerPorIdFiltroTipoAlmacen(DataSet ds)
        {
            AlmacenInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion")
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen = new TipoAlmacenInfo
                             {
                                 TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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

        internal static IList<AlmacenInfo> ObtenerPorOrganizacion(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var lista =new List<AlmacenInfo>();
                AlmacenInfo elemento;
                while (reader.Read())
                {
                    elemento =
                        new AlmacenInfo
                            {
                                AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                                Organizacion = new OrganizacionInfo
                                                   {
                                                       OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                                                   },
                                CodigoAlmacen = Convert.ToString(reader["CodigoAlmacen"]),
                                Descripcion = Convert.ToString(reader["Descripcion"]),
                                TipoAlmacen = new TipoAlmacenInfo
                                                  {
                                                      TipoAlmacenID = Convert.ToInt32(reader["TipoAlmacenID"]),
                                                  },
                                TipoAlmacenID = Convert.ToInt32(reader["TipoAlmacenID"]),
                                CuentaInventario = Convert.ToString(reader["CuentaInventario"]),
                                CuentaInventarioTransito = Convert.ToString(reader["CuentaInventarioTransito"]),
                                CuentaDiferencias = Convert.ToString(reader["CuentaDiferencias"]),
                                Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                            };
                    lista.Add(elemento);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static AlmacenInfo ObtenerPorID(IDataReader reader)
        {
            AlmacenInfo lista = null;
            try
            {
                Logger.Info();
                while (reader.Read())
                {
                    lista = new AlmacenInfo
                                {
                                    AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                                    Organizacion = new OrganizacionInfo
                                                       {
                                                           OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                                                       },
                                    CodigoAlmacen = Convert.ToString(reader["CodigoAlmacen"]),
                                    Descripcion = Convert.ToString(reader["Descripcion"]),
                                    TipoAlmacen = new TipoAlmacenInfo
                                                      {
                                                          TipoAlmacenID = Convert.ToInt32(reader["TipoAlmacenID"]),
                                                      },
                                    CuentaInventario = Convert.ToString(reader["CuentaInventario"]),
                                    CuentaInventarioTransito = Convert.ToString(reader["CuentaInventarioTransito"]),
                                    CuentaDiferencias = Convert.ToString(reader["CuentaDiferencias"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                                    UsuarioCreacionID = Convert.ToInt32(reader["UsuarioCreacionID"])
                                };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Mapeo de obtener por organizacion y tipo de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AlmacenInfo> ObtenerAlmacenesPorOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             Descripcion = info.Field<string>("Almacen"),
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtienen los datos del almacen Movimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenMovimientoInfo ObtenerAlmacenMovimientoConFecha(DataSet ds)
        {
            AlmacenMovimientoInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenMovimientoInfo
                         {
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             AlmacenID = info.Field<int>("AlmacenID"),
                             TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             ProveedorId = info["ProveedorId"] == DBNull.Value ? 0 : info.Field<int>("ProveedorId"),
                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Status = info.Field<int>("Status"),
                             AnimalMovimientoID = info["AnimalMovimientoID"] == DBNull.Value ? 0 : info.Field<long>("AnimalMovimientoID"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                             FechaModificacion = (info["FechaModificacion"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaModificacion")),
                             UsuarioModificacionID = (info["UsuarioModificacionID"] == DBNull.Value ? 0 : info.Field<int>("UsuarioModificacionID"))
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
        /// Metodo que obtiene un registro por id y tipo almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AlmacenInfo ObtenerPorIDOrganizacion(DataSet ds)
        {
            AlmacenInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                             },
                             CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoAlmacen = new TipoAlmacenInfo
                             {
                                 TipoAlmacenID = info.Field<int>("TipoAlmacenID"),
                             },
                             CuentaInventario = info.Field<string>("CuentaInventario"),
                             CuentaInventarioTransito = info.Field<string>("CuentaInventarioTransito"),
                             CuentaDiferencias = info.Field<string>("CuentaDiferencias"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        /// Mapeo de obtener identificador del almacen y descripcion
        /// </summary>
        /// <param name="ds">DataSet con Informacion</param>
        /// <returns></returns>
        internal static List<AlmacenInfo> ObtenerAlmacenesPorProducto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             Descripcion = info.Field<string>("Descripcion")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }

}
