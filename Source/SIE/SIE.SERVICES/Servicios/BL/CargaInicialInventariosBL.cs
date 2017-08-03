using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Servicios.BL
{
    public class CargaInicialInventariosBL
    {
        /// <summary>
        /// Genera la carga inicial de los inventarios
        /// </summary>
        /// <param name="cargaInventarios"></param>
        /// <returns></returns>
        public void GenerarCargaInicial(List<CargaMPPAModel> cargaInventarios)
        {
            try
            {
                var almacenInventarioBL = new AlmacenInventarioBL();
                var almacenInventarioLoteBL = new AlmacenInventarioLoteBL();
                var fechaBL = new FechaBL();

                FechaInfo fechaActual = fechaBL.ObtenerFechaActual();

                using (var transaction = new TransactionScope())
                {
                    foreach (var cargaInventario in cargaInventarios)
                    {
                        if (cargaInventario.AlmacenInventario == null)
                        {
                            cargaInventario.AlmacenInventario = new AlmacenInventarioInfo();
                        }
                        var almacenInventarioFiltro = new AlmacenInventarioInfo
                                                                      {
                                                                          AlmacenID = cargaInventario.AlmacenID,
                                                                          ProductoID = cargaInventario.ProductoID
                                                                      };

                        var almacenInventario = almacenInventarioBL.ObtenerPorAlmacenIdProductoId(almacenInventarioFiltro);

                        if (almacenInventario == null)
                        {
                            almacenInventario = new AlmacenInventarioInfo();
                        }

                        var almacenInventarioLote = new AlmacenInventarioLoteInfo();

                        almacenInventario.Importe = almacenInventario.Importe + cargaInventario.ImporteActual;
                        almacenInventario.Cantidad = almacenInventario.Cantidad + cargaInventario.CantidadActual;
                        almacenInventario.PrecioPromedio = almacenInventario.Importe /
                                                                           almacenInventario.Cantidad;
                        almacenInventario.AlmacenID = cargaInventario.Almacen.AlmacenID;
                        almacenInventario.ProductoID = cargaInventario.Producto.ProductoId;
                        almacenInventario.UsuarioCreacionID = 1;
                        almacenInventario.UsuarioModificacionID = 1;

                        if (almacenInventario.AlmacenInventarioID == 0)
                        {
                            int almacenInventarioID = almacenInventarioBL.Crear(almacenInventario);
                            almacenInventario.AlmacenInventarioID = almacenInventarioID;
                        }
                        else
                        {
                            almacenInventarioBL.Actualizar(almacenInventario);
                        }
                        if (cargaInventario.Producto.ManejaLote)
                        {
                            almacenInventarioLote = new AlmacenInventarioLoteInfo
                                                            {
                                                                AlmacenInventario = almacenInventario,
                                                                Cantidad = cargaInventario.CantidadActual,
                                                                Importe = cargaInventario.ImporteActual,
                                                                PrecioPromedio =
                                                                    cargaInventario.ImporteActual /
                                                                    cargaInventario.CantidadActual,
                                                                Lote = cargaInventario.Lote,
                                                                Piezas = cargaInventario.Piezas,
                                                                FechaInicio =
                                                                    cargaInventario.FechaInicioLote == DateTime.MinValue
                                                                        ? fechaActual.FechaActual
                                                                        : cargaInventario.FechaInicioLote,
                                                                UsuarioCreacionId = 1,
                                                                Activo = EstatusEnum.Activo
                                                            };
                            int almacenInventaroLoteID =
                                almacenInventarioLoteBL.CrearConTodosParametros(almacenInventarioLote);
                            almacenInventarioLote.AlmacenInventarioLoteId = almacenInventaroLoteID;
                        }
                        GenerarMovimientosAlmacen(cargaInventario, almacenInventarioLote);

                    }

                    transaction.Complete();
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
        /// Genera los movimientos de almacen
        /// </summary>
        /// <param name="cargaInventario"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        private void GenerarMovimientosAlmacen(CargaMPPAModel cargaInventario, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();

            var almacenMovimiento = new AlmacenMovimientoInfo
                                        {
                                            AlmacenID = cargaInventario.Almacen.AlmacenID,
                                            TipoMovimientoID = TipoMovimiento.EntradaPorAjuste.GetHashCode(),
                                            Observaciones = string.Format("Carga Inicial {0}", DateTime.Now.Date),
                                            Status = Estatus.AplicadoInv.GetHashCode(),
                                            UsuarioCreacionID = 1
                                        };
            long almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimiento);
            almacenMovimiento.AlmacenMovimientoID = almacenMovimientoID;



            var almacenMovimientoDetalle = new AlmacenMovimientoDetalle
                                               {
                                                   AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                                   AlmacenInventarioLoteId =
                                                       almacenInventarioLote.AlmacenInventarioLoteId,
                                                   Piezas = cargaInventario.Piezas,
                                                   ProductoID = cargaInventario.Producto.ProductoId,
                                                   Cantidad = cargaInventario.CantidadTamanioLote,
                                                   Importe = cargaInventario.ImporteTamanioLote,
                                                   Precio =
                                                       cargaInventario.ImporteTamanioLote /
                                                       cargaInventario.CantidadTamanioLote,
                                                   UsuarioCreacionID = 1
                                               };
            long almacenMovimientoDetalleID = almacenMovimientoDetalleBL.Crear(almacenMovimientoDetalle);
            almacenMovimientoDetalle.AlmacenMovimientoDetalleID = almacenMovimientoDetalleID;

            decimal diferenciaInventario = cargaInventario.CantidadTamanioLote - cargaInventario.CantidadActual;
            decimal importeDiferencia = cargaInventario.ImporteTamanioLote - cargaInventario.ImporteActual;
            if (diferenciaInventario > 0)
            {
                almacenMovimiento = new AlmacenMovimientoInfo
                {
                    AlmacenID = cargaInventario.Almacen.AlmacenID,
                    TipoMovimientoID = TipoMovimiento.SalidaPorAjuste.GetHashCode(),
                    Observaciones = string.Format("Carga Inicial {0}", DateTime.Now.Date),
                    Status = Estatus.AplicadoInv.GetHashCode(),
                    UsuarioCreacionID = 1
                };
                almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimiento);
                almacenMovimiento.AlmacenMovimientoID = almacenMovimientoID;



                almacenMovimientoDetalle = new AlmacenMovimientoDetalle
                {
                    AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                    AlmacenInventarioLoteId =
                        almacenInventarioLote.AlmacenInventarioLoteId,
                    Piezas = cargaInventario.Piezas,
                    ProductoID = cargaInventario.Producto.ProductoId,
                    Cantidad = diferenciaInventario,
                    Importe = importeDiferencia,
                    Precio =
                        importeDiferencia /
                        diferenciaInventario,
                    UsuarioCreacionID = 1
                };
                almacenMovimientoDetalleID = almacenMovimientoDetalleBL.Crear(almacenMovimientoDetalle);
                almacenMovimientoDetalle.AlmacenMovimientoDetalleID = almacenMovimientoDetalleID;
            }
        }
    }
}
