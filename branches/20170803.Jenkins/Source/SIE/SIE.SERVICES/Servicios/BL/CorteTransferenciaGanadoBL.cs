using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class CorteTransferenciaGanadoBL
    {

        /// <summary>
        /// Obtiene el permiso de la trampa
        /// </summary>
        /// <param name="trampaInfo"></param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPermisoTrampa(TrampaInfo trampaInfo)
        {
            TrampaInfo result;
            try
            {
                Logger.Info();
                var permisoDAL = new CorteTransferenciaGanadoDAL();
                result = permisoDAL.ObtenerPermisoTrampa(trampaInfo);
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
        /// obtiene la entrada de ganado
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradaGanado(AnimalInfo animalInfo)
        {
            EntradaGanadoInfo result;
            try
            {
                Logger.Info();
                var permisoDAL = new CorteTransferenciaGanadoDAL();
                result = permisoDAL.ObtenerEntradaGanado(animalInfo);
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
        /// Obtener totales de cabezas
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal CorteTransferenciaTotalCabezasInfo ObtenerTotales(AnimalMovimientoInfo animalInfo)
        {
            CorteTransferenciaTotalCabezasInfo result;


            try
            {
                Logger.Info();
                var objCorteTransferencia = new CorteTransferenciaGanadoDAL();
                result = objCorteTransferencia.ObtenerTotales(animalInfo);


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
        /// Obtener corrales por tipo
        /// </summary>

        internal CorralInfo ObtenerCorralesTipo(TipoCorralInfo tipoCorralInfo, int organizationID)
        {
            CorralInfo result;

            try
            {
                Logger.Info();
                var corteTransferenciaGanado = new CorteTransferenciaGanadoDAL();
                result = corteTransferenciaGanado.ObtenerCorralesTipo(tipoCorralInfo, organizationID);
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
        /// Obtener tratamientos aplicados
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        internal List<TratamientoInfo> ObtenerTratamientosAplicados(AnimalInfo animalInfo, int tipoMovimiento)
        {
            List<TratamientoInfo> result;

            try
            {
                Logger.Info();
                var corteTransferenciaGanado = new CorteTransferenciaGanadoDAL();
                result = corteTransferenciaGanado.ObtenerTratamientosAnimal(animalInfo, tipoMovimiento);
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
        /// guardar corte por transferencia
        /// </summary>
        /// <param name="guardarAnimalCorte"></param>
        /// <param name="listaTratamientos"></param>
        /// <returns></returns>
        internal CorteTransferenciaGanadoGuardarInfo GuardarCorteTransferencia(CorteTransferenciaGanadoGuardarInfo guardarAnimalCorte, IList<TratamientoInfo> listaTratamientos)
        {

            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    //var corteTransferenciaGanado = new CorteTransferenciaGanadoDAL();
                    var animalBl = new AnimalBL();
                    var animalMovimientoBL = new AnimalMovimientoBL();
                    var corralBl = new CorralBL();
                    var interfaceSalidaAnimalBl = new InterfaceSalidaAnimalBL();
                    var interfaceSalidoAnimalInfo =
                        interfaceSalidaAnimalBl.ObtenerNumeroAreteIndividual(
                            guardarAnimalCorte.AnimalCorteTransferenciaInfo.Arete, guardarAnimalCorte.OrganizacionId);

                    if (guardarAnimalCorte.AnimalActualInfo != null)
                    {
                        guardarAnimalCorte.AnimalCorteTransferenciaInfo.FechaCompra =
                            guardarAnimalCorte.AnimalActualInfo.FechaCompra;
                    }
                    else
                    {
                        if (interfaceSalidoAnimalInfo != null)
                        {
                            guardarAnimalCorte.AnimalCorteTransferenciaInfo.FechaCompra =
                                interfaceSalidoAnimalInfo.FechaCompra;
                        }
                    }

                    var resultoAnimalInfo = animalBl.GuardarAnimal(guardarAnimalCorte.AnimalCorteTransferenciaInfo);
                    guardarAnimalCorte.AnimalCorteTransferenciaInfo = resultoAnimalInfo;
                    guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.AnimalID =
                        guardarAnimalCorte.AnimalCorteTransferenciaInfo.AnimalID;

                    var loteBl = new LoteBL();
                    var resultadoLote = -1;
                    var resultadoLoteOrigen = loteBl.ObtenerPorID(guardarAnimalCorte.AnimalMovimientoOrigenInfo.LoteID);
                    var resultadoLoteBL =
                        loteBl.ObtenerLotesActivos(
                            guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.OrganizacionID,
                            Convert.ToInt32(guardarAnimalCorte.CorralInfoGen.CorralID));
                    if (resultadoLoteBL == null)
                    {
                        //var corralInfo = new CorralInfo
                        //{
                        //    Codigo = guardarAnimalCorte.TxtCorralDestino,
                        //    TipoCorral = new TipoCorralInfo { TipoCorralID = (int)TipoCorral.Improductivos },
                        //    Organizacion = new OrganizacionInfo { OrganizacionID = guardarAnimalCorte.OrganizacionId }
                        //};
                        CorralInfo resultadoCorralPl = corralBl.ObtenerPorId(guardarAnimalCorte.CorralDestinoID);
                        if (resultadoCorralPl != null)
                        {
                            var loteCrear = new LoteBL();
                            var tipoProcesoBl = new TipoProcesoBL();
                            var tipoProcesoResult =
                                tipoProcesoBl.ObtenerPorOrganizacion(guardarAnimalCorte.OrganizacionId);
                            var loteInfoLote = new LoteInfo
                                               {
                                                   Activo = EstatusEnum.Activo,
                                                   Cabezas = Convert.ToInt32(0),
                                                   CabezasInicio = Convert.ToInt32(0),
                                                   CorralID = resultadoCorralPl.CorralID,
                                                   DisponibilidadManual = false,
                                                   OrganizacionID =
                                                       guardarAnimalCorte.AnimalMovimientoOrigenInfo.OrganizacionID,
                                                   TipoCorralID = resultadoCorralPl.TipoCorral.TipoCorralID,
                                                   TipoProcesoID = tipoProcesoResult,
                                                   UsuarioCreacionID = resultadoCorralPl.UsuarioCreacionID,
                                               };
                            resultadoLote = loteCrear.GuardaLote(loteInfoLote);
                            guardarAnimalCorte.LoteDestinoInfo = loteCrear.ObtenerPorID(resultadoLote);
                        }
                    }
                    else
                    {
                        guardarAnimalCorte.LoteDestinoInfo = resultadoLoteBL;
                    }
                    //Se manda a guardar movimiento de salida de enfermeria
                    //var animalSalidaInfo = new AnimalSalidaInfo();
                    //animalSalidaInfo = animalBl.ObtenerAnimalSalidaAnimalID(guardarAnimalCorte.AnimalCorteTransferenciaInfo.AnimalID);

                    //guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.CorralID = animalSalidaInfo.CorralId;
                    //guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.LoteID = animalSalidaInfo.LoteId;
                    //guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.TipoMovimientoID = (int)TipoMovimiento.SalidaEnfermeria;
                    //guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo = animalMovimientoBL.GuardarAnimalMovimiento(guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo);

                    //Se manda a guardar movimiento de corte por transferencia
                    guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.CorralID =
                        guardarAnimalCorte.CorralInfoGen.CorralID;
                    guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.LoteID = resultadoLoteBL != null
                        ? resultadoLoteBL.LoteID
                        : resultadoLote;
                    guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.TipoMovimientoID =
                        (int)TipoMovimiento.CortePorTransferencia;
                    guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo =
                        animalMovimientoBL.GuardarAnimalMovimiento(
                            guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo);

                    if (guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo != null &&
                        guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo.AnimalMovimientoID > 0)
                    {
                        if (resultadoLoteOrigen != null && guardarAnimalCorte.LoteDestinoInfo != null)
                        {
                            guardarAnimalCorte.CorralGlobal = guardarAnimalCorte.AnimalMovimientoOrigenInfo.CorralID;
                            guardarAnimalCorte.LoteDestinoInfo.Cabezas = guardarAnimalCorte.LoteDestinoInfo.Cabezas + 1;
                            guardarAnimalCorte.LoteDestinoInfo.CabezasInicio =
                                guardarAnimalCorte.LoteDestinoInfo.CabezasInicio + 1;
                            resultadoLoteOrigen.Cabezas = resultadoLoteOrigen.Cabezas - 1;
                            var filtroActualizaCabezas = new FiltroActualizarCabezasLote
                                                         {
                                                             LoteIDOrigen = resultadoLoteOrigen.LoteID,
                                                             LoteIDDestino = guardarAnimalCorte.LoteDestinoInfo.LoteID,
                                                             CabezasProcesadas = 1,
                                                             UsuarioModificacionID =
                                                                 guardarAnimalCorte.UsuarioCreacionID
                                                         };
                            var cabezas = loteBl.ActualizarCabezasProcesadas(filtroActualizaCabezas);
                            //TODO nuevo metodo
                            //loteBl.ActualizaNoCabezasEnLote(guardarAnimalCorte.LoteDestinoInfo, resultadoLoteOrigen);
                            loteBl.ActualizarSalidaEnfermeria(guardarAnimalCorte.AnimalMovimientoOrigenInfo);
                            loteBl.ActualizarFechaSalidaEnLote(resultadoLoteOrigen);
                            //if (resultadoLoteOrigen.Cabezas == 0)
                            //{
                            //    resultadoLoteOrigen.Activo = (int)EstatusEnum.Inactivo;
                            //    loteBl.ActualizaActivoEnLote(resultadoLoteOrigen);
                            //}
                            // ObtenerTotalCabezas(loteCorralOrigen);    
                        }
                        if (ObtenerTotalCabezas(guardarAnimalCorte.AnimalMovimientoOrigenInfo))
                        {
                            corralBl.TraspasarAnimalSalidaEnfermeria(
                                guardarAnimalCorte.AnimalSalidaGuardarInfo.CorraletaId,
                                guardarAnimalCorte.AnimalMovimientoOrigenInfo.LoteID);
                            //loteDAL.EliminarSalidaEnfermeria(guardarAnimalCorte.AnimalMovimientoOrigenInfo);
                        }
                    }

                    if (guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo != null)
                    {
                        var almacenMovimientoInfo = new AlmacenMovimientoInfo
                                                    {
                                                        AlmacenID = guardarAnimalCorte.AlmacenID,
                                                        AnimalMovimientoID =
                                                            guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo
                                                            .AnimalMovimientoID,
                                                        TipoMovimientoID = (int)TipoMovimiento.SalidaPorConsumo,
                                                        Status = (int)EstatusInventario.Aplicado,
                                                        Observaciones = "",
                                                        UsuarioCreacionID = guardarAnimalCorte.UsuarioCreacionID,
                                                        AnimalID =
                                                            guardarAnimalCorte.AnimalMovimientoCorteTransferenciaInfo
                                                            .AnimalID,
                                                        //Verificar
                                                        CostoID = (int)Costo.MedicamentoDeImplante,
                                                    };
                        var almacenBL = new AlmacenBL();
                        almacenBL.GuardarDescontarTratamientos(
                            listaTratamientos.Where(item => item.Seleccionado && item.Habilitado).ToList(),
                            almacenMovimientoInfo);
                    }
                    var loteProyeccionBL = new LoteProyeccionBL();
                    LoteProyeccionInfo proyeccionDestino =
                        loteProyeccionBL.ObtenerPorLote(guardarAnimalCorte.LoteDestinoInfo);
                    //guardarAnimalCorte.LoteDestinoInfo.LoteID
                    if (proyeccionDestino == null)
                    {
                        LoteProyeccionInfo proyeccionOrigen = loteProyeccionBL.ObtenerPorLote(resultadoLoteOrigen);

                        if (proyeccionOrigen != null && resultadoLoteOrigen != null && guardarAnimalCorte.LoteDestinoInfo != null)
                        {
                            proyeccionOrigen.LoteProyeccionID = 0;
                            int diasEngordaOrigen = proyeccionOrigen.DiasEngorda;
                            DateTime fechaInicioLote = resultadoLoteOrigen.FechaInicio;
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

                            proyeccionOrigen.LoteID = guardarAnimalCorte.LoteDestinoInfo.LoteID;
                            proyeccionOrigen.UsuarioCreacionID = guardarAnimalCorte.UsuarioCreacionID;
                            loteProyeccionBL.Guardar(proyeccionOrigen);

                            var loteDAL = new LoteDAL();
                            var filtroDisponibilidad = new FiltroDisponilidadInfo
                                                       {
                                                           UsuarioId = guardarAnimalCorte.UsuarioCreacionID,
                                                           ListaLoteDisponibilidad =
                                                               new List<DisponibilidadLoteInfo>
                                                               {
                                                                   new DisponibilidadLoteInfo
                                                                   {
                                                                       LoteId = guardarAnimalCorte.LoteDestinoInfo.LoteID,
                                                                       FechaDisponibilidad =
                                                                           resultadoLoteOrigen.FechaDisponibilidad,
                                                                       DisponibilidadManual =
                                                                           resultadoLoteOrigen.DisponibilidadManual
                                                                   }
                                                               }
                                                       };
                            loteDAL.ActualizarLoteDisponibilidad(filtroDisponibilidad);
                        }

                    }

                    transaction.Complete();
                    return guardarAnimalCorte;
                }
            }
            catch (ExcepcionGenerica)
            {
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener total de cabezas
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        /// 
        private bool ObtenerTotalCabezas(AnimalMovimientoInfo animalInfo)
        {
            var objObtenerCabezas = new CorteTransferenciaGanadoDAL();
            var resultado = objObtenerCabezas.ObtenerTotales(animalInfo);
            if (resultado == null) return false;
            if (resultado.TotalCortadas != resultado.Total) return false;
            return true;
        }

    }
}
