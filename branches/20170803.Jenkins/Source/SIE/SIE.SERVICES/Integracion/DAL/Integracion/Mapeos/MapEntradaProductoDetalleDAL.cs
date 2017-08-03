using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapEntradaProductoDetalleDAL 
    {
        /// <summary>
        /// Obtiene el resultado de la consulta de los indicadores para cada entrada producto Id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoDetalleInfo> ObtenerDetalleEntradaProductosPorIdEntrada(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoDetalleInfo()
                         {
                             EntradaProductoId = info.Field<int>("EntradaProductoID"),
                             EntradaProductoDetalleId = info.Field<int>("EntradaProductoDetalleID"),
                             Indicador = new IndicadorInfo { IndicadorId = info.Field<int>("IndicadorID") },
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo
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
