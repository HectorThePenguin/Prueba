//using System;
//using System.Collections.Generic;
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
    internal  class MapCuentaSAPDAL
    {

        /// <summary>
        ///     Método  que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CuentaSAPInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CuentaSAPInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new CuentaSAPInfo
                     {
                         CuentaSAPID = info.Field<int>("CuentaSAPID"),
                         Descripcion = info.Field<string>("Descripcion"),
                         CuentaSAP = info.Field<string>("CuentaSAP"),
                         TipoCuenta =
                             new TipoCuentaInfo
                             {
                                 TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                 Descripcion = info.Field<string>("TipoCuenta")
                             },
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                     }).ToList();

                var resultado =
                    new ResultadoInfo<CuentaSAPInfo>
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
        internal  static IList<CuentaSAPInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CuentaSAPInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaSAPInfo
                         {
                             CuentaSAPID = info.Field<int>("CuentaSAPId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoCuenta =
                                 new TipoCuentaInfo
                                 {
                                     TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                     Descripcion = info.Field<string>("TipoCuenta")
                                 },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             CuentaSAP = info.Field<string>("CuentaSAP")
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
        ///     Método  que obtiene un registro de CuentaSAP
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CuentaSAPInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                CuentaSAPInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaSAPInfo
                         {
                             CuentaSAP = info.Field<string>("CuentaSAP"),
                             CuentaSAPID = info.Field<int>("CuentaSAPId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoCuenta =
                                 new TipoCuentaInfo
                                 {
                                     TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                     Descripcion = info.Field<string>("TipoCuenta")
                                 },
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
        ///     Método  que obtiene una lista de CuentaSAPs por tipo de CuentaSAP
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static IList<CuentaSAPInfo> ObtenerPorTipoCuentaSAPID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CuentaSAPInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaSAPInfo
                         {
                             CuentaSAPID = info.Field<int>("CuentaSAPId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoCuenta =
                                 new TipoCuentaInfo
                                 {
                                     TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                     Descripcion = info.Field<string>("TipoCuenta")
                                 },
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
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CuentaSAPInfo ObtenerPorFiltro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaSAPInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaSAPInfo
                         {
                             CuentaSAPID = info.Field<int>("CuentaSAPId"),
                             CuentaSAP = info.Field<string>("CuentaSAP"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoCuenta =
                                 new TipoCuentaInfo
                                 {
                                     TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                     Descripcion = info.Field<string>("TipoCuenta")
                                 },
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

        internal static IList<CuentaSAPInfo> ObtenerTodos(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var list = new List<CuentaSAPInfo>();
                CuentaSAPInfo obj;
                var propiedadesBindeables = new List<string>
                                                {
                                                    "CuentaSAPID",
                                                    "CuentaSAP",
                                                    "Descripcion",
                                                    "TipoCuentaID",
                                                };
                PropertyInfo prop;
                while (reader.Read())
                {
                    obj = Activator.CreateInstance<CuentaSAPInfo>();
                    for (int index = 0; index < propiedadesBindeables.Count; index++)
                    {
                        prop = obj.GetType().GetProperty(propiedadesBindeables[index]);
                        prop.SetValue(obj, reader[propiedadesBindeables[index]], null);
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }            
        }
    }
}
