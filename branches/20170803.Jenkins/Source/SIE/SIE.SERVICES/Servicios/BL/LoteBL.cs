using System;
using System.Collections.Generic;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Linq;
using SIE.Services.Servicios.BL.Auxiliar;
using SIE.Base.Infos;
using System.Text.RegularExpressions;

namespace SIE.Services.Servicios.BL
{
    public class LoteBL
    {
        /// <summary>
        /// Obtiene una Lista con Todos los Corrales
        /// </summary>
        /// <returns></returns>
        public IList<LoteInfo> ObtenerTodos()
        {
            IList<LoteInfo> lista;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lista = loteDAL.ObtenerTodos();
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
        /// Obtiene una Lista con Todos los Corrales
        /// </summary>
        /// <returns></returns>
        public IList<LoteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                IList<LoteInfo> lista = loteDAL.ObtenerTodos(estatus);

                return lista;
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
        /// Obtiene un Objeto de Tipo LoteInfo
        /// </summary>
        /// <param name="organizacionID">Clave de la Organizacion a la cual pertenece el Corral</param>
        /// <param name="corralID">Clave del Corral</param>        
        /// <returns></returns>
        public int ObtenerActivosPorCorral(int organizacionID, int corralID)
        {
            int totalActivos;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                totalActivos = loteDAL.ObtenerActivosPorCorral(organizacionID, corralID);
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
            return totalActivos;
        }
        /// <summary>
        /// Obtiene un Objeto de Tipo LoteInfo
        /// </summary>
        /// <param name="organizacionID">Clave de la Organizacion a la cual pertenece el Corral</param>
        /// <param name="corralID">Clave del Corral</param>        
        /// <returns></returns>
        public LoteInfo ObtenerLotesActivos(int organizacionID, int corralID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerLotesActivos(organizacionID, corralID);
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
        /// Obtiene Lote por Id
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        public LoteInfo ObtenerPorID(int loteID)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lote = loteDAL.ObtenerPorID(loteID);
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
            return lote;
        }

        /// <summary>
        /// Obtiene un Lote por Id y Su Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        public LoteInfo ObtenerPorIdOrganizacionId(int organizacionID, int corralID, int embarqueID)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lote = loteDAL.ObtenerPorIdOrganizacionId(organizacionID, corralID, embarqueID);
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
            return lote;
        }

        /// <summary>
        /// Genera un Nuevo Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        public int GuardaLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                int result = loteDAL.GuardaLote(loteInfo);

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
        /// Actualiza el Numero de Cabezas del Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        public void AcutalizaCabezasLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.AcutalizaCabezasLote(loteInfo);
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
        /// Obtiene un Lote por Organizacion y Lote
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="lote"></param>
        /// <returns></returns>
        public LoteInfo ObtenerPorOrganizacionIdLote(int organizacionID, string lote)
        {
            LoteInfo loteInfo;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteInfo = loteDAL.ObtenerPorOrganizacionIdLote(organizacionID, lote);
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
            return loteInfo;
        }
        /// <summary>
        /// Obtiene un Lote por Organizacion y Lote
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="idCorral"></param>
        /// <returns></returns>
        public LoteInfo ObtenerPorCorralCerrado(int organizacionID, int idCorral)
        {
            LoteInfo loteInfo;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteInfo = loteDAL.ObtenerPorCorralCerrado(organizacionID, idCorral);
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
            return loteInfo;
        }

        /// <summary>
        /// Actualiza la Fecha de Cierre
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="usuarioModificacionID"> </param>
        public void ActualizaFechaCierre(int loteID, int? usuarioModificacionID)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizaFechaCierre(loteID, usuarioModificacionID);
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
        /// Obtiene un Lote por Organizacion y CorralID
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        public LoteInfo ObtenerPorCorral(int organizacionID, int corralID)
        {
            LoteInfo loteInfo;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteInfo = loteDAL.ObtenerPorCorral(organizacionID, corralID);
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
            return loteInfo;
        }


        /// <summary>
        /// Obtiene un Lote por Organizacion y CorralID
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        public LoteInfo DeteccionObtenerPorCorral(int organizacionID, int corralID)
        {
            LoteInfo loteInfo;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteInfo = loteDAL.DeteccionObtenerPorCorral(organizacionID, corralID);
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
            return loteInfo;
        }


        /// <summary>
        /// Actualiza el Numero de Cabezas del Lote
        /// </summary>
        /// <param name="loteInfoDestino"></param>
        /// <param name="loteInfoOrigen"></param>
        public void ActualizaNoCabezasEnLote(LoteInfo loteInfoDestino, LoteInfo loteInfoOrigen)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizaNoCabezasEnLote(loteInfoDestino, loteInfoOrigen);
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
        /// Actualiza el Numero de Cabezas del Lote de productivo
        /// </summary>
        /// <param name="loteInfoDestino"></param>
        /// <param name="loteInfoOrigen"></param>
        public void ActualizaCabezasEnLoteProductivo(LoteInfo loteInfoDestino, LoteInfo loteInfoOrigen)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizaCabezasEnLoteProductivo(loteInfoDestino, loteInfoOrigen);
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
        /// Actualiza el campo Lote de la tabla lote
        /// </summary>
        /// <param name="loteInfo"></param>
        public void ActualizarLoteALote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizarLoteALote(loteInfo);
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
        /// Actualiza el campo activo de la tabla lote
        /// </summary>
        /// <param name="lote"></param>
        public void ActualizaActivoEnLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizaActivoEnLote(lote);
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
        /// Actualizar el numero de cabezas en lote y cambiar la fecha salida
        /// </summary>
        /// <param name="lote"></param>
        public void ActualizarFechaSalidaEnLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizarFechaSalidaEnLote(lote);
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
        /// Obtiene la información del Lote para Check List
        /// </summary>
        /// <returns></returns>
        public List<CheckListCorralInfo> ObtenerCheckListCorral(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                List<CheckListCorralInfo> checkList = loteDAL.ObtenerCheckListCorral(filtroCierreCorral);
                if (checkList == null)
                {
                    return null;
                }
                var lotesCompletos = new List<int>();
                var animalBL = new AnimalBL();
                var fechaBL = new FechaBL();
                FechaInfo fechaActual = fechaBL.ObtenerFechaActual();
                checkList.ForEach(chk =>
                    {


                        if (chk.FechaCerrado != DateTime.MinValue)
                        {
                            chk.Estatus = EstatusLote.Cerrado.GetHashCode();
                            chk.AplicaCerrado = false;
                            if (chk.FechaCerrado.Date != DateTime.Now.Date)
                            {
                                chk.CorralInvalidoFecha = true;
                            }
                        }
                        else
                        {
                            chk.Estatus = EstatusLote.Abierto.GetHashCode();
                            chk.AplicaCerrado = true;
                            if (chk.CabezasRestantes == 0)
                            {
                                lotesCompletos.Add(chk.LoteID);
                            }
                        }
                    });
                List<CheckListCorralInfo> checkListFiltradosFecha;

                if (lotesCompletos.Count == 0)
                {
                    checkListFiltradosFecha = checkList.Where(chk => !chk.CorralInvalidoFecha).ToList();

                    var lotes = (from lo in checkListFiltradosFecha
                                 select new LoteInfo
                                 {
                                     LoteID = lo.LoteID
                                 }).ToList();

                    List<AnimalInfo> animalesDisponibles = animalBL.ObtenerAnimalesPorLoteXML(lotes,
                                                                                                     filtroCierreCorral.OrganizacionID);

                    checkListFiltradosFecha.ForEach(chk =>
                    {
                        //var animalesLote = animalBL.ObtenerAnimalesPorLote(filtroCierreCorral.OrganizacionID,
                        //                                                 chk.LoteID);

                        //var animalesDetalle = (from det in animalesDisponibles.SelectMany(ani => ani.ListaAnimalesMovimiento)
                        //                        where det.LoteID == chk.LoteID
                        //                        select det).ToList();

                        //if (!animalesDetalle.Any())
                        //{
                        //    return;
                        //}

                        var animalesLote = (from animal in animalesDisponibles
                                            where animal.LoteID == chk.LoteID
                                            select animal).ToList();

                        if (animalesLote == null)
                        {
                            return;
                        }

                        var count = animalesLote.Count;
                        double temp = 0D;
                        for (int i = 0; i < count; i++)
                        {
                            temp += animalesLote[i].FechaEntrada.Ticks / (double)count;
                        }
                        var average = new DateTime((long)temp);

                        TimeSpan diferenciaDias = fechaActual.FechaActual - average;

                        if (diferenciaDias.TotalDays > 21)
                        {
                            chk.CorralInvalidoFecha = true;
                        }
                    });
                    return checkListFiltradosFecha.Where(chk => !chk.CorralInvalidoFecha).ToList();
                }
                //var animalBL = new AnimalBL();
                var fechas = animalBL.ObtenerFechasUltimoMovimiento(filtroCierreCorral.OrganizacionID, lotesCompletos);
                if (fechas == null)
                {
                    return checkList;
                }
                foreach (var fechaMovimiento in fechas)
                {
                    var checkCompleto = checkList.FirstOrDefault(chk => chk.LoteID == fechaMovimiento.LoteID);
                    if (checkCompleto == null)
                    {
                        continue;
                    }
                    checkCompleto.FechaFin = fechaMovimiento.FechaMovimiento;
                }



                //Filtrar los lotes que se hayan cerrado en una fecha que no sea la actual
                checkListFiltradosFecha = checkList.Where(chk => !chk.CorralInvalidoFecha).ToList();

                var lotesIncompletos = (from lo in checkListFiltradosFecha
                                        select new LoteInfo
                                        {
                                            LoteID = lo.LoteID
                                        }).ToList();

                List<AnimalInfo> animalesDisponiblesIncompleto = animalBL.ObtenerAnimalesPorLoteXML(lotesIncompletos,
                                                                                                 filtroCierreCorral.OrganizacionID);

                checkListFiltradosFecha.ForEach(chk =>
                {
                    //var animalesLote = animalBL.ObtenerAnimalesPorLote(filtroCierreCorral.OrganizacionID,
                    //                                                 chk.LoteID);

                    //var animalesDetalle = (from det in animalesDisponiblesIncompleto.SelectMany(ani => ani.ListaAnimalesMovimiento)
                    //                       where det.LoteID == chk.LoteID
                    //                       select det).ToList();

                    //if (!animalesDetalle.Any())
                    //{
                    //    return;
                    //}

                    var animalesLote = (from animal in animalesDisponiblesIncompleto
                                        where animal.LoteID == chk.LoteID
                                        select animal).ToList();

                    if (animalesLote == null)
                    {
                        return;
                    }

                    var count = animalesLote.Count;
                    double temp = 0D;
                    for (int i = 0; i < count; i++)
                    {
                        temp += animalesLote[i].FechaEntrada.Ticks / (double)count;
                    }
                    var average = new DateTime((long)temp);

                    TimeSpan diferenciaDias = fechaActual.FechaActual - average;

                    if (diferenciaDias.TotalDays > 21)
                    {
                        chk.CorralInvalidoFecha = true;
                    }
                });


                return checkListFiltradosFecha.Where(chk => !chk.CorralInvalidoFecha).ToList();
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
        /// Obtiene la información completa del Lote para Check List
        /// </summary>
        /// <returns></returns>
        public CheckListCorralInfo ObtenerCheckListCorralCompleto(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                CheckListCorralInfo checkList = loteDAL.ObtenerCheckListCorralCompleto(filtroCierreCorral);
                if (checkList == null)
                {
                    return null;
                }
                var porcentaje = (decimal)checkList.CabezasSistema / checkList.CapacidadCabezas;
                checkList.Ocupacion = Math.Round(porcentaje * 100, 2);

                var animalBL = new AnimalBL();
                var animalesLote = animalBL.ObtenerAnimalesPorLote(filtroCierreCorral.OrganizacionID,
                                                                 filtroCierreCorral.LoteID);

                if (animalesLote == null)
                {
                    return null;
                }

                var count = animalesLote.Count;
                double temp = 0D;
                for (int i = 0; i < count; i++)
                {
                    temp += animalesLote[i].FechaEntrada.Ticks / (double)count;
                }
                var average = new DateTime((long)temp);

                checkList.FechaAbierto = average;

                List<AnimalInfo> animalSinPesos =
                    animalesLote.Where(ani => ani.PesoCompra == 0 || ani.PesoLlegada == 0).ToList();
                if (animalSinPesos.Any())
                {
                    int totalPesoCompra1 = Convert.ToInt32(animalesLote.Average(animal => animal.PesoCompra));
                    int totalPesoLlegada1 = Convert.ToInt32(animalesLote.Average(animal => animal.PesoLlegada));
                    animalSinPesos.ForEach(ani =>
                    {
                        ani.PesoCompra = totalPesoCompra1;
                        ani.PesoLlegada = totalPesoLlegada1;
                    });
                    AnimalInfo animalInfo = animalSinPesos.FirstOrDefault();
                    /*Si el corral tiene por lo menos algun animal sin peso de compra o peso de llegada, marcara el error de
                     * que la partida no ha sido cerrada
                     * */
                    checkList.Corral = "PARTIDA";
                    checkList.LoteID = animalInfo.FolioEntrada;
                    return checkList;

                }

                var totalPesoCompra = animalesLote.Sum(animal => animal.PesoCompra);
                var totalPesoLlegada = animalesLote.Sum(animal => animal.PesoLlegada);
                var totalAnimales = animalesLote.Count();

                if(totalAnimales == 0)
                {
                    /*Si por alguna razon el conteo de Animales es 0 mandar el mensaje de error
                    */
                    checkList.Corral = "CONTEOCERO";
                    return checkList;
                }

                if (totalAnimales == 0)
                {
                    /*Si por alguna razon el conteo de Animales es 0 mandar el mensaje de error
                    */
                    checkList.Corral = "PESOCOMPRA";
                    return checkList;
                }

                string checkListTipo;
                var tipoGanadoFinal = ObtenerTipoCheckListLoteCorralCompleto(animalesLote, out checkListTipo);
                checkList.Tipo = checkListTipo;

                checkList.SexoAnimal = (char)tipoGanadoFinal.TipoGanado.Sexo;
                checkList.PesoPromedioCompra = Math.Round((decimal)totalPesoCompra / totalAnimales, 2);
                checkList.PesoPromedioLlegada = Math.Round((decimal)totalPesoLlegada / totalAnimales, 2);
                checkList.PorcentajeMerma = Math.Round((((decimal)totalPesoCompra - totalPesoLlegada) / totalPesoCompra) * 100, 2);

                return checkList;
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
        /// Actualiza el Numero de Cabezas del Lote
        /// </summary>
        /// <param name="filtroCierreCorral"></param>
        public void ActualizaFechaCerrado(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizarFechaCerrado(filtroCierreCorral);
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
        /// Obtiene los Lotes por su Disponibilidad
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        public List<DisponibilidadLoteInfo> ObtenerLotesPorDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            List<DisponibilidadLoteInfo> disponibilidadLote;
            List<DisponibilidadLoteInfo> lotesFiltrados;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                var fechaBL = new FechaBL();

                FechaInfo fechaActual = fechaBL.ObtenerFechaActual();

                disponibilidadLote = loteDAL.ObtenerLotesPorDisponibilidad(filtroDisponilidadInfo);

                if (disponibilidadLote == null)
                {
                    return null;
                }

                var lotesReimplante = new List<DisponibilidadLoteInfo>(); //loteDAL.ObtenerLotesActivosConProyeccion(filtroDisponilidadInfo.OrganizacionId);

                var animalBL = new AnimalBL();
                List<AnimalMovimientoInfo> animalesDisponiblesReimplante = animalBL.ObtenerAnimalesPorLoteReimplante(lotesReimplante,
                                                                                                     filtroDisponilidadInfo.OrganizacionId);
                //if(animalesDisponiblesReimplante == null)
                //{
                //    return null;
                //}

                foreach (var lote in lotesReimplante)
                {
                    var animalesFecha =
                            (from animal in animalesDisponiblesReimplante
                             where animal.LoteID == lote.LoteId
                             select animal).ToList();

                    if (animalesFecha.Any())
                    {
                        var count = animalesFecha.Count;
                        double temp = 0D;
                        for (int i = 0; i < count; i++)
                        {
                            temp += animalesFecha[i].FechaMovimiento.Ticks / (double)count;
                        }
                        var fechaPromedio = new DateTime((long)temp);
                        lote.FechaInicioLote = fechaPromedio;
                    }
                }

                foreach (var loteDisponibilidad in disponibilidadLote)
                {
                    if (loteDisponibilidad.FechaDisponibilidad != DateTime.MinValue)
                    {
                        continue;
                    }
                    var loteReimplante = lotesReimplante.FirstOrDefault(lo => lo.LoteId == loteDisponibilidad.LoteId);
                    if (loteReimplante == null)
                    {
                        loteDisponibilidad.FechaDisponibilidad =
                            loteDisponibilidad.FechaInicioLote.AddDays(loteDisponibilidad.DiasEngorda);
                        loteDisponibilidad.FechaAsignada =
                            loteDisponibilidad.FechaInicioLote.AddDays(loteDisponibilidad.DiasEngorda);
                    }
                    else
                    {
                        loteDisponibilidad.FechaDisponibilidad = loteReimplante.FechaInicioLote.AddDays(loteDisponibilidad.DiasEngorda);
                        loteDisponibilidad.FechaAsignada = loteReimplante.FechaInicioLote.AddDays(loteDisponibilidad.DiasEngorda);
                    }
                }


                var diasSumar = filtroDisponilidadInfo.Semanas * 7;
                filtroDisponilidadInfo.Fecha = filtroDisponilidadInfo.Fecha.AddDays(diasSumar);

                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                var configuracion = configuracionSemanaBL.ObtenerPorOrganizacion(filtroDisponilidadInfo.OrganizacionId);
                if (configuracion == null)
                {
                    filtroDisponilidadInfo.Fecha = filtroDisponilidadInfo.Fecha.StartOfWeek(DayOfWeek.Monday);
                }
                else
                {
                    var enumerador = configuracion.InicioSemana; //Enum.Parse(typeof (DiasSemana), configuracion.InicioSemana.ToUpper());
                    var dia = (DayOfWeek)enumerador.GetHashCode();
                    filtroDisponilidadInfo.Fecha = filtroDisponilidadInfo.Fecha.StartOfWeek(dia);
                }

                // Sea la semana que sea siempre se le ban a sumar 6 meses
                filtroDisponilidadInfo.FechaFin = filtroDisponilidadInfo.Fecha.AddDays(6);

                if (filtroDisponilidadInfo.Semanas != 0)
                {
                    lotesFiltrados =
                        disponibilidadLote.Where(
                            lote =>
                            lote.FechaDisponibilidad.Date >= filtroDisponilidadInfo.Fecha.Date &&
                            lote.FechaDisponibilidad.Date <= filtroDisponilidadInfo.FechaFin.Date).ToList();
                }
                else
                {
                    lotesFiltrados =
                       disponibilidadLote.Where(
                           lote =>
                           lote.Revision && lote.FechaDisponibilidadOriginal == DateTime.MinValue).ToList();
                }

                List<AnimalInfo> animalesDisponibles = animalBL.ObtenerAnimalesPorLoteDisponibilidad(lotesFiltrados,
                                                                                                     filtroDisponilidadInfo.OrganizacionId);

                if (!lotesFiltrados.Any())
                {
                    return null;
                }

                if (animalesDisponibles == null)
                {
                    return null;
                }

                foreach (var loteDisponibilidad in lotesFiltrados)
                {
                    DisponibilidadLoteInfo disponibilidad = loteDisponibilidad;
                    var animalesDetalle =
                        (from det in animalesDisponibles.SelectMany(ani => ani.ListaAnimalesMovimiento)
                         where det.LoteID == disponibilidad.LoteId
                         select det).ToList();

                    if (!animalesDetalle.Any())
                    {
                        continue;
                    }

                    var animalesLote = (from animal in animalesDisponibles
                                        join det in animalesDetalle on animal.AnimalID equals det.AnimalID
                                        select animal).ToList();

                    if (!animalesLote.Any())
                    {
                        continue;
                    }

                    var totalPesoCompra = animalesLote.Sum(animal => animal.PesoCompra);
                    var totalAnimales = animalesLote.Count();

                    var count = totalAnimales;
                    double temp = 0D;
                    for (int i = 0; i < count; i++)
                    {
                        temp += animalesLote[i].FechaEntrada.Ticks / (double)count;
                    }
                    var average = new DateTime((long)temp);

                    TimeSpan diasEngordaReales = fechaActual.FechaActual - average;

                    disponibilidad.DiasEngorda = Convert.ToInt32(Math.Round(diasEngordaReales.TotalDays, 0));

                    if (disponibilidad.FechaDisponibilidadOriginal != DateTime.MinValue)
                    {
                        TimeSpan diasProyectados = disponibilidad.FechaDisponibilidad - average;
                        disponibilidad.DatosProyector.DiasEngorda = Convert.ToInt32(Math.Round(diasProyectados.TotalDays));
                    }

                    string descripcionTipo;
                    ObtenerTipoCheckListLoteCorralCompleto(animalesLote, out descripcionTipo);
                    disponibilidad.Tipo = descripcionTipo;
                    disponibilidad.PesoOrigen = Convert.ToInt32(Math.Round((decimal)totalPesoCompra / totalAnimales, 2));

                    disponibilidad.PesoProyectado = ObtenerPesoProyectadoDisponibilidad(animalesDetalle,
                                                                                        disponibilidad.FechaInicioLote,
                                                                                        disponibilidad.GananciaDiaria);
                }
                //var permiteDiasDisponibilidad = (from dispo in ConstantesBL.PermiteDiasDisponibilidad
                //                                 where dispo == filtroDisponilidadInfo.Semanas
                //                                 select dispo).FirstOrDefault();
                //if (permiteDiasDisponibilidad != 0)
                //{
                lotesFiltrados.ForEach(lote => lote.SumarDias = true);
                //}
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
            //projects.OrderBy(p => p.Substring(p.IndexOf(' ') + 2, p.Length - (p.IndexOf(' ') + 2)));

            Func<string, object> convert = str =>
            {
                try { return int.Parse(str); }
                catch { return str; }
            };
            var sorted = lotesFiltrados.OrderBy(
                str => Regex.Split(str.CodigoCorral, "([0-9]+)").Select(convert),
                new EnumerableComparer<object>());

            return sorted.ToList();
            //return lotesFiltrados;
        }

        /// <summary>
        /// Obtiene un tipo de Ganado
        /// </summary>
        /// <param name="animalesMovimientoLote">Lista de Movimientos de Animal</param>
        /// <param name="fechaInicioLote">Fecha en que se abrió el Lote</param>
        /// /// <param name="gananciaDiaria">Ganancia Diaria del Lote</param>
        /// <returns></returns>
        private int ObtenerPesoProyectadoDisponibilidad(IEnumerable<AnimalMovimientoInfo> animalesMovimientoLote, DateTime fechaInicioLote, decimal gananciaDiaria)
        {
            var animalesActivos = animalesMovimientoLote.Where(ani => ani.Activo == EstatusEnum.Activo).ToList();
            if (!animalesActivos.Any())
            {
                return 0;
            }
            var totalPeso = animalesActivos.Sum(ani => ani.Peso);
            var pesoPromedio = totalPeso / animalesActivos.Count;
            var ganancia = (DateTime.Now - fechaInicioLote).TotalDays * (double)gananciaDiaria;
            var pesoProyectado = Convert.ToInt32(pesoPromedio + ganancia);
            return pesoProyectado;
        }

        /// <summary>
        /// Obtiene un tipo de Ganado
        /// </summary>
        /// <param name="animalesLote">Lista de Animales</param>
        /// <param name="checkListTipo">Tipo de Check List</param>
        /// <returns></returns>
        private dynamic ObtenerTipoCheckListLoteCorralCompleto(List<AnimalInfo> animalesLote, out string checkListTipo)
        {
            var agrupadosPorTipoGanado = (from animales in animalesLote
                                          group animales by animales.TipoGanado.TipoGanadoID
                                              into tiposGanado
                                              let firstOrDefault = tiposGanado.FirstOrDefault()
                                              where firstOrDefault != null
                                              select new
                                              {
                                                  firstOrDefault.TipoGanado,
                                                  TotalTipoGanado = tiposGanado.Count(),
                                                  Porcentaje = ((decimal)tiposGanado.Count() / (decimal)animalesLote.Count) * 100
                                              }).ToList();

            var porcentajeMayorTipoGanado = agrupadosPorTipoGanado.Max(agrupado => agrupado.Porcentaje);

            var tipoGanadoMayor = (from tipo in agrupadosPorTipoGanado
                                   where tipo.Porcentaje == porcentajeMayorTipoGanado
                                   select tipo);

            var tipoGanadoFinal =
                tipoGanadoMayor.OrderByDescending(tipo => tipo.TipoGanado.TipoGanadoID).FirstOrDefault();

            var agrupadorPorClasificacion = (from animal in animalesLote
                                             group animal by animal.ClasificacionGanado.ClasificacionGanadoID
                                                 into clasificacionesAgrupadas
                                                 let animalInfo = clasificacionesAgrupadas.FirstOrDefault()
                                                 where animalInfo != null
                                                 select new
                                                 {
                                                     animalInfo.ClasificacionGanado,
                                                     TotalClasificacion = clasificacionesAgrupadas.Count(),
                                                     Porcentaje = ((decimal)clasificacionesAgrupadas.Count() / (decimal)animalesLote.Count) * 100
                                                 }).ToList();

            var porcentajeMayorClasificacionGanado = agrupadorPorClasificacion.Max(agrupado => agrupado.Porcentaje);

            var clasificacionGanadoMayor = (from clasificacion in agrupadorPorClasificacion
                                            where clasificacion.Porcentaje == porcentajeMayorClasificacionGanado
                                            select clasificacion);

            var clasificacionGanadoFinal =
                clasificacionGanadoMayor.OrderByDescending(clasificacion => clasificacion.ClasificacionGanado.ClasificacionGanadoID).FirstOrDefault();

            checkListTipo = string.Empty;
            if (tipoGanadoFinal == null || clasificacionGanadoFinal == null)
            {
                return null;
            }
            checkListTipo = string.Format("{0} {1}", tipoGanadoFinal.TipoGanado.Descripcion,
                                           clasificacionGanadoFinal.ClasificacionGanado.Descripcion);
            return tipoGanadoFinal;
        }

        /// <summary>
        /// Actualiza la Disponibilidad de los Lotes
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        public void ActualizarLoteDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();

                filtroDisponilidadInfo.Fecha = filtroDisponilidadInfo.Fecha.AddDays(42);

                var configuracionSemanaBL = new ConfiguracionSemanaBL();
                var configuracion = configuracionSemanaBL.ObtenerPorOrganizacion(filtroDisponilidadInfo.OrganizacionId);
                if (configuracion == null)
                {
                    filtroDisponilidadInfo.Fecha = filtroDisponilidadInfo.Fecha.StartOfWeek(DayOfWeek.Monday);
                }
                else
                {
                    var enumerador = configuracion.InicioSemana; //Enum.Parse(typeof (DiasSemana), configuracion.InicioSemana.ToUpper());
                    var dia = (DayOfWeek)enumerador.GetHashCode();
                    filtroDisponilidadInfo.Fecha = filtroDisponilidadInfo.Fecha.StartOfWeek(dia);
                }

                filtroDisponilidadInfo.FechaFin = filtroDisponilidadInfo.Fecha.AddDays(6);
                foreach (var lote in filtroDisponilidadInfo.ListaLoteDisponibilidad)
                {
                    if (lote.FechaDisponibilidad.Date >= filtroDisponilidadInfo.Fecha.Date && lote.FechaDisponibilidad.Date <= filtroDisponilidadInfo.FechaFin.Date)
                    {
                        lote.DisponibilidadManual = true;
                    }
                    else
                    {
                        lote.DisponibilidadManual = false;
                    }
                }
                loteDAL.ActualizarLoteDisponibilidad(filtroDisponilidadInfo);
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

        public LoteInfo ObtenerPorCorralID(LoteInfo loteInfo)
        {
            LoteInfo lote;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lote = loteDAL.ObtenerPorCorralID(loteInfo);
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
            return lote;
        }

        public void ActualizarSalidaEnfermeria(AnimalMovimientoInfo resultadoLoteOrigen)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizarSalidaEnfermeria(resultadoLoteOrigen);
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

        public void EliminarSalidaEnfermeria(AnimalMovimientoInfo loteCorralOrigen)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.EliminarSalidaEnfermeria(loteCorralOrigen);
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
        /// Obtiene el tipo de corral y calida ganado promedio
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public TipoGanadoInfo ObtenerTipoGanadoCorral(LoteInfo lote)
        {
            try
            {
                var animalBL = new AnimalBL();
                var tipoGanadoInfo = new TipoGanadoInfo();
                var animalesLote = animalBL.ObtenerAnimalesPorLote(lote.OrganizacionID,
                    lote.LoteID);
                if (animalesLote != null)
                {
                    string checkListTipo;
                    var tipoGanado = ObtenerTipoCheckListLoteCorralCompleto(animalesLote, out checkListTipo);
                    if(tipoGanado != null)
                    {
                        tipoGanadoInfo = tipoGanado.TipoGanado;
                        tipoGanadoInfo.Descripcion = checkListTipo;
                    }
                }

                return tipoGanadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
        /// <summary>
        /// Se obtiene el lote de una corraleta
        /// </summary>
        /// <param name="corraleta"></param>
        /// <returns></returns>
        public IList<LoteInfo> ObtenerLoteDeCorraleta(CorralInfo corraleta)
        {
            IList<LoteInfo> lotes;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lotes = loteDAL.ObtenerLoteDeCorraleta(corraleta);
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
            return lotes;
        }

        public void ActualizaNoCabezasEnLoteOrigen(LoteInfo resultadoLoteOrigen)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizaNoCabezasEnLoteOrigen(resultadoLoteOrigen);
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


        public AnimalSalidaInfo ObtenerAnimalSalidaPorCodigo(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                return loteDAL.ObtenerAnimalSalidaPorCodigo(corralInfo);
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
        /// Verifica si existe lote en animal salida
        /// </summary>
        /// <param name="loteOrigen"></param>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        public AnimalSalidaInfo ExisteLoteAnimalSalida(LoteInfo loteOrigen, CorralInfo corralInfo)
        {
            AnimalSalidaInfo animalSalidaInfo;
            try
            {
                Logger.Info();
                var loteDal = new LoteDAL();
                animalSalidaInfo = loteDal.ExisteLoteAnimalSalida(loteOrigen, corralInfo);
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
            return animalSalidaInfo;
        }

        internal TipoGanadoInfo ObtenerSoloTipoGanadoCorral(List<AnimalInfo> animalesLote, LoteInfo lote)
        {
            try
            {
                TipoGanadoInfo tipoGanadoResultado = null;
                int peso = 0, pesoTotal = 0;
                AnimalBL animalBl = new AnimalBL();
                for (int i = 0; i < animalesLote.Count; i++)
                {
                    var animal = animalBl.ObtenerUltimoMovimientoAnimal(animalesLote[i]);
                    pesoTotal = pesoTotal + animal.Peso;
                }

                peso = pesoTotal / lote.Cabezas;

                //Para obtener el sexo
                var agrupadosPorTipoGanado = (from animales in animalesLote
                                              group animales by animales.TipoGanadoID
                                                  into tiposGanado
                                                  let firstOrDefault = tiposGanado.FirstOrDefault()
                                                  where firstOrDefault != null
                                                  select new
                                                  {
                                                      firstOrDefault.TipoGanadoID,
                                                      TotalTipoGanado = tiposGanado.Count(),
                                                      Porcentaje = ((decimal)tiposGanado.Count() / (decimal)animalesLote.Count) * 100
                                                  }).ToList();

                var porcentajeMayorTipoGanado = agrupadosPorTipoGanado.Max(agrupado => agrupado.Porcentaje);

                var tipoGanadoMayor = (from tipo in agrupadosPorTipoGanado
                                       where tipo.Porcentaje == porcentajeMayorTipoGanado
                                       select tipo);

                var tipoGanadoFinal =
                    tipoGanadoMayor.OrderByDescending(tipo => tipo.TipoGanadoID).FirstOrDefault();

                TipoGanadoBL tipoGanadoPL = new TipoGanadoBL();

                tipoGanadoResultado = tipoGanadoPL.ObtenerPorID(tipoGanadoFinal.TipoGanadoID);

                if (tipoGanadoResultado.Sexo.ToString().Contains(Sexo.Macho.ToString()))
                {
                    var sexo = (char)Sexo.Macho;
                    tipoGanadoResultado = tipoGanadoPL.ObtenerTipoGanadoSexoPeso(sexo.ToString(), peso);
                }
                else
                {
                    var sexo = (char)Sexo.Hembra;
                    tipoGanadoResultado = tipoGanadoPL.ObtenerTipoGanadoSexoPeso(sexo.ToString(), peso);
                }

                return tipoGanadoResultado;
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

        internal List<LoteInfo> ObtenerLotesDescripcionPorIDs(List<int> lotesID)
        {
            try
            {
                Logger.Info();
                var loteDal = new LoteDAL();
                return loteDal.ObtenerLotesDescripcionPorIDs(lotesID);
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

        internal Base.Infos.ResultadoInfo<LoteInfo> ObtenerPorPagina(PaginacionInfo paginacion, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteDal = new LoteDAL();
                return loteDal.ObtenerPorPagina(paginacion, lote);
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

        internal ResultadoInfo<LoteInfo> ObtenerLotesCorralPorPagina(PaginacionInfo paginacion, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteDal = new SIE.Services.Integracion.DAL.ORM.LoteDAL();
                return loteDal.ObtenerLotesCorralPorPagina(paginacion, lote);
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

        internal DateTime? ObtenerFechaUltimoConsumo(int loteId)
        {
            try
            {
                Logger.Info();
                var loteDal = new SIE.Services.Integracion.DAL.ORM.LoteDAL();
                return loteDal.ObtenerFechaUltimoConsumo(loteId);
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

        internal LoteInfo ObtenerLoteDeCorralPorLoteID(LoteInfo info)
        {
            try
            {
                Logger.Info();
                var loteDal = new SIE.Services.Integracion.DAL.ORM.LoteDAL();
                return loteDal.ObtenerLoteDeCorralPorLoteID(info);
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

        internal LoteInfo ObtenerLotePorCorral(LoteInfo info)
        {
            try
            {
                Logger.Info();
                var loteDal = new SIE.Services.Integracion.DAL.ORM.LoteDAL();
                return loteDal.ObtenerLotePorCorral(info);
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
        /// Obtiene el tipo de ganado
        /// de acuerdo una organización y loteId
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoLote(LoteInfo info)
        {
            try
            {
                Logger.Info();

                var animalBL = new AnimalBL();
                var animalesLote = animalBL.ObtenerAnimalesPorLote(info.OrganizacionID,
                                                             info.LoteID);

                TipoGanadoInfo result = null;
                if (animalesLote != null && animalesLote.Any())
                {
                    string tipo = string.Empty;
                    var tipoganado = ObtenerTipoCheckListLoteCorralCompleto(animalesLote, out tipo);
                    result = tipoganado.TipoGanado;
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

        /// <summary>
        /// Obtiene una lista de lotes por corral y organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal IList<LoteInfo> ObtenerPorOrganizacionLoteXML(int organizacionId, List<CorralInfo> corrales)
        {
            IList<LoteInfo> lotes;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lotes = loteDAL.ObtenerPorOrganizacionLoteXML(organizacionId, corrales);
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
            return lotes;
        }

        /// <summary>
        /// Obtiene Corrales Cerrados por XML
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal IList<LoteInfo> ObtenerPorCorralCerradoXML(int organizacionId, List<CorralInfo> corrales)
        {
            IList<LoteInfo> lotes;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lotes = loteDAL.ObtenerPorCorralCerradoXML(organizacionId, corrales);
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
            return lotes;
        }

        /// <summary>
        /// Obtiene la información para generar el Reparto del Lote
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal LoteDescargaDataLinkModel ObtenerLoteDataLink(int organizacionId, int loteID)
        {
            LoteDescargaDataLinkModel lotes;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lotes = loteDAL.ObtenerLoteDataLink(organizacionId, loteID);
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
            return lotes;
        }

        /// <summary>
        /// Actualiza el corral de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        internal bool ActualizarCorral(LoteInfo lote, CorralInfo corralInfoDestino, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizarCorral(lote, corralInfoDestino, usuario);
                return true;
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
        /// Obtiene el lote anterior en donde estuvo el ganado
        /// </summary>
        /// <param name="loteID">Id del lote donde se encuentra actualmente el ganado</param>
        internal LoteInfo ObtenerLoteAnteriorAnimal(int loteID)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                return loteDAL.ObtenerLoteAnteriorAnimal(loteID);
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
        /// Obtiene los Dias de grano de los lotes
        /// </summary>
        /// <param name="lote">Id del lote para buscar sus dias de gran</param>
        internal List<DiasEngordaGranoModel> ObtenerDiasEngordaGrano(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                return loteDAL.ObtenerDiasEngordaGrano(lote);
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
        /// Este metodo obtiene el tipo de ganado de un loteen base al 50 + 1, es decir
        /// obtiene todos los tipos de gadados de un lote y toma el que tiene mayor numero de animales
        /// </summary>
        /// <param name="loteCorral"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoLoteID(LoteInfo loteCorral)
        {
            try
            {
                var loteDAL = new LoteDAL();
                return loteDAL.ObtenerTipoGanadoLoteID(loteCorral);
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
        /// Obtiene corral con sus lotes, grupo y tipo de corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        internal IEnumerable<LoteInfo> ObtenerPorCodigoCorralOrganizacionID(int organizacionID, string codigo)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                IEnumerable<LoteInfo> lotes = loteDAL.ObtenerPorCodigoCorralOrganizacionID(organizacionID, codigo);
                return lotes;
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
        /// Actualiza las cabezas de una lista de lotes
        /// </summary>
        /// <param name="lotes"></param>
        internal void AcutalizaCabezasLoteXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.AcutalizaCabezasLoteXML(lotes);
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
        /// Desactiva los lotes que se encuentran en la lista
        /// </summary>
        /// <param name="lotesDesactivar"></param>
        internal void DesactivarLoteXML(List<LoteInfo> lotesDesactivar)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.DesactivarLoteXML(lotesDesactivar);
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
        /// Obtiene una lista de lotes por Organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal IList<LoteInfo> ObtenerPorOrganizacionEstatus(int organizacionId, EstatusEnum estatus)
        {
            IList<LoteInfo> lote;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                lote = loteDAL.ObtenerPorOrganizacionEstatus(organizacionId, estatus);
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
            return lote;
        }

        /// <summary>
        /// Actualiza la entrada/salida a zilmax
        /// </summary>
        /// <param name="lotes"></param>
        internal void GuardarEntradaSalidaZilmax(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.GuardarEntradaSalidaZilmax(lotes);
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
        /// Obtiene los lotes por XML
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IEnumerable<LoteInfo> ObtenerPorLoteXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                IEnumerable<LoteInfo> lotesSÏAP = loteDAL.ObtenerPorLoteXML(lotes);
                return lotesSÏAP;
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
        /// Actualiza las cabezas en lote
        /// </summary>
        /// <param name="lotesDestino"></param>
        /// <param name="lotesOrigen"></param>
        internal void ActualizaNoCabezasEnLoteXML(List<LoteInfo> lotesDestino, List<LoteInfo> lotesOrigen)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                loteDAL.ActualizaNoCabezasEnLoteXML(lotesDestino, lotesOrigen);
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
        /// Obtiene el lote filtrando por CorralID, y Codigo de Lote
        /// </summary>
        /// <param name="lote">Objeto que contiene los parametros del Lote</param>
        internal LoteInfo ObtenerLotePorCodigoLote(LoteInfo lote)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerLotePorCodigoLote(lote);
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
        /// Obtiene el Peso promedio de Compra de un Lote
        /// </summary>
        /// <param name="lote">Objeto que contiene los parametros del Lote</param>
        internal LoteInfo ObtenerPesoCompraPorLote(LoteInfo lote)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerPesoCompraPorLote(lote);
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
        /// Actualiza el Numero de Cabezas de los Lotes
        /// </summary>
        /// <param name="filtroActualizarCabezasLote"></param>
        /// <returns></returns>
        internal CabezasActualizadasInfo ActualizarCabezasProcesadas(FiltroActualizarCabezasLote filtroActualizarCabezasLote)
        {
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                return loteDAL.ActualizarCabezasProcesadas(filtroActualizarCabezasLote);
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
        /// Obtiene el estatus de un lote
        /// </summary>
        /// <param name="loteId">Objeto que contiene el LoteId</param>
        public LoteInfo ObtenerEstatusPorLoteId(int loteId)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerEstatusPorLoteId(loteId);
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

        public LoteInfo ValidarCorralCompletoParaSacrificio(int loteId)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ValidarCorralCompletoParaSacrificio(loteId);
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

        public int ValidarCorralCompletoParaSacrificioScp(string fechaProduccion, string lote, string corral, int organizacionId)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ValidarCorralCompletoParaSacrificioScp(fechaProduccion, lote, corral, organizacionId);
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

        public List<AnimalInfo> ObtenerAretesCorralPorLoteId(int loteId)
        {
            var result = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerAretesCorralPorLoteId(loteId);
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

        public List<LoteInfo> ObtenerLotesConAnimalesDisponiblesPorOrganizacionId(int organizacionId)
        {
            var result = new List<LoteInfo>();
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerLotesConAnimalesDisponiblesPorOrganizacionId(organizacionId);
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

        public List<AnimalInfo> ObtenerLoteConAnimalesScp(int organizacionId, string lote, string corral, string fechaSacrificio)
        {
            var result = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var loteDAL = new LoteDAL();
                result = loteDAL.ObtenerLoteConAnimalesScp(organizacionId, lote, corral, fechaSacrificio);
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