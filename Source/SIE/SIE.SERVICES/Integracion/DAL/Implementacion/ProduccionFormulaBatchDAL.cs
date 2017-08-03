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
using System.Collections.Generic;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProduccionFormulaBatchDAL : DALBase
    {
        /// <summary>
        /// Guarda los datos capturados en el formulario "ProduccionFormula" en la tabla "ProduccionFormulaBatch"
        /// </summary>
        internal void GuardarProduccionFormulaBatch (ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProduccionFormulaBatchDAL.GuardarProduccionFormulaBatch(produccionFormula);
                Create("ProduccionFormula_GuardarProduccionFormulaBatch", parameters);
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


        /// <summary>
        /// Guarda los datos capturados en el formulario "ProduccionFormula" en la tabla "ProduccionFormulaBatch"
        /// </summary>
        internal void GuardarProduccionFormulaBatchLista(ProduccionFormulaInfo produccionFormula, int ProduccionFormulaId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProduccionFormulaBatchDAL.GuardarProduccionFormulaBatchLista(produccionFormula, ProduccionFormulaId);
                Create("GuardarProduccionFormulaBatch", parameters);

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
        /// <summary>
        /// Valida que no exista la produccion de formula
        /// </summary>
        internal ProduccionFormulaBatchInfo ValidarProduccionFormulaBatch(ProcesarArchivoInfo produccionFormula)
        {
            ProduccionFormulaBatchInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxProduccionFormulaBatchDAL.ObtenerParametrosValidarProduccionFormulaBatch(produccionFormula);
                DataSet ds = Retrieve("ProduccionFormulaBatch_ValidarBatch", parameters);
                if(ValidateDataSet(ds))
                {
                    resultado = MapProduccionFormulaBatchDAL.ValidarProduccionFormulaBatch(ds);
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
            return resultado;
        }
    }
}
