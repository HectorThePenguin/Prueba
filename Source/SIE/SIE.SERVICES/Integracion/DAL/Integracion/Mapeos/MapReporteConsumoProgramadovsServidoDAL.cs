using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteConsumoProgramadovsServidoDAL
    {
        /// <summary>
        /// Map de los datos del reporte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ReporteConsumoProgramadovsServidoInfo> Generar(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ReporteConsumoProgramadovsServidoInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ReporteConsumoProgramadovsServidoInfo
                         {
                             //Corral Cabezas PesoProyectado DiasEngorda FormulaIDServida Descripcion CantidadProgramada CantidadServida Diferencia  
                             //ConsumoPromedio CPV Fecha Titulo Organizacion
                             Codigo = info.Field<string>("Corral").Trim(),
                             Cabezas = info.Field<int>("Cabezas"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             FormulaIDServida = info.Field<int>("FormulaIDServida"),
                             Descripcion = info.Field<string>("Descripcion").Trim(),
                             CantidadProgramada = info.Field<int>("CantidadProgramada"),
                             CantidadServida = info.Field<int>("CantidadServida"),
                             Diferencia = info.Field<int>("Diferencia"),
                             ConsumoPromedio = info.Field<int>("ConsumoPromedio"),
                             PorcentajeCPV = info.Field<decimal>("CPV"),
                             Fecha = info.Field<DateTime>("Fecha")

                         }).ToList();

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
