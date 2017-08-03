using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapCondicionJaulaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<CondicionJaulaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CondicionJaulaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CondicionJaulaInfo
                             {
                                 CondicionJaulaID = info.Field<int>("CondicionJaulaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CondicionJaulaInfo>
                        {
                            Lista = lista,
                            TotalRegistros = totalRegistros
                        };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un mapeo de condiciones jaula
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<CondicionJaulaInfo> ObtenerMapeoCondicionJaula()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<CondicionJaulaInfo> condicionJaula = MapBuilder<CondicionJaulaInfo>.MapNoProperties();
                condicionJaula.Map(x => x.CondicionJaulaID).ToColumn("CondicionJaulaID");
                condicionJaula.Map(x => x.Descripcion).ToColumn("Descripcion");
                condicionJaula.Map(x => x.Activo).WithFunc(x => Convert.ToBoolean(x["Activo"]).BoolAEnum());
                return condicionJaula;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
 