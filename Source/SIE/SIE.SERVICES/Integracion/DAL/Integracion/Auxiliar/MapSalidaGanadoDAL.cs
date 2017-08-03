using System;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class MapSalidaGanadoDAL
    {
        /// <summary>
        /// Mapea el resultado en una Salida Ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SalidaGanadoInfo ObtenerSalidaGanado(DataSet ds)
        {
            SalidaGanadoInfo salidaGanadoInfo = null;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                salidaGanadoInfo = new SalidaGanadoInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    salidaGanadoInfo.SalidaGanadoID = Convert.ToInt32(dr["SalidaGanadoID"]);
                    salidaGanadoInfo.Organizacion = new OrganizacionInfo { OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]) };
                    salidaGanadoInfo.TipoMovimiento = (TipoMovimiento)Convert.ToInt32(dr["TipoMovimientoID"]);
                    salidaGanadoInfo.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    salidaGanadoInfo.Folio = Convert.ToInt32(dr["Folio"]);
                    if (dr["VentaGanadoID"] is DBNull)
                    {
                        salidaGanadoInfo.VentaGanado = new VentaGanadoInfo();
                    }
                    else
                    {
                        salidaGanadoInfo.VentaGanado = new VentaGanadoInfo() { VentaGanadoID = Convert.ToInt32(dr["VentaGanadoID"]) };
                    }
                    
                    salidaGanadoInfo.Activo = Convert.ToInt32(dr["Activo"]) == 0 ? EstatusEnum.Inactivo : EstatusEnum.Activo;
                    salidaGanadoInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    salidaGanadoInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);

                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return salidaGanadoInfo;
        }
    }
}
