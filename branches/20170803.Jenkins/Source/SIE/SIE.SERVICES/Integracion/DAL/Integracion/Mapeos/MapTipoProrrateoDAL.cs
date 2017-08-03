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
    internal static class MapTipoProrrateoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoProrrateoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<TipoProrrateoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoProrrateoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                                 select new TipoProrrateoInfo
                                             {
                                                 TipoProrrateoID = groupinfo.Field<int>("TipoProrrateoID"),
                                                 Descripcion = groupinfo.Field<string>("Descripcion"),
                                                 Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                             }).ToList();
                resultado = new ResultadoInfo<TipoProrrateoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoProrrateoInfo ObtenerPorID(DataSet ds)
        {
            TipoProrrateoInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new TipoProrrateoInfo
                             {
                                 TipoProrrateoID = costinfo.Field<int>("TipoProrrateoID"),
                                 Descripcion = costinfo.Field<string>("Descripcion"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum()
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

        /// <summary>
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<TipoProrrateoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoProrrateoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoProrrateoInfo
                         {
                             TipoProrrateoID = info.Field<int>("TipoProrrateoID"),
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