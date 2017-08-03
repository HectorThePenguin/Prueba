using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMenuDAL
    {
        /// <summary>
        /// Obtiene los parametros para ejecutar el SP que obtiene 
        /// las pantallas a las que tiene permiso el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="aplicacionWeb">Indica si se esta accediento desde la aplicacion Web</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorUduario(string usuario, bool aplicacionWeb)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@UsuarioActiveDirectory", usuario},
                        {"@AplicacionWeb", aplicacionWeb}
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