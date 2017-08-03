using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Integracion.DAL.Auxiliar;
using System.Transactions;
using System.Xml.Linq;
using SIE.Base.Constantes;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ControlSacrificioDAL : DALBase
    {
        public void SyncResumenSacrificio_planchado(List<ControlSacrificioInfo.SincronizacionSIAP> informacionSIAP, int usuarioID, int organizacionId)
        {
            try
            {
                Logger.Info();

                var conexion = ObtenerCadenaConexionSPI(organizacionId);

                using (var transaction = new TransactionScope())
                {
                    using (var connection = new SqlConnection(conexion))
                    {
                        connection.Open();

                        //var parameters = AuxAnimalDAL.ObtenerParametrosPlancharAretes(informacionSIAP, usuarioID);

                        var xml =
                                new XElement("ROOT",
                                            from animal in informacionSIAP
                                            select new XElement("Relacion",
                                                                new XElement("Arete", animal.Arete),
                                                                new XElement("AnimalId", animal.AnimalID),
                                                                new XElement("LoteId", animal.LoteID),
                                                                new XElement("Corral", animal.Corral)
                                                ));

                        var command = new SqlCommand("SincronizarResumenSacrificio_Planchado", connection) { CommandType = CommandType.StoredProcedure };

                        var parameters = new Dictionary<string, object> { { "@InformacionSIAP", xml.ToString() }, { "@UsuarioID", usuarioID } };
                        foreach (var param in parameters)
                        {
                            IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                            command.Parameters.Add(parameter);
                        }
                        var reader = command.ExecuteNonQuery();

                    }
                    transaction.Complete();
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

        public void SyncResumenSacrificio_transferencia(List<ControlSacrificioInfo.SincronizacionSIAP> informacionSIAP, int usuarioID, int organizacionId)
        {
            try
            {
                Logger.Info();

                var conexion = ObtenerCadenaConexionSPI(organizacionId);

                using (var transaction = new TransactionScope())
                {
                    using (var connection = new SqlConnection(conexion))
                    {
                        connection.Open();

                        //var parameters = AuxAnimalDAL.ObtenerParametrosPlancharAretes(informacionSIAP, usuarioID);

                        var xml =
                                new XElement("ROOT",
                                            from animal in informacionSIAP
                                            select new XElement("Relacion",
                                                                new XElement("Arete", animal.Arete),
                                                                new XElement("AnimalId", animal.AnimalID),
                                                                new XElement("LoteId", animal.LoteID),
                                                                new XElement("Corral", animal.Corral)
                                                ));

                        var command = new SqlCommand("SincronizarResumenSacrificio_Transferencia", connection) { CommandType = CommandType.StoredProcedure };

                        var parameters = new Dictionary<string, object> { { "@InformacionSIAP", xml.ToString() }, { "@UsuarioID", usuarioID } };
                        foreach (var param in parameters)
                        {
                            IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                            command.Parameters.Add(parameter);
                        }
                        var reader = command.ExecuteNonQuery();

                    }
                    transaction.Complete();
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
    }
}
