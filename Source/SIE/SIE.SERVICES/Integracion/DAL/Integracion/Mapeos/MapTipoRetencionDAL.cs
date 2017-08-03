using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapTipoRetencionDAL
    {
        internal static List<TipoRetencionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoRetencionInfo> lista =
                    (from groupinfo in dt.AsEnumerable()
                     select
                            new TipoRetencionInfo
                                {
                                    TipoRetencionID = groupinfo.Field<int>("TipoRetencionID"),
                                    Descripcion = groupinfo.Field<string>("Descripcion"),
                                    Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                }).ToList();
                return lista;

            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
        }

    }
}
