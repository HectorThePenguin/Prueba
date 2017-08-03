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
    internal  class MapCausaSalidaDAL
    {

        internal  static CausaSalidaInfo ObtenerPorTipoMovimiento(DataSet ds)
        {
            CausaSalidaInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new CausaSalidaInfo
                                 {
                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     Descripcion = info.Field<string>("Descripcion"),
                                     CausaSalidaID = info.Field<int>("CausaSalidaID"),
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }


        internal  static List<CausaSalidaInfo> ObtenerPorTipoMovimientoLista(DataSet ds)
        {
            List<CausaSalidaInfo> resultado = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new CausaSalidaInfo
                                 {
                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     Descripcion = info.Field<string>("Descripcion"),
                                     CausaSalidaID = info.Field<int>("CausaSalidaID"),
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }


        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CausaSalidaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CausaSalidaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CausaSalidaInfo
                         {
                             CausaSalidaID = info.Field<int>("CausaSalidaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("TipoMovimiento") },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CausaSalidaInfo>
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
        internal  static List<CausaSalidaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CausaSalidaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CausaSalidaInfo
                         {
                             CausaSalidaID = info.Field<int>("CausaSalidaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("TipoMovimiento") },
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
        internal  static CausaSalidaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CausaSalidaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CausaSalidaInfo
                         {
                             CausaSalidaID = info.Field<int>("CausaSalidaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("TipoMovimiento") },
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
        internal  static CausaSalidaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CausaSalidaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CausaSalidaInfo
                         {
                             CausaSalidaID = info.Field<int>("CausaSalidaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("TipoMovimiento") },
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
        internal  static CausaSalidaInfo ObtenerPorTipoMovimientoDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CausaSalidaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CausaSalidaInfo
                         {
                             CausaSalidaID = info.Field<int>("CausaSalidaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoMovimiento = new TipoMovimientoInfo { TipoMovimientoID = info.Field<int>("TipoMovimientoID"), Descripcion = info.Field<string>("TipoMovimiento") },
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
