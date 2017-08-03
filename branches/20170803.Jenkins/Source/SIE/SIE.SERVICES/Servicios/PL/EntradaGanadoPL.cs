using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SIE.Services.Servicios.PL
{
    public class EntradaGanadoPL : IAyuda<EntradaGanadoInfo>, IAyudaDependencia<EntradaGanadoInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada de entradas 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoInfo filtro)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultadoOrganizacion = entradaGanadoBL.ObtenerPorPagina(pagina, filtro);
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
            return resultadoOrganizacion;
        }
        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// /// <param name="actualizarRecibido"></param>
        public int GuardarEntradaGanado(EntradaGanadoInfo entradaGanado, bool actualizarRecibido)
        {
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                return entradaGanadoBL.GuardarEntradaGanado(entradaGanado, actualizarRecibido);
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
        /// Metodo que obtiene una entrada de ganado por ID
        /// </summary>
        /// <param name="entradaGanadoID"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerPorID(int entradaGanadoID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorID(entradaGanadoID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerPorFolioEntrada(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorFolioEntrada(folioEntrada, organizacionID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene las entradas de ganado por Tipo de Corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerPorTipoCorral(int organizacionID, int tipoCorralID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorTipoCorral(organizacionID, tipoCorralID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene las entradas de ganado por ID de Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralTipoID"></param>
        /// <returns></returns>
        public List<EntradaGanadoInfo> TraerGanadoPorOrganizacionId(int organizacionID, int corralTipoID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorIDOrganizacion(organizacionID, corralTipoID);
                if (entradaGanadoInfo != null && entradaGanadoInfo.Lista != null &&
                    entradaGanadoInfo.Lista.Count > 0)
                {
                    var listaBusqueda = (from item in entradaGanadoInfo.Lista
                                         select new EntradaGanadoInfo
                                         {
                                             Horas = CalcularHorasTranscurridas(item.FechaEntrada, item.FechaSalida),
                                             FolioEntrada = item.FolioEntrada,
                                             CodigoCorral = item.CodigoCorral,
                                             LoteID = item.Lote.LoteID,
                                             CodigoLote = item.Lote.Lote,
                                             CabezasOrigen = item.CabezasOrigen,
                                             PesoTara = item.PesoTara,
                                             FechaEntrada = item.FechaEntrada,
                                             OrganizacionOrigen = item.OrganizacionOrigen,
                                             FolioOrigen = item.FolioOrigen,
                                             CorralID = item.CorralID,
                                             PesoBruto = item.PesoBruto,
                                             FechaSalida = item.FechaSalida,
                                             CabezasRecibidas = item.CabezasRecibidas,
                                             PesoOrigen = item.PesoOrigen
                                         }).ToList();
                    return listaBusqueda;
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
        /// Metodo para calcular las horas transcurridas del viaje
        /// </summary>
        internal double CalcularHorasTranscurridas(DateTime fechaEntrada, DateTime salida)
        {
            TimeSpan horasTranscurridas = TimeSpan.MinValue;
            horasTranscurridas = fechaEntrada - salida;
            return horasTranscurridas.TotalHours;
        }

        /// <summary>
        /// Obtiene un listado de entradas Activas Paginadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerEntradasActivasPorPagina(PaginacionInfo pagina, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerEntradasActivasPorPagina(pagina, organizacionID);
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
            return listaEntradaGanado;
        }
        /// <summary>
        /// Obtener Entrada de Ganado por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultadoInfo<EntradaGanadoInfo> IAyuda<EntradaGanadoInfo>.ObtenerPorId(int id)
        {
            var resultado = new ResultadoInfo<EntradaGanadoInfo>();
            var entradas = new List<EntradaGanadoInfo>();
            var entradasGanado = ObtenerPorID(id);
            entradas.Add(entradasGanado);
            resultado.Lista = entradas;

            return resultado;
        }

        ResultadoInfo<EntradaGanadoInfo> IAyuda<EntradaGanadoInfo>.ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion)
        {
            var organizacion = new EntradaGanadoInfo { CodigoCorral = descripcion, Activo = Info.Enums.EstatusEnum.Activo };
            ResultadoInfo<EntradaGanadoInfo> resultado = ObtenerPorPagina(pagina, organizacion);

            return resultado;
        }

        ResultadoInfo<EntradaGanadoInfo> IAyudaDependencia<EntradaGanadoInfo>.ObtenerPorId(int Id, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerPorId(Id, Dependencias);
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
            return listaEntradaGanado;
        }
        /// <summary>
        /// Obtiene la Entrada de Ganado
        /// </summary>
        /// <param name="entradaInfo"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerPorDependencias(EntradaGanadoInfo entradaInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerPorDependencias(entradaInfo, Dependencias);
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
            return resultado;
        }

        ResultadoInfo<EntradaGanadoInfo> IAyudaDependencia<EntradaGanadoInfo>.ObtenerPorDescripcion(PaginacionInfo Pagina, string Descripcion, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerPorDescripcion(Pagina, Descripcion, Dependencias);
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
            return listaEntradaGanado;
        }
        public ResultadoInfo<EntradaGanadoInfo> ObtenerPorDependencias(PaginacionInfo Pagina, EntradaGanadoInfo entradaInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerPorDependencias(Pagina, entradaInfo, Dependencias);
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
            return listaEntradaGanado;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="organizacionOrigenID"> </param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerPorEmbarqueID(int embarqueID, int organizacionOrigenID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorEmbarqueID(embarqueID, organizacionOrigenID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerPorEmbarqueID(int embarqueID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorEmbarqueID(embarqueID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado
        /// </summary>
        /// <param name="entradaInfo"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradasGanadoRecibidasPorDependencias(EntradaGanadoInfo entradaInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasGanadoRecibidasPorDependencias(entradaInfo, Dependencias);
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
            return resultado;
        }

        public ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoRecibidasPaginaPorDependencias(PaginacionInfo Pagina, EntradaGanadoInfo entradaInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerEntradaGanadoRecibidasPaginaPorDependencias(Pagina, entradaInfo, Dependencias);
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
            return listaEntradaGanado;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasGanadoRecibidas(folioEntrada, organizacionID);
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
            return resultado;
        }
        /// <summary>
        /// Metodo que obtiene entradas programadas.
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadas( int folioEntrada, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerPartidasProgramadas(folioEntrada, organizacionID);
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
            return listaEntradaGanado;
        }
        /// <summary>
        /// Metodo que obtiene entradas programadas.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadasPorPaginas(PaginacionInfo pagina, int folioEntrada, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerPartidasProgramadasPorPaginas(pagina, folioEntrada, organizacionID);
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
            return listaEntradaGanado;
        }
        /// <summary>
        /// Metodo que obtiene una entrada De CalidadGanado por sexo
        /// </summary>
        /// <returns></returns>
        public IList<CalidadGanadoInfo> ObtenerCalidadPorSexo(string sexo)
        {
            IList<CalidadGanadoInfo> calidadGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                calidadGanadoInfo = entradaGanadoBL.ObtenerCalidadPorSexo(sexo);
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
            return calidadGanadoInfo;
        }
        /// <summary>
        /// Metodo que obtiene una entrada De CalidadGanado por CausaRechazo
        /// </summary>
        /// <returns></returns>
        public IList<CausaRechazoInfo> ObtenerCalidadPorCausaRechazo()
        {
            IList<CausaRechazoInfo> causaRechazoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBl = new EntradaGanadoBL();
                causaRechazoInfo = entradaGanadoBl.ObtenerCalidadPorCausaRechazo();
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
            return causaRechazoInfo;
        }

        /// <summary>
        /// Metodo que obtiene el catalogo de clasificacion
        /// </summary>
        /// <returns></returns>
        public IList<ClasificacionGanadoInfo> ObtenerCatClasificacion()
        {
            IList<ClasificacionGanadoInfo> catClasificacionInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBl = new EntradaGanadoBL();
                catClasificacionInfo = entradaGanadoBl.ObtenerCatClasificacion();
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
            return catClasificacionInfo;
        }

        /// <summary>
        /// Metodo que obtiene las entradas de ganado por ID de Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerPorIDOrganizacion(int organizacionID,int tipoCorralID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerPorIDOrganizacion(organizacionID, tipoCorralID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado
        /// </summary>
        /// <param name="entradaInfo"></param>
        /// <param name="Dependencias"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradasGanadoCosteadoPorDependencias(EntradaGanadoInfo entradaInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasGanadoCosteadoPorDependencias(entradaInfo, Dependencias);
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
            return resultado;
        }

        public ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoCosteadoPaginaPorDependencias(PaginacionInfo Pagina, EntradaGanadoInfo entradaInfo, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerEntradaGanadoCosteadoPaginaPorDependencias(Pagina, entradaInfo, Dependencias);
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
            return listaEntradaGanado;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradasGanadoCosteado(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasGanadoCosteado(folioEntrada, organizacionID);
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
            return resultado;
        }

        /// <summary>
        /// Metodo para consultar los datos de la entrada para la captura de Calidad de Ganado
        /// </summary>
        /// <param name="filtroCalificacionGanado"></param>
        public EntradaGanadoInfo ObtenerEntradaGanadoCapturaCalidad(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                entradaGanadoInfo = entradaGanadoBL.ObtenerEntradaGanadoCapturaCalidad(filtroCalificacionGanado);
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
            return entradaGanadoInfo;
        }    

        /// <summary>
        ///   Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        public void GuardarEntradaCondicion(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                 entradaGanadoBL.GuardarEntradaCondicion(entradaGanado);
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
        /// Metodo que obtiene las entradas de ganado por Tipo de Corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        public int ObtenerPorCorralDisponible(int organizacionID, int corralID, int embarqueID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                int result =  entradaGanadoBL.ObtenerPorCorralDisponible(organizacionID, corralID, embarqueID);
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
        ///  Metodo que obtiene las entradas de ganado por Lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradaPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var entradaGanadoBl = new EntradaGanadoBL();
                var result = entradaGanadoBl.ObtenerEntradaPorLote(lote);
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

        public ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaPaginado(PaginacionInfo pagina, EntradaGanadoInfo entradaInfo)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaEntradaGanado = entradaGanadoBL.ObtenerEntradaPaginado(pagina, entradaInfo);
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
            return listaEntradaGanado;
        }

        /// <summary>
        /// Obtiene la Entrada de Ganado
        /// </summary>
        /// <param name="entradaInfo"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(EntradaGanadoInfo entradaInfo)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasGanadoRecibidas(entradaInfo);
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
            return resultado;
        }

        /// <summary>
        /// Metodo para obtener una partida de compra directa de una lista de partidas para un lote
        /// </summary>
        /// <param name="entradaSeleccionada"></param>
        /// <returns></returns>
        public int ObtenerPartidaCompraDirecta(EntradaGanadoInfo entradaSeleccionada)
        {
            int resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerPartidaCompraDirecta(entradaSeleccionada);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene entradas de ganado por EntradaGanadoID
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        public List<EntradaGanadoInfo> ObtenerEntradasPorIDs(List<int> entradas)
        {
            List<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasPorIDs(entradas);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene entradas de ganado por EntradaGanadoID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        public List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(int embarqueID)
        {
            List<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasPorEmbarqueID(embarqueID);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene las entradas para su impresion de tarjeta de recepcion
        /// </summary>
        /// <param name="filtro">filtros para obtener las entradas a imprimir</param>
        /// <returns></returns>
        public List<ImpresionTarjetaRecepcionModel> ObtenerEntradasImpresionTarjetaRecepcion(FiltroImpresionTarjetaRecepcion filtro)
        {
            List<ImpresionTarjetaRecepcionModel> resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasImpresionTarjetaRecepcion(filtro);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene las entradas para su impresion de tarjeta de recepcion
        /// </summary>
        /// <param name="filtro">filtros para obtener las entradas a imprimir</param>
        /// <returns></returns>
        public List<ImpresionCalidadGanadoModel> ObtenerEntradasImpresionCalidadGanado(FiltroImpresionCalidadGanado filtro)
        {
            List<ImpresionCalidadGanadoModel> resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasImpresionCalidadGanado(filtro);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene entradas de ganado por folio de origen
        /// </summary>
        /// <returns></returns>
        public List<EntradaGanadoInfo> ObtenerEntradasPorFolioOrigenXML(List<EntradaGanadoInfo> foliosOrigen)
        {
            List<EntradaGanadoInfo> resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerEntradasPorFolioOrigenXML(foliosOrigen);
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
            return resultado;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public EntradaGanadoInfo ObtenerPorFolioEntradaCortadaIncompleta(EntradaGanadoInfo filtro)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultado = entradaGanadoBL.ObtenerPorFolioEntradaCortadaIncompleta(filtro);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="entradaGanadoInfo">Folio Por el Cual se Filtrara</param>
        /// <returns></returns>
        public ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoPaginaCortadasIncompletas(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo)
        {
            ResultadoInfo<EntradaGanadoInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                resultadoOrganizacion = entradaGanadoBL.ObtenerEntradaGanadoPaginaCortadasIncompletas(pagina, entradaGanadoInfo);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<FiltroAnimalesReemplazoArete> ObtenerReemplazoAretes(EntradaGanadoInfo filtro)
        {
            List<FiltroAnimalesReemplazoArete> listaAretes;
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                listaAretes = entradaGanadoBL.ObtenerReemplazoAretes(filtro);
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
            return listaAretes;
        }

        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="listaAretes"></param>
        /// /// <param name="entradaGanado"></param>
        public int GuardarReemplazoAretes(List<FiltroAnimalesReemplazoArete> listaAretes, EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                var entradaGanadoBL = new EntradaGanadoBL();
                return entradaGanadoBL.GuardarReemplazoAretes(listaAretes, entradaGanado);
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
 