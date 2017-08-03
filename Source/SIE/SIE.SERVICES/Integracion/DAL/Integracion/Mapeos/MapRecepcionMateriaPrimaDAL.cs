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
    internal class MapRecepcionMateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene los datos de los pedidos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoInfo> ObtenerPedidosParcial(DataSet ds)
        {
            List<PedidoInfo> listaPedidos;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaPedidos = (from info in dt.AsEnumerable()
                    select new PedidoInfo
                    {
                        PedidoID = info.Field<int>("PedidoID"),
                        FolioPedido = info.Field<int>("FolioPedido"),
                        Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            Descripcion = info.Field<string>("DescripcionOrganizacion")
                        },
                        EstatusPedido = new EstatusInfo
                        {
                            EstatusId = info.Field<int>("EstatusID"),
                            Descripcion = info.Field<string>("DescripcionEstatus")
                        }
                    }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaPedidos;
        }
        /// <summary>
        /// Obtiene los datos del surtido
        /// </summary>
        /// <param name="ds">Data set que contiene los datos</param>
        /// <param name="pedido">Pedido actual</param>
        /// <returns></returns>
        internal static List<SurtidoPedidoInfo> ObtenerSurtidosPedido(DataSet ds, PedidoInfo pedido)
        {
            List<SurtidoPedidoInfo> listaSurtido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaSurtido = (from info in dt.AsEnumerable()
                                select new SurtidoPedidoInfo
                                {
                                    PesajeMateriaPrima = new PesajeMateriaPrimaInfo{
                                        PesajeMateriaPrimaID = info.Field<int>("PesajeMateriaPrimaID"),
                                        FechaSurtido = info.Field<DateTime>("FechaSurtido"),
                                        Ticket = info.Field<int>("Ticket"),
                                        TipoPesajeID = info.Field<int>("TipoPesajeID"),
                                        TipoPesajeDescripcion = info.Field<string>("DescripcionTipoPesaje"),
                                        ProveedorChoferID = info["ProveedorChoferID"] == DBNull.Value ? 0 : Convert.ToInt32(info["ProveedorChoferID"]),
                                        PesoBruto = info["PesoBruto"] == DBNull.Value ? 0 : info.Field<int>("PesoBruto"),
                                        PesoTara = info["PesoTara"] == DBNull.Value ? 0 : info.Field<int>("PesoTara"),
                                        Piezas = info["PiezasPesaje"] == DBNull.Value ? 0 : info.Field<int>("PiezasPesaje"),
                                        Activo = info["ActivoPesaje"] != DBNull.Value && info.Field<bool>("ActivoPesaje"),
                                        AlmacenMovimientoOrigenId = info["AlmacenMovimientoOrigenID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoOrigenID"),
                                        AlmacenMovimientoDestinoId = info["AlmacenMovimientoDestinoID"] == DBNull.Value ? 0 : info.Field<long>("AlmacenMovimientoDestinoID"),
                                    },
                                    Producto = new ProductoInfo
                                    {
                                        ProductoId = info.Field<int>("ProductoID"),
                                        ProductoDescripcion = info.Field<string>("DescripcionProducto")
                                    },
                                    Chofer = new ChoferInfo
                                    {
                                        ChoferID = info["ChoferID"] == DBNull.Value ? 0 : info.Field<int>("ChoferID"),
                                        Nombre = info.Field<string>("Nombre"),
                                        ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                                        ApellidoMaterno = info.Field<string>("ApellidoMaterno")
                                    },
                                    Proveedor = new ProveedorInfo
                                    {
                                        ProveedorID = info["ProveedorID"] == DBNull.Value ? 0 : info.Field<int>("ProveedorID"),
                                        Descripcion = info.Field<string>("DescripcionProveedor")
                                    },
                                   AlmacenInventarioLote = new AlmacenInventarioLoteInfo
                                   {
                                       Lote = info.Field<int>("Lote"),
                                       AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID"),
                                       AlmacenInventario = new AlmacenInventarioInfo
                                       {
                                           AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                                           AlmacenID = info.Field<int>("AlmacenID")
                                       },
                                       Cantidad = info.Field<decimal>("Cantidad"),
                                       PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                                       Piezas = info.Field<int>("Piezas"),
                                       Importe = info.Field<decimal>("Importe"),
                                       FechaInicio = new DateTime(),
                                       FechaFin = new DateTime()
                                   },
                                    CantidadSolicitada = info.Field<decimal>("CantidadSolicitada"),
                                    CantidadEntregada = info["CantidadEntregada"] == DBNull.Value ? 0 : info.Field<decimal>("CantidadEntregada"),
                                    PedidoDetalle = new PedidoDetalleInfo
                                    {
                                        PedidoDetalleId = info.Field<int>("PedidoDetalleID"),
                                        CantidadSolicitada = info.Field<decimal>("CantidadSolicitada"),
                                        InventarioLoteDestino = new AlmacenInventarioLoteInfo
                                        {
                                            AlmacenInventarioLoteId = info.Field<int>("InventarioLoteIDDestino")
                                        }
                                    },
                                    ProgramacionMateriaPrima = new ProgramacionMateriaPrimaInfo
                                    {
                                        ProgramacionMateriaPrimaId = info.Field<int>("ProgramacionMateriaPrimaID"),
                                        CantidadEntregada = info.Field<decimal>("CantidadEntregada"),
                                        CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                                        InventarioLoteOrigen = new AlmacenInventarioLoteInfo
                                        {
                                            AlmacenInventarioLoteId = info.Field<int>("InventarioLoteIDOrigen")
                                        }
                                    },
                                    Pedido = pedido

                                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaSurtido;
        }
    }
}
