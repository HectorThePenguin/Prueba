using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProduccionFormulaResumenPL
    {
        /// <summary>
        /// Obtiene de la formula ingresada y para la organizacion del usuario, cuanto hay en inventario y cuanto esta programada para repartir
        /// </summary>
        public ProduccionFormulaInfo ConsultarFormulaId(ProduccionFormulaResumenInfo produccionFormulaResumenInfo)
        {
            try
            {
                Logger.Info();
                var produccionFormulaResumenBL = new ProduccionFormulaResumenBL();
                ProduccionFormulaInfo result = produccionFormulaResumenBL.ConsultarFormulaId(produccionFormulaResumenInfo);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
