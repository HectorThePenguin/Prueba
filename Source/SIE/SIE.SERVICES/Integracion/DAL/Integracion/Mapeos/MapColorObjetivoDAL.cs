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
    public class MapColorObjetivoDAL
    {
        internal static List<ColorObjetivoInfo> ObtenerPorSemaforo(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ColorObjetivoInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ColorObjetivoInfo
                             {
                                 TipoObjetivoCalidad =
                                     new TipoObjetivoCalidadInfo
                                         {
                                             TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID"),
                                         },
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Tendencia = info.Field<string>("Tendencia"),
                                 CodigoColor = info.Field<string>("CodigoColor"),
                             }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
