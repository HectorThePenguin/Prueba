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
    internal static class MapTipoMovimientoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoMovimientoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoMovimientoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 EsGanado = info.Field<bool>("EsGanado"),
                                 EsProducto = info.Field<bool>("EsProducto"),
                                 EsEntrada = info.Field<bool>("EsEntrada"),
                                 EsSalida = info.Field<bool>("EsSalida"),
                                 ClaveCodigo = info.Field<string>("ClaveCodigo"),
                                 TipoPoliza =
                                     new TipoPolizaInfo
                                         {
                                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                                             Descripcion = info.Field<string>("TipoPoliza")
                                         },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TipoMovimientoInfo>
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
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<TipoMovimientoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoMovimientoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoId"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 EsGanado = info.Field<bool>("EsGanado"),
                                 EsProducto = info.Field<bool>("EsProducto"),
                                 EsEntrada = info.Field<bool>("EsEntrada"),
                                 EsSalida = info.Field<bool>("EsSalida"),
                                 ClaveCodigo = info.Field<string>("ClaveCodigo"),
                                 TipoPoliza =
                                     new TipoPolizaInfo
                                         {
                                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                                             Descripcion = info.Field<string>("TipoPoliza"),
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
        ///     Método  que obtiene un registro de TipoMovimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoMovimientoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                TipoMovimientoInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoId"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 EsGanado = info.Field<bool>("EsGanado"),
                                 EsProducto = info.Field<bool>("EsProducto"),
                                 EsEntrada = info.Field<bool>("EsEntrada"),
                                 EsSalida = info.Field<bool>("EsSalida"),
                                 ClaveCodigo = info.Field<string>("ClaveCodigo"),
                                 TipoPoliza =
                                     new TipoPolizaInfo
                                         {
                                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                                           //  Descripcion = info.Field<string>("TipoPoliza"),
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
        ///     Método  que obtiene un registro de TipoMovimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoMovimientoInfo ObtenerSoloTipoMovimiento(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                TipoMovimientoInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoId"),
                                 Descripcion = info.Field<string>("Descripcion"),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoMovimientoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoMovimientoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoMovimientoInfo
                             {
                                 TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 EsGanado = info.Field<bool>("EsGanado"),
                                 EsProducto = info.Field<bool>("EsProducto"),
                                 EsEntrada = info.Field<bool>("EsEntrada"),
                                 EsSalida = info.Field<bool>("EsSalida"),
                                 ClaveCodigo = info.Field<string>("ClaveCodigo"),
                                 TipoPoliza =
                                     new TipoPolizaInfo
                                         {
                                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                                             Descripcion = info.Field<string>("TipoPoliza")
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
        /// Obtiene una lista con los tipos de movimiento
        /// que se mostraran en Calidad pase Proceso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<TipoMovimientoInfo> ObtenerTipoMovimientoCalidadPasaeProceso(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoMovimientoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoMovimientoInfo
                         {
                             TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                             Descripcion = info.Field<string>("Descripcion"),
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
