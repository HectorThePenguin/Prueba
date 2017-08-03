using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Integracion.DAL;
using System.Data;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Data.SqlClient;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Info.Filtros;
using SIE.Base.Infos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class LoteDAL : DALBase
    {
        /// <summary>
        /// Obtiene una Lista con Todos los Corrales
        /// </summary>
        /// <returns></returns>
        internal List<LoteInfo> ObtenerTodos()
        {
            List<LoteInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Lote_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerTodos(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de Lote filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<LoteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Lote_ObtenerTodos", parameters);

                List<LoteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerTodos(ds);
                }

                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal int ObtenerActivosPorCorral(int organizacionID, int corralID)
        {
            int totalActivos;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorCorral(organizacionID, corralID);
                totalActivos = RetrieveValue<int>("Lote_ObtenerActivosPorCorral", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return totalActivos;
        }

        /// <summary>
        /// Obtiene Lote por Id
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal LoteInfo ObtenerPorID(int loteID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorID(loteID);
                DataSet ds = Retrieve("Lote_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un Lote por Id y Su Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        internal LoteInfo ObtenerPorIdOrganizacionId(int organizacionID, int corralID, int embarqueID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorIdOrganizacionId(organizacionID, corralID, embarqueID);
                DataSet ds = Retrieve("Lote_ObtenerPorCorralIdOrganizacionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorIDOrganizacionID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Genera un Nuevo Lote
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        internal int GuardaLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosGuardar(loteInfo);
                int result = Create("Lote_Crear", parameters);

                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns></returns>
        internal void AcutalizaCabezasLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaCabezas(loteInfo);
                Update("Lote_ActualizarCabezas", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal LoteInfo ObtenerPorOrganizacionIdLote(int organizacionID, string lote)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorOrganizacionIdLote(organizacionID, lote);
                DataSet ds = Retrieve("Lote_ObtenerPorOrganizacionIDLote", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorOrganizacionIDLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Actualiza la Fecha de Cierre
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="usuarioModificacionID"> </param>
        internal void ActualizaFechaCierre(int loteID, int? usuarioModificacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosFechaCierre(loteID, usuarioModificacionID);
                Update("Lote_ActualizaFechaCierre", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal LoteInfo ObtenerPorCorral(int organizacionID, int corralID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorCorral(organizacionID, corralID);
                DataSet ds = Retrieve("Lote_ObtenerPorCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un Lote por Organizacion y Lote
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal LoteInfo DeteccionObtenerPorCorral(int organizacionID, int corralID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorCorral(organizacionID, corralID);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerLotePorCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorOrganizacionIDLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un Lote por Organizacion y Lote
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal LoteInfo ObtenerPorCorralCerrado(int organizacionID, int corralID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorCorral(organizacionID, corralID);
                DataSet ds = Retrieve("Lote_ObtenerPorCorralCerrado", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorOrganizacionIDLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Actualiza el Numero de Cabezas del Lote
        /// </summary>
        /// <param name="loteInfoDestino"></param>
        /// <param name="loteInfoOrigen"></param>
        /// <returns></returns>
        internal void ActualizaNoCabezasEnLote(LoteInfo loteInfoDestino, LoteInfo loteInfoOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaNoCabezasEnLote(loteInfoDestino, loteInfoOrigen);
                Update("Lote_ActualizarNoCabezasEnLote", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns></returns>
        internal void ActualizaCabezasEnLoteProductivo(LoteInfo loteInfoDestino, LoteInfo loteInfoOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaNoCabezasEnLote(loteInfoDestino, loteInfoOrigen);
                Update("Lote_ActualizarCabezasEnLoteProductivo", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns></returns>
        internal void ActualizarLoteALote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaLoteALote(loteInfo);
                Update("Lote_ActualizarLoteEnLote", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Se actualiza el campo activo en la tabla lote
        /// </summary>
        /// <param name="lote"></param>
        internal void ActualizaActivoEnLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaActivoALote(lote);
                Update("Lote_ActualizarActivoEnLote", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal void ActualizarFechaSalidaEnLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizarFechaSalidaEnLote(lote);
                Update("Lote_ActualizarFechaSalidaEnLote", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns>CheckListCorralInfo</returns>
        internal List<CheckListCorralInfo> ObtenerCheckListCorral(FiltroCierreCorral filtroCierreCorral)
        {
            List<CheckListCorralInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosCheckListCorral(filtroCierreCorral);
                DataSet ds = Retrieve("Lote_ObtenerInformacionCheckList", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerCheckListCorral(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene la información completa del Lote para Check List
        /// </summary>
        /// <returns>CheckListCorralInfo</returns>
        internal CheckListCorralInfo ObtenerCheckListCorralCompleto(FiltroCierreCorral filtroCierreCorral)
        {
            CheckListCorralInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosCheckListCorralCompleto(filtroCierreCorral);
                DataSet ds = Retrieve("Lote_ObtenerCheckListCompleto", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerCheckListCorralCompleto(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene la información completa del Lote para Check List
        /// </summary>
        /// <returns>CheckListCorralInfo</returns>
        internal void ActualizarFechaCerrado(FiltroCierreCorral filtroCierreCorral)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaFechaCierre(filtroCierreCorral);
                Update("Lote_ActualizarFechaCerrado", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal List<DisponibilidadLoteInfo> ObtenerLotesPorDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            List<DisponibilidadLoteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosPorDisponibilidad(filtroDisponilidadInfo);
                DataSet ds = Retrieve("Lote_ObtenerPorDisponibilidad", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorDisponibilidad(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene los Lotes por su Disponibilidad
        /// </summary>
        /// <param name="filtroDisponilidadInfo"></param>
        /// <returns></returns>
        internal void ActualizarLoteDisponibilidad(FiltroDisponilidadInfo filtroDisponilidadInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizarLoteDisponibilidad(filtroDisponilidadInfo);
                Update("Lote_ActualizarDisponibilidad", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal LoteInfo ObtenerLotesActivos(int organizacionId, int corralId)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerLotesActivos(organizacionId, corralId);
                DataSet ds = Retrieve("Lote_ObtenerLotesActivos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorIDOrganizacionID(ds);
                }

                // totalActivos = RetrieveValue<int>("Lote_ObtenerLotesActivos", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal LoteInfo ObtenerPorCorralID(LoteInfo loteInfo)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosObtenerPorCorralID(loteInfo);
                DataSet ds = Retrieve("Lote_ObtenerPorCorralID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal void ActualizarSalidaEnfermeria(AnimalMovimientoInfo resultadoLoteOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizarSalidaEnfermeria(resultadoLoteOrigen);
                Update("CorteTransferenciaGanado_ActualizarSalidaEnfermeria", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal void EliminarSalidaEnfermeria(AnimalMovimientoInfo loteCorralOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosEliminarSalidaEnfermeria(loteCorralOrigen);
                Update("CorteTransferenciaGanado_EliminarPorLoteSalidaEnfermeria", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Se obtienen los Lotes de una corraleta
        /// </summary>
        /// <param name="corraleta"></param>
        /// <returns></returns>
        internal IList<LoteInfo> ObtenerLoteDeCorraleta(CorralInfo corraleta)
        {
            IList<LoteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosPorCarrelata(corraleta);
                DataSet ds = Retrieve("Lote_ObtenerLotesPorCorraleta", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorCorraleta(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        internal void ActualizaNoCabezasEnLoteOrigen(LoteInfo resultadoLoteOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizaNoCabezasEnLoteOrigen(resultadoLoteOrigen);
                Update("Lote_ActualizarNoCabezasEnLoteOrigen", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener el animal salida por codigo
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal AnimalSalidaInfo ObtenerAnimalSalidaPorCodigo(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCorralDAL.ObtenerParametrosObtenerAnimalSalidaPorCodigo(corralInfo);
                var ds = Retrieve("SalidaRecuperacion_ObtenerAnimalInfo", parameters);
                AnimalSalidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalSalidaInfo(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener si existe el lote en animal salida
        /// </summary>
        /// <param name="loteOrigen"></param>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal AnimalSalidaInfo ExisteLoteAnimalSalida(LoteInfo loteOrigen, CorralInfo corralInfo)
        {

            try
            {

                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosExisteLoteAnimalSalida(loteOrigen, corralInfo);
                var ds = Retrieve("SalidaRecuperacion_ExisteLoteAnimalSalida", parameters);

                AnimalSalidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalSalidaInfo(ds);
                }
                return result;

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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

                var lotes = string.Join("|", lotesID.ToArray());

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@lotesID", lotes);
                var ds = Retrieve("Lotes_ObtenerLotesDescripcionPorIDs", parameters);

                List<LoteInfo> result = new List<LoteInfo>();
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLotesDescripcionPorIDs(ds);
                }
                return result;

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Organizacion", lote.OrganizacionID);
                parameters.Add("@LoteID", lote.LoteID);
                parameters.Add("@Descripcion", lote.Lote);
                parameters.Add("@Activo", true);
                parameters.Add("@Inicio", paginacion.Inicio);
                parameters.Add("@Limite", paginacion.Limite);
                var ds = Retrieve("Lote_ObtenerPorPagina", parameters);

                Base.Infos.ResultadoInfo<LoteInfo> lista = new ResultadoInfo<LoteInfo>();
                if (ValidateDataSet(ds))
                {
                    lista = MapLoteDAL.ObtenerPorPagina(paginacion, ds);
                }
                return lista;

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            IList<LoteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosPorOrganizacionLoteXML(organizacionId, corrales);
                DataSet ds = Retrieve("Lote_ObtenerPorOrganizacionLoteXML", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorOrganizacionLoteXML(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene Corrales Cerrados por XML
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal IList<LoteInfo> ObtenerPorCorralCerradoXML(int organizacionId, List<CorralInfo> corrales)
        {
            IList<LoteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosPorOrganizacionCorralCerradoXML(organizacionId, corrales);
                DataSet ds = Retrieve("Lote_ObtenerPorOrganizacionCerradoXML", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorOrganizacionCorralCerradoXML(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene los datos para generar el Reparto 
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal LoteDescargaDataLinkModel ObtenerLoteDataLink(int organizacionId, int loteID)
        {
            LoteDescargaDataLinkModel result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosPorLoteDataLink(organizacionId, loteID);
                DataSet ds = Retrieve("Lote_ObtenerDatosDescargaDataLink", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLoteDataLink(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Actualiza el lote de un corral
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuario"></param>
        internal LoteInfo ActualizarCorral(LoteInfo lote, CorralInfo corralInfoDestino, UsuarioInfo usuario)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ActualizarCorral(lote, corralInfoDestino, usuario);
                DataSet ds = Retrieve("Lote_ActualizarCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene los Lotes activos con Proyeccion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<DisponibilidadLoteInfo> ObtenerLotesActivosConProyeccion(int organizacionID)
        {
            List<DisponibilidadLoteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosLotesActivosConProyeccion(organizacionID);
                DataSet ds = Retrieve("LoteProyeccion_ObtenerLotesProyeccionReimplante", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLotesActivosConProyeccion(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene el lote anterior en donde estuvo el ganado
        /// </summary>
        /// <param name="loteID">Id del lote donde se encuentra actualmente el ganado</param>
        internal LoteInfo ObtenerLoteAnteriorAnimal(int loteID)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerLoteAnteriorAnimal(loteID);
                DataSet ds = Retrieve("Lote_ObtenerLoteAnteriorGanado", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLoteAnteriorAnimal(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene los Dias de grano de los lotes
        /// </summary>
        /// <param name="lote">Id del lote para buscar sus dias de gran</param>
        internal List<DiasEngordaGranoModel> ObtenerDiasEngordaGrano(LoteInfo lote)
        {
            List<DiasEngordaGranoModel> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerDiasEngordaGrano(lote);
                DataSet ds = Retrieve("ObtenerDiasGranoSacrificio", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerDiasEngordaGrano(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Este metodo obtiene el tipo de ganado de un loteen base al 50 + 1, es decir
        /// obtiene todos los tipos de gadados de un lote y toma el que tiene mayor numero de animales
        /// </summary>
        /// <param name="loteCorral"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoLoteID(LoteInfo loteCorral)
        {
            TipoGanadoInfo result = null;
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object> { { "@LoteID", loteCorral.LoteID } };
                DataSet ds = Retrieve("ReimplanteGanado_ObtenerTipoGanadoDeLote", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerTipoGanadoInfo(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
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
                IMapBuilderContext<LoteInfo> mapeo = MapLoteDAL.ObtenerMapeoLote();
                IEnumerable<LoteInfo> corrales = GetDatabase().ExecuteSprocAccessor
                    <LoteInfo>(
                        "Lote_ObtenerPorCodigoCorralOrganizacion", mapeo.Build(),
                        new object[] { organizacionID, codigo });
                return corrales;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizarCabezasLoteXML(lotes);
                Update("Lote_ActualizarCabezasLoteXML", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosDesactivarLoteXML(lotesDesactivar);
                Update("Lote_DesactivarLoteXML", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            IList<LoteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosPorOrganizacionEstatus(organizacionId, estatus);
                DataSet ds = Retrieve("Lote_ObtenerPorOrganizacionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorOrganizacionEstatus(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
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
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosActualizarFechaZilmax(lotes);
                Update("Lote_ActualizarFechaZilmax", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                string lotesXML = AuxLoteDAL.ObtenerParametrosLoteXML(lotes);
                IMapBuilderContext<LoteInfo> mapeo = MapLoteDAL.ObtenerMapeoLote();
                IEnumerable<LoteInfo> corrales = GetDatabase().ExecuteSprocAccessor
                    <LoteInfo>(
                        "Lote_ObtenerPorLoteXML", mapeo.Build(),
                        new object[] { lotesXML });
                return corrales;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters 
                    = AuxLoteDAL.ObtenerParametrosActualizaCabezasEnLoteXML(lotesDestino, lotesOrigen);
                Update("Lote_ActualizarNoCabezasEnLoteXML", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosLotePorCodigoLote(lote);
                DataSet ds = Retrieve("Lote_ObtenerPorCodigoLote", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLotePorCodigo(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters =
                    AuxLoteDAL.ObtenerParametrosPesoCompraPorLote(lote);
                DataSet ds = Retrieve("Lote_ObtenerPesoOrigen", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPesoCompraPorLote(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            CabezasActualizadasInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxLoteDAL.ObtenerParametrosActualizarCabezasProcesadas(filtroActualizarCabezasLote);
                DataSet ds = Retrieve("Lote_ActualizarCabezasProcesadas", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerPorActualizarCabezasProcesadas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                var parameters = AuxLoteDAL.ObtenerParametrosEstatusPorLoteId(loteId);
                var ds = Retrieve("Lote_ConsultarEstatusPorLoteId", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerEstatusPorLoteId(ds);
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

        public LoteInfo ValidarCorralCompletoParaSacrificio(int loteId)
        {
            LoteInfo result = null;
            try
            {
                Logger.Info();
                var parameters = AuxLoteDAL.ObtenerParametrosValidarCorralCompletoParaSacrificio(loteId);
                var ds = Retrieve("Lote_ValidarCorralCompletoParaSacrificio", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerValidarCorralCompletoParaSacrificio(ds);
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

        public int ValidarCorralCompletoParaSacrificioScp(string fechaProduccion, string lote, string corral, int organizacionId)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var salidaSacrificioDal = new SalidaSacrificioDAL();
                string conexion = salidaSacrificioDal.ObtenerCadenaConexionSPI(organizacionId);
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("LayoutSubida_ObtenerCabezasPorLotePadre", connection)
                                      {CommandType = CommandType.StoredProcedure};
                    command.Parameters.Add("@Lote", SqlDbType.VarChar).Value = lote;
                    command.Parameters.Add("@Corral", SqlDbType.VarChar).Value = corral;
                    command.Parameters.Add("@FechaProduccion", SqlDbType.VarChar).Value = fechaProduccion;
                    var rdr = command.ExecuteReader();
                    if (rdr.Read())
                    {
                        result = Convert.ToInt32(rdr.GetValue(0));
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

        public List<AnimalInfo> ObtenerAretesCorralPorLoteId(int loteId)
        {
            var result = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var parameters = AuxLoteDAL.ObtenerParametrosObtenerAretesCorralPorLoteId(loteId);
                var ds = Retrieve("Lote_ObtenerAnimalesPorLoteID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerAretesCorralPorLoteId(ds);
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

        public List<LoteInfo> ObtenerLotesConAnimalesDisponiblesPorOrganizacionId(int organizacionId)
        {
            var result = new List<LoteInfo>();
            try
            {
                Logger.Info();
                var parameters = AuxLoteDAL.ObtenerParametrosLotesConAnimalesDisponibles(organizacionId);
                var ds = Retrieve("Lote_ConAnimalesObtenerPorOrganizacionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapLoteDAL.ObtenerLotesConAnimalesDisponibles(ds);
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

        public List<AnimalInfo> ObtenerLoteConAnimalesScp(int organizacionId, string lote, string corral, string fechaSacrificio)
        {
            var result = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var salidaSacrificioDal = new SalidaSacrificioDAL();
                string conexion = salidaSacrificioDal.ObtenerCadenaConexionSPI(organizacionId);
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("LayoutSubida_ObtenerAretes", connection) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@FechaProduccion", SqlDbType.VarChar).Value = fechaSacrificio;
                    command.Parameters.Add("@Corral", SqlDbType.VarChar).Value = corral;
                    command.Parameters.Add("@Lote", SqlDbType.VarChar).Value = lote;
                    var rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var animalInfo = new AnimalInfo
                                             {
                                                 Arete = rdr.GetValue(0).ToString(),
                                                 AnimalID = Convert.ToInt64(rdr.GetValue(1))
                                             };
                        result.Add(animalInfo);
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
 