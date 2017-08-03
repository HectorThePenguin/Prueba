using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxBitacoraErroresDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para guardar en la bitacorra de errores
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarError(BitacoraErroresInfo errorInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AccionesSiapID", (int)errorInfo.AccionesSiapID},
                            {"@Mensaje", errorInfo.Mensaje},
                            {"@UsuarioCreacionID", errorInfo.UsuarioCreacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los parametros para obtener a los usuarios que se les notificara
        /// </summary>
        /// <param name="accionSiap">Accion siap</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosNotificacionesPorAcciones(AccionesSIAPEnum accionSiap)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AccionesSiapID", (int)accionSiap},
                            {"@Activo", (int)EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitacora"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarErrorIncidencia(BitacoraIncidenciaInfo bitacora)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlertaID",bitacora.Alerta.AlertaID },
                            {"@Folio",bitacora.Folio},
                            {"@OrganizacionID",bitacora.Organizacion.OrganizacionID},
                            {"@Error",bitacora.Error}
                            
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
