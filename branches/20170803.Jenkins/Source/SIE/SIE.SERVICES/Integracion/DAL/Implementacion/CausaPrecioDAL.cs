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
    internal class CausaPrecioDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CausaPrecio
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CausaPrecioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosCrear(info);
                int result = Create("CausaPrecio_Crear", parameters);
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
        /// Metodo para actualizar un registro de CausaPrecio
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CausaPrecioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosActualizar(info);
                Update("CausaPrecio_Actualizar", parameters);
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
        internal ResultadoInfo<CausaPrecioInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaPrecioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CausaPrecio_ObtenerPorPagina", parameters);
                ResultadoInfo<CausaPrecioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CausaPrecio
        /// </summary>
        /// <returns></returns>
        internal IList<CausaPrecioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CausaPrecio_ObtenerTodos");
                IList<CausaPrecioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerTodos(ds);
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
        internal IList<CausaPrecioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CausaPrecio_ObtenerTodos", parameters);
                IList<CausaPrecioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CausaPrecio
        /// </summary>
        /// <param name="causaPrecioID">Identificador de la CausaPrecio</param>
        /// <returns></returns>
        internal CausaPrecioInfo ObtenerPorID(int causaPrecioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosPorID(causaPrecioID);
                DataSet ds = Retrieve("CausaPrecio_ObtenerPorID", parameters);
                CausaPrecioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CausaPrecio
        /// </summary>
        /// <param name="descripcion">Descripción de la CausaPrecio</param>
        /// <returns></returns>
        internal CausaPrecioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CausaPrecio_ObtenerPorDescripcion", parameters);
                CausaPrecioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CausaPrecioInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, CausaPrecioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosPorFiltroPagina(pagina, filtro);
                DataSet ds = Retrieve("CausaPrecio_ObtenerPorFiltrosPagina", parameters);
                ResultadoInfo<CausaPrecioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerPorFiltroPagina(ds);
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
        /// Obtiene una instancia de CausaPrecioInfo
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CausaPrecioInfo ObtenerPorCausaSalidaPrecioGanado(CausaPrecioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerParametrosPorCausaSalidaPrecioGanado(filtro);
                DataSet ds = Retrieve("CausaPrecio_ObtenerPorCausaSalidaPrecioGanado", parameters);
                CausaPrecioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerPorCausaSalidaPrecioGanado(ds);
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

        internal List<CausaPrecioInfo> ObtenerPorPrecioPorCausaSalida(int causa, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCausaPrecioDAL.ObtenerPorPrecioPorCausaSalida(causa, organizacionID);
                DataSet ds = Retrieve("CausaPrecio_ObtenerPorCausaSalida", parameters);
                List<CausaPrecioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCausaPrecioDAL.ObtenerPorPrecioPorCausaSalida(ds);
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

