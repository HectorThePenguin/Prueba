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
    internal class MapFormulaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<FormulaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FormulaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoFormula =
                                     new TipoFormulaInfo
                                         {
                                             TipoFormulaID = info.Field<int>("TipoFormulaID"),
                                             Descripcion = info.Field<string>("TipoFormula")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<FormulaInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FormulaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FormulaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoFormula =
                                     new TipoFormulaInfo
                                         {
                                             TipoFormulaID = info.Field<int>("TipoFormulaID"),
                                             Descripcion = info.Field<string>("TipoFormula")
                                         },
                                 Producto = new ProductoInfo
                                     {
                                         ProductoId = info.Field<int>("ProductoID"),
                                         ProductoDescripcion = info.Field<string>("Producto")
                                     },
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
        internal static FormulaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                FormulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoFormula =
                                     new TipoFormulaInfo
                                         {
                                             TipoFormulaID = info.Field<int>("TipoFormulaID"),
                                             Descripcion = info.Field<string>("TipoFormula")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         },
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FormulaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                FormulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoFormula =
                                     new TipoFormulaInfo
                                         {
                                             TipoFormulaID = info.Field<int>("TipoFormulaID"),
                                             Descripcion = info.Field<string>("TipoFormula")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         },
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
        ///     Metodo que obtiene formulas por TipoFormulaId
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<FormulaInfo> ObtenerPorFormulaId(DataSet ds)
        {
            IList<FormulaInfo> formulaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                formulaInfo = (from formula in dt.AsEnumerable()
                               select new FormulaInfo
                               {
                                   FormulaId = formula.Field<int>("FormulaID"),
                                   Descripcion = formula.Field<string>("Descripcion"),
                                   TipoFormulaId = formula.Field<int>("TipoFormulaID")

                               }).ToList<FormulaInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return formulaInfo;
        }

        internal static List<FormulaInfo> ObtenerFormulaDescripcionPorIDs(DataSet ds)
        {
            List<FormulaInfo> formulaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                formulaInfo = (from formula in dt.AsEnumerable()
                               select new FormulaInfo
                               {
                                   FormulaId = formula.Field<int>("FormulaID"),
                                   Descripcion = formula.Field<string>("Descripcion")
                               }).ToList<FormulaInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return formulaInfo;
        }

        /// <summary>
        /// Mapeo de objeto Formula correspondiente a Calidad Pase Proceso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FormulaInfo ObtenerPorFormulaIDCalidadPaseProceso(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                FormulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                                 Descripcion = info.Field<string>("Formula"),
                                 Producto =
                                     new ProductoInfo
                                         {
                                             SubFamilia = new SubFamiliaInfo
                                                              {
                                                                  Descripcion = info.Field<string>("SubFamilia")
                                                              },
                                             ProductoId = info.Field<int>("ProductoID")
                                         },
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static ResultadoInfo<FormulaInfo> ObtenerPorPaseCalidadPaginado(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FormulaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                             {
                                 FormulaId = info.Field<int>("FormulaID"),
                                 Descripcion = info.Field<string>("Formula"),
                                 Producto = new ProductoInfo
                                                {
                                                    ProductoId = info.Field<int>("ProductoID"),
                                                    SubFamilia = new SubFamiliaInfo
                                                                     {
                                                                         Descripcion = info.Field<string>("SubFamilia")
                                                                     }
                                                }
                             }).ToList();
                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<FormulaInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static IList<FormulaInfo> ObtenerTodos(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var lista = new List<FormulaInfo>();
                FormulaInfo elemento;
                while (reader.Read())
                {
                    elemento = new FormulaInfo
                                   {
                                       FormulaId = Convert.ToInt32(reader["FormulaID"]),
                                       Descripcion = Convert.ToString(reader["Descripcion"]),
                                       TipoFormula =
                                           new TipoFormulaInfo
                                               {
                                                   TipoFormulaID = Convert.ToInt32(reader["TipoFormulaID"]),
                                                   Descripcion = Convert.ToString(reader["TipoFormula"])
                                               },
                                       Producto = new ProductoInfo
                                                      {
                                                          ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                                          ProductoDescripcion = Convert.ToString(reader["Producto"]),
                                                          Descripcion = Convert.ToString(reader["Producto"])
                                                      },
                                       Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                                   };
                    lista.Add(elemento);
                }
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
        internal static List<FormulaInfo> ObtenerParametrosFormulasConfiguradas(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FormulaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FormulaInfo
                         {
                             FormulaId = info.Field<int>("FormulaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoFormula =
                                 new TipoFormulaInfo
                                 {
                                     TipoFormulaID = info.Field<int>("TipoFormulaID"),
                                     Descripcion = info.Field<string>("TipoFormula")
                                 },
                             Producto = new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 ProductoDescripcion = info.Field<string>("Producto")
                             },
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
        ///<summary>
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        internal static IList<RotoMixInfo> ObtenerRotoMixConfiguradas(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RotoMixInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RotoMixInfo
                         {
                             RotoMixId = info.Field<int>("RotoMixID"),
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
        ///<summary>
        /// Obtiene el número de batch que deberá mostrarse en el texbox "txtBatch"
        /// Este dato se inicializado en 1, por rotomix y por día.
        /// </summary>
        /// <returns></returns>
        internal static int CantidadBatch(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                RotoMixInfo lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RotoMixInfo
                         {
                             Contador = info.Field<int>("Batch"),
                         }).First();
                return lista.Contador;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
