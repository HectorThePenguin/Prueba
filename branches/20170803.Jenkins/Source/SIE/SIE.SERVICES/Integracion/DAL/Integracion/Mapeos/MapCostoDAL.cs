using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  static class MapCostoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CostoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<CostoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CostoInfo> lista = (from info in dt.AsEnumerable()
                                         select new CostoInfo
                                                    {
                                                        CostoID = info.Field<int>("CostoID"),
                                                        ClaveContable = info.Field<string>("ClaveContable"),
                                                        Descripcion = info.Field<string>("Descripcion"),
                                                        TipoCosto =
                                                            new TipoCostoInfo
                                                                {
                                                                    TipoCostoID = info.Field<int>("TipoCostoID"),
                                                                    Descripcion = info.Field<string>("TipoCosto")
                                                                },
                                                        TipoProrrateo =
                                                            new TipoProrrateoInfo
                                                                {
                                                                    TipoProrrateoID = info.Field<int>("TipoProrrateoID"),
                                                                    Descripcion = info.Field<string>("TipoProrrateo")
                                                                },
                                                        Retencion =
                                                            new RetencionInfo
                                                                {
                                                                    RetencionID = info.Field<int>("RetencionID"),
                                                                    Descripcion = info.Field<string>("Retencion"),
                                                                    TipoRetencion = info.Field<string>("TipoRetencion"),
                                                                    IndicadorRetencion = info.Field<string>("IndicadorRetencion"),
                                                                    IndicadorImpuesto = info.Field<string>("IndicadorImpuesto")
                                                                },
                                                        AbonoA = info.Field<string>("AbonoA").StringAEnum(),
                                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                        CompraIndividual = info.Field<bool>("CompraIndividual"),
                                                        Recepcion = info.Field<bool>("Recepcion"),
                                                        Compra = info.Field<bool>("Compra"),
                                                        Gasto = info.Field<bool>("Gasto"),
                                                        Costo = info.Field<bool>("Costo"),
                                                        TipoCostoCentro = new TipoCostoCentroInfo
                                                                               {
                                                                                   TipoCostoCentroID = info.Field<int?>("TipoCostoCentroID") != null ? info.Field<int>("TipoCostoCentroID") : 0,
                                                                                   Descripcion = info.Field<string>("DescripcionTipoCostoCentro") != null ? info.Field<string>("DescripcionTipoCostoCentro") : string.Empty
                                                                               },
                                                    }).ToList();
                resultado = new ResultadoInfo<CostoInfo>
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
        internal  static CostoInfo ObtenerPorID(DataSet ds)
        {
            CostoInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new CostoInfo
                             {
                                 CostoID = costinfo.Field<int>("CostoID"),
                                 Descripcion = costinfo.Field<string>("Costo"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum(),
                                 ClaveContable = costinfo.Field<string>("ClaveContable"),
                                 TipoCosto = new TipoCostoInfo
                                     {
                                         TipoCostoID = costinfo.Field<int>("TipoCostoID"),
                                         Descripcion = costinfo.Field<string>("TipoCosto")
                                     },
                                     TipoProrrateo = new TipoProrrateoInfo
                                         {
                                             TipoProrrateoID = costinfo.Field<int>("TipoProrrateoID"),
                                             Descripcion = costinfo.Field<string>("TipoProrrateo")
                                         },
                                 Retencion = new RetencionInfo
                                 {
                                     RetencionID = costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0
                                 }
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
        ///   Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static IList<CostoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CostoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoInfo
                             {
                                 CostoID = info.Field<int>("CostoID"),
                                 ClaveContable = info.Field<string>("ClaveContable"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoCosto =
                                     new TipoCostoInfo
                                     {
                                         TipoCostoID = info.Field<int>("TipoCostoID"),
                                         Descripcion = info.Field<string>("TipoCosto")
                                     },
                                 TipoProrrateo =
                                     new TipoProrrateoInfo
                                     {
                                         TipoProrrateoID = info.Field<int>("TipoProrrateoID"),
                                         Descripcion = info.Field<string>("TipoProrrateo")
                                     },
                                 Retencion =
                                     new RetencionInfo
                                     {
                                         RetencionID = info.Field<int>("RetencionID"),
                                         Descripcion = info.Field<string>("Retencion")
                                     },
                                 AbonoA = info.Field<string>("AbonoA").StringAEnum(),
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CostoInfo ObtenerPorClaveContableTipoCosto(DataSet ds)
        {
            CostoInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new CostoInfo
                             {
                                 CostoID = costinfo.Field<int>("CostoID"),
                                 Descripcion = costinfo.Field<string>("Costo"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum(),
                                 ClaveContable = costinfo.Field<string>("ClaveContable"),
                                 TipoCosto = new TipoCostoInfo
                                 {
                                     TipoCostoID = costinfo.Field<int>("TipoCostoID"),
                                     Descripcion = costinfo.Field<string>("TipoCosto")
                                 },
                                 TipoProrrateo = new TipoProrrateoInfo
                                 {
                                     TipoProrrateoID = costinfo.Field<int>("TipoProrrateoID"),
                                     Descripcion = costinfo.Field<string>("TipoProrrateo")
                                 },
                                 Retencion = new RetencionInfo
                                 {
                                     RetencionID = costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0
                                 }
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CostoInfo> ObtenerPorPaginaTipoCosto(DataSet ds)
        {
            ResultadoInfo<CostoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CostoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                         select new CostoInfo
                                         {
                                             CostoID = groupinfo.Field<int>("CostoID"),
                                             Descripcion = groupinfo.Field<string>("Descripcion"),
                                             Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                             ClaveContable = groupinfo.Field<string>("ClaveContable"),
                                             Retencion = new RetencionInfo
                                             {
                                                 RetencionID = groupinfo.Field<int?>("RetencionID") != null ? groupinfo.Field<int>("RetencionID") : 0
                                             },
                                             TipoCosto = new TipoCostoInfo
                                                 {
                                                     TipoCostoID = groupinfo.Field<int?>("TipoCostoID") != null ? groupinfo.Field<int>("TipoCostoID") : 0
                                                 }
                                         }).ToList();
                resultado = new ResultadoInfo<CostoInfo>
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CostoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CostoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoInfo
                             {
                                 CostoID = info.Field<int>("CostoID"),
                                 ClaveContable = info.Field<string>("ClaveContable"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoCosto =
                                     new TipoCostoInfo
                                         {
                                             TipoCostoID = info.Field<int>("TipoCostoID"),
                                             Descripcion = info.Field<string>("TipoCosto")
                                         },
                                 TipoProrrateo =
                                     new TipoProrrateoInfo
                                         {
                                             TipoProrrateoID = info.Field<int>("TipoProrrateoID"),
                                             Descripcion = info.Field<string>("TipoProrrateo")
                                         },
                                 Retencion =
                                     new RetencionInfo
                                         {
                                             RetencionID = info.Field<int>("RetencionID"),
                                             Descripcion = info.Field<string>("Retencion")
                                         },
                                 AbonoA = info.Field<string>("AbonoA").StringAEnum(),
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

        /// <summary>
        /// Obtiene un Resultado Info de Costo por Clave contable
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CostoInfo> ObtenerPorPaginaClaveContable(DataSet ds)
        {
            ResultadoInfo<CostoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CostoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                         select new CostoInfo
                                         {
                                             CostoID = groupinfo.Field<int>("CostoID"),
                                             Descripcion = groupinfo.Field<string>("Descripcion"),
                                             Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                             ClaveContable = groupinfo.Field<string>("ClaveContable"),
                                             Retencion = new RetencionInfo
                                             {
                                                 RetencionID = groupinfo.Field<int?>("RetencionID") != null ? groupinfo.Field<int>("RetencionID") : 0
                                             }
                                         }).ToList();
                resultado = new ResultadoInfo<CostoInfo>
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
        /// Obtiene los datos por el flete 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<FleteDetalleInfo> ObtenerPorFleteID(DataSet ds)
        {
            List<FleteDetalleInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new FleteDetalleInfo
                             {
                                 
                                     FleteID = info.Field<int>("FleteID"),
                                     FleteDetalleID = info.Field<int>("FleteDetalleID"),
                                     CostoID = info.Field<int>("CostoID"),
                                     Tarifa = info.Field<decimal>("Tarifa"),
                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     Costo = info.Field<string>("Descripcion"),
                                     Opcion = 0,
                                 
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene el costo por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CostoInfo ObtenerCostoPorID(DataSet ds)
        {
            CostoInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new CostoInfo
                             {
                                 CostoID = costinfo.Field<int>("CostoID"),
                                 Descripcion = costinfo.Field<string>("Costo"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum(),
                                 ClaveContable = costinfo.Field<string>("ClaveContable"),
                                 TipoCosto = new TipoCostoInfo
                                 {
                                     TipoCostoID = costinfo.Field<int>("TipoCostoID"),
                                     Descripcion = costinfo.Field<string>("TipoCosto")
                                 },
                                 TipoProrrateo = new TipoProrrateoInfo
                                 {
                                     TipoProrrateoID = costinfo.Field<int>("TipoProrrateoID"),
                                     Descripcion = costinfo.Field<string>("TipoProrrateo")
                                 },
                                 Retencion = new RetencionInfo
                                 {
                                     RetencionID = costinfo.Field<int?>("RetencionID") != null ? costinfo.Field<int>("RetencionID") : 0
                                 },
                                 AbonoA = (AbonoA)Enum.Parse(typeof(AbonoA), costinfo.Field<string>("AbonoA").ToUpper()),
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
        /// Mapea los datos den centro de costo perteneciente al tipo de costo dado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CentroCostoInfo ObtenerCentroCostoSAPPorCosto(DataSet ds)
        {
            CentroCostoInfo retVal = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                retVal = (from centro in dt.AsEnumerable()
                             select new CentroCostoInfo
                             {
                                 CentroCostoID = centro.Field<int>("CentroCostoID"),
                                 CentroCostoSAP = centro.Field<string>("CentroCostoSAP").Trim(),
                                 Descripcion = centro.Field<string>("Descripcion").Trim(),
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return retVal;
        }

        internal static IList<CostoInfo> ObtenerTodos(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var result = new List<CostoInfo>();
                CostoInfo elemento;
                while (reader.Read())
                {
                    elemento = new CostoInfo
                                   {
                                       CostoID = Convert.ToInt32(reader["CostoID"]),
                                       ClaveContable = Convert.ToString(reader["ClaveContable"]),
                                       Descripcion = Convert.ToString(reader["Descripcion"]),
                                       TipoCosto =
                                           new TipoCostoInfo
                                               {
                                                   TipoCostoID = Convert.ToInt32(reader["TipoCostoID"]),
                                                   Descripcion = Convert.ToString(reader["TipoCosto"])
                                               },
                                       TipoProrrateo =
                                           new TipoProrrateoInfo
                                               {
                                                   TipoProrrateoID = Convert.ToInt32(reader["TipoProrrateoID"]),
                                                   Descripcion = Convert.ToString(reader["TipoProrrateo"])
                                               },
                                       Retencion =
                                           new RetencionInfo
                                               {
                                                   RetencionID = Convert.ToInt32(reader["RetencionID"]),
                                                   Descripcion = Convert.ToString(reader["Retencion"])
                                               },
                                       AbonoA = Convert.ToString(reader["AbonoA"]).StringAEnum(),
                                       Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                                   };
                    result.Add(elemento);
                }
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