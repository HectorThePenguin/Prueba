
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using System.Linq;
namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapEntradaPremezclaDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaPremezclaInfo> ObtenerPorMovimientoEntrada(DataSet ds)
        {
            List<EntradaPremezclaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new EntradaPremezclaInfo
                                 {
                                     AlmacenMovimientoIDEntrada = info.Field<long>("AlmacenMovimientoIDEntrada"),
                                     AlmacenMovimientoIDSalida = info.Field<long>("AlmacenMovimientoIDSalida"),
                                     Activo = info.Field<bool>("Activo").BoolAEnum()
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
