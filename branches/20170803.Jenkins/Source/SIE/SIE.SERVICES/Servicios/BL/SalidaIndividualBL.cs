using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System;
using System.Reflection;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    internal class SalidaIndividualBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar un Corral
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacion"></param>
        /// <param name="codigoCorral"></param>
        /// <param name="corraletaID"></param>
        /// <param name="usuarioCreacion"></param>
        /// <param name="tipoMovimiento"></param>
        /// <param name="operador"></param>
        internal int Guardar(string arete, int organizacion, string codigoCorral, int corraletaID, int usuarioCreacion, int tipoMovimiento, int operador)
        {
            int retorno = 0;
            try
            {
                Logger.Info();
                var salidaIndividualDAL = new SalidaIndividualDAL();
                var animalBl = new AnimalBL();
                var corralBl = new CorralBL();
                var loteBl = new LoteBL();

                using (var transaccion = new TransactionScope())
                {

                    var animal = animalBl.ObtenerAnimalPorArete(arete, organizacion);
                    if (animal != null)
                    {
                        if (tipoMovimiento == TipoMovimiento.SalidaPorRecuperacion.GetHashCode())
                        {
                            var corralOrigen = corralBl.ObtenerCorralPorCodigo(organizacion, codigoCorral);
                            var loteOrigen = new LoteInfo();
                            if (corralOrigen != null)
                            {
                                loteOrigen = loteBl.DeteccionObtenerPorCorral(organizacion, corralOrigen.CorralID);
                            }

                            var corralDestino = corralBl.ObtenerPorId(corraletaID);
                            var loteDestino = new LoteInfo();
                            if (corralDestino != null)
                            {
                                loteDestino = loteBl.DeteccionObtenerPorCorral(organizacion, corralDestino.CorralID);
                            }

                            if (corralOrigen != null)
                            {
                                if (loteOrigen != null)
                                {
                                    if (corralDestino != null)
                                    {
                                        if (corralDestino.TipoCorral.TipoCorralID !=
                                            TipoCorral.CorraletaRecuperado.GetHashCode() && corralDestino.TipoCorral.TipoCorralID != TipoCorral.CorraletaRecuperadosPartida.GetHashCode())
                                        {
                                            if (loteDestino != null)
                                            {
                                                var animalMovimientoBl = new AnimalMovimientoBL();

                                                var ultimoMovimiento = animalBl.ObtenerUltimoMovimientoAnimal(animal);

                                                if (ultimoMovimiento != null)
                                                {
                                                    var animalMovimientoOrigen = new AnimalMovimientoInfo
                                                    {
                                                        AnimalID = animal.AnimalID,
                                                        CorralID = corralDestino.CorralID,
                                                        LoteID = loteDestino.LoteID,
                                                        TipoMovimientoID = tipoMovimiento,
                                                        OperadorID = operador,
                                                        OrganizacionID = organizacion,
                                                        TrampaID = ultimoMovimiento.TrampaID,
                                                        Peso = ultimoMovimiento.Peso,
                                                        Temperatura = ultimoMovimiento.Temperatura,
                                                        UsuarioCreacionID = usuarioCreacion
                                                    };
                                                    animalMovimientoBl.GuardarAnimalMovimiento(animalMovimientoOrigen);

                                                    loteOrigen.Cabezas = loteOrigen.Cabezas - 1;

                                                    loteDestino.Cabezas = loteDestino.Cabezas + 1;
                                                    loteDestino.CabezasInicio = loteDestino.CabezasInicio + 1;

                                                    var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                                                    {
                                                        LoteIDOrigen = loteOrigen.LoteID,
                                                        LoteIDDestino = loteDestino.LoteID,
                                                        CabezasProcesadas = 1,
                                                        UsuarioModificacionID = usuarioCreacion
                                                    };
                                                    loteBl.ActualizarCabezasProcesadas(filtroActualizaCabezas);

                                                }
                                                else
                                                {
                                                    retorno = -1;
                                                }
                                            }
                                            else
                                            {
                                                retorno = -1;
                                            }
                                        }
                                        else
                                        {
                                            var loteBL = new LoteBL();
                                            var animalMovimientoBl = new AnimalMovimientoBL();
                                            var ultimoMovimiento = animalBl.ObtenerUltimoMovimientoAnimal(animal);

                                            LoteInfo loteDestinoCorraleta = loteBL.DeteccionObtenerPorCorral(organizacion,
                                                                                           corralDestino.CorralID);
                                            int loteIDGuardar = 0;
                                            if (loteDestinoCorraleta == null || loteDestinoCorraleta.Activo == EstatusEnum.Inactivo)
                                            {
                                                var loteNuevo = new LoteInfo
                                                    {
                                                        Activo = EstatusEnum.Activo,
                                                        Cabezas = 1,
                                                        CabezasInicio = 1,
                                                        CorralID = corralDestino.CorralID,
                                                        DisponibilidadManual = false,
                                                        OrganizacionID = organizacion,
                                                        TipoCorralID = corralDestino.TipoCorral.TipoCorralID,
                                                        TipoProcesoID = TipoProceso.EngordaPropio.GetHashCode(),
                                                        UsuarioCreacionID = usuarioCreacion
                                                    };
                                                loteIDGuardar = loteBL.GuardaLote(loteNuevo);
                                            }
                                            else
                                            {
                                                loteIDGuardar = loteDestinoCorraleta.LoteID;
                                                loteDestinoCorraleta.Cabezas = loteDestinoCorraleta.Cabezas + 1;

                                                var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                                                {
                                                    LoteIDOrigen = 0,
                                                    LoteIDDestino = loteDestinoCorraleta.LoteID,
                                                    CabezasProcesadas = 1,
                                                    UsuarioModificacionID = usuarioCreacion
                                                };
                                                loteBl.ActualizarCabezasProcesadas(filtroActualizaCabezas);

                                                //loteBl.ActualizaNoCabezasEnLote(loteDestinoCorraleta, loteDestinoCorraleta);
                                            }

                                            if (ultimoMovimiento != null)
                                            {
                                                var animalMovimientoOrigen = new AnimalMovimientoInfo
                                                {
                                                    AnimalID = animal.AnimalID,
                                                    CorralID = corralDestino.CorralID,
                                                    LoteID = loteIDGuardar,
                                                    TipoMovimientoID = tipoMovimiento,
                                                    OrganizacionID = organizacion,
                                                    OperadorID = operador,
                                                    TrampaID = ultimoMovimiento.TrampaID,
                                                    Peso = ultimoMovimiento.Peso,
                                                    Temperatura = ultimoMovimiento.Temperatura,
                                                    UsuarioCreacionID = usuarioCreacion
                                                };
                                                animalMovimientoBl.GuardarAnimalMovimiento(animalMovimientoOrigen);


                                                loteOrigen.Cabezas = loteOrigen.Cabezas - 1;

                                                var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                                                {
                                                    LoteIDOrigen = loteOrigen.LoteID,
                                                    LoteIDDestino = 0,
                                                    CabezasProcesadas = 1,
                                                    UsuarioModificacionID = usuarioCreacion
                                                };
                                                loteBl.ActualizarCabezasProcesadas(filtroActualizaCabezas);

                                            }
                                            else
                                            {
                                                retorno = -1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        retorno = -1;
                                    }
                                }
                                else
                                {
                                    retorno = -1;
                                }
                            }
                            else
                            {
                                retorno = -1;
                            }
                        }
                        else//Salida por sacrificio
                        {
                            // Se crean los movimientos del animal
                            retorno = GuardarSalidaIndividialSacrificio(animal, organizacion, codigoCorral, corraletaID, usuarioCreacion, tipoMovimiento, operador);

                            // Se crea registro en animal Salida
                            retorno = salidaIndividualDAL.Guardar(arete, organizacion, codigoCorral,
                                                                  corraletaID, usuarioCreacion, tipoMovimiento);
                        }
                    }
                    else
                    {
                        retorno = -1;
                    }
                    transaccion.Complete();
                }

                return retorno;
            }
            catch (ExcepcionGenerica)
            {
                retorno = -1;
                throw;
            }
            catch (Exception ex)
            {
                retorno = -1;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para guardar la salida a sacrificio a un animal
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="organizacion"></param>
        /// <param name="codigoCorral"></param>
        /// <param name="corraletaID"></param>
        /// <param name="usuarioCreacion"></param>
        /// <param name="tipoMovimiento"></param>
        /// <param name="operador"></param>
        private int GuardarSalidaIndividialSacrificio(AnimalInfo animal, int organizacion, string codigoCorral, int corraletaID, int usuarioCreacion, int tipoMovimiento, int operador)
        {
            int retorno = 0;
            var animalBl = new AnimalBL();
            var corralBl = new CorralBL();
            var animalMovimientoBl = new AnimalMovimientoBL();
            var loteBl = new LoteBL();

            var corralOrigen = corralBl.ObtenerCorralPorCodigo(organizacion, codigoCorral);
            if (corralOrigen != null)
            {
                var loteOrigen = loteBl.DeteccionObtenerPorCorral(organizacion, corralOrigen.CorralID);
                if (loteOrigen != null)
                {
                    var corralDestino = corralBl.ObtenerPorId(corraletaID);
                    if (corralDestino != null)
                    {
                        var loteDestino = loteBl.DeteccionObtenerPorCorral(organizacion, corralDestino.CorralID);
                        if (loteDestino == null)
                        {
                            // Si el corral destino no tiene lo te se crea uno
                            var loteBL = new LoteBL();
                            int loteIDGuardar = 0;
                            var loteNuevo = new LoteInfo
                            {
                                Activo = EstatusEnum.Activo,
                                Cabezas = 0,
                                CabezasInicio = 0,
                                CorralID = corralDestino.CorralID,
                                DisponibilidadManual = false,
                                OrganizacionID = organizacion,
                                TipoCorralID = corralDestino.TipoCorral.TipoCorralID,
                                TipoProcesoID = TipoProceso.EngordaPropio.GetHashCode(),
                                UsuarioCreacionID = usuarioCreacion
                            };
                            loteIDGuardar = loteBL.GuardaLote(loteNuevo);
                            loteDestino = new LoteInfo {LoteID = loteIDGuardar};
                        }
                        // Se obtiene la info del ultimo movimiento del animal
                        var ultimoMovimiento = animalBl.ObtenerUltimoMovimientoAnimal(animal);
                        if (ultimoMovimiento != null)
                        {
                            var animalMovimientoOrigen = new AnimalMovimientoInfo
                            {
                                AnimalID = animal.AnimalID,
                                CorralID = corralDestino.CorralID,
                                LoteID = loteDestino.LoteID,
                                TipoMovimientoID = tipoMovimiento,
                                OperadorID = operador,
                                OrganizacionID = organizacion,
                                TrampaID = ultimoMovimiento.TrampaID,
                                Peso = ultimoMovimiento.Peso,
                                Temperatura = ultimoMovimiento.Temperatura,
                                UsuarioCreacionID = usuarioCreacion
                            };
                            animalMovimientoBl.GuardarAnimalMovimiento(animalMovimientoOrigen);

                            loteOrigen.Cabezas = loteOrigen.Cabezas - 1;
                            loteDestino.Cabezas = loteDestino.Cabezas + 1;
                            loteDestino.CabezasInicio = loteDestino.CabezasInicio + 1;

                            var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                            {
                                LoteIDOrigen = loteOrigen.LoteID,
                                LoteIDDestino = loteDestino.LoteID,
                                CabezasProcesadas = 1,
                                UsuarioModificacionID = usuarioCreacion
                            };
                            loteBl.ActualizarCabezasProcesadas(filtroActualizaCabezas);

                            //loteBl.ActualizaNoCabezasEnLote(loteDestino, loteOrigen);
                        }
                        else
                        {
                            retorno = -1;
                        }
                        
                    }
                    else
                    {
                        retorno = -1;
                    }
                }
                else
                {
                    retorno = -1;
                }
            }
            else
            {
                retorno = -1;
            }

            return retorno;
        }

        /// <summary>
        /// Método para obtener el ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal int ObtenerTicket(TicketInfo ticket)
        {
            int retorno = 0;
            try
            {
                Logger.Info();
                var salidaIndividualDAL = new SalidaIndividualDAL();
                retorno = salidaIndividualDAL.ObtenerTicket(ticket);
                return retorno;
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
        /// Metodo que le da salida por venta al ganado
        /// </summary>
        /// <param name="salidaIndividual"></param>
        /// <returns></returns>
        internal MemoryStream GuardarSalidaIndividualGanadoVenta(SalidaIndividualInfo salidaIndividual)
        {
            MemoryStream retorno = null;
            try
            {
                PolizaAbstract poliza;
                IList<PolizaInfo> polizaSalidaVenta;
                TicketInfo Ticket = new TicketInfo();

                Ticket.FolioTicket = salidaIndividual.FolioTicket;
                Ticket.Organizacion = salidaIndividual.Organizacion;
                Ticket.TipoVenta = salidaIndividual.TipoVenta;

                var ventaGanadoBL = new VentaGanadoBL();

                VentaGanadoInfo salidaPorVentaGanado = ventaGanadoBL.ObtenerVentaGanadoPorTicket(Ticket);
               

                var loteBL = new LoteBL();
                LoteInfo lote = loteBL.ObtenerPorID(salidaPorVentaGanado.LoteID);
                lote.Cabezas -= salidaIndividual.NumeroDeCabezas;

                AnimalBL animalBl = new AnimalBL();

                using (var scope = new TransactionScope())
                {
                    Logger.Info();
                    var salidaIndividualDAL = new SalidaIndividualDAL();

                    VentaGanadoDetalleBL ventaGanadoDetalleBl = new VentaGanadoDetalleBL();
                    List<VentaGanadoDetalleInfo> listaVentaGanadoDetalle = ventaGanadoDetalleBl.ObtenerVentaGanadoPorTicket(salidaPorVentaGanado.VentaGanadoID);
                    List<AnimalCostoInfo> animalCosto = new List<AnimalCostoInfo>();
                    AnimalCostoBL animalCostoBl = new AnimalCostoBL();
                    List<AnimalInfo> animalesConsulta = new List<AnimalInfo>();
                    List<AnimalInfo> animalesProcesar = new List<AnimalInfo>();
                    foreach (VentaGanadoDetalleInfo ventaGanado in listaVentaGanadoDetalle)
                    {
                        animalesConsulta = new List<AnimalInfo>();
                        ventaGanado.Animal = animalBl.ObtenerAnimalPorArete(ventaGanado.Arete, Ticket.Organizacion);

                        if (ventaGanado.Animal == null)
                        {
                            ventaGanado.Animal = animalBl.ObtenerAnimalPorAreteTestigo(ventaGanado.AreteMetalico, Ticket.Organizacion);
                        }
                        animalesConsulta.Add(ventaGanado.Animal);
                        animalCosto = animalCostoBl.ObtenerCostosAnimal(animalesConsulta);

                        if (!animalCosto.Any(registro => registro.CostoID == Costo.CostoGanado.GetHashCode()))
                        {
                            animalesProcesar.Add(ventaGanado.Animal);
                        }
                    }

                    if (animalesProcesar != null && animalesProcesar.Count > 0)
                    {
                        animalBl.ProcesoGenerarCostos(animalesProcesar);
                    }

                    salidaIndividualDAL.GuardarSalidaIndividualGanadoVenta(salidaIndividual);
                    salidaIndividualDAL.GuardarCostosHistoricos(salidaIndividual);
                    salidaIndividualDAL.GuardarConsumoHistoricos(salidaIndividual);
                    salidaIndividualDAL.GuardarAnimalHistoricos(salidaIndividual);

                    var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                    {
                        LoteIDOrigen = lote.LoteID,
                        LoteIDDestino = 0,
                        CabezasProcesadas = salidaIndividual.NumeroDeCabezas,
                        UsuarioModificacionID = salidaIndividual.Usuario
                    };
                    loteBL.ActualizarCabezasProcesadas(filtroActualizaCabezas);

                    //loteBL.ActualizaNoCabezasEnLote(lote, lote);

                    #region Poliza

                    List<ContenedorVentaGanado> ventasGanado =
                        ventaGanadoBL.ObtenerVentaGanadoPorTicketPoliza(Ticket.FolioTicket, Ticket.Organizacion);
                    

                    if (ventasGanado != null && ventasGanado.Any())
                    {
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVenta);
                        polizaSalidaVenta = poliza.GeneraPoliza(ventasGanado);
                        if (polizaSalidaVenta != null && polizaSalidaVenta.Any())
                        {
                            polizaSalidaVenta.ToList().ForEach(
                                org =>
                                {
                                    org.OrganizacionID = Ticket.Organizacion;
                                    org.UsuarioCreacionID = salidaIndividual.Usuario;
                                    org.ArchivoEnviadoServidor = 1;
                                });
                            retorno = poliza.ImprimePoliza(ventasGanado, polizaSalidaVenta);
                            var polizaBL = new PolizaBL();
                            polizaBL.GuardarServicioPI(polizaSalidaVenta, TipoPoliza.SalidaVenta);
                        }
                    }

                    // Genera el xml y lo guarda en la ruta especificada en la configuración
                    var facturaBl = new FacturaBL();
                    facturaBl.GenerarDatosFacturaVentaDeGanado(Ticket.FolioTicket, Ticket.Organizacion);

                    #endregion Poliza

                    scope.Complete();
                }
                return retorno;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
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
        /// Metodo que da salida por venta al ganado intensivo cuano se selcciona tipoventa Externo
        /// </summary>
        /// <param name="salidaIndividual"></param>
        /// <returns></returns>
        internal MemoryStream GuardarSalidaGanadoVentaIntensiva(SalidaIndividualInfo salidaIndividual)
        {
            MemoryStream retorno = null;
            try
            {
                PolizaAbstract poliza;
                IList<PolizaInfo> polizaSalidaVenta;
                TicketInfo Ticket = new TicketInfo();
                CorralInfo corralInfo = new CorralInfo();
                CorralBL corralB = new CorralBL();
                GanadoIntensivoBL salidaGanadoCosto = new GanadoIntensivoBL();
                GanadoIntensivoInfo salidaGanadoInfo = new GanadoIntensivoInfo();
                

                corralInfo = corralB.ObtenerCorralActivoPorCodigo(salidaIndividual.Organizacion, salidaIndividual.Corral);
                corralInfo.Organizacion = new OrganizacionInfo();
                corralInfo.Organizacion.OrganizacionID = salidaIndividual.Organizacion;
                salidaGanadoInfo =  salidaGanadoCosto.ObtenerMuerteGanadoIntensivo(corralInfo);
                salidaGanadoInfo.Organizacion = new OrganizacionInfo();
                salidaGanadoInfo.Organizacion.OrganizacionID = salidaIndividual.Organizacion;
                salidaGanadoInfo.Observaciones = "";



                Ticket.FolioTicket = salidaIndividual.FolioTicket;
                Ticket.Organizacion = salidaIndividual.Organizacion;

                var ventaGanadoBL = new VentaGanadoBL();

                VentaGanadoInfo salidaPorVentaGanado = ventaGanadoBL.ObtenerVentaGanadoPorTicket(Ticket);

                var loteBL = new LoteBL();
                CorralBL corralBl = new CorralBL();
                LoteInfo lote = loteBL.ObtenerPorID(salidaPorVentaGanado.LoteID);
                int iCabezasTotalLote = lote.Cabezas;

                lote.Cabezas -= salidaIndividual.NumeroDeCabezas;
                //lote.Corral = corralBl.ObtenerCorralPorLoteID(lote.LoteID, salidaIndividual.Organizacion);
                //lote.Corral = corralBl.ObtenerCorralPorCodigo()

                using (var scope = new TransactionScope())
                {
                    Logger.Info();
                    var salidaIndividualDAL = new SalidaIndividualDAL();

                    var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                    {
                        LoteIDOrigen = lote.LoteID,
                        LoteIDDestino = 0,
                        CabezasProcesadas = salidaIndividual.NumeroDeCabezas,
                        UsuarioModificacionID = salidaIndividual.Usuario
                    };
                    loteBL.ActualizarCabezasProcesadas(filtroActualizaCabezas);

                    salidaGanadoInfo.ListaGanadoIntensivoCosto = new List<GanadoIntensivoCostoInfo>();
                    foreach (
                        var entradaCosteo in
                            salidaGanadoInfo.EntradaGanadoCosteo.ListaCostoEntrada.OrderBy(id => id.Costo.CostoID))
                    {
                        var costoInt = new GanadoIntensivoCostoInfo();
                        costoInt.Costos = new CostoInfo();
                        costoInt.Costos = entradaCosteo.Costo;
                        costoInt.Costos.ImporteCosto = (entradaCosteo.Importe/
                                                        salidaGanadoInfo.EntradaGanado.CabezasRecibidas)*
                                                        salidaIndividual.NumeroDeCabezas;
                        costoInt.Importe = costoInt.Costos.ImporteCosto;
                        salidaGanadoInfo.ListaGanadoIntensivoCosto.Add(costoInt);
                    }
                    salidaGanadoInfo.TipoMovimientoID = TipoMovimiento.SalidaVentaIntensivo;
                    //salidaIndividualDAL.GuardarSalidaIndividualGanadoVenta(salidaIndividual);
                    salidaGanadoInfo.TotalCabezas = salidaIndividual.NumeroDeCabezas;
                    salidaGanadoInfo.FolioTicket = salidaIndividual.FolioTicket;
                    salidaGanadoInfo.TipoFolio = TipoFolio.VentaGanadoIntensivo;
                    salidaGanadoInfo.PesoBruto = salidaIndividual.PesoBruto;
                    salidaGanadoInfo.UsuarioCreacionID = salidaIndividual.Usuario;
                    salidaGanadoInfo.Organizacion.OrganizacionID = salidaIndividual.Organizacion;
                    salidaGanadoInfo.CabezasText = salidaIndividual.NumeroDeCabezas.ToString();
                    salidaGanadoCosto.Guardar(salidaGanadoInfo);

                    #region Poliza

                    List<ContenedorVentaGanado> ventasGanado =
                        ventaGanadoBL.ObtenerVentaGanadoIntensivoPorTicketPoliza(Ticket);

                    if (ventasGanado != null && ventasGanado.Any())
                    {
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVentaIntensiva);
                        for (int i = 0; i < ventasGanado.Count; i++)
                        {
                            ventasGanado[i].EntradaGandoId = salidaGanadoInfo.EntradaGanado.EntradaGanadoID;
                            ventasGanado[i].OrganizacionId = Ticket.Organizacion;
                            ventasGanado[i].Lote = lote;
                        }

                        polizaSalidaVenta = poliza.GeneraPoliza(ventasGanado);
                        if (polizaSalidaVenta != null && polizaSalidaVenta.Any())
                        {
                            polizaSalidaVenta.ToList().ForEach(
                                org =>
                                {
                                    org.OrganizacionID = Ticket.Organizacion;
                                    org.UsuarioCreacionID = salidaIndividual.Usuario;
                                    org.ArchivoEnviadoServidor = 1;
                                });
                            retorno = poliza.ImprimePoliza(ventasGanado, polizaSalidaVenta);
                            var polizaBL = new PolizaBL();
                            polizaBL.GuardarServicioPI(polizaSalidaVenta, TipoPoliza.SalidaVentaIntensiva);
                        }
                    }

                    // Genera el xml y lo guarda en la ruta especificada en la configuración
                    var facturaBl = new FacturaBL();
                    facturaBl.GenerarDatosFacturaVentaDeGanadoIntensivo(Ticket.FolioTicket, Ticket.Organizacion);

                    #endregion Poliza

                    scope.Complete();
                }
            
                return retorno;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
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

        private void GenerarMovimientosAnimalesVendidosIntensivo(List<AnimalInfo> animalesMuertos, SalidaIndividualInfo salidaIndividualInfo)
        {
            var animalMovimientoBL = new AnimalMovimientoBL();
            var operadorBL = new OperadorBL();
            var animalBL = new AnimalBL();
            var listaAnimalesMuertos = new List<AnimalInfo>();
            int pesoPromedio = (int) (salidaIndividualInfo.PesoBruto - salidaIndividualInfo.Peso)/
                               salidaIndividualInfo.NumeroDeCabezas;
            foreach (var animalMuerto in animalesMuertos)
            {
                var animalInfo = animalBL.ObtenerAnimalAnimalID(animalMuerto.AnimalID);
                AnimalMovimientoInfo ultimoMovimientoAnimal = null;
                List<AnimalMovimientoInfo> ultimosMovimiento = animalMovimientoBL.ObtenerUltimoMovimientoAnimal(new List<AnimalInfo> { animalInfo });
                if (ultimosMovimiento != null && ultimosMovimiento.Any())
                {
                    ultimoMovimientoAnimal =
                        ultimosMovimiento.OrderByDescending(ani => ani.AnimalMovimientoID).FirstOrDefault();
                }
                /* Insertamos el movimiento de Muerte */
                
                /* Se genera el animal Movimiento para almacenarlo*/
                if (ultimoMovimientoAnimal != null)
                {
                    OperadorInfo operador = operadorBL.ObtenerPorUsuarioId(salidaIndividualInfo.Usuario, salidaIndividualInfo.Organizacion);

                    var animalMovimientoInfo = new AnimalMovimientoInfo
                    {
                        AnimalID = animalInfo.AnimalID,
                        OrganizacionID = ultimoMovimientoAnimal.OrganizacionID,
                        CorralID = ultimoMovimientoAnimal.CorralID,
                        LoteID = ultimoMovimientoAnimal.LoteID,
                        Peso = pesoPromedio,
                        Temperatura = 0,
                        TipoMovimientoID = (int)TipoMovimiento.SalidaPorVenta,
                        TrampaID = ultimoMovimientoAnimal.TrampaID,
                        OperadorID = operador != null ? operador.OperadorID : 0,
                        Observaciones = string.Empty,
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionID = salidaIndividualInfo.Usuario
                    };
                    /* Se almacena el animalMovimiento */
                    animalMovimientoBL.GuardarAnimalMovimiento(animalMovimientoInfo);
                    animalInfo.UsuarioModificacionID = salidaIndividualInfo.Usuario;
                    /*  Se da de baja el animal */
                    animalBL.InactivarAnimal(animalInfo);

                    /* Enviar al historico el animal inactivo */
                    listaAnimalesMuertos.Add(animalInfo);

                }
            }
            if (listaAnimalesMuertos.Any())
            {
                animalBL.EnviarAHistorico(listaAnimalesMuertos);
            }
        }
    }
}
 