using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapReporteSalidaConCostosDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<ReporteSalidaConCostosInfo> obtenerReporte(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<ReporteSalidaConCostosInfo> resultado;
                
                resultado = (from salida in dt.AsEnumerable()
                             select new ReporteSalidaConCostosInfo
                        {
                            TipoSalida = salida.Field<String>("TipoSalida"),
                            TipoMovimientoID = salida.Field<int>("TipoMovimientoID"),
                            FechaMovimiento = salida.Field<DateTime>("FechaMovimiento"),
                            Corral = salida.Field<String>("Corral"),
                            TipoGanado = salida.Field<String>("tipoGanado"),
                            Arete = salida.Field<String>("Arete"),
                            PesoCompra = salida.Field<int>("PesoCompra"),
                            PesoCanal = salida.Field<Decimal>("PesoCanal"),
                            PesoNoqueo = salida.Field<int>("PesoNoqueo"),
                            PesoPiel = salida.Field<int>("PesoPiel"),
                            PesoVisceras = salida.Field<Decimal>("PesoVisceras"),
                            ImporteCanal = salida.Field<Decimal>("ImporteCanal"),
                            ImportePiel = salida.Field<Decimal>("ImportePiel"),
                            ImporteVisceras = salida.Field<Decimal>("ImporteVisceras"),
                            Costo = salida.Field<String>("DescripcionCosto"),
                            ImporteCosto = salida.Field<Decimal>("ImporteCosto"),
                           
                        }
                    ).ToList<ReporteSalidaConCostosInfo>();

                foreach (ReporteSalidaConCostosInfo reporte in resultado){
                    if(reporte.ImporteCanal > 0 && reporte.PesoCanal > 0)
                        reporte.PrecioCanal = reporte.ImporteCanal / reporte.PesoCanal;

                    if (reporte.ImportePiel > 0 && reporte.PesoPiel > 0)
                        reporte.PrecioPiel = reporte.ImportePiel / reporte.PesoPiel;
                }


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
