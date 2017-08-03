using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    class AuxPremezclaDistribucionCostoDAL
    {

        internal static Dictionary<string, object> ObtenerParametrosGuardarPremezclaDistribucionCosto(PremezclaDistribucionCostoInfo premezclaDistribucionCosto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PremezcaDistribucionCostoID", premezclaDistribucionCosto.PremezcaDistribucionCostoID},
                            {"@PremezclaDistribucionID", premezclaDistribucionCosto.PremezclaDistribucionID},
                            {"@Costo", premezclaDistribucionCosto.Costo},
                            {"@TieneCuenta", premezclaDistribucionCosto.TieneCuenta},
                            {"@Proveedor", premezclaDistribucionCosto.Proveedor},
                            {"@CuentaProvision", premezclaDistribucionCosto.CuentaProvision},
                            {"@Importe", premezclaDistribucionCosto.Importe},
                            {"@Iva", premezclaDistribucionCosto.Iva},
                            {"@Retencion", premezclaDistribucionCosto.Retencion},
                            {"@Activo", premezclaDistribucionCosto.Activo},
                            {"@UsuarioCreacionID", premezclaDistribucionCosto.UsuarioCreacionID},
                            {"@UsuarioModificacionID", premezclaDistribucionCosto.UsuarioModificacionID}
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
