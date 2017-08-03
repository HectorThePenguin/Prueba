using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    internal class TraspasoGanadoCorralesBL
    {

        internal bool GuardarTraspasoGanadoCorrales(List<AnimalInfo> aretesTotal, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            try
            {
                //using (var transaction = new TransactionScope())
                //{
                Logger.Info();
                //foreach (var animal in aretesTotal)
                //{
                GuardarMovimientoTraspasoGanadoCorrales(aretesTotal, corralInfoDestino, usuarioInfo);
                //}
                //transaction.Complete();
                return true;
                //}
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        //Metodo que guarda el movimiento
        private void GuardarMovimientoTraspasoGanadoCorrales(List<AnimalInfo> aretesTotal, CorralInfo corralInfo, UsuarioInfo usuarioInfo)
        {
            //var animalInfo = new AnimalInfo();
            int loteOrigenID = 0;
            int loteDestinoID = 0;
            var loteBl = new LoteBL();
            var animalBL = new AnimalBL();
            var animalMovimientoBL = new AnimalMovimientoBL();
            var tipoProcesoBl = new TipoProcesoBL();
            var fechaBL = new FechaBL();
            var loteProyeccionBL = new LoteProyeccionBL();

            List<AnimalMovimientoInfo> ultimosMovimientos = animalBL.ObtenerUltimoMovimientoAnimalXML(aretesTotal,
                                                                                                      usuarioInfo.
                                                                                                          Organizacion.
                                                                                                          OrganizacionID);
            if (ultimosMovimientos == null)
            {
                ultimosMovimientos = new List<AnimalMovimientoInfo>();
            }

            var resultadoLoteBl = loteBl.ObtenerLotesActivos(usuarioInfo.Organizacion.OrganizacionID,
                                                                         Convert.ToInt32(corralInfo.CorralID));

            var tipoProcesoResult =
                tipoProcesoBl.ObtenerPorOrganizacion(usuarioInfo.Organizacion.OrganizacionID);

            var fechaServidor = fechaBL.ObtenerFechaActual();

            IList<LoteInfo> lotesCorrales = loteBl.ObtenerPorOrganizacionEstatus(
                usuarioInfo.Organizacion.OrganizacionID, EstatusEnum.Activo);

            if (lotesCorrales == null)
            {
                lotesCorrales = new List<LoteInfo>();
            }

            IList<LoteProyeccionInfo> lotesProyeccion =
                                loteProyeccionBL.ObtenerPorLoteXML(usuarioInfo.Organizacion.OrganizacionID, lotesCorrales);


            var resultadoLote = -1;

            using (var transaction = new TransactionScope())
            {
                if (resultadoLoteBl == null)
                {

                    var loteCrear = new LoteBL();
                    var loteInfoLote = new LoteInfo
                    {
                        Activo = EstatusEnum.Activo,
                        Cabezas = Convert.ToInt32(0),
                        CabezasInicio = Convert.ToInt32(0),
                        CorralID = corralInfo.CorralID,
                        DisponibilidadManual = false,
                        OrganizacionID = usuarioInfo.Organizacion.OrganizacionID,
                        TipoCorralID = corralInfo.TipoCorral.TipoCorralID,
                        TipoProcesoID = tipoProcesoResult,
                        UsuarioCreacionID = usuarioInfo.UsuarioID
                    };
                    resultadoLote = loteCrear.GuardaLote(loteInfoLote);
                    if ((corralInfo.TipoCorral != null
                         && corralInfo.TipoCorral.TipoCorralID != TipoCorral.Enfermeria.GetHashCode())
                        || (corralInfo.TipoCorralId > 0
                            && corralInfo.TipoCorralId != TipoCorral.Enfermeria.GetHashCode()))
                    {
                        loteBl.ActualizaFechaCierre(resultadoLote, usuarioInfo.UsuarioID);
                    }
                }

                foreach (var animalInfo in aretesTotal)
                {
                    var animalMovimientoInfo = new AnimalMovimientoInfo();

                    animalInfo.OrganizacionIDEntrada = usuarioInfo.Organizacion.OrganizacionID;
                    //Obtener ultimo movimiento del animal
                    var ultimoMovimientoInfo =
                        ultimosMovimientos.FirstOrDefault(ani => ani.AnimalID == animalInfo.AnimalID);
                    if (ultimoMovimientoInfo != null)
                    {
                        animalMovimientoInfo.OrganizacionID = usuarioInfo.Organizacion.OrganizacionID;
                        animalMovimientoInfo.AnimalID = animalInfo.AnimalID;

                        animalMovimientoInfo.CorralID = corralInfo.CorralID;
                        animalMovimientoInfo.LoteID = resultadoLoteBl != null ? resultadoLoteBl.LoteID : resultadoLote;
                        DateTime diaHoy = fechaServidor.FechaActual.Date;
                        animalMovimientoInfo.FechaMovimiento = diaHoy.Date;
                        animalMovimientoInfo.Peso = ultimoMovimientoInfo.Peso;
                        animalMovimientoInfo.Temperatura = ultimoMovimientoInfo.Temperatura;
                        animalMovimientoInfo.TipoMovimientoID = (int)TipoMovimiento.TraspasoDeGanado;
                        animalMovimientoInfo.TrampaID = ultimoMovimientoInfo.TrampaID;
                        animalMovimientoInfo.OperadorID = ultimoMovimientoInfo.OperadorID;
                        animalMovimientoInfo.Observaciones = String.Empty;
                        animalMovimientoInfo.Activo = EstatusEnum.Activo;
                        animalMovimientoInfo.UsuarioCreacionID = usuarioInfo.UsuarioID;

                        //Se manda a guardar el registro en base de datos
                        animalMovimientoInfo = animalMovimientoBL.GuardarAnimalMovimiento(animalMovimientoInfo);
                        if (animalMovimientoInfo == null || animalMovimientoInfo.AnimalMovimientoID <= 0)
                            return;

                        var resultadoLoteOrigen = loteBl.ObtenerPorID(ultimoMovimientoInfo.LoteID);
                        var resultadoLoteDestino = loteBl.ObtenerPorID(animalMovimientoInfo.LoteID);

                        if (resultadoLoteDestino != null)
                        {
                            resultadoLoteDestino.UsuarioModificacionID = usuarioInfo.UsuarioID;
                        }

                        //Incrementar y Decrementar cabezas
                        if (resultadoLoteOrigen != null && resultadoLoteDestino != null)
                        {
                            loteOrigenID = resultadoLoteOrigen.LoteID;
                            loteDestinoID = resultadoLoteDestino.LoteID;

                            resultadoLoteDestino.Cabezas = resultadoLoteDestino.Cabezas + 1;
                            resultadoLoteDestino.CabezasInicio = resultadoLoteDestino.CabezasInicio + 1;
                            resultadoLoteOrigen.Cabezas = resultadoLoteOrigen.Cabezas - 1;

                            //Verificar si el Lote tiene cabezas en 0 para inactivarlo
                            resultadoLoteOrigen = loteBl.ObtenerPorID(ultimoMovimientoInfo.LoteID);
                            if (resultadoLoteOrigen.Cabezas != 0)
                            {
                                continue;
                            }


                        }
                    }
                }

                var loteOrigenInfo = loteBl.ObtenerPorID(loteOrigenID);
                var loteDestinoInfo = loteBl.ObtenerPorID(loteDestinoID);

                if (lotesProyeccion != null && lotesProyeccion.Any())
                {
                    LoteProyeccionInfo proyeccionOrigen =
                        lotesProyeccion.FirstOrDefault(lote => lote.LoteID == loteOrigenInfo.LoteID);
                    LoteProyeccionInfo proyeccionDestino =
                        lotesProyeccion.FirstOrDefault(lote => lote.LoteID == loteDestinoInfo.LoteID);
                    if (proyeccionOrigen != null && proyeccionDestino == null)
                    {
                        proyeccionOrigen.LoteProyeccionID = 0;
                        int diasEngordaOrigen = proyeccionOrigen.DiasEngorda;
                        DateTime fechaInicioLote = loteOrigenInfo.FechaInicio;
                        int diasEngordaReales =
                            new TimeSpan(DateTime.Now.Ticks - fechaInicioLote.Ticks).Days;

                        if ((diasEngordaOrigen - diasEngordaReales) <= 0)
                        {
                            proyeccionOrigen.DiasEngorda = 0;
                        }
                        else
                        {
                            proyeccionOrigen.DiasEngorda = diasEngordaOrigen - diasEngordaReales;
                        }

                        proyeccionOrigen.LoteID = loteDestinoInfo.LoteID;
                        proyeccionOrigen.UsuarioCreacionID = usuarioInfo.UsuarioID;
                        loteProyeccionBL.Guardar(proyeccionOrigen);

                        var loteDAL = new LoteDAL();
                        var filtroDisponibilidad = new FiltroDisponilidadInfo
                        {
                            UsuarioId = usuarioInfo.UsuarioID,
                            ListaLoteDisponibilidad =
                                new List<DisponibilidadLoteInfo>
                                                                               {
                                                                                   new DisponibilidadLoteInfo
                                                                                       {
                                                                                           LoteId = loteDestinoInfo.LoteID,
                                                                                           FechaDisponibilidad = loteOrigenInfo.FechaDisponibilidad,
                                                                                           DisponibilidadManual = loteOrigenInfo.DisponibilidadManual
                                                                                       }
                                                                               }
                        };
                        loteDAL.ActualizarLoteDisponibilidad(filtroDisponibilidad);
                    }
                }


                List<AnimalInfo> animalesOrigen =
                                    animalBL.ObtenerAnimalesPorLote(usuarioInfo.Organizacion.OrganizacionID,
                                                                    loteOrigenID);
                if (animalesOrigen != null && animalesOrigen.Any())
                {
                    loteOrigenInfo.Cabezas = animalesOrigen.Count;
                }

                List<AnimalInfo> animalesDestino =
                                    animalBL.ObtenerAnimalesPorLote(usuarioInfo.Organizacion.OrganizacionID,
                                                                    loteDestinoID);
                if (animalesDestino != null && animalesDestino.Any())
                {
                    loteDestinoInfo.Cabezas = animalesDestino.Count;
                }
                //Se actualizan las cabezas que tiene el lote
                var filtro = new FiltroActualizarCabezasLote
                                 {
                                     CabezasProcesadas = aretesTotal.Count,
                                     LoteIDDestino = loteDestinoInfo.LoteID,
                                     LoteIDOrigen = loteOrigenInfo.LoteID,
                                     UsuarioModificacionID = usuarioInfo.UsuarioID
                                 };

                loteBl.ActualizarCabezasProcesadas(filtro);

                transaction.Complete();
            }
        }

        /// <summary>
        /// Guarda el cambio de un corral a otro en los corrales de recepcion
        /// </summary>
        /// <param name="corralInfoOrigen"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuarioInfo"></param>
        /// <returns></returns>
        internal bool GuardarTraspasoGanadoCorralesRecepcion(CorralInfo corralInfoOrigen, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    //Actualiza el corral al lote
                    var loteBl = new LotePL();
                    var lote = loteBl.DeteccionObtenerPorCorral(corralInfoOrigen.OrganizacionId, corralInfoOrigen.CorralID);
                    loteBl.ActualizarCorral(lote, corralInfoDestino, usuarioInfo);

                    //Actualiza el corral a la entrada del lote
                    var entradaGanadoBl = new EntradaGanadoBL();
                    entradaGanadoBl.ActualizarCorral(lote, corralInfoDestino, usuarioInfo);
                    transaction.Complete();
                    return true;
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
    }
}
