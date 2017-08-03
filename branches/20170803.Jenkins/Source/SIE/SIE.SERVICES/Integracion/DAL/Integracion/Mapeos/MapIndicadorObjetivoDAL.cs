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
    public class MapIndicadorObjetivoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<IndicadorObjetivoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorObjetivoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorObjetivoInfo
                         {
                             IndicadorObjetivoID = info.Field<int>("IndicadorObjetivoID"),
                             Indicador = new IndicadorInfo
                                 {
                                     IndicadorId = info.Field<int>("IndicadorID"),
                                     Descripcion = info.Field<string>("Indicador")
                                 },
                             IndicadorProductoCalidad = new IndicadorProductoCalidadInfo
                                 {
                                     IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID"),
                                     Producto = new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             ProductoDescripcion = info.Field<string>("Producto")
                                         }
                                 },

                             TipoObjetivoCalidad = new TipoObjetivoCalidadInfo { TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID"), Descripcion = info.Field<string>("TipoObjetivoCalidad") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             ObjetivoMinimo = info.Field<decimal>("ObjetivoMinimo"),
                             ObjetivoMaximo = info.Field<decimal>("ObjetivoMaximo"),
                             Tolerancia = info.Field<decimal>("Tolerancia"),
                             Medicion = info.Field<string>("Medicion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<IndicadorObjetivoInfo>
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
        public static List<IndicadorObjetivoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorObjetivoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorObjetivoInfo
                         {
                             IndicadorObjetivoID = info.Field<int>("IndicadorObjetivoID"),
                             IndicadorProductoCalidad = new IndicadorProductoCalidadInfo { IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID") },
                             TipoObjetivoCalidad = new TipoObjetivoCalidadInfo { TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID"), Descripcion = info.Field<string>("TipoObjetivoCalidad") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             ObjetivoMinimo = info.Field<decimal>("ObjetivoMinimo"),
                             ObjetivoMaximo = info.Field<decimal>("ObjetivoMaximo"),
                             Tolerancia = info.Field<decimal>("Tolerancia"),
                             Medicion = info.Field<string>("Medicion"),
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
        public static IndicadorObjetivoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorObjetivoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorObjetivoInfo
                         {
                             IndicadorObjetivoID = info.Field<int>("IndicadorObjetivoID"),
                             IndicadorProductoCalidad = new IndicadorProductoCalidadInfo { IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID") },
                             TipoObjetivoCalidad = new TipoObjetivoCalidadInfo { TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID"), Descripcion = info.Field<string>("TipoObjetivoCalidad") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             ObjetivoMinimo = info.Field<decimal>("ObjetivoMinimo"),
                             ObjetivoMaximo = info.Field<decimal>("ObjetivoMaximo"),
                             Tolerancia = info.Field<decimal>("Tolerancia"),
                             Medicion = info.Field<string>("Medicion"),
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
        public static IndicadorObjetivoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorObjetivoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorObjetivoInfo
                         {
                             IndicadorObjetivoID = info.Field<int>("IndicadorObjetivoID"),
                             IndicadorProductoCalidad = new IndicadorProductoCalidadInfo { IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID") },
                             TipoObjetivoCalidad = new TipoObjetivoCalidadInfo { TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID"), Descripcion = info.Field<string>("TipoObjetivoCalidad") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             ObjetivoMinimo = info.Field<decimal>("ObjetivoMinimo"),
                             ObjetivoMaximo = info.Field<decimal>("ObjetivoMaximo"),
                             Tolerancia = info.Field<decimal>("Tolerancia"),
                             Medicion = info.Field<string>("Medicion"),
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
        /// Obtiene entidad de Indicador Objetivo Semaforo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<IndicadorObjetivoSemaforoInfo> ObtenerSemaforo(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorObjetivoSemaforoInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorObjetivoSemaforoInfo
                         {
                             IndicadorObjetivoID = info.Field<int>("IndicadorObjetivoID"),
                             ObjetivoMinimo = info.Field<decimal>("ObjetivoMinimo"),
                             ObjetivoMaximo = info.Field<decimal>("ObjetivoMaximo"),
                             Tolerancia = info.Field<decimal>("Tolerancia"),
                             Medicion = info.Field<string>("Medicion"),
                             CodigoColor = info.Field<string>("CodigoColor"),
                             ColorDescripcion = info.Field<string>("ColorDescripcion"),
                             ColorObjetivoID = info.Field<int>("ColorObjetivoID"),
                             Indicador = info.Field<string>("Indicador"),
                             IndicadorID = info.Field<int>("IndicadorID"),
                             PedidoDetalleID = info.Field<int>("PedidoDetalleID"),
                             Tendencia = info.Field<string>("Tendencia"),
                             TipoObjetivoCalidad = info.Field<string>("TipoObjetivoCalidad"),
                             TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID")
                         }).ToList();
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
        public static IndicadorObjetivoInfo ObtenerPorFiltros(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorObjetivoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorObjetivoInfo
                         {
                             IndicadorObjetivoID = info.Field<int>("IndicadorObjetivoID"),
                             IndicadorProductoCalidad = new IndicadorProductoCalidadInfo { IndicadorProductoCalidadID = info.Field<int>("IndicadorProductoCalidadID") },
                             TipoObjetivoCalidad = new TipoObjetivoCalidadInfo { TipoObjetivoCalidadID = info.Field<int>("TipoObjetivoCalidadID"), Descripcion = info.Field<string>("TipoObjetivoCalidad") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             ObjetivoMinimo = info.Field<decimal>("ObjetivoMinimo"),
                             ObjetivoMaximo = info.Field<decimal>("ObjetivoMaximo"),
                             Tolerancia = info.Field<decimal>("Tolerancia"),
                             Medicion = info.Field<string>("Medicion"),
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
