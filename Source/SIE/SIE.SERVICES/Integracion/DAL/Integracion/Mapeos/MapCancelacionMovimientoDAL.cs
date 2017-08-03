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
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapCancelacionMovimientoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CancelacionMovimientoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CancelacionMovimientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CancelacionMovimientoInfo
                             {
                                 CancelacionMovimientoId = info.Field<int>("CancelacionMovimientoID"),
                                 TipoCancelacion = new TipoCancelacionInfo { TipoCancelacionId = info.Field<int>("TipoCancelacionID"), Descripcion = info.Field<string>("TipoCancelacion") },
                                 Pedido = new PedidoInfo { PedidoID = info.Field<int>("PedidoID"), },
                                 Ticket = info.Field<int>("Ticket"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDOrigen") },
                                 AlmacenMovimientoCancelado = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDCancelado") },
                                 FechaCancelacion = info.Field<DateTime>("FechaCancelacion"),
                                 Justificacion = info.Field<string>("Justificacion"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CancelacionMovimientoInfo>
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
        public static List<CancelacionMovimientoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CancelacionMovimientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CancelacionMovimientoInfo
                             {
                                 CancelacionMovimientoId = info.Field<int>("CancelacionMovimientoID"),
                                 TipoCancelacion = new TipoCancelacionInfo { TipoCancelacionId = info.Field<int>("TipoCancelacionID"), Descripcion = info.Field<string>("TipoCancelacion") },
                                 Pedido = new PedidoInfo { PedidoID = info.Field<int>("PedidoID"), },
                                 Ticket = info.Field<int>("Ticket"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDOrigen") },
                                 AlmacenMovimientoCancelado = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDCancelado") },
                                 FechaCancelacion = info.Field<DateTime>("FechaCancelacion"),
                                 Justificacion = info.Field<string>("Justificacion"),
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
        public static CancelacionMovimientoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CancelacionMovimientoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CancelacionMovimientoInfo
                             {
                                 CancelacionMovimientoId = info.Field<int>("CancelacionMovimientoID"),
                                 TipoCancelacion = new TipoCancelacionInfo { TipoCancelacionId = info.Field<int>("TipoCancelacionID"), Descripcion = info.Field<string>("TipoCancelacion") },
                                 Pedido = new PedidoInfo { PedidoID = info.Field<int>("PedidoID"), },
                                 Ticket = info.Field<int>("Ticket"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDOrigen") },
                                 AlmacenMovimientoCancelado = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDCancelado") },
                                 FechaCancelacion = info.Field<DateTime>("FechaCancelacion"),
                                 Justificacion = info.Field<string>("Justificacion"),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static CancelacionMovimientoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CancelacionMovimientoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CancelacionMovimientoInfo
                             {
                                 CancelacionMovimientoId = info.Field<int>("CancelacionMovimientoID"),
                                 TipoCancelacion = new TipoCancelacionInfo { TipoCancelacionId = info.Field<int>("TipoCancelacionID"), Descripcion = info.Field<string>("TipoCancelacion") },
                                 Pedido = new PedidoInfo { PedidoID = info.Field<int>("PedidoID"), },
                                 Ticket = info.Field<int>("Ticket"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDOrigen") },
                                 AlmacenMovimientoCancelado = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDCancelado") },
                                 FechaCancelacion = info.Field<DateTime>("FechaCancelacion"),
                                 Justificacion = info.Field<string>("Justificacion"),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static CancelacionMovimientoInfo ObtenerCrear(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CancelacionMovimientoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CancelacionMovimientoInfo
                         {
                             CancelacionMovimientoId = info.Field<int>("CancelacionMovimientoID"),
                             TipoCancelacion = new TipoCancelacionInfo { TipoCancelacionId = info.Field<int>("TipoCancelacionID")},
                             Pedido = info.Field<int?>("PedidoID") != null ?  new PedidoInfo { PedidoID = info.Field<int>("PedidoID") } : new PedidoInfo(),
                             Ticket = info.Field<int?>("Ticket") != null ? info.Field<int>("Ticket") : 0,
                             AlmacenMovimientoOrigen = info.Field<long?>("AlmacenMovimientoIDOrigen") != null ? new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDOrigen") }: new AlmacenMovimientoInfo(),
                             AlmacenMovimientoCancelado = info.Field<long?>("AlmacenMovimientoIDCancelado") != null ? new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoIDCancelado") }: new AlmacenMovimientoInfo(),
                             FechaCancelacion = info.Field<DateTime>("FechaCancelacion"),
                             Justificacion = info.Field<string>("Justificacion"),
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
    }
}

