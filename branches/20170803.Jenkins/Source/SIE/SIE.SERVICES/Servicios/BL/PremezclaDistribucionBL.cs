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
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    internal class PremezclaDistribucionBL
    {
        internal PremezclaDistribucionInfo GuardarPremezclaDistribucion(DistribucionDeIngredientesInfo distribucionIngredientes)
        {
            PremezclaDistribucionInfo distribucion = null;

            try
            {
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                var premezclaDistribucionDal = new PremezclaDistribucionDAL();
                var premezclaDistribucionDetalleDal = new PremezclaDistribucionDetalleDAL();
                var premezclaDistribucionCostoDal = new PremezclaDistribucionCostoDAL();
                long almacenMovimientoId = 0;
                // Afectamos el inventario.
                using (var transaction = new TransactionScope())
                {
                    distribucion = premezclaDistribucionDal.GuardarPremezclaDistribucion(new PremezclaDistribucionInfo
                    {
                        ProveedorId = distribucionIngredientes.Proveedor.ProveedorID,
                        Iva = distribucionIngredientes.Iva,
                        ProductoId = distribucionIngredientes.Producto.ProductoId,
                        CantidadExistente = distribucionIngredientes.CantidadTotal,
                        CostoUnitario = distribucionIngredientes.CostoUnitario,
                        UsuarioCreacionId = distribucionIngredientes.UsuarioId,
                    });

                    distribucionIngredientes.PremezclaDistribucionID = distribucion.PremezclaDistribucionId;

                    distribucion.ListaPremezclaDistribucionCosto =
                        premezclaDistribucionCostoDal.GuardarPremezclaDistribucionCosto(distribucionIngredientes);

                    /* Se calcula el costo extra y se proratea entre las organizaciones*/
                    //decimal costoDeCostosPorOrganizacion = 0;
                    decimal importeCostoTotal = 0;

                    if (distribucionIngredientes.ListaOrganizaciones.Any())
                    {
                        importeCostoTotal = distribucionIngredientes.ListaPremezclaDistribucionCosto.Sum(c => c.Importe);
                    }

                    foreach (var distribucionorganizaciones in distribucionIngredientes.ListaOrganizaciones)
                    {
                        decimal porcentajeSurtido = distribucionIngredientes.CantidadTotal > 0 ?
                            (decimal)distribucionorganizaciones.CantidadSurtir / (decimal)distribucionIngredientes.CantidadTotal : 0;
                        if (distribucionorganizaciones.Lote.AlmacenInventarioLoteId != 0)
                        {
                            // Se Genera un movimiento de almacen
                            ////Insertar en almacenmovimiento
                            var almacenMovimientoInfo = new AlmacenMovimientoInfo
                            {
                                ProveedorId = distribucionIngredientes.Proveedor.ProveedorID,
                                AlmacenID = distribucionorganizaciones.Lote.AlmacenInventario.Almacen.AlmacenID,
                                TipoMovimientoID = TipoMovimiento.EntradaAlmacen.GetHashCode(),
                                Observaciones = "",
                                Status = Estatus.AplicadoInv.GetHashCode(),
                                UsuarioCreacionID = distribucionIngredientes.UsuarioId
                            };
                            almacenMovimientoId = almacenMovimientoBl.Crear(almacenMovimientoInfo);
                            
                            //Se crea el Almacen Movimiento Costo
                            GuardarCosto(distribucionIngredientes, almacenMovimientoId, distribucionorganizaciones);

                            // Se modifica el Almacen Inventario
                            distribucionorganizaciones.Lote.AlmacenInventario.Cantidad =
                                distribucionorganizaciones.Lote.AlmacenInventario.Cantidad + distribucionorganizaciones.CantidadSurtir;

                            distribucionorganizaciones.Lote.AlmacenInventario.Importe =
                                distribucionorganizaciones.Lote.AlmacenInventario.Importe + distribucionorganizaciones.CostoTotal + (importeCostoTotal * porcentajeSurtido);

                            distribucionorganizaciones.Lote.AlmacenInventario.PrecioPromedio = distribucionorganizaciones.Lote.AlmacenInventario.Importe / distribucionorganizaciones.Lote.AlmacenInventario.Cantidad;



                            distribucionorganizaciones.Lote.AlmacenInventario.UsuarioModificacionID =
                                distribucionIngredientes.UsuarioId;
                            almacenInventarioBl.Actualizar(distribucionorganizaciones.Lote.AlmacenInventario);

                            // Se modifica el Almacen Inventario Lote
                            distribucionorganizaciones.Lote.Cantidad =
                                distribucionorganizaciones.Lote.Cantidad + distribucionorganizaciones.CantidadSurtir;

                            distribucionorganizaciones.Lote.Importe = distribucionorganizaciones.Lote.Importe + distribucionorganizaciones.CostoTotal + (importeCostoTotal * porcentajeSurtido);

                            distribucionorganizaciones.Lote.PrecioPromedio = distribucionorganizaciones.Lote.Importe / distribucionorganizaciones.Lote.Cantidad;


                            distribucionorganizaciones.Lote.UsuarioModificacionId = distribucionIngredientes.UsuarioId;
                            almacenInventarioLoteBl.Actualizar(distribucionorganizaciones.Lote);

                            // Se genera el Almacen Movimiento Detalle
                            almacenMovimientoDetalleBl.Crear(new AlmacenMovimientoDetalle
                            {
                                AlmacenMovimientoID = almacenMovimientoId,
                                AlmacenInventarioLoteId = distribucionorganizaciones.Lote.AlmacenInventarioLoteId,
                                Piezas = 0,
                                ProductoID = distribucionorganizaciones.Lote.AlmacenInventario.ProductoID,
                                Precio = distribucionorganizaciones.CostoUnitario,
                                Cantidad = distribucionorganizaciones.CantidadSurtir,
                                Importe = distribucionorganizaciones.CostoTotal,
                                UsuarioCreacionID = distribucionIngredientes.UsuarioId,
                                FechaCreacion = DateTime.Now
                            });

                            var premezclaDistribucionDetalle = premezclaDistribucionDetalleDal.GuardarPremezclaDistribucionDetalle(new PremezclaDistribucionDetalleInfo
                                {
                                    AlmacenMovimientoId = almacenMovimientoId,
                                    PremezclaDistribucionId = distribucion.PremezclaDistribucionId,
                                    CantidadASurtir = distribucionorganizaciones.CantidadSurtir,
                                    OrganizacionId = distribucionorganizaciones.Organizacion.OrganizacionID,
                                    UsuarioCreacionId = distribucionIngredientes.UsuarioId
                                });
                            distribucion.ListaPremezclaDistribucionDetalle.Add(premezclaDistribucionDetalle);

                            foreach (var distribucionOrg in distribucionIngredientes
                                .ListaOrganizaciones.Where(distribucionOrg => 
                                    distribucionOrg.Organizacion.OrganizacionID == premezclaDistribucionDetalle.OrganizacionId))
                            {
                                distribucionOrg.AlmaceMovimiento = new AlmacenMovimientoInfo
                                {
                                    AlmacenMovimientoID = premezclaDistribucionDetalle.AlmacenMovimientoId
                                };
                            }
                        }
                    }

                    #region POLIZA

                    distribucionIngredientes.AlmaceMovimientoID = almacenMovimientoId;
                    var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaPremezcla);
                    distribucionIngredientes.FechaEntrada = DateTime.Now;
                    IList<PolizaInfo> polizas = poliza.GeneraPoliza(distribucionIngredientes);
                    if (polizas != null && polizas.Any())
                    {
                        var polizaBL = new PolizaBL();
                        int organizacionID =
                            distribucionIngredientes.ListaOrganizaciones[0].Organizacion.OrganizacionID;
                        int usuarioCreacionID = distribucionIngredientes.UsuarioId;
                        polizas.ToList().ForEach(datos =>
                                                     {
                                                         datos.OrganizacionID = organizacionID;
                                                         datos.UsuarioCreacionID = usuarioCreacionID;
                                                         datos.ArchivoEnviadoServidor = 1;
                                                     });
                        polizaBL.GuardarServicioPI(polizas, TipoPoliza.PolizaPremezcla);
                    }

                    #endregion POLIZA
                    transaction.Complete();
                }

                return distribucion;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Guarda los costos de los movimientos
        /// </summary>
        /// <param name="distribucionDeIngredientes"></param>
        /// <param name="almacenMovimientoID"></param>
        /// <param name="distribucionorganizaciones"></param>
        /// <returns></returns>
        internal bool GuardarCosto(DistribucionDeIngredientesInfo distribucionDeIngredientes, long almacenMovimientoID, DistribucionDeIngredientesOrganizacionInfo distribucionorganizaciones)
        {
            bool regreso = true;
            try
            {
                var cuentaSAPBL = new CuentaSAPBL();
                IList<CuentaSAPInfo> cuentasSAP = cuentaSAPBL.ObtenerTodos(EstatusEnum.Activo);
                var almacenMovimientoCostoBl = new AlmacenMovimientoCostoBL();
                if (almacenMovimientoID > 0)
                {
                    //Se obtiene el porcentaje a cobrar cargar por organizacion
                    decimal porcentajeSurtido = distribucionDeIngredientes.CantidadTotal > 0 ?
                        (decimal)distribucionorganizaciones.CantidadSurtir / (decimal)distribucionDeIngredientes.CantidadTotal : 0;
                    foreach (var costoDistribucion in distribucionDeIngredientes.ListaPremezclaDistribucionCosto)
                    {
                        
                        var almacenMovimientoCosto = new AlmacenMovimientoCostoInfo
                        {
                            AlmacenMovimientoId = almacenMovimientoID,
                            Iva = costoDistribucion.Iva,
                            Retencion = costoDistribucion.Retencion,
                            CostoId = costoDistribucion.Costo.CostoID,
                            Importe = costoDistribucion.Importe * porcentajeSurtido,
                            UsuarioCreacionId = costoDistribucion.UsuarioCreacionID
                        };

                        if (costoDistribucion.TieneCuenta)
                        {
                            CuentaSAPInfo cuenta =
                                cuentasSAP.FirstOrDefault(
                                    sap => sap.CuentaSAP.Trim().Equals(costoDistribucion.CuentaSAP.CuentaSAP.Trim()));
                            if (cuenta != null)
                            {
                                almacenMovimientoCosto.CuentaSAPID = cuenta.CuentaSAPID;
                                almacenMovimientoCosto.TieneCuenta = costoDistribucion.TieneCuenta;
                            }
                        }
                        else
                        {
                            almacenMovimientoCosto.ProveedorId = costoDistribucion.Proveedor.ProveedorID;
                        }
                        
                        almacenMovimientoCostoBl.Crear(almacenMovimientoCosto);
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
        /// Obtiene una lista de distribucion de ingredientes
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<DistribucionDeIngredientesInfo> ObtenerPremezclaDistribucionConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var premezclaDistribucionDAL = new PremezclaDistribucionDAL();
                List<DistribucionDeIngredientesInfo> distribucionDeIngredientes =
                    premezclaDistribucionDAL.ObtenerPremezclaDistribucionConciliacion(organizacionID, fechaInicio, fechaFinal);
                return distribucionDeIngredientes;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<PremezclaDistribucionCostoInfo> ObtenerPremezclaDistribucionCosto(int productoID)
        {
            try
            {
                var premezclaDistribucionDAL = new PremezclaDistribucionDAL();
                List<PremezclaDistribucionCostoInfo> distribucionDeIngredientes =
                    premezclaDistribucionDAL.ObtenerPremezclaDistribucionCosto(productoID);
                return distribucionDeIngredientes;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
