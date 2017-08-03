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
    internal  class MapConceptoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista Costo Embarque Detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<ConceptoInfo> ObtenerListadoDeConceptos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConceptoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConceptoInfo
                         {
                             ConceptoID = info.Field<int>("ConceptoDeteccionID"),
                             ConceptoDescripcion = info.Field<string>("Descripcion")
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
