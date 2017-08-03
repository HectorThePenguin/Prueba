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
    internal static class MapOrganizacionDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OrganizacionInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<OrganizacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionId"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoOrganizacion = new TipoOrganizacionInfo
                                 {
                                     TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                     Descripcion = info.Field<string>("TipoOrganizacion"),
                                 },
                                 Direccion = info.Field<string>("Direccion"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Division = info.Field<string>("Division"),
                                 Sociedad = info.Field<string>("Sociedad"),
                                 RFC = info.Field<string>("RFC"),
                                 Iva = new IvaInfo
                                 {
                                     IvaID = info.Field<int>("IvaID"),
                                     Descripcion = info.Field<string>("Iva"),
                                 },
                                 Zona = new ZonaInfo()
                                            {
                                                ZonaID = info.Field<int?>("ZonaID"),
                                                Descripcion = info.Field<string>("Zona")
                                            }, 
                             }).ToList();

                resultado = new ResultadoInfo<OrganizacionInfo>
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaCompleto(DataSet ds)
        {
            ResultadoInfo<OrganizacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                             {
                                 TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                 Descripcion = info.Field<string>("TipoOrganizacion"),
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             Direccion = info.Field<string>("Direccion"),
                             RFC = info.Field<string>("RFC"),
                             Iva = new IvaInfo
                             {
                                 IvaID = info.Field<int>("IvaID"),
                                 Descripcion = info.Field<string>("Iva"),
                             },
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();

                resultado = new ResultadoInfo<OrganizacionInfo>
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
        ///     Metodo que obtiene una lista de las organizaciones tipo ganaderas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<OrganizacionInfo> ObtenerTipoGanaderas(DataSet ds)
        {
            List<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                                    {
                                        OrganizacionID = info.Field<int>("OrganizacionId"),
                                        Descripcion = info.Field<string>("Descripcion")
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<OrganizacionInfo> ObtenerTodos(DataSet ds)
        {
            List<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                                    {
                                        OrganizacionID = info.Field<int>("OrganizacionId"),
                                        TipoOrganizacion = new TipoOrganizacionInfo
                                                               {
                                                                   TipoOrganizacionID =
                                                                       info.Field<int>("TipoOrganizacionID")
                                                               },
                                        Descripcion = info.Field<string>("Descripcion"),
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                        Division = info.Field<string>("Division"),
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerPorID(DataSet ds)
        {
            OrganizacionInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                                    {
                                        OrganizacionID = info.Field<int>("OrganizacionId"),
                                        TipoOrganizacion =
                                            new TipoOrganizacionInfo
                                                {
                                                    TipoOrganizacionID = info.Field<int>("TipoOrganizacionID")//,
                                                    //Descripcion = info.Field<string>("TipoOrganizacion")
                                                },
                                        Descripcion = info.Field<string>("Descripcion"),
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                        Direccion = info.Field<string>("Direccion") ?? string.Empty,
                                        Division = info.Field<string>("Division") ?? string.Empty,
                                        Moneda = info.Field<string>("Moneda") ?? string.Empty,
                                        Sociedad = info.Field<int>("Sociedad").ToString() ?? string.Empty,
                                    }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerPorDependenciaId(DataSet ds)
        {
            OrganizacionInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                                 {
                                     TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                     Descripcion = info.Field<string>("TipoOrganizacion")
                                 },
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static OrganizacionInfo ObtenerPorEmbarqueTipoOrganizacion(DataSet ds)
        {
            OrganizacionInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<OrganizacionInfo> ObtenerPendientesRecibir(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new OrganizacionInfo
                                {
                                    OrganizacionID = info.Field<int>("OrganizacionId"),
                                    Descripcion = info.Field<string>("Descripcion"),
                                    Direccion = info.Field<string>("Direccion"),
                                    TipoOrganizacion =
                                        new TipoOrganizacionInfo
                                            {
                                                TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                                Descripcion = info.Field<string>("TipoOrganizacion"),
                                            },
                                    Iva =
                                        new IvaInfo
                                            {
                                                IvaID = info.Field<int>("IvaID"),
                                                TasaIva = info.Field<decimal>("TasaIva"),
                                                IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                                                IndicadorIvaRecuperar = info.Field<string>("IndicadorIvaRecuperar"),
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
        /// Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerPorIdConIva(DataSet ds)
        {
            OrganizacionInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Sociedad = info.Field<string>("Sociedad"),
                             Division = info.Field<string>("Division"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                                                    {
                                                        TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                                        Descripcion = info.Field<string>("TipoOrganizacion"),
                                                        TipoProceso = new TipoProcesoInfo
                                                                          {
                                                                              TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                                                              Descripcion = info.Field<string>("DescripcionTipoProceso"),
                                                                          },
                                                        DescripcionTipoProceso = info.Field<string>("DescripcionTipoProceso")
                                                    },
                             Descripcion = info.Field<string>("Descripcion"),
                             Direccion = info.Field<string>("Direccion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Iva = new IvaInfo
                             {
                                 IvaID = info.Field<int>("IvaID"),
                                 Descripcion = info.Field<string>("DescripcionIva"),
                                 TasaIva = info.Field<decimal>("TasaIva"),
                                 IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                                 IndicadorIvaRecuperar = info.Field<string>("IndicadorIvaRecuperar"),
                                 CuentaPagar = new CuentaInfo
                                                   {
                                                       ClaveCuenta = info.Field<string>("CuentaPagar")
                                                   },
                                 CuentaRecuperar = new CuentaInfo
                                                   {
                                                       ClaveCuenta = info.Field<string>("CuentaRecuperar")
                                                   }
                             }
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OrganizacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 TipoOrganizacion = new TipoOrganizacionInfo
                                                        {
                                                            TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                                            Descripcion = info.Field<string>("TipoOrganizacion"),
                                                        },
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Direccion = info.Field<string>("Direccion"),
                                 RFC = info.Field<string>("RFC"),
                                 Iva = new IvaInfo
                                           {
                                               IvaID = info.Field<int>("IvaID"),
                                               Descripcion = info.Field<string>("Iva"),
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OrganizacionInfo> ObtenerParametrosPorFolio(DataSet ds)
        {
            ResultadoInfo<OrganizacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                             {
                                 TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                 Descripcion = info.Field<string>("TipoOrganizacion"),
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             RFC = info.Field<string>("RFC"),
                             Iva = new IvaInfo
                             {
                                 IvaID = info.Field<int>("IvaID"),
                                 Descripcion = info.Field<string>("Iva"),
                             },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<OrganizacionInfo>
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
        ///  Metodo que obtiene una lista paginada por tipo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OrganizacionInfo> ObtenerPorTipoOrigenPaginado(DataSet ds)
        {
            ResultadoInfo<OrganizacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                                 {
                                     TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                     Descripcion = info.Field<string>("TipoOrganizacion")
                                 },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<OrganizacionInfo>
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
        ///  Metodo que obtiene una lista paginada por tipo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OrganizacionInfo> ObtenerOrganizacionPorOrigenIDPaginado(DataSet ds)
        {
            ResultadoInfo<OrganizacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<OrganizacionInfo>
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
        ///  Metodo que obtiene una lista paginada por tipo de origen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OrganizacionInfo> ObtenerPorEmbarqueTipoOrigenPaginado(DataSet ds)
        {
            ResultadoInfo<OrganizacionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OrganizacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<OrganizacionInfo>
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
        /// Obtiene una organizacion por id y tipo organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerPorIdFiltroTiposOrganizacion(DataSet ds)
        {
            OrganizacionInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             TipoOrganizacion =
                                 new TipoOrganizacionInfo
                                 {
                                     TipoOrganizacionID = info.Field<int>("TipoOrganizacionID")//,
                                     //Descripcion = info.Field<string>("TipoOrganizacion")
                                 },
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Direccion = info.Field<string>("Direccion") ?? string.Empty
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene el listado de organizaciones del producto de la premezcla seleccionada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<OrganizacionInfo> ObtenerOrganizacionesPremezcla(DataSet ds)
        {
            List<OrganizacionInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                             {
                                 TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                 Descripcion = info.Field<string>("TipoOrganizacion"),
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             Direccion = info.Field<string>("Direccion"),
                             RFC = info.Field<string>("RFC"),
                             Iva = new IvaInfo
                             {
                                 IvaID = info.Field<int>("IvaID"),
                                 Descripcion = info.Field<string>("Iva"),
                             },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        /// Obtiene una organizacion por id y tipo organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerPorAlmacenID(DataSet ds)
        {
            OrganizacionInfo organizacion;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                organizacion = (from info in dt.AsEnumerable()
                         select new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionId"),
                             Sociedad = info.Field<string>("Sociedad"),
                             Division = info.Field<string>("Division"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                             {
                                 TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                 Descripcion = info.Field<string>("TipoOrganizacion"),
                                 TipoProceso = new TipoProcesoInfo
                                 {
                                     TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                     Descripcion = info.Field<string>("DescripcionTipoProceso"),
                                 },
                                 DescripcionTipoProceso = info.Field<string>("DescripcionTipoProceso")
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             Direccion = info.Field<string>("Direccion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Iva = new IvaInfo
                             {
                                 IvaID = info.Field<int>("IvaID"),
                                 Descripcion = info.Field<string>("DescripcionIva"),
                                 TasaIva = info.Field<decimal>("TasaIva"),
                                 IndicadorIvaPagar = info.Field<string>("IndicadorIvaPagar"),
                                 IndicadorIvaRecuperar = info.Field<string>("IndicadorIvaRecuperar"),
                                 CuentaPagar = new CuentaInfo
                                 {
                                     ClaveCuenta = info.Field<string>("CuentaPagar")
                                 },
                                 CuentaRecuperar = new CuentaInfo
                                 {
                                     ClaveCuenta = info.Field<string>("CuentaRecuperar")
                                 }
                             }
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return organizacion;
        }

        internal static OrganizacionInfo ObtenerPorIdConIva(IDataReader reader)
        {
            OrganizacionInfo organizacion = null;
            try
            {
                Logger.Info();
                while (reader.Read())
                {
                    organizacion = new OrganizacionInfo
                                       {
                                           OrganizacionID = Convert.ToInt32(reader["OrganizacionId"]),
                                           Sociedad = Convert.ToString(reader["Sociedad"]),
                                           Division = Convert.ToString(reader["Division"]),
                                           TipoOrganizacion = new TipoOrganizacionInfo
                                                                  {
                                                                      TipoOrganizacionID =
                                                                          Convert.ToInt32(reader["TipoOrganizacionID"]),
                                                                      Descripcion =
                                                                          Convert.ToString(reader["TipoOrganizacion"]),
                                                                      TipoProceso = new TipoProcesoInfo
                                                                                        {
                                                                                            TipoProcesoID =
                                                                                                Convert.ToInt32(
                                                                                                    reader[
                                                                                                        "TipoProcesoID"]),
                                                                                            Descripcion =
                                                                                                Convert.ToString(reader[
                                                                                                    "DescripcionTipoProceso"
                                                                                                                     ]),
                                                                                        },
                                                                      DescripcionTipoProceso =
                                                                          Convert.ToString(
                                                                              reader["DescripcionTipoProceso"])
                                                                  },
                                           Descripcion = Convert.ToString(reader["Descripcion"]),
                                           Direccion = Convert.ToString(reader["Direccion"]),
                                           Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                                           Iva = new IvaInfo
                                                     {
                                                         IvaID = Convert.ToInt32(reader["IvaID"]),
                                                         Descripcion = Convert.ToString(reader["DescripcionIva"]),
                                                         TasaIva = Convert.ToDecimal(reader["TasaIva"]),
                                                         IndicadorIvaPagar =
                                                             Convert.ToString(reader["IndicadorIvaPagar"]),
                                                         IndicadorIvaRecuperar =
                                                             Convert.ToString(reader["IndicadorIvaRecuperar"]),
                                                         CuentaPagar = new CuentaInfo
                                                                           {
                                                                               ClaveCuenta =
                                                                                   Convert.ToString(
                                                                                       reader["CuentaPagar"])
                                                                           },
                                                         CuentaRecuperar = new CuentaInfo
                                                                               {
                                                                                   ClaveCuenta =
                                                                                       Convert.ToString(
                                                                                           reader["CuentaRecuperar"])
                                                                               }
                                                     }
                                       };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return organizacion;
        }

        /// <summary>
        /// Obtiene la descripcion de la sociedad, su id y la division
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static OrganizacionInfo ObtenerOrganizacionSociedadDivision(IDataReader reader)
        {
            OrganizacionInfo organizacion = null;
            try
            {
                Logger.Info();
                while (reader.Read())
                {
                    organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = Convert.ToInt32(reader["OrganizacionId"]),
                        Sociedad = Convert.ToString(reader["Sociedad"]),
                        Division = Convert.ToString(reader["Division"]),
                        TituloPoliza = Convert.ToString(reader["TituloPoliza"]),
                        TipoOrganizacion = new TipoOrganizacionInfo
                        {
                            TipoOrganizacionID =
                                Convert.ToInt32(reader["TipoOrganizacionID"]),
                            Descripcion =
                                Convert.ToString(reader["TipoOrganizacion"]),
                            TipoProceso = new TipoProcesoInfo
                            {
                                TipoProcesoID =
                                    Convert.ToInt32(
                                        reader[
                                            "TipoProcesoID"]),
                                Descripcion =
                                    Convert.ToString(reader[
                                        "DescripcionTipoProceso"
                                                         ]),
                            },
                            DescripcionTipoProceso =
                                Convert.ToString(
                                    reader["DescripcionTipoProceso"])
                        },
                        Descripcion = Convert.ToString(reader["Descripcion"]),
                        Direccion = Convert.ToString(reader["Direccion"]),
                        Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                        Iva = new IvaInfo
                        {
                            IvaID = Convert.ToInt32(reader["IvaID"]),
                            Descripcion = Convert.ToString(reader["DescripcionIva"]),
                            TasaIva = Convert.ToDecimal(reader["TasaIva"]),
                            IndicadorIvaPagar =
                                Convert.ToString(reader["IndicadorIvaPagar"]),
                            IndicadorIvaRecuperar =
                                Convert.ToString(reader["IndicadorIvaRecuperar"]),
                            CuentaPagar = new CuentaInfo
                            {
                                ClaveCuenta =
                                    Convert.ToString(
                                        reader["CuentaPagar"])
                            },
                            CuentaRecuperar = new CuentaInfo
                            {
                                ClaveCuenta =
                                    Convert.ToString(
                                        reader["CuentaRecuperar"])
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return organizacion;
        }

    }
}