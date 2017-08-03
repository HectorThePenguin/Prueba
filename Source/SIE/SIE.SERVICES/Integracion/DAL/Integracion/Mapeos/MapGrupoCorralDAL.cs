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
    public class MapGrupoCorralDAL
    {
        /// <summary>
        /// Obtiene la lista de Grupo Corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<GrupoCorralInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<GrupoCorralInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoCorralInfo
                         {
                             GrupoCorralID = info.Field<int>("GrupoCorralID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
