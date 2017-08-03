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
    public class ProveedorRetencionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ProveedorRetencion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(ProveedorRetencionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerParametrosCrear(info);
                int result = Create("ProveedorRetencion_Crear", parameters);
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
        /// Metodo para actualizar un registro de ProveedorRetencion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(ProveedorRetencionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerParametrosActualizar(info);
                Update("ProveedorRetencion_Actualizar", parameters);
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
        public ResultadoInfo<ProveedorRetencionInfo> ObtenerPorPagina(PaginacionInfo pagina, ProveedorRetencionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ProveedorRetencion_ObtenerPorPagina", parameters);
                ResultadoInfo<ProveedorRetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorRetencionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ProveedorRetencion
        /// </summary>
        /// <returns></returns>
        public IList<ProveedorRetencionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ProveedorRetencion_ObtenerTodos");
                IList<ProveedorRetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorRetencionDAL.ObtenerTodos(ds);
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
        public IList<ProveedorRetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ProveedorRetencion_ObtenerTodos", parameters);
                IList<ProveedorRetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorRetencionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ProveedorRetencion
        /// </summary>
        /// <param name="proveedorRetencionID">Identificador de la ProveedorRetencion</param>
        /// <returns></returns>
        public ProveedorRetencionInfo ObtenerPorID(int proveedorRetencionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerParametrosPorID(proveedorRetencionID);
                DataSet ds = Retrieve("ProveedorRetencion_ObtenerPorID", parameters);
                ProveedorRetencionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorRetencionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ProveedorRetencion
        /// </summary>
        /// <param name="descripcion">Descripción de la ProveedorRetencion</param>
        /// <returns></returns>
        public ProveedorRetencionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ProveedorRetencion_ObtenerPorDescripcion", parameters);
                ProveedorRetencionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorRetencionDAL.ObtenerPorDescripcion(ds);
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
        /// Guarda a los proveedores y sus choferes
        /// </summary>
        /// <param name="listaRetenciones">Lista que contiene al proveedor y al chofer</param>
        /// <returns></returns>
        internal void GuardarLista(List<ProveedorRetencionInfo> listaRetenciones)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProveedorRetencionDAL.ObtenerParametrosGuardarLista(listaRetenciones);
                Create("ProveedorRetencion_GuardarLista", parameters);
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
        /// Obtiene una lista de las retenciones configuradas para el proveedor
        /// </summary> 
        /// <param name="proveedorID">Representa el ID del Proveedor </param>
        /// <returns></returns>
        internal IList<ProveedorRetencionInfo> ObtenerPorProveedorID(int proveedorID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProveedorRetencionDAL.ObtenerPorProveedorID(proveedorID);
                DataSet ds = Retrieve("ProveedorRetencion_ObtenerPorProveedorID", parameters);
                IList<ProveedorRetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProveedorRetencionDAL.ObtenerPorProveedorID(ds);
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

