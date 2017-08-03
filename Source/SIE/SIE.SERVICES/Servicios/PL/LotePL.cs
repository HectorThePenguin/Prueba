using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Servicios.BL;
using SIE.Base.Infos;
using System.Linq;

namespace SIE.Services.Servicios.PL
{
    public class LotePL
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
                var loteBL = new LoteBL();
                lista = loteBL.ObtenerTodos();
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
        /// Obtiene una lista de Lote filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<LoteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                IList<LoteInfo> lista = loteBL.ObtenerTodos(estatus);

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
                var loteBL = new LoteBL();
                totalActivos = loteBL.ObtenerActivosPorCorral(organizacionID, corralID);
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
                var loteBL = new LoteBL();
                result = loteBL.ObtenerLotesActivos(organizacionID, corralID);
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
        public LoteInfo ObtenerPorId(int loteID)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo lote = loteBL.ObtenerPorID(loteID);

                return lote;
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
        /// Obtiene un Lote por Id y Su Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        public LoteInfo ObtenerPorIdOrganizacionId(int organizacionID, int corralID, int embarqueID)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo lote = loteBL.ObtenerPorIdOrganizacionId(organizacionID, corralID, embarqueID);

                return lote;
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
        /// Genera un Nuevo Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        public int GuardaLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                int result = loteBL.GuardaLote(loteInfo);

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
        public void ActualizaCabezasLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.AcutalizaCabezasLote(loteInfo);
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
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo loteInfo = loteBL.ObtenerPorOrganizacionIdLote(organizacionID, lote);

                return loteInfo;
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
        /// Obtiene un Lote por corral cerrado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="idCorral"></param>
        /// <returns></returns>
        public LoteInfo ObtenerPorCorralCerrado(int organizacionID, int idCorral)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo loteInfo = loteBL.ObtenerPorCorralCerrado(organizacionID, idCorral);

                return loteInfo;
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
        /// Actualiza la Fecha de Cierre
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="usuarioModificacionID"> </param>
        public void ActualizaFechaCierre(int loteID, int usuarioModificacionID)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.ActualizaFechaCierre(loteID, usuarioModificacionID);
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
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo loteInfo = loteBL.ObtenerPorCorral(organizacionID, corralID);

                return loteInfo;
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

        public LoteInfo DeteccionObtenerPorCorral(int organizacionID, int corralID)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo loteInfo = loteBL.DeteccionObtenerPorCorral(organizacionID, corralID);

                return loteInfo;
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
        /// <param name="loteInfoDestino"></param>
        /// <param name="loteInfoOrigen"></param>
        public void ActualizaNoCabezasEnLote(LoteInfo loteInfoDestino, LoteInfo loteInfoOrigen)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.ActualizaNoCabezasEnLote(loteInfoDestino, loteInfoOrigen);
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
                var loteBL = new LoteBL();
                loteBL.ActualizaCabezasEnLoteProductivo(loteInfoDestino, loteInfoOrigen);
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
                var loteBL = new LoteBL();
                loteBL.ActualizarLoteALote(loteInfo);
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
                var loteBL = new LoteBL();
                loteBL.ActualizaActivoEnLote(lote);
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
                var loteBL = new LoteBL();
                loteBL.ActualizarFechaSalidaEnLote(lote);
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
                var loteBL = new LoteBL();
                List<CheckListCorralInfo> checkList = loteBL.ObtenerCheckListCorral(filtroCierreCorral);

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
        /// Obtiene la información del Lote para Check List
        /// </summary>
        /// <returns></returns>
        public CheckListCorralInfo ObtenerCheckListCorralCompleto(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                CheckListCorralInfo checkList = loteBL.ObtenerCheckListCorralCompleto(filtroCierreCorral);

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
        /// Obtiene los Lotes por su Disponibilidad
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        public List<DisponibilidadLoteInfo> ObtenerLotesPorDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                List<DisponibilidadLoteInfo> disponibilidadLote =
                    loteBL.ObtenerLotesPorDisponibilidad(filtroDisponilidadInfo);

                return disponibilidadLote;
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
        /// Actualiza la Disponibilidad de los Lotes
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        public void ActualizarLoteDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.ActualizarLoteDisponibilidad(filtroDisponilidadInfo);
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
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo lote = loteBL.ObtenerPorCorralID(loteInfo);

                return lote;
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

        public void ActualizarSalidaEnfermeria(AnimalMovimientoInfo resultadoLoteOrigen)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.ActualizarSalidaEnfermeria(resultadoLoteOrigen);
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
                var loteBL = new LoteBL();
                loteBL.EliminarSalidaEnfermeria(loteCorralOrigen);
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
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerTipoGanadoCorral(lote);
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
        /// Se obtiene el lote de una corraleta
        /// </summary>
        /// <param name="corraleta"></param>
        /// <returns></returns>
        public IList<LoteInfo> ObtenerLoteDeCorraleta(CorralInfo corraleta)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                var lote = loteBL.ObtenerLoteDeCorraleta(corraleta);

                return lote;
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

        public void ActualizaNoCabezasEnLoteOrigen(LoteInfo resultadoLoteOrigen)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.ActualizaNoCabezasEnLoteOrigen(resultadoLoteOrigen);
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
                var loteBL = new LoteBL();
                return loteBL.ObtenerAnimalSalidaPorCodigo(corralInfo);
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
        /// Obtener lote si existe el lote en animal salida
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
                var loteBL = new LoteBL();
                animalSalidaInfo = loteBL.ExisteLoteAnimalSalida(loteOrigen, corralInfo);
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

        public TipoGanadoInfo ObtenerSoloTipoGanadoCorral(List<AnimalInfo> animales, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerSoloTipoGanadoCorral(animales, lote);
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

        public ResultadoInfo<LoteInfo> ObtenerPorPagina(PaginacionInfo paginacion, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerPorPagina(paginacion, lote);
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

        public LoteInfo ObtenerPorLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                LoteInfo lote = loteBL.ObtenerPorID(loteInfo.LoteID);

                return lote;
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

        public ResultadoInfo<LoteInfo> ObtenerLotesCorralPorPagina(PaginacionInfo paginacion, LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerLotesCorralPorPagina(paginacion, lote);
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

        public LoteInfo ObtenerLoteDeCorralPorLoteID(LoteInfo info)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerLoteDeCorralPorLoteID(info);
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

        public DateTime? ObtenerFechaUltimoConsumo(int loteId)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerFechaUltimoConsumo(loteId);
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

        public LoteInfo ObtenerLotePorCorral(LoteInfo info)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                return loteBL.ObtenerLotePorCorral(info);
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
                var loteBL = new LoteBL();
                return loteBL.ActualizarCorral(lote, corralInfoDestino, usuario);
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
        /// Obtiene corral con sus lotes, grupo y tipo de corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IEnumerable<LoteInfo> ObtenerPorCodigoCorralOrganizacionID(int organizacionID, string codigo)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                IEnumerable<LoteInfo> lotes = loteBL.ObtenerPorCodigoCorralOrganizacionID(organizacionID, codigo);
                return lotes;
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
        /// Actualiza la entrada/salida a zilmax
        /// </summary>
        /// <param name="lotes"></param>
        public void GuardarEntradaSalidaZilmax(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                loteBL.GuardarEntradaSalidaZilmax(lotes);
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
        public IEnumerable<LoteInfo> ObtenerPorLoteXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                IEnumerable<LoteInfo> lotesSIAP = loteBL.ObtenerPorLoteXML(lotes);
                return lotesSIAP;
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
        /// Obtiene el lote filtrando por CorralID, y Codigo de Lote
        /// </summary>
        /// <param name="lote">Objeto que contiene los parametros del Lote</param>
        public LoteInfo ObtenerLotePorCodigoLote(LoteInfo lote)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ObtenerLotePorCodigoLote(lote);
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
        public LoteInfo ObtenerPesoCompraPorLote(LoteInfo lote)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ObtenerPesoCompraPorLote(lote);
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
        public CabezasActualizadasInfo ActualizarCabezasProcesadas(
            FiltroActualizarCabezasLote filtroActualizarCabezasLote)
        {
            CabezasActualizadasInfo result = null;
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ActualizarCabezasProcesadas(filtroActualizarCabezasLote);
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
        /// Obtiene el estatus de un lote
        /// </summary>
        /// <param name="loteId">Objeto que contiene el LoteId</param>
        public LoteInfo ObtenerEstatusPorLoteId(int loteId)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ObtenerEstatusPorLoteId(loteId);
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
                var loteBL = new LoteBL();
                result = loteBL.ValidarCorralCompletoParaSacrificio(loteId);
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

        public int ValidarCorralCompletoParaSacrificioScp(string fechaProduccion, string lote, string corral,
                                                          int organizacionId)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ValidarCorralCompletoParaSacrificioScp(fechaProduccion, lote, corral, organizacionId);
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

        public List<AnimalInfo> ObtenerAretesCorralPorLoteId(int loteid)
        {
            var result = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ObtenerAretesCorralPorLoteId(loteid);
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
                var loteBL = new LoteBL();
                result = loteBL.ObtenerLotesConAnimalesDisponiblesPorOrganizacionId(organizacionId);
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

        public List<AnimalInfo> ObtenerLoteConAnimalesScp(int organizacionId, string lote, string corral,
                                                          string fechaSacrificio)
        {
            var result = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var loteBL = new LoteBL();
                result = loteBL.ObtenerLoteConAnimalesScp(organizacionId, lote, corral, fechaSacrificio);
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

        public bool ExistenAretesRepetidos(List<AnimalInfo> aretes)
        {
            // AnimalID realmente almacena la cantidad de veces que se encontró el arete
            return aretes.Where(item => item.AnimalID > 1).ToList().Any();
        }
    }
}