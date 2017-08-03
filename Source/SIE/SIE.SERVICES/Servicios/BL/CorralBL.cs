using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class CorralBL
    {
        internal ResultadoInfo<CorralInfo> ObtenerPorPagina(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                result = corralDAL.ObtenerPorPagina(pagina, filtro);
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

        internal ResultadoInfo<CorralInfo> ObtenerPorDependencia(PaginacionInfo pagina, CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> resultadoCorral = corralDAL.ObtenerPorDependencia(pagina, corralInfo);
                return resultadoCorral;
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
        /// Metodo para Guardar/Modificar un Corral
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(CorralInfo info)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                int corralID = info.CorralID;
                if (info.CorralID == 0)
                {
                    corralID = corralDAL.Crear(info);
                }
                else
                {
                    corralDAL.Actualizar(info);
                }
                return corralID;
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
        /// Metodo que Indica si el Corral Seleccionado
        /// Es Usando en Ruteo
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="corralID"></param>
        /// <returns>
        /// true - En caso de que Si ha sido Seleccionado para Ruteo
        /// flase - En caso de que No ha sido Seleccionado para Ruteo
        /// </returns>
        internal bool CorralSeleccionadoParaRuteo(int embarqueID, int corralID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                bool corralEstaEnRuteo = corralDAL.CorralSeleccionadoParaRuteo(embarqueID, corralID);
                return corralEstaEnRuteo;
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

        internal CorralInfo ObtenerPorDependencia(CorralInfo corralInfo
                                                )
        {

            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral = corralDAL.ObtenerPorDependencia(corralInfo);
                return resultadoCorral;
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

        internal CorralInfo ObtenerPorId(int corralID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral = corralDAL.ObtenerPorId(corralID);
                return resultadoCorral;
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
        /// Metodo que valida si existe el codigo del corral y no tiene asignado un lote ni servio de alimento
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="codigoCorral"></param>
        internal CorralInfo ObtenerValidacionCorral(int organizacionID, string codigoCorral)
        {
            CorralInfo corral;
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();

                corral = corralDAL.ObtenerValidacionCorral(organizacionID, codigoCorral);
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

            return corral;
        }

        /// <summary>
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodicoCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral = corralDAL.ObtenerPorCodicoCorral(corralInfo);
                return resultadoCorral;
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
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodicoCorraleta(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral = corralDAL.ObtenerPorCodicoCorraleta(corralInfo);
                return resultadoCorral;
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
        /// Obtiene el corral que ya fue asignado a un embarque
        /// </summary>
        /// <param name="folioEmbarque"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorEmbarqueRuteo(int folioEmbarque, int organizacionID)
        {
            CorralInfo corralInfo;
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                corralInfo = corralDAL.ObtenerPorEmbarqueRuteo(folioEmbarque, organizacionID);
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
            return corralInfo;
        }

        /// <summary>
        /// Obtiene una entidad Corral por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerPorDescripcion(descripcion);
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

        internal CorralInfo ObtenerPorCodigoGrupo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerPorCodigoGrupo(corral);
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
        /// Obtiene el corral por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesPorTipo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> resultadoCorral = corralDAL.ObtenerCorralesPorTipo(corral);
                return resultadoCorral;
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
        /// Obtiene el corral por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesPorTipoCorralDetector(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> resultadoCorral = corralDAL.ObtenerCorralesPorTipoCorralDetector(corral);
                return resultadoCorral;
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
        /// Obtiene el corral por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <param name="listaTiposCorral"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesPorTipo(CorralInfo corral, List<int> listaTiposCorral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> resultadoCorral = corralDAL.ObtenerCorralesPorTipo(corral, listaTiposCorral);
                return resultadoCorral;
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
        /// Obtiene el corral por codigo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerCorralPorCodigo(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
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
        /// Obtiene la partida del corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPartidaCorral(int organizacionId, int corralID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                EntradaGanadoInfo entrada = corralDAL.ObtenerPartidaCorral(organizacionId, corralID);
                return entrada;
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
        /// Metodo para validar si el corral pertenece a la enfermeria
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="enfermeria"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal bool ValidarCorralPorEnfermeria(string codigoCorral, int enfermeria, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ValidarCodigoCorralPorEnfermeria(codigoCorral, enfermeria, organizacionId);
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
        /// Actualiza las cabezas del corral
        /// </summary>
        /// <param name="animalMovimiento"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal string ActualizarCorralesCabezas(AnimalMovimientoInfo animalMovimiento, int loteOrigen)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ActualizarCorralesCabezas(animalMovimiento, loteOrigen);
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
        /// Obtiene el corral por codigo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerExistenciaCorral(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerExistenciaCorral(organizacionId, corralCodigo);
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
        /// Genera el Reporte Proyector y Comportamiento de Ganado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<ReporteProyectorInfo> ObtenerReporteProyectorComportamiento(int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerReporteProyectorComportamiento(organizacionID);
                //var listaCorrales = corralDAL.ObtenerReporteProyectorComportamiento(organizacionID);
                //if(listaCorrales == null)
                //{
                //    return null;
                //}
                //var reporte = (from corral in listaCorrales
                //               let pesoOrigen = ObtenerPesoOrigen(corral.ListaAnimales)
                //               let diasEngorda = ObtenerDiasEngorda(corral.ListaAnimales)
                //               let primerReimplante = ObtenerReimplante(1, corral.LoteProyeccion.ListaReimplantes, pesoOrigen, corral.FechaInicioLote)
                //               let segundoReimplante = ObtenerReimplante(2, corral.LoteProyeccion.ListaReimplantes, pesoOrigen, corral.FechaInicioLote)
                //               let tercerReimplante = ObtenerReimplante(3, corral.LoteProyeccion.ListaReimplantes, pesoOrigen, corral.FechaInicioLote)
                //               select new ReporteProyectorInfo
                //                   {
                //                       Corral = corral.CodigoCorral,
                //                       Lote = corral.CodigoLote,
                //                       Cabezas = corral.Cabezas,
                //                       TipoGanado = corral.TipoGanado,
                //                       Clasificacion = ObtenerClasificacionMayor(corral.ListaAnimales),
                //                       PesoOrigen = pesoOrigen,
                //                       Merma = ObtenerPorcentajeMerma(corral.ListaAnimales),
                //                       PesoProyectado = ObtenerPesoProyectado(corral.LoteProyeccion),
                //                       GananciaDiaria = corral.LoteProyeccion.GananciaDiaria,
                //                       DiasEngorda =  diasEngorda,
                //                       Fecha1Reimplante = (primerReimplante != null && primerReimplante.FechaReal != DateTime.MinValue) ? string.Format("{0}", primerReimplante.FechaReal.ToString("dd/MM/yyyy")) : string.Empty,
                //                       Peso1Reimplante = primerReimplante != null ? primerReimplante.PesoReal : 0,
                //                       Ganancia1Diaria = primerReimplante != null ? primerReimplante.GananciaDiaria : 0,
                //                       Fecha2Reimplante = (segundoReimplante != null && segundoReimplante.FechaReal != DateTime.MinValue) ? string.Format("{0}", segundoReimplante.FechaReal.ToString("dd/MM/yyyy")) : string.Empty,
                //                       Peso2Reimplante = segundoReimplante != null ? segundoReimplante.PesoReal : 0,
                //                       Ganancia2Diaria = segundoReimplante != null ? segundoReimplante.GananciaDiaria : 0,
                //                       Fecha3Reimplante = (tercerReimplante != null && tercerReimplante.FechaReal != DateTime.MinValue) ? string.Format("{0}", tercerReimplante.FechaReal.ToString("dd/MM/yyyy")) : string.Empty,
                //                       Peso3Reimplante = tercerReimplante != null ? tercerReimplante.PesoReal : 0,
                //                       Ganancia3Diaria = tercerReimplante != null ? tercerReimplante.GananciaDiaria : 0,
                //                       DiasF4 = corral.DiasF4,
                //                       DiasZilmax = corral.DiasZilmax,
                //                       FechaSacrificio = ObtenerFechaSacrificio(corral)
                //                   }).ToList();
                //return reporte;
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
        /// Obtiene la Fecha de Sacrificio del Corral
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        private static string ObtenerFechaSacrificio(ContenedorReporteProyectorInfo corral)
        {
            if(corral.DisponibilidadManual)
            {
                return string.Format("{0}", corral.FechaDisponibilidad.AddDays(corral.DiasSacrificio).ToString("dd/MM/yyyy"));
            }
            return string.Format("{0}", corral.FechaInicioLote.AddDays(corral.LoteProyeccion.DiasEngorda).ToString("dd/MM/yyyy"));
        }

        /// <summary>
        /// Obtiene el reimplante del Lote
        /// </summary>
        /// <param name="numeroReimplante"></param>
        /// /// <param name="listaReimplantes"></param>
        /// /// <param name="pesoOrigen"></param>
        /// /// <param name="fechaInicio"></param>
        /// <returns></returns>
        private static LoteReimplanteInfo ObtenerReimplante(int numeroReimplante, IEnumerable<LoteReimplanteInfo> listaReimplantes, int pesoOrigen, DateTime fechaInicio)
        {
            var reimplante = listaReimplantes.FirstOrDefault(reim => reim.NumeroReimplante == numeroReimplante);
            if (reimplante == null)
            {
                return null;
            }
            var dias = reimplante.FechaReal - fechaInicio;
            var diasTranscurridos = (int)dias.TotalDays;
            if(diasTranscurridos != 0)
            reimplante.GananciaDiaria = Math.Round((decimal)(reimplante.PesoReal - pesoOrigen) / diasTranscurridos, 2);
            else
            {
                reimplante.GananciaDiaria = Math.Round((decimal)(reimplante.PesoReal - pesoOrigen), 2);
            }
            return reimplante;
        }

        /// <summary>
        /// Obtiene el Porcentaje de la merma de los Animales por Corral
        /// </summary>
        /// <param name="listaAnimales"></param>
        /// <returns></returns>
        private static decimal ObtenerPorcentajeMerma(IEnumerable<AnimalInfo> listaAnimales)
        {
            int totalCabezas = 0;
            decimal merma = 0;
            foreach (var animal in listaAnimales)
            {
                if (animal.PesoCompra == 0)
                {
                    continue;
                }
                var mermaAnimal = ((decimal)(animal.PesoCompra - animal.PesoLlegada) / animal.PesoCompra) * 100;
                merma = merma + mermaAnimal;
                totalCabezas = totalCabezas + 1;
            }
            return Math.Round(merma / totalCabezas, 2);
        }

        /// <summary>
        /// Obtiene el Peso Origen de los Animales por Corral
        /// </summary>
        /// <param name="listaAnimales"></param>
        /// <returns></returns>
        private static int ObtenerPesoOrigen(IEnumerable<AnimalInfo> listaAnimales)
        {
            var animalesAgrupados = (from animal in listaAnimales
                                     group animal by animal.CorralID
                                         into agru
                                         select new
                                             {
                                                 TotalPesoOrigen = agru.Sum(ani => ani.PesoCompra),
                                                 TotalCabezas = agru.Count()
                                             }).FirstOrDefault();

            if (animalesAgrupados == null)
            {
                return 0;
            }
            var pesoOrigen = (animalesAgrupados.TotalPesoOrigen / animalesAgrupados.TotalCabezas);
            return pesoOrigen;
        }

        private int ObtenerDiasEngorda(IEnumerable<AnimalInfo> listaAnimales)
        {
            int retValue = 0;
            var animalesAgrupados = (from animal in listaAnimales
                                     group animal by animal.CorralID
                                         into agru
                                         select new
                                         {
                                             Dias = (int)agru.Average(ani => ani.DiasEngorda)
                                         }).FirstOrDefault();

            if (animalesAgrupados != null)
            {


                retValue = animalesAgrupados.Dias;

            }
            

            return retValue;
        }

        /// <summary>
        /// Obtiene la descripción de la Clasificación del Ganado
        /// </summary>
        /// <param name="listaAnimales"></param>
        /// <returns></returns>
        private static string ObtenerClasificacionMayor(ICollection<AnimalInfo> listaAnimales)
        {
            var agrupadorPorClasificacion = (from animal in listaAnimales
                                             group animal by animal.ClasificacionGanado.ClasificacionGanadoID
                                                 into clasificacionesAgrupadas
                                                 let animalInfo = clasificacionesAgrupadas.FirstOrDefault()
                                                 where animalInfo != null
                                                 select new
                                                 {
                                                     animalInfo.ClasificacionGanado,
                                                     TotalClasificacion = clasificacionesAgrupadas.Count(),
                                                     Porcentaje = ((decimal)clasificacionesAgrupadas.Count() / (decimal)listaAnimales.Count) * 100
                                                 }).ToList();

            var porcentajeMayorClasificacionGanado = agrupadorPorClasificacion.Max(agrupado => agrupado.Porcentaje);

            var clasificacionGanadoMayor = (from clasificacion in agrupadorPorClasificacion
                                            where clasificacion.Porcentaje == porcentajeMayorClasificacionGanado
                                            select clasificacion);

            var clasificacionGanadoFinal =
                clasificacionGanadoMayor.OrderByDescending(clasificacion => clasificacion.ClasificacionGanado.ClasificacionGanadoID).FirstOrDefault();

            if (clasificacionGanadoFinal == null)
            {
                return null;
            }
            string checkListTipo = clasificacionGanadoFinal.ClasificacionGanado.Descripcion;
            return checkListTipo;
        }

        /// <summary>
        /// Obtiene el Peso Proyectado del Lote
        /// </summary>
        /// <param name="loteProyeccion"></param>
        /// <returns></returns>
        private static int ObtenerPesoProyectado(LoteProyeccionInfo loteProyeccion)
        {
            var ultimoReimplante =
                loteProyeccion.ListaReimplantes.OrderBy(reim => reim.NumeroReimplante).LastOrDefault();
            if (ultimoReimplante == null)
            {
                return 0;
            }
            if (ultimoReimplante.FechaReal == DateTime.MinValue && ultimoReimplante.PesoReal == 0)
            {
                var ultimoReimplanteReal = loteProyeccion.ListaReimplantes.OrderBy(reim => reim.NumeroReimplante).LastOrDefault(reim=> reim.PesoReal != 0);
                if (ultimoReimplanteReal == null)
                {
                    var primerReimpante = loteProyeccion.ListaReimplantes.OrderBy(reim => reim.NumeroReimplante).FirstOrDefault();
                    if (primerReimpante == null)
                    {
                        return 0;
                    }
                    return primerReimpante.PesoProyectado;
                }

                var pesoProyectadoReal = (int)((int)(DateTime.Now - ultimoReimplanteReal.FechaReal).TotalDays * loteProyeccion.GananciaDiaria) +
                                ultimoReimplanteReal.PesoReal;
                return pesoProyectadoReal;
            }
            var pesoProyectado = (int)((int)(DateTime.Now - ultimoReimplante.FechaReal).TotalDays * loteProyeccion.GananciaDiaria) +
                                 ultimoReimplante.PesoReal;
            return pesoProyectado;
        }
        /// <summary>
        /// Cuante el numero de cabezas en el corral
        /// </summary>
        /// <param name="corral">Corral al cual se le contara las cabezas</param>
        /// <returns>Total de cabezas</returns>
        internal int ContarCabezas(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ContarCabezas(corral);
               
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
        /// Obtiene los aretes de una corraleta
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAretesCorraleta(CorralInfo corralInfo, LoteInfo loteOrigen)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerAretesCorraleta(corralInfo, loteOrigen);

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
        /// Traspasa los animales
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="loteOrigen"></param>
        internal void TraspasarAnimalSalidaEnfermeria(int corralInfo, int loteOrigen)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                corralDAL.TraspasarAnimalSalidaEnfermeria(corralInfo, loteOrigen);

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
        /// Obtiene el Corral por Organizacion y Codigo
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodicoOrganizacionCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerPorCodicoOrganizacionCorral(corralInfo);
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
        /// Valida el corral de enfermeria
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ValidarCorralEnfermeria(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ValidarCorralEnfermeria(corralInfo);
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
        /// Obtiene el corral por codigo y grupo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodigoGrupoCorral(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerPorCodigoGrupoCorral(corral);
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
        /// Verifica si existe interfaz salida
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralCodigo"></param>
        /// <param name="arete"></param>
        /// <returns></returns>
        internal int ObtenerExisteInterfaceSalida(int organizacionID,string corralCodigo, string arete)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerExisteInterfaceSalida(organizacionID, corralCodigo, arete);
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

        internal CorralInfo ObtenerCorralPorLoteID(int loteID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerCorralPorLoteID(loteID, organizacionID);
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
        /// Obtiene los corrales para reparto
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesParaReparto(int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralDal = new CorralDAL();
                var result = corralDal.ObtenerCorralesParaReparto(organizacionId);
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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral al que pertenecen
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<CorralInfo> ObtenerCorralesPorGrupo(GrupoCorralInfo grupo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerPorCorralesPorGrupo(grupo, organizacionId);
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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral Enfermeria y que tengan programacion en servicio de alimentos.
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<CorralInfo> ObtenerPorCorralesPorGrupoConProgramacionDeAlimentos(GrupoCorralInfo grupo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerPorCorralesPorGrupoConProgramacionDeAlimentos(grupo, organizacionId);
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
        /// Obtiene un corral por su grupo corral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorGrupoCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral = corralDAL.ObtenerPorGrupoCorral(corralInfo);
                return resultadoCorral;
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
        /// Obtiene una lista de corrales paginada
        /// por su grupo corral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerPorPaginaGrupoCorral(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                result = corralDAL.ObtenerPorPaginaGrupoCorral(pagina, filtro);
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
        /// Obtiene un corral por codigo que este activo y tenga lote activo.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerCorralActivoPorCodigo(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerCorralActivoPorCodigo(organizacionId, corralCodigo);
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
        /// Obtiene una entidad Corral por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorDescripcionOrganizacion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerPorDescripcionOrganizacion(descripcion, organizacionID);
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
        /// Obtiene todos los corrales que pertencen al tipo de corral y organizacion especificados, 
        /// ademas deberan contener un lote activo y no tener enfermerias asignadas.
        /// </summary>
        /// <param name="tipoCorral">Tipo de corral al que pertenece el corral.</param>
        /// <param name="organizacionId">OrganizacionId al que pertenece el corral.</param>
        /// <returns>Una lista de corrales</returns>
        internal List<CorralInfo> ObtenerCorralesConLoteActivoPorTipoCorralSinEnfermeriaAsignada(TipoCorralInfo tipoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new SIE.Services.Integracion.DAL.ORM.CorralDAL();
                List<CorralInfo> result = corralDAL.ObtenerCorralesConLoteActivoPorTipoCorralSinEnfermeriaAsignada(tipoCorral, organizacionId);
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
        /// Obtiene todos los corrales que pertencen al tipo de corral y organizacion especificados y no tener enfermerias asignadas.
        /// </summary>
        /// <param name="tipoCorral">Tipo de corral al que pertenece el corral.</param>
        /// <param name="organizacionId">OrganizacionId al que pertenece el corral.</param>
        /// <returns>Una lista de corrales</returns>
        internal List<CorralInfo> ObtenerCorralesPorTipoCorralSinEnfermeriaAsignada(TipoCorralInfo tipoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new SIE.Services.Integracion.DAL.ORM.CorralDAL();
                List<CorralInfo> result = corralDAL.ObtenerCorralesPorTipoCorralSinEnfermeriaAsignada(tipoCorral, organizacionId);
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

        internal List<CorralInfo> ObtenerCorralesPorId(int[] corralesId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new SIE.Services.Integracion.DAL.ORM.CorralDAL();
                List<CorralInfo> result = corralDAL.ObtenerCorralesPorId(corralesId);
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

        internal List<CorralInfo> ObtenerCorralesPorEnfermeriaId(int enfermeriaId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new SIE.Services.Integracion.DAL.ORM.CorralDAL();
                List<CorralInfo> result = corralDAL.ObtenerCorralesPorEnfermeriaId(enfermeriaId);
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

        public ResultadoInfo<CorralInfo> ObtenerPorPaginaGruposCorrales(PaginacionInfo pagina, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                result = corralDAL.ObtenerPorPaginaGruposCorrales(pagina, filtro);
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

        internal ResultadoInfo<CorralInfo> ObtenerParaProgramacionReimplanteDestino(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> result = corralDAL.ObtenerParaProgramacionReimplanteDestino (pagina, filtro);
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

        internal CorralInfo ObtenerPorCodigoParaProgramacionReimplanteDestino(CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ObtenerPorCodigoParaProgramacionReimplanteDestino(filtro);
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
        /// Obtiene una lista de corrales por xml
        /// </summary>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal IList<CorralInfo> ObtenerPorCorralIdXML(List<CorralInfo> corrales)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                IList<CorralInfo> result = corralDAL.ObtenerPorCorralIdXML(corrales);
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
        /// Valida que el corral tenga existencia
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal CorralInfo ValidaCorralConLoteConExistenciaActivo(int corralID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo result = corralDAL.ValidaCorralConLoteConExistenciaActivo(corralID);
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
        /// Obtiene los dias de engorda de un corral
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal int ObtenerDiasEngordaPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerDiasEngordaPorLote(lote);
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
        /// Obtiene los Corrales por sus codigos
        /// </summary>
        /// <param name="codigosCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<CorralInfo> ObtenerCorralesPorCodigosCorral(List<string> codigosCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerCorralesPorCodigosCorral(codigosCorral, organizacionID);
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
        /// Obtiene las Secciones de los corrales
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<SeccionModel> ObtenerSeccionesCorral(int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerSeccionesCorral(organizacionID);
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
        /// Obtiene los Corrales por el tipo de corral
        /// </summary>
        /// <param name="tipoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<CorralInfo> ObtenerCorralesPorTipoCorral(TipoCorralInfo tipoCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerCorralesPorTipoCorral(tipoCorral, organizacionID);
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

        internal ResultadoInfo<CorralInfo> ObtenerFormulaPorDependencia(PaginacionInfo pagina, CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> resultadoCorral = corralDAL.ObtenerFormulaPorDependencia(pagina, corralInfo);
                return resultadoCorral;
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

        internal List<CorralInfo> ObtenerCorralesImproductivos(int tipoCorralID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                List<CorralInfo> resultadoCorral = corralDAL.ObtenerCorralesImproductivos(tipoCorralID);
                return resultadoCorral;
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

        public CorralInfo ObtenerFormulaCorralPorID(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral = corralDAL.ObtenerFormulaCorralPorID(corral);
                return resultadoCorral;
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
        /// Obtener lista de corralestas configuradas para sacrificio
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="codigoCorraletas"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerInformacionCorraletasDisponiblesSacrificio(int organizacionId, string codigoCorraletas)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                ResultadoInfo<CorralInfo> resultadoCorral =
                    corralDAL.ObtenerInformacionCorraletasDisponiblesSacrificio(organizacionId, codigoCorraletas);
                return resultadoCorral;
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
        /// Obtiene los dias de engorda de un corral
        /// </summary>
        /// <param name="lotesXml"></param>
        /// <returns></returns>
        internal IList<DiasEngordaLoteModel> ObtenerDiasEngordaPorLoteXML(IList<LoteInfo> lotesXml)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                return corralDAL.ObtenerDiasEngordaPorLoteXML(lotesXml);
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
        /// Obtener corral por codigo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        public CorralInfo ObtenerCorralesPorTipos(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                CorralInfo resultadoCorral =
                    corralDAL.ObtenerCorralesPorTipos(corral);
                if (resultadoCorral != null)
                {
                    if (TipoCorral.Intensivo.GetHashCode() == resultadoCorral.TipoCorralId ||
                    TipoCorral.Maquila.GetHashCode() == resultadoCorral.TipoCorralId)
                    {
                        return resultadoCorral;
                    }
                }
                return null;
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
        /// Obtener corrales por pagina
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerCorralesPorPagina(PaginacionInfo paginacion, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> result;
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                result = corralDAL.ObtenerCorralesPorPagina(paginacion, filtro);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
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
        /// Consulta todos los corrales validos para registrar una salida de ganado en transito por venta o muerte
        /// </summary>
        /// <param name="OrganizacionId">Organizacion en en el cual se busca el corral</param>
        /// <returns>Regresa la lista corrales configurados para salida de ganado en transito por venta o muerte</returns>
        public static IList<CorralesPorOrganizacionInfo> ObtenerCorralesPorOrganizacionID(int OrganizacionId)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                IList<CorralesPorOrganizacionInfo> result = corralDAL.ObtenerCorralesPorOrganizacionID(OrganizacionId);
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
        /// Consulta todos los costos de una Entrada de Ganado Transito por id
        /// </summary>
        /// <param name="entradaGanadoTransitoID">Entrada de ganado en transito al cual se le buscaran los costos</param>
        /// <returns>Regresa la lista de costos de la entrada de ganado en transito</returns>
        public static IList<CostoCorralInfo> ObtenerCostosCorralActivos(int entradaGanadoTransitoID)
        {
            try
            {
                Logger.Info();
                var corralDAL = new CorralDAL();
                IList<CostoCorralInfo> result =
                    corralDAL.ObtenerCostosCorralPorEntradaGanadoTransito(entradaGanadoTransitoID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        } 
    }
}