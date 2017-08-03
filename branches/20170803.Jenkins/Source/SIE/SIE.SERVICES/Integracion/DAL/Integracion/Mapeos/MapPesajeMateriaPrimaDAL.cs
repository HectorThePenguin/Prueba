using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPesajeMateriaPrimaDAL
    {
        /// <summary>
        /// Metodo que obtiene un PesajeMateriaPrimaInfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PesajeMateriaPrimaInfo ObtenerPorId(DataSet ds)
        {
            PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                pesajeMateriaPrimaInfo = new PesajeMateriaPrimaInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    pesajeMateriaPrimaInfo.PesajeMateriaPrimaID = Convert.ToInt32(dr["PesajeMateriaPrimaID"]);
                    pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID = Convert.ToInt32(dr["ProgramacionMateriaPrimaID"]);
                    pesajeMateriaPrimaInfo.ProveedorChoferID = dr["ProveedorChoferID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ProveedorChoferID"]);
                    pesajeMateriaPrimaInfo.Ticket = Convert.ToInt32(dr["Ticket"]);
                    pesajeMateriaPrimaInfo.CamionID = dr["CamionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CamionID"]);
                    pesajeMateriaPrimaInfo.PesoBruto = Convert.ToInt32(dr["PesoBruto"]);
                    pesajeMateriaPrimaInfo.PesoTara = Convert.ToInt32(dr["PesoTara"]);
                    pesajeMateriaPrimaInfo.Piezas = Convert.ToInt32(dr["Piezas"]);
                    pesajeMateriaPrimaInfo.TipoPesajeID = Convert.ToInt32(dr["TipoPesajeID"]);
                    pesajeMateriaPrimaInfo.UsuarioIDSurtido = Convert.ToInt32(dr["UsuarioIDSurtido"]);
                    pesajeMateriaPrimaInfo.FechaSurtido = Convert.ToDateTime(dr["FechaSurtido"]);
                    pesajeMateriaPrimaInfo.UsuarioIDRecibe = Convert.ToInt32(dr["UsuarioIDRecibe"]);
                    pesajeMateriaPrimaInfo.FechaRecibe = Convert.ToDateTime(dr["FechaRecibe"]);
                    pesajeMateriaPrimaInfo.EstatusID = Convert.ToInt32(dr["EstatusID"]);
                    pesajeMateriaPrimaInfo.Activo = Convert.ToBoolean(dr["Activo"]);
                    pesajeMateriaPrimaInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    pesajeMateriaPrimaInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                    pesajeMateriaPrimaInfo.AlmacenMovimientoOrigenId = dr["AlmacenMovimientoOrigenID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AlmacenMovimientoOrigenID"]);
                    pesajeMateriaPrimaInfo.AlmacenMovimientoDestinoId = dr["AlmacenMovimientoDestinoID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AlmacenMovimientoDestinoID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pesajeMateriaPrimaInfo;
        }

        /// <summary>
        /// Metodo que obtiene un PesajeMateriaPrimaInfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PesajeMateriaPrimaInfo ObtenerPorTicket(DataSet ds)
        {
            PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                pesajeMateriaPrimaInfo = new PesajeMateriaPrimaInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    pesajeMateriaPrimaInfo.PesajeMateriaPrimaID = Convert.ToInt32(dr["PesajeMateriaPrimaID"]);
                    pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID = Convert.ToInt32(dr["ProgramacionMateriaPrimaID"]);
                    pesajeMateriaPrimaInfo.ProveedorChoferID = dr["ProveedorChoferID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ProveedorChoferID"]);
                    pesajeMateriaPrimaInfo.Ticket = Convert.ToInt32(dr["Ticket"]);
                    pesajeMateriaPrimaInfo.CamionID = dr["CamionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CamionID"]);
                    pesajeMateriaPrimaInfo.PesoBruto = Convert.ToInt32(dr["PesoBruto"]);
                    pesajeMateriaPrimaInfo.PesoTara = Convert.ToInt32(dr["PesoTara"]);
                    pesajeMateriaPrimaInfo.Piezas = Convert.ToInt32(dr["Piezas"]);
                    pesajeMateriaPrimaInfo.TipoPesajeID = Convert.ToInt32(dr["TipoPesajeID"]);
                    pesajeMateriaPrimaInfo.UsuarioIDSurtido = Convert.ToInt32(dr["UsuarioIDSurtido"]);
                    pesajeMateriaPrimaInfo.FechaSurtido = Convert.ToDateTime(dr["FechaSurtido"]);
                    pesajeMateriaPrimaInfo.UsuarioIDRecibe = Convert.ToInt32(dr["UsuarioIDRecibe"]);
                    pesajeMateriaPrimaInfo.FechaRecibe = Convert.ToDateTime(dr["FechaRecibe"]);
                    pesajeMateriaPrimaInfo.EstatusID = Convert.ToInt32(dr["EstatusID"]);
                    pesajeMateriaPrimaInfo.Activo = Convert.ToBoolean(dr["Activo"]);
                    pesajeMateriaPrimaInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    pesajeMateriaPrimaInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pesajeMateriaPrimaInfo;
        }
        /// <summary>
        /// Se obtienen los datos del pesaje materia prima por programacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionMateriaPrimaId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PesajeMateriaPrimaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PesajeMateriaPrimaInfo
                         {
                             PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID"),
                             ProgramacionMateriaPrimaID = info.Field<int>("ProgramacionMateriaPrimaID"),
                             ProveedorChoferID = info["ProveedorChoferID"] == DBNull.Value ? 0 : Convert.ToInt32(info["ProveedorChoferID"]),
                             Ticket = info.Field<int>("Ticket"),
                             CamionID = info["CamionID"] == DBNull.Value ? 0 : Convert.ToInt32(info["CamionID"]),
                             PesoBruto = info.Field<int>("PesoBruto"),
                             PesoTara = info.Field<int>("PesoTara"),
                             Piezas = info.Field<int>("Piezas"),
                             TipoPesajeID = info.Field<int>("TipoPesajeID"),
                             UsuarioIDSurtido = info.Field<int>("UsuarioIDSurtido"),
                             FechaSurtido = info.Field<DateTime>("FechaSurtido"),
                             UsuarioIDRecibe = info.Field<int>("UsuarioIDRecibe"),
                             FechaRecibe = info.Field<DateTime>("FechaRecibe"),
                             EstatusID = info.Field<int>("EstatusID"),
                             Activo = info.Field<bool>("Activo"),
                             FechaCreacion = info["FechaCreacion"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionID = info["UsuarioCreacionID"] == DBNull.Value ? 0 : info.Field<int>("UsuarioCreacionID"),
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
        /// Obtiene una lista con los valores necesarios
        /// para la generacion de la poliza de pase a proceso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PolizaPaseProcesoModel> ObtenerValoresPolizaPaseProceso(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtProveedor = ds.Tables[ConstantesDAL.DtProveedor];
                DataTable dtFletesCostos = ds.Tables[ConstantesDAL.DtFletesCostos];
                DataTable dtFletesPesaje = ds.Tables[ConstantesDAL.DtFletesPesaje];
                DataTable dtAlmacenMovimientoCosto = ds.Tables[ConstantesDAL.DtAlmacenMovimientoCosto];
                List<PolizaPaseProcesoModel> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PolizaPaseProcesoModel
                         {
                             Producto = new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Producto"),
                                 UnidadId = info.Field<int>("UnidadID")
                             },
                             PesajeMateriaPrima = new PesajeMateriaPrimaInfo
                             {
                                 PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID"),
                                 Ticket = info.Field<int>("Ticket")
                             },
                             ProgramacionMateriaPrima = new ProgramacionMateriaPrimaInfo
                             {
                                 Observaciones = info.Field<string>("Observaciones"),
                                 CantidadEntregada =
                                     info.Field<decimal>("CantidadEntregada"),
                                 Almacen = new AlmacenInfo
                                 {
                                     AlmacenID =
                                         info.Field<int>("AlmacenID"),
                                     Descripcion =
                                         info.Field<string>("Almacen")
                                 },
                                 ProgramacionMateriaPrimaId = info.Field<int>("ProgramacionMateriaPrimaID")
                             },
                             FleteInterno = new FleteInternoInfo
                             {
                                 FleteInternoId = info.Field<int?>("FleteInternoID") ?? 0
                             },
                             FleteInternoCosto = new FleteInternoCostoInfo(),
                             AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle
                             {
                                 Precio =
                                     info.Field<decimal>(
                                         "PrecioAlmacenMovimientoDetalle"),
                                 Importe =
                                     info.Field<decimal>(
                                         "ImporteAlmacenMovimientoDetalle")
                             },
                             Proveedor = new ProveedorInfo(),
                             ProveedorChofer = new ProveedorChoferInfo
                             {
                                 ProveedorChoferID = info.Field<int?>("ProveedorChoferID") ?? 0
                             },
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                             {
                                 Lote = info.Field<int>("Lote")
                             },
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                                 Iva = new IvaInfo
                                 {
                                     TasaIva = info.Field<decimal>("TasaIva")
                                 }
                             },
                             Pedido = new PedidoInfo
                             {
                                 FolioPedido = info.Field<int>("FolioPedido"),
                                 FechaPedido = info.Field<DateTime>("FechaPedido")
                             },
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info.Field<int?>("AlmacenIDOrigen") ?? 0,
                                 Descripcion = info.Field<string>("AlmacenOrigen")
                             }
                         }).ToList();
                if (dtProveedor != null && dtProveedor.Rows.Count > 0)
                {
                    lista = (from lst in lista
                             from prov in dtProveedor.AsEnumerable()
                             where lst.ProveedorChofer.ProveedorChoferID == prov.Field<int>("ProveedorChoferID")
                             select new PolizaPaseProcesoModel
                                        {
                                            Producto = lst.Producto,
                                            PesajeMateriaPrima = lst.PesajeMateriaPrima,
                                            ProgramacionMateriaPrima = lst.ProgramacionMateriaPrima,
                                            FleteInterno = lst.FleteInterno,
                                            FleteInternoCosto = new FleteInternoCostoInfo(),
                                            AlmacenMovimientoDetalle = lst.AlmacenMovimientoDetalle,
                                            Proveedor = new ProveedorInfo
                                                            {
                                                                ProveedorID = prov.Field<int>("ProveedorID"),
                                                                CodigoSAP = prov.Field<string>("CodigoSAP"),
                                                                Descripcion = prov.Field<string>("Descripcion")
                                                            },
                                            ProveedorChofer = lst.ProveedorChofer,
                                            AlmacenInventarioLote = lst.AlmacenInventarioLote,
                                            Organizacion = lst.Organizacion,
                                            Pedido = lst.Pedido,
                                            Almacen = lst.Almacen
                                        }).ToList();
                }
                if (dtFletesCostos != null && dtFletesCostos.Rows.Count > 1)
                {
                    lista = (from lst in lista
                             from costo in dtFletesCostos.AsEnumerable()
                             where (lst.FleteInterno.FleteInternoId == costo.Field<int>("FleteInternoID") || lst.ProveedorChofer.ProveedorChoferID == 0)
                                   && lst.Proveedor.ProveedorID == costo.Field<int>("ProveedorID")
                             select new PolizaPaseProcesoModel
                                        {
                                            Producto = lst.Producto,
                                            PesajeMateriaPrima = lst.PesajeMateriaPrima,
                                            ProgramacionMateriaPrima = lst.ProgramacionMateriaPrima,
                                            FleteInterno = lst.FleteInterno,
                                            FleteInternoCosto = new FleteInternoCostoInfo
                                                                    {
                                                                        Costo = new CostoInfo
                                                                                    {
                                                                                        CostoID =
                                                                                            costo.Field<int>("CostoID")
                                                                                    },
                                                                        Tarifa = costo.Field<decimal>("Tarifa"),
                                                                        FleteInternoDetalleId =
                                                                            costo.Field<int>("FleteInternoDetalleID"),
                                                                        TipoTarifaID = costo.Field<int?>("TipoTarifaID") != null ? costo.Field<int>("TipoTarifaID") : 1
                                                                    },
                                            AlmacenMovimientoDetalle = lst.AlmacenMovimientoDetalle,
                                            Proveedor = lst.Proveedor,
                                            ProveedorChofer = lst.ProveedorChofer,
                                            AlmacenInventarioLote = lst.AlmacenInventarioLote,
                                            Organizacion = lst.Organizacion,
                                            Pedido = lst.Pedido,
                                            Almacen = lst.Almacen
                                        }).ToList();
                }
                if (dtFletesPesaje != null && dtFletesPesaje.Rows.Count > 0)
                {
                    lista = (from lst in lista
                             from pesaje in dtFletesPesaje.AsEnumerable()
                             where lst.ProveedorChofer.ProveedorChoferID == (pesaje.Field<int?>("ProveedorChoferID") ?? 0) &&
                                 lst.ProgramacionMateriaPrima.ProgramacionMateriaPrimaId == pesaje.Field<int>("ProgramacionMateriaPrimaID") &&
                                 lst.PesajeMateriaPrima.PesajeMateriaPrimaID == pesaje.Field<int>("PesajeMateriaPrimaID")
                             select new PolizaPaseProcesoModel
                                        {
                                            Producto = lst.Producto,
                                            PesajeMateriaPrima = new PesajeMateriaPrimaInfo
                                                                     {
                                                                         PesajeMateriaPrimaID = pesaje.Field<int>("PesajeMateriaPrimaID"),
                                                                         PesoBruto = pesaje.Field<int>("PesoBruto"),
                                                                         PesoTara = pesaje.Field<int>("PesoTara"),
                                                                         Ticket = lst.PesajeMateriaPrima.Ticket,
                                                                         AlmacenMovimientoDestinoId = pesaje.Field<long?>("AlmacenMovimientoDestinoID") != null ? pesaje.Field<long>("AlmacenMovimientoDestinoID") : 0
                                                                     },
                                            ProgramacionMateriaPrima = lst.ProgramacionMateriaPrima,
                                            FleteInterno = lst.FleteInterno,
                                            FleteInternoCosto = lst.FleteInternoCosto,
                                            AlmacenMovimientoDetalle = lst.AlmacenMovimientoDetalle,
                                            Proveedor = lst.Proveedor,
                                            ProveedorChofer = lst.ProveedorChofer,
                                            AlmacenInventarioLote = lst.AlmacenInventarioLote,
                                            Organizacion = lst.Organizacion,
                                            Pedido = lst.Pedido,
                                            Almacen = lst.Almacen,
                                            ListaAlmacenMovimientoCosto = (from costos in dtAlmacenMovimientoCosto.AsEnumerable()
                                                                      where costos.Field<long>("AlmacenMovimientoID") == (pesaje.Field<long?>("AlmacenMovimientoDestinoID") != null ? pesaje.Field<long>("AlmacenMovimientoDestinoID") : 0)
                                                                      select new AlmacenMovimientoCostoInfo
                                                                             {
                                                                                 AlmacenMovimientoCostoId = costos.Field<int>("AlmacenMovimientoCostoID"),
                                                                                 AlmacenMovimientoId = costos.Field<long>("AlmacenMovimientoID"),
                                                                                 Proveedor = new ProveedorInfo
                                                                                             {
                                                                                                 ProveedorID = costos.Field<int?>("ProveedorID") != null ? costos.Field<int>("ProveedorID") : 0
                                                                                             },
                                                                                 CuentaSap = new CuentaSAPInfo
                                                                                             {
                                                                                                 CuentaSAPID = costos.Field<int?>("CuentaSAPID") != null ? costos.Field<int>("CuentaSAPID") : 0
                                                                                             },
                                                                                 Costo = new CostoInfo
                                                                                             {
                                                                                                 CostoID = costos.Field<int?>("CostoID") != null ? costos.Field<int>("CostoID") : 0,
                                                                                                 Descripcion = costos.Field<string>("Costo")
                                                                                             },
                                                                                 Cantidad = costos.Field<decimal>("Cantidad"),
                                                                                 Importe = costos.Field<decimal>("Importe")
                                                                             }).ToList()
                                        }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista con los valores necesarios
        /// para la generacion de la poliza de pase a proceso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PolizaPaseProcesoModel> ObtenerValoresPolizaPaseProcesoReimpresion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtProveedor = ds.Tables[ConstantesDAL.DtProveedor];
                DataTable dtFletesCostos = ds.Tables[ConstantesDAL.DtFletesCostos];
                DataTable dtFletesPesaje = ds.Tables[ConstantesDAL.DtFletesPesaje];
                DataTable dtAlmacenMovimientoCosto = ds.Tables[ConstantesDAL.DtAlmacenMovimientoCosto];
                List<PolizaPaseProcesoModel> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PolizaPaseProcesoModel
                         {
                             Producto = new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Producto"),
                                 UnidadId = info.Field<int>("UnidadID")
                             },
                             PesajeMateriaPrima = new PesajeMateriaPrimaInfo
                             {
                                 PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID"),
                                 Ticket = info.Field<int>("Ticket")
                             },
                             ProgramacionMateriaPrima = new ProgramacionMateriaPrimaInfo
                             {
                                 Observaciones = info.Field<string>("Observaciones"),
                                 CantidadEntregada =
                                     info.Field<decimal>("CantidadEntregada"),
                                 Almacen = new AlmacenInfo
                                 {
                                     AlmacenID =
                                         info.Field<int>("AlmacenID"),
                                     Descripcion =
                                         info.Field<string>("Almacen")
                                 },
                                 ProgramacionMateriaPrimaId = info.Field<int>("ProgramacionMateriaPrimaID")
                             },
                             FleteInterno = new FleteInternoInfo
                             {
                                 FleteInternoId = info.Field<int?>("FleteInternoID") ?? 0
                             },
                             FleteInternoCosto = new FleteInternoCostoInfo(),
                             AlmacenMovimientoDetalle = new AlmacenMovimientoDetalle
                             {
                                 Precio =
                                     info.Field<decimal>(
                                         "PrecioAlmacenMovimientoDetalle"),
                                 Importe =
                                     info.Field<decimal>(
                                         "ImporteAlmacenMovimientoDetalle")
                             },
                             Proveedor = new ProveedorInfo(),
                             ProveedorChofer = new ProveedorChoferInfo
                             {
                                 ProveedorChoferID = info.Field<int?>("ProveedorChoferID") ?? 0
                             },
                             AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                             {
                                 Lote = info.Field<int>("Lote")
                             },
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                                 Iva = new IvaInfo
                                 {
                                     TasaIva = info.Field<decimal>("TasaIva")
                                 }
                             },
                             Pedido = new PedidoInfo
                             {
                                 FolioPedido = info.Field<int>("FolioPedido"),
                                 FechaPedido = info.Field<DateTime>("FechaPedido")
                             },
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info.Field<int?>("AlmacenIDOrigen") ?? 0,
                                 Descripcion = info.Field<string>("AlmacenOrigen")
                             }
                         }).ToList();
                if (dtProveedor != null && dtProveedor.Rows.Count > 0)
                {
                    lista = (from lst in lista
                             from prov in dtProveedor.AsEnumerable()
                             where lst.ProveedorChofer.ProveedorChoferID == prov.Field<int>("ProveedorChoferID")
                             select new PolizaPaseProcesoModel
                             {
                                 Producto = lst.Producto,
                                 PesajeMateriaPrima = lst.PesajeMateriaPrima,
                                 ProgramacionMateriaPrima = lst.ProgramacionMateriaPrima,
                                 FleteInterno = lst.FleteInterno,
                                 FleteInternoCosto = new FleteInternoCostoInfo(),
                                 AlmacenMovimientoDetalle = lst.AlmacenMovimientoDetalle,
                                 Proveedor = new ProveedorInfo
                                 {
                                     ProveedorID = prov.Field<int>("ProveedorID"),
                                     CodigoSAP = prov.Field<string>("CodigoSAP"),
                                     Descripcion = prov.Field<string>("Descripcion")
                                 },
                                 ProveedorChofer = lst.ProveedorChofer,
                                 AlmacenInventarioLote = lst.AlmacenInventarioLote,
                                 Organizacion = lst.Organizacion,
                                 Pedido = lst.Pedido,
                                 Almacen = lst.Almacen
                             }).ToList();
                }
                if (dtFletesCostos != null && dtFletesCostos.Rows.Count > 0)
                {
                    lista = (from lst in lista.DefaultIfEmpty()
                             from costo in dtFletesCostos.AsEnumerable()
                             where lst.FleteInterno.FleteInternoId == costo.Field<int>("FleteInternoID")
                                   && lst.Proveedor.ProveedorID == costo.Field<int>("ProveedorID")
                             select new PolizaPaseProcesoModel
                             {
                                 Producto = lst.Producto,
                                 PesajeMateriaPrima = lst.PesajeMateriaPrima,
                                 ProgramacionMateriaPrima = lst.ProgramacionMateriaPrima,
                                 FleteInterno = lst.FleteInterno,
                                 FleteInternoCosto = new FleteInternoCostoInfo
                                 {
                                     Costo = new CostoInfo
                                     {
                                         CostoID =
                                             costo.Field<int>("CostoID")
                                     },
                                     Tarifa = costo.Field<decimal>("Tarifa"),
                                     FleteInternoDetalleId =
                                         costo.Field<int>("FleteInternoDetalleID"),
                                     TipoTarifaID = costo.Field<int?>("TipoTarifaID") != null ? costo.Field<int>("TipoTarifaID") : 1,
                                 },
                                 AlmacenMovimientoDetalle = lst.AlmacenMovimientoDetalle,
                                 Proveedor = lst.Proveedor,
                                 ProveedorChofer = lst.ProveedorChofer,
                                 AlmacenInventarioLote = lst.AlmacenInventarioLote,
                                 Organizacion = lst.Organizacion,
                                 Pedido = lst.Pedido,
                                 Almacen = lst.Almacen
                             }).ToList();
                }
                if (dtFletesPesaje != null && dtFletesPesaje.Rows.Count > 0)
                {
                    lista = (from lst in lista
                             from pesaje in dtFletesPesaje.AsEnumerable()
                             where lst.ProveedorChofer.ProveedorChoferID == (pesaje.Field<int?>("ProveedorChoferID") ?? 0) &&
                                  lst.ProgramacionMateriaPrima.ProgramacionMateriaPrimaId == pesaje.Field<int>("ProgramacionMateriaPrimaID") &&
                                  lst.PesajeMateriaPrima.PesajeMateriaPrimaID == pesaje.Field<int>("PesajeMateriaPrimaID")
                             select new PolizaPaseProcesoModel
                             {
                                 Producto = lst.Producto,
                                 PesajeMateriaPrima = new PesajeMateriaPrimaInfo
                                 {
                                     PesajeMateriaPrimaID =
                                         pesaje.Field<int>("PesajeMateriaPrimaID"),
                                     PesoBruto = pesaje.Field<int>("PesoBruto"),
                                     PesoTara = pesaje.Field<int>("PesoTara"),
                                     Ticket = lst.PesajeMateriaPrima.Ticket
                                 },
                                 ProgramacionMateriaPrima = lst.ProgramacionMateriaPrima,
                                 FleteInterno = lst.FleteInterno,
                                 FleteInternoCosto = lst.FleteInternoCosto,
                                 AlmacenMovimientoDetalle = lst.AlmacenMovimientoDetalle,
                                 Proveedor = lst.Proveedor,
                                 ProveedorChofer = lst.ProveedorChofer,
                                 AlmacenInventarioLote = lst.AlmacenInventarioLote,
                                 Organizacion = lst.Organizacion,
                                 Pedido = lst.Pedido,
                                 Almacen = lst.Almacen,
                                 ListaAlmacenMovimientoCosto = (from costos in dtAlmacenMovimientoCosto.AsEnumerable()
                                                                where costos.Field<long>("AlmacenMovimientoID") == (pesaje.Field<long?>("AlmacenMovimientoDestinoID") != null ? pesaje.Field<long>("AlmacenMovimientoDestinoID") : 0)
                                                                select new AlmacenMovimientoCostoInfo
                                                                {
                                                                    AlmacenMovimientoCostoId = costos.Field<int>("AlmacenMovimientoCostoID"),
                                                                    AlmacenMovimientoId = costos.Field<long>("AlmacenMovimientoID"),
                                                                    Proveedor = new ProveedorInfo
                                                                    {
                                                                        ProveedorID = costos.Field<int?>("ProveedorID") != null ? costos.Field<int>("ProveedorID") : 0
                                                                    },
                                                                    CuentaSap = new CuentaSAPInfo
                                                                    {
                                                                        CuentaSAPID = costos.Field<int?>("CuentaSAPID") != null ? costos.Field<int>("CuentaSAPID") : 0
                                                                    },
                                                                    Costo = new CostoInfo
                                                                    {
                                                                        CostoID = costos.Field<int?>("CostoID") != null ? costos.Field<int>("CostoID") : 0,
                                                                        Descripcion = costos.Field<string>("Costo")
                                                                    },
                                                                    Cantidad = costos.Field<decimal>("Cantidad"),
                                                                    Importe = costos.Field<decimal>("Importe")
                                                                }).ToList()
                             }).ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Se obtienen los datos del pesaje materia prima por programacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteInventarioPaseProcesoModel> ObtenerFoliosPaseProceso(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ReporteInventarioPaseProcesoModel> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ReporteInventarioPaseProcesoModel
                         {
                             FolioPaseProceso = info.Field<string>("FolioPaseProceso"),
                             TipoMovimientoOrigenID = info.Field<int>("TipoMovimientoOrigenID"),
                             TipoMovimientoDestinoID = info.Field<int>("TipoMovimientoDestinoID"),
                             AlmacenMovimientoOrigenID = info.Field<long>("AlmacenMovimientoOrigenID"),
                             AlmacenMovimientoDestinoID = info.Field<long>("AlmacenMovimientoDestinoID")
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
