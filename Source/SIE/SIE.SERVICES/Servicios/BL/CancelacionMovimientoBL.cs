
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections;
using System.Reflection;
using System.Transactions;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.Generic;


namespace SIE.Services.Servicios.BL
{
    internal class CancelacionMovimientoBL
    {
        /// <summary>
        /// Funcion que cancela un movimiento de entrada por compra
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="justificacion"></param>
        /// <returns></returns>
        internal bool CancelarEntradaCompra(EntradaProductoInfo entradaProducto, string justificacion)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    AlmacenMovimientoBL almacenMovimientoBl = new AlmacenMovimientoBL();
                    AlmacenMovimientoInfo almacenMovimientoCancelacion = new AlmacenMovimientoInfo();

                    almacenMovimientoCancelacion.AlmacenID = entradaProducto.AlmacenMovimiento.AlmacenID;
                    almacenMovimientoCancelacion.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionEntradaCompra.GetHashCode() };
                    almacenMovimientoCancelacion.TipoMovimientoID = TipoMovimiento.CancelacionEntradaCompra.GetHashCode();
                    almacenMovimientoCancelacion.Observaciones = justificacion;
                    almacenMovimientoCancelacion.Status = Estatus.AplicadoInv.GetHashCode();
                    almacenMovimientoCancelacion.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                    if (entradaProducto.Contrato != null)
                    {
                        almacenMovimientoCancelacion.ProveedorId = entradaProducto.Contrato.Proveedor.ProveedorID;
                    }

                    long movimientoCancelacionId = almacenMovimientoBl.Crear(almacenMovimientoCancelacion);

                    AlmacenMovimientoDetalle almacenMovimientoCancelacionDetalle = new AlmacenMovimientoDetalle();

                    almacenMovimientoCancelacionDetalle.AlmacenMovimientoID = movimientoCancelacionId;
                    almacenMovimientoCancelacionDetalle.Producto = entradaProducto.Producto;
                    almacenMovimientoCancelacionDetalle.ProductoID = entradaProducto.Producto.ProductoId;
                    almacenMovimientoCancelacionDetalle.AlmacenInventarioLoteId = entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId;
                    almacenMovimientoCancelacionDetalle.Precio = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Precio;
                    almacenMovimientoCancelacionDetalle.Cantidad = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad;
                    almacenMovimientoCancelacionDetalle.Importe = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe;
                    almacenMovimientoCancelacionDetalle.Piezas = entradaProducto.Piezas;
                    almacenMovimientoCancelacionDetalle.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                    if (entradaProducto.Contrato != null)
                    {
                        almacenMovimientoCancelacionDetalle.ContratoId = entradaProducto.Contrato.ContratoId;
                    }
                    AlmacenMovimientoDetalleBL almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                    almacenMovimientoDetalleBl.Crear(almacenMovimientoCancelacionDetalle);

                    AlmacenInventarioLoteBL almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                    entradaProducto.AlmacenInventarioLote = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);

                    entradaProducto.AlmacenInventarioLote.Cantidad = entradaProducto.AlmacenInventarioLote.Cantidad - entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad;
                    decimal costos = 0;
                    if (entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoCosto != null)
                    {
                        costos = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoCosto.Sum(registro => registro.Importe);
                    }
                    entradaProducto.AlmacenInventarioLote.Importe = entradaProducto.AlmacenInventarioLote.Importe - entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe - costos;

                    if (entradaProducto.AlmacenInventarioLote.Importe > 0 && entradaProducto.AlmacenInventarioLote.Cantidad > 0)
                    {
                        entradaProducto.AlmacenInventarioLote.PrecioPromedio = entradaProducto.AlmacenInventarioLote.Importe / entradaProducto.AlmacenInventarioLote.Cantidad;
                    }
                    else
                    {
                        entradaProducto.AlmacenInventarioLote.PrecioPromedio = 0;
                    }
                    entradaProducto.AlmacenInventarioLote.UsuarioModificacionId = entradaProducto.UsuarioCreacionID;

                    almacenInventarioLoteBl.Actualizar(entradaProducto.AlmacenInventarioLote);

                    AlmacenInventarioInfo almacenInventario = new AlmacenInventarioInfo();
                    AlmacenInventarioBL almacenInventarioBl = new AlmacenInventarioBL();
                    almacenInventario = almacenInventarioBl.ObtenerAlmacenInventarioPorId(entradaProducto.AlmacenInventarioLote.AlmacenInventario.AlmacenInventarioID);

                    List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventario);
                    almacenInventario.Cantidad = listaAlmacenInventarioLote.Sum(registro => registro.Cantidad);
                    almacenInventario.Importe = listaAlmacenInventarioLote.Sum(registro => registro.Importe);

                    if (almacenInventario.Importe > 0 && almacenInventario.Cantidad > 0)
                    {
                        almacenInventario.PrecioPromedio = almacenInventario.Importe / almacenInventario.Cantidad;
                    }
                    else
                    {
                        almacenInventario.PrecioPromedio = 0;
                    }
                    almacenInventario.UsuarioModificacionID = entradaProducto.UsuarioCreacionID;
                    almacenInventarioBl.Actualizar(almacenInventario);

                    CancelacionMovimientoDAL cancelacionMovimientoDal = new CancelacionMovimientoDAL();
                    CancelacionMovimientoInfo cancelacionMovimiento = new CancelacionMovimientoInfo();
                    cancelacionMovimiento.Pedido = new PedidoInfo();
                    cancelacionMovimiento.TipoCancelacion = new TipoCancelacionInfo() { TipoCancelacionId = TipoCancelacionEnum.EntradaCompra.GetHashCode() };
                    cancelacionMovimiento.AlmacenMovimientoOrigen = entradaProducto.AlmacenMovimiento;
                    cancelacionMovimiento.AlmacenMovimientoCancelado = new AlmacenMovimientoInfo() { AlmacenMovimientoID = movimientoCancelacionId };
                    cancelacionMovimiento.Justificacion = justificacion;
                    cancelacionMovimiento.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;

                    EntradaProductoDAL entradaProductoDal = new EntradaProductoDAL();
                    entradaProductoDal.Cancelar(entradaProducto);

                    cancelacionMovimientoDal.Crear(cancelacionMovimiento);

                    transaction.Complete();
                    return true;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Cancela una entrada por traspaso
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="justificacion"></param>
        /// <returns></returns>
        internal bool CancelarEntradaTraspaso(EntradaProductoInfo entradaProducto, string justificacion)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    AlmacenMovimientoBL almacenMovimientoBl = new AlmacenMovimientoBL();
                    AlmacenMovimientoInfo almacenMovimientoCancelacion = new AlmacenMovimientoInfo();
                    almacenMovimientoCancelacion.AlmacenID = entradaProducto.AlmacenMovimiento.AlmacenID;
                    almacenMovimientoCancelacion.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionEntradaTraspaso.GetHashCode() };
                    almacenMovimientoCancelacion.TipoMovimientoID = TipoMovimiento.CancelacionEntradaTraspaso.GetHashCode();
                    almacenMovimientoCancelacion.Observaciones = justificacion;
                    almacenMovimientoCancelacion.Status = Estatus.AplicadoInv.GetHashCode();
                    almacenMovimientoCancelacion.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                    if (entradaProducto.Contrato != null)
                    {
                        almacenMovimientoCancelacion.ProveedorId = entradaProducto.Contrato.Proveedor.ProveedorID;
                    }

                    long movimientoCancelacionId = almacenMovimientoBl.Crear(almacenMovimientoCancelacion);

                    AlmacenMovimientoDetalle almacenMovimientoCancelacionDetalle = new AlmacenMovimientoDetalle();
                    almacenMovimientoCancelacionDetalle.AlmacenMovimientoID = movimientoCancelacionId;
                    almacenMovimientoCancelacionDetalle.Producto = entradaProducto.Producto;
                    almacenMovimientoCancelacionDetalle.ProductoID = entradaProducto.Producto.ProductoId;
                    almacenMovimientoCancelacionDetalle.AlmacenInventarioLoteId = entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId;
                    almacenMovimientoCancelacionDetalle.Precio = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Precio;
                    almacenMovimientoCancelacionDetalle.Cantidad = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad;
                    almacenMovimientoCancelacionDetalle.Importe = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe;
                    almacenMovimientoCancelacionDetalle.Piezas = entradaProducto.Piezas;
                    almacenMovimientoCancelacionDetalle.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                    if (entradaProducto.Contrato != null)
                    {
                        almacenMovimientoCancelacionDetalle.ContratoId = entradaProducto.Contrato.ContratoId;
                    }

                    AlmacenMovimientoDetalleBL almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                    almacenMovimientoDetalleBl.Crear(almacenMovimientoCancelacionDetalle);

                    AlmacenInventarioLoteBL almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                    entradaProducto.AlmacenInventarioLote = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(entradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);

                    entradaProducto.AlmacenInventarioLote.Cantidad = entradaProducto.AlmacenInventarioLote.Cantidad - entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad;
                    decimal costos = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoCosto.Sum(registro => registro.Importe);
                    entradaProducto.AlmacenInventarioLote.Importe = entradaProducto.AlmacenInventarioLote.Importe - entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe - costos;
                    if (entradaProducto.AlmacenInventarioLote.Importe > 0 && entradaProducto.AlmacenInventarioLote.Cantidad > 0)
                    {
                        entradaProducto.AlmacenInventarioLote.PrecioPromedio = entradaProducto.AlmacenInventarioLote.Importe / entradaProducto.AlmacenInventarioLote.Cantidad;
                    }
                    else
                    {
                        entradaProducto.AlmacenInventarioLote.PrecioPromedio = 0;
                    }

                    entradaProducto.AlmacenInventarioLote.UsuarioModificacionId = entradaProducto.UsuarioCreacionID;

                    almacenInventarioLoteBl.Actualizar(entradaProducto.AlmacenInventarioLote);

                    AlmacenInventarioInfo almacenInventario = new AlmacenInventarioInfo();
                    AlmacenInventarioBL almacenInventarioBl = new AlmacenInventarioBL();
                    almacenInventario = almacenInventarioBl.ObtenerAlmacenInventarioPorId(entradaProducto.AlmacenInventarioLote.AlmacenInventario.AlmacenInventarioID);

                    List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventario);
                    almacenInventario.Cantidad = listaAlmacenInventarioLote.Sum(registro => registro.Cantidad);
                    almacenInventario.Importe = listaAlmacenInventarioLote.Sum(registro => registro.Importe);

                    if (almacenInventario.Importe > 0 && almacenInventario.Cantidad > 0)
                    {
                        almacenInventario.PrecioPromedio = almacenInventario.Importe / almacenInventario.Cantidad;
                    }
                    almacenInventario.UsuarioModificacionID = entradaProducto.UsuarioCreacionID;
                    almacenInventarioBl.Actualizar(almacenInventario);

                    CancelacionMovimientoDAL cancelacionMovimientoDal = new CancelacionMovimientoDAL();
                    CancelacionMovimientoInfo cancelacionMovimiento = new CancelacionMovimientoInfo();
                    cancelacionMovimiento.Pedido = new PedidoInfo();
                    cancelacionMovimiento.TipoCancelacion = new TipoCancelacionInfo() { TipoCancelacionId = TipoCancelacionEnum.EntradaTraspaso.GetHashCode() };
                    cancelacionMovimiento.AlmacenMovimientoOrigen = entradaProducto.AlmacenMovimiento;
                    cancelacionMovimiento.AlmacenMovimientoCancelado = new AlmacenMovimientoInfo() { AlmacenMovimientoID = movimientoCancelacionId };
                    cancelacionMovimiento.Justificacion = justificacion;
                    cancelacionMovimiento.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;

                    EntradaProductoDAL entradaProductoDal = new EntradaProductoDAL();
                    entradaProductoDal.Cancelar(entradaProducto);
                    AlmacenMovimientoCostoBL almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                    if (entradaProducto.Producto.SubFamilia.SubFamiliaID == SubFamiliasEnum.MicroIngredientes.GetHashCode())
                    {
                        EntradaPremezclaBL entradaPremezclaBl = new EntradaPremezclaBL();
                        List<EntradaPremezclaInfo> listaEntradaPremezcla= entradaPremezclaBl.ObtenerPorMovimientoEntrada(entradaProducto.AlmacenMovimiento);

                        foreach(EntradaPremezclaInfo entradaPremezcla in listaEntradaPremezcla){
                            AlmacenMovimientoInfo almacenMovimientoEntradaPremezcla = almacenMovimientoBl.ObtenerPorId(entradaPremezcla.AlmacenMovimientoIDSalida);

                            AlmacenMovimientoInfo almacenMovimientoSalida = new AlmacenMovimientoInfo();
                            almacenMovimientoSalida.AlmacenID = almacenMovimientoEntradaPremezcla.AlmacenID;
                            almacenMovimientoSalida.TipoMovimientoID = TipoMovimiento.CancelacionEntradaTraspaso.GetHashCode();
                            almacenMovimientoSalida.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionEntradaTraspaso.GetHashCode() };
                            almacenMovimientoSalida.Status = Estatus.AplicadoInv.GetHashCode();
                            almacenMovimientoSalida.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                            long almacenmovimientoSalidaId = almacenMovimientoBl.Crear(almacenMovimientoSalida);

                            AlmacenMovimientoDetalle almacenMovimientoSalidaDetalle = new AlmacenMovimientoDetalle();
                            almacenMovimientoSalidaDetalle.AlmacenMovimientoID = almacenmovimientoSalidaId;
                            almacenMovimientoSalidaDetalle.Producto = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Producto;
                            almacenMovimientoSalidaDetalle.ProductoID = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Producto.ProductoId;
                            almacenMovimientoSalidaDetalle.Precio = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Precio;
                            almacenMovimientoSalidaDetalle.Cantidad = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Cantidad;
                            almacenMovimientoSalidaDetalle.Importe = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Importe;
                            almacenMovimientoSalidaDetalle.AlmacenInventarioLoteId = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].AlmacenInventarioLoteId;
                            almacenMovimientoSalidaDetalle.Piezas = almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Piezas;
                            almacenMovimientoSalidaDetalle.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                            almacenMovimientoDetalleBl.Crear(almacenMovimientoSalidaDetalle);

                            AlmacenInventarioLoteInfo almacenInventarioLoteTerceros = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].AlmacenInventarioLoteId);
                            almacenInventarioLoteTerceros.Cantidad = almacenInventarioLoteTerceros.Cantidad + almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Cantidad;
                            almacenInventarioLoteTerceros.Importe = almacenInventarioLoteTerceros.Importe + almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoDetalle[0].Importe + almacenMovimientoEntradaPremezcla.ListaAlmacenMovimientoCosto.Sum(registro => registro.Importe);

                            if (almacenInventarioLoteTerceros.Importe > 0 && almacenInventarioLoteTerceros.Cantidad > 0)
                            {
                                almacenInventarioLoteTerceros.PrecioPromedio = almacenInventarioLoteTerceros.Importe / almacenInventarioLoteTerceros.Cantidad;
                            }
                            else
                            {
                                almacenInventarioLoteTerceros.PrecioPromedio = 0;
                            }
                            almacenInventarioLoteTerceros.UsuarioCreacionId = entradaProducto.UsuarioCreacionID;
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLoteTerceros);

                            AlmacenInventarioInfo almacenInventarioSalida = almacenInventarioBl.ObtenerAlmacenInventarioPorId(almacenInventarioLoteTerceros.AlmacenInventario.AlmacenInventarioID);
                            List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLoteSalida = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventarioSalida);
                            almacenInventarioSalida.Cantidad = listaAlmacenInventarioLoteSalida.Sum(registro => registro.Cantidad);
                            almacenInventarioSalida.Importe = listaAlmacenInventarioLoteSalida.Sum(registro => registro.Importe);

                            if (almacenInventarioSalida.Importe > 0 && almacenInventarioSalida.Cantidad > 0)
                            {
                                almacenInventarioSalida.PrecioPromedio = almacenInventarioSalida.Importe / almacenInventarioSalida.Cantidad;
                            }
                            else
                            {
                                almacenInventarioSalida.PrecioPromedio = 0;
                            }
                            almacenInventarioSalida.UsuarioModificacionID = entradaProducto.UsuarioCreacionID;
                            almacenInventarioBl.Actualizar(almacenInventarioSalida);
                        }
                    }
                    else
                    {
                        
                        AlmacenMovimientoInfo almacenMovimientoSalida = new AlmacenMovimientoInfo();
                        almacenMovimientoSalida.AlmacenID = entradaProducto.AlmacenMovimientoSalida.AlmacenID;
                        almacenMovimientoSalida.TipoMovimientoID = TipoMovimiento.CancelacionEntradaTraspaso.GetHashCode();
                        almacenMovimientoSalida.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionEntradaTraspaso.GetHashCode()};
                        almacenMovimientoSalida.Status = Estatus.AplicadoInv.GetHashCode();
                        almacenMovimientoSalida.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                        if (entradaProducto.Contrato.ContratoId > 0)
                        {
                            almacenMovimientoSalida.ProveedorId = entradaProducto.Contrato.Proveedor.ProveedorID;
                        }
                        
                        long almacenmovimientoSalidaId = almacenMovimientoBl.Crear(almacenMovimientoSalida);

                        entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle>();
                        entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoDetalle.Add(almacenMovimientoDetalleBl.ObtenerPorAlmacenMovimientoID(entradaProducto.AlmacenMovimientoSalida.AlmacenMovimientoID));
                        entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoCosto = almacenMovimientoCostoBl.ObtenerPorAlmacenMovimientoId(entradaProducto.AlmacenMovimientoSalida);
                        AlmacenMovimientoDetalle almacenMovimientoSalidaDetalle = new AlmacenMovimientoDetalle();
                        almacenMovimientoSalidaDetalle.AlmacenMovimientoID = almacenmovimientoSalidaId;
                        almacenMovimientoSalidaDetalle.Producto = entradaProducto.Producto;
                        almacenMovimientoSalidaDetalle.ProductoID = entradaProducto.Producto.ProductoId;
                        almacenMovimientoSalidaDetalle.Precio = entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoDetalle[0].Precio;
                        almacenMovimientoSalidaDetalle.Cantidad = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad;
                        almacenMovimientoSalidaDetalle.Importe = entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe;
                        almacenMovimientoSalidaDetalle.AlmacenInventarioLoteId = entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoDetalle[0].AlmacenInventarioLoteId;
                        almacenMovimientoSalidaDetalle.ContratoId = entradaProducto.Contrato.ContratoId;
                        almacenMovimientoSalidaDetalle.Piezas = entradaProducto.Piezas;
                        almacenMovimientoSalidaDetalle.UsuarioCreacionID = entradaProducto.UsuarioCreacionID;
                        almacenMovimientoDetalleBl.Crear(almacenMovimientoSalidaDetalle);

                        AlmacenInventarioLoteInfo almacenInventarioLoteTerceros = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoDetalle[0].AlmacenInventarioLoteId);
                        almacenInventarioLoteTerceros.Cantidad = almacenInventarioLoteTerceros.Cantidad + entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Cantidad;

                        if (entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoCosto != null)
                        {
                            almacenInventarioLoteTerceros.Importe = almacenInventarioLoteTerceros.Importe + entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe + entradaProducto.AlmacenMovimientoSalida.ListaAlmacenMovimientoCosto.Sum(registro => registro.Importe);
                        }
                        else
                        {
                            almacenInventarioLoteTerceros.Importe = almacenInventarioLoteTerceros.Importe + entradaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle[0].Importe;
                        }

                        if (almacenInventarioLoteTerceros.Importe > 0 && almacenInventarioLoteTerceros.Cantidad > 0)
                        {
                            almacenInventarioLoteTerceros.PrecioPromedio = almacenInventarioLoteTerceros.Importe / almacenInventarioLoteTerceros.Cantidad;
                        }
                        else
                        {
                            almacenInventarioLoteTerceros.PrecioPromedio = 0;
                        }
                        almacenInventarioLoteTerceros.UsuarioModificacionId = entradaProducto.UsuarioCreacionID;
                        almacenInventarioLoteBl.Actualizar(almacenInventarioLoteTerceros);

                        AlmacenInventarioInfo almacenInventarioSalida = almacenInventarioBl.ObtenerAlmacenInventarioPorId(almacenInventarioLoteTerceros.AlmacenInventario.AlmacenInventarioID);
                        List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLoteSalida = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventarioSalida);
                        almacenInventarioSalida.Cantidad = listaAlmacenInventarioLoteSalida.Sum(registro => registro.Cantidad);
                        almacenInventarioSalida.Importe = listaAlmacenInventarioLoteSalida.Sum(registro => registro.Importe);

                        if (almacenInventarioSalida.Importe > 0 && almacenInventarioSalida.Cantidad > 0)
                        {
                            almacenInventarioSalida.PrecioPromedio = almacenInventarioSalida.Importe / almacenInventarioSalida.Cantidad;
                        }
                        else
                        {
                            almacenInventarioSalida.PrecioPromedio = 0;
                        }
                        almacenInventarioSalida.UsuarioModificacionID = entradaProducto.UsuarioCreacionID;
                        almacenInventarioBl.Actualizar(almacenInventarioSalida);
                    }

                    cancelacionMovimientoDal.Crear(cancelacionMovimiento);

                    transaction.Complete();
                    return true;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CancelacionMovimiento
        /// </summary>
        /// <param name="info"></param>
        public CancelacionMovimientoInfo Guardar(CancelacionMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                var cancelacionMovimientoDAL = new CancelacionMovimientoDAL();
                CancelacionMovimientoInfo result = null;
                if (info.CancelacionMovimientoId == 0)
                {
                    result = cancelacionMovimientoDAL.Crear(info);
                }
                else
                {
                    cancelacionMovimientoDAL.Actualizar(info);
                    result = info;
                }
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CancelacionMovimientoInfo> ObtenerPorPagina(PaginacionInfo pagina, CancelacionMovimientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var cancelacionMovimientoDAL = new CancelacionMovimientoDAL();
                ResultadoInfo<CancelacionMovimientoInfo> result = cancelacionMovimientoDAL.ObtenerPorPagina(pagina, filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un lista de CancelacionMovimiento
        /// </summary>
        /// <returns></returns>
        public IList<CancelacionMovimientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cancelacionMovimientoDAL = new CancelacionMovimientoDAL();
                IList<CancelacionMovimientoInfo> result = cancelacionMovimientoDAL.ObtenerTodos();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<CancelacionMovimientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cancelacionMovimientoDAL = new CancelacionMovimientoDAL();
                IList<CancelacionMovimientoInfo> result = cancelacionMovimientoDAL.ObtenerTodos(estatus);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad CancelacionMovimiento por su Id
        /// </summary>
        /// <param name="cancelacionMovimientoID">Obtiene una entidad CancelacionMovimiento por su Id</param>
        /// <returns></returns>
        public CancelacionMovimientoInfo ObtenerPorID(int cancelacionMovimientoID)
        {
            try
            {
                Logger.Info();
                var cancelacionMovimientoDAL = new CancelacionMovimientoDAL();
                CancelacionMovimientoInfo result = cancelacionMovimientoDAL.ObtenerPorID(cancelacionMovimientoID);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Cancela una salida por venta o traspaso
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <param name="justificacion"></param>
        /// <returns></returns>
        internal bool CancelarVentaTraspaso(SalidaProductoInfo salidaProducto, string justificacion)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    AlmacenMovimientoBL almacenMovimientoBl = new AlmacenMovimientoBL();
                    AlmacenMovimientoInfo almacenMovimientoCancelacion = new AlmacenMovimientoInfo();
                    almacenMovimientoCancelacion.AlmacenID = salidaProducto.Almacen.AlmacenID;
                    almacenMovimientoCancelacion.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionSalidaVentaTraspaso.GetHashCode()};
                    almacenMovimientoCancelacion.TipoMovimientoID = TipoMovimiento.CancelacionSalidaVentaTraspaso.GetHashCode();
                    almacenMovimientoCancelacion.Observaciones = justificacion;
                    almacenMovimientoCancelacion.Status = Estatus.AplicadoInv.GetHashCode();
                    almacenMovimientoCancelacion.UsuarioCreacionID = salidaProducto.UsuarioCreacionId;

                    long almacenMovimientoId = almacenMovimientoBl.Crear(almacenMovimientoCancelacion);

                    ProductoBL productoBl = new ProductoBL();
                    salidaProducto.AlmacenInventarioLote.AlmacenInventario.Producto = productoBl.ObtenerPorID(new ProductoInfo() { ProductoId = salidaProducto.AlmacenInventarioLote.AlmacenInventario.ProductoID });
                    AlmacenMovimientoDetalleBL almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                    AlmacenMovimientoDetalle almacenMovimientoDetalleCancelacion = new AlmacenMovimientoDetalle();
                    almacenMovimientoDetalleCancelacion.AlmacenMovimientoID = almacenMovimientoId;
                    almacenMovimientoDetalleCancelacion.Producto = salidaProducto.AlmacenInventarioLote.AlmacenInventario.Producto;
                    almacenMovimientoDetalleCancelacion.ProductoID = salidaProducto.AlmacenInventarioLote.AlmacenInventario.Producto.ProductoId;
                    almacenMovimientoDetalleCancelacion.Precio = salidaProducto.Precio;
                    almacenMovimientoDetalleCancelacion.Cantidad = salidaProducto.PesoBruto - salidaProducto.PesoTara;
                    almacenMovimientoDetalleCancelacion.Importe = salidaProducto.Importe + salidaProducto.AlmacenMovimiento.ListaAlmacenMovimientoDetalle.Sum(registro => registro.Importe);
                    almacenMovimientoDetalleCancelacion.AlmacenInventarioLoteId = salidaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId;
                    almacenMovimientoDetalleCancelacion.Piezas = salidaProducto.Piezas;
                    almacenMovimientoDetalleCancelacion.UsuarioCreacionID = salidaProducto.UsuarioCreacionId;

                    almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleCancelacion);

                    AlmacenInventarioLoteBL almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                    salidaProducto.AlmacenInventarioLote = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(salidaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                    salidaProducto.AlmacenInventarioLote.Cantidad = (salidaProducto.PesoBruto - salidaProducto.PesoTara) + salidaProducto.AlmacenInventarioLote.Cantidad;
                    salidaProducto.AlmacenInventarioLote.Importe = salidaProducto.Importe + salidaProducto.AlmacenInventarioLote.Importe;

                    if (salidaProducto.AlmacenInventarioLote.Importe > 0 && salidaProducto.AlmacenInventarioLote.Cantidad > 0)
                    {
                        salidaProducto.AlmacenInventarioLote.PrecioPromedio = salidaProducto.AlmacenInventarioLote.Importe / salidaProducto.AlmacenInventarioLote.Cantidad;
                    }
                    else
                    {
                        salidaProducto.AlmacenInventarioLote.PrecioPromedio = 0;
                    }

                    salidaProducto.AlmacenInventarioLote.UsuarioModificacionId = salidaProducto.UsuarioCreacionId;

                    almacenInventarioLoteBl.Actualizar(salidaProducto.AlmacenInventarioLote);

                    AlmacenInventarioBL almacenInventarioBl = new AlmacenInventarioBL();
                    AlmacenInventarioInfo almacenInventario = almacenInventarioBl.ObtenerAlmacenInventarioPorId(salidaProducto.AlmacenInventarioLote.AlmacenInventario.AlmacenInventarioID);

                    List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(salidaProducto.AlmacenInventarioLote.AlmacenInventario);
                    almacenInventario.Cantidad = listaAlmacenInventarioLote.Sum(registro => registro.Cantidad);
                    almacenInventario.Importe = listaAlmacenInventarioLote.Sum(registro => registro.Importe);

                    if (almacenInventario.Importe > 0 && almacenInventario.Cantidad > 0)
                    {
                        almacenInventario.PrecioPromedio = almacenInventario.Importe / almacenInventario.Cantidad;
                    }
                    else
                    {
                        almacenInventario.PrecioPromedio = 0;
                    }
                    almacenInventario.UsuarioModificacionID = salidaProducto.UsuarioCreacionId;

                    almacenInventarioBl.Actualizar(almacenInventario);

                    SalidaProductoBL salidaProductoBl = new SalidaProductoBL();
                    salidaProducto.UsuarioModificacionId = salidaProducto.UsuarioCreacionId;
                    salidaProductoBl.Cancelar(salidaProducto);

                    CancelacionMovimientoDAL cancelacionMovimientoDal = new CancelacionMovimientoDAL();
                    CancelacionMovimientoInfo cancelacionMovimiento = new CancelacionMovimientoInfo();
                    cancelacionMovimiento.Pedido = new PedidoInfo();
                    cancelacionMovimiento.TipoCancelacion = new TipoCancelacionInfo() { TipoCancelacionId = TipoCancelacionEnum.VentaTraspaso.GetHashCode() };
                    cancelacionMovimiento.AlmacenMovimientoOrigen = salidaProducto.AlmacenMovimiento;
                    cancelacionMovimiento.AlmacenMovimientoCancelado = new AlmacenMovimientoInfo() { AlmacenMovimientoID = almacenMovimientoId };
                    cancelacionMovimiento.Justificacion = justificacion;
                    cancelacionMovimiento.UsuarioCreacionID = salidaProducto.UsuarioCreacionId;

                    cancelacionMovimientoDal.Crear(cancelacionMovimiento);

                    transaction.Complete();
                    return true;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal bool CancelarPedidoTicket(PedidoCancelacionMovimientosInfo pedido, string justificacion)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {

                    if (pedido.PesajeMateriaPrimaId > 0)
                    {
                        PesajeMateriaPrimaBL pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
                        PesajeMateriaPrimaInfo pesaje = pesajeMateriaPrimaBl.ObtenerPorId(new PesajeMateriaPrimaInfo() { PesajeMateriaPrimaID = pedido.PesajeMateriaPrimaId });
                        pesaje.UsuarioModificacionID = pedido.UsuarioID;
                        pesaje.EstatusID = Estatus.CanceladoPesaje.GetHashCode();
                        pesajeMateriaPrimaBl.ActualizarPesajePorId(pesaje);
                    }

                    if (pedido.CancelarProgramacion)
                    {
                        ProgramacionMateriaPrimaBL programacionBl = new ProgramacionMateriaPrimaBL();
                        ProgramacionMateriaPrimaInfo programacion = new ProgramacionMateriaPrimaInfo();
                        programacion = programacionBl.ObtenerPorPesajeMateriaPrima(new PesajeMateriaPrimaInfo() { ProgramacionMateriaPrimaID = pedido.ProgramacionMateriaPrimaId});
                        programacion.UsuarioModificacion = new UsuarioInfo() { UsuarioID = pedido.UsuarioID };
                        programacionBl.Cancelar(programacion);
                    }

                    if (pedido.AlmacenMovimientoOrigen != null)
                    {
                        AlmacenBL almacenBl = new AlmacenBL();
                        var listaAlmacenes = almacenBl.ObtenerAlmacenesPorOrganizacion(pedido.OrganizacionId);

                        AlmacenInfo almacenPlantaAlimentos = listaAlmacenes.FirstOrDefault(registro => registro.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode());

                        #region Movimiento Principal
                        AlmacenMovimientoBL almacenMovimientoBl = new AlmacenMovimientoBL();
                        AlmacenMovimientoInfo almacenMovimientoCancelacion = new AlmacenMovimientoInfo();
                        almacenMovimientoCancelacion.AlmacenID = almacenPlantaAlimentos.AlmacenID;
                        almacenMovimientoCancelacion.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionTicket.GetHashCode() };
                        almacenMovimientoCancelacion.TipoMovimientoID = TipoMovimiento.CancelacionTicket.GetHashCode();
                        almacenMovimientoCancelacion.Observaciones = justificacion;
                        almacenMovimientoCancelacion.UsuarioCreacionID = pedido.UsuarioID;
                        almacenMovimientoCancelacion.Status = Estatus.AplicadoInv.GetHashCode();
                        long almacenMovimientoCanceladoId = almacenMovimientoBl.Crear(almacenMovimientoCancelacion);

                        ProductoBL productoBl = new ProductoBL();
                        pedido.Producto = productoBl.ObtenerPorID(new ProductoInfo() { ProductoId = pedido.Producto.ProductoId });
                        AlmacenMovimientoDetalleBL almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                        AlmacenMovimientoDetalle almacenMovimientoDetalleCancelacion = new AlmacenMovimientoDetalle();
                        almacenMovimientoDetalleCancelacion.AlmacenMovimientoID = almacenMovimientoCanceladoId;
                        almacenMovimientoDetalleCancelacion.Producto = pedido.Producto;
                        almacenMovimientoDetalleCancelacion.ProductoID = pedido.Producto.ProductoId;

                        var almacenMovimientoCancelar = almacenMovimientoDetalleBl.ObtenerPorAlmacenMovimientoID(pedido.AlmacenMovimientoDestino.AlmacenMovimientoID);

                        almacenMovimientoDetalleCancelacion.Precio = almacenMovimientoCancelar.Precio;
                        almacenMovimientoDetalleCancelacion.Cantidad = almacenMovimientoCancelar.Cantidad;
                        almacenMovimientoDetalleCancelacion.Importe = almacenMovimientoCancelar.Importe;
                        almacenMovimientoDetalleCancelacion.AlmacenInventarioLoteId = almacenMovimientoCancelar.AlmacenInventarioLoteId;
                        almacenMovimientoDetalleCancelacion.Piezas = almacenMovimientoCancelar.Piezas;
                        almacenMovimientoDetalleCancelacion.UsuarioCreacionID = pedido.UsuarioID;
                        almacenMovimientoDetalleCancelacion.ContratoId = almacenMovimientoCancelar.ContratoId;

                        almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleCancelacion);

                        AlmacenInventarioLoteBL almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                        var almacenInventarioLoteCancelar = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(almacenMovimientoCancelar.AlmacenInventarioLoteId);

                        almacenInventarioLoteCancelar.Cantidad = almacenInventarioLoteCancelar.Cantidad - almacenMovimientoDetalleCancelacion.Cantidad;
                        almacenInventarioLoteCancelar.Importe = almacenInventarioLoteCancelar.Importe - almacenMovimientoDetalleCancelacion.Importe;
                        almacenInventarioLoteCancelar.PrecioPromedio = almacenInventarioLoteCancelar.Importe / almacenInventarioLoteCancelar.Cantidad;
                        almacenInventarioLoteCancelar.UsuarioModificacionId = pedido.UsuarioID;

                        almacenInventarioLoteBl.Actualizar(almacenInventarioLoteCancelar);

                        AlmacenInventarioBL almacenInventarioBl = new AlmacenInventarioBL();
                        AlmacenInventarioInfo almacenInventario = almacenInventarioBl.ObtenerAlmacenInventarioPorId(almacenInventarioLoteCancelar.AlmacenInventario.AlmacenInventarioID);

                        List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventarioLoteCancelar.AlmacenInventario);
                        almacenInventario.Cantidad = listaAlmacenInventarioLote.Sum(registro => registro.Cantidad);
                        almacenInventario.Importe = listaAlmacenInventarioLote.Sum(registro => registro.Importe);
                        almacenInventario.PrecioPromedio = almacenInventario.Importe / almacenInventario.Cantidad;
                        almacenInventario.UsuarioModificacionID = pedido.UsuarioID;

                        #endregion

                        #region Movimiento Secundario



                        AlmacenInfo almacenMateriasPrimas = listaAlmacenes.FirstOrDefault(registro => registro.TipoAlmacenID == TipoAlmacenEnum.MateriasPrimas.GetHashCode());

                        AlmacenMovimientoInfo almacenMovimientoSalida = new AlmacenMovimientoInfo();
                        almacenMovimientoSalida.AlmacenID = almacenMateriasPrimas.AlmacenID;
                        almacenMovimientoSalida.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.CancelacionTicket.GetHashCode() };
                        almacenMovimientoSalida.TipoMovimientoID = TipoMovimiento.CancelacionTicket.GetHashCode();
                        almacenMovimientoSalida.Observaciones = justificacion;
                        almacenMovimientoSalida.Status = Estatus.AplicadoInv.GetHashCode();
                        almacenMovimientoSalida.UsuarioCreacionID = pedido.UsuarioID;

                        long almacenMovimientoSalidaid = almacenMovimientoBl.Crear(almacenMovimientoSalida);

                        var almacenMovimientoSalidaCancelar = almacenMovimientoDetalleBl.ObtenerPorAlmacenMovimientoID(pedido.AlmacenMovimientoOrigen.AlmacenMovimientoID);

                        AlmacenMovimientoDetalle almacenMovimientoDetalleSalida = new AlmacenMovimientoDetalle();
                        almacenMovimientoDetalleSalida.AlmacenMovimientoID = almacenMovimientoSalidaid;
                        almacenMovimientoDetalleSalida.Producto = pedido.Producto;
                        almacenMovimientoDetalleSalida.Producto.ProductoId = pedido.Producto.ProductoId;
                        almacenMovimientoDetalleSalida.Precio = almacenMovimientoSalidaCancelar.Precio;
                        almacenMovimientoDetalleSalida.Cantidad = almacenMovimientoSalidaCancelar.Cantidad;
                        almacenMovimientoDetalleSalida.Importe = almacenMovimientoSalidaCancelar.Importe;
                        almacenMovimientoDetalleSalida.AlmacenInventarioLoteId = almacenMovimientoSalidaCancelar.AlmacenInventarioLoteId;
                        almacenMovimientoDetalleSalida.ContratoId = almacenMovimientoSalidaCancelar.ContratoId;
                        almacenMovimientoDetalleSalida.Piezas = almacenMovimientoSalidaCancelar.Piezas;
                        almacenMovimientoDetalleSalida.UsuarioCreacionID = pedido.UsuarioID;

                        almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleSalida);

                        var almacenInventarioLoteSalida = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(almacenMovimientoSalidaCancelar.AlmacenInventarioLoteId);
                        almacenInventarioLoteSalida.Cantidad = almacenInventarioLoteSalida.Cantidad - almacenMovimientoDetalleSalida.Cantidad;
                        almacenInventarioLoteSalida.Importe = almacenInventarioLoteSalida.Importe - almacenMovimientoDetalleSalida.Importe;
                        almacenInventarioLoteSalida.PrecioPromedio = almacenInventarioLoteSalida.Importe / almacenInventarioLoteSalida.Cantidad;
                        almacenInventarioLoteSalida.UsuarioModificacionId = pedido.UsuarioID;

                        almacenInventarioLoteBl.Actualizar(almacenInventarioLoteSalida);

                        var almacenInventarioSalida = almacenInventarioBl.ObtenerAlmacenInventarioPorId(almacenInventarioLoteSalida.AlmacenInventario.AlmacenInventarioID);
                        var listaLotes = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventarioSalida);
                        almacenInventarioSalida.Cantidad = listaLotes.Sum(registro => registro.Cantidad);
                        almacenInventarioSalida.Importe = listaLotes.Sum(registro => registro.Importe);
                        almacenInventarioSalida.PrecioPromedio = almacenInventarioSalida.Importe / almacenInventarioSalida.Cantidad;
                        almacenInventarioSalida.UsuarioModificacionID = pedido.UsuarioID;

                        #endregion


                        #region Cancelacion Movimiento

                        CancelacionMovimientoBL cancelacionMovimientoBl = new CancelacionMovimientoBL();
                        CancelacionMovimientoInfo cancelacionMovimientoEntrada = new CancelacionMovimientoInfo();
                        cancelacionMovimientoEntrada.TipoCancelacion = new TipoCancelacionInfo() { TipoCancelacionId = TipoCancelacionEnum.Pedido.GetHashCode() };
                        cancelacionMovimientoEntrada.Pedido = new PedidoInfo() { PedidoID = pedido.PedidoId };
                        cancelacionMovimientoEntrada.Ticket = pedido.Ticket;
                        cancelacionMovimientoEntrada.AlmacenMovimientoOrigen = new AlmacenMovimientoInfo() { AlmacenMovimientoID = almacenMovimientoCanceladoId };
                        cancelacionMovimientoEntrada.AlmacenMovimientoCancelado = new AlmacenMovimientoInfo() { AlmacenMovimientoID = almacenMovimientoCancelar.AlmacenMovimientoID };
                        cancelacionMovimientoEntrada.Justificacion = justificacion;
                        cancelacionMovimientoEntrada.UsuarioCreacionID = pedido.UsuarioID;

                        cancelacionMovimientoBl.Guardar(cancelacionMovimientoEntrada);

                        CancelacionMovimientoInfo cancelacionMovimientoSalida = new CancelacionMovimientoInfo();
                        cancelacionMovimientoSalida.TipoCancelacion = new TipoCancelacionInfo() { TipoCancelacionId = TipoCancelacionEnum.Pedido.GetHashCode() };
                        cancelacionMovimientoSalida.Pedido = new PedidoInfo() { PedidoID = pedido.PedidoId };
                        cancelacionMovimientoSalida.Ticket = pedido.Ticket;
                        cancelacionMovimientoSalida.AlmacenMovimientoOrigen = new AlmacenMovimientoInfo() { AlmacenMovimientoID = almacenMovimientoSalidaid };
                        cancelacionMovimientoSalida.AlmacenMovimientoCancelado = new AlmacenMovimientoInfo() { AlmacenMovimientoID = almacenMovimientoSalidaCancelar.AlmacenMovimientoID };
                        cancelacionMovimientoSalida.Justificacion = justificacion;
                        cancelacionMovimientoSalida.UsuarioCreacionID = pedido.UsuarioID;

                        cancelacionMovimientoBl.Guardar(cancelacionMovimientoSalida);

                        #endregion
                    }

                    transaction.Complete();
                    return true;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
