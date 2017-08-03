using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;


namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTipoCostoCentroDAL
    {
        /// <summary>
        /// Mapeo para Tipo costos de centro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoCostoCentroInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoCostoCentroInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCostoCentroInfo
                         {
                             TipoCostoCentroID = info.Field<int>("TipoCostoCentroID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
