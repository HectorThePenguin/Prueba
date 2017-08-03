using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProduccionFormulaResumenDAL : DALBase
    {
        /// <summary>
        /// Obtiene de la formula ingresada y para la organizacion del usuario, cuanto hay en inventario y cuanto esta programada para repartir
        /// </summary>
        internal ProduccionFormulaInfo ConsultarFormulaId(ProduccionFormulaResumenInfo produccionFormulaResumenInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProduccionFormulaResumenDAL.ConsultarFormulaId(produccionFormulaResumenInfo);
                DataSet ds = Retrieve("ProduccionFormula_Resumen", parameters);
                ProduccionFormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionFormulaResumen.ConsultarFormulaId(ds);
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
