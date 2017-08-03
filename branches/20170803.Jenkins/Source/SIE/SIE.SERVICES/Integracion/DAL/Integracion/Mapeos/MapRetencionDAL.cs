using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapRetencionDAL
    {

        /// <summary>
        ///     Método  que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<RetencionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<RetencionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new RetencionInfo
                     {
                         RetencionID = info.Field<int>("RetencionId"),
                         Descripcion = info.Field<string>("Descripcion"),
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                     }).ToList();

                var resultado =
                    new ResultadoInfo<RetencionInfo>
                    {
                        Lista = lista,
                        TotalRegistros =
                            Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RetencionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RetencionInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new RetencionInfo
                         {
                             RetencionID = info.Field<int>("RetencionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             TipoRetencion = info.Field<string>("TipoRetencion"),
                             IndicadorRetencion = info.Field<string>("IndicadorRetencion"),
                             IndicadorImpuesto = info.Field<string>("IndicadorImpuesto")
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método  que obtiene un registro de Retencion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RetencionInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                RetencionInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new RetencionInfo
                         {
                             RetencionID = info.Field<int>("RetencionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método  que obtiene una lista de Retencions por tipo de Retencion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RetencionInfo> ObtenerPorTipoRetencionID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<RetencionInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new RetencionInfo
                         {
                             RetencionID = info.Field<int>("RetencionId"),
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

        /// <summary>
        /// Obtiene las retenciones con su costo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RetencionInfo> ObtenerRetencionesConCosto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<RetencionInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new RetencionInfo
                         {
                             RetencionID = info.Field<int?>("RetencionID") != null ? info.Field<int>("RetencionID") : 0,
                             Descripcion = info.Field<string>("Descripcion"),
                             IndicadorImpuesto = info.Field<string>("IndicadorImpuesto"),
                             IndicadorRetencion = info.Field<string>("IndicadorRetencion"),
                             Tasa = info.Field<decimal?>("Tasa") != null ? info.Field<decimal>("Tasa") : 0,
                             TipoRetencion = info.Field<string>("TipoRetencion"),
                             CostoID = info.Field<int?>("CostoID") != null ? info.Field<int>("CostoID") : 0
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
