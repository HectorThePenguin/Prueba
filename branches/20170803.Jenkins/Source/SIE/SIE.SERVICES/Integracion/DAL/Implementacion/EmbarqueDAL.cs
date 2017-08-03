using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EmbarqueDAL : DALBase
    {
        /// <summary>
        /// Obtiene una lista de embarques pendiente de recibir 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaEmbarqueInfo> ObtenerEmbarquesPedientesPorPagina(PaginacionInfo pagina,
                                                                                  FiltroEmbarqueInfo filtro)
        {
            ResultadoInfo<EntradaEmbarqueInfo> listaEmbarque = null;
            try
            {
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametrosPendientesPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Embarque_ObtenerPendientesPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    listaEmbarque = MapEmbarqueDAL.ObtenerTodosEntradaEmbarque(ds);
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
            return listaEmbarque;
        }

        /// <summary>
        ///     Metodo que crear un Embarque
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametrosCrear(info);
                int infoId = Create("Embarque_Crear", parameters);
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
        ///     Metodo que actualiza un Embarque
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(EmbarqueInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametrosActualizar(info);
                Update("Embarque_Actualizar", parameters);
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
        ///     Obtiene un Embarque por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EmbarqueInfo ObtenerPorID(int filtro)
        {
            EmbarqueInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametroPorID(filtro);
                DataSet ds = Retrieve("Embarque_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapEmbarqueDAL.ObtenerPorID(ds);
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
        ///     Obtiene un Embarque por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal IList<EmbarqueDetalleInfo> ObtenerEscalasPorEmbarqueID(int filtro)
        {
            IList<EmbarqueDetalleInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametrosEscalasPorEmbarqueID(filtro);
                DataSet ds = Retrieve("EmbarqueDetalle_ObtenerPorEmbarqueID", parameters); 
                if (ValidateDataSet(ds))
                {
                    result = MapEmbarqueDAL.ObtenerEscalasPorEmbarqueID(ds);
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
        ///     Obtiene un Embarque por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EmbarqueInfo ObtenerPorFolioEmbarque(int filtro)
        {
            EmbarqueInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametroPorFolioEmbarque(filtro);
                DataSet ds = Retrieve("Embarque_ObtenerPorFolioEmbarque", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapEmbarqueDAL.ObtenerPorFolioEmbarque(ds);
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
        ///     Metodo para guardar la lista de escalas de la programación de embarques
        /// </summary>
        /// <param name="listaEscalas"></param>
        /// <param name="embarqueId"></param>
        internal void GuardarEscala(IEnumerable<EmbarqueDetalleInfo> listaEscalas, int embarqueId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametrosGuardadoProgramacionEscalas(listaEscalas, embarqueId);
                Create("Embarque_GuardarEscala", parameters);
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
        internal void GuardarCosto(IEnumerable<CostoEmbarqueDetalleInfo> listaCostos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametrosGuardadoProgramacionCostos(listaCostos);
                Create("Embarque_GuardarCosto", parameters);
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
        /// Obtiene un listado de entradas Activas Paginadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EmbarqueInfo> ObtenerPorPagina(PaginacionInfo pagina, FiltroEmbarqueInfo filtro)
        {
            ResultadoInfo<EmbarqueInfo> resultadoProgramacionInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEmbarqueDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Embarque_ObtenerPorPagina", parametros);
                if (ValidateDataSet(ds))
                {
                    resultadoProgramacionInfo = MapEmbarqueDAL.ObtenerPorPagina(ds);
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
            return resultadoProgramacionInfo;
        }

        /// <summary>
        /// Metodo que actualiza el estatus a recibido de la programacion de embarque (detalle) 
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal void ActualizarEstatusDetalle(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEmbarqueDAL.ObtenerParametrosActualizarEstatusDetalle(entradaGanado);
                Update("Embarque_ActualizarEstatusDetalle", parametros);
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
        /// Metodo que actualiza el estatus a recibido de la programacion de embarque (Cabecero) 
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstatus(EntradaGanadoInfo entradaGanado, Estatus estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEmbarqueDAL.ObtenerParametrosActualizarEstatus(entradaGanado, estatus);
                Update("Embarque_ActualizarEstatus", parametros);
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
        /// Metodo que Obtiene los embarques que estan pendientes de recibir 
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal int PendientesRecibir(EntradaGanadoInfo entradaGanado)
        {
            int totalPendientes;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEmbarqueDAL.ObtenerParametrosPendientesRecibir(entradaGanado);
                totalPendientes = RetrieveValue<int>("Embarque_ObtenerPendientesRecibir", parametros);
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
            return totalPendientes;
        }

        /// <summary>
        ///     Obtiene un Embarque por Folio Embarque y Organizacion
        /// </summary>
        /// <param name="folioEmbarqueId">Folio del embarque</param>
        /// <param name="organizacionId">Organización a la que pertenece el folio</param>
        /// <returns></returns>
        internal EmbarqueInfo ObtenerPorFolioEmbarqueOrganizacion(int folioEmbarqueId, int organizacionId)
        {
            EmbarqueInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmbarqueDAL.ObtenerParametroPorFolioEmbarqueOrganizacion(folioEmbarqueId, organizacionId);
                DataSet ds = Retrieve("Embarque_ObtenerPorFolioEmbarqueOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapEmbarqueDAL.ObtenerPorFolioEmbarqueOrganizacion(ds);
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
    }
}