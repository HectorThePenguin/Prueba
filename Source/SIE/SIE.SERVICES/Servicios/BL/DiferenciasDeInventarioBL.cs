using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Info.Modelos;
using SIE.Services.Polizas.Fabrica;
using System.IO;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    internal class DiferenciasDeInventarioBL
    {
        /// <summary>
        /// Guarda los ajustes
        /// </summary>
        /// <param name="listaDiferenciasInventario"></param>
        /// <param name="usuarioInfo"></param>
        internal IList<MemoryStream> Guardar(List<DiferenciasDeInventariosInfo> listaDiferenciasInventario, UsuarioInfo usuarioInfo)
        {
            int usuarioId = usuarioInfo.UsuarioID;
            PolizaAbstract poliza = null;
            IList<PolizaInfo> polizas = null;
            int organizacionID;
            //var contratoCreado = new ContratoInfo();
            IList<MemoryStream> streams = null;
            try
            {
                using (var transaction = new TransactionScope())
                {

                    var almacenMovimientoBl = new AlmacenMovimientoBL();
                    var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                    organizacionID = usuarioInfo.OrganizacionID;
                    foreach (var diferenciasDeInventariosInfo in listaDiferenciasInventario)
                    {
                        if (diferenciasDeInventariosInfo.DescripcionAjuste.Equals(TipoAjusteEnum.CerrarLote.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                            var almacenInventarioLoteInfo = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                                                diferenciasDeInventariosInfo.AlmacenInventarioLote.AlmacenInventarioLoteId);
                            if (almacenInventarioLoteInfo != null)
                            {
                                almacenInventarioLoteInfo.UsuarioModificacionId = usuarioInfo.UsuarioID;
                                //Kilogramos teoricos en 0 se desactiva el lote
                                if (diferenciasDeInventariosInfo.KilogramosFisicos == 0)
                                {
                                    //Desactivar lote
                                    almacenInventarioLoteBl.DesactivarLote(almacenInventarioLoteInfo);
                                }
                            }
                            continue;
                        }

                        diferenciasDeInventariosInfo.DiferenciaInventario =
                            Math.Abs(diferenciasDeInventariosInfo.KilogramosFisicos -
                                     diferenciasDeInventariosInfo.KilogramosTeoricos);
                        ////Se le quita el signo para que no guarde en negativos
                        //diferenciasDeInventariosInfo.KilogramosTeoricos =
                        //    Math.Abs(diferenciasDeInventariosInfo.KilogramosTeoricos);
                        //organizacionID = diferenciasDeInventariosInfo.AlmacenMovimiento.OrganizacionID;
                        //Si no esta guardado se actualiza
                        if (!diferenciasDeInventariosInfo.Guardado)
                        {
                            AlmacenMovimientoInfo almacen = new AlmacenMovimientoInfo();
                            //Estatus aplicado guarda y actualiza inventario
                            if (diferenciasDeInventariosInfo.AlmacenMovimiento.Status ==
                                Estatus.DifInvAplicado.GetHashCode())
                            {
                                //Insertar movimiento
                                diferenciasDeInventariosInfo.AlmacenMovimiento.AlmacenID =
                                    diferenciasDeInventariosInfo.Almacen.AlmacenID;
                                var almacenMovimientoId =
                                    almacenMovimientoBl.Crear(diferenciasDeInventariosInfo.AlmacenMovimiento);

                                //Insertar detalle
                                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle
                                {
                                    AlmacenMovimientoID = almacenMovimientoId,
                                    AlmacenInventarioLoteId =
                                        diferenciasDeInventariosInfo.
                                        AlmacenInventarioLote.
                                        AlmacenInventarioLoteId,
                                    Piezas = 0,
                                    ProductoID =
                                        diferenciasDeInventariosInfo.Producto.
                                        ProductoId,
                                    Precio =
                                        diferenciasDeInventariosInfo.
                                        AlmacenInventarioLote.PrecioPromedio,
                                    Cantidad =
                                        diferenciasDeInventariosInfo.
                                        DiferenciaInventario,
                                    Importe =
                                        diferenciasDeInventariosInfo.
                                            DiferenciaInventario *
                                        diferenciasDeInventariosInfo.
                                            AlmacenInventarioLote.PrecioPromedio,
                                    UsuarioCreacionID = usuarioInfo.UsuarioID
                                };
                                long almacenMovimientoDetalleID =
                                    almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleInfo);


                                //SE AGREGA DETALLE
                                almacenMovimientoDetalleInfo.AlmacenMovimientoDetalleID = almacenMovimientoDetalleID;



                                //var almacenmovimientoBl1 = new AlmacenMovimientoDetalleBL();

                                //var d =
                                //    almacenmovimientoBl1.ObtenerPorAlmacenMovimientoDetalleId(almacenMovimientoDetalleInfo);


                                diferenciasDeInventariosInfo.AlmacenMovimientoDetalle = almacenMovimientoDetalleInfo;
                                //SE AGREGA DETALLE
                                //Actualizamos inventario y lote
                                ActualizarInventarioYLote(diferenciasDeInventariosInfo, usuarioInfo.UsuarioID);
                            }

                            //Si es pendiente solo guarda el movimiento y detalle
                            if (diferenciasDeInventariosInfo.AlmacenMovimiento.Status ==
                                Estatus.DifInvPendiente.GetHashCode())
                            {
                                //Insertar movimiento
                                diferenciasDeInventariosInfo.AlmacenMovimiento.AlmacenID =
                                    diferenciasDeInventariosInfo.Almacen.AlmacenID;
                                var almacenMovimientoId =
                                    almacenMovimientoBl.Crear(diferenciasDeInventariosInfo.AlmacenMovimiento);

                                //Insertar detalle
                                var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle()
                                {
                                    AlmacenMovimientoID = almacenMovimientoId,
                                    AlmacenInventarioLoteId =
                                        diferenciasDeInventariosInfo.
                                        AlmacenInventarioLote.
                                        AlmacenInventarioLoteId,
                                    Piezas = 0,
                                    ProductoID =
                                        diferenciasDeInventariosInfo.Producto.
                                        ProductoId,
                                    Precio =
                                        diferenciasDeInventariosInfo.
                                        AlmacenInventarioLote.PrecioPromedio,
                                    Cantidad =
                                        diferenciasDeInventariosInfo.
                                        DiferenciaInventario,
                                    Importe =
                                        diferenciasDeInventariosInfo.
                                            DiferenciaInventario *
                                        diferenciasDeInventariosInfo.
                                            AlmacenInventarioLote.PrecioPromedio,
                                    UsuarioCreacionID = usuarioInfo.UsuarioID
                                };
                                long almacenMovimientoDetalleID =
                                    almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleInfo);


                                //Se valida si requiere autorizacion
                                if (diferenciasDeInventariosInfo.RequiereAutorizacion)
                                {
                                    var almacenBl = new AlmacenBL();
                                    AlmacenMovimientoInfo almacenMovimientoInfo = new AlmacenMovimientoInfo
                                    {
                                        AlmacenID = diferenciasDeInventariosInfo.Almacen.AlmacenID,
                                        AlmacenMovimientoID = almacenMovimientoId
                                    };
                                    almacen = almacenBl.ObtenerAlmacenMovimiento(almacenMovimientoInfo);

                                    var autorizacionMateriaPrimaInfo = new AutorizacionMateriaPrimaInfo
                                    {
                                        OrganizacionID = usuarioInfo.OrganizacionID,
                                        TipoAutorizacionID = TipoAutorizacionEnum.AjustedeInventario.GetHashCode(),
                                        Folio = almacen.FolioMovimiento,
                                        Justificacion = almacen.Observaciones,
                                        Lote = diferenciasDeInventariosInfo.AlmacenInventarioLote.Lote,
                                        Precio = almacenMovimientoDetalleInfo.Precio,
                                        Cantidad = almacenMovimientoDetalleInfo.Cantidad,
                                        ProductoID = almacenMovimientoDetalleInfo.ProductoID,
                                        AlmacenID = almacen.AlmacenID,
                                        EstatusID = Estatus.AMPPendien.GetHashCode(),
                                        UsuarioCreacion = usuarioInfo.UsuarioID,
                                        Activo = EstatusEnum.Activo.GetHashCode()
                                    };

                                    var almacenInventarioLoteBL = new AlmacenInventarioLoteBL();
                                    almacenInventarioLoteBL.GuardarAutorizacionMateriaPrima(
                                        autorizacionMateriaPrimaInfo);
                                }
                                //SE AGREGA DETALLE
                                almacenMovimientoDetalleInfo.AlmacenMovimientoDetalleID = almacenMovimientoDetalleID;
                                diferenciasDeInventariosInfo.AlmacenMovimientoDetalle = almacenMovimientoDetalleInfo;
                                //SE AGREGA DETALLE
                            }
                        }
                        else
                        {
                            if (diferenciasDeInventariosInfo.AlmacenMovimiento.Status ==
                                Estatus.DifInvAplicado.GetHashCode())
                            {
                                //Actualizar estatus a regitro
                                //Agregar observaciones
                                almacenMovimientoBl.ActualizarEstatus(diferenciasDeInventariosInfo.AlmacenMovimiento);

                                //Actualizar detalle movimiento
                                var almacenMovimientoDetalleInfo =
                                    almacenMovimientoDetalleBl.ObtenerPorAlmacenMovimientoDetalleId(
                                        diferenciasDeInventariosInfo.AlmacenMovimientoDetalle);
                                almacenMovimientoDetalleInfo.Cantidad =
                                    diferenciasDeInventariosInfo.DiferenciaInventario;
                                almacenMovimientoDetalleInfo.Importe = almacenMovimientoDetalleInfo.Cantidad *
                                                                       almacenMovimientoDetalleInfo.Precio;
                                almacenMovimientoDetalleInfo.UsuarioModificacionID = usuarioInfo.UsuarioID;
                                almacenMovimientoDetalleBl.ActualizarAlmacenMovimientoDetalle(
                                    almacenMovimientoDetalleInfo);
                                //Actualizamos inventario y lote
                                ActualizarInventarioYLote(diferenciasDeInventariosInfo, usuarioInfo.UsuarioID);
                                //SE AGREGA DETALLE
                                diferenciasDeInventariosInfo.AlmacenMovimientoDetalle = almacenMovimientoDetalleInfo;
                                //SE AGREGA DETALLE
                            }

                            //Guardado con estatus pendiente se actualiza almacen movimiento y almacen movimiento detalle
                            if (diferenciasDeInventariosInfo.AlmacenMovimiento.Status ==
                                Estatus.DifInvPendiente.GetHashCode())
                            {
                                //Actualizar almacen movimiento
                                almacenMovimientoBl.ActualizarEstatus(diferenciasDeInventariosInfo.AlmacenMovimiento);

                                //Actualizar movimiento detalle
                                //Verificar si se ocupa obtenerlo
                                var almacenMovimientoDetalleInfo =
                                    almacenMovimientoDetalleBl.ObtenerPorAlmacenMovimientoDetalleId(
                                        diferenciasDeInventariosInfo.AlmacenMovimientoDetalle);
                                almacenMovimientoDetalleInfo.Cantidad =
                                    diferenciasDeInventariosInfo.DiferenciaInventario;
                                almacenMovimientoDetalleInfo.Importe = almacenMovimientoDetalleInfo.Cantidad *
                                                                       almacenMovimientoDetalleInfo.Precio;
                                //Pendiente usuario modificacion
                                almacenMovimientoDetalleInfo.UsuarioModificacionID = usuarioInfo.UsuarioID;
                                almacenMovimientoDetalleBl.ActualizarAlmacenMovimientoDetalle(
                                    almacenMovimientoDetalleInfo);
                                //SE AGREGA DETALLE
                                diferenciasDeInventariosInfo.AlmacenMovimientoDetalle = almacenMovimientoDetalleInfo;
                                //SE AGREGA DETALLE
                            }
                        }
                    }

                    #region POLIZA

                    var listaDiferenciasInventarioAplicados =
                        listaDiferenciasInventario.Where(dif => dif.AlmacenMovimiento.Status == Estatus.DifInvAplicado.GetHashCode()).ToList();

                    List<PolizaEntradaSalidaPorAjusteModel> salidasPorAjuste =
                        listaDiferenciasInventarioAplicados.Where(dif => !dif.DescripcionAjuste.Trim().Equals(TipoAjusteEnum.CerrarLote.ToString().Trim(), StringComparison.InvariantCultureIgnoreCase)).Select(ajuste => new PolizaEntradaSalidaPorAjusteModel
                        {
                            Importe =
                                ajuste.DiferenciaInventario *
                                ajuste.AlmacenInventarioLote.
                                    PrecioPromedio,
                            Cantidad = ajuste.DiferenciaInventario,
                            TipoAjuste =
                                ajuste.DescripcionAjuste.Equals(
                                    TipoAjusteEnum.Merma.ToString(),
                                    StringComparison.
                                        CurrentCultureIgnoreCase)
                                    ? TipoAjusteEnum.Merma
                                    : TipoAjusteEnum.Superávit,
                            Precio = ajuste.AlmacenInventarioLote.
                                PrecioPromedio,
                            AlmacenInventarioID =
                                ajuste.AlmacenInventarioLote.
                                AlmacenInventario.AlmacenInventarioID,
                            AlmacenMovimientoDetalleID =
                                ajuste.AlmacenMovimientoDetalle.
                                AlmacenMovimientoDetalleID,
                            ProductoID = ajuste.Producto.ProductoId,
                            CantidadInventarioFisico =
                                ajuste.KilogramosFisicos,
                            CantidadInventarioTeorico =
                                ajuste.KilogramosTeoricos,
                            Observaciones =
                                ajuste.AlmacenMovimiento.Observaciones
                        }).ToList();
                    var agrupado =
                        salidasPorAjuste.GroupBy(tipo => new { tipo.TipoAjuste, tipo.AlmacenMovimientoDetalleID }).Select(
                            ajuste => new PolizaEntradaSalidaPorAjusteModel
                            {
                                TipoAjuste = ajuste.Key.TipoAjuste,
                                AlmacenInventarioID = ajuste.First().AlmacenInventarioID,
                                AlmacenMovimientoDetalleID = ajuste.Key.AlmacenMovimientoDetalleID,
                                Cantidad = ajuste.First().Cantidad,
                                CantidadInventarioFisico = ajuste.First().CantidadInventarioFisico,
                                CantidadInventarioTeorico = ajuste.First().CantidadInventarioTeorico,
                                Importe = ajuste.First().Importe,
                                Observaciones = ajuste.First().Observaciones,
                                Precio = ajuste.First().Precio,
                                PrecioInventarioFisico = ajuste.First().PrecioInventarioFisico,
                                PrecioInventarioTeorico = ajuste.First().PrecioInventarioTeorico,
                                ProductoID = ajuste.First().ProductoID
                            }).ToList();
                    if (agrupado != null && agrupado.Any())
                    {
                        streams = new List<MemoryStream>();
                        for (int indexAjustes = 0; indexAjustes < agrupado.Count; indexAjustes++)
                        {
                            var tipoPoliza = TipoPoliza.SalidaAjuste;
                            switch (agrupado[indexAjustes].TipoAjuste)
                            {
                                case TipoAjusteEnum.Superávit:
                                    tipoPoliza = TipoPoliza.EntradaAjuste;
                                    break;
                            }
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
                            var grupo = new List<PolizaEntradaSalidaPorAjusteModel>
                                            {
                                                agrupado[indexAjustes]
                                            };
                            polizas = poliza.GeneraPoliza(grupo);
                            if (polizas != null && polizas.Any())
                            {
                                MemoryStream stream = poliza.ImprimePoliza(grupo, polizas);
                                if (stream != null)
                                {
                                    streams.Add(stream);
                                }
                                var polizaBL = new PolizaBL();
                                polizas.ToList().ForEach(datos =>
                                                             {
                                                                 datos.OrganizacionID = organizacionID;
                                                                 datos.UsuarioCreacionID = usuarioId;
                                                                 datos.ArchivoEnviadoServidor = 1;
                                                             });
                                polizaBL.GuardarServicioPI(polizas, tipoPoliza);
                            }
                        }
                    }

                    #endregion POLIZA


                    transaction.Complete();
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
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
            return streams;
        }

        /// <summary>
        /// Se actualiza inventario y lote
        /// </summary>
        /// <param name="diferenciasDeInventariosInfo"></param>
        /// <param name="usuarioId"></param>
        internal void ActualizarInventarioYLote(DiferenciasDeInventariosInfo diferenciasDeInventariosInfo, int usuarioId)
        {
            var almacenInventarioBl = new AlmacenInventarioBL();
            var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
            //Actualiza inventario
            //Obtenemos el inventario de acuerdo al almacen y producto obtenido
            var inventarioInfo = almacenInventarioBl.ObtenerPorAlmacenIdProductoId(new AlmacenInventarioInfo() { AlmacenID = diferenciasDeInventariosInfo.Almacen.AlmacenID, ProductoID = diferenciasDeInventariosInfo.Producto.ProductoId });
            if (inventarioInfo != null)
            {
                if (diferenciasDeInventariosInfo.AlmacenMovimiento.TipoMovimientoID ==
                    TipoMovimiento.SalidaPorAjuste.GetHashCode())
                {
                    inventarioInfo.Cantidad = inventarioInfo.Cantidad -
                                              diferenciasDeInventariosInfo.DiferenciaInventario;
                    inventarioInfo.Importe = inventarioInfo.PrecioPromedio * inventarioInfo.Cantidad;
                }
                if (diferenciasDeInventariosInfo.AlmacenMovimiento.TipoMovimientoID ==
                    TipoMovimiento.EntradaPorAjuste.GetHashCode())
                {
                    inventarioInfo.Cantidad = inventarioInfo.Cantidad +
                                              diferenciasDeInventariosInfo.DiferenciaInventario;
                    inventarioInfo.Importe = inventarioInfo.PrecioPromedio * inventarioInfo.Cantidad;
                }
                inventarioInfo.UsuarioModificacionID = usuarioId;
                //Actualiza inventario
                almacenInventarioBl.Actualizar(inventarioInfo);
            }

            //Actualiza inventario lote
            var almacenInventarioLoteInfo =
                almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                    diferenciasDeInventariosInfo.AlmacenInventarioLote.AlmacenInventarioLoteId);
            if (almacenInventarioLoteInfo != null)
            {
                if (diferenciasDeInventariosInfo.AlmacenMovimiento.TipoMovimientoID ==
                    TipoMovimiento.SalidaPorAjuste.GetHashCode())
                {
                    almacenInventarioLoteInfo.Cantidad = almacenInventarioLoteInfo.Cantidad -
                                              diferenciasDeInventariosInfo.DiferenciaInventario;
                    almacenInventarioLoteInfo.Importe = almacenInventarioLoteInfo.PrecioPromedio * almacenInventarioLoteInfo.Cantidad;
                }
                if (diferenciasDeInventariosInfo.AlmacenMovimiento.TipoMovimientoID ==
                    TipoMovimiento.EntradaPorAjuste.GetHashCode())
                {
                    almacenInventarioLoteInfo.Cantidad = almacenInventarioLoteInfo.Cantidad +
                                              diferenciasDeInventariosInfo.DiferenciaInventario;
                    almacenInventarioLoteInfo.Importe = almacenInventarioLoteInfo.PrecioPromedio * almacenInventarioLoteInfo.Cantidad;
                }
                almacenInventarioLoteInfo.UsuarioModificacionId = usuarioId;
                //Actualiza inventario
                almacenInventarioLoteBl.Actualizar(almacenInventarioLoteInfo);
                //Kilogramos teoricos en 0 se desactiva el lote
                //if (diferenciasDeInventariosInfo.KilogramosFisicos == 0)
                //{
                //    //Desactivar lote
                //    almacenInventarioLoteBl.DesactivarLote(almacenInventarioLoteInfo);
                //}
            }
        }

        /// <summary>
        /// Obtiene un listado de ajustes pendientes
        /// </summary>
        /// <returns></returns>
        public List<DiferenciasDeInventariosInfo> ObtenerAjustesPendientesPorUsuario(List<EstatusInfo> listaEstatusInfo, List<TipoMovimientoInfo> listaTiposMovimiento, UsuarioInfo usuarioInfo)
        {
            List<DiferenciasDeInventariosInfo> listaAjustesPendientes;

            try
            {
                Logger.Info();
                var diferenciasDeInventarioDal = new DiferenciasDeInventarioDAL();
                listaAjustesPendientes = diferenciasDeInventarioDal.ObtenerAjustesPendientesPorUsuario(listaEstatusInfo, listaTiposMovimiento, usuarioInfo);
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
            return listaAjustesPendientes;
        }
    }
}
