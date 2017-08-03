using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class SupervisionGanadoDAL : DALBase
    {
        /// <summary>
        /// Guarda los aretes detectados despues de la revision
        /// </summary>
        /// <param name="supervision"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal int GuardarEstatusDeteccion(List<SupervisionGanadoInfo> supervision, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSupervisionGanadoDAL.ObtenerParametrosGuardarSupervisionGanado(supervision, organizacionId);
                Create("SupervisionGanado_GuardarSupervision", parameters);
                return 1;
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
        /// Valida el arete y arete testigo ingresado que no se haya detectado en el dia
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="areteTestigo"></param>
        /// <returns></returns>
        internal int ValidarAreteDetectado(string arete, string areteTestigo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSupervisionGanadoDAL.ObtenerParametrosValidarAretesDetectados( arete, areteTestigo);
                DataSet ds = Retrieve("SupervisionGanado_ValidarAretesDetectados", parameters);
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
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
