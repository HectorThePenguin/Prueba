using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteEstadoComederoDAL
    {
        internal static void MetodoBase(Action accion)
        {
            try
            {
                Logger.Info();
                accion();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<Info.Modelos.AlimentacionCorralPorFormulaModel> ObtenerCorralesPorFormula(System.Data.DataSet ds)
        {
            List<Info.Modelos.AlimentacionCorralPorFormulaModel> lista = new List<Info.Modelos.AlimentacionCorralPorFormulaModel>();
            MetodoBase(() =>
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = DataRow2CorralPorFormulaModel(dt).ToList();
            });
            return lista;
        }

        internal static List<Info.Modelos.AlimentacionCorralPorEstadoComederoModel> ObtenerCorralesPorEstadoComedero(DataSet ds)
        {
            List<Info.Modelos.AlimentacionCorralPorEstadoComederoModel> lista = new List<Info.Modelos.AlimentacionCorralPorEstadoComederoModel>();
            MetodoBase(() =>
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = DataRow2CorralPorEstadoComederoModel(dt).ToList();
            });
            return lista;
        }

        internal static AlimentacionEstadoComederoModel Generar(DataSet ds)
        {
            var modelo = new AlimentacionEstadoComederoModel();

            MetodoBase(() =>
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                modelo.DatosReporteComederoInfo = new System.Collections.ObjectModel.ObservableCollection<AlimentacionEstadoComederoInfo>(MapearDatos(dt));

                dt = ds.Tables[ConstantesDAL.DtDetalle];
                modelo.FechaServidorBD =  obtenerfecha(dt);
            });
            return modelo;
        }

        private static IEnumerable<AlimentacionEstadoComederoInfo> MapearDatos(DataTable dt)
        {
            var query = from dr in dt.AsEnumerable()
                         select new AlimentacionEstadoComederoInfo
                         {
                             Corral = dr.Field<string>("Corral"),
                             Lote = dr.Field<string>("Lote"),
                             TipoGanado = dr.Field<string>("TipoGanado"),
                             Cabezas = dr.Field<int>("Cabezas"),
                             DiasEngorda = dr.Field<int>("DiasEngorda"),
                             PesoProyectado = dr.Field<int>("PesoProyectado"),
                             DiasUltimaFormula = dr.Field<int>("DiasUltimaFormula"),
                             Promedio5Dias = dr.Field<decimal>("Promedio5Dias"),
                             EstadoComederoHoy = dr.Field<int>("EstadoComederoHoy"),
                             AlimentacionProgramadaMatutinaHoy = dr.Field<int>("AlimentacionProgramadaMatutinaHoy"),
                             FormulaProgramadaMatutinaHoy = dr.Field<string>("FormulaProgramadaMatutinaHoy"),
                             AlimentacionProgramadaVespertinaHoy = dr.Field<int>("AlimentacionProgramadaVespertinaHoy"),
                             FormulaProgramadaVespertinaHoy = dr.Field<string>("FormulaProgramadaVespertinaHoy"),
                             TotalProgramadoHoy = dr.Field<int>("TotalProgramadoHoy"),
                             EstadoComederoRealAyer = dr.Field<int>("EstadoComederoRealAyer"),
                             AlimentacionRealMatutinaAyer = dr.Field<int>("AlimentacionRealMatutinaAyer"),
                             FormulaRealMatutinaAyer = dr.Field<string>("FormulaRealMatutinaAyer"),
                             AlimentacionRealVespertinoAyer = dr.Field<int>("AlimentacionRealVespertinoAyer"),
                             FormulaRealVespertinoAyer = dr.Field<string>("FormulaRealVespertinoAyer"),
                             TotalRealAyer = dr.Field<int>("TotalRealAyer"),
                             Kilogramos3Dias = dr.Field<int>("Kilogramos3Dias"),
                             ConsumoCabeza3Dias = dr.Field<decimal>("ConsumoCabeza3Dias"),
                             EstadoComedero3Dias = dr.Field<int>("EstadoComedero3Dias"),
                             Kilogramos4Dias = dr.Field<int>("Kilogramos4Dias"),
                             ConsumoCabeza4Dias = dr.Field<decimal>("ConsumoCabeza4Dias"),
                             EstadoComedero4Dias = dr.Field<int>("EstadoComedero4Dias"),
                             Kilogramos5Dias = dr.Field<int>("Kilogramos5Dias"),
                             ConsumoCabeza5Dias = dr.Field<decimal>("ConsumoCabeza5Dias"),
                             EstadoComedero5Dias = dr.Field<int>("EstadoComedero5Dias"),
                             Kilogramos6Dias = dr.Field<int>("Kilogramos6Dias"),
                             ConsumoCabeza6Dias = dr.Field<decimal>("ConsumoCabeza6Dias"),
                             EstadoComedero6Dias = dr.Field<int>("EstadoComedero6Dias"),
                             Kilogramos7Dias = dr.Field<int>("Kilogramos7Dias"),
                             ConsumoCabeza7Dias = dr.Field<decimal>("ConsumoCabeza7Dias"),
                             EstadoComedero7Dias = dr.Field<int>("EstadoComedero7Dias")
                         };
            return query;
        }
        /// <summary>
        /// Por requerimiento, se solicita que se genere el reporte con la fecha del servidor y no con la fecha del PC del usuario
        /// Por lo tanto, se recupera la fecha del servidor (sql) para mostrarse en el reporte
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static DateTime obtenerfecha(DataTable dt)
        {
            var columna = dt.Columns["FechaReporte"].Ordinal;
            return Convert.ToDateTime(dt.Rows[0][columna]);
        }

        private static IEnumerable<Info.Modelos.AlimentacionCorralPorFormulaModel> DataRow2CorralPorFormulaModel(DataTable dt)
        {
            var query = from dr in dt.AsEnumerable()
                        select new Info.Modelos.AlimentacionCorralPorFormulaModel
                        {
                            FormulaID = dr.Field<int>("FormulaID"),
                            FormulaDescripcion = dr.Field<string>("FormulaDescripcion"),
                            TotalCorrales = dr.Field<int>("TotalCorrales")
                        };
            return query;
        }

        private static IEnumerable<Info.Modelos.AlimentacionCorralPorEstadoComederoModel> DataRow2CorralPorEstadoComederoModel(DataTable dt)
        {
            var query = from dr in dt.AsEnumerable()
                        select new Info.Modelos.AlimentacionCorralPorEstadoComederoModel
                        {
                            EstadoComederoID = dr.Field<int>("EstadoComederoID"),
                            EstadoComederoDescripcion = dr.Field<string>("EstadoComederoDescripcion"),
                            TotalCorrales = dr.Field<int>("TotalCorrales")
                        };
            return query;
        }
    }
}
