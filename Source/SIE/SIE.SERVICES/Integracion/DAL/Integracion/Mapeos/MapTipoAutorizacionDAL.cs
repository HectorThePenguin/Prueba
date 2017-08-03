using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTipoAutorizacionDAL
    {
        /// <summary>
        /// Mapeo de obtener por organizacion y tipo de almacen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<MovimientosAutorizarModel> ObtenerMovimientosAutorizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MovimientosAutorizarModel
                         {
                             TipoAutorizacionID = info.Field<int>("TipoAutorizacionID"),
                             Descripcion = info.Field<string>("Descripcion"),
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
