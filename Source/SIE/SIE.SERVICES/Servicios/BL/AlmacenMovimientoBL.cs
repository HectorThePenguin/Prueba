using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    internal class AlmacenMovimientoBL
    {
        /// <summary>
        /// Crear un registro en almacen movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal long Crear(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                long result = almacenMovimientoDal.Crear(almacenMovimientoInfo);
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
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerPorId(long almacenMovimientoId)
        {
            AlmacenMovimientoInfo result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ObtenerPorId(almacenMovimientoId);
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
            return result;
        }

        /// <summary>
        /// Actualizar estatus del movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal void ActualizarEstatus(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                almacenMovimientoDal.ActualizarEstatus(almacenMovimientoInfo);
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
        /// Genera el Cierre de Dia de Inventario
        /// </summary>
        /// <param name="cierreDiaInventarioPA"></param>
        internal IList<ResultadoPolizaModel> GuardarCierreDiaInventarioPA(CierreDiaInventarioPAInfo cierreDiaInventarioPA)
        {
            try
            {
                var listaMovimientosFinal = new List<AlmacenMovimientoInfo>();
                var listaMovimientosDetalleFinal = new List<AlmacenMovimientoDetalle>();
                var listaOriginalDetalles = new List<CierreDiaInventarioPADetalleInfo>();
                var listaAlmacenInventario = new List<AlmacenInventarioInfo>();
                var listaAlmacenInventarioLote = new List<AlmacenInventarioLoteInfo>();

                GenerarAjustesInventario(listaAlmacenInventarioLote, listaAlmacenInventario, cierreDiaInventarioPA);

                listaOriginalDetalles.AddRange(cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle);
                var estatusDAL = new EstatusDAL();
                List<EstatusInfo> listaEstatusInventario =
                    estatusDAL.ObtenerEstatusTipoEstatus(TipoEstatus.Inventario.GetHashCode());

                //Toma los registros que no necesitan autorización para generar sus movimientos Fisicos
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle =
                    listaOriginalDetalles.Where(det => !det.RequiereAutorizacion).ToList();
                ArmarAlmacenMovimientoInventarioFisico(listaMovimientosFinal, cierreDiaInventarioPA, listaEstatusInventario);

                //Toma los registros a los que se le van a generar los Movimientos de Entrada de Almacen
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle =
                    listaOriginalDetalles.Where(
                        det => !det.RequiereAutorizacion && (det.InventarioFisico - det.InventarioTeorico) > 0).ToList();
                if (cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Any())
                {
                    ArmarAlmacenMovimientoEntradaSalidaAlmacen(listaMovimientosFinal, cierreDiaInventarioPA,
                                                               listaEstatusInventario, true);
                }

                //Toma los registros a los que se le van a generar los Movimientos de Salida de Almacen
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle =
                    listaOriginalDetalles.Where(
                        det => !det.RequiereAutorizacion && (det.InventarioFisico - det.InventarioTeorico) < 0).ToList();
                if (cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Any())
                {
                    ArmarAlmacenMovimientoEntradaSalidaAlmacen(listaMovimientosFinal, cierreDiaInventarioPA,
                                                               listaEstatusInventario, false);
                }

                //Toma los registros a los que se le van a generar los Fisicos como estatus de Pendientes
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle =
                    listaOriginalDetalles.Where(det => det.RequiereAutorizacion).ToList();
                if (cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Any())
                {
                    ArmarAlmacenMovimientoInventarioFisicoPendiente(listaMovimientosFinal, cierreDiaInventarioPA,
                                                                    listaEstatusInventario);
                }
                IList<ResultadoPolizaModel> pdfs = null;
                using (var transaction = new TransactionScope())
                {

                    var almacenMovimientoDAL = new AlmacenMovimientoDAL();
                    var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                    var almacenInventarioDAL = new AlmacenInventarioDAL();
                    var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();

                    foreach (var almacenMovimiento in listaMovimientosFinal)
                    {
                        long almacenMovimientoID = almacenMovimientoDAL.GuardarMovimientoCierreDiaPA(almacenMovimiento);
                        almacenMovimiento.ListaAlmacenMovimientoDetalle.ForEach(
                            det => det.AlmacenMovimientoID = almacenMovimientoID);
                        listaMovimientosDetalleFinal.AddRange(almacenMovimiento.ListaAlmacenMovimientoDetalle);
                    }
                    almacenMovimientoDetalleDAL.GuardarDetalleCierreDiaInventarioPA(listaMovimientosDetalleFinal);
                    if (listaAlmacenInventario.Any())
                    {
                        almacenInventarioDAL.AjustarAlmacenInventario(listaAlmacenInventario);
                    }
                    if (listaAlmacenInventarioLote.Any())
                    {
                        almacenInventarioLoteDAL.AjustarAlmacenInventarioLote(listaAlmacenInventarioLote);
                    }
                    ActualizarFolioAlmacen(cierreDiaInventarioPA.AlmacenID);

                    cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle = listaOriginalDetalles;
                    pdfs = GenerarPolizaCierreDiaPA(cierreDiaInventarioPA, listaEstatusInventario, true);
                    transaction.Complete();
                }
                return pdfs;
            }
            catch (ExcepcionServicio)
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

        internal void ActualizarFolioAlmacen(int almacenID)
        {
            var almacenDAL = new AlmacenDAL();
            var filtroCierreDiaAlmacen = new FiltroCierreDiaInventarioInfo
                {
                    AlmacenID = almacenID,
                    TipoMovimientoID = TipoMovimiento.DiferenciasDeInventario.GetHashCode()
                };
            almacenDAL.ActualizarFolioAlmacen(filtroCierreDiaAlmacen);
        }

        /// <summary>
        /// Genera los registros para guardar el ajuste del almacen, en la tabla AlmacenInventario o AlmacenInventarioLote
        /// </summary>
        private void GenerarAjustesInventario(List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote,
                                              List<AlmacenInventarioInfo> listaAlmacenInventario,
                                              CierreDiaInventarioPAInfo cierreDiaInventarioPA)
        {
            var productosConLote =
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Where(
                    prod => prod.ManejaLote && !prod.RequiereAutorizacion);
            var productosSinLote =
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Where(
                    prod => !prod.ManejaLote && !prod.RequiereAutorizacion);

            listaAlmacenInventarioLote.AddRange((from lote in productosConLote
                                                 let esEntrada = (lote.InventarioFisico - lote.InventarioTeorico > 0)
                                                 select new AlmacenInventarioLoteInfo
                                                     {
                                                         AlmacenInventarioLoteId = lote.AlmacenInventarioLoteID,
                                                         Cantidad = Math.Abs(lote.InventarioFisico - lote.InventarioTeorico),
                                                         Piezas = Math.Abs(lote.PiezasFisicas - lote.PiezasTeoricas),
                                                         UsuarioModificacionId = cierreDiaInventarioPA.UsuarioCreacionID,
                                                         ProductoId = lote.ProductoID,
                                                         EsEntrada = esEntrada
                                                     }).ToList());


            listaAlmacenInventario.AddRange((from producto in productosSinLote
                                             let esEntrada = (producto.InventarioFisico - producto.InventarioTeorico > 0)
                                             select new AlmacenInventarioInfo
                                                 {
                                                     AlmacenID = cierreDiaInventarioPA.AlmacenID,
                                                     ProductoID = producto.ProductoID,
                                                     Cantidad = Math.Abs(producto.InventarioFisico - producto.InventarioTeorico),
                                                     UsuarioModificacionID = cierreDiaInventarioPA.UsuarioCreacionID,
                                                     EsEntrada = esEntrada
                                                 }));

            foreach (var almacenInventarioLoteInfo in listaAlmacenInventarioLote)
            {
                AlmacenInventarioLoteInfo info = almacenInventarioLoteInfo;

                if (!listaAlmacenInventario.Where(registro => registro.ProductoID == info.ProductoId).ToList().Any())
                {
                    var almacenInventario = new AlmacenInventarioInfo
                        {
                            AlmacenID = cierreDiaInventarioPA.AlmacenID,
                            ProductoID = info.ProductoId,
                            Cantidad =
                                listaAlmacenInventarioLote.Where(registro => registro.ProductoId == info.ProductoId)
                                    .ToList()
                                    .Sum(registro => registro.Cantidad),
                            UsuarioModificacionID = cierreDiaInventarioPA.UsuarioCreacionID
                        };
                    listaAlmacenInventario.Add(almacenInventario);
                }
            }
        }

        /// <summary>
        /// Genera el registro del inventario Físico de los productos aplicados
        /// </summary>
        private void ArmarAlmacenMovimientoInventarioFisico(List<AlmacenMovimientoInfo> listaMovimientosFinal,
                                                            CierreDiaInventarioPAInfo cierreDiaInventarioPA,
                                                            IEnumerable<EstatusInfo> listaEstatusInventario)
        {
            EstatusInfo estatusAplicado =
                listaEstatusInventario.FirstOrDefault(
                    esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoAplicado.Trim()));
            if (estatusAplicado == null)
            {
                return;
            }
            var movimiento = new AlmacenMovimientoInfo
                {
                    AlmacenID = cierreDiaInventarioPA.AlmacenID,
                    TipoMovimientoID = TipoMovimiento.InventarioFisico.GetHashCode(),
                    FolioMovimiento = cierreDiaInventarioPA.FolioMovimiento,
                    Observaciones = cierreDiaInventarioPA.Observaciones,
                    //FechaMovimiento = DateTime.Now,
                    Status = estatusAplicado.EstatusId,
                    UsuarioCreacionID = cierreDiaInventarioPA.UsuarioCreacionID
                };

            var movimientoDetalles = (from detalle in cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle
                                      select new AlmacenMovimientoDetalle
                                          {
                                              AlmacenInventarioLoteId = detalle.AlmacenInventarioLoteID,
                                              ProductoID = detalle.ProductoID,
                                              Precio = detalle.CostoUnitario,
                                              Cantidad = detalle.InventarioFisico,
                                              Piezas = detalle.PiezasFisicas,
                                              Importe =
                                                  Math.Round(
                                                      Convert.ToDecimal(detalle.InventarioFisico) * detalle.CostoUnitario,
                                                      2),
                                              UsuarioCreacionID = cierreDiaInventarioPA.UsuarioCreacionID
                                          }).ToList();
            movimiento.ListaAlmacenMovimientoDetalle = movimientoDetalles;
            listaMovimientosFinal.Add(movimiento);
        }

        /// <summary>
        /// Genera los registros de los movimientos para entrada y/o salida del Almacen
        /// </summary>
        private void ArmarAlmacenMovimientoEntradaSalidaAlmacen(List<AlmacenMovimientoInfo> listaMovimientosFinal,
                                                                CierreDiaInventarioPAInfo cierreDiaInventarioPA,
                                                                IEnumerable<EstatusInfo> listaEstatusInventario,
                                                                bool esEntrada)
        {
            EstatusInfo estatusAutorizado =
                listaEstatusInventario.FirstOrDefault(
                    esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoAutorizado.Trim()));
            if (estatusAutorizado == null)
            {
                return;
            }
            var movimiento = new AlmacenMovimientoInfo
                {
                    AlmacenID = cierreDiaInventarioPA.AlmacenID,
                    TipoMovimientoID =
                        esEntrada
                            ? TipoMovimiento.EntradaPorAjuste.GetHashCode()
                            : TipoMovimiento.SalidaPorAjuste.GetHashCode(),
                    FolioMovimiento = cierreDiaInventarioPA.FolioMovimiento,
                    Observaciones = cierreDiaInventarioPA.Observaciones,
                    //FechaMovimiento = DateTime.Now,
                    Status = estatusAutorizado.EstatusId,
                    UsuarioCreacionID = cierreDiaInventarioPA.UsuarioCreacionID
                };

            var movimientoDetalles = (from detalle in cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle
                                      select new AlmacenMovimientoDetalle
                                          {
                                              AlmacenInventarioLoteId = detalle.AlmacenInventarioLoteID,
                                              ProductoID = detalle.ProductoID,
                                              Precio = detalle.CostoUnitario,
                                              Cantidad = Math.Abs(detalle.InventarioFisico - detalle.InventarioTeorico),
                                              Piezas = Math.Abs((detalle.PiezasFisicas - detalle.PiezasTeoricas)),
                                              Importe =
                                                  Math.Round(
                                                      Convert.ToDecimal(Math.Abs(detalle.InventarioFisico - detalle.InventarioTeorico)) *
                                                      detalle.CostoUnitario, 2),
                                              UsuarioCreacionID = cierreDiaInventarioPA.UsuarioCreacionID
                                          }).ToList();
            movimiento.ListaAlmacenMovimientoDetalle = movimientoDetalles;
            listaMovimientosFinal.Add(movimiento);
        }

        /// <summary>
        /// Genera los registro que quedarán pendientes de autorizar
        /// </summary>
        private void ArmarAlmacenMovimientoInventarioFisicoPendiente(List<AlmacenMovimientoInfo> listaMovimientosFinal,
                                                                     CierreDiaInventarioPAInfo cierreDiaInventarioPA,
                                                                     IEnumerable<EstatusInfo> listaEstatusInventario)
        {
            EstatusInfo estatusPendiente =
                listaEstatusInventario.FirstOrDefault(
                    esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoPendiente.Trim()));
            if (estatusPendiente == null)
            {
                return;
            }
            var movimiento = new AlmacenMovimientoInfo
                {
                    AlmacenID = cierreDiaInventarioPA.AlmacenID,
                    TipoMovimientoID = TipoMovimiento.InventarioFisico.GetHashCode(),
                    FolioMovimiento = cierreDiaInventarioPA.FolioMovimiento,
                    Observaciones = cierreDiaInventarioPA.Observaciones,
                    //FechaMovimiento = DateTime.Now,
                    Status = estatusPendiente.EstatusId,
                    UsuarioCreacionID = cierreDiaInventarioPA.UsuarioCreacionID
                };

            var movimientoDetalles = (from detalle in cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle
                                      select new AlmacenMovimientoDetalle
                                          {
                                              AlmacenInventarioLoteId = detalle.AlmacenInventarioLoteID,
                                              ProductoID = detalle.ProductoID,
                                              Precio = detalle.CostoUnitario,
                                              Cantidad = detalle.InventarioFisico,
                                              Piezas = detalle.PiezasFisicas,
                                              Importe =
                                                  Math.Round(
                                                      Convert.ToDecimal(detalle.InventarioFisico) * detalle.CostoUnitario,
                                                      2),
                                              UsuarioCreacionID = cierreDiaInventarioPA.UsuarioCreacionID
                                          }).ToList();
            movimiento.ListaAlmacenMovimientoDetalle = movimientoDetalles;
            listaMovimientosFinal.Add(movimiento);
        }

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal bool ValidarEjecucionCierreDia(int almacenID)
        {
            bool result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ValidarEjecucionCierreDia(almacenID);
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
            return result;
        }

        /// <summary>
        /// Obtiene los movimientos pendientes pro autorizar del cierre de dia de inventario de planta de alimentos
        /// </summary>
        /// <returns></returns>
        internal List<MovimientosAutorizarCierreDiaPAModel> ObtenerMovimientosPendientesAutorizar(
            FiltrosAutorizarCierreDiaInventarioPA filtrosAutorizarCierreDia)
        {
            List<MovimientosAutorizarCierreDiaPAModel> result;
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                var listaMermas = mermaSuperavitDAL.ObtenerPorAlmacenID(filtrosAutorizarCierreDia.AlmacenID);
                var almacenMovimientoDal = new AlmacenMovimientoDAL();

                filtrosAutorizarCierreDia.TipoMovimientoID = TipoMovimiento.InventarioFisico.GetHashCode();
                filtrosAutorizarCierreDia.EstatusMovimiento = Estatus.PendienteInv.GetHashCode();
                filtrosAutorizarCierreDia.FechaMovimiento = DateTime.Now;

                List<MovimientosAutorizarCierreDiaPAModel> listaMovimientosPendientes = almacenMovimientoDal.ObtenerMovimientosPendientesAutorizar(filtrosAutorizarCierreDia);
                if (listaMovimientosPendientes == null)
                {
                    return null;
                }
                foreach (var movimiento in listaMovimientosPendientes)
                {
                    if (movimiento.InventarioTeorico == 0)
                    {
                        continue;
                    }
                    movimiento.PorcentajeMermaSuperavit = Math.Round((Convert.ToDecimal(movimiento.InventarioTeorico - movimiento.InventarioFisico) /
                                                          movimiento.InventarioTeorico) * 100, 2);
                    movimiento.PorcentajeLote = movimiento.ManejaLote ? Math.Round(Convert.ToDecimal(movimiento.InventarioFisico) / Convert.ToDecimal(movimiento.InventarioTeorico) * 100, 2) : 0;
                    var merma = listaMermas.FirstOrDefault(mer => mer.Producto.ProductoId == movimiento.ProductoID);
                    if (merma == null)
                    {
                        continue;
                    }
                    movimiento.PorcentajePermitido = movimiento.PorcentajeMermaSuperavit > 0 ? merma.Merma : merma.Superavit;
                }
                result = listaMovimientosPendientes;
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
            return result;
        }

        /// <summary>
        /// Obtiene los movimientos de inventario para generar
        /// poliza de consumo de producto
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventario(int almacenID, int organizacionID)
        {
            List<ContenedorAlmacenMovimientoCierreDia> result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ObtenerMovimientosInventario(almacenID, organizacionID);
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
            return result;
        }

        /// <summary>
        /// Actualiza el estatus para indicar si ya se genero ó no poliza
        /// </summary>
        /// <param name="movimientos"></param>
        /// <returns></returns>
        internal void ActualizarGeneracionPoliza(List<ContenedorAlmacenMovimientoCierreDia> movimientos)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                almacenMovimientoDal.ActualizarGeneracionPoliza(movimientos);
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
        /// Genera el Cierre de Dia de Inventario
        /// </summary>
        /// <param name="cierreDiaInventarioPA"></param>
        internal void GuardarAutorizarCierreDiaInventarioPA(CierreDiaInventarioPAInfo cierreDiaInventarioPA)
        {
            var listaMovimientosFinal = new List<AlmacenMovimientoInfo>();
            var listaMovimientosDetalleFinal = new List<AlmacenMovimientoDetalle>();
            var listaAlmacenInventario = new List<AlmacenInventarioInfo>();
            var listaAlmacenInventarioLote = new List<AlmacenInventarioLoteInfo>();

            var listaOriginalDetalles = new List<CierreDiaInventarioPADetalleInfo>();

            listaOriginalDetalles.AddRange(cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle);

            GenerarAjustesInventario(listaAlmacenInventarioLote, listaAlmacenInventario, cierreDiaInventarioPA);

            var estatusDAL = new EstatusDAL();
            List<EstatusInfo> listaEstatusInventario =
                estatusDAL.ObtenerEstatusTipoEstatus(TipoEstatus.Inventario.GetHashCode());

            //Toma los registros a los que se le van a generar los Movimientos de Entrada de Almacen
            cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle =
                listaOriginalDetalles.Where(
                    det => !det.RequiereAutorizacion && (det.InventarioFisico - det.InventarioTeorico) > 0).ToList();
            if (cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Any())
            {
                ArmarAlmacenMovimientoEntradaSalidaAlmacen(listaMovimientosFinal, cierreDiaInventarioPA,
                                                           listaEstatusInventario, true);
            }

            //Toma los registros a los que se le van a generar los Movimientos de Salida de Almacen
            cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle =
                listaOriginalDetalles.Where(
                    det => !det.RequiereAutorizacion && (det.InventarioFisico - det.InventarioTeorico) < 0).ToList();
            if (cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Any())
            {
                ArmarAlmacenMovimientoEntradaSalidaAlmacen(listaMovimientosFinal, cierreDiaInventarioPA,
                                                           listaEstatusInventario, false);
            }


            EstatusInfo estatusAplicado =
                listaEstatusInventario.FirstOrDefault(
                    esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoAplicado.Trim()));

            EstatusInfo estatusPendiente =
                listaEstatusInventario.FirstOrDefault(
                    esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoPendiente.Trim()));

            EstatusInfo estatusCancelado =
                listaEstatusInventario.FirstOrDefault(
                    esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoCancelado.Trim()));
            if (estatusAplicado == null || estatusPendiente == null || estatusCancelado == null)
            {
                return;
            }

            var filtrosActualizar = new FiltroCambiarEstatusInfo
                {
                    AlmacenID = cierreDiaInventarioPA.AlmacenID,
                    FolioMovimiento = cierreDiaInventarioPA.FolioMovimiento,
                    EstatusAnterior = estatusPendiente.EstatusId,
                    EstatusNuevo = cierreDiaInventarioPA.EsCancelacion ? estatusCancelado.EstatusId : estatusAplicado.EstatusId,
                    UsuarioModificacionID = cierreDiaInventarioPA.UsuarioCreacionID
                };

            if (cierreDiaInventarioPA.EsCancelacion)
            {
                var almacenMovimientoDAL = new AlmacenMovimientoDAL();
                almacenMovimientoDAL.ActualizarEstatusAlmacenMovimiento(filtrosActualizar);
                return;
            }

            using (var transaction = new TransactionScope())
            {

                var almacenMovimientoDAL = new AlmacenMovimientoDAL();
                var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                var almacenInventarioDAL = new AlmacenInventarioDAL();
                var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();

                foreach (var almacenMovimiento in listaMovimientosFinal)
                {
                    long almacenMovimientoID = almacenMovimientoDAL.GuardarMovimientoCierreDiaPA(almacenMovimiento);
                    almacenMovimientoDAL.ActualizarEstatusAlmacenMovimiento(filtrosActualizar);
                    almacenMovimiento.ListaAlmacenMovimientoDetalle.ForEach(
                        det => det.AlmacenMovimientoID = almacenMovimientoID);
                    listaMovimientosDetalleFinal.AddRange(almacenMovimiento.ListaAlmacenMovimientoDetalle);
                }
                almacenMovimientoDetalleDAL.GuardarDetalleCierreDiaInventarioPA(listaMovimientosDetalleFinal);
                if (listaAlmacenInventario.Any())
                {
                    almacenInventarioDAL.AjustarAlmacenInventario(listaAlmacenInventario);
                }
                if (listaAlmacenInventarioLote.Any())
                {
                    almacenInventarioLoteDAL.AjustarAlmacenInventarioLote(listaAlmacenInventarioLote);
                }
                cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle = listaOriginalDetalles;
                GenerarPolizaCierreDiaPA(cierreDiaInventarioPA, listaEstatusInventario, false);
                transaction.Complete();
            }

        }

        /// <summary>
        /// Obtiene el Almacen Movimiento por su Detalle
        /// </summary>
        /// <param name="almancenMovimientosDetalle"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerMovimientoPorClaveDetalle(List<AlmacenMovimientoDetalle> almancenMovimientosDetalle)
        {
            AlmacenMovimientoInfo almacenMovimiento;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                almacenMovimiento = almacenMovimientoDal.ObtenerMovimientoPorClaveDetalle(almancenMovimientosDetalle);
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
            return almacenMovimiento;
        }

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerPorIDCompleto(long almacenMovimientoId)
        {
            AlmacenMovimientoInfo result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ObtenerPorIDCompleto(almacenMovimientoId);
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
            return result;
        }

        private IList<ResultadoPolizaModel> GenerarPolizaCierreDiaPA(CierreDiaInventarioPAInfo cierreDiaInventarioPA, List<EstatusInfo> listaEstatusInventario, bool imprimePoliza)
        {
            EstatusInfo estatusAplicado =
               listaEstatusInventario.FirstOrDefault(
                   esta => esta.DescripcionCorta.Trim().Equals(ConstantesBL.MovimientoAplicado.Trim()));
            if (estatusAplicado == null)
            {
                return null;
            }
            var filtros = new FiltroAlmacenMovimientoInfo
                {
                    AlmacenID = cierreDiaInventarioPA.AlmacenID,
                    OrganizacionID = cierreDiaInventarioPA.OrganizacionID,
                    TipoMovimientoID = TipoMovimiento.InventarioFisico.GetHashCode(),
                    EstatusID = estatusAplicado.EstatusId,
                    FolioMovimiento = cierreDiaInventarioPA.FolioMovimiento
                };

            var almacenMovimientoDAL = new AlmacenMovimientoDAL();
            List<ContenedorAlmacenMovimientoCierreDia> movimientos = almacenMovimientoDAL.ObtenerMovimientosInventarioFiltros(filtros);
            if (movimientos == null || !movimientos.Any())
            {
                return null;
            }
            var resultadosPolizaModel = new List<ResultadoPolizaModel>();
            var listaDatosPolizaEntrada = new List<PolizaEntradaSalidaPorAjusteModel>();
            var listaDatosPolizaSalida = new List<PolizaEntradaSalidaPorAjusteModel>();
            foreach (var detalles in cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Where(det => (det.InventarioFisico - det.InventarioTeorico > 0)))
            {
                ContenedorAlmacenMovimientoCierreDia almacenMovimientoDetalle;
                if (detalles.ManejaLote)
                {
                    almacenMovimientoDetalle =
                        movimientos.FirstOrDefault(
                            mov => mov.Producto.ProductoId == detalles.ProductoID &&
                                   mov.AlmacenMovimientoDetalle.AlmacenInventarioLoteId ==
                                   detalles.AlmacenInventarioLoteID);
                }
                else
                {
                    almacenMovimientoDetalle =
                        movimientos.FirstOrDefault(
                            mov => mov.Producto.ProductoId == detalles.ProductoID);
                }
                if (almacenMovimientoDetalle == null)
                {
                    continue;
                }
                int cantidadEntrada = Math.Abs(detalles.InventarioFisico - detalles.InventarioTeorico);
                var polizaDetalle = new PolizaEntradaSalidaPorAjusteModel
                    {
                        Importe = detalles.CostoUnitario * cantidadEntrada,
                        Cantidad = cantidadEntrada,
                        Precio = almacenMovimientoDetalle.AlmacenMovimientoDetalle.Precio,
                        AlmacenMovimientoDetalleID =
                            almacenMovimientoDetalle.AlmacenMovimientoDetalle.AlmacenMovimientoDetalleID,
                        ProductoID = detalles.ProductoID,
                        CantidadInventarioFisico = detalles.InventarioFisico,
                        CantidadInventarioTeorico = detalles.InventarioTeorico,
                        Observaciones = almacenMovimientoDetalle.AlmacenMovimiento.Observaciones,
                        Lote = detalles.Lote
                    };
                listaDatosPolizaEntrada.Add(polizaDetalle);
            }

            foreach (var detalles in cierreDiaInventarioPA.ListaCierreDiaInventarioPADetalle.Where(det => (det.InventarioFisico - det.InventarioTeorico < 0)))
            {
                ContenedorAlmacenMovimientoCierreDia almacenMovimientoDetalle;
                if (detalles.ManejaLote)
                {
                    almacenMovimientoDetalle =
                        movimientos.FirstOrDefault(
                            mov => mov.Producto.ProductoId == detalles.ProductoID &&
                                   mov.AlmacenMovimientoDetalle.AlmacenInventarioLoteId ==
                                   detalles.AlmacenInventarioLoteID);
                }
                else
                {
                    almacenMovimientoDetalle =
                        movimientos.FirstOrDefault(
                            mov => mov.Producto.ProductoId == detalles.ProductoID);
                }
                if (almacenMovimientoDetalle == null)
                {
                    continue;
                }
                int cantidadSalida = Math.Abs(detalles.InventarioFisico - detalles.InventarioTeorico);
                var polizaDetalle = new PolizaEntradaSalidaPorAjusteModel
                {
                    Importe = detalles.CostoUnitario * cantidadSalida,
                    Cantidad = cantidadSalida,
                    Precio = almacenMovimientoDetalle.AlmacenMovimientoDetalle.Precio,
                    AlmacenMovimientoDetalleID =
                        almacenMovimientoDetalle.AlmacenMovimientoDetalle.AlmacenMovimientoDetalleID,
                    ProductoID = detalles.ProductoID,
                    CantidadInventarioFisico = detalles.InventarioFisico,
                    CantidadInventarioTeorico = detalles.InventarioTeorico,
                    Observaciones = almacenMovimientoDetalle.AlmacenMovimiento.Observaciones,
                    Lote = detalles.Lote
                };
                listaDatosPolizaSalida.Add(polizaDetalle);
            }
            if (listaDatosPolizaSalida.Any())
            {
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaAjuste);

                IList<PolizaInfo> polizas = poliza.GeneraPoliza(listaDatosPolizaSalida);
                if (polizas != null)
                {
                    MemoryStream stream = null;
                    if (imprimePoliza)
                    {
                        stream = poliza.ImprimePoliza(listaDatosPolizaSalida, polizas);
                    }
                    var polizaBL = new PolizaBL();
                    polizas.ToList().ForEach(datos =>
                        {
                            datos.UsuarioCreacionID =
                                cierreDiaInventarioPA.UsuarioCreacionID;
                            datos.OrganizacionID = cierreDiaInventarioPA.OrganizacionID;
                            datos.ArchivoEnviadoServidor = 1;
                        });
                    polizaBL.GuardarServicioPI(polizas, TipoPoliza.SalidaAjuste);
                    var resultadoPolizaModel = new ResultadoPolizaModel
                                                   {
                                                       Polizas = polizas,
                                                       PDFs =
                                                           new Dictionary<TipoPoliza, MemoryStream>
                                                               {{TipoPoliza.SalidaAjuste, stream}}
                                                   };
                    resultadosPolizaModel.Add(resultadoPolizaModel);
                }
            }
            if (listaDatosPolizaEntrada.Any())
            {
                var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaAjuste);

                IList<PolizaInfo> polizas = poliza.GeneraPoliza(listaDatosPolizaEntrada);
                if (polizas != null)
                {
                    MemoryStream stream = null;
                    if (imprimePoliza)
                    {
                        stream = poliza.ImprimePoliza(listaDatosPolizaEntrada, polizas);
                    }
                    var polizaBL = new PolizaBL();
                    polizas.ToList().ForEach(datos =>
                    {
                        datos.UsuarioCreacionID =
                            cierreDiaInventarioPA.UsuarioCreacionID;
                        datos.OrganizacionID = cierreDiaInventarioPA.OrganizacionID;
                        datos.ArchivoEnviadoServidor = 1;
                    });
                    polizaBL.GuardarServicioPI(polizas, TipoPoliza.EntradaAjuste);
                    var resultadoPolizaModel = new ResultadoPolizaModel
                    {
                        Polizas = polizas,
                        PDFs =
                            new Dictionary<TipoPoliza, MemoryStream> { { TipoPoliza.EntradaAjuste, stream } }
                    };
                    resultadosPolizaModel.Add(resultadoPolizaModel);
                }
            }
            return resultadosPolizaModel;
        }

        /// <summary>
        /// Obtiene los movimientos de inventario para generar
        /// poliza de consumo de producto
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"> </param>
        /// <returns></returns>
        internal List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventario(int almacenID, int organizacionID, DateTime fecha)
        {
            List<ContenedorAlmacenMovimientoCierreDia> result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ObtenerMovimientosInventario(almacenID, organizacionID, fecha);
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
            return result;
        }

        /// <summary>
        /// Obtiene los movimientos de los subproductos de premezcla
        /// </summary>
        /// <param name="productosPremezcla"></param>
        /// <returns></returns>
        internal IEnumerable<AlmacenMovimientoSubProductosModel> ObtenerMovimientosSubProductos(IEnumerable<AlmacenMovimientoSubProductosModel> productosPremezcla)
        {
            IEnumerable<AlmacenMovimientoSubProductosModel> result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ObtenerMovimientosSubProductos(productosPremezcla);
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
            return result;
        }

         /// <summary>
        /// Obtiene todos los movimientos de un Contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoInfo> ObtenerMovimientosPorContrato(ContratoInfo contrato)
        {
            List<AlmacenMovimientoInfo> result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoDAL();
                result = almacenMovimientoDal.ObtenerMovimientosPorContrato(contrato);
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
            return result;
        }
    }
}
