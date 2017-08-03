using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace SIE.Services.Servicios.BL
{
    internal class EnvioAlimentoBL
    {
        /// <summary>
        /// Registra el envio de alimento de una organizacion a otra
        /// </summary>
        /// <param name="envioAlimento">Información del envio de alimento a guardar</param>
        /// <returns>Regresa una confirmación de registro del envio de alimento</returns>
        internal EnvioAlimentoInfo RegistrarEnvioAlimento(EnvioAlimentoInfo envioAlimento)
        {
            EnvioAlimentoInfo confirmacion = new EnvioAlimentoInfo();
            try
            {
                Logger.Info();
                AlmacenMovimientoBL almacenMovimientoBL = new AlmacenMovimientoBL();

                EnvioAlimentoDAL salidaAlimentoDAL = new EnvioAlimentoDAL();
                AlmacenMovimientoDetalleBL almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                AlmacenMovimientoDetalle detalleMovimiento = new AlmacenMovimientoDetalle();
                AlmacenInventarioBL inventario = new AlmacenInventarioBL();
                AlmacenInventarioLoteBL loteBl = new AlmacenInventarioLoteBL();

                AlmacenMovimientoInfo almacenMovimiento = new AlmacenMovimientoInfo();
                almacenMovimiento.AlmacenID = envioAlimento.Almacen.AlmacenID;
                almacenMovimiento.TipoMovimientoID = TipoMovimiento.ProductoSalidaTraspaso.GetHashCode();

                almacenMovimiento.Status = Estatus.AplicadoInv.GetHashCode();
                almacenMovimiento.UsuarioCreacionID = envioAlimento.UsuarioCreacionID;
                almacenMovimiento.UsuarioModificacionID = envioAlimento.UsuarioCreacionID;
                almacenMovimiento.EsEnvioAlimento = true;
                almacenMovimiento.OrganizacionID = envioAlimento.Origen.OrganizacionID;

                PolizaAbstract poliza = null;
                IList<PolizaInfo> listaPolizas = null;

                using (var transaccion = new TransactionScope())
                {
                    //registrar en TB AlmacenMovimiento
                    long almacenMovimientoID = almacenMovimientoBL.Crear(almacenMovimiento);
                    almacenMovimiento.AlmacenMovimientoID = almacenMovimientoID;
                    envioAlimento.AlmacenMovimientoId = almacenMovimientoID;

                    if (envioAlimento.AlmacenMovimientoId == 0)
                        return new EnvioAlimentoInfo { EnvioId = 0 };

                    //registrar en TB AlmacenMovimientoDetalle
                    detalleMovimiento.AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;
                    almacenMovimiento = almacenMovimientoBL.ObtenerPorId(almacenMovimiento.AlmacenMovimientoID);

                    if (almacenMovimiento.AlmacenMovimientoID == 0)
                        return new EnvioAlimentoInfo { EnvioId = 0 };

                    envioAlimento.Folio = almacenMovimiento.FolioMovimiento;
                    detalleMovimiento.Producto = envioAlimento.Producto;

                    if (detalleMovimiento.Producto.ManejaLote)
                    {
                        detalleMovimiento.AlmacenInventarioLoteId = envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().AlmacenInventarioLoteId;
                        detalleMovimiento.Precio = envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().PrecioPromedio;
                    }
                    else
                        detalleMovimiento.Precio = envioAlimento.AlmacenInventario.PrecioPromedio;

                    detalleMovimiento.ProductoID = envioAlimento.Producto.ProductoId;
                    detalleMovimiento.Cantidad = envioAlimento.Cantidad;
                    detalleMovimiento.Importe = envioAlimento.Importe;
                    detalleMovimiento.UsuarioCreacionID = envioAlimento.UsuarioCreacionID;
                    detalleMovimiento.Piezas = envioAlimento.Piezas;
                    int almacenMovimientoDetalleid = almacenMovimientoDetalleBl.Crear(detalleMovimiento);
                    if (almacenMovimientoDetalleid == 0)
                        return new EnvioAlimentoInfo { EnvioId = 0 };

                    //registrar en TB EnvioProducto
                    confirmacion = salidaAlimentoDAL.RegistrarEnvioAlimento(envioAlimento);

                    if (confirmacion.EnvioId == 0)
                        return new EnvioAlimentoInfo { EnvioId = 0 };

                    envioAlimento.EnvioId = confirmacion.EnvioId;
                    envioAlimento.FechaEnvio = confirmacion.FechaEnvio;

                    envioAlimento.AlmacenInventario.Cantidad -= envioAlimento.Cantidad;
                    envioAlimento.AlmacenInventario.Importe -= envioAlimento.Importe;
                    decimal precioPromedioInicial=0M;
                    if (envioAlimento.Producto.ManejaLote)
                    {
                        precioPromedioInicial = envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().PrecioPromedio;
                    }
                    else 
                    {
                        precioPromedioInicial = envioAlimento.AlmacenInventario.PrecioPromedio;
                    }
                    if (envioAlimento.AlmacenInventario.Cantidad == 0) 
                    {
                        envioAlimento.AlmacenInventario.PrecioPromedio = 0M;
                    }

                    if (envioAlimento.AlmacenInventario.Cantidad < 0)
                        envioAlimento.AlmacenInventario.Cantidad = 0;

                    if (envioAlimento.AlmacenInventario.Importe < 0)
                        envioAlimento.AlmacenInventario.Importe = 0;

                    if (envioAlimento.AlmacenInventario.Cantidad > 0 && envioAlimento.AlmacenInventario.Importe > 0)
                        envioAlimento.AlmacenInventario.PrecioPromedio = envioAlimento.AlmacenInventario.Importe / envioAlimento.AlmacenInventario.Cantidad;

                    envioAlimento.AlmacenInventario.UsuarioModificacionID = envioAlimento.UsuarioCreacionID;
                    inventario.ActualizarPorProductoId(envioAlimento.AlmacenInventario);

                    if (envioAlimento.Producto.ManejaLote)
                    {
                        FechaInfo fechaInfo = new FechaBL().ObtenerFechaActual();
                        envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad -= envioAlimento.Cantidad;
                        envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Importe -= envioAlimento.Importe;

                        if (envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad <= 0)
                        {
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().PrecioPromedio = 0M;
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad = 0;
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Importe = 0;

                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().FechaInicio = fechaInfo.FechaActual;
                        }

                        if (envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Importe > 0 && envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad > 0){
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().PrecioPromedio = envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Importe / envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Cantidad;
                        } else {
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Activo = EstatusEnum.Inactivo;
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().FechaFin = fechaInfo.FechaActual;
                        }

                       if (envioAlimento.Producto.SubfamiliaId == SubFamiliasEnum.Forrajes.GetHashCode()){
                            envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Piezas -= envioAlimento.Piezas;
                            if (envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Piezas < 0)
                                envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().Piezas = 0;
                        }

                        envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().UsuarioModificacionId = envioAlimento.UsuarioCreacionID;
                        loteBl.ActualizarEnvioAlimento(envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault());
                    }

                    if (salidaAlimentoDAL.RegistrarRecepcionProductoEnc(envioAlimento, TipoMovimiento.ProductoSalidaTraspaso) == 0)
                        return new EnvioAlimentoInfo { EnvioId = 0 };

                    if (salidaAlimentoDAL.RegistrarRecepcionProductoDet(envioAlimento, TipoMovimiento.ProductoSalidaTraspaso) == 0)
                        return new EnvioAlimentoInfo { EnvioId = 0 };

                    poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaTraspaso);

                    SolicitudProductoInfo oSolicitud = new SolicitudProductoInfo();
                    oSolicitud.FolioSolicitud = envioAlimento.Folio;
                    oSolicitud.OrganizacionID = envioAlimento.Origen.OrganizacionID;
                    oSolicitud.AlmacenGeneralID = envioAlimento.Almacen.AlmacenID;
                    oSolicitud.Almacen = new AlmacenBL().ObtenerAlmacenPorOrganizacion(envioAlimento.Destino.OrganizacionID).FirstOrDefault();
                    oSolicitud.FechaEntrega = envioAlimento.FechaEnvio;
                    oSolicitud.UsuarioCreacionID = envioAlimento.UsuarioCreacionID;
                    oSolicitud.Detalle = new List<SolicitudProductoDetalleInfo>();
                    SolicitudProductoDetalleInfo detalle=new SolicitudProductoDetalleInfo() {
                        Cantidad = envioAlimento.Cantidad,
                        PrecioPromedio = envioAlimento.Producto.ManejaLote ? envioAlimento.AlmacenInventario.ListaAlmacenInventarioLote.FirstOrDefault().PrecioPromedio: envioAlimento.AlmacenInventario.PrecioPromedio,
                        Producto = envioAlimento.Producto
                    };
                    detalle.PrecioPromedio = decimal.Round(detalle.PrecioPromedio, 2);
                    if(detalle.PrecioPromedio == 0)
                    {
                        detalle.PrecioPromedio = precioPromedioInicial;
                    }
                    oSolicitud.Detalle.Add(detalle);

                    listaPolizas = poliza.GeneraPoliza(oSolicitud);
                    var polizaDAL = new PolizaDAL();
                    if (listaPolizas != null && listaPolizas.Any())
                    {
                        string Referencia =  "03  " + envioAlimento.Folio.ToString() + new Random().Next(10, 20).ToString() + new Random().Next(30, 40) + DateTime.Now.Millisecond.ToString() + listaPolizas.ToList().FirstOrDefault().ClaseDocumento;
                        listaPolizas.ToList().ForEach(datos =>
                        {
                            datos.NumeroReferencia =  envioAlimento.Folio.ToString();
                            datos.OrganizacionID = oSolicitud.OrganizacionID;
                            datos.UsuarioCreacionID = envioAlimento.UsuarioCreacionID;
                            datos.Activo = EstatusEnum.Activo;
                            datos.ArchivoEnviadoServidor = 1;
                            datos.Referencia3 = Referencia;
                        });

                        ParametroOrganizacionInfo oParametroOrg = new ParametroOrganizacionBL().ObtenerPorOrganizacionIDClaveParametro(oSolicitud.OrganizacionID, ParametrosEnum.CuentaInventarioTransito.ToString());
                        if(oParametroOrg != null)
                            listaPolizas[listaPolizas.ToList().FindIndex(datos => datos.NumeroLinea.Trim() == "1")].Cuenta = oParametroOrg.Valor;
                        
                        envioAlimento.Poliza = poliza.ImprimePoliza(oSolicitud, listaPolizas);
                        polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.SalidaTraspaso);

                        transaccion.Complete();
                    }

                }
               
                return envioAlimento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
