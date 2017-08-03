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
    internal class MapTratamientoProductoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TratamientoProductoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TratamientoProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoProductoInfo
                             {
								TratamientoProductoID = info.Field<int>("TratamientoProductoID"),
								Tratamiento = new TratamientoInfo { TratamientoID = info.Field<int>("TratamientoID"), CodigoTratamiento = info.Field<int>("Tratamiento") },
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Producto") },
								Dosis = info.Field<int>("Dosis"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TratamientoProductoInfo>
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
        internal static List<TratamientoProductoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TratamientoProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoProductoInfo
                             {
								TratamientoProductoID = info.Field<int>("TratamientoProductoID"),
                                Tratamiento = new TratamientoInfo { TratamientoID = info.Field<int>("TratamientoID"), CodigoTratamiento = info.Field<int>("Tratamiento") },
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Producto") },
								Dosis = info.Field<int>("Dosis"),
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
        internal static TratamientoProductoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TratamientoProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoProductoInfo
                             {
                                 TratamientoProductoID = info.Field<int>("TratamientoProductoID"),
                                 Tratamiento =
                                     new TratamientoInfo
                                         {
                                             TratamientoID = info.Field<int>("TratamientoID"),
                                             CodigoTratamiento = info.Field<int>("Tratamiento")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         },
                                 Dosis = info.Field<int>("Dosis"),
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
        internal static TratamientoProductoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TratamientoProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoProductoInfo
                             {
								TratamientoProductoID = info.Field<int>("TratamientoProductoID"),
								Tratamiento = new TratamientoInfo { TratamientoID = info.Field<int>("TratamientoID"), CodigoTratamiento = info.Field<int>("Tratamiento") },
								Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Producto") },
								Dosis = info.Field<int>("Dosis"),
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TratamientoProductoInfo> ObtenerPorPaginaTratamientoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TratamientoProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoProductoInfo
                         {
                             TratamientoProductoID = info.Field<int>("TratamientoProductoID"),
                             Tratamiento = new TratamientoInfo { TratamientoID = info.Field<int>("TratamientoID"), CodigoTratamiento = info.Field<int>("CodigoTratamiento") },
                             Producto = new ProductoInfo
                                 {
                                     ProductoId = info.Field<int>("ProductoID"), 
                                     ProductoDescripcion = info.Field<string>("Producto"),
                                     SubFamilia = new SubFamiliaInfo
                                         {
                                             SubFamiliaID = info.Field<int>("SubFamiliaID"),
                                             Descripcion = info.Field<string>("SubFamilia")
                                         },
                                     Familia = new FamiliaInfo
                                     {
                                         FamiliaID = info.Field<int>("FamiliaID"),
                                         Descripcion = info.Field<string>("Familia")
                                     }
                                 },
                             Dosis = info.Field<int>("Dosis"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TratamientoProductoInfo>
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

        internal static List<TratamientoProductoInfo> ObtenerPorTratamientoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TratamientoProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TratamientoProductoInfo
                         {
                             TratamientoProductoID = info.Field<int>("TratamientoProductoID"),
                             Tratamiento = new TratamientoInfo { TratamientoID = info.Field<int>("TratamientoID")},
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), ProductoDescripcion = info.Field<string>("Descripcion") },
                             Dosis = info.Field<int>("Dosis"),
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

        internal static List<HistorialClinicoDetalleInfo> ObtenerPorMovimientoTratamientoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<HistorialClinicoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                     new HistorialClinicoDetalleInfo
                     {
                         Problema = String.Empty,
                         Tratamiento = string.Format("{0:d3}", info.Field<int>("CodigoTratamiento")),
                         Costo = info.Field<decimal>("Costo"),
                         TratamientoId = info.Field<int>("TratamientoID"),
                         DescripcionProducto = info.Field<string>("Descripcion"),
                     }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


    }
}

