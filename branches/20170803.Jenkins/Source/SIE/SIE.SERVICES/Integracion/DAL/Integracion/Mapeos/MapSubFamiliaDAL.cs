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
    internal class MapSubFamiliaDAL
    {
        /// <summary>
        /// Obtiene Info de SubFamilia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SubFamiliaInfo ObtenerPorID(DataSet ds)
        {
            SubFamiliaInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new SubFamiliaInfo
                                        {
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            Descripcion = info.Field<string>("DescripcionSubFamilia"),
                                            Familia = new FamiliaInfo
                                                          {
                                                              FamiliaID = info.Field<int>("FamiliaID"),
                                                              Descripcion = info.Field<string>("DescripcionFamilia")
                                                          },
                                            SubFamiliaID = info.Field<int>("SubFamiliaID")
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene Info de Resultado correspondiente
        /// a la Familia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<SubFamiliaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<SubFamiliaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<SubFamiliaInfo> lista = (from info in dt.AsEnumerable()
                                              select new SubFamiliaInfo
                                                         {
                                                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                             Descripcion = info.Field<string>("DescripcionSubFamilia"),
                                                             Familia = new FamiliaInfo
                                                                           {
                                                                               FamiliaID = info.Field<int>("FamiliaID"),
                                                                               Descripcion =
                                                                                   info.Field<string>(
                                                                                       "DescripcionFamilia")
                                                                           },
                                                             SubFamiliaID = info.Field<int>("SubFamiliaID")
                                                         }).ToList();
                resultado = new ResultadoInfo<SubFamiliaInfo>
                                {
                                    Lista = lista,
                                    TotalRegistros =
                                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SubFamiliaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SubFamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SubFamiliaInfo
                             {
                                 Familia =
                                     new FamiliaInfo
                                         {
                                             FamiliaID = info.Field<int>("FamiliaID"),
                                             Descripcion = info.Field<string>("Familia")
                                         },
                                 SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
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

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SubFamiliaInfo> Centros_ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SubFamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SubFamiliaInfo
                         {
                             Familia =
                                 new FamiliaInfo
                                 {
                                     FamiliaID = info.Field<int>("FamiliaID"),
                                     Descripcion = info.Field<string>("Familia")
                                 },
                             SubFamiliaID = info.Field<int>("SubFamiliaID"),
                             Descripcion = info.Field<string>("Descripcion"),
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

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SubFamiliaInfo> ObtenerPorFamiliaID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SubFamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SubFamiliaInfo
                             {
                                 Familia =
                                     new FamiliaInfo
                                         {
                                             FamiliaID = info.Field<int>("FamiliaID"),
                                             Descripcion = info.Field<string>("Familia")
                                         },
                                 SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
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

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SubFamiliaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SubFamiliaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SubFamiliaInfo
                             {
                                 Familia =
                                     new FamiliaInfo
                                         {
                                             FamiliaID = info.Field<int>("FamiliaID")
                                         },
                                 SubFamiliaID = info.Field<int>("SubFamiliaID"),
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

        /// <summary>
        /// Obtiene una SubFamilia por Familia(s)
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static SubFamiliaInfo ObtenerPorIDPorFamilia(DataSet ds)
        {
            SubFamiliaInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new SubFamiliaInfo
                             {
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Descripcion = info.Field<string>("DescripcionSubFamilia"),
                                 Familia = new FamiliaInfo
                                 {
                                     FamiliaID = info.Field<int>("FamiliaID"),
                                     Descripcion = info.Field<string>("DescripcionFamilia")
                                 },
                                 SubFamiliaID = info.Field<int>("SubFamiliaID")
                             }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un resultado de SubFamilia
        /// por su Familia(s)
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<SubFamiliaInfo> ObtenerPorPaginaPorFamilia(DataSet ds)
        {
            ResultadoInfo<SubFamiliaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<SubFamiliaInfo> lista = (from info in dt.AsEnumerable()
                                              select new SubFamiliaInfo
                                              {
                                                  Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                  Descripcion = info.Field<string>("DescripcionSubFamilia"),
                                                  Familia = new FamiliaInfo
                                                  {
                                                      FamiliaID = info.Field<int>("FamiliaID"),
                                                      Descripcion =
                                                          info.Field<string>(
                                                              "DescripcionFamilia")
                                                  },
                                                  SubFamiliaID = info.Field<int>("SubFamiliaID")
                                              }).ToList();
                resultado = new ResultadoInfo<SubFamiliaInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
