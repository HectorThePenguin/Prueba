using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    internal class GanadoIntensivoBL
    {
        /// <summary>
        /// Obtener muerte de ganado intensivo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal GanadoIntensivoInfo ObtenerMuerteGanadoIntensivo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var muerteGanadoIntensivoDAL = new GanadoIntensivoDAL();
                GanadoIntensivoInfo muerteGanadoIntensivoInfo = new GanadoIntensivoInfo();
                var loteBL = new LoteBL();
                var entradaGanadoBL = new EntradaGanadoBL();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                LoteInfo lote = loteBL.ObtenerLotesActivos(corral.Organizacion.OrganizacionID, corral.CorralID);
                if (lote != null)
                {
                    muerteGanadoIntensivoInfo.Lote = lote;
                    muerteGanadoIntensivoInfo.TotalCabezas = lote.Cabezas;
                    var entrada = entradaGanadoBL.ObtenerEntradaGanadoLoteCorral(lote);
                    if (entrada != null)
                    {
                        muerteGanadoIntensivoInfo.EntradaGanado = entrada;
                        EntradaGanadoCosteoInfo entradaGanadoCosteo = entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entrada.EntradaGanadoID);
                        if (entradaGanadoCosteo != null)
                        {
                            muerteGanadoIntensivoInfo.EntradaGanadoCosteo = entradaGanadoCosteo;
                        }
                    }
                }
                return muerteGanadoIntensivoInfo;
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
        /// Se calcula costos de las cabezas.
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        internal GanadoIntensivoInfo CalcularCostosDeCabezas(GanadoIntensivoInfo contexto)
        {
            List<GanadoIntensivoCostoInfo> listaganGanadoIntensivoCosto = new List<GanadoIntensivoCostoInfo>();
            try
            {
                Logger.Info();
                if (contexto.EntradaGanadoCosteo != null)
                {
                    if (contexto.EntradaGanadoCosteo.ListaCostoEntrada != null && contexto.EntradaGanadoCosteo.ListaCostoEntrada.Any())
                    {
                        List<int> ListaCostos =
                            contexto.EntradaGanadoCosteo.ListaCostoEntrada.Select(lista => lista.Costo.CostoID)
                                .Distinct()
                                .ToList();
                        
                        foreach (var costoID in ListaCostos)
                        {
                            decimal importe =
                                contexto.EntradaGanadoCosteo.ListaCostoEntrada.Where(
                                    lista => lista.Costo.CostoID == costoID).Sum(imp => imp.Importe);
                                var ganadoIntensivoCostoInfo = new GanadoIntensivoCostoInfo
                                {
                                    Costos = new CostoInfo
                                    {
                                        CostoID = costoID
                                    },
                                    Importe = Math.Round(((importe / contexto.EntradaGanado.CabezasRecibidas) * contexto.Cabezas),2)
                                };
                                listaganGanadoIntensivoCosto.Add(ganadoIntensivoCostoInfo);
                        }
                    }
                }
                if (listaganGanadoIntensivoCosto.Any())
                {
                    decimal importeTotal = listaganGanadoIntensivoCosto.Sum(imp => imp.Importe);
                    contexto.Importe = importeTotal;
                }
                contexto.ListaGanadoIntensivoCosto = listaganGanadoIntensivoCosto;
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
            return contexto;
            
        }

        public int Guardar(GanadoIntensivoInfo ganadoIntensivo)
        {
            try
            {
                int result;
                Logger.Info();
                
                    var ganadoIntensivoDAL = new GanadoIntensivoDAL();
                    var loteBL = new LoteBL();
                    result = ganadoIntensivoDAL.Guardar(ganadoIntensivo);
                    var filtroActualizarCabezasLote = new FiltroActualizarCabezasLote
                    {
                        CabezasProcesadas = ganadoIntensivo.Cabezas,
                        LoteIDOrigen = ganadoIntensivo.Lote.LoteID,
                        UsuarioModificacionID = ganadoIntensivo.UsuarioCreacionID,
                        LoteIDDestino = 0
                    };
                    //loteBL.ActualizarCabezasProcesadas(filtroActualizarCabezasLote);
                
                
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



        /// <summary>
        /// Guardar muerte de ganado intenvio
        /// </summary>
        /// <param name="ganadoIntensivo"></param>
        /// <returns></returns>
        public GanadoIntensivoInfo MuerteGanadoIntensivo_Guardar(GanadoIntensivoInfo ganadoIntensivo)
        {
            try
            {
                int result;
                Logger.Info();
                MemoryStream polizaPDF = null;
                GanadoIntensivoInfo resultado = new GanadoIntensivoInfo();
                using (var transaction = new TransactionScope())
                {
                    var loteBL = new LoteBL();
                    PolizaAbstract poliza;
                    IList<PolizaInfo> polizaSalidaMuerteIntensiva;
                    result = Guardar(ganadoIntensivo);
                    resultado.FolioTicket = result;
                    ganadoIntensivo.FolioTicket = result;
                    var filtroActualizarCabezasLote = new FiltroActualizarCabezasLote
                    {
                        CabezasProcesadas = ganadoIntensivo.Cabezas,
                        LoteIDOrigen = ganadoIntensivo.Lote.LoteID,
                        UsuarioModificacionID = ganadoIntensivo.UsuarioCreacionID,
                        LoteIDDestino = 0
                    };
                    loteBL.ActualizarCabezasProcesadas(filtroActualizarCabezasLote);

                    #region Poliza


                    if (ganadoIntensivo != null)
                    {
                        if (ganadoIntensivo.ListaGanadoIntensivoCosto != null &&
                            ganadoIntensivo.ListaGanadoIntensivoCosto.Any())
                        {
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaMuerteIntensiva);
                            polizaSalidaMuerteIntensiva = poliza.GeneraPoliza(ganadoIntensivo);
                            if (polizaSalidaMuerteIntensiva != null && polizaSalidaMuerteIntensiva.Any())
                            {
                                polizaSalidaMuerteIntensiva.ToList().ForEach(
                                    org =>
                                    {
                                        org.OrganizacionID = ganadoIntensivo.Organizacion.OrganizacionID;
                                        org.UsuarioCreacionID = ganadoIntensivo.UsuarioCreacionID;
                                        org.ArchivoEnviadoServidor = 1;
                                    });
                                polizaPDF = poliza.ImprimePoliza(ganadoIntensivo, polizaSalidaMuerteIntensiva);
                                var polizaBL = new PolizaBL();
                                polizaBL.GuardarServicioPI(polizaSalidaMuerteIntensiva, TipoPoliza.SalidaMuerteIntensiva);
                                resultado.stream = polizaPDF;


                            }
                        }
                    }
                    #endregion Poliza
                    transaction.Complete();
                }

                return resultado;

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

        private void GenerarMovimientosAnimalesMuertos(List<AnimalInfo> animalesMuertos, GanadoIntensivoInfo ganadoIntensivo, decimal pesoMuertoTotal)
        {
            var animalMovimientoBL = new AnimalMovimientoBL();
            var operadorBL = new OperadorBL();
            var animalBL = new AnimalBL();
            var listaAnimalesMuertos = new List<AnimalInfo>();
            int pesoPromedio = (int)pesoMuertoTotal / ganadoIntensivo.Cabezas;
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
                OperadorInfo operador = operadorBL.ObtenerPorUsuarioId(ganadoIntensivo.UsuarioCreacionID, ganadoIntensivo.Organizacion.OrganizacionID);
                /* Se genera el animal Movimiento para almacenarlo*/
                if (ultimoMovimientoAnimal != null)
                {
                    var animalMovimientoInfo = new AnimalMovimientoInfo
                                               {
                                                   AnimalID = animalInfo.AnimalID,
                                                   OrganizacionID = ultimoMovimientoAnimal.OrganizacionID,
                                                   CorralID = ultimoMovimientoAnimal.CorralID,
                                                   LoteID = ultimoMovimientoAnimal.LoteID,
                                                   Peso = pesoPromedio,
                                                   Temperatura = 0,
                                                   TipoMovimientoID = (int) TipoMovimiento.SalidaPorVenta,
                                                   TrampaID = ultimoMovimientoAnimal.TrampaID,
                                                   OperadorID = operador != null ? operador.OperadorID : 0,
                                                   Observaciones = ganadoIntensivo.Observaciones,
                                                   Activo = EstatusEnum.Activo,
                                                   UsuarioCreacionID = ganadoIntensivo.UsuarioCreacionID
                                               };
                    /* Se almacena el animalMovimiento */
                    animalMovimientoBL.GuardarAnimalMovimiento(animalMovimientoInfo);
                    animalInfo.UsuarioModificacionID = ganadoIntensivo.UsuarioCreacionID;
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
