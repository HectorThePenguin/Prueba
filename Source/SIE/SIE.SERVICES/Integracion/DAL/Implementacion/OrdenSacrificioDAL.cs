using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Constantes;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;
using System.Transactions;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class OrdenSacrificioDAL : DALBase
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal OrdenSacrificioInfo GuardarOrdenSacrificio(OrdenSacrificioInfo orden)
        {
            try
            {
                Logger.Info();
                var parameters = AuxOrdenSacrificioDAL.ObtenerParametrosGuardarOrdenSacrificio(orden);
                var ds = Retrieve("OrdenSacrificio_GuardarOrden", parameters);
                OrdenSacrificioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerOrdenSacrificio(ds);

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
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal IList<OrdenSacrificioDetalleInfo> GuardarDetalleOrdenSacrificio(OrdenSacrificioInfo orden, IList<OrdenSacrificioDetalleInfo> detalleOrden)
        {
            try
            {
                Logger.Info();
                var parameters = AuxOrdenSacrificioDAL.ObtenerParametrosGuardarDetalleOrdenSacrificio(orden, detalleOrden);
                var ds = Retrieve("OrdenSacrificio_GuardarDetalleOrden", parameters);
                IList<OrdenSacrificioDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerDetalleOrdenSacrificio(ds,true);
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
        /// Obtiene la orden de sacrificio de un dia en eespecifico
        /// </summary>
        /// <param name="orden"></param>
        /// <returns></returns>
        internal OrdenSacrificioInfo ObtenerOrdenSacrificioDelDia(OrdenSacrificioInfo orden)
        {
            try
            {
                Logger.Info();
                var parameters = AuxOrdenSacrificioDAL.ObtenerParametrosOrdenSacrificioDelDia(orden);
                var ds = Retrieve("OrdenSacrificio_ObtenerOrdenDiaActual", parameters);
                OrdenSacrificioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerOrdenSacrificio(ds);

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
        /// Obtiene el detalle de una orden de sacrificio
        /// </summary>
        /// <param name="orden"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        internal IList<OrdenSacrificioDetalleInfo> ObtenerDetalleOrdenSacrificio(OrdenSacrificioInfo orden,  bool activo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxOrdenSacrificioDAL.ObtenerParametrosOrdenSacrificioDetalle(orden);
                var ds = Retrieve("OrdenSacrificio_ObtenerDetalleOrden", parameters);
                IList<OrdenSacrificioDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerDetalleOrdenSacrificio(ds,activo);
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
        /// Cambia el estatus al detalle de una orden de sacrificio
        /// </summary>
        /// <param name="detalleOrdenSacrificio"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        internal int EliminarDetalleOrdenSacrificio(IList<OrdenSacrificioDetalleInfo> detalleOrdenSacrificio,int idUsuario )
        {

            try
            {
                Logger.Info();
                var parametros =
                    AuxOrdenSacrificioDAL.ObtenerParametrosEliminarOrdenSacrificioDetalle(detalleOrdenSacrificio,idUsuario);
                return Create("OrdenSacrificio_CambiarEstatus", parametros);
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
        /// Obtiene los dias de engorda 70
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal int ObtnerDiasEngorda70(LoteInfo lote)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxOrdenSacrificioDAL.ObtenerParametroDiasEngorda70(lote);
                var ds = Retrieve("OrdenSacrificio_CalcularDiasEngorda70", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerDiasEngorda70(ds);
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
            return result;
        }
        /// <summary>
        /// Obtiene el numero de cabezas programadas en otras ordenes para el lote determinado
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal int ObtnerCabezasDiferentesOrdenes(LoteInfo lote, int ordenSacrificioId)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxOrdenSacrificioDAL.ObtenerParametroObtnerCabezasDiferentesOrdenes(lote,ordenSacrificioId);
                var ds = Retrieve("OrdenSacrificio_CabezasSacrificarPorLoteOrdenDiferente", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtnerCabezasDiferentesOrdenes(ds);
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
            return result;
        }
        /// <summary>
        /// Metodo para decrementar el animal muerto de la orden se sacrificio.
        /// </summary>
        /// <param name="animalID"></param>
        public void DecrementarAnimalMuerto(long animalID)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>{{"@AnimalID", animalID}};
                Update("OrdenSacrificio_DecrementarAnimalMuerto", parametros);
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
        /// Obtiene el numero de cabezas programadas en otras ordenes para el lote determinado
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal bool ValidarLoteOrdenSacrificio(int loteID)
        {
            var result = false;
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object> { { "@LoteID", loteID } };
                //var parameters = AuxOrdenSacrificioDAL.ObtenerParametroObtnerCabezasDiferentesOrdenes(lote, ordenSacrificioId);
                var ds = Retrieve("OrdenSacrificio_ValidarLoteProgramado", parameters);
                if (ValidateDataSet(ds))
                {
                    result = true;
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
            return result;
        }

        public List<OrdenSacrificioDetalleInfo> DetalleOrden(string fecha, int organizacionId)
        {

            try
            {
                Logger.Info();
                var parametrs = AuxOrdenSacrificioDAL.ObtenerParametroDetalleOrden(fecha, organizacionId);
                var ds = Retrieve("OrdenSacrificio_ObtenerDetalleOrdenFecha", parametrs);
                List<OrdenSacrificioDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerDetalleOrdenSacrificioFecha(ds);
                }
                return result;
            }
            catch (DataException ex)
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

        public OrdenSacrificioInfo OrdenSacrificioFecha(string fecha, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametro = AuxOrdenSacrificioDAL.ObtenerParametroDetalleOrden(fecha, organizacionId);
                var ds = Retrieve("OrdenSacrificio_ObtenerOrdenFecha", parametro);
                OrdenSacrificioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerOrdenSacrificioFecha(ds);
                }
                return result;
            }
            catch (DataException ex)
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
        
        public List<string> ConexionOrganizacion(int organizacionId)
        {
            List<string> result = new List<string>();
            try
            {
                Logger.Info();
                var parametro = new Dictionary<string, object> { { "@OrganizacionID ", organizacionId } };
                var ds = Retrieve("ConexionOrganizacion", parametro);
                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerConexionOrganizacion(ds);
                }
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        public List<string> ValidaLoteNoqueado(string con, string xml)
        {
            var listLoteNoqueado = new List<string>();
            try
            {
                Logger.Info();
                using (SqlConnection cn = new SqlConnection(con))
                {
                    SqlCommand cmd = new SqlCommand("CancelacionOrdenSacrificioFecha", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@XmlDetalle", SqlDbType.VarChar);
                    cmd.Parameters["@XmlDetalle"].Value = xml;
                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listLoteNoqueado.Add(reader.GetValue(0).ToString());
                    }
                }

                return listLoteNoqueado;
            }
            catch (SqlException ex)
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

        public bool ValidaCancelacionCabezasSIAP(string xml, int ordenSacrificioId, int usuarioId)
        {
            var result = false;
            try
            {
                Logger.Info();
                var parametro = new Dictionary<string, object> { { "@XmlDetalleOrden", xml }, { "@OrdenSacrificioID", ordenSacrificioId }, { "@UsuarioID", usuarioId } };
                var ds = Retrieve("CancelacionOrdenSacrificioSIAP", parametro);

                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerCancelacionOrden(ds);
                }

                return result;
            }
            catch (SqlException ex)
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

        internal List<SalidaSacrificioInfo> ConsultarEstatusOrdenSacrificioEnMarel(int organizacionId, string fechaSacrificio, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                Logger.Info();
                string conexion = ObtenerCadenaConexionSPI(organizacionId);
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("ExportSalidaSacrificio_ObtenerStatus", connection) { CommandType = CommandType.StoredProcedure };
                    Dictionary<string, object> parameters = AuxOrdenSacrificioDAL.ObtenerParametroConsultarEstatusMarel(fechaSacrificio, detalle);
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }
                    var reader = command.ExecuteReader();
                    return MapOrdenSacrificioDAL.ObtenerEstatusOrdenSacrificioMarel(reader);
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

        internal List<OrdenSacrificioDetalleInfo> ValidarCabezasActualesVsCabezasSacrificar(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                Logger.Info();
                var result = new List<OrdenSacrificioDetalleInfo>();
                var parametros = AuxOrdenSacrificioDAL.ObtenerParametroValidarCabezasActualesVsCabezasSacrificar(organizacionId, detalle);
                var ds = Retrieve("Lote_ValidarCabezas", parametros);

                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerCabezasActualesVsCabezasSacrificar(ds);
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

        internal List<string> ValidarCorralConLotesActivos(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                Logger.Info();
                var result = new List<string>();
                var parametros = AuxOrdenSacrificioDAL.ObtenerParametroCorralConLotesActivos(organizacionId, detalle);
                var ds = Retrieve("Lote_ExitenCorralesConLotesActivos", parametros);

                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerCorralesConLotesActivos(ds);
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

        public bool CancelacionOrdenSacrificioMarel(string xml, int organizacionId)
        {
            try
            {
                Logger.Info();
                string conexion = ObtenerCadenaConexionSPI(organizacionId);
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("ExportSalidaSacrificio_Cancelar", connection) { CommandType = CommandType.StoredProcedure };
                    var parameters = new Dictionary<string, object> { { "@Xml", xml } };
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }
                    var reader = command.ExecuteReader();
                    return MapOrdenSacrificioDAL.ObtenerCancelacionMarel(reader);
                }
            }
            catch (SqlException ex)
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

        //public bool CancelacionOrdenSacrificioSCP(int ordenSacrificioID, string fechaSacrificio, int organizacionId)
        public bool CancelacionOrdenSacrificioSCP(string xmlOrdenesDetalle, string fechaSacrificio, int organizacionId)
        {
            try
            {
                Logger.Info();
                string conexion = ObtenerCadenaConexionSPI(organizacionId);
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("SP_CancelarOrdenSacrificio", connection) { CommandType = CommandType.StoredProcedure };
                    var parameters = new Dictionary<string, object> { 
                        { "@OrdenDetalle", xmlOrdenesDetalle }, 
                        { "@OrganizacionId", organizacionId }, 
                        { "@FechaSacrificion", fechaSacrificio } 
                    };
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }

                    var reader = command.ExecuteReader();

                    return MapOrdenSacrificioDAL.ObtenerCancelacionMarel(reader);
                }
            }
            catch (SqlException ex)
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

        private string ObtenerCadenaConexionSPI(int organizacionId)
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

        public List<ControlSacrificioInfo> ObtenerResumenSacrificioScp(string fechaSacrificio, int organizacionId)
        {
            try
            {
                Logger.Info();
                var conexion = ObtenerCadenaConexionSPI(organizacionId);
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand("ResumenSacrificio_ObtenerPorFecha", connection) { CommandType = CommandType.StoredProcedure };
                    var parameters = new Dictionary<string, object> { { "@Fecha_Sacrificio", fechaSacrificio } };
                    foreach (var param in parameters)
                    {
                        IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                        command.Parameters.Add(parameter);
                    }
                    var reader = command.ExecuteReader();
                    return MapOrdenSacrificioDAL.ObtenerResumenSacrificioScp(reader);
                }
            }
            catch (SqlException ex)
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

        internal List<ControlSacrificioInfo> ObtenerResumenSacrificioSiap(List<ControlSacrificioInfo> resumenSacrificio)
        {
            try
            {
                Logger.Info();
                var result = new List<ControlSacrificioInfo>();
                var parametros = AuxOrdenSacrificioDAL.ObtenerParametroResumenSacrificioSiap(resumenSacrificio);
                var ds = Retrieve("ResumenSacrificio_ObtenerPorFecha", parametros);

                if (ValidateDataSet(ds))
                {
                    result = MapOrdenSacrificioDAL.ObtenerResumenSacrificioSiap(ds);
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

        ///// <summary>
        ///// Metrodo Para Guardar la orden de sacrificio en la 
        ///// en al interfaz de SPI
        ///// </summary>
        //internal void GuardarOrdenSacrificioInterfazSPI(IList<SalidaSacrificioInfo> lista)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        var parameters = AuxOrdenSacrificioDAL.ObtenerParametrosGuardarOrdenSacrificioInterfazSPI(lista);

        //        int organizacion = lista[0].OrganizacionID;

        //        string conexion = ConnectionString;
        //        ConnectionString = ObtenerCadenaConexionSPI(organizacion);

        //        Create("SalidaSacrificio_Crear", parameters);

        //        ConnectionString = conexion;
        //    }
        //    catch (SqlException ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    catch (DataException ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}
      
    }
}
