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
    internal class MapObservacionDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ObservacionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ObservacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ObservacionInfo
                             {
								ObservacionID = info.Field<int>("ObservacionID"),
								Descripcion = info.Field<string>("Descripcion"),
								TipoObservacion = new TipoObservacionInfo { TipoObservacionID = info.Field<int>("TipoObservacionID"), Descripcion = info.Field<string>("TipoObservacion") },
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ObservacionInfo>
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
        internal static List<ObservacionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ObservacionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ObservacionInfo
                             {
								ObservacionID = info.Field<int>("ObservacionID"),
								Descripcion = info.Field<string>("Descripcion"),
								TipoObservacion = new TipoObservacionInfo { TipoObservacionID = info.Field<int>("TipoObservacionID"), Descripcion = info.Field<string>("TipoObservacion") },
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
        internal static ObservacionInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ObservacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ObservacionInfo
                             {
								ObservacionID = info.Field<int>("ObservacionID"),
								Descripcion = info.Field<string>("Descripcion"),
								TipoObservacion = new TipoObservacionInfo { TipoObservacionID = info.Field<int>("TipoObservacionID"), Descripcion = info.Field<string>("TipoObservacion") },
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
        internal static ObservacionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ObservacionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ObservacionInfo
                             {
								ObservacionID = info.Field<int>("ObservacionID"),
								Descripcion = info.Field<string>("Descripcion"),
								TipoObservacion = new TipoObservacionInfo { TipoObservacionID = info.Field<int>("TipoObservacionID"), Descripcion = info.Field<string>("TipoObservacion") },
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

