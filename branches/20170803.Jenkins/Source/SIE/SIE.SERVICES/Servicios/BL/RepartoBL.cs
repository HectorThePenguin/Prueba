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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    internal class RepartoBL
    {
        /// <summary>
        /// Obtiene el reparto por lote
        /// </summary>
        /// <param name="lote">Informacion del lote</param>
        /// <param name="fechaReparto">Fecha del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerPorLote(LoteInfo lote, DateTime fechaReparto)
        {
            RepartoInfo info;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                info = repartoDal.ObtenerPorLote(lote, fechaReparto);
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
        /// Obtiene los dias de retiro
        /// </summary>
        /// <param name="lote">Informacion del lote</param>
        /// <returns>Numero de dias de retiro</returns>
        internal int ObtenerDiasRetiro(LoteInfo lote)
        {
            int info;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                info = repartoDal.ObtenerDiasRetiro(lote);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }
        /// <summary>
        /// Obtiene el detalle de la orden de reparto
        /// </summary>
        /// <param name="reparto">Informacion del reparto</param>
        /// <returns>Lista con el detalle del reparto</returns>
        internal IList<RepartoDetalleInfo> ObtenerDetalle(RepartoInfo reparto)
        {
            IList<RepartoDetalleInfo> info;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                info = repartoDal.ObtenerDetalle(reparto);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        /// Obtiene el listado de repartos del operador logueado.
        /// </summary>
        /// <param name="operador">Informacion del operador</param>
        /// <param name="corral">Informacion del corral</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorOperadorId(OperadorInfo operador, CorralInfo corral)
        {
            RepartoInfo info;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                var corralBl = new CorralBL();
                info = repartoDal.ObtenerRepartoPorOperadorId(operador, corral);
                if (info != null)
                {
                    info.Corral = corralBl.ObtenerCorralPorLoteID(info.LoteID, info.OrganizacionID);
                    info.TotalRepartos = repartoDal.ObtenerTotalRepartosPorOperadorId(operador.OperadorID, info.OrganizacionID);
                    info.TotalRepartosLeidos = repartoDal.ObtenerTotalRepartosLeidosPorOperadorId(operador.OperadorID,
                        info.OrganizacionID);
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
        /// Obtiene los tipos de servicios configurados
        /// </summary>
        /// <returns>Lista con los tipos de servicios</returns>
        internal List<TipoServicioInfo> ObtenerTiposDeServicios()
        {
            List<TipoServicioInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerTiposDeServicios();
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }


        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="lote">Lote del cual se consultura el reparto</param>
        /// <returns>Lista de los repartos</returns>
        internal List<RepartoInfo> ObtenerRepartoActual(LoteInfo lote)
        {
            List<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoActual(lote);
                if (resultado != null)
                {
                    foreach (var reparto in resultado)
                    {
                        reparto.DetalleReparto = repartoDal.ObtenerDetalle(reparto);
                    }
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Valida la orden de reparto antes de guardarse
        /// </summary>
        /// <param name="tipoServicioId">Tipo de servicio que se guardara</param>
        /// <param name="organizacionId">Organizacion que genera la orden de reparto</param>
        /// <returns>Resultado de la validacion</returns>
        internal ResultadoValidacion ValidarOrdenReparto(int tipoServicioId, int organizacionId)
        {

            var resultado = new ResultadoValidacion { Resultado = true };
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                var fecha = DateTime.Now;
                if (tipoServicioId == (int)TipoServicioEnum.Matutino)
                {
                    fecha = DateTime.Now.AddDays(1);
                }
                var matutinosServidos = 0;
                var vespertinoServidos = 0;
                var detalleReparto = repartoDal.ObtenerDetalleDelDia(organizacionId, fecha);

                if (detalleReparto != null)
                {
                    matutinosServidos =
                       detalleReparto.Count(
                           tmpDetalleReparto =>
                               tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Matutino &&
                               tmpDetalleReparto.Servido);
                    vespertinoServidos =
                        detalleReparto.Count(
                            tmpDetalleReparto =>
                                tmpDetalleReparto.TipoServicioID == (int)TipoServicioEnum.Vespertino &&
                                tmpDetalleReparto.Servido);
                }

                switch (tipoServicioId)
                {
                    case (int)TipoServicioEnum.Matutino:
                        if (matutinosServidos > 0 || vespertinoServidos > 0)
                        {
                            resultado.Resultado = false;
                        }
                        resultado.TipoResultadoValidacion =
                            TipoResultadoValidacion.AlimentoNoServidoMatutino;
                        resultado.Mensaje =
                            TipoResultadoValidacion.AlimentoNoServidoMatutino.ToString();
                        break;

                    case (int)TipoServicioEnum.Vespertino:
                        if (matutinosServidos == 0 || vespertinoServidos > 0)
                        {
                            resultado.Resultado = false;
                        }
                        resultado.TipoResultadoValidacion =
                            TipoResultadoValidacion.AlimentoNoServidoVespertino;
                        resultado.Mensaje =
                            TipoResultadoValidacion.AlimentoNoServidoVespertino.ToString();
                        break;
                }

                return resultado;

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Genera la orden de reparto
        /// </summary>
        /// <param name="ordenReparto">Informacion del reparto</param>
        /// <returns>Resultado de la operacion</returns>
        internal ResultadoOperacion GenerarOrdenReparto(OrdenRepartoAlimentacionInfo ordenReparto)
        {
            var resultado = new ResultadoOperacion { Resultado = true };
            int corralidMomento = 0;
            var corralesProcesados = 0;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                //var fechaActual = DateTime.Now;

                var avanceReparto = new RepartoAvanceInfo
                {
                    UsuarioID = ordenReparto.UsuarioID,
                    Seccion = MensajesRepartoAvancesEnum.Iniciando.ToString(),
                    PorcentajeSeccion = 0,
                    PorcentajeTotal = 0,
                    TotalCorrales = 0,
                    TotalCorralesProcesados = 0,
                    TotalCorralesProcesadosSeccion = 0,
                    TotalCorralesSeccion = 0,
                    EstatusError = 0
                };
                ReportarAvanceReparto(avanceReparto);

                TipoServicioEnum servicioSolicitado;
                ParametrosEnum parametroSolicitado;
                if (ordenReparto.TipoServicioID == 1)
                {
                    servicioSolicitado = TipoServicioEnum.Matutino;
                    ordenReparto.TipoServicioID = (int)TipoServicioEnum.Matutino;
                    parametroSolicitado = ParametrosEnum.porcentajeMatutino;
                }
                else
                {
                    servicioSolicitado = TipoServicioEnum.Vespertino;
                    ordenReparto.TipoServicioID = (int)TipoServicioEnum.Vespertino;
                    parametroSolicitado = ParametrosEnum.porcentajeMatutino;
                }

                var corralesBl = new CorralBL();
                var corralesTotales = corralesBl.ObtenerCorralesParaReparto(ordenReparto.OrganizacionID);
                var corrales = new ResultadoInfo<CorralInfo>();
                // = corralesBl.ObtenerCorralesParaReparto(ordenReparto.OrganizacionID);

                if (ordenReparto.Seccion == -1)
                {
                    corrales = corralesTotales;
                }
                else
                {
                    corrales.Lista = corralesTotales.Lista.Where(cor => cor.Seccion == ordenReparto.Seccion).ToList();
                }
                var totalCorrales = 0;
                if (corrales != null)
                {
                    totalCorrales = corrales.Lista.Count;
                    corrales.TotalRegistros = corrales.Lista.Count;
                }

                avanceReparto = new RepartoAvanceInfo
                {
                    UsuarioID = ordenReparto.UsuarioID,
                    Seccion = MensajesRepartoAvancesEnum.Iniciando + " " + servicioSolicitado,
                    PorcentajeSeccion = 0,
                    PorcentajeTotal = 0,
                    TotalCorrales = totalCorrales,
                    TotalCorralesProcesados = 0,
                    TotalCorralesProcesadosSeccion = 0,
                    TotalCorralesSeccion = 0,
                    EstatusError = 0
                };
                ReportarAvanceReparto(avanceReparto);

                if (corrales != null && corrales.TotalRegistros > 0)
                {
                    var parametroPorcentaje = LeerConfiguracion(ordenReparto.OrganizacionID, parametroSolicitado);
                    if (string.IsNullOrEmpty(parametroPorcentaje))
                    {
                        resultado.Resultado = false;
                        resultado.Mensaje = MensajesRepartoEnum.msgFaltaPorcentaje.ToString();
                        return resultado;
                    }

                    var porcentaje = int.Parse(parametroPorcentaje);

                    var redondeo = LeerConfiguracion(ordenReparto.OrganizacionID, ParametrosEnum.ajustePorRedondeo);

                    var ajusteRedondeo = int.Parse(redondeo);

                    //SECCION PARA REEMPLAZAR LAS LLAMADAS A BD EN EL FOREACH


                    var loteBl = new LoteBL();
                    IList<LoteInfo> lotesRepartos = loteBl.ObtenerPorOrganizacionLoteXML(ordenReparto.OrganizacionID,
                                                                                         corrales.Lista.ToList());

                    if (lotesRepartos == null)
                    {
                        lotesRepartos = new List<LoteInfo>();
                    }

                    IList<OrdenRepartoAlimentacionInfo> pesosRepartos = ObtenerPesoLlegadaPorLoteXML(lotesRepartos, ordenReparto.OrganizacionID);

                    //var fechaOrden = DateTime.Now;
                    var fechaOrden = ordenReparto.FechaReparto;
                    //if (ordenReparto.TipoServicioID == (int)TipoServicioEnum.Matutino)
                    //{
                    //    fechaOrden = fechaOrden.AddDays(1);
                    //}
                    if (pesosRepartos == null)
                    {
                        pesosRepartos = new List<OrdenRepartoAlimentacionInfo>();
                    }


                    IList<RepartoInfo> ordenesReparto = ObtenerRepartoPorFechaXML(fechaOrden, lotesRepartos, ordenReparto.OrganizacionID);

                    if (ordenesReparto == null)
                    {
                        ordenesReparto = new List<RepartoInfo>();
                    }

                    var corralBL = new CorralBL();

                    IList<DiasEngordaLoteModel> diasEngordaRepartos =
                        corralBL.ObtenerDiasEngordaPorLoteXML(lotesRepartos);

                    if (diasEngordaRepartos == null)
                    {
                        diasEngordaRepartos = new List<DiasEngordaLoteModel>();
                    }

                    var loteProyeccionBl = new LoteProyeccionBL();

                    IList<LoteProyeccionInfo> loteProyeccionRepartos =
                        loteProyeccionBl.ObtenerPorLoteXML(ordenReparto.OrganizacionID, lotesRepartos);

                    if (loteProyeccionRepartos == null)
                    {
                        loteProyeccionRepartos = new List<LoteProyeccionInfo>();
                    }

                    //DateTime fechaRepartos = fechaOrden;
                    //if (ordenReparto.TipoServicioID == (int)TipoServicioEnum.Matutino)
                    //{
                    //    fechaRepartos = fechaOrden.AddDays(-1);
                    //}

                    DateTime fechaRepartos = fechaOrden;
                    if (ordenReparto.TipoServicioID == (int)TipoServicioEnum.Matutino)
                    {
                        fechaRepartos = fechaOrden.AddDays(-1);
                    }

                    IList<ConsumoTotalCorralModel> consumosTotalesRepartos =
                        ObtenerConsumoTotalDiaXML(ordenReparto.OrganizacionID, corrales.Lista.ToList(), fechaRepartos);

                    if (consumosTotalesRepartos == null)
                    {
                        consumosTotalesRepartos = new List<ConsumoTotalCorralModel>();
                    }

                   

                    IList<LectorRegistroInfo> lectorRegistroRepartos = ObtenerLectorRegistroXML(lotesRepartos, fechaRepartos,
                                                                                               ordenReparto.
                                                                                                   OrganizacionID);

                    if (lectorRegistroRepartos == null)
                    {
                        lectorRegistroRepartos = new List<LectorRegistroInfo>();
                    }


                    foreach (var corral in corrales.Lista)
                    {
                        corralidMomento = corral.CorralID;
                        //reportando el avance
                        corralesProcesados++;
                        var porcentajeProceso = (int)(((decimal)corralesProcesados / (decimal)totalCorrales) * 100);
                        avanceReparto = new RepartoAvanceInfo
                        {
                            UsuarioID = ordenReparto.UsuarioID,
                            Seccion = string.Format(" {0} {1}", MensajesRepartoAvancesEnum.Sección, corral.Seccion),
                            PorcentajeSeccion = 0,
                            PorcentajeTotal = porcentajeProceso,
                            TotalCorrales = totalCorrales,
                            TotalCorralesProcesados = corralesProcesados,
                            TotalCorralesProcesadosSeccion = 0,
                            TotalCorralesSeccion = 0,
                            EstatusError = 0
                        };
                        ReportarAvanceReparto(avanceReparto);

                        var ordenRepartoAlimentacion = new OrdenRepartoAlimentacionInfo();
                        ordenReparto.Reparto = new RepartoInfo();
                        ordenRepartoAlimentacion.Corral = corral;
                        LoteInfo lote = lotesRepartos.FirstOrDefault(lo => lo.Corral.CorralID == corral.CorralID);
                        ordenRepartoAlimentacion.Lote = lote;
                        //var loteBl = new LoteBL();
                        //ordenRepartoAlimentacion.Lote =
                        //    loteBl.ObtenerPorCorralID(new LoteInfo() { OrganizacionID = corral.Organizacion.OrganizacionID, CorralID = corral.CorralID });
                        if (ordenRepartoAlimentacion.Lote != null)
                        {
                            var ordenTemporal =
                                pesosRepartos.FirstOrDefault(
                                    pes => pes.Lote.LoteID == ordenRepartoAlimentacion.Lote.LoteID);

                            if (ordenTemporal == null)
                            {
                                ordenTemporal = new OrdenRepartoAlimentacionInfo();
                            }

                            //var ordenTemporal = ordenRepartoAlimentacion.Lote != null ?
                            //                        ObtenerPesoLlegadaPorLote(ordenRepartoAlimentacion.Lote) :
                            //                        new OrdenRepartoAlimentacionInfo();

                            if (ordenTemporal != null)
                            {
                                //var fechaOrden = DateTime.Now;
                                //if (ordenReparto.TipoServicioID == (int)TipoServicioEnum.Matutino)
                                //{
                                //    fechaOrden = fechaOrden.AddDays(1);
                                //}
                                RepartoInfo ordenActual = null;
                                if (ordenRepartoAlimentacion.Lote != null)
                                {
                                    ordenActual =
                                        ordenesReparto.FirstOrDefault(
                                            or => or.LoteID == ordenRepartoAlimentacion.Lote.LoteID);
                                    //ordenActual = ObtenerRepartoPorFecha(fechaOrden, ordenRepartoAlimentacion.Lote);
                                }
                                else
                                {
                                    ordenActual = ObtenerRepartoPorFechaCorral(fechaOrden, corral);
                                }

                                if (ordenActual != null)
                                {
                                    ordenRepartoAlimentacion.Reparto = new RepartoInfo
                                    {
                                        RepartoID = ordenActual.RepartoID,
                                        DetalleReparto = new List<RepartoDetalleInfo>()
                                    };
                                    if (ordenActual.DetalleReparto != null)
                                    {
                                        ordenRepartoAlimentacion.Reparto.DetalleReparto = ordenActual.DetalleReparto;
                                        /* Si ya trae el reparto ajustado que continue con el siguiente corral  */

                                        var existeServicioCreado = ordenRepartoAlimentacion.Reparto.DetalleReparto
                                            .Count(detalle => detalle.TipoServicioID == (int)servicioSolicitado);

                                        if (existeServicioCreado > 0)
                                        {
                                            var servicio = ordenRepartoAlimentacion.Reparto.DetalleReparto
                                            .Where(detalle => detalle.TipoServicioID == (int)servicioSolicitado);
                                            if (servicio.FirstOrDefault().Ajuste)
                                            {
                                                continue;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    ordenRepartoAlimentacion.Reparto = new RepartoInfo { RepartoID = 0 };
                                }

                                ordenRepartoAlimentacion.OrganizacionID = ordenReparto.OrganizacionID;
                                ordenRepartoAlimentacion.PesoLlegada = ordenTemporal.PesoLlegada;
                                ordenRepartoAlimentacion.TotalAnimalesPeso = ordenTemporal.TotalAnimalesPeso;
                                ordenRepartoAlimentacion.PesoActual = ordenTemporal.PesoActual;
                                ordenRepartoAlimentacion.Reparto.Fecha = fechaOrden;

                                if (ordenRepartoAlimentacion.Lote != null)
                                {
                                    /*var diferenciaFechasDiasEngorda = fechaActual -
                                                                  ordenRepartoAlimentacion.Lote.FechaInicio;
                                     ordenRepartoAlimentacion.Reparto.DiasEngorda = diferenciaFechasDiasEngorda.Days;
                                     */
                                    ordenRepartoAlimentacion.Reparto.DiasEngorda = 1;
                                    if (corral.TipoCorral.GrupoCorralID != (int)GrupoCorralEnum.Recepcion)
                                    {
                                        DiasEngordaLoteModel diasEngordaLote =
                                            diasEngordaRepartos.FirstOrDefault(
                                                dia => dia.LoteID == ordenRepartoAlimentacion.Lote.LoteID);
                                        if (diasEngordaLote != null)
                                        {
                                            ordenRepartoAlimentacion.Reparto.DiasEngorda = diasEngordaLote.DiasEngorda;
                                        }
                                        //ordenRepartoAlimentacion.Reparto.DiasEngorda =
                                        //corralesBl.ObtenerDiasEngordaPorLote(ordenRepartoAlimentacion.Lote);
                                    }

                                    ordenRepartoAlimentacion.Proyeccion =
                                        loteProyeccionRepartos.FirstOrDefault(
                                            pro => pro.LoteID == ordenRepartoAlimentacion.Lote.LoteID);
                                    //var loteProyeccionBl = new LoteProyeccionBL();

                                    //ordenRepartoAlimentacion.Proyeccion =
                                    //    loteProyeccionBl.ObtenerPorLote(ordenRepartoAlimentacion.Lote);

                                    if (ordenRepartoAlimentacion.Proyeccion != null)
                                    {
                                        ordenRepartoAlimentacion.GananciaCorral =
                                            ordenRepartoAlimentacion.Reparto.DiasEngorda *
                                            ordenRepartoAlimentacion.Proyeccion
                                                .GananciaDiaria;


                                        ordenRepartoAlimentacion.Reparto.PesoProyectado = ordenRepartoAlimentacion.PesoActual == 0 ? 0 :
                                            ordenRepartoAlimentacion.PesoActual /
                                            ordenRepartoAlimentacion.Lote.Cabezas +
                                            (int)ordenRepartoAlimentacion.GananciaCorral;

                                        var loteReimplanteBl = new LoteReimplanteBL();
                                        //TODO revisar con Jesus porque toma el primero reimplante
                                        ordenRepartoAlimentacion.LoteReimplante =
                                            loteReimplanteBl.ObtenerPorLote(ordenRepartoAlimentacion.Lote);

                                        if (ordenRepartoAlimentacion.LoteReimplante != null)
                                        {
                                            ordenRepartoAlimentacion.Reparto.PesoRepeso = ordenRepartoAlimentacion.LoteReimplante.PesoReal;
                                        }
                                    }

                                    if (ordenRepartoAlimentacion.Lote.Cabezas > 0 && ordenRepartoAlimentacion.PesoLlegada > 0)
                                    {
                                        ordenRepartoAlimentacion.Reparto.PesoInicio = ordenRepartoAlimentacion.PesoLlegada /
                                                                                      ordenRepartoAlimentacion.Lote.Cabezas;
                                    }
                                    else
                                    {
                                        ordenRepartoAlimentacion.Reparto.PesoInicio = 0;
                                    }
                                }
                                else
                                {
                                    ordenRepartoAlimentacion.Reparto.DiasEngorda = 1;
                                    ordenRepartoAlimentacion.Reparto.PesoProyectado = 0;
                                    ordenRepartoAlimentacion.Reparto.PesoInicio = 0;
                                }



                            }



                            var cantidadProgramada = 0;
                            var idFormulaProgramada = 0;
                            if (ordenReparto.TipoServicioID == (int)TipoServicioEnum.Matutino)
                            {
                                ConsumoTotalCorralModel consumoTotal =
                                    consumosTotalesRepartos.FirstOrDefault(con => con.CorralID == corral.CorralID);

                                if (consumoTotal == null)
                                {
                                    consumoTotal = new ConsumoTotalCorralModel();
                                }

                                //var consumoTotalDia = ObtenerTotalConsumoDia(ordenReparto.OrganizacionID,
                                //    corral);

                                cantidadProgramada = (consumoTotal.TotalConsumo * porcentaje) / 100;
                                cantidadProgramada = RedondearKilos(cantidadProgramada, ajusteRedondeo);
                                if (ordenRepartoAlimentacion.Lote != null)
                                {
                                    RepartoInfo reparto =
                                        ordenesReparto.FirstOrDefault(
                                            rep => rep.LoteID == ordenRepartoAlimentacion.Lote.LoteID);

                                    if (reparto != null)
                                    {
                                        RepartoDetalleInfo detalle =
                                            reparto.DetalleReparto.FirstOrDefault(
                                                det => det.TipoServicioID == servicioSolicitado.GetHashCode());

                                        if (detalle != null)
                                        {
                                            idFormulaProgramada = detalle.FormulaIDProgramada;
                                        }
                                    }
                                    else
                                    {
                                        idFormulaProgramada = ObtenerFormulaProgramada(ordenRepartoAlimentacion.Lote,
                                                servicioSolicitado, fechaRepartos);
                                    }

                                    //idFormulaProgramada = ObtenerFormulaProgramada(ordenRepartoAlimentacion.Lote,
                                    //        servicioSolicitado);
                                }

                            }
                            else
                            {
                                if (ordenRepartoAlimentacion.Lote != null)
                                {
                                    LectorRegistroInfo lector =
                                        lectorRegistroRepartos.FirstOrDefault(
                                            lect => lect.LoteID == ordenRepartoAlimentacion.Lote.LoteID);

                                    //var lector = ObtenerLectorRegistro(ordenRepartoAlimentacion.Lote, fechaActual);

                                    var cantidadServidaMatutino = 0;
                                    if (lector != null)
                                    {

                                        //lector.DetalleLector = ObtenerDetalleLectorRegistro(lector);

                                        if (ordenRepartoAlimentacion.Reparto.DetalleReparto != null)
                                        {
                                            foreach (
                                                var detalleReparto in
                                                    ordenRepartoAlimentacion.Reparto.DetalleReparto.Where(
                                                        detalleReparto =>
                                                            detalleReparto.TipoServicioID ==
                                                            (int)TipoServicioEnum.Matutino))
                                            {
                                                cantidadServidaMatutino = detalleReparto.CantidadServida;
                                            }

                                            foreach (var detalleLector in lector.DetalleLector.Where(detalleLector => detalleLector.TipoServicioID == (int)TipoServicioEnum.Vespertino))
                                            {
                                                idFormulaProgramada = detalleLector.FormulaIDProgramada;
                                            }
                                        }

                                        cantidadProgramada = (int)(lector.CantidadPedido - cantidadServidaMatutino);
                                        cantidadProgramada = RedondearKilos(cantidadProgramada, ajusteRedondeo);
                                    }
                                }
                            }

                            var repartoDetalle = new RepartoDetalleInfo();
                            ordenRepartoAlimentacion.OrganizacionID = ordenReparto.OrganizacionID;
                            ordenRepartoAlimentacion.UsuarioID = ordenReparto.UsuarioID;

                            repartoDetalle.RepartoDetalleID = 0;
                            repartoDetalle.TipoServicioID = (int)servicioSolicitado;
                            repartoDetalle.Servido = false;
                            if (ordenRepartoAlimentacion.Reparto.DetalleReparto != null)
                            {
                                foreach (
                                    var detalle in
                                        ordenRepartoAlimentacion.Reparto.DetalleReparto.Where(
                                            detalle => detalle.TipoServicioID == (int)servicioSolicitado))
                                {
                                    repartoDetalle = detalle;
                                }
                            }
                            if (!repartoDetalle.Servido)
                            {
                                repartoDetalle.CantidadProgramada = cantidadProgramada;
                                repartoDetalle.FormulaIDProgramada = idFormulaProgramada;

                                repartoDetalle.Cabezas = ordenRepartoAlimentacion.Lote != null ? ordenRepartoAlimentacion.Lote.Cabezas : 1;
                                if (ordenRepartoAlimentacion.Lote != null)
                                {
                                    LectorRegistroInfo lector =
                                        lectorRegistroRepartos.FirstOrDefault(
                                            lect => lect.LoteID == ordenRepartoAlimentacion.Lote.LoteID);

                                    repartoDetalle.LectorRegistro = lector;
                                    //repartoDetalle.LectorRegistro = ObtenerLectorRegistro(ordenRepartoAlimentacion.Lote, fechaActual);

                                    if (repartoDetalle.LectorRegistro == null)
                                    {
                                        repartoDetalle.LectorRegistro = new LectorRegistroInfo();
                                    }

                                    repartoDetalle.EstadoComederoID = repartoDetalle.LectorRegistro.EstadoComederoID;
                                }
                                else
                                {
                                    repartoDetalle.EstadoComederoID = (int)EstadoComederoEnum.Normal;
                                }

                                repartoDetalle.Observaciones = string.Empty;
                            }

                            ordenRepartoAlimentacion.DetalleOrdenReparto = repartoDetalle;
                            if (repartoDetalle.FormulaIDProgramada > 0)
                            {
                                using (var transaccion = new TransactionScope())
                                {
                                    repartoDal.GenerarOrdenReparto(ordenRepartoAlimentacion);
                                    transaccion.Complete();
                                }
                            }
                            else
                            {
                                resultado.Resultado = false;
                                resultado.Mensaje = MensajesRepartoEnum.msgCorralesIncompletos.ToString();
                            }
                        }
                        else
                        {
                            resultado.Resultado = false;
                            resultado.Mensaje = MensajesRepartoEnum.msgCorralesIncompletos.ToString();
                        }
                    }
                    if (ordenReparto.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode())
                    {
                        var detallesModificar = new List<RepartoDetalleInfo>();
                        var fechaReparto = ordenReparto.FechaReparto;
                        List<RepartoInfo> repartosAjustar = repartoDal.ObtenerRepartosAjustados(fechaReparto, ordenReparto.OrganizacionID);

                        if (repartosAjustar != null && repartosAjustar.Any())
                        {
                            foreach (var repartoInfo in repartosAjustar)
                            {
                                if(repartoInfo.DetalleReparto == null)
                                {
                                    continue;
                                }
                                RepartoDetalleInfo repartoMatutino =
                                    repartoInfo.DetalleReparto.FirstOrDefault(
                                        det => det.TipoServicioID == TipoServicioEnum.Matutino.GetHashCode());

                                RepartoDetalleInfo repartoVespertino =
                                    repartoInfo.DetalleReparto.FirstOrDefault(
                                        det => det.TipoServicioID == TipoServicioEnum.Vespertino.GetHashCode());

                                if (repartoMatutino == null || repartoVespertino == null)
                                {
                                    continue;
                                }
                                if (!repartoMatutino.Servido || repartoMatutino.CantidadServida == 0 || repartoMatutino.CantidadProgramada == 0)
                                {
                                    continue;
                                }
                                int ajusteTarde = repartoMatutino.CantidadProgramada - repartoMatutino.CantidadServida;

                                repartoVespertino.CantidadProgramada = repartoVespertino.CantidadProgramada + ajusteTarde;
                                repartoVespertino.UsuarioCreacionID = ordenReparto.UsuarioID;
                                detallesModificar.Add(repartoVespertino);
                            }
                            if (detallesModificar.Any())
                            {
                                using (var transaccion = new TransactionScope())
                                {
                                    repartoDal.ActualizarCantidadProgramadaReparto(detallesModificar);
                                    transaccion.Complete();
                                }
                            }
                        }
                    }
                }
                else
                {
                    resultado.RegistrosAfectados = 0;
                    resultado.Mensaje = MensajesRepartoEnum.msgSinCorrales.ToString();
                    resultado.Resultado = false;
                }

            }
            catch (ExcepcionGenerica ex)
            {
                var corral = corralidMomento;
                Logger.Error(ex);
                var avanceReparto = new RepartoAvanceInfo
                {
                    UsuarioID = ordenReparto.UsuarioID,
                    Seccion = MensajesRepartoAvancesEnum.Error.ToString(),
                    PorcentajeSeccion = 0,
                    PorcentajeTotal = 0,
                    TotalCorrales = 0,
                    TotalCorralesProcesados = corralesProcesados,
                    TotalCorralesProcesadosSeccion = 0,
                    TotalCorralesSeccion = 0,
                    EstatusError = 1
                };
                ReportarAvanceReparto(avanceReparto);
                throw;
            }
            catch (Exception ex)
            {
                var corral = corralidMomento;
                var avanceReparto = new RepartoAvanceInfo
                {
                    UsuarioID = ordenReparto.UsuarioID,
                    Seccion = MensajesRepartoAvancesEnum.Error.ToString(),
                    PorcentajeSeccion = 0,
                    PorcentajeTotal = 0,
                    TotalCorrales = 0,
                    TotalCorralesProcesados = 0,
                    TotalCorralesProcesadosSeccion = 0,
                    TotalCorralesSeccion = 0,
                    EstatusError = 1
                };
                ReportarAvanceReparto(avanceReparto);
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el total del consumo del dia
        /// </summary>
        /// <param name="organizacionId">Identificacion de la organizacion</param>
        /// <param name="corral">Informacion del lote</param>
        /// <returns>Numero de dias</returns>
        internal int ObtenerTotalConsumoDia(int organizacionId, CorralInfo corral)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerConsumoTotalDia(organizacionId, corral);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Lectura de configuracion
        /// </summary>
        /// <param name="organizacionId">Identificador del la organizacion</param>
        /// <param name="valorRequerido">Valor requerido</param>
        /// <returns></returns>
        internal string LeerConfiguracion(int organizacionId, ParametrosEnum valorRequerido)
        {
            try
            {
                var parametrosBl = new ConfiguracionParametrosBL();
                var parametroSolicitado = new ConfiguracionParametrosInfo
                {
                    TipoParametro = (int)TiposParametrosEnum.OrdenRepartoAlimentacion,
                    OrganizacionID = organizacionId
                };
                var parametros = parametrosBl.ObtenerPorOrganizacionTipoParametro(parametroSolicitado);
                foreach (var parametro in parametros)
                {
                    ParametrosEnum enumTemporal;
                    var res = Enum.TryParse(parametro.Clave, out enumTemporal);

                    if (!res) continue;
                    if (valorRequerido == enumTemporal)
                    {
                        return parametro.Valor;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return "";
        }
        /// <summary>
        /// Obtiene el peso de llegada por lote
        /// </summary>
        /// <param name="lote">Informacion del lote</param>
        /// <returns>Orden de reparto de alimentacion</returns>
        internal OrdenRepartoAlimentacionInfo ObtenerPesoLlegadaPorLote(LoteInfo lote)
        {
            OrdenRepartoAlimentacionInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerPesoLlegada(lote);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene la formula programada del dia anterior
        /// </summary>
        /// <param name="lote">Lote del cual se obtendra la formula</param>
        /// <param name="proceso">Tipo de proceso</param>
        /// <param name="fechaRepartos">fecha del reparto</param>
        /// <returns>Identificador de la formula</returns>
        internal int ObtenerFormulaProgramada(LoteInfo lote, TipoServicioEnum proceso, DateTime fechaRepartos)
        {
            var resultado = 0;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                //var fecha = DateTime.Now;
                //if (proceso == TipoServicioEnum.Matutino)
                //{
                //    fecha = fecha.AddDays(-1);
                //}
                //if(proceso == TipoServicioEnum.Matutino)
                //{
                //    fecha = fecha.AddDays(-1);
                //}

                var reparto = repartoDal.ObtenerPorLote(lote, fechaRepartos);
                if (reparto != null)
                {
                    var detalleReparto = repartoDal.ObtenerDetalle(reparto);

                    if (detalleReparto != null)
                    {
                        foreach (var detalle in detalleReparto.Where(detalle => detalle.TipoServicioID == (int)proceso))
                        {
                            resultado = detalle.FormulaIDProgramada;
                        }
                    }
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene el lector registro de un lote y una fecha especifica
        /// </summary>
        /// <param name="lote">Lote del cual se quiere obtner el lector</param>
        /// <param name="fecha">Fecha del lector</param>
        /// <returns>Informacion del lector registro</returns>
        internal LectorRegistroInfo ObtenerLectorRegistro(LoteInfo lote, DateTime fecha)
        {
            LectorRegistroInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new LectorRegistroDAL();
                resultado = repartoDal.ObtenerLectorRegistro(lote, fecha);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene el detalle de lector
        /// </summary>
        /// <param name="lector">Informacion del lector</param>
        /// <returns>Lista con el detalle del lector</returns>
        internal List<LectorRegistroDetalleInfo> ObtenerDetalleLectorRegistro(LectorRegistroInfo lector)
        {
            List<LectorRegistroDetalleInfo> resultado;
            try
            {
                Logger.Info();
                var lectorDal = new LectorRegistroDAL();
                resultado = lectorDal.ObtenerDetalleLectorRegistro(lector);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="lote">Corral del cual se consultura el reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorFecha(DateTime fecha, LoteInfo lote)
        {
            RepartoInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoPorFecha(fecha, lote);
                if (resultado != null)
                {
                    resultado.DetalleReparto = repartoDal.ObtenerDetalle(resultado);
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Redondear los kilos
        /// </summary>
        /// <param name="numero">Numero que se redondeara</param>
        /// <param name="redondear">Valor al cual se decea redondear</param>
        /// <returns>Valor redondeado</returns>
        private int RedondearKilos(int numero, int redondear)
        {
            var multiplo = 0;
            var resultado = 0;
            var multiploMenor = 0;
            var multiploMayor = 0;
            var ciclo = true;
            try
            {
                while (ciclo)
                {
                    multiplo = multiplo + redondear;

                    if (multiplo > numero)
                    {
                        multiploMayor = multiplo;
                        ciclo = false;
                    }
                    else
                    {
                        multiploMenor = multiplo;
                    }
                }
                var resultadoMenor = numero - multiploMenor;
                var resultadoMayor = multiploMayor - numero;

                resultado = resultadoMenor > resultadoMayor ? multiploMayor : multiploMenor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return resultado;
        }
        /// <summary>
        /// Obtine el avance del reparto
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <returns>Informacion con el avance del Reparto</returns>
        internal RepartoAvanceInfo ObtenerAvanceReparto(int usuarioId)
        {
            RepartoAvanceInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerAvanceReparto(usuarioId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Reportar avance del reparto
        /// </summary>
        /// <param name="avance">Informacion del reparto</param>
        internal void ReportarAvanceReparto(RepartoAvanceInfo avance)
        {
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                repartoDal.ReportarAvanceReparto(avance);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Guarda el estatus de la distribucion
        /// </summary>
        /// <param name="corral">Identificador del corral</param>
        /// <param name="estatusDistribucion">Estatus de la distribucion</param>
        /// <returns></returns>
        internal int GuardarEstatusDistribucion(CorralInfo corral, int estatusDistribucion)
        {
            int info;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                info = repartoDal.GuardarEstatusDistribucion(corral, estatusDistribucion);
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
        /// Gurda los cambios a la orde de reparto detalle
        /// </summary>
        /// <param name="cambiosDetalle">Lista con los cambios del detalle del reparto</param>
        /// <returns></returns>
        internal int GuardarCambiosRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();

                resultado = repartoDal.GuardarCambiosRepartoDetalle(cambiosDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Carga la informacion obtenida del datalink
        /// </summary>
        /// <param name="listaDatalink">Lista con la informacion del datalink</param>
        /// <returns></returns>
        internal int CargarArchivoDatalink(List<DataLinkInfo> listaDatalink)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();

                resultado = repartoDal.CargarArchivoDatalink(listaDatalink);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal List<RepartoDetalleInfo> ObtenerRepartoDetallePorOrganizacionID(OrganizacionInfo organizacion, DateTime fecha)
        {
            List<RepartoDetalleInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoDetallePorOrganizacionID(organizacion, fecha);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal RepartoInfo ObtenerPorId(RepartoInfo reparto)
        {
            RepartoInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerPorId(reparto);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Genera un reparto manual para cada corral especificado.
        /// </summary>
        /// <param name="corrales"></param>
        /// <param name="parametroConsumo"></param>
        /// <returns></returns>
        internal ResultadoOperacion GenerarOrdenRepartoManual(List<CorralInfo> corrales, ParametroCapturaManualConsumoInfo parametroConsumo)
        {
            var respuesta = new ResultadoOperacion { Resultado = false };
            var fechaActual = DateTime.Now;
            //var tieneServicioManual = false;
            var repartoDal = new RepartoDAL();
            int totalDeCabezasTodosLosCorrales = 0, kilogramosConsumidosPorCorral = 0, corralesProcesados = 0, kilogramosSobrados = 0;
            decimal kilogramPorCabeza = 0;
            var ordenRepartoAlimentacion = new OrdenRepartoAlimentacionInfo();
            bool afectarInventarioProducto = false;
            int productoId = 0;
            var totalCorrales = corrales.Count;

            var loteProyeccionBl = new LoteProyeccionBL();

            var loteBl = new LoteBL();
            var corralBl = new CorralBL();
            var entradaGanadoBl = new EntradaGanadoBL();
            var almacenBl = new AlmacenBL();
            var animalBl = new AnimalBL();
            var formulaBl = new FormulaBL();
            var repartoBL = new RepartoBL();
            var repartoGenerado = new OrdenRepartoAlimentacionInfo();
            var detalles = new List<RepartoDetalleInfo>();

            try
            {
                int organizacionId = parametroConsumo.OrganizacionId;
                IList<LoteInfo> lotes = loteBl.ObtenerPorOrganizacionLoteXML(organizacionId, corrales);
                if (lotes != null && lotes.Any())
                {
                    totalDeCabezasTodosLosCorrales = lotes.Sum(cab => cab.Cabezas);
                }
                if (totalDeCabezasTodosLosCorrales > 0)
                {
                    kilogramPorCabeza = (decimal)parametroConsumo.KilogramosServidos / (decimal)totalDeCabezasTodosLosCorrales;
                    kilogramosSobrados = parametroConsumo.KilogramosServidos % totalDeCabezasTodosLosCorrales;
                }
                int totalRepartido = 0;
                IList<CorralInfo> corralesXML = corralBl.ObtenerPorCorralIdXML(corrales);
                //lotes = loteBl.ObtenerPorCorralCerradoXML(organizacionId, corrales);

                if (lotes != null)
                {
                    IList<EntradaGanadoInfo> entradas = entradaGanadoBl.ObtenerEntradaPorLoteXML(organizacionId, lotes);

                    if (entradas == null)
                    {
                        entradas = new List<EntradaGanadoInfo>();
                    }

                    IList<LoteProyeccionInfo> lotesProyeccion = loteProyeccionBl.ObtenerPorLoteXML(organizacionId, lotes);

                    if (lotesProyeccion == null)
                    {
                        lotesProyeccion = new List<LoteProyeccionInfo>();
                    }

                    var loteReimplanteBl = new LoteReimplanteBL();
                    IList<LoteReimplanteInfo> lotesReimplante = loteReimplanteBl.ObtenerPorLoteXML(organizacionId, lotes);

                    if (lotesReimplante == null)
                    {
                        lotesReimplante = new List<LoteReimplanteInfo>();
                    }

                    IList<RepartoInfo> repartos = ObtenerRepartoPorFechaCompleto(fechaActual, lotes);

                    if (repartos == null)
                    {
                        repartos = new List<RepartoInfo>();
                    }

                    var avanceReparto = new RepartoAvanceInfo
                    {
                        UsuarioID = parametroConsumo.UsuarioId,
                        Seccion = MensajesRepartoAvancesEnum.Iniciando.ToString(),
                        PorcentajeSeccion = 0,
                        PorcentajeTotal = 0,
                        TotalCorrales = totalCorrales,
                        TotalCorralesProcesados = corralesProcesados,
                        TotalCorralesProcesadosSeccion = 0,
                        TotalCorralesSeccion = 0,
                        EstatusError = 0
                    };
                    ReportarAvanceReparto(avanceReparto);

                    using (var scope = new TransactionScope())
                    {
                        foreach (CorralInfo corral in corralesXML)
                        {
                            ordenRepartoAlimentacion.Lote =
                                lotes.FirstOrDefault(clave => clave.CorralID == corral.CorralID);

                            ordenRepartoAlimentacion.Corral = corral;

                            //reportando el avance
                            corralesProcesados++;
                            var porcentajeProceso = (int)(((decimal)corralesProcesados / (decimal)totalCorrales) * 100);

                            avanceReparto = new RepartoAvanceInfo
                            {
                                UsuarioID = parametroConsumo.UsuarioId,
                                Seccion = MensajesRepartoAvancesEnum.Sección + " " + corral.Seccion,
                                PorcentajeSeccion = 0,
                                PorcentajeTotal = porcentajeProceso,
                                TotalCorrales = totalCorrales,
                                TotalCorralesProcesados = corralesProcesados,
                                TotalCorralesProcesadosSeccion = 0,
                                TotalCorralesSeccion = 0,
                                EstatusError = 0
                            };
                            ReportarAvanceReparto(avanceReparto);

                            if (ordenRepartoAlimentacion.Lote != null)
                            {
                                if (repartos != null)
                                {
                                    ordenRepartoAlimentacion.Reparto =
                                        repartos.FirstOrDefault(
                                            clave => clave.LoteID == ordenRepartoAlimentacion.Lote.LoteID);
                                }

                                if (ordenRepartoAlimentacion.Reparto == null)
                                {
                                    ordenRepartoAlimentacion.Reparto = new RepartoInfo { RepartoID = 0 };
                                }

                                afectarInventarioProducto = true;

                                var diferenciaFechasDiasEngorda = fechaActual -
                                                                  ordenRepartoAlimentacion.Lote.FechaInicio;
                                ordenRepartoAlimentacion.Reparto.DiasEngorda = diferenciaFechasDiasEngorda.Days;
                                // Si los corrales son de recepcion se manejan de forma diferente.
                                if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.Recepcion)
                                {
                                    ordenRepartoAlimentacion.Reparto.PesoInicio = 0;
                                    ordenRepartoAlimentacion.Reparto.PesoProyectado = 0;
                                    ordenRepartoAlimentacion.Reparto.PesoRepeso = 0;

                                    EntradaGanadoInfo entradaGanado = null;
                                    if (entradas != null)
                                    {
                                        entradaGanado =
                                            entradas.FirstOrDefault(
                                                clave =>
                                                    clave.LoteID == ordenRepartoAlimentacion.Lote.LoteID &&
                                                    clave.CorralID == corral.CorralID);
                                    }
                                    if (entradaGanado != null)
                                    {
                                        if (entradaGanado.CabezasRecibidas > 0)
                                        {
                                            ordenRepartoAlimentacion.Reparto.PesoInicio =
                                                ((int)entradaGanado.PesoBruto -
                                                 (int)entradaGanado.PesoTara) /
                                                entradaGanado.CabezasRecibidas;
                                        }
                                    }
                                }
                                else
                                {
                                    var ordenTemporal = ObtenerPesoLlegadaPorLote(ordenRepartoAlimentacion.Lote);
                                    //Obtiene el peso de llegada
                                    if (ordenTemporal != null)
                                    {
                                        ordenRepartoAlimentacion.PesoLlegada = ordenTemporal.PesoLlegada;
                                        ordenRepartoAlimentacion.TotalAnimalesPeso = ordenTemporal.TotalAnimalesPeso;
                                        ordenRepartoAlimentacion.PesoActual = ordenTemporal.PesoActual;
                                        if (ordenRepartoAlimentacion.Lote.Cabezas > 0)
                                        {
                                            ordenRepartoAlimentacion.Reparto.PesoInicio = ordenTemporal.PesoLlegada /
                                                                                          ordenRepartoAlimentacion
                                                                                              .Lote
                                                                                              .Cabezas;
                                        }
                                        else
                                        {
                                            ordenRepartoAlimentacion.Reparto.PesoInicio = 0;
                                        }
                                    }

                                    if (lotesProyeccion != null)
                                    {
                                        ordenRepartoAlimentacion.Proyeccion =
                                            lotesProyeccion.FirstOrDefault(
                                                clave =>
                                                    clave.LoteID == ordenRepartoAlimentacion.Lote.LoteID &&
                                                    clave.OrganizacionID == organizacionId);
                                        if (ordenRepartoAlimentacion.Proyeccion != null)
                                        {
                                            ordenRepartoAlimentacion.GananciaCorral =
                                                ordenRepartoAlimentacion.Reparto.DiasEngorda *
                                                ordenRepartoAlimentacion.Proyeccion.GananciaDiaria;

                                            ordenRepartoAlimentacion.Reparto.PesoProyectado =
                                                ordenRepartoAlimentacion.PesoActual /
                                                ordenRepartoAlimentacion.Lote.Cabezas +
                                                (int)ordenRepartoAlimentacion.GananciaCorral;

                                            if (lotesReimplante != null)
                                            {
                                                ordenRepartoAlimentacion.LoteReimplante =
                                                    lotesReimplante.FirstOrDefault(
                                                        clave =>
                                                            clave.LoteProyeccionID ==
                                                            ordenRepartoAlimentacion.Proyeccion.LoteProyeccionID);
                                                if (ordenRepartoAlimentacion.LoteReimplante != null)
                                                {
                                                    ordenRepartoAlimentacion.Reparto.PesoRepeso =
                                                        ordenRepartoAlimentacion.LoteReimplante.PesoReal;
                                                }
                                            }
                                        }
                                    }
                                }

                                ordenRepartoAlimentacion.OrganizacionID = parametroConsumo.OrganizacionId;
                                ordenRepartoAlimentacion.UsuarioID = parametroConsumo.UsuarioId;
                                ordenRepartoAlimentacion.Reparto.Fecha = fechaActual.Date;

                                kilogramosConsumidosPorCorral = Convert.ToInt32(Math.Round(kilogramPorCabeza * ordenRepartoAlimentacion.Lote.Cabezas, 0));

                                var ultimoElemento = corralesXML.LastOrDefault();
                                if (ultimoElemento != null)
                                {
                                    if (corral.CorralID == ultimoElemento.CorralID)
                                    {
                                        kilogramosConsumidosPorCorral = parametroConsumo.KilogramosServidos -
                                                                        totalRepartido;
                                    }
                                }


                                // Le agregamos los kilogramos sobrados al primer corral y lo dejamos en 0
                                //kilogramosConsumidosPorCorral = (kilogramPorCabeza * ordenRepartoAlimentacion.Lote.Cabezas); + kilogramosSobrados;


                                totalRepartido = totalRepartido + kilogramosConsumidosPorCorral;

                                kilogramosSobrados = 0;

                                var repartoDetalle = new RepartoDetalleInfo
                                {
                                    Cabezas = ordenRepartoAlimentacion.Lote.Cabezas,
                                    TipoServicioID = (int)TipoServicioEnum.Manual,
                                    FormulaIDProgramada = parametroConsumo.FormulaId,
                                    FormulaIDServida = parametroConsumo.FormulaId,
                                    CantidadProgramada = kilogramosConsumidosPorCorral,
                                    CantidadServida = kilogramosConsumidosPorCorral,
                                    HoraReparto = parametroConsumo.HoraSistema,
                                    EstadoComederoID = (int)EstadoComederoEnum.Normal
                                };

                                ordenRepartoAlimentacion.DetalleOrdenReparto = repartoDetalle;
                                repartoGenerado = repartoDal.GenerarOrdenRepartoManual(ordenRepartoAlimentacion);

                                // Se afecta el costeo de los animales del corral
                                if (repartoGenerado != null)
                                {
                                    repartoGenerado.DetalleOrdenReparto.TipoServicioID = (int)TipoServicioEnum.Manual;
                                    repartoGenerado.DetalleOrdenReparto.RepartoID = repartoGenerado.Reparto.RepartoID;
                                    repartoGenerado.DetalleOrdenReparto.Prorrateo = true;

                                    FormulaInfo formulaInfo = formulaBl.ObtenerPorID(parametroConsumo.FormulaId);

                                    productoId = formulaInfo.Producto.ProductoId;
                                    var tipoAlmacenId = (int)TipoAlmacenEnum.MateriasPrimas;

                                    // La busqueda del almacen cambia para el caso de Formulas de Inicio(F0).
                                    if (parametroConsumo.LoteId == -1)
                                    {
                                        tipoAlmacenId = (int)TipoAlmacenEnum.PlantaDeAlimentos;
                                    }

                                    if (formulaInfo != null)
                                    {
                                        var almacenInventarioInfo =
                                            almacenBl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(
                                                new ParametrosOrganizacionTipoAlmacenProductoActivo
                                                {
                                                    Activo = (int)EstatusEnum.Activo,
                                                    OrganizacionId = organizacionId,
                                                    ProductoId = formulaInfo.Producto.ProductoId,
                                                    TipoAlmacenId = tipoAlmacenId
                                                });

                                        if (almacenInventarioInfo != null)
                                        {

                                            if (ordenRepartoAlimentacion.Lote.TipoCorralID != (int)TipoCorral.Recepcion &&
                                                corral.TipoCorral.TipoCorralID != (int)TipoCorral.Recepcion)
                                            {
                                                var animalesLote =
                                                    animalBl.ObtenerAnimalesPorLoteID(ordenRepartoAlimentacion.Lote);

                                                if (animalesLote != null)
                                                {
                                                    decimal consumoAnimal =
                                                        Convert.ToDecimal(
                                                            (Convert.ToDecimal(kilogramosConsumidosPorCorral) /
                                                             Convert.ToDecimal(
                                                                 ordenRepartoAlimentacion.Lote.Cabezas)));

                                                    decimal consumoImporteAnimal = consumoAnimal * almacenInventarioInfo.PrecioPromedio;

                                                    var listaAnimalCosto = new List<AnimalCostoInfo>();
                                                    var listaAnimalConsumo = new List<AnimalConsumoInfo>();

                                                    var reparto = new RepartoInfo
                                                    {
                                                        RepartoID = repartoGenerado.Reparto.RepartoID,
                                                        OrganizacionID = parametroConsumo.OrganizacionId
                                                    };
                                                    reparto = repartoBL.ObtenerPorId(reparto);

                                                    foreach (AnimalInfo animal in animalesLote)
                                                    {
                                                        var animalCosto = new AnimalCostoInfo
                                                        {
                                                            CostoID = (int)Costo.Alimento,
                                                            TipoReferencia = TipoReferenciaAnimalCosto.Reparto,
                                                            FolioReferencia = repartoGenerado.Reparto.RepartoID,
                                                            AnimalID = animal.AnimalID,
                                                            Importe = consumoImporteAnimal,
                                                            FechaCosto = fechaActual,
                                                            UsuarioCreacionID = parametroConsumo.UsuarioId
                                                        };
                                                        listaAnimalCosto.Add(animalCosto);

                                                        var animalConsumo = new AnimalConsumoInfo
                                                        {
                                                            AnimalId = animal.AnimalID,
                                                            Cantidad = consumoAnimal,
                                                            FormulaServidaId = parametroConsumo.FormulaId,
                                                            Fecha = reparto.Fecha,
                                                            RepartoId = repartoGenerado.Reparto.RepartoID,
                                                            TipoServicio = TipoServicioEnum.Manual,
                                                            UsuarioCreacionId = parametroConsumo.UsuarioId
                                                        };
                                                        listaAnimalConsumo.Add(animalConsumo);
                                                    }

                                                    if (listaAnimalCosto != null && listaAnimalCosto.Any())
                                                    {
                                                        animalBl.GuardarAnimalCostoXMLManual(listaAnimalCosto);
                                                    }

                                                    if (listaAnimalConsumo != null && listaAnimalConsumo.Any())
                                                    {
                                                        animalBl.GuardarAnimalConsumoXmlManual(listaAnimalConsumo);
                                                    }
                                                }
                                            }
                                        }

                                        almacenInventarioInfo.UsuarioModificacionID = parametroConsumo.UsuarioId;

                                        GuardarImporte(repartoGenerado.DetalleOrdenReparto, almacenInventarioInfo, ordenRepartoAlimentacion.Lote);

                                        //var repartoBL = new RepartoBL();
                                        detalles.Add(repartoGenerado.DetalleOrdenReparto);

                                    }
                                }
                            }
                        }

                        // SI POR LO MENOS UN CORRAL FUE INGRESADO AL CONSUMO MANUAL
                        int almacenID = 0;
                        long almacenMovimientoID = 0;
                        if (afectarInventarioProducto)
                        {
                            // SI LA FORMULA MANEJA LOTE
                            if (parametroConsumo.LoteId != -1)
                            {
                                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();

                                //SE OBTIENEN LOS DATOS DEL LOTE ORIGEN
                                var almacenInventarioLoteOrigen =
                                    almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                        parametroConsumo.LoteId);

                                var KGxPiezas = 0;
                                if (almacenInventarioLoteOrigen.Piezas > 0)
                                {
                                    KGxPiezas = (int)(almacenInventarioLoteOrigen.Cantidad / almacenInventarioLoteOrigen.Piezas);
                                }

                                // GENERA MOVIMIENTO DE INVENTARIO
                                var almacenMovimiento = almacenBl.GuardarAlmacenMovimiento(new AlmacenMovimientoInfo
                                {
                                    AlmacenID =
                                        almacenInventarioLoteOrigen
                                        .AlmacenInventario.
                                        AlmacenID,
                                    TipoMovimientoID = (int)TipoMovimiento.AplicacionAlimento,
                                    Observaciones = "",
                                    Status = (int)Estatus.AplicadoInv,
                                    AnimalMovimientoID = 0,
                                    ProveedorId = 0,
                                    UsuarioCreacionID = parametroConsumo.UsuarioId
                                });

                                almacenID = almacenInventarioLoteOrigen.AlmacenInventario.AlmacenID;
                                almacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;

                                // GENERA EL DETALLE DEL MOVIMIENTO
                                var PiezasConsumo = 0;
                                if (KGxPiezas > 0)
                                {
                                    PiezasConsumo = (int)(parametroConsumo.KilogramosServidos / KGxPiezas);
                                }
                                almacenBl.GuardarAlmacenMovimientoDetalleProducto(
                                    new AlmacenMovimientoDetalle
                                    {
                                        AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                        ProductoID = productoId,
                                        Precio = almacenInventarioLoteOrigen.PrecioPromedio,
                                        Cantidad = parametroConsumo.KilogramosServidos,
                                        Importe = almacenInventarioLoteOrigen.PrecioPromedio * parametroConsumo.KilogramosServidos,
                                        AlmacenInventarioLoteId = almacenInventarioLoteOrigen.AlmacenInventarioLoteId,
                                        ContratoId = 0,
                                        Piezas = PiezasConsumo, //Piezas = 0,
                                        UsuarioCreacionID = parametroConsumo.UsuarioId
                                    });
                                // GENERA LA SALIDA DEL INVENTARIO LOTE
                                almacenInventarioLoteOrigen.Cantidad -= parametroConsumo.KilogramosServidos;
                                almacenInventarioLoteOrigen.Importe = almacenInventarioLoteOrigen.PrecioPromedio *
                                                                      almacenInventarioLoteOrigen.Cantidad;
                                almacenInventarioLoteOrigen.Piezas -= PiezasConsumo;
                                almacenInventarioLoteOrigen.UsuarioModificacionId = parametroConsumo.UsuarioId;
                                almacenInventarioLoteBl.Actualizar(almacenInventarioLoteOrigen);

                                var almacenInventarioBl = new AlmacenInventarioBL();
                                // GENERA LA SALIDA DEL ALMACEN INVENTARIO
                                var almacenInventarioInfo =
                                    almacenInventarioBl.ObtenerAlmacenInventarioPorId(
                                        almacenInventarioLoteOrigen.AlmacenInventario.AlmacenInventarioID);
                                almacenInventarioInfo.Cantidad = almacenInventarioInfo.Cantidad -
                                                                 parametroConsumo.KilogramosServidos;
                                almacenInventarioInfo.Importe = almacenInventarioInfo.PrecioPromedio *
                                                                almacenInventarioInfo.Cantidad;
                                almacenInventarioInfo.UsuarioModificacionID = parametroConsumo.UsuarioId;
                                almacenInventarioBl.Actualizar(almacenInventarioInfo);

                                detalles.ForEach(det =>
                                {
                                    det.AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;
                                    det.UsuarioCreacionID = parametroConsumo.UsuarioId;
                                });
                                repartoBL.ActualizarAlmacenMovimientoReparto(detalles);
                            }
                            else // CUANDO LA FORMULA NO MANEJA LOTE O ES DE TIPO INICIO (F0)
                            {
                                //SE OBTIENEN LOS DATOS DEL LOTE ORIGEN
                                var almacenInventarioInfo = almacenBl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(new ParametrosOrganizacionTipoAlmacenProductoActivo()
                                {
                                    Activo = 1,
                                    OrganizacionId = parametroConsumo.OrganizacionId,
                                    ProductoId = productoId,
                                    TipoAlmacenId = (int)TipoAlmacenEnum.PlantaDeAlimentos
                                });

                                // GENERA MOVIMIENTO DE INVENTARIO
                                var almacenMovimiento = almacenBl.GuardarAlmacenMovimiento(new AlmacenMovimientoInfo
                                {
                                    AlmacenID = almacenInventarioInfo.AlmacenID,
                                    TipoMovimientoID = (int)TipoMovimiento.AplicacionAlimento,
                                    Observaciones = "",
                                    Status = (int)Estatus.AplicadoInv,
                                    AnimalMovimientoID = 0,
                                    ProveedorId = 0,
                                    UsuarioCreacionID = parametroConsumo.UsuarioId
                                });

                                almacenID = almacenInventarioInfo.AlmacenID;
                                almacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;

                                // GENERA EL DETALLE DEL MOVIMIENTO
                                almacenBl.GuardarAlmacenMovimientoDetalleProducto(
                                    new AlmacenMovimientoDetalle
                                    {
                                        AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID,
                                        ProductoID = productoId,
                                        Precio = almacenInventarioInfo.PrecioPromedio,
                                        Cantidad = parametroConsumo.KilogramosServidos,
                                        Importe = almacenInventarioInfo.PrecioPromedio * parametroConsumo.KilogramosServidos,
                                        AlmacenInventarioLoteId = 0, //Se almacena Null en el SP
                                        ContratoId = 0,
                                        Piezas = 0, //Queda en 0, por no manejar piezas.
                                        UsuarioCreacionID = parametroConsumo.UsuarioId
                                    });
                                var almacenInventarioBl = new AlmacenInventarioBL();
                                // GENERA LA SALIDA DEL ALMACEN INVENTARIO
                                almacenInventarioInfo.Cantidad = almacenInventarioInfo.Cantidad - parametroConsumo.KilogramosServidos;
                                almacenInventarioInfo.Importe = almacenInventarioInfo.PrecioPromedio * almacenInventarioInfo.Cantidad;
                                almacenInventarioInfo.UsuarioModificacionID = parametroConsumo.UsuarioId;
                                almacenInventarioBl.Actualizar(almacenInventarioInfo);

                                detalles.ForEach(det =>
                                {
                                    det.AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;
                                    det.UsuarioCreacionID = parametroConsumo.UsuarioId;
                                });
                                repartoBL.ActualizarAlmacenMovimientoReparto(detalles);
                            }

                        }

                        #region POLIZA

                        if (afectarInventarioProducto)
                        {
                            var almacenMovimientoDetalleBL = new AlmacenMovimientoDetalleBL();
                            var almacenMovimeintoBL = new AlmacenMovimientoBL();
                            AlmacenMovimientoDetalle almacenMovimientoDetalle =
                                almacenMovimientoDetalleBL.ObtenerPorAlmacenMovimientoID(almacenMovimientoID);

                            AlmacenMovimientoInfo almacenMovimiento = almacenMovimeintoBL.ObtenerPorId(almacenMovimientoID);

                            if (almacenMovimientoDetalle != null)
                            {
                                PolizaAbstract polizaConsumo =
                                    FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.ConsumoAlimento);
                                var produccionFormulaDetalle = new ProduccionFormulaDetalleInfo
                                {
                                    ProduccionFormulaId = parametroConsumo.FormulaId,
                                    OrganizacionID = organizacionId,
                                    AlmacenID = almacenID,
                                    Producto = almacenMovimientoDetalle.Producto,
                                };
                                var repartoDetalle = new RepartoDetalleInfo
                                {
                                    Importe = almacenMovimientoDetalle.Importe,
                                    FormulaIDServida = parametroConsumo.FormulaId,
                                    OrganizacionID = organizacionId
                                };
                                var polizaConsumoAlimentoModel = new PolizaConsumoAlimentoModel
                                {
                                    Reparto = new RepartoInfo
                                    {
                                        Fecha = fechaActual,
                                        DetalleReparto = new List<RepartoDetalleInfo> { repartoDetalle }
                                    },
                                    ProduccionFormula = new ProduccionFormulaInfo
                                    {
                                        ProduccionFormulaDetalle
                                            =
                                            new List<ProduccionFormulaDetalleInfo> { produccionFormulaDetalle }
                                    },
                                    AlmacenMovimiento = almacenMovimiento
                                };
                                IList<PolizaInfo> polizasConsumo = polizaConsumo.GeneraPoliza(polizaConsumoAlimentoModel);
                                if (polizasConsumo != null && polizasConsumo.Any())
                                {
                                    var polizaBL = new PolizaBL();
                                    polizasConsumo.ToList().ForEach(datos =>
                                    {
                                        datos.OrganizacionID = organizacionId;
                                        datos.UsuarioCreacionID =
                                            parametroConsumo.UsuarioId;
                                        datos.ArchivoEnviadoServidor = 1;
                                    });
                                    polizaBL.GuardarServicioPI(polizasConsumo, TipoPoliza.ConsumoAlimento);
                                }
                            }
                        }

                        #endregion POLIZA

                        scope.Complete();
                    }
                    respuesta.Resultado = true;
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return respuesta;
        }
        /// <summary>
        /// Metodo para obtener los repartos de Forraje
        /// </summary>
        /// <param name="corteGanadoGuardarInfo"></param>
        public List<RepartoDetalleInfo> ObtenerRepartoPorTipoServicioFecha(CorteGanadoGuardarInfo corteGanadoGuardarInfo)
        {
            List<RepartoDetalleInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoPorTipoServicioFecha(corteGanadoGuardarInfo);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal int GuardarImporte(RepartoDetalleInfo repartoDetalleInfo, AlmacenInventarioInfo inventarioInfo, LoteInfo lote)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.GuardarImporte(repartoDetalleInfo, inventarioInfo, lote);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los datos para generar la poliza de consumo
        /// </summary>
        /// <returns></returns>
        internal PolizaConsumoAlimentoModel ObtenerDatosPolizaConsumo(List<AlmacenMovimientoDetalle> movimientoDetalles, int organizacionID)
        {
            PolizaConsumoAlimentoModel resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerDatosPolizaConsumo(movimientoDetalles, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="lotes">Lote del cual se consultura el reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal IList<RepartoInfo> ObtenerRepartoPorFechaCompleto(DateTime fecha, IList<LoteInfo> lotes)
        {
            IList<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoPorFechaCompleto(fecha, lotes);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="reparto">Fecha del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal int Guardar(RepartoInfo reparto)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.Crear(reparto);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Guarda los Reparto Detalle
        /// </summary>
        /// <param name="repartoDetalle">Fecha del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal int GuardarRepartoDetalle(List<RepartoDetalleInfo> repartoDetalle)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.GuardarRepartoDetalle(repartoDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Genera orden de reparto para los corrales que no tienen
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        internal int GenerarOrdenRepartoConfiguracionAjustes(List<CambiosReporteInfo> cambiosDetalle)
        {
            int resultado;
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var repartoDal = new RepartoDAL();
                    resultado = repartoDal.GenerarOrdenRepartoConfiguracionAjustes(cambiosDetalle);
                    transaction.Complete();
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="corral">Corral del cual se consultura el reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorFechaCorral(DateTime fecha, CorralInfo corral)
        {
            RepartoInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoPorFechaCorral(fecha, corral);
                if (resultado != null)
                {
                    resultado.DetalleReparto = repartoDal.ObtenerDetalle(resultado);
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        ///  Metodo que actualiza las formulas de los repartos de la mañana y tarde.
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        internal int GuardarFormulasRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();

                resultado = repartoDal.GuardarFormulasRepartoDetalle(cambiosDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal int ActualizarAlmacenMovimientoReparto(List<RepartoDetalleInfo> repartoDetalle)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ActualizarAlmacenMovimientoReparto(repartoDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene parametros para obtener los repartos por la lista de ids
        /// </summary>
        /// <param name="repartosID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerPorRepartosID(List<long> repartosID, int organizacionID)
        {
            List<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerPorRepartosID(repartosID, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene repartos por lote y organizacion
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerPorLoteOrganizacion(int loteID, int organizacionId)
        {
            List<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerPorLoteOrganizacion(loteID, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal int GuardarImporteXML(List<RepartoDetalleInfo> listaRepartoDetalle)
        {
            int resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.GuardarImporteXML(listaRepartoDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el peso de llegada por lote
        /// </summary>
        /// <param name="lotesXml">Informacion del lote</param>
        /// <param name="organizacionID">Informacion del lote</param>
        /// <returns>Orden de reparto de alimentacion</returns>
        internal IList<OrdenRepartoAlimentacionInfo> ObtenerPesoLlegadaPorLoteXML(IList<LoteInfo> lotesXml, int organizacionID)
        {
            IList<OrdenRepartoAlimentacionInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerPesoLlegadaXML(lotesXml, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="lotesXml">Lotes del cual se consultura el reparto</param>
        /// <param name="organizacionID">Lotes del cual se consultura el reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal IList<RepartoInfo> ObtenerRepartoPorFechaXML(DateTime fecha, IList<LoteInfo> lotesXml, int organizacionID)
        {
            IList<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoPorFechaXML(fecha, lotesXml, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el total del consumo del dia
        /// </summary>
        /// <param name="organizacionId">Identificacion de la organizacion</param>
        /// <param name="corralesXml">Informacion del lote</param>
        /// <param name="fecha">fecha de los consumos</param>
        internal IList<ConsumoTotalCorralModel> ObtenerConsumoTotalDiaXML(int organizacionId, IList<CorralInfo> corralesXml, DateTime fecha)
        {
            IList<ConsumoTotalCorralModel> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerConsumoTotalDiaXML(organizacionId, corralesXml, fecha);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el lector registro de un lote y una fecha especifica
        /// </summary>
        /// <param name="lotesXml">Lote del cual se quiere obtner el lector</param>
        /// <param name="fecha">Fecha del lector</param>
        /// <param name="organizacionID">Fecha del lector</param>
        /// <returns>Informacion del lector registro</returns>
        internal IList<LectorRegistroInfo> ObtenerLectorRegistroXML(IList<LoteInfo> lotesXml, DateTime fecha, int organizacionID)
        {
            IList<LectorRegistroInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new LectorRegistroDAL();
                resultado = repartoDal.ObtenerLectorRegistroXML(lotesXml, fecha, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene repartos por lote y organizacion
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerPorCorralOrganizacion(int corralID, int organizacionId)
        {
            List<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerPorCorralOrganizacion(corralID, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los consumos pendientes de aplicar
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal List<AplicacionConsumoDetalleModel> ObtenerConsumoPendiente(AplicacionConsumoModel filtros)
        {
            List<AplicacionConsumoDetalleModel> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerConsumoPendiente(filtros);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una orden de reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha de la orden de reparto</param>
        /// <param name="organizacionID">Organizacion del reparto</param>
        /// <param name="corrales">corrales del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal List<RepartoInfo> ObtenerRepartosPorFechaCorrales(DateTime fecha, int organizacionID, List<CorralInfo> corrales)
        {
            List<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartosPorFechaCorrales(fecha, organizacionID, corrales);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Guardar los repartos de Servicio de Corral
        /// </summary>
        /// <param name="cambiosDetalle">Contiene los valores a guardar</param>
        /// <returns>Informacion del reparto</returns>
        internal void GuardarRepartosServicioCorrales(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                repartoDal.GuardarRepartosServicioCorrales(cambiosDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="corral">Corral del cual se consultura el reparto</param>
        /// <param name="tipoServicioID">servicio del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorFechaCorralServicio(DateTime fecha, CorralInfo corral, int tipoServicioID)
        {
            RepartoInfo resultado;
            try
            {
                Logger.Info();
                var repartoDal = new RepartoDAL();
                resultado = repartoDal.ObtenerRepartoPorFechaCorralServicio(fecha, corral, tipoServicioID);
                if (resultado != null)
                {
                    resultado.DetalleReparto = repartoDal.ObtenerDetalle(resultado);
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
