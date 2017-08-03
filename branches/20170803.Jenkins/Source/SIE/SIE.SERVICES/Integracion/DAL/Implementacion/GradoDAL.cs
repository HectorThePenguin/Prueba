using System;
using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class GradoDAL : DALBase
    {

        internal IList<GradoInfo> ObtenerGrados()
        {
            try
            {
                DataSet ds = Retrieve("DeteccionGanado_ConsultaGrado");
                IList<GradoInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapGradoDAL.ObtenerGrado(ds);
                }
                return lista;
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
        /// Metodo para Crear un registro de Grado
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(GradoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGradoDAL.ObtenerParametrosCrear(info);
                int result = Create("Grado_Crear", parameters);
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
        /// Metodo para actualizar un registro de Grado
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(GradoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGradoDAL.ObtenerParametrosActualizar(info);
                Update("Grado_Actualizar", parameters);
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
        public ResultadoInfo<GradoInfo> ObtenerPorPagina(PaginacionInfo pagina, GradoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxGradoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Grado_ObtenerPorPagina", parameters);
                ResultadoInfo<GradoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGradoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Grado
        /// </summary>
        /// <returns></returns>
        public IList<GradoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Grado_ObtenerTodos");
                IList<GradoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGradoDAL.ObtenerTodos(ds);
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
        public IList<GradoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGradoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Grado_ObtenerTodos", parameters);
                IList<GradoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGradoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Grado
        /// </summary>
        /// <param name="gradoID">Identificador de la Grado</param>
        /// <returns></returns>
        public GradoInfo ObtenerPorID(int gradoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGradoDAL.ObtenerParametrosPorID(gradoID);
                DataSet ds = Retrieve("Grado_ObtenerPorID", parameters);
                GradoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGradoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Grado
        /// </summary>
        /// <param name="descripcion">Descripción de la Grado</param>
        /// <returns></returns>
        public GradoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxGradoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Grado_ObtenerPorDescripcion", parameters);
                GradoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGradoDAL.ObtenerPorDescripcion(ds);
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
