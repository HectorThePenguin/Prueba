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
    public class MapReporteMuertesGanadoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<ReporteMuertesGanadoInfo> Generar(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                var detalle = from row in dtDetalle.AsEnumerable()
                              select new
                              {
                                  AnimalId = row.Field<long>("AnimalId"),
                                  Producto = row.Field<string>("Producto"),
                                  Orden = row.Field<int>("Orden"),
                              };
                List<ReporteMuertesGanadoInfo> resultado =
                    (from info in dt.AsEnumerable()
                     let p1 = detalle.Where(e => e.AnimalId == info.Field<long>("AnimalId") && e.Orden == 1).Select(e => e.Producto)
                     let p2 = detalle.Where(e => e.AnimalId == info.Field<long>("AnimalId") && e.Orden == 2).Select(e => e.Producto)
                     let p3 = detalle.Where(e => e.AnimalId == info.Field<long>("AnimalId") && e.Orden == 3).Select(e => e.Producto)
                     select
                         new ReporteMuertesGanadoInfo
                             {
								AnimalID = info.Field<long>("AnimalID"),
								Enfermeria = info.Field<string>("Enfermeria"),
								CorralID = info.Field<int>("CorralID"),
								Codigo = info.Field<string>("Codigo"),
								TipoGanado = info.Field<string>("TipoGanado"),
								FechaLlegada = info["FechaLlegada"] != DBNull.Value ? info.Field<DateTime>("FechaLlegada") : new DateTime(1900,1,1),
								Arete = info.Field<string>("Arete"),
								Origen = info.Field<string>("Origen"),
                                Partida = info.Field<int>("Partida"),
								Sexo = info.Field<string>("Sexo"),
								Peso = info.Field<int>("Peso"),
								DiasEngorda = info.Field<int>("DiasEngorda"),
								Causa = info.Field<string>("Causa"),
								Detector = info.Field<string>("Detector"),
								FechaTratamiento1 =  info["FechaTratamiento1"] != DBNull.Value ?  info.Field<DateTime>("FechaTratamiento1") : new DateTime(1900,1,1),
								MedicamentoAplicado1 =  string.Join("," , p1.ToArray()),
                                FechaTratamiento2 = info["FechaTratamiento2"] != DBNull.Value ? info.Field<DateTime>("FechaTratamiento2") : new DateTime(1900, 1, 1),
                                MedicamentoAplicado2 = string.Join(",", p2.ToArray()),
                                FechaTratamiento3 = info["FechaTratamiento3"] != DBNull.Value ? info.Field<DateTime>("FechaTratamiento3") : new DateTime(1900, 1, 1),
                                MedicamentoAplicado3 = string.Join(",", p3.ToArray()),
								TipoDeteccion = info.Field<string>("TipoDeteccion"),
								Tabla = info.Field<string>("Tabla"),
                                Temperatura = info.Field<decimal>("Temperatura"),
                                Grado = info.Field<int?>("GradoID") ?? 0
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

