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
    internal class MapReporteDetalleCorteDAL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Medicamentos Aplicados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteDetalleCorteDatos> ObtenerParametrosDatosReporteDetalleCorte(DataSet ds)
        {
            List<ReporteDetalleCorteDatos> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteDetalleCorteDatos
                         {
                             AreteID = info.Field<string>("Arete"),
                             Descripcion = info.Field<string>("Descripcion"),
                             CorralOrigen = info.Field<string>("CorralOrigen"),
                             CorralDestino = info.Field<string>("CorralDestino"),
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoCorte = info.Field<int>("PesoCorte"),
                             Merma = info.Field<decimal>("Merma"),
                             Temperatura = info.Field<decimal>("Temperatura"),
                             FolioEntrada = info.Field<int>("FolioEntrada")
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
