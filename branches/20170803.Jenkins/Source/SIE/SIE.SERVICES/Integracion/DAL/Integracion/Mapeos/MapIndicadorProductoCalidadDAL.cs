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
    public class MapIndicadorProductoCalidadDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<IndicadorProductoCalidadInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoCalidadInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoCalidadInfo
                             {
                                 IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID"),
                                 Indicador =
                                     new IndicadorInfo
                                         {
                                             IndicadorId = info.Field<int>("IndicadorID"),
                                             Descripcion = info.Field<string>("Indicador")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<IndicadorProductoCalidadInfo>
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
        public static List<IndicadorProductoCalidadInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoCalidadInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoCalidadInfo
                             {
                                 IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID"),
                                 Indicador =
                                     new IndicadorInfo
                                         {
                                             IndicadorId = info.Field<int>("IndicadorID"),
                                             Descripcion = info.Field<string>("Indicador")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto"),
                                             ProductoDescripcion = info.Field<string>("Producto"),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IndicadorProductoCalidadInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorProductoCalidadInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoCalidadInfo
                             {
                                 IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID"),
                                 Indicador =
                                     new IndicadorInfo
                                         {
                                             IndicadorId = info.Field<int>("IndicadorID"),
                                             Descripcion = info.Field<string>("Indicador")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto"),
                                             ProductoDescripcion = info.Field<string>("Producto"),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IndicadorProductoCalidadInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorProductoCalidadInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoCalidadInfo
                             {
                                 IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID"),
                                 Indicador =
                                     new IndicadorInfo
                                         {
                                             IndicadorId = info.Field<int>("IndicadorID"),
                                             Descripcion = info.Field<string>("Indicador")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto"),
                                             ProductoDescripcion = info.Field<string>("Producto"),
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
        /// Obtiene un indicador producto calidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IndicadorProductoCalidadInfo ObtenerPorIndicadorProducto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorProductoCalidadInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoCalidadInfo
                         {
                             IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID"),
                             Indicador =
                                 new IndicadorInfo
                                 {
                                     IndicadorId = info.Field<int>("IndicadorID"),
                                     Descripcion = info.Field<string>("Indicador")
                                 },
                             Producto =
                                 new ProductoInfo
                                 {
                                     ProductoId = info.Field<int>("ProductoID"),
                                     Descripcion = info.Field<string>("Producto"),
                                     ProductoDescripcion = info.Field<string>("Producto"),
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
        /// Obtiene un indicador producto calidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ProductoInfo> ObtenerProductosPorIndicador(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select new ProductoInfo
                                 {
                                     ProductoId = info.Field<int>("ProductoID"),
                                     Descripcion = info.Field<string>("Producto"),
                                     ProductoDescripcion = info.Field<string>("Producto"),
                                 }).ToList();
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
