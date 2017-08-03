using System;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.Polizas;

namespace SIE.Services.Servicios.BL
{
    /// <summary>
    /// Clase para adminitrar la capa de negocios de Muerte
    /// </summary>
    internal class MuerteBL
    {
        /// <summary>
        /// Obtiene la lista de ganado detectado, recolectado e ingresado a necropsia para su salida
        /// </summary>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerTodosMuertosParaNecropsia(int organizacionId)
        {
            IList<MuerteInfo> lista;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                lista = muerteDal.ObtenerMuertosParaNecropsia(organizacionId);
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
            return lista;
        }

        /// <summary>
        /// Obtiene la lista de problemas identificados para necropsia
        /// </summary>
        /// <returns></returns>
        internal IList<ProblemaInfo> ObtenerListaProblemasNecropsia()
        {
            IList<ProblemaInfo> lista;
            try
            {
                Logger.Info();
                var proDal = new ProblemaDAL();
                lista = proDal.ObtenerListaProblemasNecropsia();
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
            return lista;
        }

        /// <summary>
        /// Obtiene un ganado muerto por arete
        /// </summary>
        /// <param name="organizacionId">Id de la Organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        internal MuerteInfo ObtenerGanadoMuertoPorArete(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                retValue = muerteDal.ObtenerGanadoMuertoPorArete(organizacionId, numeroArete);
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
            return retValue;
        }

        /// <summary>
        /// Obtiene la lista de movimientos(Muertes) a cancelar
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerInformacionCancelarMovimiento(int organizacionId)
        {
            IList<MuerteInfo> lista;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                lista = muerteDal.ObtenerInformacionCancelarMovimiento(organizacionId);
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
            return lista;
        }

        /// <summary>
        /// Guarda la salida por muerte en necropsia
        /// </summary>
        /// <param name="muerte"></param>
        /// <returns></returns>
        internal int GuardarSalidaPorMuerteNecropsia(MuerteInfo muerte)
        {
            int retValue;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                var animalMovimientoBL = new AnimalMovimientoBL();
                var corralBL = new CorralBL();
                var loteBL = new LoteBL();
                var trampaBL = new TrampaBL();
                var entradaGanadoBL = new EntradaGanadoBL();
                var animalMovimientoInfo = new AnimalMovimientoInfo();
                var corralInfo = new CorralInfo();
                var muerteDAL = new MuerteDAL();
                PolizaAbstract poliza;
                IList<PolizaInfo> polizaSalidaMuerte;

                AnimalInfo animalInfo = null;
                using (var transaccion = new TransactionScope())
                {
                    //Se obtiene la info del Corral 
                    if (muerte.CorralId > 0)
                    {
                        corralInfo = corralBL.ObtenerPorId(muerte.CorralId);
                    }
                    else if (muerte.CorralCodigo != "")
                    {
                        corralInfo = corralBL.ObtenerCorralPorCodigo(muerte.OrganizacionId,
                            muerte.CorralCodigo);
                    }
                    //Se obtiene la info del Lote 
                    LoteInfo loteInfo;
                    if (muerte.LoteId > 0)
                    {
                        loteInfo = loteBL.ObtenerPorID(muerte.LoteId);
                    }
                    else
                    {
                        loteInfo = loteBL.DeteccionObtenerPorCorral(muerte.OrganizacionId, corralInfo.CorralID);
                    }
                    /* Se valida el Grupo del corral */
                    switch (corralInfo.GrupoCorral)
                    {
                        case (int)GrupoCorralEnum.Recepcion:
                            /* almacena el animal en el Inventario   */
                            animalInfo = new AnimalInfo
                            {
                                Arete = muerte.Arete,
                                AreteMetalico = muerte.AreteMetalico,
                                FechaCompra = DateTime.Now,
                                TipoGanadoID = 1,
                                CalidadGanadoID = 1,//***
                                ClasificacionGanadoID = 1,//***
                                PesoCompra = 0,
                                PesoLlegada = 0,//***
                                FolioEntrada = 1,
                                OrganizacionIDEntrada = muerte.OrganizacionId,
                                Paletas = 0,
                                Venta = false,
                                Cronico = false,
                                Activo = true,
                                UsuarioCreacionID = muerte.UsuarioCreacionID
                            };

                            var interfazAnimalBl = new InterfaceSalidaAnimalBL();
                            var interfaceSalidoAnimalInfo =
                                interfazAnimalBl.ObtenerNumeroAreteIndividual(muerte.Arete, muerte.OrganizacionId);
                            int folioEntradaInterfaz = 0;
                            if (interfaceSalidoAnimalInfo != null)
                            {
                                animalInfo.FechaCompra = interfaceSalidoAnimalInfo.FechaCompra;
                                animalInfo.PesoCompra = (int)interfaceSalidoAnimalInfo.PesoCompra;
                                animalInfo.TipoGanadoID = interfaceSalidoAnimalInfo.TipoGanado.TipoGanadoID;
                                folioEntradaInterfaz = interfaceSalidoAnimalInfo.Partida;
                            }

                            EntradaGanadoInfo entradaGanadoInterfaz =
                                entradaGanadoBL.ObtenerEntradasGanadoCosteado(folioEntradaInterfaz, muerte.OrganizacionId);

                            /* Obtener la entrada de ganado del Corral-Lote para conocer el origen */
                            EntradaGanadoInfo entradaGanadoInfo = entradaGanadoBL.ObtenerEntradaPorLote(loteInfo);
                            if (entradaGanadoInfo != null)
                            {
                                if (entradaGanadoInterfaz != null)
                                {
                                    if (entradaGanadoInfo.EmbarqueID == entradaGanadoInterfaz.EmbarqueID)
                                    {
                                        animalInfo.FolioEntrada = entradaGanadoInterfaz.FolioEntrada;
                                    }
                                }
                                if (animalInfo.FolioEntrada == 1)
                                {
                                    if (!entradaGanadoInfo.EsRuteo)
                                    {
                                        animalInfo.FolioEntrada = entradaGanadoInfo.FolioEntrada;
                                    }
                                    else
                                    {
                                        List<CabezasPartidasModel> partidasRuteo =
                                            entradaGanadoBL.ObtenerCabezasEntradasRuteo(entradaGanadoInfo.EmbarqueID);
                                        if (partidasRuteo != null && partidasRuteo.Any())
                                        {
                                            CabezasPartidasModel partidaConPendientes =
                                                partidasRuteo.OrderByDescending(part => part.CabezasPendientes).
                                                    FirstOrDefault();
                                            if (partidaConPendientes != null)
                                            {
                                                animalInfo.FolioEntrada = partidaConPendientes.FolioEntrada;
                                            }
                                        }
                                    }
                                }
                                //if (entradaGanadoInfo.TipoOrganizacionOrigenId != (int)TipoOrganizacion.CompraDirecta)
                                //{

                                //}
                                //animalInfo.FolioEntrada = entradaGanadoInfo.FolioEntrada;
                            }
                            /* Se almacena el animalMovimiento */
                            animalInfo = animalBL.GuardarAnimal(animalInfo);
                            break;
                        default:
                            if (muerte.AnimalId > 0)
                            {
                                animalInfo = animalBL.ObtenerAnimalAnimalID(muerte.AnimalId);
                                /* Se busca el animal En Salida Animal */
                                AnimalSalidaInfo animalSalidaInfo =
                                    animalBL.ObtenerAnimalSalidaAnimalID(animalInfo.AnimalID);
                                if (animalSalidaInfo != null)
                                {
                                    /* Se valida si no esta en una corraleta de sacrificio */
                                    CorralInfo corraleta = corralBL.ObtenerPorId(animalSalidaInfo.CorraletaId);
                                    if (corraleta != null &&
                                        corraleta.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                                    {
                                        /* Se disminuyen las cabezas del lote en todas las ordenes de sacrificio   */
                                        var ordenSacrificioBL = new OrdenSacrificioBL();
                                        ordenSacrificioBL.DecrementarAnimalMuerto(muerte.AnimalId);
                                        /* Eliminar AnimalSalida */
                                        animalBL.EliminarAnimalSalida(animalInfo.AnimalID, loteInfo.LoteID);
                                    }
                                }
                            }
                            else
                            {
                                var listaAnimales = animalBL.ObtenerAnimalesPorCorral(corralInfo, muerte.OrganizacionId);
                                if (listaAnimales != null) //&& listaAnimales.Any())
                                {
                                    //animalInfo = listaAnimales.FirstOrDefault(registro => registro.UsuarioCreacionID == 1);
                                    animalInfo = listaAnimales.FirstOrDefault(registro => registro.UsuarioCreacionID == 1);
                                    if (animalInfo == null)
                                    {
                                        animalInfo = listaAnimales.OrderBy(ani => ani.AnimalID).FirstOrDefault();
                                    }
                                }
                            }
                            break;
                    }

                    if (animalInfo != null)
                    {
                        animalInfo.UsuarioCreacionID = muerte.UsuarioCreacionID;
                        animalInfo.UsuarioModificacionID = muerte.UsuarioCreacionID;
                        if (muerte.AnimalId == 0)
                        {
                            muerte.AnimalId = animalInfo.AnimalID;
                        }
                    }

                    /* Se actualiza la muerte con salida por necropsia */
                    muerteDAL.GuardarSalidaPorMuerteNecropsia(muerte);
                    if (animalInfo != null)
                    {
                        if (muerte.Peso == 0)
                        {
                            List<AnimalMovimientoInfo> ultimosMovimiento = animalMovimientoBL.ObtenerUltimoMovimientoAnimal(new List<AnimalInfo> { animalInfo });
                            if (ultimosMovimiento != null && ultimosMovimiento.Any())
                            {
                                AnimalMovimientoInfo ultimoMovimientoAnimal =
                                    ultimosMovimiento.OrderByDescending(ani => ani.AnimalMovimientoID).FirstOrDefault();

                                if (ultimoMovimientoAnimal != null)
                                {
                                    muerte.Peso = ultimoMovimientoAnimal.Peso;
                                }
                            }
                        }
                        /* Insertamos el movimiento de Muerte */
                        var trampaInfo = trampaBL.ObtenerPorHostName(TrampaGlobal.TrampaNecropsia.ToString());
                        if (trampaInfo != null)
                        {
                            animalMovimientoInfo.TrampaID = trampaInfo.TrampaID;
                        }
                        /* Se genera el animal Movimiento para almacenarlo*/
                        animalMovimientoInfo = new AnimalMovimientoInfo
                        {
                            AnimalID = animalInfo.AnimalID,
                            OrganizacionID = muerte.OrganizacionId,
                            CorralID = loteInfo.CorralID,
                            LoteID = loteInfo.LoteID,
                            Peso = muerte.Peso,
                            Temperatura = 0,
                            TipoMovimientoID = (int)TipoMovimiento.Muerte,
                            TrampaID = trampaInfo != null ? trampaInfo.TrampaID : 1,
                            OperadorID = muerte.OperadorNecropsiaId,
                            Observaciones = muerte.Observaciones,
                            Activo = EstatusEnum.Activo,
                            UsuarioCreacionID = muerte.UsuarioCreacionID
                        };

                        //Proceso para Guardar Costos
                        AnimalCostoBL animalCostoBl = new AnimalCostoBL();
                        List<AnimalInfo> animales = new List<AnimalInfo>();
                        animales.Add(animalInfo);
                        List<AnimalCostoInfo> listaCostos = animalCostoBl.ObtenerCostosAnimal(animales);

                        if (listaCostos == null || listaCostos.Count <= 0)
                        {
                            animales = animalBL.ProcesoGenerarCostos(animales);
                        }
                        else
                        {
                            if (!listaCostos.Any(registro => registro.CostoID == Costo.CostoGanado.GetHashCode()))
                            {
                                animales = animalBL.ProcesoGenerarCostos(animales);
                            }
                        }

                        //Proceso Armar Poliza
                        if (animales != null && animales.Count > 0)
                        {
                            if (animales.FirstOrDefault().ListaCostosAnimal != null && animales.FirstOrDefault().ListaCostosAnimal.Count > 0)
                            {
                                poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaMuerte);
                                List<AnimalCostoInfo> listaCostosAnimal = animales.FirstOrDefault().ListaCostosAnimal;
                                foreach (AnimalCostoInfo animalCosto in listaCostosAnimal)
                                {
                                    animalCosto.OrganizacionID = muerte.OrganizacionId;
                                }
                                polizaSalidaMuerte = poliza.GeneraPoliza(listaCostosAnimal);
                                if (polizaSalidaMuerte != null && polizaSalidaMuerte.Any())
                                {
                                    polizaSalidaMuerte.ToList().ForEach(
                                        org =>
                                        {
                                            org.OrganizacionID = muerte.OrganizacionId;
                                            org.UsuarioCreacionID = muerte.UsuarioCreacionID;
                                            org.ArchivoEnviadoServidor = 1;
                                        });
                                    //retorno = poliza.ImprimePoliza(ventasGanado, polizaSalidaMuerte);
                                    var polizaBL = new PolizaBL();
                                    polizaBL.GuardarServicioPI(polizaSalidaMuerte, TipoPoliza.SalidaMuerte);
                                }
                            }
                        }

                        /* Se almacena el animalMovimiento */
                        animalMovimientoBL.GuardarAnimalMovimiento(animalMovimientoInfo);

                        /*  Se da de baja el animal */
                        animalBL.InactivarAnimal(animalInfo);

                        /* Enviar al historico el animal inactivo */
                        var animalMuerto = new List<AnimalInfo> { animalInfo };
                        animalBL.EnviarAHistorico(animalMuerto);
                    }
                    /* Decrementar la cabeza del lote */
                    loteInfo.Cabezas = loteInfo.Cabezas - 1;
                    loteInfo.UsuarioModificacionID = muerte.UsuarioCreacionID;
                    var filtro = new FiltroActualizarCabezasLote
                    {
                        CabezasProcesadas = 1,
                        LoteIDDestino = 0,
                        LoteIDOrigen = loteInfo.LoteID,
                        UsuarioModificacionID = muerte.UsuarioCreacionID
                    };

                    CabezasActualizadasInfo resultadoCabezas = loteBL.ActualizarCabezasProcesadas(filtro);


                    //Si ya no tenemos cabezas en el lote se actualizanb los pesos llegada
                    if (resultadoCabezas.CabezasOrigen <= 0)
                    {
                        if (corralInfo.GrupoCorral == (int)GrupoCorralEnum.Recepcion)
                        {
                            /* Si el corral es de Recepcion */
                            var corteGanadoPl = new CorteGanadoBL();
                            corteGanadoPl.ObtenerPesosOrigenLlegada(muerte.OrganizacionId,
                                                                     loteInfo.CorralID,
                                                                     loteInfo.LoteID);
                        }
                    }

                    transaccion.Complete();
                    retValue = 1;
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

            return retValue;
        }

        /// <summary>
        /// Metodo para Guardar el muerteInfo
        /// </summary>
        /// <param name="muerteInfo">contenedor donde se encuentra la información de la muerte</param>
        /// <returns></returns>
        internal void CancelarMovimientoMuerte(MuerteInfo muerteInfo)
        {
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();

                muerteDal.CancelarMovimientoMuerte(muerteInfo);

                //return result;
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
        /// Obtiene la lista de muetes para recepcion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerGanadoMuertoParaRecepcion(int organizacionId)
        {
            IList<MuerteInfo> lista;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                lista = muerteDal.ObtenerGanadoMuertoParaRecepcion(organizacionId);
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
            return lista;
        }

        /// <summary>
        /// Almacena la lista de ganado recibido en necropsia
        /// </summary>
        /// <param name="muertes"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal int GuardarRecepcionGanadoMuerto(IList<MuerteInfo> muertes, int operadorId)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var dal = new MuerteDAL();

                using (var transaccion = new TransactionScope())
                {

                    foreach (var muerteId in muertes)
                    {
                        var retorno = dal.GuardarRecepcionGanadoMuerto(muerteId, operadorId);
                        retValue = retorno;

                        if (retorno != 1)
                        {
                            retValue = 0;
                            break;
                        }

                    }

                    if (retValue == 1)
                    {

                        transaccion.Complete();
                        retValue = 1;
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

            return retValue;
        }

        /// <summary>
        /// Obtiene la lista de aretes muertos para recoleccion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerAretesMuertosRecoleccion(int organizacionId)
        {
            IList<MuerteInfo> lista;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                lista = muerteDal.ObtenerAretesMuertosRecoleccion(organizacionId);
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
            return lista;
        }

        /// <summary>
        /// Almancena a la capa de datos los ids de las muertes recolectadas
        /// </summary>
        /// <param name="muertes">Lista de id de muertes</param>
        /// <param name="operadorId">Operador de la recoleccion</param>
        /// <returns></returns>
        internal int GuardarRecoleccionGanadoMuerto(List<MuerteInfo> muertes, int operadorId)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var dal = new MuerteDAL();

                using (var transaccion = new TransactionScope())
                {

                    foreach (var muerteId in muertes)
                    {
                        var retorno = dal.GuardarRecoleccionGanadoMuerto(muerteId, operadorId);
                        retValue = retorno;

                        if (retorno != 1)
                        {
                            retValue = 0;
                            break;
                        }

                    }

                    if (retValue == 1)
                    {

                        transaccion.Complete();
                        retValue = 1;
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

            return retValue;
        }

        /// <summary>
        /// Guarda la informacion de un arete muerto
        /// </summary>
        /// <param name="muerte"></param>
        /// <param name="esCargaInicial"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal int GuardarMuerte(MuerteInfo muerte, FlagCargaInicial esCargaInicial, AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                int resultado;

                using (var transaccion = new TransactionScope())
                {
                    if (animal != null)
                    {
                        var animalBL = new AnimalBL();
                        // Se valida el flag de EsCargaInicial
                        switch (esCargaInicial)
                        {
                            case FlagCargaInicial.EsCargaInicial:
                                var deteccionGrabar = new DeteccionInfo
                                {
                                    CorralID = muerte.CorralId,
                                    LoteID = muerte.LoteId,
                                    UsuarioCreacionID = muerte.UsuarioCreacionID
                                };
                                // Se intercambian aretes por encontrarse el animal en un corral distinto y ser carga inicial
                                animalBL.ReemplazarAretes(animal, deteccionGrabar);
                                break;
                            case FlagCargaInicial.EsAreteNuevo:
                                // Se Reemplaza arete nuevo sobre uno existente del lote
                                animalBL.ReemplazarAreteMismoCorral(animal);
                                break;
                        }
                    }
                    if (muerte.Corral.GrupoCorral != GrupoCorralEnum.Recepcion.GetHashCode()
                        && string.IsNullOrWhiteSpace(muerte.Arete))
                    {
                        muerte.Arete = GenerarAreteGenerico(muerte);
                    }
                    var muerteDal = new MuerteDAL();
                    resultado = muerteDal.GuardarMuerte(muerte);
                    // Se cierral la transaccion
                    transaccion.Complete();
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
        private string GenerarAreteGenerico(MuerteInfo muerte)
        {
            IList<MuerteInfo> animalesMuertos = ObtenerGanadoMuertoPorLoteID(muerte.LoteId);
            string organzacion = muerte.OrganizacionId.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            string corral = Convert.ToString(new Random(1).Next(999)).PadLeft(3, '0');
            string anio = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            string mes = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            string dia = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            string consecutivo = string.Empty;
            if (animalesMuertos == null || !animalesMuertos.Any())
            {
                consecutivo = "01";
            }
            else
            {
                IList<MuerteInfo> aretesLongitudGenerica =
                    animalesMuertos.Where(muerto => muerto.Arete.Length == 15).OrderByDescending(muer => muer.MuerteId).
                        ToList();

                if (!aretesLongitudGenerica.Any())
                {
                    consecutivo = "01";
                }

                foreach (var muerteInfo in aretesLongitudGenerica)
                {
                    string organizacionMuerte = muerteInfo.Arete.Substring(0, 2); //Obtener la organizacion
                    if (organzacion != organizacionMuerte)
                    {
                        continue;
                    }
                    string anioMuerte = muerteInfo.Arete.Substring(5, 4); //Obtener el Año
                    if (anio != anioMuerte)
                    {
                        continue;
                    }
                    string mesMuerte = muerteInfo.Arete.Substring(9, 2); //Obtener el mes
                    if (mes != mesMuerte)
                    {
                        continue;
                    }
                    string diaMuerte = muerteInfo.Arete.Substring(11, 2); //Obtener el dia
                    if (dia != diaMuerte)
                    {
                        continue;
                    }
                    int consecutivoMuerte = Convert.ToInt32(muerteInfo.Arete.Substring(13, 2)) + 1;
                    consecutivo = consecutivoMuerte.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                    break;
                }

            }
            if (string.IsNullOrWhiteSpace(consecutivo))
            {
                consecutivo = "01";
            }
            string areteGenerico = string.Format("{0}{1}{2}{3}{4}{5}", organzacion, corral, anio, mes, dia, consecutivo);

            return areteGenerico;
        }

        /// <summary>
        /// Obtiene un ganado muerto por arete
        /// </summary>
        /// <param name="organizacionId">Id de la Organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        public MuerteInfo ObtenerGanadoMuertoPorAreteRecepcion(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                retValue = muerteDal.ObtenerGanadoMuertoPorAreteRecepcion(organizacionId, numeroArete);
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
            return retValue;
        }

        /// <summary>
        /// Obtiene las muertes por fecha necropsia
        /// </summary>
        /// <param name="muerteInfo"></param>
        /// <returns></returns>
        internal List<SalidaGanadoMuertoInfo> ObtenerMuertesFechaNecropsia(MuerteInfo muerteInfo)
        {
            var listaMuertes = new List<SalidaGanadoMuertoInfo>();
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                listaMuertes = muerteDal.ObtenerMuertesFechaNecropsia(muerteInfo).ToList();
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return listaMuertes;
        }

        /// <summary>
        /// Obtiene la lista de muertes por lote
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerGanadoMuertoPorLoteID(int loteID)
        {
            IList<MuerteInfo> lista;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                lista = muerteDal.ObtenerGanadoMuertoPorLoteID(loteID);
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
            return lista;
        }

        /// <summary>
        /// Obtiene un ganado muerto por arete
        /// </summary>
        /// <param name="organizacionId">Id de la Organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        internal MuerteInfo ObtenerMuertoPorArete(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue;
            try
            {
                Logger.Info();
                var muerteDal = new MuerteDAL();
                retValue = muerteDal.ObtenerMuertoPorArete(organizacionId, numeroArete);
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
            return retValue;
        }
    }
}
