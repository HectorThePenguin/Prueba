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
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ProduccionDiariaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ProduccionDiaria
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(ProduccionDiariaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDAL.ObtenerParametrosCrear(info);
                int result = Create("ProduccionDiaria_Crear", parameters);
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
        /// Metodo para actualizar un registro de ProduccionDiaria
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(ProduccionDiariaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDAL.ObtenerParametrosActualizar(info);
                Update("ProduccionDiaria_Actualizar", parameters);
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
        public ResultadoInfo<ProduccionDiariaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionDiariaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProduccionDiariaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ProduccionDiaria_ObtenerPorPagina", parameters);
                ResultadoInfo<ProduccionDiariaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ProduccionDiaria
        /// </summary>
        /// <returns></returns>
        public IList<ProduccionDiariaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ProduccionDiaria_ObtenerTodos");
                IList<ProduccionDiariaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDAL.ObtenerTodos(ds);
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
        public IList<ProduccionDiariaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ProduccionDiaria_ObtenerTodos", parameters);
                IList<ProduccionDiariaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ProduccionDiaria
        /// </summary>
        /// <param name="produccionDiariaID">Identificador de la ProduccionDiaria</param>
        /// <returns></returns>
        public ProduccionDiariaInfo ObtenerPorID(int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDAL.ObtenerParametrosPorID(produccionDiariaID);
                DataSet ds = Retrieve("ProduccionDiaria_ObtenerPorID", parameters);
                ProduccionDiariaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ProduccionDiaria
        /// </summary>
        /// <param name="descripcion">Descripción de la ProduccionDiaria</param>
        /// <returns></returns>
        public ProduccionDiariaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ProduccionDiaria_ObtenerPorDescripcion", parameters);
                ProduccionDiariaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDAL.ObtenerPorDescripcion(ds);
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

