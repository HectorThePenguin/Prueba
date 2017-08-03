using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapCausaRechazoDAL
    {

        /// <summary>
        /// Obtiene una lista de Causas de Rechazo dadas de alta 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CausaRechazoInfo> ObtenerCatalogoCausaRechazo(DataSet ds)
        {
            List<CausaRechazoInfo> causaRechazoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                causaRechazoInfo = (from info in dt.AsEnumerable()
                              select new CausaRechazoInfo
                              {
                                  Descripcion = info.Field<string>("Descripcion")
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return causaRechazoInfo;
        }

    }
}
