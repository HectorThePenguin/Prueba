using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using System.IO;

namespace SIE.Services.Servicios.BL
{
    public class AjusteDeInventarioBL
    {
        /// <summary>
        /// Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        public EstatusInfo ObtenerEstatusInfo(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            EstatusInfo info = null;
            try
            {
                Logger.Info();
                var estatusBL = new EstatusBL();
                var resultadoEstatus = estatusBL.ObtenerEstatusTipoEstatus((int)TipoEstatus.Inventario);

                if (resultadoEstatus != null)
                {
                    foreach (var estatus in resultadoEstatus.Where(estatus => estatus.EstatusId == almacenMovimientoInfo.Status))
                    {
                        info = new EstatusInfo
                        {
                            EstatusId = estatus.EstatusId,
                            Descripcion = estatus.Descripcion
                        };
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
            return info;
        }

        /// <summary>
        /// Obtiene los productos que presentan diferencia entre inventario teorico e inventario fisico
        /// </summary>
        /// <returns> </returns>
        public List<AjusteDeInventarioDiferenciasInventarioInfo> ObtenerDiferenciaInventarioFisicoTeorico(AlmacenMovimientoInfo almacenMovimientoInfo, int organizacionID)
        {
            var listaAjustes = new List<AjusteDeInventarioDiferenciasInventarioInfo>();
            List<AjusteDeInventarioDiferenciasInventarioInfo> listaAjustesFiltrados;

            try
            {
                Logger.Info();
                var ajusteDeInventarioDAL = new AjusteDeInventarioDAL();
                listaAjustesFiltrados = ajusteDeInventarioDAL.ObtenerDiferenciasInventario(almacenMovimientoInfo, organizacionID);
                if (listaAjustesFiltrados != null)
                {
                    listaAjustesFiltrados.ForEach(ajus =>
                        {
                            ajus.Cantidad = ajus.Cantidad - ajus.CantidadInventarioTeorico;
                            ajus.Importe = ajus.Cantidad * ajus.Precio;
                        });
                    listaAjustes.AddRange(listaAjustesFiltrados.Where(ajustes => ajustes.Cantidad == 0));

                    foreach (var ajustes in listaAjustes)
                    {
                        listaAjustesFiltrados.Remove(ajustes);
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
            return listaAjustesFiltrados;
        }

        /// <summary>
        /// Metodo para actualizar los ajustes de inventario contenidos en el grid
        /// </summary>
        /// <param name="articulosDiferenciasInventario"></param>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public IList<ResultadoPolizaModel> GuardarAjusteDeInventario(List<AjusteDeInventarioDiferenciasInventarioInfo> articulosDiferenciasInventario, AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                var resultadosPolizaModel = new List<ResultadoPolizaModel>();
                PolizaAbstract poliza = null;
                var cierreDiaInventarioDAL = new CierreDiaInventarioDAL();
                var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                var articulosEntrada = new List<AjusteDeInventarioDiferenciasInventarioInfo>();
                var articulosSalida = new List<AjusteDeInventarioDiferenciasInventarioInfo>();
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    if (articulosDiferenciasInventario != null)
                    {
                        var seleccionados =
                            articulosDiferenciasInventario.Any(articulosComprobar => articulosComprobar.Seleccionado);
                        if (seleccionados)
                        {
                            foreach (var articulos in articulosDiferenciasInventario.Where(art => art.Seleccionado))
                            {
                                ActualizarArticulosDiferencias(articulos, almacenMovimientoInfo);
                            }
                            almacenMovimientoInfo.Status = (int) EstatusInventario.Aplicado;
                            var almacenBL = new AlmacenBL();
                            almacenBL.ActualizarAlmacenMovimiento(almacenMovimientoInfo);

                            articulosEntrada =
                                articulosDiferenciasInventario.Where(mov => (mov.Seleccionado && mov.Cantidad > 0)).
                                    ToList();

                            articulosSalida =
                                articulosDiferenciasInventario.Where(mov => (mov.Seleccionado && mov.Cantidad < 0)).
                                    ToList();

                            //Generar los Movimientos de Entrada y Salida

                            #region Movimientos Entrada

                            if (articulosEntrada.Any())
                            {
                                var movimientoEntrada = new AlmacenCierreDiaInventarioInfo
                                                            {
                                                                Almacen = almacenMovimientoInfo.Almacen,
                                                                TipoMovimiento =
                                                                    TipoMovimiento.EntradaPorAjuste.GetHashCode(),
                                                                Observaciones = almacenMovimientoInfo.Observaciones,
                                                                Estatus = EstatusInventario.Autorizado.GetHashCode(),
                                                                UsuarioCreacionId =
                                                                    almacenMovimientoInfo.UsuarioModificacionID
                                                            };

                                var movimientosEntrada = (from detalle in articulosEntrada

                                                          select new AlmacenMovimientoDetalle
                                                                     {
                                                                         ProductoID = detalle.ProductoID,
                                                                         Precio = detalle.Precio,
                                                                         Cantidad = Math.Abs(detalle.Cantidad),
                                                                         Importe =
                                                                             Math.Round(
                                                                                 Convert.ToDecimal(
                                                                                     Math.Abs(detalle.Cantidad))*
                                                                                 detalle.Precio, 2),
                                                                         UsuarioCreacionID =
                                                                             almacenMovimientoInfo.UsuarioModificacionID
                                                                     }).ToList();

                                AlmacenCierreDiaInventarioInfo resultadoAlmacenMovimiento =
                                    cierreDiaInventarioDAL.GuardarAlmacenMovimiento(movimientoEntrada);

                                almacenMovimientoDetalleDAL.GuardarAlmacenMovimientoDetalle(movimientosEntrada,
                                                                                            resultadoAlmacenMovimiento.
                                                                                                AlmacenMovimientoID);

                            }

                            #endregion Movimientos Entrada

                            #region Movimientos Salida

                            if (articulosSalida.Any())
                            {
                                var movimientoSalida = new AlmacenCierreDiaInventarioInfo
                                                           {
                                                               Almacen = almacenMovimientoInfo.Almacen,
                                                               TipoMovimiento =
                                                                   TipoMovimiento.SalidaPorAjuste.GetHashCode(),
                                                               Observaciones = almacenMovimientoInfo.Observaciones,
                                                               Estatus = EstatusInventario.Autorizado.GetHashCode(),
                                                               UsuarioCreacionId =
                                                                   almacenMovimientoInfo.UsuarioModificacionID
                                                           };

                                var movimientosSalida = (from detalle in articulosSalida
                                                         select new AlmacenMovimientoDetalle
                                                                    {
                                                                        ProductoID = detalle.ProductoID,
                                                                        Precio = detalle.Precio,
                                                                        Cantidad = Math.Abs(detalle.Cantidad),
                                                                        Importe =
                                                                            Math.Round(
                                                                                Convert.ToDecimal(
                                                                                    Math.Abs(detalle.Cantidad))*
                                                                                detalle.Precio, 2),
                                                                        UsuarioCreacionID =
                                                                            almacenMovimientoInfo.UsuarioModificacionID
                                                                    }).ToList();

                                AlmacenCierreDiaInventarioInfo resultadoAlmacenMovimiento =
                                    cierreDiaInventarioDAL.GuardarAlmacenMovimiento(movimientoSalida);

                                almacenMovimientoDetalleDAL.GuardarAlmacenMovimientoDetalle(movimientosSalida,
                                                                                            resultadoAlmacenMovimiento.
                                                                                                AlmacenMovimientoID);

                            }

                            #endregion Movimientos Entrada

                        }
                        else
                        {
                            almacenMovimientoInfo.Status = (int) EstatusInventario.Cancelado;
                            var almacenBL = new AlmacenBL();
                            almacenBL.ActualizarAlmacenMovimiento(almacenMovimientoInfo);
                        }

                        #region POLIZA

                        #region PolizaSalida

                        MemoryStream stream;
                        if (articulosSalida.Any())
                        {
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaAjuste);

                            List<PolizaEntradaSalidaPorAjusteModel> salidasPorAjuste =
                                articulosSalida.Select(ajuste => new PolizaEntradaSalidaPorAjusteModel
                                                                     {
                                                                         Importe = Math.Abs(ajuste.Importe),
                                                                         Cantidad = Math.Abs(ajuste.Cantidad),
                                                                         TipoAjuste = TipoAjusteEnum.Merma,
                                                                         Precio = ajuste.Precio,
                                                                         AlmacenInventarioID =
                                                                             ajuste.AlmacenInventarioID,
                                                                         AlmacenMovimientoDetalleID =
                                                                             ajuste.AlmacenMovimientoDetalleID,
                                                                         ProductoID = ajuste.ProductoID,
                                                                         CantidadInventarioFisico =
                                                                             ajuste.CantidadInventarioFisico,
                                                                         CantidadInventarioTeorico =
                                                                             ajuste.CantidadInventarioTeorico,
                                                                         Observaciones =
                                                                             almacenMovimientoInfo.Observaciones
                                                                     }).ToList();
                            if (salidasPorAjuste.Any())
                            {
                                IList<PolizaInfo> polizasSalida = poliza.GeneraPoliza(salidasPorAjuste);
                                if (polizasSalida != null)
                                {
                                    stream = poliza.ImprimePoliza(salidasPorAjuste, polizasSalida);
                                    var polizaBL = new PolizaBL();
                                    polizasSalida.ToList().ForEach(datos =>
                                                                       {
                                                                           datos.UsuarioCreacionID =
                                                                               almacenMovimientoInfo.
                                                                                   UsuarioModificacionID;
                                                                           datos.OrganizacionID =
                                                                               almacenMovimientoInfo.OrganizacionID;
                                                                           datos.ArchivoEnviadoServidor = 1;
                                                                       });
                                    polizaBL.GuardarServicioPI(polizasSalida, TipoPoliza.SalidaAjuste);
                                    var resultadoPolizaModel = new ResultadoPolizaModel
                                                                   {
                                                                       Polizas = polizasSalida,
                                                                       PDFs =
                                                                           new Dictionary<TipoPoliza, MemoryStream>
                                                                               {{TipoPoliza.SalidaAjuste, stream}}
                                                                   };
                                    resultadosPolizaModel.Add(resultadoPolizaModel);
                                }
                            }
                        }

                        #endregion PolizaSalida

                        #region PolizaEntrada

                        if (articulosEntrada.Any())
                        {
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaAjuste);

                            List<PolizaEntradaSalidaPorAjusteModel> entradasPorAjuste =
                                articulosEntrada.Select(ajuste => new PolizaEntradaSalidaPorAjusteModel
                                                                      {
                                                                          Importe = Math.Abs(ajuste.Importe),
                                                                          Cantidad = Math.Abs(ajuste.Cantidad),
                                                                          TipoAjuste = TipoAjusteEnum.Merma,
                                                                          Precio = ajuste.Precio,
                                                                          AlmacenInventarioID =
                                                                              ajuste.AlmacenInventarioID,
                                                                          AlmacenMovimientoDetalleID =
                                                                              ajuste.AlmacenMovimientoDetalleID,
                                                                          ProductoID = ajuste.ProductoID,
                                                                          CantidadInventarioFisico =
                                                                              ajuste.CantidadInventarioFisico,
                                                                          CantidadInventarioTeorico =
                                                                              ajuste.CantidadInventarioTeorico,
                                                                          Observaciones =
                                                                              almacenMovimientoInfo.Observaciones
                                                                      }).ToList();
                            if (entradasPorAjuste.Any())
                            {
                                IList<PolizaInfo> polizasEntrada = poliza.GeneraPoliza(entradasPorAjuste);
                                if (polizasEntrada != null)
                                {
                                    stream = poliza.ImprimePoliza(entradasPorAjuste, polizasEntrada);
                                    var polizaBL = new PolizaBL();
                                    polizasEntrada.ToList().ForEach(datos =>
                                                                        {
                                                                            datos.UsuarioCreacionID =
                                                                                almacenMovimientoInfo.
                                                                                    UsuarioModificacionID;
                                                                            datos.OrganizacionID =
                                                                                almacenMovimientoInfo.OrganizacionID;
                                                                            datos.ArchivoEnviadoServidor = 1;
                                                                        });
                                    polizaBL.GuardarServicioPI(polizasEntrada, TipoPoliza.EntradaAjuste);
                                    var resultadoPolizaModel = new ResultadoPolizaModel
                                                                   {
                                                                       Polizas = polizasEntrada,
                                                                       PDFs =
                                                                           new Dictionary<TipoPoliza, MemoryStream>
                                                                               {{TipoPoliza.EntradaAjuste, stream}}
                                                                   };
                                    resultadosPolizaModel.Add(resultadoPolizaModel);
                                }
                            }
                        }

                        #endregion PolizaEntrada

                        #endregion POLIZA
                    }
                    else
                    {
                        almacenMovimientoInfo.Status = (int) EstatusInventario.Cancelado;
                        var almacenBL = new AlmacenBL();
                        almacenBL.ActualizarAlmacenMovimiento(almacenMovimientoInfo);
                    }
                    transaction.Complete();
                }
                return resultadosPolizaModel;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Se actualizan los registros seleccionados en AlmacenInventario
        /// </summary>
        /// <param name="articulo"></param>
        /// <param name="almacenMovimientoInfo"></param>
        public void ActualizarArticulosDiferencias(AjusteDeInventarioDiferenciasInventarioInfo articulo, AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            var cantidad = articulo.CantidadInventarioTeorico + articulo.Cantidad;
            var almacenInventarioInfo = new AlmacenInventarioInfo
            {
                Cantidad = cantidad,
                Importe = cantidad * articulo.PrecioInventarioTeorico,
                UsuarioModificacionID = almacenMovimientoInfo.UsuarioModificacionID,
                AlmacenInventarioID = articulo.AlmacenInventarioID
            };
            var almacenBL = new AlmacenBL();
            almacenBL.ActualizarAlmacenInventario(almacenInventarioInfo);
        }

        /// <summary>
        /// Se actualiza en AlmacenMovimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        public void ActualizarAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            var almacenBL = new AlmacenBL();
            almacenBL.ActualizarAlmacenMovimiento(almacenMovimientoInfo);
        }

        /// <summary>
        /// Se eliminan los registros no seleccionados en AlmacenMovimientoDetalle
        /// </summary>
        /// <param name="articulo"></param>
        /// <param name="almacenMovimientoInfo"></param>
        public void EliminarAlmacenMovimientoDetalle(AjusteDeInventarioDiferenciasInventarioInfo articulo, AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle
            {
                AlmacenMovimientoDetalleID = articulo.AlmacenMovimientoDetalleID
            };
            var almacenBL = new AlmacenBL();
            almacenBL.EliminaAlmacenMovimientoDetalle(almacenMovimientoDetalleInfo);
        }
    }
}
