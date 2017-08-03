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
    internal class EstatusDAL : DALBase
    {

        internal List<EstatusInfo> ObtenerEstatusTipoEstatus(int tipoEstatus)
        {
            List<EstatusInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEstatusDAL.ObtenerParametrosObtenerEstatusTipoEstatus(tipoEstatus);
                DataSet ds = Retrieve("Estatus_ObtenerPorTipoEstatusID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapEstatusDAL.ObtenerListaEstatus(ds);
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
