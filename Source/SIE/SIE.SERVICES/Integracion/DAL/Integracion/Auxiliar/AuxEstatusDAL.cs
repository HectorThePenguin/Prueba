using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEstatusDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener un Almacen por su Id
        /// </summary>
        /// <param name="tipoEstatusID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerEstatusTipoEstatus(int tipoEstatusID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TipoEstatusID", tipoEstatusID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()},
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
