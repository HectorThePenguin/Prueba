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
    internal  class MapCuentaDAL
    {
    
        /// <summary>
        ///     Método  que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CuentaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CuentaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new CuentaInfo
                                {
                                    CuentaID = info.Field<int>("CuentaID"),
                                    Descripcion = info.Field<string>("Descripcion"),
                                    ClaveCuenta = info.Field<string>("ClaveCuenta"),
                                    TipoCuenta =
                                        new TipoCuentaInfo
                                            {
                                                TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                                Descripcion = info.Field<string>("TipoCuenta")
                                            },
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                }).ToList();

                var resultado =
                    new ResultadoInfo<CuentaInfo>
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
        internal  static IList<CuentaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CuentaInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaInfo
                             {
                                 CuentaID = info.Field<int>("CuentaId"),
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
        ///     Método  que obtiene un registro de cuenta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CuentaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                CuentaInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaInfo
                             {
                                 CuentaID = info.Field<int>("CuentaId"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoCuenta =
                                     new TipoCuentaInfo
                                         {
                                             TipoCuentaID = info.Field<int>("TipoCuentaID"),
                                             Descripcion = info.Field<string>("TipoCuenta")
                                         },
                                 ClaveCuenta = info.Field<string>("ClaveCuenta"),
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
        ///     Método  que obtiene una lista de cuentas por tipo de cuenta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static IList<CuentaInfo> ObtenerPorTipoCuentaID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CuentaInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaInfo
                             {
                                 CuentaID = info.Field<int>("CuentaId"),
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
        ///     Método  que obtiene una lista de cuentas por tipo de cuenta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ClaveContableInfo ObtenerPorClaveCuentaOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                ClaveContableInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new ClaveContableInfo
                         {
                             Valor = info.Field<string>("Valor"),
                             Descripcion = info.Field<string>("Descripcion")
                         }).FirstOrDefault();
                return result;
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
        public static CuentaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaInfo
                         {
                             CuentaID = info.Field<int>("CuentaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoCuenta = new TipoCuentaInfo { TipoCuentaID = info.Field<int>("TipoCuentaID"), Descripcion = info.Field<string>("TipoCuenta") },
                             ClaveCuenta = info.Field<string>("ClaveCuenta"),
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
