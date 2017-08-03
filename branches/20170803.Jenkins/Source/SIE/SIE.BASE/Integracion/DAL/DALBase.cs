using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Log;

namespace SIE.Base.Integracion.DAL
{
    public abstract class DALBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected DALBase()
        {
            ConnectionString = "ConnectionString";
        }

        /// <summary>
        /// Propiedad de conexion a base de datos
        /// </summary>
        public string ConnectionString { get; set; }

        public IDbConnection Connection
        {
            get { return GetDatabase().CreateConnection(); }
        }

        /// <summary>
        /// Metodo que genera la conexion a la Base de Datos
        /// </summary>
        /// <returns></returns>
        protected Database GetDatabase()
        {
            Database database;
            try
            {
                database = DatabaseFactory.CreateDatabase(ConnectionString);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return database;
        }

        /// <summary>
        /// Metodo que obtiene una lista de un procedimiento almacenado sin parametros
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado</param>
        /// <returns>Dataset</returns>        
        public DataSet Retrieve(string spName)
        {
            DataSet result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                result = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que obtiene una lista de un procedimiento almacenado sin parametros
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public IDataReader RetrieveReader(string spName)
        {
            IDataReader result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                result = db.ExecuteReader(cmd); //cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que obtiene una lista de un procedimiento almacenado sin parametros
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"> </param>
        /// <returns></returns>
        public IDataReader RetrieveReader(string spName, Dictionary<string, object> parameters)
        {
            IDataReader result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                AuxiliarParametros(parameters, cmd);
                result = db.ExecuteReader(cmd); //cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que obtiene una lista de un procedimiento almacenado con parametros
        /// </summary>
        /// <param name="spName">Nombre de procedimiento almacenado</param>
        /// <param name="parameters">parametros</param>
        /// <returns>Dataset</returns>                
        protected DataSet Retrieve(string spName, Dictionary<string, object> parameters)
        {
            DataSet result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                AuxiliarParametros(parameters, cmd);
                result = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que obtiene una lista de un select directo
        /// </summary>
        /// <param name="spConsulta">Nombre de procedimiento almacenado</param>
        /// <returns>Dataset</returns>                
        protected DataSet RetrieveConsulta(string spConsulta)
        {
            DataSet result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetSqlStringCommand(spConsulta);
                cmd.CommandTimeout = 5000;
                result = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que obtiene un valor de un procedimiento almacenado con parametros
        /// </summary>
        /// <typeparam name="T">Tipo de valor</typeparam>
        /// <param name="spName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">parametros</param>
        /// <returns>valor</returns>        
        protected T RetrieveValue<T>(string spName, Dictionary<string, object> parameters)
        {
            T result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                AuxiliarParametros(parameters, cmd);
                result = (T) db.ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que obtiene un valor de un procedimiento almacenado 
        /// </summary>
        /// <typeparam name="T">Tipo de valor</typeparam>
        /// <param name="spName">Nombre del procedimiento almacenado</param>        
        /// <returns>valor</returns>        
        protected T RetrieveValue<T>(string spName)
        {
            T result;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                result = (T) db.ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que guarda un registro en una tabla con un procedimiento almacenado
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parametros</param>
        /// <returns>Id del registro insertado</returns>        
        protected int Create(string spName, Dictionary<string, object> parameters)
        {
            int id;
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 10000;
                AuxiliarParametros(parameters, cmd);
                id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return id;
        }

        /// <summary>
        /// Asigna los parametros al command por referencia
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="cmd"></param>
        protected void AuxiliarParametros(Dictionary<string, object> parameters, DbCommand cmd)
        {
            if (parameters == null || parameters.Count <= 0) return;
            foreach (var param in parameters)
            {
                IDbDataParameter parameter = new SqlParameter(string.Format("{0}",param.Key), param.Value ?? DBNull.Value);
                cmd.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Metodo que Actualiza un registro en una tabla con un procedimiento almacenado
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parametros</param>                
        public void Update(string spName, Dictionary<string, object> parameters)
        {
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                AuxiliarParametros(parameters, cmd);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Valida la estructura del data set.
        /// </summary>
        /// <param name="dataSet">DataSet para ser validado</param>
        /// <returns>Verdadedo si es valido, falso si es invalido</returns>
        protected bool ValidateDataSet(DataSet dataSet)
        {
            bool isValid = true;

            if (dataSet == null)
            {
                isValid = false;
            }
            else if (dataSet.Tables.Count == 0)
            {
                isValid = false;
            }
            else if (dataSet.Tables[0].Rows.Count == 0)
            {
                isValid = false;
            }

            return isValid;
        }

        protected bool ValidateDataReader(IDataReader reader)
        {
            var isValid = true;
            if (reader == null)
            {
                isValid = false;
            }
            else
            {
                if (reader.RecordsAffected == 0)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Valida la estructura del data set.
        /// </summary>
        /// <param name="dataSet">DataSet para ser validado</param>
        /// <returns>Verdadedo si es valido, falso si es invalido</returns>
        protected bool ValidateDataSetMultiTabla(DataSet dataSet)
        {
            bool isValid = true;

            if (dataSet == null)
            {
                isValid = false;
            }
            else if (dataSet.Tables.Count == 0)
            {
                isValid = false;
            }
            else if (dataSet.Tables[0].Rows.Count == 0)
            {
                isValid = false;
            }
            if (dataSet != null)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    if (dataSet.Tables[i].Rows.Count > 0)
                    {
                        isValid = true;
                        break;
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Metodo que borra un registro de una tabla con un procedimiento almacenado
        /// </summary>
        /// <param name="spName">Nombre del procedimiento almacenado</param>
        /// <param name="parameters">Parametros</param>        
        protected void Delete(string spName, Dictionary<string, object> parameters)
        {
            try
            {
                Logger.Info();
                Database db = GetDatabase();
                DbCommand cmd = db.GetStoredProcCommand(spName);
                AuxiliarParametros(parameters, cmd);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Metodo que obtiene una lista de un procedimiento almacenado con parametros
        /// </summary>
        /// <param name="spName">Nombre de procedimiento almacenado</param>
        /// <param name="parameters">parametros</param>
        /// <returns>Dataset</returns>                
        protected DataSet Retrieve(string spName, Dictionary<string, object> parameters, string connectionString)
        {
            DataSet result;
            try
            {
                Logger.Info();
                Database db = GetDatabase(connectionString);
                DbCommand cmd = db.GetStoredProcCommand(spName);
                cmd.CommandTimeout = 5000;
                AuxiliarParametros(parameters, cmd);
                result = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Metodo que genera la conexion a la Base de Datos
        /// </summary>
        /// <returns></returns>
        protected Database GetDatabase(string connectionString)
        {
            Database database;
            try
            {
                database = DatabaseFactory.CreateDatabase(connectionString);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return database;
        }
    }
}