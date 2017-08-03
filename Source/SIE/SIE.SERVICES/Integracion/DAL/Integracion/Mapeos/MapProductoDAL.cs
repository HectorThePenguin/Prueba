using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapProductoDAL
    {
        /// <summary>
        /// Mapea la lista de productos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProductoInfo> ObtenerTodos(DataSet ds)
        {
            ResultadoInfo<ProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                //ProductoID, Descripcion, SubFamiliaID, UnidadID, Activo 
                List<ProductoInfo> lista = (from info in dt.AsEnumerable()
                                            select new ProductoInfo
                                            {
                                                ProductoId = info.Field<int>("ProductoID"),
                                                ProductoDescripcion = info.Field<string>("Descripcion").Trim(),
                                                SubfamiliaId = info.Field<int>("SubFamiliaID"),
                                                UnidadId = info.Field<int>("UnidadID"),
                                                DescripcionUnidad = info.Field<string>("UnidadDescripcion").Trim(),
                                                Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes 
                                            }).ToList();

                resultado = new ResultadoInfo<ProductoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(lista.Count)
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
        /// Mapea los datos desde la consolta de productos por tratamiento al objeto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
       
        internal static ResultadoInfo<ProductoInfo> ObtenerDesdeTratamientos(DataSet ds)
        {
            ResultadoInfo<ProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                int linea = 0;
                List<ProductoInfo> lista = (from info in dt.AsEnumerable()
                                            let renglon = linea++
                                            select new ProductoInfo
                                                       {
                                                           ProductoId = info.Field<int>("ProductoID"),
                                                           ProductoDescripcion =
                                                               info.Field<string>("Descripcion").Trim(),
                                                           SubfamiliaId = info.Field<int>("SubFamiliaID"),
                                                           UnidadId = info.Field<int>("UnidadID"),
                                                           Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                           Dosis = info.Field<int>("Dosis"),
                                                           Renglon = linea,
                                                           TratamientoID = info.Field<int>("TratamientoID")
                                                       }).ToList();
                resultado = new ResultadoInfo<ProductoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(lista.Count)
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProductoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubfamiliaId = info.Field<int>("SubFamiliaID"),
                             DescripcionSubFamilia = info.Field<string>("DescripcionSubFamilia"),
                             UnidadId = info.Field<int>("UnidadID"),
                             DescripcionUnidad = info.Field<string>("DescripcionUnidad"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FamiliaId = info.Field<int>("FamiliaID"),
                             DescripcionFamilia = info.Field<string>("DescripcionFamilia"),
                             ManejaLote = info.Field<bool>("ManejaLote"),
                             ManejaLoteEnum = info.Field<bool>("ManejaLote").BoolManejaLoteEnum(),
                             MaterialSAP = info.Field<string>("MaterialSAP"),
                             UnidadMedicion = new UnidadMedicionInfo
                                                  {
                                                      UnidadID = info.Field<int>("UnidadID"), 
                                                      Descripcion = info.Field<string>("DescripcionUnidad")
                                                  }
                                                  
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoInfo>
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

        internal static ResultadoInfo<ProductoInfo> ObtenerPorPaginaLoteExistencia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             UnidadMedicion = new UnidadMedicionInfo
                                                  {
                                                      UnidadID = info.Field<int>("UnidadID")
                                                  }
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoInfo>
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubfamiliaId = info.Field<int>("SubFamiliaID"),
                             UnidadId = info.Field<int>("UnidadID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes
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
        internal static ProductoInfo ObtenerPorProductoIDSubFamilia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubFamilia = new SubFamiliaInfo
                                 {
                                  SubFamiliaID   = info.Field<int>("SubFamiliaID"),
                                  Descripcion = info.Field<string>("SubFamilia"),
                                 },
                                 Familia = new FamiliaInfo
                                     {
                                         FamiliaID = info.Field<int>("FamiliaID"),
                                         Descripcion = info.Field<string>("Familia")
                                     },
                             UnidadId = info.Field<int>("UnidadID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProductoInfo> ObtenerPorPaginaSubFamilia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubFamilia = new SubFamiliaInfo
                             {
                                 SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                 Descripcion = info.Field<string>("SubFamilia"),
                             },
                             Familia = new FamiliaInfo
                             {
                                 FamiliaID = info.Field<int>("FamiliaID"),
                                 Descripcion = info.Field<string>("Familia")
                             },
                             UnidadId = info.Field<int>("UnidadID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoInfo>
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProductoInfo> Centros_ObtenerPorPaginaSubFamilia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubFamilia = new SubFamiliaInfo
                             {
                                 SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                 Descripcion = info.Field<string>("SubFamilia"),
                             },
                             Familia = new FamiliaInfo
                             {
                                 FamiliaID = info.Field<int>("FamiliaID"),
                                 Descripcion = info.Field<string>("Familia")
                             },
                             UnidadId = info.Field<int>("UnidadID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoInfo>
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
        /// Obtiene un producto por su ID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoInfo ObtenerPorProductoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 ProductoDescripcion = info.Field<string>("Descripcion"),
                                 SubfamiliaId = info.Field<int>("SubFamiliaID"),
                                 DescripcionSubFamilia =  info.Field<string>("SubFamilia"),
                                 SubFamilia = new SubFamiliaInfo
                                                  {
                                                      SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                                      Descripcion = info.Field<string>("SubFamilia"),
                                                  },
                                 FamiliaId = info.Field<int>("FamiliaID"),
                                 DescripcionFamilia = info.Field<string>("Familia"),
                                 Familia = new FamiliaInfo
                                               {
                                                   FamiliaID = info.Field<int>("FamiliaID"),
                                                   Descripcion = info.Field<string>("Familia")
                                               },
                                 UnidadId = info.Field<int>("UnidadID"),
                                 DescripcionUnidad = info.Field<string>("Unidad"),
                                 UnidadMedicion = new UnidadMedicionInfo
                                                      {
                                                          UnidadID = info.Field<int>("UnidadID"),
                                                          Descripcion = info.Field<string>("Unidad"),
                                                          ClaveUnidad = info.Field<string>("ClaveUnidad")
                                                      },
                                 ManejaLote = info.Field<bool>("ManejaLote"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes 
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
        /// Obtiene un producto por su ID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoInfo ObtenerPorProductoIDLoteExistencia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             UnidadMedicion = new UnidadMedicionInfo
                                                  {
                                                      UnidadID = info.Field<int>("UnidadID")
                                                  }
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
        /// Mapea la lista de productos por estado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ProductoInfo> ObtenerProductosEstado(DataSet ds)
        {
            List<ProductoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                                            select new ProductoInfo
                                            {
                                                ProductoId = info.Field<int>("ProductoID"),
                                                ProductoDescripcion = info.Field<string>("ProductoDescripcion").Trim(),
                                                SubfamiliaId = info.Field<int>("SubFamiliaID"),
                                                FamiliaId = info.Field<int>("FamiliaID"),
                                                DescripcionFamilia = info.Field<string>("FamiliaDescripcion"),
                                                UnidadId = info.Field<int>("UnidadID"),
                                                Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<ProductoInfo> ObtenerTodosCompleto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
                             UnidadMedicion = new UnidadMedicionInfo { UnidadID = info.Field<int>("UnidadID"), Descripcion = info.Field<string>("UnidadMedicion") },
                             ManejaLote = info.Field<bool>("ManejaLote"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes
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
        /// Metodo para obtener el producto por pedido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoInfo ObtenerPorPedidoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Producto"),
                             Descripcion = info.Field<string>("Producto"),
                             SubFamilia = new SubFamiliaInfo
                             {
                                 Descripcion = info.Field<string>("SubFamilia"),
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

        internal static ResultadoInfo<ProductoInfo> ObtenerPorPedidoPaginado(DataSet ds)
        {
            ResultadoInfo<ProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista = (from info in dt.AsEnumerable()
                                            select new ProductoInfo
                                            {
                                                ProductoId = info.Field<int>("ProductoID"),
                                                Descripcion = info.Field<string>("Producto"),
                                                ProductoDescripcion = info.Field<string>("Producto"),
                                                SubFamilia = new SubFamiliaInfo
                                                                 {
                                                                     Descripcion = info.Field<string>("SubFamilia")
                                                                 }
                                            }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                resultado = new ResultadoInfo<ProductoInfo>
                {
                    Lista = lista,
                    TotalRegistros = totalRegistros
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
        /// Obtiene un producto por folio de salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoInfo ObtenerPorFolioSalida(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubFamilia = new SubFamiliaInfo
                             {
                                 SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                 Descripcion = info.Field<string>("SubFamilia"),
                             },
                             Familia = new FamiliaInfo
                             {
                                 FamiliaID = info.Field<int>("FamiliaID"),
                                 Descripcion = info.Field<string>("Familia")
                             },
                             UnidadId = info.Field<int>("UnidadID"),
                             UnidadMedicion = new UnidadMedicionInfo { UnidadID = info.Field<int>("UnidadID") },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes
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
        /// Mapea la lista de productos por estado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProductoInfo> ObtenerCompletoPorFamilia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista = (from info in dt.AsEnumerable()
                         select new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("Descripcion").Trim(),
                             ProductoDescripcion = info.Field<string>("Descripcion").Trim(),
                             SubfamiliaId = info.Field<int>("SubFamiliaID"),
                             DescripcionSubFamilia = info.Field<string>("DescripcionSubFamilia").Trim(),
                             UnidadId = info.Field<int>("UnidadID"),
                             DescripcionUnidad = info.Field<string>("DescripcionUnidad").Trim(),
                             FamiliaId = info.Field<int>("FamiliaId"),
                             DescripcionFamilia = info.Field<string>("DescripcionFamilia").Trim(),
                             ManejaLote = info.Field<bool>("ManejaLote"), 
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoInfo>
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

        internal static List<ProductoInfo> ObtenerProductosEstado(IDataReader reader)
        {
            var lista = new List<ProductoInfo>();
            try
            {
                Logger.Info();
                ProductoInfo elemento;
                while (reader.Read())
                {
                    elemento = new ProductoInfo
                                   {
                                       ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                       ProductoDescripcion = Convert.ToString(reader["ProductoDescripcion"]).Trim(),
                                       SubfamiliaId = Convert.ToInt32(reader["SubFamiliaID"]),
                                       FamiliaId = Convert.ToInt32(reader["FamiliaID"]),
                                       DescripcionFamilia = Convert.ToString(reader["FamiliaDescripcion"]),
                                       UnidadId = Convert.ToInt32(reader["UnidadID"]),
                                       DescripcionUnidad = Convert.ToString(reader["UnidadDescripcion"]),
                                       ManejaLote = Convert.ToBoolean(reader["ManejaLote"]),
                                       Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                                       EsPremezcla =
                                           Convert.ToInt32(reader["SubFamiliaID"]) ==
                                           (int) SubFamiliasEnum.MicroIngredientes,
                                       SubFamilia = new SubFamiliaInfo
                                                        {
                                                            SubFamiliaID = Convert.ToInt32(reader["SubFamiliaID"]),
                                                            Descripcion = Convert.ToString(reader["SubFamilia"]),
                                                            Familia = new FamiliaInfo
                                                                          {
                                                                              Descripcion =
                                                                                  Convert.ToString(
                                                                                      reader["FamiliaDescripcion"]),
                                                                              FamiliaID =
                                                                                  Convert.ToInt32(reader["FamiliaID"]),
                                                                          }
                                                        },
                                       Familia = new FamiliaInfo
                                                     {
                                                         Descripcion = Convert.ToString(reader["FamiliaDescripcion"]),
                                                         FamiliaID = Convert.ToInt32(reader["FamiliaID"]),
                                                     }
                                   };
                    lista.Add(elemento);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        internal static ProductoInfo ObtenerPorProductoIDFamilias(DataSet ds)
        {
            ProductoInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                resultado = (from info in dt.AsEnumerable()
                                            select new ProductoInfo
                                            {
                                                ProductoId = info.Field<int>("ProductoID"),
                                                Descripcion = info.Field<string>("Producto"),
                                                ProductoDescripcion = info.Field<string>("Producto"),
                                                SubFamilia = new SubFamiliaInfo
                                                {
                                                    Descripcion = info.Field<string>("SubFamilia")
                                                }
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
        /// Obtiene un producto por indicador
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoInfo ObtenerPorIndicador(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 ProductoDescripcion = info.Field<string>("Descripcion"),
                                 SubFamilia = new SubFamiliaInfo
                                                  {
                                                      SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                                      Descripcion = info.Field<string>("SubFamilia"),
                                                  },
                                 Familia = new FamiliaInfo
                                               {
                                                   FamiliaID = info.Field<int>("FamiliaID"),
                                                   Descripcion = info.Field<string>("Familia")
                                               },
                                 UnidadId = info.Field<int>("UnidadID"),
                                 UnidadMedicion =
                                     new UnidadMedicionInfo
                                         {
                                             UnidadID = info.Field<int>("UnidadID"),
                                             Descripcion = info.Field<string>("UnidadMedicion")
                                         },
                                 IndicadorID = info.Field<int>("IndicadorID")
                             }).FirstOrDefault();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un resultado por indicador
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProductoInfo> ObtenerPorIndicadorPagina(DataSet ds)
        {
            ResultadoInfo<ProductoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista = (from info in dt.AsEnumerable()
                                            select new ProductoInfo
                                            {
                                                ProductoId = info.Field<int>("ProductoID"),
                                                Descripcion = info.Field<string>("Descripcion"),
                                                SubfamiliaId = info.Field<int>("SubFamiliaID"),
                                                UnidadId = info.Field<int>("UnidadID"),
                                                SubFamilia = new SubFamiliaInfo
                                                                 {
                                                                     SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                                                     Descripcion = info.Field<string>("SubFamilia")
                                                                 },
                                                Familia = new FamiliaInfo
                                                              {
                                                                  FamiliaID = info.Field<int>("FamiliaID"),
                                                                  Descripcion = info.Field<string>("Familia")
                                                              },
                                                UnidadMedicion = new UnidadMedicionInfo
                                                                     {
                                                                         UnidadID = info.Field<int>("UnidadID"),
                                                                         Descripcion = info.Field<string>("UnidadMedicion")
                                                                     },
                                                IndicadorID = info.Field<int>("IndicadorID")
                                            }).ToList();
                resultado = new ResultadoInfo<ProductoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(lista.Count)
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
        internal static ProductoInfo ObtenerPorMaterialSAP(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubfamiliaId = info.Field<int>("SubFamiliaID"),
                             UnidadId = info.Field<int>("UnidadID"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsPremezcla = info.Field<int>("SubFamiliaID") == (int)SubFamiliasEnum.MicroIngredientes,
                             MaterialSAP = info.Field<string>("MaterialSAP")
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
        /// Genera una lista de productos a partir de informacion
        /// </summary>
        /// <param name="ds">Dataset con los registros</param>
        /// <returns>Regresa una lista paginada de productos</returns>
        internal static ResultadoInfo<ProductoInfo> ObtenerPorPaginaFiltroSubFamiliaParaEnvioAlimento(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoInfo
                         {
                             ProductoId = info.Field<int>("ProductoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ProductoDescripcion = info.Field<string>("Descripcion"),
                             SubfamiliaId = info.Field<int>("SubFamiliaID"),
                             UnidadId = info.Field<int>("UnidadID"),
                             DescripcionUnidad = info.Field<string>("DescripcionUnidad"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             UnidadMedicion = new UnidadMedicionInfo
                             {
                                 UnidadID = info.Field<int>("UnidadID"),
                                 Descripcion = info.Field<string>("DescripcionUnidad")
                             },
                             ManejaLote = info.Field<bool>("ManejaLote")
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoInfo>
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
    }
}
