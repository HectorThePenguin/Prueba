using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Servicios.PL;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    internal class ProduccionFormulaBL
    {
        /// <summary>
        /// Guarda la produccion de una formula, ademas graba el detalle
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        internal ProduccionFormulaInfo GuardarProduccionFormula(ProduccionFormulaInfo produccionFormula)
        {
            ProduccionFormulaInfo produccionFormulaGuardada;
            try
            {
                var listaAlmacenInventarioLotePolizas = new List<AlmacenInventarioLoteInfo>();
                Logger.Info();
                var produccionFormulaDal = new ProduccionFormulaDAL();
                var productoPl = new ProductoPL();
                AlmacenInfo almacen = null;

                using (var transaction = new TransactionScope())
                {
                    produccionFormulaGuardada = produccionFormula;

                    if (produccionFormulaGuardada != null && produccionFormula.ProduccionFormulaDetalle != null)
                    {
                        foreach (var produccionFormulaDetalleInfo in produccionFormula.ProduccionFormulaDetalle)
                        {
                            produccionFormulaDetalleInfo.Producto =
                                productoPl.ObtenerPorID(produccionFormulaDetalleInfo.Producto);
                            produccionFormulaDetalleInfo.ProduccionFormulaId =
                                produccionFormulaGuardada.ProduccionFormulaId;
                            produccionFormulaDetalleInfo.UsuarioCreacionId = produccionFormulaGuardada.UsuarioCreacionId;
                        }

                        produccionFormulaGuardada.ProduccionFormulaDetalle =
                            produccionFormula.ProduccionFormulaDetalle;

                        var formulaBl = new FormulaBL();
                        produccionFormulaGuardada.Formula =
                            formulaBl.ObtenerPorID(produccionFormulaGuardada.Formula.FormulaId);

                        var almacenBl = new AlmacenBL();
                        List<AlmacenInfo> listaAlmacenes =
                            almacenBl.ObtenerAlmacenPorOrganizacion(produccionFormula.Organizacion.OrganizacionID)
                                .ToList();

                        if (listaAlmacenes.Count > 0)
                        {
                            almacen = listaAlmacenes.FirstOrDefault(
                                registro =>
                                registro.TipoAlmacen.TipoAlmacenID == (int)TipoAlmacenEnum.PlantaDeAlimentos);

                            if (almacen != null)
                            {
                                if (!DisminuirInventario(produccionFormula, almacen,
                                                   listaAlmacenInventarioLotePolizas))
                                {
                                    //No existe inventario suficiente
                                    produccionFormulaGuardada.ProduccionFormulaId = -1;
                                }

                                GuardarMovimientoFormula(produccionFormula, almacen);

                                var almacenInventarioBl = new AlmacenInventarioBL();
                                List<AlmacenInventarioInfo> almacenInventario =
                                    almacenInventarioBl.ObtienePorAlmacenId(almacen);


                                AlmacenInventarioInfo inventarioFormula = null;

                                if (almacenInventario != null)
                                {
                                    inventarioFormula = almacenInventario.FirstOrDefault(
                                        registro =>
                                        registro.ProductoID ==
                                        produccionFormulaGuardada.Formula.Producto.ProductoId);
                                }

                                if (inventarioFormula != null)
                                {
                                    inventarioFormula.Cantidad = inventarioFormula.Cantidad +
                                                                 produccionFormula.CantidadProducida;
                                    inventarioFormula.ProductoID = produccionFormulaGuardada.Formula.ProductoId;
                                    inventarioFormula.Importe = inventarioFormula.Importe +
                                                                produccionFormula.ImporteFormula;
                                    inventarioFormula.PrecioPromedio = inventarioFormula.Importe /
                                                                       inventarioFormula.Cantidad;
                                    inventarioFormula.UsuarioModificacionID = produccionFormula.UsuarioCreacionId;
                                    almacenInventarioBl.Actualizar(inventarioFormula);
                                    inventarioFormula.Almacen = almacen;

                                    listaAlmacenInventarioLotePolizas.Add(
                                        GenerarAlmacenIventarioLotePoliza(inventarioFormula,
                                                                          produccionFormulaGuardada));
                                }
                                else
                                {
                                    var inventarioFormulaNueva = new AlmacenInventarioInfo
                                                                     {
                                                                         AlmacenID = almacen.AlmacenID,
                                                                         ProductoID =
                                                                             produccionFormulaGuardada.Formula.Producto.
                                                                             ProductoId,
                                                                         Minimo = 0,
                                                                         Maximo = 0,
                                                                         Importe =
                                                                             produccionFormula.ImporteFormula,
                                                                         Cantidad =
                                                                             produccionFormulaGuardada.CantidadProducida
                                                                     };
                                    inventarioFormulaNueva.PrecioPromedio = inventarioFormulaNueva.Importe /
                                                                            inventarioFormulaNueva.Cantidad;
                                    inventarioFormulaNueva.UsuarioCreacionID = produccionFormula.UsuarioCreacionId;
                                    almacenInventarioBl.Crear(inventarioFormulaNueva);
                                    inventarioFormulaNueva.Almacen = almacen;
                                    listaAlmacenInventarioLotePolizas.Add(
                                        GenerarAlmacenIventarioLotePoliza(inventarioFormulaNueva,
                                                                          produccionFormulaGuardada));
                                }
                            }
                            else
                            {
                                //No existe almacen donde modificar inventario
                                produccionFormulaGuardada.ProduccionFormulaId = -2;
                            }
                        }
                        else
                        {
                            //No existen almacenes
                            produccionFormulaGuardada.ProduccionFormulaId = -3;
                        }
                    }
                    else
                    {
                        //Ocurrio un error al guardar
                        produccionFormulaGuardada = new ProduccionFormulaInfo { ProduccionFormulaId = -4 };
                    }
                    if (produccionFormulaGuardada.ProduccionFormulaId >= 0)
                    {
                        var produccion = produccionFormulaDal.GuardarProduccionFormula(produccionFormula);
                        if (produccion != null)
                        {
                            if (produccionFormula.ProduccionFormulaDetalle != null)
                            {
                                produccionFormula.ProduccionFormulaDetalle.ForEach(
                                    pro => pro.ProduccionFormulaId = produccion.ProduccionFormulaId);
                            }

                            //-------------------------------------------------------------------
                            ////Camino para insertar datos en la tabla ProduccionFormulaBatch
                            var produccionFormulaBatchBL = new ProduccionFormulaBatchBL();
                            produccionFormulaBatchBL.GuardarProduccionFormulaBatch(produccionFormula);
                            //-------------------------------------------------------------------

                            var produccionFormulaDetalleBl = new ProduccionFormulaDetalleBL();
                            produccionFormulaDetalleBl.GuardarProduccionFormulaDetalle(
                                produccionFormula.ProduccionFormulaDetalle);

                            produccionFormulaGuardada.ProduccionFormulaId = produccion.ProduccionFormulaId;
                            produccionFormulaGuardada.FechaProduccion = produccion.FechaProduccion;
                            produccionFormulaGuardada.FolioFormula = produccion.FolioFormula;
                            produccionFormulaGuardada.Almacen = almacen;

                            GenerarPolizaProduccionFormula(produccion);

                            transaction.Complete();
                        }
                    }
                }
                return produccionFormulaGuardada;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                produccionFormulaGuardada = new ProduccionFormulaInfo { ProduccionFormulaId = -5, MensajePolizas = ex.Message };
                return produccionFormulaGuardada;
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        private void GenerarPolizaProduccionFormula(ProduccionFormulaInfo produccionFormula)
        {
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            ProduccionFormulaInfo produccion = ObtenerPorIDCompleto(produccionFormula.ProduccionFormulaId);

            if (produccion != null)
            {
                var almacenDAL = new AlmacenDAL();
                IList<AlmacenInfo> almacenes = almacenDAL.ObtenerTodos();
                AlmacenInfo almacenPlantaAlimento =
                    almacenes.FirstOrDefault(
                        org => org.Organizacion.OrganizacionID == produccionFormula.Organizacion.OrganizacionID
                               && org.TipoAlmacen.TipoAlmacenID == TipoAlmacenEnum.PlantaDeAlimentos.GetHashCode());

                AlmacenMovimientoInfo almacenMovimiento =
                    almacenMovimientoBL.ObtenerPorIDCompleto(produccion.AlmacenMovimientoSalidaID);
                var produccionFormulaDetalles = new List<ProduccionFormulaDetalleInfo>();
                if (almacenMovimiento != null)
                {
                    foreach (var detalle in produccion.ProduccionFormulaDetalle)
                    {
                        var movimiento = almacenMovimiento.ListaAlmacenMovimientoDetalle.FirstOrDefault(
                            alm => alm.Producto.ProductoId == detalle.Producto.ProductoId
                                   && alm.AlmacenInventarioLoteId == detalle.AlmacenInventarioLoteID);

                        if (movimiento == null)
                        {
                            continue;
                        }
                        detalle.CantidadProducto = movimiento.Cantidad;
                        detalle.PrecioPromedio = movimiento.Precio;
                        produccionFormulaDetalles.Add(detalle);
                    }
                }

                if (almacenPlantaAlimento == null)
                {
                    return;
                }
                produccion.ProduccionFormulaDetalle = produccionFormulaDetalles;
                produccion.Almacen = almacenPlantaAlimento;

                PolizaAbstract poliza =
                FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ProduccionAlimento);
                IList<PolizaInfo> listaPolizas = poliza.GeneraPoliza(produccion);
                
                if (listaPolizas != null && listaPolizas.Any())
                {
                    var polizaDAL = new PolizaDAL();
                    listaPolizas.ToList().ForEach(datos =>
                    {
                        datos.OrganizacionID = produccionFormula.Organizacion.OrganizacionID;
                        datos.UsuarioCreacionID = produccionFormula.UsuarioCreacionId;
                        datos.Activo = EstatusEnum.Activo;
                        datos.ArchivoEnviadoServidor = 1;
                    });
                    polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.ProduccionAlimento);
                }
            }
        }

        /// <summary>
        /// Guarda los movimientos de la formula
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <param name="almacen"></param>
        private void GuardarMovimientoFormula(ProduccionFormulaInfo produccionFormula, AlmacenInfo almacen)
        {
            try
            {
                var formulaBl = new FormulaBL();
                var almacenBl = new AlmacenBL();
                var almacenMovimiento = new AlmacenMovimientoInfo();
                almacenMovimiento.AlmacenID = almacen.AlmacenID;
                almacenMovimiento.TipoMovimientoID = (int)TipoMovimiento.EntradaProduccion;
                almacenMovimiento.Status = (int)Estatus.AplicadoInv;
                almacenMovimiento.UsuarioCreacionID = produccionFormula.UsuarioCreacionId;
                almacenMovimiento.FechaMovimiento = produccionFormula.FechaProduccion;
                almacenMovimiento = almacenBl.GuardarAlmacenMovimientoConFecha(almacenMovimiento);


                var almacenMovimientoDetalle = new AlmacenMovimientoDetalle();
                almacenMovimientoDetalle.AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;
                produccionFormula.Formula = formulaBl.ObtenerPorID(produccionFormula.Formula.FormulaId);
                almacenMovimientoDetalle.ProductoID = produccionFormula.Formula.Producto.ProductoId;
                almacenMovimientoDetalle.Importe = produccionFormula.ImporteFormula; //ObtenerImporte(produccionFormula, almacen);
                almacenMovimientoDetalle.Cantidad = produccionFormula.CantidadProducida;
                almacenMovimientoDetalle.Precio = almacenMovimientoDetalle.Importe / almacenMovimientoDetalle.Cantidad;
                almacenMovimientoDetalle.UsuarioCreacionID = produccionFormula.UsuarioCreacionId;

                almacenBl.GuardarAlmacenMovimientoDetalleProducto(almacenMovimientoDetalle);

                produccionFormula.AlmacenMovimientoEntradaID = almacenMovimiento.AlmacenMovimientoID;
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        ///// <summary>
        ///// Obtiene el importe para la formula producida
        ///// </summary>
        ///// <param name="produccionFormula"></param>
        ///// <param name="almacen"></param>
        ///// <returns></returns>
        //private decimal ObtenerImporte(ProduccionFormulaInfo produccionFormula, AlmacenInfo almacen)
        //{
        //    decimal retorno = 0;
        //    try
        //    {
        //        var almacenInventarioBl = new AlmacenInventarioBL();
        //        List<AlmacenInventarioInfo> almacenInventarioLista = almacenInventarioBl.ObtienePorAlmacenId(almacen);
        //        foreach (var produccionFormulaDetalleInfo in produccionFormula.ProduccionFormulaDetalle)
        //        {
        //            AlmacenInventarioInfo almacenInventario =
        //                almacenInventarioLista.FirstOrDefault(
        //                    registro => registro.ProductoID == produccionFormulaDetalleInfo.Producto.ProductoId);

        //            if (almacenInventario != null)
        //            {
        //                retorno = retorno +
        //                          (almacenInventario.PrecioPromedio * produccionFormulaDetalleInfo.CantidadProducto);
        //            }
        //        }
        //    }
        //    catch (ExcepcionDesconocida)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }

        //    return retorno;
        //}

        /// <summary>
        /// Genera los movimientos del inventario para los ingredientes y ademas regresa el importe para la formula
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <param name="almacen"></param>
        /// <param name="listaAlmacenInventarioLotePolizas"></param>
        /// <returns></returns>
        private bool DisminuirInventario(ProduccionFormulaInfo produccionFormula, AlmacenInfo almacen, List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLotePolizas)
        {
            bool retorno = true;
            try
            {
                var almacenBl = new AlmacenBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                List<AlmacenInventarioInfo> almacenInventarioLista = almacenInventarioBl.ObtienePorAlmacenId(almacen);

                var almacenMovimiento = new AlmacenMovimientoInfo
                {
                    AlmacenID = almacen.AlmacenID,
                    TipoMovimientoID = (int)TipoMovimiento.SalidaProduccion,
                    Status = (int)Estatus.AplicadoInv,
                    FechaMovimiento = produccionFormula.FechaProduccion,
                    UsuarioCreacionID = produccionFormula.UsuarioCreacionId
                };
                almacenMovimiento = almacenBl.GuardarAlmacenMovimientoConFecha(almacenMovimiento);
                almacenMovimiento.ListaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle>();
                foreach (var produccionFormulaDetalleInfo in produccionFormula.ProduccionFormulaDetalle)
                {
                    AlmacenInventarioInfo almacenInventario =
                        almacenInventarioLista.FirstOrDefault(
                            registro => registro.ProductoID == produccionFormulaDetalleInfo.Producto.ProductoId);

                    if (almacenInventario != null)
                    {
                        var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();

                        if(almacenInventario.Cantidad <= 0)
                        {
                            produccionFormulaDetalleInfo.CantidadProducto = -1;
                            return false;
                        }

                        List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote =
                            almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventario);

                        if(listaAlmacenInventarioLote == null || !listaAlmacenInventarioLote.Any())
                        {
                            produccionFormulaDetalleInfo.CantidadProducto = -1;
                            return false;
                        }

                        //AlmacenInventarioLoteInfo almacenInventarioLote =
                        //    listaAlmacenInventarioLotePolizas.OrderBy(alm => alm.Lote).FirstOrDefault();
                        //AlmacenInventarioLoteInfo almacenInventarioLote =
                        //    almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                        //        produccionFormulaDetalleInfo.AlmacenInventarioLoteID);




                        almacenInventario.Cantidad = almacenInventario.Cantidad -
                                                        produccionFormulaDetalleInfo.CantidadProducto;
                        almacenInventario.Importe = almacenInventario.Cantidad * almacenInventario.PrecioPromedio;
                        almacenInventario.UsuarioModificacionID = produccionFormula.UsuarioCreacionId;

                        if (retorno)
                            almacenInventarioBl.Actualizar(almacenInventario);

                        //var almacenMovimientoDetalle = new AlmacenMovimientoDetalle();
                        //almacenMovimientoDetalle.AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;
                        //almacenMovimientoDetalle.ProductoID = produccionFormulaDetalleInfo.Producto.ProductoId;

                        //if (almacenInventarioLote != null)
                        //{
                        //    almacenMovimientoDetalle.Precio = almacenInventarioLote.PrecioPromedio;
                        //    almacenMovimientoDetalle.AlmacenInventarioLoteId =
                        //        almacenInventarioLote.AlmacenInventarioLoteId;

                        //    //else
                        //    //{
                        //    //produccionFormulaDetalleInfo.CantidadProducto = -1;
                        //    //retorno = false;
                        //    //}
                        //}
                        //else
                        //{
                        //    produccionFormulaDetalleInfo.CantidadProducto = -1;
                        //    retorno = false;
                        //}

                        if (retorno)
                        {
                            //decimal cantidadSalida = produccionFormulaDetalleInfo.CantidadProducto;
                            //decimal cantidadSobrante = produccionFormulaDetalleInfo.CantidadProducto -
                            //                           almacenInventarioLote.Cantidad;
                            //if (cantidadSalida > almacenInventarioLote.Cantidad)
                            //{
                            if (listaAlmacenInventarioLote != null && listaAlmacenInventarioLote.Any())
                            {
                                bool incompleto = true;
                                decimal cantidadAlmacen = 0;
                                decimal cantidadSobrante = 0;
                                decimal cantidadProducida = produccionFormulaDetalleInfo.CantidadProducto;
                                foreach (var almacenInventarioLoteNuevo in listaAlmacenInventarioLote.Where(alm => alm.Activo ==EstatusEnum.Activo))
                                {
                                    if (cantidadProducida > almacenInventarioLoteNuevo.Cantidad)
                                    {
                                        cantidadSobrante = cantidadProducida - almacenInventarioLoteNuevo.Cantidad;
                                        cantidadAlmacen = almacenInventarioLoteNuevo.Cantidad;
                                    }
                                    else
                                    {
                                        cantidadAlmacen = cantidadProducida;
                                        cantidadSobrante = 0;
                                    }
                                    if (!incompleto)
                                    {
                                        break;
                                    }
                                    if (cantidadAlmacen == 0)
                                    {
                                        continue;
                                    }
                                    var almacenMovimientoDetalleIncompleto = new AlmacenMovimientoDetalle
                                        {
                                            AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                            ProductoID = produccionFormulaDetalleInfo.Producto.ProductoId,
                                            Precio = almacenInventarioLoteNuevo.PrecioPromedio,
                                            AlmacenInventarioLoteId =
                                                almacenInventarioLoteNuevo.AlmacenInventarioLoteId,
                                            Cantidad = cantidadAlmacen,
                                            Importe = almacenInventarioLoteNuevo.PrecioPromedio * cantidadAlmacen,
                                            UsuarioCreacionID = produccionFormula.UsuarioCreacionId
                                        };

                                  
                                    cantidadProducida = cantidadProducida - cantidadAlmacen;
                                    //almacenMovimiento.ListaAlmacenMovimientoDetalle.Add(almacenMovimientoDetalleIncompleto);

                                    almacenMovimientoDetalleIncompleto.Cantidad = cantidadAlmacen;
                                    almacenMovimientoDetalleIncompleto.Importe = almacenMovimientoDetalleIncompleto.Precio *
                                                                        almacenMovimientoDetalleIncompleto.Cantidad;
                                    almacenMovimientoDetalleIncompleto.UsuarioCreacionID = produccionFormula.UsuarioCreacionId;
                                    almacenMovimiento.ListaAlmacenMovimientoDetalle.Add(almacenMovimientoDetalleIncompleto);
                                    //almacenBl.GuardarAlmacenMovimientoDetalleProducto(almacenMovimientoDetalle);


                                    //AlmacenInventarioLoteInfo almacenInventarioLoteNuevo =
                                    //    almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                    //        almacenInventarioLoteInfo.AlmacenInventarioLoteId);

                                    int piezasPromedio = 0;
                                    if (almacenInventarioLoteNuevo.Piezas > 0)
                                    {
                                        piezasPromedio = (int)almacenInventarioLoteNuevo.Cantidad /
                                                             almacenInventarioLoteNuevo.Piezas;
                                    }

                                    almacenInventarioLoteNuevo.Cantidad = almacenInventarioLoteNuevo.Cantidad - cantidadAlmacen;

                                    almacenInventarioLoteNuevo.Importe = almacenInventarioLoteNuevo.Cantidad * almacenInventarioLoteNuevo.PrecioPromedio;

                                    if (piezasPromedio > 0)
                                    {
                                        int piezas = (int)almacenInventarioLoteNuevo.Cantidad / piezasPromedio;
                                        almacenInventarioLoteNuevo.Piezas = almacenInventarioLoteNuevo.Piezas - piezas;
                                    }

                                    //almacenInventarioLoteNuevo.Piezas = almacenInventarioLoteInfo.Piezas -
                                    //                                    almacenMovimientoDetalle.Piezas;
                                    almacenInventarioLoteNuevo.UsuarioModificacionId =
                                        produccionFormula.UsuarioCreacionId;
                                    almacenInventarioLoteBl.Actualizar(almacenInventarioLoteNuevo);

                                    ////Kilogramos teoricos en 0 se desactiva el lote
                                    //if (almacenInventarioLoteNuevo.Cantidad == 0)
                                    //{
                                    //    //Desactivar lote
                                    //    almacenInventarioLoteBl.DesactivarLote(almacenInventarioLoteNuevo);
                                    //}

                                    if (cantidadSobrante < almacenInventarioLoteNuevo.Cantidad || cantidadSobrante == 0)
                                    {
                                        incompleto = false;
                                    }

                                    almacenInventarioLoteNuevo.AlmacenInventario.Almacen = almacen;

                                    almacenInventarioLoteNuevo.Cantidad = cantidadAlmacen;

                                    almacenInventarioLoteNuevo.Importe = almacenInventarioLoteNuevo.Cantidad *
                                                                            almacenInventarioLoteNuevo.PrecioPromedio;
                                    listaAlmacenInventarioLotePolizas.Add(almacenInventarioLoteNuevo);

                                   
                                }
                                if (cantidadSobrante > 0)
                                {
                                    produccionFormulaDetalleInfo.CantidadProducto = -1;
                                    return false;
                                    //retorno = false;
                                }
                            }
                            else
                            {
                                produccionFormulaDetalleInfo.CantidadProducto = -1;
                                return false;
                                //retorno = false;
                            }
                        }
                        else
                        {
                            produccionFormulaDetalleInfo.CantidadProducto = -1;
                            return false;
                            //retorno = false;
                        }
                    }
                    else
                    {
                        produccionFormulaDetalleInfo.CantidadProducto = -1;
                        return false;
                        //retorno = false;
                    }
                }
                produccionFormula.AlmacenMovimientoSalidaID = almacenMovimiento.AlmacenMovimientoID;
                if (almacenMovimiento.ListaAlmacenMovimientoDetalle != null && almacenMovimiento.ListaAlmacenMovimientoDetalle.Any())
                {
                    produccionFormula.ImporteFormula =
                        almacenMovimiento.ListaAlmacenMovimientoDetalle.Sum(det => det.Importe);
                    var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
                    almacenMovimientoDetalleBL.GuardarAlmacenMovimientoDetalle(almacenMovimiento.ListaAlmacenMovimientoDetalle,
                                                              almacenMovimiento.AlmacenMovimientoID);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                retorno = false;
                Logger.Error(ex);
            }

            return retorno;
        }

        private AlmacenInventarioLoteInfo GenerarAlmacenIventarioLotePoliza(AlmacenInventarioInfo almacenInventario, ProduccionFormulaInfo produccionFormula)
        {
            var almacenInventarioLote = new AlmacenInventarioLoteInfo
                {
                    AlmacenInventario = almacenInventario,
                    Cantidad = almacenInventario.Cantidad,
                    PrecioPromedio = almacenInventario.PrecioPromedio,
                    Importe = almacenInventario.Importe,
                    FechaProduccionFormula = produccionFormula.FechaProduccion
                };
            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProduccionFormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionFormulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var produccionFormulaDAL = new ProduccionFormulaDAL();
                ResultadoInfo<ProduccionFormulaInfo> result = produccionFormulaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una entidad ProduccionFormula por su Id
        /// </summary>
        /// <param name="produccionFormulaID">Obtiene una entidad ProduccionFormula por su Id</param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorID(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                var produccionFormulaDAL = new ProduccionFormulaDAL();
                ProduccionFormulaInfo result = produccionFormulaDAL.ObtenerPorID(produccionFormulaID);
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
        /// Obtiene una entidad ProduccionFormula por su Id
        /// </summary>
        /// <param name="produccionFormula">Obtiene una entidad ProduccionFormula por su Id</param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorFolioMovimiento(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                var produccionFormulaDAL = new ProduccionFormulaDAL();
                ProduccionFormulaInfo result = produccionFormulaDAL.ObtenerPorFolioMovimiento(produccionFormula);
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
        /// Obtiene una entidad ProduccionFormula por su Id
        /// </summary>
        /// <param name="produccionFormulaID">Obtiene una entidad ProduccionFormula por su Id</param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorIDCompleto(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                var produccionFormulaDAL = new ProduccionFormulaDAL();
                ProduccionFormulaInfo result = produccionFormulaDAL.ObtenerPorIDCompleto(produccionFormulaID);
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
        /// Obtiene una lista de produccion de formula
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<ProduccionFormulaInfo> ObtenerProduccionFormulaConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var produccionFormulaDAL = new ProduccionFormulaDAL();
                List<ProduccionFormulaInfo> result =
                    produccionFormulaDAL.ObtenerProduccionFormulaConciliacion(organizacionID, fechaInicio, fechaFinal);
                AsignarCamposFaltantes(result);
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

        private void AsignarCamposFaltantes(List<ProduccionFormulaInfo> produccionesFormula)
        {
            var almacenMovimientoBL = new AlmacenMovimientoBL();
            foreach (var produccion in produccionesFormula)
            {
                using (var almacenDAL = new Integracion.DAL.ORM.AlmacenDAL())
                {                    
                    if (produccion != null)
                    {
                        AlmacenInfo almacenPlantaAlimento =
                            almacenDAL.ObtenerAlmacenOrganizacionTipo(produccion.Organizacion.OrganizacionID,
                                                                      TipoAlmacenEnum.PlantaDeAlimentos);

                        AlmacenMovimientoInfo almacenMovimiento =
                            almacenMovimientoBL.ObtenerPorIDCompleto(produccion.AlmacenMovimientoSalidaID);

                        var produccionFormulaDetalles = new List<ProduccionFormulaDetalleInfo>();
                        if (almacenMovimiento != null)
                        {
                            foreach (var detalle in produccion.ProduccionFormulaDetalle)
                            {
                                var movimiento = almacenMovimiento.ListaAlmacenMovimientoDetalle.FirstOrDefault(
                                    alm => alm.Producto.ProductoId == detalle.Producto.ProductoId
                                           && alm.AlmacenInventarioLoteId == detalle.AlmacenInventarioLoteID);

                                if (movimiento == null)
                                {
                                    continue;
                                }
                                detalle.CantidadProducto = movimiento.Cantidad;
                                detalle.PrecioPromedio = movimiento.Precio;
                                produccionFormulaDetalles.Add(detalle);
                            }
                        }

                        if (almacenPlantaAlimento == null)
                        {
                            continue;
                        }
                        produccion.ProduccionFormulaDetalle = produccionFormulaDetalles;
                        produccion.Almacen = almacenPlantaAlimento;
                    }
                }
            }
        }

        //agregado por Jose Angel Rodriguez Rodriguez 
        internal ProduccionFormulaAutomaticaModel GuardarProduccionFormulaLista(List<ProduccionFormulaInfo> produccionFormula,DateTime fecha)
        {
            var resultado =new ProduccionFormulaAutomaticaModel();
            resultado.CodigoMensajeRetorno = 0;
            resultado.Mensaje = "OK";
            try
            {
                var listaAlmacenInventarioLotePolizas = new List<AlmacenInventarioLoteInfo>();
                Logger.Info();
                var produccionFormulaDal = new ProduccionFormulaDAL();
                var productoPl = new ProductoPL();
                

                using (var transaction = new TransactionScope())
                {
                    foreach (var principal in produccionFormula)
                    {
                        principal.FechaProduccion = fecha;
                        var produccionFormulaGuardada = new ProduccionFormulaInfo();
                        produccionFormulaGuardada.FechaProduccion = fecha;
                        AlmacenInfo almacen;
                        if (principal.ProduccionFormulaDetalle != null)
                        {
                            foreach (var produccionFormulaDetalleInfo in principal.ProduccionFormulaDetalle)
                            {
                                produccionFormulaDetalleInfo.Producto = productoPl.ObtenerPorID(produccionFormulaDetalleInfo.Producto);
                                produccionFormulaDetalleInfo.ProduccionFormulaId = principal.ProduccionFormulaId;
                                produccionFormulaDetalleInfo.UsuarioCreacionId = principal.UsuarioCreacionId;
                            }

                            produccionFormulaGuardada.ProduccionFormulaDetalle = principal.ProduccionFormulaDetalle;

                            foreach (var produccionFormulaDetalleInfo in principal.ProduccionFormulaBatch)
                            {
                                produccionFormulaDetalleInfo.ProduccionFormulaID = principal.ProduccionFormulaId;
                                produccionFormulaDetalleInfo.UsuarioCreacionID = principal.UsuarioCreacionId;
                            }

                            produccionFormulaGuardada.ProduccionFormulaBatch = principal.ProduccionFormulaBatch;
                            produccionFormulaGuardada.CantidadProducida = principal.CantidadProducida;
                            produccionFormulaGuardada.UsuarioCreacionId = principal.UsuarioCreacionId;

                            var formulaBl = new FormulaBL();
                            produccionFormulaGuardada.Formula = formulaBl.ObtenerPorID(principal.Formula.FormulaId);
                            
                            var organizacionBl = new OrganizacionBL();
                            produccionFormulaGuardada.Organizacion = organizacionBl.ObtenerPorID(principal.Organizacion.OrganizacionID);

                            var almacenBl = new AlmacenBL();
                            List<AlmacenInfo> listaAlmacenes = almacenBl.ObtenerAlmacenPorOrganizacion(principal.Organizacion.OrganizacionID).ToList();

                            if (listaAlmacenes.Count > 0)
                            {
                                almacen = listaAlmacenes.FirstOrDefault(registro => registro.TipoAlmacen.TipoAlmacenID == (int)TipoAlmacenEnum.PlantaDeAlimentos);

                                if (almacen != null)
                                {
                                    resultado = DisminuirInventarioLista(produccionFormulaGuardada, almacen, listaAlmacenInventarioLotePolizas);
                                    if (resultado.Mensaje != "OK")
                                    {
                                        return resultado;
                                    }

                                    GuardarMovimientoFormula(produccionFormulaGuardada, almacen);

                                    var almacenInventarioBl = new AlmacenInventarioBL();
                                    List<AlmacenInventarioInfo> almacenInventario = almacenInventarioBl.ObtienePorAlmacenId(almacen);

                                    AlmacenInventarioInfo inventarioFormula = null;

                                    if (almacenInventario != null)
                                    {
                                        inventarioFormula = almacenInventario.FirstOrDefault(
                                            registro => registro.ProductoID == produccionFormulaGuardada.Formula.Producto.ProductoId);
                                    }

                                    if (inventarioFormula != null)
                                    {
                                        inventarioFormula.Cantidad = inventarioFormula.Cantidad + principal.CantidadProducida;
                                        inventarioFormula.ProductoID = produccionFormulaGuardada.Formula.ProductoId;
                                        inventarioFormula.Importe = inventarioFormula.Importe + produccionFormulaGuardada.ImporteFormula;
                                        inventarioFormula.PrecioPromedio = inventarioFormula.Importe / inventarioFormula.Cantidad;
                                        inventarioFormula.UsuarioModificacionID = produccionFormulaGuardada.UsuarioCreacionId;
                                        almacenInventarioBl.Actualizar(inventarioFormula);
                                        inventarioFormula.Almacen = almacen;

                                        listaAlmacenInventarioLotePolizas.Add(
                                            GenerarAlmacenIventarioLotePoliza(inventarioFormula,produccionFormulaGuardada));
                                    }
                                    else
                                    {
                                        var inventarioFormulaNueva = new AlmacenInventarioInfo
                                        {
                                            AlmacenID = almacen.AlmacenID,
                                            ProductoID =produccionFormulaGuardada.Formula.Producto.ProductoId,
                                            Minimo = 0,
                                            Maximo = 0,
                                            Importe = produccionFormulaGuardada.ImporteFormula,
                                            Cantidad = produccionFormulaGuardada.CantidadProducida
                                        };
                                        inventarioFormulaNueva.PrecioPromedio = inventarioFormulaNueva.Importe / inventarioFormulaNueva.Cantidad;
                                        inventarioFormulaNueva.UsuarioCreacionID = principal.UsuarioCreacionId;
                                        almacenInventarioBl.Crear(inventarioFormulaNueva);
                                        inventarioFormulaNueva.Almacen = almacen;
                                        listaAlmacenInventarioLotePolizas.Add(GenerarAlmacenIventarioLotePoliza(inventarioFormulaNueva,produccionFormulaGuardada));
                                    }
                                }
                                else
                                {
                                    //No existe almacen donde modificar inventario
                                    produccionFormulaGuardada.ProduccionFormulaId = -2;
                                    resultado.CodigoMensajeRetorno = 4; //Mensaje de que no Existe el Almacen de Planta de Alimentos
                                    resultado.Mensaje = string.Empty;
                                    return resultado; //= "falso";
                                }
                            }
                            else
                            {
                                //No existen almacenes
                                resultado.CodigoMensajeRetorno = 4; //Mensaje de que no Existe el Almacen de Planta de Alimentos
                                resultado.Mensaje = string.Empty;
                                return resultado; //= "falso";
                            }
                        }
                        else
                        {
                            //Ocurrio un error al guardar
                            //produccionFormulaGuardada = new ProduccionFormulaInfo() { ProduccionFormulaId = -4 };
                            resultado.CodigoMensajeRetorno = 5; //Mensaje de error
                            resultado.Mensaje = string.Empty;
                            return resultado; //= "falso";
                        }
                        if (produccionFormulaGuardada.ProduccionFormulaId >= 0)
                        {
                            produccionFormulaGuardada.FechaProduccion = fecha;
                            var produccion = produccionFormulaDal.GuardarProduccionFormula(produccionFormulaGuardada);
                            if (produccion != null)
                            {
                                if (produccionFormulaGuardada.ProduccionFormulaDetalle != null)
                                {
                                    produccionFormulaGuardada.ProduccionFormulaDetalle.ForEach(pro => pro.ProduccionFormulaId = produccion.ProduccionFormulaId);
                                }

                                //-------------------------------------------------------------------
                                ////Camino para insertar datos en la tabla ProduccionFormulaBatch
                                var produccionFormulaBatchBL = new ProduccionFormulaBatchBL();
                                produccionFormulaBatchBL.GuardarProduccionFormulaBatchLista(produccionFormulaGuardada, produccion.ProduccionFormulaId);
                                //-------------------------------------------------------------------

                                var produccionFormulaDetalleBl = new ProduccionFormulaDetalleBL();
                                produccionFormulaDetalleBl.GuardarProduccionFormulaDetalle(produccionFormulaGuardada.ProduccionFormulaDetalle);

                                produccionFormulaGuardada.ProduccionFormulaId = produccion.ProduccionFormulaId;
                                produccionFormulaGuardada.FechaProduccion = produccion.FechaProduccion;
                                produccionFormulaGuardada.FolioFormula = produccion.FolioFormula;
                                produccionFormulaGuardada.Almacen = almacen;

                                GenerarPolizaProduccionFormula(produccion);
                            }
                        }
                    }
                    transaction.Complete();
                }
                return resultado;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                resultado.CodigoMensajeRetorno = 6; //Mensaje de la Poliza
                resultado.Mensaje = ex.Message;
                return resultado;
                //return produccionFormulaGuardada;
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }

        //recupera el resumen de produccion de formulas 
        internal List<ProduccionFormulaInfo> ResumenProduccionFormulaLista(List<ProduccionFormulaInfo> produccionFormula)
        {
            try
            {
                Logger.Info();
                var produccionFormulaDal = new ProduccionFormulaDAL();
                produccionFormula = produccionFormulaDal.ObtenerProduccionFormulaResumen(produccionFormula);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                //produccionFormulaGuardada = new ProduccionFormulaInfo() { ProduccionFormulaId = -5, MensajePolizas = ex.Message };
                //return produccionFormulaGuardada;
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return produccionFormula;
        }





        //agregado por Jose Angel Rodriguez Rodriguez 
        //Insertar Movimiento de Salida en la Tabla de Almacen de Inventario
        //Disminuye el Inventario
        private ProduccionFormulaAutomaticaModel DisminuirInventarioLista(ProduccionFormulaInfo produccionFormula, AlmacenInfo almacen, List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLotePolizas)
        {

            var retorno2 = new ProduccionFormulaAutomaticaModel();
            
            bool retorno = true;
            try
            {
                var almacenBl = new AlmacenBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                List<AlmacenInventarioInfo> almacenInventarioLista = almacenInventarioBl.ObtienePorAlmacenId(almacen);

                var almacenMovimiento = new AlmacenMovimientoInfo
                {
                    AlmacenID = almacen.AlmacenID,
                    TipoMovimientoID = (int)TipoMovimiento.SalidaProduccion,
                    Status = (int)Estatus.AplicadoInv,
                    UsuarioCreacionID = produccionFormula.UsuarioCreacionId
                };
                almacenMovimiento = almacenBl.GuardarAlmacenMovimiento(almacenMovimiento);
                almacenMovimiento.ListaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle>();

                foreach (var produccionFormulaDetalleInfo in produccionFormula.ProduccionFormulaDetalle)
                {
                    AlmacenInventarioInfo almacenInventario =
                        almacenInventarioLista.FirstOrDefault(registro => registro.ProductoID == produccionFormulaDetalleInfo.Producto.ProductoId);

                    if (almacenInventario != null)
                    {
                        var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();

                        if (almacenInventario.Cantidad <= 0)
                        {
                            //retorno = false;
                            retorno2.CodigoMensajeRetorno = 1; //Mensaje que indica si no hay cantidad del producto;
                            retorno2.Mensaje = produccionFormulaDetalleInfo.Producto.ProductoDescripcion;
                            return retorno2; //= "CantidadDeAlmacenEn0";
                        }

                        List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote = almacenInventarioLoteBl.ObtenerPorAlmacenInventarioID(almacenInventario);

                        //var almacenInventarioBL = new AlmacenInventarioBL();

                        //AlmacenInventarioInfo almacenInventariopropiedad = new AlmacenInventarioInfo();

                        if (listaAlmacenInventarioLote == null || !listaAlmacenInventarioLote.Any())
                        {
                            //if (almacenInventario == null)
                            //{
                                produccionFormulaDetalleInfo.CantidadProducto = -1;
                                //retorno = false;
                                retorno2.CodigoMensajeRetorno = 2; //Mensaje que indica que el producto no tiene Lotes
                                retorno2.Mensaje = produccionFormulaDetalleInfo.Producto.ProductoDescripcion;
                                return retorno2; //= "NoExisteAlmacenInventario";
                            //}
                        }

                        almacenInventario.Cantidad = almacenInventario.Cantidad - produccionFormulaDetalleInfo.CantidadProducto;
                        almacenInventario.Importe = almacenInventario.Cantidad * almacenInventario.PrecioPromedio;
                        almacenInventario.UsuarioModificacionID = produccionFormula.UsuarioCreacionId;

                        if (retorno)
                            almacenInventarioBl.Actualizar(almacenInventario);

                        if (retorno)
                        {
                            if (listaAlmacenInventarioLote.Any())
                            {
                                bool incompleto = true;
                                decimal cantidadAlmacen;
                                decimal cantidadSobrante = 0;
                                decimal cantidadProducida = produccionFormulaDetalleInfo.CantidadProducto;
                                foreach (var almacenInventarioLoteNuevo in listaAlmacenInventarioLote.Where(alm => alm.Activo == EstatusEnum.Activo && alm.Cantidad > 0))
                                {
                                    if (cantidadProducida > almacenInventarioLoteNuevo.Cantidad)
                                    {
                                        cantidadSobrante = cantidadProducida - almacenInventarioLoteNuevo.Cantidad;
                                        cantidadAlmacen = almacenInventarioLoteNuevo.Cantidad;
                                    }
                                    else
                                    {
                                        cantidadAlmacen = cantidadProducida;
                                        cantidadSobrante = 0;
                                    }
                                    if (!incompleto)
                                    {
                                        break;
                                    }
                                    if (cantidadAlmacen == 0)
                                    {
                                        continue;
                                    }
                                    var almacenMovimientoDetalleIncompleto = new AlmacenMovimientoDetalle
                                    {
                                        AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                        ProductoID = produccionFormulaDetalleInfo.Producto.ProductoId,
                                        Precio = almacenInventarioLoteNuevo.PrecioPromedio,
                                        AlmacenInventarioLoteId = almacenInventarioLoteNuevo.AlmacenInventarioLoteId,
                                        Cantidad = cantidadAlmacen,
                                        Importe = almacenInventarioLoteNuevo.PrecioPromedio * cantidadAlmacen,
                                        UsuarioCreacionID = produccionFormula.UsuarioCreacionId
                                    };

                                    cantidadProducida = cantidadProducida - cantidadAlmacen;

                                    almacenMovimientoDetalleIncompleto.Cantidad = cantidadAlmacen;
                                    almacenMovimientoDetalleIncompleto.Importe = almacenMovimientoDetalleIncompleto.Precio *
                                                                        almacenMovimientoDetalleIncompleto.Cantidad;
                                    almacenMovimientoDetalleIncompleto.UsuarioCreacionID = produccionFormula.UsuarioCreacionId;
                                    almacenMovimiento.ListaAlmacenMovimientoDetalle.Add(almacenMovimientoDetalleIncompleto);

                                    int piezasPromedio = 0;
                                    if (almacenInventarioLoteNuevo.Piezas > 0)
                                    {
                                        piezasPromedio = (int)almacenInventarioLoteNuevo.Cantidad / almacenInventarioLoteNuevo.Piezas;
                                    }

                                    almacenInventarioLoteNuevo.Cantidad = almacenInventarioLoteNuevo.Cantidad - cantidadAlmacen;

                                    almacenInventarioLoteNuevo.Importe = almacenInventarioLoteNuevo.Cantidad * almacenInventarioLoteNuevo.PrecioPromedio;

                                    if (piezasPromedio > 0)
                                    {
                                        int piezas = (int)almacenInventarioLoteNuevo.Cantidad / piezasPromedio;
                                        almacenInventarioLoteNuevo.Piezas = almacenInventarioLoteNuevo.Piezas - piezas;
                                    }

                                    almacenInventarioLoteNuevo.UsuarioModificacionId = produccionFormula.UsuarioCreacionId;
                                    almacenInventarioLoteBl.Actualizar(almacenInventarioLoteNuevo);

                                    if (cantidadSobrante < almacenInventarioLoteNuevo.Cantidad || cantidadSobrante == 0)
                                    {
                                        incompleto = false;
                                    }

                                    almacenInventarioLoteNuevo.AlmacenInventario.Almacen = almacen;

                                    almacenInventarioLoteNuevo.Cantidad = cantidadAlmacen;

                                    almacenInventarioLoteNuevo.Importe = almacenInventarioLoteNuevo.Cantidad * almacenInventarioLoteNuevo.PrecioPromedio;
                                    listaAlmacenInventarioLotePolizas.Add(almacenInventarioLoteNuevo);

                                }
                                if (cantidadSobrante > 0)
                                {
                                    produccionFormulaDetalleInfo.CantidadProducto = -1;
                                    //retorno = false;
                                    retorno2.CodigoMensajeRetorno = 3; //Indica que queda cantidad pendiente por aplica y no alcanza el inventario
                                    retorno2.Mensaje = string.Format("Formula {0}, Producto {1}, Cantidad {2}",produccionFormula.Formula.Descripcion, produccionFormulaDetalleInfo.Producto.ProductoDescripcion, cantidadSobrante);
                                    return retorno2; //= "CantidadSobrante";
                                }
                            }
                            //else
                            //{
                            //    //disminuira el inventario de la tabla AlmacenInventario

                            //    //produccionFormulaDetalleInfo.CantidadProducto = -1;
                            //    //return false;
                            //}
                        }
                        else
                        {
                            produccionFormulaDetalleInfo.CantidadProducto = -1;
                            retorno2.CodigoMensajeRetorno = 1; //Mensaje que indica si no hay cantidad del producto;
                            retorno2.Mensaje = produccionFormulaDetalleInfo.Producto.ProductoDescripcion;
                            return retorno2; //= "CantidadDeAlmacenEn0";
                        }
                    }
                    else
                    {
                        //busca por Almacen Inventario Lote
                        produccionFormulaDetalleInfo.CantidadProducto = -1;
                        retorno2.CodigoMensajeRetorno = 1; //Mensaje que indica si no hay cantidad del producto;
                        retorno2.Mensaje = produccionFormulaDetalleInfo.Producto.ProductoDescripcion;
                        return retorno2; //= "CantidadDeAlmacenEn0";
                        //retorno = false;
                    }
                }
                produccionFormula.AlmacenMovimientoSalidaID = almacenMovimiento.AlmacenMovimientoID;
                if (almacenMovimiento.ListaAlmacenMovimientoDetalle != null && almacenMovimiento.ListaAlmacenMovimientoDetalle.Any())
                {
                    var movimientosNegativos =
                        almacenMovimiento.ListaAlmacenMovimientoDetalle.Where(det => det.Cantidad <= 0).ToList();

                    if(movimientosNegativos.Any())
                    {
                        retorno2.CodigoMensajeRetorno = 5; //Mensaje de error
                        retorno2.Mensaje = string.Empty;
                        return retorno2; //= "falso";
                    }

                    produccionFormula.ImporteFormula =
                        almacenMovimiento.ListaAlmacenMovimientoDetalle.Sum(det => det.Importe);
                    var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
                    almacenMovimientoDetalleBL.GuardarAlmacenMovimientoDetalle(almacenMovimiento.ListaAlmacenMovimientoDetalle,
                                                              almacenMovimiento.AlmacenMovimientoID);
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                //retorno = false;
                Logger.Error(ex);
            }
            retorno2.CodigoMensajeRetorno = 0;
            retorno2.Mensaje = "OK";
            return retorno2;
        }

    }
}
