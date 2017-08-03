using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EnfermeriaCorralDAL : DALBase
    {
        /// <summary>
        /// Obtiene un registro de Enfermeria
        /// </summary>
        /// <param name="enfermeriaID">Identificador de la Enfermeria</param>
        /// <returns></returns>
        internal List<EnfermeriaCorralInfo> ObtenerCorralesPorEnfermeriaID(int enfermeriaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEnfermeriaCorralDAL.ObtenerParametrosObtenerCorralesEnfermeria(enfermeriaID);
                DataSet ds = Retrieve("EnfermeriaCorral_ObtenerPorEnfermeria", parameters);
                List<EnfermeriaCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEnfermeriaCorralDAL.ObtenerCorralesEnfermeria(ds);
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
        /// Metodo para Crear un registro de Tratamiento
        /// </summary>
        /// <param name="listaEnfermeriaCorral">Valores de la entidad que será creada</param>
        /// <param name="enfermeriaID">Id de la enfermeria</param>
        internal int GuardarEnfermeriaCorral(List<EnfermeriaCorralInfo> listaEnfermeriaCorral, int enfermeriaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEnfermeriaCorralDAL.ObtenerParametrosGuardarEnfermeriaCorral(listaEnfermeriaCorral, enfermeriaID);
                int result = Create("EnfermeriaCorral_Guardar", parameters);
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
        /// Metodo para Inactivar los registros en las tablas de EnfermeriaCorral y SupervisorEnfermeria
        /// </summary>
        /// <param name="enfermeriaID">Id de la enfermeria</param>
        internal int InactivarEnfermeriaCorralYSupervisorEnfermeria(int enfermeriaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEnfermeriaCorralDAL.ObtenerParametrosInactivarEnfermeriaCorralYSupervisorEnfermeria(enfermeriaID);
                int result = Create("EnfermeriaCorral_InactivarCorralesSupervisores", parameters);
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
