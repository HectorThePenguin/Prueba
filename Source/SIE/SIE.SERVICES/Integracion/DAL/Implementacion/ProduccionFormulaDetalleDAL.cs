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

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProduccionFormulaDetalleDAL:DALBase
    {
        /// <summary>
        /// Guardar detalle de la produccion de formula
        /// </summary>
        /// <param name="produccionFormulaDetalle"></param>
        /// <returns></returns>
        internal bool GuardarProduccionFormulaDetalle(List<ProduccionFormulaDetalleInfo> produccionFormulaDetalle)
        {
            bool retorno = true;

            try
            {
                Logger.Info();
                var parameters = AuxProduccionFormulaDetalleDAL.GuardarProduccionFormulaDetalle(produccionFormulaDetalle);
                Create("ProduccionFormulaDetalle_Crear", parameters);
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
            return retorno;
        }
    }
}
