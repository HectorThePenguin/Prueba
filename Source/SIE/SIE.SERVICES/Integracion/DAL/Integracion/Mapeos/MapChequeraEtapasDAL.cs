using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapChequeraEtapasDAL
    {

        internal static List<ChequeraEtapasInfo> ObtenerTodos(DataSet ds)
        {
            List<ChequeraEtapasInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new ChequeraEtapasInfo
                                         {
                                             EtapaId = info.Field<int>("EtapaId"),
                                             Descripcion = info.Field<string>("Descripcion")
                                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

    }
}
