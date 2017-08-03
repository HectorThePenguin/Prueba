using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxAjusteDeInventarioDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener diferencias de inventario
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDiferenciasInventario(AlmacenMovimientoInfo almacenMovimientoInfo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenID", almacenMovimientoInfo.AlmacenID},
								{"@AlmacenMovimientoID", almacenMovimientoInfo.AlmacenMovimientoID},
                                {"@OrganizacionID",organizacionID},
                                {"@Activo",(int)EstatusEnum.Activo}
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
