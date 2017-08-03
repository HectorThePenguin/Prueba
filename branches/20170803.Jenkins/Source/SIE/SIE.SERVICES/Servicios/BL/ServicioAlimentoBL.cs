using System;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class ServicioAlimentoBL
    {
        /// <summary>
        /// Obtiene el Servicio de Alimento por Organizacion y CorralID
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ServicioAlimentoInfo ObtenerPorCorralID(int organizacionID, int corralID)
        {
            ServicioAlimentoInfo servicioAlimentoInfo;
            try
            {
                Logger.Info();
                var servicioAlimentoDAL = new ServicioAlimentoDAL();
                servicioAlimentoInfo = servicioAlimentoDAL.ObtenerPorCorralID(organizacionID, corralID);
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
            return servicioAlimentoInfo;
        }

        /// <summary>
        ///     Metodo que guarda ServicioAlimento
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(IList<ServicioAlimentoInfo> info)
        {
            try
            {
                Logger.Info();
                var repartoBL = new RepartoBL();
                var listaRepartosGuardar = new List<CambiosReporteInfo>();
                var tipoServicio = TipoServicioEnum.Matutino;

                int organizacionID = info.Select(rep => rep.OrganizacionID).First();
                var organizacionInfo = new OrganizacionInfo
                                           {
                                               OrganizacionID = organizacionID
                                           };
                DateTime fechaHoy = DateTime.Now.Date;

                List<RepartoDetalleInfo> repartosDelDia =
                    repartoBL.ObtenerRepartoDetallePorOrganizacionID(organizacionInfo, fechaHoy);

                if (repartosDelDia != null)
                {
                    int repartosMatutino =
                                            repartosDelDia.Count(
                                                rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());

                    int repartosServidosMatutino =
                        repartosDelDia.Count(
                            rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode() && rep.Servido);

                    int porcentajeServido = (repartosServidosMatutino / repartosMatutino) * 100;
                    if (porcentajeServido > 30)
                    {
                        tipoServicio = TipoServicioEnum.Vespertino;
                    }

                }


                List<CorralInfo> listaCorrales = (from rep in info
                                                  select new CorralInfo
                                                             {
                                                                 CorralID = rep.CorralID
                                                             }).ToList();
                List<RepartoInfo> listaRepartosHoy = repartoBL.ObtenerRepartosPorFechaCorrales(fechaHoy, organizacionID, listaCorrales);
                List<RepartoInfo> listaRepartosManiana = repartoBL.ObtenerRepartosPorFechaCorrales(fechaHoy.AddDays(1), organizacionID, listaCorrales);

                if (listaRepartosHoy == null)
                {
                    listaRepartosHoy = new List<RepartoInfo>();
                }
                if (listaRepartosManiana == null)
                {
                    listaRepartosManiana = new List<RepartoInfo>();
                }

                foreach (var servicio in info)
                {
                    var repartoActual = new CambiosReporteInfo();
                    RepartoDetalleInfo servicioMatutino;
                    RepartoDetalleInfo servicioVespertino;
                    RepartoInfo repartoHoy =
                        listaRepartosHoy.FirstOrDefault(rep => rep.Corral.CorralID == servicio.CorralID);

                    RepartoInfo repartoManiana =
                        listaRepartosManiana.FirstOrDefault(rep => rep.Corral.CorralID == servicio.CorralID);

                    repartoActual.RepartoID = 0;
                    repartoActual.CantidadProgramada = servicio.KilosProgramados;
                    repartoActual.Observaciones = servicio.Comentarios;
                    repartoActual.EstadoComederoID = EstadoComederoEnum.Normal.GetHashCode();
                    if (repartoHoy != null)
                    {
                        repartoActual.FechaReparto = DateTime.Now.AddDays(1);
                    }
                    else
                    {
                        repartoActual.FechaReparto = DateTime.Now;
                    }
                    repartoActual.FormulaIDProgramada = servicio.FormulaID;
                    repartoActual.OrganizacionID = servicio.OrganizacionID;
                    repartoActual.UsuarioModificacionID = servicio.UsuarioCreacionID;
                    repartoActual.TipoServicioID = tipoServicio.GetHashCode();
                    repartoActual.CorralInfo = new CorralInfo
                                                   {
                                                       CorralID = servicio.CorralID,
                                                       Codigo = servicio.CodigoCorral
                                                   };

                    if (repartoHoy != null)
                    {
                        repartoActual.RepartoID = repartoHoy.RepartoID;
                        servicioMatutino =
                            repartoHoy.DetalleReparto.FirstOrDefault(
                                rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());
                        servicioVespertino =
                            repartoHoy.DetalleReparto.FirstOrDefault(
                                rep => rep.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode());
                        if (servicioMatutino != null)
                        {
                            if (!servicioMatutino.Servido)
                            {
                                repartoActual.RepartoDetalleIdManiana = servicioMatutino.RepartoDetalleID;
                                listaRepartosGuardar.Add(repartoActual);
                                continue;
                            }
                        }
                        if (servicioVespertino != null)
                        {
                            if (!servicioVespertino.Servido)
                            {
                                repartoActual.RepartoDetalleIdTarde = servicioVespertino.RepartoDetalleID;
                                listaRepartosGuardar.Add(repartoActual);
                                continue;
                            }
                        }
                    }
                    if (repartoManiana != null)
                    {
                        repartoActual.RepartoID = repartoManiana.RepartoID;
                        servicioMatutino =
                            repartoManiana.DetalleReparto.FirstOrDefault(
                                rep => rep.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());
                        servicioVespertino =
                            repartoManiana.DetalleReparto.FirstOrDefault(
                                rep => rep.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode());
                        if (servicioMatutino != null)
                        {
                            if (!servicioMatutino.Servido)
                            {
                                repartoActual.RepartoDetalleIdManiana = servicioMatutino.RepartoDetalleID;
                                listaRepartosGuardar.Add(repartoActual);
                                continue;
                            }
                        }
                        if (servicioVespertino != null)
                        {
                            if (!servicioVespertino.Servido)
                            {
                                repartoActual.RepartoDetalleIdTarde = servicioVespertino.RepartoDetalleID;
                                listaRepartosGuardar.Add(repartoActual);
                                continue;
                            }
                        }
                    }

                    listaRepartosGuardar.Add(repartoActual);
                }
                var servicioAlimentoDAL = new ServicioAlimentoDAL();
                using (var transaccion = new TransactionScope())
                {
                    repartoBL.GuardarRepartosServicioCorrales(listaRepartosGuardar);
                    servicioAlimentoDAL.Guardar(info);
                    transaccion.Complete();
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
        /// Obtener informacion diaria de servicioalimento.
        /// </summary>
        /// <returns></returns>
        internal IList<ServicioAlimentoInfo> ObtenerInformacionDiariaAlimento(int organizacionId)
        {
            IList<ServicioAlimentoInfo> servicioAlimentoInfo;
            try
            {
                Logger.Info();
                var servicioAlimentoDAL = new ServicioAlimentoDAL();
                servicioAlimentoInfo = servicioAlimentoDAL.ObtenerInformacionDiariaAlimento(organizacionId);
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
            return servicioAlimentoInfo;
        }

        /// <summary>
        /// Metodo para eliminar el servicio de alimento programado
        /// </summary>
        /// <param name="corralGrid"></param>
        internal void Eliminar(CorralRangoInfo corralGrid)
        {
            try
            {
                Logger.Info();
                var servicioAlimentoDAL = new ServicioAlimentoDAL();
                servicioAlimentoDAL.Eliminar(corralGrid);
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
        /// Metodo para eliminar el servicio de alimento programado
        /// </summary>
        /// <param name="corralesEliminar"></param>
        internal void EliminarXML(List<CorralInfo> corralesEliminar)
        {
            try
            {
                Logger.Info();
                var servicioAlimentoDAL = new ServicioAlimentoDAL();
                servicioAlimentoDAL.EliminarXML(corralesEliminar);
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
