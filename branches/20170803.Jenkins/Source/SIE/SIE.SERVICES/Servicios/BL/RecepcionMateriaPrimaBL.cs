using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Polizas;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    public class RecepcionMateriaPrimaBL
    {
        /// <summary>
        /// Obtiene los pedidos parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<PedidoInfo> ObtenerPedidosParciales(PedidoInfo pedido)
        {
            var listaPedidos = new List<PedidoInfo>();
            try
            {
                Logger.Info();

                var recepcionMateriaPrimaDal = new RecepcionMateriaPrimaDAL();
                listaPedidos = recepcionMateriaPrimaDal.ObtenerPedidosParciales(pedido);
                return listaPedidos;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaPedidos;
        }
        /// <summary>
        /// Obtiene surtido del pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<SurtidoPedidoInfo> ObtenerSurtidoPedido(PedidoInfo pedido)
        {
            var listaPedidos = new List<SurtidoPedidoInfo>();
            try
            {
                Logger.Info();

                var recepcionMateriaPrimaDal = new RecepcionMateriaPrimaDAL();
                listaPedidos = recepcionMateriaPrimaDal.ObtenerSurtidoPedidos(pedido);
                return listaPedidos;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaPedidos;
        }
        /// <summary>
        /// Actualiza la recepcion de materia prima
        /// </summary>
        /// <param name="listaSurtido"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal bool ActualizarRecepcionMateriaPrima(List<SurtidoPedidoInfo> listaSurtido, PedidoInfo pedido)
        {
            bool resultado;
            try
            {
                Logger.Info();
                var almacenInventarioBl = new AlmacenInventarioBL();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                var almacenBl = new AlmacenBL();
                var proveedorChofeBl = new ProveedorChoferBL();
                int proveedorId = 0;
                var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();

                int organizacionID;
                PolizaAbstract poliza;
                IList<PolizaInfo> polizas;
                IList<ResultadoPolizaModel> resultadoPolizaModel;
                var contenedorAlmacenesMovimientos = new List<ContenedorAlmacenMovimientoCierreDia>();
                ContenedorAlmacenMovimientoCierreDia contenedorAlmacenMovimiento;
                var surtidoGenerarPoliza = new List<SurtidoPedidoInfo>();
                using (var transaction = new TransactionScope())
                {
                    ProgramacionMateriaPrimaInfo programacionMateriaPrima;
                    var programacionMateriaPrimaBL = new ProgramacionMateriaPrimaBL();
                    foreach (var surtidoTmp in listaSurtido)
                    {
                        if (surtidoTmp.Seleccionado)
                        {
                            programacionMateriaPrima = programacionMateriaPrimaBL.
                                ObtenerPorProgramacionMateriaPrimaTicket(
                                    surtidoTmp.ProgramacionMateriaPrima.ProgramacionMateriaPrimaId,
                                    surtidoTmp.PesajeMateriaPrima.Ticket);
                            if (programacionMateriaPrima != null)
                            {
                                continue;
                            }
                            surtidoGenerarPoliza.Add(surtidoTmp);

                            var pesaje = pesajeMateriaPrimaBl.ObtenerPorId(surtidoTmp.PesajeMateriaPrima);

                            // Se consulta el proveedor del Proveedor chofer seleccionado para el pesaje.
                            var proveedorChofeInfo = proveedorChofeBl.ObtenerProveedorChoferPorId(
                                surtidoTmp.PesajeMateriaPrima.ProveedorChoferID);

                            if (proveedorChofeInfo != null)
                            {
                                proveedorId = proveedorChofeInfo.Proveedor.ProveedorID;
                            }

                            //SE OBTIENEN LOS DATOS DEL LOTE DESTINO
                            var almacenInventarioLoteDestino =
                                almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                    surtidoTmp.PedidoDetalle.InventarioLoteDestino.AlmacenInventarioLoteId);
                            //SE OBTIENEN LOS DATOS DEL LOTE ORIGEN
                            var almacenInventarioLoteOrigen =
                                almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                    surtidoTmp.ProgramacionMateriaPrima.InventarioLoteOrigen
                                        .AlmacenInventarioLoteId);

                            #region  INVENTARIO ORIGEN

                            // GENERA MOVIMIENTO DE INVENTARIO
                            var almacenMovimiento = almacenBl.GuardarAlmacenMovimiento(new AlmacenMovimientoInfo
                            {
                                AlmacenID =
                                    almacenInventarioLoteOrigen
                                    .AlmacenInventario.
                                    AlmacenID,
                                TipoMovimientoID =
                                    (int)
                                    TipoMovimiento.
                                        PaseProceso,
                                Observaciones = "",
                                Status = (int)EstatusInventario.Aplicado,
                                AnimalMovimientoID = 0,
                                ProveedorId = proveedorId,
                                UsuarioCreacionID =
                                    pedido.
                                    UsuarioModificacion.
                                    UsuarioID
                            });

                            // SE LE ASIGNA EL MOVIMIENTO GENERADO AL PESAJE
                            pesaje.AlmacenMovimientoOrigenId = almacenMovimiento.AlmacenMovimientoID;
                            contenedorAlmacenMovimiento = new ContenedorAlmacenMovimientoCierreDia
                            {
                                AlmacenMovimiento = new AlmacenMovimientoInfo
                                {

                                    AlmacenMovimientoID =
                                        almacenMovimiento.
                                        AlmacenMovimientoID
                                }
                            };
                            contenedorAlmacenesMovimientos.Add(contenedorAlmacenMovimiento);

                            // GENERA EL DETALLE DEL MOVIMIENTO
                            almacenBl.GuardarAlmacenMovimientoDetalleProducto(
                                new AlmacenMovimientoDetalle
                                {
                                    AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                    ProductoID = surtidoTmp.Producto.ProductoId,
                                    Precio = almacenInventarioLoteOrigen.PrecioPromedio,
                                    Cantidad = surtidoTmp.CantidadEntregada,
                                    Importe =
                                        almacenInventarioLoteOrigen.PrecioPromedio * surtidoTmp.CantidadEntregada,
                                    AlmacenInventarioLoteId =
                                        almacenInventarioLoteOrigen.AlmacenInventarioLoteId,
                                    ContratoId = 0,
                                    Piezas = surtidoTmp.PesajeMateriaPrima.Piezas,
                                    UsuarioCreacionID = pedido.UsuarioModificacion.UsuarioID
                                });
                            // GENERA LA SALIDA DEL INVENTARIO LOTE
                            almacenInventarioLoteOrigen.Cantidad -= surtidoTmp.CantidadEntregada;
                            almacenInventarioLoteOrigen.Piezas -= surtidoTmp.PesajeMateriaPrima.Piezas;
                            almacenInventarioLoteOrigen.Importe = almacenInventarioLoteOrigen.PrecioPromedio *
                                                                  almacenInventarioLoteOrigen.Cantidad;
                            almacenInventarioLoteOrigen.UsuarioModificacionId =
                                pedido.UsuarioModificacion.UsuarioID;
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLoteOrigen);

                            // GENERA LA SALIDA DEL ALMACEN INVENTARIO
                            var almacenInventarioInfo =
                                almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                    almacenInventarioLoteOrigen.AlmacenInventario.AlmacenInventarioID);
                            almacenInventarioInfo.Cantidad = almacenInventarioInfo.Cantidad -
                                                             surtidoTmp.CantidadEntregada;
                            almacenInventarioInfo.Importe = almacenInventarioInfo.PrecioPromedio *
                                                            almacenInventarioInfo.Cantidad;
                            almacenInventarioInfo.UsuarioModificacionID = pedido.UsuarioModificacion.UsuarioID;
                            almacenInventarioBl.Actualizar(almacenInventarioInfo);

                            #endregion

                            #region  INVENTARIO DESTINO
                            //Obtener costos de flete
                            var fleteInternoPl = new FleteInternoPL();
                            var listadoCostos = fleteInternoPl.ObtenerCostosPorFleteConfiguracion(new FleteInternoInfo
                            {
                                Organizacion =
                                    new OrganizacionInfo { OrganizacionID = pedido.Organizacion.OrganizacionID },
                                AlmacenOrigen = new AlmacenInfo { AlmacenID = almacenInventarioLoteOrigen.AlmacenInventario.AlmacenID },
                                Producto = new ProductoInfo { ProductoId = surtidoTmp.Producto.ProductoId }
                            }, new ProveedorInfo { ProveedorID = surtidoTmp.Proveedor.ProveedorID });

                            decimal importeCostos = 0;
                            if (listadoCostos != null)
                            {
                                foreach (var fleteInternoCostoInfo in listadoCostos)
                                {
                                    if (fleteInternoCostoInfo.TipoTarifaID == TipoTarifaEnum.Viaje.GetHashCode())
                                    {
                                        importeCostos += fleteInternoCostoInfo.Tarifa;
                                    }
                                    else
                                    {
                                        importeCostos += (surtidoTmp.CantidadEntregada / 1000) *
                                                         fleteInternoCostoInfo.Tarifa;
                                    }
                                }
                                //importeCostos = listadoCostos.Sum(fleteInternoCostoInfo => fleteInternoCostoInfo.Tarifa);
                            }

                            almacenMovimiento = almacenBl.GuardarAlmacenMovimiento(new AlmacenMovimientoInfo
                            {
                                AlmacenID =
                                    almacenInventarioLoteDestino
                                    .AlmacenInventario.
                                    AlmacenID,
                                TipoMovimientoID =
                                    (int)
                                    TipoMovimiento.
                                        RecepcionAProceso,
                                Observaciones =
                                    pedido.Observaciones,
                                Status = (int)EstatusInventario.Aplicado,
                                AnimalMovimientoID = 0,
                                ProveedorId = proveedorId,
                                UsuarioCreacionID =
                                    pedido.
                                    UsuarioModificacion.
                                    UsuarioID
                            });

                            // SE LE ASIGNA EL MOVIMIENTO AL PESAJE
                            pesaje.AlmacenMovimientoDestinoId = almacenMovimiento.AlmacenMovimientoID;

                            // GENERA LA ENTRADA DEL INVENTARIO LOTE
                            almacenInventarioLoteDestino.Piezas += surtidoTmp.PesajeMateriaPrima.Piezas;
                            decimal importe = surtidoTmp.CantidadEntregada *
                                              almacenInventarioLoteOrigen.PrecioPromedio;
                            almacenInventarioLoteDestino.Importe += importe;
                            almacenInventarioLoteDestino.Cantidad += surtidoTmp.CantidadEntregada;
                            almacenInventarioLoteDestino.PrecioPromedio = almacenInventarioLoteDestino.Importe /
                                                                          almacenInventarioLoteDestino.Cantidad;
                            almacenInventarioLoteDestino.UsuarioModificacionId =
                                pedido.UsuarioModificacion.UsuarioID;
                            //
                            //Se agregan los costos al importe del lote
                            //importeCostos = importeCostos * surtidoTmp.CantidadEntregada;
                            almacenInventarioLoteDestino.Importe = almacenInventarioLoteDestino.Importe + importeCostos;
                            almacenInventarioLoteDestino.PrecioPromedio = almacenInventarioLoteDestino.Importe /
                                                                          almacenInventarioLoteDestino.Cantidad;
                            //
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLoteDestino);

                            // GENERA EL DETALLE DEL MOVIMIENTO
                            almacenBl.GuardarAlmacenMovimientoDetalleProducto(
                                new AlmacenMovimientoDetalle
                                {
                                    AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                    ProductoID = surtidoTmp.Producto.ProductoId,
                                    Precio = almacenInventarioLoteOrigen.PrecioPromedio,
                                    Cantidad = surtidoTmp.CantidadEntregada,
                                    Importe = importe,
                                    AlmacenInventarioLoteId =
                                        almacenInventarioLoteDestino.AlmacenInventarioLoteId,
                                    ContratoId = 0,
                                    Piezas = surtidoTmp.PesajeMateriaPrima.Piezas,
                                    UsuarioCreacionID = pedido.UsuarioModificacion.UsuarioID
                                });

                            //Guarda almacen movimiento costo
                            if (listadoCostos != null)
                            {
                                foreach (var fleteInternoCostoInfo in listadoCostos)
                                {
                                    var almacenMovimientoCosto = new AlmacenMovimientoCostoBL();
                                    decimal importeCostoFlete = 0;

                                    if (fleteInternoCostoInfo.TipoTarifaID == TipoTarifaEnum.Viaje.GetHashCode())
                                    {
                                        importeCostoFlete = fleteInternoCostoInfo.Tarifa;
                                    }
                                    else
                                    {
                                        importeCostoFlete = (surtidoTmp.CantidadEntregada / 1000) *
                                                         fleteInternoCostoInfo.Tarifa;
                                    }

                                    almacenMovimientoCosto.Crear(new AlmacenMovimientoCostoInfo
                                    {
                                        AlmacenMovimientoId = almacenMovimiento.AlmacenMovimientoID,
                                        ProveedorId = surtidoTmp.Proveedor.ProveedorID,
                                        CostoId = fleteInternoCostoInfo.Costo.CostoID,
                                        Importe = importeCostoFlete,
                                        Cantidad = surtidoTmp.CantidadEntregada,
                                        Activo = EstatusEnum.Activo,
                                        UsuarioCreacionId = pedido.UsuarioModificacion.UsuarioID
                                    });
                                }
                            }
                            //                            
                            programacionMateriaPrimaBL.ActualizarAlmacenMovimiento(
                                surtidoTmp.ProgramacionMateriaPrima.ProgramacionMateriaPrimaId,
                                almacenMovimiento.AlmacenMovimientoID);

                            // GENERA LA ENTRADA DEL ALMACEN INVENTARIO
                            almacenInventarioInfo =
                                almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                    almacenInventarioLoteDestino.AlmacenInventario.AlmacenInventarioID);

                            List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote =
                                almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventarioInfo);

                            var almacenInventario = new AlmacenInventarioInfo();
                            almacenInventario.AlmacenInventarioID = almacenInventarioInfo.AlmacenInventarioID;
                            // SE SUMAN LAS CANTIDADES E IMPORTES QUE TIENE EL ALMACEN
                            foreach (var almacenInventarioLoteInfo in listaAlmacenInventarioLote)
                            {
                                almacenInventario.Cantidad += almacenInventarioLoteInfo.Cantidad;
                                almacenInventario.Importe += almacenInventarioLoteInfo.Importe;
                            }

                            almacenInventario.PrecioPromedio = almacenInventario.Importe /
                                                               almacenInventario.Cantidad;

                            almacenInventario.UsuarioModificacionID = pedido.UsuarioModificacion.UsuarioID;
                            almacenInventario.ProductoID = surtidoTmp.Producto.ProductoId;
                            almacenInventario.AlmacenID =
                                almacenInventarioLoteDestino.AlmacenInventario.AlmacenID;
                            almacenInventarioBl.ActualizarPorProductoId(almacenInventario);

                            #endregion

                            // SE ACTUALIZA EL PESAJE DE MATERIA PRIMA
                            pesaje.EstatusID = (int)Estatus.PedidoCompletado;
                            pesaje.Activo = false;
                            pesaje.UsuarioModificacionID = pedido.UsuarioModificacion.UsuarioID;
                            pesajeMateriaPrimaBl.ActualizarPesajePorId(pesaje);
                        }
                    }

                    #region POLIZA

                    organizacionID = pedido.Organizacion.OrganizacionID;
                    string lotes = ObtenerXMLLote(listaSurtido);
                    var pesajeMateriaPrimaBL = new PesajeMateriaPrimaBL();
                    List<PolizaPaseProcesoModel> datosPoliza =
                        pesajeMateriaPrimaBL.ObtenerValoresPolizaPaseProceso(pedido.FolioPedido, organizacionID,
                                                                             lotes);
                    if (datosPoliza != null)
                    {
                        datosPoliza = (from dp in datosPoliza
                                       from ls in surtidoGenerarPoliza
                                       where dp.Proveedor.ProveedorID == ls.Proveedor.ProveedorID
                                             && dp.Producto.ProductoId == ls.Producto.ProductoId
                                             && dp.PesajeMateriaPrima.Ticket == ls.PesajeMateriaPrima.Ticket
                                       select dp).ToList();
                    }
                    if (datosPoliza != null && datosPoliza.Any())
                    {
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PaseProceso);
                        IList<PolizaPaseProcesoModel> polizasPaseProcesoModel;
                        resultadoPolizaModel = new List<ResultadoPolizaModel>();
                        datosPoliza =
                            datosPoliza.GroupBy(prod => new { prod.Producto, prod.ProveedorChofer, prod.PesajeMateriaPrima.Ticket })
                                .Select(grupo => new PolizaPaseProcesoModel
                                {
                                    Organizacion = grupo.Select(org => org.Organizacion).FirstOrDefault(),
                                    Almacen = grupo.Select(alm => alm.Almacen).FirstOrDefault(),
                                    Producto = grupo.Key.Producto,
                                    Proveedor = grupo.Select(prov => prov.Proveedor).FirstOrDefault(),
                                    AlmacenMovimiento = grupo.Select(alm => alm.AlmacenMovimiento).FirstOrDefault(),
                                    AlmacenMovimientoDetalle = grupo.Select(alm => alm.AlmacenMovimientoDetalle).FirstOrDefault(),
                                    AlmacenInventarioLote = grupo.Select(alm => alm.AlmacenInventarioLote).FirstOrDefault(),
                                    FleteInterno = grupo.Select(flete => flete.FleteInterno).FirstOrDefault(),
                                    FleteInternoCosto = grupo.Select(flete => flete.FleteInternoCosto).FirstOrDefault(),
                                    Pedido = grupo.Select(ped => ped.Pedido).FirstOrDefault(),
                                    ProveedorChofer = grupo.Key.ProveedorChofer,
                                    PesajeMateriaPrima = grupo.Select(pesaje => pesaje.PesajeMateriaPrima).FirstOrDefault(),
                                    ProgramacionMateriaPrima = grupo.Select(prog => prog.ProgramacionMateriaPrima).FirstOrDefault(),
                                    ListaAlmacenMovimientoCosto = grupo.Select(prog => prog.ListaAlmacenMovimientoCosto).FirstOrDefault(),
                                }).ToList();
                        IList<PolizaInfo> polizaExistente;
                        var polizaBL = new PolizaBL();
                        for (var indexPoliza = 0; indexPoliza < datosPoliza.Count; indexPoliza++)
                        {
                            polizasPaseProcesoModel = new List<PolizaPaseProcesoModel> { datosPoliza[indexPoliza] };
                            polizas = poliza.GeneraPoliza(polizasPaseProcesoModel);
                            if (polizas != null)
                            {
                                var resultadoPoliza = new ResultadoPolizaModel
                                {
                                    Polizas = polizas
                                };
                                polizas.ToList().ForEach(datos =>
                                {
                                    datos.OrganizacionID = organizacionID;
                                    datos.UsuarioCreacionID =
                                        pedido.UsuarioModificacion.UsuarioID;
                                    datos.ArchivoEnviadoServidor = 1;
                                });
                                polizaExistente = polizaBL.ObtenerPoliza(TipoPoliza.PaseProceso, organizacionID,
                                                                         pedido.FechaPedido,
                                                                         string.Format("{0}-{1}", pedido.FolioPedido,
                                                                                       datosPoliza[indexPoliza].
                                                                                           PesajeMateriaPrima.Ticket),
                                                                         "PP", 1);
                                if (polizaExistente != null && polizaExistente.Any())
                                {
                                    List<PolizaInfo> excluir = (from existente in polizaExistente
                                                                join guardar in polizas on existente.Concepto equals guardar.Concepto
                                                                select guardar).ToList();
                                    polizas = polizas.Except(excluir).ToList();
                                }
                                if (polizas.Any())
                                {
                                    polizaBL.GuardarServicioPI(polizas, TipoPoliza.PaseProceso);
                                    resultadoPolizaModel.Add(resultadoPoliza);
                                }
                            }
                        }
                        var almacenMovimientoBL = new AlmacenMovimientoBL();
                        contenedorAlmacenesMovimientos.ForEach(alm => alm.Almacen = new AlmacenInfo
                        {
                            UsuarioModificacionID =
                                pedido.
                                UsuarioModificacion.
                                UsuarioID
                        });
                        almacenMovimientoBL.ActualizarGeneracionPoliza(contenedorAlmacenesMovimientos);
                    }

                    #endregion POLIZA

                    transaction.Complete();
                    resultado = true;
                }
            }
            catch (ExcepcionGenerica exg)
            {
                resultado = false;
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                resultado = false;
                Logger.Error(ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un xml con los lotes recibidos
        /// </summary>
        /// <param name="listaSurtido"></param>
        /// <returns></returns>
        private string ObtenerXMLLote(List<SurtidoPedidoInfo> listaSurtido)
        {
            List<int> lotes = listaSurtido.Select(sur => sur.AlmacenInventarioLote.AlmacenInventarioLoteId).ToList();
            var xml =
                new XElement("ROOT",
                             from surtido in lotes.Distinct()
                             select new XElement("Lotes",
                                                 new XElement("Lote",
                                                              surtido)
                                 ));
            return xml.ToString();
        }

        /// <summary>
        /// Genera el documento de la boleta de recepcion.
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="entradaProducto"></param>
        internal void ImpresionBoletaRecepcion(ImpresionBoletaRecepcionInfo datosReporte, EntradaProductoInfo entradaProducto)
        {
            try
            {
                if (datosReporte != null && entradaProducto != null)
                {

                    var subfamilia = entradaProducto.Producto.SubFamilia;

                    if (subfamilia != null)
                    {
                        if (subfamilia.SubFamiliaID == ((int)SubFamiliasEnum.Forrajes))
                        {
                            ImpresionBoletaRecepcionForraje(datosReporte, entradaProducto);
                        }
                        else
                        {
                            ImpresionBoletaRecepcionMateriaPrima(datosReporte, entradaProducto);
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), exception);
            }

        }

        /// <summary>
        /// Genera el documento de la boleta recepcion de materia prima.
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        private void ImpresionBoletaRecepcionMateriaPrima(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            var impresionBl = new ImpresionBoletaRecepcionBL();
            impresionBl.ImprimirBoletaMateriaPrima(etiquetas, entradaProducto);
        }

        /// <summary>
        /// Genera el documento de la boleta recepción forraje.
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        private void ImpresionBoletaRecepcionForraje(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            var impresionBl = new ImpresionBoletaRecepcionBL();
            impresionBl.ImprimirBoletaForraje(etiquetas, entradaProducto);
        }
    }
}
