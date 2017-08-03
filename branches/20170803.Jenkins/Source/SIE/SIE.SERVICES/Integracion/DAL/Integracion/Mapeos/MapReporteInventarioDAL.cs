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
    internal class MapReporteInventarioDAL
    {
        /// <summary>
        /// Obtiene una lista de Tipo de Ganado Reporte Inventario
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteInventarioInfo> ObtenerTipoGanadoReporteEjecutivo(DataSet ds)
        {
            List<ReporteInventarioInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteInventarioInfo
                                    {
                                        TipoGanado = info.Field<string>("TipoGanado"),
                                        Entradas = info.Field<int>("Entradas"),
                                        InventarioFinal = info.Field<int>("InventarioFinal"),
                                        InventarioInicial = info.Field<int>("InventarioInicial"),
                                        Muertes = info.Field<int>("Muertes"),
                                        Ventas = info.Field<int>("Ventas"),
                                        Sacrificio = info.Field<int>("Sacrificio")
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
