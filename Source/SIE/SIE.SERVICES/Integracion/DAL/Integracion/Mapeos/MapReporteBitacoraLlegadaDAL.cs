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
    internal class MapReporteBitacoraLlegadaDAL
    {
        /// <summary>
        /// Mapea los datos que devuelve el SP de Reporte Bitacora Llegada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteBitacoraLlegadaInfo> ObtenerDatosBitacoraLlegada(DataSet ds)
        {
            List<ReporteBitacoraLlegadaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteBitacoraLlegadaInfo
                         {
                             Folio = info.Field<int>("Folio"),
                             Empresa = info.Field<string>("Empresa"),
                             Chofer = info.Field<string>("Chofer"),
                             AccesoA = info.Field<string>("AccesoA"),
                             Marca = info.Field<string>("Marca"),
                             Placa = info.Field<string>("Placa"),
                             Color = info.Field<string>("Color"),
                             Producto = info.Field<string>("Producto"),
                             PorcentajeHumedad = info.Field<decimal>("PorcentajeHumedad"),
                             Llegada = info.Field<DateTime>("Llegada"),
                             Entrada = info.Field<DateTime>("EntradaGanadera"),
                             Pesaje = info.Field<DateTime>("FechaPesaje"),
                             Destara = info.Field<DateTime>("FechaDestara"),
                             InicioDescarga = info.Field<DateTime>("FechaInicioDescarga"),
                             FinDescarga = info.Field<DateTime>("FechaFinDescarga"),
                             TiempoEstandar = info.Field<TimeSpan>("TiempoEstandar")
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
