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
    internal class MapEstatusDAL
    {
        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<EstatusInfo> ObtenerListaEstatus(DataSet ds)
        {
            List<EstatusInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new EstatusInfo
                         {
                             EstatusId = info.Field<int>("EstatusID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             DescripcionCorta = info.Field<string>("DescripcionCorta")
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
