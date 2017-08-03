using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class FechaDAL : DALBase
    {
        /// <summary>
        /// Obtiene la fecha actual
        /// </summary>
        internal FechaInfo ObtenerFechaActual()
        {
            FechaInfo result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Fecha_ObtenerActual");
                if (ValidateDataSet(ds))
                {
                    result = MapFechaDAL.ObtenerFechaActual(ds);
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
