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
    public class MapEntradaProductoMuestrasDAL 
    {
        /// <summary>
        /// Obtiene el resultado de la consulta de las muestras de cada indicador para cada entrada producto Id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EntradaProductoMuestraInfo> ObtenerEntradaProductoMuestraPorEntradaDetalleId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaProductoMuestraInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaProductoMuestraInfo()
                         {
                             EntradaProductoMuestraId = info.Field<int>("EntradaProductoMuestraID"),
                             EntradaProductoDetalleId = info.Field<int>("EntradaProductoDetalleID"),
                             Descuento = info.Field<decimal>("Descuento"),
                             Porcentaje = info.Field<decimal>("Porcentaje"),
                             Rechazo = info.Field<bool>("Rechazo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                             Activo = info.Field<bool>("Activo") ? EstatusEnum.Activo : EstatusEnum.Inactivo,
                             EsOrigen = info.Field<bool>("EsOrigen") ? EsOrigenEnum.Origen : EsOrigenEnum.Destino 
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
