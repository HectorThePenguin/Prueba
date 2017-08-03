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
    internal class GenerarArchivoDatalinkDAL : DALBase
    {
        /// <summary>
        /// Obtiene datos de reparto y reparto detalle
        /// </summary>
        /// <returns></returns>
        internal List<GenerarArchivoDatalinkInfo> ObtenerDatosRepartoDetalle(RepartoInfo repartoInfo, int tipoServicioID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxGenerarArchivoDatalinkDAL.ObtenerParametrosObtenerRepartoDetalle(repartoInfo, tipoServicioID);
                var ds = Retrieve("GenerarArchivoDataLink_ObtenerRepartoDetalle", parameters);
                List<GenerarArchivoDatalinkInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapGenerarArchivoDatalinkDAL.ObtenerRepartoDetalle(ds);
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
