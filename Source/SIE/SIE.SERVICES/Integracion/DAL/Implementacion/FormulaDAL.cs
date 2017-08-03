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
    internal class FormulaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Formula
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(FormulaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerParametrosCrear(info);
                int result = Create("Formula_Crear", parameters);
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
        /// Metodo para actualizar un registro de Formula
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(FormulaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerParametrosActualizar(info);
                Update("Formula_Actualizar", parameters);
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
        internal ResultadoInfo<FormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, FormulaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Formula_ObtenerPorPagina", parameters);
                ResultadoInfo<FormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Formula
        /// </summary>
        /// <returns></returns>
        internal IList<FormulaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Formula_ObtenerTodos");
                IList<FormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerTodos(ds);
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
        internal IList<FormulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerTodos(estatus);
                using(IDataReader reader = RetrieveReader("Formula_ObtenerTodos", parameters))
                {
                    IList<FormulaInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapFormulaDAL.ObtenerTodos(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;                    
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
        }

        /// <summary>
        /// Obtiene un registro de Formula
        /// </summary>
        /// <param name="formulaID">Identificador de la Formula</param>
        /// <returns></returns>
        internal FormulaInfo ObtenerPorID(int formulaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerParametrosPorID(formulaID);
                DataSet ds = Retrieve("Formula_ObtenerPorID", parameters);
                FormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Formula
        /// </summary>
        /// <param name="descripcion">Descripción de la Formula</param>
        /// <returns></returns>
        internal FormulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Formula_ObtenerPorDescripcion", parameters);
                FormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerPorDescripcion(ds);
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
        /// 
        /// </summary>
        /// <param name="tipoFormulaId"></param>
        /// <returns></returns>
        internal IList<FormulaInfo> ObtenerPorFormulaId(int tipoFormulaId)
        {
            IList<FormulaInfo> formulaInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerPorFormulaId(tipoFormulaId);
                DataSet ds = Retrieve("ServicioAlimento_ObtenerPorFormulaID", parameters);
                if (ValidateDataSet(ds))
                {
                    formulaInfo = MapFormulaDAL.ObtenerPorFormulaId(ds);
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
            return formulaInfo;
        }

        internal List<FormulaInfo> ObtenerFormulaDescripcionPorIDs(List<int> formulasID)
        {
            List<FormulaInfo> formulaInfo = new List<FormulaInfo>();
            try
            {
                Logger.Info();

                var ids = string.Join("|", formulasID.ToArray());

                Dictionary<string, object> parameters = new Dictionary<string,object>();
                parameters.Add("@ids", ids);
                DataSet ds = Retrieve("Formula_ObtenerFormulaDescripcionPorIDs", parameters);
                if (ValidateDataSet(ds))
                {
                    formulaInfo = MapFormulaDAL.ObtenerFormulaDescripcionPorIDs(ds);
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
            return formulaInfo;
        }

        /// <summary>
        /// Obtiene la formula por su clave para
        /// opcion de Calidad pase a proceso
        /// </summary>
        /// <param name="formulaID"></param>
        /// <returns></returns>
        internal FormulaInfo ObtenerPorFormulaIDCalidadPaseProceso(int formulaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxFormulaDAL.ObtenerParametrosPorFormulaIDCalidadPaseProceso(formulaID);
                DataSet ds = Retrieve("Formula_ObtenerPorFormulaIDPaseCalidad", parameters);
                FormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerPorFormulaIDCalidadPaseProceso(ds);
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

        internal ResultadoInfo<FormulaInfo> ObtenerPorPaseCalidadPaginado(PaginacionInfo pagina, FormulaInfo formula)
        {
            try
            {
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerParametrosPorPaseCalidadPaginado(pagina, formula);
                DataSet ds = Retrieve("Formula_ObtenerPorPaseCalidadPaginado", parameters);
                ResultadoInfo<FormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerPorPaseCalidadPaginado(ds);
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
        /// Obtiene una lista de Formula
        /// </summary>
        /// <returns></returns>
        internal IList<FormulaInfo> ObtenerFormulasConfiguradas(EstatusEnum activo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerFormulasConfiguradas(activo);
                DataSet ds = Retrieve("Formula_ObtenerConfiguradas", parameters);
                IList<FormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerParametrosFormulasConfiguradas(ds);
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
        ///<summary>
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        internal IList<RotoMixInfo> ObtenerRotoMixConfiguradas(int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.ObtenerRotoMixConfiguradas(organizacionId);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerRotoMix", parameters);
                IList<RotoMixInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.ObtenerRotoMixConfiguradas(ds);
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
        ///<summary>
        /// Obtiene el número de batch que deberá mostrarse en el texbox "txtBatch"
        /// Este dato se inicializado en 1, por rotomix y por día.
        /// </summary>
        /// <returns></returns>
        internal int CantidadBatch(int organizacionId, int rotoMix)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFormulaDAL.CantidadBatch(organizacionId, rotoMix);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerCantidadBatch", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapFormulaDAL.CantidadBatch(ds);
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
