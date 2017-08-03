using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Constantes;
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
    public class SalidaSacrificioDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de SalidaSacrificio
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public void Crear(SalidaSacrificioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosCrear(info);
                //int result = Create("SalidaSacrificio_Crear", parameters);
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    var command = new SqlCommand("SalidaSacrificio_Crear", connection);
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }
                    command.Connection.Open();
                    command.ExecuteNonQuery();
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
        /// Metodo para actualizar un registro de SalidaSacrificio
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(SalidaSacrificioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosActualizar(info);
                Update("SalidaSacrificio_Actualizar", parameters);
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
        public ResultadoInfo<SalidaSacrificioInfo> ObtenerPorPagina(PaginacionInfo pagina, SalidaSacrificioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("SalidaSacrificio_ObtenerPorPagina", parameters);
                ResultadoInfo<SalidaSacrificioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaSacrificioDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de SalidaSacrificio
        /// </summary>
        /// <returns></returns>
        public IList<SalidaSacrificioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("SalidaSacrificio_ObtenerTodos");
                IList<SalidaSacrificioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaSacrificioDAL.ObtenerTodos(ds);
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
        public IList<SalidaSacrificioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("SalidaSacrificio_ObtenerTodos", parameters);
                IList<SalidaSacrificioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaSacrificioDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de SalidaSacrificio
        /// </summary>
        /// <param name="salidaSacrificioID">Identificador de la SalidaSacrificio</param>
        /// <returns></returns>
        public SalidaSacrificioInfo ObtenerPorID(int salidaSacrificioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosPorID(salidaSacrificioID);
                DataSet ds = Retrieve("SalidaSacrificio_ObtenerPorID", parameters);
                SalidaSacrificioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaSacrificioDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de SalidaSacrificio
        /// </summary>
        /// <param name="ordenSacrificioID">Identificador de la SalidaSacrificio</param>
        /// <returns></returns>
        public IList<SalidaSacrificioInfo> ObtenerPorOrdenSacrificioID(int ordenSacrificioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosPorOrdenSacrificioID(ordenSacrificioID);
                DataSet ds = Retrieve("SalidaSacrificio_ObtenerPorOrdenSacrificioID", parameters);
                IList<SalidaSacrificioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaSacrificioDAL.ObtenerPorOrdenSacrificioID(ds);
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

        public bool CrearLista(IList<SalidaSacrificioInfo> salidaSacrificio, string conexion)
        {
            var result = false;
            try
            {
                Logger.Info();
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("SalidaSacrificio_Crear", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var salida in salidaSacrificio.GroupBy(e => e.NUM_CORR))
                    {
                        var corralSacrificar = salida.FirstOrDefault();
                        command.Parameters.Clear();
                        Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosCrear(corralSacrificar);
                        foreach (var param in parameters)
                        {
                            IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                            command.Parameters.Add(parameter);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY constraint 'PK_ConfiguracionVisceraCorral'"))
                {

                }
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }

        public bool EliminarDetalleOrdenSacrificio(List<SalidaSacrificioInfo> orden, int organizacionId, int ordenSacrificioId, int aplicaMarel, int usuarioId)
        {
            var result = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosEliminarDetalleOrdenSacrificio(orden, organizacionId, ordenSacrificioId, aplicaMarel, usuarioId);
                DataSet ds = Retrieve("OrdenSacrificioDetalle_Eliminar", parameters);
                if (ValidateDataSet(ds))
                {
                    result = true;
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                Logger.Error(new Exception("Ocurrió un error al realizar el rollback del SIAP por error del SCP."));
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                Logger.Error(new Exception("Ocurrió un error al realizar el rollback del SIAP por error del SCP."));
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Error(new Exception("Ocurrió un error al realizar el rollback del SIAP por error del SCP."));
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        public string ObtenerCadenaConexionSPI(int organizacionId)
        {
            var filtro = new ConfiguracionParametrosInfo
            {
                OrganizacionID = organizacionId,
                TipoParametro = (int)TiposParametrosEnum.InterfazControlPiso
            };

            var configuracionParametrosDal = new ConfiguracionParametrosDAL();
            var configuracion = configuracionParametrosDal.ObtenerPorOrganizacionTipoParametro(filtro);

            var servidorInfo = configuracion.FirstOrDefault(e => e.Clave == "ServidorSPI");
            var baseDatosInfo = configuracion.FirstOrDefault(e => e.Clave == "BaseDatosSPI");
            var usuarioInfo = configuracion.FirstOrDefault(e => e.Clave == "UsuarioSPI");
            var passwordInfo = configuracion.FirstOrDefault(e => e.Clave == "PasswordSPI");

            string servidor = servidorInfo != null ? servidorInfo.Valor : string.Empty;
            string baseDatos = baseDatosInfo != null ? baseDatosInfo.Valor : string.Empty;
            string usuario = usuarioInfo != null ? usuarioInfo.Valor : string.Empty;
            string password = passwordInfo != null ? passwordInfo.Valor : string.Empty;

            var parametros = new[] { servidor, baseDatos, usuario, password };

            string conexion =
                string.Format(Constante.SqlConexion, parametros);
            return conexion;
        }

        public List<SalidaSacrificioInfo> ObtenerSalidaSacrificio(List<SalidaSacrificioDetalleInfo> movimientosSiap, int organizacionId, string conexion)
        {
            var sacrificio = new List<SalidaSacrificioInfo>();
            try
            {
                Logger.Info();
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("SalidaSacrificio_ObtenerPorID", connection) { CommandType = CommandType.StoredProcedure };
                    var parameters = AuxSalidaSacrificioDAL.ObtenerParametrosObtenerSalidaSacrificio(movimientosSiap);
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }
                    var reader = command.ExecuteReader();
                    sacrificio = MapSalidaSacrificioDAL.ObtenerPorOrdenSacrificio(reader);
                }

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                Logger.Error(new Exception("Ocurrió un error al obtener la salida de sacrificio del SCP."));
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                Logger.Error(new Exception("Ocurrió un error al obtener la salida de sacrificio del SCP."));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Error(new Exception("Ocurrió un error al obtener la salida de sacrificio del SCP."));
            }

            return sacrificio;
        }

        public IList<SalidaSacrificioDetalleInfo> ObtenerPorOrdenSacrificioId(int ordenSacrificioId, int organizacionId, byte aplicaRollBack, int usuarioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosPorOrdenSacrificioId(ordenSacrificioId, organizacionId, aplicaRollBack, usuarioID);
                DataSet ds = Retrieve("ExportSalidaSacrificio_ObtenerPorID", parameters);
                IList<SalidaSacrificioDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSalidaSacrificioDAL.ObtenerPorOrdenSacrificioDetalleId(ds);
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

        public bool GuardarSalidaSacrificioMarel(IList<SalidaSacrificioInfo> salidaSacrificio, int organizacionId, string conexion)
        {
            var result = false;
            try
            {
                Logger.Info();
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var parameters = AuxSalidaSacrificioDAL.ObtenerParametrosGuardarMarel(salidaSacrificio.ToList());

                    var command = new SqlCommand("SalidaSacrificio_Guardar", connection) { CommandType = CommandType.StoredProcedure };
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }
                    var reader = command.ExecuteReader();
                    var error = MapSalidaSacrificioDAL.GuardarSalidaSacrificioMarel(reader);
                    if (error.Trim() != string.Empty)
                    {
                        Logger.Error(new Exception(error));
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return result;
        }
    }
}

