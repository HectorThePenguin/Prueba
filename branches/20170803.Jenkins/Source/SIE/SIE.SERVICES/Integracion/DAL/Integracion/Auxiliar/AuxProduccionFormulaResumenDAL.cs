using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProduccionFormulaResumenDAL
    {
        /// <summary>
        /// Obtiene de la formula ingresada y para la organizacion del usuario, cuanto hay en inventario y cuanto esta programada para repartir
        /// </summary>
        internal static Dictionary<string, object> ConsultarFormulaId(ProduccionFormulaResumenInfo produccionFormulaResumenInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@organizacionid", produccionFormulaResumenInfo.OrganizacionID},
                            {"@formulaId", produccionFormulaResumenInfo.FormulaID},
                            {"@tipoAlmacenId", produccionFormulaResumenInfo.TipoAlmacenID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


    }
}
