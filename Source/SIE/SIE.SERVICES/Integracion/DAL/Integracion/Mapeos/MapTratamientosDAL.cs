using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTratamientoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TratamientoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TratamientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoInfo
                         {
                             TratamientoID = info.Field<int>("TratamientoID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                             TipoTratamientoInfo = new TipoTratamientoInfo { TipoTratamientoID = info.Field<int>("TipoTratamientoID"), Descripcion = info.Field<string>("TipoTratamiento") },
                             Sexo = Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo") ? Sexo.Macho : Sexo.Hembra,
                             RangoInicial = info.Field<int>("RangoInicial"),
                             RangoFinal = info.Field<int>("RangoFinal"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TratamientoInfo>
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
        internal static List<TratamientoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TratamientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoInfo
                         {
                             TratamientoID = info.Field<int>("TratamientoID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                             TipoTratamientoInfo = new TipoTratamientoInfo { TipoTratamientoID = info.Field<int>("TipoTratamientoID"), Descripcion = info.Field<string>("TipoTratamiento") },
                             Sexo = Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo") ? Sexo.Macho : Sexo.Hembra,
                             RangoInicial = info.Field<int>("RangoInicial"),
                             RangoFinal = info.Field<int>("RangoFinal"),
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
        internal static TratamientoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TratamientoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoInfo
                             {
                                 TratamientoID = info.Field<int>("TratamientoID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                                 TipoTratamientoInfo =
                                     new TipoTratamientoInfo
                                         {
                                             TipoTratamientoID = info.Field<int>("TipoTratamientoID"),
                                             Descripcion = info.Field<string>("TipoTratamiento")
                                         },
                                 Sexo =
                                     Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo")
                                         ? Sexo.Macho
                                         : Sexo.Hembra,
                                 RangoInicial = info.Field<int>("RangoInicial"),
                                 RangoFinal = info.Field<int>("RangoFinal"),
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
        internal static TratamientoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TratamientoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoInfo
                         {
                             TratamientoID = info.Field<int>("TratamientoID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                             TipoTratamientoInfo = new TipoTratamientoInfo { TipoTratamientoID = info.Field<int>("TipoTratamientoID"), Descripcion = info.Field<string>("TipoTratamiento") },
                             Sexo = Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo") ? Sexo.Macho : Sexo.Hembra,
                             RangoInicial = info.Field<int>("RangoInicial"),
                             RangoFinal = info.Field<int>("RangoFinal"),
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

        internal static IList<TratamientoInfo> ObtenerTipoTratamientos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                //t.TratamientoID, t.CodigoTratamiento, t.OrganizacionID, t.Sexo, t.RangoInicial, t.RangoFinal
                List<TratamientoInfo> result =
                    (from info in dt.AsEnumerable()
                     select new TratamientoInfo
                         {
                             TratamientoID = info.Field<int>("TratamientoID"),
                             OrganizacionId = info.Field<int>("OrganizacionID"),
                             CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                             Sexo = Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo") ? Sexo.Macho : Sexo.Hembra,
                             RangoInicial = info.Field<int>("RangoInicial"),
                             RangoFinal = info.Field<int>("RangoFinal"),
                             TipoTratamiento = info.Field<int>("TipoTratamientoID"),
                             Habilitado =  true 

                         }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static IList<TratamientoInfo> ObtenerTipoTratamientosConDias(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                //t.TratamientoID, t.CodigoTratamiento, t.OrganizacionID, t.Sexo, t.RangoInicial, t.RangoFinal
                List<TratamientoInfo> result =
                    (from info in dt.AsEnumerable()
                     select new TratamientoInfo
                     {
                         ProblemaID = info.Field<int>("ProblemaID"),
                         TratamientoID = info.Field<int>("TratamientoID"),
                         Dias = info.Field<int>("Dias"),
                         OrganizacionId = info.Field<int>("OrganizacionID"),
                         CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                         Sexo = Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo") ? Sexo.Macho : Sexo.Hembra,
                         RangoInicial = info.Field<int>("RangoInicial"),
                         RangoFinal = info.Field<int>("RangoFinal"),
                         TipoTratamiento = info.Field<int>("TipoTratamientoID")

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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TratamientoInfo> ObtenerPorFiltro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtTratamientoProducto = ds.Tables[ConstantesDAL.DtTratamientoProducto];
                List<TratamientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoInfo
                         {
                             TratamientoID = info.Field<int>("TratamientoID"),
                             Organizacion = new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                     Descripcion = info.Field<string>("Organizacion"),
                                     TipoOrganizacion = new TipoOrganizacionInfo
                                         {
                                             TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                             Descripcion = info.Field<string>("TipoOrganizacion")
                                         }
                                 },
                             CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                             TipoTratamientoInfo = new TipoTratamientoInfo { TipoTratamientoID = info.Field<int>("TipoTratamientoID"), Descripcion = info.Field<string>("TipoTratamiento") },
                             Sexo = Sexo.Macho.ToString()[0].ToString() == info.Field<string>("Sexo") ? Sexo.Macho : Sexo.Hembra,
                             RangoInicial = info.Field<int>("RangoInicial"),
                             RangoFinal = info.Field<int>("RangoFinal"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             ListaTratamientoProducto = (from producto in dtTratamientoProducto.AsEnumerable()
                                                         where info.Field<int>("TratamientoID") == producto.Field<int>("TratamientoID")
                                                         select new TratamientoProductoInfo
                                                             {
                                                                 TratamientoProductoID = producto.Field<int>("TratamientoProductoID"),
                                                                 Tratamiento = new TratamientoInfo
                                                                     {
                                                                         TratamientoID = producto.Field<int>("TratamientoID")
                                                                     },
                                                                 Dosis = producto.Field<int>("Dosis"),
                                                                 Producto = new ProductoInfo
                                                                     {
                                                                         ProductoId = producto.Field<int>("ProductoID"),
                                                                         ProductoDescripcion = producto.Field<string>("Producto"),
                                                                         SubFamilia = new SubFamiliaInfo
                                                                             {
                                                                                 SubFamiliaID = producto.Field<int>("SubFamiliaID"),
                                                                                 Descripcion = producto.Field<string>("SubFamilia")
                                                                             },
                                                                         Familia = new FamiliaInfo
                                                                         {
                                                                             FamiliaID = producto.Field<int>("FamiliaID"),
                                                                             Descripcion = producto.Field<string>("Familia")
                                                                         }
                                                                     },
                                                                 Activo = producto.Field<bool>("Activo").BoolAEnum()
                                                             }).ToList()
                         }).ToList();

                var resultado = new ResultadoInfo<TratamientoInfo>
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TratamientoInfo> Centros_ObtenerPorFiltro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtTratamientoProducto = ds.Tables[ConstantesDAL.DtTratamientoProducto];
                List<TratamientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoInfo
                         {
                             TratamientoID = info.Field<int>("TratamientoID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                                 TipoOrganizacion = new TipoOrganizacionInfo
                                 {
                                     TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                     Descripcion = info.Field<string>("TipoOrganizacion")
                                 }
                             },
                             CodigoTratamiento = info.Field<int>("CodigoTratamiento"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Auxiliar = info.Field<string>("Descripcion"),
                             ListaTratamientoProducto = (from producto in dtTratamientoProducto.AsEnumerable()
                                                         where info.Field<int>("TratamientoID") == producto.Field<int>("TratamientoID") && info.Field<int>("OrganizacionID") == producto.Field<int>("OrganizacionID")
                                                         select new TratamientoProductoInfo
                                                         {
                                                             TratamientoProductoID = producto.Field<int>("TratamientoProductoID"),
                                                             Tratamiento = new TratamientoInfo
                                                             {
                                                                 TratamientoID = producto.Field<int>("TratamientoID")
                                                             },
                                                             Dosis = producto.Field<int>("Dosis"),
                                                             Factor = producto.Field<bool>("Factor"),
                                                             FactorMacho = producto.Field<decimal>("FactorMacho"),
                                                             FactorHembra = producto.Field<decimal>("FactorHembra"),
                                                             Producto = new ProductoInfo
                                                             {
                                                                 ProductoId = producto.Field<int>("ProductoID"),
                                                                 ProductoDescripcion = producto.Field<string>("Producto"),
                                                                 SubFamilia = new SubFamiliaInfo
                                                                 {
                                                                     SubFamiliaID = producto.Field<int>("SubFamiliaID"),
                                                                     Descripcion = producto.Field<string>("SubFamilia")
                                                                 },
                                                                 Familia = new FamiliaInfo
                                                                 {
                                                                     FamiliaID = producto.Field<int>("FamiliaID"),
                                                                     Descripcion = producto.Field<string>("Familia")
                                                                 }
                                                             },
                                                             Activo = producto.Field<bool>("Activo").BoolAEnum()
                                                         }).ToList()
                         }).ToList();

                var resultado = new ResultadoInfo<TratamientoInfo>
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
        /// obtiene el costo del tratamieno
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static Decimal ObtenerCosto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                Decimal costo =
                    (from info in dt.AsEnumerable()
                        select
                            info["costo"] == DBNull.Value ? 0 : info.Field<decimal>("costo")
                        ).First();
                return costo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
