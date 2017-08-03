using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    internal class GastoMateriaPrimaBL
    {
        /// <summary>
        /// Guarda los datos para un gasto materia prima
        /// </summary>
        /// <param name="gasto"></param>
        /// <returns></returns>
        internal long Guardar(GastoMateriaPrimaInfo gasto)
        {
            long folioGasto = 0;
            try
            {
                Logger.Info();
                IList<PolizaInfo> polizas;
                PolizaAbstract poliza;
                using (var transaction = new TransactionScope())
                {
                    var tipoOrg = gasto.Organizacion.TipoOrganizacion.TipoOrganizacionID;
                    var gastoMateriaPrimaDal = new GastoMateriaPrimaDAL();

                    if (gasto.GuardaAretes)
                    { 
                        gastoMateriaPrimaDal.GuardarAretes(gasto);
                    }

                    if (tipoOrg == TipoOrganizacion.Centro.GetHashCode() || tipoOrg == TipoOrganizacion.Cadis.GetHashCode() || tipoOrg == TipoOrganizacion.Descanso.GetHashCode())
                    {
                        #region CENTROS

                        gasto.TipoFolio = TipoFolio.GastoMateriaPrima;
                        gasto.AlmacenMovimientoID = null;
                        folioGasto = gastoMateriaPrimaDal.Guardar(gasto);

                        #endregion CENTROS
                    }
                    else
                    {
                        #region SIAP

                        #region MOVIMIENTOS

                        var listaAlmacenDetalle = new List<AlmacenMovimientoDetalle>();

                        var almacenMovimiento = new AlmacenMovimientoInfo
                        {
                            AlmacenID = gasto.AlmacenID,
                            TipoMovimientoID = gasto.TipoMovimiento.TipoMovimientoID,
                            ProveedorId = gasto.Proveedor.ProveedorID,
                            Observaciones = gasto.Observaciones,
                            Status = (int)EstatusInventario.Aplicado,
                            UsuarioCreacionID = gasto.UsuarioCreacionID
                        };
                        var almacenMovimientoBl = new AlmacenMovimientoBL();
                        var almacenMovimientoID = almacenMovimientoBl.Crear(almacenMovimiento);
                        gasto.AlmacenMovimientoID = almacenMovimientoID;

                        var almacenMovimientoDetalle = new AlmacenMovimientoDetalle
                        {
                            AlmacenMovimientoID = almacenMovimientoID,
                            ProductoID = gasto.Producto.ProductoId,
                            UsuarioCreacionID = gasto.UsuarioCreacionID,
                            Importe = gasto.Importe,
                            Cantidad = 0,
                            Precio = 0
                        };
                        if (gasto.UnidadMedida)
                        {
                            almacenMovimientoDetalle.Cantidad = gasto.Kilogramos;
                            if (gasto.Kilogramos > 0)
                            {
                                almacenMovimientoDetalle.Precio = gasto.Importe / gasto.Kilogramos;
                            }
                            else
                            {
                                almacenMovimientoDetalle.Precio = 0;
                            }
                        }
                        gasto.TipoFolio = TipoFolio.GastoMateriaPrima;
                        int gastoMateriaPrimaID = gastoMateriaPrimaDal.Guardar(gasto);
                        //Actualizamos en AlmacenInventarioLote
                        if (gasto.AlmacenInventarioLote.AlmacenInventarioLoteId != 0)
                        {
                            almacenMovimientoDetalle.AlmacenInventarioLoteId =
                                gasto.AlmacenInventarioLote.AlmacenInventarioLoteId;

                            var almacenInventarioLote = new AlmacenInventarioLoteDAL();
                            var almacenInventarioLoteInfo = new AlmacenInventarioLoteInfo();
                            almacenInventarioLoteInfo =
                                almacenInventarioLote.ObtenerAlmacenInventarioLotePorId(
                                    gasto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                            almacenInventarioLoteInfo.UsuarioModificacionId = gasto.UsuarioCreacionID;
                            if (gasto.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.EntradaPorAjuste)
                            {
                                almacenInventarioLoteInfo.Importe += gasto.Importe;
                                almacenInventarioLoteInfo.Cantidad += gasto.Kilogramos;
                            }
                            else // Salida por Ajuste
                            {
                                almacenInventarioLoteInfo.Importe -= gasto.Importe;
                                almacenInventarioLoteInfo.Cantidad -= gasto.Kilogramos;
                            }
                            if (almacenInventarioLoteInfo.Cantidad == 0)
                            {
                                almacenInventarioLoteInfo.PrecioPromedio = 0;
                            }
                            else
                            {
                                almacenInventarioLoteInfo.PrecioPromedio = almacenInventarioLoteInfo.Importe /
                                                                           almacenInventarioLoteInfo.Cantidad;
                            }
                            almacenInventarioLote.Actualizar(almacenInventarioLoteInfo);
                        }
                        // Actualizamos en AlmacenInventario
                        var almacenInventario = new AlmacenInventarioDAL();
                        var almacenInventarioInfo = new AlmacenInventarioInfo
                        {
                            AlmacenID = gasto.AlmacenID,
                            ProductoID = gasto.Producto.ProductoId
                        };
                        almacenInventarioInfo = almacenInventario.ObtenerPorAlmacenIdProductoId(almacenInventarioInfo);
                        if (gasto.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.EntradaPorAjuste)
                        {
                            almacenInventarioInfo.Importe += gasto.Importe;
                            almacenInventarioInfo.Cantidad += gasto.Kilogramos;
                            gasto.EsEntrada = true;
                        }
                        else // Salida por Ajuste
                        {
                            almacenInventarioInfo.Importe -= gasto.Importe;
                            almacenInventarioInfo.Cantidad -= gasto.Kilogramos;
                        }
                        almacenInventarioInfo.UsuarioModificacionID = gasto.UsuarioCreacionID;
                        if (almacenInventarioInfo.Cantidad == 0)
                        {
                            almacenInventarioInfo.PrecioPromedio = 0;
                        }
                        else
                        {
                            almacenInventarioInfo.PrecioPromedio = almacenInventarioInfo.Importe / almacenInventarioInfo.Cantidad;
                        }
                        almacenInventario.Actualizar(almacenInventarioInfo);

                        listaAlmacenDetalle.Add(almacenMovimientoDetalle);
                        var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
                        almacenMovimientoDetalleBL.GuardarAlmacenMovimientoDetalle(listaAlmacenDetalle, almacenMovimientoID);

                        #endregion MOVIMIENTOS

                        #region POLIZA

                        GastoMateriaPrimaInfo gastoMateriaGuardado = gastoMateriaPrimaDal.ObtenerPorID(gastoMateriaPrimaID);

                        gasto.Fecha = gastoMateriaGuardado.Fecha;
                        gasto.FolioGasto = gastoMateriaGuardado.FolioGasto;
                        folioGasto = gastoMateriaGuardado.FolioGasto;
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.GastosMateriaPrima);
                        polizas = poliza.GeneraPoliza(gasto);
                        if (polizas != null && polizas.Any())
                        {
                            polizas.ToList().ForEach(datos =>
                            {
                                datos.OrganizacionID = gasto.Organizacion.OrganizacionID;
                                datos.UsuarioCreacionID = gasto.UsuarioCreacionID;
                                datos.ArchivoEnviadoServidor = 1;
                            });
                            var polizaBL = new PolizaBL();
                            if (gasto.EsEntrada)
                            {
                                polizaBL.GuardarServicioPI(polizas, TipoPoliza.EntradaAjuste);
                            }
                            else
                            {
                                polizaBL.GuardarServicioPI(polizas, TipoPoliza.SalidaAjuste);
                            }
                        }

                        #endregion POLIZA

                        #endregion SIAP
                    }

                    transaction.Complete();
                }
                return folioGasto;
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
        }

        /// <summary>
        /// Obtiene una entidad GastoMateriaPrima por su Id
        /// </summary>
        /// <param name="gastoMateriaPrimaID">Obtiene una entidad GastoMateriaPrima por su Id</param>
        /// <returns></returns>
        internal GastoMateriaPrimaInfo ObtenerPorID(int gastoMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                var gastoMateriaPrimaDAL = new GastoMateriaPrimaDAL();
                GastoMateriaPrimaInfo result = gastoMateriaPrimaDAL.ObtenerPorID(gastoMateriaPrimaID);
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
        /// Obtiene una lista de gastos de materia prima
        /// por sus movimientos de almacen
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal IEnumerable<GastoMateriaPrimaInfo> ObtenerGastosMateriaPrimaPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                var gastoMateriaPrimaDAL = new GastoMateriaPrimaDAL();
                IEnumerable<GastoMateriaPrimaInfo> result =
                    gastoMateriaPrimaDAL.ObtenerGastosMateriaPrimaPorAlmacenMovimientoXML(almacenesMovimiento);
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

        internal IEnumerable<AreteInfo> ConsultarAretes(List<AreteInfo> aretes, int organizacionID, bool esAreteSukarne, bool esEntradaAlmacen)
        {
            try
            {
                Logger.Info();
                var dal = new GastoMateriaPrimaDAL();
                return dal.ConsultarAretes(aretes, organizacionID, esAreteSukarne, esEntradaAlmacen);
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
