using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Transactions;
using SIE.Services.Polizas.Fabrica;
using System.IO;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    public class RecepcionProductoBL
    {
        /// <summary>
        /// Guarda una recepcion de producto
        /// </summary>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal MemoryStream Guardar(RecepcionProductoInfo recepcionProducto)
        {
            try
            {
                MemoryStream pdf = null;
                PolizaAbstract poliza = null;
                IList<PolizaInfo> polizas = null;
                int organizacionID = 0;
                var recepcionProductoDal = new RecepcionProductoDAL();
                using (var transaction = new TransactionScope())
                {
                    int usuarioID = 0;
                    if (GuardarInventario(recepcionProducto))
                    {
                        long almacenMovimiento = GuardarMovimientos(recepcionProducto);

                        recepcionProducto.AlmacenMovimientoId = almacenMovimiento;
                        var recepcionGuardada = recepcionProductoDal.Guardar(recepcionProducto);
                        if (recepcionGuardada != null)
                        {
                            foreach (var recepcionProductoDetalleInfo in recepcionProducto.ListaRecepcionProductoDetalle
                                )
                            {
                                recepcionProductoDetalleInfo.RecepcionProductoId = recepcionGuardada.RecepcionProductoId;
                                recepcionProductoDetalleInfo.UsuarioCreacion = new UsuarioInfo()
                                                                                   {
                                                                                       UsuarioID =
                                                                                           recepcionProducto.
                                                                                           UsuarioCreacion.UsuarioID
                                                                                   };
                                usuarioID = recepcionProducto.UsuarioCreacion.UsuarioID;
                                organizacionID = recepcionProducto.Proveedor.OrganizacionID;
                                recepcionProductoDetalleInfo.Activo = EstatusEnum.Activo;
                            }

                            var recepcionProductoDetalleBl = new RecepcionProductoDetalleBL();
                            recepcionProductoDetalleBl.Guardar(recepcionProducto.ListaRecepcionProductoDetalle);

                            #region POLIZA

                            poliza =
                                FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaCompraMateriaPrima);
                            recepcionProducto.FolioRecepcion = recepcionGuardada.FolioRecepcion;
                            recepcionProducto.FechaRecepcion = recepcionGuardada.FechaRecepcion;
                            polizas = poliza.GeneraPoliza(recepcionProducto);
                            if (polizas != null)
                            {
                                if (organizacionID == 0)
                                {
                                    organizacionID = recepcionProducto.Almacen.Organizacion.OrganizacionID;
                                }
                                var polizaBL = new PolizaBL();
                                polizas.ToList().ForEach(datos =>
                                                             {
                                                                 datos.OrganizacionID = organizacionID;
                                                                 datos.UsuarioCreacionID = usuarioID;
                                                                 datos.ArchivoEnviadoServidor = 1;
                                                             });
                                polizaBL.GuardarServicioPI(polizas, TipoPoliza.EntradaCompraMateriaPrima);
                                pdf = poliza.ImprimePoliza(recepcionProducto, polizas);
                            }

                            #endregion POLIZA

                            transaction.Complete();
                        }
                    }
                }
                return pdf;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        private long GuardarMovimientos(RecepcionProductoInfo recepcionProducto)
        {
            long almacenMovimientoId = 0;
            try
            {
                var almacenMovimientoBl = new AlmacenMovimientoBL(); 
                AlmacenMovimientoInfo almacenMovimiento = new AlmacenMovimientoInfo();
                almacenMovimiento.AlmacenID = recepcionProducto.Almacen.AlmacenID;
                almacenMovimiento.TipoMovimientoID = (int)TipoMovimiento.RecepcionProducto;
                almacenMovimiento.Observaciones = recepcionProducto.Observaciones;
                almacenMovimiento.Status = (int)EstatusInventario.Aplicado;
                almacenMovimiento.ProveedorId = recepcionProducto.Proveedor.ProveedorID;
                almacenMovimiento.UsuarioCreacionID = recepcionProducto.UsuarioCreacion.UsuarioID;

                almacenMovimientoId = almacenMovimientoBl.Crear(almacenMovimiento);

                if (almacenMovimientoId > 0)
                {
                    var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                    foreach (var recepcionProductoDetalle in recepcionProducto.ListaRecepcionProductoDetalle)
                    {
                        var almacenMovimientoDetalle = new AlmacenMovimientoDetalle();
                        almacenMovimientoDetalle.AlmacenMovimientoID = almacenMovimientoId;
                        almacenMovimientoDetalle.ProductoID = recepcionProductoDetalle.Producto.ProductoId;
                        almacenMovimientoDetalle.Importe = recepcionProductoDetalle.Importe;
                        almacenMovimientoDetalle.Cantidad = recepcionProductoDetalle.Cantidad;
                        almacenMovimientoDetalle.Precio = almacenMovimientoDetalle.Importe/
                                                          almacenMovimientoDetalle.Cantidad;
                        almacenMovimientoDetalle.UsuarioCreacionID = recepcionProducto.UsuarioCreacion.UsuarioID;
                        almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalle);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return almacenMovimientoId;
        }

        internal bool GuardarInventario(RecepcionProductoInfo recepcionProducto)
        {
            try
            {
                AlmacenBL almacenBl = new AlmacenBL();
                AlmacenInventarioBL almacenInventarioBl = new AlmacenInventarioBL();
                foreach (var recepcionProductodetalle in recepcionProducto.ListaRecepcionProductoDetalle)
                {
                    AlmacenInfo almacen = almacenBl.ObtenerPorID(recepcionProducto.Almacen.AlmacenID);
                    List<AlmacenInventarioInfo> listaAlmacenlmacenInventario =
                        almacenInventarioBl.ObtienePorAlmacenId(almacen);

                    if (listaAlmacenlmacenInventario != null)
                    {
                        List<AlmacenInventarioInfo> almacenInventarioListaResultado =
                            listaAlmacenlmacenInventario.Where(
                                registro => registro.ProductoID == recepcionProductodetalle.Producto.ProductoId)
                                .ToList();

                        if (almacenInventarioListaResultado.Count > 0)
                        {
                            var almacenInventario = almacenInventarioListaResultado[0];
                            almacenInventario.Cantidad = almacenInventario.Cantidad +
                                                         recepcionProductodetalle.Cantidad;
                            almacenInventario.Importe = almacenInventario.Importe + recepcionProductodetalle.Importe;
                            almacenInventario.PrecioPromedio = almacenInventario.Importe/almacenInventario.Cantidad;
                            almacenInventario.UsuarioModificacionID = recepcionProducto.UsuarioCreacion.UsuarioID;

                            almacenInventarioBl.Actualizar(almacenInventario);
                        }
                        else
                        {
                            var almacenInventarioNuevo = new AlmacenInventarioInfo();
                            almacenInventarioNuevo.Almacen = recepcionProducto.Almacen;
                            almacenInventarioNuevo.AlmacenID = recepcionProducto.Almacen.AlmacenID;
                            almacenInventarioNuevo.Producto = recepcionProductodetalle.Producto;
                            almacenInventarioNuevo.ProductoID = recepcionProductodetalle.Producto.ProductoId;
                            almacenInventarioNuevo.Cantidad = recepcionProductodetalle.Cantidad;
                            almacenInventarioNuevo.Importe = recepcionProductodetalle.Importe;
                            almacenInventarioNuevo.PrecioPromedio = almacenInventarioNuevo.Importe /
                                                                    almacenInventarioNuevo.Cantidad;
                            almacenInventarioNuevo.UsuarioCreacionID = recepcionProducto.UsuarioCreacion.UsuarioID;
                            almacenInventarioBl.Crear(almacenInventarioNuevo);
                        }
                    }
                    else
                    {
                        var almacenInventarioNuevo = new AlmacenInventarioInfo();
                        almacenInventarioNuevo.Almacen = recepcionProducto.Almacen;
                        almacenInventarioNuevo.AlmacenID = recepcionProducto.Almacen.AlmacenID;
                        almacenInventarioNuevo.Producto = recepcionProductodetalle.Producto;
                        almacenInventarioNuevo.ProductoID = recepcionProductodetalle.Producto.ProductoId;
                        almacenInventarioNuevo.Cantidad = recepcionProductodetalle.Cantidad;
                        almacenInventarioNuevo.Importe = recepcionProductodetalle.Importe;
                        almacenInventarioNuevo.PrecioPromedio = almacenInventarioNuevo.Importe/
                                                                almacenInventarioNuevo.Cantidad;
                        almacenInventarioNuevo.UsuarioCreacionID = recepcionProducto.UsuarioCreacion.UsuarioID;
                        almacenInventarioBl.Crear(almacenInventarioNuevo);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recepcionProductoCompra"></param>
        /// <param name="organizacionId"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        internal RecepcionProductoInfo ObtenerRecepcionVista(RecepcionProductoInfo recepcionProductoCompra, int organizacionId, string conexion)
        {
            RecepcionProductoInfo info;
            try
            {
                Logger.Info();
                var recepcionProductoDAL = new RecepcionProductoDAL(conexion);
                info = recepcionProductoDAL.ObtenerRecepcionVista(recepcionProductoCompra, organizacionId);
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

        internal RecepcionProductoInfo ObtenerRecepcionPorFolio(RecepcionProductoInfo recepcionProductoCompra)
        {
            RecepcionProductoInfo info;
            try
            {
                Logger.Info();
                var recepcionProductoDAL = new RecepcionProductoDAL();
                info = recepcionProductoDAL.ObtenerRecepcionPorFolio(recepcionProductoCompra);
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
        /// Obtiene una coleccion de recepcion producto
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal List<RecepcionProductoInfo> ObtenerRecepcionProductoConciliacionPorAlmacenMovimiento(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            List<RecepcionProductoInfo> resultado;
            try
            {
                Logger.Info();
                var recepcionProductoDAL = new RecepcionProductoDAL();
                resultado = recepcionProductoDAL.ObtenerRecepcionProductoConciliacionPorAlmacenMovimiento(almacenesMovimiento);
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
            return resultado;
        }
    }
}
