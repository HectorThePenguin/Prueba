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
    public class MapIndicadorProductoBoletaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<IndicadorProductoBoletaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoBoletaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoBoletaInfo
                             {
                                 IndicadorProductoBoletaID = info.Field<int>("IndicadorProductoBoletaID"),
                                 IndicadorProducto =
                                     new IndicadorProductoInfo
                                         {
                                             IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                                             IndicadorInfo = new IndicadorInfo
                                                                 {
                                                                     IndicadorId = info.Field<int>("IndicadorID"),
                                                                     Descripcion = info.Field<string>("Descripcion")
                                                                 },
                                             Producto = new ProductoInfo
                                                            {
                                                                ProductoId = info.Field<int>("ProductoID"),
                                                                Descripcion = info.Field<string>("Producto"),
                                                                IndicadorID = info.Field<int>("IndicadorID"),
                                                            }
                                         },
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 RangoMinimo = info.Field<decimal>("RangoMinimo"),
                                 RangoMaximo = info.Field<decimal>("RangoMaximo"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<IndicadorProductoBoletaInfo>
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
        public static List<IndicadorProductoBoletaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoBoletaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoBoletaInfo
                             {
                                 IndicadorProductoBoletaID = info.Field<int>("IndicadorProductoBoletaID"),
                                 IndicadorProducto =
                                     new IndicadorProductoInfo
                                         {IndicadorProductoId = info.Field<int>("IndicadorProductoID"),},
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 RangoMinimo = info.Field<decimal>("RangoMinimo"),
                                 RangoMaximo = info.Field<decimal>("RangoMaximo"),
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
        public static IndicadorProductoBoletaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorProductoBoletaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoBoletaInfo
                             {
                                 IndicadorProductoBoletaID = info.Field<int>("IndicadorProductoBoletaID"),
                                 IndicadorProducto =
                                     new IndicadorProductoInfo
                                         {IndicadorProductoId = info.Field<int>("IndicadorProductoID"),},
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 RangoMinimo = info.Field<decimal>("RangoMinimo"),
                                 RangoMaximo = info.Field<decimal>("RangoMaximo"),
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
        /// Obtiene una entidad de indicador producto boleta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IndicadorProductoBoletaInfo ObtenerPorIndicadorProductoOrganizacion(DataSet ds)
        {
            /*
             * IPB.IndicadorProductoBoletaID,
		        IPB.IndicadorProductoID,
		        IPB.OrganizacionID,
		        IPB.RangoMinimo,
		        IPB.RangoMaximo,
		        IPB.Activo
		        , O.Descripcion		AS Organizacion
		        , IP.IndicadorProductoID
		        , IP.IndicadorID
		        , IP.ProductoID
		        , P.Descripcion		AS Producto
		        , I.Descripcion		AS Indicador
             */
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorProductoBoletaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoBoletaInfo
                             {
                                 IndicadorProductoBoletaID = info.Field<int>("IndicadorProductoBoletaID"),
                                 IndicadorProducto =
                                     new IndicadorProductoInfo
                                         {
                                             IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                                             Producto = new ProductoInfo
                                                            {
                                                                ProductoId = info.Field<int>("ProductoID"),
                                                                Descripcion = info.Field<string>("Producto"),
                                                                IndicadorID = info.Field<int>("IndicadorID"),
                                                            },
                                             IndicadorInfo = new IndicadorInfo
                                                                 {
                                                                     IndicadorId = info.Field<int>("IndicadorID"),
                                                                     Descripcion = info.Field<string>("Indicador")
                                                                 }
                                         },
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 RangoMinimo = info.Field<decimal>("RangoMinimo"),
                                 RangoMaximo = info.Field<decimal>("RangoMaximo"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).FirstOrDefault();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de indicador producto boleta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<IndicadorProductoBoletaInfo> ObtenerPorProductoOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorProductoBoletaInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorProductoBoletaInfo
                         {
                             IndicadorProductoBoletaID = info.Field<int>("IndicadorProductoBoletaID"),
                             IndicadorProducto =
                                 new IndicadorProductoInfo
                                 {
                                     IndicadorProductoId = info.Field<int>("IndicadorProductoID"),
                                     Producto = new ProductoInfo
                                     {
                                         ProductoId = info.Field<int>("ProductoID"),
                                         Descripcion = info.Field<string>("Producto"),
                                         IndicadorID = info.Field<int>("IndicadorID"),
                                     },
                                     IndicadorInfo = new IndicadorInfo
                                     {
                                         IndicadorId = info.Field<int>("IndicadorID"),
                                         Descripcion = info.Field<string>("Indicador")
                                     }
                                 },
                             Organizacion =
                                 new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                     Descripcion = info.Field<string>("Organizacion")
                                 },
                             RangoMinimo = info.Field<decimal>("RangoMinimo"),
                             RangoMaximo = info.Field<decimal>("RangoMaximo"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
