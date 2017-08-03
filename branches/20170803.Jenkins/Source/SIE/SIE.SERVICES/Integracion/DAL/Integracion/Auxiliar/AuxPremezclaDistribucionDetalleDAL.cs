using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxPremezclaDistribucionDetalleDAL
    {
        /// <summary>
        /// Obtiene los parametros para guardar la Premezcla distribucion detalle
        /// </summary>
        /// <param name="premezclaDistribucionDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarPremezclaDistribucionDetalle(PremezclaDistribucionDetalleInfo premezclaDistribucionDetalle)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PremezclaDistribucionId", premezclaDistribucionDetalle.PremezclaDistribucionId},
                            {"@OrganizacionId", premezclaDistribucionDetalle.OrganizacionId},
                            {"@CantidadASurtir", premezclaDistribucionDetalle.CantidadASurtir},
                            {"@AlmacenMovimientoId", premezclaDistribucionDetalle.AlmacenMovimientoId},
                            {"@UsuarioCreacionId", premezclaDistribucionDetalle.UsuarioCreacionId}
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
