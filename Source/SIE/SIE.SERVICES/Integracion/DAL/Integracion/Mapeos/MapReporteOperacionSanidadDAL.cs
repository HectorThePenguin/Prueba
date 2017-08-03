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
    internal class MapReporteOperacionSanidadDAL
    {
        /// <summary>
        /// Obtiene una lista de Tipo de Ganado Reporte Ejecutivo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContenedorReporteOperacionSanidadInfo> ObtenerConceptosOperacionSanidad(DataSet ds)
        {
            List<ContenedorReporteOperacionSanidadInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ContenedorReporteOperacionSanidadInfo
                         {
                            Concepto = info.Field<string>("Concepto"),
                            DiferenciaDias = info.Field<int>("DiferenciaDias"),
                            TotalEntradas = info.Field<int>("TotalEntradas"),
                            TotalMedicados = info.Field<int>("TotalMedicados")
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
