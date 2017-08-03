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
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapTraspasoMateriaPrimaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<TraspasoMpPaMedInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                var contratoDAL = new ContratoDAL();
                var almacenDAL = new AlmacenDAL();
                var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();
                var cuentaSAPDAL = new CuentaSAPDAL();
                var productoDAL = new ProductoDAL();
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TraspasoMpPaMedInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TraspasoMpPaMedInfo
                             {
                                 TraspasoMateriaPrimaID = info.Field<int>("TraspasoMateriaPrimaID"),
                                 ContratoOrigen = info.Field<int?>("ContratoOrigenID") != null ? contratoDAL.ObtenerPorId(new ContratoInfo { ContratoId = info.Field<int>("ContratoOrigenID") }) : new ContratoInfo(),
                                 ContratoDestino = info.Field<int?>("ContratoDestinoID") != null ? contratoDAL.ObtenerPorId(new ContratoInfo { ContratoId = info.Field<int>("ContratoDestinoID") }) : new ContratoInfo(),
                                 FolioTraspaso = info.Field<long>("FolioTraspaso"),
                                 AlmacenOrigen = almacenDAL.ObtenerPorID(info.Field<int>("AlmacenOrigenID")),
                                 AlmacenDestino = almacenDAL.ObtenerPorID(info.Field<int>("AlmacenDestinoID")),
                                 LoteMpOrigen = info.Field<int?>("InventarioLoteOrigenID") != null ? almacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorId(info.Field<int>("InventarioLoteOrigenID")) : new AlmacenInventarioLoteInfo(),
                                 LoteMpDestino = info.Field<int?>("InventarioLoteDestinoID") != null ? almacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorId(info.Field<int>("InventarioLoteDestinoID")) : new AlmacenInventarioLoteInfo(),
                                 CuentaContable = info.Field<int?>("CuentaSAPID") != null ? cuentaSAPDAL.ObtenerPorID(info.Field<int>("CuentaSAPID")) : new CuentaSAPInfo(),
                                 JustificacionDestino = info.Field<string>("Justificacion"),
                                 CantidadTraspasarOrigen = info.Field<decimal>("CantidadSalida"),
                                 CantidadTraspasarDestino = info.Field<decimal>("CantidadEntrada"),
                                 ProductoOrigen = productoDAL.ObtenerPorID(new ProductoInfo { ProductoId = info.Field<int>("ProductoID") }),
                                 FechaTraspaso = info.Field<DateTime>("FechaMovimiento")
                                 //AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID")},
                                 //AlmacenMovimientoDestino = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID")},
                                 //Activo = info.Field<bool>("Activo").BoolAEnum(),

                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TraspasoMpPaMedInfo>
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
        public static List<TraspasoMateriaPrimaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TraspasoMateriaPrimaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TraspasoMateriaPrimaInfo
                             {
                                 TraspasoMateriaPrimaID = info.Field<int>("TraspasoMateriaPrimaID"),
                                 ContratoOrigen = new ContratoInfo { ContratoId = info.Field<int>("ContratoOrigenID"), Folio = info.Field<int>("FolioContratoOrigen") },
                                 ContratoDestino = new ContratoInfo { ContratoId = info.Field<int>("ContratoDestinoID"), Folio = info.Field<int>("FolioContratoDestino") },
                                 FolioTraspaso = info.Field<long>("FolioTraspaso"),
                                 AlmacenOrigen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenOrigenID") },
                                 AlmacenDestino = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenDestinoID") },
                                 AlmacenInventarioLoteOrigen = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID") },
                                 AlmacenInventarioLoteDestino = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID") },
                                 CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
                                 Justificacion = info.Field<string>("Justificacion"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID") },
                                 AlmacenMovimientoDestino = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID") },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 FechaMovimiento = info.Field<DateTime>("FechaMovimiento")
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
        public static TraspasoMateriaPrimaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TraspasoMateriaPrimaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TraspasoMateriaPrimaInfo
                             {
                                 TraspasoMateriaPrimaID = info.Field<int>("TraspasoMateriaPrimaID"),
                                 ContratoOrigen = new ContratoInfo { ContratoId = info.Field<int>("ContratoOrigenID"), Folio = info.Field<int>("FolioContratoOrigen") },
                                 ContratoDestino = new ContratoInfo { ContratoId = info.Field<int>("ContratoDestinoID"), Folio = info.Field<int>("FolioContratoDestino") },
                                 FolioTraspaso = info.Field<long>("FolioTraspaso"),
                                 AlmacenOrigen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenOrigenID") },
                                 AlmacenDestino = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenDestinoID") },
                                 AlmacenInventarioLoteOrigen = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID") },
                                 AlmacenInventarioLoteDestino = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID") },
                                 CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
                                 Justificacion = info.Field<string>("Justificacion"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID") },
                                 AlmacenMovimientoDestino = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID") },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 FechaMovimiento = info.Field<DateTime>("FechaMovimiento")
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
        public static TraspasoMateriaPrimaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TraspasoMateriaPrimaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TraspasoMateriaPrimaInfo
                             {
                                 TraspasoMateriaPrimaID = info.Field<int>("TraspasoMateriaPrimaID"),
                                 ContratoOrigen = new ContratoInfo { ContratoId = info.Field<int>("ContratoOrigenID"), Folio = info.Field<int>("FolioContratoOrigen") },
                                 ContratoDestino = new ContratoInfo { ContratoId = info.Field<int>("ContratoDestinoID"), Folio = info.Field<int>("FolioContratoDestino") },
                                 FolioTraspaso = info.Field<long>("FolioTraspaso"),
                                 AlmacenOrigen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenOrigenID") },
                                 AlmacenDestino = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenDestinoID") },
                                 AlmacenInventarioLoteOrigen = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID") },
                                 AlmacenInventarioLoteDestino = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("AlmacenInventarioLoteID") },
                                 CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
                                 Justificacion = info.Field<string>("Justificacion"),
                                 AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID") },
                                 AlmacenMovimientoDestino = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID") },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 FechaMovimiento = info.Field<DateTime>("FechaMovimiento")
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
        public static TraspasoMateriaPrimaInfo ObtenerPorCrear(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TraspasoMateriaPrimaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TraspasoMateriaPrimaInfo
                         {
                             TraspasoMateriaPrimaID = info.Field<int>("TraspasoMateriaPrimaID"),
                             ContratoOrigen = new ContratoInfo { ContratoId = info.Field<int?>("ContratoOrigenID") != null ? info.Field<int>("ContratoOrigenID") : 0},
                             ContratoDestino = new ContratoInfo { ContratoId = info.Field<int?>("ContratoDestinoID") != null ? info.Field<int>("ContratoDestinoID") : 0 },
                             FolioTraspaso = info.Field<long>("FolioTraspaso"),
                             AlmacenOrigen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenOrigenID") },
                             AlmacenDestino = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenDestinoID") },
                             AlmacenInventarioLoteOrigen = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int?>("InventarioLoteOrigenID") != null ? info.Field<int>("InventarioLoteOrigenID") : 0 },
                             AlmacenInventarioLoteDestino = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int?>("InventarioLoteDestinoID") != null ? info.Field<int>("InventarioLoteDestinoID") : 0 },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int?>("CuentaSAPID") != null ? info.Field<int>("CuentaSAPID") : 0},
                             Justificacion = info.Field<string>("Justificacion"),
                             AlmacenMovimientoOrigen = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoSalidaID") },
                             AlmacenMovimientoDestino = new AlmacenMovimientoInfo { AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoEntradaID") },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento")
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
        /// Método que obtiene un traspaso de materia prima por su folio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static TraspasoMpPaMedInfo ObtenerPorFolio(DataSet ds)
        {
            try
            {
                var contratoDAL = new ContratoDAL();
                var almacenDAL = new AlmacenDAL();
                var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();
                var cuentaSAPDAL = new CuentaSAPDAL();
                var productoDAL = new ProductoDAL();
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TraspasoMpPaMedInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new TraspasoMpPaMedInfo
                         {
                             TraspasoMateriaPrimaID = info.Field<int>("TraspasoMateriaPrimaID"),
                             ContratoOrigen = info.Field<int?>("ContratoOrigenID") != null ? contratoDAL.ObtenerPorId(new ContratoInfo { ContratoId = info.Field<int>("ContratoOrigenID") }) : new ContratoInfo(),
                             ContratoDestino = info.Field<int?>("ContratoDestinoID") != null ? contratoDAL.ObtenerPorId(new ContratoInfo { ContratoId = info.Field<int>("ContratoDestinoID") }) : new ContratoInfo(),
                             FolioTraspaso = info.Field<long>("FolioTraspaso"),
                             AlmacenOrigen = almacenDAL.ObtenerPorID(info.Field<int>("AlmacenOrigenID")),
                             AlmacenDestino = almacenDAL.ObtenerPorID(info.Field<int>("AlmacenDestinoID")),
                             LoteMpOrigen = info.Field<int?>("InventarioLoteOrigenID") != null ? almacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorId(info.Field<int>("InventarioLoteOrigenID")) : new AlmacenInventarioLoteInfo(),
                             LoteMpDestino = info.Field<int?>("InventarioLoteDestinoID") != null ? almacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorId(info.Field<int>("InventarioLoteDestinoID")) : new AlmacenInventarioLoteInfo(),
                             CuentaContable = info.Field<int?>("CuentaSAPID") != null ? cuentaSAPDAL.ObtenerPorID(info.Field<int>("CuentaSAPID")) : new CuentaSAPInfo(),
                             JustificacionDestino = info.Field<string>("Justificacion"),
                             CantidadTraspasarOrigen = info.Field<decimal>("CantidadSalida"),
                             CantidadTraspasarDestino = info.Field<decimal>("CantidadEntrada"),
                             ProductoOrigen = productoDAL.ObtenerPorID(new ProductoInfo { ProductoId = info.Field<int>("ProductoID") }),
                             FechaTraspaso = info.Field<DateTime>("FechaMovimiento")
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

