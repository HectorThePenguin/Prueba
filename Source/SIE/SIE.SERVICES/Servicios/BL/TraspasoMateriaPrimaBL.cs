using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    public class TraspasoMateriaPrimaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TraspasoMateriaPrima
        /// </summary>
        /// <param name="info"></param>
        public Dictionary<long, MemoryStream> GuardarTraspaso(TraspasoMpPaMedInfo info)
        {
            try
            {
                var resultado = new Dictionary<long, MemoryStream>();
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                using (var transaction = new TransactionScope())
                {
                    long almacenMovimientoOrigenID = GenerarMovimientoSalida(info);
                    long almacenMovimientoDestinoID = GenerarMovimientoEntrada(info);

                    var traspasoMateriaPrimaGuardar = new TraspasoMateriaPrimaInfo
                    {
                        ContratoOrigen = info.ContratoOrigen,
                        ContratoDestino = info.ContratoDestino,
                        Organizacion = info.Usuario.Organizacion,
                        AlmacenOrigen = info.AlmacenOrigen,
                        AlmacenDestino = info.AlmacenDestino,
                        AlmacenInventarioLoteOrigen = info.LoteMpOrigen,
                        AlmacenInventarioLoteDestino = info.LoteMpDestino,
                        CuentaSAP = info.CuentaContable,
                        Justificacion = info.JustificacionDestino,
                        AlmacenMovimientoOrigen =
                            new AlmacenMovimientoInfo { AlmacenMovimientoID = almacenMovimientoOrigenID },
                        AlmacenMovimientoDestino =
                            new AlmacenMovimientoInfo { AlmacenMovimientoID = almacenMovimientoDestinoID },
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionID = info.Usuario.UsuarioID
                    };

                    traspasoMateriaPrimaGuardar = traspasoMateriaPrimaDAL.Crear(traspasoMateriaPrimaGuardar);
                    info.FolioTraspaso = traspasoMateriaPrimaGuardar.FolioTraspaso;
                    info.FechaTraspaso = traspasoMateriaPrimaGuardar.FechaMovimiento;
                    info.AlmacenMovimientoID = almacenMovimientoDestinoID;
                    MemoryStream pdfPoliza = null;
                    if (info.AlmacenOrigen.AlmacenID != info.AlmacenDestino.AlmacenID)
                    {
                        var productoBL = new ProductoBL();
                        info.ProductoOrigen = productoBL.ObtenerPorID(info.ProductoOrigen);
                        info.ProductoDestino = info.ProductoOrigen;
                        PolizaAbstract poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaTraspaso);
                        IList<PolizaInfo> listaPolizas = poliza.GeneraPoliza(info);
                        pdfPoliza = poliza.ImprimePoliza(info, listaPolizas);
                        
                        if (listaPolizas != null && listaPolizas.Any())
                        {
                            listaPolizas.ToList().ForEach(datos =>
                            {
                                datos.OrganizacionID = info.Usuario.Organizacion.OrganizacionID;
                                datos.UsuarioCreacionID = info.Usuario.UsuarioID;
                                datos.Activo = EstatusEnum.Activo;
                                datos.ArchivoEnviadoServidor = 1;
                            });
                            var polizaDAL = new PolizaDAL();
                            polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.EntradaTraspaso);
                        }
                    }
                    transaction.Complete();
                    resultado.Add(info.FolioTraspaso, pdfPoliza);
                    return resultado;
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
        /// Metodo para Cancelar un traspaso de MP
        /// </summary>
        /// <param name="info"></param>
        public Dictionary<long, MemoryStream> CancelarTraspaso(TraspasoMpPaMedInfo info)
        {
            try
            {
                var resultado = new Dictionary<long, MemoryStream>();
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                var almacenMovimientoBL = new AlmacenMovimientoBL();
                var cancelacionMovimientoBL = new CancelacionMovimientoBL();
                TraspasoMateriaPrimaInfo traspasoCancelar =
                    traspasoMateriaPrimaDAL.ObtenerPorID(info.TraspasoMateriaPrimaID);

                if (traspasoCancelar == null)
                {
                    return null;
                }
                AlmacenMovimientoInfo movimientoOrigen = almacenMovimientoBL.ObtenerPorIDCompleto(
                    traspasoCancelar.AlmacenMovimientoOrigen.AlmacenMovimientoID);
                AlmacenMovimientoInfo movimientoDestino = almacenMovimientoBL.ObtenerPorIDCompleto(
                    traspasoCancelar.AlmacenMovimientoDestino.AlmacenMovimientoID);

                AlmacenInfo almacenOrigenAux = info.AlmacenOrigen;
                AlmacenInfo almacenDestinoAux = info.AlmacenDestino;

                AlmacenMovimientoDetalle detalleOrigen = movimientoOrigen.ListaAlmacenMovimientoDetalle.FirstOrDefault();
                AlmacenMovimientoDetalle detalleDestino = movimientoDestino.ListaAlmacenMovimientoDetalle.FirstOrDefault();
                if (detalleOrigen == null || detalleDestino == null)
                {
                    return null;
                }
                info.CantidadTraspasarOrigen = detalleOrigen.Cantidad;
                info.CantidadTraspasarDestino = detalleDestino.Cantidad;
                info.PrecioTraspasoOrigen = detalleOrigen.Precio;
                info.PrecioTraspasoDestino = detalleDestino.Precio;
                info.ImporteTraspaso = detalleDestino.Importe;

                info.AlmacenDestino = almacenOrigenAux;
                info.AlmacenOrigen = almacenDestinoAux;

                using (var transaction = new TransactionScope())
                {
                    long almacenMovimientoEntradaID = 0;
                    long almacenMovimientoSalidaID = 0;

                    if (movimientoDestino != null)
                    {
                        almacenMovimientoEntradaID = GenerarMovimientoEntradaCancelacion(movimientoDestino, info.Usuario);
                        almacenMovimientoSalidaID = GenerarMovimientoSalidaCancelacion(movimientoOrigen, info.Usuario);
                    }

                    var cancelacionMovimientoEntrada = new CancelacionMovimientoInfo
                    {
                        TipoCancelacion = new TipoCancelacionInfo
                        {
                            TipoCancelacionId = TipoCancelacionEnum.TraspasoMpPaMed.GetHashCode()
                        },
                        Pedido = new PedidoInfo(),
                        AlmacenMovimientoOrigen = new AlmacenMovimientoInfo
                        {
                            AlmacenMovimientoID = almacenMovimientoEntradaID
                        },
                        AlmacenMovimientoCancelado = new AlmacenMovimientoInfo
                        {
                            AlmacenMovimientoID = movimientoOrigen.AlmacenMovimientoID
                        },
                        Activo = EstatusEnum.Activo,
                        Justificacion = traspasoCancelar.Justificacion,
                        UsuarioCreacionID = info.Usuario.UsuarioID
                    };
                    CancelacionMovimientoInfo movimientoCancelado = cancelacionMovimientoBL.Guardar(cancelacionMovimientoEntrada);


                    if (movimientoDestino != null)
                    {
                        var cancelacionMovimientoSalida = new CancelacionMovimientoInfo
                        {
                            TipoCancelacion = new TipoCancelacionInfo
                            {
                                TipoCancelacionId = TipoCancelacionEnum.TraspasoMpPaMed.GetHashCode()
                            },
                            Pedido = new PedidoInfo(),
                            AlmacenMovimientoOrigen = new AlmacenMovimientoInfo
                            {
                                AlmacenMovimientoID = almacenMovimientoSalidaID
                            },
                            AlmacenMovimientoCancelado = new AlmacenMovimientoInfo
                            {
                                AlmacenMovimientoID = movimientoDestino.AlmacenMovimientoID
                            },
                            Activo = EstatusEnum.Activo,
                            Justificacion = traspasoCancelar.Justificacion,
                            UsuarioCreacionID = info.Usuario.UsuarioID
                        };
                        cancelacionMovimientoBL.Guardar(cancelacionMovimientoSalida);
                    }


                    traspasoCancelar.Activo = EstatusEnum.Inactivo;
                    traspasoCancelar.UsuarioModificacionID = info.Usuario.UsuarioID;

                    traspasoMateriaPrimaDAL.Actualizar(traspasoCancelar);


                    movimientoOrigen.UsuarioModificacionID = info.Usuario.UsuarioID;
                    if (movimientoDestino != null) movimientoDestino.UsuarioModificacionID = info.Usuario.UsuarioID;

                    movimientoOrigen.Status = Estatus.CanceladoInv.GetHashCode();
                    if (movimientoDestino != null) movimientoDestino.Status = Estatus.CanceladoInv.GetHashCode();


                    almacenMovimientoBL.ActualizarEstatus(movimientoOrigen);
                    almacenMovimientoBL.ActualizarEstatus(movimientoDestino);

                    AlmacenMovimientoInfo movimientoCancelacion =
                        almacenMovimientoBL.ObtenerPorIDCompleto(almacenMovimientoEntradaID);
                    if (movimientoCancelacion == null)
                    {
                        return null;
                    }

                    info.FolioTraspaso = traspasoCancelar.FolioTraspaso;
                    info.FechaTraspaso = movimientoCancelado.FechaCancelacion;
                    info.AlmacenMovimientoID = movimientoDestino.AlmacenMovimientoID;
                    MemoryStream pdfPoliza = null;
                    if (info.AlmacenOrigen.AlmacenID != info.AlmacenDestino.AlmacenID)
                    {
                        info.EsCancelacion = true;
                        var productoBL = new ProductoBL();
                        info.ProductoOrigen = productoBL.ObtenerPorID(info.ProductoOrigen);
                        info.ProductoDestino = info.ProductoOrigen;
                        PolizaAbstract poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaTraspaso);
                        IList<PolizaInfo> listaPolizas = poliza.GeneraPoliza(info);
                        pdfPoliza = poliza.ImprimePoliza(info, listaPolizas);

                        if (listaPolizas != null && listaPolizas.Any())
                        {
                            listaPolizas.ToList().ForEach(datos =>
                            {
                                datos.OrganizacionID = info.Usuario.Organizacion.OrganizacionID;
                                datos.UsuarioCreacionID = info.Usuario.UsuarioID;
                                datos.Activo = EstatusEnum.Activo;
                                datos.ArchivoEnviadoServidor = 1;
                            });
                            var polizaDAL = new PolizaDAL();
                            polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.EntradaTraspaso);
                        }
                    }
                    transaction.Complete();
                    resultado.Add(info.FolioTraspaso, pdfPoliza);
                    return resultado;
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

        private long GenerarMovimientoSalida(TraspasoMpPaMedInfo info)
        {
            var almacenInventarioBL = new AlmacenInventarioBL();
            var almacenInventarioLoteBL = new AlmacenInventarioLoteBL();
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
            decimal cantidadTraspaso = info.CantidadTraspasarOrigen;

            List<AlmacenInventarioInfo> inventariosOrigen = almacenInventarioBL.ObtienePorAlmacenId(info.AlmacenOrigen);
            AlmacenInventarioInfo almacenInventarioOrigen = null;
            decimal precioPromedio = 0;
            decimal importe = 0;

            if (inventariosOrigen != null && inventariosOrigen.Any())
            {
                almacenInventarioOrigen =
                    inventariosOrigen.FirstOrDefault(inv => inv.ProductoID == info.ProductoOrigen.ProductoId);
                if (almacenInventarioOrigen != null)
                {
                    almacenInventarioOrigen.Cantidad = almacenInventarioOrigen.Cantidad - cantidadTraspaso;
                    almacenInventarioOrigen.Importe = almacenInventarioOrigen.Importe -
                                                      Math.Round(cantidadTraspaso * almacenInventarioOrigen.PrecioPromedio, 2);
                    if (almacenInventarioOrigen.Importe < 0)
                    {
                        almacenInventarioOrigen.Importe = 0;
                    }
                    if (almacenInventarioOrigen.Cantidad > 0)
                    {
                        almacenInventarioOrigen.PrecioPromedio = almacenInventarioOrigen.Importe / almacenInventarioOrigen.Cantidad;
                    }
                    almacenInventarioOrigen.UsuarioModificacionID = info.Usuario.UsuarioID;

                    almacenInventarioBL.Actualizar(almacenInventarioOrigen);
                    precioPromedio = almacenInventarioOrigen.PrecioPromedio;
                    importe = precioPromedio * cantidadTraspaso;
                }
            }
            if (info.LoteMpOrigen.AlmacenInventarioLoteId != 0)
            {
                AlmacenInventarioLoteInfo loteOrigen = almacenInventarioLoteBL.ObtenerAlmacenInventarioLotePorId(info.LoteMpOrigen.AlmacenInventarioLoteId);

                if (loteOrigen != null)
                {
                    loteOrigen.Cantidad = loteOrigen.Cantidad - cantidadTraspaso;
                    loteOrigen.Importe = loteOrigen.Importe - Math.Round(cantidadTraspaso * loteOrigen.PrecioPromedio, 2);
                    if (loteOrigen.Importe < 0)
                    {
                        loteOrigen.Importe = 0;
                    }
                    if (loteOrigen.Cantidad > 0)
                    {
                        loteOrigen.PrecioPromedio = loteOrigen.Importe / loteOrigen.Cantidad;
                    }
                    loteOrigen.UsuarioModificacionId = info.Usuario.UsuarioID;

                    almacenInventarioLoteBL.Actualizar(loteOrigen);
                    precioPromedio = loteOrigen.PrecioPromedio;
                    importe = precioPromedio * cantidadTraspaso;
                }
            }
            var almacenMovimientoOrigen = new AlmacenMovimientoInfo
            {
                AlmacenID = info.AlmacenOrigen.AlmacenID,
                TipoMovimientoID = TipoMovimiento.ProductoSalidaTraspaso.GetHashCode(),
                ProveedorId = info.ProveedorOrigen.ProveedorID,
                Status = Estatus.AplicadoInv.GetHashCode(),
                UsuarioCreacionID = info.Usuario.UsuarioID,
                Observaciones = info.JustificacionDestino
            };
            long almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimientoOrigen);

            if (almacenInventarioOrigen != null)
            {
                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle
                    {
                        AlmacenMovimientoID = almacenMovimientoID,
                        AlmacenInventarioLoteId = info.LoteMpOrigen.AlmacenInventarioLoteId,
                        Piezas = 0,
                        ProductoID = info.ProductoOrigen.ProductoId,
                        Precio = precioPromedio,
                        Cantidad = cantidadTraspaso,
                        Importe = importe,
                        UsuarioCreacionID = info.Usuario.UsuarioID
                    };
                almacenMovimientoDetalleBL.Crear(almacenMovimientoDetalleInfo);
            }
            info.ImporteTraspaso = importe;
            return almacenMovimientoID;
        }

        private long GenerarMovimientoEntrada(TraspasoMpPaMedInfo info)
        {
            var almacenInventarioBL = new AlmacenInventarioBL();
            var almacenInventarioLoteBL = new AlmacenInventarioLoteBL();
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
            decimal cantidadTraspaso = info.CantidadTraspasarDestino;

            decimal precioPromedio = 0;

            List<AlmacenInventarioInfo> inventariosDestino = almacenInventarioBL.ObtienePorAlmacenId(info.AlmacenDestino);

            if (inventariosDestino != null && inventariosDestino.Any())
            {
                AlmacenInventarioInfo almacenInventarioDestino = inventariosDestino.FirstOrDefault(inv => inv.ProductoID == info.ProductoDestino.ProductoId);
                if (almacenInventarioDestino != null)
                {
                    almacenInventarioDestino.Cantidad = almacenInventarioDestino.Cantidad + cantidadTraspaso;
                    almacenInventarioDestino.Importe = almacenInventarioDestino.Importe + info.ImporteTraspaso;
                    almacenInventarioDestino.PrecioPromedio = almacenInventarioDestino.Importe / almacenInventarioDestino.Cantidad;
                    almacenInventarioDestino.UsuarioModificacionID = info.Usuario.UsuarioID;

                    almacenInventarioBL.Actualizar(almacenInventarioDestino);
                }
                else
                {
                    //Crear el Inventario 

                    var almacenInventarioGuardar = new AlmacenInventarioInfo
                    {
                        AlmacenID = info.AlmacenDestino.AlmacenID,
                        ProductoID = info.ProductoDestino.ProductoId,
                        Cantidad = cantidadTraspaso,
                        Importe = info.ImporteTraspaso,
                        PrecioPromedio = info.ImporteTraspaso / cantidadTraspaso,
                        UsuarioCreacionID = info.Usuario.UsuarioID
                    };
                    almacenInventarioBL.Crear(almacenInventarioGuardar);
                }
            }
            else
            {
                //Crear el Inventario 

                var almacenInventarioGuardar = new AlmacenInventarioInfo
                {
                    AlmacenID = info.AlmacenDestino.AlmacenID,
                    ProductoID = info.ProductoDestino.ProductoId,
                    Cantidad = cantidadTraspaso,
                    Importe = info.ImporteTraspaso,
                    PrecioPromedio = info.ImporteTraspaso / cantidadTraspaso,
                    UsuarioCreacionID = info.Usuario.UsuarioID
                };
                almacenInventarioBL.Crear(almacenInventarioGuardar);
            }
            if (info.LoteMpDestino.AlmacenInventarioLoteId != 0)
            {
                AlmacenInventarioLoteInfo loteDestino = almacenInventarioLoteBL.ObtenerAlmacenInventarioLotePorId(info.LoteMpDestino.AlmacenInventarioLoteId);

                if (loteDestino != null)
                {
                    loteDestino.Cantidad = loteDestino.Cantidad + cantidadTraspaso;
                    loteDestino.Importe = loteDestino.Importe + info.ImporteTraspaso;
                    loteDestino.PrecioPromedio = loteDestino.Importe / loteDestino.Cantidad;
                    loteDestino.UsuarioModificacionId = info.Usuario.UsuarioID;

                    almacenInventarioLoteBL.Actualizar(loteDestino);
                }
            }
            var almacenMovimientoDestino = new AlmacenMovimientoInfo
            {
                AlmacenID = info.AlmacenDestino.AlmacenID,
                TipoMovimientoID = TipoMovimiento.EntradaAlmacen.GetHashCode(),
                ProveedorId = info.ProveedorDestino.ProveedorID,
                Status = Estatus.AplicadoInv.GetHashCode(),
                UsuarioCreacionID = info.Usuario.UsuarioID,
                Observaciones = info.JustificacionDestino
            };
            long almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimientoDestino);

            precioPromedio = info.ImporteTraspaso / info.CantidadTraspasarDestino;

            var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle
            {
                AlmacenMovimientoID = almacenMovimientoID,
                //ContratoId = info.ContratoDestino.ContratoId,
                AlmacenInventarioLoteId = info.LoteMpDestino.AlmacenInventarioLoteId,
                Piezas = 0,
                ProductoID = info.ProductoDestino.ProductoId,
                Precio = precioPromedio,
                Cantidad = cantidadTraspaso,
                Importe = info.ImporteTraspaso,
                UsuarioCreacionID = info.Usuario.UsuarioID
            };
            almacenMovimientoDetalleBL.Crear(almacenMovimientoDetalleInfo);

            return almacenMovimientoID;
        }

        private long GenerarMovimientoSalidaCancelacion(AlmacenMovimientoInfo info, UsuarioInfo usuario)
        {
            var almacenInventarioBL = new AlmacenInventarioBL();
            var almacenInventarioLoteBL = new AlmacenInventarioLoteBL();
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();

            AlmacenMovimientoDetalle detalle = info.ListaAlmacenMovimientoDetalle.FirstOrDefault();
            if (detalle == null)
            {
                return 0;
            }

            //decimal cantidadTraspaso = detalle.Cantidad;

            List<AlmacenInventarioInfo> inventariosOrigen = almacenInventarioBL.ObtienePorAlmacenId(info.Almacen);
            AlmacenInventarioInfo almacenInventarioOrigen = null;

            if (inventariosOrigen != null && inventariosOrigen.Any())
            {
                almacenInventarioOrigen =
                    inventariosOrigen.FirstOrDefault(inv => inv.ProductoID == detalle.Producto.ProductoId);
                if (almacenInventarioOrigen != null)
                {
                    almacenInventarioOrigen.Cantidad = almacenInventarioOrigen.Cantidad - detalle.Cantidad;
                    almacenInventarioOrigen.Importe = almacenInventarioOrigen.Importe - detalle.Importe;
                    if (almacenInventarioOrigen.Importe < 0)
                    {
                        almacenInventarioOrigen.Importe = 0;
                    }
                    if (almacenInventarioOrigen.Cantidad > 0)
                    {
                        almacenInventarioOrigen.PrecioPromedio = almacenInventarioOrigen.Importe /
                                                                 almacenInventarioOrigen.Cantidad;
                    }
                    almacenInventarioOrigen.UsuarioModificacionID = usuario.UsuarioID;

                    almacenInventarioBL.Actualizar(almacenInventarioOrigen);
                }
            }
            if (detalle.AlmacenInventarioLoteId != 0)
            {
                AlmacenInventarioLoteInfo loteOrigen = almacenInventarioLoteBL.ObtenerAlmacenInventarioLotePorId(detalle.AlmacenInventarioLoteId);

                if (loteOrigen != null)
                {
                    loteOrigen.Cantidad = loteOrigen.Cantidad - detalle.Cantidad;
                    loteOrigen.Importe = loteOrigen.Importe - detalle.Importe;
                    if (loteOrigen.Importe < 0)
                    {
                        loteOrigen.Importe = 0;
                    }
                    if (loteOrigen.Cantidad > 0)
                    {
                        loteOrigen.PrecioPromedio = loteOrigen.Importe / loteOrigen.Cantidad;
                    }
                    loteOrigen.UsuarioModificacionId = usuario.UsuarioID;

                    almacenInventarioLoteBL.Actualizar(loteOrigen);
                }
            }
            var almacenMovimientoDestino = new AlmacenMovimientoInfo
            {
                AlmacenID = info.Almacen.AlmacenID,
                TipoMovimientoID = TipoMovimiento.ProductoSalidaTraspaso.GetHashCode(),
                ProveedorId = info.ProveedorId,
                Status = Estatus.AplicadoInv.GetHashCode(),
                UsuarioCreacionID = usuario.UsuarioID,
            };
            long almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimientoDestino);

            if (almacenInventarioOrigen != null)
            {
                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle
                {
                    AlmacenMovimientoID = almacenMovimientoID,
                    AlmacenInventarioLoteId = detalle.AlmacenInventarioLoteId,
                    //ContratoId = detalle.ContratoId,
                    Piezas = 0,
                    ProductoID = detalle.Producto.ProductoId,
                    Precio = detalle.Precio,
                    Cantidad = detalle.Cantidad,
                    Importe = detalle.Importe,
                    UsuarioCreacionID = usuario.UsuarioID
                };
                almacenMovimientoDetalleBL.Crear(almacenMovimientoDetalleInfo);
            }
            return almacenMovimientoID;
        }

        private long GenerarMovimientoEntradaCancelacion(AlmacenMovimientoInfo info, UsuarioInfo usuario)
        {
            var almacenInventarioBL = new AlmacenInventarioBL();
            var almacenInventarioLoteBL = new AlmacenInventarioLoteBL();
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();

            AlmacenMovimientoDetalle detalle = info.ListaAlmacenMovimientoDetalle.FirstOrDefault();
            if (detalle == null)
            {
                return 0;
            }

            //decimal cantidadTraspaso = detalle.Cantidad;

            List<AlmacenInventarioInfo> inventariosDestino = almacenInventarioBL.ObtienePorAlmacenId(info.Almacen);
            AlmacenInventarioInfo almacenInventarioDestino = null;

            if (inventariosDestino != null && inventariosDestino.Any())
            {
                almacenInventarioDestino =
                    inventariosDestino.FirstOrDefault(inv => inv.ProductoID == detalle.Producto.ProductoId);
                if (almacenInventarioDestino != null)
                {
                    almacenInventarioDestino.Cantidad = almacenInventarioDestino.Cantidad + detalle.Cantidad;
                    almacenInventarioDestino.Importe = almacenInventarioDestino.Importe + detalle.Importe;
                    almacenInventarioDestino.PrecioPromedio = almacenInventarioDestino.Importe / almacenInventarioDestino.Cantidad;
                    almacenInventarioDestino.UsuarioModificacionID = usuario.UsuarioID;

                    almacenInventarioBL.Actualizar(almacenInventarioDestino);
                }
            }
            if (detalle.AlmacenInventarioLoteId != 0)
            {
                AlmacenInventarioLoteInfo loteDestino = almacenInventarioLoteBL.ObtenerAlmacenInventarioLotePorId(detalle.AlmacenInventarioLoteId);

                if (loteDestino != null)
                {
                    loteDestino.Cantidad = loteDestino.Cantidad + detalle.Cantidad;
                    loteDestino.Importe = loteDestino.Importe + detalle.Importe;
                    loteDestino.PrecioPromedio = loteDestino.Importe / loteDestino.Cantidad;
                    loteDestino.UsuarioModificacionId = usuario.UsuarioID;

                    almacenInventarioLoteBL.Actualizar(loteDestino);
                }
            }
            var almacenMovimientoDestino = new AlmacenMovimientoInfo
            {
                AlmacenID = info.Almacen.AlmacenID,
                TipoMovimientoID = TipoMovimiento.EntradaAlmacen.GetHashCode(),
                ProveedorId = info.ProveedorId,
                Status = Estatus.AplicadoInv.GetHashCode(),
                UsuarioCreacionID = usuario.UsuarioID,
            };
            long almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimientoDestino);

            if (almacenInventarioDestino != null)
            {
                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle
                {
                    AlmacenMovimientoID = almacenMovimientoID,
                    AlmacenInventarioLoteId = detalle.AlmacenInventarioLoteId,
                    //ContratoId = detalle.ContratoId,
                    Piezas = 0,
                    ProductoID = detalle.Producto.ProductoId,
                    Precio = detalle.Precio,
                    Cantidad = detalle.Cantidad,
                    Importe = detalle.Importe,
                    UsuarioCreacionID = usuario.UsuarioID
                };
                almacenMovimientoDetalleBL.Crear(almacenMovimientoDetalleInfo);
            }
            return almacenMovimientoID;
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TraspasoMpPaMedInfo> ObtenerPorPagina(PaginacionInfo pagina, FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                ResultadoInfo<TraspasoMpPaMedInfo> result = traspasoMateriaPrimaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TraspasoMateriaPrima
        /// </summary>
        /// <returns></returns>
        public IList<TraspasoMateriaPrimaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                IList<TraspasoMateriaPrimaInfo> result = traspasoMateriaPrimaDAL.ObtenerTodos();
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
        public IList<TraspasoMateriaPrimaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                IList<TraspasoMateriaPrimaInfo> result = traspasoMateriaPrimaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TraspasoMateriaPrima por su Id
        /// </summary>
        /// <param name="traspasoMateriaPrimaID">Obtiene una entidad TraspasoMateriaPrima por su Id</param>
        /// <returns></returns>
        public TraspasoMateriaPrimaInfo ObtenerPorID(int traspasoMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                TraspasoMateriaPrimaInfo result = traspasoMateriaPrimaDAL.ObtenerPorID(traspasoMateriaPrimaID);
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
        /// Obtiene un trapaso de materia prima por su folio
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public TraspasoMpPaMedInfo ObtenerPorFolioTraspaso(FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Logger.Info();
                var traspasoMateriaPrimaDAL = new TraspasoMateriaPrimaDAL();
                TraspasoMpPaMedInfo result = traspasoMateriaPrimaDAL.ObtenerPorFolioTraspaso(filtro);
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
    }
}
