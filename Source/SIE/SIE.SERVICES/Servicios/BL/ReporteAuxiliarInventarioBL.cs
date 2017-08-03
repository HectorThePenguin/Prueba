using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Info.Enums;
using System.Linq;
using SIE.Services.Info.Info;
using System.Text;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteAuxiliarInventarioBL
    {
        const int ENTRADA = 1;
        const int SALIDA = 1;

        /// <summary>
        /// Obtiene los datos para el Reporte Auxiliar de Inventario
        /// </summary>
        /// <returns> </returns>
        internal CorralReporteAuxiliarInventarioInfo ObtenerDatosCorral(string codigoCorral, int organizacionID)
        {
            CorralReporteAuxiliarInventarioInfo datosCorral = null;
            try
            {
                Logger.Info();
                var reporteAuxiliarInventarioDAL = new ReporteAuxiliarInventarioDAL();
                CorralReporteAuxiliarInventarioInfo datosPreliminares =
                    reporteAuxiliarInventarioDAL.ObtenerDatosCorral(codigoCorral, organizacionID);
                if (datosPreliminares != null)
                {
                    datosCorral = ObtenerValoresCorralAuxiliarInventario(datosPreliminares);
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
            return datosCorral;
        }

        /// <summary>
        /// Genera el reporte de auxiliar de inventario
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="grupoCorral"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AuxiliarDeInventarioInfo> ObtenerDatosReporteAuxiliarInventario(int loteID, GrupoCorralEnum grupoCorral, int organizacionId)
        {
            List<AuxiliarDeInventarioInfo> reporteAuxiliarInventario = null;
            try
            {
                Logger.Info();
                var reporteAuxiliarInventarioDAL = new ReporteAuxiliarInventarioDAL();
                ReporteAuxiliarInventarioInfo auxiliarInventario = reporteAuxiliarInventarioDAL.ObtenerDatosReporteAuxiliarInventario(loteID);
                if (auxiliarInventario != null)
                {
                    reporteAuxiliarInventario = GenerarReporte(loteID,auxiliarInventario, grupoCorral, organizacionId);

                    if(reporteAuxiliarInventario != null && reporteAuxiliarInventario.Any())
                    {
                        reporteAuxiliarInventario.ForEach(rep =>
                                                          {
                                                              rep.FechaLlegada = rep.FechaMovimiento.ToShortDateString();
                                                              if (string.IsNullOrWhiteSpace(rep.FechaCorte))
                                                              {
                                                                  rep.FechaCorte = rep.FechaLlegada;
                                                              }
                                                          });

                        return reporteAuxiliarInventario.OrderBy(rep => long.Parse(rep.Arete)).ThenBy(rep => DateTime.Parse(rep.FechaCorte)).ToList();
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
            return reporteAuxiliarInventario;
        }

        /// <summary>
        /// Formatea los valores para mostrarse en pantalla
        /// </summary>
        /// <param name="corralReporteAuxiliarInventario"></param>
        /// <returns></returns>
        private CorralReporteAuxiliarInventarioInfo ObtenerValoresCorralAuxiliarInventario(CorralReporteAuxiliarInventarioInfo corralReporteAuxiliarInventario)
        {
            var tipoCorral = (TipoCorral)corralReporteAuxiliarInventario.TipoCorralID;
            switch (tipoCorral)
            {
                case TipoCorral.Recepcion:
                    corralReporteAuxiliarInventario.TipoGanado = string.Empty;
                    corralReporteAuxiliarInventario.Clasificacion = string.Empty;
                    break;
                case TipoCorral.Enfermeria:
                    corralReporteAuxiliarInventario.TipoGanado = string.Empty;
                    break;
                case TipoCorral.Improductivos:
                case TipoCorral.Maquila:
                case TipoCorral.Intensivo:
                case TipoCorral.CronicoRecuperacion:
                case TipoCorral.CronicoVentaMuerte:
                    corralReporteAuxiliarInventario.Clasificacion = corralReporteAuxiliarInventario.TipoCorral;
                    break;
            }
            return corralReporteAuxiliarInventario;
        }

        /// <summary>
        /// Genera Reporte Auxiliar de Inventario
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="reporteAuxiliarInventario"></param>
        /// <param name="grupoCorral"> </param>
        /// <param name="organizacionId"> </param>
        private List<AuxiliarDeInventarioInfo> GenerarReporte(int loteID,ReporteAuxiliarInventarioInfo reporteAuxiliarInventario, GrupoCorralEnum grupoCorral, int organizacionId)
        {
            List<AuxiliarDeInventarioInfo> datosReporteAuxiliarInventario = null;

            switch (grupoCorral)
            {
                case GrupoCorralEnum.Recepcion:
                    datosReporteAuxiliarInventario = GenerarMovimientosGrupoCorralRecepcion(loteID, reporteAuxiliarInventario);
                    break;
                case GrupoCorralEnum.Produccion:
                    datosReporteAuxiliarInventario = GenerarMovimientosGrupoCorralProduccion(loteID, reporteAuxiliarInventario, organizacionId);
                    break;
                case GrupoCorralEnum.Enfermeria:
                    datosReporteAuxiliarInventario = GenerarMovimientosGrupoCorralEnfermeria(loteID,reporteAuxiliarInventario, organizacionId);
                    break;
            }
            return datosReporteAuxiliarInventario;
        }

        /// <summary>
        /// Genera movimientos de produccion
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="reporteAuxiliarInventario"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        private List<AuxiliarDeInventarioInfo> GenerarMovimientosGrupoCorralProduccion(int loteID, ReporteAuxiliarInventarioInfo reporteAuxiliarInventario, int organizacionId)
        {
            //Lista de las entradas obtenidas
            List<int> foliosEntrada = reporteAuxiliarInventario.Animales.Select(animal => animal.FolioEntrada).Distinct().ToList();

            var datosReporteAuxiliarInventario = new List<AuxiliarDeInventarioInfo>();

            for (int indexFolios = 0; indexFolios < foliosEntrada.Count; indexFolios++)
            {
                var entradaGanadoBL = new EntradaGanadoBL();
                int folioEntrada = foliosEntrada[indexFolios]; //folio de entrada


                //Obtiene la entrada de ganado
                EntradaGanadoInfo entradaGanado = entradaGanadoBL.ObtenerPorFolioEntradaOrganizacion(folioEntrada, organizacionId);

                //Obtiene la lista de animales de la entrada
                List<AnimalInfo> animales =
                    reporteAuxiliarInventario.Animales.Where(folio => folio.FolioEntrada == folioEntrada).ToList();

                if (animales.Any())
                {
                    animales.ForEach(animal =>
                    {
                        
                        if (entradaGanado == null)
                        {
                            entradaGanado = new EntradaGanadoInfo();
                        }

                        //Fecha de llegada = a la fecha de entrada
                        
                        //Obtenemos el lote
                        LoteInfo lote = reporteAuxiliarInventario.Lotes.FirstOrDefault();/*condicion => condicion.Corral.CorralID == entradaGanado.CorralID);*/
                        if (lote == null)
                        {
                            lote = new LoteInfo
                            {
                                Corral = new CorralInfo()
                            };
                        }

                        //Movimientos de animales
                        List<AnimalMovimientoInfo> animalMovimiento =
                            reporteAuxiliarInventario.AnimalesMovimiento.Where( 
                            condicion => condicion.AnimalID == animal.AnimalID /*&& condicion.FechaMovimiento >= lote.FechaInicio*/).ToList();

                        animalMovimiento.ForEach(movimientos =>
                        {
                            var datoReporteAuxiliarInventario = new AuxiliarDeInventarioInfo();
                            datoReporteAuxiliarInventario.FechaMovimiento = entradaGanado.FechaEntrada; //.ToShortDateString();
                            //Tipo de movimiento
                            TipoMovimientoInfo tipoMovimiento =
                                reporteAuxiliarInventario.TiposMovimiento.FirstOrDefault( mov =>  mov.TipoMovimientoID == movimientos.TipoMovimientoID);

                            Logger.Info();

                            if (tipoMovimiento != null)
                            {
                                datoReporteAuxiliarInventario.Partida = movimientos.FolioEntrada;

                                //determinacion de los dias de engorda para ganado vivo
                                var fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                var auxFecha = new DateTime(entradaGanado.FechaEntrada.Year, entradaGanado.FechaEntrada.Month, entradaGanado.FechaEntrada.Day);

                                TimeSpan diasIntervalo = fechaHoy - auxFecha;
                                datoReporteAuxiliarInventario.DiasEngorda = diasIntervalo.Days;
                                
                                //arete del animal
                                /*AnimalInfo areteAnimal = reporteAuxiliarInventario.Animales.FirstOrDefault(
                                            condicion => condicion.AnimalID == movimientos.AnimalID);
                                
                                if (areteAnimal == null)
                                {
                                    areteAnimal = new AnimalInfo();
                                }*/

                                datoReporteAuxiliarInventario.Arete = animal!=null?animal.Arete:String.Empty;

                                //Si el movimiento es entrada
                                if(movimientos.LoteID == loteID&&
                                    movimientos.TipoMovimientoID != TipoMovimiento.Muerte.GetHashCode() &&
                                    movimientos.TipoMovimientoID != TipoMovimiento.SalidaEnfermeria.GetHashCode()) //(tipoMovimiento.EsEntrada)
                                {
                                    datoReporteAuxiliarInventario.CorralOrigen = !string.IsNullOrWhiteSpace(movimientos.CorralOrigen) ? movimientos.CorralOrigen : entradaGanado.CodigoCorral;  //entradaGanado.CodigoCorral;
                                    datoReporteAuxiliarInventario.Entradas = ENTRADA;
                                    datoReporteAuxiliarInventario.Salidas = 0;
                                }
                                datoReporteAuxiliarInventario.CorralOrigen = !string.IsNullOrWhiteSpace(movimientos.CorralOrigen) ? movimientos.CorralOrigen : entradaGanado.CodigoCorral;


                                var tipoMovimientoEnum = (TipoMovimiento) tipoMovimiento.TipoMovimientoID;
                                if (movimientos.LoteID == loteID && tipoMovimientoEnum != TipoMovimiento.Muerte 
                                    && tipoMovimientoEnum != TipoMovimiento.SalidaPorVenta && tipoMovimientoEnum != TipoMovimiento.SalidaPorSacrificio) //(tipoMovimiento.EsEntrada)
                                {
                                    var salidaDatoReporteAuxiliarEE = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                    if (salidaDatoReporteAuxiliarEE != null)
                                    {
                                        salidaDatoReporteAuxiliarEE.FechaCorte =
                                            movimientos.FechaMovimiento.ToShortDateString();
                                        salidaDatoReporteAuxiliarEE.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" +
                                                                                    movimientos.AnimalMovimientoID;
                                        datoReporteAuxiliarInventario.FechaMovimiento = entradaGanado.FechaEntrada;
                                            //.ToShortDateString();

                                        if (
                                            datosReporteAuxiliarInventario.Where(
                                                registro =>
                                                    registro.ClaveCodigo ==
                                                    salidaDatoReporteAuxiliarEE.ClaveCodigo).ToList().Count == 0)
                                        {
                                            datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliarEE);
                                        }
                                    }
                                }
                                else
                                {
                                    var salidaDatoReporteAuxiliarES = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                    if (salidaDatoReporteAuxiliarES != null)
                                    {
                                        salidaDatoReporteAuxiliarES.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;
                                        salidaDatoReporteAuxiliarES.Salidas = SALIDA;
                                        salidaDatoReporteAuxiliarES.Entradas = 0;
                                        LoteInfo ultimoCorral = reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                                condicion => condicion.Corral.CorralID == movimientos.CorralID && condicion.LoteID == movimientos.LoteID);
                                        if (ultimoCorral != null)
                                        {
                                            salidaDatoReporteAuxiliarES.CorralOrigen = ultimoCorral.Corral.Codigo;
                                        }
                                        salidaDatoReporteAuxiliarES.FechaMovimiento = movimientos.FechaMovimiento; //.ToShortDateString();
                                        salidaDatoReporteAuxiliarES.FechaCorte = movimientos.FechaMovimiento.ToShortDateString(); //.ToShortDateString();
                                        if (
                                            datosReporteAuxiliarInventario.Where(
                                                registro =>
                                                    registro.ClaveCodigo ==
                                                    salidaDatoReporteAuxiliarES.ClaveCodigo).ToList().Count == 0)
                                        {
                                            datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliarES);
                                        }
                                    }
                                }

                                //Revisar tipo de movimiento
                                #region Version Vieja
                                //switch (tipoMovimientoEnum)
                                //{
                                //    case TipoMovimiento.EntradaSalidaEnfermeria:
                                //    case TipoMovimiento.SalidaPorRecuperacion:
                                //    case TipoMovimiento.SalidaEnfermeria:
                                //    case TipoMovimiento.CortePorTransferencia:
                                //    case TipoMovimiento.Reimplante:
                                //        if (movimientos.LoteID == loteID) //(tipoMovimiento.EsEntrada)
                                //        {
                                //            datoReporteAuxiliarInventario.FechaCorte = movimientos.FechaMovimiento.ToShortDateString();
                                //            datoReporteAuxiliarInventario.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;
                                //            datoReporteAuxiliarInventario.FechaMovimiento =entradaGanado.FechaEntrada; //.ToShortDateString();



                                //            if (
                                //                datosReporteAuxiliarInventario.Where(
                                //                    registro =>
                                //                        registro.ClaveCodigo ==
                                //                        datoReporteAuxiliarInventario.ClaveCodigo).ToList().Count == 0)
                                //            {
                                //                datosReporteAuxiliarInventario.Add(datoReporteAuxiliarInventario);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            var salidaDatoReporteAuxiliarES = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                //            if (salidaDatoReporteAuxiliarES != null)
                                //            {
                                //                salidaDatoReporteAuxiliarES.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;
                                //                salidaDatoReporteAuxiliarES.Salidas = SALIDA;
                                //                salidaDatoReporteAuxiliarES.Entradas = 0;
                                //                LoteInfo ultimoCorral = reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                //                        condicion => condicion.Corral.CorralID == movimientos.CorralID && condicion.LoteID == movimientos.LoteID);
                                //                if (ultimoCorral != null)
                                //                {
                                //                    salidaDatoReporteAuxiliarES.CorralOrigen = ultimoCorral.Corral.Codigo;
                                //                }
                                //                salidaDatoReporteAuxiliarES.FechaMovimiento =movimientos.FechaMovimiento; //.ToShortDateString();
                                //                if (
                                //                    datosReporteAuxiliarInventario.Where(
                                //                        registro =>
                                //                            registro.ClaveCodigo ==
                                //                            salidaDatoReporteAuxiliarES.ClaveCodigo).ToList().Count == 0)
                                //                {
                                //                    datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliarES);
                                //                }
                                //            }
                                //        }
                                //        break;
                                //    // Cuando el tipo de movimiento es corte
                                    
                                //    case TipoMovimiento.Corte:
                                //        datoReporteAuxiliarInventario.FechaCorte = movimientos.FechaMovimiento.ToShortDateString();
                                //        datoReporteAuxiliarInventario.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;
                                //        datoReporteAuxiliarInventario.FechaMovimiento =entradaGanado.FechaEntrada; //.ToShortDateString();
                                //        if (
                                //            datosReporteAuxiliarInventario.Where(
                                //                registro =>
                                //                    registro.ClaveCodigo ==
                                //                    datoReporteAuxiliarInventario.ClaveCodigo).ToList().Count == 0)
                                //        {
                                //            datosReporteAuxiliarInventario.Add(datoReporteAuxiliarInventario);
                                //        }
                                //        break;

                                //    //Es muerte salida por sacrificio reimplante o traspaso
                                //    case TipoMovimiento.Muerte:
                                //    case TipoMovimiento.SalidaPorSacrificio:
                                //    case TipoMovimiento.TraspasoDeGanado:
                                //    case TipoMovimiento.EntradaEnfermeria:
                                //        var salidaDatoReporteAuxiliar = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                //        if ( salidaDatoReporteAuxiliar != null)
                                //        {
                                //            salidaDatoReporteAuxiliar .ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;

                                //            if (movimientos.LoteID != loteID || tipoMovimientoEnum == TipoMovimiento.Muerte || tipoMovimientoEnum == TipoMovimiento.SalidaEnfermeria)// (tipoMovimiento.EsSalida)
                                //            {
                                //                salidaDatoReporteAuxiliar.Salidas = SALIDA;
                                //                salidaDatoReporteAuxiliar.Entradas = 0;
                                //                 LoteInfo ultimoCorral = reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                //                        condicion => condicion.Corral.CorralID == movimientos.CorralID && condicion.LoteID == movimientos.LoteID);
                                //                if (ultimoCorral != null)
                                //                {
                                //                    salidaDatoReporteAuxiliar.CorralOrigen = ultimoCorral.Corral.Codigo;
                                //                }
                                //            }

                                //            if (tipoMovimientoEnum == TipoMovimiento.Muerte)
                                //            {
                                //                //determinacion de los dias de engorda para ganado vivo
                                //                //var fechaMuerte = new DateTime(movimientos.FechaMovimiento.Year, movimientos.FechaMovimiento.Month, movimientos.FechaMovimiento.Day);
                                //                //var auxFechaEntrada = new DateTime(entradaGanado.FechaEntrada.Year, entradaGanado.FechaEntrada.Month, entradaGanado.FechaEntrada.Day);

                                //                //TimeSpan diasIntervaloMuerte = fechaMuerte - auxFechaEntrada;
                                //                //salidaDatoReporteAuxiliar.DiasEngorda = diasIntervaloMuerte.Days;
                                //                salidaDatoReporteAuxiliar.DiasEngorda = 0;
                                //            }


                                //            salidaDatoReporteAuxiliar.FechaMovimiento =movimientos.FechaMovimiento; //.ToShortDateString();
                                //            if (
                                //                datosReporteAuxiliarInventario.Where(
                                //                    registro =>
                                //                        registro.ClaveCodigo ==
                                //                        salidaDatoReporteAuxiliar.ClaveCodigo).ToList().Count == 0)
                                //            {
                                //                datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliar);
                                //            }
                                //        }
                                //        break;
                                //}
                                #endregion Version Vieja
                            }
                        });
                    });
                }
            }
            
            if (datosReporteAuxiliarInventario.Count > 0)
            {
                return datosReporteAuxiliarInventario.OrderBy(registro => registro.FechaCorte).ToList();
            }
            else
            {
                return datosReporteAuxiliarInventario;
            }
        }

        /// <summary>
        /// Genera los movimientos que se mostraran para enfermeria
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="reporteAuxiliarInventario"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        private List<AuxiliarDeInventarioInfo> GenerarMovimientosGrupoCorralEnfermeria(int loteID, ReporteAuxiliarInventarioInfo reporteAuxiliarInventario, int organizacionId)
        {
            //Lista de las entradas obtenidas
            List<int> foliosEntrada = reporteAuxiliarInventario.Animales.Select(animal => animal.FolioEntrada).Distinct().ToList();

            var datosReporteAuxiliarInventario = new List<AuxiliarDeInventarioInfo>();

            for (int indexFolios = 0; indexFolios < foliosEntrada.Count; indexFolios++)
            {
                var entradaGanadoBL = new EntradaGanadoBL();
                int folioEntrada = foliosEntrada[indexFolios]; //folio de entrada


                //Obtiene la entrada de ganado
                EntradaGanadoInfo entradaGanado = entradaGanadoBL.ObtenerPorFolioEntradaOrganizacion(folioEntrada, organizacionId);

                //Obtiene la lista de animales de la entrada
                List<AnimalInfo> animales =
                    reporteAuxiliarInventario.Animales.Where(folio => folio.FolioEntrada == folioEntrada).ToList();

                if (animales.Any())
                {
                    //animales.ForEach(animal =>
                    foreach (var animal in animales)
                    {
                        var datoReporteAuxiliarInventario = new AuxiliarDeInventarioInfo();
                        if (entradaGanado == null)
                        {
                            entradaGanado = new EntradaGanadoInfo();
                        }

                        //Fecha de llegada = a la fecha de entrada
                        datoReporteAuxiliarInventario.FechaMovimiento =entradaGanado.FechaEntrada;//.ToShortDateString();
                        //Obtenemos el lote
                        LoteInfo lote = reporteAuxiliarInventario.Lotes.FirstOrDefault();/*condicion => condicion.Corral.CorralID == entradaGanado.CorralID);*/
                        if (lote == null)
                        {
                            lote = new LoteInfo
                            {
                                Corral = new CorralInfo()
                            };
                        }

                        //Movimientos de animales
                        List<AnimalMovimientoInfo> animalMovimiento =
                            reporteAuxiliarInventario.AnimalesMovimiento.Where(
                            condicion => condicion.AnimalID == animal.AnimalID /*&& condicion.FechaMovimiento >= lote.FechaInicio*/).ToList();

                        foreach(var movimientos in animalMovimiento)
                        {
                            //Tipo de movimiento
                            TipoMovimientoInfo tipoMovimiento =
                                reporteAuxiliarInventario.
                                    TiposMovimiento.FirstOrDefault(mov => mov.TipoMovimientoID == movimientos.TipoMovimientoID);

                            Logger.Info();

                            if (tipoMovimiento != null)
                            {
                                datoReporteAuxiliarInventario.Partida = movimientos.FolioEntrada;

                                //determinacion de los dias de engorda para ganado vivo
                                var fechaHoy = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                var auxFecha = new DateTime(entradaGanado.FechaEntrada.Year, entradaGanado.FechaEntrada.Month, entradaGanado.FechaEntrada.Day);
                                
                                TimeSpan diasIntervalo = fechaHoy - auxFecha;
                                datoReporteAuxiliarInventario.DiasEngorda = diasIntervalo.Days;

                                //arete del animal
                                /*AnimalInfo areteAnimal = reporteAuxiliarInventario.Animales.FirstOrDefault(
                                            condicion => condicion.AnimalID == movimientos.AnimalID);

                                if (areteAnimal == null)
                                {
                                    areteAnimal = new AnimalInfo();
                                }*/

                                datoReporteAuxiliarInventario.Arete = animal!=null?animal.Arete:String.Empty;

                                //Si el movimiento es entrada
                                if (movimientos.LoteID == loteID &&
                                    movimientos.TipoMovimientoID != TipoMovimiento.Muerte.GetHashCode() &&
                                    movimientos.TipoMovimientoID != TipoMovimiento.EntradaSalidaEnfermeria.GetHashCode())
                                    //(tipoMovimiento.EsEntrada)
                                {
                                    datoReporteAuxiliarInventario.Entradas = ENTRADA;
                                    datoReporteAuxiliarInventario.Salidas = 0;
                                }
                                datoReporteAuxiliarInventario.CorralOrigen = entradaGanado.CodigoCorral;

                                //Revisar tipo de movimiento
                                //var tipoMovimientoEnum = (TipoMovimiento)tipoMovimiento.TipoMovimientoID;

                                var tipoMovimientoEnum = (TipoMovimiento)tipoMovimiento.TipoMovimientoID;
                                if (movimientos.LoteID == loteID && tipoMovimientoEnum != TipoMovimiento.Muerte
                                    && tipoMovimientoEnum != TipoMovimiento.SalidaPorVenta && tipoMovimientoEnum != TipoMovimiento.SalidaPorSacrificio) //(tipoMovimiento.EsEntrada)
                                {
                                    var salidaDatoReporteAuxiliarEE = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                    if (salidaDatoReporteAuxiliarEE != null)
                                    {

                                        salidaDatoReporteAuxiliarEE.FechaCorte =
                                            movimientos.FechaMovimiento.ToShortDateString();
                                        salidaDatoReporteAuxiliarEE.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" +
                                                                                    movimientos.AnimalMovimientoID;
                                        datoReporteAuxiliarInventario.FechaMovimiento = entradaGanado.FechaEntrada;
                                            //.ToShortDateString();

                                        if (
                                            datosReporteAuxiliarInventario.Where(
                                                registro =>
                                                    registro.ClaveCodigo ==
                                                    salidaDatoReporteAuxiliarEE.ClaveCodigo).ToList().Count == 0)
                                        {
                                            datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliarEE);
                                        }
                                    }
                                }
                                else
                                {
                                    var salidaDatoReporteAuxiliarES = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                    if (salidaDatoReporteAuxiliarES != null)
                                    {
                                        salidaDatoReporteAuxiliarES.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;
                                        salidaDatoReporteAuxiliarES.Salidas = SALIDA;
                                        salidaDatoReporteAuxiliarES.Entradas = 0;
                                        salidaDatoReporteAuxiliarES.FechaMovimiento = entradaGanado.FechaEntrada;
                                        salidaDatoReporteAuxiliarES.FechaCorte =
                                           movimientos.FechaMovimiento.ToShortDateString();
                                        LoteInfo ultimoCorral = reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                                condicion => condicion.Corral.CorralID == movimientos.CorralID && condicion.LoteID == movimientos.LoteID);
                                        if (ultimoCorral != null)
                                        {
                                            salidaDatoReporteAuxiliarES.CorralOrigen = ultimoCorral.Corral.Codigo;
                                        }
                                        //salidaDatoReporteAuxiliarES.FechaMovimiento = movimientos.FechaMovimiento; //.ToShortDateString();
                                        if (
                                            datosReporteAuxiliarInventario.Where(
                                                registro =>
                                                    registro.ClaveCodigo ==
                                                    salidaDatoReporteAuxiliarES.ClaveCodigo).ToList().Count == 0)
                                        {
                                            datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliarES);
                                        }
                                    }
                                }

                                #region Version Vieja
                                //switch (tipoMovimientoEnum)
                                //{
                                //    // Cuando el tipo de movimiento es corte
                                    
                                //    case TipoMovimiento.EntradaEnfermeria:
                                //        if (movimientos.LoteID == loteID)
                                //        {
                                //            datoReporteAuxiliarInventario.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" +
                                //                                                        movimientos.AnimalMovimientoID;
                                //            datoReporteAuxiliarInventario.FechaMovimiento =entradaGanado.FechaEntrada; //.ToShortDateString();
                                //            if (
                                //                datosReporteAuxiliarInventario.Where(
                                //                    registro =>
                                //                        registro.ClaveCodigo ==
                                //                        datoReporteAuxiliarInventario.ClaveCodigo).ToList().Count == 0)
                                //            {
                                //                datosReporteAuxiliarInventario.Add(datoReporteAuxiliarInventario);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            var salidaDatoReporteAuxiliarEntrada = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                //            if (salidaDatoReporteAuxiliarEntrada != null)
                                //            {
                                //                salidaDatoReporteAuxiliarEntrada.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;

                                //                salidaDatoReporteAuxiliarEntrada.Salidas = SALIDA;
                                //                salidaDatoReporteAuxiliarEntrada.Entradas = 0;
                                //                LoteInfo ultimoCorral = reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                //                        condicion => condicion.Corral.CorralID == movimientos.CorralID && condicion.LoteID == movimientos.LoteID);
                                //                if (ultimoCorral != null)
                                //                {
                                //                    salidaDatoReporteAuxiliarEntrada.CorralOrigen = ultimoCorral.Corral.Codigo;
                                //                }

                                //                salidaDatoReporteAuxiliarEntrada.FechaMovimiento =movimientos.FechaMovimiento;//.ToShortDateString();
                                //                if (
                                //                    datosReporteAuxiliarInventario.Where(
                                //                        registro =>
                                //                            registro.ClaveCodigo ==
                                //                            salidaDatoReporteAuxiliarEntrada.ClaveCodigo).ToList().Count == 0)
                                //                {
                                //                    datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliarEntrada);
                                //                }
                                //            }
                                //        }
                                //        break;
                                //    //Es muerte salida por sacrificio reimplante o traspaso
                                //    case TipoMovimiento.Corte:
                                //        datoReporteAuxiliarInventario.FechaCorte = movimientos.FechaMovimiento.ToShortDateString();
                                //        break; 
                                //    case TipoMovimiento.SalidaPorRecuperacion:
                                //    case TipoMovimiento.SalidaPorSacrificio:
                                //    case TipoMovimiento.SalidaPorVenta:
                                //    case TipoMovimiento.Muerte:
                                //    case TipoMovimiento.TraspasoDeGanado:
                                //    case TipoMovimiento.SalidaEnfermeria:
                                //        var salidaDatoReporteAuxiliar = ClonarInfo(datoReporteAuxiliarInventario) as AuxiliarDeInventarioInfo;
                                //        if (salidaDatoReporteAuxiliar != null)
                                //        {
                                //            salidaDatoReporteAuxiliar.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + movimientos.AnimalMovimientoID;

                                //            if (movimientos.LoteID != loteID || tipoMovimientoEnum == TipoMovimiento.Muerte || tipoMovimientoEnum == TipoMovimiento.SalidaEnfermeria)//(tipoMovimiento.EsSalida)
                                //            {
                                //                salidaDatoReporteAuxiliar.Salidas = SALIDA;
                                //                salidaDatoReporteAuxiliar.Entradas = 0;
                                //                LoteInfo ultimoCorral = reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                //                       condicion => condicion.Corral.CorralID == movimientos.CorralID && condicion.LoteID == movimientos.LoteID);
                                //                if (ultimoCorral != null)
                                //                {
                                //                    salidaDatoReporteAuxiliar.CorralOrigen = ultimoCorral.Corral.Codigo;
                                //                }
                                //            }

                                //            if (tipoMovimientoEnum == TipoMovimiento.Muerte)
                                //            {
                                //                //determinacion de los dias de engorda para ganado vivo
                                //                var fechaMuerte = new DateTime(movimientos.FechaMovimiento.Year, movimientos.FechaMovimiento.Month, movimientos.FechaMovimiento.Day);
                                //                var auxFechaEntrada = new DateTime(entradaGanado.FechaEntrada.Year, entradaGanado.FechaEntrada.Month, entradaGanado.FechaEntrada.Day);

                                //                TimeSpan diasIntervaloMuerte = fechaMuerte - auxFechaEntrada;
                                //                salidaDatoReporteAuxiliar.DiasEngorda = diasIntervaloMuerte.Days;
                                //            }

                                //            salidaDatoReporteAuxiliar.FechaMovimiento =movimientos.FechaMovimiento;//.ToShortDateString();
                                //            if (
                                //                datosReporteAuxiliarInventario.Where(
                                //                    registro =>
                                //                        registro.ClaveCodigo ==
                                //                        salidaDatoReporteAuxiliar.ClaveCodigo).ToList().Count == 0)
                                //            {
                                //                datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliar);
                                //            }
                                //        }
                                //        break;
                                //}
                                #endregion Version Vieja
                            }
                        }//);
                    }//);
                }
            }
            if (datosReporteAuxiliarInventario.Count > 0)
            {
                return datosReporteAuxiliarInventario.OrderBy(registro => registro.FechaLlegada).ToList();
            }
            else
            {
                return datosReporteAuxiliarInventario;
            }
        }

        /// <summary>
        /// Genera los movimientos que se mostraran para Recepcion
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="reporteAuxiliarInventario"></param>
        private List<AuxiliarDeInventarioInfo> GenerarMovimientosGrupoCorralRecepcion(int loteID, ReporteAuxiliarInventarioInfo reporteAuxiliarInventario)
        {
            var datosReporteAuxiliarInventario = new List<AuxiliarDeInventarioInfo>();
            List<int> foliosEntrada = reporteAuxiliarInventario.Animales.Select(animal => animal.FolioEntrada).Distinct().ToList();
            List<EntradaGanadoInfo> entradas = reporteAuxiliarInventario.EntradasGanado;

            //for (int indexFolios = 0; indexFolios < foliosEntrada.Count; indexFolios++)
            //{
            //    int folioEntrada = foliosEntrada[indexFolios]; //folio de entrada

                if (entradas != null && entradas.Any()) //Se valida que exista informacion de entrada
                {
                    int indice = 1;
                    //Para cada movimiento especificado
                    foreach (var movs in entradas)
                    {
                        //int cabezas = movs.CabezasRecibidas;

                        List<AnimalMovimientoInfo> animalMovimiento2 =
                                reporteAuxiliarInventario.AnimalesMovimiento.Where(
                                condicion => condicion.FolioEntrada == movs.FolioEntrada && condicion.FolioEntrada != 0 ).ToList(); //
                        
                        #region Generar Movimientos de Entrada genericos, de lotes Activos
                        if (animalMovimiento2.Count < movs.CabezasRecibidas)
                        {
                            var loteBL = new LoteBL();
                            LoteInfo lote = loteBL.ObtenerPorID(movs.LoteID);
                            if(lote != null && lote.Activo == EstatusEnum.Activo)
                            {
                                int animalesPendientes = movs.CabezasRecibidas - animalMovimiento2.Count;
                                for (int i = 0; i < animalesPendientes; i++)
                                {
                                    var datoReporteAuxiliarGenerico = new AuxiliarDeInventarioInfo();

                                    datoReporteAuxiliarGenerico.FechaMovimiento = movs.FechaEntrada;
                                    datoReporteAuxiliarGenerico.Entradas = ENTRADA;
                                    if (movs.FolioOrigen > 0)
                                    {
                                        datoReporteAuxiliarGenerico.ClaveCodigo = "CA";
                                    }
                                    else
                                    {
                                        datoReporteAuxiliarGenerico.ClaveCodigo = "CD";
                                    }
                                    datoReporteAuxiliarGenerico.CorralOrigen = movs.CodigoCorral;
                                    datoReporteAuxiliarGenerico.Partida = movs.FolioEntrada; //dato de la partida
                                    datoReporteAuxiliarGenerico.DiasEngorda = 0; //dias de engorda

                                    datosReporteAuxiliarInventario.Add(datoReporteAuxiliarGenerico);
                                        //Agregamos el dato  a la lista
                                }
                            }
                        }
                        #endregion Generar Movimientos de Entrada genericos, de lotes Activos

                        animalMovimiento2.ForEach(movimientos =>
                        {
                            //Tipo de movimiento
                            TipoMovimientoInfo tipoMovimiento =
                                reporteAuxiliarInventario.TiposMovimiento.FirstOrDefault(mov => mov.TipoMovimientoID == movimientos.TipoMovimientoID);

                            var arete = new StringBuilder();
                            var datoReporteAuxiliar = new AuxiliarDeInventarioInfo();

                            datoReporteAuxiliar.FechaMovimiento =movs.FechaEntrada;//.ToShortDateString(); //Fecha de llegada igual a la fecha de inicio

                            if (tipoMovimiento != null)
                            {
                                datoReporteAuxiliar.ClaveCodigo = tipoMovimiento.ClaveCodigo;
                                datoReporteAuxiliar.Entradas = ENTRADA;
                            }

                            //Datos del lote
                            LoteInfo lote =
                                reporteAuxiliarInventario.Lotes.FirstOrDefault(
                                    condicion => condicion.Corral.CorralID == movs.CorralID);

                            if (lote == null)
                            {
                                lote = new LoteInfo
                                {
                                    Corral = new CorralInfo()
                                };
                            }

                            //Datos del corral
                            if (movimientos.LoteID == loteID)//(!string.IsNullOrWhiteSpace(lote.Corral.Codigo))
                            {
                                datoReporteAuxiliar.CorralOrigen = lote.Corral.Codigo;
                            }
                            else
                            {
                                datoReporteAuxiliar.CorralOrigen = movs.CodigoCorral;
                            }

                            //Datos del tipo de corral
                            TipoCorral tipoCorral;
                            if (lote.Corral.TipoCorral == null)
                            {
                                tipoCorral = TipoCorral.Recepcion;
                            }
                            else
                            {
                                tipoCorral = (TipoCorral)lote.Corral.TipoCorral.TipoCorralID;
                            }

                            //Cuando es tipo corral improductivo
                            if (tipoCorral == TipoCorral.Improductivos)
                            {
                                //Se toma el tipo de movimiento de corte por transferencia
                                tipoMovimiento =
                                    reporteAuxiliarInventario.TiposMovimiento.FirstOrDefault(
                                        condicion => condicion.TipoMovimientoID == TipoMovimiento.CortePorTransferencia.GetHashCode());

                                if (tipoMovimiento == null)
                                {
                                    datoReporteAuxiliar.ClaveCodigo = string.Empty;
                                }
                                else
                                {
                                    datoReporteAuxiliar.ClaveCodigo = tipoMovimiento.ClaveCodigo;
                                }
                            }

                            //Tipo de organziacion
                            var tipoOrganizacion = (TipoOrganizacion)movs.TipoOrigen;
                            switch (tipoOrganizacion)
                            {
                                //Cuando es centro, pradera o descanso, se toma el arete de la interfaceSalida
                                case TipoOrganizacion.Centro:
                                case TipoOrganizacion.Praderas:
                                case TipoOrganizacion.Descanso:
                                    InterfaceSalidaAnimalInfo interfaceSalida =
                                        reporteAuxiliarInventario.InterfaceSalidasAnimal.FirstOrDefault(
                                            condicion => condicion.SalidaID == movs.FolioOrigen &&
                                                         condicion.CorralID == movs.CorralID &&
                                                         condicion.Partida == indice);

                                    if (interfaceSalida != null)
                                    {
                                        datoReporteAuxiliar.Arete = interfaceSalida.Arete;
                                        arete.Append(interfaceSalida.Arete);
                                    }

                                    break;

                                //Cuando es compra directa se coloca el arete en vacio
                                case TipoOrganizacion.CompraDirecta:
                                    datoReporteAuxiliar.Arete = string.Empty;
                                    break;
                            }

                            datoReporteAuxiliar.Partida = movs.FolioEntrada; //dato de la partida
                            datoReporteAuxiliar.DiasEngorda = 0;   //dias de engorda

                            datosReporteAuxiliarInventario.Add(datoReporteAuxiliar); //Agregamos el dato  a la lista

                            //List<AnimalInfo> animales = reporteAuxiliarInventario.Animales.Where(folio => folio.FolioEntrada == folioEntrada).ToList();

                            AnimalInfo animal;
                            if (tipoMovimiento.ClaveCodigo == "CE" || tipoMovimiento.ClaveCodigo == "EE") 
                            {
                                animal =
                                reporteAuxiliarInventario.Animales.FirstOrDefault(
                                    condicion => condicion.FolioEntrada == movs.FolioEntrada && condicion.AnimalID == movimientos.AnimalID);
                            }
                            else
                            {
                                animal =
                                reporteAuxiliarInventario.Animales.FirstOrDefault(
                                    condicion => condicion.FolioEntrada == movs.FolioEntrada && condicion.Arete.Equals(arete.ToString()));
                            }
                            

                            if (animal != null)
                            {
                                //Obtenemos el animalmovimiento de la informacion con la ultima fecha de movimiento
                                AnimalMovimientoInfo animalMovimiento = reporteAuxiliarInventario.
                                    AnimalesMovimiento.OrderByDescending(
                                        fecha => fecha.FechaMovimiento).FirstOrDefault(
                                            condicion => condicion.AnimalID == animal.AnimalID &&
                                                (condicion.TipoMovimientoID == TipoMovimiento.Corte.GetHashCode() || //revisamos si ya esta cortado el arete o tiene entrada a enfermeria
                                                condicion.TipoMovimientoID == TipoMovimiento.EntradaEnfermeria.GetHashCode() ||
                                                condicion.TipoMovimientoID == TipoMovimiento.Muerte.GetHashCode() ||
                                                condicion.TipoMovimientoID == TipoMovimiento.SalidaPorVenta.GetHashCode()
                                                )
                                         );
                                //Si tiene animalmovimiento de corte o entrada a enfermeria se duplica el registro como salida
                                if (animalMovimiento != null)
                                {
                                    var salidaDatoReporteAuxiliar = ClonarInfo(datoReporteAuxiliar) as AuxiliarDeInventarioInfo;
                                    if (salidaDatoReporteAuxiliar != null)
                                    {
                                        var corralBl = new CorralBL();
                                        var corral = corralBl.ObtenerPorId(movimientos.CorralID);
                                        if (corral != null)
                                        {
                                            salidaDatoReporteAuxiliar.CorralOrigen = corral.Codigo;
                                        }
                                        tipoMovimiento =
                                            reporteAuxiliarInventario.TiposMovimiento.FirstOrDefault(
                                                condicion => condicion.TipoMovimientoID == animalMovimiento.TipoMovimientoID);

                                        if (tipoMovimiento != null)
                                        {
                                            salidaDatoReporteAuxiliar.ClaveCodigo = tipoMovimiento.ClaveCodigo + "-" + animalMovimiento.AnimalMovimientoID;
                                            salidaDatoReporteAuxiliar.Arete = animal.Arete;
                                        }
                                        salidaDatoReporteAuxiliar.Entradas = 0;
                                        salidaDatoReporteAuxiliar.Salidas = SALIDA;
                                        salidaDatoReporteAuxiliar.FechaCorte = animalMovimiento.FechaMovimiento.ToShortDateString();

                                        if (movs.FolioOrigen > 0)
                                        {
                                            datosReporteAuxiliarInventario[datosReporteAuxiliarInventario.Count - 1]
                                                .ClaveCodigo = "CA";
                                        }
                                        else
                                        {
                                            datosReporteAuxiliarInventario[datosReporteAuxiliarInventario.Count - 1]
                                                .ClaveCodigo = "CD";
                                        }
                                        //Se agrega el movimiento de salida al reporte
                                        datosReporteAuxiliarInventario.Add(salidaDatoReporteAuxiliar);
                                    }
                                }
                            }
                        });
                    }
                //}

            }
            if (datosReporteAuxiliarInventario.Count > 0)
            {
                return datosReporteAuxiliarInventario.OrderBy(registro => registro.FechaLlegada).ToList();
            }
            else
            {
                return datosReporteAuxiliarInventario;
            }
        }

        /// <summary>
        /// Genera un Clone de la Instancia que se le envia
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private Object ClonarInfo(Object info)
        {
            Object clone = Activator.CreateInstance(info.GetType(), null);
            PropertyInfo[] properties = info.GetType().GetProperties();
            const string NAME_SPACE = ".Info.Info";
            foreach (var propertyInfo in properties)
            {
                MethodInfo getMethod = info.GetType().GetProperty(propertyInfo.Name).GetGetMethod();
                if (getMethod != null)
                {
                    object valorPropiedad = info.GetType().GetProperty(propertyInfo.Name).GetValue(info, null);
                    if (valorPropiedad != null && valorPropiedad.GetType().Namespace.Contains(NAME_SPACE))
                    {
                        valorPropiedad = ClonarInfo(valorPropiedad);
                    }
                    MethodInfo setMethod = clone.GetType().GetProperty(propertyInfo.Name).GetSetMethod();
                    if (setMethod != null)
                    {
                        clone.GetType().GetProperty(propertyInfo.Name).SetValue(clone, valorPropiedad, null);
                    }
                }
            }
            return clone;
        }
    }
}
