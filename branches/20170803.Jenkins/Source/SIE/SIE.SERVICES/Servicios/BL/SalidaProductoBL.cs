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
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Polizas;
using System.IO;

namespace SIE.Services.Servicios.BL
{
    internal class SalidaProductoBL
    {
        /// <summary>
        /// Obtiene una salida de producto por id
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal SalidaProductoInfo ObtenerPorSalidaProductoId(SalidaProductoInfo salidaProducto)
        {
            try
            {
                var salidaProductoDal = new SalidaProductoDAL();
                salidaProducto = salidaProductoDal.ObtenerPorSalidaProductoId(salidaProducto);
                if (salidaProducto != null)
                {
                    if (salidaProducto.Organizacion.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        salidaProducto.Organizacion =
                            organizacionBl.ObtenerPorID(salidaProducto.Organizacion.OrganizacionID);
                    }

                    if (salidaProducto.OrganizacionDestino.OrganizacionID > 0)
                    {
                        var organizacionBl = new OrganizacionBL();
                        salidaProducto.Organizacion =
                            organizacionBl.ObtenerPorID(salidaProducto.OrganizacionDestino.OrganizacionID);
                    }

                    if (salidaProducto.Almacen.AlmacenID > 0)
                    {
                        var almacenBl = new AlmacenBL();
                        salidaProducto.Almacen = almacenBl.ObtenerPorID(salidaProducto.Almacen.AlmacenID);
                    }

                    if (salidaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId > 0)
                    {
                        var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                        salidaProducto.AlmacenInventarioLote =
                            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                salidaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId);
                    }

                    if (salidaProducto.Cliente.ClienteID > 0)
                    {
                        var clienteBl = new ClienteBL();
                        salidaProducto.Cliente = clienteBl.ObtenerPorID(salidaProducto.Cliente.ClienteID);
                    }
                    if (salidaProducto.CuentaSAP.CuentaSAPID > 0)
                    {
                        var cuentaSapBl = new CuentaSAPBL();
                        salidaProducto.CuentaSAP = cuentaSapBl.ObtenerPorID(salidaProducto.CuentaSAP.CuentaSAPID);
                    }

                    if (salidaProducto.TipoMovimiento.TipoMovimientoID > 0)
                    {
                        var tipoMovimientoBl = new TipoMovimientoBL();
                        salidaProducto.TipoMovimiento =
                            tipoMovimientoBl.ObtenerPorID(salidaProducto.TipoMovimiento.TipoMovimientoID);
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
            return salidaProducto;
        }

        /// <summary>
        /// Obtiene una salida de producto por foliosalida
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal SalidaProductoInfo ObtenerPorFolioSalida(SalidaProductoInfo filtro)
        {
            SalidaProductoInfo salidaProducto = null;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                salidaProducto = entradaProductoDAL.ObtenerPorFolioSalida(filtro);
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
            return salidaProducto;
        }

        /// <summary>
        ///     Obtiene un lista de salidas de productos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaSalidaProducto(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> costoLista = null;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                costoLista = entradaProductoDAL.ObtenerFoliosPorPaginaParaSalidaProducto(pagina, filtro);
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
            return costoLista;
        }
        /// <summary>
        /// Guarda el primer pesaje
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal SalidaProductoInfo GuardarPrimerPesajeSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    var entradaProductoDAL = new SalidaProductoDAL();
                    int resultado = entradaProductoDAL.GuardarPrimerPesajeSalida(salida);
                    if (resultado > 0)
                    {
                        salida.FolioSalida = resultado;
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
            return salida;
        }

        /// <summary>
        /// Guarda el segundo pesaje de la salida de producto
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal SalidaProductoInfo GuardarSegundoPesajeSalida(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                using (var transaction = new TransactionScope())
                {
                    var entradaProductoDAL = new SalidaProductoDAL();
                    int resultado = entradaProductoDAL.GuardarSegundoPesajeSalida(salida);
                    if (resultado > 0)
                    {
                        salida.FolioSalida = resultado;
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
            return salida;
        }
        /// <summary>
        /// Termina la salida del producto
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        internal MemoryStream TerminarSalidaProducto(SalidaProductoInfo salida)
        {
            try
            {
                Logger.Info();
                bool generaFactura = salida.GeneraFactura;
                MemoryStream resultado = null;
                var almacenBl = new AlmacenBL();
                var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                var almacenMovimiento = new AlmacenMovimientoInfo();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                var almacenInventarioBl = new AlmacenInventarioBL();
                var salidaProductoDal = new SalidaProductoDAL();

                PolizaAbstract poliza = null;
                IList<PolizaInfo> polizas = null;
                using (var transaction = new TransactionScope())
                {
                    almacenMovimiento.AlmacenID = salida.Almacen.AlmacenID;
                    almacenMovimiento.TipoMovimientoID = salida.TipoMovimiento.TipoMovimientoID;
                    almacenMovimiento.UsuarioCreacionID = salida.UsuarioModificacionId;
                    almacenMovimiento.Status = (int) EstatusInventario.Aplicado;

                    AlmacenMovimientoInfo almacenMovimientoGenerado =
                        almacenBl.GuardarAlmacenMovimiento(almacenMovimiento);

                    if (almacenMovimientoGenerado != null)
                    {
                        AlmacenInventarioLoteInfo almacenInventarioLote =
                            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                salida.AlmacenInventarioLote.AlmacenInventarioLoteId);
                        var listaAlmacenInventarioDetalle = new List<AlmacenMovimientoDetalle>();

                        var movimientoDetalle = new AlmacenMovimientoDetalle
                                                    {
                                                        AlmacenMovimientoID =
                                                            almacenMovimientoGenerado.AlmacenMovimientoID,
                                                        AlmacenInventarioLoteId =
                                                            salida.AlmacenInventarioLote.AlmacenInventarioLoteId,
                                                        Precio = almacenInventarioLote.PrecioPromedio,
                                                        ProductoID = salida.Producto.ProductoId,
                                                        Cantidad = salida.PesoBruto - salida.PesoTara,
                                                        Piezas = salida.Piezas,
                                                        Importe =
                                                            (salida.PesoBruto - salida.PesoTara)*
                                                            almacenInventarioLote.PrecioPromedio,
                                                        UsuarioCreacionID = salida.UsuarioModificacionId

                                                    };
                        listaAlmacenInventarioDetalle.Add(movimientoDetalle);

                        almacenMovimientoDetalleDAL.GuardarAlmacenMovimientoDetalle(listaAlmacenInventarioDetalle,
                                                                                    almacenMovimientoGenerado.
                                                                                        AlmacenMovimientoID);
                        if (almacenInventarioLote != null)
                        {
                            almacenInventarioLote.Cantidad = almacenInventarioLote.Cantidad -
                                                             (salida.PesoBruto - salida.PesoTara);
                            almacenInventarioLote.Importe = almacenInventarioLote.PrecioPromedio*
                                                            almacenInventarioLote.Cantidad;

                            almacenInventarioLote.UsuarioModificacionId = salida.UsuarioModificacionId;
                            almacenInventarioLoteBl.Actualizar(almacenInventarioLote);

                            AlmacenInfo almacen = almacenBl.ObtenerPorID(almacenMovimiento.AlmacenID);
                            List<AlmacenInventarioInfo> listaAlmacenlmacenInventario =
                                almacenInventarioBl.ObtienePorAlmacenId(almacen);
                            if (listaAlmacenlmacenInventario != null)
                            {
                                AlmacenInventarioInfo inventarioProducto = listaAlmacenlmacenInventario.FirstOrDefault(
                                    registro => registro.ProductoID == salida.Producto.ProductoId);

                                if (inventarioProducto != null)
                                {
                                    inventarioProducto.Cantidad = inventarioProducto.Cantidad -
                                                                  (salida.PesoBruto - salida.PesoTara);
                                    inventarioProducto.Importe = (inventarioProducto.PrecioPromedio*
                                                                  inventarioProducto.Cantidad);
                                    //Actualiza inventario
                                    inventarioProducto.UsuarioModificacionID = salida.UsuarioModificacionId;
                                    inventarioProducto.ProductoID = salida.Producto.ProductoId;
                                    inventarioProducto.AlmacenID = almacen.AlmacenID;
                                    almacenInventarioBl.ActualizarPorProductoId(inventarioProducto);
                                    salida.GeneraFactura = generaFactura;
                                    salidaProductoDal.TerminarSalidaProducto(salida, almacenMovimientoGenerado);

                                    #region POLIZA

                                    poliza =
                                        FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(
                                            TipoPoliza.SalidaVentaProducto);
                                    salida.Importe = (salida.PesoBruto - salida.PesoTara)*salida.Precio;
                                    salida = ObtenerFolioPorReimpresion(salida);
                                    polizas = poliza.GeneraPoliza(salida);
                                    if (polizas != null)
                                    {
                                        var polizaBL = new PolizaBL();
                                        polizas.ToList().ForEach(datos =>
                                                                     {
                                                                         datos.OrganizacionID =
                                                                             salida.Organizacion.OrganizacionID;
                                                                         datos.UsuarioCreacionID =
                                                                             salida.UsuarioCreacionId;
                                                                         datos.ArchivoEnviadoServidor = 1;
                                                                     });
                                        polizaBL.GuardarServicioPI(polizas, TipoPoliza.SalidaVentaProducto);
                                        if (salida.Almacen == null)
                                        {
                                            salida.Almacen = new AlmacenInfo();
                                        }
                                        salida.Almacen.Organizacion = new OrganizacionInfo
                                                                          {
                                                                              OrganizacionID =
                                                                                  salida.Organizacion.OrganizacionID
                                                                          };
                                        resultado = poliza.ImprimePoliza(salida, polizas);
                                    }

                                    #endregion POLIZA

                                    if (generaFactura)
                                    {
                                        #region FACTURA

                                        if (salida.TipoMovimiento.TipoMovimientoID ==
                                            TipoMovimiento.ProductoSalidaVenta.GetHashCode())
                                        {
                                            //Genera el xml y lo guarda en la ruta especificada en la configuración
                                            var facturaBl = new FacturaBL();
                                            facturaBl.GenerarDatosFacturaVentaDeMateriaPrima(salida);
                                        }

                                        #endregion
                                    }

                                    transaction.Complete();
                                }
                                else
                                {
                                    throw new ExcepcionDesconocida(
                                        Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                                }
                            }
                            else
                            {
                                throw new ExcepcionDesconocida(
                                    Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                            }
                        }
                        else
                        {
                            throw new ExcepcionDesconocida(
                                Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                        }
                    }
                    else
                    {
                        throw new ExcepcionDesconocida(
                            Properties.ResourceServices.InventarioNormal_ErrorMovimientos);
                    }
                }
                return resultado;
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
        /// Actualiza el almacen, almacen inventario lote y piezas.
        /// </summary>
        /// <param name="salida"></param>
        internal void ActualizarAlmacenInventarioLote(SalidaProductoInfo salida)
        {
            try
            {
                var entradaProductoDAL = new SalidaProductoDAL();
                entradaProductoDAL.ActualizarAlmacenInventarioLote(salida);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Consulta los folios activos con el peso tara capturado.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<SalidaProductoInfo> ObtenerTraspasoFoliosActivos(SalidaProductoInfo filtro)
        {
            List<SalidaProductoInfo> salidaProductos = null;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                salidaProductos = entradaProductoDAL.ObtenerTraspasoFoliosActivos(filtro);
                if (salidaProductos != null && salidaProductos.Count > 0)
                {
                    foreach (var salida in salidaProductos)
                    {
                        if (salida.Cliente != null && salida.Cliente.ClienteID > 0)
                        {
                            var clienteBl = new ClienteBL();
                            salida.Cliente = clienteBl.ObtenerPorID(salida.Cliente.ClienteID);
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
            return salidaProductos;
        }

        /// <summary>
        /// Obtiene un lista paginada de los folios de salida
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<SalidaProductoInfo> ObtenerFolioPorPaginaReimpresion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> salidas;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                salidas = entradaProductoDAL.ObtenerFolioPorPaginaReimpresion(pagina, filtro);
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
            return salidas;
        }

        /// <summary>
        /// Obtiene un folio de salida
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal SalidaProductoInfo ObtenerFolioPorReimpresion(SalidaProductoInfo filtro)
        {
            SalidaProductoInfo salida;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                salida = entradaProductoDAL.ObtenerFolioPorReimpresion(filtro);
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
            return salida;
        }

        /// <summary>
        /// Obtiene una lista de salidas de producto
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal IEnumerable<SalidaProductoInfo> ObtenerSalidasProductioConciliacionPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            IEnumerable<SalidaProductoInfo> salidasProducto;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                salidasProducto =
                    entradaProductoDAL.ObtenerSalidasProductioConciliacionPorAlmacenMovimientoXML(almacenesMovimiento);
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
            return salidasProducto;
        }

        internal bool Cancelar(SalidaProductoInfo salidaProducto)
        {
            try
            {
                Logger.Info();
                var salidaProductoDAL = new SalidaProductoDAL();
                return salidaProductoDAL.Cancelar(salidaProducto);
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

        internal ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaCancelacion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> salidas;
            try
            {
                Logger.Info();
                var entradaProductoDAL = new SalidaProductoDAL();
                salidas = entradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacion(pagina, filtro);
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
            return salidas;
        }


    }
}
