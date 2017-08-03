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
    public class DescripcionGanadoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de DescripcionGanado
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(DescripcionGanadoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDescripcionGanadoDAL.ObtenerParametrosCrear(info);
                int result = Create("DescripcionGanado_Crear", parameters);
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
        /// Metodo para actualizar un registro de DescripcionGanado
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(DescripcionGanadoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDescripcionGanadoDAL.ObtenerParametrosActualizar(info);
                Update("DescripcionGanado_Actualizar", parameters);
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
        public ResultadoInfo<DescripcionGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, DescripcionGanadoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxDescripcionGanadoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("DescripcionGanado_ObtenerPorPagina", parameters);
                ResultadoInfo<DescripcionGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDescripcionGanadoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de DescripcionGanado
        /// </summary>
        /// <returns></returns>
        public IList<DescripcionGanadoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("DescripcionGanado_ObtenerTodos");
                IList<DescripcionGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDescripcionGanadoDAL.ObtenerTodos(ds);
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
        public IList<DescripcionGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDescripcionGanadoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("DescripcionGanado_ObtenerTodos", parameters);
                IList<DescripcionGanadoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDescripcionGanadoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de DescripcionGanado
        /// </summary>
        /// <param name="descripcionGanadoID">Identificador de la DescripcionGanado</param>
        /// <returns></returns>
        public DescripcionGanadoInfo ObtenerPorID(int descripcionGanadoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDescripcionGanadoDAL.ObtenerParametrosPorID(descripcionGanadoID);
                DataSet ds = Retrieve("DescripcionGanado_ObtenerPorID", parameters);
                DescripcionGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDescripcionGanadoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de DescripcionGanado
        /// </summary>
        /// <param name="descripcion">Descripción de la DescripcionGanado</param>
        /// <returns></returns>
        public DescripcionGanadoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDescripcionGanadoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("DescripcionGanado_ObtenerPorDescripcion", parameters);
                DescripcionGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDescripcionGanadoDAL.ObtenerPorDescripcion(ds);
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

