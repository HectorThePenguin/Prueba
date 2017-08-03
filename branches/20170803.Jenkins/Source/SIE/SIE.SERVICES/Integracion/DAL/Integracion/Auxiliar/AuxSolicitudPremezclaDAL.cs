using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSolicitudPremezclaDAL
    {
        /// <summary>
        /// Obtiene los parametros para crear una solicitud
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        internal static Dictionary<string,object> ObtenerParametrosGuardar(SolicitudPremezclaInfo solicitud)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", solicitud.Organizacion.OrganizacionID},
                            {"@FechaInicio", solicitud.FechaInicio},
                            {"@FechaFin", solicitud.FechaFin},
                            {"@UsuarioCreacionID", solicitud.UsuarioCreacion.UsuarioID},
                            {"@Activo",solicitud.Activo}
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
