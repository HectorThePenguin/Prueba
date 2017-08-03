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
    internal class MapIndicadorProductoDAL 
    {
        internal static List<IndicadorProductoInfo> ObtenerPorProductoId(DataSet ds)
        {
            List<IndicadorProductoInfo> indicadorProductoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                indicadorProductoInfo = (from info in dt.AsEnumerable()
                                                               select new IndicadorProductoInfo
                                                               {
                                                                   IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                                                                   IndicadorInfo = new IndicadorInfo
                                                                   {
                                                                       IndicadorId = info.Field<int>("IndicadorID"),
                                                                       Descripcion = info.Field<string>("Descripcion")
                                                                   },
                                                                   ProductoId = info.Field<int>("ProductoID"),
                                                                   Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                                   Minimo = info.Field<decimal>("Minimo"),
                                                                   Maximo = info.Field<decimal>("Maximo")
                                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return indicadorProductoInfo;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<IndicadorProductoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoInfo
                             {
                                 IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                                 IndicadorInfo =
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

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<IndicadorProductoInfo>
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
        public static List<IndicadorProductoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoInfo
                             {
                                 IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                                 IndicadorInfo =
                                     new IndicadorInfo
                                         {
                                             IndicadorId = info.Field<int>("IndicadorID"),
                                             Descripcion = info.Field<string>("Indicador")
                                         },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto")
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
        /// Obtiene un indicador producto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IndicadorProductoInfo ObtenerPorIndicadorProducto(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorProductoInfo lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoInfo
                         {
                             IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                             IndicadorInfo =
                                 new IndicadorInfo
                                 {
                                     IndicadorId = info.Field<int>("IndicadorID"),
                                     Descripcion = info.Field<string>("Indicador")
                                 },
                             Producto =
                                 new ProductoInfo
                                 {
                                     ProductoId = info.Field<int>("ProductoID"),
                                     Descripcion = info.Field<string>("Producto")
                                 },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).FirstOrDefault();
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
