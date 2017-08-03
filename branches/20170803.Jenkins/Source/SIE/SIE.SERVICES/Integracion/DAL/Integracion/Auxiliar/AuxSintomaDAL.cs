using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSintomaDAL
    {
        /// <summary>
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="problemaID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorProblema(int problemaID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ProblemaID", problemaID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
