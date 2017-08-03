
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapMermaEsperadaDAL
    {
        /// <summary>
        /// Obtiene una lista paginada por OrganizacionOrigenID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<MermaEsperadaInfo> ObtenerMermaPorOrganizacionOrigenID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<MermaEsperadaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MermaEsperadaInfo
                         {
                             OrganizacionOrigen = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionOrigenID"),
                                 Descripcion = info.Field<string>("OrganizacionOrigen"),
                             },
                             OrganizacionDestino = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionDestinoID"),
                                 Descripcion = info.Field<string>("OrganizacionDestino"),
                             },
                             Merma = info.Field<decimal>("Merma")
                         }).ToList();

                
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
