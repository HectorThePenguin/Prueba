using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using BLToolkit.ServiceModel;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapConciliacionDAL
    {
        /// <summary>
        /// Obtiene una lista de pases a proceso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PolizaPaseProcesoModel> ObtenerConciliacionPaseProceso(DataSet ds)
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
                    lista = (from lst in lista.DefaultIfEmpty()
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
        /// metodo que obtiene una lista ConciliacionInfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PolizasIncorrectasInfo> ConciliacionTipoPoliza(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PolizasIncorrectasInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                        new PolizasIncorrectasInfo
                        {
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            FolioMovto = info.Field<string>("FolioMovto"),
                            FechaDocto = info.Field<string>("FechaDocto"),
                            Concepto = info.Field<string>("Concepto"),
                            Ref3 = info.Field<string>("Ref3"),
                            Cargos = info.Field<decimal>("Cargos"),
                            Abonos = info.Field<decimal>("Abonos"),
                            DocumentoSAP = info.Field<string>("DocumentoSAP"),
                            Procesada = info.Field<bool>("Procesada"),
                            Mensaje = info.Field<string>("Mensaje"),
                            PolizaID = info.Field<int>("PolizaID"),
                            TipoPolizaID = info.Field<int>("TipoPolizaID"),
                            TipoPoliza = info.Field<string>("TipoPoliza")
                        }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<ConciliacionInfo> ConciliacionDetalle(DataSet ds)
        {
        try
        {
            Logger.Info();
            DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
            List<ConciliacionInfo> lista = 
                (from info in dt.AsEnumerable()
                     select 
                        new ConciliacionInfo
                        {
                            NoRef = info.Field<string>("NoRef"),
                            FechaDocto = info.Field<string>("FechaDocto"),
                            FechaCont = info.Field<string>("FechaCont"),
                            ClaseDoc = info.Field<string>("ClaseDoc"),
                            Sociedad = info.Field<string>("Sociedad"),
                            Moneda = info.Field<string>("Moneda"),
                            TipoCambio = info.Field<string>("TipoCambio"),
                            TextoDocto = info.Field<string>("TextoDocto"),
                            Mes = info.Field<string>("Mes"),
                            Cuenta = info.Field<string>("Cuenta"),
                            Proveedor = info.Field<string>("Proveedor"),
                            Cliente = info.Field<string>("Cliente"),
                            Importe = info.Field<string>("Importe"),
                            Concepto = info.Field<string>("Concepto"),
                            Division = info.Field<string>("Division"),
                            NoLinea = info.Field<string>("NoLinea"),
                            Ref3 = info.Field<string>("Ref3"),
                            ArchivoFolio = info.Field<string>("ArchivoFolio"),
                            DocumentoSAP = info.Field<string>("DocumentoSAP"),
                            DocumentoCancelacionSAP = info.Field<string>("DocumentoCancelacionSAP"),
                            Segmento = info.Field<string>("Segmento"),
                            OrganizacionID = info.Field<int>("OrganizacionID"),
                            Conciliada = info.Field<bool>("Conciliada"),
                            Procesada = info.Field<bool>("Procesada"),
                            Cancelada = info.Field<bool>("Cancelada"),
                            Mensaje = info.Field<string>("Mensaje"),
                            PolizaID = info.Field<int>("PolizaID"),
                            TipoPolizaID = info.Field<int>("TipoPolizaID"),
                            TipoPoliza = info.Field<string>("TipoPoliza")
                        }).ToList();
            return lista;
        }
        catch(Exception ex)
        {
            Logger.Error(ex);
            throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
        }
        }


        /// <summary>
        /// Obtiene una lista con los movimientos del almacen
        /// para realizar la conciliacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ConciliacionMovimientosAlmacenModel ObtenerMovimientosAlmacenConciliacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                var lista = new ConciliacionMovimientosAlmacenModel();
                lista.AlmacenesMovimientos = (from info in dt.AsEnumerable()
                                              select new AlmacenMovimientoInfo
                                                         {
                                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                                             AlmacenMovimientoID =
                                                                 info.Field<long>("AlmacenMovimientoID"),
                                                             AlmacenID = info.Field<int>("AlmacenID"),
                                                             Almacen = new AlmacenInfo
                                                                           {
                                                                               AlmacenID = info.Field<int>("AlmacenID"),
                                                                               //Descripcion = info.Field<string>("Almacen")
                                                                               Organizacion = new OrganizacionInfo
                                                                                                  {
                                                                                                      OrganizacionID =
                                                                                                          info.Field
                                                                                                          <int>(
                                                                                                              "OrganizacionID"),
                                                                                                  }
                                                                           },
                                                             ProveedorId = info.Field<int?>("ProveedorID") ?? 0,
                                                             FolioMovimiento = info.Field<long>("FolioMovimiento"),
                                                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                                             Observaciones = info.Field<string>("Observaciones"),
                                                             Status = info.Field<int>("Status"),
                                                             AnimalMovimientoID =
                                                                 info.Field<long?>("AnimalMovimientoID") ?? 0,
                                                             TipoMovimiento = new TipoMovimientoInfo
                                                                                  {
                                                                                      TipoMovimientoID =
                                                                                          info.Field<int>(
                                                                                              "TipoMovimientoID"),
                                                                                      Descripcion =
                                                                                          info.Field<string>(
                                                                                              "TipoMovimiento"),
                                                                                      EsEntrada =
                                                                                          info.Field<bool>("EsEntrada"),
                                                                                      EsGanado =
                                                                                          info.Field<bool>("EsGanado"),
                                                                                      EsProducto =
                                                                                          info.Field<bool>("EsProducto"),
                                                                                      EsSalida =
                                                                                          info.Field<bool>("EsSalida"),
                                                                                      TipoPoliza = new TipoPolizaInfo
                                                                                                       {
                                                                                                           TipoPolizaID
                                                                                                               =
                                                                                                               info.
                                                                                                               Field
                                                                                                               <int>(
                                                                                                                   "TipoPolizaID")
                                                                                                       }
                                                                                  },
                                                         }).ToList();
                lista.AlmacenesMovimientosDetalle = (from det in dtDetalle.AsEnumerable()
                                                     select new AlmacenMovimientoDetalle
                                                                {
                                                                    AlmacenMovimientoDetalleID =
                                                                        det.Field<long>(
                                                                            "AlmacenMovimientoDetalleID"),
                                                                    AlmacenMovimientoID =
                                                                        det.Field<long>("AlmacenMovimientoID"),
                                                                    AlmacenInventarioLoteId =
                                                                        det.Field<int?>("AlmacenInventarioLoteID") ??
                                                                        0,
                                                                    ContratoId =
                                                                        det.Field<int?>("ContratoID") ?? 0,
                                                                    Piezas = det.Field<int>("Piezas"),
                                                                    TratamientoID =
                                                                        det.Field<int?>("TratamientoID") ?? 0,
                                                                    ProductoID = det.Field<int>("ProductoID"),
                                                                    Precio = det.Field<decimal>("Precio"),
                                                                    Cantidad = det.Field<decimal>("Cantidad"),
                                                                    Importe = det.Field<decimal>("Importe"),
                                                                    Tratamiento = new TratamientoInfo
                                                                                      {
                                                                                          TratamientoID = det.Field<int>("TratamientoID"),
                                                                                          TipoTratamientoInfo = new TipoTratamientoInfo
                                                                                                                    {
                                                                                                                        TipoTratamientoID = det.Field<int>("TipoTratamientoID"),
                                                                                                                        Descripcion = det.Field<string>("TipoTratamiento")
                                                                                                                    }
                                                                                      },
                                                                    FechaCreacion = det.Field<DateTime>("FechaCreacion")
                                                                }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        internal static List<ContenedorEntradaMateriaPrimaInfo> ObtenerEntradaMateriaPrimaConciliacion(DataSet ds)
        {
            List<ContenedorEntradaMateriaPrimaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtCostos = ds.Tables[ConstantesDAL.DtCostos];
                DataTable dtCostosEntrada = ds.Tables[ConstantesDAL.DtCostosEntrada];

                resultado = (from info in dt.AsEnumerable()
                             select new ContenedorEntradaMateriaPrimaInfo
                             {
                                 Contrato = new ContratoInfo
                                 {
                                     ContratoId = info.Field<int?>("ContratoID") ?? 0,
                                     TipoContrato = new TipoContratoInfo
                                     {
                                         TipoContratoId =
                                             info.Field<int?>(
                                                 "TipoContratoId") ?? 0
                                     },
                                     Organizacion = new OrganizacionInfo
                                     {
                                         OrganizacionID =
                                             info.Field<int?>(
                                                 "OrganizacionID") ?? 0
                                     },
                                     Folio = info.Field<int?>("Folio") ?? 0,
                                     Proveedor = new ProveedorInfo
                                     {
                                         ProveedorID = info.Field<int?>("ProveedorID") ?? 0,
                                         CodigoSAP = info.Field<string>("CodigoSAP"),
                                         Descripcion = info.Field<string>("Proveedor")
                                     },
                                     PesoNegociar = info.Field<string>("PesoNegociar"),
                                     TipoCambio = new TipoCambioInfo
                                     {
                                         TipoCambioId = info.Field<int?>("TipoCambioID") ?? 0
                                     },
                                     Cuenta = new CuentaSAPInfo
                                     {
                                         CuentaSAPID = info.Field<int?>("CuentaSAPID") ?? 0
                                     }
                                 },
                                 EntradaProducto = new EntradaProductoInfo
                                 {
                                     Folio = info.Field<int>("Folio"),
                                     Producto = new ProductoInfo
                                     {
                                         ProductoId =
                                             info.Field<int>(
                                                 "ProductoID"),
                                         Descripcion =
                                             info.Field<string>(
                                                 "Producto"),
                                         UnidadMedicion =
                                             new UnidadMedicionInfo
                                             {
                                                 UnidadID =
                                                     info.Field<int>
                                                     ("UnidadID")
                                             }
                                     },
                                     Organizacion = new OrganizacionInfo
                                     {
                                         OrganizacionID =
                                             info.Field<int>(
                                                 "OrganizacionID")
                                     },
                                     Fecha = info.Field<DateTime>("Fecha"),
                                     Observaciones =
                                         info.Field<string>("Observaciones"),
                                     PesoOrigen = info.Field<int>("PesoOrigen"),
                                     PesoBruto = info.Field<int>("PesoBruto"),
                                     PesoTara = info.Field<int>("PesoTara"),
                                     PesoDescuento = info.Field<int>("PesoDescuento"),
                                     PesoBonificacion = info.Field<int>("PesoBonificacion"),
                                     AlmacenMovimiento = new AlmacenMovimientoInfo
                                     {
                                         AlmacenMovimientoID =
                                             info.Field<long>(
                                                 "AlmacenMovimientoID"),
                                         Almacen =
                                             new AlmacenInfo
                                             {
                                                 AlmacenID
                                                     =
                                                     info.
                                                     Field
                                                     <int>(
                                                         "AlmacenID")
                                             },
                                         FolioMovimiento =
                                             info.Field<long>(
                                                 "FolioMovimiento"),
                                         FechaMovimiento =
                                             info.Field
                                             <DateTime>(
                                                 "FechaMovimiento"),
                                         ProveedorId =
                                             info.Field<int>(
                                                 "ProveedorID"),
                                     },
                                     AlmacenInventarioLote =
                                         new AlmacenInventarioLoteInfo
                                         {
                                             Lote = info.Field<int>("Lote")
                                         }
                                 },
                             }).ToList();
                List<AlmacenMovimientoDetalle> almacenMovimientoDetalles = (from det in dtDetalle.AsEnumerable()
                                                                            select new AlmacenMovimientoDetalle
                                                                                       {
                                                                                           Producto = new ProductoInfo
                                                                                                          {
                                                                                                              ProductoId
                                                                                                                  =
                                                                                                                  det.
                                                                                                                  Field
                                                                                                                  <int>(
                                                                                                                      "ProductoID")
                                                                                                          },
                                                                                           Precio =
                                                                                               det.Field<decimal>(
                                                                                                   "Precio"),
                                                                                           Cantidad =
                                                                                               det.Field<decimal>(
                                                                                                   "Cantidad"),
                                                                                           Importe =
                                                                                               det.Field<decimal>(
                                                                                                   "Importe"),
                                                                                           AlmacenMovimientoID =
                                                                                               det.Field<long>(
                                                                                                   "AlmacenMovimientoID")
                                                                                       }).ToList();
                resultado.ForEach(dato =>
                                      {
                                          dato.EntradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle =
                                              almacenMovimientoDetalles.Where(
                                                  id =>
                                                  id.AlmacenMovimientoID ==
                                                  dato.EntradaProducto.AlmacenMovimiento.AlmacenMovimientoID).ToList();
                                      });
                IList<CostoEntradaMateriaPrimaInfo> costos = (from costo in dtCostos.AsEnumerable()
                                                              join costoEntrada in dtCostosEntrada.AsEnumerable() on
                                                                  costo.Field<int>("CostoID") equals
                                                                  costoEntrada.Field<int>("CostoID")
                                                              where
                                                                  costo.Field<long>("AlmacenMovimientoID") ==
                                                                  costoEntrada.Field<long?>("AlmacenMovimientoID")
                                                              select new CostoEntradaMateriaPrimaInfo
                                                                         {
                                                                             Provedor = new ProveedorInfo
                                                                                            {
                                                                                                ProveedorID =
                                                                                                    costo.Field<int?>(
                                                                                                        "ProveedorID") ??
                                                                                                    0,
                                                                                                Descripcion =
                                                                                                    costoEntrada.Field
                                                                                                    <string>(
                                                                                                        "Descripcion"),
                                                                                                CodigoSAP =
                                                                                                    costoEntrada.Field
                                                                                                    <string>("CodigoSAP")
                                                                                            },
                                                                             Costos = new CostoInfo
                                                                                          {
                                                                                              CostoID =
                                                                                                  costo.Field<int>(
                                                                                                      "CostoID")
                                                                                          },
                                                                             Importe = costo.Field<decimal>("Importe"),
                                                                             TieneCuenta =
                                                                                 costoEntrada.Field<bool>("TieneCuenta"),
                                                                             Iva = costoEntrada.Field<bool>("Iva"),
                                                                             Retencion =
                                                                                 costoEntrada.Field<bool>("Retencion"),
                                                                             Observaciones =
                                                                                 costoEntrada.Field<string>(
                                                                                     "Observaciones"),
                                                                             CuentaSap =
                                                                                 costoEntrada.Field<string>(
                                                                                     "CuentaProvision"),
                                                                             AlmacenMovimientoID =
                                                                                 costoEntrada.Field<long?>(
                                                                                     "AlmacenMovimientoID") ?? 0
                                                                         }).ToList();
                costos =
                    costos.GroupBy(grupo => new {grupo.Costos.CostoID, grupo.Importe, grupo.Provedor.ProveedorID}).
                        Select(x => new CostoEntradaMateriaPrimaInfo
                                        {
                                            Provedor = new ProveedorInfo
                                                           {
                                                               ProveedorID = x.Key.ProveedorID,
                                                               Descripcion =
                                                                   x.Select(prov => prov.Provedor.Descripcion).
                                                                   FirstOrDefault(),
                                                               CodigoSAP =
                                                                   x.Select(prov => prov.Provedor.CodigoSAP).
                                                                   FirstOrDefault()
                                                           },
                                            Costos = new CostoInfo
                                                         {
                                                             CostoID = x.Key.CostoID
                                                         },
                                            Importe = x.Select(imp => imp.Importe).FirstOrDefault(),
                                            TieneCuenta = x.Select(cuenta => cuenta.TieneCuenta).FirstOrDefault(),
                                            Iva = x.Select(iva => iva.Iva).FirstOrDefault(),
                                            Retencion = x.Select(ret => ret.Retencion).FirstOrDefault(),
                                            Observaciones = x.Select(obs => obs.Observaciones).FirstOrDefault(),
                                            CuentaSap = x.Select(cuenta => cuenta.CuentaSap).FirstOrDefault(),
                                            AlmacenMovimientoID = x.Select(alm => alm.AlmacenMovimientoID).FirstOrDefault()
                                        }).ToList();
                var hashCostos = new HashSet<CostoEntradaMateriaPrimaInfo>(costos);
                resultado.ForEach(dato =>
                                      {
                                          dato.ListaCostoEntradaMateriaPrima = new ObservableCollection
                                              <CostoEntradaMateriaPrimaInfo>(
                                              hashCostos.Where(
                                                  id =>
                                                  id.AlmacenMovimientoID ==
                                                  dato.EntradaProducto.AlmacenMovimiento.AlmacenMovimientoID).ToList());
                                      });
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
