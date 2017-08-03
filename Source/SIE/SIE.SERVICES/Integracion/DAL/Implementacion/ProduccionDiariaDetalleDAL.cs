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
    public class ProduccionDiariaDetalleDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ProduccionDiariaDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(ProduccionDiariaDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerParametrosCrear(info);
                int result = Create("ProduccionDiariaDetalle_Crear", parameters);
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
        /// Metodo para actualizar un registro de ProduccionDiariaDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(ProduccionDiariaDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerParametrosActualizar(info);
                Update("ProduccionDiariaDetalle_Actualizar", parameters);
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
        public ResultadoInfo<ProduccionDiariaDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionDiariaDetalleInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ProduccionDiariaDetalle_ObtenerPorPagina", parameters);
                ResultadoInfo<ProduccionDiariaDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDetalleDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ProduccionDiariaDetalle
        /// </summary>
        /// <returns></returns>
        public IList<ProduccionDiariaDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ProduccionDiariaDetalle_ObtenerTodos");
                IList<ProduccionDiariaDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDetalleDAL.ObtenerTodos(ds);
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
        public IList<ProduccionDiariaDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ProduccionDiariaDetalle_ObtenerTodos", parameters);
                IList<ProduccionDiariaDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDetalleDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ProduccionDiariaDetalle
        /// </summary>
        /// <param name="produccionDiariaDetalleID">Identificador de la ProduccionDiariaDetalle</param>
        /// <returns></returns>
        public ProduccionDiariaDetalleInfo ObtenerPorID(int produccionDiariaDetalleID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerParametrosPorID(produccionDiariaDetalleID);
                DataSet ds = Retrieve("ProduccionDiariaDetalle_ObtenerPorID", parameters);
                ProduccionDiariaDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDetalleDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ProduccionDiariaDetalle
        /// </summary>
        /// <param name="descripcion">Descripción de la ProduccionDiariaDetalle</param>
        /// <returns></returns>
        public ProduccionDiariaDetalleInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ProduccionDiariaDetalle_ObtenerPorDescripcion", parameters);
                ProduccionDiariaDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionDiariaDetalleDAL.ObtenerPorDescripcion(ds);
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
        /// Metodo para Crear un registro de ProduccionDiariaDetalle
        /// </summary>
        /// <param name="produccionDiaria">valores para armar el XML</param>
        /// <param name="produccionDiariaID">ID de Produccion Diaria</param>
        public int GuardarProduccionDiariaDetalle(ProduccionDiariaInfo produccionDiaria, int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionDiariaDetalleDAL.ObtenerGuardarProduccionDiariaDetalle(produccionDiaria, produccionDiariaID);
                int result = Create("ProduccionDiaria_GuardarProduccionDiariaDetalle", parameters);
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

