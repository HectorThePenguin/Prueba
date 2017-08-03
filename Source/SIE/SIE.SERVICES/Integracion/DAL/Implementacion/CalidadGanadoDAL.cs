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
    internal class CalidadGanadoDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de CalidadGanado
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal void Crear(CalidadGanadoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCalidadGanadoDAL.ObtenerParametrosCrear(info);
                Create("CalidadGanado_Crear", parameters);
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
        ///     Metodo para Actualizar un nuevo registro de CalidadGanado
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CalidadGanadoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCalidadGanadoDAL.ObtenerParametrosActualizar(info);
                Update("CalidadGanado_Actualizar", parameters);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CalidadGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, CalidadGanadoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCalidadGanadoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[CalidadGanado_ObtenerPorPagina]", parameters);
                ResultadoInfo<CalidadGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadGanadoDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene una lista de CalidadGanado 
        /// </summary>
        /// <returns></returns>
        internal List<CalidadGanadoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CalidadGanado_ObtenerTodos");
                List<CalidadGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadGanadoDAL.ObtenerTodos(ds);
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
        ///     Obtiene una lista de CalidadGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<CalidadGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCalidadGanadoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CalidadGanado_ObtenerTodos", parameters);
                List<CalidadGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadGanadoDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de CalidadGanado
        /// </summary>
        /// <param name="calidadGanadoID">Identificador de la CalidadGanado</param>
        /// <returns></returns>
        internal CalidadGanadoInfo ObtenerPorID(int calidadGanadoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCalidadGanadoDAL.ObtenerParametrosPorID(calidadGanadoID);
                DataSet ds = Retrieve("CalidadGanado_ObtenerPorID", parameters);
                CalidadGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadGanadoDAL.ObtenerPorID(ds);
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
        /// Obtiene la Lista de Entrada Ganado Calidad
        /// </summary>
        /// <returns></returns>
        internal List<EntradaGanadoCalidadInfo> ObtenerListaCalidadGanado()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CalidadGanado_ObtenerTodos");
                List<EntradaGanadoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadGanadoDAL.ObtenerListaCalidadGanado(ds);
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
        /// Obtiene un registro de CalidadGanado
        /// </summary>
        /// <param name="descripcion">Descripción de la CalidadGanado</param>
        /// <param name="sexo"> </param>
        /// <returns></returns>
        internal CalidadGanadoInfo ObtenerPorDescripcion(string descripcion, Sexo sexo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCalidadGanadoDAL.ObtenerParametrosPorDescripcion(descripcion, sexo);
                DataSet ds = Retrieve("CalidadGanado_ObtenerPorDescripcion", parameters);
                CalidadGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadGanadoDAL.ObtenerPorDescripcion(ds);
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
