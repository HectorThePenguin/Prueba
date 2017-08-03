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
    internal class MapCuentaAlmacenSubFamiliaDAL
    {
        /// <summary>
        /// Obtiene una lista de Cuentas Almacen por SubFamilia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<CuentaAlmacenSubFamiliaInfo> ObtenerCuentaAlmacenSubFamiliaPorAlmacen(DataSet ds)
        {
            List<CuentaAlmacenSubFamiliaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new CuentaAlmacenSubFamiliaInfo
                         {
                             AlmacenID = info.Field<int>("AlmacenID"),
                             SubFamiliaID = info.Field<int>("SubFamiliaID"),
                             CuentaSAPID = info.Field<int>("CuentaSAPID"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info.Field<int>("AlmacenID")
                             },
                             SubFamilia = new SubFamiliaInfo
                             {
                                 SubFamiliaID = info.Field<int>("SubFamiliaID")
                             },
                             CuentaSAP = new CuentaSAPInfo
                             {
                                 CuentaSAPID = info.Field<int>("CuentaSAPID")
                             },
                             CuentaAlmacenSubFamiliaID = info.Field<int>("CuentaAlmacenSubFamiliaID")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        internal static IList<CuentaAlmacenSubFamiliaInfo> ObtenerCuentaAlmacenSubFamiliaPorAlmacen(IDataReader reader)
        {
            var lista = new List<CuentaAlmacenSubFamiliaInfo>();
            try
            {
                Logger.Info();
                CuentaAlmacenSubFamiliaInfo elemento;
                while (reader.Read())
                {
                    elemento = new CuentaAlmacenSubFamiliaInfo
                                   {
                                       AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                                       SubFamiliaID = Convert.ToInt32(reader["SubFamiliaID"]),
                                       CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"]),
                                       Almacen = new AlmacenInfo
                                                     {
                                                         AlmacenID = Convert.ToInt32(reader["AlmacenID"])
                                                     },
                                       SubFamilia = new SubFamiliaInfo
                                                        {
                                                            SubFamiliaID = Convert.ToInt32(reader["SubFamiliaID"])
                                                        },
                                       CuentaSAP = new CuentaSAPInfo
                                                       {
                                                           CuentaSAPID = Convert.ToInt32(reader["CuentaSAPID"])
                                                       },
                                       CuentaAlmacenSubFamiliaID = Convert.ToInt32(reader["CuentaAlmacenSubFamiliaID"])
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CuentaAlmacenSubFamiliaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CuentaAlmacenSubFamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaAlmacenSubFamiliaInfo
                         {
                             CuentaAlmacenSubFamiliaID = info.Field<int>("CuentaAlmacenSubFamiliaID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAPDescripcion"), CuentaSAP = info.Field<string>("CuentaSAP")},
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CuentaAlmacenSubFamiliaInfo>
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
        public static List<CuentaAlmacenSubFamiliaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CuentaAlmacenSubFamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaAlmacenSubFamiliaInfo
                         {
                             CuentaAlmacenSubFamiliaID = info.Field<int>("CuentaAlmacenSubFamiliaID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAPDescripcion"), CuentaSAP = info.Field<string>("CuentaSAP") },
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
        public static CuentaAlmacenSubFamiliaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaAlmacenSubFamiliaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaAlmacenSubFamiliaInfo
                         {
                             CuentaAlmacenSubFamiliaID = info.Field<int>("CuentaAlmacenSubFamiliaID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAPDescripcion"), CuentaSAP = info.Field<string>("CuentaSAP") },
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
        public static CuentaAlmacenSubFamiliaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaAlmacenSubFamiliaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaAlmacenSubFamiliaInfo
                         {
                             CuentaAlmacenSubFamiliaID = info.Field<int>("CuentaAlmacenSubFamiliaID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                             SubFamilia = new SubFamiliaInfo { SubFamiliaID = info.Field<int>("SubFamiliaID"), Descripcion = info.Field<string>("SubFamilia") },
                             CuentaSAP = new CuentaSAPInfo { CuentaSAPID = info.Field<int>("CuentaSAPID"), Descripcion = info.Field<string>("CuentaSAPDescripcion"), CuentaSAP = info.Field<string>("CuentaSAP") },
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
