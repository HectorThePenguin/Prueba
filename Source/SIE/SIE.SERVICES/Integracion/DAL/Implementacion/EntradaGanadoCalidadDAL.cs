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
    internal class EntradaGanadoCalidadDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de EntradaGanadoCalidad
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(EntradaGanadoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCalidadDAL.ObtenerParametrosCrear(info);
                int result = Create("EntradaGanadoCalidad_Crear", parameters);
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
        /// Metodo para actualizar un registro de EntradaGanadoCalidad
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(EntradaGanadoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCalidadDAL.ObtenerParametrosActualizar(info);
                Update("EntradaGanadoCalidad_Actualizar", parameters);
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
        internal ResultadoInfo<EntradaGanadoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoCalidadInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxEntradaGanadoCalidadDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("EntradaGanadoCalidad_ObtenerPorPagina", parameters);
                ResultadoInfo<EntradaGanadoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCalidadDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de EntradaGanadoCalidad
        /// </summary>
        /// <returns></returns>
        internal IList<EntradaGanadoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("EntradaGanadoCalidad_ObtenerTodos");
                IList<EntradaGanadoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCalidadDAL.ObtenerTodos(ds);
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
        internal IList<EntradaGanadoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCalidadDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("EntradaGanadoCalidad_ObtenerTodos", parameters);
                IList<EntradaGanadoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCalidadDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de EntradaGanadoCalidad
        /// </summary>
        /// <param name="entradaGanadoCalidadID">Identificador de la EntradaGanadoCalidad</param>
        /// <returns></returns>
        internal EntradaGanadoCalidadInfo ObtenerPorID(int entradaGanadoCalidadID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCalidadDAL.ObtenerParametrosPorID(entradaGanadoCalidadID);
                DataSet ds = Retrieve("EntradaGanadoCalidad_ObtenerPorID", parameters);
                EntradaGanadoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCalidadDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de EntradaGanadoCalidad
        /// </summary>
        /// <param name="descripcion">Descripción de la EntradaGanadoCalidad</param>
        /// <returns></returns>
        internal EntradaGanadoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEntradaGanadoCalidadDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("EntradaGanadoCalidad_ObtenerPorDescripcion", parameters);
                EntradaGanadoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCalidadDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una lista filtrando por la Entrada Ganado Id
        /// </summary>
        /// <returns></returns>
        internal List<EntradaGanadoCalidadInfo> ObtenerPorEntradaGanadoId(int entradaGanadoId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaGanadoCalidadDAL.ObtenerParametrosPorEntradaGanadoID(entradaGanadoId);
                DataSet ds = Retrieve("EntradaGanadoCalidad_ObtenerPorEntradaGanadoId", parameters);
                List<EntradaGanadoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoCalidadDAL.ObtenerPorEntradaGanadoID(ds);
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

