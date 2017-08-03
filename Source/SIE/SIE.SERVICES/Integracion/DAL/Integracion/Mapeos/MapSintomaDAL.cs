using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapSintomaDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<SintomaInfo> ObtenerPorProblema(DataSet ds)
        {
            IList<SintomaInfo> sintomaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                sintomaInfo = (from sintoma in dt.AsEnumerable()
                               select new SintomaInfo
                                {
                                    SintomaID = sintoma.Field<int>("SintomaID"),
                                    Descripcion = sintoma.Field<string>("Descripcion"),
                                    Activo = sintoma.Field<bool>("Activo").BoolAEnum()
                                }).ToList<SintomaInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return sintomaInfo;
        }
    }
}
