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
    internal class CriterioSupervisionDAL: DALBase
    {
        /// <summary>
        /// Obtiene la lista de criterios de evaluacion de deteccion
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<CriterioSupervisionInfo> ObtenerCriteriosSupervision()
        {
            ResultadoInfo<CriterioSupervisionInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CriteriosSupervision_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapCriterioSupervisionDAL.ObtenerTodos(ds);
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

        
    }
}
