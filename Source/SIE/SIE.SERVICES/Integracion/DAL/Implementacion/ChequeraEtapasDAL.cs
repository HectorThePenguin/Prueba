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
using SIE.Services.Integracion.DAL.Integracion.Mapeos;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ChequeraEtapasDAL: DALBase
    {
        internal IList<ChequeraEtapasInfo> ObtenerTodos()
        {
            IList<ChequeraEtapasInfo> lista = null;
            try
            {
                DataSet ds = Retrieve("ChequeraEtapas_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    lista = MapChequeraEtapasDAL.ObtenerTodos(ds);
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
            return lista;
        }
    }
}
