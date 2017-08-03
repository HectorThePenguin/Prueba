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
    internal  class MapCriterioSupervisionDAL
    {
        /// <summary>
        /// Obtiene la lista de criterios de supervision
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CriterioSupervisionInfo> ObtenerTodos(DataSet ds)
        {
            ResultadoInfo<CriterioSupervisionInfo> resultado = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<CriterioSupervisionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CriterioSupervisionInfo
                         {
                             CriterioSupervisionId = info.Field<int>("CriterioSupervisionId"),
                             CodigoColor = info.Field<string>("CodigoColor").Trim(),
                             Descripcion = info.Field<string>("Descripcion").Trim(),
                             ValorInicial = info.Field<int>("ValorInicial"),
                             ValorFinal = info.Field<int>("ValorFinal"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                             
                         }).ToList();

                resultado = new ResultadoInfo<CriterioSupervisionInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(lista.Count())
                };
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
