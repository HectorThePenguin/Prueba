using System;
using System.Collections.Generic;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.PL
{
    public class ProduccionFormulaDetalleBL
    {
        /// <summary>
        /// Guarda los detalles de una produccion de formulas
        /// </summary>
        /// <param name="produccionFormulaDetalle"></param>
        internal bool GuardarProduccionFormulaDetalle(List<ProduccionFormulaDetalleInfo> produccionFormulaDetalle)
        {
            bool retorno = true;

            try
            {
                var produccionFormulaDal = new ProduccionFormulaDetalleDAL();
                retorno = produccionFormulaDal.GuardarProduccionFormulaDetalle(produccionFormulaDetalle);
            }
            catch (ExcepcionDesconocida)
            {
                retorno = false;
                throw;
            }
            catch (Exception ex)
            {
                retorno = false;
                Logger.Error(ex);
            }
            return retorno;
        }
    }
}
