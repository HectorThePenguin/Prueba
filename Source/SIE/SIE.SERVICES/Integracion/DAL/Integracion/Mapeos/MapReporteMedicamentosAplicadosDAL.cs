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
    internal class MapReporteMedicamentosAplicadosDAL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Medicamentos Aplicados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteMedicamentosAplicadosDatos> ObtenerDatosMedicamentosAplicados(DataSet ds)
        {
            List<ReporteMedicamentosAplicadosDatos> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteMedicamentosAplicadosDatos
                         {
                             TipoTratamientoID = info.Field<int>("TipoTratamientoID"),
                             TipoTratamiento = info.Field<string>("TipoTratamiento"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Producto = info.Field<string>("Producto"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Precio = info.Field<decimal>("Precio"),
                             Unidad = info.Field<string>("Unidad"),
                             Contar = info.Field<int>("ContadorCabezas"),
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
        /// Mapea los datos para poder generar el dataset que será enviado a "RptReporteMedicamentosAplicados.rpt" para obtener el reporte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteMedicamentosAplicadosDatos> ObtenerDatosMedicamentosCabezasAplicados(DataSet ds)
        {
            List<ReporteMedicamentosAplicadosDatos> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteMedicamentosAplicadosDatos
                         {
                             TipoTratamientoID = info.Field<int>("TipoTratamientoID"),
                             TipoTratamiento = info.Field<string>("TipoTratamiento"),
                             ProductoID = info.Field<int>("ProductoID"),
                             Producto = info.Field<string>("Producto"),
                             Cantidad = info.Field<decimal>("Cantidad"),
                             Precio = info.Field<decimal>("Precio"),
                             Unidad = info.Field<string>("Unidad"),
                             AnimalID = info.Field<int>("AnimalID"),
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
