using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaGanadoCosteoDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de EntradaGanadoCosteo
        /// </summary>
        /// <param name="info">Información que será guardada</param>
        internal int Crear(EntradaGanadoCosteoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosCrear(info);
                int infoId = Create("EntradaGanadoCosteo_Crear", parameters);
                return infoId;
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
        ///     Metodo para guardar la lista de costos de la programación de embarques
        /// </summary>
        /// <param name="listaCostos"></param>
        /// <param name="entradaGanadoCosteoId"> </param>
        internal void GuardarEntradaDetalle(IEnumerable<EntradaDetalleInfo> listaCostos, int entradaGanadoCosteoId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosGuardadoEntradaDetalle(listaCostos, entradaGanadoCosteoId);
                Create("EntradaGanadoCosteo_GuardarEntradaDetalle", parameters);
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
        ///     Metodo para guardar la lista de costos de la programación de embarques
        /// </summary>
        /// <param name="listaCalidadesGanado"></param>
        internal void GuardarCalidadGanado(IEnumerable<EntradaGanadoCalidadInfo> listaCalidadesGanado)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosGuardadoCalidadGanado(listaCalidadesGanado);
                Create("EntradaGanadoCosteo_GuardarCalidadGanado", parameters);
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
        ///     Metodo para guardar la lista de costos de la programación de embarques
        /// </summary>
        /// <param name="listaCostosEntrada"></param>
        /// <param name="entradaGanadoCosteoId"> </param>
        internal void GuardarCostoEntrada(IEnumerable<EntradaGanadoCostoInfo> listaCostosEntrada, int entradaGanadoCosteoId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosGuardadoCostoEntrada(listaCostosEntrada, entradaGanadoCosteoId);
                Create("EntradaGanadoCosteo_GuardarCostoEntrada", parameters);
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
        ///     Metodo para Actualizar un nuevo registro de EntradaGanadoCosteo
        /// </summary>
        /// <param name="info">Información que será actualizada</param>
        internal void Actualizar(EntradaGanadoCosteoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosActualizar(info);
                Update("EntradaGanadoCosteo_Actualizar", parameters);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su Id
        /// </summary>
        /// <param name="entradaGanadoCosteoID">Identificador de la Entrada Ganado Costeo</param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorID(int entradaGanadoCosteoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosPorID(entradaGanadoCosteoID);
                DataSet ds = Retrieve("[dbo].[EntradaGanadoCosteo_ObtenerPorID]", parameters);
                EntradaGanadoCosteoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCosteoDAL.ObtenerPorID(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <param name="entradaId">Identificador de la entrada</param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorEntradaGanadoID(int entradaId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosPorEntradaGanadoID(entradaId);
                DataSet ds = Retrieve("EntradaGanadoCosteo_ObtenerPorEntradaGanadoID", parameters);
                EntradaGanadoCosteoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCosteoDAL.ObtenerPorEntradaGanadoID(ds);
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
        /// Marcar el costeo de la entrada de ganado como prorrateado
        /// </summary>
        /// <param name="entradaGanadoCosteoId"></param>
        /// <returns></returns>
        public void InactivarProrrateoaCosteo(int entradaGanadoCosteoId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCosteoDAL.ObtenerParametrosInactivarProrrateoaCosteo(entradaGanadoCosteoId);
                Update("EntradaGanadoCosteo_ActualizarProrrateo", parameters);
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
        /// Obtiene una lista de entradas ganado costeo
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<EntradaGanadoCosteoInfo> ObtenerEntradasPorFechasConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaGanadoCosteoDAL.ObtenerParametrosPorFechasConciliacion(organizacionID, fechaInicial,
                                                                                     fechaFinal);
                DataSet ds = Retrieve("EntradaGanadoCosteo_ObtenerPorFechaConciliacion", parameters);
                List<EntradaGanadoCosteoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCosteoDAL.ObtenerPorFechasConciliacion(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <param name="entradaId">Identificador de la entrada</param>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorEntradaGanadoID(int entradaId, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaGanadoCosteoDAL.ObtenerParametrosPorEntradaGanadoID(entradaId, estatus);
                DataSet ds = Retrieve("EntradaGanadoCosteo_ObtenerPorEntradaGanadoID", parameters);
                EntradaGanadoCosteoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCosteoDAL.ObtenerPorEntradaGanadoID(ds);
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
    }
}
