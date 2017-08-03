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
    internal class InterfaceSalidaCostoDAL : DALBase
    {
        /// <summary>
        /// Obtener los animales y sus costos en la interface salida
        /// </summary>
        /// <param name="folioOrigen"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        public IList<InterfaceSalidaCostoInfo> ObtenerCostoAnimales(int folioOrigen, int organizacionOrigenID)
        {
            IList<InterfaceSalidaCostoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaCostoDAL.ObtenerParametroCostoAnimales(folioOrigen, organizacionOrigenID);
                DataSet ds = Retrieve("InterfaceSalidaCosto_ObtenerCostoAnimales", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaCostoDAL.ObtenerCostoAnimales(ds);
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
