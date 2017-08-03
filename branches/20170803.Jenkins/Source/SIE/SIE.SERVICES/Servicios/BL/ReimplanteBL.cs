using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ReimplanteBL
    {
        /// <summary>
        /// Metrodo Para Reasignar arete o el arete metalico
        /// </summary>
        internal AnimalInfo ReasignarAreteMetalico(AnimalInfo animalInfo, int banderaGuardar)
        {
            AnimalInfo result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ReasignarAreteMetalico(animalInfo, banderaGuardar);
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
        /// Metrodo Para Reasignar arete o el arete metalico
        /// </summary>
        internal DatosCompra ObtenerDatosCompra(AnimalInfo animalInfo)
        {
            DatosCompra result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ObtenerDatosCompra(animalInfo);
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

        internal bool ExisteProgramacionReimplate(int organizacionID)
        {

            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                var result = reimplanteDal.ExisteProgramacionReimplate(organizacionID);

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
        /// Metodo Para Obtener lo el animal de reimplante
        /// </summary>
        internal ReimplanteInfo ObtenerAreteIndividual(AnimalInfo animalInfo, TipoMovimiento corte)
        {
            ReimplanteInfo result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ObtenerAreteIndividual(animalInfo, corte);
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

        internal ReimplanteInfo ObtenerAreteMetalico(AnimalInfo animalInfo, TipoMovimiento corte)
        {
            ReimplanteInfo result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ObtenerAreteMetalico(animalInfo, corte);
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
        /// Metodo para validar el corral destino
        /// </summary>
        internal int ValidarCorralDestino(string corralOrigen, string corralDestino, int idOrganizacion)
        {
            int result = 0;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ValidarCorralDestino(corralOrigen, corralDestino, idOrganizacion);
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

        internal bool ValidarReimplate(AnimalInfo animal)
        {

            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                var result = reimplanteDal.ValidarReimplate(animal);

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

        internal bool ValidarReimplatePorAreteMetalico(AnimalInfo animal)
        {

            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                var result = reimplanteDal.ValidarReimplatePorAreteMetalico(animal);

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
        /// Obtiene el total de cabezas en enfermeria para reimplante
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        internal int ObtenerCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int tipoMovimiento)
        {
            int result;
            try
            {
                Logger.Info();
                var reimplanteDAL = new ReimplanteDAL();
                result = reimplanteDAL.ObtenerCabezasEnEnfermeria(ganadoEnfermeria, tipoMovimiento);
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
        /// Obtiene el numero de cabezas reimplantadas
        /// </summary>
        /// <param name="cabezas"></param>
        /// <returns></returns>
        internal List<CabezasCortadas> ObtenerCabezasReimplantadas(CabezasCortadas cabezas)
        {
            List<CabezasCortadas> result;
            try
            {
                Logger.Info();
                var reimplanteDAL = new ReimplanteDAL();
                result = reimplanteDAL.ObtenerCabezasReimplantadas(cabezas);
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
        /// Obtiene el numero de cabezas muertas para un lote
        /// </summary>
        /// <param name="cabezas"></param>
        /// <returns></returns>
        internal int ObtenerCabezasMuertas(CabezasCortadas cabezas)
        {
            int result;
            try
            {
                Logger.Info();
                var reimplanteDAL = new ReimplanteDAL();
                result = reimplanteDAL.ObtenerCabezasMuertas(cabezas);
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
        /// Obtener los corrales programados para reimplante q no han sido cerrados
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<ProgramacionReinplanteInfo> ObtenerCorralesParaAjuste(int organizacionId)
        {
            List<ProgramacionReinplanteInfo> result;
            try
            {
                Logger.Info();
                var reimplanteDAL = new ReimplanteDAL();
                result = reimplanteDAL.ObtenerCorralesParaAjuste(organizacionId);
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

        internal ReimplanteInfo ObtenerAreteIndividualReimplantar(LoteInfo lote)
        {
            ReimplanteInfo result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ObtenerAreteIndividualReimplantar(lote);
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
        ///  Se valida corral destino si tiene punta chica
        /// </summary>
        /// <param name="corralOrigen"></param>
        /// <param name="corralDestino"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public CorralInfo ValidarCorralDestinoPuntaChica(string corralOrigen, string corralDestino, int organizacionId)
        {
            CorralInfo result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ValidarCorralDestinoPuntaChica(corralOrigen,corralDestino,organizacionId);
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
        /// Metodo para generar la proyeccion de reimplante
        /// </summary>
        internal void GenerarProyeccionReimplante()
        {
            //ResultadoPolizaModel resultadoPolizaModel = null;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                var usuarioBL = new UsuarioBL();
                var loteBL = new LoteBL();
                var corralBl = new CorralBL();
                var loteProyeccionBL = new LoteProyeccionBL();
                var loteReimplanteBL = new LoteReimplanteBL();
                //Se obtiene el usuario del proceso de alimentacion
                UsuarioInfo usuario = usuarioBL.ObtenerPorActiveDirectory(UsuarioProcesosEnum.ProyeccionReimplante.ToString());
                //Si se encontro el usuario
                if (usuario != null)
                {
                    //Consulta todas las organizaciones que esten activas
                    List<OrganizacionInfo> listaOrganizaciones = 
                        organizacionBL.ObtenerTodos(EstatusEnum.Activo).ToList();
                    if (listaOrganizaciones != null && listaOrganizaciones.Any())
                    {
                        //Se obtienen las organizaciones que son de tipo ganadera
                        List<OrganizacionInfo> listaOrganizacionesFiltrada =
                            listaOrganizaciones.Where(
                                organizacion =>
                                    organizacion.TipoOrganizacion.TipoOrganizacionID == (int) TipoOrganizacion.Ganadera)
                                .ToList();
                        if (listaOrganizacionesFiltrada.Count > 0)
                        {
                            foreach (OrganizacionInfo organizacion in listaOrganizacionesFiltrada)
                            {
                                try
                                {
                                    //using (var scope = new TransactionScope())
                                    //{

                                        //Obtener los corrales-Lotes que fueron reimplantados
                                        List<LoteCorralReimplanteInfo> listaCorrales = ObtenerCorralesReimplantados(organizacion);
                                        if (listaCorrales != null && listaCorrales.Any())
                                        {
                                            foreach (var loteCorral in listaCorrales)
                                            {
                                                //Obtener el tipo de ganado del lote
                                                TipoGanadoInfo tipoGanadoInfo = loteBL.ObtenerTipoGanadoLoteID(loteCorral.Lote);
                                                //Info del lote
                                                loteCorral.Lote = loteBL.ObtenerPorID(loteCorral.Lote.LoteID);

                                                //Obtener las proyecciones de los corrales origenes
                                                IList<LoteProyeccionInfo> listaProyeccionOrigen =
                                                    loteProyeccionBL.ObtenerProyeccionDeLotesOrigen(loteCorral);

                                                if (listaProyeccionOrigen != null && listaProyeccionOrigen.Any())
                                                {
                                                    var pesoReimplantePromedio = loteCorral.PesoReimplante/
                                                                                    loteCorral.TotalCabezas;
                                                    var pesoOrigenPromedio = loteCorral.PesoOrigen /
                                                                                    loteCorral.TotalCabezas;

                                                    //ObtenerDias engorda del corral
                                                    var diasEngordaCorral = 
                                                        corralBl.ObtenerDiasEngordaPorLote(loteCorral.Lote);
                                                    //Calcular la Ganacia Diaria = (PesoReimplante-PesoOrigen)/DiasEngordaDelCorral
                                                    decimal gananciaDiaria = 
                                                        ((pesoReimplantePromedio - pesoOrigenPromedio) /
                                                                    (decimal)diasEngordaCorral);
                                                    gananciaDiaria = Math.Round(gananciaDiaria, 2);

                                                    //Calculo diasEngorda: (PesoSalida-PesoPromedioReimplante)/GananciaDiaria
                                                    var diasEngorda = (int) ((tipoGanadoInfo.PesoSalida - pesoReimplantePromedio ) /
                                                                                gananciaDiaria);

                                                    var loteProyeccion =
                                                        loteProyeccionBL.ObtenerPorLote(loteCorral.Lote);
                                                
                                                    LoteReimplanteInfo loteReimplante = null;
                                                    var numeroReimplante = listaProyeccionOrigen.Max(p => p.NumeroReimplante);
                                                

                                                    if (loteProyeccion == null)
                                                    {
                                                        // Se crea el registro en lote proyeccion
                                                        loteProyeccion = new LoteProyeccionInfo
                                                        {
                                                            LoteID = loteCorral.Lote.LoteID, //OK
                                                            OrganizacionID = organizacion.OrganizacionID, //OK
                                                            Frame = 0, //OK
                                                            GananciaDiaria = gananciaDiaria, //OK
                                                            ConsumoBaseHumeda =
                                                                listaProyeccionOrigen.Select(p => p.ConsumoBaseHumeda).Sum()/
                                                                listaProyeccionOrigen.Count(), // Promedio De los Corrales Origenes
                                                            Conversion =
                                                                listaProyeccionOrigen.Select(p => p.Conversion).Sum()/
                                                                listaProyeccionOrigen.Count(), // Promedio De los Corrales Origenes
                                                            PesoMaduro =
                                                                listaProyeccionOrigen.Select(p => p.PesoMaduro).Sum()/
                                                                listaProyeccionOrigen.Count(), // Promedio De los Corrales Origenes
                                                            PesoSacrificio =
                                                                listaProyeccionOrigen.Select(p => p.PesoSacrificio).Sum()/
                                                                listaProyeccionOrigen.Count(), // Promedio De los Corrales Origenes
                                                            DiasEngorda = diasEngorda, // OK
                                                            FechaEntradaZilmax =
                                                                ObtenerFechaZilmax(loteCorral.Lote, tipoGanadoInfo,
                                                                    diasEngorda), //OK
                                                            UsuarioCreacionID = usuario.UsuarioID //OK
                                                        };

                                                        loteProyeccion.LoteProyeccionID =
                                                            loteProyeccionBL.Guardar(loteProyeccion);

                                                        // Fecha Sacrificio = fechaInicioLote + loteProyeccion.DiasEngorda
                                                        DateTime fechaProyectada =
                                                            loteCorral.Lote.FechaInicio.AddDays(loteProyeccion.DiasEngorda);

                                                        DateTime fechaHoy = DateTime.Now;
                                                        TimeSpan ts = fechaProyectada - fechaHoy;
                                                        // GananciaDiaria*DiasQueFlatanParaReimplante) + PesoReimplante
                                                        int pesoProyectado =
                                                            (int)
                                                                ((loteProyeccion.GananciaDiaria*ts.Days) +
                                                                 pesoReimplantePromedio);
                                                        //Se almacena el lote reimplante nuevo
                                                        GuardarNuevoReimplante(numeroReimplante, loteProyeccion, fechaProyectada, usuario, loteCorral, pesoProyectado, pesoOrigenPromedio);
                                                    }
                                                    else
                                                    {
                                                        // Si ya existe el LoteProyeccion Obtener los lotesReimplantes
                                                        List<LoteReimplanteInfo> loteReimplanteInfo =
                                                            loteReimplanteBL.ObtenerListaPorLote(loteCorral.Lote);

                                                        DateTime fechaProyectada =
                                                            loteCorral.Lote.FechaInicio.AddDays(loteProyeccion.DiasEngorda);
                                                        DateTime fechaHoy = DateTime.Now;
                                                        TimeSpan ts = fechaProyectada- fechaHoy ;
                                                        // GananciaDiaria*DiasQueFlatanParaReimplante) + PesoReimplante
                                                        int pesoProyectado =
                                                            (int)
                                                                ((loteProyeccion.GananciaDiaria*ts.Days) +
                                                                 pesoReimplantePromedio);

                                                        if (loteReimplanteInfo != null)
                                                        {
                                                            var numeroReimplanteMaximo = loteReimplanteInfo.Max(p => p.NumeroReimplante);
                                                            if (numeroReimplante == 2 && numeroReimplanteMaximo != 3)
                                                            {
                                                                // El peso Origen sea menor a 200 generar el lote reimplante
                                                                // y que los dias engorda sean mayores a 90 dias
                                                                if (pesoOrigenPromedio <= 200 && loteProyeccion.DiasEngorda >= 90)
                                                                {
                                                                    // Se crea el registro en lote reimplante
                                                                    loteReimplante = new LoteReimplanteInfo
                                                                    {
                                                                        LoteProyeccionID = loteProyeccion.LoteProyeccionID, 
                                                                        NumeroReimplante = 3,                               
                                                                        FechaProyectada = fechaProyectada,                  
                                                                        PesoProyectado = pesoProyectado,                    
                                                                        PesoReal = 0,
                                                                        FechaReal = new DateTime(1900, 01, 01),
                                                                        UsuarioCreacionID = usuario.UsuarioID               
                                                                    };
                                                                    loteReimplanteBL.Guardar(loteReimplante);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //Se almacena el lote reimplante nuevo
                                                            GuardarNuevoReimplante(numeroReimplante, loteProyeccion, fechaProyectada, usuario, loteCorral, pesoProyectado, pesoOrigenPromedio);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    //    scope.Complete();
                                    //}
                                }
                                catch (Exception ex)
                                {

                                    var bitacoraBL = new BitacoraIncidenciasBL();
                                    var bitacora = new BitacoraErroresInfo
                                    {
                                        AccionesSiapID = AccionesSIAPEnum.SerProReim,
                                        Mensaje = ex.Message,
                                        UsuarioCreacionID = usuario.UsuarioID
                                    };
                                    bitacoraBL.GuardarError(bitacora);
                                }
                            }
                        }
                    }
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para generar el nuevo LoteReimplante
        /// </summary>
        /// <param name="numeroReimplante"></param>
        /// <param name="loteProyeccion"></param>
        /// <param name="fechaProyectada"></param>
        /// <param name="usuario"></param>
        /// <param name="loteCorral"></param>
        /// <param name="pesoProyectado"></param>
        private void GuardarNuevoReimplante(int numeroReimplante, LoteProyeccionInfo loteProyeccion, DateTime fechaProyectada, UsuarioInfo usuario, LoteCorralReimplanteInfo loteCorral, int pesoProyectado, int pesoOrigenPromedio)
        {
            var loteReimplanteBL = new LoteReimplanteBL();
            var loteReimplante = new LoteReimplanteInfo();

            if (numeroReimplante == 1)
            {
                loteReimplante = new LoteReimplanteInfo
                {
                    LoteProyeccionID = loteProyeccion.LoteProyeccionID,
                    NumeroReimplante = 2,
                    FechaProyectada = fechaProyectada,
                    PesoProyectado = pesoProyectado,
                    PesoReal = 0,
                    FechaReal = new DateTime(1900,01,01),
                    UsuarioCreacionID = usuario.UsuarioID
                };
                loteReimplanteBL.Guardar(loteReimplante);
            }
            else if (numeroReimplante == 2)
            {
                // El peso Origen sea menor a 200 generar el lote reimplante
                // y que los dias engorda sean mayores a 90 dias
                if (pesoOrigenPromedio <= 200 && loteProyeccion.DiasEngorda >= 90)
                {
                    loteReimplante = new LoteReimplanteInfo
                    {
                        LoteProyeccionID = loteProyeccion.LoteProyeccionID, 
                        NumeroReimplante = 3,                               
                        FechaProyectada = fechaProyectada,                  
                        PesoProyectado = pesoProyectado,                    
                        PesoReal = 0,
                        FechaReal = new DateTime(1900, 01, 01),           
                        UsuarioCreacionID = usuario.UsuarioID               
                    };
                    loteReimplanteBL.Guardar(loteReimplante);
                }
            }
        }

        /// <summary>
        /// Metodo para obtener la fecha zilmax de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="tipoGanadoInfo"></param>
        /// <param name="diasEngorda"></param>
        /// <returns></returns>
        private DateTime ObtenerFechaZilmax(LoteInfo lote, TipoGanadoInfo tipoGanadoInfo, int diasEngorda)
        {
            DateTime result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ObtenerFechaZilmax( lote, tipoGanadoInfo, diasEngorda);
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
        /// Metodo para obtener una lista de corrales que fueron reimpantados
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        private List<LoteCorralReimplanteInfo> ObtenerCorralesReimplantados(OrganizacionInfo organizacion)
        {
            List<LoteCorralReimplanteInfo> result;
            try
            {
                Logger.Info();
                var reimplanteDal = new ReimplanteDAL();
                result = reimplanteDal.ObtenerCorralesReimplantados(organizacion);
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
