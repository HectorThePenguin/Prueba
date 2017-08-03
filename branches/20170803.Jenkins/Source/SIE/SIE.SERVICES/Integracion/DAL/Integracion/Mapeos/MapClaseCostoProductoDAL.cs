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
    internal class MapClaseCostoProductoDAL
    {
        /// <summary>
        /// Obtiene una lista de Clase Costo Producto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ClaseCostoProductoInfo> ObtenerClaseCostoProductoPorAlmacen(DataSet ds)
        {
            List<ClaseCostoProductoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ClaseCostoProductoInfo
                                    {
                                        AlmacenID = info.Field<int>("AlmacenID"),
                                        ProductoID = info.Field<int>("ProductoID"),
                                        CuentaSAPID = info.Field<int>("CuentaSAPID"),
                                        Almacen = new AlmacenInfo
                                                      {
                                                          AlmacenID = info.Field<int>("AlmacenID")
                                                      },
                                        Producto = new ProductoInfo
                                                       {
                                                           ProductoId = info.Field<int>("ProductoID")
                                                       },
                                        CuentaSAP = new CuentaSAPInfo
                                                        {
                                                            CuentaSAPID = info.Field<int>("CuentaSAPID")
                                                        },
                                        ClaseCostoProductoID = info.Field<int>("ClaseCostoProductoID")
                                    }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        internal static IList<ClaseCostoProductoInfo> ObtenerClaseCostoProductoPorAlmacen(IDataReader reader)
        {
            var lista = new List<ClaseCostoProductoInfo>();
            try
            {
                Logger.Info();
                ClaseCostoProductoInfo elemento;
                while (reader.Read())
                {
                    elemento = new ClaseCostoProductoInfo
                                   {
                                       AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                                       ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                       CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"]),
                                       Almacen = new AlmacenInfo
                                                     {
                                                         AlmacenID = Convert.ToInt32(reader["AlmacenID"])
                                                     },
                                       Producto = new ProductoInfo
                                                      {
                                                          ProductoId = Convert.ToInt32(reader["ProductoID"])
                                                      },
                                       CuentaSAP = new CuentaSAPInfo
                                                       {
                                                           CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"])
                                                       },
                                       ClaseCostoProductoID = Convert.ToInt32(reader["ClaseCostoProductoID"])
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

        /// <summary>
        /// Obtiene una clase producto
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static ClaseCostoProductoInfo ObtenerClaseCostoPorProductoAlmacen(IDataReader reader)
        {
            try
            {
                Logger.Info();
                ClaseCostoProductoInfo elemento = null;
                while (reader.Read())
                {
                    elemento = new ClaseCostoProductoInfo
                    {
                        AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                        ProductoID = Convert.ToInt32(reader["ProductoID"]),
                        CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"]),
                        Almacen = new AlmacenInfo
                        {
                            AlmacenID = Convert.ToInt32(reader["AlmacenID"])
                        },
                        Producto = new ProductoInfo
                        {
                            ProductoId = Convert.ToInt32(reader["ProductoID"])
                        },
                        CuentaSAP = new CuentaSAPInfo
                        {
                            CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"]),
                            Descripcion = Convert.ToString(reader["CuentaSAPDescripcion"]),
                            CuentaSAP = Convert.ToString(reader["CuentaSAP"]),
                            Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum()
                        },
                        ClaseCostoProductoID = Convert.ToInt32(reader["ClaseCostoProductoID"]),
                        Activo = Convert.ToBoolean(reader["CuentaSAPActivo"]).BoolAEnum()
                    };
                }
                return elemento;
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
        public static ResultadoInfo<ClaseCostoProductoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ClaseCostoProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ClaseCostoProductoInfo
                         {
                             ClaseCostoProductoID = info.Field<int>("ClaseCostoProductoID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Producto") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), CuentaSAP = info.Field<string>("CuentaSAP"), Descripcion = info.Field<string>("CuentaSAPDescripcion")},
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ClaseCostoProductoInfo>
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
        public static List<ClaseCostoProductoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ClaseCostoProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ClaseCostoProductoInfo
                         {
                             ClaseCostoProductoID = info.Field<int>("ClaseCostoProductoID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
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
        public static ClaseCostoProductoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ClaseCostoProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ClaseCostoProductoInfo
                         {
                             ClaseCostoProductoID = info.Field<int>("ClaseCostoProductoID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
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
        public static ClaseCostoProductoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ClaseCostoProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ClaseCostoProductoInfo
                         {
                             ClaseCostoProductoID = info.Field<int>("ClaseCostoProductoID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAP") },
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
