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
    internal class MapIvaDAL
    {

        /// <summary>
        ///     Método  que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<IvaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<IvaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new IvaInfo
                     {
                         IvaID = info.Field<int>("IvaId"),
                         Descripcion = info.Field<string>("Descripcion"),
                         IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                         CuentaPagar = 
                            new CuentaInfo
                             {
                                 CuentaID = info.Field<int>("CuentaPagarID"),
                                 Descripcion = info.Field<string>("CuentaPagar")
                             },
                         IndicadorIvaRecuperar = info.Field<string>("IndicadorRecuperar"),
                         CuentaRecuperar = 
                        new CuentaInfo
                        {
                            CuentaID = info.Field<int>("CuentaRecuperarID"),
                            Descripcion = info.Field<string>("CuentaRecuperar")
                        },
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                     }).ToList();

                var resultado =
                    new ResultadoInfo<IvaInfo>
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
        internal static IList<IvaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IvaInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new IvaInfo
                         {
                             IvaID = info.Field<int>("IvaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TasaIva = info.Field<decimal>("TasaIva"),
                             IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                             CuentaPagar = new CuentaInfo
                                           {
                                               ClaveCuenta = info.Field<string>("CuentaPagar")
                                           },
                             IndicadorIvaRecuperar = info.Field<string>("IndicadorIvaRecuperar"),
                             CuentaRecuperar = new CuentaInfo
                                               {
                                                  ClaveCuenta = info.Field<string>("CuentaRecuperar"),
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
        ///     Método  que obtiene un registro de Iva
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IvaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IvaInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new IvaInfo
                         {
                             IvaID = info.Field<int>("IvaId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                             CuentaPagar =
                                new CuentaInfo
                                {
                                    CuentaID = info.Field<int>("CuentaPagarID"),
                                    Descripcion = info.Field<string>("CuentaPagar")
                                },
                             IndicadorIvaRecuperar = info.Field<string>("IndicadorRecuperar"),
                             CuentaRecuperar =
                            new CuentaInfo
                            {
                                CuentaID = info.Field<int>("CuentaRecuperarID"),
                                Descripcion = info.Field<string>("CuentaRecuperar")
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
        ///     Método  que obtiene una lista de Ivas por tipo de Iva
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<IvaInfo> ObtenerPorTipoIvaID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<IvaInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new IvaInfo
                         {
                             IvaID = info.Field<int>("IvaId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                             CuentaPagar =
                                new CuentaInfo
                                {
                                    CuentaID = info.Field<int>("CuentaPagarID"),
                                    Descripcion = info.Field<string>("CuentaPagar")
                                },
                             IndicadorIvaRecuperar = info.Field<string>("IndicadorRecuperar"),
                             CuentaRecuperar =
                            new CuentaInfo
                            {
                                CuentaID = info.Field<int>("CuentaRecuperarID"),
                                Descripcion = info.Field<string>("CuentaRecuperar")
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
    }
}
