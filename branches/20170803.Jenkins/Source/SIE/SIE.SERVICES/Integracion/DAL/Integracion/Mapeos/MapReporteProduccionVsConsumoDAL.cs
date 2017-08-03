using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
   internal class MapReporteProduccionVsConsumoDal
    {
        /// <summary>
        /// Obtiene una lista de Reporte Produccion Vs Consumo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteProduccionVsConsumoInfo> Generar(DataSet ds)
        {
            List<ReporteProduccionVsConsumoInfo> lista;
            try
            {
                Logger.Info();
                
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ReporteProduccionVsConsumoInfo
                         {
                             Descripcion = info.Field<string>("Descripcion"),
                             CantidadEnPlanta = info.Field<decimal>("CantidadEnPlanta"),
                             CantidadDelReparto = info.Field<int>("CantidadDeReparto"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"),Descripcion =info.Field<string>("Organizacion") }
                         }).ToList();

                foreach(ReporteProduccionVsConsumoInfo datos  in lista)
                {
                    datos.OrganizacionDescripcion = datos.Organizacion.Descripcion;
                    datos.Diferencial=datos.CantidadEnPlanta-datos.CantidadDelReparto;
                    datos.Fisico=0;
                    datos.AplicarADiario=datos.Fisico-datos.Diferencial;
                    datos.AplicadosSemana=0;
                    datos.DiferencialSemanal=datos.AplicadosSemana+datos.AplicarADiario;
                }

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
