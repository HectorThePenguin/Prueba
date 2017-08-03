using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using System.Linq;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Filtros;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class EnfermeriaBL
    {
        /// <summary>
        /// Obtiene los corrales con ganado detectado enfermo
        /// </summary>
        /// <param name="organizacionId">Organizacion</param>
        /// <param name="pagina">Configuracion de paginacion</param>
        /// <returns>Lista de corrales con ganado detectado</returns>
        internal ResultadoInfo<EnfermeriaInfo> ObtenerCorralesConGanadoDetectadoEnfermo(int organizacionId, PaginacionInfo pagina)
        {
            ResultadoInfo<EnfermeriaInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerCorralesConGanadoDetectadoEnfermo(organizacionId, pagina);

                if (result != null)
                {
                    foreach (var corralesConEnfermos in result.Lista)
                    {
                        corralesConEnfermos.ListaAnimales = enfermeriaDal.ObtenerAnimalesDetectadosEnfermosPorCorral(organizacionId, corralesConEnfermos.Corral.CorralID);

                        if (corralesConEnfermos.FolioEntrada == 0) continue;
                        var datosCompra = ObtenerDatosCompra(corralesConEnfermos.FolioEntrada, organizacionId);
                        if (datosCompra == null) continue;
                        corralesConEnfermos.Organizacion = datosCompra.Origen;
                        corralesConEnfermos.TipoOrganizacion = datosCompra.Origen;
                        corralesConEnfermos.TipoOrigen = datosCompra.TipoOrigen;

                        if (corralesConEnfermos.ListaAnimales != null)
                        {
                            foreach (var animal in corralesConEnfermos.ListaAnimales)
                            {
                                animal.EnfermeriaCorral.TipoOrganizacion = datosCompra.Origen;
                                animal.EnfermeriaCorral.Organizacion = datosCompra.Origen;
                                animal.EnfermeriaCorral.TipoOrigen = datosCompra.TipoOrigen;
                            }
                        }
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
            return result;
        }

        /// <summary>
        /// Obtine el animal detectado
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal AnimalDeteccionInfo ObtenerAnimalDetectadoPorArete(AnimalInfo animal)
        {
            AnimalDeteccionInfo result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerAnimalDetectadoPorArete(animal);
                if (result != null)
                {
                    var datosCompra = enfermeriaDal.ObtenerDatosCompra(result.EnfermeriaCorral.FolioEntrada, animal.OrganizacionIDEntrada);
                    if (datosCompra != null)
                    {
                        result.EnfermeriaCorral.TipoOrigen = datosCompra.TipoOrigen;
                        result.EnfermeriaCorral.Organizacion = datosCompra.Origen;
                        result.EnfermeriaCorral.TipoOrganizacion = datosCompra.Origen;
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
            return result;
        }
        /// <summary>
        /// Obtiene el ultimo movimiento de enfermeria
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal AnimalMovimientoInfo ObtenerUltimoMovimientoEnfermeria(AnimalInfo animal)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerUltimoMovimientoEnfermeria(animal);
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
            return result;
        }
        /// <summary>
        /// Obtiene los problemas
        /// </summary>
        /// <returns></returns>
        internal List<ProblemaInfo> ObtenerProblemas()
        {
            List<ProblemaInfo> result;
            try
            {
                Logger.Info();
                var problemaDal = new ProblemaDAL();
                result = problemaDal.ObtenerListaProblemasNecropsia();
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
            return result;
        }
        /// <summary>
        /// Obtiene los grados
        /// </summary>
        /// <returns></returns>
        internal IList<GradoInfo> ObtenerGrados()
        {
            IList<GradoInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerGrados();
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
            return result;
        }
        /// <summary>
        /// Obtiene el ultimo movimiento de recuperaacion
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal AnimalMovimientoInfo ObtenerUltimoMovimientoRecuperacion(AnimalInfo animalInfo)
        {
            AnimalMovimientoInfo result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerUltimoMovimientoRecuperacion(animalInfo);
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
            return result;
        }

        /// <summary>
        /// Obtine el animal detectado
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>  
        internal IList<HistorialClinicoInfo> ObtenerHistorialClinico(AnimalInfo animal)
        {
            IList<HistorialClinicoInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerHistorialClinico(animal);
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
            return result;
        }
        /// <summary>
        /// Obtiene el costo de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamientoId">Identificador del producto</param>
        /// <returns></returns>
        internal decimal ObtenerCosto(AnimalMovimientoInfo movimiento, int tratamientoId)
        {
            decimal result;
            try
            {
                Logger.Info();
                var tratamientoDal = new TratamientoDAL();
                result = tratamientoDal.ObtenerCostoPorMovimiento(movimiento, tratamientoId);
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
            return result;
        }
        /// <summary>
        /// Obtiene el costo de un producto de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamiento">Identificador del producto</param>
        /// <returns></returns>
        internal decimal ObtenerCostoProducto(AnimalMovimientoInfo movimiento, TratamientoProductoInfo tratamiento)
        {
            decimal result;
            try
            {
                Logger.Info();
                var tratamientoDal = new TratamientoDAL();
                result = tratamientoDal.ObtenerCostoPorMovimientoProducto(movimiento, tratamiento);
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
            return result;
        }
        /// <summary>
        /// Elimina la deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <returns></returns>
        internal int EliminarDeteccion(DeteccionInfo deteccion)
        {
            int result;
            try
            {
                Logger.Info();
                var enfermeriDal = new EnfermeriaDAL();
                result = enfermeriDal.EliminarDeteccion(deteccion);
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
            return result;

        }
        /// <summary>
        /// Guarda los datos de la deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <param name="problemasDetectados"></param>
        /// <returns></returns>
        internal int GurdarDeteccion(AnimalDeteccionInfo deteccion, IList<ProblemaInfo> problemasDetectados)
        {
            int result;
            try
            {
                Logger.Info();
                var enfermeriDal = new EnfermeriaDAL();
                result = enfermeriDal.GurdarDeteccion(deteccion, problemasDetectados);
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
            return result;

        }
        /// <summary>
        /// Guardar entrada enfermeria
        /// </summary>
        internal EntradaGanadoEnfermeriaInfo GuardarEntradaEnfermeria(EntradaGanadoEnfermeriaInfo entradaGanadoEnfermeria)
        {
            entradaGanadoEnfermeria.Resultado = false;
            try
            {
                var resultadoCabezas = new CabezasActualizadasInfo();
                var transactionOption = new TransactionOptions();
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                {
                    var animalDal = new AnimalDAL();
                    var animalMovimientoBL = new AnimalMovimientoBL();
                    var corralBL = new CorralBL();
                    var loteBL = new LoteBL();

                    bool actualizaAreteDeteccion = entradaGanadoEnfermeria.Deteccion.ActualizarAreteDeteccion;
                    bool animalRecaido = entradaGanadoEnfermeria.AnimalRecaido;

                    if (entradaGanadoEnfermeria.CambiarTipoGanado &&
                        entradaGanadoEnfermeria.Deteccion.Animal.AnimalID > 0 &&
                        entradaGanadoEnfermeria.Deteccion.Animal.TipoGanadoID != entradaGanadoEnfermeria.Animal.TipoGanadoID)
                    {
                        entradaGanadoEnfermeria.Animal.AnimalID = entradaGanadoEnfermeria.Deteccion.Animal.AnimalID;
                        entradaGanadoEnfermeria.Animal.UsuarioModificacionID = entradaGanadoEnfermeria.UsuarioId;
                    }
                    if (entradaGanadoEnfermeria.Animal.AnimalID == 0)
                    {
                        entradaGanadoEnfermeria.Animal.AnimalID = entradaGanadoEnfermeria.Deteccion.Animal.AnimalID;
                        entradaGanadoEnfermeria.Animal.AplicaBitacora = entradaGanadoEnfermeria.Deteccion.Animal.AplicaBitacora;
                    }
                    AnimalInfo animalInfo = animalDal.GuardarAnimal(entradaGanadoEnfermeria.Animal);
                    if (actualizaAreteDeteccion)
                    {
                        //Si el flag esta activo se actualiza el arete en la deteccion de ganado cuando solo traen fotos
                        var deteccionBl = new DeteccionBL();
                        deteccionBl.ActualizarDeteccionConFoto(entradaGanadoEnfermeria.Deteccion);
                    }
                    /* Si esta activo el flag de recaido generar la deteccion */
                    if (animalRecaido)
                    {
                        entradaGanadoEnfermeria.Deteccion.DeteccionID = GenerarDeteccionAnimalGenerica(entradaGanadoEnfermeria);
                    }

                    if (animalInfo != null && animalInfo.AnimalID > 0)
                    {
                        entradaGanadoEnfermeria.Animal = animalInfo;
                        entradaGanadoEnfermeria.Movimiento.AnimalID = animalInfo.AnimalID;
                        if (entradaGanadoEnfermeria.LoteDestino.LoteID == 0)
                        {
                            entradaGanadoEnfermeria.LoteDestino.LoteID = loteBL.GuardaLote(entradaGanadoEnfermeria.LoteDestino);
                            entradaGanadoEnfermeria.Movimiento.LoteID = entradaGanadoEnfermeria.LoteDestino.LoteID;
                        }
                        //Se almacena el movimiento
                        AnimalMovimientoInfo animalMovimientoInfo =
                            animalMovimientoBL.GuardarAnimalMovimiento(entradaGanadoEnfermeria.Movimiento);
                        if (animalMovimientoInfo != null && animalMovimientoInfo.AnimalMovimientoID > 0)
                        {
                            if (entradaGanadoEnfermeria.Tratamientos.Any(registro => registro.Seleccionado))
                            {
                                var almacenpl = new AlmacenBL();
                                entradaGanadoEnfermeria.AlmacenMovimiento.AnimalMovimientoID =
                                    animalMovimientoInfo.AnimalMovimientoID;
                                entradaGanadoEnfermeria.AlmacenMovimiento.AnimalID = animalInfo.AnimalID;
                                almacenpl.GuardarDescontarTratamientos(entradaGanadoEnfermeria.Tratamientos,
                                    entradaGanadoEnfermeria.AlmacenMovimiento);
                            }
                            entradaGanadoEnfermeria.Deteccion.AnimalMovimiento = animalMovimientoInfo;
                            GurdarDeteccion(entradaGanadoEnfermeria.Deteccion, entradaGanadoEnfermeria.ListaProblemas);

                            /* Se desactiva la deteccion */
                            var deteccion = new DeteccionInfo
                            {
                                DeteccionID = entradaGanadoEnfermeria.Deteccion.DeteccionID,
                                UsuarioCreacionID = entradaGanadoEnfermeria.UsuarioId,
                                Arete = entradaGanadoEnfermeria.Animal.Arete
                            };
                            EliminarDeteccion(deteccion);
                            #region LOTE
                            //Se decrementan las cabezas del lote
                            if (entradaGanadoEnfermeria.LoteDestino.LoteID != entradaGanadoEnfermeria.LoteOrigen.LoteID)
                            {
                                entradaGanadoEnfermeria.LoteDestino = loteBL.ObtenerPorID(entradaGanadoEnfermeria.LoteDestino.LoteID);
                                var animalBL = new AnimalBL();
                                List<AnimalInfo> animalesDestino =
                                    animalBL.ObtenerAnimalesPorLote(entradaGanadoEnfermeria.LoteOrigen.OrganizacionID,
                                                                    entradaGanadoEnfermeria.LoteDestino.LoteID);
                                if (animalesDestino != null && animalesDestino.Any())
                                {
                                    entradaGanadoEnfermeria.LoteDestino.Cabezas = animalesDestino.Count;
                                }
                                //Una vez insertado el lote y el animal se incrementan las cabezas de lote
                                entradaGanadoEnfermeria.LoteDestino.Cabezas =
                                    entradaGanadoEnfermeria.LoteDestino.Cabezas + 1;
                                if (entradaGanadoEnfermeria.LoteDestino.Cabezas > entradaGanadoEnfermeria.LoteDestino.CabezasInicio)
                                {
                                    entradaGanadoEnfermeria.LoteDestino.CabezasInicio =
                                    entradaGanadoEnfermeria.LoteDestino.CabezasInicio + 1;
                                }
                                entradaGanadoEnfermeria.LoteDestino.UsuarioModificacionID =
                                    entradaGanadoEnfermeria.Movimiento.UsuarioCreacionID;
                                // ------ //
                                entradaGanadoEnfermeria.LoteOrigen.Cabezas =
                                    entradaGanadoEnfermeria.LoteOrigen.Cabezas - 1;

                                List<AnimalInfo> animales =
                                    animalBL.ObtenerAnimalesPorLote(entradaGanadoEnfermeria.LoteOrigen.OrganizacionID,
                                                                    entradaGanadoEnfermeria.LoteOrigen.LoteID);
                                if (animales != null && animales.Any())
                                {
                                    entradaGanadoEnfermeria.LoteOrigen.Cabezas = animales.Count;
                                }
                                //Se actualizan las cabezas que tiene el lote
                                var filtro = new FiltroActualizarCabezasLote
                                {
                                    CabezasProcesadas = 1,
                                    LoteIDDestino = entradaGanadoEnfermeria.LoteDestino.LoteID,
                                    LoteIDOrigen = entradaGanadoEnfermeria.LoteOrigen.LoteID,
                                    UsuarioModificacionID = entradaGanadoEnfermeria.Movimiento.UsuarioCreacionID
                                };

                                resultadoCabezas = loteBL.ActualizarCabezasProcesadas(filtro);

                            }
                            #endregion LOTE
                            //Si ya no tenemos cabezas en el lote se actualizanb los pesos llegada
                            if (resultadoCabezas.CabezasOrigen <= 0)
                            {
                                //Se obtiene el Corral para ver Si es de Recepcion
                                CorralInfo corralInfo = corralBL.ObtenerCorralPorCodigo(entradaGanadoEnfermeria.LoteOrigen.OrganizacionID,
                                                                                        entradaGanadoEnfermeria.LoteOrigen.Corral.Codigo);
                                if (corralInfo.GrupoCorral == (int)GrupoCorralEnum.Recepcion)
                                {
                                    /* Si el corral es de Recepcion */
                                    var corteGanadoPl = new CorteGanadoBL();
                                    corteGanadoPl.ObtenerPesosOrigenLlegada(entradaGanadoEnfermeria.LoteOrigen.OrganizacionID,
                                                                             entradaGanadoEnfermeria.LoteOrigen.CorralID,
                                                                             entradaGanadoEnfermeria.LoteOrigen.LoteID);
                                }
                            }
                            transaction.Complete();
                            entradaGanadoEnfermeria.Resultado = true;
                        }
                    }
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                entradaGanadoEnfermeria.Resultado = false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                entradaGanadoEnfermeria.Resultado = false;
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradaGanadoEnfermeria;
        }

        /// <summary>
        /// Metodo para generar una deteccion generica
        /// </summary>
        /// <param name="entradaGanadoEnfermeria"></param>
        private int GenerarDeteccionAnimalGenerica(EntradaGanadoEnfermeriaInfo entradaGanadoEnfermeria)
        {
            try
            {
                Logger.Info();
                var deteccionBl = new DeteccionBL();
                int result = deteccionBl.GuardarDeteccionGenerica(entradaGanadoEnfermeria.Deteccion.DeteccionID);
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
        /// Obtiene el animal 
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal AnimalSalidaInfo AnimalSalidaEnfermeria(AnimalInfo animalInfo)
        {
            AnimalSalidaInfo result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.AnimalSalidaEnfermeria(animalInfo);
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
            return result;
        }

        /// <summary>
        /// Método para consultar las enfermerias que le pertenecen al operador
        /// </summary>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal List<EnfermeriaInfo> ObtenerEnfermeriasPorOperadorID(int operadorId)
        {
            List<EnfermeriaInfo> result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerEnfermeriasPorOperadorID(operadorId);
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
            return result;
        }
        /// <summary>
        /// Obtiene los datos de la compra
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organicacionId"></param>
        /// <returns></returns>
        internal DatosCompra ObtenerDatosCompra(int folioEntrada, int organicacionId)
        {
            DatosCompra result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerDatosCompra(folioEntrada, organicacionId);
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
            return result;
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EnfermeriaInfo> ObtenerPorPagina(PaginacionInfo pagina, EnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                var enfermeriaDAL = new EnfermeriaDAL();
                ResultadoInfo<EnfermeriaInfo> result = enfermeriaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una entidad Enfermeria por su Id
        /// </summary>
        /// <param name="enfermeriaId">Obtiene una entidad Enfermeria por su Id</param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorID(int enfermeriaId)
        {
            try
            {
                Logger.Info();
                var enfermeriaDAL = new EnfermeriaDAL();
                EnfermeriaInfo result = enfermeriaDAL.ObtenerPorID(new EnfermeriaInfo { EnfermeriaID = enfermeriaId });
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
        /// Obtiene una entidad Enfermeria por su Id
        /// </summary>
        /// <param name="filtro">Obtiene una entidad Enfermeria por su Id</param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorID(EnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                var enfermeriaDAL = new EnfermeriaDAL();
                EnfermeriaInfo result = enfermeriaDAL.ObtenerPorID(filtro);
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
        /// Metodo para Guardar/Modificar una entidad Enfermeria
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(EnfermeriaInfo info)
        {
            try
            {
                int result = info.EnfermeriaID;
                using (var scope = new TransactionScope())
                {
                    Logger.Info();
                    var enfermeriaDAL = new EnfermeriaDAL();

                    if (info.EnfermeriaID == 0)
                    {
                        result = enfermeriaDAL.Crear(info);
                    }
                    else
                    {
                        enfermeriaDAL.Actualizar(info);
                        if (info.Activo == EstatusEnum.Inactivo)
                        {
                            info.Corrales = null;
                            InactivarEnfermeriaCorralYSupervisorEnfermeria(info.EnfermeriaID);
                        }
                    }

                    if (info.Corrales != null && info.Corrales.Any())
                    {
                        GuardarEnfermeriaCorrales(info, result);
                    }

                    scope.Complete();
                }
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

        private void InactivarEnfermeriaCorralYSupervisorEnfermeria(int enfermeriaID)
        {
            var enfermeriaCorralDAL = new EnfermeriaCorralDAL();
            enfermeriaCorralDAL.InactivarEnfermeriaCorralYSupervisorEnfermeria(enfermeriaID);
        }

        private void GuardarEnfermeriaCorrales(EnfermeriaInfo info, int enfermeriaID)
        {
            var enfermeriaCorralDAL = new EnfermeriaCorralDAL();
            List<EnfermeriaCorralInfo> listaEnfermeriasCorral =
                enfermeriaCorralDAL.ObtenerCorralesPorEnfermeriaID(enfermeriaID);

            var listaEnfermeriasCorralGuardar = new List<EnfermeriaCorralInfo>();
            bool tieneCorrales = listaEnfermeriasCorral != null;
            foreach (var corral in info.Corrales)
            {
                var enfermeriaCorralGuardar = new EnfermeriaCorralInfo();
                if (tieneCorrales)
                {
                    var enfermeriaCorralExistente =
                        listaEnfermeriasCorral.FirstOrDefault(enf => enf.Corral.CorralID == corral.CorralID);
                    if (enfermeriaCorralExistente != null)
                    {
                        if (enfermeriaCorralExistente.Activo == corral.Activo)
                        {
                            continue;
                        }
                        enfermeriaCorralGuardar.EnfermeriaCorralID = enfermeriaCorralExistente.EnfermeriaCorralID;
                        enfermeriaCorralGuardar.UsuarioModificacionID = corral.UsuarioCreacionID;
                    }
                    else
                    {
                        enfermeriaCorralGuardar.UsuarioCreacionID = corral.UsuarioCreacionID;
                    }
                }
                else
                {
                    enfermeriaCorralGuardar.UsuarioCreacionID = corral.UsuarioCreacionID;
                }
                enfermeriaCorralGuardar.Activo = corral.Activo;
                enfermeriaCorralGuardar.EnfermeriaID = enfermeriaID;
                enfermeriaCorralGuardar.CorralID = corral.CorralID;
                listaEnfermeriasCorralGuardar.Add(enfermeriaCorralGuardar);
            }
            if (listaEnfermeriasCorralGuardar.Any())
            {
                enfermeriaCorralDAL.GuardarEnfermeriaCorral(listaEnfermeriasCorralGuardar, enfermeriaID);
            }
        }

        /// <summary>
        /// Obtiene una entidad Enfermeria por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorDescripcion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                var enfermeriaDAL = new EnfermeriaDAL();
                EnfermeriaInfo result = enfermeriaDAL.ObtenerPorDescripcion(descripcion, organizacionID);
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

        internal AnimalDeteccionInfo ObtenerAnimalDetectadoPorAreteSinActivo(AnimalInfo animalEnfermo)
        {
            AnimalDeteccionInfo result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerAnimalDetectadoPorAreteSinActivo(animalEnfermo);
                if (result != null)
                {
                    var datosCompra = enfermeriaDal.ObtenerDatosCompra(result.EnfermeriaCorral.FolioEntrada, animalEnfermo.OrganizacionIDEntrada);
                    if (datosCompra != null)
                    {
                        result.EnfermeriaCorral.TipoOrigen = datosCompra.TipoOrigen;
                        result.EnfermeriaCorral.Organizacion = datosCompra.Origen;
                        result.EnfermeriaCorral.TipoOrganizacion = datosCompra.Origen;
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
            return result;
        }

        /// <summary>
        /// Metodo para obtener la ultima deteccion que sele realizo al animal
        /// </summary>
        /// <param name="animalEnfermo"></param>
        /// <returns></returns>
        internal AnimalDeteccionInfo ObtenerAnimalDetectadoPorAreteUltimaDeteccion(AnimalInfo animalEnfermo)
        {
            AnimalDeteccionInfo result;
            try
            {
                Logger.Info();
                var enfermeriaDal = new EnfermeriaDAL();
                result = enfermeriaDal.ObtenerAnimalDetectadoPorAreteUltimaDeteccion(animalEnfermo);
                if (result != null)
                {
                    var datosCompra = enfermeriaDal.ObtenerDatosCompra(result.EnfermeriaCorral.FolioEntrada, animalEnfermo.OrganizacionIDEntrada);
                    if (datosCompra != null)
                    {
                        result.EnfermeriaCorral.TipoOrigen = datosCompra.TipoOrigen;
                        result.EnfermeriaCorral.Organizacion = datosCompra.Origen;
                        result.EnfermeriaCorral.TipoOrganizacion = datosCompra.Origen;
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
            return result;
        }
    }
}
