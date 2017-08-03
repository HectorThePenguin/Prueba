using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Base.Infos;


namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    class MapReporteEntradaSalidaZilmaxDAL
    {
        /// <summary>
        /// Obtiene una Lista de corrales entrantes y salientes Zilmax
        /// de Resultados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteEntradaSalidaZilmaxInfo> ObtenerTodos(DataSet ds)
        {
            List<ReporteEntradaSalidaZilmaxInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                select new ReporteEntradaSalidaZilmaxInfo
                {
                    Corral = info.Field<string>("Corral"),
                    Estatus = info.Field<string>("Estatus"),
                    Tipo = info.Field<string>("Tipo"),
                    Formula = info.Field<string>("Formula"),
                    Cbz = info.Field<int>("Cbz"),
                    LoteID = info.Field<int>("LoteID")
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
