using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEntradaPremezclaDAL
    {
        /// <summary>
        /// Metodo para almacenar los movimientos que genera la premezcla
        /// </summary>
        /// <param name="entradaPremezclaInfo"></param>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(EntradaPremezclaInfo entradaPremezclaInfo)
        {
            try
            {
                //Validar si la observacion es nula
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoIDEntrada", entradaPremezclaInfo.AlmacenMovimientoIDEntrada},
                            {"@AlmacenMovimientoIDSalida", entradaPremezclaInfo.AlmacenMovimientoIDSalida},
                            {"@UsuarioCreacionID", entradaPremezclaInfo.UsuarioCreacionID}
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
        /// Funcion que obtiene los parametros para consultar por movimiento de entrada
        /// </summary>
        /// <param name="entradaPremezclaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorMovimientoEntrada(AlmacenMovimientoInfo almacenMovimiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AlmacenMovimientoIDEntrada", almacenMovimiento.AlmacenMovimientoID}
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
