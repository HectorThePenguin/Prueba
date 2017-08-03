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
    internal class MapReporteDiaDiaCalidadDAL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Medicamentos Aplicados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ReporteDiaDiaCalidadInfo> ObtenerDatosDiaDiaCalidad(DataSet ds)
        {
            IList<ReporteDiaDiaCalidadInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteDiaDiaCalidadInfo()
                         {
                             Indicador = info.Field<string>("Indicador"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Producto = info.Field<string>("Producto"),
                             RangoObjetivo = info.Field<string>("RangoObjetivo"),
                             Resultado = info.Field<decimal>("Resultado")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene la informaciojn de otros analisis
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ReporteDiaDiaCalidadAnalisisInfo> ObtenerDatosDiaDiaCalidadAnalisis(DataSet ds)
        {
            IList<ReporteDiaDiaCalidadAnalisisInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteDiaDiaCalidadAnalisisInfo()
                         {
                             Clave = info.Field<string>("Clave"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Resultados = info.Field<decimal>("Resultado"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Rango = info.Field<string>("Rango")
                            
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
