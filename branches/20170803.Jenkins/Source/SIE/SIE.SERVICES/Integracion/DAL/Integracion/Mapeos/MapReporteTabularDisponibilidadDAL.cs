using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;


namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteTabularDisponibilidadDAL
    {
        /// <summary>
        /// Obtiene una lista de tabular disponibilidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteTabularDisponibilidadInfo> ObtenerReporteTabularDisponibilidad(DataSet ds)
        {
            List<ReporteTabularDisponibilidadInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteTabularDisponibilidadInfo
                         {
                             Codigo =info.Field<string>("Codigo"),
                             LoteID = info.Field<int>("LoteID"),
                             Cabezas =info.Field<int>("Cabezas"),
                             Descripcion= info.Field<string>("Descripcion"),
                             FechaCierre = info["FechaCierre"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaCierre"),
                             FechaDisponibilidadProyectada = info["FechaDisponibilidadProyectada"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaDisponibilidadProyectada"),
                             FechaDisponibilidad = info["FechaDisponibilidad"] == DBNull.Value ? new DateTime(1900,1,1) : info.Field<DateTime>("FechaDisponibilidad"),
                             DisponibilidadManual = info.Field<bool>("DisponibilidadManual") == true ? 1 : 0,
                             //TipoServicioID = info.Field<int>("TipoServicioID"),
                             PesoTotalLote = info.Field<int>("PesoTotalLote"),
                             PesoPromedio=info.Field<int>("PesoPromedio"),
                             Sexo= info.Field<string>("Sexo"),
                             FormulaIDServida = info.Field<string>("FormulaIDServida"),
                             Semana = info.Field<int>("Semana"),
                             FechaInicioSemana = info.Field<DateTime>("FechaInicioSemana")
                         }).ToList();
                //FormulaIDServida = info["FormulaIDServida"] == DBNull.Value ? 0 : info.Field<int>("FormulaIDServida"),
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