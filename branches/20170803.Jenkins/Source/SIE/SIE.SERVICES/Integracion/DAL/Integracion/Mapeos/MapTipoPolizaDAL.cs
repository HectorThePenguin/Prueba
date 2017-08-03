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
    internal static class MapTipoPolizaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoPolizaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoPolizaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoPolizaInfo
                         {
                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ClavePoliza = info.Field<string>("ClavePoliza"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TipoPolizaInfo>
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
        internal static List<TipoPolizaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoPolizaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoPolizaInfo
                         {
                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ClavePoliza = info.Field<string>("ClavePoliza"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             PostFijoRef3 = info.Field<string>("PostFijoRef3"),
                             TextoDocumento = info.Field<string>("TextoDocumento"),
                             ImprimePoliza = info.Field<bool>("ImprimePoliza")
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
        internal static TipoPolizaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoPolizaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoPolizaInfo
                         {
                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ClavePoliza = info.Field<string>("ClavePoliza"),
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
        internal static TipoPolizaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoPolizaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoPolizaInfo
                         {
                             TipoPolizaID = info.Field<int>("TipoPolizaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ClavePoliza = info.Field<string>("ClavePoliza"),
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
