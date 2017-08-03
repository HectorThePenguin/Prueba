using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTipoCancelacionDAL
    {
        /// <summary>
        /// Metodo para recuperar la lista de tipos de cancelacion del dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoCancelacionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoCancelacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCancelacionInfo
                         {
                             TipoCancelacionId = info.Field<int>("TipoCancelacionID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             DiasPermitidos = info.Field<int>("DiasPermitidos"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
