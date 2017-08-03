using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AuxSalidaGanadoDAL
    {
        /// <summary>
        /// Metodo para mapear los parametros para valdiar si existe la venta en la salida Ganado
        /// </summary>
        /// <param name="salidaGanadoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosExisteVentaEnSalidaGanado(SalidaGanadoInfo salidaGanadoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@VentaGanadoID", salidaGanadoInfo.VentaGanado.VentaGanadoID}
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
        /// Mapear el objeto Salida Ganado para guardarlo en base de datos
        /// </summary>
        /// <param name="salidaGanadoInfo"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosSalidaGanado(SalidaGanadoInfo salidaGanadoInfo, int tipoFolio)
        {
            try
            {
                Logger.Info();
                object ventaGanadoID = null;
                if (salidaGanadoInfo.VentaGanado != null)
                {
                    ventaGanadoID = salidaGanadoInfo.VentaGanado.VentaGanadoID;
                }
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TipoMovimientoID", (int)salidaGanadoInfo.TipoMovimiento},
                            {"@OrganizacionID", salidaGanadoInfo.Organizacion.OrganizacionID},
                            {"@VentaGanadoID", ventaGanadoID},
                            {"@TipoFolio", tipoFolio},
                            {"@Activo", salidaGanadoInfo.Activo},
                            {"@UsuarioCreacionID", salidaGanadoInfo.UsuarioCreacionID}
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
