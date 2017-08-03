using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PesajeMateriaPrimaDAL :DALBase
    {
        /// <summary>
        /// Metodo que crea una registro en PesajeMateriaPrima
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        internal PesajeMateriaPrimaInfo Crear(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            PesajeMateriaPrimaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPesajeMateriaPrimaDAL.ObtenerParametrosCrear(pesajeMateriaPrimaInfo);
                DataSet ds = Retrieve("PesajeMateriaPrima_Crear", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPesajeMateriaPrimaDAL.ObtenerPorId(ds);
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
        /// Obtiene un registro por ticket y pedido
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <returns></returns>
        internal PesajeMateriaPrimaInfo ObtenerPorTicketPedido(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerPorTicketPedido(pesajeMateriaPrimaInfo);
                var ds = Retrieve("PesajeMateriaPrima_ObtenerPorTicketPedido", parameters);
                PesajeMateriaPrimaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPesajeMateriaPrimaDAL.ObtenerPorTicket(ds);
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
        /// Obtiene los pesajes de materia prima por programacion
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <returns></returns>
        internal List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionMateriaPrimaId(int programacionMateriaPrimaId)
        {
            List<PesajeMateriaPrimaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerPorProgramacionMateriaPrimaId(programacionMateriaPrimaId);
                DataSet ds = Retrieve("PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaId", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPesajeMateriaPrimaDAL.ObtenerPesajesPorProgramacionMateriaPrimaId(ds);
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
        /// Obtiene los pesajes de materia prima por programacion
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <returns></returns>
        internal List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionMateriaPrimaIdSinActivo(int programacionMateriaPrimaId)
        {
            List<PesajeMateriaPrimaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerPorProgramacionMateriaPrimaId(programacionMateriaPrimaId);
                DataSet ds = Retrieve("PesajeMateriaPrima_ObtenerPorProgramacionMateriaPrimaIdSinActivo", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPesajeMateriaPrimaDAL.ObtenerPesajesPorProgramacionMateriaPrimaId(ds);
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
        /// Actualiza todos los campos del pesaje ( se consulta primero en base al Id y se sobre Escribe)
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        internal void ActualizarPesajePorId(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerActualizarPesajePorId(pesajeMateriaPrimaInfo);
                Update("PesajeMateriaPrima_ActualizarPesajePorId", parameters);
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
        /// Obtiene los valores para generar la poliza
        /// de Pase a Proceso
        /// </summary>
        internal List<PolizaPaseProcesoModel> ObtenerValoresPolizaPaseProceso(int folioPedido, int organizacionID, string xmlLote)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerValoresPolizaPaseProceso(folioPedido, organizacionID, xmlLote);
                DataSet ds = Retrieve("PaseProceso_ObtenerDatosPoliza", parameters);
                List<PolizaPaseProcesoModel> polizasPaseProceso = null;
                if (ValidateDataSet(ds))
                {
                    polizasPaseProceso = MapPesajeMateriaPrimaDAL.ObtenerValoresPolizaPaseProceso(ds);
                }
                return polizasPaseProceso;
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
        /// Obtiene los datos de pase a proceso para 
        /// su reimpresion
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<PolizaPaseProcesoModel> ObtenerValoresPolizaPaseProcesoReimpresion(int folioPedido, int organizacionId)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerValoresPolizaPaseProcesoReimpresion(folioPedido,
                                                                                                         organizacionId);
                DataSet ds = Retrieve("PaseProceso_ObtenerDatosPolizaReimpresion", parameters);
                List<PolizaPaseProcesoModel> polizasPaseProceso = null;
                if (ValidateDataSet(ds))
                {
                    polizasPaseProceso = MapPesajeMateriaPrimaDAL.ObtenerValoresPolizaPaseProcesoReimpresion(ds);
                }
                return polizasPaseProceso;
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
        /// Obtiene el pesaje de materia prima por id
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <returns></returns>
        internal PesajeMateriaPrimaInfo ObtenerPorId(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            PesajeMateriaPrimaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPesajeMateriaPrimaDAL.ObtenerParametrosObtenerPorId(pesajeMateriaPrimaInfo);
                DataSet ds = Retrieve("PesajeMateriaPrima_ObtenerPorId", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPesajeMateriaPrimaDAL.ObtenerPorId(ds);
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
        /// Obtiene los Folios y ticket de los pase a proceso
        /// </summary>
        /// <param name="movimientos"></param>
        /// <returns></returns>
        internal List<ReporteInventarioPaseProcesoModel> ObtenerFoliosPaseProceso(List<long> movimientos)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxPesajeMateriaPrimaDAL.ObtenerFoliosPaseProceso(movimientos);
                DataSet ds = Retrieve("PesajeMateriaPrima_ObtenerPorAlmacenMovimiento", parameters);
                List<ReporteInventarioPaseProcesoModel> foliosPaseProceso = null;
                if (ValidateDataSet(ds))
                {
                    foliosPaseProceso = MapPesajeMateriaPrimaDAL.ObtenerFoliosPaseProceso(ds);
                }
                return foliosPaseProceso;
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
    }
}
