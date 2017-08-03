using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class GanadoIntensivoDAL : DALBase
    {
        public GanadoIntensivoInfo ObtenerMuerteGanadoIntensivo(CorralInfo corral)
        {
            try
            {
                Dictionary<string, object> parameters = AuxGanadoIntensivoDAL.ObtenerParametrosMuerteGanadoIntensivo(corral);
                DataSet ds = Retrieve("[dbo].[MuerteGanadoIntensivo_ObtenerEntradaGanadoPorCorralID]", parameters);
                GanadoIntensivoInfo corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = new GanadoIntensivoInfo();
                    //corralInfo = MapCorralDAL.ObtenerPorId(ds);
                }
                return corralInfo;
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

        public int Guardar(GanadoIntensivoInfo ganadoIntensivo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxGanadoIntensivoDAL.ObtenerParametrosGuardar(ganadoIntensivo);
                int result = Create("[dbo].[GanadoIntesivo_Guardar]", parameters);
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
