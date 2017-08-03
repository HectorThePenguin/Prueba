using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class MigracionDAL : DALBase
    {
        // public MigracionDAL MigracionDAL;
        /// <summary>
        /// 
        /// </summary>
        public MigracionDAL()
        {
            ConnectionString = "ConnectionString";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizacionID"></param>
        public MigracionDAL(int organizacionID)
        {
            // Se valida que organizacion se esta procesando para asignar la cadena de conexion
            switch (organizacionID)
            {
                case (int)Organizacion.GanaderaIntegralCentinela: // Conexion para ControlIndividual
                    ConnectionString = "ConnectionStringControlIndividual";
                    break;
                case (int)Organizacion.GanaderaIntegralLucero: // Conexion para ControlIndividual
                    ConnectionString = "ConnectionStringControlIndividualLucero";
                    break;
            }
        }

        /// <summary>
        /// Metodo para obtener los animales del control individual
        /// </summary>
        internal ControlIndividualInfo ObtenerAnimalesControlIndividual()
        {
            ControlIndividualInfo controlIndividualInfo = null;
            try
            {

                //string conexion = ObtenerCadenaConexionSPI(organizacionId);

                //using (var connection = new SqlConnection(conexion))
                //{
                //    connection.Open();
                //    var command = new SqlCommand("SalidaSacrificio_Crear", connection);
                //    command.CommandType = CommandType.StoredProcedure;

                //    foreach (var salida in salidaSacrificio.GroupBy(e => e.NUM_CORR))
                //    {
                //        var corralSacrificar = salida.FirstOrDefault();
                //        command.Parameters.Clear();
                //        Dictionary<string, object> parameters = AuxSalidaSacrificioDAL.ObtenerParametrosCrear(corralSacrificar);
                //        foreach (var param in parameters)
                //        {
                //            IDbDataParameter parameter = new SqlParameter(string.Format("{0}", param.Key), param.Value ?? DBNull.Value);
                //            command.Parameters.Add(parameter);
                //        }
                //        command.ExecuteNonQuery();
                //    }
                //}
                DataSet ds = new DataSet();
                string conexion = @"Data Source=srv-sqlluce;Initial Catalog=CONTROL;User ID=usrSIAP;Password=usrSIAP";

                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    var command = new SqlCommand(String.Format("{0}; {1};",
                    "SELECT [Num_Corr], [Arete], [Fecha_Comp], [Tipo_Gan], [Cal_Eng], [Paletas], [Fecha_Corte], [Peso_Corte], [Temperatura] FROM Control.dbo.CtrMani cm",
                    "SELECT cr.[Arete], cr.[Fecha_Comp], cr.[Fecha_Reim], cr.[Peso_Reimp] FROM Control.dbo.CtrReim cr INNER JOIN Control.dbo.CtrMani cm ON cr.Arete = cm.Arete AND cr.Fecha_Comp = cm.Fecha_Comp"), connection);
                    command.CommandType = CommandType.Text;

                    command.CommandText = String.Format("{0}; {1};",
                    "SELECT [Num_Corr], [Arete], [Fecha_Comp], [Tipo_Gan], [Cal_Eng], [Paletas], [Fecha_Corte], [Peso_Corte], [Temperatura] FROM Control.dbo.CtrMani cm",
                    "SELECT cr.[Arete], cr.[Fecha_Comp], cr.[Fecha_Reim], cr.[Peso_Reimp] FROM Control.dbo.CtrReim cr INNER JOIN Control.dbo.CtrMani cm ON cr.Arete = cm.Arete AND cr.Fecha_Comp = cm.Fecha_Comp");

                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }

                if (ValidateDataSet(ds))
                {
                    controlIndividualInfo = MapMigracionDAL.ObtenerAnimalesControlIndividual(ds);
                }

                //Logger.Info();
                //// var parameters = AuxMigracionDAL.ObtenerParametrosGuardarResumenXML();
                //var instruccionSelect = String.Format("{0}; {1};",
                //    "SELECT [Num_Corr], [Arete], [Fecha_Comp], [Tipo_Gan], [Cal_Eng], [Paletas], [Fecha_Corte], [Peso_Corte], [Temperatura] FROM Control.dbo.CtrMani cm",
                //    "SELECT cr.[Arete], cr.[Fecha_Comp], cr.[Fecha_Reim], cr.[Peso_Reimp] FROM Control.dbo.CtrReim cr INNER JOIN Control.dbo.CtrMani cm ON cr.Arete = cm.Arete AND cr.Fecha_Comp = cm.Fecha_Comp");
                //var ds = RetrieveConsulta(instruccionSelect);

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return controlIndividualInfo;
        }

        /// <summary>
        /// Metodo para almacenar los animales en SIAP
        /// </summary>
        internal MigracionCifrasControlInfo GuardarAnimalesSIAP(int organizacionId)
        {
            try
            {
                Logger.Info();
                try
                {
                    Logger.Info();
                    var parametros =
                        new Dictionary<string, object> { { "@OrganizacionID", organizacionId } };
                    var ds = Retrieve("Migracion_GuardarInventario", parametros);
                    MigracionCifrasControlInfo resultado = null;
                    if (ValidateDataSet(ds))
                    {
                        resultado = MapMigracionDAL.ObtenerMigracionCifrasContro(ds, 3);
                    }
                    return resultado;

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
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Metodo para guardr el resumen
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal MigracionCifrasControlInfo GuardarResumen(List<ResumenInfo> lista, int organizacionId)
        {

            try
            {
                Logger.Info();
                var parameters = AuxMigracionDAL.ObtenerParametrosGuardarResumenXML(lista, organizacionId);
                var ds = Retrieve("Migracion_GuardarResumen", parameters);
                MigracionCifrasControlInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMigracionDAL.ObtenerMigracionCifrasContro(ds, 1);
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
        /// Metodo para almacenar los animales de control individual en SIAP ademas de crear las cargas iniciales.
        /// </summary>
        /// <param name="controlIndividualInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal MigracionCifrasControlInfo GuardarCargaInicialSIAP(ControlIndividualInfo controlIndividualInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxMigracionDAL.ObtenerParametrosCargaInicialSIAP(controlIndividualInfo, organizacionId);
                var ds = Retrieve("Migracion_GuardarCargaInicialSIAP", parameters);
                MigracionCifrasControlInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMigracionDAL.ObtenerMigracionCifrasContro(ds, 2);
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
