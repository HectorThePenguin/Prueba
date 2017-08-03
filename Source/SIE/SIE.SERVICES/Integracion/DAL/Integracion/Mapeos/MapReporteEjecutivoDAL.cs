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
    internal class MapReporteEjecutivoDAL
    {
        /// <summary>
        /// Obtiene una lista de Tipo de Ganado Reporte Ejecutivo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoGanadoReporteEjecutivoInfo> ObtenerTipoGanadoReporteEjecutivo(DataSet ds)
        {
            List<TipoGanadoReporteEjecutivoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new TipoGanadoReporteEjecutivoInfo
                         {
                             Cabezas = info.Field<int>("Cabezas"),
                             EntradaGanadoCosteoId = info.Field<int>("EntradaGanadoCosteoID"),
                             PesoBruto = info.Field<decimal>("PesoBruto"),
                             PesoLlegada = info.Field<decimal>("PesoLlegada"),
                             PesoOrigen = info.Field<decimal>("PesoOrigen"),
                             PesoTara = info.Field<decimal>("PesoTara"),
                             PrecioKilo = info.Field<decimal>("PrecioKilo"),
                             Sexo = info.Field<string>("Sexo").StringASexoEnum(),
                             TipoGanado = info.Field<string>("TipoGanado"),
                             TipoGanadoId = info.Field<int>("TipoGanadoID"),
                             Merma = info.Field<decimal>("Merma"),
                             CostoIntegrado = info.Field<decimal>("CostoIntegrado")
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
        /// Obtiene una Lista de Costos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CostosReporteEjecutivoInfo> ObtenerCostosReporteEjecutivo(DataSet ds)
        {
            List<CostosReporteEjecutivoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new CostosReporteEjecutivoInfo
                         {
                             Costo = info.Field<string>("Descripcion"),
                             CostoId = info.Field<int>("CostoID"),
                             EntradaGanadoCosteoId = info.Field<int>("EntradaGanadoCosteoID"),
                             Importe = info.Field<decimal>("Importe")
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
