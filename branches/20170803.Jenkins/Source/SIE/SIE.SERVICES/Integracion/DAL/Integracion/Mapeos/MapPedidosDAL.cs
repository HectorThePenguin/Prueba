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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPedidosDAL
    {
        /// <summary>
        /// Obtiene los datos de los pedidos programados y parciales
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoInfo> ObtenerPedidosProgramadosYParciales(DataSet ds)
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
        /// Obtiene los datos de los todos pedidos.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoInfo> ObtenerPedidosTodos(DataSet ds)
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
                                        OrganizacionID = info.Field<int>("OrganizacionID")
                                    },
                                    EstatusPedido = new EstatusInfo
                                    {
                                        EstatusId = info.Field<int>("EstatusID"),
                                        Descripcion = info.Field<string>("DescripcionEstatus"),
                                    },
                                    Almacen = new AlmacenInfo
                                    {
                                        AlmacenID = info.Field<int>("AlmacenID")
                                    },
                                    Observaciones =  info["Observaciones"] == DBNull.Value ?
                                                      string.Empty :
                                                      info.Field<string>("Observaciones"),
                                    FechaPedido = info["FechaPedido"] == DBNull.Value ?
                                                             new DateTime() :
                                                             info.Field<DateTime>("FechaPedido"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    FechaCreacion = info["FechaCreacion"] == DBNull.Value ?
                                                             new DateTime(1900, 1, 1) : 
                                                             info.Field<DateTime>("FechaCreacion"),
                                    UsuarioCreacion = info["UsuarioCreacionID"] == DBNull.Value ?
                                                      new UsuarioInfo() :
                                                      new UsuarioInfo
                                                      {
                                                          UsuarioID = info.Field<int>("UsuarioCreacionID")
                                                      },
                                    FechaModificacion = info["FechaModificacion"] == DBNull.Value ?
                                                        new DateTime(1900, 1, 1) :
                                                        info.Field<DateTime>("FechaModificacion"),
                                    UsuarioModificacion = info["UsuarioModificacionID"] == DBNull.Value ?
                                                        new UsuarioInfo() :
                                                        new UsuarioInfo
                                                        {
                                                            UsuarioID = info.Field<int>("UsuarioModificacionID")
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
        /// Obtiene todos los datos de los pedidos que fueron consultados por folio
        /// paginado.
        /// </summary>
        /// <returns></returns>
        internal static ResultadoInfo<PedidoInfo> ObtenerPedidosPorFolioPaginado(DataSet ds)
        {
            ResultadoInfo<PedidoInfo> resultado;
            List<PedidoInfo> listaPedidos;
            try
            {
                Logger.Info();

                listaPedidos = ObtenerPedidosTodos(ds);

                resultado = new ResultadoInfo<PedidoInfo>
                {
                    Lista = listaPedidos,
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
        /// Obtiene todos los datos del pedido que fueron consultados por folio pedido.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoInfo ObtenerPedidoPorFolio(DataSet ds)
        {
            PedidoInfo pedido = null;
            try
            {
                List<PedidoInfo> listaPedidos = ObtenerPedidosTodos(ds);
                if (listaPedidos != null)
                {
                    pedido = (from pedidos in listaPedidos
                        select pedidos).First();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }

            return pedido;
        
        }

        /// <summary>
        /// Obtiene una lista paginada de pedidos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<PedidoInfo> ObtenerPedidosProgramadosPaginado(DataSet ds)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista = (from info in dt.AsEnumerable()
                           select new PedidoInfo
                           {
                               PedidoID = info.Field<int>("PedidoID"),
                               FolioPedido = info.Field<int>("FolioPedido"),
                               Almacen = new AlmacenInfo
                                             {
                                                 Descripcion = info.Field<string>("Descripcion")
                                             }
                           }).ToList();
                resultado =
                    new ResultadoInfo<PedidoInfo>
                    {
                        Lista = lista,
                        TotalRegistros =
                            Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        /// Obtiene un pedido por su folio de pedido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoInfo ObtenerPedidosProgramadosPorFolioPedido(DataSet ds)
        {
            PedidoInfo pedidio;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedidio = (from info in dt.AsEnumerable()
                           select new PedidoInfo
                                {
                                    PedidoID = info.Field<int>("PedidoID"),
                                    FolioPedido = info.Field<int>("FolioPedido"),                                    
                                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedidio;
        }

        /// <summary>
        /// Obtiene un pedido por ticket
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoInfo ObtenerPedidoPorTicketPesaje(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                PedidoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new PedidoInfo
                         {
                             FolioPedido = info.Field<int>("FolioPedido")
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static PedidoInfo Crear(DataSet ds)
        {
            PedidoInfo pedido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedido = (from info in dt.AsEnumerable()
                                select new PedidoInfo
                                {
                                    PedidoID = info.Field<int>("PedidoID"),
                                    FolioPedido = info.Field<int>("FolioPedido"),
                                    Organizacion = new OrganizacionInfo
                                    {
                                        OrganizacionID = info.Field<int>("OrganizacionID")
                                    },
                                    EstatusPedido = new EstatusInfo
                                    {
                                        EstatusId = info.Field<int>("EstatusID")
                                    },
                                    Almacen = new AlmacenInfo
                                    {
                                        AlmacenID = info.Field<int>("AlmacenID")
                                    },
                                    Observaciones = info["Observaciones"] == DBNull.Value ?
                                                      string.Empty :
                                                      info.Field<string>("Observaciones"),
                                    FechaPedido = info["FechaPedido"] == DBNull.Value ?
                                                             new DateTime() :
                                                             info.Field<DateTime>("FechaPedido"),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    FechaCreacion = info["FechaCreacion"] == DBNull.Value ?
                                                             new DateTime(1900, 1, 1) :
                                                             info.Field<DateTime>("FechaCreacion"),
                                    UsuarioCreacion = info["UsuarioCreacionID"] == DBNull.Value ?
                                                      new UsuarioInfo() :
                                                      new UsuarioInfo
                                                      {
                                                          UsuarioID = info.Field<int>("UsuarioCreacionID")
                                                      }

                                }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedido;
        }

        /// <summary>
        /// Obtiene un objeto con el resultado paginado
        /// de los pedidos completados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<PedidoInfo> ObtenerPedidoCompletoPaginado(DataSet ds)
        {
            ResultadoInfo<PedidoInfo> result;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<PedidoInfo> listaPedidos = (from info in dt.AsEnumerable()
                                                  select new PedidoInfo
                                                             {
                                                                 PedidoID = info.Field<int>("PedidoID"),
                                                                 FolioPedido = info.Field<int>("FolioPedido"),
                                                                 Organizacion = new OrganizacionInfo
                                                                                    {
                                                                                        OrganizacionID =
                                                                                            info.Field<int>(
                                                                                                "OrganizacionID"),
                                                                                        Descripcion =
                                                                                            info.Field<string>(
                                                                                                "Organizacion")
                                                                                    },
                                                                 Almacen = new AlmacenInfo
                                                                               {
                                                                                   AlmacenID =
                                                                                       info.Field<int>("AlmacenID"),
                                                                                   Descripcion =
                                                                                       info.Field<string>("Almacen")
                                                                               },
                                                                 Observaciones = info.Field<string>("Almacen"),
                                                                 FechaPedido = info.Field<DateTime>("FechaPedido")
                                                             }).ToList();
                result = new ResultadoInfo<PedidoInfo>
                             {
                                 Lista = listaPedidos,
                                 TotalRegistros =
                                     Convert.ToInt32(ds.Tables[ConstantesDAL.DtDetalle].Rows[0]["TotalReg"])
                             };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un Pedido por Folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoInfo ObtenerPedidoCompleto(DataSet ds)
        {
            PedidoInfo pedido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedido = (from info in dt.AsEnumerable()
                          select new PedidoInfo
                                     {
                                         PedidoID = info.Field<int>("PedidoID"),
                                         FolioPedido = info.Field<int>("FolioPedido"),
                                         Organizacion = new OrganizacionInfo
                                                            {
                                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                                                Descripcion = info.Field<string>("Organizacion")
                                                            },
                                         Almacen = new AlmacenInfo
                                                       {
                                                           AlmacenID = info.Field<int>("AlmacenID"),
                                                           Descripcion = info.Field<string>("Almacen")
                                                       },
                                         FechaPedido = info.Field<DateTime>("FechaPedido")
                                     }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedido;
        }

        /// <summary>
        /// Obtiene un Pedido por Folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoPendienteLoteModel> ObtenerPedidosPendientesPorLote(DataSet ds)
        {
            List<PedidoPendienteLoteModel> pedido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedido = (from info in dt.AsEnumerable()
                          select new PedidoPendienteLoteModel
                          {
                              PedidoID = info.Field<int>("PedidoID"),
                              FolioPedido = info.Field<int>("FolioPedido"),
                              PedidoDetalleID = info.Field<int>("PedidoDetalleID"),
                              ProductoID = info.Field<int>("ProductoID"),
                              InventarioLoteIDDestino = info.Field<int>("InventarioLoteIDDestino"),
                              ProgramacionMateriaPrimaID = info.Field<int?>("ProgramacionMateriaPrimaID") != null ? info.Field<int>("ProgramacionMateriaPrimaID") : 0,
                              InventarioLoteIDOrigen = info.Field<int?>("InventarioLoteIDOrigen") != null ? info.Field<int>("InventarioLoteIDOrigen") : 0,
                              PesajeMateriaPrimaID = info.Field<int?>("PesajeMateriaPrimaID") != null ? info.Field<int>("PesajeMateriaPrimaID") : 0,
                              EstatusID = info.Field<int?>("EstatusID") != null ? info.Field<int>("EstatusID") : 0,
                             
                          }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedido;
        }

        /// <summary>
        /// Obtiene un Pedido por Folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoPendienteLoteModel> ObtenerPedidosProgramadosPorLote(DataSet ds)
        {
            List<PedidoPendienteLoteModel> pedido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedido = (from info in dt.AsEnumerable()
                          select new PedidoPendienteLoteModel
                          {
                              PedidoID = info.Field<int>("PedidoID"),
                              FolioPedido = info.Field<int>("FolioPedido"),
                              PedidoDetalleID = info.Field<int>("PedidoDetalleID"),
                              ProductoID = info.Field<int>("ProductoID"),
                              InventarioLoteIDDestino = info.Field<int>("InventarioLoteIDDestino"),
                              ProgramacionMateriaPrimaID = info.Field<int?>("ProgramacionMateriaPrimaID") != null ? info.Field<int>("ProgramacionMateriaPrimaID") : 0,
                              InventarioLoteIDOrigen = info.Field<int?>("InventarioLoteIDOrigen") != null ? info.Field<int>("InventarioLoteIDOrigen") : 0,
                              PesajeMateriaPrimaID = info.Field<int?>("PesajeMateriaPrimaID") != null ? info.Field<int>("PesajeMateriaPrimaID") : 0,
                              EstatusID = info.Field<int?>("EstatusID") != null ? info.Field<int>("EstatusID") : 0,
                              CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                              AlmacenMovimientoOrigenID = info.Field<long?>("AlmacenMovimientoOrigenID") != null ? info.Field<long>("AlmacenMovimientoOrigenID") : 0
                          }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedido;
        }

        /// <summary>
        /// Obtiene un Pedido por Folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoPendienteLoteModel> ObtenerPedidosEntregadosPorLote(DataSet ds)
        {
            List<PedidoPendienteLoteModel> pedido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedido = (from info in dt.AsEnumerable()
                          select new PedidoPendienteLoteModel
                          {
                              PedidoID = info.Field<int>("PedidoID"),
                              FolioPedido = info.Field<int>("FolioPedido"),
                              PedidoDetalleID = info.Field<int>("PedidoDetalleID"),
                              ProductoID = info.Field<int>("ProductoID"),
                              InventarioLoteIDDestino = info.Field<int>("InventarioLoteIDDestino"),
                              ProgramacionMateriaPrimaID = info.Field<int?>("ProgramacionMateriaPrimaID") != null ? info.Field<int>("ProgramacionMateriaPrimaID") : 0,
                              InventarioLoteIDOrigen = info.Field<int?>("InventarioLoteIDOrigen") != null ? info.Field<int>("InventarioLoteIDOrigen") : 0,
                              PesajeMateriaPrimaID = info.Field<int?>("PesajeMateriaPrimaID") != null ? info.Field<int>("PesajeMateriaPrimaID") : 0,
                              EstatusID = info.Field<int?>("EstatusID") != null ? info.Field<int>("EstatusID") : 0,
                              CantidadProgramada = info.Field<decimal>("CantidadProgramada"),
                              AlmacenMovimientoOrigenID = info.Field<long?>("AlmacenMovimientoOrigenID") != null ? info.Field<long>("AlmacenMovimientoOrigenID") : 0,
                              CantidadEntregada = info.Field<int?>("PesoNeto") != null ? info.Field<int>("PesoNeto") : 0
                          }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedido;
        }
    }
}
