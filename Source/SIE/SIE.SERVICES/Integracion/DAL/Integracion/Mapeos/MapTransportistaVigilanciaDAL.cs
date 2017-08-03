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
    class MapTransportistaVigilanciaDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<VigilanciaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<VigilanciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Direccion = info.Field<string>("Direccion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<VigilanciaInfo>
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
        internal static ResultadoInfo<VigilanciaInfo> ObtenerTransportistaPorPaginaCompleto(DataSet ds)
        {
            ResultadoInfo<VigilanciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                            new VigilanciaInfo
                            {
                                ID = info.Field<int>("ProveedorID"),
                                Descripcion = info.Field<string>("Descripcion"),
                                CodigoSAP = info.Field<string>("CodigoSAP"),
                                Activo = info.Field<bool>("Activo").BoolAEnum(),
                            }
                    ).ToList();

                resultado = new ResultadoInfo<VigilanciaInfo>
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<VigilanciaInfo> ObtenerTodos(DataSet ds)
        {
            List<VigilanciaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                             {
                                 TipoOrganizacionID =
                                     info.Field<int>("TipoOrganizacionID")
                             },
                             Descripcion = info.Field<string>("Descripcion"),
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static VigilanciaInfo ObtenerPorID(DataSet ds)
        {
            VigilanciaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new VigilanciaInfo
                         {
                             ID = info.Field<int>("ProveedorID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        internal static VigilanciaInfo ObtenerPorDependenciaId(DataSet ds)
        {
            VigilanciaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
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

        internal static VigilanciaInfo ObtenerPorEmbarqueTipoOrganizacion(DataSet ds)
        {
            VigilanciaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
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
        internal static List<VigilanciaInfo> ObtenerPendientesRecibir(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new VigilanciaInfo
                     {
                         ID = info.Field<int>("OrganizacionId"),
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
        internal static VigilanciaInfo ObtenerPorIdConIva(DataSet ds)
        {
            VigilanciaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
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
        internal static VigilanciaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                VigilanciaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new VigilanciaInfo
                         {
                             ID = info.Field<int>("ID"),
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
        internal static ResultadoInfo<VigilanciaInfo> ObtenerParametrosPorFolio(DataSet ds)
        {
            ResultadoInfo<VigilanciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
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

                resultado = new ResultadoInfo<VigilanciaInfo>
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
        internal static ResultadoInfo<VigilanciaInfo> ObtenerPorTipoOrigenPaginado(DataSet ds)
        {
            ResultadoInfo<VigilanciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoOrganizacion = new TipoOrganizacionInfo
                             {
                                 TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                 Descripcion = info.Field<string>("TipoOrganizacion")
                             },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<VigilanciaInfo>
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
        internal static ResultadoInfo<VigilanciaInfo> ObtenerOrganizacionPorOrigenIDPaginado(DataSet ds)
        {
            ResultadoInfo<VigilanciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<VigilanciaInfo>
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
        internal static ResultadoInfo<VigilanciaInfo> ObtenerPorEmbarqueTipoOrigenPaginado(DataSet ds)
        {
            ResultadoInfo<VigilanciaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<VigilanciaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new VigilanciaInfo
                         {
                             ID = info.Field<int>("OrganizacionId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                resultado = new ResultadoInfo<VigilanciaInfo>
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
