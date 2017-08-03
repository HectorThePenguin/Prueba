using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteDiarioInventarioAlCierre
    {

        public static ReporteDiarioInventarioAlCierreDatos ObtenerDatosReporte(DataSet ds)
        {
            ReporteDiarioInventarioAlCierreDatos resultado;
            try
            {
                Logger.Info();
                DataTable oTable = ds.Tables[0];

                resultado = new ReporteDiarioInventarioAlCierreDatos
                {
                    ListaReporteInventario = (from movs in oTable.AsEnumerable()
                                              select new ReporteDiarioInventarioAlCierreInfo
                                              {
                                                  ProductoId = movs.Field<int>("ProductoID"),
                                                  Ingrediente = movs.Field<string>("Ingrediente"),
                                                  TMExisAlmacenPA = movs.Field<decimal>("TMExisAlmacenPA"),
                                                  TMExisAlmacenMP = movs.Field<decimal>("TMExisAlmacenMP"),
                                                  TMInvTotalPAyMA = movs.Field<decimal>("TMInvTotalPAyMA"),
                                                  TMConsumoDia = movs.Field<decimal>("TMConsumoDia"),
                                                  DiasCobertura = movs.Field<decimal>("DiasCobertura"),
                                                  CapacidadAlamacenajeDias = movs.Field<decimal>("CapacidadAlmacenajeDias"),
                                                  DiasCoberturaFaltante = movs.Field<decimal>("DiasCoberturaFaltante"),
                                                  MinimoDiasReorden = movs.Field<decimal>("MinimoDiasReorden"),
                                                  TMCapacidadAlmacenaje = movs.Field<decimal>("TMCapacidadAlmacenaje"),
                                                  EstatusReorden = movs.Field<string>("EstatusReorden")
                                              }).ToList()
                };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
