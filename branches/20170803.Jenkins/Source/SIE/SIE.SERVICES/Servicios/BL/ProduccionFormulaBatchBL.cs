using System;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ProduccionFormulaBatchBL
    {
        /// <summary>
        /// Guarda los datos capturados en el formulario "ProduccionFormula" en la tabla "ProduccionFormulaBatch"
        /// </summary>
        public void GuardarProduccionFormulaBatch(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBatchDAL = new ProduccionFormulaBatchDAL();
                produccionFormulaBatchDAL.GuardarProduccionFormulaBatch(produccionFormula);
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Guarda los datos capturados en el formulario "ProduccionFormula" en la tabla "ProduccionFormulaBatch"
        /// </summary>
        public void GuardarProduccionFormulaBatchLista(ProduccionFormulaInfo produccionFormula, int ProduccionFormulaId)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBatchDAL = new ProduccionFormulaBatchDAL();
                produccionFormulaBatchDAL.GuardarProduccionFormulaBatchLista(produccionFormula, ProduccionFormulaId);
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Valida que no exista la produccion de formula
        /// </summary>
        public ProduccionFormulaBatchInfo ValidarProduccionFormulaBatch(ProcesarArchivoInfo produccionFormula)
        {
            ProduccionFormulaBatchInfo resultado = null;
            try
            {
                Logger.Info();
                var produccionFormulaBatchDAL = new ProduccionFormulaBatchDAL();
                resultado = produccionFormulaBatchDAL.ValidarProduccionFormulaBatch(produccionFormula);
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return resultado;
        }
    }
}
