using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapBitacoraErroresDAL
    {
        /// <summary>
        /// Obtiene los datos de las notificaciones por accion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<NotificacionesInfo> ObtnerNotificacionesPorAccion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<NotificacionesInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new NotificacionesInfo
                         {
                             NotificionID = info.Field<int>("NotificacionID"),
                             AccionesSiapID = info.Field<int>("AccionesSiapID"),
                             UsuarioDestino = info.Field<string>("UsuarioDestino"),
                            
                      
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
