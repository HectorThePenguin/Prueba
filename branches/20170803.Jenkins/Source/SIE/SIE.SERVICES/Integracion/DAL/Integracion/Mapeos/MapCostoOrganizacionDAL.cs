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
    internal  static class MapCostoOrganizacionDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CostoOrganizacionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CostoOrganizacionInfo> lista =
                    (from groupinfo in dt.AsEnumerable()
                     select
                         new CostoOrganizacionInfo
                             {
                                 CostoOrganizacionID = groupinfo.Field<int>("CostoOrganizacionID"),
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = groupinfo.Field<int>("CostoID"),
                                             Descripcion = groupinfo.Field<string>("Costo"),
                                             ClaveContable = groupinfo.Field<string>("ClaveContable"),
                                             Retencion = new RetencionInfo
                                             {
                                                 RetencionID = groupinfo.Field<int?>("RetencionID") != null ? groupinfo.Field<int>("RetencionID") : 0,
                                                 Descripcion = groupinfo.Field<string>("Retencion") ?? string.Empty
                                             }
                                         },
                                 TipoOrganizacion =
                                     new TipoOrganizacionInfo
                                         {
                                             TipoOrganizacionID = groupinfo.Field<int>("TipoOrganizacionID"),
                                             Descripcion = groupinfo.Field<string>("TipoOrganizacion")
                                         },

                                 Automatico = groupinfo.Field<bool>("Automatico").BoolAutomaticoEnum(),
                                 Activo = groupinfo.Field<bool>("Activo").BoolAEnum()
                             }).ToList();

                var resultado = new ResultadoInfo<CostoOrganizacionInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CostoOrganizacionInfo> ObtenerPorOrganizacion(DataSet ds)
        {
            List<CostoOrganizacionInfo> listaCostoOrganizacionInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                listaCostoOrganizacionInfo =
                    (from costInfo in dt.AsEnumerable()
                     select
                         new CostoOrganizacionInfo
                             {
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = costInfo.Field<int>("CostoID"),
                                             Descripcion = costInfo.Field<string>("Costo"),
                                             AbonoA = costInfo.Field<string>("AbonoA").StringAEnum(),
                                             ClaveContable = costInfo.Field<string>("ClaveContable"),
                                             TipoProrrateo =
                                                 new TipoProrrateoInfo
                                                     {
                                                         TipoProrrateoID = costInfo.Field<int>("TipoProrrateoID")
                                                     },
                                             Retencion = new RetencionInfo
                                             {
                                                 RetencionID = costInfo.Field<int?>("RetencionID") != null ? costInfo.Field<int>("RetencionID") : 0,
                                                 Descripcion = costInfo.Field<string>("Retencion") ?? string.Empty
                                             }
                                         },
                                 Importe = costInfo.Field<decimal>("Importe")
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaCostoOrganizacionInfo;
        }

        internal static IList<CostoOrganizacionInfo> ObtenerTodos(DataSet ds)
        {
            throw new NotImplementedException();
        }

        internal static CostoOrganizacionInfo ObtenerPorID(DataSet ds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Regresa una entidad de Costo Organizacion por
        /// Tipo Organizacion y Costo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CostoOrganizacionInfo PorTipoOrganizacionCosto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                CostoOrganizacionInfo costoOrganizacion =
                    (from groupinfo in dt.AsEnumerable()
                     select
                         new CostoOrganizacionInfo
                             {
                                 CostoOrganizacionID = groupinfo.Field<int>("CostoOrganizacionID"),
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = groupinfo.Field<int>("CostoID"),
                                             Descripcion = groupinfo.Field<string>("Costo"),
                                             ClaveContable = groupinfo.Field<string>("ClaveContable"),
                                             Retencion = new RetencionInfo
                                                             {
                                                                 RetencionID =
                                                                     groupinfo.Field<int?>("RetencionID") != null
                                                                         ? groupinfo.Field<int>("RetencionID")
                                                                         : 0,
                                                                 Descripcion =
                                                                     groupinfo.Field<string>("Retencion") ??
                                                                     string.Empty
                                                             }
                                         },
                                 TipoOrganizacion =
                                     new TipoOrganizacionInfo
                                         {
                                             TipoOrganizacionID = groupinfo.Field<int>("TipoOrganizacionID"),
                                             Descripcion = groupinfo.Field<string>("TipoOrganizacion")
                                         },
                                 Automatico = groupinfo.Field<bool>("Automatico").BoolAutomaticoEnum(),
                                 Activo = groupinfo.Field<bool>("Activo").BoolAEnum()
                             }).FirstOrDefault();
                return costoOrganizacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}