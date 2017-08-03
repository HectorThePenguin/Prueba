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
   internal class MapReporteEntradasSinCosteoDal
    {
        /// <summary>
        /// Obtiene una lista de Reporte Entradas Sin costeo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteEntradasSinCosteoInfo> Generar(DataSet ds)
        {
            List<ReporteEntradasSinCosteoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ReporteEntradasSinCosteoInfo
                         {
                             Folio= info.Field<int>("Folio"),
                             Fecha=info.Field<DateTime>("Fecha"),
                             Producto= info.Field<string>("Producto"),
                             Peso=info.Field<int>("Peso"),
                             Observaciones = info.Field<string>("Observaciones"),
                             AlmacenDestino = info.Field<string>("AlmacenDestino"),
                             LoteDestino= info.Field<int>("LoteDestino"),
                             Proveedor= info.Field<string>("Proveedor"),
                             Organizacion = new OrganizacionInfo {Descripcion = info.Field<string>("Organizacion") }
                         }).ToList();

                foreach (ReporteEntradasSinCosteoInfo datos in lista)
                {
                    datos.OrganizacionDescripcion = datos.Organizacion.Descripcion;
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
