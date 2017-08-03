using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    internal class RegistrarProgramacionFletesInternaBL
    {
        /// <summary>
        /// Hace el guardado de la premezcla
        /// </summary>
        internal void Guardar(FleteInternoInfo fleteInternoInfo, int usuarioId)
        {
            try
            {
                var fleteInternoBl = new FleteInternoBL();
                var fleteInternoDetalleBl = new FleteInternoDetalleBL();
                var fleteInternoCostoBl = new FleteInternoCostoBL();
                var listaFleteInternoCostoAgregados = new List<FleteInternoCostoInfo>();
                var listaFleteInternoCostoModificados = new List<FleteInternoCostoInfo>();
                var listaFleteInternoCostoEliminados = new List<FleteInternoCostoInfo>();
                using (var transaction = new TransactionScope())
                {
                    var fleteInternoId = 0;
                    //Si el flete es nuevo se crea
                    if (!fleteInternoInfo.Guardado)
                    {
                        fleteInternoInfo.UsuarioCreacionId = usuarioId;
                        fleteInternoId = fleteInternoBl.Crear(fleteInternoInfo);
                    }
                    else
                    {
                        fleteInternoId = fleteInternoInfo.FleteInternoId;
                    }

                    //Procesar cada detalle
                    foreach (var fleteInternoDetalleP in fleteInternoInfo.ListadoFleteInternoDetalle)
                    {
                        var fleteInternoDetalleId = 0;
                        if (!fleteInternoDetalleP.Guardado)
                        {
                            fleteInternoDetalleP.FleteInternoId = fleteInternoId;
                            fleteInternoDetalleP.UsuarioCreacionId = usuarioId;
                            fleteInternoDetalleId = fleteInternoDetalleBl.Crear(fleteInternoDetalleP);
                        }
                        else
                        {
                            if (fleteInternoDetalleP.Modificado)
                            {
                                //Actualizar flete interno detalle
                                fleteInternoDetalleBl.Actualizar(fleteInternoDetalleP);
                            }else
                            {
                                if (fleteInternoDetalleP.Eliminado)
                                {
                                    //Actualizar activo a 0
                                    fleteInternoDetalleBl.Actualizar(fleteInternoDetalleP);
                                    foreach (var fleteInternoCostoInfo in fleteInternoDetalleP.ListadoFleteInternoCosto)
                                    {
                                        fleteInternoCostoInfo.Activo = EstatusEnum.Inactivo;
                                        fleteInternoCostoInfo.UsuarioModificacionId = usuarioId;
                                    }
                                    fleteInternoCostoBl.Actualizar(fleteInternoDetalleP.ListadoFleteInternoCosto);
                                }
                            }
                            fleteInternoDetalleId = fleteInternoDetalleP.FleteInternoDetalleId;
                        }

                        //Guardar agregados
                        listaFleteInternoCostoAgregados.Clear();
                        listaFleteInternoCostoAgregados.AddRange(fleteInternoDetalleP.ListadoFleteInternoCosto.Where(fleteInternoCostoInfoP => !fleteInternoCostoInfoP.Guardado));
                        if (listaFleteInternoCostoAgregados.Count > 0)
                        {
                            //Crear flete interno costo
                            foreach (var fleteInternoCostoInfo in listaFleteInternoCostoAgregados)
                            {
                                fleteInternoCostoInfo.Tarifa = Decimal.Round(fleteInternoCostoInfo.Tarifa, 2);// fleteInternoCostoInfo.Tarifa;
                                //
                            }
                            fleteInternoCostoBl.Crear(listaFleteInternoCostoAgregados, new FleteInternoDetalleInfo() { FleteInternoDetalleId = fleteInternoDetalleId });
                        }

                        //Guardar modificados
                        listaFleteInternoCostoModificados.Clear();
                        listaFleteInternoCostoModificados.AddRange(fleteInternoDetalleP.ListadoFleteInternoCosto.Where(fleteInternoCostoInfoP => fleteInternoCostoInfoP.Modificado));
                        if (listaFleteInternoCostoModificados.Count > 0)
                        {
                            //Actualizar flete interno costo
                            foreach (var fleteInternoCostoInfo in listaFleteInternoCostoModificados)
                            {
                                fleteInternoCostoInfo.Tarifa = Decimal.Round(fleteInternoCostoInfo.Tarifa, 2);//fleteInternoCostoInfo.Tarifa / 1000;
                            }
                            fleteInternoCostoBl.Actualizar(listaFleteInternoCostoModificados);
                        }

                        //Guardar eliminados
                        listaFleteInternoCostoEliminados.Clear();
                        listaFleteInternoCostoEliminados.AddRange(fleteInternoDetalleP.ListadoFleteInternoCosto.Where(fleteInternoCostoInfoP => fleteInternoCostoInfoP.Eliminado));
                        if (listaFleteInternoCostoEliminados.Count > 0)
                        {
                            //Actualizar flete interno costo
                            fleteInternoCostoBl.Actualizar(listaFleteInternoCostoEliminados);
                        }
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
        }

        /// <summary>
        /// Cancela un flete
        /// </summary>
        /// <param name="fleteInternoInfo"></param>
        /// <param name="usuarioId"></param>
        internal void CancelarFlete(FleteInternoInfo fleteInternoInfo, int usuarioId)
        {
            try
            {
                var fleteInternoBl = new FleteInternoBL();
                var fleteInternoDetalleBl = new FleteInternoDetalleBL();
                var fleteInternoCostoBl = new FleteInternoCostoBL();
                using (var transaction = new TransactionScope())
                {
                    fleteInternoInfo.Activo = EstatusEnum.Inactivo;
                    fleteInternoInfo.UsuarioModificacionId = usuarioId;
                    fleteInternoBl.ActualizarEstado(fleteInternoInfo);
                    fleteInternoInfo.Activo = EstatusEnum.Activo;

                    var listaFleteInternoDetalle = fleteInternoDetalleBl.ObtenerPorFleteInternoId(fleteInternoInfo);
                    //Desactivar detalles
                    if (listaFleteInternoDetalle != null)
                    {
                        foreach (var fleteInternoDetalleInfo in listaFleteInternoDetalle)
                        {
                            fleteInternoDetalleInfo.Activo = EstatusEnum.Inactivo;
                            fleteInternoDetalleInfo.UsuarioModificacionId = usuarioId;
                            fleteInternoDetalleBl.Actualizar(fleteInternoDetalleInfo);
                            fleteInternoDetalleInfo.Activo = EstatusEnum.Activo;

                            var listaFleteInternoCosto =
                                fleteInternoCostoBl.ObtenerPorFleteInternoDetalleId(fleteInternoDetalleInfo);
                            foreach (var fleteInternoCosto in listaFleteInternoCosto)
                            {
                                fleteInternoCosto.Activo = EstatusEnum.Inactivo;
                                fleteInternoCosto.UsuarioModificacionId = usuarioId;
                            }
                            fleteInternoCostoBl.Actualizar(listaFleteInternoCosto);
                        }
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
        }
    }
}
