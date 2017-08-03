using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ProduccionFormulaResumenBL
    {
        /// <summary>
        /// Obtiene de la formula ingresada y para la organizacion del usuario, cuanto hay en inventario y cuanto esta programada para repartir
        /// </summary>
        internal ProduccionFormulaInfo ConsultarFormulaId(ProduccionFormulaResumenInfo produccionFormulaResumenInfo)
        {
            try
            {
                Logger.Info();
                var produccionFormulaResumenDAL = new ProduccionFormulaResumenDAL();
                ProduccionFormulaInfo result = produccionFormulaResumenDAL.ConsultarFormulaId(produccionFormulaResumenInfo);
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
