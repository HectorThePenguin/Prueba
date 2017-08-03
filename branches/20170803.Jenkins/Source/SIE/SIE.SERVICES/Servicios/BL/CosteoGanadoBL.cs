using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Properties;
using SIE.Services.Servicios.PL;

namespace SIE.Services.Servicios.BL
{
    internal class CosteoGanadoBL
    {

        /// <summary>
        /// Metodo para generar el costeo de ganado para las entradas que no tienen
        /// los costos estan prorrateados.
        /// </summary>
        internal void GenerarCosteoGanado()
        {
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();
                var entradaGanadoBL = new EntradaGanadoBL();
                var usuarioBL = new UsuarioBL();
                var entradaGanadoCosteoBL = new EntradaGanadoCosteoBL();
                var organizacionProcesada = 0;
                //IList<CabezasSobrantesPorEntradaInfo> entradasCabezasSobrantes = null;
                var corteGanadoBl = new CorteGanadoBL();
                var entradaGanadoMuerteBL = new EntradaGanadoMuerteBL();
                var entradaGanadoSobranteBL = new EntradaGanadoSobranteBL();
                List<EntradaGanadoSobranteInfo> cabezasSobrante = null;

                //Obtener Entradas costeadas Sin prorratear y que sean de origen Ganadera
                List<EntradaGanadoInfo> listaEntradaGanado =
                    entradaGanadoBL.ObtenerEntradasCosteadasSinProrratear((int) TipoOrganizacion.Ganadera);

                //Se obtiene el usuario del proceso de alimentacion
                UsuarioInfo usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.CosteoGanado.ToString());

                if (listaEntradaGanado != null && listaEntradaGanado.Count > 0)
                {
                    List<int> organizaciones = listaEntradaGanado.Select(org => org.OrganizacionID).Distinct().ToList();
                    List<EntradaGanadoInfo> entradasPorOrganizacion;
                    for (int indexOrganizaciones = 0; indexOrganizaciones < organizaciones.Count; indexOrganizaciones++)
                    {
                        entradasPorOrganizacion =
                            listaEntradaGanado.Where(org => org.OrganizacionID == organizaciones[indexOrganizaciones]).
                                ToList();


                        //Recorrer la lista de entradas obtenidas
                        foreach (var entradaGanadoInfo in entradasPorOrganizacion)
                        {
                            entradaGanadoInfo.UsuarioCreacionID = usuario == null ? 1 : usuario.UsuarioID;

                            //Obtener los animales del inventario para cada folioEntrada
                            entradaGanadoInfo.ListaAnimal =
                                animalBL.ObtenerInventarioAnimalesPorFolioEntrada(
                                    entradaGanadoInfo.FolioEntrada.ToString(),
                                    entradaGanadoInfo.OrganizacionID);

                            //Agregar a la lista los animales de las tablas de control
                            ControlEntradaGanadoBL controlEntradaGanadoBl = new ControlEntradaGanadoBL();
                            List<ControlEntradaGanadoInfo> listaControl = controlEntradaGanadoBl.ObtenerControlEntradaGanadoPorID(0, entradaGanadoInfo.EntradaGanadoID);
                            int cabezasCortadas = 0;
                            if(entradaGanadoInfo.ListaAnimal != null)
                                cabezasCortadas = entradaGanadoInfo.ListaAnimal.Count;

                            if(listaControl != null)
                                cabezasCortadas = cabezasCortadas + listaControl.Count;
                            //Validar Integridad de información Entrada-Lote
                            if (entradaGanadoInfo.ListaAnimal != null &&
                                entradaGanadoInfo.CabezasRecibidas == cabezasCortadas)
                            {
                                //Se obtiene los costos de Entrada de ganado
                                var entradaGanadoCosteoInfo =
                                    entradaGanadoCosteoBL.ObtenerPorEntradaGanadoID(entradaGanadoInfo.EntradaGanadoID);
                                try
                                {
                                    // Se Calcula si se tiene sobrantes o faltantes
                                    var cabezasFaltantes = entradaGanadoInfo.CabezasOrigen -
                                                           entradaGanadoInfo.CabezasRecibidas;
                                    /////////////////Se obtienen Muertas y sobrantes////////////////////////////////////
                                    if (cabezasFaltantes < 0)
                                    {
                                        // Tenemos cabezas sobrantes
                                        cabezasSobrante =
                                            entradaGanadoSobranteBL.ObtenerSobrantePorEntradaGanado(
                                                entradaGanadoInfo.EntradaGanadoID);
                                    }

                                    // Se inicia la transaccion por Entrada validada
                                    using (var transaction = new TransactionScope())
                                    {
                                        // Centro/Pradera/Descanso/Cadis  && que no cuente con cabezas faltantes(Si tiene faltantes se prrorratean los costos)
                                        if (ValidarOrigen(entradaGanadoInfo) && cabezasFaltantes <= 0 && cabezasSobrante == null)
                                        {
                                            AplicarCostosDeInterface(entradaGanadoInfo,
                                                                     entradaGanadoCosteoInfo,
                                                                     cabezasFaltantes,
                                                                     cabezasSobrante);
                                        }
                                        //Prorratear costos de EntradaGanadoCosteo
                                        ProrratearCostosEntradaGanado(entradaGanadoCosteoInfo,
                                                                      entradaGanadoInfo,
                                                                      cabezasFaltantes,
                                                                      cabezasSobrante);

                                        //Marcar el costeo de la entrada de ganado como prorrateado
                                        entradaGanadoCosteoBL.InactivarProrrateoaCosteo(
                                            entradaGanadoCosteoInfo.EntradaGanadoCosteoID);

                                        //Validar si hay animales dado de baja
                                        var listaAnimalesInactivos = (
                                                                         from o in entradaGanadoInfo.ListaAnimal
                                                                         where
                                                                             !o.Activo &&
                                                                             o.FolioEntrada ==
                                                                             entradaGanadoInfo.FolioEntrada
                                                                         select o
                                                                     ).ToList();

                                        if (listaAnimalesInactivos.Count > 0)
                                        {
                                            foreach (var listaAnimalesInactivo in listaAnimalesInactivos)
                                            {
                                                listaAnimalesInactivo.UsuarioCreacionID = usuario == null
                                                                                              ? 1
                                                                                              : usuario.UsuarioID;
                                            }
                                            //Si hay animales muertos y hay q Mandarlos a las historicas
                                            animalBL.EnviarAHistorico(listaAnimalesInactivos);
                                        }
                                        transaction.Complete();
                                    }
                                }
                                catch (ExcepcionDesconocida ex) 
                                {
                                    var bitacoraBL = new BitacoraIncidenciasBL();
                                    var bitacora = new BitacoraErroresInfo
                                                       {
                                                           AccionesSiapID = AccionesSIAPEnum.SerCostGan,
                                                           Mensaje = ex.Message,
                                                           UsuarioCreacionID = usuario.UsuarioID
                                                       };
                                    bitacoraBL.GuardarError(bitacora);
                                    if (organizacionProcesada != organizaciones[indexOrganizaciones])
                                    {
                                        EnviarEmailNotificacion(organizaciones[indexOrganizaciones], false);
                                    }
                                    organizacionProcesada = organizaciones[indexOrganizaciones];
                                }
                            }
                        }
                        EnviarEmailNotificacion(organizaciones[indexOrganizaciones], true);
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
        }

        /// <summary>
        /// Metodo para controlar el envio de email por organizacion
        /// </summary>
        /// <param name="organizacionProcesada"></param>
        /// <param name="envioEmailOK"></param>
        private static void EnviarEmailNotificacion(int organizacionProcesada, bool envioEmailOK)
        {
            var correopl = new CorreoPL();
            CorreoInfo correo;
            if (envioEmailOK)
            {
                //Esta funcion enviara el correo y generara la bitacora
                correo = new CorreoInfo
                {
                    Asunto = "Costeo de Ganado",
                    Mensaje = "El proceso se ejecuto correctamente.",
                    NombreOrigen = "Sukarne",
                    AccionSiap = AccionesSIAPEnum.SerCostGan
                };
            }
            else
            {
                correo = new CorreoInfo
                {
                    Asunto = "Costeo de Ganado",
                    Mensaje = "El proceso termino su ejecución y se encontraron algunas incidencias. Favor de validar la bitácora.",
                    NombreOrigen = "Sukarne",
                    AccionSiap = AccionesSIAPEnum.SerCostGan
                };
            }
            correopl.EnviarCorreoElectronicoInsidencia(
                   new OrganizacionInfo { OrganizacionID = organizacionProcesada },
                   correo);
        }

        /// <summary>
        /// Medoto para tomar los costos de interface y meterlos en AnimalCosto
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <param name="entradaGanadoCosteoInfo"></param>
        /// <param name="cabezasFaltantes"></param>
        /// <param name="cabezasSobrante"></param>
        private void AplicarCostosDeInterface(EntradaGanadoInfo entradaGanadoInfo, 
                                                EntradaGanadoCosteoInfo entradaGanadoCosteoInfo,
                                                int cabezasFaltantes,
                                                List<EntradaGanadoSobranteInfo> cabezasSobrante)
        {
            var animalBL = new AnimalBL();
            var interfaceSalidaCostoBL = new InterfaceSalidaCostoBL();
            var listaCostosProrrateados = new List<EntradaGanadoCostoInfo>();
            var interfaceSalidaAnimalBL = new InterfaceSalidaAnimalBL();


            //Ir por la interface salida animal
            List<InterfaceSalidaAnimalInfo> interfaceSalidaAnimal = interfaceSalidaAnimalBL.ObtenerInterfazSalidaAnimalPorEntradaGanado(entradaGanadoInfo);

            // Ir a InterfaceSalida por los animales y sus costos
            IList<InterfaceSalidaCostoInfo> listaAnimales =
                interfaceSalidaCostoBL.ObtenerCostoAnimales(entradaGanadoInfo.FolioOrigen,
                        entradaGanadoInfo.OrganizacionOrigenID);


            if (listaAnimales != null && entradaGanadoCosteoInfo != null &&
                entradaGanadoCosteoInfo.ListaCostoEntrada != null &&
                listaAnimales.Count >= 0 &&
                entradaGanadoCosteoInfo.ListaCostoEntrada.Count >= 0)
            {
                //Se obtiene la lista de costos de los datos obtenidos en la interfaz
                var listaCostosInterfaz = (from o in listaAnimales
                                           select new {o.Costo}
                                          ).GroupBy(i => i.Costo.CostoID).ToList().Select(group => group.First());

                IList<InterfaceSalidaCostoInfo> animalesSinIntegridad = ValidarIntegridadAretes(interfaceSalidaAnimal,
                                                                                                listaAnimales,entradaGanadoInfo);
                int cabezasSobrantes = cabezasFaltantes == 0 ? cabezasFaltantes : (cabezasFaltantes * (-1));
                //Si encontramos la info empezamos a valdiar --> Se recorren los costos de la entrada
                foreach (var costoInterfaz in listaCostosInterfaz)
                {
                    //Se genera una lista de los costos q coincidan con el costo recorrido de la entrada OJO

                    var listaAnimalesCostosInterfaz = (from o in listaAnimales
                                                        where !animalesSinIntegridad.Contains(o)
                                                           && o.Costo.CostoID == costoInterfaz.Costo.CostoID
                                                       select new { o.Importe, o.Arete, o.Costo }
                                                      ).Distinct().ToList();
                    
                     
                    decimal sumaCostoInterfaz = listaAnimalesCostosInterfaz.Sum(c => c.Importe);
                    //Se obtiene la entradaCosto de los costos de entrada
                    var entradaGanadoCostoInfo =
                        (from o in entradaGanadoCosteoInfo.ListaCostoEntrada
                         where o.Costo.CostoID == costoInterfaz.Costo.CostoID
                         select o).First();
                    //Se validan que los importes de los costos coincidan
                    //if (sumaCostoInterfaz == entradaGanadoCostoInfo.Importe)
                    {
                        decimal costoAcumulado = 0;
                        int contadorAnimales = 0;
                        var listaAnimalCosto = new List<AnimalCostoInfo>();
                        //Si son iguales el sistema debe guardar los registros de la tabla “InterfaceSalidaAnimal” a la tabla “AnimalCosto”
                        foreach (var interfaceCosto in listaAnimalesCostosInterfaz)
                        {

                            //Se obtiene la interface salida animal de la interface costo 
                            InterfaceSalidaAnimalInfo animalsalida = interfaceSalidaAnimal.FirstOrDefault(animal => animal.Arete.Equals(interfaceCosto.Arete));

                            AnimalInfo animalInterfaz = entradaGanadoInfo
                                .ListaAnimal.FirstOrDefault(animal => animal.AnimalID == animalsalida.AnimalID
                                                             || animal.Arete == animalsalida.Arete);
                            if (animalInterfaz != null)
                            {
                                var importeCalculado = decimal.Round(interfaceCosto.Importe, 2);
                                costoAcumulado += importeCalculado;
                                contadorAnimales++;
                                //Si es el ultimo animal se le ajusta el redondeo
                                if (contadorAnimales == listaAnimalesCostosInterfaz.Count())
                                {
                                    //Se obtiene la entradaCosto de los costos de entrada
                                    decimal diferencia = entradaGanadoCostoInfo.Importe - costoAcumulado;
                                    importeCalculado = importeCalculado + diferencia;
                                }
                                // se inserta en AnimalCosto
                                var animalCosto = new AnimalCostoInfo
                                {
                                    AnimalID = animalInterfaz.AnimalID,
                                    CostoID = interfaceCosto.Costo.CostoID,
                                    TipoReferencia = TipoReferenciaAnimalCosto.Manejo,
                                    FolioReferencia = entradaGanadoCostoInfo.EntradaGanadoCosteoID,
                                    Importe = importeCalculado,/*interfaceCosto.Importe,*/
                                    UsuarioCreacionID = entradaGanadoInfo.UsuarioCreacionID
                                };
                                //Almacenr el costo en Animal Costo
                                
                                listaAnimalCosto.Add(animalCosto);
                            }
                        }

                        List<long> listaAnimalCos = listaAnimalCosto.Select(a => a.AnimalID).Distinct().ToList();
                        bool existenDiferencias = entradaGanadoInfo.CabezasRecibidas != listaAnimalCos.Count(); 
                        if (existenDiferencias)
                        {
                            List<long> idCostos = listaAnimalCosto.Select(id => id.AnimalID).ToList();
                            List<long> idAnimales = entradaGanadoInfo.ListaAnimal.Select(id => id.AnimalID).ToList();

                            List<long> idDiferentes;
                            if (idAnimales.Count > idCostos.Count)
                            {
                                idDiferentes = idAnimales.Except(idCostos).ToList();
                            }
                            else
                            {
                                idDiferentes = idCostos.Except(idAnimales).ToList();
                            }
                            if (idDiferentes.Any())
                            {
                                List<AnimalInfo> animalesNoExistentes =
                                    entradaGanadoInfo.ListaAnimal.Join(idDiferentes, ani => ani.AnimalID, id => id,
                                                                       (ani, id) => ani).ToList();
                                int cabezasSinIntegridad = animalesNoExistentes.Count;
                                decimal importeAnimalesExistentes =
                                    listaAnimalCosto.Where(cost => cost.CostoID == costoInterfaz.Costo.CostoID).Sum(
                                        imp => imp.Importe);
                                decimal diferencia = sumaCostoInterfaz - importeAnimalesExistentes;
                                listaAnimalCosto.AddRange(animalesNoExistentes
                                                              .Select(ani => new AnimalCostoInfo
                                                                                 {
                                                                                     AnimalID = ani.AnimalID,
                                                                                     CostoID =
                                                                                         costoInterfaz.Costo.
                                                                                         CostoID,
                                                                                         FolioReferencia = entradaGanadoCostoInfo.EntradaGanadoCosteoID,
                                                                                     TipoReferencia =
                                                                                         TipoReferenciaAnimalCosto.
                                                                                         Manejo,
                                                                                     Importe =
                                                                                         Math.Round(
                                                                                             diferencia /
                                                                                             cabezasSinIntegridad,
                                                                                             2),
                                                                                     UsuarioCreacionID =
                                                                                         entradaGanadoInfo.
                                                                                         UsuarioCreacionID
                                                                                 }));
                                importeAnimalesExistentes = listaAnimalCosto.Where(
                                    cost => cost.CostoID == costoInterfaz.Costo.CostoID).Sum(
                                        imp => imp.Importe);
                                diferencia = sumaCostoInterfaz - importeAnimalesExistentes;
                                if (diferencia != 0)
                                {
                                    AnimalCostoInfo animalCosto = listaAnimalCosto.LastOrDefault();
                                    if (animalCosto != null)
                                    {
                                        animalCosto.Importe += diferencia;
                                    }
                                }
                            }
                        }
                        //Si se esta procesando el costo de ganado y se cuentan con sobrantes
                        if (cabezasSobrantes > 0 &&
                            costoInterfaz.Costo.CostoID == (int)TipoCostoEnum.Ganado)
                        {
                            List<AnimalCostoInfo> listaAnimalCostoCabezasSobrantes =
                                AplicarCostosGanadoCabezasSobrantes(entradaGanadoInfo,
                                                                    entradaGanadoCostoInfo,
                                                                    cabezasSobrante);
                            if (listaAnimalCostoCabezasSobrantes != null && listaAnimalCostoCabezasSobrantes.Any())
                            {
                                foreach (var animal in listaAnimalCostoCabezasSobrantes)
                                {
                                    if (listaAnimalCosto.All(registro => registro.AnimalID != animal.AnimalID))
                                    {
                                        listaAnimalCosto.Add(animal); 
                                    }
                                }
                            }
                        }

                        /*  Se almacenan los costos */
                        if (listaAnimalCosto.Any())
                        {
                            animalBL.GuardarAnimalCostoXMLManual(listaAnimalCosto);
                        }
                        //Esta es un costo ya procesado
                        listaCostosProrrateados.Add(entradaGanadoCostoInfo);
                    }
                }
                //Se el total de costos del costeo Se eliminan los costos capturados de la interfaz
                foreach (var costoProrrateado in listaCostosProrrateados)
                {
                    entradaGanadoCosteoInfo.ListaCostoEntrada.Remove(costoProrrateado);
                }
            }
        }
        /// <summary>
        /// Metodo para aplicr los costos de ganado de las cabezas sobrantes
        /// </summary>
        private static List<AnimalCostoInfo> AplicarCostosGanadoCabezasSobrantes(EntradaGanadoInfo entradaGanadoInfo,
                                                                                 EntradaGanadoCostoInfo entradaGanadoCostoInfo,
                                                                                 List<EntradaGanadoSobranteInfo> cabezasSobrante)
        {
            var entradaGanadoSobranteBL = new EntradaGanadoSobranteBL();
            var listaAnimalCosto = new List<AnimalCostoInfo>();

            if (cabezasSobrante != null && cabezasSobrante.Any())
            {
                /* Se cambia el flag de costeado a las cabezas sobrantes */
                foreach (var entradaGanadoSobranteInfo in cabezasSobrante)
                {
                    // se inserta en AnimalCosto
                    var animalCosto = new AnimalCostoInfo
                    {
                        AnimalID = entradaGanadoSobranteInfo.Animal.AnimalID,
                        CostoID = (int)TipoCostoEnum.Ganado,
                        TipoReferencia = TipoReferenciaAnimalCosto.Manejo,
                        FolioReferencia = entradaGanadoCostoInfo.EntradaGanadoCosteoID,
                        Importe = entradaGanadoSobranteInfo.Importe,/*interfaceCosto.Importe,*/
                        UsuarioCreacionID = entradaGanadoInfo.UsuarioCreacionID
                    };
                    //Almacenr el costo en Animal Costo
                    listaAnimalCosto.Add(animalCosto);

                    entradaGanadoSobranteInfo.UsuarioModificacionID =
                        entradaGanadoInfo.UsuarioCreacionID;
                }
                /* Se actualizan el flag de costeado en la tabla de sobrantes */
                entradaGanadoSobranteBL.ActualizarCosteadoEntradaGanadoSobrante(
                        cabezasSobrante);
            }
            return listaAnimalCosto;
        }

        /// <summary>
        /// Metodo para validar si el origen es de un Centro/Pradera/Descanso/Cadis
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        private static bool ValidarOrigen(EntradaGanadoInfo entradaGanadoInfo)
        {
            var resp = false;
            var organizacionBL = new OrganizacionBL();
            //Si son iguales Cargar costos ingresados en el costeo
            var organizacionOrigen = organizacionBL.ObtenerPorID(entradaGanadoInfo.OrganizacionOrigenID);
            if (organizacionOrigen.TipoOrganizacion.TipoOrganizacionID == (int)TipoOrganizacion.Centro ||
                organizacionOrigen.TipoOrganizacion.TipoOrganizacionID == (int)TipoOrganizacion.Praderas ||
                organizacionOrigen.TipoOrganizacion.TipoOrganizacionID == (int)TipoOrganizacion.Descanso ||
                organizacionOrigen.TipoOrganizacion.TipoOrganizacionID == (int)TipoOrganizacion.Cadis)
            {
                resp = true;
            }
            return resp;
        }

        /// <summary>
        /// Metodo para prorratear los costos de entradaGanadoCosteo
        /// </summary>
        /// <param name="entradaGanadoCosteoInfo"></param>
        /// <param name="entradaGanadoInfo"></param>
        /// <param name="cabezasFaltantes"></param>
        /// <param name="cabezasSobrante"></param>
        private static void ProrratearCostosEntradaGanado(EntradaGanadoCosteoInfo entradaGanadoCosteoInfo, 
                                                          EntradaGanadoInfo entradaGanadoInfo,
                                                          int cabezasFaltantes,
                                                          List<EntradaGanadoSobranteInfo> cabezasSobrante)
        {
            if (entradaGanadoCosteoInfo != null && 
                entradaGanadoCosteoInfo.ListaCostoEntrada != null &&                
                entradaGanadoCosteoInfo.ListaCostoEntrada.Count > 0)
            {
                var huboFaltante = false;

                var animalBL = new AnimalBL();
                var controlEntradaGanado = new ControlEntradaGanadoBL();
                var listaAnimalCosto = new List<AnimalCostoInfo>();
                //decimal importeMuertes = 0;
                decimal pesoFaltante = 0;
                decimal pesoSobrante = 0;
                //decimal pesoMuertes = 0;
                int totalAnimales;

                decimal porcentajeAnimalesSobrantes = 0;

                // Se convierte en positivo
                bool cabezasSobrantes = entradaGanadoCosteoInfo.ListaEntradaDetalle.Sum(imp => imp.Importe - imp.ImporteOrigen) > 0;
                if (cabezasSobrantes)
                {
                    cabezasFaltantes = 0;
                    EntradaGanadoCostoInfo entradaGanadoCosto = entradaGanadoCosteoInfo.ListaCostoEntrada
                                                                                       .FirstOrDefault(id => id.Costo.CostoID == TipoCostoEnum.Ganado.GetHashCode());
                    if (entradaGanadoCosto != null)
                    {
                        decimal importeGanado = entradaGanadoCosteoInfo.ListaEntradaDetalle.Sum(imp => imp.Importe);
                        decimal importeGanadoDetalle = entradaGanadoCosto.Importe;
                        decimal diferencia = importeGanado - importeGanadoDetalle;
                        entradaGanadoCosto.Importe = importeGanado;
                        porcentajeAnimalesSobrantes = (diferencia * 100) / importeGanado;
                    }
                }
                // Se obtiene el peso promedio de las cabezas origenes
                var pesoPromedio =
                    entradaGanadoCosteoInfo.ListaEntradaDetalle.Sum(e => e.PesoOrigen) /
                    entradaGanadoCosteoInfo.ListaEntradaDetalle.Sum(e => e.Cabezas);
                if (cabezasFaltantes > 0)
                {   // Tenemos Cabezas Faltantes
                    pesoFaltante = pesoPromedio * cabezasFaltantes;
                    huboFaltante = true;
                }

                //Se obtiene el peso origen de la EntradaGanadoDetalle en base al tipo de ganado del animal
                decimal sumatoriaPesoOrigen =
                    (from entradaDetalle in entradaGanadoCosteoInfo.ListaEntradaDetalle
                     select entradaDetalle.PesoOrigen).Sum();
                if (cabezasSobrantes && cabezasSobrante != null && cabezasSobrante.Any())
                {
                    pesoSobrante = cabezasSobrante.Sum(item => item.Animal.PesoCompra);
                }

                decimal pesoOrigen = sumatoriaPesoOrigen;
                //Si hay o quedan registros a prorratear
                var parametroOrganizacionPL = new ParametroOrganizacionPL();
                var codigoParametro = ParametrosEnum.CORRALFALTDIRECTA;
                switch ((TipoOrganizacion)entradaGanadoInfo.TipoOrigen)
                {
                    case TipoOrganizacion.Cadis:
                    case TipoOrganizacion.Centro:
                    case TipoOrganizacion.Praderas:
                        codigoParametro = ParametrosEnum.CORRALFALTPROPIO;
                        break;
                }
                ParametroOrganizacionInfo parametroOrganizacion =
                    parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(
                        entradaGanadoInfo.OrganizacionID, codigoParametro.ToString());

                var entradaGanadoTransitoBL = new EntradaGanadoTransitoBL();
                EntradaGanadoTransitoInfo entradaGanadoTransito =
                    entradaGanadoTransitoBL.ObtenerPorCorralOrganizacion(parametroOrganizacion.Valor,
                                                                            entradaGanadoInfo.OrganizacionID);
                List<AnimalInfo> animalesDelFolio;
                var controlEntradaBL = new ControlEntradaGanadoBL();
                var ListaControlEntradaGanado = controlEntradaBL.ObtenerControlEntradaGanadoPorID(0, entradaGanadoInfo.EntradaGanadoID);
                if (ListaControlEntradaGanado == null)
                {
                    ListaControlEntradaGanado = new List<ControlEntradaGanadoInfo>();
                }

                var detallesControlEntradaGanado = new List<ControlEntradaGanadoDetalleInfo>();
                ListaControlEntradaGanado.ForEach(m => detallesControlEntradaGanado.AddRange(m.ListaControlEntradaGanadoDetalle));
                // Se filtran los animales por el FolioEntrada
                if (ListaControlEntradaGanado.Any())
                {
                    animalesDelFolio = (from o in entradaGanadoInfo.ListaAnimal
                                        where o.FolioEntrada == entradaGanadoInfo.FolioEntrada
                                        select o).ToList();

                    animalesDelFolio =
                        animalesDelFolio.Where(
                            registro =>
                                ListaControlEntradaGanado.All(control => control.Animal.AnimalID != registro.AnimalID)).ToList();
                }
                else
                {
                    animalesDelFolio = (from o in entradaGanadoInfo.ListaAnimal
                                        where o.FolioEntrada == entradaGanadoInfo.FolioEntrada
                                        select o).ToList();
                }

                

                totalAnimales = animalesDelFolio.Count;

                var costoBL = new CostoBL();
                decimal costoAcumulado;
                int contadorAnimales;
                decimal importeTotalDelCosto;

                decimal porcentajeAnimalesFaltantes = 0;

                ControlEntradaGanadoDetalleInfo detalleControlEntradaGanado;

                EntradaGanadoTransitoDetalleInfo detalleTransito;
                entradaGanadoCosteoInfo.ListaCostoEntrada = entradaGanadoCosteoInfo.ListaCostoEntrada
                                                                                   .OrderBy(id => id.Costo.CostoID)
                                                                                   .ToList();
                foreach (var costoEntradaGanado in entradaGanadoCosteoInfo.ListaCostoEntrada)
                {
                    costoAcumulado = 0;
                    contadorAnimales = 0;


                    List<ControlEntradaGanadoDetalleInfo> detalleControlEntradaGanadoLista = detallesControlEntradaGanado.Where(id => id.Costo.CostoID == costoEntradaGanado.Costo.CostoID).ToList();
                    detalleControlEntradaGanado = new ControlEntradaGanadoDetalleInfo
                    {
                        Costo = costoEntradaGanado.Costo,
                        Importe = detalleControlEntradaGanadoLista.Sum(registro => registro.Importe)
                    };

                    importeTotalDelCosto = costoEntradaGanado.Importe;
                    if (cabezasSobrantes && costoEntradaGanado.Costo.CostoID != TipoCostoEnum.Ganado.GetHashCode())
                    {
                        detalleTransito = entradaGanadoTransito.EntradasGanadoTransitoDetalles
                                       .FirstOrDefault(id => id.Costo.CostoID
                                                        == costoEntradaGanado.Costo.CostoID);
                        if (detalleTransito != null)
                        {
                            decimal importeSobrante = ((porcentajeAnimalesSobrantes * importeTotalDelCosto) / 100);
                            importeTotalDelCosto += importeSobrante;
                            costoEntradaGanado.Importe = importeTotalDelCosto;
                        }
                    }

                    importeTotalDelCosto = costoEntradaGanado.Importe - detalleControlEntradaGanado.Importe;

                    costoEntradaGanado.Costo = costoBL.ObtenerPorID(costoEntradaGanado.Costo.CostoID);
                    if (huboFaltante && costoEntradaGanado.Costo.CostoID != TipoCostoEnum.Ganado.GetHashCode())
                    {
                        decimal importeDescuento = ((porcentajeAnimalesFaltantes * importeTotalDelCosto) / 100);
                        importeTotalDelCosto -= importeDescuento;

                        detalleTransito = entradaGanadoTransito.EntradasGanadoTransitoDetalles
                                       .FirstOrDefault(id => id.Costo.CostoID
                                                        == costoEntradaGanado.Costo.CostoID);
                        if (detalleTransito != null)
                        {
                            detalleTransito.Importe = importeDescuento;
                        }
                    }
                    if (costoEntradaGanado.Costo.CostoID == (int)TipoCostoEnum.Ganado)
                    {
                        if (huboFaltante)
                        {
                            decimal importeCompleto = costoEntradaGanado.Importe;
                            decimal importeFaltante = entradaGanadoCosteoInfo.ListaEntradaDetalle.Sum(imp => imp.Importe);
                            decimal diferencia = importeCompleto - importeFaltante;

                            porcentajeAnimalesFaltantes = (diferencia * 100) / importeTotalDelCosto;

                            detalleTransito = entradaGanadoTransito.EntradasGanadoTransitoDetalles
                                                                   .FirstOrDefault(id => id.Costo.CostoID
                                                                                    == costoEntradaGanado.Costo.CostoID);
                            if (detalleTransito != null)
                            {
                                detalleTransito.Importe = diferencia;
                            }

                            if (!ListaControlEntradaGanado.Any())
                            {
                                diferencia = 0;
                            }

                            importeTotalDelCosto = importeCompleto - diferencia - detalleControlEntradaGanado.Importe;
                        }
                    }

                    listaAnimalCosto = new List<AnimalCostoInfo>();
                    decimal importeCalculado = 0;
                    foreach (var animalInventario in animalesDelFolio)
                    {
                        importeCalculado = 0;
                        if (costoEntradaGanado.Costo.TipoProrrateo.TipoProrrateoID ==
                            (int)TipoProrrateo.Cabezas)
                        {
                            //Calculo por cabezas se recorren cabezas
                            if (entradaGanadoInfo.Lote.CabezasInicio > 0)
                            {
                                importeCalculado = (importeTotalDelCosto / totalAnimales);
                            }
                        }
                        else
                        {
                            //Calculo por (Importe/Costos)*Peso Individual
                            if (pesoOrigen > 0)
                            {
                                importeCalculado =
                                    ((importeTotalDelCosto / pesoOrigen) * animalInventario.PesoCompra);
                            }
                        }
                        importeCalculado = decimal.Round(importeCalculado, 2);
                        costoAcumulado += importeCalculado;
                        contadorAnimales++;

                        //Si es el ultimo animal se le ajusta el redondeo
                        if (contadorAnimales == totalAnimales)
                        {
                            //Se obtiene la entradaCosto de los costos de entrada
                            decimal diferencia = 0;
                            if (!huboFaltante)
                            {
                                diferencia = importeTotalDelCosto - costoAcumulado;
                            }
                            importeCalculado = importeCalculado + diferencia;
                        }
                        // se inserta en AnimalCosto
                        var animalCosto = new AnimalCostoInfo
                        {
                            AnimalID = animalInventario.AnimalID,
                            CostoID = costoEntradaGanado.Costo.CostoID,
                            TipoReferencia = TipoReferenciaAnimalCosto.Manejo,
                            FolioReferencia = costoEntradaGanado.EntradaGanadoCosteoID,
                            Importe = importeCalculado,
                            UsuarioCreacionID = entradaGanadoInfo.UsuarioCreacionID
                        };
                        //Almacenar el costo en Animal Costo
                        listaAnimalCosto.Add(animalCosto);
                    }
                    decimal costosAnimal = listaAnimalCosto.Sum(imp => imp.Importe);
                    decimal diferenciaCosto = importeTotalDelCosto - costosAnimal;
                    if (diferenciaCosto != 0)
                    {
                        AnimalCostoInfo costoDiferencia = listaAnimalCosto.LastOrDefault();
                        costoDiferencia.Importe += Math.Abs(diferenciaCosto);
                    }
                    //Si se esta procesando el costo de ganado y se cuentan con sobrantes
                    if (cabezasSobrantes &&
                        costoEntradaGanado.Costo.CostoID == (int)TipoCostoEnum.Ganado)
                    {
                        var listaAnimalCostoCabezasSobrantes =
                            AplicarCostosGanadoCabezasSobrantes(entradaGanadoInfo, costoEntradaGanado, cabezasSobrante);
                        if (listaAnimalCostoCabezasSobrantes != null && listaAnimalCostoCabezasSobrantes.Any())
                        {
                            foreach (var animal in listaAnimalCostoCabezasSobrantes)
                            {
                                if(!listaAnimalCosto.Any(registro=> registro.AnimalID == animal.AnimalID))
                                    listaAnimalCosto.Add(animal);
                            }
                        }
                        pesoOrigen = pesoOrigen + pesoSobrante;
                    }
                    // Se almacenan los costos acumulados
                    animalBL.GuardarAnimalCostoXMLManual(listaAnimalCosto);
                    //if (ListaControlEntradaGanado.Any())
                    //{
                    //    controlEntradaGanado.EliminaControlEntradaGanado(entradaGanadoInfo.EntradaGanadoID);
                    //}
                }
            }
        }

        /// <summary>
        /// Generar el costeo del consumo de forraje
        /// </summary>
        /// <param name="corteGanadoGuardarInfo"></param>
        /// <returns></returns>
        internal bool GenerarCosteoConsumoForraje(CorteGanadoGuardarInfo corteGanadoGuardarInfo)
        {
            bool generarCosteo;
            try
            {
                Logger.Info();
                var repartoBL = new RepartoBL();
                var formulaBL = new FormulaBL();
                var almacenBL = new AlmacenBL();
                var animalBL = new AnimalBL();
                var almacenGeneral = new List<AlmacenInfo>();

                corteGanadoGuardarInfo.EntradaGanado.Activo = EstatusEnum.Activo;
                corteGanadoGuardarInfo.TipoFormula =
                    new TipoFormulaInfo { TipoFormulaID = (int)TipoFormula.Forraje };
                corteGanadoGuardarInfo.TipoServicioInfo =
                    new TipoServicioInfo { TipoServicioId = (int)TipoServicioEnum.Manual };

                //Se obtiene el almacen general
                IList<AlmacenInfo> almacenLista =
                    almacenBL.ObtenerAlmacenPorOrganizacion(corteGanadoGuardarInfo.EntradaGanado.OrganizacionID);
                if (almacenLista != null && almacenLista.Count > 0)
                {
                    almacenGeneral = (from o in almacenLista
                                      where o.TipoAlmacen.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera
                                      select o
                        ).ToList();

                    if (!(almacenGeneral.Count > 0))
                    {
                        // Error no se tiene configurado un almacen general para esta organizacion
                        throw new ExcepcionDesconocida(ResourceServices.CosteoConsumoForraje_AlmacenGeneral);
                    }
                }
                else
                {
                    // Error no se tiene configurado un almacen general para esta organizacion
                    throw new ExcepcionDesconocida(ResourceServices.CosteoConsumoForraje_AlmacenGeneral);
                }


                //Obtener Los repartos para el tipo de servicio Manual
                List<RepartoDetalleInfo> listaRepartoDetalle =
                    repartoBL.ObtenerRepartoPorTipoServicioFecha(corteGanadoGuardarInfo);

                if (listaRepartoDetalle != null && listaRepartoDetalle.Count > 0)
                {
                    foreach (var repartoDetalleInfo in listaRepartoDetalle)
                    {
                        //Se obtiene la info de la formula Servida
                        FormulaInfo formulaServida = formulaBL.ObtenerPorID(repartoDetalleInfo.FormulaIDServida);
                        //Obtener los costos de los productos de Almacen Inventario
                        AlmacenInventarioInfo almmacenInventarioInfo =
                            almacenBL.ObtenerCantidadProductoEnInventario(formulaServida.Producto,
                                                             almacenGeneral.First().AlmacenID);

                        if (almmacenInventarioInfo != null)
                        {

                            //Obtener la lista de animales por partidas agrupadas
                            corteGanadoGuardarInfo.EntradaGanado.ListaAnimal =
                                animalBL.ObtenerInventarioAnimalesPorFolioEntrada(
                                    corteGanadoGuardarInfo.EntradaGanado.FolioEntradaAgrupado,
                                    corteGanadoGuardarInfo.EntradaGanado.OrganizacionID);
                            ////TODO:chekar la organizacion ID origen
                            if (corteGanadoGuardarInfo.EntradaGanado.ListaAnimal != null &&
                                corteGanadoGuardarInfo.EntradaGanado.ListaAnimal.Any())
                            {
                                //Validar el total de cabezas
                                if (corteGanadoGuardarInfo.LoteOrigen.CabezasInicio ==
                                    corteGanadoGuardarInfo.EntradaGanado.ListaAnimal.Count())
                                {
                                    //se obtiene el costo total
                                    decimal costototal = repartoDetalleInfo.CantidadServida*
                                                         almmacenInventarioInfo.PrecioPromedio;

                                    //Costo por animal
                                    decimal costoPorAnimal = (costototal/
                                                              corteGanadoGuardarInfo.EntradaGanado.ListaAnimal.Count());
                                    costoPorAnimal = Decimal.Round(costoPorAnimal, 2);

                                    decimal costoAcumulado = 0;
                                    int contadorAnimales = 0;

                                    using (var transaction = new TransactionScope())
                                    {
                                        //Almacenr los costos por animal
                                        foreach (var animalInfo in corteGanadoGuardarInfo.EntradaGanado.ListaAnimal)
                                        {

                                            costoAcumulado += costoPorAnimal;
                                            contadorAnimales++;
                                            //Si es el ultimo animal se le ajusta el redondeo
                                            if (contadorAnimales ==
                                                corteGanadoGuardarInfo.EntradaGanado.ListaAnimal.Count())
                                            {
                                                //Se obtiene la entradaCosto de los costos de entrada

                                                decimal diferencia = costototal - costoAcumulado;

                                                costoPorAnimal = costoPorAnimal + diferencia;
                                            }

                                            // se inserta en AnimalCosto
                                            var animalCosto = new AnimalCostoInfo
                                            {
                                                AnimalID = animalInfo.AnimalID,
                                                CostoID = (int) Costo.Alimento,
                                                FolioReferencia = repartoDetalleInfo.RepartoID,
                                                Importe = costoPorAnimal,
                                                UsuarioCreacionID = corteGanadoGuardarInfo.UsuarioCreacionID
                                            };

                                            //Almacenar el costo en Animal Costo
                                            animalBL.GuardarAnimalCosto(animalCosto);
                                        }

                                        //Descrementar la cantidad servida del inventario del almacen
                                        DecrementarProductoServidoDelInventario(almacenGeneral,
                                            corteGanadoGuardarInfo,
                                            almmacenInventarioInfo,
                                            repartoDetalleInfo);

                                        transaction.Complete();
                                    }

                                }
                                /*else{// Las cabezas no son iguales}*/
                            }
                        }
                        else
                        {
                            // Error no se tiene configurado un almacen general para esta organizacion
                            throw new ExcepcionDesconocida("No se encuentra el producto: " + formulaServida.Producto.ProductoDescripcion + 
                                ", en el inventario. Favor de validar.");
                        }
                    }
                }
                generarCosteo = true;

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
            return generarCosteo;
        }

        /// <summary>
        /// //Descrementar la cantidad servida del inventario del almacen
        /// </summary>
        /// <param name="almacenGeneral"></param>
        /// <param name="corteGanadoGuardarInfo"></param>
        /// <param name="almmacenInventarioInfo"></param>
        /// <param name="repartoDetalleInfo"></param>
        private void DecrementarProductoServidoDelInventario(List<AlmacenInfo> almacenGeneral, CorteGanadoGuardarInfo corteGanadoGuardarInfo, AlmacenInventarioInfo almmacenInventarioInfo, RepartoDetalleInfo repartoDetalleInfo)
        {
            var almacenBL = new AlmacenBL();
            var almacenMovimientoInfo = new AlmacenMovimientoInfo
            {
                AlmacenID = almacenGeneral.First().AlmacenID,
                /*AnimalMovimientoID = animalMovimientoInfo.AnimalMovimientoID,*/
                TipoMovimientoID = (int)TipoMovimiento.SalidaPorConsumo,
                Status = (int)EstatusInventario.Aplicado,
                Observaciones = "",
                UsuarioCreacionID = corteGanadoGuardarInfo.UsuarioCreacionID,
                /*AnimalID = animalMovimientoInfo.AnimalID,*/
                CostoID = (int)Costo.Alimento,
            };

            //Se genera el cabecero del movimiento
            var resp = almacenBL.GuardarAlmacenMovimiento(almacenMovimientoInfo);
            //Se genera el detalle del movimiento
            if (resp != null)
            {
                almacenMovimientoInfo.AlmacenMovimientoID = resp.AlmacenMovimientoID;
                almacenMovimientoInfo.FolioMovimiento = resp.FolioMovimiento;
                almacenMovimientoInfo.FechaCreacion = resp.FechaCreacion;
                almacenMovimientoInfo.FechaMovimiento = resp.FechaMovimiento;

                //Se obtiene la lista de Consumos a insertar
                var almacenMovimientoDetalle = new AlmacenMovimientoDetalle
                {
                    TratamientoID = 0,
                    ProductoID = almmacenInventarioInfo.ProductoID,
                    Precio = 0,
                    Cantidad = repartoDetalleInfo.CantidadServida,
                    Importe = 0,
                    AlmacenMovimientoID = almacenMovimientoInfo.AlmacenMovimientoID,
                    UsuarioCreacionID = almacenMovimientoInfo.UsuarioCreacionID

                };
                var listaAlmacenMovimientoDetalle = new List<AlmacenMovimientoDetalle> { almacenMovimientoDetalle };


                //Se almacena el detalle del movimiento
                almacenBL.GuardarAlmacenMovimientoDetalle(listaAlmacenMovimientoDetalle,
                                                          almacenMovimientoInfo.AlmacenID);
            }
        }

        /// <summary>
        /// Metodo para validar si los animales del inventario se encuentran en la info de la interfaz
        /// </summary>
        /// <param name="interfaceSalidaAnimal"></param>
        /// <param name="listaAnimales"></param>
        /// <returns></returns>
        private IList<InterfaceSalidaCostoInfo> ValidarIntegridadAretes(List<InterfaceSalidaAnimalInfo> interfaceSalidaAnimal, IList<InterfaceSalidaCostoInfo> listaAnimales, EntradaGanadoInfo entradaGanado)
        {
            var controlEntradaGanadoBL = new ControlEntradaGanadoBL();
            var controlEntrada = controlEntradaGanadoBL.ObtenerControlEntradaGanadoPorID(0, entradaGanado.EntradaGanadoID) ??
                                 new List<ControlEntradaGanadoInfo>();


            //Se obtiene la lista de animales ya costeados de las tablas de control
            List<InterfaceSalidaAnimalInfo> listaCosteada = 
                interfaceSalidaAnimal.Where( animalInfo =>
                    controlEntrada.Any(cEntrada => animalInfo.AnimalID == cEntrada.Animal.AnimalID)).ToList();


            //Se obtiene la interface salida costo de los animales ya costeados
            var result = new List<InterfaceSalidaCostoInfo>();
            foreach (var lCosteada in listaCosteada)
            {
                result.AddRange(listaAnimales.Where(lanimales => lCosteada.Arete == lanimales.Arete));
            }


            var animalesNoRegistradosPartida = new List<InterfaceSalidaCostoInfo>();


            if (result.Any())
            {
                //Se agrega la lista de InterfaceSalidaAnimalInfo ya costeados
                animalesNoRegistradosPartida.AddRange(result);
            }

            return animalesNoRegistradosPartida;
        }
    }
}
