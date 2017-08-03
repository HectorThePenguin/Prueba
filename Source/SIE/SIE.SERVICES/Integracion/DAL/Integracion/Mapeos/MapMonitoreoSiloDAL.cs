using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapMonitoreoSiloDAL
    {
        ///     Metodo que obtiene un registro
        internal static List<MonitoreoSiloInfo> ObtenerSilosParaMonitoreo(DataSet ds)
        {
            List<MonitoreoSiloInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new MonitoreoSiloInfo
                         {
                             SiloDescripcion = info.Field<string>("Descripcion"),
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<MonitoreoSiloInfo> ObtenerOrdenAlturaMonitoreoGrid(DataSet ds)
        {
            List<MonitoreoSiloInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new MonitoreoSiloInfo
                         {
                             UbicacionSensor = info.Field<int>("UbicacionSensor"),
                             AlturaSilo = info.Field<int>("AlturaSilo"),
                             OrdenSensor = info.Field<int>("OrdenSensor")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        ///// Metodo que obtiene un registro MonitoreoSiloID
        //internal int Guardar(DataSet ds)
        //{
        //    int resultado = 0;
        //    List<MonitoreoSiloInfo> lista;
        //    try
        //    {
        //        Logger.Info();
        //        DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

        //        lista = (from info in dt.AsEnumerable()
        //                 select new MonitoreoSiloInfo
        //                 {
        //                     SiloDescripcion = info.Field<string>("Descripcion"),
        //                 }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }

        //    return resultado;
        //}
    }
}
