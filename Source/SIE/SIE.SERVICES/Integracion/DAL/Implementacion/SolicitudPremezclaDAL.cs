
using System;
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
    public class SolicitudPremezclaDAL:DALBase
    {
        internal SolicitudPremezclaInfo Guardar(SolicitudPremezclaInfo solicitud)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudPremezclaDAL.ObtenerParametrosGuardar(solicitud);
                var ds = Retrieve("SolicitudPremezcla_Guardar", parameters);
                SolicitudPremezclaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudPremezclaDAL.ObtenerSolicitud(ds);
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
