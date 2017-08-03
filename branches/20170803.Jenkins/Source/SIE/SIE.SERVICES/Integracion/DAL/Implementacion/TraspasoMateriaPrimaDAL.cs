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
    public class TraspasoMateriaPrimaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TraspasoMateriaPrima
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public TraspasoMateriaPrimaInfo Crear(TraspasoMateriaPrimaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerParametrosCrear(info);
                DataSet ds = Retrieve("TraspasoMateriaPrima_Crear", parameters);
                TraspasoMateriaPrimaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerPorCrear(ds);
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
        /// Metodo para actualizar un registro de TraspasoMateriaPrima
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(TraspasoMateriaPrimaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerParametrosActualizar(info);
                Update("TraspasoMateriaPrima_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TraspasoMpPaMedInfo> ObtenerPorPagina(PaginacionInfo pagina, FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TraspasoMateriaPrima_ObtenerPorPagina", parameters);
                ResultadoInfo<TraspasoMpPaMedInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TraspasoMateriaPrima
        /// </summary>
        /// <returns></returns>
        public IList<TraspasoMateriaPrimaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TraspasoMateriaPrima_ObtenerTodos");
                IList<TraspasoMateriaPrimaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TraspasoMateriaPrimaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TraspasoMateriaPrima_ObtenerTodos", parameters);
                IList<TraspasoMateriaPrimaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TraspasoMateriaPrima
        /// </summary>
        /// <param name="traspasoMateriaPrimaID">Identificador de la TraspasoMateriaPrima</param>
        /// <returns></returns>
        public TraspasoMateriaPrimaInfo ObtenerPorID(int traspasoMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerParametrosPorID(traspasoMateriaPrimaID);
                DataSet ds = Retrieve("TraspasoMateriaPrima_ObtenerPorID", parameters);
                TraspasoMateriaPrimaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TraspasoMateriaPrima
        /// </summary>
        /// <param name="descripcion">Descripción de la TraspasoMateriaPrima</param>
        /// <returns></returns>
        public TraspasoMateriaPrimaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TraspasoMateriaPrima_ObtenerPorDescripcion", parameters);
                TraspasoMateriaPrimaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un trapaso de materia prima por su folio
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public TraspasoMpPaMedInfo ObtenerPorFolioTraspaso(FiltroTraspasoMpPaMed filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTraspasoMateriaPrimaDAL.ObtenerParametrosPorFolio(filtro);
                DataSet ds = Retrieve("TraspasoMateriaPrima_ObtenerPorFolio", parameters);
                TraspasoMpPaMedInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTraspasoMateriaPrimaDAL.ObtenerPorFolio(ds);
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

