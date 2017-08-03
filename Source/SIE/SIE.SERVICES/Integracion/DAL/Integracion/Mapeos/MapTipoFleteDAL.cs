
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
    internal class MapTipoFleteDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoFleteInfo> ObtenerTodos(DataSet ds)
        {
            List<TipoFleteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new TipoFleteInfo
                         {
                             TipoFleteId = info.Field<int>("TipoFleteID"),
                             Descripcion = info.Field<string>("Descripcion")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de tipos de flete
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>Lista de TipoContratoInfo</returns>
        internal static TipoFleteInfo ObtenerPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                TipoFleteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoFleteInfo
                         {
                             TipoFleteId = info.Field<int>("TipoFleteID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
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
