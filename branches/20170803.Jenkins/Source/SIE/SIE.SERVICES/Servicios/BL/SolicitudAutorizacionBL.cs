using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using System.Transactions;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Properties;

namespace SIE.Services.Servicios.BL
{
    public class SolicitudAutorizacionBL
    {
        /// Obtiene los datos de la solicitud de autorizacion
        public SolicitudAutorizacionInfo ObtenerDatosSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                solicitudInfo = solicitudDAL.ObtenerDatosSolicitudAutorizacion(solicitudInfo);
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

            return solicitudInfo;
        }

        /// Valida si el precio venta menor al costo capturado ya fue rechazado para el folio salida
        public int ConsultarPrecioRechazadoSolicitud(int folioSalida, decimal precioVenta, int organizacionID)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                result = solicitudDAL.ConsultarPrecioRechazadoSolicitud(folioSalida, precioVenta, organizacionID);
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

        /// Se genera la solicitud de auorizacion para el precio venta menos capturado
        public int GenerarSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                int result = solicitudDAL.GenerarSolicitudAutorizacion(solicitudInfo);

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

        public AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizacionProgramacionMP(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                AutorizacionMateriaPrimaInfo result = solicitudDAL.ObtenerDatosSolicitudAutorizacionProgramacionMP(autorizacionInfo);

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
        /// Se obtiene las lista de solicitudes de precio de venta
        public List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesPrecioVenta(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                listaSolicitudesPendientes = solicitudDAL.ObtenerSolicitudesPendientesPrecioVenta(organizacionID, tipoAutorizacionID);
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

            return listaSolicitudesPendientes;
        }

        public AutorizacionMateriaPrimaInfo ObtenerDatosSolicitudAutorizada(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                AutorizacionMateriaPrimaInfo result = solicitudDAL.ObtenerDatosSolicitudAutorizada(autorizacionInfo);

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

        /// Se obtiene las lista de solicitudes de tipo uso de lote
        public List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesUsoLote(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                listaSolicitudesPendientes = solicitudDAL.ObtenerSolicitudesPendientesUsoLote(organizacionID, tipoAutorizacionID);
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

            return listaSolicitudesPendientes;
        }

        /// Se obtiene las lista de solicitudes de tipo ajuste de inventario
        public List<SolicitudAutorizacionPendientesInfo> ObtenerSolicitudesPendientesAjusteInventario(int organizacionID, int tipoAutorizacionID)
        {
            List<SolicitudAutorizacionPendientesInfo> listaSolicitudesPendientes = null;
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                listaSolicitudesPendientes = solicitudDAL.ObtenerSolicitudesPendientesAjusteInventario(organizacionID, tipoAutorizacionID);
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

            return listaSolicitudesPendientes;
        }
        /// <summary>
        /// Se guardan las respuesta a las solicitudes de materia prima
        /// </summary>
        /// <param name="respuestaSolicitudes"></param>
        /// <param name="organizacionID"></param>
        /// <param name="tipoAutorizacionID"></param>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public ResultadoValidacion GuardarRespuestasSolicitudes(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID, int tipoAutorizacionID, int usuarioID)
        {
            var result = new ResultadoValidacion();
            var mensaje = new StringBuilder();
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                var almacenMovimientoBl = new AlmacenMovimientoBL();

                using (var transaction = new TransactionScope())
                {
                    List<DiferenciasDeInventariosInfo> listaMovimientosPendientes =
                        solicitudDAL.ObtenerMovimientosAutorizacion(respuestaSolicitudes);

                    if (listaMovimientosPendientes != null && listaMovimientosPendientes.Any())
                    {
                        foreach (var movimiento in listaMovimientosPendientes)
                        {
                            if (movimiento.AlmacenMovimiento.TipoMovimientoID == TipoMovimiento.SalidaPorAjuste.GetHashCode())
                            {
                                if (movimiento.AlmacenMovimientoDetalle.Cantidad >
                                    movimiento.AlmacenInventarioLote.Cantidad)
                                {
                                    AutorizacionMovimientosInfo autorizacionCancelar =
                                        respuestaSolicitudes.FirstOrDefault(
                                            aut =>
                                            aut.AlmacenMovimientoID == movimiento.AlmacenMovimiento.AlmacenMovimientoID);

                                    if (autorizacionCancelar != null)
                                    {
                                        autorizacionCancelar.EstatusID = Estatus.AMPRechaza.GetHashCode();
                                        autorizacionCancelar.Observaciones =
                                            ResourceServices.SolicitudAutorizacionBL_RechazoAutomatico;
                                        mensaje.Append(string.Format(", {0}:{1}",
                                                                     movimiento.Producto.ProductoDescripcion,
                                                                     movimiento.AlmacenMovimientoDetalle.Cantidad));
                                    }
                                }
                            }
                        }
                    }

                    bool guardado = solicitudDAL.GuardarRespuestasSolicitudes(respuestaSolicitudes, organizacionID, tipoAutorizacionID, usuarioID);

                    if (guardado && tipoAutorizacionID == (int)TipoAutorizacionEnum.AjustedeInventario)
                    {
                        for (int i = 0; i < respuestaSolicitudes.Count; i++)
                        {
                            if (respuestaSolicitudes[i].EstatusID == (int)Estatus.AMPAutoriz)
                            {
                                respuestaSolicitudes[i].EstatusInventarioID = (int)Estatus.DifInvAutorizado;
                            }
                            else
                            {
                                respuestaSolicitudes[i].EstatusInventarioID = (int)Estatus.DifInvRechazado;
                            }
                        }
                        // Se realiza el proceso de afectar el inventario para los movimientos autorizados
                        List<DiferenciasDeInventariosInfo> listaAjustesPendientes;
                        var diferenciasDeInventarioBl = new DiferenciasDeInventarioBL();

                        listaAjustesPendientes = solicitudDAL.GuardarAjusteInventario(respuestaSolicitudes, organizacionID);

                        if (listaAjustesPendientes != null)
                        {
                            foreach (var diferenciasDeInventariosInfoPar in listaAjustesPendientes)
                            {
                                diferenciasDeInventariosInfoPar.DescripcionAjuste =
                                    diferenciasDeInventariosInfoPar.AlmacenMovimiento.TipoMovimientoID ==
                                    TipoMovimiento.SalidaPorAjuste.GetHashCode()
                                        ? TipoAjusteEnum.Merma.ToString()
                                        : TipoAjusteEnum.Superávit.ToString();
                                diferenciasDeInventariosInfoPar.DiferenciaInventario =
                                    diferenciasDeInventariosInfoPar.AlmacenMovimientoDetalle.Cantidad;

                                diferenciasDeInventariosInfoPar.AlmacenMovimiento.UsuarioModificacionID = usuarioID;
                                diferenciasDeInventariosInfoPar.AlmacenMovimientoDetalle.UsuarioModificacionID = usuarioID;
                                diferenciasDeInventariosInfoPar.AlmacenInventarioLote.UsuarioModificacionId = usuarioID;

                                diferenciasDeInventariosInfoPar.AlmacenMovimiento.Status =
                                Estatus.DifInvAplicado.GetHashCode();
                                diferenciasDeInventariosInfoPar.TieneConfiguracion = true;

                                //Se cambia el status por aplicado
                                almacenMovimientoBl.ActualizarEstatus(diferenciasDeInventariosInfoPar.AlmacenMovimiento);
                                //Actualiza inventario y lote
                                diferenciasDeInventarioBl.ActualizarInventarioYLote(diferenciasDeInventariosInfoPar, usuarioID);
                            }

                            #region POLIZA

                            List<PolizaEntradaSalidaPorAjusteModel> salidasPorAjuste =
                                listaAjustesPendientes.Where(dif => !dif.DescripcionAjuste.Trim().Equals(TipoAjusteEnum.CerrarLote.ToString().Trim(), StringComparison.InvariantCultureIgnoreCase)).Select(ajuste => new PolizaEntradaSalidaPorAjusteModel
                                {
                                    Importe =
                                        ajuste.DiferenciaInventario *
                                        ajuste.AlmacenInventarioLote.
                                            PrecioPromedio,
                                    Cantidad = ajuste.DiferenciaInventario,
                                    TipoAjuste =
                                        ajuste.DescripcionAjuste.Equals(
                                            TipoAjusteEnum.Merma.ToString(),
                                            StringComparison.
                                                CurrentCultureIgnoreCase)
                                            ? TipoAjusteEnum.Merma
                                            : TipoAjusteEnum.Superávit,
                                    Precio = ajuste.AlmacenInventarioLote.
                                        PrecioPromedio,
                                    AlmacenInventarioID =
                                        ajuste.AlmacenInventarioLote.
                                        AlmacenInventario.AlmacenInventarioID,
                                    AlmacenMovimientoDetalleID =
                                        ajuste.AlmacenMovimientoDetalle.
                                        AlmacenMovimientoDetalleID,
                                    ProductoID = ajuste.Producto.ProductoId,
                                    CantidadInventarioFisico =
                                        ajuste.KilogramosFisicos,
                                    CantidadInventarioTeorico =
                                        ajuste.KilogramosTeoricos,
                                    Observaciones =
                                        ajuste.AlmacenMovimiento.Observaciones
                                }).ToList();
                            var agrupado =
                                salidasPorAjuste.GroupBy(tipo => new { tipo.TipoAjuste, tipo.AlmacenMovimientoDetalleID }).Select(
                                    ajuste => new PolizaEntradaSalidaPorAjusteModel
                                    {
                                        TipoAjuste = ajuste.Key.TipoAjuste,
                                        AlmacenInventarioID = ajuste.First().AlmacenInventarioID,
                                        AlmacenMovimientoDetalleID = ajuste.Key.AlmacenMovimientoDetalleID,
                                        Cantidad = ajuste.First().Cantidad,
                                        CantidadInventarioFisico = ajuste.First().CantidadInventarioFisico,
                                        CantidadInventarioTeorico = ajuste.First().CantidadInventarioTeorico,
                                        Importe = ajuste.First().Importe,
                                        Observaciones = ajuste.First().Observaciones,
                                        Precio = ajuste.First().Precio,
                                        PrecioInventarioFisico = ajuste.First().PrecioInventarioFisico,
                                        PrecioInventarioTeorico = ajuste.First().PrecioInventarioTeorico,
                                        ProductoID = ajuste.First().ProductoID
                                    }).ToList();
                            if (agrupado != null && agrupado.Any())
                            {
                                var streams = new List<MemoryStream>();
                                for (int indexAjustes = 0; indexAjustes < agrupado.Count; indexAjustes++)
                                {
                                    var tipoPoliza = TipoPoliza.SalidaAjuste;
                                    switch (agrupado[indexAjustes].TipoAjuste)
                                    {
                                        case TipoAjusteEnum.Superávit:
                                            tipoPoliza = TipoPoliza.EntradaAjuste;
                                            break;
                                    }
                                    var poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(tipoPoliza);
                                    var grupo = new List<PolizaEntradaSalidaPorAjusteModel>
                                            {
                                                agrupado[indexAjustes]
                                            };
                                    var polizas = poliza.GeneraPoliza(grupo);
                                    if (polizas != null && polizas.Any())
                                    {
                                        MemoryStream stream = poliza.ImprimePoliza(grupo, polizas);
                                        if (stream != null)
                                        {
                                            streams.Add(stream);
                                        }
                                        var polizaBL = new PolizaBL();
                                        polizas.ToList().ForEach(datos =>
                                        {
                                            datos.OrganizacionID = organizacionID;
                                            datos.UsuarioCreacionID = usuarioID;
                                            datos.ArchivoEnviadoServidor = 1;
                                        });
                                        polizaBL.GuardarServicioPI(polizas, tipoPoliza);
                                    }
                                }
                            }

                            #endregion POLIZA

                        }
                    }
                    transaction.Complete();
                }
            }
            catch (ExcepcionServicio ex)
            {
                result.Mensaje = ex.Message;
                result.Resultado = false;
                result.CodigoMensaje = 2;
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
            if (mensaje.Length == 0)
            {
                result.Resultado = true;
            }
            else
            {
                result.Mensaje = String.Format(ResourceServices.SolicitudAutorizacionBL_AutorizacionRechazadas, mensaje);
                result.Resultado = false;
                result.CodigoMensaje = 1;
            }
            return result;
        }
        /// <summary>
        /// Se obtienen los datos de la solicitud de autorización para el folio
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        public AutorizacionMateriaPrimaInfo ObtenerSolicitudAutorizacion(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var solicitudDAL = new SolicitudAutorizacionDAL();
                AutorizacionMateriaPrimaInfo result = solicitudDAL.ObtenerSolicitudAutorizacion(autorizacionInfo);

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
    }
}
