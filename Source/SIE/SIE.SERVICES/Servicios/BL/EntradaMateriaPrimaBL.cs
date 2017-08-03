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
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using System.IO;

namespace SIE.Services.Servicios.BL
{
    internal class EntradaMateriaPrimaBL
    {
        /// <summary>
        /// Obtiene una los costros de los fletes de un contrato
        /// </summary>
        /// <returns></returns>
        internal List<CostoEntradaMateriaPrimaInfo> ObtenerCostosFletes(ContenedorEntradaMateriaPrimaInfo contenedorMateriaPrima, CostoEntradaMateriaPrimaInfo pesosCostos)
        {
            try
            {
                Logger.Info();
                var entradaMateriaPrimaDal = new EntradaMateriaPrimaDAL();
                List<CostoEntradaMateriaPrimaInfo> resultados = entradaMateriaPrimaDal.ObtenerCostosFletes(contenedorMateriaPrima.Contrato, contenedorMateriaPrima.EntradaProducto);
                if (resultados != null)
                {
                    var costoBl = new CostoBL();
                    var provedorBl = new ProveedorBL();
                    foreach (var tmpResultado in resultados)
                    {
                        tmpResultado.KilosEntrada = pesosCostos.KilosEntrada;
                        tmpResultado.KilosOrigen = pesosCostos.KilosOrigen;

                        tmpResultado.Costos = costoBl.ObtenerPorID(tmpResultado.FleteDetalle.CostoID);
                        tmpResultado.TieneCuenta = false;
                        tmpResultado.Provedor = provedorBl.ObtenerPorID(tmpResultado.Flete.Proveedor.ProveedorID);
                    }
                }

                return resultados;
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
        /// Guarda los costos de la entrada materia prima
        /// </summary>
        /// <param name="entradaMateriaPrima">Datos necesarios para guardar</param>
        /// <returns></returns>
        internal MemoryStream GuardarEntradaMateriaPrima(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima)
        {
            MemoryStream pdf = null;
            try
            {
                Logger.Info();
                var entradaMateriaPrimaDal = new EntradaMateriaPrimaDAL();
                var almacenMovimientoBL = new AlmacenMovimientoBL();


                var listaMovimientosContrato = new List<AlmacenMovimientoInfo>();
                if (entradaMateriaPrima.Contrato != null && entradaMateriaPrima.Contrato.ContratoId > 0)
                {
                    listaMovimientosContrato = almacenMovimientoBL.ObtenerMovimientosPorContrato(entradaMateriaPrima.Contrato);
                }

                

                if (listaMovimientosContrato == null)
                {
                    listaMovimientosContrato = new List<AlmacenMovimientoInfo>();
                }

                PolizaAbstract poliza;
                IList<PolizaInfo> polizas;
                using (var transaction = new TransactionScope())
                {
                    if (entradaMateriaPrima.ListaCostoEntradaMateriaPrima != null)
                    {
                        if (entradaMateriaPrima.ListaCostoEntradaMateriaPrima.Count > 0)
                        {
                            entradaMateriaPrimaDal.GuardarEntradaMateriaPrima(entradaMateriaPrima);
                        }
                    }

                    if ((entradaMateriaPrima.Contrato.TipoContrato != null &&
                         (entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaNormal ||
                          entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero ||
                          entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.EnTransito)
                        ) || entradaMateriaPrima.EntradaProducto.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                    {
                        List<AlmacenMovimientoInfo> listaMovimientos = GuardarAlmacenMovimiento(entradaMateriaPrima, listaMovimientosContrato);
                        
                        if (listaMovimientos != null)
                        {
                            if (entradaMateriaPrima.Contrato != null)
                            {
                                if (
                                    entradaMateriaPrima.Contrato.Parcial == CompraParcialEnum.Parcial &&
                                    entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
                                {
                                    var entradaProductoParcialBl = new EntradaProductoParcialBL();
                                    bool grabo = entradaProductoParcialBl.Crear(entradaMateriaPrima);
                                    if (!grabo)
                                    {
                                        throw new ExcepcionDesconocida(
                                            Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                                    }
                                }
                            }

                            List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle =
                                GuardarMovimientoDetalle(entradaMateriaPrima, listaMovimientos, listaMovimientosContrato);

                            GuardarCosto(entradaMateriaPrima, listaMovimientos);
                            //aki
                            //Se obtienen los importes de los productos agregados a la premezcla
                            if (entradaMateriaPrima.EntradaProducto.Producto.SubFamilia.SubFamiliaID ==
                                (int)SubFamiliasEnum.MicroIngredientes)
                            {
                                decimal costosProductosPremezcla = 0;
                                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                                foreach (var ingredientePremezcla in
                                        entradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos)
                                {
                                    //Crear el movimiento en AlmacenMovimientoDetalle
                                    var almacenMovimientoDetalle = new AlmacenMovimientoDetalle();
                                    almacenMovimientoDetalle.AlmacenInventarioLoteId =
                                        ingredientePremezcla.Lote.AlmacenInventarioLoteId;
                                    var almacenInventarioLoteInfo =
                                        almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                            almacenMovimientoDetalle.AlmacenInventarioLoteId);
                                    costosProductosPremezcla += almacenInventarioLoteInfo.PrecioPromedio *
                                                                ingredientePremezcla.Kilogramos;
                                }
                                entradaMateriaPrima.CostoProductosPremezcla = costosProductosPremezcla;
                            }

                            if (listaAlmacenMovimientoDetalle != null)
                            {
                                GuardarInventario(entradaMateriaPrima, listaMovimientos, listaAlmacenMovimientoDetalle);
                            }
                            else
                            {
                                throw new ExcepcionDesconocida(
                                    Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                            }

                            //Si es una premezcla decrementar los ingredientes del inventario
                            if (entradaMateriaPrima.EntradaProducto.Producto.SubFamilia.SubFamiliaID ==
                                (int)SubFamiliasEnum.MicroIngredientes)
                            {
                                DecrementarIngredientesPremezclaDeInventario(entradaMateriaPrima, listaMovimientos);
                            }

                            #region POLIZA

                            var polizaBL = new PolizaBL();

                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaCompra);
                            var entradaProductoBL = new EntradaProductoBL();
                            ContenedorEntradaMateriaPrimaInfo contenedorEntrada =
                                entradaProductoBL.ObtenerPorFolioEntradaContrato(
                                    entradaMateriaPrima.EntradaProducto.Folio,
                                    entradaMateriaPrima.Contrato.ContratoId,
                                    entradaMateriaPrima.EntradaProducto.Organizacion.OrganizacionID);
                            contenedorEntrada.aplicaRestriccionDescuento = entradaMateriaPrima.aplicaRestriccionDescuento;
                            contenedorEntrada.PorcentajeRestriccionDescuento = entradaMateriaPrima.PorcentajeRestriccionDescuento;
                            contenedorEntrada.EntradaProducto.NotaDeVenta = entradaMateriaPrima.EntradaProducto.NotaDeVenta;
                            if (contenedorEntrada.aplicaRestriccionDescuento)
                            {
                                contenedorEntrada.EntradaProducto.PesoDescuento = 0;
                            }

                            if (contenedorEntrada != null)
                            {
                                contenedorEntrada.ListaCostoEntradaMateriaPrima =
                                    entradaMateriaPrima.ListaCostoEntradaMateriaPrima;
                                polizas = poliza.GeneraPoliza(contenedorEntrada);
                                if (polizas != null)
                                {
                                    pdf = poliza.ImprimePoliza(contenedorEntrada, polizas);
                                    polizas.ToList().ForEach(claves =>
                                                                 {
                                                                     claves.OrganizacionID =
                                                                         entradaMateriaPrima.Contrato.Organizacion.
                                                                             OrganizacionID;
                                                                     claves.UsuarioCreacionID =
                                                                         entradaMateriaPrima.UsuarioId;
                                                                     claves.ArchivoEnviadoServidor = 1;
                                                                 });
                                    List<string> referencias = polizas.Select(ref3 => ref3.Referencia3).Distinct().ToList();
                                    List<PolizaInfo> polizasEntradaCompra;
                                    for (int indexRef3 = 0; indexRef3 < referencias.Count; indexRef3++)
                                    {
                                        polizasEntradaCompra = polizas.Where(ref3 =>
                                                                             ref3.Referencia3
                                                                                 .Equals(referencias[indexRef3])).ToList();
                                        polizaBL.GuardarServicioPI(polizasEntradaCompra, TipoPoliza.EntradaCompra);
                                    }
                                }
                            }

                            if (entradaMateriaPrima.EntradaProducto.PremezclaInfo != null
                                    && entradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos != null
                                    && entradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos.Any())
                            {
                                poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaSubProducto);
                                contenedorEntrada.EntradaProducto.PremezclaInfo =
                                    entradaMateriaPrima.EntradaProducto.PremezclaInfo;
                                IList<PolizaInfo> polizaPremezcla =
                                    poliza.GeneraPoliza(contenedorEntrada);
                                if (polizaPremezcla != null && polizaPremezcla.Any())
                                {
                                    polizaPremezcla.ToList().ForEach(claves =>
                                                                         {
                                                                             claves.OrganizacionID =
                                                                                 entradaMateriaPrima.Contrato.
                                                                                     Organizacion.
                                                                                     OrganizacionID;
                                                                             claves.UsuarioCreacionID =
                                                                                 entradaMateriaPrima.UsuarioId;
                                                                             claves.ArchivoEnviadoServidor = 1;
                                                                         });
                                    polizaBL.GuardarServicioPI(polizaPremezcla, TipoPoliza.PolizaSubProducto);
                                }
                            }
                            var movimientos = new List<ContenedorAlmacenMovimientoCierreDia>();
                            listaMovimientos.ForEach(movs =>
                                                         {
                                                             var contenedor = new ContenedorAlmacenMovimientoCierreDia
                                                                                  {
                                                                                      AlmacenMovimiento =
                                                                                          new AlmacenMovimientoInfo
                                                                                              {
                                                                                                  AlmacenMovimientoID
                                                                                                      =
                                                                                                      movs.
                                                                                                      AlmacenMovimientoID
                                                                                              }
                                                                                  };
                                                             movimientos.Add(contenedor);
                                                         });
                            movimientos.ForEach(alm => alm.Almacen = new AlmacenInfo
                                                                         {
                                                                             UsuarioModificacionID =
                                                                                 entradaMateriaPrima.UsuarioId
                                                                         });
                            almacenMovimientoBL.ActualizarGeneracionPoliza(movimientos);

                            #endregion POLIZA
                        }
                        else
                        {
                            throw new ExcepcionDesconocida(Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                        }
                    }
                    transaction.Complete();
                }
                return pdf;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (ExcepcionDesconocida)
            {
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
        }

        /// <summary>
        /// Si es una premezcla decrementar los ingredientes del inventario
        /// </summary>
        private void DecrementarIngredientesPremezclaDeInventario(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima, List<AlmacenMovimientoInfo> listaMovimientos)
        {
            try
            {
                var almacenBl = new AlmacenBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();

                var almacenMovimientoInfo = listaMovimientos.FirstOrDefault();
                if (almacenMovimientoInfo != null)
                {
                    AlmacenInfo almacen = almacenBl.ObtenerPorID(almacenMovimientoInfo.AlmacenID);

                    if (entradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos.Any())
                    {
                        var almacenMovimiento = new AlmacenMovimientoInfo
                        {
                            TipoMovimientoID = (int)TipoMovimiento.SalidaAlmacen,
                            ProveedorId = entradaMateriaPrima.Contrato.Proveedor.ProveedorID,
                            Status = (int)EstatusInventario.Aplicado,
                            UsuarioCreacionID = entradaMateriaPrima.UsuarioId,
                            AlmacenID = almacen.AlmacenID
                        };
                        //Crear el movimiento en AlmacenMovimiento
                        AlmacenMovimientoInfo almacenMovimientoGenerado =
                            almacenBl.GuardarAlmacenMovimiento(almacenMovimiento);

                        /* Almacenar los movimientos de entrada y de salida de los productos de la premezcla   */
                        var entradaPremezclaBl = new EntradaPremezclaBL();
                        var entradaPremezclaInfo = new EntradaPremezclaInfo
                        {
                            AlmacenMovimientoIDEntrada = almacenMovimientoInfo.AlmacenMovimientoID,
                            AlmacenMovimientoIDSalida = almacenMovimientoGenerado.AlmacenMovimientoID,
                            UsuarioCreacionID = entradaMateriaPrima.UsuarioId
                        };
                        entradaPremezclaBl.Guardar(entradaPremezclaInfo);

                        List<AlmacenInventarioInfo> listaAlmacenlmacenInventario = almacenInventarioBl.ObtienePorAlmacenId(almacen);

                        foreach (var ingredientePremezcla in
                                entradaMateriaPrima.EntradaProducto.PremezclaInfo.ListaPremezclaDetalleInfos)
                        {
                            var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();

                            //Crear el movimiento en AlmacenMovimientoDetalle
                            var almacenMovimientoDetalle = new AlmacenMovimientoDetalle();
                            almacenMovimientoDetalle.AlmacenMovimientoID = almacenMovimientoGenerado.AlmacenMovimientoID;
                            almacenMovimientoDetalle.AlmacenInventarioLoteId =
                                ingredientePremezcla.Lote.AlmacenInventarioLoteId;
                            var almacenInventarioLoteInfo =
                                almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                    almacenMovimientoDetalle.AlmacenInventarioLoteId);
                            almacenMovimientoDetalle.Piezas = almacenInventarioLoteInfo.Piezas;
                            almacenMovimientoDetalle.ProductoID = ingredientePremezcla.Producto.ProductoId;
                            almacenMovimientoDetalle.Precio = 0;
                            almacenMovimientoDetalle.Precio = almacenInventarioLoteInfo.PrecioPromedio;
                            almacenMovimientoDetalle.Cantidad = ingredientePremezcla.Kilogramos;
                            almacenMovimientoDetalle.Importe = almacenMovimientoDetalle.Precio *
                                                               almacenMovimientoDetalle.Cantidad;
                            almacenMovimientoDetalle.UsuarioCreacionID = entradaMateriaPrima.UsuarioId;
                            //Se crea el detalle del movimiento de la salida del almacen
                            almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalle);

                            //Crear el decemento del inventario
                            almacenInventarioLoteInfo.Cantidad = almacenInventarioLoteInfo.Cantidad -
                                                                 almacenMovimientoDetalle.Cantidad;
                            almacenInventarioLoteInfo.Importe = almacenInventarioLoteInfo.Importe -
                                                                almacenMovimientoDetalle.Importe;
                            almacenInventarioLoteInfo.UsuarioModificacionId = entradaMateriaPrima.UsuarioId;
                            //Se actualiza el inventario
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLoteInfo);
                            

                            if (listaAlmacenlmacenInventario != null)
                            {
                                AlmacenInventarioInfo inventarioProducto = listaAlmacenlmacenInventario.FirstOrDefault(
                                    registro => registro.ProductoID == ingredientePremezcla.Producto.ProductoId);

                                //Crear el Inventario del lote
                                var almacenInventario = new AlmacenInventarioInfo();
                                List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote =
                                    almacenInventarioLoteBl.ObtenerPorAlmacenInventarioIDLigero(inventarioProducto);
                                if(listaAlmacenInventarioLote != null)
                                {
                                    almacenInventario.Cantidad = almacenInventario.Cantidad + listaAlmacenInventarioLote.Sum(lote=> lote.Cantidad);
                                    almacenInventario.Importe = almacenInventario.Importe + listaAlmacenInventarioLote.Sum(lote => lote.Importe);
                                }
                                //foreach (var almacenInventarioLote in listaAlmacenInventarioLote)
                                //{
                                //    almacenInventario.Cantidad = almacenInventario.Cantidad +
                                //                                 almacenInventarioLote.Cantidad;
                                //    almacenInventario.Importe = almacenInventario.Importe +
                                //                                almacenInventarioLote.Importe;
                                //    almacenInventario.PrecioPromedio = almacenInventarioLote.PrecioPromedio;
                                //}
                                //Actualiza inventario
                                if (inventarioProducto != null)
                                    almacenInventario.AlmacenInventarioID = inventarioProducto.AlmacenInventarioID;

                                if (almacenInventario.Cantidad > 0)
                                {
                                    almacenInventario.PrecioPromedio = almacenInventario.Importe /
                                                                       almacenInventario.Cantidad;
                                }
                                almacenInventario.UsuarioModificacionID = entradaMateriaPrima.UsuarioId;
                                almacenInventario.ProductoID = ingredientePremezcla.Producto.ProductoId;
                                almacenInventario.AlmacenID = almacen.AlmacenID;
                                almacenInventarioBl.ActualizarPorProductoId(almacenInventario);
                            }
                        }
                    }
                }

            }
            catch (ExcepcionDesconocida)
            {
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
        }

        /// <summary>
        /// Funcion que guarda los registros en almacenmovimiento
        /// </summary>
        /// <param name="entradaMateriaPrima"></param>
        /// <param name="listaMovimientosContrato"></param>
        /// <returns>Lista de almacenmovimiento</returns>
        internal List<AlmacenMovimientoInfo> GuardarAlmacenMovimiento(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima, List<AlmacenMovimientoInfo> listaMovimientosContrato)
        {
            var almacenBl = new AlmacenBL();
            var almacenMovimiento = new AlmacenMovimientoInfo();
            var listaAlmacenMovimiento = new List<AlmacenMovimientoInfo>();
            var proveedorAlmacenBl = new ProveedorAlmacenBL();
            var proveedorAlmacen = new ProveedorAlmacenInfo();
            var listaAlmacenMovimientoGenerado = new List<AlmacenMovimientoInfo>();
            var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
            try
            {
                Logger.Info();

                AlmacenInventarioLoteInfo almacenInventarioLoteInfo = almacenInventarioLoteBl
                  .ObtenerAlmacenInventarioLotePorId(
                      entradaMateriaPrima.EntradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);

                if (entradaMateriaPrima.TipoEntrada == TipoEntradaEnum.Compra.ToString())
                {
                    //AlmacenInventarioLoteInfo almacenInventarioLoteInfo = almacenInventarioLoteBl
                    //    .ObtenerAlmacenInventarioLotePorId(
                    //        entradaMateriaPrima.EntradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                    if (almacenInventarioLoteInfo != null)
                    {
                        almacenMovimiento.AlmacenID = almacenInventarioLoteInfo.AlmacenInventario.Almacen.AlmacenID;
                    }
                }
                else if (entradaMateriaPrima.TipoEntrada == TipoEntradaEnum.Traspaso.ToString())
                {
                    proveedorAlmacen = proveedorAlmacenBl.ObtenerPorProveedorId(entradaMateriaPrima.Contrato.Proveedor);
                    almacenMovimiento.AlmacenID = proveedorAlmacen.AlmacenId;
                }

                //Se valida si es uan entrada de una premezcla
                if (entradaMateriaPrima.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                {
                    almacenMovimiento.TipoMovimientoID = (int)TipoMovimiento.EntradaPorCompra;
                    almacenMovimiento.ProveedorId = entradaMateriaPrima.Contrato.Proveedor.ProveedorID;
                    almacenMovimiento.Status = (int)EstatusInventario.Aplicado;
                    listaAlmacenMovimiento.Add(almacenMovimiento);
                }
                else if (entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaNormal)
                {
                    almacenMovimiento.TipoMovimientoID = (int)TipoMovimiento.EntradaPorCompra;
                    almacenMovimiento.ProveedorId = entradaMateriaPrima.Contrato.Proveedor.ProveedorID;
                    almacenMovimiento.Status = (int)EstatusInventario.Aplicado;

                    listaAlmacenMovimiento.Add(almacenMovimiento);
                }
                else if (entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero ||
                            entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.EnTransito)
                {
                    almacenMovimiento.TipoMovimientoID = (int)TipoMovimiento.ProductoSalidaTraspaso;
                    var almacenMovimientoEntrada = new AlmacenMovimientoInfo();
                    almacenMovimiento.Status = (int)EstatusInventario.Aplicado;

                    if (almacenInventarioLoteInfo != null)
                    {
                        almacenMovimientoEntrada.AlmacenID = almacenInventarioLoteInfo.AlmacenInventario.Almacen.AlmacenID;
                    }

                    almacenMovimientoEntrada.TipoMovimientoID = (int)TipoMovimiento.EntradaAlmacen;
                    almacenMovimientoEntrada.ProveedorId = entradaMateriaPrima.Contrato.Proveedor.ProveedorID;
                    almacenMovimientoEntrada.Status = (int)EstatusInventario.Aplicado;

                    AlmacenMovimientoInfo almacenMovimientoConsulta =
                        listaMovimientosContrato.FirstOrDefault(
                            mov => mov.TipoMovimientoID == TipoMovimiento.EntradaBodegaTerceros.GetHashCode());

                    if (almacenMovimientoConsulta != null)
                    {
                        almacenMovimiento.AlmacenID = almacenMovimientoConsulta.AlmacenID;
                    }

                    //List<AlmacenMovimientoDetalle> listaMovimientos =
                    //    almacenBl.ObtenerAlmacenMovimientoPorContrato(entradaMateriaPrima.Contrato);
                    //if (listaMovimientos != null)
                    //{
                    //    foreach (var almacenMovimientoDetalle in listaMovimientos)
                    //    {
                    //        var almacenMovimientoBl = new AlmacenMovimientoBL();
                    //        var almacenMovimientoConsulta =
                    //            almacenMovimientoBl.ObtenerPorId(almacenMovimientoDetalle.AlmacenMovimientoID);
                    //        if (almacenMovimientoConsulta != null)
                    //        {
                    //            if (almacenMovimientoConsulta.TipoMovimientoID ==
                    //                (int)TipoMovimiento.EntradaBodegaTerceros)
                    //            {
                    //                almacenMovimiento.AlmacenID = almacenMovimientoConsulta.AlmacenID;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}

                    listaAlmacenMovimiento.Add(almacenMovimiento);
                    listaAlmacenMovimiento.Add(almacenMovimientoEntrada);
                }

                foreach (var almacenMovimientoInfo in listaAlmacenMovimiento)
                {
                    almacenMovimientoInfo.UsuarioCreacionID = entradaMateriaPrima.UsuarioId;
                    AlmacenMovimientoInfo almacenMovimientoGenerado =
                        almacenBl.GuardarAlmacenMovimiento(almacenMovimientoInfo);
                    listaAlmacenMovimientoGenerado.Add(almacenMovimientoGenerado);
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
            return listaAlmacenMovimientoGenerado;
        }

        /// <summary>
        /// Obtiene el precio origen del contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal decimal ObtenerPrecioOrigen(ContratoInfo contrato)
        {
            decimal resultado = 0;
            try
            {
                var almacenBl = new AlmacenBL();
                var almaceInventarioLoteBl = new AlmacenInventarioLoteBL();

                if (contrato.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaNormal)
                {
                    resultado = contrato.Precio;
                    return resultado;
                }

                List<AlmacenMovimientoDetalle> listaMovimientos = almacenBl.ObtenerAlmacenMovimientoPorContrato(contrato);

                foreach (var almacenMovimientoDetalle in listaMovimientos)
                {
                    var almacenMovimientoBl = new AlmacenMovimientoBL();
                    var almacenMovimientoConsulta = almacenMovimientoBl.ObtenerPorId(almacenMovimientoDetalle.AlmacenMovimientoID);
                    if (almacenMovimientoConsulta != null)
                    {
                        if (almacenMovimientoConsulta.TipoMovimientoID ==
                            (int)TipoMovimiento.EntradaBodegaTerceros || almacenMovimientoConsulta.TipoMovimientoID == (int)TipoMovimiento.EntradaAlmacen)
                        {
                            var almacenInventarioLoteInfo =
                                almaceInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                    almacenMovimientoDetalle.AlmacenInventarioLoteId);

                            if (almacenInventarioLoteInfo != null)
                            {
                                resultado = almacenInventarioLoteInfo.PrecioPromedio;
                                break;
                            }

                        }
                    }
                }

                if (resultado == 0)
                {
                    resultado = contrato.Precio;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

        /// <summary>
        /// Almacena el detalle de los movimientos
        /// </summary>
        /// <param name="entradaMateriaPrima"></param>
        /// <param name="listaMovimientos"></param>
        /// <param name="listaMovimientosContrato"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> GuardarMovimientoDetalle(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima, List<AlmacenMovimientoInfo> listaMovimientos, List<AlmacenMovimientoInfo> listaMovimientosContrato)
        {
            var listaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle>();
            try
            {
                if (listaMovimientos != null)
                {
                    //var almacenBl = new AlmacenBL();
                    var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();

                    foreach (var almacenMovimientoInfo in listaMovimientos)
                    {
                        var almacenMovimientoDetalle = new AlmacenMovimientoDetalle
                        {
                            AlmacenMovimientoID = almacenMovimientoInfo.AlmacenMovimientoID
                        };

                        if (almacenMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.EntradaPorCompra ||
                            almacenMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.EntradaAlmacen)
                        {
                            almacenMovimientoDetalle.AlmacenInventarioLoteId =
                                entradaMateriaPrima.EntradaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId;

                            var entradaProductoBl = new EntradaProductoBL();
                            if (listaMovimientos.Any(registro => registro.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso))
                            {
                                entradaMateriaPrima.EntradaProducto.AlmacenMovimiento = new AlmacenMovimientoInfo
                                                                                            {
                                                                                                AlmacenMovimientoID = listaMovimientos.FirstOrDefault(registro => registro.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso).AlmacenMovimientoID
                                                                                            };
                            }
                            entradaProductoBl.ActualizarAlmacenMovimiento(entradaMateriaPrima.EntradaProducto, almacenMovimientoInfo);
                        }
                        else if (almacenMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                        {
                            almacenMovimientoDetalle.ContratoId = entradaMateriaPrima.Contrato.ContratoId;

                            AlmacenMovimientoInfo almacenMovimientoConsulta =
                                listaMovimientosContrato.FirstOrDefault(
                                    mov => mov.TipoMovimientoID == TipoMovimiento.EntradaBodegaTerceros.GetHashCode()
                                           || mov.TipoMovimientoID == TipoMovimiento.EntradaAlmacen.GetHashCode());

                            if (almacenMovimientoConsulta != null)
                            {
                                AlmacenMovimientoDetalle detalle =
                                    almacenMovimientoConsulta.ListaAlmacenMovimientoDetalle.FirstOrDefault();
                                if (detalle != null)
                                {
                                    almacenMovimientoDetalle.AlmacenInventarioLoteId = detalle.AlmacenInventarioLoteId;
                                }
                            }

                            //List<AlmacenMovimientoDetalle> listaMovimientosAlmacen = almacenBl.ObtenerAlmacenMovimientoPorContrato(entradaMateriaPrima.Contrato);
                            //if (listaMovimientosAlmacen != null)
                            //{
                            //    foreach (var almacenMovimientoDetalleInfo in listaMovimientosAlmacen)
                            //    {
                            //        var almacenMovimientoConsulta = new AlmacenMovimientoInfo
                            //        {
                            //            AlmacenMovimientoID = almacenMovimientoDetalleInfo.AlmacenMovimientoID,
                            //            AlmacenID = almacenMovimientoInfo.AlmacenID
                            //        };
                            //        almacenMovimientoConsulta =
                            //            almacenBl.ObtenerAlmacenMovimiento(almacenMovimientoConsulta);
                            //        if (almacenMovimientoConsulta != null)
                            //        {
                            //            if (almacenMovimientoConsulta.TipoMovimientoID == (int)TipoMovimiento.EntradaBodegaTerceros || 
                            //                  almacenMovimientoConsulta.TipoMovimientoID == (int)TipoMovimiento.EntradaAlmacen)
                            //            {
                            //                almacenMovimientoDetalle.AlmacenInventarioLoteId =
                            //                    almacenMovimientoDetalleInfo.AlmacenInventarioLoteId;
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}
                        }

                        almacenMovimientoDetalle.Piezas = entradaMateriaPrima.EntradaProducto.Piezas;
                        almacenMovimientoDetalle.ProductoID = entradaMateriaPrima.Producto.ProductoId;

                        /*if ((entradaMateriaPrima.Contrato.TipoContrato != null && 
                            entradaMateriaPrima.Contrato.TipoContrato.TipoContratoId ==
                                                          (int) TipoContratoEnum.BodegaNormal) ||
                         -    entradaMateriaPrima.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes) { 

                            if(entradaMateriaPrima.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString())
                                almacenMovimientoDetalle.Precio = entradaMateriaPrima.Contrato.Precio;
                            else
                            {
                                if (entradaMateriaPrima.Contrato.Parcial == CompraParcialEnum.Parcial)
                                {
                                    if (entradaMateriaPrima.Contrato.ListaContratoParcial != null)
                                    {
                                        var listaContratoParcial =
                                            entradaMateriaPrima.Contrato.ListaContratoParcial.Where(
                                                registro => registro.Seleccionado).ToList();
                                        if (listaContratoParcial.Count > 0)
                                        {
                                            decimal precio =
                                                listaContratoParcial.Sum(
                                                    registro => registro.Importe * registro.Cantidad)/
                                                listaContratoParcial.Sum(
                                                    registro => registro.Cantidad);
                                            almacenMovimientoDetalle.Precio = (precio*
                                                                               entradaMateriaPrima.EntradaProducto
                                                                                   .PesoBonificacion)/
                                                                              (entradaMateriaPrima.EntradaProducto
                                                                                  .PesoBruto -
                                                                               entradaMateriaPrima.EntradaProducto
                                                                                   .PesoTara);
                                        }
                                    }
                                }
                                else
                                {
                                    almacenMovimientoDetalle.Precio = (entradaMateriaPrima.Contrato.Precio *
                                                                           entradaMateriaPrima.EntradaProducto
                                                                               .PesoBonificacion) /
                                                                          (entradaMateriaPrima.EntradaProducto.PesoBruto -
                                                                           entradaMateriaPrima.EntradaProducto.PesoTara);
                                }
                            }
                        }else
                        {
                            almacenMovimientoDetalle.Precio = entradaMateriaPrima.Contrato.Precio;
                                //ObtenerPrecioOrigen(entradaMateriaPrima.Contrato);
                        }*/


                        //Si el contrato es de peso Destino o es una entrada
                        if (entradaMateriaPrima.Producto.SubfamiliaId == (int)SubFamiliasEnum.MicroIngredientes)
                        {
                            // Se cambia cuando son premezclas para que tome el peso origen
                            almacenMovimientoDetalle.Cantidad = entradaMateriaPrima.EntradaProducto.PesoOrigen;

                            almacenMovimientoDetalle.Precio = entradaMateriaPrima.Contrato.Precio;

                            almacenMovimientoDetalle.Importe = almacenMovimientoDetalle.Precio *
                                                               almacenMovimientoDetalle.Cantidad;
                        }
                        if (entradaMateriaPrima.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString())
                        {
                            almacenMovimientoDetalle.Cantidad = (decimal)(entradaMateriaPrima.EntradaProducto.PesoBruto -
                                                                           entradaMateriaPrima.EntradaProducto.PesoTara - entradaMateriaPrima.EntradaProducto.PesoDescuento);

                            almacenMovimientoDetalle.Precio = entradaMateriaPrima.Contrato.Precio;

                            almacenMovimientoDetalle.Importe = almacenMovimientoDetalle.Precio *
                                                               almacenMovimientoDetalle.Cantidad;
                        }
                        //Si el contrato es peso origen
                        else if (entradaMateriaPrima.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                        {
                            if (almacenMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                            {
                                almacenMovimientoDetalle.Precio = ObtenerPrecioOrigen(entradaMateriaPrima.Contrato);
                                almacenMovimientoDetalle.Cantidad =
                                    entradaMateriaPrima.EntradaProducto.PesoBonificacion;
                                almacenMovimientoDetalle.Importe = almacenMovimientoDetalle.Precio *
                                                           entradaMateriaPrima.EntradaProducto.PesoBonificacion;
                            }
                            else
                            {
                                almacenMovimientoDetalle.Precio = entradaMateriaPrima.Contrato.Precio;
                                almacenMovimientoDetalle.Cantidad = (decimal)(entradaMateriaPrima.EntradaProducto.PesoBruto -
                                                                               entradaMateriaPrima.EntradaProducto.PesoTara - entradaMateriaPrima.EntradaProducto.PesoDescuento);
                                almacenMovimientoDetalle.Importe = almacenMovimientoDetalle.Precio *
                                                           entradaMateriaPrima.EntradaProducto.PesoBonificacion;
                                almacenMovimientoDetalle.Precio = almacenMovimientoDetalle.Importe /
                                                                  almacenMovimientoDetalle.Cantidad;
                            }
                        }


                        almacenMovimientoDetalle.UsuarioCreacionID = entradaMateriaPrima.UsuarioId;

                        if(entradaMateriaPrima.aplicaRestriccionDescuento)
                        {
                            string pesodescuento = "0.0";
                            if (entradaMateriaPrima.EntradaProducto.PesoDescuento != null)
                                pesodescuento = entradaMateriaPrima.EntradaProducto.PesoDescuento.ToString();

                            decimal dpesodescuento = decimal.Parse(pesodescuento);

                            almacenMovimientoDetalle.Cantidad = entradaMateriaPrima.EntradaProducto.PesoBruto - entradaMateriaPrima.EntradaProducto.PesoTara;
                            almacenMovimientoDetalle.Importe = entradaMateriaPrima.Contrato.Precio * (entradaMateriaPrima.EntradaProducto.PesoOrigen - dpesodescuento);
                            almacenMovimientoDetalle.Precio = almacenMovimientoDetalle.Importe / almacenMovimientoDetalle.Cantidad;
                            
                        }

                        almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalle);

                        //Se disminuira el inventario
                        if (almacenMovimientoInfo.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                        {
                            almacenMovimientoDetalle.Cantidad = almacenMovimientoDetalle.Cantidad * -1;
                            almacenMovimientoDetalle.Importe = almacenMovimientoDetalle.Importe * -1;
                        }
                        listaAlmacenMovimientoDetalle.Add(almacenMovimientoDetalle);
                    }
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

            return listaAlmacenMovimientoDetalle;
        }

        /// <summary>
        /// Guarda los costos de los movimientos
        /// </summary>
        /// <param name="entradaMateriaPrima"></param>
        /// <param name="listaMovimientos"></param>
        /// <returns></returns>
        internal bool GuardarCosto(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima, List<AlmacenMovimientoInfo> listaMovimientos)
        {
            bool regreso = true;
            try
            {
                var cuentaSAPBL = new CuentaSAPBL();
                IList<CuentaSAPInfo> cuentasSAP = cuentaSAPBL.ObtenerTodos(EstatusEnum.Activo);
                var almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                if (listaMovimientos != null)
                {
                    foreach (var almacenMovimientoInfo in listaMovimientos)
                    {
                        if (almacenMovimientoInfo.TipoMovimientoID != (int)TipoMovimiento.ProductoSalidaTraspaso)
                        {

                            foreach (var costoMateriaPrima in entradaMateriaPrima.ListaCostoEntradaMateriaPrima)
                            {
                                AlmacenMovimientoCostoInfo almacenMovimientoCosto = new AlmacenMovimientoCostoInfo();
                                almacenMovimientoCosto.AlmacenMovimientoId = almacenMovimientoInfo.AlmacenMovimientoID;

                                if (costoMateriaPrima.TieneCuenta)
                                {
                                    CuentaSAPInfo cuenta =
                                        cuentasSAP.FirstOrDefault(
                                            sap => sap.CuentaSAP.Trim().Equals(costoMateriaPrima.CuentaSap.Trim()));
                                    if (cuenta != null)
                                    {
                                        almacenMovimientoCosto.CuentaSAPID = cuenta.CuentaSAPID;
                                    }
                                }
                                else
                                {
                                    almacenMovimientoCosto.ProveedorId = costoMateriaPrima.Provedor.ProveedorID;
                                }
                                almacenMovimientoCosto.Iva = costoMateriaPrima.Iva;
                                almacenMovimientoCosto.Retencion = costoMateriaPrima.Retencion;
                                almacenMovimientoCosto.CostoId = costoMateriaPrima.Costos.CostoID;
                                almacenMovimientoCosto.Importe = costoMateriaPrima.Importe;
                                almacenMovimientoCosto.UsuarioCreacionId = entradaMateriaPrima.UsuarioId;
                                almacenMovimientoCostoBl.Crear(almacenMovimientoCosto);
                            }
                        }
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                regreso = false;
                throw;
            }
            catch (Exception ex)
            {
                regreso = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return regreso;
        }


        /// <summary>
        /// Guarda el inventario del producto
        /// </summary>
        /// <param name="entradaMateriaPrima"></param>
        /// <param name="listaMovimientos"></param>
        /// <param name="listaAlmacenMovimientoDetalle"></param>
        /// <returns></returns>
        private bool GuardarInventario(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima, List<AlmacenMovimientoInfo> listaMovimientos, List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle)
        {
            try
            {
                var almacenInventarioBl = new AlmacenInventarioBL();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                decimal importe = 0;
                if (listaAlmacenMovimientoDetalle != null)
                {
                    foreach (var almacenMovimientoInfo in listaAlmacenMovimientoDetalle)
                    {
                        AlmacenMovimientoInfo almacenMovimiento;
                        if (almacenMovimientoInfo.Cantidad > 0)
                        {
                            almacenMovimiento =
                                listaMovimientos.FirstOrDefault(
                                    registro =>
                                        registro.TipoMovimientoID != (int)TipoMovimiento.ProductoSalidaTraspaso);
                            foreach (var costoMateriaPrima in entradaMateriaPrima.ListaCostoEntradaMateriaPrima)
                            {
                                importe = importe + costoMateriaPrima.Importe;
                            }
                        }
                        else
                        {
                            almacenMovimiento = listaMovimientos.FirstOrDefault(
                                registro =>
                                    registro.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso);
                        }

                        AlmacenInventarioLoteInfo almacenInventarioLote = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(almacenMovimientoInfo.AlmacenInventarioLoteId);
                        if (almacenInventarioLote != null && almacenMovimiento != null)
                        {
  
                            //Actualiza almacenInventarioLote
                            if (almacenMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                            {
                                almacenInventarioLote.Cantidad = almacenInventarioLote.Cantidad + almacenMovimientoInfo.Cantidad;
                                almacenInventarioLote.Importe = almacenInventarioLote.Importe + almacenMovimientoInfo.Importe;
                            }
                            else
                            {
                                almacenInventarioLote.Cantidad = almacenInventarioLote.Cantidad + almacenMovimientoInfo.Cantidad;
                                almacenInventarioLote.Importe = importe + almacenInventarioLote.Importe + almacenMovimientoInfo.Importe;
                                almacenInventarioLote.PrecioPromedio = almacenInventarioLote.Importe / almacenInventarioLote.Cantidad;
                                almacenInventarioLote.Piezas = almacenInventarioLote.Piezas + almacenMovimientoInfo.Piezas;
                            }
                            almacenInventarioLote.UsuarioModificacionId = entradaMateriaPrima.UsuarioId;
                            //Se valida si es premezcla y se agregan los costos de los productos agregados
                            if (entradaMateriaPrima.EntradaProducto.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes)
                            {
                                almacenInventarioLote.Importe = almacenInventarioLote.Importe + entradaMateriaPrima.CostoProductosPremezcla;
                                almacenInventarioLote.PrecioPromedio = almacenInventarioLote.Importe / almacenInventarioLote.Cantidad;
                            }
 
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLote);
                            //

                            var almacenInventarioInfo =
                                almacenInventarioBl.ObtenerAlmacenInventarioPorId(almacenInventarioLote.AlmacenInventario.AlmacenInventarioID);
                            List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioIDLigero(almacenInventarioInfo);

                            if (listaAlmacenInventarioLote != null)
                            {
                                decimal cantidadInventario = listaAlmacenInventarioLote.Sum(lote => lote.Cantidad);
                                decimal importeInventario = listaAlmacenInventarioLote.Sum(lote => lote.Importe);
                               
                                almacenInventarioInfo.Cantidad = cantidadInventario;
                                almacenInventarioInfo.Importe = importeInventario;
                                almacenInventarioInfo.PrecioPromedio = almacenInventarioInfo.Importe / almacenInventarioInfo.Cantidad;
                            }
                            almacenInventarioInfo.UsuarioModificacionID = entradaMateriaPrima.UsuarioId;
                            //if(entradaMateriaPrima.aplicaRestriccionDescuento)
                            //{
                            //    almacenInventarioInfo.Cantidad = entradaMateriaPrima.EntradaProducto.PesoBruto - entradaMateriaPrima.EntradaProducto.PesoTara;
                            //    almacenInventarioInfo.Importe = entradaMateriaPrima.EntradaProducto.PesoOrigen * entradaMateriaPrima.EntradaProducto.Contrato.Precio;
                            //}
                            almacenInventarioBl.Actualizar(almacenInventarioInfo);
                        }
                    }
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

            return true;
        }

        /// <summary>
        /// Obtiene la merma permitida por entrada de producto
        /// </summary>
        /// <param name="EntradaProducto"></param>
        /// <returns></returns>
        internal CostoEntradaMateriaPrimaInfo ObtenerMermaPermitida(EntradaProductoInfo EntradaProducto)
        {
            try
            {
                Logger.Info();
                var entradaMateriaPrimaDal = new EntradaMateriaPrimaDAL();
                CostoEntradaMateriaPrimaInfo resultados = entradaMateriaPrimaDal.ObtenerMermaPermitida(EntradaProducto);

                return resultados;
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
