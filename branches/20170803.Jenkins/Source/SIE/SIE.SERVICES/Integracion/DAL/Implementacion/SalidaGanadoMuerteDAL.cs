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
    class SalidaGanadoMuerteDAL:DALBase
    {
        /// <summary>
        /// Obtiene el folio para el reporte de salida por muerte 
        /// </summary>
        /// <param name="salidaGanadoMuertoInfo"></param>
        /// <returns></returns>
        internal int ObtenerFolio(SalidaGanadoMuertoInfo salidaGanadoMuertoInfo)
        {
            int Folio = 0;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaGanadoMuertoDAL.ObtenerParametrosObtenerFolio(salidaGanadoMuertoInfo);
                DataSet ds = Retrieve("SalidaGanadoMuerto_ObtenerFolio", parameters);
                if (ValidateDataSet(ds))
                {
                    Folio = (int)ds.Tables[0].Rows[0][0];
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
            return Folio;
        }

        /// <summary>
        /// Asigna el folio a las muertes relacionadas con el reporte
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="MuerteID"></param>
        internal void AsignarFolioMuerte(IList<SalidaGanadoMuertoInfo> listaSalidaGanadoMuerto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaGanadoMuertoDAL.ObtenerParametrosListaAsignarFolio(listaSalidaGanadoMuerto);
                Update("SalidaGanadoMuerto_AsignaFolio", parameters);
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
