using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapReporteKardexGanadoDal
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ReporteKardexGanadoInfo> Generar(DataSet ds)
        {
            List<ReporteKardexGanadoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteKardexGanadoInfo
                         {
                             //SELECT CorralId, Codigo, LoteId, TipoCorralId, Lote, Tipo, CabezasInicial, KgsInicial, CabezasEntradas, KgsEntradas, CabezasSalidas, 
                             //KgsSalidas, CabezasFinal,
			                 //KgsFinal, CostoInv, Promedio
                             CorralId = info.Field<int>("CorralId"),
                             Codigo = info.Field<string>("Codigo").Trim(),
                             LoteId = info.Field<int>("LoteId"),
                             TipoCorralId = info.Field<int>("TipoCorralId"),
                             Lote = info.Field<string>("Lote").Trim(),
                             Tipo = info.Field<string>("Tipo").Trim(),
                             CabezasInicial = info.Field<int>("CabezasInicial"),
                             CabezasSalidas = info.Field<int>("CabezasSalidas"),
                             CabezasEntradas = info.Field<int>("CabezasEntradas"),
                             CabezasFinal = info.Field<int>("CabezasFinal"),
                             KgsInicial = info.Field<int>("KgsInicial"),
                             KgsEntradas = info.Field<int>("KgsEntradas"),
                             KgsSalidas = info.Field<int>("KgsSalidas"),
                             KgsFinal = info.Field<int>("KgsFinal"),
                             CostoInv = info.Field<decimal>("CostoInv"),
                             Promedio = info.Field<decimal>("Promedio")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}
