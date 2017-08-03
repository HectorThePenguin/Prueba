using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using System.Transactions;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using System.Linq;
using System.IO;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Servicios.BL
{
    internal class MuertesEnTransitoBL
    {

        internal MemoryStream Guardar(MuertesEnTransitoInfo muerteEnTransito, List<AnimalInfo> animales)
        {
            MemoryStream retorno = null;
            try
            {
                Logger.Info();
                PolizaAbstract poliza;
                IList<PolizaInfo> polizaMuerteEnTransito;
                var entradaGanadoMuerteLista = new List<EntradaGanadoMuerteInfo>();

                using (var transaction = new TransactionScope())
                {
                    var fecha = DateTime.Now;
                    int organizacionID = muerteEnTransito.OrganizacionID;
                    int usuarioCreacionID = muerteEnTransito.UsuarioCreacionID;
                    int pesoAnimal = 0;
                    int corralID = 0;
                    int loteID = 0;

                    var datosInterface = false;
                    List<InterfaceSalidaAnimalInfo> interfaceSalidaAnimal = null;
                    var entradaGanadoBL = new EntradaGanadoBL();
                    EntradaGanadoInfo entradaGanado = entradaGanadoBL.ObtenerPorID(muerteEnTransito.EntradaGanadoID);
                    if (entradaGanado != null)
                    {
                        loteID = entradaGanado.LoteID;
                        corralID = entradaGanado.CorralID;
                        if (entradaGanado.FolioOrigen > 0)
                        {
                            var interfaceSalidaBL = new InterfaceSalidaAnimalBL();
                            interfaceSalidaAnimal =
                                interfaceSalidaBL.ObtenerInterfazSalidaAnimal(entradaGanado.FolioOrigen,
                                                                              entradaGanado.OrganizacionOrigenID);
                            if (interfaceSalidaAnimal == null || interfaceSalidaAnimal.Count <= 0)
                            {
                                fecha = entradaGanado.FechaEntrada;
                                pesoAnimal = Convert.ToInt32((entradaGanado.PesoBruto - entradaGanado.PesoTara) /
                                                             entradaGanado.CabezasRecibidas);
                            }
                            else
                            {
                                datosInterface = true;
                            }
                        }
                        else
                        {
                            pesoAnimal = Convert.ToInt32((entradaGanado.PesoBruto - entradaGanado.PesoTara) /
                                                         entradaGanado.CabezasRecibidas);
                        }
                    }
                    // Generamos el Folio

                    FolioBL folioBL = new FolioBL();
                    var folioMuerte = folioBL.ObtenerFolio(organizacionID, TipoFolio.MuertesEnTransito);

                    foreach (var animal in animales)
                    {
                        if (datosInterface)
                        {
                            pesoAnimal = interfaceSalidaAnimal.Where(
                                are => are.Arete.Equals(animal.Arete)).Select(
                                    peso => Convert.ToInt32(peso.PesoCompra)).FirstOrDefault();
                        }

                        if (datosInterface)
                        {
                            fecha =
                                interfaceSalidaAnimal.Where(arete => arete.Arete.Equals(animal.Arete)).Select(
                                    fechaItz => fechaItz.FechaCompra).FirstOrDefault();
                        }

                        if (fecha == new DateTime(1, 1, 1))
                            fecha = DateTime.Now;

                        animal.OrganizacionIDEntrada = organizacionID;
                        animal.FolioEntrada = muerteEnTransito.FolioEntrada;
                        animal.UsuarioCreacionID = usuarioCreacionID;
                        animal.ClasificacionGanadoID = 1;
                        animal.CalidadGanadoID = 1;
                        animal.TipoGanadoID = 1;
                        animal.FechaCompra = fecha;
                        animal.PesoCompra = pesoAnimal;
                        animal.Activo = false;
                    }

                    //Guardamos los Animales
                    var animalBL = new AnimalBL();
                    animalBL.GuardarAnimal(animales);

                    //Obtenemos los animales almacenados con su 
                    animales = animalBL.ObtenerPorArete(animales);

                    foreach (AnimalInfo animal in animales)
                    {
                        animal.UsuarioCreacionID = usuarioCreacionID;
                    }

                    //Genera los costos por animal
                    animales = animalBL.ProcesoGenerarCostos(animales);

                    foreach (AnimalInfo animal in animales)
                    {
                        var entradaGanadoMuerte = new EntradaGanadoMuerteInfo
                        {
                            EntradaGanado =
                                new EntradaGanadoInfo
                                {
                                    EntradaGanadoID =
                                        muerteEnTransito.EntradaGanadoID,
                                },
                            Animal = new AnimalInfo { Arete = animal.Arete },
                            FolioMuerte = folioMuerte,
                            Activo = EstatusEnum.Activo,
                            UsuarioCreacionID = usuarioCreacionID,
                            Peso = animal.PesoCompra,
                            EntradaGanadMuerteDetalle = new List<EntradaGanadoMuerteDetalleInfo>(),
                            ProveedorFletes = new ProveedorInfo() { CodigoSAP = muerteEnTransito.CodigoProveedor },
                            Cliente = new ClienteInfo(){CodigoSAP = muerteEnTransito.Cliente.CodigoSAP, Descripcion = muerteEnTransito.Cliente.Descripcion},
                            OrganizacionDestinoID = entradaGanado.OrganizacionID
                        };

                        if (animal.ListaCostosAnimal != null)
                        {
                            entradaGanadoMuerte.EntradaGanadMuerteDetalle =
                                animal.ListaCostosAnimal.Select(im => new EntradaGanadoMuerteDetalleInfo
                                {
                                    Costo = new CostoInfo
                                    {
                                        CostoID = im.CostoID
                                    },
                                    Importe = im.Importe,
                                    EntradaGanadoMuerte = entradaGanadoMuerte
                                }).ToList();
                        }
                        entradaGanadoMuerteLista.Add(entradaGanadoMuerte);
                    }

                    if (interfaceSalidaAnimal != null && interfaceSalidaAnimal.Count > 0 && animales != null && animales.Count > 0)
                    {
                        var interfaceSalidaBL = new InterfaceSalidaAnimalBL();
                        foreach (AnimalInfo animal in animales)
                        {
                            InterfaceSalidaAnimalInfo interfaceSalidaAnimalIndividual = interfaceSalidaAnimal.FirstOrDefault(animalInfo => animalInfo.Arete.Equals(animal.Arete));
                            interfaceSalidaBL.GuardarAnimalID(interfaceSalidaAnimalIndividual, animal.AnimalID);
                        }
                    }

                    //Asignamos el AnimalID en la lista de muertes ante sde guardar.
                    foreach (EntradaGanadoMuerteInfo muerte in entradaGanadoMuerteLista)
                    {
                        muerte.Animal.AnimalID = animales.Where(animal => animal.Arete.Equals(muerte.Animal.Arete))
                                                         .Select(animal => animal.AnimalID).FirstOrDefault();
						muerte.Peso = animales.Where(animal => animal.Arete.Equals(muerte.Animal.Arete))
										.Select(animal => animal.PesoCompra).FirstOrDefault();
                    }
                    //Guardamos las muertes en transito
                    var muertesEnTransitoDal = new MuertesEnTransitoDAL();
                    muertesEnTransitoDal.Guardar(entradaGanadoMuerteLista, muerteEnTransito.Cliente);

                    
                    //Creamos los movimientos para el animal
                    if (animales != null && animales.Any())
                    {
                        fecha = DateTime.Now;
                        var animalMovimientoBL = new AnimalMovimientoBL();
                        List<AnimalMovimientoInfo> movimientosAnimal = animales.Select(ani =>
                            new AnimalMovimientoInfo
                            {
                                AnimalID = ani.AnimalID,
                                Activo = EstatusEnum.Activo,
                                CorralID = corralID,
                                LoteID = loteID,
                                FechaMovimiento = fecha,
                                OrganizacionID = organizacionID,
                                TipoMovimientoID = TipoMovimiento.MuerteTransito.GetHashCode(),
                                OperadorID = 1,
                                TrampaID = 1,
                                Peso = ani.PesoCompra,
                                UsuarioCreacionID = usuarioCreacionID,
                            }).ToList();
                        animalMovimientoBL.GuardarAnimalMovimientoXML(movimientosAnimal);
                    }
                    //Afectamos el lote correspondiente.
                    var loteBl = new LoteBL();
                    var loteInfo = loteBl.ObtenerPorID(muerteEnTransito.LoteID);
                    loteInfo.UsuarioModificacionID = muerteEnTransito.UsuarioCreacionID;
                    loteInfo.Cabezas -= muerteEnTransito.MuertesTransito;

                    var filtro = new FiltroActualizarCabezasLote
                    {
                        CabezasProcesadas = muerteEnTransito.MuertesTransito,
                        LoteIDDestino = 0,
                        LoteIDOrigen = loteInfo.LoteID,
                        UsuarioModificacionID = usuarioCreacionID
                    };

                    var cabezasActualizadas = loteBl.ActualizarCabezasProcesadas(filtro);

                    //Executamos el SP CorteGanado_CierrePartidaPesoOrigenLLegada(Antes-->CorteGanado_PesoCompraDirecta), si se inactiva el lote.
                    if (cabezasActualizadas.CabezasOrigen <= 0)
                    {
                        //muertesEnTransitoDal.CorteGanado_PesoCompraDirecta(muerteEnTransito.OrganizacionID, muerteEnTransito.CorralID, muerteEnTransito.LoteID);
                        var corteGanadoBl = new CorteGanadoBL();
                        corteGanadoBl.ObtenerPesosOrigenLlegada(muerteEnTransito.OrganizacionID,
                                                                muerteEnTransito.CorralID,
                                                                muerteEnTransito.LoteID);
                    }

                    #region Poliza
                    //VentaGanadoBL ventaGanadoBL = new VentaGanadoBL();
                    //List<EntradaGanadoMuerteInfo> ventasGanado = ventaGanadoBL.ObtenerVentaGanadoPorTicketPoliza(folio, organizacionID);
                    //if (ventasGanado != null && ventasGanado.Any())

                    if (entradaGanadoMuerteLista != null && entradaGanadoMuerteLista.Any())
                    {
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaMuerteTransito);
                        polizaMuerteEnTransito = poliza.GeneraPoliza(entradaGanadoMuerteLista);
                        if (polizaMuerteEnTransito != null && polizaMuerteEnTransito.Any())
                        {
                            polizaMuerteEnTransito.ToList().ForEach(
                                org =>
                                {
                                    org.OrganizacionID = organizacionID;
                                    org.UsuarioCreacionID = usuarioCreacionID;
                                    org.ArchivoEnviadoServidor = 1;
                                });

                            entradaGanadoMuerteLista.ToList().ForEach(org =>
                                {
                                    org.EntradaGanado.Lote = entradaGanado.Lote;
                                    org.EntradaGanado.CodigoCorral = entradaGanado.CodigoCorral;
                                });
                            retorno = poliza.ImprimePoliza(entradaGanadoMuerteLista, polizaMuerteEnTransito);
                            var polizaBL = new PolizaBL();
                            polizaBL.GuardarServicioPI(polizaMuerteEnTransito, TipoPoliza.PolizaMuerteTransito);
                        }
                    }

                    // Genera el xml y lo guarda en la ruta especificada en la configuración
                    var facturaBl = new FacturaBL();
                    facturaBl.GenerarDatosFacturaMuertesEnTransito(folioMuerte, organizacionID);

                    #endregion Poliza

                    animalBL.EnviarAHistorico(animales);

                    transaction.Complete();
                }
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
        /// Obtiene las entradas de ganado con muertes pendientes a registrar en la ayuda Folio de Entrada.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<MuertesEnTransitoInfo> ObtenerPorPagina(PaginacionInfo pagina, MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var muertesDAL = new MuertesEnTransitoDAL();
                ResultadoInfo<MuertesEnTransitoInfo> result = muertesDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene la informacion de la entrada de gandado por Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal MuertesEnTransitoInfo ObtenerPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var muertesDAL = new MuertesEnTransitoDAL();
                MuertesEnTransitoInfo result = muertesDAL.ObtenerPorFolioEntrada(filtro);
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
        /// Obtiene los aretes de los animales que correspondan al Folio de Entrada y la Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var muertesDAL = new MuertesEnTransitoDAL();
                List<AnimalInfo> result = muertesDAL.ObtenerAnimalesPorFolioEntrada(filtro);
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

        internal int ObtenerTotalFoliosValidos(int organizacionId)
        {
            try
            {
                Logger.Info();
                var muertesDAL = new MuertesEnTransitoDAL();
                int result = muertesDAL.ObtenerTotalFoliosValidos(organizacionId);
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

        public ValidacionesFolioVentaMuerte ValidarFolio(int folioEntrada, int organizacionId, List<string> aretes)
        {
            ValidacionesFolioVentaMuerte result = new ValidacionesFolioVentaMuerte();
            try
            {
                Logger.Info();
                var muertesDAL = new MuertesEnTransitoDAL();
                result = muertesDAL.ValidarFolio(folioEntrada, organizacionId, aretes);
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
